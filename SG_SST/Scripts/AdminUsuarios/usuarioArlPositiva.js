var urlBase = utils.getBaseUrl();
var urlUsuario = '/AdminUsuarios';
$(document).ready(function () {
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
    $("#DeptoSedePpalEmpresa").change(function () {
        if ($("#DeptoSedePpalEmpresa").val() == '') {
            validacion = false;
            return false;
        }
        var depto = $("#DeptoSedePpalEmpresa").val()
        PopupPosition();
        $.ajax({
            type: "POST",
            data: { idDepto: depto },
            url: urlBase + urlUsuario + '/ConsultarMunicipiosPorDepto'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                $("#MunicipioSedePpalEmpresa").empty();
                $.each(response.Data, function (i, munici) {
                    $("#MunicipioSedePpalEmpresa").append('<option value="' + munici.Value + '">' +
                             munici.Text + '</option>');
                });
                OcultarPopupposition();
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response);
            OcultarPopupposition();
        });
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