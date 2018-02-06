

var urlBase = ""
var urlReporte = '/Reporte';
var addPlanAccion = 1;



$(function () {

    $("#ConsecutivoReporte").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
    $("#RazonSocialEmpresa").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
    $("#nitEmpresa").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
    $("#fechaSistena").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
    $("#NombreQuienReporta").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
    $("#CargoQuienReporta").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
    $("#Cedula").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;



    // Write on keyup event of keyword input element
    $("#buscar").keyup(function () {
        // When value of the input is not blank
        if ($(this).val() != "") {
            // Show only matching TR, hide rest of them
            $("#select_reporte tbody>tr").hide();
            $("#select_reporte td:contains-ci('" + $(this).val() + "')").parent("tr").show();
        }
        else {
            // When there is no input or clean again, show everything back
            $("#select_reporte tbody>tr").show();
        }
    });

    // jQuery expression for case-insensitive filter
    $.extend($.expr[":"],
    {
        "contains-ci": function (elem, i, match, array) {
            return (elem.textContent || elem.innerText || $(elem).text() || "").toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
        }
    });

    darFormatoSoloNumeros("CedulaQuienReporta");
    darFormatoSoloNumeros("CedulaReporta");

    ConstruirDatePickerPorElemento("fechaInicio");
    ConstruirDatePickerPorElemento("fechaFin");
    ConstruirDatePickerPorElemento("FechaOcurrencia");
    tabla = $('#TablaActividades');
    tr = $("#filaActividad");
    $('#agregarFila').on('click', function () {

        var validarActividades = true;
        var valNombre = validarCamposDinamicos($("#actividades"), "nombreActividad", "input", "*");
        var valResponsable = validarCamposDinamicos($("#actividades"), "RespActividad", "input", "*");
        var valFecha = validarCamposDinamicos($("#actividades"), "FecEjecucion", "input", "*");

        var fechaEjecucion;
        var isFechaValida =false;
        var hoy = new Date();

        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
      
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {

            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        if (valNombre == false || valResponsable == false || valFecha==false)
        {
            validarActividades = false;
        }

        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {
         
          
            var fechaEjecucion = $(fila).find("#FecEjecucion").attr("name", "[" + ind + "].FecEjecucion");

         

            if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) > $.datepicker.parseDate('dd/mm/yy', fechaEjecucion.val())) {
                fechaEjecucion.val("");
                validarActividades = false;

            }
           

        });
    
       
            

        if (validarActividades)
        {
            tr.clone().appendTo(tabla).find(':text').val('');
            ConstruirDatePickerPorElemento("FecEjecucion");
        }
        else
        {
          
       
        }
       



        //$("#filaActividad").closest("tr").find("#td_eliminar").find("span[name=iconoRend]").attr("class", "glyphicon glyphicon-erase");

        //$("#filaActividad").closest("tr").find("#td_eliminar").attr("title", "Borrar Actividad")
        //$("#filaActividad").closest("tr").find("#td_eliminar").attr("onclick", "eliminarActividad(this)")
        //addPlanAccion++;
    });


    

    $('#consultarReporte').click(function () {




        var validar = true;
        var form = ("#VerReporte");
        var fechaFin = $("#fechaFin").val();
        var fechaInicio = $("#fechaInicio").val();
        var tipo = $("#FKTipoReporte").val();
        var sedes = $("#sedes").val();
        var cedula = $("#CedulaQuienReporta").val();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        var form = ("#VerReporte");
        var mensaje = "";

        if (fechaFin == "" && fechaInicio == "" && tipo == "" && cedula == "" && sedes.length == 0) {

            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'Por favor seleccionar un campo de busqueda',
                confirmButtonColor: '#7E8A97'
            });
            return false;

        }
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {

            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }

        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaInicio)) {

      
            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'Por favor revisar el rango de Fecha de busqueda',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }



        if (fechaInicio != "") {
            if (fechaFin == "") {

   
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Debe seleccionar una Fecha Fin',
                    confirmButtonColor: '#7E8A97'
                });
                return false;
            }
        }

        if (fechaInicio == "") {
            if (fechaFin != "") {

               swal({
                   type: 'warning',
                   title: 'Estimado Usuario:',
                   text: 'Debe seleccionar una Fecha de Inicio',
                   confirmButtonColor: '#7E8A97'
               });
                return false;
            }
        }


        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaFin)) {
           
      

            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'La Fecha Final no puede ser superior a la Fecha Actual',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        if (fechaFin != "" && fechaInicio == "") {

            if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaFin)) {

             
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Por favor seleccione una Fecha de Inicio',
                    confirmButtonColor: '#7E8A97'
                });

                $("#busqueda").attr("hidden");
                return false;
            }
        }

        if (fechaInicio != "" && fechaFin != "") {
            if ($.datepicker.parseDate('dd/mm/yy', fechaFin) == ($.datepicker.parseDate('dd/mm/yy', fechaInicio))) {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'La Fecha Fin no puede ser igual a la Fecha Actual',
                    confirmButtonColor: '#7E8A97'
                });

                $("#busqueda").attr("hidden");
                return false;
            }
        }

    

        $.ajax({
            type: "POST",
            data: form.serialize(),

            url: urlBase + urlPerfil + '/VerReporte'
        }).done(function (response) {

            if (response) {

                $('#busqueda').html(result);

               
             
            }
          


        });
    });



    $('#generarReporte').click(function () {
        var form = $("#ReporteCondicionesInseguras");
        if (!$("#ReporteCondicionesInseguras").valid()) {
            validacion = false;
        }
        else {
            validacion = true;
        }
        var fechaOcurrencia = $("#FechaOcurrencia").val();

        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {

            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }

        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaOcurrencia)) {
         

            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'La Fecha de Ocurrencia del evento no puede ser superior a la Fecha Actual',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }


        if ($("#imagenesCargar").val() == "") {
         
            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'Por favor agregar imagenes',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }





        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {

            $(fila).find("#nombreActividad").attr("name", "[" + ind + "].nombreActividad");
            $(fila).find("#RespActividad").attr("name", "[" + ind + "].RespActividad");
            $(fila).find("#FecEjecucion").attr("name", "[" + ind + "].FecEjecucion");

        });

        if ($("#Procesos").val() != "")
        {
            if ($("#FK_Proceso").val()=="")
            {
               
            

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'No es permitido seleccionar un Proceso sin el Subproceso',
                    confirmButtonColor: '#7E8A97'
                });
                return false;
            }
        }


        if (validacion == false) {

        
            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'Faltan campos por llenar',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        } else {
            form.submit();
            form.reset();
        }

    });



    $('#generarReporteEditado').click(function () {
        var form = $("#ReporteCondicionesInseguras");
        if (!$("#ReporteCondicionesInseguras").valid()) {
            validacion = false;
        }
        else {
            validacion = true;
        }
        var fechaOcurrencia = $("#FechaOcurrencia").val();

        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {

            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }

        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaOcurrencia)) {

          

            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'La Fecha de Ocurrencia del evento no puede ser superior a la Fecha Actual',
                confirmButtonColor: '#7E8A97'
            });

            validacion = false;
        }


        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {

            $(fila).find("#nombreActividad").attr("name", "[" + ind + "].nombreActividad");
            $(fila).find("#RespActividad").attr("name", "[" + ind + "].RespActividad");
            $(fila).find("#FecEjecucion").attr("name", "[" + ind + "].FecEjecucion");

        });
        
     if ($("#Procesos").val() != "")
        {
            if ($("#FK_Proceso").val()=="")
            {
               swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'No es permitido seleccionar un Proceso sin en el Subproceso:',
                    confirmButtonColor: '#7E8A97'
               });



                return false;
            }
        }


        if (validacion == false) {
            return false;
        } else {
            form.submit();

     
        }

    });



    $('body').on('focus', ".datepicker", function () {
        $(this).datepicker({
            firstDay: 1,
            format: "dd/mm/yyyy",
            language: 'es',
            autoclose: true,
            changeMonth: true,
            changeYear: true
        });

    });

});



