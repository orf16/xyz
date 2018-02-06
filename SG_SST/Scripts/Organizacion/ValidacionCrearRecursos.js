function ValidacionCrearRecursos() {
    modalvalidar();
    if ($("#grabarrecurso").valid()) {
        $('#myModal1').modal("show");
    }

}

////Funcion para Validar Campos en el  Registro de Recursos a la sede.
function modalvalidar() {

    $('#grabarrecurso').validate({
        errorClass: "error",

        rules: {
            Periodo: { required: true },
            Fk_Id_Sede: { required: true },
            Nombre_Recurso: { required: true },
            Fk_Id_TipoRecurso: { required: true },
            Fk_Id_Fase: { required: true }


        },
        messages: {
            Periodo: { required: "Se debe seleccionar un Periodo" },

            Fk_Id_Sede: {
                required: "Se debe seleccionar una Sede",
            },

            Nombre_Recurso: {
                required: "Se debe ingresar un Nombre de Recurso"
            },

            Fk_Id_TipoRecurso: {
                required: "Se debe seleccionar un Tipo de Recurso"
            },

            Fk_Id_Fase: {
                required: "Se debe seleccionar una Fase de la lista"
            },
        }
    });
}
