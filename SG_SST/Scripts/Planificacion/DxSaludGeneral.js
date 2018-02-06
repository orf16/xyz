var urlBase = "";
var urlAusencias = '/Ausencias';
var IDCLASIFICACIONDEPELIGRO = 46 //ID o Pk de la tabla clasificacion de peligro de la clase de peligro Otro 
var IDOTRO = 8;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de peligro para el peligro de tipo otro
var $idSede = $("#Pk_Id_Sede");
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

$(function () {
    darFormatoSoloNumeros("NumeroTrabajadoresLugar");
    darFormatoSoloNumeros("NumeroTrabajadoresSintomatologia");
    darFormatoSoloNumeros("NumeroTrabajadoresConPrueba");
    darFormatoSoloNumeros("NumeroTrabajadoresConPruebaP");
    darFormatoSoloNumeros("NumeroTrabajadoresConDiagnostico");
});

function BuscarSedeMunicipio() {
    $.ajax({
        url: urlBase + '/PerfilSocioDemoGrafico/BuscarMunicipioPorSede',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Fk_Sede: $idSede.val()
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $("#IdMunicipio_Sede").val(result.Municipio.DescripcionMunicipio);
                $("#IdDepartamento_Sede").val(result.Municipio.DescripcionDepartamento);

            }
        }
    });
}
//funcion que me permite eliminar un documento de diagnostico
function EliminarDocDx(element, idEDDocDxSalud) {
    $div = $(element).closest("tr");
    $.ajax({
        url: urlBase + '/DxGralCondicionSalud/EliminarDocDxSalud',
        data: {
            idDocDx: idEDDocDxSalud
        },
        type: 'POST',
        success: function (result) {

            if (result.success) {
                $("#modalesEliminados").append($("#modalEliminar" + idEDDocDxSalud));
                $div.remove();

                swal(
                    'Estimado usuario',
                    'se ha eliminado el documento del diagn\u00F3stico',
                    'success'
                    )
            } else {
                swal(
                   'Estimado usuario',
                   'no ha sido posible eliminar el documento del diagn\u00F3stico',
                   'warning'
                   )
            }
        }
    });


}

///Funcion que me permite validar que los campos para guardar el documento estén diligenciados
function ValidarFormularioDocDx() {

    $("#FormularioDocDX").validate({
        rules: {
            idSede:
            {
                required: true
            },
            Nombre_Diagnostico:
            {
                required: true
            },
            File:
            {
                required: true
            }

        },
        messages: {
            idSede:
            {
                required: "Se debe seleccionar una sede"
            },
            Nombre_Diagnostico:
            {
                required: "Se debe ingresar un nombre del documento de diagnostico"
            },
            File:
            {
                required: "se debe cargar seleccionar una archivo"
            }
        }

    });

}


// funcion para consultar las clases de peligro por tipo de peligro
function ConsultarClasesPeligrosDx(selectPeligro) {
    var $inputOtro = $(selectPeligro).closest("tr").find("input[name=Otro]");
    var $selectClasesP = $(selectPeligro).closest("tr").find("select[name=FK_Clasificacion_De_Peligro]");
    var $divselectClasesP = $(selectPeligro).closest("tr").find("div[name=divSelectClas]");
    if (IDOTRO == $(selectPeligro).val()) {
        $inputOtro.attr("type", "text");
        $selectClasesP.find("option").remove();//Removemos las opciones anteriores 
        $selectClasesP.append(new Option("Otro", 46));//agregamos la opcion de otro, el numero 46 hace referencia al id en la base de datos de la clasificacion otro
        $divselectClasesP.attr("hidden", "hidden");
    }
    else {
        $inputOtro.attr("type", "hidden");
        $divselectClasesP.removeAttr("hidden");
        $.ajax({
            url: urlBase + '/ClasificacionDePeligros/ConsultarClasesPeligros',
            data: {
                Pk_Tipo_Peligro: $(selectPeligro).val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $selectClasesP.find("option").remove();//Removemos las opciones anteriores 
                    $selectClasesP.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar                

                    $.each(result, function (ind, element) {
                        $selectClasesP.append(new Option(element.ClasesDescription, element.PK_ClasesPeligros));//agregamos las opciones consultadas
                    });


                }
            }
        });
    }
}


