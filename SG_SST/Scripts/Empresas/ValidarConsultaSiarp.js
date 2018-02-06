
function ValidarConsultaSiarp() {
    CamposBuscar();
    if ($("#siarp").valid()) {
        ObtenerSiarpCentro();
    }
}

function CamposBuscar() {

    $('#siarp').validate({
        errorClass: "error",

        rules: {
            nitempresa: { required: true }
        },
        messages: {
            nitempresa: { required: "Debe ingresar un NIT" }
        }
    });
}
