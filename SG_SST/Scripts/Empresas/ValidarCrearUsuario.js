function ValidarCrearUsuario() {

    $('#grabarusuario').validate({
        errorClass: "error",

        rules: {
            Fk_Tipo_Documento: { required: true, },
            Numero_Documento: { required: true, },
            Fk_Id_Rol: { required: true },
            Nombre_Usuario: { required: true },
            
        },
        messages: {
            Fk_Tipo_Documento: { required: "Debe seleccionar un Tipo Documento" },

            Numero_Documento: {
                required: "Debe ingresar Número Documento"
            },

            Fk_Id_Rol: {
                required: "Debe seleccionar un Rol"
            },

            Nombre_Usuario: {
                required: "Debe ingresar un Nombre Usuario"
            }
        }
    });
}