$(document).ready(function () {
    var inputCIE = $("#tabladiagnosticoCie").find("input[name=Diagnostico]");
    var inputCIEHiden = $("#tabladiagnosticoCie").find("input[name=IdDiagnostico]");
    convertirCampoCIE10(inputCIE, inputCIEHiden);
})

///funcion para buscar los dianosticos CIE10 por autocomplentar
function convertirCampoCIE10(inputCIE, inputCIEHiden) {

    $(inputCIE).autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: urlBase + urlAusencias + "/AutoCompletarDiagnostico",
                type: "POST",
                dataType: "json",
                data: { prefijo: request.term },
            }).done(function (data) {
                response($.map(data, function (item) {
                    return { label: item.Codigo + "-" + item.Descripcion, value: item.IdDiagnostico };
                }))
            })
        },
        focus: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
        },
        select: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
            $(inputCIEHiden).val(ui.item.value);
        }
    });
}

function CrearDiagnostico() {

    ValidarFormularioDiagnostico();
    var respuestaDinamicaClas = validarCamposDinamicos($("#tablaClasificacionPeligros"), "FK_Clasificacion_De_Peligro", "select");
    var respuestaDinamicaSintDes = validarCamposDinamicos($("#tablaSintomatologia"), "Sintomatologia", "input");
    var respuestaDinamicaSintNum = validarCamposDinamicos($("#tablaSintomatologia"), "NumeroTrabajadoresSintomatologia", "input");
    var respuestaDinamicaSPruebas = validarCamposDinamicos($("#tablaPruebas"), "PruebaClinica", "input");
    var respuestaDinamicaSPruebasNum = validarCamposDinamicos($("#tablaPruebas"), "NumeroTrabajadoresConPrueba", "input");
    var respuestaDinamicaSPruebasp = validarCamposDinamicos($("#tablaPruebasP"), "PruebaPClinica", "input");
    var respuestaDinamicaSPruebaspNum = validarCamposDinamicos($("#tablaPruebasP"), "NumeroTrabajadoresConPruebaP", "input");
    var respuestaDinamicaSCie = validarCamposDinamicos($("#tabladiagnosticoCie"), "Diagnostico", "input");
    var respuestaDinamicaSCieNum = validarCamposDinamicos($("#tabladiagnosticoCie"), "NumeroTrabajadoresConDiagnostico", "input");
    var respuestaFechas = ValidarFechasDX();

    if ($("#crearFormularioDX").valid() && respuestaDinamicaSintDes
        && respuestaDinamicaSintNum && respuestaDinamicaSPruebas && respuestaDinamicaSPruebasNum && respuestaDinamicaSPruebasp && respuestaDinamicaSPruebaspNum
        && respuestaDinamicaSCie && respuestaDinamicaSCieNum && respuestaFechas) {
        EditarNombreFormularioDX();
        $("#crearFormularioDX").submit();
    }
}


