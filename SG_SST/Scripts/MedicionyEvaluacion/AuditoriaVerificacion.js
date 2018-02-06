//Guardar Registro Lista

$(function () {
    AgregarLista
    $("#AgregarLista").bind("click", function () {
        var onEventLaunchGuardar = new postGuardar();
        onEventLaunchGuardar.launchGuardar();
    });
});
function postGuardar() {
    this.launchGuardar = function () {

        //Traer datos al modelo JSON

        var stringArray = new Array();
        stringArray[0] = $("#Pregunta").val();
        stringArray[1] = $("#Requisito").val();
        stringArray[2] = $("#Hallazgo").val();
        stringArray[3] = $("#TipoHallazgo option:selected").val();
        stringArray[4] = $("#EdicionAuditoria").val();

        // Construir objeto JSON
        var EDAuditoriaVerificacion = {
            Fk_Id_Auditoria: stringArray[4],
            Pregunta: stringArray[0],
            Requisito: stringArray[1],
            Hallazgo: stringArray[2],
            Tipo_Hallazgo: stringArray[3]

        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/AgregarListaVerificacion',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(EDAuditoriaVerificacion),
            success: function (data) {
                OcultarPopupposition();
                $("#val-pregunta").css("display", "none");
                $("#val-pregunta").text('');
                $("#val-requisito").css("display", "none");
                $("#val-requisito").text('');
                $("#val-deschallazgo").css("display", "none");
                $("#val-deschallazgo").text('');
                $("#val-hallazgo").css("display", "none");
                $("#val-hallazgo").text('');

                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-pregunta").css("display", "block");
                        $("#val-pregunta").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-requisito").css("display", "block");
                        $("#val-requisito").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-deschallazgo").css("display", "block");
                        $("#val-deschallazgo").text(data.ValidacionStr[2]);
                    }
                    if (data.Validacion[3] == true) {
                        $("#val-hallazgo").css("display", "block");
                        $("#val-hallazgo").text(data.ValidacionStr[3]);
                    }
                    swal("Advertencia", data.Estado);
                }
                else {
                    swal({
                        title: "Se agrego a la lista de verificación correctamente",
                        text: "",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: false
                    },
                function () {
                    location.reload(true);
                });
                }
            },
            error: function (data) {
            OcultarPopupposition();

            }
        });

    }
}


//Mostrar Cuadro Edicion
$(function () {
    $('.btnEditarlista').click(function () {
        $('#EdicionListaDiv').hide();
        $('#EdicionPregunta').removeAttr('value');
        $('#EdicionRequisito').removeAttr('value');
        $('#EdicionHallazgo').removeAttr('value');
        $('#EdicionTipo').removeAttr('value');
        $('#PK_Lista_Ed').removeAttr('value');
        var Id_Elm = $(this).attr('id');
        $.ajax({
            type: "POST",
            url: "/Auditoria/MostrarEdicionListaVerificacion",
            data: '{values: "' + Id_Elm + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {
                if (response.probar == false) {
                    $('#EdicionListaDiv').hide();
                }
                else {
                    $('#EdicionListaDiv').show();
                    var pk_s = response.Model.Pk_Id_Lista_Verificacion;
                    var Pregunta_s = response.Model.Pregunta;
                    var Requisito_s = response.Model.Requisito;
                    var Hallazgo_s = response.Model.Hallazgo;
                    var Tipo_s = response.Model.Tipo_Hallazgo;
                    JSON.stringify(Pregunta_s);
                    JSON.stringify(Requisito_s);
                    JSON.stringify(Hallazgo_s);
                    JSON.stringify(Tipo_s);
                    JSON.stringify(pk_s);
                    $("#EdicionPregunta").val(Pregunta_s);
                    $("#EdicionRequisito").val(Requisito_s);
                    $("#EdicionHallazgo").val(Hallazgo_s);
                    $("#EdicionTipo").val(Tipo_s);
                    $("#PK_Lista_Ed").val(pk_s);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#msj_novedad").text('No se ha podido editar este elemento de la lista o no existe');
                $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
            }
        });
    });
});

//Eliminar Actividad
$(function () {
    $('.btnEliminarlista').click(function () {
        var Id_Elm = $(this).attr('id');
        swal({
            title: "Advertencia",
            text: "Estas seguro(a) que desea eliminar este elemento de la lista?",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si Borrarlo, por favor",
            cancelButtonText: "No",
            closeOnConfirm: false
        },
function () {

    $.ajax({
        type: "POST",
        url: "/Auditoria/EliminarListaVerificacion",
        data: '{values: "' + Id_Elm + '" }',
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
            $("#msj_novedad").text('No se ha podido eliminar este elemento de la lista o no existe');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
        }
    });

});

    });
});

