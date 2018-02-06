//Eliminar Accion

    $(document).ready(function () {
        $('.btnEliminarAccion').click(function () {
            var Id_Elm = $(this).attr('id');
            var Id_Elm1 = $(this).attr('name');
            swal({
                title: "Advertencia",
                text: "Estas seguro(a) que desea eliminar la acción número " + Id_Elm1 + "?",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si Borrarla",
                cancelButtonText: "No",
                closeOnConfirm: false
            },
function () {

    $.ajax({
        type: "POST",
        url: "/Acciones/EliminarAccion",
        data: '{id: "' + Id_Elm + '" }',
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: "json",
        success: function (response) {
            if (response.probar == false) {
                swal({
                    title: "Acción No Eliminada",
                    text: response.resultado,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: false
                },
                                function () {

                                });
            }
            else {
                swal({
                    title: "Acción Eliminada",
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
            $("#msj_novedad").text('No se ha podido eliminar la acción o no existe');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
        }
    });

});
        });
    });

    jQuery(function ($) {
        var items = $(".paginacc");
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


