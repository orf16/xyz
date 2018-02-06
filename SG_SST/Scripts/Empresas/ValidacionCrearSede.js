
function ValidacionCrearSede() {
    $('#grabarsede').validate({
        errorClass: "error",

        rules: {
            Nombre_Sede: { required: true, },
            Direccion_Sede: { required: true, },
            Sector: { required: true },
            Fk_Id_Departamento: { required: true },
            Fk_Id_Municipio: { required: true },
            centros: { required: true },
            nitempresa: { required: true },
        },
        messages: {
            Nombre_Sede: { required: "Debe ingresar un Nombre Sede." },
            Direccion_Sede: {
                required: "Debe ingresar una Dirección."
            },
            Sector: {
                required: "Debe seleccionar un Sector."
            },
            Fk_Id_Departamento: {
                required: "Debe seleccionar un Departamento."
            },
            Fk_Id_Municipio: {
                required: "Debe seleccionar un Municipio."
            },
           centros: {
               required: " * Debe Seleccionar como minimo una Actividad Economica."
         },
            nitempresa:{
                required: "Debe ingresar un Nit y obtener los Centros de Trabajo."
            }
        }
    });
}