//Editar Registro Lista
$(function () {
    $("#EditarLista").bind("click", function () {
        var onEventLaunchGuardar1 = new postGuardar1();
        onEventLaunchGuardar1.launchGuardar1();
    });
});
function postGuardar1() {
    this.launchGuardar1 = function () {
        //Traer datos al modelo JSON
        var stringArray = new Array();
        stringArray[0] = $("#EdicionPregunta").val();
        stringArray[1] = $("#EdicionRequisito").val();
        stringArray[2] = $("#EdicionHallazgo").val();
        stringArray[3] = $("#EdicionTipo").val();
        stringArray[4] = $("#PK_Lista_Ed").val();
        stringArray[5] = $("#EdicionAuditoria").val();

        // Construir objeto JSON
        var EDAuditoriaVerificacion = {
            Pk_Id_Lista_Verificacion: stringArray[4],
            Pregunta: stringArray[0],
            Requisito: stringArray[1],
            Hallazgo: stringArray[2],
            Tipo_Hallazgo: stringArray[3],
            Fk_Id_Auditoria: stringArray[5]
        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/EditarListaVerificacion',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(EDAuditoriaVerificacion),
            success: function (data) {
                OcultarPopupposition();
                $("#val-preguntaed").css("display", "none");
                $("#val-preguntaed").text('');
                $("#val-requisitoed").css("display", "none");
                $("#val-requisitoed").text('');
                $("#val-deschallazgoed").css("display", "none");
                $("#val-deschallazgoed").text('');
                $("#val-hallazgoed").css("display", "none");
                $("#val-hallazgoed").text('');

                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-preguntaed").css("display", "block");
                        $("#val-preguntaed").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-requisitoed").css("display", "block");
                        $("#val-requisitoed").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-deschallazgoed").css("display", "block");
                        $("#val-deschallazgoed").text(data.ValidacionStr[2]);
                    }
                    if (data.Validacion[3] == true) {
                        $("#val-hallazgoed").css("display", "block");
                        $("#val-hallazgoed").text(data.ValidacionStr[3]);
                    }

                    swal("Advertencia", data.Estado);

                }
                else {
                    swal({
                        title: "Se editó la lista de verificación correctamente",
                        text: "",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: false
                    },
                function () {
                    location.reload(true);
                });
                }
            },
            error: function (data) {
                OcultarPopupposition();
                console.log(data.Estado)
            }
        });

    }
}

//Cancelar cuadro edicion
$(function () {
    $('#EditarCancelar').click(function () {
        $('#EdicionListaDiv').hide();
        $('#EdicionPregunta').removeAttr('value');
        $('#EdicionRequisito').removeAttr('value');
        $('#EdicionHallazgo').removeAttr('value');
        $('#EdicionTipo').removeAttr('value');
        $('#PK_Lista_Ed').removeAttr('value');
    });
});

jQuery(function ($) {
    var items = $("table tbody tr");
    var numItems = items.length;
    var perPage = 5;
    var PageInit = numItems / perPage;
    var PageInitMod = numItems % perPage;
    var pageInitu=Math.ceil(PageInit);
    pageInitu = pageInitu - 1;

    //PageInit = parseInt(PageInit, 10);
    items.slice(perPage).hide();
    $(".pagination").pagination({
        items: numItems,
        itemsOnPage: perPage,
        invertPageOrder: false,
        cssStyle: "compact-theme",
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
