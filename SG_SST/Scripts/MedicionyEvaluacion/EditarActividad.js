//Generar datetime
    $(document).ready(function () {
        ConstruirDatePickerPorElemento('FechaFinalizacion');
    });
//Adjuntar firma*@
    $(function () {
        $('#btn-add-act').click(function () {
            if (window.FormData !== undefined) {
                var fileUpload = $("#input-file-act").get(0);
                var files = fileUpload.files;
                var fileData = new FormData();

                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                PopupPosition();
                $.ajax({
                    url: '/Acciones/FirmaAct',
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
                            $("#img-act").attr("src", result.ImgScr);
                        }
                    },
                    error: function (err) {
                        OcultarPopupposition();
                        $("#msj_novedad").text("Error al cargar el archivo");
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                    }
                });
            } else {
                
            }

        });
    });

//Quitar Firma
    $(function () {
    $('#btn-delete-act').click(function () {

        $.ajax({
            url: '/Acciones/QuitarImgAct',
            type: "POST",
            success: function (result) {

                if (result.probar == false) {
                    $("#msj_novedad").text(result.resultado);
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                }
                else {
                    $("#msj_novedad").text(result.resultado);
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                    $("#img-act").attr("src", "");
                }
            },
            error: function (err) {
                $("#msj_novedad").text("Ha ocurrido un error, no se ha podido eliminar la imagen o ya no existe");
                $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

            }
        });


    });
    });


    //buscar nombre y cargo del responsable(NUEVO)
    $(document).ready(function () {
        $("#BuscarCedula").on('keyup', function () {
            $("#msj_novedad").text('');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");

            $("#Responsable").val('');



            if ($(this).val().length > 5) {
                PopupPosition();
                $.ajax({
                    type: "POST",
                    url: "/Acciones/BuscarPersonaDocumentoCargo1",
                    data: '{documento: "' + $("#BuscarCedula").val() + '" }',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    dataType: "json",
                    success: function (response) {
                        OcultarPopupposition();
                        var PersonaArray = new Array();
                        var PersonaArray1 = new Array();
                        var array = [];
                        if (response.RelacionLaboral != null) {
                            PersonaArray = response.RelacionLaboral;
                            for (var i in PersonaArray) {
                                if (PersonaArray.hasOwnProperty(i) && !isNaN(+i)) {
                                    array[+i] = PersonaArray[i];
                                }
                            }
                        }
                        $.each(array, function (array1, group) {
                            var NombreS = group.Nombre1;
                            var Ocupacion = group.Ocupacion;
                            var TipoDocumento = group.TipoDocumento;
                            $("#Responsable").val(NombreS + " - " + Ocupacion);
                            $("#msj_novedad").text("Persona encontrada");
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                        });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        OcultarPopupposition();
                        $("#Responsable").val('');
                    }
                });


            }


        });
    });
