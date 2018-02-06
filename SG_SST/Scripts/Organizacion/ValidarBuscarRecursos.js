function ValidarBuscarRecursos()
{
    CamposBuscar();
    if ($("#recursossede").valid()) {
        BuscarRecursosPorSede();
    }

}

///Funcion para Validar que seleccionen listas y  buscar los recursos.
function CamposBuscar() {

    $('#recursossede').validate({
        errorClass: "error",
        rules: {
            Periodo: { required: true },
            Pk_Id_Sede: { required: true },
        },
        messages: {
            Periodo: { required: "Se debe seleccionar un Periodo" },
            Pk_Id_Sede: {
                required: "Se debe seleccionar una Sede",
            }
        }
    });
}
