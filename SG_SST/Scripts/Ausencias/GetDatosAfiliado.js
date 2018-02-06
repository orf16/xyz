var urlBase = utils.getBaseUrl();
var urlAfiliado = '/Ausencias';
var urlAusencias = '/Ausencias';

$(document).ready(function () {
    //var validaDocumento = $("#Documento");
    //validaDocumento.validate({
    //    rules: {
    //        Documento: {
    //            required: true,
    //            minlength: 2,
    //            digits: true
    //        }
    //    },
    //    messages: {
    //        Documento: {
    //            required: 'Debe Ingresar un número de Documento',
    //            digits: 'Documento debe ser numérico'
    //        }
    //    }
    //});

    //$('#DatosTrabajor_Sexo').on("change", function () {
    //    swal("Atención", 'No es posible cambiar el tipo de sexo asociado al numero de identificaion.');
    //    return false;
    //});

    $('#Documento').on('change', function () {
        var documento = $("#Documento").val();
        if (documento != '') {
            DatosTrabajador(documento);
        } else
            swal('Atención', 'Seleccione un valor válido para continuar.');
        return false;
    });

    $("#Documento").keypress(function (tecla) {
        if (tecla.charCode < 47 || tecla.charCode > 57) return false;
    });


    $(document).on("input", "#DatosTrabajor_Salario", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#DatosTrabajor_Salario").keypress(function (tecla) {
        if (tecla.charCode < 44 || tecla.charCode > 57 || tecla.charCode == 45 || tecla.charCode == 47)
            return false;
    });

    $('#DatosTrabajor_Salario').on('change', function () {
        EtiquetarValoresAPrecio('DatosTrabajor_Salario');
    });



    $("#Si").click(function () {
        FrmAusentismo();
    });
    $("#No").click(function () {
        var documento = $("#Documento").val();
        HistorialAusentismo(documento);
    });

    $("#Departamento").change(function () {
        if (!$("#Departamento").valid()) {
            validacion = false;
            return false;
        }

        var depto = $("#Departamento").val()

        PopupPosition();
        $.ajax({
            type: "POST",
            data: { idDepto: depto },
            url: urlBase + urlAusencias + '/ConsultarMunicipiosPorDepto'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $("#Municipio").empty();
                $.each(response.Data, function (i, munici) {
                    $("#Municipio").append('<option value="' + munici.Value + '">' +
                             munici.Text + '</option>');
                });
                OcultarPopupposition();
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response);
            OcultarPopupposition();
        });
    })

    //Consulta la informacion del trabajador
    function DatosTrabajador(documento) {
        if (documento == '')
            swal("Atención", 'Este campo debe ser obligatorio');
        PopupPosition();
        $.ajax({
            type: "post",
            data: { numeroDocumento: documento },
            url: urlBase + urlAfiliado + '/ConsultarDatosTrabajador'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                var trabajador = response.Data;
                var nombre = trabajador.nombre1 + ' ' + trabajador.apellido1;
                var fecha1 = trabajador.fechaNacimiento.toString();
                var fecha = fecha1.split('-')[2] + '/' + fecha1.split('-')[1] + '/' + fecha1.split('-')[0];
                $('#DatosTrabajor_Nombre1').val(nombre);
                $('#DatosTrabajor_FechaNacimiento').val(fecha);
                if (trabajador.sexoPersona != '') {
                    $('#DatosTrabajor_Sexo option[value =' + trabajador.sexoPersona + ']').prop('selected', true);
                    $('#DatosTrabajor_Sexo').prop("disabled", true);
                }
                else {
                    $('#DatosTrabajor_Sexo option:contains(Seleccione)').prop('selected', true);
                    $('#DatosTrabajor_Sexo').prop("disabled", false);
                }

                //$('#Sexo option:contains(' + trabajador.Sexo + ')').prop('selected', true);
                $('#DatosTrabajor_Ocupacion').val(trabajador.ocupacion);
                $('#DatosTrabajor_NombreEps').val(trabajador.nombreEps);
                if (trabajador.salario != 0)
                    $('#DatosTrabajor_Salario').val(trabajador.salario);
                else
                    $('#DatosTrabajor_Salario').val(800000);
                EtiquetarValoresAPrecio('DatosTrabajor_Salario');
                $('#Preguntas').show();
                //var deptoEmpleado;
                //if (trabajador.nomDepAfiliado.includes("BOGOTA"))
                //    deptoEmpleado = "BOGOTA";
                //else
                //    deptoEmpleado = trabajador.nomDepAfiliado;
                $('#Departamento').find('option').each(function () {
                    var deptoIterator = $.trim($(this).text());
                    var deptoObtenido = $.trim(trabajador.nomDepAfiliado);
                    if (deptoIterator == deptoObtenido) {
                        $(this).prop('selected', true);
                    }
                });

                var depto = $("#Departamento").val()

                $.ajax({
                    type: "POST",
                    data: { idDepto: depto },
                    url: urlBase + urlAusencias + '/ConsultarMunicipiosPorDepto'
                }).done(function (response) {
                    if (response != undefined && response != '' && response.Mensaje == 'Success') {
                        $("#Municipio").empty();
                        $.each(response.Data, function (i, munici) {
                            $("#Municipio").append('<option value="' + munici.Value + '">' +
                                     munici.Text + '</option>');
                        });
                        $('#Municipio option:contains(' + trabajador.nomMunAfiliado + ')').prop('selected', true);
                    }
                })
            } else if (response != undefined && response != '' && response.Mensaje != '') {
                $("#Documento").val('');
                swal("Atención", response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            $("#Documento").val('');
            swal("Atención", "No se logró obtener datos del trabajador. Intente más tarde.");
            OcultarPopupposition();
        });
    }
    //se consultan las ausencias registradas del el trabajador
    function HistorialAusentismo(documento) {
        var datos = {
            Documento: documento
        }
        PopupPosition();
        $.ajax({
            type: "post",
            data: { Ausencia: datos, tipoVIsta: 1 },
            url: urlBase + urlAusencias + '/ConsultarAusencia'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#datosAusentismo').empty();
                $('#datosAusentismo').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            swal("Atención", "No se logró consultar la información del Trabajador. Intente más tarde.");
            OcultarPopupposition();
        });
    }
    //se renderiza el formulario para registrar una nueva ausencia
    function FrmAusentismo() {
        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlAusencias + '/RegistrarNuevaAusencia'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#datosAusentismo').empty();
                $('#datosAusentismo').html(response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            swal("Atención", "No se logró consultar la información del Trabajador. Intente más tarde.");
            OcultarPopupposition();
        });
    }
});
