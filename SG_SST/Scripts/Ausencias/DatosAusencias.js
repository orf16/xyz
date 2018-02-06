var urlBase = utils.getBaseUrl();
var urlAusencias = '/Ausencias';

$(document).ready(function () {
    $('#FrmAusencias').find('input[type="text"]').on('focus', function () {
        $(this).siblings().hide();

    });
    ConstruirDatePickerPorElemento('dateDesde');
    ConstruirDatePickerPorElemento('dateHasta');
});

$("#inputDocumento").keypress(function (tecla) {
    if (tecla.charCode < 48 || tecla.charCode > 57) return false;
});

$(document).on("input", "#inputDocumento", function () {
    var limite = 50;
    var textreal = $(this).val();
    var text;

    if ($(this).val().length > limite) {
        text = textreal.substr(0, limite);
        $(this).val(text);
    }
});

$("#inputDocumento").autocomplete({
    minLength: 2,
    source: function (request, response) {
        $.ajax({
            url: urlBase + urlAusencias + "/AutoCompletarDocumentos",
            type: "POST",
            dataType: "json",
            data: { prefijo: request.term },
        }).done(function (data) {
            response($.map(data, function (item) {
                return { label: item };
            }))
        })
    },
    focus: function (event, ui) {
        event.preventDefault();
        $(this).val(ui.item.label);
    },
    select: function (event, ui) {
        event.preventDefault();
        $(this).val(ui.item.label);
        $("#inputDocumento").val(ui.item.value);
    }
});


$("#FrmAusencias").validate({
    rules: {
        //inputDocumento: {
        //    required: true
        //},
        //Sede : {
        //    required: true,
        //    min: 1
        //},
        //Diagnostico_IdDiagnoticoSeleccionado: {
        //    required: true, 
        //    min: 1
        //},
        IdEmpresaUsuaria: {
            required: true,
            min: 1
        }
    },
    messages: {
        //inputDocumento: {
        //    required: "Este compo es obligatorio",
        //},
        //Sede: {
        //    required: "Debe seleccionar una sede",
        //    min: "Debe seleccionar una sede"
        //},
        //Diagnostico_IdDiagnoticoSeleccionado: {
        //    required: "Debe seleccionar una causa",
        //    min: "Debe seleccionar una causa"
        //},
        IdEmpresaUsuaria: {
            required: "Debe seleccionar una empresa usuaria",
            min: "Debe seleccionar una enpresa usuaria"
        }
    }
});
function ValidarFiltros() {
    var tipoDoc = $('#inputDocumento').val();
    var sede = $('#Sede').val();
    var causa = $('#Diagnostico_IdDiagnoticoSeleccionado').val();
    var FechaInicio = $("#dateDesde").val();
    var FechaFin = $("#dateHasta").val();
    var idempresaUsuaria = $('#IdEmpresaUsuaria').val();
    if (tipoDoc == '' && sede == '' && causa == '' && FechaInicio == '' && FechaFin == '' && idempresaUsuaria == '') {
        return false;
    }
    else if (tipoDoc != '' || sede != '' && causa != '') {
        if (FechaInicio != '') {
            if (FechaFin == '')
                return false;
            else
                return true;
        }
        else if (FechaFin != '') {
            if (FechaInicio == '')
                return false;
            else
                return true;
        }
        else
            return true;
    }
    else if (FechaInicio != '') {
        if (FechaFin == '')
            return false;
        else
            return true;
    }
    else if (FechaFin != '') {
        if (FechaInicio == '')
            return false;
        else
            return true;
    }
    else
        return true;        
}

