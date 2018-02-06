 //Fecha Script
$(document).ready(function () {
    ConstruirDatePickerPorElemento('Fecha_Programacion');
});
//Adjuntar firma Presidente
$(function () {
    $('#AgregarFirmaPres').click(function () {

        if (window.FormData !== undefined) {
            var fileUpload = $("#UploadPhotoPres").get(0);
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
                    $("#msj_novedad").text('');
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");
                    if (result.probar == false) {
                        $("#msj_novedad").text(result.resultado);
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        $("#ImagenFirmaPresId").attr("src", result.ImgScr);
                    }
                    OcultarPopupposition();
                },
                error: function (err) {
                    $("#msj_novedad").text("Error al cargar el archivo");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    OcultarPopupposition();
                }
            });
        } else {
            //alert("Archivo no soportado");
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
                    $("#msj_novedad").text('');
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");
                    if (result.probar == false) {
                        $("#msj_novedad").text(result.resultado);
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        $("#ImagenFirmaResId").attr("src", result.ImgScr);
                    }
                    OcultarPopupposition();
                }
                ,
                error: function (err) {
                    $("#msj_novedad").text("Error al cargar el archivo");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    OcultarPopupposition();
                }
            });
            
        }
        else {

        }
    });
});
//Quitar Firma Presidente
$(function () {
    $('#QuitarFirmaPres').click(function () {
        $("#msj_novedad").text('');
        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");
        $("#ImagenFirmaPresId").attr("src", "");
    });
});
//Quitar Firma Responsable
$(function () {
    $('#QuitarFirmaRes').click(function () {
        $("#msj_novedad").text('');
        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");
        $("#ImagenFirmaResId").attr("src", "");
    });
});
//Guardar Programa
$(function () {
    $("#GuardarPrograma").bind("click", function () {
        var onEventLaunchGuardar = new postGuardar();
        onEventLaunchGuardar.launchGuardar();
    });
});
function postGuardar() {
    this.launchGuardar = function () {

        //Traer datos al modelo JSON
        var imgRes = document.getElementById('ImagenFirmaResId').getAttribute('src');
        var imgsPres = document.getElementById('ImagenFirmaPresId').getAttribute('src');

        var stringArray = new Array();
        stringArray[0] = $("#ListaPeriodo option:selected").val();
        stringArray[1] = $("#Pk_Id_Sede1").val();
        stringArray[2] = $("#A_o").val();
        stringArray[3] = $("#Fecha_Programacion").val();
        stringArray[4] = $("#ListaPeriodo option:selected").val();
        stringArray[5] = $("#Titulo").val();
        stringArray[6] = $("#Alcance").val();
        stringArray[7] = $("#Metodologia").val();
        stringArray[8] = $("#Competencia").val();
        stringArray[9] = $("#Recursos").val();
        stringArray[10] = $("#Objetivo").val();
        stringArray[11] = imgRes;
        stringArray[12] = imgsPres;
        stringArray[13] = $("#Nombre_Responsable").val();
        stringArray[14] = $("#Numero_Id_Responsable").val();
        stringArray[15] = $("#Nombre_Copasst").val();
        stringArray[16] = $("#Numero_Id_Copasst").val();

        // Construir objeto JSON
        var AuditoriaPrograma = {
            Titulo: stringArray[5],
            Objetivo: stringArray[10],
            Alcance: stringArray[6],
            Metodologia: stringArray[7],
            Competencia: stringArray[8],
            Recursos: stringArray[9],
            Fecha_Programacion: stringArray[3],
            Año: stringArray[2],
            Periodicidad: stringArray[4],
            Fk_Id_Sede: stringArray[1],
            FirmaScrImageRes: stringArray[11],
            FirmaScrImagePres: stringArray[12],
            Nombre_Responsable: stringArray[13],
            Numero_Id_Responsable: stringArray[14],
            Nombre_Copasst: stringArray[15],
            Numero_Id_Copasst: stringArray[16]
        };
        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/CrearPrograma',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(AuditoriaPrograma),
            success: function (data) {
                OcultarPopupposition();
                $("#val-sede").css("display", "none");
                $("#val-sede").text('');
                $("#val-año").css("display", "none");
                $("#val-año").text('');
                $("#val-fecha").css("display", "none");
                $("#val-fecha").text('');
                $("#val-periodo").css("display", "none");
                $("#val-periodo").text('');
                $("#val-titulo").css("display", "none");
                $("#val-titulo").text('');
                $("#val-objetivo").css("display", "none");
                $("#val-objetivo").text('');
                $("#val-alcance").css("display", "none");
                $("#val-alcance").text('');
                $("#val-metodologia").css("display", "none");
                $("#val-metodologia").text('');
                $("#val-competencia").css("display", "none");
                $("#val-competencia").text('');
                $("#val-recursos").css("display", "none");
                $("#val-recursos").text('');
                if (data.Probar == false) {
                    
                    if (data.Validacion[0] == true) {
                        $("#val-sede").css("display", "block");
                        $("#val-sede").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-año").css("display", "block");
                        $("#val-año").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-fecha").css("display", "block");
                        $("#val-fecha").text(data.ValidacionStr[2]);
                    }
                    if (data.Validacion[3] == true) {
                        $("#val-periodo").css("display", "block");
                        $("#val-periodo").text(data.ValidacionStr[3]);
                    }
                    if (data.Validacion[4] == true) {
                        $("#val-titulo").css("display", "block");
                        $("#val-titulo").text(data.ValidacionStr[4]);
                    }
                    if (data.Validacion[5] == true) {
                        $("#val-objetivo").css("display", "block");
                        $("#val-objetivo").text(data.ValidacionStr[5]);
                    }
                    if (data.Validacion[6] == true) {
                        $("#val-alcance").css("display", "block");
                        $("#val-alcance").text(data.ValidacionStr[6]);
                    }
                    if (data.Validacion[7] == true) {
                        $("#val-metodologia").css("display", "block");
                        $("#val-metodologia").text(data.ValidacionStr[7]);
                    }
                    if (data.Validacion[8] == true) {
                        $("#val-competencia").css("display", "block");
                        $("#val-competencia").text(data.ValidacionStr[8]);
                    }
                    if (data.Validacion[9] == true) {
                        $("#val-recursos").css("display", "block");
                        $("#val-recursos").text(data.ValidacionStr[9]);
                    }
                    swal("Advertencia", data.Estado);
                    $("#ImagenFirmaPresId").attr("src", result.ImgScr) = data.Model.FirmaScrImagePres;
                    $("#ImagenFirmaResId").attr("src", result.ImgScr) = data.Model.FirmaScrImageRes;
                }
                else {
                    swal({
                        title: "Programa de auditorias agregado exitosamente",
                        text: "",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: false
                    },
                function () {
                    window.location.href = "/Auditoria/Programa"
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
//buscar nombre responsable
$(function () {
    $("#buscar_id_res").click(function () {
		PopupPosition();
        $.ajax({
            type: "POST",
            url: "/Auditoria/BuscarPersonaDocumento",
            data: '{documento: "' + $("#Numero_Id_Responsable").val() + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {
                $("#Nombre_Responsable").val(response.resultado[0]);
                if (response.probar == false) {
                    $("#msj_novedad").text("Persona no encontrada");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                }
                else {
                    $("#msj_novedad").text("Persona encontrada");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                }
				OcultarPopupposition();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var value = "";
                $("#Nombre_Responsable").val(value);
				OcultarPopupposition();
            }
        });
    });
});
//buscar nombre Presidente
$(function () {
    $("#buscar_id_pres").click(function () {
		PopupPosition();
        $.ajax({
            type: "POST",
            url: "/Auditoria/BuscarPersonaDocumento",
            data: '{documento: "' + $("#Numero_Id_Copasst").val() + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {
                $("#Nombre_Copasst").val(response.resultado[0]);
                if (response.probar == false) {
                    $("#msj_novedad").text("Persona no encontrada");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                }
                else {
                    $("#msj_novedad").text("Persona encontrada");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                }
				OcultarPopupposition();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var value = "";
                $("#Nombre_Copasst").val(value);
				OcultarPopupposition();
            }
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
