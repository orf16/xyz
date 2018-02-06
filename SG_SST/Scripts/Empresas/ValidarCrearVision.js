//funcion para formatear el texto de los mensajes de Jquery Validate
$(document).ready(function () {
    $.extend(jQuery.validator.messages, {
        minlength: $.validator.format("Debe ingresar mínimo {0} caracteres"),

    });
})



///funcion para validar que no ingresen datos en blanco en la mision.
function ValidarCrearVision() {

    $('#formvision').validate({
        errorClass: "error",

        rules: {
            vision: { required: true, minlength: 10 },

        },
        messages: {
            vision: { required: "La Visión no puede estar vacía" }

        }

    });

}