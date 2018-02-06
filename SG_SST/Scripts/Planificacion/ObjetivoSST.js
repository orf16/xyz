var urlBase = utils.getBaseUrl();
var urlObjetivo = '/ObetivoSST';
$(document).ready(function () {
    ConstruirDatePickerPorElemento('FechaInicial');
    ConstruirDatePickerPorElemento('FechaFinal');

    $("#frmObjetivos").validate({
        rules: {
            Descripcion: {
                required: true
            },
            Meta: {
                required: true,
                min: 1,
                max: 100
            }
        },
        messages: {
            Descripcion: {
                required: "Este compo es obligatorio"
            },
            Meta: {
                required: "Este compo es obligatorio",
                min: "El valor debe ser mayor a 0",
                max: "El valor no puede ser mayor de 100"
            }
        }
    });

    $("#Meta").keypress(function (tecla) {
        if (tecla.charCode > 47 && tecla.charCode < 58) return true;
        else
            return false;
    });

    $(document).on("input", "#Descripcion", function (tecla) {
        var limite = 2000;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });


    PopupPosition();
    $.ajax({
        type: "POST",
        url: urlBase + urlObjetivo + '/ObtenerObjetivos'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Mensaje == 'OK') {
            $('#tablaConfiguraciones').empty();
            $('#tablaConfiguraciones').html(response.Data);
            OcultarPopupposition()
        }
        if (response != undefined && response.Mensaje == 'ERROR') {
            $('#tablaConfiguraciones').empty();
            OcultarPopupposition()
        }
        if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
            swal({
                title: 'Estimado Usuario',
                text: response.Data,
                type: 'warning',
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
                closeOnCancel: false,
                html: true
            }, function (isConfirm) {
                if (isConfirm) {
                    window.location.href = '../Home/Login';
                }
            });
            OcultarPopupposition()
        }
    }).fail(function (response) {
        console.log("Error en la peticion: " + response.Data);
        OcultarPopupposition()
    });
    OcultarPopupposition()


    $("#btnCrearObjetivo").click(function () {
        if (!$("#frmObjetivos").valid())
            return false;


        var datos = {
            Descripcion: $("#Descripcion").val(),
            Meta: $("#Meta").val(),
            EsPorcentaje: $('input:radio[class=porcentaje]:checked').val()
        };

        PopupPosition();
        $.ajax({
            type: "POST",
            data: datos,
            url: urlBase + urlObjetivo + '/CrearObjetivo'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                swal({
                    title: 'Estimado Usuario',
                    text: 'El objetivo ha sido creado correctamente',
                    type: 'success',
                    confirmButtonText: "Aceptar",                    
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    html: true
                });
                $('#tablaConfiguraciones').empty();
                $('#tablaConfiguraciones').html(response.Data);
                $("#Descripcion").val("");
                $("#Meta").val("");
            }
            OcultarPopupposition();
            if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal({
                    title: 'Estimado Usuario',
                    text: response.Data,
                    type: 'warning',
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    html: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = '../Home/Login';
                    }
                });
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition()
        });
    });

});