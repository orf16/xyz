var urlBase = utils.getBaseUrl();

$(document).ready(function () {
    var conf = $("#frmConfiguracion");
    conf.validate({
        rules: {
            Anio: { required: true },
            MesSeleccionado: { required: true },
            XT: { required: true, min: 1, digits: true },
            DTM: { required: true, min: 1, digits: true },
            HTD: { required: true, min: 1, digits: true },
            NHE: { required: true, digits: true },
        }, messages: {
            Anio: { required: "Este Campo es Obligatorio" },
            MesSeleccionado: { required: "Este Campo es Obligatorio" },
            XT: {
                required: "Este Campo es Obligatorio",
                min: "Este Campo es Obligatorio",
                digits: "Este Campo debe ser numérico"
            },
            DTM: {
                required: "Este Campo es Obligatorio",
                min: "Este Campo es Obligatorio",
                digits: "Este Campo debe ser numérico"
            },
            HTD: {
                required: "Este Campo es Obligatorio",
                min: "Este Campo es Obligatorio",
                digits: "Este Campo debe ser numérico"
            },
            NHE: { required: "Este Campo es Obligatorio", digits: "Este Campo debe ser numérico" },
        }
    });

    $('#contentnha').tooltip({ title: "<h5 width:80px><em>Este valor es cambiante y afecta automáticamente al total HHT, depende de los ausentismos registrados.</em></h5>", html: true, placement: "top" });


    $("#XT").focusout(function () {
        var total = 0;
        var xt = parseInt($("#XT").val())
        var dmt = parseInt($("#DTM").val())
        var htd = parseInt($("#HTD").val())
        var nhe = parseInt($("#NHE").val())
        var nha = parseInt($("#NHA").val())
        if (xt > 0 && dmt > 0 && htd > 0) {
            total = (xt * htd * dmt)
            total = total + nhe
            total = total - nha
        }
        $("#Total").val(total);
    });

    $("#DTM").focusout(function () {
        var total = 0;
        var xt = parseInt($("#XT").val())
        var dmt = parseInt($("#DTM").val())
        var htd = parseInt($("#HTD").val())
        var nhe = parseInt($("#NHE").val())
        var nha = parseInt($("#NHA").val())
        if (xt > 0 && dmt > 0 && htd > 0) {
            total = (xt * htd * dmt)
            total = total + nhe
            total = total - nha
        }
        $("#Total").val(total);
    });

    $("#HTD").focusout(function () {
        var total = 0;
        var xt = parseInt($("#XT").val())
        var dmt = parseInt($("#DTM").val())
        var htd = parseInt($("#HTD").val())
        var nhe = parseInt($("#NHE").val())
        var nha = parseInt($("#NHA").val())
        if (xt > 0 && dmt > 0 && htd > 0) {
            total = (xt * htd * dmt)
            total = total + nhe
            total = total - nha
        }
        $("#Total").val(total);
    });

    $("#NHE").focusout(function () {
        var total = 0;
        var xt = parseInt($("#XT").val())
        var dmt = parseInt($("#DTM").val())
        var htd = parseInt($("#HTD").val())
        var nhe = parseInt($("#NHE").val())
        var nha = parseInt($("#NHA").val())
        if (xt > 0 && dmt > 0 && htd > 0) {
            total = ((xt * htd * dmt) + nhe) - nha
        }
        $("#Total").val(total);
    });

    $("#guardarConfiguracion").click(function () {
        if (conf.valid() != false) {
            GuardarConfiguracion();
        }
    });

    //$("#Anio").change(function () {        
    //    var mes = $("#MesSeleccionado").val()
    //    var ano = $("#Anio").val()
    //    if (mes != "") {
    //        PopupPosition();
    //        $.ajax({
    //            type: "POST",
    //            data: { mes: mes, ano: ano },
    //            url: urlBase + '/Configuracion_HHT/AusenciasMes'
    //        }).done(function (response) {
    //            if (response != undefined && response != '' && response.Mensaje == 'Success') {
    //                var nha = response.Data
    //                $("#NHA").val(nha);
    //                OcultarPopupposition();
    //            }
    //        }).fail(function (response) {
    //            console.log("Error en la peticion: " + response);
    //            OcultarPopupposition();
    //        });
    //    }
    //})

    $("#XT").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#XT", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#DTM").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#DTM", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#HTD").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#HTD", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#NHE").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#NHE", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#MesSeleccionado").change(function () {
        if (!$("#Anio").valid()) {
            validacion = false;
            return false;
        }

        var mes = $("#MesSeleccionado").val()
        var ano = $("#Anio").val()
        PopupPosition();
        $.ajax({
            type: "POST",
            data: { mes: mes, ano: ano },
            url: urlBase + '/Configuracion_HHT/AusenciasMes'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                var nha = response.Data
                $("#NHA").val(nha);
                OcultarPopupposition();
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response);
            OcultarPopupposition();
        });
    })

    $("#ConsultarConfiguracion").click(function () {
        var ano = $("#Anio").val()
        PopupPosition();
        $.ajax({
            type: "post",
            data: { ano: ano },
            url: urlBase + '/Configuracion_HHT/CargarConfiguraciones'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $("#tablaConfiguraciones").empty();
                $('#tablaConfiguraciones').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'FAILD') {
                $('pnlconfiguraciones').prop('visibility', true);
                $("#tablaConfiguraciones").empty();
            }
            else if (response != undefined && response != '' && response.Mensaje == 'FinSesion') {
                window.location.href = urlBase + '/Home/Login'
            }

            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
            swal("Registro configuración HHT", "No Existen registros para el año seleccionado.");
        });
    });


    function GuardarConfiguracion() {

        if (!$("#XT").valid()) {
            validacion = false;
            return false;
        }

        var objConfiguracion = {
            IdEmpresaSeleccionada: $("#IdEmpresaSeleccionada").val(),
            "Anio": $("#Anio").val(),
            "MesSeleccionado": $("#MesSeleccionado").val(),
            "XT": $("#XT").val(),
            "DTM": $("#DTM").val(),
            "HTD": $("#HTD").val(),
            "NHE": $("#NHE").val(),
            "NHA": $("#NHA").val(),
            "Total": $("#Total").val(),
            "IsLunesViernes": $('input[id="lunesviernes"]:checked').length > 0,
            "IsLunesSabado": $('input[id="lunessabado"]:checked').length > 0
        };
        PopupPosition();
        $.ajax({
            type: "post",
            url: urlBase + '/Configuracion_HHT/ConfiguracionHHT',
            data: objConfiguracion
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                swal("Registro configuración HHT", "Se ha registrado la configuración HHT");
            } else if (response != undefined && response != '' && response.Mensaje == 'FAILD') {
                swal("Registro configuración HHT", "No se pudo registrar la congiguración. Intente más tarde.");
            }
            OcultarPopupposition();
        }).fail(function (response) {
            OcultarPopupposition();
            swal("Registro configuración HHT", "No se pudo registrar la congiguración. Intente más tarde.");
        });
    }
});
