var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};
ConstruirDatePickerPorElemento('fechapi');

jQuery.extend(jQuery.validator.messages, {
    required: "Debe diligenciar este campo"
});
$(document).ready(function () {
    $.extend(jQuery.validator.messages, {
        minlength: $.validator.format("Debe ingresar mínimo {0} caracteres"),

    });
})

$(document).ready(function () {
    $("#rbotro").click(function () {
        $("#inputOtro").show("toogle");
    });
    $("#rbfelicitacion").click(function () {
        $("#inputOtro").hide("toogle");
    });
    $("#rbpeticion").click(function () {
        $("#inputOtro").hide("toogle");
    });
    $("#rbreclamo").click(function () {
        $("#inputOtro").hide("toogle");
    });
    $("#rbqueja").click(function () {
        $("#inputOtro").hide("toogle");
    });
});

function GrabarSolicitud() {
    validarConsulta();
    if ($("#consulta").valid()) {
        PopupPosition();
        DescripcionConsultaVM = $('#descconsulta').val();
        TipoConsultaVM = $('input[name=consulta]:checked', '#consulta').val();
        if (DescripcionConsultaVM == "") {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar una Descripción para la consulta',
                showConfirmButton: true,
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            PopupPosition();
            var datos = {
                TipoConsultaVM: $('input[name=consulta]:checked', '#consulta').val(),
                DescripcionConsultaVM: $('#descconsulta').val()
            }
            $.ajax({
                url: urlBase + '/Consulta/GrabarConsultaSST',
                data: datos,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result) {
                        $("#consulta").trigger("reset");
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Información enviada satisfactoriamente.',
                            showConfirmButton: true,
                            confirmButtonText: 'Continuar',
                            confirmButtonColor: '#7E8A97'
                        });
                        $('.confirm').on('click', function () {
                            window.location.href = "../Consulta/Index";
                            OcultarPopupposition();
                        });
                    }
                }, error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error. Por favor, intente mas tarde',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

function validarConsulta() {
    $("#consulta").validate({
        errorClass: "error",
        rules: {
            descconsulta: {
                required: true
            },
            //Otro: {
            //    required: true, minlength: 10
            //},

            messages: {
                descconsulta: {
                    required: "* Ingrese una Descripción"
                },
                //Otro: {
                //    required: "* Ingrese una Solicitud"
                //},
            }
        }
    });
}


function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        // Consider adding an ID to your table
        // incase a second table ever enters the picture.
        var items = $(idTabla).find(nameTr);
        var numItems = items.length;
        var perPage = 10;

        // Only show the first 10 (or first `per_page`) items initially.
        items.slice(perPage).hide();

        // Now setup the pagination using the `.pagination-page` div.
        $(pagination).pagination({
            prevText: '<i class="fa fa-chevron-left"><i class="fa fa-chevron-left">',
            nextText: '<i class="fa fa-chevron-right"><i class="fa fa-chevron-right">',
            items: numItems,
            itemsOnPage: perPage,
            cssStyle: "compact-theme",

            // This is the actual page changing functionality.
            onPageClick: function (pageNumber) {
                // We need to show and hide `tr`s appropriately.
                var showFrom = perPage * (pageNumber - 1);
                var showTo = showFrom + perPage;

                // We'll first hide everything...
                items.hide()
                     // ... and then only show the appropriate rows.
                     .slice(showFrom, showTo).show();
            }
        });

        // EDIT: Let's cover URL fragments (i.e. #page-3 in the URL).
        // More thoroughly explained (including the regular expression) in:
        // https://github.com/bilalakil/bin/tree/master/simplepagination/page-fragment/index.html

        // We'll create a function to check the URL fragment
        // and trigger a change of page accordingly.
        function checkFragment() {
            // If there's no hash, treat it like page 1.
            var hash = window.location.hash || "#page-1";

            // We'll use a regular expression to check the hash string.
            //hash = hash.match(/^#page-(\d+)$/);
            //if (hash) {
            //    // The `selectPage` function is described in the documentation.
            //    // We've captured the page number in a regex group: `(\d+)`.
            //    $(pagination).pagination("selectPage", parseInt(hash[1]));
            //}
        };

        // We'll call this function whenever back/forward is pressed...
        $(window).bind("popstate", checkFragment);

        // ... and we'll also call it when the page has loaded
        // (which is right now).
        checkFragment();
    });
}

function ValidarConsulta() {
    var form = $("#consultarSST");
    if (!$("#consultarSST").valid()) {
        validacion = false;
    }
    else {
        validacion = true;
    }
    var fechainicio = $("#Fecha_ini").val();
    var fechafinal = $("#Fecha_Fin").val();
    var hoy = new Date();
    dia = hoy.getDate();
    mes = hoy.getMonth() + 1;
    anio = hoy.getFullYear();

    if (mes < 10) {
        fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
    } else {

        fecha_actual = String(dia + "/" + mes + "/" + anio);
    }

    if ($.datepicker.parseDate('dd/mm/yy', fechafinal) < $.datepicker.parseDate('dd/mm/yy', fechainicio)) {
        swal("Estimado Usuario", 'La Fecha Final,  no puede ser inferior a la Fecha Inicial', "warning");
        return false;
    }

    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechafinal)) {
        swal("Estimado Usuario", 'La Fecha Final,  no puede ser superior a la Fecha Actual', "warning");
        return false;
    }
    if (validacion == false) {

        swal("Estimado Usuario", 'Falta campos por llenar', "warning");
        return false;
    }
    else {
        form.submit();
        form.reset();
    }
}


function validaciontamañodocumento() {
    if (typeof FileReader !== "undefined") {
        var size = document.getElementById('input-file').files[0].size;

        if (size > 10800332.8) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe cargar un archivo con peso menor a 10 Mb',
                //timer: 4000,
                confirmButtonColor: '#7E8A97'
            })
            document.getElementById("input-file").value = "";
        }
    }
}
