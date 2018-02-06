var urlBase = utils.getBaseUrl();
var urlAusencias = '/Ausencias';

$(document).ready(function () {
    //jQuery.validator.addMethod("validaSexo", function (value, element, param) {
    //    if (this.optional(element)) {
    //        return true;
    //    }
    //    var valor;
    //    if (value == 'M' || value == 'F')
    //        valor = value;
    //    return valor;
    //}, "Selecciones una opción.");

    $("#form_reg_ausen").validate({
        rules: {
            DatosTrabajor_Salario: {
                required: true
            },
            idSede: {
                required: true, min: 1
            },
            idProceso: {
                required: true
            },
            IdContingencia: {
                required: true
            },
            FechaInicio: {
                required: true
            },
            FechaFin: {
                required: true
            },
            //IdEmpresaUsuaria: {
            //    required: true, min: 1
            //},
            //IdDiagnostico: {
            //    required: true, min: 1
            //},
            //DatosTrabajor_Sexo: {
            //    required: true, validaSexo: '([0-2][0-9]+(?=:))|([0-5][0-9])'
            //},
        },
        messages: {
            DatosTrabajor_Salario: {
                required: "Este compo es obligatorio",
            },
            idSede: {
                required: "Debe seleccionar una sede",
                min: "Debe seleccionar una sede"
            },
            idProceso: {
                required: "Debe seleccionar un area"
                //min: "Debe seleccionar un area"
            },
            IdContingencia: {
                required: "Debe seleccionar una contingencia"
            },
            FechaInicio: {
                required: "Debe seleccionar una fecha de inicio"
            },
            FechaFin: {
                required: "Debe seleccionar una fecha de finalización"
            },
            //IdEmpresaUsuaria: {
            //    required: "Debe seleccionar una empresa asociada",
            //    min: "Debe seleccionar una empresa asociada"
            //},
            //IdDiagnostico: {
            //    required: "El diagnostico ingersado no es valido",
            //    min: "El diagnostico ingersado no es valido"
            //},
            //DatosTrabajor_Sexo: {
            //    required: "Debe seleccionar una opción",
            //    validaSexo: "Debe seleccionar una opcioón"
            //},
        }
    });


    $('#form_reg_ausen').find('input[type="text"]').on('focus', function () {
        $(this).siblings().hide();
    });
    ConstruirDatePickerPorElemento('FechaInicio');
    ConstruirDatePickerPorElemento('FechaFin');
    $("#IdContingencia").on('change', function () {
        var valorContingencia = $('#IdContingencia option:selected').val();
        if (valorContingencia == '1' || valorContingencia == '2' || valorContingencia == '3') {
            $('#Diagnostico').show();
            $('#labelDiag').show();
        } else {
            $('#Diagnostico').hide();
            $('#labelDiag').hide();
        }
        var textoContingencia = $("#IdContingencia option:selected").text();
        var sx = $('#DatosTrabajor_Sexo').val();
        if (textoContingencia == 'Licencia de maternidad' && sx == 'M') {
            swal("Atención", 'Esta contingecia no puede ser asignada a este Trabajador.')
            $("#IdContingencia").val("");
        } else if (textoContingencia == 'Licencia de paternidad' && sx == 'F') {
            swal("Atención", 'Esta contingecia no puede ser asignada a este Afiliado');
            $("#IdContingencia").val("");
        } else if (textoContingencia == 'Permiso por horas Día') {
            $('#conf_horas').show();
        }
        else if (textoContingencia != 'Permiso por horas Día') {
            $('#conf_horas').hide();
        }
        //borrar los campos debido a que se cambio de contingencia
        $('#FechaInicio').val('');
        $('#FechaFin').val('');
        $('#DiasAusencia').val('');
        $('#FactorPrestacional').val('');
        $('#Costo').val('');
        $('#Observaciones').val('');
        if ($('#Diagnostico').length > 0)
            $('#Diagnostico').val('');
        if ($('#HoraInicio').length > 0)
            $('#HoraInicio').val('');
        if ($('#HoraFinalizacion').length > 0)
            $('#HoraFinalizacion').val('');
    });
    $("#Diagnostico").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: urlBase + urlAusencias + "/AutoCompletarDiagnostico",
                type: "POST",
                dataType: "json",
                data: { prefijo: request.term },
            }).done(function (data) {
                $("#IdDiagnostico").val(0);
                response($.map(data, function (item) {
                    return { label: item.Codigo + "-" + item.Descripcion, value: item.IdDiagnostico };
                }))
            })
        },
        focus: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
        },
        select: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
            $("#IdDiagnostico").val(ui.item.value);
            $('#errordiagnostico').hide();
        }
    });



    $('#FechaFin').on("change", function (e) {
        console.log(4);
        var f1 = document.getElementById("FechaInicio").value;
        var f2 = document.getElementById("FechaFin").value;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        if (f2 == '')
            return false;

        var horaInicio = '';
        var horaFin = '';
        var idTipoConting = $('#IdContingencia').val();
        if (idTipoConting == '') {
            $('#DiasAusencia').val('');
            swal("Atención", 'Debe Seleccionar una contingecia.');
            return false;
        }

        if (fecha2.getTime() < fecha1.getTime()) {
            swal("Atención", 'La Fecha de finalizacion no puede ser menor a la inicial');
            return false;
        }

        if (idTipoConting == '9') {
            horaInicio = $('#HoraInicio').val();
            horaFin = $('#HoraFinalizacion').val();
        }
        if (f1 == '' || f2 == '') {
            $('#DiasAusencia').val('');
            swal("Atención", 'Debe seleccionar las Fechas Inicio y Fin.');
            OcultarPopupposition();
            e.stopPropagation();
        }
        else if (fecha1.getTime() <= fecha2.getTime() && idTipoConting != '9') {
            PopupPosition();
            
            var objNuevaAusencia = {
                Documento: $('#Documento').val(),
                IdEmpresa: $('#IdEmpresa').val(),
                FechaInicio: $('#FechaInicio').val(),
                FechaFin: $('#FechaFin').val(),
            };

            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + urlAusencias + '/ValidarCruceDeFechas',
                data: objNuevaAusencia
            }).done(function (response) {
                if (response != undefined && response.status == 'Success') {
                    CalcularDias(f1, f2, idTipoConting, horaInicio, horaFin, 'DiasAusencia', 'FechaInicio', 'FechaFin');
                    var salario = $("#DatosTrabajor_Salario").val();
                    var facPres = $("#FactorPrestacional").val();
                    var diasa = $('#DiasAusencia').val();
                    var cont = $("#IdContingencia").val();
                    if (diasa != 126 && idTipoConting == 4) {
                        $('#DiasAusencia').val("");
                        $('#FechaFin').val('');
                        swal("Atención", 'La Licencia de maternidad debe tener exactamente 126 dias de incapacidad, actualmente esta ingresando ' + diasa + ' dias, Verifique las fechas');
                        OcultarPopupposition();
                        return false;
                    } else if (diasa != 8 && idTipoConting == 5) {
                        $('#DiasAusencia').val("");
                        $('#FechaFin').val('');
                        swal("Atención", 'La Licencia de paternidad debe tener exactamente 8 dias de incapacidad, Verifique las fechas');
                        OcultarPopupposition();
                        return false;
                    } else if (diasa != 5 && idTipoConting == 7) {
                        $('#DiasAusencia').val("");
                        $('#FechaFin').val('');
                        swal("Atención", 'La Licencia de luto debe tener exactamente 5 dias de incapacidad, Verifique las fechas');
                        OcultarPopupposition();
                        return false;
                    }
                    var salarioNumerico = salario.replace("$", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    costo(salarioNumerico, facPres, diasa);
                    OcultarPopupposition();
                    e.stopPropagation();
                }
                else if (response != undefined && response.status == 'CRUCE') {
                    swal("Estimado Usuario", '"El afiliado ya presenta ausetimos registrados en las fechas ingresadas.');
                    $('#FechaFin').val('');
                    OcultarPopupposition();
                    return false;
                }
            })
        } else {
            if (idTipoConting == '9') {
                if (f1 != f2) {
                    $('#FechaFin').val('');
                    swal("Atención", 'En este tipo de contingecia la Fecha Incio y Fin deben ser iguales.');
                    return false;
                }
            } else {
                $('#FechaFin').val('');
                swal("Atención", 'La Fecha de finalizacion no puede ser menor a la inicial');
                return false;
            }
        }
    });

    $('#FechaInicio').on("change", function (e) {
        var f1 = document.getElementById("FechaInicio").value;
        var f2 = document.getElementById("FechaFin").value;

        if (f2 == '')
            return false;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        var horaInicio = '';
        var horaFin = '';
        var idTipoConting = $('#IdContingencia').val();
        if (idTipoConting == '') {
            $('#DiasAusencia').val('');
            swal("Atención", 'Debe Seleccionar una contingecia.');
            return false;
        }

        if (fecha2.getTime() < fecha1.getTime()) {
            $('#FechaFin').val('');
            swal("Atención", 'La Fecha de finalizacion no puede ser menor a la inicial');
            return false;
        }

        if (idTipoConting == '9') {
            horaInicio = $('#HoraInicio').val();
            horaFin = $('#HoraFinalizacion').val();
        }
        if (f1 == '' || f2 == '') {
            $('#DiasAusencia').val('');
            swal("Atención", 'Debe seleccionar las Fechas Inicio y Fin.');
            OcultarPopupposition();
            e.stopPropagation();
        }
        else if (fecha1.getTime() < fecha2.getTime() && idTipoConting != '9') {
            PopupPosition();
            
            var objNuevaAusencia = {
                Documento: $('#Documento').val(),
                IdEmpresa: $('#IdEmpresa').val(),
                FechaInicio: $('#FechaInicio').val(),
                FechaFin: $('#FechaFin').val(),
            };

            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + urlAusencias + '/ValidarCruceDeFechas',
                data: objNuevaAusencia
            }).done(function (response) {
                if (response != undefined && response.status == 'Success') {
                    CalcularDias(f1, f2, idTipoConting, horaInicio, horaFin, 'DiasAusencia', 'FechaInicio', 'FechaFin');
                    var salario = $("#DatosTrabajor_Salario").val();
                    var facPres = $("#FactorPrestacional").val();
                    var diasa = $('#DiasAusencia').val();
                    var cont = $("#IdContingencia").val();
                    if (diasa != 126 && idTipoConting == 4) {
                        $('#DiasAusencia').val("");
                        $('#FechaFin').val('');
                        swal("Atención", 'La Licencia de maternidad debe tener exactamente 126 dias de incapacidad, actualmente esta ingresando ' + diasa + ' dias, Verifique las fechas');
                        return false;
                    } else if (diasa != 8 && idTipoConting == 5) {
                        $('#DiasAusencia').val("");
                        $('#FechaFin').val('');
                        swal("Atención", 'La Licencia de paternidad debe tener exactamente 8 dias de incapacidad, Verifique las fechas');
                        return false;
                    } else if (diasa != 5 && idTipoConting == 7) {
                        $('#DiasAusencia').val("");
                        $('#FechaFin').val('');
                        swal("Atención", 'La Licencia de luto debe tener exactamente 5 dias de incapacidad, Verifique las fechas');
                        return false;
                    }
                    var salarioNumerico = salario.replace("$", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    salarioNumerico = salarioNumerico.replace(".", "");
                    costo(salarioNumerico, facPres, diasa);
                    OcultarPopupposition();
                    e.stopPropagation();
                }
                else if (response != undefined && response.status == 'CRUCE') {
                    swal("Estimado Usuario", '"El afiliado ya presenta ausetimos registrados en las fechas ingresadas.');
                    $('#FechaFin').val('');
                    OcultarPopupposition();
                    return false;
                }
            })
        } else {
            if (idTipoConting == '9') {
                if (f1 != f2) {
                    swal("Atención", 'En este tipo de contingecia la Fecha Incio y Fin deben ser iguales.');
                    return false;
                }
            } else {
                $('#DiasAusencia').val("");
                $('#FechaFin').val('');
                swal("Atención", 'La Fecha de finalizacion no puede ser menor a la inicial');
                return false;
            }
        }
    });

    $('#DatosTrabajor_Salario').on("change", function () {
        var f1 = document.getElementById("FechaInicio").value;
        var f2 = document.getElementById("FechaFin").value;
        var horaInicio = '';
        var horaFin = '';

        var idTipoConting = $('#IdContingencia').val();
        if (idTipoConting == '9') {
            horaInicio = $('#HoraInicio').val();
            horaFin = $('#HoraFinalizacion').val();
        }

        if (f2 == '' && f1 == '')
            return false;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        if (fecha1.getTime() < fecha2.getTime() && idTipoConting != '9') {
            PopupPosition();
            CalcularDias(f1, f2, idTipoConting, horaInicio, horaFin, 'DiasAusencia', 'FechaInicio', 'FechaFin');
            var salario = $("#DatosTrabajor_Salario").val();
            var facPres = $("#FactorPrestacional").val();
            var diasa = $('#DiasAusencia').val();
            var cont = $("#IdContingencia").val();
            var salarioNumerico = salario.replace("$", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            costo(salarioNumerico, facPres, diasa);
            OcultarPopupposition();
        }
    });

    $('#FactorPrestacional').on("change", function () {
        var f1 = document.getElementById("FechaInicio").value;
        var f2 = document.getElementById("FechaFin").value;

        var horaInicio = '';
        var horaFin = '';

        var idTipoConting = $('#IdContingencia').val();
        if (idTipoConting == '9') {
            horaInicio = $('#HoraInicio').val();
            horaFin = $('#HoraFinalizacion').val();
        }

        if (f2 == '' && f1 == '')
            return false;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);


        if (fecha1.getTime() < fecha2.getTime() && idTipoConting != '9') {
            PopupPosition();
            CalcularDias(f1, f2, idTipoConting, horaInicio, horaFin, 'DiasAusencia', 'FechaInicio', 'FechaFin');
            var salario = $("#DatosTrabajor_Salario").val();
            var facPres = $("#FactorPrestacional").val();
            var diasa = $('#DiasAusencia').val();
            var cont = $("#IdContingencia").val();
            var salarioNumerico = salario.replace("$", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            costo(salarioNumerico, facPres, diasa);
            OcultarPopupposition();
        }
    });



    $('#HoraInicio').on('change', function () {
        $('#HoraFinalizacion').val('');
    });
    $('#HoraFinalizacion').on('change', function () {
        var f1 = $("#FechaInicio").val();
        var f2 = $("#FechaFin").val();
        var salario = $("#DatosTrabajor_Salario").val();
        var facPres = $("#FactorPrestacional").val();
        var idTipoConting = $('#IdContingencia').val();
        var horaInicio = $('#HoraInicio option:selected').val();
        var horaFin = $('#HoraFinalizacion option:selected').val();
        if (f1 == '' || f2 == '') {
            $('#HoraInicio').val('');
            $('#HoraFinalizacion').val('');
            $("#FechaInicio").val('');
            $("#FechaFin").val('');
            swal("Atención", 'Debe seleccionar una Fecha para la Ausencia.');
            return false;
        }
        if (horaInicio == '') {
            $('#HoraInicio').val('');
            $('#HoraFinalizacion').val('');
            swal("Atención", 'Debe seleccionar una Hora de Inicio.');
            return false;
        } else if (parseInt(horaInicio) >= parseInt(horaFin)) {
            $('#HoraInicio').val('');
            $('#HoraFinalizacion').val('');
            swal("Atención", 'La Hora Inicio debe ser menor que la Hora fin.');
            return false;
        }
        // else if (horaInicio == '5') {
        //    $('#HoraInicio').val('');
        //    $('#HoraFinalizacion').val('');
        //    swal("Atención", 'La Hora de Inicio que seleccionó está por fuera del rango de horas laborales.');
        //    return false;
        //}

        var catidadTotal = 8;
        var horaspermiso = parseInt(horaFin) - parseInt(horaInicio)
        if (parseInt(horaInicio) >= 2 && parseInt(horaInicio) < 7 && parseInt(horaFin) > 7)
            catidadTotal = 9
        if (horaspermiso > catidadTotal) {
            $('#HoraInicio').val('');
            $('#HoraFinalizacion').val('');
            swal("Atención", 'El permiso no debe exceder las 8 horas laborables por dia.');
            return false;
        } else {
            PopupPosition();
            CalcularDias(f1, f2, idTipoConting, horaInicio, horaFin, 'DiasAusencia', 'FechaInicio', 'FechaFin');
            var diasa = $('#DiasAusencia').val();
            var salarioNumerico = salario.replace("$", "").replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            salarioNumerico = salarioNumerico.replace(".", "");
            costo(salarioNumerico, facPres, diasa);
            OcultarPopupposition();
        }
    });

    $("#NuevaAusencia").click(function (event) {
        var validacion = true;
        var diagno = new Object();
        var visible = $('#Diagnostico').is(':visible');
        var diag = $('#IdDiagnostico').val();
        if (visible) {
            if (diag == undefined || diag < 1) {
                $('#errordiagnostico').show();
                validacion = false;
            }
        }
        if (!$("#form_reg_ausen").valid()) {
            var diag = $('#IdDiagnostico').val();
            if (visible) {
                if (diag == undefined || diag < 1)
                    $('#errordiagnostico').show();
            }
            validacion = false;
        }
        if ($("#IdContingencia").val() != '') {
            var cont = $('#IdContingencia').val();
            if ((cont == 1 || cont == 2 || cont == 3)) {
                if ($("#Diagnostico").val() != '') {
                    diagno.IdDiagnoticoSeleccionado = $("#IdDiagnostico").val();
                    diagno.TipoDiagnostico = $("#Diagnostico").val();
                }
            } else {
                diagno.IdDiagnoticoSeleccionado = 0;
                diagno.TipoDiagnostico = 'No Aplica';
            }
        }
        if ($("#IdContingencia").val() != '' && $("#IdContingencia").val() == 9) {
            if ($("#HoraInicio").val() == '') {
                var elmError = $("#HoraInicio").siblings('label[class="error"]');
                if (elmError.length == 0) {
                    var msgError = '<label for="HoraInicio" class="error">Debe seleccionar una Hora de Inicio.</label>';
                    $('#HoraInicio').after(msgError);
                }
                $('label[for="HoraInicio"]').show();
                validacion = false;
            }

            if ($("#HoraFinalizacion").val() == '') {
                var elmError = $("#HoraFinalizacion").siblings('label[class="error"]');
                if (elmError.length == 0) {
                    var msgError = '<label for="HoraFinalizacion" class="error">Debe seleccionar una Hora Fin.</label>';
                    $('#HoraFinalizacion').after(msgError);
                }
                $('label[for="HoraFinalizacion"]').show();
                validacion = false;
            }
        }
        if (!validacion)
            return false;
        var DiasAusencia = $('#DiasAusencia').val();
        if (DiasAusencia == "") {
            swal("Atención", 'El campo dias de ausencia esta vacio, por favor verifique las fechas o el campo salario');
            $("#FechaFin").val("");
            $("#FechaInicio").val("");
            return false;
        }
        NuevaAusencia(diagno);
    });
});
//calcula el costo de la Susencia
function costo(salario, facPres, diasa) {
    if (facPres == '' || facPres == undefined) {
        facPres = parseFloat(1.00);
        $("#FactorPrestacional").val(facPres);
        var fin = Math.floor(((salario / 30) * diasa) * facPres);
        var total = parseFloat(fin);
        $('#Costo').val('');
        $('#Costo').val(total);
        EtiquetarValoresAPrecio('Costo');
    } else {
        var facPres = parseFloat($("#FactorPrestacional").val());
        if (facPres < 1) {
            swal("Atención", 'El Factor Prestacional debe ser mayor o igual a 1.00');
            return false;
        } else
            var fin = Math.floor(((salario / 30) * diasa) * facPres)
        console.log(diasa);
        var total = parseFloat(fin)
        $('#Costo').val('');
        $('#Costo').val(total);
        EtiquetarValoresAPrecio('Costo');
    }
}