function eliminarActividad(event) {
    $(event).closest('tr').remove();
    addPlanAccion--;

}
function subirArchivos() {
    //  var inputFileImage = document.getElementById("archivoSubir");


    var formData = new FormData();

    var totalFiles = document.getElementById("imagenesCargar").files.length;
    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementById("imagenesCargar").files[i];

        formData.append("archivo", file);
    }


    $.ajax({
        url: urlBase + urlReporte + '/ObtenerArchivo',
        type: 'POST',
        contentType: false,
        data: formData,
        processData: false,
        cache: false
    }).done(function (res) {

        if (res.Mensaje == 'OK') {
            $("#reporte").show();
            swal({
                type: 'success',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        } else if (res.Mensaje == 'ERROR') {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }




    });
}




function eliminarResponsabilidad(element) {
    $(element).closest("div[name = filaActividad]").remove();
    addRespoCont = addRespoCont - 1;
}


function mostrarBoton() {
    $("#imagenesCargar").click();
}


//Funcion para eliminar una Responsabilidad
function eliminarResponsabilidad(element) {
    $(element).closest("div[name = filaActividad]").remove();
    addPlanAccion = addPlanAccion - 1;
}

function mostrarPlan() {

    $("#planAccion").show();

}

function validarReporte() {


    $("#ReporteCondicionesInseguras").validate({

        rules: {
            RazonSocialEmpresa: { required: true },
            nitEmpresa: { required: true },
            //IdReportes: { required: true },
            fechaSistena: { required: true },
            FKSede: { required: true },
            FKTipoReporte: { required: true },
            AreaLugar: { required: true },
            FechaOcurrencia: { required: true },
            CedulaQuienReporta: { required: true },
            NombreQuienReporta: { required: true },
            CargoQuienReporta: { required: true },

            DescripcionReporte: { required: true },
            CausaReporte: { required: true },
            SugerenciasReporte: { required: true }
        },
        messages: {
            RazonSocialEmpresa: {
                required: "La razón social de la empresa es obligatoria"
            },
            nitEmpresa: {
                required: "Debe seleccionar el NIT de la empresa"
            },
            //IdReportes: {
            //    required: ""
            //},
            fechaSistena: {
                required: "La Fecha del sistema es obligatoria"
            },
            FKSede: {
                required: "Por favor seleccione una Sede"
            },
            FKTipoReporte: {
                required: "Por favor seleccione un Tipo de Reporte"
            },
            AreaLugar: {
                required: "Por favor seleccione el Área o Lugar"
            },
            FechaOcurrencia: {
                required: "Por favor seleccione Fecha de Ocurrencia"
            },
            CedulaQuienReporta: {
                required: "Por favor ingrese la Cédula"
            },
            NombreQuienReporta: {
                required: "Por favor ingrese el Nombre de la persona que reporta"

            },
            CargoQuienReporta: {
                required: "Por favor ingrese el Cargo"
            },
            DescripcionReporte: {
                required: "Por favor ingrese la Descripción del reporte"
            },
            CausaReporte: {
                required: "Por favor ingrese la Causa del reporte"
            },
            SugerenciasReporte: {
                required: "Por favor ingrese la Sugerencia del reporte"


            }
        }

    });

}


//funcion que solo permite el ingreso de numero en los campos 
function darFormatoSoloNumeros(idCampo) {
    $('#' + idCampo).on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}


function ObtenerSiarp() {
    utils.showLoading();
    $.ajax({

        url: urlBase + urlReporte + '/ObtenerSiarp',
        data: {                                           // se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Documento: $("#CedulaQuienReporta").val()
        },
        type: 'POST',
    }).done(function (result) {
        var nombreCompleto;
        utils.closeLoading();
        if (result.Mensaje == 'OK') {




            nombreCompleto = result.Data[0].nombre1 + " " + result.Data[0].nombre2 + " " + result.Data[0].apellido1 + " " + result.Data[0].apellido2;
            $("#NombreQuienReporta").val(nombreCompleto);
            $("#NombreQuienReporta").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;

            $("#CargoQuienReporta").val(result.Data[0].ocupacion);
            $("#CargoQuienReporta").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;

        } else if (result.Mensaje == 'ERROR') {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }
        else if (result.Mensaje == 'VACIO') {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }
        else if (result.Mensaje == 'CONEXION') {
           
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }
    }).fail(function (result) {
        console.log("Error en la peticion: " + result.Data);
    });
}

function consultarSubProcesos() {
    var idProcesoPrincipal = $("#Procesos");
    var idSubProceso = $("#FK_Proceso");
    $.ajax({
        url: urlBase + '/Proceso/obtenerSuprocesos',
        data: {
            Pk_ProcesoPrincipal: idProcesoPrincipal.val()
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                idSubProceso.find("option").remove();//Removemos las opciones anteriores 
                idSubProceso.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar           
                $.each(result, function (ind, element) {
                    idSubProceso.append(new Option(element.Descripcion_Proceso, element.Procesoid));//agregamos las opciones consultadas
                })
            }
        }
    });

}

