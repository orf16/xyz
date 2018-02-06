var urlBase = utils.getBaseUrl();
var urlAlerta = '/Alertas'

$(document).ready(function () {
    var al = $("#frmAlertas")
    al.validate({
        rules: {
            AnioSeleccionado: {
                required: true
            }
        }, messages: {
            AnioSeleccionado: {
                required: "Debe seleccionar un año de Gestion"
            }
        }
    });
    $('#ConsultarAlerta').on("click", function () {
        var anio = $("#AnioSeleccionado").val();
        if(al.valid() != false){
            $.ajax({
                type: "POST",
                data: { anio: anio },
                url: urlBase + urlAlerta +'/ConsultarAlertas'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'Success') {
                    $('#Alertas').empty();
                    $('#Alertas').html(response.Data);
                }
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
            });
        }
    })
});

//ConsultarAlertas