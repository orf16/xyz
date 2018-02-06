//declaración de variables
var numeroAspecto = 8;

$(document).ready(function () {
    OcultarPopupposition();
    //configura el datepicker para el elemneto dado
    ConstruirDatePickerPorElemento('FechaDiligenciamiento');
    //muestra la ventana para que el usuario adicione un nuevo aspecto
    $('#agr_asp').on('click', function () {
        $.fancybox({
            'content': $("#nuev_asp").html(),
            type: 'inline',
            'autoScale': false,
            'autoSize': false,
            'titlePosition': 'inside',
            'transitionIn': 'none',
            'transitionOut': 'none',
            'type': 'html',
            'width': '600',
            'height': '250',
            fitToView: false,
            openEffect: 'none',
            closeEffect: 'none',
            helpers: {
                overlay: { closeClick: false }
            }
        });
        $('.fancybox-inner input[type="text"]').on('focus', function () {
            $('.fancybox-inner span[class="msg-validacion"]').hide();
        });
    });
    $(document).on("input", "#Aspecto", function () {
        var limite = 500;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });
    //controla la adición de un nuevo aspecto
    $(document).on('click', '.fancybox-inner button[type="submit"]', function (event) {
        var nuevoAspecto = $('.fancybox-inner input[type="text"]').val();
        numeroAspecto = numeroAspecto + 1;
        if (nuevoAspecto != '') {
            PopupPosition();
            $.ajax({
                url: '../Evaluacion/AgregarNuevoAspecto',
                data: { aspecto: nuevoAspecto, idAspecto: numeroAspecto },
                type: 'post'
            }).done(function (response) {
                if (response.Mensaje == 'OK') {
                    $('#body_asp').show();
                    $('#tbl_asp tbody').append(response.Data);
                    $.fancybox.close();
                }
                OcultarPopupposition();
            }).fail(function (response) {
                OcultarPopupposition();
            });
        } else
            $('.fancybox-inner span[class="msg-validacion"]').show();
        event.preventDefault ? event.preventDefault() : event.returnValue = false;
    });

    //Valida el formulario antes de ser enviado
    //a guardar.
    $('#formEvaluacion').validate({
        rules: {
            RazonSocial: {
                required: true,
                maxlength: 150
            },
            Nit: {
                required: true,
                minlength: 2,
                maxlength: 15,
                digits: true
            },
            ActividadEconomica: {
                required: true
            },
            ResponsableSGSST: {
                required: true,
                maxlength: 20
            },
            ElaboradoPor: {
                required: true,
                maxlength: 20
            },
            LicenciaSOSL: {
                required: true,
                maxlength: 15
            },
            SedeCentroTrabajo: {
                required: true
            },
            FechaDiligenciamiento: {
                required: true
            }
        },
        messages: {
            RazonSocial: {
                required: 'Este campo es obligatorio',
                maxlength: 'Este campo debe tener un máximo de 150 caracteres de longitud'
            },
            Nit: {
                required: 'Este campo es obligatorio',
                maxlength: 'Este campo debe tener un máximo de 15 caracteres',
                digits: 'Este campo debe ser numérico'
            },
            ActividadEconomica: {
                required: 'Este campo es obligatorio'
            },
            ResponsableSGSST: {
                required: 'Este campo es obligatorio',
                maxlength: 'Este campo debe tener un máximo de 20 caracteres de longitud'
            },
            ElaboradoPor: {
                required: 'Este campo es obligatorio',
                maxlength: 'Este campo debe tener un máximo de 20 caracteres de longitud'
            },
            LicenciaSOSL: {
                required: 'Este campo es obligatorio',
                maxlength: 'Este campo debe tener un máximo de 15 caracteres de longitud'
            },
            SedeCentroTrabajo: {
                required: 'Este campo es obligatorio'
            },
            FechaDiligenciamiento: {
                required: 'Este campo es obligatorio'
            }
         }
    });


    //guardado de datos
    $('#formEvaluacion').on('submit', function (e) {
        e.preventDefault();
        var validado = true;
        if ($("#formEvaluacion").valid()) {
            //se obtienen los aspectos creados
            var aspectos = $('#tbl_asp tbody').find('tr');
            if (aspectos.length == 0) {
                swal('Atención', 'Debe tener creado al menos un aspecto a evaluar.');
                validado = false;
            }
            if (!ValidarCalificacionAspectos(aspectos)) {
                swal('Atención', 'Todos los aspectos se deben calificar.');
                validado = false;
            }
            if (validado) {
                PopupPosition();
                var aspectos = new Array();
                $('#tbl_asp tbody').find('tr').each(function (ini) {
                    var datosAspectos = new Object();
                    var idTr = $(this).attr('id');
                    $(this).find('td').each(function (seg) {
                        if ($(this).find('input[type="radio"]').is(':checked')) {
                            $(this).find('input[type="radio"]').val(true);
                        } else
                            $(this).find('input[type="radio"]').val(false);
                    });
                    datosAspectos.IdAspecto = 0;
                    datosAspectos.AspectoEvaluar = $(this).find('textarea#AspectoEvaluar').val();
                    datosAspectos.Cumple = $(this).find('#Cumple_' + idTr).val();
                    datosAspectos.NoCumple = $(this).find('#NoCumple_' + idTr).val();
                    datosAspectos.CumpleParcial = $(this).find('#CumpleParcial_' + idTr).val();
                    datosAspectos.Observaciones = $(this).find('textarea#Observaciones').val();
                    aspectos.push(datosAspectos);
                });
                //Se crea el objeto de evaluación a guardar.
                var datos = {
                    RazonSocial: $('#RazonSocial').val(),
                    Nit: $('#Nit').val(),
                    CodActividadeEconomica: $('#CodActividadeEconomica').val(),
                    ActividadEconomica: $('#ActividadEconomica').val(),
                    ResponsableSGSST: $('#ResponsableSGSST').val(),
                    ElaboradoPor: $('#ElaboradoPor').val(),
                    LicenciaSOSL: $('#LicenciaSOSL').val(),
                    SedeCentroTrabajo: $('#SedeCentroTrabajo').val(),
                    FechaDiligenciamiento: $('#FechaDiligenciamiento').val(),
                    AspectosCreados: aspectos
                };
                $.ajax({
                    url: '../Evaluacion/Index',
                    type: 'post',
                    data: datos,
                }).done(function (response) {
                    if (response != undefined && response.Mensaje == 'OK') {
                        OcultarPopupposition();
                        $('#val_eva_ini').val(response.Data);
                        $('#cal_eval_ini').show();
                        swal({
                            title: 'Atención!',
                            text: 'Se registró con éxito la Evaluación Inicial.',
                            type: 'success',
                            showConfirmButton: true,
                            allowOutsideClick: false,
                            confirmButtonText: 'Aceptar'
                        });
                    } else if (response != undefined && response.Mensaje == 'ERROR') {
                        swal('Atención', 'No se pudo registrar la Evaluación Inicial. Intente nuevamente.');
                        OcultarPopupposition();
                    }
                }).fail(function (response) {
                    OcultarPopupposition();
                });
            }
        }
    });

    //Metodo para genrar en pdf el formulario en pantalla
    $('#_FormularioPdf').on('click', function (e) {
        e.preventDefault();
       var aspectos = new Array();
        $('#tbl_asp tbody').find('tr').each(function (ini) {
            var datosAspectos = new Object();
            var idTr = $(this).attr('id');
            $(this).find('td').each(function (seg) {
                if ($(this).find('input[type="radio"]').is(':checked')) {
                    $(this).find('input[type="radio"]').val(true);
                } else
                    $(this).find('input[type="radio"]').val(false);
            });
            datosAspectos.IdAspecto = 0;
            datosAspectos.AspectoEvaluar = $(this).find('textarea#AspectoEvaluar').val();
            datosAspectos.Cumple = $(this).find('#Cumple_' + idTr).val();
            datosAspectos.NoCumple = $(this).find('#NoCumple_' + idTr).val();
            datosAspectos.CumpleParcial = $(this).find('#CumpleParcial_' + idTr).val();
            datosAspectos.Observaciones = $(this).find('textarea#Observaciones').val();
            aspectos.push(datosAspectos);
        });
        //Se crea el objeto de evaluación a guardar.
        var datos = {
            RazonSocial: $('#RazonSocial').val(),
            Nit: $('#Nit').val(),
            ResponsableSGSST: $('#ResponsableSGSST').val(),
            ElaboradoPor: $('#ElaboradoPor').val(),
            LicenciaSOSL: $('#LicenciaSOSL').val(),
            SedeCentroTrabajo: $('#SedeCentroTrabajo option:selected').text(),
            FechaDiligenciamiento: $('#FechaDiligenciamiento').val(),
            CodActividadeEconomica: $('#CodActividadeEconomica').val(),
            ActividadEconomica: $('#ActividadEconomica').val(),
            AspectosCreados: aspectos
        };
        PopupPosition();
        $.ajax({
            url: '../Evaluacion/ObtenerViewToPdf',
            type: 'post',
            data: datos,
        }).done(function (response) {
            if (response != undefined && response.Mensaje == 'OK') {
                window.location.href = '../EvaluacionPdf/GeneradorPdf';
                OcultarPopupposition();
            }
        }).fail(function (response) {
            OcultarPopupposition();
        });
    });
});

///||| Declaración de funciones |||
//Aplica la búsqueda
function GuardarDatosEvaluacion(url, metodo, datos) {
    $.ajax({
        url: url,
        type: metodo,
        data: datos
    }).done(function (response) {
        if (response != undefined && response.Status == "ok") {
            //$('#_bodyReport').empty();
            //$('#_bodyReport').html(response.Data);
        } else if (response != undefined && response.Status == "error") {
            $.fancybox({
                'content': response.Data,
                'autoDimensions': false,
                'type': 'iframe',
                'scrolling': 'no',
                'autoSize': true,
                'closeBtn': true,
                'closeClick': true,
                helpers: {
                    overlay: { closeClick: true }
                }
            });
        }
    });
}

//valida la cantidad de aspectos calificados
function ValidarCalificacionAspectos(aspectos) {
    var calificacionAspectos = new Array();
    var cantidadAspectos = aspectos.length;
    $('#tbl_asp tbody').find('tr').each(function (ini) {
        $(this).find('td').find('input[type="radio"]').each(function (seg) {
            if ($(this).is(':checked'))
                calificacionAspectos.push($(this).val());
        });
    });
    if (calificacionAspectos.length < cantidadAspectos)
        return false;
    else
        return true;
}