function mostrarImagenes() {
    if ($("#tbl_Imagenes").is(":visible"))
    {
        $("#tbl_Imagenes").hide();
    }
    else
    {
        $("#tbl_Imagenes").show();
    }

}

function mostrarActividadesEdit() {
    if ($("#actividadesEditadas").is(":visible")) {
        $("#actividadesEditadas").hide();
    }
    else {
        $("#actividadesEditadas").show();
    }

}

//funcion que me permite eliminar un imagen delreportr
function EliminarImagenSelec(element, IDImagenesReportes) {
    $div = $(element).closest("tr");
    $.ajax({
        url: urlBase + urlReporte + '/EliminarImagenes',
        data: {
            idImagen: IDImagenesReportes
        },
        type: 'POST',
        success: function (result) {

            if (result.success) {
                $("#modalesEliminados").append($("#modalEliminar" + IDImagenesReportes));
                $div.remove();
               

                swal({
                    type: 'success',
                    title: 'Estimado usuario:',
                    text: 'Se ha eliminado la imagen',

                    confirmButtonColor: '#7E8A97'
                });
            } else {
               

                swal({
                    type: 'warning',
                    title: 'Estimado usuario:',
                    text: 'No ha sido posible eliminar la imagen',

                    confirmButtonColor: '#7E8A97'
                });
            }
        }
    });


}




