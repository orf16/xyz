function validarCamposRol() {
    CamposRol();
    if ($("#rol").valid()) {
        CargarFormularioRol();
    }
}


function CamposRol() {
    $("#rol").validate({
        rules: {
            NameRol: {
                required: true,
            },
            Responsab: {
                required: true,
            },
            Rendicion: {
                required: true,
            },
        },
        messages: {
            NameRol: {
                required: "El Nombre del Rol es requerido",
            },
            Responsab: {
                required: "Ingrese al menos una Responsabilidad",
            },
            Rendicion: {
                required: "Ingrese al menos un ítem Rendición de cuentas",
            },
        }
    });
}