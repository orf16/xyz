var urlBase = utils.getBaseUrl();
var urlAlerta = '/Ausencias'

$(document).ready(function () {
    var al = $("#FrmAusencias")
    al.validate({
        rules: {
            AnioSeleccionado: {
                required: true
            },
            //IdEmpresaUsuaria: {
            //    required: true
            //}
        }, messages: {
            AñoSeleccionado: {
                required: "Debe seleccionar un año de Gestion"
            },
            //IdEmpresaUsuaria: {
            //    required: "Debe seleccionar la empresa usuaria"
            //}
        }
    });

    $('#ConsultarAlertarAusencia').on("click", function () {
        var anio = $("#AnioSeleccionado").val();
        var idempresa = $("#IdEmpresaUsuaria").val();
        if (idempresa == '')
            idempresa = 0;

        if (al.valid() != false) {
            $.ajax({
                type: "POST",
                data: { anioGestion: anio, idEmpresaUsuaria: idempresa },
                url: urlBase + urlAlerta + '/AlertarAusenciaConsultar'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'Success') {
                    $('#ResultadoAusencias').empty();
                    $('#ResultadoAusencias').html(response.Data);
                }
                else if (response != undefined && response.Data =='' && response.Mensaje == 'Fail') {
                    swal("Atención", 'No existen datos registros en alerta para visualizar');
                    $('#ResultadoAusencias').empty();                    
                }
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
            });
        }
    })
});

//ConsultarAlertas