//contenedor: contenedor donde se encuentran los campos dinamicos a validar ejemplo $("#actividades")
//nameElementoAValidar: nombre(name) de los elementos a validar ejemplo "DescripcionActividad"
//tipoElemento:tipo de elemento a buscar para realizar la validacion por ejemplo "input"
//tipoSelector: es el tipo de selector para jquery un ejemplo es el asterisco(*) es el que encuentra todos elementos que contengan un subestring especificada, 
//sino se especifica un selector se toma el igual como defecto..consultar api de jquery https://api.jquery.com/category/selectors/
//errorClass:es la clase con la que va aparecer la etiqueta que mostrara el mensaje de validación por defecto viene con la clase "error-dinamico".
//errorMensaje: mensaje que se mostrará de la validación el mensaje por defecto es "el campo es obligatorio"
function validarCamposDinamicos(contenedor, nameElementoAValidar, tipoElemento, tipoSelector, errorClass, errorMensaje) {

    if (contenedor != undefined && nameElementoAValidar != undefined) {
        var formulario = $(contenedor);

        var stringElementoBuscar = "";
        if (tipoSelector != undefined) {
            stringElementoBuscar = tipoElemento + "[name" + tipoSelector + "=" + nameElementoAValidar + "]";
        } else {
            stringElementoBuscar = tipoElemento + "[name=" + nameElementoAValidar + "]";
        }

        var valido = true;
        var labelError = "";
        if (errorClass === undefined && errorMensaje === undefined) {
            labelError = '<label class="error-dinamico">El campo es obligatorio</label>';
        } else if (errorClass != undefined && errorMensaje === undefined) {
            labelError = '<label class="' + errorClass + '">El campo es obligatorio</label>';
        } else if (errorClass != undefined && errorMensaje != undefined) {
            labelError = '<label class="' + errorClass + '">' + errorMensaje + '</label>';
        } else if (errorClass === undefined && errorMensaje != undefined) {
            labelError = '<label class="error-dinamico">' + errorMensaje + '</label>';
        }

        formulario.find(stringElementoBuscar).each(function (ind, element) {
            var label = $(element).next("label");
            if ($(element).val().trim() === '') {
                if (label != undefined) {
                    label.remove();
                }
                $(element).after(labelError);
                $(element).attr("onchange", "quitarlabelError(this)");
                valido = false;
            } else {
                label.remove();
            }
        })
        return valido;
    } else {
        return false;
    }


}

function validaciontamañodocumentoRepCIns() {
    if (typeof FileReader !== "undefined") {
        var imageSize = 0;
        var fileInput = document.getElementById("imagenesCargar");

        if (fileInput.multiple == true) {


            var files = fileInput.files;
            var fl = files.length;
            for (var i = 0, len = fl; i < len; i++) {
                // loop fileInput.files


                imageSize =imageSize + files[i].size;
            }
        }
            if (imageSize > 10800332.8) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'La carga de las imágenes no puede ser superior a 10 Mb',
                    //timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
                document.getElementById("imagenesCargar").value = "";
            }
        
    }
}
