var urlBase = utils.getBaseUrl();
var urlIncidentes = '/Incidente';

$(document).ready(function () {
    ConstruirDatePickerPorElemento('FechaInicial');
    ConstruirDatePickerPorElemento('FechaFinal');

    $("#Frmconsulincidente").validate({
        rules: {
            FechaInicial: {
                required: true
            },
            FechaFinal: {
                required: true
            }
        },
        messages: {
            FechaInicial: {
                required: "Este campo es obligatorio"
            },
            FechaFinal: {
                required: "Este campo es obligatorio"
            }
        }
    });

    $("#FechaInicial").keypress(function (tecla) {
        if (tecla.charCode > 0) return false;
    });

    $("#FechaFinal").keypress(function (tecla) {
        if (tecla.charCode > 0) return false;
    });


    $('#FechaFinal').on("change", function (e) {
        console.log(4);
        var f1 = document.getElementById("FechaInicial").value;
        var f2 = document.getElementById("FechaFinal").value;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        var dtActual = new Date();

        if (f1 == '') {
            swal("Estimado Usuario", 'Debe seleccionar una fecha inicial');
            $('#FechaFinal').val('')
            return false;
        }

        var dtActual = new Date();
        if (dtActual.getTime() < fecha2.getTime()) {
            swal("Estimado Usuario", 'La Fecha de finalización no puede ser mayor a la actual');
            $('#FechaFinal').val('')
            return false;
        }

        if (fecha2.getTime() < fecha1.getTime()) {
            swal("Estimado Usuario", 'La Fecha de finalización no puede ser menor a la inicial');
            $('#FechaFinal').val('')
            return false;
        }
    });

    $('#FechaInicial').on("change", function (e) {
        console.log(4);
        var f1 = document.getElementById("FechaInicial").value;
        var f2 = document.getElementById("FechaFinal").value;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        if (f2 == '')
            return false;
        if (dtActual.getTime() < fecha2.getTime()) {
            swal("Estimado Usuario", 'La Fecha de finalización no puede ser mayor a la actual');
            $('#FechaFinal').val('')
            return false;
        }

        if (fecha2.getTime() < fecha1.getTime()) {
            swal("Estimado Usuario", 'La Fecha de finalización no puede ser menor a la inicial');
            $('#FechaFinal').val('')
            return false;
        }
    });


    $(document).on("input", "#DocumentoEmpleado", function () {
        var limite = 15;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#DocumentoEmpleado").keypress(function (tecla) {
        if (tecla.charCode > 47 && tecla.charCode < 58) return true;
        else if (tecla.charCode > 64 && tecla.charCode < 91) return true;
        else if (tecla.charCode > 96 && tecla.charCode < 123) return true;
        else
            return false;
    });

    $("#traerpaginado").click(function () {
        var p = 0;
    });

    $("#traerpaginado1").click(function () {
        var p = 0;
    });



    $("#ConsultarIndidentes").click(function () {
        if (!$("#Frmconsulincidente").valid())
            return false;



        var datos = {
            FechaInicial: $("#FechaInicial").val(),
            FechaFinal: $("#FechaFinal").val(),
            DocumentoEmpleado: $("#DocumentoEmpleado").val(),
            IdConsecuencia: $("#IdConsecuencia").val(),
            idSede: $("#IdSede").val(),
            IdTipoIncidente: $("#IdTipoIncidente").val(),
            IdSitioIncidente: $("#IdSitioIncidente").val(),
            idLugarIncidente: $("#idLugarIncidente").val()
        };

        if (datos.FechaInicial != '') {
            if (datos.FechaFinal == '') {
                swal("Estimado Usuario", 'Debe ingresar el parametro de la fecha final para realizar la consulta')
                return false;
            }
        }

        if (datos.FechaFinal != '') {
            if (datos.FechaInicial == '') {
                swal("Estimado Usuario", 'Debe ingresar el parametro de la fecha de inicio para realizar la consulta')
                return false;
            }
        }

        if (Date.parse(datos.FechaInicial) > Date.parse(datos.FechaFinal)) {
            swal("Estimado Usuario", 'La fecha de inicio no puede ser mayor que la fecha fin')
            return false;
        }

        var temp = new Date();
        if (Date.parse(datos.FechaInicial) > temp) {
            swal("Estimado Usuario", 'La fecha de inicio no puede ser mayor que la fecha actual')
            return false;
        }
        PopupPosition();
        $.ajax({
            type: "POST",
            data: datos,
            url: urlBase + urlIncidentes + '/Consultar'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                $('#ResultadoIncidente').empty();
                $('#ResultadoIncidente').html(response.Data);
                $('#descargarexcel').show();
            }
            if (response != undefined && response != '' && response.Mensaje == 'ERROR') {
                swal("Estimado Usuario", response.Data)
                $('#ResultadoIncidente').empty();
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
            }
            OcultarPopupposition();
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition()
        });

    });

    $("#descargarexcel").click(function () {
        if (!$("#Frmconsulincidente").valid())
            return false;

        var datos = {
            FechaInicial: $("#FechaInicial").val(),
            FechaFinal: $("#FechaFinal").val(),
            DocumentoEmpleado: $("#DocumentoEmpleado").val(),
            IdConsecuencia: $("#IdConsecuencia").val(),
            idSede: $("#IdSede").val(),
            IdTipoIncidente: $("#IdTipoIncidente").val(),
            IdSitioIncidente: $("#IdSitioIncidente").val(),
            idLugarIncidente: $("#idLugarIncidente").val()
        };

        if (datos.FechaInicial != '') {
            if (datos.FechaFinal == '') {
                swal("Estimado Usuario", 'Debe ingresar el parametro de la fecha final para realizar la consulta')
                return false;
            }
        }

        if (datos.FechaFinal != '') {
            if (datos.FechaInicial == '') {
                swal("Estimado Usuario", 'Debe ingresar el parametro de la fecha de inicio para realizar la consulta')
                return false;
            }
        }

        if (Date.parse(datos.FechaInicial) > Date.parse(datos.FechaFinal)) {
            swal("Estimado Usuario", 'La fecha de inicio no puede ser mayor que la fecha fin')
            return false;
        }

        var temp = new Date();
        if (Date.parse(datos.FechaInicial) > temp) {
            swal("Estimado Usuario", 'La fecha de inicio no puede ser mayor que la fecha actual')
            return false;
        }
        PopupPosition();
        $.ajax({
            type: "POST",
            data: datos,
            url: urlBase + urlIncidentes + '/ObtenerExcel'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                window.location.href = response.Data
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
            }
            OcultarPopupposition();
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition()
        });
    });

});

