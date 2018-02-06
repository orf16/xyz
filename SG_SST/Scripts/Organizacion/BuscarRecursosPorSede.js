
var urlBase = ""

try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

////Funcion para Buscar los Recursos Asignados por sede
function BuscarRecursosPorSede() {
    if ($("#recursossede").valid()) {
        PopupPosition();
        $.ajax({
            url: urlBase + '/Recursos/BuscarRecursosPorSede',  
            data: {
                Pk_Id_Sede: $("#Pk_Id_Sede").val(),
                Periodo: $("#Periodo").val()
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result) {
                    $('#tablaPV').html(result)
                }
                else
                {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encontraron registros.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error. Intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}