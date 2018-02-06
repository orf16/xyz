var urlBase = utils.getBaseUrl();
var urlAusencias = '/Ausencias';



$(document).ready(function () {
    //$("#frmCargueMasimo").validate({
    //    rules: {
    //        IdEmpresaUsuaria: {
    //            required: true, min: 1
    //        },
    //    },
    //    messages: {
    //        IdEmpresaUsuaria: {
    //            required: "Debe seleccionar una empresa asociada",
    //            min: "Debe seleccionar una empresa asociada"
    //        }
    //    }
    //});
   $('#idDownPlantillaAuMasivo').click(function () {
        $.ajax({
            type: "POST",
            data: "",
            url: urlBase + urlAusencias + '/ObtenerPlantilla'
        }).done(function (response) {
            if (response != undefined && response.Mensaje == 'Success') {
                window.location.href = "/Ausencias/DescargarPlantilla";
            }
        });
   });  


    $('#idUpPlantillaAuMasivo').click(function () {
        //if (!$("#frmCargueMasimo").valid())
        //    return false

        var form_data = new FormData();
        var filedata = $("#file").prop("files")[0];
        if (filedata != undefined)
            form_data.append("cargarArchivo", filedata);
        else {
            swal("Atención", "Debe seleccionar un archivo.");
            return
        }

        var idEmpresa = $("#IdEmpresaUsuaria").val();
        if (idEmpresa != null)
            form_data.append("IdEmpresaUsuaria", idEmpresa);
        else
            form_data.append("IdEmpresaUsuaria", 0);
        PopupPosition();
        $.ajax({
            type: "POST",   
            data: form_data,
            url: urlBase + urlAusencias + '/CargueMasivo',
            processData: false,
            dataType: 'json',
            contentType: false
        }).done(function (response) {
            if (response != undefined && response.Mensaje == 'Success') {
                swal("Atención", response.Data);
                OcultarPopupposition();
            }
            else if (response != undefined && response.Mensaje == 'ERROR') {
                swal("Atención", response.Data);
                OcultarPopupposition();
            }
        }).fail(function (response) {
            $("#Documento").val('');
            swal("Apreciado Usuario", "El se a presentado un error al cargar el archivo, Por favor intentelo mas tarde.");
            OcultarPopupposition();
        });
    });

});



