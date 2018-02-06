//Consultar Programa
$(function () {
    $('#Consultar-Programa').click(function () {
        var stringArray = new Array();
        stringArray[0] = $("#SelectAnio1").val();
        stringArray[1] = $("#NombreProg").val();
        stringArray[2] = $("#Pk_Id_Sede1").val();
        var postData = { values: stringArray };

        $.ajax({
            type: "POST",
            url: "/Auditoria/ConsultarPrograma",
            data: postData,
            success: function (data) {
                window.location = data.url;
            },
            dataType: "json",
            traditional: true
        });
    });
});
//Eliminar Programa de Auditoria
$(function () {
    $('.Eliminar-Programa').click(function () {
        var Id_Elm = $(this).attr('id');
        swal({
            title: "Advertencia",
            text: "Estas seguro(a) que desea eliminar este programa?",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si Borrarlo, por favor",
            cancelButtonText: "No",
            closeOnConfirm: false
        },
function () {

    $.ajax({
        type: "POST",
        url: "/Auditoria/EliminarPrograma",
        data: '{IdPrograma: "' + Id_Elm + '" }',
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: "json",
        success: function (response) {
            if (response.probar == false) {
                swal({
                    title: "Advertencia",
                    text: response.resultado,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: false
                },
                                function () {
                                    location.reload(true);
                                });
            }
            else {
                swal({
                    title: "",
                    text: response.resultado,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: false
                },
                                function () {
                                    location.reload(true);
                                });
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#msj_novedad").text('No se ha podido eliminar el programa o no existe');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
        }
    });

});
    });
});
jQuery(function ($) {
    var items = $("table tbody tr");
    var numItems = items.length;
    var perPage = 10;
    items.slice(perPage).hide();
    $(".pagination").pagination({
        items: numItems,
        itemsOnPage: perPage,
        cssStyle: "compact-theme",
        invertPageOrder: false,
        currentPage: 1,
        nextText: '<i class="fa fa-chevron-right"><i class="fa fa-chevron-right">',
        prevText: '<i class="fa fa-chevron-left"><i class="fa fa-chevron-left">',
        onPageClick: function (pageNumber) {
            var showFrom = perPage * (pageNumber - 1);
            var showTo = showFrom + perPage;
            items.hide()
                 .slice(showFrom, showTo).show();
        }
    });
    function checkFragment() {
        var hash = window.location.hash || "#page-1";
        hash = hash.match(/^#page-(\d+)$/);
        if (hash) {
            $(".pagination-page").pagination("selectPage", parseInt(hash[1]));
        }
    };
    $(window).bind("popstate", checkFragment);
    checkFragment();
});
