//Funcion que permite validar que no ingresen datos nullos en el maestro de creacion tipos de Inspeccion
function ValidarCrearMaestro() {

    $('#forconfiprio').validate({
        errorClass: "error",

        rules: {
                Descripcion_Tipo_Inspeccion:{ required: true }
        },
        messages: {
            Descripcion_Tipo_Inspeccion: { required: " * Debe ingresar un tipo de inspección." }
        }
    });


}

function ValidarCrearMaestroConfiguracion() {

    $('#forconfiprio').validate({
        errorClass: "error",

        rules: {
            DescripcionPrioridad: { required: true }
        },
        rules: {
            DiasDesde: { required: true }
        },
        rules: {
            DiasHasta: { required: true }
        },
        messages: {
            DescripcionPrioridad: { required: " * Debe ingresar una Descripcion de la configuración." }
        },
        messages: {
            DiasDesde: { required: " * Debe diligenciar este campo." }
        },
        messages: {
            DiasHasta: { required: " * Debe diligenciar este campo." }
        }

    });


}