function GuardarDiagnostico() {
    var form = $("#crearFormularioDX");
    $.ajax({
        url: urlBase + '/DxGralCondicionSalud/CrearDxGralDeSalud',
        data: form.serialize(),
        type: 'POST',
        success: function (result) {

            if (result.success) {
                var trDx = "<tr>";
                trDx = trDx + "<td>" + $("#Pk_Id_Sede").find("option:selected").text() + "</td>";
                trDx = trDx + "<td>" + $("#IdMunicipio_Sede").val() + "</td>";
                trDx = trDx + "<td>" + $("#IdDepartamento_Sede").val() + "</td>";
                trDx = trDx + "<td>" + result.dx.ZonaLugar + "</td>";
                trDx = trDx + "<td>" + $("#FK_Tipo_De_Peligro").find("option:selected").text() + "</td>";
                if ($("#FK_Clasificacion_De_Peligro").val() == IDCLASIFICACIONDEPELIGRO) {
                    trDx = trDx + "<td>" + $("#Otro").val() + "</td>";
                }
                else {
                    trDx = trDx + "<td>" + $("#FK_Clasificacion_De_Peligro").find("option:selected").text() + "</td>";
                }
                trDx = trDx + "<td>" + result.dx.NumeroTrabajadoresLugar + "</td>";
                trDx = trDx + "<td>" + result.dx.Sintomatologia + "</td>";
                trDx = trDx + "<td>" + result.dx.NumeroTrabajadoresSintomatologia + "</td>";
                trDx = trDx + "<td>" + $("#porcentajeSintomatologia").val() + "</td>";
                trDx = trDx + "<td>" + result.dx.PruebaClinica + "</td>";
                trDx = trDx + "<td>" + result.dx.NumeroTrabajadoresConPrueba + "</td>";
                trDx = trDx + "<td>" + $("#porcentajeSintomatologiaPruebaClinica").val() + "</td>";
                trDx = trDx + "<td>" + result.dx.PruebaPClinica + "</td>";
                trDx = trDx + "<td>" + result.dx.NumeroTrabajadoresConPruebaP + "</td>";
                trDx = trDx + "<td>" + $("#porcentajeSintomatologiaPruebaPClinica").val() + "</td>";
                trDx = trDx + "<td>" + $("#Diagnostico").val() + "</td>";
                trDx = trDx + "<td>" + result.dx.NumeroTrabajadoresConDiagnostico + "</td>";
                trDx = trDx + "<td>" + $("#porcentajeTrabajadoresConDiagnostico").val() + "</td>";
                trDx = trDx + "</tr>"
                $("#tableDx").append(trDx);
                swal(
                    'Estimado usuario',
                    'se ha guardado el diagn\u00F3stico general de salud',
                    'success'
                    )
                form[0].reset();
            } else {
                swal(
                   'Estimado usuario',
                   'no ha sido posible grabar el diagn\u00F3stico general de salud',
                   'error'
                   )
            }
        }
    });
}

function EditarNombreFormularioDX() {
    //cambiar los nombres de los campos de la tabla sintomatologia para que puedan ser recibidos como lista
    $("#tablaSintomatologia").find("input[name=Sintomatologia]").each(function (ind, element) {
        $(element).attr("name", "EDSintomatologiaDx[" + ind + "].Sintomatologia");
    });
    $("#tablaSintomatologia").find("input[name=NumeroTrabajadoresSintomatologia]").each(function (ind, element) {
        $(element).attr("name", "EDSintomatologiaDx[" + ind + "].Trabajadores_Sintomatologia");
    });

    //cambiar los nombres de los campos de la tabla pruebas clinicas para que puedan ser recibidos como lista
    $("#tablaPruebas").find("input[name=PruebaClinica]").each(function (ind, element) {
        $(element).attr("name", "EDPruebasClinicasDx[" + ind + "].Prueba_Clinica");
    });
    $("#tablaPruebas").find("input[name=NumeroTrabajadoresConPrueba]").each(function (ind, element) {
        $(element).attr("name", "EDPruebasClinicasDx[" + ind + "].Trabajadores_Con_Prueba");
    });
    //cambiar los nombres de los campos de la tabla pruebas paraclinicas para que puedan ser recibidos como lista
    $("#tablaPruebasP").find("input[name=PruebaPClinica]").each(function (ind, element) {
        $(element).attr("name", "EDPruebasPClinicasDx[" + ind + "].Prueba_P_Clinica");
    });
    $("#tablaPruebasP").find("input[name=NumeroTrabajadoresConPruebaP]").each(function (ind, element) {
        $(element).attr("name", "EDPruebasPClinicasDx[" + ind + "].Trabajadores_Con_Prueba_P");
    });
    //cambiar los nombres  de los campos de la tabla diagnostico  cie10
    $("#tabladiagnosticoCie").find("tr").each(function (ind, element) {
        $(element).find("input[name=IdDiagnostico]").attr("name", "EDDiagnosticoCie10Dx[" + ind + "].IdDiagnostico");
        $(element).find("input[name=NumeroTrabajadoresConDiagnostico]").attr("name", "EDDiagnosticoCie10Dx[" + ind + "].NumeroTrabajadoresConDiagnostico");
    });

    //cambiar los nombres  de los campos de la tabla Clasificacion peligros
    $("#tablaClasificacionPeligros").find("tr[name=ClasPeligroDx]").each(function (ind, element) {
        $(element).find("select[name=FK_Clasificacion_De_Peligro]").attr("name", "EDClasificacionPeligroDx[" + ind + "].idClasifiacionPeligro");

    });

}

