var urlBase = utils.getBaseUrl();
var urlEstudioPT = '/EstudioPuestoTrabajo';
var urlAfiliado = '/Ausencias';

$(document).ready(function () {
    ConstruirDatePickerPorElemento('FechaAnalisis');

    $("#form_reg_estudio").validate({
        rules: {
            Documento: {
                required: true
            },
            idSede: {
                required: true
            },
            idProceso: {
                required: true
            },
            idObjetivo: {
                required: true
            },
            idTipoAnalisisPT: {
                required: true
            }
        },
        messages: {
            Documento: {
                required: "El número de identificación es obligatorio"
            },
            idSede: {
                required: "La sede es obligatoria"
            },
            idProceso: {
                required: "El proceso es obligatorio"
            },
            idObjetivo: {
                required: "El Objetivo del Análisis es obligatorio"
            },
            idTipoAnalisisPT: {
                required: "El Tipo de Análisis es obligatorio"
            }
        }
    });

    $('#Documento').on('change', function () {
        var documento = $("#Documento").val();
        if (documento != '') {
            DatosTrabajador(documento);
        } else
            swal('Estimado Usuario', 'Seleccione un valor válido para continuar.');
        return false;
    });

    $("#Documento").keypress(function (tecla) {
        if (tecla.charCode < 47 || tecla.charCode > 57) return false;
    });

    $("#txtDocumento").keypress(function (tecla) {
        if (tecla.charCode < 47 || tecla.charCode > 57) return false;
    });

    function DatosTrabajador(documento) {
        if (documento == '')
            swal("Estimado Usuario", 'Este campo debe ser obligatorio');
        PopupPosition();
        $.ajax({
            type: "post",
            data: { numeroDocumento: documento },
            url: urlBase + urlAfiliado + '/ConsultarDatosTrabajador'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                var trabajador = response.Data;
                var nombre = trabajador.nombre1 + ' ' + trabajador.apellido1;
                $('#Apellido1').val(trabajador.apellido1);
                $('#Apellido2').val(trabajador.apellido2);
                $('#Nombre1').val(trabajador.nombre1);
                $('#Nombre2').val(trabajador.nombre2);
                $('#Cargo').val(trabajador.cargo);
            }
            else if (response != undefined && response != '' && response.Mensaje != '') {
                $("#Documento").val('');
                swal("Estimado Usuario", response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            $("#Documento").val('');
            swal("Estimado Usuario", "No se logró obtener datos del trabajador. Intente más tarde.");
            OcultarPopupposition();
        });
    }

    $("#Diagnostico").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: urlBase + urlAfiliado + "/AutoCompletarDiagnostico",
                type: "POST",
                dataType: "json",
                data: { prefijo: request.term },
            }).done(function (data) {
                //$("#IdDiagnostico").val(0);
                //debugger;
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

    $("#txtCargo").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: urlBase + urlEstudioPT + "/BuscarCargo",
                type: "POST",
                dataType: "json",
                data: { prefijo: request.term },
            }).done(function (Data) {
                $("#IdCargo").val(0);
                //debugger;
                response($.map(Data.Data, function (item) {
                    return { label: item.Cargo, value: item.Cargo };
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
            $("#IdCargo").val(ui.item.value);
            $('#errorcargo').hide();
        }
    });

    $("#AgregarSeguimiento").click(function () {
        FrmSeguimiento();
    });

    function FrmSeguimiento() {
        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/RegistrarNuevoSeguimiento'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#datosSeguimiento').empty();
                $('#datosSeguimiento').html(response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            swal("Estimado Usuario", "No se logró consultar la información del Seguimiento. Intente más tarde.");
            OcultarPopupposition();
        });
    }

    $("#btnGuardar").click(function (event) {
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

        if (!$("#form_reg_estudio").valid()) {
            var diag = $('#IdDiagnostico').val();
            if (visible) {
                if (diag == undefined || diag < 1)
                    $('#errordiagnostico').show();
            }
            validacion = false;
        }
            
        if (!validacion)
           return false;
            

        var objNuevoEstudio = {
            Documento: $('#Documento').val(),
            Apellido1: $('#Apellido1').val(),
            Apellido2: $('#Apellido2').val(),
            Nombre1: $('#Nombre1').val(),
            Nombre2: $('#Nombre2').val(),
            Cargo: $('#Cargo').val(),
            FechaAnalisisStr: $('#FechaAnalisis').val(),
            idSede: $('#idSede').val(),
            idProceso: $('#idProceso').val(),
            idDiagnostico: $('#IdDiagnostico').val(),
            idObjetivo: $('#idObjetivo').val(),
            idTipoAnalisisPT: $('#idTipoAnalisisPT').val()
            
        };

        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/NuevoEstudio',
            data: objNuevoEstudio
        }).done(function (response) {
            if (response != undefined && response != '' && response.status == 'Success') {
                OcultarPopupposition();
                $("#IdEstudioPT").val(response.Id);
                swal({
                    title: 'Estimado Usuario',
                    text: response.Message,
                    type: 'success',
                    showCancelButton: false
                });
                $("#btnArchivos").show();
                var valorObjetivo = $('#idObjetivo :selected').text();
                if (valorObjetivo !== 'Definicion de Origen') {
                    $("#AgregarSeguimiento").show();
                }
                
            }
            else if (response != undefined && response != '' && response.status == 'Error') {
                OcultarPopupposition();
                swal({
                    title: 'Estimado Usuario',
                    text: response.Message,
                    type: 'warning',
                    showCancelButton: false
                });
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition();
            swal({
                title: 'Estimado Usuario',
                text: 'No se logró registar el estudio de Puesto de Trabajo. Intente nuevamente',
                type: 'warning',
                showCancelButton: false
            });
        });
    });

    $('#AgregarArchivo').click(function () {
        if (window.FormData !== undefined) {
            var fileUpload = $("#UploadFile").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            fileData.append("Id", $("#IdEstudioPT").val());
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + urlEstudioPT + '/GuardarArchivos',
                contentType: false,
                processData: false,
                data: fileData
            }).done(function (response) {
                if (response != undefined && response != '' && response.status == 'Success') {
                    OcultarPopupposition();
                    swal({
                        title: 'Estimado Usuario',
                        text: response.Message,
                        type: 'success',
                        showCancelButton: false
                    });
                    GridArchivos();
                    $("#UploadFile").val('');
                }
                else if (response != undefined && response != '' && response.status == 'Error') {
                    OcultarPopupposition();
                    swal({
                        title: 'Estimado Usuario',
                        text: response.Message,
                        type: 'warning',
                        showCancelButton: false
                    });
                }
                OcultarPopupposition();
            }).fail(function (response) {
                swal("Estimado Usuario", "No se logró subir los archivos. Intente más tarde.");
                OcultarPopupposition();
            });
        };
    });

    $("#btnBuscar").click(function () {
        MostrarInfo();
        $("#btnSiguiente").show();
        
    });

    $("#btnRealizarBusqueda").click(function () {
        var isVisible = $("#buscarInfo").is(":visible");
        $('#Documento').val('');
        $('#Apellido1').val('');
        $('#Apellido2').val('');
        $('#Nombre1').val('');
        $('#Nombre2').val('');
        $('#Cargo').val('');
        $('#idSede').val('');
        $('#idProceso').val('');
        $('#idObjetivo').val('');
        $('#idTipoAnalisisPT').val('');
        $("#Diagnostico").val('');
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        
        var output = (day < 10 ? '0' : '') + day + '/' +
            (month < 10 ? '0' : '') + month + '/' +
            d.getFullYear();

        $("#FechaAnalisis").val(output);
        $("#IdEstudioPT").val('');
        $('#GridSeguimiento').empty();
        $('#GridArchivos').empty();
        $("#btnSiguiente").hide();
        $("#btnAnterior").hide();
        if (isVisible) {
            $("#Documento").prop('disabled', false);
            $("#Documento").removeClass("form-control bloqueado");
            $("#Documento").addClass("form-control");
            $("#btnGuardar").show();
            $("#idSede").prop('disabled', false);
            $("#idSede").removeClass("form-control bloqueado");
            $("#idSede").addClass("form-control");
            $("#idProceso").prop('disabled', false);
            $("#idProceso").removeClass("form-control bloqueado");
            $("#idProceso").addClass("form-control");
            $("#Diagnostico").prop('disabled', false);
            $("#Diagnostico").removeClass("form-control bloqueado");
            $("#Diagnostico").addClass("form-control");
            $("#idObjetivo").prop('disabled', false);
            $("#idObjetivo").removeClass("form-control bloqueado");
            $("#idObjetivo").addClass("form-control");
            $("#idTipoAnalisisPT").prop('disabled', false);
            $("#idTipoAnalisisPT").removeClass("form-control bloqueado");
            $("#idTipoAnalisisPT").addClass("form-control");
            
        }
        else {
            $("#Documento").prop('disabled', true);
            $("#Documento").removeClass("form-control");
            $("#Documento").addClass("form-control bloqueado");
            $("#btnGuardar").hide();
            $("#idSede").prop('disabled', true);
            $("#idSede").removeClass("form-control");
            $("#idSede").addClass("form-control bloqueado");
            $("#idProceso").prop('disabled', true);
            $("#idProceso").removeClass("form-control");
            $("#idProceso").addClass("form-control bloqueado");
            $("#Diagnostico").prop('disabled', true);
            $("#Diagnostico").removeClass("form-control");
            $("#Diagnostico").addClass("form-control bloqueado");
            $("#idObjetivo").prop('disabled', true);
            $("#idObjetivo").removeClass("form-control");
            $("#idObjetivo").addClass("form-control bloqueado");
            $("#idTipoAnalisisPT").prop('disabled', true);
            $("#idTipoAnalisisPT").removeClass("form-control");
            $("#idTipoAnalisisPT").addClass("form-control bloqueado");
        }
               
        $("#btnArchivos").hide();
        $('#informeAPT').collapse({ toggle: false });
        $("#AgregarSeguimiento").hide();
        $("#pnlSeguimiento").collapse({ toggle: false });
       

    });

    $("#btnSiguiente").click(function () {
        var infocont = 0;
        var numreg = 0;
        var infocont = parseInt($("#RegistroPT").val(), 10);
        var numreg = parseInt($("#NumRegistrosPT").val(), 10);
        if (infocont < numreg) {
            ++infocont;
        }
        
        $('#RegistroPT').val(infocont);
        $("#btnAnterior").show();
        MostrarInfo();

    });

    $("#btnAnterior").click(function () {
        var infocont = 0;
        var infocont = parseInt($("#RegistroPT").val(), 10);
        if (infocont > 0)
            --infocont;
        $('#RegistroPT').val(infocont);
        MostrarInfo();

    });

    function MostrarInfo() {
        var contador = $("#RegistroPT").val();
        if ($('#txtDocumento').val() != undefined && $('#txtDocumento').val() != '') {
            BuscarXNumIden(contador);
        }
        if ($('#IdCargo').val() != undefined && $('#IdCargo').val() != '') {
            BuscarXCargo(contador);
        }
        $("#btnGuardar").hide();
        $("#Documento").prop('disabled', true);
        $("#Documento").addClass("form-control bloqueado");
        $("#idSede").prop('disabled', true);
        $("#idSede").addClass("form-control bloqueado");
        $("#idProceso").prop('disabled', true);
        $("#idProceso").addClass("form-control bloqueado");
        $("#Diagnostico").prop('disabled', true);
        $("#Diagnostico").addClass("form-control bloqueado");
        $("#idObjetivo").prop('disabled', true);
        $("#idObjetivo").addClass("form-control bloqueado");
        $("#idTipoAnalisisPT").prop('disabled', true);
        $("#idTipoAnalisisPT").addClass("form-control bloqueado");
    }

    function GridSeguimiento() {

        var objGridSeguimiento = {
            Actividad: $('#Actividad').val(),
            Fecha: $('#Fecha').val(),
            Responsable: $('#Responsable').val(),
            IdEstudioPuestoTrabajo: $('#IdEstudioPT').val()
        };
        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/MostrarGridSeguimiento',
            data: objGridSeguimiento
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#GridSeguimiento').empty();
                $('#GridSeguimiento').html(response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            swal("Estimado Usuario", "No se logró consultar la información del Seguimiento. Intente más tarde.");
            OcultarPopupposition();
        });
    }

    function GridArchivos() {

        var objGridSeguimiento = {
            Actividad: null,
            Fecha: null,
            Responsable: null,
            IdEstudioPuestoTrabajo: $('#IdEstudioPT').val()
        };
        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/MostrarArchivos',
            data: objGridSeguimiento
        }).done(function (response) {
            if (response != undefined && response != '') {
                $('#GridArchivos').empty();
                $('#GridArchivos').html(response);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            swal("Estimado Usuario", "No se logró consultar la información. Intente más tarde.");
            OcultarPopupposition();
        });
    }

    function BuscarXNumIden(i)
    {
        var objEstudio = {
            Documento: $('#txtDocumento').val(),
            Apellido1: null,
            Apellido2: null,
            Nombre1: null,
            Nombre2: null,
            Cargo: null,
            FechaAnalisisStr: null,
            idSede: null,
            idProceso: null,
            idObjetivo: null,
            idTipoAnalisisPT: null

        };

        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/ConsultarEstudioXNumIden',
            data: objEstudio
        }).done(function (response) {
            if (response != undefined && response != '' && response.status == 'Success') {
                OcultarPopupposition();
                $('#NumRegistrosPT').val(response.Data.length);
                if (response.Data.length > 0) {
                    $('#Documento').val(response.Data[i].NumeroIdentificacion);
                    $('#Apellido1').val(response.Data[i].Apellido1);
                    $('#Apellido2').val(response.Data[i].Apellido2);
                    $('#Nombre1').val(response.Data[i].Nombre1);
                    $('#Nombre2').val(response.Data[i].Nombre2);
                    $('#Cargo').val(response.Data[i].Cargo);
                    $('#idSede').val(response.Data[i].IdSede);
                    $('#idProceso').val(response.Data[i].IdProceso);
                    $('#idObjetivo').val(response.Data[i].IdObjetivoAnalisis);
                    $('#idTipoAnalisisPT').val(response.Data[i].IdTipoAnalisis);
                    $("#Diagnostico").val(response.Data[i].Diagnostico);
                    $("#FechaAnalisis").val(response.Data[i].FechaAnalisisStr);
                    $("#IdEstudioPT").val(response.Data[i].IdEstudioPuestoTrabajo);
                    GridSeguimiento();
                    GridArchivos();
                }
                else {
                    $('#Documento').val('');
                    $('#Apellido1').val('');
                    $('#Apellido2').val('');
                    $('#Nombre1').val('');
                    $('#Nombre2').val('');
                    $('#Cargo').val('');
                    $('#idSede').val('');
                    $('#idProceso').val('');
                    $('#idObjetivo').val('');
                    $('#idTipoAnalisisPT').val('');
                    $("#Diagnostico").val('');
                    $("#FechaAnalisis").val('');
                }
                

            }
            else if (response != undefined && response != '' && response.status == 'Error') {
                OcultarPopupposition();

            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition();

        });
    }

    function BuscarXCargo(i) {
        var objEstudio = {
            Documento: null,
            Apellido1: null,
            Apellido2: null,
            Nombre1: null,
            Nombre2: null,
            Cargo: $('#IdCargo').val(),
            FechaAnalisisStr: null,
            idSede: null,
            idProceso: null,
            idObjetivo: null,
            idTipoAnalisisPT: null

        };

        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/ConsultarEstudioXCargo',
            data: objEstudio
        }).done(function (response) {
            if (response != undefined && response != '' && response.status == 'Success') {
                OcultarPopupposition();
                $('#NumRegistrosPT').val(response.Data.length);
                if (response.Data.length > 0) {
                    $('#Documento').val(response.Data[i].NumeroIdentificacion);
                    $('#Apellido1').val(response.Data[i].Apellido1);
                    $('#Apellido2').val(response.Data[i].Apellido2);
                    $('#Nombre1').val(response.Data[i].Nombre1);
                    $('#Nombre2').val(response.Data[i].Nombre2);
                    $('#Cargo').val(response.Data[i].Cargo);
                    $('#idSede').val(response.Data[i].IdSede);
                    $('#idProceso').val(response.Data[i].IdProceso);
                    $('#idObjetivo').val(response.Data[i].IdObjetivoAnalisis);
                    $('#idTipoAnalisisPT').val(response.Data[i].IdTipoAnalisis);
                    $("#Diagnostico").val(response.Data[i].IdDiagnostico);
                    $("#FechaAnalisis").val(response.Data[i].FechaAnalisis);
                    $("#IdEstudioPT").val(response.Data[i].IdEstudioPuestoTrabajo);
                    GridSeguimiento();
                    GridArchivos();
                }
                else {
                    $('#Documento').val('');
                    $('#Apellido1').val('');
                    $('#Apellido2').val('');
                    $('#Nombre1').val('');
                    $('#Nombre2').val('');
                    $('#Cargo').val('');
                    $('#idSede').val('');
                    $('#idProceso').val('');
                    $('#idObjetivo').val('');
                    $('#idTipoAnalisisPT').val('');
                    $("#Diagnostico").val('');
                    $("#FechaAnalisis").val('');
                }

            }
            else if (response != undefined && response != '' && response.status == 'Error') {
                OcultarPopupposition();

            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition();

        });
    }

});