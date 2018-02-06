//Guardar EPPS
$(function () {
    $("#GuardarEPP").bind("click", function () {
        $("#msj_glyphicon").removeClass("glyphicon glyphicon-ok glyphicon glyphicon-exclamation-sign");
        $("#msj_novedad_validar").text('');
        $("#div_novedad_validar").removeClass("alert alert-info alert-warning alert-danger alert-success");
        var onEventLaunchGuardar = new postGuardar();
        onEventLaunchGuardar.launchGuardar();
    });
});
function postGuardar() {
    this.launchGuardar = function () {
        var EPP = {
            NombreEPP: "control",
            ParteCuerpo: "control",
            EspecificacionTecnica: "control",
            Uso: "control",
            Mantenimiento: "control",
            VidaUtil: "control",
            Reposicion: "control",
            DisposicionFinal: "control"
        };
        var ListaEPPs = new Array();
        var tableBody = $('#Grid3 > tbody');
        $('#Grid3 > tbody').find('tr.paginacc').each(function () {
            var row = $(this);
            var stringArray = new Array();

            var N_imagen = "";
            var N_imagen_D = "";
            var N_archivo = "";
            var N_archivo_D = "";
            var cont = 0;
            var ListaEPPCargos = new Array();
            row.find('td.td_principal').each(function () {
                var row1 = $(this);
                row1.css("background-color", "#ffffff");
                row1.attr("title", "");

                if (cont == 0) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 1) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 2) {
                    row1.find('select').each(function () {

                        var selectlist = $(this);
                        var conceptName = selectlist.find(":selected").val();
                        stringArray[cont] = conceptName;
                    });
                }
                if (cont == 3) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 4) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 5) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 6) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 7) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 8) {
                    stringArray[cont] = row1[0].innerText;
                }
                if (cont == 9) {
                    row1.find('table > tbody tr').each(function () {
                        var table_cargos_tr = $(this);
                        var cargoid = table_cargos_tr.attr('id');
                        var cont1 = 0;
                        var stringArrayCargos = new Array();
                        table_cargos_tr.find('td').each(function () {

                            var cargos_td = $(this);
                            if (cont1 == 0) {
                                stringArrayCargos[0] = cargoid;
                            }
                            if (cont1 == 1) {
                                stringArrayCargos[1] = cargos_td[0].innerText;
                            }
                            cont1++;
                        });
                        var CargoEPP = {
                            Fk_Id_Cargo: stringArrayCargos[0],
                            Cantidad: stringArrayCargos[1]
                        };
                        ListaEPPCargos.push(CargoEPP);
                    });
                }
                if (cont == 10) {
                    var cont2 = 0;
                    row1.find('label').each(function () {

                        var label = $(this);
                        if (cont2 == 0) {
                            N_imagen = label.val();
                        }
                        if (cont2 == 1) {
                            N_imagen_D = label.val();
                        }
                        cont2++;
                    });
                }
                if (cont == 11) {
                    var cont3 = 0;
                    row1.find('input').each(function () {

                        var label = $(this);
                        if (cont3 == 0) {
                            N_archivo = label.val();
                        }
                        if (cont3 == 1) {
                            N_archivo_D = label.val();
                        }
                        cont3++;
                    });
                }
                cont++;
            });
            var AdmoEPP = {
                NombreEPP: stringArray[0],
                ParteCuerpo: stringArray[1],
                Fk_Id_Clasificacion_De_Peligro: stringArray[2],
                EspecificacionTecnica: stringArray[3],
                Uso: stringArray[4],
                Mantenimiento: stringArray[5],
                VidaUtil: stringArray[6],
                Reposicion: stringArray[7],
                DisposicionFinal: stringArray[8],
                ArchivoImagen: N_imagen,
                ArchivoImagen_download: N_imagen_D,
                NombreArchivo: N_archivo,
                NombreArchivo_download: N_archivo_D,
                Cargos: ListaEPPCargos
            };
            ListaEPPs.push(AdmoEPP);
        });

        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/AdmoEPP/GuardarMasivoNuevoEPP',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ Control: EPP, ListaEPP: ListaEPPs }),
            success: function (data) {
                OcultarPopupposition();
                if (data.Probar == false) {
                    if (data.Estado == "El usuario no ha iniciado sesión en el sistema") {
                        location.reload(true);
                    }
                    swal("Estimado Usuario", data.Estado, "error");
                    $("#msj_novedad_validar").text(data.Estado);
                    $("#div_novedad_validar").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    var contValidacion = 0;
                    var contValidacion2 = 0;
                    $('#Grid3 > tbody').find('tr.paginacc').each(function () {
                        var row = $(this);
                        var contColumnas = 0;
                        var Validacion = new Array();
                        var Validacionstr = new Array();
                        var contValidacion1 = 0;
                        contValidacion = contValidacion2;
                        for (var i = contValidacion; i < contValidacion + 8; i++) {
                            Validacion[contValidacion1] = data.Validacion[i];
                            Validacionstr[contValidacion1] = data.ValidacionStr[i];
                            contValidacion1++;
                            contValidacion2++;
                        }
                        row.find('td').each(function () {
                            var row1 = $(this);
                            if (Validacion[contColumnas] == true) {
                                row1.css("background-color", "#e2dfd9");
                                row1.attr("title", Validacionstr[contColumnas]);
                            }
                            contColumnas++;
                        });
                    });
                }
                else {
                    $(".Epptable").remove();
                    swal({
                        title: "Estimado Usuario",
                        text: "El cargue de elementos de protección personal se ha guardado exitosamente",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: false
                    },
                function () {
                    window.location.href = "/AdmoEPP/MatrizEPP"
                });
                }
            },
            error: function (data) {
                OcultarPopupposition();
            }
        });
    }
}
//Validar al Cargar
$(document).ready(function () {
    $("#msj_glyphicon").removeClass("glyphicon glyphicon-ok glyphicon glyphicon-exclamation-sign");
    $("#msj_novedad_validar").text('');
    $("#div_novedad_validar").removeClass("alert alert-info alert-warning alert-danger alert-success");
    var onEventLaunchValidar = new postValidar();
    onEventLaunchValidar.launchValidar();
});
//Validar al Cargar
function postValidar() {
    this.launchValidar = function () {

        var EPP = {
            NombreEPP: "control",
            ParteCuerpo: "control",
            EspecificacionTecnica: "control",
            Uso: "control",
            Mantenimiento: "control",
            VidaUtil: "control",
            Reposicion: "control",
            DisposicionFinal: "control"
        };
        var ListaEPPs = new Array();
        var tableBody = $('#Grid3 > tbody');
        $('#Grid3 > tbody').find('tr.paginacc').each(function () {
            var row = $(this);
            var stringArray = new Array();
            var cont = 0;
            row.find('td').each(function () {
                var row1 = $(this);
                row1.css("background-color", "#ffffff");
                row1.attr("title", "");
                if (cont == 2) {
                    row1.find('select').each(function () {
                        var selectlist = $(this);
                        var conceptName = selectlist.find(":selected").val();
                        stringArray[cont] = conceptName;
                    });
                }
                else {
                    stringArray[cont] = row1[0].innerText;
                }
                cont++;
            });
            var AdmoEPP = {
                NombreEPP: stringArray[0],
                ParteCuerpo: stringArray[1],
                Fk_Id_Clasificacion_De_Peligro: stringArray[2],
                EspecificacionTecnica: stringArray[3],
                Uso: stringArray[4],
                Mantenimiento: stringArray[5],
                VidaUtil: stringArray[6],
                Reposicion: stringArray[7],
                DisposicionFinal: stringArray[8]
            };
            ListaEPPs.push(AdmoEPP);
        });

        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/AdmoEPP/ValidarModeloEPP',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ Control: EPP, ListaEPP: ListaEPPs }),
            success: function (data) {
                OcultarPopupposition();

                if (data.Estado_get==null) {
                    if (data.Probar == false) {
                        if (data.Estado == "El usuario no ha iniciado sesión en el sistema") {
                            location.reload(true);
                        }
                        if (data.Estado == "No hay elementos de protección a cargar") {
                            swal("Estimado Usuario", data.Estado, "warning");
                        }
                        else {
                            swal("Estimado Usuario", data.Estado, "warning");
                            $("#msj_glyphicon").removeClass("glyphicon glyphicon-ok glyphicon glyphicon-exclamation-sign").addClass("glyphicon glyphicon-exclamation-sign");
                            $("#msj_novedad_validar").text(' ' + data.Estado);
                            $("#div_novedad_validar").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-danger");
                            var contValidacion = 0;
                            var contValidacion2 = 0;
                            $('#Grid3 > tbody').find('tr.paginacc').each(function () {
                                var row = $(this);
                                var contColumnas = 0;
                                var Validacion = new Array();
                                var Validacionstr = new Array();
                                var contValidacion1 = 0;
                                contValidacion = contValidacion2;
                                for (var i = contValidacion; i < contValidacion + 8; i++) {
                                    Validacion[contValidacion1] = data.Validacion[i];
                                    Validacionstr[contValidacion1] = data.ValidacionStr[i];
                                    contValidacion1++;
                                    contValidacion2++;
                                }
                                row.find('td').each(function () {
                                    var row1 = $(this);
                                    if (Validacion[contColumnas] == true) {
                                        row1.css("background-color", "#e2dfd9");

                                        row1.attr("title", Validacionstr[contColumnas]);

                                    }
                                    contColumnas++;
                                });
                            });
                        }

                    }
                    else {
                        swal("Estimado Usuario", data.Estado, "success");
                        $("#msj_glyphicon").removeClass("glyphicon glyphicon-ok glyphicon glyphicon-exclamation-sign").addClass("glyphicon glyphicon-ok");
                        $("#msj_novedad_validar").text(' La validación de la hoja de cálculo se ejecutó correctamente. Ahora puede editar las columnas riesgo controlado, cargos asociados, imagen y ficha técnica, recuerde que es OBLIGATORIO diligenciar la columna RIESGO CONTROLADO. Cuando termine de consignar la información de los EPP haga click en guardar para terminar el proceso');
                        $("#div_novedad_validar").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                    }
                }


                
            },
            error: function (data) {
                OcultarPopupposition();
            }
        });
    }
}
//Buscar clasificacion peligro(riesgo asociado) por TipoPeligro
function jsFunction_121() {
    var myselect = document.getElementById("Pk_Id_Tipo_Peligro");
    var idselect = myselect.options[myselect.selectedIndex].value;
    PopupPosition();
    $.ajax({
        type: "POST",
        url: "/AdmoEPP/ConsultarClasPorTipo",
        data: '{IdTipoPeligro: "' + idselect + '" }',
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: "json",
        success: function (data) {
            OcultarPopupposition();
            if (data.Probar == false) {
                var options = $("#Pk_Id_Clasif_Peligro");
                options.empty();
                options.append($("<option />").val(null).text("-- Seleccione una Clasificación --"));
            }
            else {
                var options = $("#Pk_Id_Clasif_Peligro");
                options.empty();
                options.append($("<option />").val(null).text("-- Seleccione una Clasificación --"));
                var cont = 0;
                $.each(data.ListaValor, function () {
                    options.append($("<option />").val(data.ListaValor[cont]).text(data.ListaTexto[cont]));
                    cont = cont + 1;
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            OcultarPopupposition();
        }
    });
}
//Mostrar cargos en Modal
$(function () {
    $(".MostrarCargos").click(function () {
        $('#tabla_modal').html('');
        $("#Nombre_EPP").empty();

        $("#val-error").css("display", "none");
        $("#val-error").text('');

        var modal = document.getElementById('myModal');
        var row = $(this).attr('name');
        var NombreEPP = $(this).attr('nameEPP');
        $("#Nombre_EPP").append(NombreEPP);
        $('#IdTabla').val(row.toString());
        var IdTabla = 'TablaCargoEPP ' + row;
        var TablaElegida = document.getElementById(IdTabla);
        $('#tabla_modal').html($(TablaElegida).html());
        //$(TablaElegida).contents().appendTo('#tabla_modal');
        modal.style.display = "block";
    });
});
//Funciones del Modal
$(document).ready(function () {
    var modal = document.getElementById('myModal');
    var span = document.getElementById("close_modal");
    span.onclick = function () {
        modal.style.display = "none";
        $("#val-error").css("display", "none");
        $("#val-error").text('');
    }
    document.addEventListener('keyup', function (e) {
        if (e.keyCode == 27) {
            modal.style.display = "none";
            $("#val-error").css("display", "none");
            $("#val-error").text('');
        }
    });
});
//Agregar Cargo
$(function () {
    $('#Agregar_Cargo').click(function () {
        var NombreCargo = $('#NombreCargo').val();
        var NombreCargoText = $("#NombreCargo option:selected").text();
        var NumeroTrabajadores = $('#NumeroTrabajadores').val();
        var TextoError = "";
        var IdTabla = $('#IdTabla').val();
        $("#val-error").css("display", "none");
        $("#val-error").text('');
        var probar_numero = false;
        var probar_cargo = false;

        if (NumeroTrabajadores == null) {
            probar_numero = true;
        }
        else {
            if (NumeroTrabajadores == "") {
                probar_numero = true;
            }
        }
        if (NombreCargo == null) {
            probar_cargo = true;
        }
        else {
            if (NombreCargo == "") {
                probar_cargo = true;
            }
        }
        if (probar_cargo && probar_numero == false) {
            TextoError = "No ha elegido un cargo";
            $("#val-error").css("display", "block");
            $("#val-error").text(TextoError);
        }
        if (probar_cargo == false && probar_numero) {
            TextoError = "No ha digitado el número de trabajadores";
            $("#val-error").css("display", "block");
            $("#val-error").text(TextoError);
        }
        if (probar_cargo && probar_numero) {
            TextoError = "No ha elegido un cargo y tampoco ha digitado el número de trabajadores";
            $("#val-error").css("display", "block");
            $("#val-error").text(TextoError);
        }

        if (probar_cargo == false && probar_numero == false) {
            if (IdTabla != null) {
                if (IdTabla != "") {
                    var location = "#Tabla_" + IdTabla + " > tbody";
                    var location1 = "#Tabla_" + IdTabla + "  tbody";
                    var location2 = "#TbodyCargos_" + IdTabla;

                    var tableBody = $(location);
                    var probar_repetido = false;

                    tableBody.find('tr').each(function () {
                        var row = $(this);
                        if (row.attr('id') == NombreCargo) {
                            probar_repetido = true;
                        }
                    });

                    if (probar_repetido) {
                        TextoError = "El cargo ya se encuentra en la lista, si desea editarlo retirelo de la lista y vuelva a asignarlo";
                        $("#val-error").css("display", "block");
                        $("#val-error").text(TextoError);
                    }
                    else {
                        var html = "<tr id=\"" + NombreCargo + "\"><td style=\"border-right: 2px solid lightslategray; vertical-align:middle; text-align:center\"  name=\"" + NombreCargo + "\">" + NombreCargoText + "</td>";
                        html += "<td style=\"border-right: 2px solid lightslategray; vertical-align:middle; text-align:center\">" + NumeroTrabajadores + "</td>";
                        html += "<td style=\"border-right: 2px solid lightslategray; vertical-align:middle; text-align:center\">" + "<a name=\"" + NombreCargo + "\" class=\"btn btn-search btn-md btnEliminarlista\" location=\"" + location2 + "\" tableid=\"" + IdTabla + "\" title=\"Eliminar Cargo\"><span class=\"glyphicon glyphicon-erase\"></span></a>" + "</td>";
                        $(location1).append(html);
                        $('#NumeroTrabajadores').val('');
                        $('#NombreCargo option')[0].selected = true;
                        ContadorCargos(IdTabla);
                    }
                }
            }
        }


    });
});
//Eliminar cargos de lista a guardar
$(document).on("click", ".btnEliminarlista", function () {
    var Id_Elm = $(this).attr('name');
    var Location = $(this).attr('location');
    var TableId = $(this).attr('tableid');
    //Location+=" tr";
    var tableBody = $(Location);
    var Location1 = Location + "> tr";
    var tableBody1 = $(Location1);

    swal({
        title: "Estimado Usuario",
        text: "Esta seguro(a) que desea eliminar este elemento de la lista?",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        type: "warning",
        closeOnConfirm: false
    },
    function () {
        var ElementoEliminar;
        var cont = 0;

        tableBody.find('tr').each(function () {
            var row = $(this);
            if (row.attr('id') == Id_Elm) {
                row.remove();
                cont = cont + 1;
            }
        });
        if (cont > 0) {
            $('#tabla_modal').html('');
            var IdTabla = 'TablaCargoEPP ' + TableId;
            var TablaElegida = document.getElementById(IdTabla);
            $('#tabla_modal').html($(TablaElegida).html());
            ContadorCargos(TableId);
            swal("Estimado Usuario", "El cargo se ha eliminado de la lista", "success");
        }
        else {
            swal("Estimado Usuario", "El Cargo no se ha eliminado, por favor vuelva a intentar", "error");
        }

    });
});
//Establecer número de cargos por tabla
function ContadorCargos(NombreTabla) {
    var location = ".Epptable #Tabla_" + NombreTabla + " > tbody";
    var location_span = "#SpanTabla_" + NombreTabla;
    var tableBody = $(location);
    var span = $(location_span);
    var cont = 0;
    tableBody.find('tr').each(function () {
        cont = cont + 1;
    });
    span[0].innerText = " (" + cont.toString() + ")";
}
$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });
});
///Imagenes///
//Mostrar Imagenes en Modal1
$(function () {
    $(".MostrarImagen").click(function () {
        $('#imagen_modal').html('');
        $("#Nombre_EPP_img").empty();

        $("#val-error-imagen").css("display", "none");
        $("#val-error-imagen").text('');

        var modal = document.getElementById('myModal1');
        var row = $(this).attr('name');
        var NombreEPP = $(this).attr('nameEPP');
        $("#Nombre_EPP_img").append(NombreEPP);
        $('#IdTabla_img').val(row.toString());
        var IdTabla = 'ImagenEPP ' + row;
        var TablaElegida = document.getElementById(IdTabla);
        $('#imagen_modal').html($(TablaElegida).html());
        modal.style.display = "block";
    });
});
//Funciones del Modal1
$(document).ready(function () {
    var modal = document.getElementById('myModal1');
    var span = document.getElementById("close_modal1");
    span.onclick = function () {
        modal.style.display = "none";
        $("#val-error-imagen").css("display", "none");
        $("#val-error-imagen").text('');
    }
    document.addEventListener('keyup', function (e) {
        if (e.keyCode == 27) {
            modal.style.display = "none";
            $("#val-error-imagen").css("display", "none");
            $("#val-error-imagen").text('');
        }
    });
});
//Cargar Imagenes
$(function () {
    $('#btn-adj-img').click(function () {
        var LimiteMb = $('#MB_limit').val();
        var modal = document.getElementById('myModal1');
        var IdTabla = $('#IdTabla_img').val();
        var Img = "#Imagen_" + IdTabla;
        var ImgD = "#Imagen_D_" + IdTabla;
        var ImgContainer = "#Imagen_C_" + IdTabla;

        var LabelImagen = $(Img);

        if (window.FormData !== undefined) {
            var fileUpload = $("#input-img").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
                fileData.append('extraParam1', $(Img).val());
                fileData.append('extraParam2', $(ImgD).val());
            }
            PopupPosition();
            $.ajax({
                url: '/AdmoEPP/UploadImg',
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (result) {
                    fileUpload.value = "";
                    $("#val-error-imagen").css("display", "none");
                    $("#val-error-imagen").text('');
                    if (result.probar == false) {
                        $("#val-error-imagen").text(result.resultado);
                        $("#val-error-imagen").css("display", "block");
                    }
                    else {

                        $(ImgContainer).attr("src", result.Thumbnails);
                        $(Img).val(result.NombreArchivos);
                        $(ImgD).val(result.NombreArchivos_short);
                        LabelImagen = result.NombreArchivos;
                        if (result.display == true) {
                            $(ImgContainer).css("display", "block");
                        }
                        else {
                            $(ImgContainer).css("display", "none");
                        }

                        modal.style.display = "none";
                        $("#val-error-imagen").css("display", "none");
                        $("#val-error-imagen").text('');
                    }
                    OcultarPopupposition();
                }
                ,
                error: function (err) {
                    var Error_response = err.responseText;
                    if (Error_response.indexOf("la longitud de solicitud") >= 0) {
                        $("#val-error-imagen").text("Error al cargar el archivo, la imagen debe tener un tamaño máximo de " + LimiteMb + "MB");
                        $("#val-error-imagen").css("display", "block");
                    }
                    else {
                        $("#val-error-imagen").text("Error al cargar el archivo");
                        $("#val-error-imagen").css("display", "block");
                    }
                    fileUpload.value = "";

                    OcultarPopupposition();
                }
            });
        }
        else {
        }
    });
});
//Eliminar Imagenes
$(function () {
    $("#EliminarImagen").click(function () {

        var modal = document.getElementById('myModal1');
        var IdTabla = $('#IdTabla_img').val();
        var Img = "#Imagen_" + IdTabla;
        var ImgD = "#Imagen_D_" + IdTabla;
        var ImgContainer = "#Imagen_C_" + IdTabla;
        var LabelImagen = $(Img).val();

        $(ImgContainer).css("display", "none");
        $(Img).val(null);
        $(ImgD).val(null);
        $(ImgContainer).attr("src", null);

        PopupPosition();
        $.ajax({
            type: "POST",
            url: "/AdmoEPP/EliminarImg",
            data: '{ruta: "' + LabelImagen + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {

                OcultarPopupposition();
                if (response.probar == false) {
                }
                else {

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                OcultarPopupposition();
            }
        });

        modal.style.display = "none";
        $("#val-error-imagen").css("display", "none");
        $("#val-error-imagen").text('');

    });

});
///Archivos///
//Mostrar Archivos en Modal2
$(function () {
    $(".MostrarArchivo").click(function () {


        $('#archivo_modal').html('');
        $("#Nombre_EPP_arch").empty();
        $("#Nombre_EPP_arch_name").empty();

        $("#val-error-archivo").css("display", "none");
        $("#val-error-archivo").text('');

        var modal = document.getElementById('myModal2');
        var row = $(this).attr('name');
        var NombreEPP = $(this).attr('nameEPP');
        $("#Nombre_EPP_arch").append(NombreEPP);
        $('#IdTabla_arch').val(row.toString());
        var IdTabla = 'ArchivoEPP ' + row;
        var ImgContainer = "#Archivo_D_" + row;
        var NombreArchivo = $(ImgContainer).val();
        $("#Nombre_EPP_arch_name").append(NombreArchivo);
        var TablaElegida = document.getElementById(IdTabla);
        $('#archivo_modal').html($(TablaElegida).html());
        modal.style.display = "block";
    });
});
//Funciones del Modal1
$(document).ready(function () {
    var modal = document.getElementById('myModal2');
    var span = document.getElementById("close_modal2");
    span.onclick = function () {
        modal.style.display = "none";
        $("#val-error-archivo").css("display", "none");
        $("#val-error-archivo").text('');
    }
    document.addEventListener('keyup', function (e) {
        if (e.keyCode == 27) {
            modal.style.display = "none";
            $("#val-error-archivo").css("display", "none");
            $("#val-error-archivo").text('');
        }
    });
});
//Cargar Archivos
$(function () {
    $('#btn-adj-arch').click(function () {
        var LimiteMb = $('#MB_limit').val();
        var modal = document.getElementById('myModal2');
        var IdTabla = $('#IdTabla_arch').val();
        var arch = "#Archivo_" + IdTabla;
        var archD = "#Archivo_D_" + IdTabla;
        var ArchContainer = "#Archivo_C_" + IdTabla;

        if (window.FormData !== undefined) {
            var fileUpload = $("#input-arch").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
                fileData.append('extraParam1', $(arch).val());
                fileData.append('extraParam2', $(archD).val());
            }
            PopupPosition();
            $.ajax({
                url: '/AdmoEPP/UploadArchivo',
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (result) {
                    fileUpload.value = "";
                    $("#val-error-archivo").css("display", "none");
                    $("#val-error-archivo").text('');
                    if (result.probar == false) {
                        $("#val-error-archivo").text(result.resultado);
                        $("#val-error-archivo").css("display", "block");
                    }
                    else {
                        var ArchivoShort = result.NombreArchivos_short;

                        $("#Nombre_EPP_arch_name").append(ArchivoShort);
                        if (ArchivoShort.length > 30) {
                            var res = result.NombreArchivos_short.substring(0, 30) + "...";
                            $(ArchContainer).empty();
                            $(ArchContainer).append(res);
                        }
                        else {
                            $(ArchContainer).empty();
                            $(ArchContainer).append(ArchivoShort);
                        }

                        $(arch).val(result.NombreArchivos);
                        $(archD).val(result.NombreArchivos_short);

                        if (result.display == true) {
                            $(ArchContainer).css("display", "block");
                        }
                        else {
                            $(ArchContainer).css("display", "none");
                        }

                        modal.style.display = "none";
                        $("#val-error-archivo").css("display", "none");
                        $("#val-error-archivo").text('');
                    }
                    OcultarPopupposition();
                }
                ,
                error: function (err) {
                    var Error_response = err.responseText;
                    if (Error_response.indexOf("la longitud de solicitud") >= 0) {
                        $("#val-error-archivo").text("Error al cargar el archivo, el archivo debe tener un tamaño máximo de " + LimiteMb + "MB");
                        $("#val-error-archivo").css("display", "block");
                    }
                    else {
                        $("#val-error-archivo").text("Error al cargar el archivo");
                        $("#val-error-archivo").css("display", "block");
                    }
                    fileUpload.value = "";
                    OcultarPopupposition();
                }
            });
        }
        else {
        }
    });
});
//Eliminar Archivos
$(function () {
    $("#EliminarArchivo").click(function () {

        var modal = document.getElementById('myModal2');
        var IdTabla = $('#IdTabla_arch').val();
        var arch = "#Archivo_" + IdTabla;
        var archD = "#Archivo_D_" + IdTabla;
        var ArchContainer = "#Archivo_C_" + IdTabla;
        var LabelArchivo = $(arch).val();

        $(ArchContainer).css("display", "none");
        $(arch).val(null);
        $(archD).val(null);
        $(ArchContainer).val(null);


        PopupPosition();
        $.ajax({
            type: "POST",
            url: "/AdmoEPP/EliminarArchivo",
            data: '{ruta: "' + LabelArchivo + '" }',
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (response) {

                OcultarPopupposition();
                if (response.probar == false) {
                }
                else {

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                OcultarPopupposition();
            }
        });

        modal.style.display = "none";
        $("#val-error-archivo").css("display", "none");
        $("#val-error-archivo").text('');

    });

});
