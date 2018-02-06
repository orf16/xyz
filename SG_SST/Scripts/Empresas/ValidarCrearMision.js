//funcion para formatear el texto de los mensajes de Jquery Validate
$(document).ready(function () {
    $.extend(jQuery.validator.messages, {
        minlength: $.validator.format("Debe ingresar mínimo {0} caracteres"),
       
    });
})

///funcion para validar que no ingresen datos en blanco en la mision.
function ValidarCrearMision() {

    $('#myModal12').validate({
        errorClass: "error",

        rules: {
            mision: { required: true, minlength: 10 },
            
        },
        messages: {
            mision: { required: "La Misión no puede estar vacía" }

        }

    });

}