//Fecha Script
$(document).ready(function () {
    ConstruirDatePickerPorElemento('FechaPlan');
    ConstruirDatePickerPorElemento('FechaCierreEd');
    ConstruirDatePickerPorElemento('FechaPlanEd');
    ConstruirDatePickerPorElemento('FechaPlanNuevo');
});
//Guardar Plan Accion
$(function () {

    $("#AgregarPlan").bind("click", function () {
        $(".val-message").css("display", "none");
        var onEventLaunchGuardar = new postGuardar();
        onEventLaunchGuardar.launchGuardar();
    });
});
function postGuardar() {
    this.launchGuardar = function () {

        //Traer datos al modelo JSON

        var stringArray = new Array();
        stringArray[0] = $("#ActividadNuevo").val();
        stringArray[1] = $("#ResponsableNuevo").val();
        stringArray[2] = $("#FechaPlanNuevo").val();
        stringArray[3] = $("#EdicionAuditoria").val();

        // Construir objeto JSON
        var EDActividadAuditoria = {
            Fk_Id_Auditoria: stringArray[3],
            Actividad: stringArray[0],
            Responsable: stringArray[1],
            FechaFinalizacion: stringArray[2]
        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/AgregarPlanAccion',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(EDActividadAuditoria),
            success: function (data) {
                OcultarPopupposition();
                $("#val-actividad").css("display", "none");
                $("#val-actividad").text('');
                $("#val-responsableplan").css("display", "none");
                $("#val-responsableplan").text('');
                $("#val-fechaplan").css("display", "none");
                $("#val-fechaplan").text('');
                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-actividad").css("display", "block");
                        $("#val-actividad").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-responsableplan").css("display", "block");
                        $("#val-responsableplan").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-fechaplan").css("display", "block");
                        $("#val-fechaplan").text(data.ValidacionStr[2]);
                    }
                    swal("Advertencia", data.Estado);
                }
                else {
                    swal({
                        title: "Este plan de acción se agregó correctamente",
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
//Mostrar cuadro edicion Plan Accion
$(function () {
    $('.btnEditarPlan').click(function () {

        $('#EdicionListaDivPlan').hide();

        $('#ActividadPlanEd').removeAttr('value');
        $('#ResponsablePlanEd').removeAttr('value');
        $('#FechaPlanEd').removeAttr('value');
        $('#PK_Lista_Ed_Plan').removeAttr('value');

        var Id_Elm = $(this).attr('id');
        $.ajax({
            type: "POST",
            url: "/Auditoria/MostrarEdicionPlan",
            data: '{values: "' + Id_Elm + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {
                if (response.probar == false) {
                    $('#EdicionListaDivPlan').hide();
                }
                else {
                    $('#EdicionListaDivPlan').show();

                    var pk_s = response.Model.Pk_Id_Actividad;
                    var Actividad_s = response.Model.Actividad;
                    var Responsable_s = response.Model.Responsable;

                    JSON.stringify(Actividad_s);
                    JSON.stringify(Responsable_s);
                    JSON.stringify(pk_s);

                    $("#ActividadPlanEd").val(Actividad_s);
                    $("#ResponsablePlanEd").val(Responsable_s);
                    $("#FechaPlanEd").val(response.Fecha_s);
                    $("#PK_Lista_Ed_Plan").val(pk_s);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#msj_novedad_Plan").text('No se ha podido editar este elemento de la lista o no existe');
                $("#div_novedad_Plan").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
            }
        });
    });
});
//Editar Plan Accion
$(function () {
    $("#EditarListaPlan").bind("click", function () {
        $(".val-message").css("display", "none");
        var onEventLaunchGuardar1 = new postGuardar1();
        onEventLaunchGuardar1.launchGuardar1();
    });
});
function postGuardar1() {
    this.launchGuardar1 = function () {
        //Traer datos al modelo JSON
        var stringArray = new Array();
        stringArray[0] = $("#ActividadPlanEd").val();
        stringArray[1] = $("#ResponsablePlanEd").val();
        stringArray[2] = $("#FechaPlanEd").val();
        stringArray[3] = $("#PK_Lista_Ed_Plan").val();
        stringArray[4] = $("#EdicionAuditoria").val();

        // Construir objeto JSON
        var EDActividadAuditoria = {
            Fk_Id_Auditoria: stringArray[4],
            Actividad: stringArray[0],
            Responsable: stringArray[1],
            FechaFinalizacion: stringArray[2],
            Pk_Id_Actividad: stringArray[3]
        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/EditarPlan',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(EDActividadAuditoria),
            success: function (data) {
                OcultarPopupposition();
                $("#val-actividadEd").css("display", "none");
                $("#val-actividadEd").text('');
                $("#val-responsableplanEd").css("display", "none");
                $("#val-responsableplanEd").text('');
                $("#val-fechaplanEd").css("display", "none");
                $("#val-fechaplanEd").text('');

                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-actividadEd").css("display", "block");
                        $("#val-actividadEd").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-responsableplanEd").css("display", "block");
                        $("#val-responsableplanEd").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-fechaplanEd").css("display", "block");
                        $("#val-fechaplanEd").text(data.ValidacionStr[2]);
                    }
                    swal("Advertencia", data.Estado);
                }
                else {
                    swal({
                        title: "Se editó el plan de acción correctamente",
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
//Cancelar cuadro edicion Plan Accion
$(function () {
    $('#EditarCancelarPlan').click(function () {
        $('#EdicionListaDivPlan').hide();
        $('#ActividadPlanEd').removeAttr('value');
        $('#ResponsablePlanEd').removeAttr('value');
        $('#FechaPlanEd').removeAttr('value');
        $('#PK_Lista_Ed_Plan').removeAttr('value');
    });
});
//Mostrar cuadro edicion Compromiso
$(function () {
    $('.btnEditarCompromiso').click(function () {

        $('#EdicionListaDivCompromiso').hide();

        $('#PK_Lista_Ed_Compromiso').removeAttr('value');
        $('#CompromisoEd').removeAttr('value');
        $('#ResponsableEd').removeAttr('value');
        $('#FechaCierreEd').removeAttr('value');

        var Id_Elm = $(this).attr('id');
        $.ajax({
            type: "POST",
            url: "/Auditoria/MostrarEdicionCompromiso",
            data: '{values: "' + Id_Elm + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {
                if (response.probar == false) {
                    $('#EdicionListaDivCompromiso').hide();
                }
                else {
                    $('#EdicionListaDivCompromiso').show();
                    var pk_s = response.Model.Pk_Id_Lista_Verificacion;
                    var Requisito_s = response.Model.FechaCierre;
                    var Hallazgo_s = response.Model.Responsable;
                    var Tipo_s = response.Model.Compromiso;

                    JSON.stringify(Requisito_s);
                    JSON.stringify(Hallazgo_s);
                    JSON.stringify(Tipo_s);
                    JSON.stringify(pk_s);


                    $("#FechaCierreEd").val(response.Fecha_s);
                    $("#ResponsableEd").val(Hallazgo_s);
                    $("#CompromisoEd").val(Tipo_s);
                    $("#PK_Lista_Ed_Compromiso").val(pk_s);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#msj_novedad_compromiso").text('No se ha podido editar este elemento de la lista o no existe');
                $("#div_novedad_compromiso").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
            }
        });
    });
});
//Editar Compromiso
$(function () {
    $("#EditarListaCompromiso").bind("click", function () {
        $(".val-message").css("display", "none");
        var onEventLaunchGuardar2 = new postGuardar2();
        onEventLaunchGuardar2.launchGuardar2();
    });
});
function postGuardar2() {
    this.launchGuardar2 = function () {
        //Traer datos al modelo JSON
        var stringArray = new Array();
        stringArray[0] = $("#FechaCierreEd").val();
        stringArray[1] = $("#ResponsableEd").val();
        stringArray[2] = $("#CompromisoEd").val();
        stringArray[3] = $("#PK_Lista_Ed_Compromiso").val();
        stringArray[4] = $("#EdicionAuditoria").val();

        stringArray[5] = "N/A";
        stringArray[6] = "N/A";

        // Construir objeto JSON
        var EDAuditoriaVerificacion = {
            Pk_Id_Lista_Verificacion: stringArray[3],
            Compromiso: stringArray[2],
            Responsable: stringArray[1],
            FechaCierre: stringArray[0],
            Pregunta: stringArray[5],
            Requisito: stringArray[6],
            Fk_Id_Auditoria: stringArray[4]
        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/EditarCompromiso',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(EDAuditoriaVerificacion),
            success: function (data) {
                OcultarPopupposition();
                $("#val-compromiso").css("display", "none");
                $("#val-compromiso").text('');
                $("#val-responsableComp").css("display", "none");
                $("#val-responsableComp").text('');
                $("#val-fechacierre").css("display", "none");
                $("#val-fechacierre").text('');

                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-compromiso").css("display", "block");
                        $("#val-compromiso").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-responsableComp").css("display", "block");
                        $("#val-responsableComp").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-fechacierre").css("display", "block");
                        $("#val-fechacierre").text(data.ValidacionStr[2]);
                    }
                    swal("Advertencia", data.Estado);
                }
                else {
                    swal({
                        title: "Se editó el compromiso exitosamente",
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
//Cancelar cuadro edicion Compromiso
$(function () {
    $('#EditarCancelarCompromiso').click(function () {
        $('#EdicionListaDivCompromiso').hide();
        $('#FechaCierreEd').removeAttr('value');
        $('#ResponsableEd').removeAttr('value');
        $('#CompromisoEd').removeAttr('value');
        $('#PK_Lista_Ed_Compromiso').removeAttr('value');
    });
});
//Adjuntar firma Presidente
$(function () {
    $('#AgregarFirmaAud').click(function () {
        if (window.FormData !== undefined) {
            var fileUpload = $("#UploadPhotoAud").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            PopupPosition();
            $.ajax({
                url: '/Auditoria/UploadImgPres',
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (result) {
                    $("#msj_novedad_conclu").text('');
                    $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success");
                    if (result.probar == false) {
                        $("#msj_novedad_conclu").text(result.resultado);
                        $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        $("#ImagenFirmaAudId").attr("src", result.ImgScr);
                    }
                    OcultarPopupposition();
                },
                error: function (err) {
                    $("#msj_novedad_conclu").text("Error al cargar el archivo");
                    $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    OcultarPopupposition();
                }
            });
        } else {

        }
    });
});
//Adjuntar firma responsable
$(function () {
    $('#AgregarFirmaRes').click(function () {

        if (window.FormData !== undefined) {
            var fileUpload = $("#UploadPhotoRes").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            PopupPosition();
            $.ajax({
                url: '/Auditoria/UploadImgRes',
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (result) {
                    $("#msj_novedad_conclu").text('');
                    $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success");
                    if (result.probar == false) {
                        $("#msj_novedad_conclu").text(result.resultado);
                        $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        $("#ImagenFirmaResId").attr("src", result.ImgScr);
                    }
                    OcultarPopupposition();
                },
                error: function (err) {
                    $("#msj_novedad_conclu").text("Error al cargar el archivo");
                    $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    OcultarPopupposition();
                }
            });
        } else {
            //alert("Archivo no soportado");
        }
    });
});
//Quitar Firma Presidente
$(function () {
    $('#QuitarFirmaAud').click(function () {
        $("#msj_novedad_conclu").text('');
        $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success");
        $("#ImagenFirmaAudId").attr("src", "");
    });
});
//Quitar Firma Responsable
$(function () {
    $('#QuitarFirmaRes').click(function () {
        $("#msj_novedad_conclu").text('');
        $("#div_novedad_conclu").removeClass("alert alert-info alert-warning alert-danger alert-success");
        $("#ImagenFirmaResId").attr("src", "");
    });
});
//Actualizar conclusiones
$(function () {
    $("#Actualizar-conclusiones").bind("click", function () {
        $(".val-message").css("display", "none");
        var onEventLaunchGuardar3 = new postGuardar3();
        onEventLaunchGuardar3.launchGuardar3();
    });
});
function postGuardar3() {
    this.launchGuardar3 = function () {
        //Traer datos al modelo JSON
        var imgRes = document.getElementById('ImagenFirmaResId').getAttribute('src');
        var imgAud = document.getElementById('ImagenFirmaAudId').getAttribute('src');
        var stringArray = new Array();
        stringArray[0] = $("#EdicionAuditoria").val();
        stringArray[1] = $("#EdicionPrograma").val();
        stringArray[2] = imgRes
        stringArray[3] = imgAud;
        stringArray[4] = $("#Conclusiones").val();
        // Construir objeto JSON
        var AuditoriaInforme = {
            Pk_Id_Auditoria: stringArray[0],
            FechaRealizacion: stringArray[0],
            Conclusiones: stringArray[4],
            FirmaScrImageRes: stringArray[2],
            FirmaScrImageAuditor: stringArray[3]
        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/ActualizarConclusiones',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(AuditoriaInforme),
            success: function (data) {
                OcultarPopupposition();
                $("#val-conclu").css("display", "none");
                $("#val-conclu").text('');

                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-conclu").css("display", "block");
                        $("#val-conclu").text(data.ValidacionStr[0]);
                    }
                    swal("Advertencia", data.Estado);
                    $("#ImagenFirmaAudId").attr("src", result.ImgScr) = data.Model.FirmaScrImageAuditor;
                    $("#ImagenFirmaResId").attr("src", result.ImgScr) = data.Model.FirmaScrImageRes;
                }
                else {
                    swal({
                        title: "Conclusiones y firmas actualizadas correctamente",
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

jQuery(function ($) {
    var items = $(".paginver");
    var numItems = items.length;
    var perPage = 5;
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

jQuery(function ($) {
    var items = $(".pagincom");
    var numItems = items.length;
    var perPage = 5;
    items.slice(perPage).hide();
    $(".pagination1").pagination({
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
        var hash = window.location.hash || "#page1-1";
        hash = hash.match(/^#page1-(\d+)$/);
        if (hash) {
            $(".pagination-page").pagination("selectPage", parseInt(hash[1]));
        }
    };
    $(window).bind("popstate", checkFragment);
    checkFragment();
});

jQuery(function ($) {
    var items = $(".paginact");
    var numItems = items.length;
    var perPage = 5;
    items.slice(perPage).hide();
    $(".pagination2").pagination({
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
        var hash = window.location.hash || "#page2-1";
        hash = hash.match(/^#page2-(\d+)$/);
        if (hash) {
            $(".pagination-page").pagination("selectPage", parseInt(hash[1]));
        }
    };
    $(window).bind("popstate", checkFragment);
    checkFragment();
});