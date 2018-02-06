//Generar datetime
    $(document).ready(function () {
        ConstruirDatePickerPorElemento('Fecha_Seg');
    });
//Adjuntar firma
$(function () {
        $('#btn-add-seg').click(function () {

            if (window.FormData !== undefined) {
                var fileUpload = $("#input-file-seg").get(0);
                var files = fileUpload.files;
                var fileData = new FormData();

                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                PopupPosition();
                $.ajax({
                    url: '/Acciones/FirmaSeg',
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        OcultarPopupposition();
                        if (result.probar == false) {
                            $("#msj_novedad").text(result.resultado);
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                        }
                        else {
                            $("#msj_novedad").text(result.resultado);
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                            $("#img-seg").attr("src", result.ImgScr);
                        }
                    },
                    error: function (err) {
                        OcultarPopupposition();
                        $("#msj_novedad").text("Error al cargar el archivo, asegurese que el tamaño del archivo sea inferior a 4 MB");
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                    }
                });
            } else {
               
            }
        });
    });

//Quitar Firma
 $(function () {
        $('#btn-delete-seg').click(function () {

            $.ajax({
                url: '/Acciones/QuitarImgSeg',
                type: "POST",
                success: function (result) {

                    if (result.probar == false) {
                        $("#msj_novedad").text(result.resultado);
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        $("#msj_novedad").text(result.resultado);
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                        $("#img-seg").attr("src", "");
                    }
                },
                error: function (err) {
                    $("#msj_novedad").text("Ha ocurrido un error, no se ha podido eliminar la imagen o ya no existe");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                }
            });


        });
    });