function calcularPorcentajes() {
    porcentajesSintomatologia();
    porcentajesSintomatologiaPruebaClinica();
    porcentajesSintomatologiaPruebaPClinica();
    porcentajesTrabajadoresConDiagnostico();
}

function porcentajesSintomatologia(element) {
    var numeroTrabajadoresLugar = $("#NumeroTrabajadoresLugar").val();
    var numeroTrabajadoresSintomatologia = $(element).val()

    if (numeroTrabajadoresLugar != "" && numeroTrabajadoresSintomatologia != "" && numeroTrabajadoresLugar != 0) {
        porcentaje = (numeroTrabajadoresSintomatologia / numeroTrabajadoresLugar * 100).toFixed(2);
    } else {
        porcentaje = "";
    }
    $(element).closest("tr").find("input[name=porcentajeSintomatologia]").val(porcentaje);

    validarNumeroTrabajadores(element);
}

function porcentajesSintomatologiaPruebaClinica(element) {
    var numeroTrabajadoresLugar = $("#NumeroTrabajadoresLugar").val();
    var numeroTrabajadoresSintomatologia = $(element).val()

    if (numeroTrabajadoresLugar != "" && numeroTrabajadoresSintomatologia != "" && numeroTrabajadoresLugar != 0) {
        porcentaje = (numeroTrabajadoresSintomatologia / numeroTrabajadoresLugar * 100).toFixed(2);
    } else {
        porcentaje = "";
    }
    $(element).closest("tr").find("input[name=porcentajeSintomatologiaPruebaClinica]").val(porcentaje);
    validarNumeroTrabajadores(element);
}

function porcentajesSintomatologiaPruebaPClinica(element) {
    var numeroTrabajadoresLugar = $("#NumeroTrabajadoresLugar").val();
    var numeroTrabajadoresSintomatologia = $(element).val()

    if (numeroTrabajadoresLugar != "" && numeroTrabajadoresSintomatologia != "" && numeroTrabajadoresLugar != 0) {
        porcentaje = (numeroTrabajadoresSintomatologia / numeroTrabajadoresLugar * 100).toFixed(2);
    } else {
        porcentaje = "";
    }
    $(element).closest("tr").find("input[name=porcentajeSintomatologiaPruebaPClinica]").val(porcentaje);
    validarNumeroTrabajadores(element);
}

function porcentajesTrabajadoresConDiagnostico(element) {
    var numeroTrabajadoresLugar = $("#NumeroTrabajadoresLugar").val();
    var numeroTrabajadoresSintomatologia = $(element).val()

    if (numeroTrabajadoresLugar != "" && numeroTrabajadoresSintomatologia != "" && numeroTrabajadoresLugar != 0) {
        porcentaje = (numeroTrabajadoresSintomatologia / numeroTrabajadoresLugar * 100).toFixed(2);
    } else {
        porcentaje = "";
    }
    $(element).closest("tr").find("input[name=porcentajeTrabajadoresConDiagnostico]").val(porcentaje);
    validarNumeroTrabajadores(element);
}


