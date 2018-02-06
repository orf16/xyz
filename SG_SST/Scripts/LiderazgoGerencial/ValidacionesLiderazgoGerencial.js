
function validarCamposPresupuesto() {
    //CargarFormularioPresupuesto();
    CamposPresupuesto();
    if ($("#Presupuesto").valid()) {
        CargarFormularioPresupuesto();
    }

}

function validarCamposPresupuestoBuscar(esInforme) {
    //CargarFormularioPresupuesto();    
    CamposPresupuesto();
    if ($("#Presupuesto").valid()) {
        CargarFormularioBusquedaPrespuestoPorAnio(esInforme);
    }

}

function validarCamposPresupuestoGuardar() {
    
    CamposPresupuesto();
    var respuestaDinamica = validarCamposDinamicos($("#actividades"), "DescripcionActividad", "input");
    if ($("#Presupuesto").valid() && respuestaDinamica) {
        GuardarActividades();
        $("#Presupuesto").submit();
    }

}

function validarCamposPresupuestoEditar() {

    CamposPresupuesto();
    var respuestaDinamica = validarCamposDinamicos($("#actividades"), "DescripcionActividad", "input","*");
    if ($("#Presupuesto").valid() && respuestaDinamica) {
        EditarPresupuesto();
        $("#Presupuesto").submit();
    }

}



function CamposPresupuesto() {

    $("#Presupuesto").validate({

        rules: {
            RubroTotal: {
                required: true,
                // min: 1,

            },
            FK_Sede: {
                required: true
            },
            Periodo: {
                required: true
            },
        },
        messages: {
            RubroTotal: {
                required: "El rubro total es requerido",
                // min: "se debe ingresar un valor mayor que cero",

            },
            FK_Sede: {
                required: "La sede es requerida"
            },
            Periodo: {
                required: "El periodo es requerido"
            },

        }



    });


}

//contenedor: contenedor donde se encuentran los campos dinamicos a validar ejemplo $("#actividades")
//nameElementoAValidar: nombre(name) de los elementos a validar ejemplo "DescripcionActividad"
//tipoElemento:tipo de elemento a buscar para realizar la validacion por ejemplo "input"
//tipoSelector: es el tipo de selector para jquery un ejemplo es el asterisco(*) es el que encuentra todos elementos que contengan un subestring especificada, 
//sino se especifica un selector se toma el igual como defecto..consultar api de jquery https://api.jquery.com/category/selectors/
//errorClass:es la clase con la que va aparecer la etiqueta que mostrara el mensaje de validación por defecto viene con la clase "error-dinamico".
//errorMensaje: mensaje que se mostrará de la validación el mensaje por defecto es "el campo es obligatorio"
function validarCamposDinamicos(contenedor, nameElementoAValidar, tipoElemento,tipoSelector, errorClass,errorMensaje) {

    if (contenedor != undefined && nameElementoAValidar != undefined)
    {
        var formulario = $(contenedor);

        var stringElementoBuscar = "";
        if (tipoSelector != undefined)
        {
            stringElementoBuscar = tipoElemento + "[name" + tipoSelector + "=" + nameElementoAValidar + "]";
        } else
        {
            stringElementoBuscar = tipoElemento + "[name=" + nameElementoAValidar + "]";
        }

        var valido = true;
        var labelError="";
        if (errorClass === undefined && errorMensaje === undefined)
        {
            labelError = '<label class="error-dinamico">El campo es obligatorio</label>';
        } else if (errorClass != undefined && errorMensaje === undefined)
        {
            labelError = '<label class="' + errorClass + '">El campo es obligatorio</label>';
        } else if (errorClass != undefined && errorMensaje != undefined)
        {
            labelError = '<label class="' + errorClass + '">' + errorMensaje + '</label>';
        } else if (errorClass === undefined && errorMensaje != undefined)
        {
            labelError = '<label class="error-dinamico">' + errorMensaje + '</label>';
        }

        formulario.find(stringElementoBuscar).each(function (ind,element) {
            var label = $(element).next("label");
            if ($(element).val().trim() === '') {
                if (label != undefined) {
                    label.remove();
                }
                $(element).after(labelError);
                $(element).attr("onchange", "quitarlabelError(this)");
                valido = false;
            } else
            {
                label.remove();
            }
        })
        return valido;
    } else {
        return false;
    }


}


function quitarlabelError(element)
{
    var label = $(element).next("label");
    if ($(element).val().trim() === '') {
        if (label != undefined) {
            label.remove();
        }       
    } else {
        label.remove();
    }

}