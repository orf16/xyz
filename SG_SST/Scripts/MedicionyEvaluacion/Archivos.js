
$(function () {
        $('#CargarArchivo').click(function () {
            if (window.FormData !== undefined) {
                var fileUpload = $("#txtUploadFile").get(0);
                var files = fileUpload.files;
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                PopupPosition();
                $.ajax({
                    url: '/Acciones/UploadFiles',
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
                            swal({
                                title: "Archivo Adjuntado",
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
                    error: function (err) {
                        $("#msj_novedad").text("Error al cargar el archivo, asegurese que el tamaño del archivo sea inferior a 4 MB");
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                        OcultarPopupposition();
                    }
                });
            } else {
            }
        });
    });

$(function () {
        $('.btnEliminarArchivo').click(function () {
            PopupPosition();
            $.ajax({
                type: "POST",
                url: "/Acciones/EliminarArchivo",
                data: '{values: "' + $(this).attr('id') + '" }',
                contentType: "application/json; charset=utf-8",
                cache: false,
                dataType: "json",
                success: function (response) {
                OcultarPopupposition();
                    if (response.probar == false) {
                        if (response.probarEncuentraArchivo == false) {
                            swal({
                                title: "Advertencia",
                                text: "Archivo no encontrado, por favor intente de nuevo",
                                type: "error",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "OK",
                                closeOnConfirm: false
                            },
                                            function () {
                                                location.reload(true);
                                            });
                        }
                        else {
                            $("#msj_novedad").text(response.resultado);
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                        }
                    }
                    else {
                        swal({
                            title: "Operación Exitosa",
                            text: "Archivo eliminado con exito",
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
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#msj_novedad").text(textStatus);
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    OcultarPopupposition();
                }
            });
        });
    });