function ValidarFormularioDiagnostico() {
    jQuery.validator.addMethod("Numberonly", function (value, element) {
        return this.optional(element) || /^[0-9]+$/i.test(value);
    }, "Solo se permite el ingreso de numeros");

    $("#crearFormularioDX").validate({
        rules: {
            Fecha_Inicial_Dx:
           {
               required: true
           },
            Fecha_Final_Dx:
           {
               required: true
           },
            vigencia:
            {
                required: true
            },
            Pk_Id_Sede:
            {
                required: true
            },
            ZonaLugar:
            {
                required: true
            },
            Responsable_informacion:
            {
                required: true
            },
            Profesion_Responsable:
            {
                required: true
            },
            Tarjeta_Profesional:
            {
                required: true
            },
            Fk_Id_Municipio:
            {
                required: true
            },
            DepartamentoSede:
            {
                required: true
            },
            //NumeroTrabajadoresLugar:
            //{
            //    required: true,
            //    Numberonly: true,
            //    min: 1,
            //    maxlength: 10,
            //},
            //Sintomatologia:
            //{
            //    required: true
            //},
            //NumeroTrabajadoresSintomatologia:
            //{
            //    required: true,
            //    Numberonly: true,
            //    maxlength: 10,
            //},
            //PruebaClinica:
            //{
            //    required: true
            //},
            //NumeroTrabajadoresConPrueba:
            //{
            //    required: true,
            //    Numberonly: true,
            //    maxlength: 10,
            //},
            //PruebaPClinica:
            //{
            //    required: true
            //},
            //NumeroTrabajadoresConPruebaP:
            //{
            //    required: true,
            //    Numberonly: true,
            //    maxlength: 10,
            //},
            //Diagnostico:
            //{
            //    required: true
            //},
            //NumeroTrabajadoresConDiagnostico:
            //{
            //    required: true,
            //    Numberonly: true,
            //    maxlength: 10,
            //}



        },
        messages: {
            Fecha_Inicial_Dx:
            {
                required: "Se debe seleccionar una fecha inicial"
            },
            Fecha_Final_Dx:
            {
                required: "Se debe seleccionar una fecha final"
            },
            vigencia:
            {
                required: "Se debe seleccionar una vigencia"
            },
            Pk_Id_Sede:
            {
                required: "Se debe seleccionar una sede"
            },
            ZonaLugar:
            {
                required: "Se debe seleccionar una zona o lugar"
            },
            Responsable_informacion:
            {
                required: "Se debe ingresar un responsable"
            },
            Profesion_Responsable:
            {
                required: "Se debe ingresar una profesión"
            },
            Tarjeta_Profesional:
           {
               required: "Se debe ingresar una tarjeta profesional"
           },
            Fk_Id_Municipio:
            {
                required: "se debe seleccionar un municipio"
            },
             DepartamentoSede:
             {
                 required: "Se debe seleccionar un departamento"
             },
            //NumeroTrabajadoresLugar:
            //{
            //    required: "se debe ingresar el número de trabajadores de la zona o lugar",
            //    min: "el numero de trabajadores debe ser mayor  a cero(0)",
            //    maxlength: "solo se permite el ingreso de números de máximo 10 caraceres"
            //},
            //Sintomatologia:
            //{
            //    required: "se de ingresar una Sintomatología"
            //},
            //NumeroTrabajadoresSintomatologia:
            //{
            //    required: "se debe ingresar el número de trabajadores con la sintomatología",
            //    maxlength: "solo se permite el ingreso de números de máximo 10 caraceres"
            //},
            //PruebaClinica:
            //{
            //    required: "se debe ingresar la prueba clínica"
            //},
            //NumeroTrabajadoresConPrueba:
            //{
            //    required: "se debe ingresar el número de trabajadores con prueba clínica anormal",
            //    maxlength: "solo se permite el ingreso de números de máximo 10 caraceres"
            //},
            //PruebaPClinica:
            //{
            //    required: "se debe ingresar la prueba P_clínica"
            //},
            //NumeroTrabajadoresConPruebaP:
            //{
            //    required: "se debe ingresar el número de trabajadores con prueba P_clínica anormal",
            //    maxlength: "solo se permite el ingreso de números de máximo 10 caraceres"
            //},
            //Diagnostico:
            //{
            //    required: "se debe ingresar el diagnóstico CIE10"
            //},
            //NumeroTrabajadoresConDiagnostico:
            //{
            //    required: "se debe ingresar el número de trabajadores con el diagnóstico",
            //    maxlength: "solo se permite el ingreso de números de máximo 10 caraceres"
            //}

        }

    });

}

//funcion que solo permite el ingreso de numero en los campos 
function darFormatoSoloNumeros(idCampo) {
    $('#' + idCampo).on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}

