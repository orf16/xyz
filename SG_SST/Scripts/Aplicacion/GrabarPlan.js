var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};
$(function () {
    $("#btnlistadoplan").hide();

});


//Funcion que envia el controlador el plan de Accion Para Ser Guardado junto con la lista de Condiciones Inseguras.
function GrabaPlanes() {
    if ($("#actividad").val() == "" || $("#responsablepa").val() == "" || $('#condicion tbody').find('tr').length==0) {
        swal({ title: "Estimado Usuario", text: "Debe Ingresar los Campos Actividad y Responsable y Seleccionar Un Item de la Lista.", type: "warning", confirmButtonColor: '#7E8A97' })
        OcultarPopupposition();
    }
    else
    {
        PopupPosition();
        var Condiciones = $('#condicion tbody').find('tr');
        var CondicionesI = new Array();
        var idTr = 0;
        $('#condicion tbody').find('tr').each(function () {
            var row = $(this);
            var check = row.find('input[type="checkbox"]');
            if (check.is(':checked')) {
                var diadesdep = parseInt(check.attr('name1'));
                var diahastap = parseInt(check.attr('name2'));
                var descripcion = check.attr('desc');
                var clave = check.attr('value');
                var condicion = new Object();
                condicion.Diasdesde = diadesdep,
                condicion.Diashasta = diahastap,
                condicion.DescribeCondicionvm = descripcion;
                condicion.pkcondicionvm = clave,
                CondicionesI.push(condicion)
            }
        });
        var plan = {
            actividadvm: $("#actividad").val(),
            responsablevm: $("#responsablepa").val(),
            fechafinvm: $("#fechafin").val(),
            Condiciones: CondicionesI,
        };
        $.ajax({
            url: urlBase + '/PlandeInspeccion/GuardarPlanesAccion',
            data: plan,
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result.Data != 0) {
                    $('input[type="checkbox"]').each(function () {
                        if ($(this).prop('checked')) {
                            $(this).closest('tr').remove();
                        }
                       
                    });
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Plan de acción generada para las Condiciones Inseguras Seleccionadas.',
                        showConfirmButton: true,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#7E8A97'
                    });
                    $('.confirm').on('click', function () {
                        $("#planas").trigger("reset");
                        
                        OcultarPopupposition();
                    });

                    if( $('#condicion tbody').find('tr').length==0)
                    {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Ha finalizado el registro completo de la Inspección, No Existen Condiciones Inseguras para generarles Plan de Accion.',
                            showConfirmButton: true,
                            confirmButtonText: 'Aceptar',
                            confirmButtonColor: '#7E8A97'
                        });
                        $('.confirm').on('click', function () {
                            $("#btnplanes").remove();
                            window.location.href = '../PlandeInspeccion/Index';
                            
                            OcultarPopupposition();
                        });
                        
                         $("#btnagregarcondicion").remove();
                    }
                    $("#btnlistadoplan").show();
                    //$("#btnplanes").show("toogle"); 
                    OcultarPopupposition();
                }
                if (result.Data == 0) {

                    swal({ title: "Estimado Usuario", text: "Item Seleccionado fué registrado anteriormente.", type: "warning", confirmButtonColor: '#7E8A97' })
                    OcultarPopupposition();
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

       
    //}
}
function CamposGrabarplanaccion() {
    $('#planacciones').validate({
        errorClass: "error",
        rules: {
            actividad: { required: true },
            responsable: { required: true },
            checkcondicion: { required: true },

        },
        messages: {
            actividad: { required: "Debe ingresar una Actividad" },
            responsable: { required: "Debe ingresar un Responsable" },
            checkcondicion: { required: "Debe seleccionar una Condición" },
        }
    });

}