//realiza la peticion para registrar la nueva ausencia
function NuevaAusencia(diagno) {

    var fechaNacimiento = $("#DatosTrabajor_FechaNacimiento").val();
    var fecha = new Date(fechaNacimiento.split('/')[2], fechaNacimiento.split('/')[1] - 1, fechaNacimiento.split('/')[0]);
    var hoy = new Date();

    var edad = parseInt((hoy - fecha) / 365 / 24 / 60 / 60 / 1000);
    $('#DatosTrabajor_Salario').val($('#DatosTrabajor_Salario').val().replace("$", ""));
    $('#DatosTrabajor_Salario').val($('#DatosTrabajor_Salario').val().replace(".", ""));
    $('#DatosTrabajor_Salario').val($('#DatosTrabajor_Salario').val().replace(".", ""));
    $('#DatosTrabajor_Salario').val($('#DatosTrabajor_Salario').val().replace(".", ""));
    $('#DatosTrabajor_Salario').val($('#DatosTrabajor_Salario').val().replace(".", ""));
    $('#Costo').val($('#Costo').val().replace("$", ""));
    $('#Costo').val($('#Costo').val().replace(".", ""));
    $('#Costo').val($('#Costo').val().replace(".", ""));
    $('#Costo').val($('#Costo').val().replace(".", ""));
    $('#Costo').val($('#Costo').val().replace(".", ""));

    var objNuevaAusencia = {
        Documento: $('#Documento').val(),
        IdEmpresa: $('#IdEmpresa').val(),
        IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
        TipoVinculacion: $('#TipoVinculacion option:selected').val(),
        idDepartamento: $('#Departamento option:selected').val(),
        idMunicipio: $('#Municipio option:selected').val(),
        DatosTrabajor: {
            Nombre1: $('#DatosTrabajor_Nombre1').val()
        },
        Contingencia: {
            IdContingenciaSeleccionada: $('#IdContingencia').val(),
        },
        Diagnostico: {
            IdDiagnoticoSeleccionado: diagno.IdDiagnoticoSeleccionado,
            TipoDiagnostico: diagno.TipoDiagnostico
        },
        idSede: $('#idSede').val(),
        idProceso: $('#idProceso').val(),
        FechaInicio: $('#FechaInicio').val(),
        FechaFin: $('#FechaFin').val(),
        DiasAusencia: $('#DiasAusencia').val(),
        Costo: $('#Costo').val(),
        FactorPrestacional: $('#FactorPrestacional').val(),
        Observaciones: $('#Observaciones').val(),
        Sexo: $('#DatosTrabajor_Sexo').val(),
        Ocupacion: $('#DatosTrabajor_Ocupacion').val(),
        Eps: $('#DatosTrabajor_NombreEps').val(),
        Edad: edad
    };

    PopupPosition();
    $.ajax({
        type: "post",
        url: urlBase + urlAusencias + '/NuevaAusencia',
        data: objNuevaAusencia
    }).done(function (response) {
        if (response != undefined && response != '' && response.status == 'Success') {
            OcultarPopupposition();
            swal({
                title: 'Atención',
                text: response.Message,
                type: 'success',
                showCancelButton: false
            }, function () {
                window.location.href = urlBase + urlAusencias + "/RegistrarAusencia";
            });
        }
        else if (response != undefined && response != '' && response.status == 'CRUCE') {
            OcultarPopupposition();
            swal({
                title: 'Atención',
                text: response.Message,
                type: 'warning',
                showCancelButton: false
            });
        }
        else if (response != undefined && response != '' && response.status == 'Error') {
            OcultarPopupposition();
            swal({
                title: 'Atención',
                text: response.Message,
                type: 'warning',
                showCancelButton: false
            });
        }
    }).fail(function (response) {
        console.log("Error en la peticion: " + response.Data);
        OcultarPopupposition();
        swal({
            title: 'Atención',
            text: 'No se logró registar la ausencia. Intente nuevamente',
            type: 'warning',
            showCancelButton: false
        }, function () {
            window.location.href = urlBase + urlAusencias + "/RegistrarAusencia";
        });
    });
}