//funcion para agregar otra sintomatología 
function agregarFilaSintomatologia(element) {

    var trDx = "<tr>";
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="text" id="Sintomatologia" name="Sintomatologia" class="form-control" /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input id="NumeroTrabajadoresSintomatologia" name="NumeroTrabajadoresSintomatologia" class="form-control" onkeyup="porcentajesSintomatologia(this)" /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="text" id="porcentajeSintomatologia" name="porcentajeSintomatologia" class="form-control" readonly /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a  onclick="removerFila(this)" class="btn btn-search btn-md"><span class="glyphicon glyphicon-erase"></span></a></th>';
    trDx = trDx + '</tr>';

    $(element).closest("tbody").append(trDx);//se agrega la fila al cuerpo de  la tabla

}

//funcion para agregar otra prueba clinica
function agregarFilaPruebasClinicas(element) {

    var trDx = "<tr>";
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="text" id="PruebaClinica" name="PruebaClinica" class="form-control" /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input id="NumeroTrabajadoresConPrueba" name="NumeroTrabajadoresConPrueba" class="form-control" onkeyup="porcentajesSintomatologiaPruebaClinica(this)" /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="text" id="porcentajeSintomatologiaPruebaClinica" name="porcentajeSintomatologiaPruebaClinica" class=" form-control" readonly /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a  onclick="removerFila(this)" class="btn btn-search btn-md"><span class="glyphicon glyphicon-erase"></span></a></th>';
    trDx = trDx + '</tr>';

    $(element).closest("tbody").append(trDx);//se agrega la fila al cuerpo de  la tabla

}

//funcion para agregar otra prueba paraclinica
function agregarFilaPruebasParaClinicas(element) {

    var trDx = "<tr>";
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="text" id="PruebaPClinica" name="PruebaPClinica" class="form-control" /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input id="NumeroTrabajadoresConPruebaP" name="NumeroTrabajadoresConPruebaP" class="form-control" onkeyup="porcentajesSintomatologiaPruebaPClinica(this)" /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="text" id="porcentajeSintomatologiaPruebaPClinica" name="porcentajeSintomatologiaPruebaPClinica" class=" form-control" readonly /></th>';
    trDx = trDx + '<th style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a  onclick="removerFila(this)" class="btn btn-search btn-md"><span class="glyphicon glyphicon-erase"></span></a></th>';
    trDx = trDx + '</tr>';

    $(element).closest("tbody").append(trDx);//se agrega la fila al cuerpo de  la tabla

}

//funcion para agregar otro DiagnosticoCIE10
function agregarFilaDiagnosticoCIE10(element) {
    var tr = $("#trCie10").clone();
    $("#trCie10").find("input[name=Diagnostico]").val("");
    $("#trCie10").find("input[name=NumeroTrabajadoresConDiagnostico]").val("");
    $("#trCie10").find("input[name=porcentajeTrabajadoresConDiagnostico]").val("");
    $(tr).find("input[name=Diagnostico]").attr("readonly", "readonly");
    $(tr).find("input[name=NumeroTrabajadoresConDiagnostico]").attr("readonly", "readonly");
    $(tr).find("a").attr("onclick", "removerFila(this)");
    $(tr).find("span").attr("class", "glyphicon glyphicon-erase");
    $(tr).attr("name", "trCie1010");
    $(element).closest("tbody").append(tr);//se agrega la fila al cuerpo de  la tabla

}

//funcion para agregar otro DiagnosticoCIE10
function agregarFilaClasifiacion(element) {
    var tr = $("#ClasPeligroDx").clone();
    $(tr).find("a").attr("onclick", "removerFila(this)");
    $(tr).find("span").attr("class", "glyphicon glyphicon-erase");
    $(tr).find("select[name=FK_Clasificacion_De_Peligro]").find("option").remove();//Removemos las opciones anteriores 
    $(tr).find("select[name=FK_Clasificacion_De_Peligro]").append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar
    $(element).closest("tbody").append(tr);//se agrega la fila al cuerpo de  la tabla

}

//Funcion para removar una  fila de la sintomatologia o pruebas clinicas o prebas paraclinicas o de diaganotico CIE
function removerFila(element) {
    $(element).closest("tr").remove();
}

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


