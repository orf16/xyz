
        //Cargar Archivo
        $(function () {
            $('#CargarArchivo').click(function () {
                var userInfo = $("#userInfoJson").val();
                if (window.FormData !== undefined) {

                    var fileUpload = $("#txtUploadFile").get(0);
                    var files = fileUpload.files;
                    var fileData = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                        fileData.append("TempdataID", userInfo);
                    }
                    PopupPosition();
                    $.ajax({
                        url: '/Acciones/UploadFilesEd',
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fileData,
                        success: function (result) {

                            if (result.probar == false) {
                            OcultarPopupposition();
                                $("#msj_novedad").text(result.resultado);
                                $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                            }
                            else {
                            OcultarPopupposition();
                                swal({
                                    title: "Operación Exitosa",
                                    text: "Archivo Adjuntado",
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

        //Eliminar Archivo
        $(function () {
            $('.btnEliminarArchivo').click(function () {
                var IdButton = $(this).attr('id');
                var userInfo = $("#userInfoJson").val();
                var stringArray = new Array();
                stringArray[0] = IdButton;
                stringArray[1] = userInfo;
                var postData = { values: stringArray };
                PopupPosition();
                $.ajax({
                    type: "POST",
                    url: "/Acciones/EliminarArchivoEd",
                    data: postData,
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
                                title: "operación Exitosa",
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
