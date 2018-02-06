$(document).ready(function () {
    //valores iniciales
    $('.radio_no_aplica').css('display', 'none');
    $('#justificacion').hide();
    $('.btn_est_min').on('click', function (e) {
        var idElemento = $(this).attr('id');
        PopupPosition();
        $.ajax({
            url: '../EvaluacionEstandarMinimo/ObtenerCriteriosPorCiclo',
            data: { idCiclo: idElemento },
            type: 'post'
        }).done(function (response) {
            if (response != null && response.Mensaje == 'OK') {
                $('#container_est_min').empty();
                $('#container_est_min').html(response.Datos);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
        });
    });
    $('.opc-calificacion').on('change', function (event) {
        var opcionCalificada = $(this).attr('id');
        $(this).prop('checked', true);
        if (opcionCalificada == 3) {
            $('.radio_no_aplica').css('display', 'inline-block');
            $('#container_actividades').hide();
        } else {
            $('.radio_no_aplica').css('display', 'none');
            $('#justificacion').hide();
            $('.opc-opcionnoaplica').each(function (elem) {
                $(this).prop('checked', false);
            });
            if (opcionCalificada == 1) {
                $('#container_actividades').hide();
            }
            if (opcionCalificada == 2) {
                $('#CrearNuevaActividad').modal('show');
                GestionarActividadesGeneradas();
            }
        }
        event.preventDefault ? event.preventDefault() : event.returnValue = false;
    });
    $('.opc-opcionnoaplica').on('change', function (e) {
        var opcionJustifica = $(this).attr('id');
        if (opcionJustifica == 4)
            $('#justificacion').show();
        else
            $('#justificacion').hide();
    });
    //botón para guardar la calificación de un estandar
    $('#guardar_calif_actual').on('click', function (e) {
        ValidarCriterioAGuardar();
    });
    //botón para guardar la calificación de un estandar
    $('#guardar_ciclo').on('click', function (e) {
        ValidarCriterioAGuardar();
    });
    $('#btn_inf_parcial').on('click', function () {
        PopupPosition();
        $.ajax({
            url: '../EvaluacionEstandarMinimo/ObtenerInformeParcial',
            type: 'post'
        }).done(function (response) {
            if (response != undefined && response.Mensaje == 'OK') {
                $('#container_est_min').empty();
                $('#container_est_min').html(response.Datos);
                //$.fancybox(response.Datos);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
        });
    });
    $('#inf_final').on('click', function (e) {
        PopupPosition();
        $.ajax({
            url: '../EvaluacionEstandarMinimo/ObtenerInformeFinal',
            type: 'post'
        }).done(function (response) {
            if (response != undefined && response.Mensaje == 'OK') {
                $('#container_est_min').empty();
                $('#container_est_min').html(response.Datos);
                //$.fancybox(response.Datos);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
        });
        //DescargarExcelEstandesMinimosFinla();
    });
    
    //Limita a 500 los carateres digitados en la activida de plande accion
    $(document).on("input", "#Actividad", function () {
        var limite = 500;        
        var textreal = $(this).val();
        var text;  
        
        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }        
    });

    //Limita a 50 los caracteres digitados del responsable en el plan de accion
    $(document).on("input", "#Responsable", function () {
        var limite = 50;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    

    $("#_planAccion").on('click', function (event) {
        PopupPosition();
        $.ajax({
            url: '../EvaluacionEstandarMinimo/PlanDeAccion',
            data: '',
            type: 'post'
        }).done(function (response) {
            if (response.Mensaje == 'OK') {
                $('#containeplanaccion').empty();
                $('#containeplanaccion').html(response.Data);
                $('#PlanAccionModal').modal('show');
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
        });
    });
    $('#FechaFin').datepicker({
    firstDay: 1,
    format: "dd/mm/yyyy",
    language: 'es',
    autoclose: true
});
$('#form_agr_actv input[type="text"]').on('focus', function () {
    if ($(this).attr('id') === 'FechaFin')
        $(this).parent().siblings().hide();
    else
        $(this).siblings().hide();
});
$('#btn_crear_act').on('click', function (event) {
    //llama a función para guardar nueva actividad
    GuardarNuevaActividad();
    event.preventDefault ? event.preventDefault() : event.returnValue = false;
});
$('.btn_cerrarProrroga').on('click', function (e) {
    var actividades = new Array();
    var actividadesGuardadas = sessionStorage.getItem('Actividades');
    if (!actividadesGuardadas) {
        $('.calif_crit').find('input[type=radio]').each(function () {
            $(this).prop('checked', false);
        });
    } else {
        actividades = JSON.parse(actividadesGuardadas);
        if (actividades.length == 0) {
            $('.calif_crit').find('input[type=radio]').each(function () {
                $(this).prop('checked', false);
            });
        }
    }
});
$('#btn_agr_actv').on('click', function () {
    $('#form_agr_actv input[type="text"]').each(function () {
        $(this).val('');
    });
    $('#btn_crear_act').show();
    $('#btn_edit_act').hide();
});
});



//controla la adición de una nueva actividad
function GuardarNuevaActividad() {
    actividades = new Array();
    var validado = true;
    var nuevaActividad = $('#Actividad').val();
    var responsable = $('#Responsable').val();
    var fechaFin = $('#FechaFin').val();
    if (nuevaActividad == null || nuevaActividad == undefined || nuevaActividad == '') {
        $('#Actividad').siblings().show();
        validado = false;
    }
    if (responsable == null || responsable == undefined || responsable == '') {
        $('#Responsable').siblings().show();
        validado = false;
    }
    if (fechaFin == null || fechaFin == undefined || fechaFin == '') {
        $('#FechaFin').parent().siblings().show();
        validado = false;
    }
    if (validado) {
        var actividades;
        var actividadesGuardadas = sessionStorage.getItem('Actividades');
        if (actividadesGuardadas) {
            actividades = JSON.parse(actividadesGuardadas);
            var ultimoId = actividades.length;
            var datos = {
                Id_Actividad: ultimoId + 1,
                Descripcion: nuevaActividad,
                Responsable: responsable,
                FechaFin: fechaFin
            };
            actividades.push(datos);
        } else {
            var datos = {
                Id_Actividad: 1,
                Descripcion: nuevaActividad,
                Responsable: responsable,
                FechaFin: fechaFin
            };
            actividades.push(datos);
        }
        sessionStorage.setItem('Actividades', JSON.stringify(actividades));
        PopupPosition();
        $.ajax({
            url: '../EvaluacionEstandarMinimo/AgregarNuevaActividad',
            data: { nuevaActividad: datos },
            type: 'post'
        }).done(function (response) {
            if (response.Mensaje == 'OK') {
                $('#container_actividades').show();
                $('#inner_actividades_agr').append(response.Data);
                $('#CrearNuevaActividad').modal('hide');
                //swal('Atención', 'La Actividad se creó con éxito.');
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
        });
    } else
        return false;
}

function ValidarCriterioAGuardar() {
    var calificacion = 0;
    var opcionNoAplica = 0;
    var actividades = new Array();
    var justificacion = '';
    $('.opc-calificacion').each(function (elem) {
        if ($(this).is(':checked'))
            calificacion = $(this).attr('id');
    });
    if (calificacion <= 0) {
        swal('Atención', 'Debe realizar una calificación para continuar.');
        return false;
    } else if(calificacion == 2){
        var actividadesGuardadas = sessionStorage.getItem('Actividades');
        if (actividadesGuardadas) {
            actividades = JSON.parse(actividadesGuardadas);
            if(actividades.length == 0){
                swal('Atención', 'Debe agregar al menos una actividad para continuar.');
                $('.opc-calificacion').prop('checked', false);
                return false;
            }
        }else{
            swal('Atención', 'Debe agregar al menos una actividad para continuar.');
            $('.opc-calificacion').prop('checked', false);
            return false;
        }
    } else if (calificacion == 3) {
        $('.opc-opcionnoaplica').each(function (elem) {
            if ($(this).is(':checked'))
                opcionNoAplica = $(this).attr('id');
        });
        console.log(opcionNoAplica);
        if (opcionNoAplica <= 0) {
            swal('Atención', 'Debe seleccionar una de las opciones para la calificación "No Aplica".');
            return false;
        } else if (opcionNoAplica == 4) {
            var justificacion = $('#textarea_justif').val();
            console.log(justificacion);
            if (justificacion == '') {
                swal('Atención', 'Debe ingresar una justificación para la calificación "No Aplica".');
                return false;
            }
        }
    }
    var cicloGuardo = sessionStorage.getItem('CicloActual');
    if (cicloGuardo) {
        var cicloActual = JSON.parse(cicloGuardo);
        var datos = {
            IdCiclo: cicloActual.IdCiclo,
            IdCriterio: cicloActual.IdCriterio,
            IdEmpresaEvaluar: cicloActual.IdEmpresaEvaluar,
            IdEvalEstandarMinimo: 0,
            IdValoracionCriterio: calificacion,
            Justificacion: justificacion,
            Actividades: actividades
        };
        GuardarCalificacionCriterio(datos);
        return true;
    }
}
//guarda la calificacion de un criterio
function GuardarCalificacionCriterio(datos) {
    PopupPosition();
    $.ajax({
        url: '../EvaluacionEstandarMinimo/CalificarCriterioPorCiclo',
        type: 'post',
        data: { objCalificacion: datos }
    }).done(function (response) {
        if (response != null && response.Mensaje == 'OK') {
            if (response.CicloCalificado)
                window.location.href = '../EvaluacionEstandarMinimo/Index'
            if (response.TerminaCalfEstMin)
                window.location.href = '../EvaluacionEstandarMinimo/Index'
            $('#container_est_min').empty();
            $('#container_est_min').html(response.Datos);
            var actividades = sessionStorage.getItem('Actividades');
            if (actividades != null && actividades != 'undefined' && actividades != '') {
                sessionStorage.removeItem('Actividades');
            }
        }
        OcultarPopupposition();
    }).fail(function (responde) {
        OcultarPopupposition();
    });
}
//
function GestionarActividadesGeneradas() {
    var actividades = new Array();
    var actividadesGuardadas = sessionStorage.getItem('Actividades');
    if (!actividadesGuardadas)
        return false;
    actividades = JSON.parse(actividadesGuardadas);
    if(actividades.length > 0){
        PopupPosition();
        $.ajax({
            url: '../EvaluacionEstandarMinimo/Renderizarctividades',
            data: { actividades: actividades },
            type: 'post'
        }).done(function (response) {
            if (response.Mensaje == 'OK') {
                $('#container_actividades').show();
                $('#inner_actividades_agr').empty();
                $('#inner_actividades_agr').append($('.head-activ').html());
                $('#inner_actividades_agr').append(response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
        });
        return true;
    } else
        return false;
}
//descargar informe en excel
function DescargarExcelEstandesMinimosFinla() {
    var datos = 1;
    $.ajax({
        url: '../EvaluacionEstandarMinimo/ObtenerInformeExccel',
        type: 'post',
        data: { idEmpresa: datos },
    }).done(function (response) {
        if (response != undefined && response.Mensaje == 'OK') {
            window.location.href = '../EvaluacionEstandarMinimo/DescargarInformeExccel';
        }
    });
}