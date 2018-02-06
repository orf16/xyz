//Muestra los divs que se utilizan para mostrar
//el efecto de espera en una petición ajax.
function PopupPosition() {
    var topLoading = $(window).scrollTop() + ($(window).height() / 2);
    var leftLoading = $(window).scrollLeft() + ($(window).width() / 2);
    //$("#capa_loading").css({ "top": $(window).scrollTop() }).show();
    $("#capa_loading").show();
    $("#_loading").css({ "top": topLoading, "left": leftLoading }).show();
}

//Oculta los divs que se utilizan para mostrar
//el efecto de espera en una petición ajax.
function OcultarPopupposition() {
    $("#capa_loading").hide();
    $("#_loading").hide();
}

//renderiza en un input tipo text un datapicker para
//la selección de fechas
function ConstruirDatePickerPorElemento(idElemento) {
    $.datepicker.setDefaults($.datepicker.regional["es"]);
    if ($('#' + idElemento).length > 0) {
        $('#' + idElemento).datepicker({
            firstDay: 1,
            format: "dd/mm/yyyy",
            language: 'es',
            autoclose: true,
            changeMonth: true,
            changeYear: true
        });
    }
}
//calcula dias apartir de dos fechas dadas. Tambien tiene en cuenta las horas.
function CalcularDias(f1, f2, idTipoConting, horaIni, horaFn, idDiasAusencias, idFechaI, idFechaF) {
    var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
    var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);
    if (fecha1.getTime() <= fecha2.getTime()) {
        $.ajax({
            url: '../Ausencias/CalcularDiasAusenciaTrabajador',
            type: 'post',
            async: false,
            data: { fechaInicio: f1, fechaFin: f2, tipoContingencia: idTipoConting, horaInicio: horaIni, horaFin: horaFn }
        }).done(function (response) {
            if (response != undefined && response.Mensaje == 'OK')
                $('#' + idDiasAusencias).val(response.Data).attr('readonly', 'readonly');;
        }).fail(function (response) {
            swal("Atención",'No se pudo calcular la cantida de dias de ausencia. Intente nuevamente');
            $('#' + idFechaI).val('');
            $('#' + idFechaF).val('');
        });
    } else {
        swal("Atención",'La Fecha de finalizacion no puede ser menor a la inicial');
        $('#' + idFechaI).val('');
        $('#' + idFechaF).val('');
        return false;
    }
}
function MostrarPopupMsg() {
    var url = $('#btn_aceptMsg').data('href');
    if (url)
        window.location.href = url;
    else
        $.fancybox.close();
}

//representa los valores numéricos como precio
function EtiquetarValoresAPrecio(idElemento) {
    if ($("#" + idElemento).val() != undefined && $("#" + idElemento).val() != "") {
        $("#" + idElemento).val($("#" + idElemento).val().replace("$", ""));
        $("#" + idElemento).val($("#" + idElemento).val().replace(".", ""));
        $("#" + idElemento).val($("#" + idElemento).val().replace(".", ""));
        $("#" + idElemento).val($("#" + idElemento).val().replace(".", ""));
        $("#" + idElemento).val($("#" + idElemento).val().replace(".", ""));
        $("#" + idElemento).val($("#" + idElemento).val().replace(".", ""));
        $("#" + idElemento).val('$' + $("#" + idElemento).val().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1."));
    } else if ($("#" + idElemento).text() != "") {
        $("#" + idElemento).text($("#" + idElemento).text().replace("$", ""));
        $("#" + idElemento).text('$' + $("#" + idElemento).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1."));
    }
}
//Valida que no se puedan escribir caracteres diferentes a numeros.
function ValidarSoloNumeros(idElemento, tamnioCampo) {
    $("#" + idElemento).attr('maxlength', tamnioCampo);
    $("#" + idElemento).keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
            // Allow: Ctrl+A
				 (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: home, end, left, right, down, up
				(e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}
