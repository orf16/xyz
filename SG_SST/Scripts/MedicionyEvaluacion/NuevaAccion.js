//Funciones datetime
    $(document).ready(function () {
        ConstruirDatePickerPorElemento('Fecha_dil');
        ConstruirDatePickerPorElemento('Fecha_hall');
        localStorage.clear();
        $("#Fecha_hall").on("change", function () {

            var from = $("#Fecha_dil").val();
            var to = $("#Fecha_hall").val();
            
            var fecha_dili = $("#Fecha_dil").val().split("/");
            var fecha_halla = $("#Fecha_hall").val().split("/");
            var datedili = new Date(fecha_dili[2], fecha_dili[1], fecha_dili[0]);
            var datehalla = new Date(fecha_halla[2], fecha_halla[1], fecha_halla[0]);

            
            if (datedili < datehalla) {
                    $("#val-fechahall").css("display", "block");
                    $("#val-fechahall").text('La fecha de hallazgo no puede ser posterior a la de diligenciamiento');
            }
            else {
                $("#val-fechahall").css("display", "none");
                $("#val-fechahall").text('');
            }
        });

   
    });
// Buscar por Cédula
    $(document).ready(function () {
        $("#Halla_Num_Doc").on('keyup', function () {
            $("#busqueda").replaceWith('<input class="form-control" id="busqueda" maxlength="300" name="busqueda" type="text" value="" style="display:none;">');
            $("#msj_novedad_documentacion1").text('');
            $("#div_novedad_documentacion1").removeClass("alert alert-info alert-warning alert-danger alert-success");
            $("result").empty();
            $("#Halla_Nombre").val('');
            $("#Halla_TipoDoc").val('');
            $("#Halla_Cargo").val('');
            if ($(this).val().length > 5) {
                PopupPosition();
                $.ajax({
                    type: "POST",
                    url: "/Acciones/BuscarPersonaDocumentoCargo1",
                    data: '{documento: "' + $("#Halla_Num_Doc").val() + '" }',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    dataType: "json",
                    success: function (response) {
                        OcultarPopupposition();
                        var PersonaArray = new Array();
                        var PersonaArray1 = new Array();
                        var array = [];
                        if (response.RelacionLaboral!=null) {
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
                            $("#Halla_Nombre").val(NombreS);
                            $("#Halla_TipoDoc").val(TipoDocumento);
                            $("#Halla_Cargo").val(Ocupacion);
                        });

                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        OcultarPopupposition();
                        var value = "";
                        $("#Halla_Nombre").val(value);
                        $("#Halla_TipoDoc").val(value);

                    }
                });


            }


          });
     });   
//buscar nombre y cargos responsable
    $(document).ready(function () {
        $("#Accion_Res_Num_Doc").on('keyup', function () {     
            $("#msj_novedad").text('');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");

            $("#Nombre_Responsable").val('');
            $("#Cargo_Responsable").val('');
            if ($(this).val().length > 5) {
                PopupPosition();
                $.ajax({
                    type: "POST",
                    url: "/Acciones/BuscarPersonaDocumentoCargo1",
                    data: '{documento: "' + $("#Accion_Res_Num_Doc").val() + '" }',
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
                            $("#Nombre_Responsable").val(NombreS);
                            $("#Cargo_Responsable").val(Ocupacion);
                            $("#msj_novedad").text("Persona encontrada");
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                        });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        OcultarPopupposition();
                        $("#Nombre_Responsable").val('');
                        $("#Cargo_Responsable").val('');
                    }
                });


            }


        });
    });
//buscar nombre y cargos Auditor
    $(document).ready(function () {
        $("#Accion_aud_Num_Doc").on('keyup', function () {
            $("#msj_novedad").text('');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");

            $("#Nombre_Auditor").val('');
            $("#Cargo_Auditor").val('');

            
            if ($(this).val().length > 5) {
                PopupPosition();
                $.ajax({
                    type: "POST",
                    url: "/Acciones/BuscarPersonaDocumentoCargo1",
                    data: '{documento: "' + $("#Accion_aud_Num_Doc").val() + '" }',
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
                            $("#Nombre_Auditor").val(NombreS);
                            $("#Cargo_Auditor").val(Ocupacion);

                            $("#msj_novedad").text("Persona encontrada");
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
                        });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        OcultarPopupposition();
                        $("#Nombre_Auditor").val('');
                        $("#Cargo_Auditor").val('');
                    }
                });


            }


        });
    });
 
