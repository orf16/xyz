var urlBase = utils.getBaseUrl();
var urlEstudioPT = '/EstudioPuestoTrabajo';

$(document).ready(function () {
    ConstruirDatePickerPorElemento('Fecha');

    $("#form_seguimiento").validate({
        rules: {
            Actividad: {
                required: true
            },
            Fecha: {
                required: true
            },
            Responsable: {
                required: true
            }
        },
        messages: {
            Actividad: {
                required: "La actividad es obligatoria"
            },
            Fecha: {
                required: "La fecha es obligatoria"
            },
            Responsable: {
                required: "El responsable es obligatorio"
            }
            
        }
    });

    $('#Documento').on('change', function () {
        var documento = $("#Documento").val();
        if (documento != '') {
            DatosTrabajador(documento);
        } else
            swal('Estimado Usuario', 'Seleccione un valor válido para continuar.');
        return false;
    });


    $("#btnNuevoSeguimiento").click(function (event) {
        debugger;
        event.preventDefault();

        if (!$("#form_seguimiento").valid()) {
            return false;
        }
        
        FechaSeg = $("#Fecha").val();
                
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = (day < 10 ? '0' : '') + day + '/' +
            (month < 10 ? '0' : '') + month + '/' +
            d.getFullYear();

        if (new Date(FechaSeg).getTime() < new Date(output).getTime()) {

            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'La Fecha del Seguimiento no puede ser menor a la Fecha Actual.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
            return false;
        }
        else {
            var objNuevoSeguimiento = {
                Actividad: $('#Actividad').val(),
                FechaStr: $('#Fecha').val(),
                Responsable: $('#Responsable').val(),
                IdEstudioPuestoTrabajo: $('#IdEstudioPT').val()
            };

            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + urlEstudioPT + '/NuevoSeguimiento',
                data: objNuevoSeguimiento
            }).done(function (response) {
                if (response != undefined && response != '' && response.status == 'Success') {
                    OcultarPopupposition();
                    swal({
                        title: 'Estimado Usuario',
                        text: response.Message,
                        type: 'success',
                        showCancelButton: false
                    });
                    GridSeguimiento();
                    $('#Actividad').val('');
                    $('#Responsable').val('');
                }
                else if (response != undefined && response != '' && response.status == 'Error') {
                    OcultarPopupposition();
                    swal({
                        title: 'Estimado Usuario',
                        text: response.Message,
                        type: 'warning',
                        showCancelButton: false
                    });
                }
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
                OcultarPopupposition();
                swal({
                    title: 'Estimado Usuario',
                    text: 'No se logró registar el seguimiento de estudio de Puesto de Trabajo. Intente nuevamente',
                    type: 'warning',
                    showCancelButton: false
                });
            });
        }

    });

    function GridSeguimiento() {
        
        var objGridSeguimiento = {
            Actividad: $('#Actividad').val(),
            Fecha: $('#Fecha').val(),
            Responsable: $('#Responsable').val(),
            IdEstudioPuestoTrabajo: $('#IdEstudioPT').val()
        };
        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + urlEstudioPT + '/MostrarGridSeguimiento',
            data: objGridSeguimiento
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#GridSeguimiento').empty();
                $('#GridSeguimiento').html(response.Data);
            }
            OcultarPopupposition();
        }).fail(function (response) {
            swal("Estimado Usuario", "No se logró consultar la información del Seguimiento. Intente más tarde.");
            OcultarPopupposition();
        });
    }

});