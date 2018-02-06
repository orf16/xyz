//funcion para validar maestro de Tipo de Recurso


    function ValidarCrearTipoRecurso() {
        $('#frtrecurso').validate({
            errorClass: "error",
            rules: {
                Descripcion_Tipo_Recurso: { required: true },
            },
            messages: {
                Descripcion_Tipo_Recurso: { required: "Se debe ingresar un Tipo de Recurso" }

             

            }
        });


    }


