var urlBase = utils.getBaseUrl();
var urlUsuario = '/AdminUsuarios';

$(document).ready(function () {
    $('#formEvaluacion input[type="text"], select').on('focus', function(){
        $(this).siblings('span').hide();
    });
    //consulta la información asociada a la empresa
    $('#Documento').on('change', function (evt) {
        var tipoDocumentoEmpresa = $("#TipoDocumentoEmpresa").val();
        var documentoEmpresa = $("#DocumentoEmpresa").val();
        var tipoDocumento = $("#TipoDocumento").val();
        var documento = $("#Documento").val();
        if (tipoDocumentoEmpresa != '' && documentoEmpresa != '' && tipoDocumento != '' && documento != '') {
            ConsultarInformacionUsuarioEmpresa(tipoDocumentoEmpresa, documentoEmpresa, tipoDocumento, documento);
        } else {
            swal('Atención', 'Antes de continuar debe diligenciar los anteriores campos.');
            evt.preventDefault();
            //return false;
        }
    });
    //
    $('#formEvaluacion').on('submit', function (evt) {
        evt.preventDefault();
        $('#formEvaluacion input[id="RazonSocialEmpresa"]').val($('#RazonSocialEmpresa').val());
        $('#formEvaluacion input[id="Nombres"]').val($('#Nombres').val());
        $('#formEvaluacion input[id="Apellidos"]').val($('#Apellidos').val());
        $('#formEvaluacion input[id="EmailPersona"]').val($('#EmailPersona').val());
        $('#formEvaluacion input[id="MunicipioSedePpalEmpresa"]').val($('#MunicipioSedePpalEmpresa').val());
        this.submit();
    });
    //
    $('#formRecuperarClave').on('submit', function (evt) {
        evt.preventDefault();
        var respuestaUno = $('#ConfiguracionPreguntasSeguridad_RespuestaUno').val();
        var respuestaDos = $('#ConfiguracionPreguntasSeguridad_RespuestaDos').val();
        var respuestaTres = $('#ConfiguracionPreguntasSeguridad_RespuestaTres').val();
        if (respuestaUno == '' || respuestaDos == '' || respuestaTres == '') {
            swal({
                title: 'Atención',
                text: 'Debe diligenciar todos los campos para poder continuar.',
                type: 'warning',
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                closeOnCancel: false,
                html: true
            });
            return false;
        } else {
            var preguntaUno = $('#ConfiguracionPreguntasSeguridad_NombrePreguntaUno').val();
            var preguntaDos = $('#ConfiguracionPreguntasSeguridad_NombrePreguntaDos').val();
            var preguntaTres = $('#ConfiguracionPreguntasSeguridad_NombrePreguntaTres').val();
            $('#formRecuperarClave input[id="ConfiguracionPreguntasSeguridad_NombrePreguntaUno"]').val(preguntaUno);
            $('#formRecuperarClave input[id="ConfiguracionPreguntasSeguridad_NombrePreguntaDos"]').val(preguntaDos);
            $('#formRecuperarClave input[id="ConfiguracionPreguntasSeguridad_NombrePreguntaTres"]').val(preguntaTres);
            this.submit();
        }
    });
});
//Consulta la informacion del trabajador
function ConsultarInformacionUsuarioEmpresa(tipoDocumentoEmpresa, documentoEmpresa, tipoDocumento, documento) {
    PopupPosition();
    $.ajax({
        type: "post",
        data: { tipoDocumentoEmp: tipoDocumentoEmpresa, numDocumentoEmp: documentoEmpresa, tipoDocumento: tipoDocumento, numDucumento: documento },
        url: urlBase + urlUsuario + '/ConsultarInformacionUsuarioEmpresaSiarp'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Estado == 'OK' && response.MensajeError == '') {
            var nombres = response.NombresUsuario;
            var apellidos = response.ApellidosUsuario;
            var razonSocial = response.RazonSocialEmpresa;
            var municipioEmpresa = response.MunicipioSedePpalEmrpresa;
            if (response.PreguntasSeguridad != 'undefined' && response.PreguntasSeguridad != null && response.PreguntasSeguridad.length > 0) {
                var preguntaUno = response.PreguntasSeguridad[0].NombrePregunta;
                var preguntaDos = response.PreguntasSeguridad[1].NombrePregunta;
                var preguntaTres = response.PreguntasSeguridad[2].NombrePregunta;
                if ($('#ConfiguracionPreguntasSeguridad_NombrePreguntaUno').length > 0) {
                    $('#ConfiguracionPreguntasSeguridad_NombrePreguntaUno').val(preguntaUno);
                }
                if ($('#ConfiguracionPreguntasSeguridad_NombrePreguntaDos').length > 0) {
                    $('#ConfiguracionPreguntasSeguridad_NombrePreguntaDos').val(preguntaDos);
                }
                if ($('#ConfiguracionPreguntasSeguridad_NombrePreguntaTres').length > 0) {
                    $('#ConfiguracionPreguntasSeguridad_NombrePreguntaTres').val(preguntaTres);
                }
            }
            $('#Nombres').val(nombres);
            $('#Apellidos').val(apellidos);
            $('#RazonSocialEmpresa').val(razonSocial);
            $('#MunicipioSedePpalEmpresa').val(municipioEmpresa);
        } else if (response != undefined && response != '' && response.Estado == 'OK' && response.MensajeError != '') {
            $("#Documento").val('');
            $('#Nombres').val('');
            $('#Apellidos').val('');
            $('#RazonSocialEmpresa').val('');
            $('#MunicipioSedePpalEmpresa').val('');
            swal("Atención", response.MensajeError);
        }
        OcultarPopupposition();
    }).fail(function (response) {
        $("#Documento").val('');
        $('#Nombres').val('');
        $('#Apellidos').val('');
        $('#RazonSocialEmpresa').val('');
        $('#MunicipioSedePpalEmpresa').val('');
        swal("Atención", "No se logró obtener datos del Usuario. Intente nuevamente.");
        OcultarPopupposition();
    });
}
