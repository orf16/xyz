//funcion para validar que no ingresen valores nulos en el formulario maestro
function ValidarCrearFaseRecurso() {

    $('#frmfrecurso').validate({
        errorClass: "error",

        rules: {
            Descripcion_Fase: { required: true, },
           

        },
        messages: {
            Descripcion_Fase: { required: "Se debe ingresar una Fase del recurso" }

          



        }
    });


}