$(document).ready(function () {
    $("#DescargarExcel").click(function () {
        //if (!$("#FrmAusencias").valid())
        if (!ValidarFiltros()) {
            swal("Atención", 'Por favor verifique que los criterios de busqueda son correctos')
            return false;
        }
        var datos = {
            Documento: $("#inputDocumento").val(),
            FechaInicio: $("#dateDesde").val(),
            FechaFin: $("#dateHasta").val(),
            idSede: $("#Sede").val(),
            IdDiagnostico: $("#Diagnostico_IdDiagnoticoSeleccionado").val(),
            IdEmpresaUsuaria: $("#IdEmpresaUsuaria").val()
        };
        //if (datos.Documento == '' && datos.FechaInicio == ''
        //    && datos.FechaFin == '' && datos.Sede == '' && datos.IdDiagnostico == '' && datos.IdEmpresaUsuaria == '') {
        //    swal("Atención", 'Debe ingresar al menos un criterio de búsqueda')
        //    return false;
        //}

        //if (datos.FechaInicio != '') {
        //    if (datos.FechaFin == '') {
        //        swal("Atención", 'Debe ingresar el parametro de la fecha final para realizar la consulta')
        //        return false;
        //    }
        //}

        //if (datos.FechaFin != '') {
        //    if (datos.FechaInicio == '') {
        //        swal("Atención", 'Debe ingresar el parametro de la fecha de inicio para realizar la consulta')
        //        return false;
        //    }
        //}

        if (Date.parse(datos.FechaInicio) > Date.parse(datos.FechaFin)) {
            swal("Atención", 'La fecha de inicio no puede ser mayor que la fecha fin')
            return false;
        }

        var temp = new Date();
        if (Date.parse(datos.FechaInicio) > temp) {
            swal("Atención", 'La fecha de inicio no puede ser mayor que la fecha actual')
            return false;
        }
        PopupPosition();
        $.ajax({
            type: "POST",
            data: datos,
            url: urlBase + urlAusencias + '/GenerarExcelAusencias'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                window.location.href = urlBase + urlAusencias + response.Data;
            }
            OcultarPopupposition();
            if (response != undefined && response != '' && response.Mensaje == '') {
                swal("Atención", 'No se encontraron registros para los parametros de busqueda ingresados')
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            OcultarPopupposition()
        });
    });

    $("#ConsultarAusencias").click(function () {
        //if (!$("#FrmAusencias").valid())
        if (!ValidarFiltros()) {
            swal("Atención", 'Por favor verifique que los criterios de busqueda son correctos.')
            $('#ResultadoAusencias').empty();
            $('#contebotonexcel').hide();
            return false;
        }
        var datos = {
            Documento: $("#inputDocumento").val(),
            FechaInicio: $("#dateDesde").val(),
            FechaFin: $("#dateHasta").val(),
            idSede: $("#Sede").val(),
            IdDiagnostico: $("#Diagnostico_IdDiagnoticoSeleccionado").val(),
            IdEmpresaUsuaria: $("#IdEmpresaUsuaria").val()
        };

        var fecha1 = new Date(datos.FechaInicio.split('/')[2], datos.FechaInicio.split('/')[1] - 1, datos.FechaInicio.split('/')[0]);
        var fecha2 = new Date(datos.FechaFin.split('/')[2], datos.FechaFin.split('/')[1] - 1, datos.FechaFin.split('/')[0]);

        //if (datos.Documento == '' && datos.FechaInicio == ''
        //    && datos.FechaFin == '' && datos.Sede == '' && datos.IdDiagnostico == '' && datos.IdEmpresaUsuaria == '') {
        //    swal("Atención", 'Debe ingresar al menos un criterio de búsqueda')
        //    return false;
        //}

        //if (datos.FechaInicio != '') {
        //    if (datos.FechaFin == '') {
        //        swal("Atención", 'Debe ingresar el parametro de la fecha final para realizar la consulta')
        //        return false;
        //    }
        //}

        //if (datos.FechaFin != '') {
        //    if (datos.FechaInicio == '') {
        //        swal("Atención", 'Debe ingresar el parametro de la fecha de inicio para realizar la consulta')
        //        return false;
        //    }
        //}

        if (fecha1.getTime() > fecha2.getTime()) {
            swal("Atención", 'La fecha de inicio no puede ser mayor que la fecha fin')
            return false;
        }

        var temp = new Date();
        if (fecha1.getTime() > temp.getTime()) {
            swal("Atención", 'La fecha de inicio no puede ser mayor que la fecha actual')
            return false;
        }
        PopupPosition();
        $.ajax({
            type: "POST",
            data: datos,
            url: urlBase + urlAusencias + '/ConsultarAusencia'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#ResultadoAusencias').empty();
                $('#contebotonexcel').show();
                $('#ResultadoAusencias').html(response.Data);
            }
            OcultarPopupposition();
            if (response != undefined && response != '' && response.Mensaje == '') {
                swal("Atención", 'No se encontraron registros para los parametros de busqueda ingresados')
                $('#ResultadoAusencias').empty();
                $('#contebotonexcel').hide();
            }

        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
            $('#contebotonexcel').hide();
            OcultarPopupposition()
        });
    });
});