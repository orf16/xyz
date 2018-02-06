var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

////jqXHRData needed for grabbing by data object of fileupload scope
var jqXHRData;
$(document).ready(function () {
  
    //Initialization of fileupload
    GrabarCondiciones();
        //Handler for "Start upload" button 
        $("#hl-start-upload").on('click', function () {
            if (jqXHRData) {
                jqXHRData.submit();
            }
            else {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'No se ha cargado ningún Archivo de Evidencia',
                    confirmButtonColor: '#7E8A97',
                    confirmButtonText: "Continuar",
                });
            }
            return false;

        });
   
});




function GrabarCondiciones() {
    valci();
    if ($("#formularioCondicion").valid()) {
        DescribeCondicionVM = $('#DesCI').val(),
        UbicacionespecificaVM = $('#Uesp').val(),
        RiesgopeligroVM = $('#tipopeligro').val(),
        ClasificacionRiesgoVM = $('#FK_Clasificacion_De_Peligro').val(),
        ConfiguracioncondicionVM = $('#DescripcionConfig').val(),
        idinspeccion = $("#idinspeccion").val(),
        OtroRiesgoVM = $("#Otro").val();

        'use strict';
        $('#Evidencia').fileupload({

            url: '/PlandeInspeccion/SubirArchivo',
            dataType: 'json',
            add: function (e, data) {
                jqXHRData = data
            },
            done: function (event, data) {
                if (data.result == 1) {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'El registro no fué exitoso, el archivo cargado no es permitido.',
                        confirmButtonColor: '#7E8A97',
                        confirmButtonText: "Continuar",
                    });
                    $("#AgregaCondicion").modal("hide");
                    $("#infoinspeccion").slideDown();
                    $("#infoinspeccions").slideDown();
                    $("#panelascf").slideDown();
                    return;
                }
                else {
                    if (data.result.isUploaded == true & $('#DesCI').val() != "" & $('#Uesp').val() != "" & $('#tipopeligro').val() != "" & $('#DescripcionConfig').val() != "") {
                        var condiciones = {
                            DescribeCondicionVM: $('#DesCI').val(),
                            UbicacionespecificaVM: $('#Uesp').val(),
                            RiesgopeligroVM: $('#tipopeligro').val(),
                            ClasificacionRiesgoVM: $('#FK_Clasificacion_De_Peligro').val(),
                            EvidenciacondicionVM: (data.result.message),
                            ConfiguracioncondicionVM: $('#DescripcionConfig').val(),
                            idinspeccion: $("#idinspeccion").val(),
                            OtroRiesgoVM: $("#Otro").val(),
                        };

                        $.ajax({
                            type: 'GET',
                            url: '/PlandeInspeccion/GuardarCondicionesInseguras',
                            data: condiciones,
                            traditional: true,
                            success: function (result) {
                                if (result) {
                                    OcultarPopupposition();
                                   
                                    $('#condicionesins').empty();
                                    $('#condicionesins').append;
                                    var contadorcs = 0;
                                    $.each(result.Data, function (ind, element) {
                                        var elementoscs = '<tr name="condiciones" id="contador">' +
                                                        '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="condicionesIns" name1="' + element.EDDiasDesde + '" name2="' + element.EDDiasHasta + '" desc="' + element.EDDescribeCondicion + '" value="' + element.EDpkCondicion + '" id=' + contadorcs + '  class ="condicionesIns">' + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDescribePrioridad" id="EDescribePrioridad' + contadorcs + '"value="' + element.EDescribePrioridad + '">' + element.EDescribePrioridad + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasDesde" id="EDDiasDesde' + contadorcs + '"value="' + element.EDDiasDesde + '">' + element.EDDiasDesde + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + element.EDDiasHasta + '">' + element.EDDiasHasta + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
                                                        '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-search btn-md">' + element.EDEvidenciacondicion + '</span></a></td>' +
                                                        '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + element.EDpkCondicion + ',' + element.EDPkInspeccion + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr>'
                                        $('#condicionesins').append(elementoscs)
                                        contadorcs = contadorcs + 1
                                    });
                                    paginador("#condicionesins", "tr[name = condiciones]", "#paginador9")
                                    if (result.Data) {
                                        
                                        swal({
                                            type: 'success',
                                            title: 'Estimado Usuario',
                                            text: 'Condición Insegura Agregada Satisfactoriamente',
                                            showConfirmButton: true,
                                            confirmButtonText: 'Aceptar',
                                            confirmButtonColor: '#7E8A97'
                                        });
                                        $('.confirm').on('click', function () {
                                           // $("#AgregaCondicion").modal("hide");
                                            $("#formularioCondicion").trigger("reset");
                                           
                                          
                                        });
                           

                                    }
                                }

                            },
                            error: function (result) {
                                swal({
                                    type: 'warning',
                                    title: 'Estimado Usuario',
                                    text: 'No fué posible Guardar la Información, por favor revise los datos Ingresados pueden estar vacios o el tamaño no es permitido.',
                                    confirmButtonColor: '#7E8A97',
                                    confirmButtonText: "Continuar",
                                });
                            }
                        });
                    } else {
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'No fué posible Guardar la Información, por favor revise los datos Ingresados pueden estar vacios, máximo de caracteres excedido, ó el archivo cargado no es permitido solo puede cargar Pdf, Jpg, ó Png hasta de 6MB.',
                            confirmButtonColor: '#7E8A97',
                            confirmButtonText: "Continuar",
                        });
                    }
                }
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });







    }
}