function paginaInicial() {
    SiguientePagina(1);
}

function SiguientePagina(i) {
    var datos = {
        numPagina: i,
        FechaInicial: $("#FechaInicial").val(),
        FechaFinal: $("#FechaFinal").val(),
        DocumentoEmpleado: $("#DocumentoEmpleado").val(),
        IdConsecuencia: $("#IdConsecuencia").val(),
        idSede: $("#IdSede").val(),
        IdTipoIncidente: $("#IdTipoIncidente").val(),
        IdSitioIncidente: $("#IdSitioIncidente").val(),
        idLugarIncidente: $("#idLugarIncidente").val()
    };

    if (datos.FechaInicial != '') {
        if (datos.FechaFinal == '') {
            swal("Estimado Usuario", 'Debe ingresar el parametro de la fecha final para realizar la consulta')
            return false;
        }
    }

    if (datos.FechaFinal != '') {
        if (datos.FechaInicial == '') {
            swal("Estimado Usuario", 'Debe ingresar el parametro de la fecha de inicio para realizar la consulta')
            return false;
        }
    }

    if (Date.parse(datos.FechaInicial) > Date.parse(datos.FechaFinal)) {
        swal("Estimado Usuario", 'La fecha de inicio no puede ser mayor que la fecha fin')
        return false;
    }

    var temp = new Date();
    if (Date.parse(datos.FechaInicial) > temp) {
        swal("Estimado Usuario", 'La fecha de inicio no puede ser mayor que la fecha actual')
        return false;
    }


    PopupPosition();
    $.ajax({
        type: "POST",
        data: datos,
        url: urlBase + urlIncidentes + '/ListaIncidentes'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Mensaje == 'OK') {
            $('#ResultadoIncidente').empty();
            $('#ResultadoIncidente').html(response.Data);
            $('#descargarexcel').show();

            $('#traerpaginado_' + i).attr('class', 'active')

        }
        OcultarPopupposition();
        if (response != undefined && response != '' && response.Mensaje == 'ERROR') {
            swal("Estimado Usuario", response.Data)
            $('#ResultadoIncidente').empty();
        }

    }).fail(function (response) {
        console.log("Error en la peticion: " + response.Data);
        OcultarPopupposition()
    });
}