

var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};
ConstruirDatePickerPorElemento('fechapi');

$(document).ready(function () {
    $.extend(jQuery.validator.messages, {
        minlength: $.validator.format("Debe ingresar mínimo {0} caracteres"),

    });
})
$("#panelcondiciones").hide();
///Funcion para guardar la planeacion de una inspeccion.
function CamposGrabar() {
    CamposGrabarplan();
    if ($("#formtipoI").valid()) {
        PopupPosition();
        var Fecha = $("#fechapi").val();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        if ($.datepicker.parseDate('dd/mm/yy', Fecha) < $.datepicker.parseDate('dd/mm/yy', fecha_actual)) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'La Fecha ingresada no puede ser inferior a la Fecha actual',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
            return false;       
        }
        else {
            var planInspeccion = {
                DescripcionTipoInspeccion: $("#DescripcionTipoInspeccion").val(),
                responsable: $("#responsable").val(),
                Fecha: $("#fechapi").val(),
                idEmpresaVM: $("#idEmpresaVM").val(),

            }
            $.ajax({
                url: urlBase + '/PlandeInspeccion/CrearPlaneacionInspeccion',
                data: planInspeccion,
                type: 'POST',
                success: function (result) {
                    if (result.Data) {
                        OcultarPopupposition();
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Planeación de Inspección generada',
                            showConfirmButton: true,
                            confirmButtonText: 'Registrar Inspección',
                            confirmButtonColor: '#7E8A97'
                        });
                        $('.confirm').on('click', function () {
                            window.location.href = "../PlandeInspeccion/CrearI";
                            OcultarPopupposition();
                        });
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error en la transacción',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }


            });
        }
    }
}
function CamposGrabarplan() {
    $('#formtipoI').validate({
        errorClass: "error",
        rules: {
            DescripcionTipoInspeccion: { required: true },
            responsable: { required: true, minlength:10, maxlength:50 },
            fechapi: { required: true }
        },
        messages: {
            DescripcionTipoInspeccion: { required: "Debe seleccionar un Tipo de Inspección" },
            responsable: { required: "Debe ingresar un Responsable" },
            fechapi: { required: "Debe ingresar una Fecha" }
        }
    });
};

///funcion para continuar con la Planeacion generada sin Ejecutar
function ContinuarEjecucionPlan(){
    
    var continuar =
    {
        DescripcionTipoInspecciona: $("#DescripcionTipoInspecciona").val(),
        Responsable: $("#Responsable").val(),
        FechaPI: $("#FechaPI").val(),
        Consecutivo: $("#Consecutivo").val(),
        IdPkPlan: $("#IdPlan").val(),
    }
        $.ajax({
            url: urlBase + '/PlandeInspeccion/ContinuarEjecucionPlan',
            data: continuar,
            type: 'POST',
            success: function (result) {
                if (result) {
                    //swal({
                    //    type: 'success',
                    //    title: 'Estimado Usuario',
                    //    text: 'Información de Plan Inspección Obtenida con Exito.',
                    //    showConfirmButton: true,
                    //    confirmButtonText: 'Continuar Inspección',
                    //    confirmButtonColor: '#7E8A97'
                    //});
                    //$('.confirm').on('click', function () {
                    //    Window.location.href = "../PlandeInspeccion/ContinuarEjecucionPlan";
                       
                    //});
                }
            }
        });

    


}