//Habilitar cambio de documentacion 

    $(document).ready(function () {
        $('input:radio[name="Cambio_Doc"]').change(function () {
            if (this.value == 'Si') {
                $("#Des_Cambio_Doc").removeAttr('disabled');
                $("#msj_novedad_documentacion").text("Descripción obligatoria");
                $("#div_novedad_documentacion").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
            }
            else if (this.value == 'No') {
                $("#Des_Cambio_Doc").attr('disabled', 'disabled');
                $("#msj_novedad_documentacion").text("");
                $("#div_novedad_documentacion").removeClass("alert alert-info alert-warning alert-danger alert-success");
            }
        });
    });
$(document).ready(function () {


    if ($(".RadioNo").is(':checked')) {
        $("#Des_Cambio_Doc").attr('disabled', 'disabled');
        $("#msj_novedad_documentacion").text("");
        $("#div_novedad_documentacion").removeClass("alert alert-info alert-warning alert-danger alert-success");
    }
    if ($(".RadioSi").is(':checked')) {
        $("#Des_Cambio_Doc").removeAttr('disabled');
        $("#msj_novedad_documentacion").text("Descripción obligatoria");
        $("#div_novedad_documentacion").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
    }



});
//Pasar informacion del formulario a la variable session
    $(function () {
        $("#NuevoHall").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {
                },
                dataType: "json",
                traditional: true
            });


        });
        $(".editar-hallazgo").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {
                },
                dataType: "json",
                traditional: true
            });


        });
        $(".btnEliminarHall").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {
                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnArbol").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnCausa").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnCinco").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnLluvias").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnPlan").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnSeg").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#btnAdj").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#AgregarFirmaAud").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#AgregarFirmaRes").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#QuitarFirmaAud").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $("#QuitarFirmaRes").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $(".btnEditarActividad").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });
        $(".btnEditarSeg").click(function () {
            var stringArray = new Array();
            stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
            stringArray[1] = $("#Fecha_dil").val();
            stringArray[2] = $('input:radio[name="Clase"]:checked').val();
            stringArray[3] = $("#Fecha_hall").val();
            stringArray[4] = $("#Halla_Num_Doc").val();
            stringArray[5] = $("#Halla_TipoDoc").val();
            stringArray[6] = $("#Halla_Nombre").val();
            stringArray[7] = $("#Halla_Cargo").val();
            stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
            stringArray[9] = $("#Correccion").val();
            stringArray[10] = $("#Causa_Raiz").val();
            stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
            stringArray[12] = $("#Des_Cambio_Doc").val();
            stringArray[13] = $("#Verificacion").val();
            stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
            stringArray[15] = $("#Nombre_Auditor").val();
            stringArray[16] = $("#Cargo_Auditor").val();
            stringArray[17] = $("#Nombre_Responsable").val();
            stringArray[18] = $("#Cargo_Responsable").val();
            stringArray[19] = $("#ListaOrigen option:selected").val();
            stringArray[20] = $("#Otro_Origen").val();
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/GuardarFormulario",
                data: postData,
                success: function (data) {

                },
                dataType: "json",
                traditional: true
            });


        });



    });
 
//Adjuntar firma auditor
 

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
                    url: '/Acciones/UploadImgAud',
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        OcultarPopupposition();
                        $("#msj_novedad").text('');
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");
                        if (result.probar == false) {
                            $("#msj_novedad").text(result.resultado);
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                        }
                        else {
                            swal({
                                title: "Operación Exitosa",
                                text: "Firma de Auditor Adjuntada",
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
                        OcultarPopupposition();
                        $("#msj_novedad").text("Error al cargar el archivo");
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

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
                    url: '/Acciones/UploadImgRes',
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        OcultarPopupposition();
                        $("#msj_novedad").text('');
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success");
                        if (result.probar == false) {
                            $("#msj_novedad").text(result.resultado);
                            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                        }
                        else {
                            swal({
                                title: "Operación Exitosa",
                                text: "Firma del responsable Adjuntada",
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
                        OcultarPopupposition();
                        $("#msj_novedad").text("Error al cargar el archivo");
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                    }
                });
            } else {

            }
        });
    });
 