//function GrabarCondiciones() {
//    valci();
//    if ($("#formularioCondicion").valid()) {
//        DescribeCondicionVM = $('#DesCI').val(),
//        UbicacionespecificaVM = $('#Uesp').val(),
//        RiesgopeligroVM = $('#tipopeligro').val(),
//        ClasificacionRiesgoVM = $('#FK_Clasificacion_De_Peligro').val(),
//        ConfiguracioncondicionVM = $('#DescripcionConfig').val(),
//        idinspeccion = $("#idinspeccion").val(),
//        OtroRiesgoVM = $("#Otro").val();

//        var condiciones = {
//            DescribeCondicionVM: $('#DesCI').val(),
//            UbicacionespecificaVM: $('#Uesp').val(),
//            RiesgopeligroVM: $('#tipopeligro').val(),
//            ClasificacionRiesgoVM: $('#FK_Clasificacion_De_Peligro').val(),
//            //EvidenciacondicionVM: (data.result.message),
//            ConfiguracioncondicionVM: $('#DescripcionConfig').val(),
//            idinspeccion: $("#idinspeccion").val(),
//            OtroRiesgoVM: $("#Otro").val(),
//        };

//        $.ajax({
//            type: 'GET',
//            url: '/PlandeInspeccion/GuardarCondicionesInseguras',
//            data: condiciones,
//            traditional: true,
//            success: function (result) {
//                OcultarPopupposition();
//                $("#AgregaCondicion").modal("hide");
//                $('#condicionesins').empty();
//                $('#condicionesins').append;
//                var contadorcs = 0;
//                $.each(result.Data, function (ind, element) {
//                    var elementoscs = '<tr name="condiciones" id="contador">' +
//                                    '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="condicionesIns" name1="' + element.EDDiasDesde + '" name2="' + element.EDDiasHasta + '" desc="' + element.EDDescribeCondicion + '" value="' + element.EDpkCondicion + '" id=' + contadorcs + '  class ="condicionesIns">' + '</td>' +
//                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDescribePrioridad" id="EDescribePrioridad' + contadorcs + '"value="' + element.EDescribePrioridad + '">' + element.EDescribePrioridad + '</td>' +
//                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasDesde" id="EDDiasDesde' + contadorcs + '"value="' + element.EDDiasDesde + '">' + element.EDDiasDesde + '</td>' +
//                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + element.EDDiasHasta + '">' + element.EDDiasHasta + '</td>' +
//                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
//                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
//                                    '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-lg"><span class="glyphicon  glyphicon-search"></span></a></td>' +
//                                     '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + element.EDpkCondicion + ',' + element.EDPkInspeccion + ')" class="btn btn-lg"><span class="glyphicon  glyphicon-trash"></span></a></td>' +
//                                    '</tr>'
//                    $('#condicionesins').append(elementoscs)
//                    contadorcs = contadorcs + 1
//                });
//                paginador("#condicionesins", "tr[name = condiciones]", "#paginador9")
//                if (result.Data) {
//                    swal({ title: "Estimado Usuario", text: "Condición Insegura Agregada Satisfactoriamente.", type: "success", confirmButtonColor: '#7E8A97' },
//                function () {
//                    $("#formularioCondicion").trigger("reset");
//                });

//                }
//            },
//            error: function (result) {
//                swal({
//                    type: 'warning',
//                    title: 'Estimado Usuario',
//                    text: 'No fué posible Guardar la Información.',
//                    confirmButtonColor: '#7E8A97',
//                    confirmButtonText: "Continuar",
//                });
//            }
//        });


//    }
//}


///////funcion para validar y grabar una condicion Insegura a una Inspeccion.

//Funcion para Ver la informacion Adjunta como Evidencia de una Condicion Insegura.

function verImagen(EDpkCondicion) {
    var envio =
        { clavecondicion: EDpkCondicion }
    $.ajax({
        type: 'GET',
        url: '/PlandeInspeccion/ObtenerImagen',
        data: envio,
        traditional: true,
        success: function (result) {
            if (result.Data = !null){          
               var extensiones = result.substring(result.lastIndexOf("."));         
               console.log(extensiones)
               if (extensiones ==".pdf") {
                    $('#ModalPDF').modal('show');
                    $("#imagen").attr("src", "/Content/Doc_Inspecciones/" + result);
                }
               if (extensiones == ".jpg" || extensiones == ".png") {
                    $('#ModalImagen').modal('show');
                    $("#img").attr("src", "/Content/Doc_Inspecciones/" + result);
                }
            }
        }
    });
}

//Validar el formulario de la condicion Insegura.
function valci() {
    $("#formularioCondicion").validate({
        errorClass: "error",

        rules: {
            DesCI: { required: true },
            Uesp: { required: true },
            tipopeligro: { required: true },
            // Otro: { required: true },
            FK_Clasificacion_De_Peligro: { required: true },
            DescripcionConfig: { required: true },
            //Evidencia: { required: true },
            condiciones: { required: true }
        },
        messages:
            {
                DesCI: { required: " * Descripción no puede estar vacio." },
                Uesp: { required: " * Ubicación no puede estar vacio." },
                tipopeligro: { required: " * Debe Seleccionar un Tipo de Peligro." },
                // Otro: { required: " * no puede estar vacio." },
                FK_Clasificacion_De_Peligro: { required: " * Campo no puede estar vacio." },
                DescripcionConfig: { required: " * Selecciona un item de la lista." },
                //Evidencia: { required: " * Se Debe Adjuntar Evidencia." },
                condiciones: { required: " * Debe Seleccionar almenos una Condición Insegura." },
            }
    });

}