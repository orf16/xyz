//Funcion para Validar buscar los recursos.
function ValidacionBuscar() {

    $('#buscarecurso').validate({
        errorClass: "error",

        rules: {
            Periodo: { required: true },
            Pk_Id_Sede: { required: true },
          


        },
        messages: {
            Periodo: { required: "Se debe seleccionar un Periodo de la lista" },

            Pk_Id_Sede: {
                required: "Se debe seleccionar una Sede de la lista"
            },

          

        }
    });


}