//Quitar Firma Auditor
 

    $(function () {
        $('#QuitarFirmaAud').click(function () {

            $.ajax({
                url: '/Acciones/QuitarImgAud',
                type: "POST",
                success: function (result) {

                    if (result.probar == false) {
                        $("#msj_novedad").text(result.resultado);
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        swal({
                            title: "Operación Exitosa",
                            text: "Firma del Auditor Eliminada",
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
                    $("#msj_novedad").text("Ha ocurrido un error, no se ha podido eliminar la imagen o ya no existe");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                }
            });


        });
    });
 
//Quitar Firma Responsable
 

    $(function () {
        $('#QuitarFirmaRes').click(function () {

            $.ajax({
                url: '/Acciones/QuitarImgRes',
                type: "POST",
                success: function (result) {

                    if (result.probar == false) {
                        $("#msj_novedad").text(result.resultado);
                        $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        swal({
                            title: "Operación Exitosa",
                            text: "Firma del responsable Eliminada",
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
                    $("#msj_novedad").text("Ha ocurrido un error, no se ha podido eliminar la imagen o ya no existe");
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                }
            });


        });
    });
 
//Guardad AC/AP
 
    $(function () {

        $("#GuardarAccion").bind("click", function () {
            var onEventLaunchGuardar = new postGuardar();
            onEventLaunchGuardar.launchGuardar();
        });
    });
function postGuardar() {
    this.launchGuardar = function () {

        //Traer datos al modelo JSON
        var stringArray = new Array();
        stringArray[0] = $('input:radio[name="Tipo"]:checked').val();
        stringArray[1] = $("#Fecha_ocurrencia").val();
        stringArray[2] = $('input:radio[name="Clase"]:checked').val();
        stringArray[3] = $("#Fecha_hall").val();
        stringArray[4] = $("#Halla_Num_Doc").val();
        stringArray[5] = $("#Halla_TipoDoc").val();
        stringArray[6] = $("#Halla_Nombre").val();
        stringArray[7] = $("#Halla_Cargo").val();
        stringArray[8] = $("#Pk_Id_Sede1 option:selected").val();
        stringArray[9] = $("#Correccion").val();
        stringArray[10] = $("#Causa_Raiz").val();
        stringArray[11] = $('input:radio[name="Cambio_Doc"]:checked').val();
        stringArray[12] = $("#Des_Cambio_Doc").val();
        stringArray[13] = $("#Verificacion").val();
        stringArray[14] = $('input:radio[name="Eficacia"]:checked').val();
        stringArray[15] = $("#Nombre_Auditor").val();
        stringArray[16] = $("#Cargo_Auditor").val();
        stringArray[17] = $("#Nombre_Responsable").val();
        stringArray[18] = $("#Cargo_Responsable").val();
        stringArray[19] = $("#Fecha_dil").val();
        stringArray[20] = $('input:radio[name="Estado"]:checked').val();
        stringArray[21] = $("#ListaOrigen option:selected").val();
        stringArray[22] = $("#Otro_Origen").val();
        // Construir objeto JSON
        var Accion = {
            Tipo: stringArray[0],
            Fecha_ocurrencia: stringArray[1],
            Clase: stringArray[2],
            Fecha_hall: stringArray[3],
            Halla_Num_Doc: stringArray[4],
            Halla_Nombre: stringArray[6],
            Halla_TipoDoc: stringArray[5],
            Halla_Cargo: stringArray[7],
            Halla_Sede: stringArray[8],
            Correccion: stringArray[9],
            Causa_Raiz: stringArray[10],
            Cambio_Doc: stringArray[11],
            Des_Cambio_Doc: stringArray[12],
            Verificacion: stringArray[13],
            Eficacia: stringArray[14],
            Nombre_Auditor: stringArray[15],
            Cargo_Auditor: stringArray[16],
            Nombre_Responsable: stringArray[17],
            Cargo_Responsable: stringArray[18],
            Fecha_dil: stringArray[19],
            Estado: stringArray[20],
            Origen: stringArray[21],
            Otro_Origen: stringArray[22]
        };

        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Acciones/PostGuardar',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(Accion),
            success: function (data) {
                OcultarPopupposition();

                $("#val-fechahall").css("display", "none");
                $("#val-fechahall").text('');

                $("#val-fechadil").css("display", "none");
                $("#val-fechadil").text('');

                $("#val-tipo").css("display", "none");
                $("#val-tipo").text('');

                $("#val-clase").css("display", "none");
                $("#val-clase").text('');

                $("#val-numdoc").css("display", "none");
                $("#val-numdoc").text('');

                $("#val-nombre").css("display", "none");
                $("#val-nombre").text('');

                $("#val-tipodoc").css("display", "none");
                $("#val-tipodoc").text('');

                $("#val-cargo").css("display", "none");
                $("#val-cargo").text('');

                $("#val-sede").css("display", "none");
                $("#val-sede").text('');

                $("#val-deschall").css("display", "none");
                $("#val-deschall").text('');

                $("#val-cambiodoc").css("display", "none");
                $("#val-cambiodoc").text('');

                $("#val-origen").css("display", "none");
                $("#val-origen").text('');

                if (data.probar == false) {
                    if (data.url!=null) {
                        window.location = data.url;
                    }
                    $("#msj_novedad_ult").html(data.status);
                    $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");

                    if (data.Validacion[0] == true) {
                        $("#val-fechahall").css("display", "block");
                        $("#val-fechahall").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-fechadil").css("display", "block");
                        $("#val-fechadil").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-tipo").css("display", "block");
                        $("#val-tipo").text(data.ValidacionStr[2]);
                    }
                    if (data.Validacion[3] == true) {
                        $("#val-clase").css("display", "block");
                        $("#val-clase").text(data.ValidacionStr[3]);
                    }
                    if (data.Validacion[4] == true) {
                        $("#val-numdoc").css("display", "block");
                        $("#val-numdoc").text(data.ValidacionStr[4]);
                    }
                    if (data.Validacion[5] == true) {
                        $("#val-nombre").css("display", "block");
                        $("#val-nombre").text(data.ValidacionStr[5]);
                    }
                    if (data.Validacion[6] == true) {
                        $("#val-tipodoc").css("display", "block");
                        $("#val-tipodoc").text(data.ValidacionStr[6]);
                    }
                    if (data.Validacion[7] == true) {
                        $("#val-cargo").css("display", "block");
                        $("#val-cargo").text(data.ValidacionStr[7]);
                    }
                    if (data.Validacion[8] == true) {
                        $("#val-sede").css("display", "block");
                        $("#val-sede").text(data.ValidacionStr[8]);
                    }
                    if (data.Validacion[9] == true) {
                        $("#val-deschall").css("display", "block");
                        $("#val-deschall").text(data.ValidacionStr[9]);
                    }
                    if (data.Validacion[10] == true) {
                        $("#val-cambiodoc").css("display", "block");
                        $("#val-cambiodoc").text(data.ValidacionStr[10]);
                    }
                    if (data.Validacion[11] == true) {
                        $("#val-origen").css("display", "block");
                        $("#val-origen").text(data.ValidacionStr[11]);
                    }
                    swal("Advertencia",data.status);

                }
                else {
                    swal({
                        title: "Operación Exitosa",
                        text: "Acción agregada exitosamente",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: false
                    },
                function () {
                    window.location.href = "/Acciones/ConsultaACAP"
                });
                }
            },
            error: function (data) {
                OcultarPopupposition();
                console.log(data.status)
            }
        });

    }
}

 
//Eliminar Actividad
 

    $(function () {
        $('.btnEliminarAct').click(function () {
            var Id_Elm = $(this).attr('id');
            swal({
                title: "Advertencia",
                text: "Estas seguro(a) que desea eliminar esta actividad?",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si Borrarla",
                cancelButtonText: "No",
                closeOnConfirm: false
            },
function () {

    $.ajax({
        type: "POST",
        url: "/Acciones/EliminarActividad",
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
                    title: "Actividad Eliminada",
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
            $("#msj_novedad").text('No se ha podido eliminar la Actividad o no existe');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
        }
    });

});




        });
    });
 
//Eliminar Seguimiento
 

    $(function () {
        $('.btnEliminarSeg').click(function () {
            var Id_Elm = $(this).attr('id');
            swal({
                title: "Advertencia",
                text: "Estas seguro(a) que desea eliminar este seguimiento?",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si Borrarlo",
                cancelButtonText: "No",
                closeOnConfirm: false
            },
function () {

    $.ajax({
        type: "POST",
        url: "/Acciones/EliminarSeguimiento",
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
                    title: "Seguimiento Eliminado",
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
            $("#msj_novedad").text('No se ha podido eliminar el seguimiento o no existe');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
        }
    });

});




        });
    });
 
//Eliminar Hallazgo
 

    $(function () {
        $('.btnEliminarHall').click(function () {
            var Id_Elm = $(this).attr('id');
            swal({
                title: "Advertencia",
                text: "Estas seguro(a) que desea eliminar este hallazgo?",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si Borrarlo",
                cancelButtonText: "No",
                closeOnConfirm: false
            },
function () {

    $.ajax({
        type: "POST",
        url: "/Acciones/EliminarHallazgo",
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
                    title: "Hallazgo Eliminado",
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
            $("#msj_novedad").text('No se ha podido eliminar el hallazgo o no existe');
            $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
        }
    });

});




        });
    });
 
//Cancelar Accion
 

    $(function () {
        $('#CancelarAccion').click(function () {
            swal({
                title: "Advertencia",
                text: "Estas seguro(a) que desea cancelar el registro de la acción?",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si",
                cancelButtonText: "No",
                closeOnConfirm: false
            },
function () {
    $.ajax({
        type: "POST",
        url: "/Acciones/CancelarNuevaAccion",
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: "json",
        success: function (response) {
            window.location = response.url;
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

});




        });
    });
 





    //Mostrar cargos en Modal ayuda analisis
    $(function () {
        $("#MostrarAyudaAnalisis").click(function () {
            var modal = document.getElementById('myModal');
            modal.style.display = "block";
        });
    });
    //Funciones del Modal ayuda analisis
    $(document).ready(function () {
        var modal = document.getElementById('myModal');
        var span = document.getElementById("close_modal");
        span.onclick = function () {
            modal.style.display = "none";
        }
        document.addEventListener('keyup', function (e) {
            if (e.keyCode == 27) {
                modal.style.display = "none";
            }
        });
    });


    //Mostrar cargos en Modal ayuda plan accion
    $(function () {
        $("#MostrarAyudaPlanAccion").click(function () {
            var modal = document.getElementById('myModal1');
            modal.style.display = "block";
        });
    });
    //Funciones del Modal ayuda plan accion
    $(document).ready(function () {
        var modal = document.getElementById('myModal1');
        var span = document.getElementById("close_modal1");
        span.onclick = function () {
            modal.style.display = "none";
        }
        document.addEventListener('keyup', function (e) {
            if (e.keyCode == 27) {
                modal.style.display = "none";
            }
        });
    });

    //Mostrar cargos en Modal ayuda seguimiento
    $(function () {
        $("#MostrarAyudaSeguimiento").click(function () {
            var modal = document.getElementById('myModal2');
            modal.style.display = "block";
        });
    });
    //Funciones del Modal ayuda ayuda seguimiento
    $(document).ready(function () {
        var modal = document.getElementById('myModal2');
        var span = document.getElementById("close_modal2");
        span.onclick = function () {
            modal.style.display = "none";
        }
        document.addEventListener('keyup', function (e) {
            if (e.keyCode == 27) {
                modal.style.display = "none";
            }
        });
    });

    //Mostrar cargos en Modal ayuda cerrar la acción
    $(function () {
        $("#MostrarAyudacerrar").click(function () {
            var modal = document.getElementById('myModal3');
            modal.style.display = "block";
        });
    });
    //Funciones del Modal ayuda ayuda cerrar la acción
    $(document).ready(function () {
        var modal = document.getElementById('myModal3');
        var span = document.getElementById("close_modal3");
        span.onclick = function () {
            modal.style.display = "none";
        }
        document.addEventListener('keyup', function (e) {
            if (e.keyCode == 27) {
                modal.style.display = "none";
            }
        });
    });


//Funciones origen
    function jsFunction_122() {
        $("#Otro_Origen").prop('disabled', 'disabled');
        $("#val-otroorigen").text('');
        $("#val-otroorigen").css("display", "none");
        $("#otros_origen_id").css("display", "none");

        var myselect = document.getElementById("ListaOrigen");
        var idselect = myselect.options[myselect.selectedIndex].value;
        
        
        if (idselect!=null) {
            if (idselect == "Otros") {
                $("#Otro_Origen").prop('disabled', '');
                $("#Otro_Origen").removeAttr('disabled');
                $("#val-otroorigen").text('Por favor indique el origen del hallazgo');
                $("#val-otroorigen").css("display", "block");
                $("#otros_origen_id").css("display", "block");
            }
            else {
                $("#Otro_Origen").val('');
            }
        }
        else {
            $("#Otro_Origen").val('');
        }
    }
    $(document).ready(function () {
        $("#Otro_Origen").on('keyup', function () {

            var myselect = document.getElementById("ListaOrigen");
            var idselect = myselect.options[myselect.selectedIndex].value;

            if (idselect != null) {
                if (idselect == "Otros") {
                    if ($("#Otro_Origen").val() != null) {
                        if ($("#Otro_Origen").val() != "") {
                            $("#val-otroorigen").text('');
                            $("#val-otroorigen").css("display", "none");
                        }
                        else {
                            $("#val-otroorigen").text('Por favor indique el origen del hallazgo');
                            $("#val-otroorigen").css("display", "block");
                        }
                    }
                    else {
                        $("#val-otroorigen").text('Por favor indique el origen del hallazgo');
                        $("#val-otroorigen").css("display", "block");
                    }
                }

            }

            

        });
    });


    //Paginador
    jQuery(function ($) {
        var items = $(".paginver");
        var numItems = items.length;
        var perPage = 4;
        items.slice(perPage).hide();
        $(".paginationver").pagination({
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
        var perPage = 4;
        items.slice(perPage).hide();
        $(".paginationcom").pagination({
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