var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

var esObligatorioTematica = true;

function validarCamposRolCargos(numberTab) {   
    esObligatorioTematica = false;   
    FormularioCampoGenerales();
    if ($("#competencia").valid()) {
        AsignarAtributosPestañaSiguiente(numberTab);
        removerAtributosPestañaAnterior(numberTab - 1);
    }
}

function AsignarAtributosPestañaSiguiente(numberTab) {
    var tabSiguiente = "#tab" + numberTab;
    $(tabSiguiente).attr("href", "#step" + numberTab);
    $(tabSiguiente).attr("data-toggle", "tab");
    $(tabSiguiente).click();
}

function removerAtributosPestañaAnterior(numberTab) {
    var tabSiguiente = "#tab" + numberTab;
    $(tabSiguiente).removeAttr("href");
    $(tabSiguiente).removeAttr("data-toggle");
}

function AnteriorPanel(numberTab) {
    AsignarAtributosPestañaSiguiente(numberTab);
    removerAtributosPestañaAnterior(numberTab + 1);
}

function uploadAjaxValidate() {
    esObligatorioTematica = true;
    var valor = $("#nuevaFormacion").val();
    FormularioCampoGenerales();
    var valor = $("#nuevaFormacion").val();
    var vell = esObligatorioTematica;
    if ($("#competencia").valid()) {
        uploadAjax();
    }
}

//function CamposCrearTematicaEmp() {
//    $("#competencia").validate({
//        errorClass: "my-error-class",
//        rules: {
//            nuevaFormacion: {
//                required: true
//            }
//            //archivoSubir: {
//            //    required: false,
//            //    extension: "pdf",
//            //},           
//        },
//        messages: {
//            nuevaFormacion: {
//                required: "Debe ingresar una temática"
//            }
//            //archivoSubir: {
//            //    required: "Solo archivos en formato pdf",
//            //},            
//        }
//    });
//}

function FormularioCampoGenerales() {
    $("#competencia").validate({
        errorClass: "error-dinamico",
        rules: {
            Fk_Id_Rol: {
                required: true
            },
            Fk_Id_Cargo: {
                required: true
            },
            list: {
                required: true
            },
            nuevaFormacion: {
                required: esObligatorioTematica
            },
        },
        messages: {
            Fk_Id_Rol: {
                required: "Debe elegir al menos un rol"
            },
            Fk_Id_Cargo: {
                required: "Debe elegir al menos un cargo"
            },
            list: {
                required: "Debe seleccionar un empleado"
            },
            nuevaFormacion: {
                required: "Debe ingresar una temática"
            },
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "list") {
                error.appendTo("#errorToShow");
            } else {
                error.insertAfter(element);
            }
        }

    });

}