function quitarlabelError(element) {
    var label = $(element).next("label");
    if ($(element).val().trim() === '') {
        if (label != undefined) {
            label.remove();
        }
    } else {
        label.remove();
    }

}

//funcion que me permite eliminar de diagnostico
function EliminarDx(element, idDxSalud) {
    $div = $(element).closest("tr");
    $.ajax({
        url: urlBase + '/DxGralCondicionSalud/EliminarDxSalud',
        data: {
            idDx: idDxSalud
        },
        type: 'POST',
        success: function (result) {

            if (result.success) {
                $("#modalesEliminados").append($("#modalEliminar" + idDxSalud));
                $div.remove();

                swal(
                    'Estimado usuario',
                    'se ha eliminado el  diagn\u00F3stico',
                    'success'
                    )
            } else {
                swal(
                   'Estimado usuario',
                   'no ha sido posible eliminar el diagn\u00F3stico',
                   'warning'
                   )
            }
        }
    });


}

//funcion que me permite eliminar de diagnostico
function BuscarDx() {
   
    $.ajax({
        url: urlBase + '/DxGralCondicionSalud/BuscarHistoricoDxPorSedes',
        data: {
            strZonaLugar: $("#Pk_Id_Sede").val()
        },
        type: 'POST',
        success: function (result) {

            $("#tablaHistoricoDx").html(result);
        }
    });
}

function ValidarFechasDX() {
    var fechainicio = $("#Fecha_Inicial_Dx").val();
    var fechafinal = $("#Fecha_Final_Dx").val();
    var hoy = new Date();
    dia = hoy.getDate();
    mes = hoy.getMonth() + 1;
    anio = hoy.getFullYear();

    if (mes < 10) {
        fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
    } else {

        fecha_actual = String(dia + "/" + mes + "/" + anio);
    }

    if ($.datepicker.parseDate('dd/mm/yy', fechafinal) < $.datepicker.parseDate('dd/mm/yy', fechainicio)) {
        swal("Estimado Usuario", 'La Fecha Final,  no puede ser inferior a la Fecha Inicial', "warning");
        return false;
    }
    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechafinal)) {
        swal("Estimado Usuario", 'La Fecha Final,  no puede ser superior a la Fecha Actual', "warning");
        return false;
    }
    return true;
}

function validarNumeroTrabajadores(Element)
{
    var numeroTrabajadoresTotal =parseInt($("#NumeroTrabajadoresLugar").val());
    var numerotrabajadoresPorLinea = parseInt($(Element).val());
    if (numeroTrabajadoresTotal < numerotrabajadoresPorLinea)
    {
        swal("Estimado Usuario", 'El número de trabajadores con anormalidad no puede ser superior al  total de trabajadores  de la zona o lugar', "error");
        $(Element).val("");
        $(Element).closest("tr").find("input").val("");
    }

}


$('#idUpPlantillaDx').click(function () {

    var form_data = new FormData();
    var filedata = $("#file").prop("files")[0];
    if (filedata != undefined)
        form_data.append("cargarArchivo", filedata);
    else {



        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'Debe seleccionar un archivo',

            confirmButtonColor: '#7E8A97'
        });
        return
    }
    PopupPosition();

    $.ajax({
        type: "POST",
        data: form_data,
        url: urlBase + '/DxGralCondicionSalud/CargueMasivo',
        processData: false,
        dataType: 'json',
        contentType: false
    }).done(function (response) {
        if (response != undefined && response.Mensaje == 'Success') {
            swal({
                type: 'success',
                title: 'Estimado usuario:',
                text: response.Data,

                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
            $("#listadoCargue").show();
        }
        else if (response != undefined && response.Mensaje == 'ERROR') {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: response.Data,

                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else if (response != undefined && response.Mensaje == 'CONEXION') {
            swal("Estimado Usuario",
                  response.Data,
                  'warning');


            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: response.Data,

                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
    }).fail(function (response) {
        $("#Documento").val('');
        swal("Estimado Usuario",
            "No se logró obtener los datos. Intente más tarde.",
            'warning');
        OcultarPopupposition();
    });
});