
var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

var asistEliminado = new Array();
var addAsistente = 0;
var addcondicion = 0;
var IDOTRO = 8;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de peligro para el peligro de tipo otro
var $formularioMetodologia = $("#planaccion");

$(function () {
    $("#div_main").hide();
    $(".accordion-titulo").click(function () {
        var contenido = $(this).next(".accordion-content");
        if (contenido.css("display") == "none") { //open
            contenido.slideDown(250);
            $(this).addClass("open");
        }
        else { //close
            contenido.slideUp(250);
            $(this).removeClass("open");
        }
    });

});
$(function () {
    $("#planaccion").css("display", "none");
    $("#resultadoverificar").hide();
    $("#resultadoinspecciones").hide();
    $("#nuevaInspeccion").css("display", "none");
    $("#nuevainspeccion").css("display", "none");


});



//funcion para agregar  Asistentes a una Inspeccion de Seguridad
function agregarasistentes() {
    addAsistente = addAsistente + 1;
    var $nuevoasistente = $("#divv").clone();
    $nuevoasistente.removeAttr("id");
    $nuevoasistente.find("input[name = asistente]").val("").css("background-color", "White").css("color", "black");
    $("#divasistentes").append($nuevoasistente);
    $("#divasistentes").find("input[id = asistente]").attr("id", "asistente" + addAsistente)
    $("#divasistentes").find("a[name = agregarasistente]").find("i[name=icono]").attr("class", "glyphicon glyphicon-remove")
    $("#divasistentes").find("a[name = agregarasistente]").attr("title", "Eliminar")
    $("#divasistentes").find("a[name = agregarasistente]").attr("onclick", "eliminarasistentes(this)")
}

//renderiza en un input tipo text un datapicker para
//la selección de fechas 
$(function () {

    ConstruirDatePickerPorElemento('FechaInicial');
    ConstruirDatePickerPorElemento('FechaFinal');
    ConstruirDatePickerPorElemento('FechaFinVer');
    ConstruirDatePickerPorElemento('FechaIniVer');
    ConstruirDatePickerPorElemento('FechaInicialB');
    ConstruirDatePickerPorElemento('FechaR');
});

//Funcion para eliminar un Asistente de una Inspeccion de Seguridad
function eliminarasistentes(element) {
    $(element).closest("div[name = divv]").remove();
    addAsistente = addAsistente - 1;
}

//Funcion para cargar formulario de AgregarCondicionInsegura
function agregarcondicion() {
    addcondicion = addcondicion + 1;
    var $nuevacondicion = $("#divc").clone();
    $nuevacondicion.removeAttr("id");
    $nuevacondicion.find("input[name = condicion]").val("").css("background-color", "White").css("color", "black");
    $("#condicioninsegura").append($nuevacondicion);
    $("#condicioninsegura").find("input[id = condicion]").attr("id", "condicion" + addcondicion)
    $("#condicioninsegura").find("a[name = agregarcondicion]").find("i[name=icono]").attr("class", "glyphicon glyphicon-remove btn btn-default")
    $("#condicioninsegura").find("a[name = agregarcondicion]").attr("title", "Eliminar Formulario Condicion")
    $("#condicioninsegura").find("a[name = agregarcondicion]").attr("onclick", "eliminarcondiciones(this)")
}

//Funcion para eliminar borrar formulario de Agregar Condicion
function eliminarcondiciones(element) {
    $(element).closest("div[name = divc]").remove();
    addcondicion = addcondicion - 1;
}


//Funcion para mostrar el listado de las planeaciones de Inspecciones Sin Ejecutar.
$("#btnnoejecutados").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: urlBase + '/PlandeInspeccion/ListaNoEjecutados',  //controlador/metodo que esta en el controlador
        data: {},
        type: 'POST',
        success: function (result) {
            if (result) {
                $('#sinejecutar').empty();
                $('#sinejecutar').append
                //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Seleccionar</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Tipo Inspección</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Responsable</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Fecha Planeación</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Estado</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr></table>');
                var contador = 0;
                $.each(result.Data, function (ind, element) {
                    var Fecha = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                    var elementos = ('<tr name="pse" style="border:solid 1px #808080">' +
                                    //'<td style="border:solid 1px #808080 vertical-align:middle; text-align:center">' + '<input type="radio" name="sinejecutar" id=' + contador + '  class ="sinejecutar" >' + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="EDConsecutivoPlaneacion" id="consecutivo' + contador + '" value="' + element.EDConsecutivoPlaneacion + '">' + element.EDConsecutivoPlaneacion + '</td>' +
                                    //'<td style="border:solid 1px #808080 ; vertical-align:middle; text-align:center"><input type="hidden" name="Idplaninspeccion" id="Idplaninspeccion' + contador + '"value="' + element.EDfkIdPlaneacionInspeccion + '"><input type="hidden" name="EDDescripcionTipoI" id = "tipoinspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="Idinspeccion" id="Idinspeccion' + contador + '"value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDDescripcionTipoI" id = "tipoinspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="EDResponsablePlaneacion" id="responsable' + contador + '"value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="Fecha"  id="Fecha' + contador + '" value="' + Fecha + '">' + Fecha + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="EDEStadoPlaneacion"' + contador + ' value="' + element.EDEStadoPlaneacion + '">' + element.EDEStadoPlaneacion + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a id="btnejecutar" title="Continuar Ejecución" onclick="btncrearnoejecutado(' + element.EDpkinspeccion + ', ' + element.EDfkIdPlaneacionInspeccion + ')" class="btn btn-search btn-md" title="Ejecutar"><span class="glyphicon glyphicon-check"></span></a></td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a id="BotonBorrar" title="Eliminar Registro" onclick="BorrarSinejecutar(' + element.EDpkinspeccion + ', ' + element.EDfkIdPlaneacionInspeccion + ')" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>' +
                                    '</tr>')
                    $('#sinejecutar').append(elementos)
                    contador = contador + 1
                    if (ind == 0) {
                        var radio = $('input:radio[name=sinejecutar]').prop('checked', true);
                    };
                })
                paginador("#sinejecutar", "tr[name = pse]", "#paginador4")
            }
        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error. Intente mas tarde',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
    });
});


//Funcion para eliminar una Planeacion Inspeccion Sin Ejecutar
function BorrarSinejecutar(EDpkinspeccion, EDfkIdPlaneacionInspeccion) {
    PopupPosition();
    if (EDpkinspeccion != null & EDfkIdPlaneacionInspeccion != null) {
        $.ajax({
            url: urlBase + '/PlandeInspeccion/EliminarInspeccion',
            data: {
                IdInspeccion: EDpkinspeccion,
                IdPlaneacion: EDfkIdPlaneacionInspeccion,
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result) {
                    $("#modalnoejecutados").modal('hide');

                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Planeacion Inspección Eliminada',
                        showConfirmButton: true,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error. Intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
    else {
        $.ajax({
            url: urlBase + '/PlandeInspeccion/EliminarPlaneaciones',
            data: {
                IdPlaneacion: EDfkIdPlaneacionInspeccion,
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result) {
                    $("#modalnoejecutados").modal("hide");
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Planeacion Inspección Eliminada',
                        showConfirmButton: true,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#7E8A97'
                    });
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error. Intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }

        });
    }



}
///Funcion para Enviar a la Vista la informacion del plan de inspeccion seleccionado
///que no se ha ejecutado.

$("#regresar").hide();
$("#continuar").hide();
$("#FechaPI").hide();
$("#Responsable").hide();
$("#DescripcionTipoInspecciona").hide();
$("#generarcondicioni").hide();

function btncrearnoejecutado(EDpkinspeccion, EDfkIdPlaneacionInspeccion) {
    PopupPosition();
    if (EDpkinspeccion != null & EDfkIdPlaneacionInspeccion != null) {
        var Planse =
     {
         Idinspeccion: EDpkinspeccion,
         Idplaninspeccion: EDfkIdPlaneacionInspeccion,
     };
        $.ajax({
            url: urlBase + '/PlandeInspeccion/ObtenerInspeccionNoejecutada',  //controlador/metodo que esta en el controlador
            data: Planse,
            type: 'POST',
            success: function (result) {
                if (result) {
                    $("#panelcondiciones").show();
                    $('#asistentes').empty();
                    $('#asistentes').append
                    // ("<table class='table table-condensed table-hover'><tr class='titulos_tabla'><td><b>Nombre Asistente</b></td></tr></table>");
                    var contadora = 0;
                    $.each(result.Data.Asistentes, function (ind, element) {

                        var elementos = '<tr name="asisten">' +
                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="idasistente" id="idasistente' + contador + '"value="' + element.NombreAsistente + '"><input type="hidden" name="NombreAsistente" id = "NombreAsistente' + contadora + '" value="' + element.NombreAsistente + '">' + element.NombreAsistente + '</td>' +

                                        '</tr>'
                        $('#asistentes').append(elementos)
                        contadora = contadora + 1

                    })
                    paginador("#asistentes", "tr[name = asisten]", "#paginador6")

                    $('#configuraciones').empty();
                    $('#configuraciones').append
                    //('<table class="table table-condensed table-hover"><tr class="titulos_tabla"><td><b>Prioridad</b></td><td><b>Dias Desde</b></td><td><b>Dias Hasta</b></td></tr></table>');
                    var contadorc = 0;
                    $.each(result.Data.Configuraciones, function (ind, element) {
                        var elementosc = '<tr>' +
                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="idconfiguracion" id="idconfiguracion' + contadorc + '"value="' + element.NombreAsistente + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contador + '" value="' + element.Descripcion + '">' + element.Descripcion + '</td>' +
                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="diasdesde" id="diasdesde' + contadorc + '"value="' + element.diasdesde + '">' + element.diasdesde + '</td>' +
                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="diashasta" id="diashasta' + contadorc + '"value="' + element.diashasta + '">' + element.diashasta + '</td>' +
                                        '</tr>'
                        $('#configuraciones').append(elementosc)
                        contadorc = contadorc + 1
                    });
                    if (result.Data.EDElementos != "") {
                        $('#elementos').empty();
                        $('#elementos').append
                        var contadore = 0;
                        $.each(result.Data.EDElementos, function (ind, element) {
                            var elementosce = '<tr name="elementos">' +
                                            '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="Pk_Id_AdmoEMH" id="Pk_Id_AdmoEMH' + contador + '"value="' + element.TipoElemento + '"><input type="hidden" name="TipoElemento" id = "TipoElemento' + contador + '" value="' + element.TipoElemento + '">' + element.TipoElemento + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="NombreElemento" id="NombreElemento' + contador + '"value="' + element.NombreElemento + '">' + element.NombreElemento + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="Marca" id="Marca' + contador + '"value="' + element.Marca + '">' + element.Marca + '</td>' +
                                            '</tr>'
                            $('#elementos').append(elementosce)
                            contadore = contadore + 1
                        })
                        paginador("#elementos", "tr[name = elementos]", "#paginador7")
                    }
                    else {
                        $("#panelelementos").hide();

                    }

                    $('#condicionesins').empty();
                    $('#condicionesins').append;
                    var contadorcs = 0;
                    //('<table class="table table-condensed table-hover"><tr class="titulos_tabla"><td><b>Prioridad</b></td><td><b>Dias Desde</b></td><td><b>Dias Hasta</b></td></tr></table>');
                    $.each(result.Data.CondicionesIns, function (ind, element) {
                        var elementoscs = '<tr name="condiciones" id="contador">' +
                                                        '<td style="border:solid 2px lightslategray;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="condicionesIns" name1="' + element.EDDiasDesde + '" name2="' + element.EDDiasHasta + '" desc="' + element.EDDescribeCondicion + '" value="' + element.EDpkCondicion + '" id=' + contadorcs + '  class ="condicionesIns">' + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDescribePrioridad" id="EDescribePrioridad' + contadorcs + '"value="' + element.EDescribePrioridad + '">' + element.EDescribePrioridad + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasDesde" id="EDDiasDesde' + contadorcs + '"value="' + element.EDDiasDesde + '">' + element.EDDiasDesde + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + element.EDDiasHasta + '">' + element.EDDiasHasta + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                                        '<td style="vertical-align:middle;border:solid 2px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
                                                        '<td style="border:solid 2px lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-search btn-md">' + element.EDEvidenciacondicion + '<span class="glyphicon glyphicon-search"></span></a></td>' +
                                                        '<td style="border:solid 2px lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + element.EDpkCondicion + ',' + (result.Data.EDpkinspeccion) + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr>'
                        $('#condicionesins').append(elementoscs)
                        contadorcs = contadorcs + 1
                    });
                    paginador("#condicionesins", "tr[name = condiciones]", "#paginador9")
                    $("#formtipoI").hide();
                    $("#modalnoejecutados").modal("hide");
                    $("#generarcondicioni").show("toogle");
                    $("#Consecutivoplan").val(result.Data.EDConsecutivoPlaneacion);
                    $("#IdInspeccion").val(result.Data.EDpkinspeccion);
                    $("#DescripcionTipoInspecciona").val(result.Data.EDDescripcionTipoI);
                    $("#Responsable").val(result.Data.EDResponsablePlaneacion);
                    $("#FechaPI").val(result.Data.EDFechaPlaneacion);
                    $("#DescribeTipoInspeccion").val(result.Data.EDDescribeinspeccion);
                    $("#Sede").val(result.Data.EDdescribesede);
                    $("#Hora").val(result.Data.EDhora);
                    var fechaConvertida = moment(result.Data.EDFechaRealizacion).format("DD/MM/YYYY");
                    $('#FechaR').text(fechaConvertida);
                    $("#FechaR").val(fechaConvertida);
                    $("#Proceso").val(result.Data.EDdescribeProceso);
                    $("#Arealugar").val(result.Data.EDarealugar);
                    $("#Reslugar").val(result.Data.EDresponsableLugar);
                    $("#nombreempresa").val(result.Data.EDDescribeEmpresa);
                    $("#nit").val(result.Data.EDNitEmpresa);
                    $("#foraneaPlan").val(Planse.Idplaninspeccion);
                    $("#creanoejecutado").remove();
                    $("#fechapi").remove();
                    $("#responsable").remove();
                    $("#DescripcionTipoInspeccion").remove();
                    $("#continuar").show("toogle");
                    $("#FechaPI").show("toogle");
                    $("#DescripcionTipoInspecciona").show("toogle");
                    $("#Responsable").show("toogle");
                    $("#btnnoejecutados").remove();
                    $("#regresar").show("toogle");
                }
                OcultarPopupposition();
            }

        });

    }
    else {
        PopupPosition();
        $.ajax({
            url: urlBase + '/PlandeInspeccion/ContinuarEjecucionPlan',
            data: {
                IdPlaneacion: EDfkIdPlaneacionInspeccion,
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result) {
                    $("#panelcondiciones").show();
                    $("#modalnoejecutados").hide();
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Información Planeación de Inspección Recuperada Satisfactoriamente.',
                        showConfirmButton: true,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#7E8A97'
                    });
                    $('.confirm').on('click', function () {
                        IdPlaneacion = EDfkIdPlaneacionInspeccion,
                        window.location.href = '../PlandeInspeccion/ContinuarEjecucionPlan?IdPlaneacion=' + IdPlaneacion;

                        OcultarPopupposition();
                    });
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error. Intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}
$("#planas").hide();
//Funcion para Generar un plan de Accion a las condiciones inseguras de una Inspección.
function PlanAccion() {
    validarcheckcondiciones();
    PopupPosition();
    var Condiciones = $('#condicion tbody').find('tr');
    var CondicionesI = new Array();
    var idTr = 0;
    $('#condicion tbody').find('tr').each(function () {
        var row = $(this);
        var check = row.find('input[type="checkbox"]');
        if (check.is(':checked')) {
            var diadesdep = parseInt(check.attr('name1'));
            var diahastap = parseInt(check.attr('name2'));
            var descripcion = check.attr('desc');
            var clave = check.attr('value');
            var condicion = new Object();
            condicion.Diasdesde = diadesdep,
            condicion.Diashasta = diahastap,
            condicion.DescribeCondicionvm = descripcion;
            condicion.pkcondicionvm = clave,
            CondicionesI.push(condicion)
        }
    });
    var condiciones = {
        Condiciones: CondicionesI,
    }
    if (CondicionesI.length > 0) {
        $.ajax({
            url: urlBase + '/PlandeInspeccion/PlanAccion',  //controlador/metodo que esta en el controlador
            data: condiciones,
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result) {
                    $("#infoinspeccion").slideUp();
                    $("#infoinspeccions").slideUp();
                    $("#panelascf").slideUp();
                    $("#planaccion").show();
                    $("#planas").show();
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Fecha final plan de acción generada, se obtiene sumando la fecha de la Planeación de Inspección más la mayor prioridad en dias de las condiciones inseguras seleccionadas.',
                        confirmButtonColor: '#7E8A97',
                        showConfirmButton: false,
                        timer: 7000,
                    });
                    
                    //("#fechafin").blur();
                    var fechaConvertida = moment(result.Data).format("DD/MM/YYYY");
                    $('#fechafin').text(fechaConvertida);
                    $("#fechafin").val(fechaConvertida);
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presento un error, intente nuevamente',
                    confirmButtonColor: '#7E8A97'
                });
            }
        });
    }
    else {
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Debe seleccionar mínimo una Condición Insegura',
            confirmButtonColor: '#7E8A97'
        });
        OcultarPopupposition();
    }


    //}
}



//funcion para validar campos de una Inspeccion y cargar Formulario para una Condicion Insegura
function CargarFormularioCondicion() {
    FormularioGenerarInspeccion();
    if ($("#generarcondicion").valid()) {

        var Fecha = $("#FechaR").val();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        if ($.datepicker.parseDate('dd/mm/yy', Fecha) < $.datepicker.parseDate('dd/mm/yy', fecha_actual)) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'La Fecha  de realización no puede ser inferior a la Fecha actual.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
            return false;
        }
        else {
            var Configuraciones = $('#configuracion tbody').find('tr');
            PopupPosition();
            var Configuraciones = new Array();
            var idTr = 0;
            if ($("#idplaninspeccion").val() != 0) {
                $('#configuracion tbody').find('tr').each(function (ini) {
                    var datosconfiguraciones = new Object();
                    var idTr = $(this).attr('id');
                    $(this).find('td').each(function (seg) {
                    });
                    datosconfiguraciones.idconfiguracion = $('#item_idconfiguracion_' + idTr).val();
                    datosconfiguraciones.Descripcionconfiguracion = $("#item_Descripcionconfiguracion_" + idTr).val();
                    datosconfiguraciones.diasdesde = $('#item_diasdesde_' + idTr).val();
                    datosconfiguraciones.diashasta = $('#item_diashasta_' + idTr).val();
                    Configuraciones.push(datosconfiguraciones);
                });
                var asistentepi = new Array();
                $("#generarcondicion").find("input[name = asistente]").each(function (int, element) {
                    var asistente = new Object();
                    asistente.nombreasistente = $(element).val()
                    asistentepi.push(asistente)
                });
                //Crea el objeto que se envia al controlador para ser Guardado
                var crearmodel = {
                    fecharealizacioninspeccion: $("#FechaR").val(),
                    idempresa: $('#idempresa').val(),
                    //IDEmpresa: $('#IDEmpresa').val(),
                    Consecutivo: $('#Consecutivo').val(),
                    idplaninspeccion: $('#idplaninspeccion').val(),
                    DescribeInspeccion: $('#DescribeInspeccion').val(),
                    idSede: $('#idSede').val(),
                    idProceso: $('#idProceso').val(),
                    area: $('#area').val(),
                    hora: $('#hora').val(),
                    elementos: $("#ListaEHM").val(),
                    responsable: $('#responsable').val(),
                    Configuraciones: Configuraciones,
                    Asistentes: asistentepi,
                };
                $.ajax({
                    url: urlBase + '/PlandeInspeccion/ObtenerFormularioCondicion',
                    type: 'POST',
                    data: crearmodel,
                    success: function (result) {
                        OcultarPopupposition();
                        if (result) {
                            $("#panelcondiciones").slideDown();
                            if (result.crearmodel == 0) {
                                swal({
                                    type: 'success',
                                    title: 'Estimado Usuario',
                                    text: 'Ya existe una Planeación de Inspección con ese Número Consecutivo, ó anteriormente fué registrado,  Revise el listado de planeaciones no ejecutadas y continúe con el proceso.',
                                    showConfirmButton: true,
                                    confirmButtonText: 'Aceptar',
                                    confirmButtonColor: '#7E8A97'
                                });
                                $('.confirm').on('click', function () {
                                    window.location.href = "../PlandeInspeccion/Index";
                                    OcultarPopupposition();
                                });
                            }
                            else {
                                swal({
                                    type: 'success',
                                    title: 'Estimado Usuario',
                                    text: 'Inspección generada satisfactoriamente',
                                    confirmButtonColor: '#7E8A97',
                                    confirmButtonText: "Continuar",
                                });
                                $("#div_main").css("display", "block");
                                $("#btncreari").hide();
                                $("#btnconfig").hide();
                                $("#hora").attr("readonly", true);
                                $("#FechaR").attr("readonly", true);
                                $("#area").attr("readonly", true);
                                $('#responsable').attr("readonly", true);
                                $('#idSede').attr("readonly", true);
                                $('#idProceso').attr("readonly", true);
                                $('#asistente').attr("readonly", true);
                                $('#ListaEHM').attr("readonly", true);
                                $('#idinspeccion').val(result.idinspeccion);
                                if (result.peligrosos != null)
                                {
                                    $("#tipopeligro").empty();
                                    $("#tipopeligro").append("<option value=\"\">--Seleccionar Tipo Peligro--</option>");
                                    $.each(result.peligrosos, function (key, value) {
                                        $("#tipopeligro").append("<option value=\"" + value.idpeligro + "\">" + value.Describepeligro + "</option>");
                                    });
                                }
                                else
                                {
                                    swal({
                                        type: 'error',
                                        title: 'Estimado Usuario',
                                        text: 'No fue posible Obtener los Tipos de Peligros.',
                                        confirmButtonColor: '#7E8A97',
                                        confirmButtonText: "Continuar",
                                    });
                                }
                                $("#DescripcionConfig").empty();
                                $("#DescripcionConfig").append("<option value=\"\">--Seleccionar Configuracion Prioridad--</option>");
                                $.each(result.Configuraciones, function (key, value) {
                                    $("#DescripcionConfig").append("<option value=\"" + value.idconfiguracion + "\">" + value.Descripcionconfiguracion + "</option>");
                                });
                            }
                        }
                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error en la transacción',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                });
            }
            else {

                //Crea el objeto que se envia al controlador para ser Guardado
                var crearmodel = {
                    fecharealizacioninspeccion: $("#FechaR").val(),
                    idempresa: $('#idempresa').val(),
                    Consecutivo: $('#Consecutivo').val(),
                    idplaninspeccion: $('#idplaninspeccion').val(),
                    DescribeInspeccion: $('#DescribeInspeccion').val(),
                    idSede: $('#idSede').val(),
                    idProceso: $('#idProceso').val(),
                    area: $('#area').val(),
                    hora: $('#hora').val(),
                    elementos: $("#ListaEHM").val(),
                    responsable: $('#responsable').val(),
                    Configuraciones: Configuraciones,
                    Asistentes: asistentepi,
                };
                $.ajax({
                    url: urlBase + '/PlandeInspeccion/ObtenerFormularioCondicion',
                    type: 'POST',
                    data: crearmodel,
                    success: function (result) {
                        OcultarPopupposition();
                        if (result) {
                            swal({
                                type: 'success',
                                title: 'Estimado Usuario',
                                text: 'Inspección generada satisfactoriamente, Continue Agregando las Condiciones Inseguras.',
                                confirmButtonColor: '#7E8A97',
                                confirmButtonText: "Continuar",
                            });
                            $("#div_main").css("display", "block");
                            $("#btncreari").hide("slow");
                            $("#btnconfig").hide("slow");
                            $("#hora").attr("readonly", true);
                            $("#FechaR").attr("readonly", true);
                            $("#area").attr("readonly", true);
                            $('#responsable').attr("readonly", true);
                            $('#idSede').attr("readonly", true);
                            $('#idProceso').attr("readonly", true);
                            $('#asistente').attr("readonly", true);
                            $('#ListaEHM').attr("readonly", true);
                            $('#idinspeccion').val(result.idinspeccion);
                            if (result.peligrosos != null) {
                                $("#tipopeligro").empty();
                                $("#tipopeligro").append("<option value=\"\">--Seleccionar Tipo Peligro--</option>");
                                $.each(result.peligrosos, function (key, value) {
                                    $("#tipopeligro").append("<option value=\"" + value.idpeligro + "\">" + value.Describepeligro + "</option>");
                                });
                            }
                            else {
                                swal({
                                    type: 'error',
                                    title: 'Estimado Usuario',
                                    text: 'No fue posible Obtener los Tipos de Peligros.',
                                    confirmButtonColor: '#7E8A97',
                                    confirmButtonText: "Continuar",
                                });
                            }
                            $("#DescripcionConfig").empty();
                            $("#DescripcionConfig").append("<option value=\"\">--Seleccionar Configuracion Prioridad--</option>");
                            $.each(result.Configuraciones, function (key, value) {
                                $("#DescripcionConfig").append("<option value=\"" + value.idconfiguracionViewModel + "\">" + value.DescripcionViewModel + "</option>");
                            });
                        }
                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error en la transacción',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                });
            }
        }
    }
}
///Funcion para Agregar una Condicion Insegura en Flujo de la Planeacion 

function ObtenerConfigInspecc() {
    //var identinspeccion = $("#idinspeccion").val();
    if ($("#IdInspeccion").val() == undefined || $("#IdInspeccion").val()=="") {
        var IdInspeccion =$("#idinspeccion").val();
        var idinspeccion = { IdInspeccion: IdInspeccion }           
        $.ajax({
            data: idinspeccion,
            url: urlBase + '/PlandeInspeccion/ObtenerConfiguracionesPorInspeccion',
            type: 'POST',
            success: function (result) {

                if (result) {
                    //$("#planacciones").hide();
                    $('#idinspeccion').val((result.inspeccion));
                    $("#DescripcionConfig").empty();
                    $("#DescripcionConfig").append("<option value=\"\">--Seleccionar Configuracion Prioridad--</option>");
                    $.each(result.configurados, function (key, value) {
                        $("#DescripcionConfig").append("<option value=\"" + value.idconfiguracionViewModel + "\">" + value.DescripcionViewModel + "</option>");
                    });
                    $("#tipopeligro").empty();
                    $("#tipopeligro").append("<option value=\"\">--Seleccionar Tipo Peligro--</option>");
                    $.each(result.peligrosos, function (key, value) {
                        $("#tipopeligro").append("<option value=\"" + value.idpeligro + "\">" + value.Describepeligro + "</option>");
                    });
                }
            }
        });
    }
    else {
        var IdInspeccion = parseInt($("#IdInspeccion").val());
        var idinspeccion =
            { IdInspeccion: IdInspeccion }
        $.ajax({
            data: idinspeccion,
            url: urlBase + '/PlandeInspeccion/ObtenerConfiguracionesPorInspeccion',
            type: 'POST',
            success: function (result) {
                if (result) {
                    $('#idinspeccion').val((result.inspeccion));
                    $("#DescripcionConfig").empty();
                    $("#DescripcionConfig").append("<option value=\"\">-- Seleccionar Configuración Prioridad --</option>");

                    $.each(result.configurados, function (key, value) {
                        $("#DescripcionConfig").append("<option value=\"" + value.idconfiguracionViewModel + "\">" + value.DescripcionViewModel + "</option>");
                    });

                    $("#tipopeligro").empty();
                    $("#tipopeligro").append("<option value=\"\">-- Seleccionar Tipo Peligro --</option>");
                    $.each(result.peligrosos, function (key, value) {
                        $("#tipopeligro").append("<option value=\"" + value.idpeligro + "\">" + value.Describepeligro + "</option>");
                    });
                }
            }
        });
    }
}

/////Funcion para buscar una inspeccion por Tipo Inspeccion
$("#btnbuscarporTI").click(function (e) {
    e.preventDefault();
    validarbuscaTI();
    $("#idcondicioninsegura").hide();
    if ($("#buscarinspeccion").valid()) {
        PopupPosition();
        FechaInicialB = $("#FechaInicialB").val();
        FechaFinal = $("#FechaFinal").val();
        DescripcionTipoInspeccion = $("#DescripcionTipoInspeccion").val();
        IdSede = $("#idSede").val();

        if (FechaInicialB != "" & FechaFinal != "" & DescripcionTipoInspeccion != "" & IdSede != "") {

            if ($.datepicker.parseDate('dd/mm/yy', FechaFinal) < ($.datepicker.parseDate('dd/mm/yy', FechaInicialB))) {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'La Fecha Fin no puede ser menor que la Fecha Inicial.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
                return false;
            }
            else {

                var buscaTI = {
                    IdSede: $("#idSede").val(),
                    DescripcionTipoInspeccion: $("#DescripcionTipoInspeccion").val(),
                    FechaInicialB: $("#FechaInicialB").val(),
                    FechaFinal: $("#FechaFinal").val(),
                }
                $.ajax({
                    url: urlBase + '/PlandeInspeccion/BuscaTipoInspeccion',
                    data: buscaTI,
                    type: 'POST',
                    success: function (result) {
                        //$("#idinspeccion") = (result.EDpkinspeccion).val();
                        if (result.Data.length > 0) {
                            OcultarPopupposition();
                            $("#myModal").modal("show");
                            $("#resultadoinspecciones").slideDown();
                            $("#resultadoinspecciones").trigger("reset");
                            $('#tablainspecciones').empty();
                            $('#tablainspecciones').append
                            var contador = 0;
                            $.each(result.Data, function (ind, element) {
                                if (element.EDEstadoPlanAccionInspeccion == "Abierto-Vigente" || element.EDEstadoPlanAccionInspeccion == "Cerrado-Vigente") {
                                    var EDFechaPlaneacionIns = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                                    var FechaFinPlanED = moment(result.Data[ind].FechaFinPlanED).format("DD/MM/YYYY");
                                    if (result.Data[ind].FechaCierrePlanED != null) {
                                        var fechaConvertida = moment(result.Data[ind].FechaCierrePlanED).format("DD/MM/YYYY");
                                    }
                                    else {
                                        fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                    }
                                    var elemento =
                                         '<tr name="fili">' +
                                  //'<td style="border:solid 1px #808080;vertical-align:middle;text-align:center;color:black;background-color:whitesmoke">' + '<input type="radio" name="Inspeccion" id=' + contador + '  class ="Inspeccion" >' + '</td>' +
                                   //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescripcionTipoI" id = "' + contador + '" +  value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkinspeccion" id = "pkinspeccion' + contador + '" value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDpkinspeccion" id = "inspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescribeinspeccion" id = "pkplanea' + contador + '" value="' + element.EDDescribeinspeccion + '">' + element.EDDescribeinspeccion + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDFechaPlaneacionIns" id = "EDFechaPlaneacionIns' + contador + '" +  value="' + EDFechaPlaneacionIns + '">' + EDFechaPlaneacionIns + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDResponsablePlaneacion" id = "' + contador + '" +  value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                                  
                                   '<td style="vertical-align:middle;border:2px solid lightslategray text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkCondicion" id = "pkcondicion' + contador + '" value="' + element.EDpkCondicion + '"><input type="hidden" name="EDDescribeCondicion" id = "condicion' + contador + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaFinPlanED" id = "' + contador + '" +  value="' + FechaFinPlanED + '">' + FechaFinPlanED + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaCierrePlanED" id = "' + contador + '" +  value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +

                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDdescribesede" id = "sede' + contador + '" value="' + element.EDdescribesede + '">' + element.EDdescribesede + '</td>' +
                                   '<td style="background-color:#229954;color:white;vertical-align:middle;border:2px solid lightslategray; text-align:center"><input type="hidden" name="EDEstadoPlanAccionInspeccion"  id = "epla' + contador + '"value="' + element.EDEstadoPlanAccionInspeccion + '">' + element.EDEstadoPlanAccionInspeccion + '</td>' +
                                   '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="btnejecutar" title="Editar Condición " onclick="EditarCondicion(' + element.EDpkCondicion + ', ' + element.EDpkinspeccion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-pencil"></span></a></td>' +
                                   '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonBorrar" title="Ver Información Inspección" onclick="(' + element.EDpkinspeccion + ', ' + element.EDpkCondicion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-search"></span></a></td>' +

                                  '</tr>'
                                    $('#tablainspecciones').append(elemento)
                                    contador = contador + 1;
                                    if (ind == 0) {
                                        var radio = $('input:radio[name=Inspeccion]').prop('checked', true);
                                    };
                                }
                                else {
                                    if (element.EDEstadoPlanAccionInspeccion == "Abierto-Vencido" || element.EDEstadoPlanAccionInspeccion == "Cerrado-Vencido") {
                                        OcultarPopupposition();
                                        var EDFechaPlaneacionIns = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                                        var FechaFinPlanED = moment(result.Data[ind].FechaFinPlanED).format("DD/MM/YYYY");
                                        if (result.Data[ind].FechaCierrePlanED != null) {
                                            var fechaConvertida = moment(result.Data[ind].FechaCierrePlanED).format("DD/MM/YYYY");
                                        }
                                        else {
                                            fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                        }
                                        var elemento =
                                        '<tr name="fili">' +
                                      //'<td style="border:solid 1px #808080;vertical-align:middle;text-align:center;color:black;background-color:whitesmoke">' + '<input type="radio" name="Inspeccion" id=' + contador + '  class ="Inspeccion" >' + '</td>' +
                                       //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescripcionTipoI" id = "' + contador + '" +  value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkinspeccion" id = "pkinspeccion' + contador + '" value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDpkinspeccion" id = "inspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescribeinspeccion" id = "pkplanea' + contador + '" value="' + element.EDDescribeinspeccion + '">' + element.EDDescribeinspeccion + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDFechaPlaneacionIns" id = "EDFechaPlaneacionIns' + contador + '" +  value="' + EDFechaPlaneacionIns + '">' + EDFechaPlaneacionIns + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDResponsablePlaneacion" id = "' + contador + '" +  value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                                       
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkCondicion" id = "pkcondicion' + contador + '" value="' + element.EDpkCondicion + '"><input type="hidden" name="EDDescribeCondicion" id = "condicion' + contador + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaFinPlanED" id = "' + contador + '" +  value="' + FechaFinPlanED + '">' + FechaFinPlanED + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaCierrePlanED" id = "' + contador + '" +  value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                       
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDdescribesede" id = "sede' + contador + '" value="' + element.EDdescribesede + '">' + element.EDdescribesede + '</td>' +
                                       '<td style="background-color:red;color:white;vertical-align:middle;border:2px solid lightslategray; text-align:center"><input type="hidden" name="EDEstadoPlanAccionInspeccion"  id = "epla' + contador + '"value="' + element.EDEstadoPlanAccionInspeccion + '">' + element.EDEstadoPlanAccionInspeccion + '</td>' +
                                       '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="btnejecutar" title="Editar Condición " onclick="EditarCondicion(' + element.EDpkCondicion + ', ' + element.EDpkinspeccion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-pencil"></span></a></td>' +
                                       '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonBorrar" title="Ver Información Inspección" onclick="VerInfoInspeccion(' + element.EDpkinspeccion + ', ' + element.EDpkCondicion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-search"></span></a></td>' +

                                      '</tr>'
                                        $('#tablainspecciones').append(elemento)
                                        contador = contador + 1;
                                    }
                                }
                            });
                            paginador("#tablainspecciones", "tr[name = fili]", "#paginador2")
                        }
                        if (result.Data.length == 0) {

                            swal({ title: "Estimado Usuario", text: "No se encontraron Registros.", type: "warning", confirmButtonColor: '#7E8A97' },
                              function () {
                                  $("#InformacionGeneralInspeccion").hide();
                                  $("#resultadoinspecciones").slideUp();
                                  OcultarPopupposition();
                              });
                        }
                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presento un error, intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                });

            }



        }
        else {
            if (IdSede != "" & DescripcionTipoInspeccion != "") {
                $("#InformacionGeneralInspeccion").hide();
                var buscaTI = {
                    IdSede: $("#idSede").val(),
                    DescripcionTipoInspeccion: $("#DescripcionTipoInspeccion").val(),
                    FechaInicialB: $("#FechaInicialB").val(),
                    FechaFinal: $("#FechaFinal").val(),
                }
                $.ajax({
                    url: urlBase + '/PlandeInspeccion/BuscaTipoInspeccion',
                    data: buscaTI,
                    type: 'POST',
                    success: function (result) {
                        OcultarPopupposition();
                        //$("#idinspeccion").val(result.Data[0].EDpkinspeccion);
                        if (result.Data.length > 0) {
                            $("#resultadoinspecciones").slideDown();
                            $("#resultadoinspecciones").trigger("reset");
                            $('#tablainspecciones').empty();
                            $('#tablainspecciones').append
                            $("#InformacionGeneralInspeccion").hide();
                            var contador = 0;
                            $.each(result.Data, function (ind, element) {

                                if (element.EDEstadoPlanAccionInspeccion == "Abierto-Vigente" || element.EDEstadoPlanAccionInspeccion == "Cerrado-Vigente") {
                                    var EDFechaPlaneacionIns = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                                    var FechaFinPlanED = moment(result.Data[ind].FechaFinPlanED).format("DD/MM/YYYY");
                                    if (result.Data[ind].FechaCierrePlanED != null) {
                                        var fechaConvertida = moment(result.Data[ind].FechaCierrePlanED).format("DD/MM/YYYY");
                                    }
                                    else {
                                        fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                    }
                                    var elemento =
                                        '<tr name="fili">' +
                                  //'<td style="border:solid 1px #808080;vertical-align:middle;text-align:center;color:black;background-color:whitesmoke">' + '<input type="radio" name="Inspeccion" id=' + contador + '  class ="Inspeccion" >' + '</td>' +
                                   //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescripcionTipoI" id = "' + contador + '" +  value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkinspeccion" id = "pkinspeccion' + contador + '" value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDpkinspeccion" id = "inspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescribeinspeccion" id = "pkplanea' + contador + '" value="' + element.EDDescribeinspeccion + '">' + element.EDDescribeinspeccion + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDFechaPlaneacionIns" id = "EDFechaPlaneacionIns' + contador + '" +  value="' + EDFechaPlaneacionIns + '">' + EDFechaPlaneacionIns + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDResponsablePlaneacion" id = "' + contador + '" +  value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                           
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkCondicion" id = "pkcondicion' + contador + '" value="' + element.EDpkCondicion + '"><input type="hidden" name="EDDescribeCondicion" id = "condicion' + contador + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaFinPlanED" id = "' + contador + '" +  value="' + FechaFinPlanED + '">' + FechaFinPlanED + '</td>' +
                                    '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaCierrePlanED" id = "' + contador + '" +  value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                           
                                   '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDdescribesede" id = "sede' + contador + '" value="' + element.EDdescribesede + '">' + element.EDdescribesede + '</td>' +
                                   '<td style="background-color:#229954;color:white;vertical-align:middle;border:2px solid lightslategray; text-align:center"><input type="hidden" name="EDEstadoPlanAccionInspeccion"  id = "epla' + contador + '"value="' + element.EDEstadoPlanAccionInspeccion + '">' + element.EDEstadoPlanAccionInspeccion + '</td>' +
                                      '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="btnejecutar" title="Editar Condición " onclick="EditarCondicion(' + element.EDpkCondicion + ', ' + element.EDpkinspeccion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-pencil"></span></a></td>' +
                                    '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonBorrar" title="Ver Información Inspección" onclick="VerInfoInspeccion(' + element.EDpkinspeccion + ', ' + element.EDpkCondicion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-search"></span></a></td>' +

                                  '</tr>'
                                    $('#tablainspecciones').append(elemento)
                                    contador = contador + 1;
                                    if (ind == 0) {
                                        var radio = $('input:radio[name=Inspeccion]').prop('checked', true);
                                    };
                                }
                                else {
                                    if (element.EDEstadoPlanAccionInspeccion == "Abierto-Vencido" || element.EDEstadoPlanAccionInspeccion == "Cerrado-Vencido") {
                                        var EDFechaPlaneacionIns = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                                        var FechaFinPlanED = moment(result.Data[ind].FechaFinPlanED).format("DD/MM/YYYY");
                                        if (result.Data[ind].FechaCierrePlanED != null) {
                                            var fechaConvertida = moment(result.Data[ind].FechaCierrePlanED).format("DD/MM/YYYY");
                                        }
                                        else {
                                            fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                        }
                                        var elemento =
                                              '<tr name="fili">' +
                                      //'<td style="border:2px solid lightslategray;vertical-align:middle;text-align:center;color:black;background-color:whitesmoke">' + '<input type="radio" name="Inspeccion" id=' + contador + '  class ="Inspeccion" >' + '</td>' +
                                       //'<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescripcionTipoI" id = "' + contador + '" +  value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkinspeccion" id = "pkinspeccion' + contador + '" value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDpkinspeccion" id = "inspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescribeinspeccion" id = "pkplanea' + contador + '" value="' + element.EDDescribeinspeccion + '">' + element.EDDescribeinspeccion + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDFechaPlaneacionIns" id = "EDFechaPlaneacionIns' + contador + '" +  value="' + EDFechaPlaneacionIns + '">' + EDFechaPlaneacionIns + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDResponsablePlaneacion" id = "' + contador + '" +  value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkCondicion" id = "pkcondicion' + contador + '" value="' + element.EDpkCondicion + '"><input type="hidden" name="EDDescribeCondicion" id = "condicion' + contador + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaFinPlanED" id = "' + contador + '" +  value="' + FechaFinPlanED + '">' + FechaFinPlanED + '</td>' +
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaCierrePlanED" id = "' + contador + '" +  value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                       
                                       '<td style="vertical-align:middle;border:2px solid lightslategray; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDdescribesede" id = "sede' + contador + '" value="' + element.EDdescribesede + '">' + element.EDdescribesede + '</td>' +
                                       '<td style="background-color:red;color:white;vertical-align:middle;border:2px solid lightslategray; text-align:center"><input type="hidden" name="EDEstadoPlanAccionInspeccion"  id = "epla' + contador + '"value="' + element.EDEstadoPlanAccionInspeccion + '">' + element.EDEstadoPlanAccionInspeccion + '</td>' +
                                          '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="btnejecutar" title="Editar Condición" onclick="EditarCondicion(' + element.EDpkCondicion + ', ' + element.EDpkinspeccion + ')" class="btn btn-serch btn-md"><span class="glyphicon glyphicon-pencil"></span></a></td>' +
                                        '<td style="border:2px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonBorrar" title="Ver Información Inspección" onclick="VerInfoInspeccion(' + element.EDpkinspeccion + ', ' + element.EDpkCondicion + ')" class="btn btn-search btn-md"><span class="glyphicon glyphicon-search"></span></a></td>' +

                                      '</tr>'
                                        $('#tablainspecciones').append(elemento)
                                        contador = contador + 1;
                                    }
                                }
                            });
                            paginador("#tablainspecciones", "tr[name = fili]", "#paginador2")
                        }
                        else {

                            swal({ title: "Estimado Usuario", text: "No se encontraron Registros.", type: "warning", confirmButtonColor: '#7E8A97' },
                              function () {
                                  $("#resultadoinspecciones").slideUp();
                                  $("#InformacionGeneralInspeccion").hide();
                              });
                        }
                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presento un error, intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                });


            }
            else {
                if (IdSede != "" | DescripcionTipoInspeccion != "") {
                    $("#InformacionGeneralInspeccion").hide();
                    var buscaTI = {
                        IdSede: $("#idSede").val(),
                        DescripcionTipoInspeccion: $("#DescripcionTipoInspeccion").val(),
                        FechaInicialB: $("#FechaInicialB").val(),
                        FechaFinal: $("#FechaFinal").val(),
                    }
                    $.ajax({
                        url: urlBase + '/PlandeInspeccion/BuscaTipoInspeccion',
                        data: buscaTI,
                        type: 'POST',
                        success: function (result) {
                            $("#InformacionGeneralInspeccion").hide();
                            OcultarPopupposition();
                            //$("#idinspeccion").val(result.Data[0].EDpkinspeccion);
                            if (result.Data.length > 0) {
                                $("#resultadoinspecciones").slideDown();
                                $("#resultadoinspecciones").trigger("reset");
                                $('#tablainspecciones').empty();
                                $('#tablainspecciones').append
                                $("#InformacionGeneralInspeccion").hide();
                                var contador = 0;
                                $.each(result.Data, function (ind, element) {
                                    if (element.EDEstadoPlanAccionInspeccion == "Abierto-Vigente" || element.EDEstadoPlanAccionInspeccion == "Cerrado-Vigente") {
                                        var EDFechaPlaneacionIns = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                                        var FechaFinPlanED = moment(result.Data[ind].FechaFinPlanED).format("DD/MM/YYYY");
                                        if (result.Data[ind].FechaCierrePlanED != null) {
                                            var fechaConvertida = moment(result.Data[ind].FechaCierrePlanED).format("DD/MM/YYYY");
                                        }
                                        else {
                                            fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                        }
                                        var elemento =
                                             '<tr name="fili">' +
                                  //'<td style="border:solid 1px #808080;vertical-align:middle;text-align:center;color:black;background-color:whitesmoke">' + '<input type="radio" name="Inspeccion" id=' + contador + '  class ="Inspeccion" >' + '</td>' +
                                   //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescripcionTipoI" id = "' + contador + '" +  value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkinspeccion" id = "pkinspeccion' + contador + '" value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDpkinspeccion" id = "inspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescribeinspeccion" id = "pkplanea' + contador + '" value="' + element.EDDescribeinspeccion + '">' + element.EDDescribeinspeccion + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDFechaPlaneacionIns" id = "EDFechaPlaneacionIns' + contador + '" +  value="' + EDFechaPlaneacionIns + '">' + EDFechaPlaneacionIns + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDResponsablePlaneacion" id = "' + contador + '" +  value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkCondicion" id = "pkcondicion' + contador + '" value="' + element.EDpkCondicion + '"><input type="hidden" name="EDDescribeCondicion" id = "condicion' + contador + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaFinPlanED" id = "' + contador + '" +  value="' + FechaFinPlanED + '">' + FechaFinPlanED + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaCierrePlanED" id = "' + contador + '" +  value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                   
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDdescribesede" id = "sede' + contador + '" value="' + element.EDdescribesede + '">' + element.EDdescribesede + '</td>' +
                                   '<td style="background-color:#229954;color:white;vertical-align:middle;border:solid 1px #808080; text-align:center"><input type="hidden" name="EDEstadoPlanAccionInspeccion"  id = "epla' + contador + '"value="' + element.EDEstadoPlanAccionInspeccion + '">' + element.EDEstadoPlanAccionInspeccion + '</td>' +
                                      '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="btnejecutar" title="Editar Condición" data-toggle="modal"  onclick="EditarCondicion(' + element.EDpkCondicion + ', ' + element.EDpkinspeccion + ')" class="btn btn-d btn-lg"><span class="glyphicon glyphicon-check"></span></a></td>' +
                                    '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonBorrar" title="Ver Información Inspección" onclick="VerInfoInspeccion(' + element.EDpkinspeccion + ', ' + element.EDpkCondicion + ')" class="btn btn-d btn-lg"><span class="glyphicon  glyphicon-th-list"></span></a></td>' +

                                  '</tr>'
                                        $('#tablainspecciones').append(elemento)
                                        contador = contador + 1;
                                        if (ind == 0) {
                                            var radio = $('input:radio[name=Inspeccion]').prop('checked', true);
                                        };
                                    }
                                    else {
                                        if (element.EDEstadoPlanAccionInspeccion == "Abierto-Vencido" || element.EDEstadoPlanAccionInspeccion == "Cerrado-Vencido") {
                                        var EDFechaPlaneacionIns = moment(result.Data[ind].EDFechaPlaneacionIns).format("DD/MM/YYYY");
                                        var FechaFinPlanED = moment(result.Data[ind].FechaFinPlanED).format("DD/MM/YYYY");
                                        if (result.Data[ind].FechaCierrePlanED != null) {
                                            var fechaConvertida = moment(result.Data[ind].FechaCierrePlanED).format("DD/MM/YYYY");
                                        }
                                        else {
                                            fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                        }
                                        var elemento = '<tr name="fili">' +
                                                                        
                                  //'<td style="border:solid 1px #808080;vertical-align:middle;text-align:center;color:black;background-color:whitesmoke">' + '<input type="radio" name="Inspeccion" id=' + contador + '  class ="Inspeccion" >' + '</td>' +
                                   //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescripcionTipoI" id = "' + contador + '" +  value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkinspeccion" id = "pkinspeccion' + contador + '" value="' + element.EDpkinspeccion + '"><input type="hidden" name="EDpkinspeccion" id = "inspeccion' + contador + '" value="' + element.EDDescripcionTipoI + '">' + element.EDDescripcionTipoI + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDDescribeinspeccion" id = "pkplanea' + contador + '" value="' + element.EDDescribeinspeccion + '">' + element.EDDescribeinspeccion + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDFechaPlaneacionIns" id = "EDFechaPlaneacionIns' + contador + '" +  value="' + EDFechaPlaneacionIns + '">' + EDFechaPlaneacionIns + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDResponsablePlaneacion" id = "' + contador + '" +  value="' + element.EDResponsablePlaneacion + '">' + element.EDResponsablePlaneacion + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDpkCondicion" id = "pkcondicion' + contador + '" value="' + element.EDpkCondicion + '"><input type="hidden" name="EDDescribeCondicion" id = "condicion' + contador + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaFinPlanED" id = "' + contador + '" +  value="' + FechaFinPlanED + '">' + FechaFinPlanED + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="FechaCierrePlanED" id = "' + contador + '" +  value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                   '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke"><input type="hidden" name="EDdescribesede" id = "sede' + contador + '" value="' + element.EDdescribesede + '">' + element.EDdescribesede + '</td>' +
                                   '<td style="background-color:red;color:white;vertical-align:middle;border:solid 1px #808080; text-align:center"><input type="hidden" name="EDEstadoPlanAccionInspeccion"  id = "epla' + contador + '"value="' + element.EDEstadoPlanAccionInspeccion + '">' + element.EDEstadoPlanAccionInspeccion + '</td>' +
                                      '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="btnejecutar" title="Editar Condición" data-toggle="modal"  onclick="EditarCondicion(' + element.EDpkCondicion + ', ' + element.EDpkinspeccion + ')" class="btn btn-d btn-lg"><span class="glyphicon glyphicon-check"></span></a></td>' +
                                    '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonBorrar" title="Ver Información Inspección" onclick="VerInfoInspeccion(' + element.EDpkinspeccion + ', ' + element.EDpkCondicion + ')" class="btn btn-d btn-lg"><span class="glyphicon glyphicon-th-list"></span></a></td>' +

                                  '</tr>'
                                        $('#tablainspecciones').append(elemento)
                                        contador = contador + 1;
                                    }
                                }
                                });
                                paginador("#tablainspecciones", "tr[name = fili]", "#paginador2")
                            }
                            else {

                                swal({ title: "Estimado Usuario", text: "No se encontraron Registros.", type: "warning", confirmButtonColor: '#7E8A97' },
                                  function () {
                                      $("#resultadoinspecciones").slideUp();
                                  });
                            }
                        },
                        error: function (result) {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presento un error, intente mas tarde.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                    });
                }
            }
        }
    }
});

///Funcion para Eliminar una Condicion Insegura
function EliminarCondicion(EDpkCondicion, EDpkinspeccion) {
    PopupPosition();
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar la Condición Insegura?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#7E8A97',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    },
     (function (isConfirm) {
         if (isConfirm) {
             var envio = { clavecondicion: EDpkCondicion, claveinspeccion: EDpkinspeccion }
             $.ajax({
                 type: 'Post',
                 url: '/PlandeInspeccion/EliminarCondicion',
                 data: envio,
                 traditional: true,
                 success: function (result) {
                     if (result) {
                         
                         $("#planaccion").hide();
                        // swal({ title: "Estimado Usuario", text: "Condición Insegura Eliminada.", type: "success", confirmButtonColor: '#7E8A97' });
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
                                             '<td style="border:solid 2px lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-search btn-md">' + element.EDEvidenciacondicion + '<span class="glyphicon glyphicon-search"></span></a></td>' +
                                            // '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-lg" style="font-size:16px;text-decoration: underline">' + element.EDEvidenciacondicion + '</span></a></td>' +
                                              '<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + element.EDpkCondicion + ',' + element.EDPkInspeccion + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                             '</tr>'
                             $('#condicionesins').append(elementoscs)
                             contadorcs = contadorcs + 1
                         });

                         
                     }

                     else {
                         swal({ title: "Estimado Usuario", text: "No fue posible Eliminar esta Condición Insegura.", type: "warning", confirmButtonColor: '#7E8A97' });
                         OcultarPopupposition();
                     }
                 },

                 error: function (result) {
                     swal({
                         type: 'error',
                         title: 'Estimado Usuario',
                         text: 'Se presento un error, intente mas tarde.',
                         confirmButtonColor: '#7E8A97'
                     });
                     OcultarPopupposition();
                 }
             });
            OcultarPopupposition();
         }

         else
         {
             OcultarPopupposition();

         }

     }))
  
  
}

//Funcion para Ver toda La Informacion Completa de la Inspeccion
$("#InformacionGeneralInspeccion").hide();
function VerInfoInspeccion(EDpkinspeccion, EDpkCondicion) {
    PopupPosition();
    if (EDpkinspeccion != null & EDpkCondicion != null) {
        var Datos = {
            claveCondicion: EDpkCondicion,
            claveInspeccion: EDpkinspeccion,

        };

        $.ajax({
            url: urlBase + '/PlandeInspeccion/InformacionInspeccion',  //controlador/metodo que esta en el controlador
            data: Datos,
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result) {
                    OcultarPopupposition();
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'La información de la Inspección Seleccionada ha sido Obtenida.',
                        showConfirmButton: true,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#7E8A97'
                    });
                    $('.confirm').on('click', function () {
                        $("#resultadoinspecciones").slideUp();
                        $("#InformacionGeneralInspeccion").slideDown();
                        $("#Hora").html(result.Data.EDhora);
                        $("#Sede").html(result.Data.EDdescribesede);
                        $("#RazonSocial").html(result.Data.EDDescribeEmpresa);
                        $("#NitEmpresa").html(result.Data.EDNitEmpresa);
                        $("#TipoI").html(result.Data.EDDescripcionTipoI);
                        $("#DescribeTI").html(result.Data.EDDescribeinspeccion);
                        $("#ResponsableL").html(result.Data.EDresponsableLugar);
                        $("#ResponsableI").html(result.Data.EDResponsablePlaneacion);
                        $("#Area").html(result.Data.EDarealugar);
                        $("#Proceso").html(result.Data.EDdescribeProceso);
                        var fechaConvertida = moment(result.Data.EDFechaRealizacion).format("DD/MM/YYYY");
                        $('#FechaR').text(fechaConvertida);
                        $("#FechaR").val(fechaConvertida);
                        var fechaConvertida1 = moment(result.Data.EDFechaPlaneacionIns).format("DD/MM/YYYY");
                        $('#FechaPlan').text(fechaConvertida1);
                        $("#FechaPlan").val(fechaConvertida1);
                        $("#Consecutivo").html(result.Data.EDConsecutivo);
                        $("#Estado").html(result.Data.EDEstadoInspeccion);
                        if ($("#Estado").val() == 1) {
                            $("#Estado").text("Sin Ejecutar");
                        }
                        else {

                            $("#Estado").text("Ejecutada");
                        }
                        $('#asistentes').empty();
                        $('#asistentes').append
                        var contadora = 0;
                        $.each(result.Data.Asistentes, function (ind, element) {
                            var elementos = '<tr name="asisten">' +
                                            '<td style="vertical-align:middle;border:solid 1px lightslategray; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="idasistente" id="idasistente' + contador + '"value="' + element.NombreAsistente + '"><input type="hidden" name="NombreAsistente" id = "NombreAsistente' + contadora + '" value="' + element.NombreAsistente + '">' + element.NombreAsistente + '</td>' +

                                            '</tr>'
                            $('#asistentes').append(elementos)
                            contadora = contadora + 1
                        })
                        if (result.Data.EDElementos != "") {
                            $('#elementos').empty();
                            $('#elementos').append
                            var contadore = 0;
                            $.each(result.Data.EDElementos, function (ind, element) {
                                var elementosce = '<tr name="elementos">' +
                                                '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="Pk_Id_AdmoEMH" id="Pk_Id_AdmoEMH' + contador + '"value="' + element.TipoElemento + '"><input type="hidden" name="TipoElemento" id = "TipoElemento' + contador + '" value="' + element.TipoElemento + '">' + element.TipoElemento + '</td>' +
                                                '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="NombreElemento" id="NombreElemento' + contador + '"value="' + element.NombreElemento + '">' + element.NombreElemento + '</td>' +
                                                '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="Marca" id="Marca' + contador + '"value="' + element.Marca + '">' + element.Marca + '</td>' +
                                                '</tr>'
                                $('#elementos').append(elementosce)
                                contadore = contadore + 1
                            })
                        }
                        else {
                            $("#panelelementos").hide()
                        }
                        $('#condicionesins').empty();
                        $('#condicionesins').append;
                        var contadorcs = 0;
                        $.each(result.Data.CondicionesIns, function (ind, element) {
                            var elementoscs = '<tr name="condiciones" id="contador">' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDescribePrioridad" id="EDescribePrioridad' + contadorcs + '"value="' + element.EDescribePrioridad + '">' + element.EDescribePrioridad + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasDesde" id="EDDiasDesde' + contadorcs + '"value="' + element.EDDiasDesde + '">' + element.EDDiasDesde + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + element.EDDiasHasta + '">' + element.EDDiasHasta + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
                                            '</tr>'
                            $('#condicionesins').append(elementoscs)
                            contadorcs = contadorcs + 1
                        });

                        OcultarPopupposition();
                    })

                }


            }, error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}

//Funcion para Validar el Buscar por fecha, sede y tipo Inspeccion
function validarbuscaTI() {
    $("#buscarinspeccion").validate({
        errorClass: "error",
        rules: {
            idSede: {
                required: true
            }
        },
        messages: {
            idSede: {
                required: "Seleccione Una Sede."
            }
        }
    });
}

///Funcion para Editar una condicion Insegura.
$("#idcondicioninsegura").hide();
function EditarCondicion(EDpkCondicion, EDpkinspeccion) {
    PopupPosition();
    var condicion = { EDpkCondicion: EDpkCondicion, EDpkinspeccion: EDpkinspeccion }
    $.ajax({
        url: urlBase + '/PlandeInspeccion/ObtenerInformacionCondicionInspeccion',  //controlador/metodo que esta en el controlador
        data: condicion,
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {

                if (result.Data.EDEstadoPlanAccion == 0) {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No es posible editar la Condición Insegura seleccionada; actividad del plan de acción ya se encuentra cerrada.',
                        confirmButtonColor: '#7E8A97'
                    });

                }
                else {
                    $("#idcondicioninsegura").show("show");
                    $("#resultadoinspecciones").slideUp("");
                    $("#DesCI").val(result.Data.EDDescribeCondicion);
                    $("#Uesp").val(result.Data.EDUbicacionespecifica);
                    $("#pkcondicion").val(result.Data.EDpkCondicion)

                    $("#tipopeligro").empty();
                    $("#tipopeligro").append("<option value=\"\">--Seleccionar Tipo Peligro--</option>");
                    $.each(result.Data.Peligros, function (key, value) {
                        if (result.Data.EDClasificacionRiesgo == value.PK_Tipo_De_Peligro) {
                            $("#tipopeligro").append("<option selected value=\"" + value.PK_Tipo_De_Peligro + "\">" + value.Descripcion_Del_Peligro + "</option>");
                        }
                        else {
                        $("#tipopeligro").append("<option value=\"" + value.PK_Tipo_De_Peligro + "\">" + value.Descripcion_Del_Peligro + "</option>");
                        }
                    });

                    $("#FK_Clasificacion_De_Peligro").empty();
                    $("#FK_Clasificacion_De_Peligro").append("<option value=\"\">--Seleccionar Tipo Peligro--</option>");
                    $.each(result.Data.ClasificacionPeligros, function (key, value) {
                        if (result.Data.EDRiesgopeligro == value.IdClasificacionDePeligro) {
                            $("#FK_Clasificacion_De_Peligro").append("<option selected value=\"" + value.IdClasificacionDePeligro + "\">" + value.DescripcionClaseDePeligro + "</option>");
                            
                        }
                        else {
                            $("#FK_Clasificacion_De_Peligro").append("<option value=\"" + value.IdClasificacionDePeligro + "\">" + value.DescripcionClaseDePeligro + "</option>");
                            
                        }

                    });

                    $("#DescripcionConfig").empty();
                    $("#DescripcionConfig").append();
                    $.each(result.Data.Configuraciones, function (key, value) {
                        if (result.Data.EDConfiguracioncondicion == value.idconfiguracion) {
                            $("#DescripcionConfig").append("<option selected value=\"" + value.idconfiguracion + "\">" + value.Descripcion + "</option>");
                        }
                        else {
                        $("#DescripcionConfig").append("<option value=\"" + value.idconfiguracion + "\">" + value.Descripcion + "</option>");
                        }     
                    });
                }
            }
            else {
                swal({
                    type: 'success',
                    title: 'Estimado Usuario',
                    text: 'No fué Posible Obtener la Información',
                    confirmButtonColor: '#7E8A97'
                });

            }


        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error, intente mas tarde',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
    });

}


///Funcion para obtener en el modal la informacion de la condicion Insegura.
$("#btneditarcondicion").click(function (e) {
    e.preventDefault();
    $('#tablainspecciones').find("input[type=radio]").each(function () {
        if ($(this).prop('checked')) {
            var idTr = $(this).attr('id');
            var infocondicion = {
                EDpkCondicion: $("#pkcondicion" + idTr).val(),
                EDpkinspeccion: ("#pkinspeccion" + idTr).val(),
            };
            $.ajax({
                url: urlBase + '/PlandeInspeccion/ObtenerInformacionCondicionInspeccion',  //controlador/metodo que esta en el controlador
                data: infocondicion,
                type: 'POST',
                success: function (result) {
                    $("#DesCI").val(result.Data.EDDescribeCondicion);
                    $("#Uesp").val(result.Data.EDUbicacionespecifica);

                    $("#tipopeligro").empty();
                    $("#tipopeligro").append();
                    $.each(result.Data.Peligros, function (key, value) {

                        $("#tipopeligro").append("<option value=\"" + key.PK_Tipo_De_Peligro + "\">" + value.Descripcion_Del_Peligro + "</option>");
                    });
                    $("#DescripcionConfig").empty();
                    $("#DescripcionConfig").append();
                    $.each(result.Data.Configuraciones, function (key, value) {
                        $("#DescripcionConfig").append("<option value=\"" + key.idconfiguracion + "\">" + value.Descripcion + "</option>");
                    });
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente mas tarde',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    });

});

///Funcion para Enviar al controlador la informacion de Edicion de la Condicion Insegura. 

function Modificarcondicion() {
    DescribeCondicionVM = $('#DesCI').val();
    UbicacionespecificaVM = $('#Uesp').val();
    RiesgopeligroVM = $('#tipopeligro').val();
    ClasificacionRiesgoVM = $('#FK_Clasificacion_De_Peligro').val();
    EvidenciacondicionVM = $('#Evidencia').val(),
    ConfiguracioncondicionVM = $('#DescripcionConfig').val();
    pkCondicionVM = $("#pkcondicion").val();
    OtroRiesgoVM = $("#Otro").val();

    if ($("#DesCI").val() == "" || $('#Uesp').val() == "" || $('#tipopeligro').val() == "" || $('#FK_Clasificacion_De_Peligro').val() == null || $('#Evidencia').val() == "" || $('#DescripcionConfig').val() == "") {
        swal({ title: "Estimado Usuario", text: "Los campos no pueden estar vacios.", type: "success", confirmButtonColor: '#7E8A97' });
    }
    else {
        PopupPosition();
        var condiciones = {
            DescribeCondicionVM: $('#DesCI').val(),
            UbicacionespecificaVM: $('#Uesp').val(),
            RiesgopeligroVM: $('#tipopeligro').val(),
            ClasificacionRiesgoVM: $('#FK_Clasificacion_De_Peligro').val(),
            EvidenciacondicionVM: $('#Evidencia').val(),
            ConfiguracioncondicionVM: $('#DescripcionConfig').val(),
            pkCondicionVM: $("#pkcondicion").val(),
            OtroRiesgoVM: $("#Otro").val(),

        }
        $.ajax({
            url: urlBase + '/PlandeInspeccion/ModificarCondicion',  //controlador/metodo que esta en el controlador
            data: condiciones,
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                if (result.Data != "") {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Condición Insegura modificada satisfactoriamente.',
                        showConfirmButton: true,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#7E8A97'
                    });
                    $('.confirm').on('click', function () {
                        $("#buscarinspeccion").trigger("reset");
                        $("#idcondicioninsegura").slideUp();
                        $("#InformacionGeneralInspeccion").hide();
                        OcultarPopupposition();
                    });
                }

                else {
                    swal({ title: "Estimado Usuario", text: "No se pudo realizar la modificación.", type: "success", confirmButtonColor: '#7E8A97' },
            function () {
                $("#idcondicioninsegura").trigger("reset");
                $("#idcondicioninsegura").slideUp();
                $("#InformacionGeneralInspeccion").hide();
                OcultarPopupposition();
            });
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}



///Funcion que envia al Modulo acciones Correctivas los planes de Accion de las Inspecciones
$(function () {
    $("#GuardarAccion").bind("click", function () {
        var onEventLaunchGuardar = new postGuardar();
        onEventLaunchGuardar.launchGuardar();
    });
});
function postGuardar() {
    this.launchGuardar = function () {
        var Condiciones = $('#condicion tbody').find('tr');
        //PopupPosition();
        var Condiciones = new Array();
        var idTr = 0;
        var variable_total = "";
        $("#condicion tbody").find("input[type=checkbox]").each(function () {
            if ($(this).prop('checked')) {
                var idTr = $(this).attr('id');
                var accionescorrect = new Object();
                accionescorrect.sedeVM = parseInt($(this).attr('idsede'));
                accionescorrect.procesoVM = parseInt($(this).attr('idproceso'));
                accionescorrect.resumenvm = $(this).attr('resumen');
                accionescorrect.DescribeProcesoVM = $(this).attr('desc');
                Condiciones.push(accionescorrect);
            }
        });

        if (Condiciones.length > 0) {

            for (var i = 0; i < Condiciones.length; i++)
                if (variable_total == "") {
                    variable_total = Condiciones[i].resumenvm
                }
                else {
                    variable_total = variable_total + "," + Condiciones[i].resumenvm
                }

            //Traer datos al modelo JSON
            var stringArray = new Array();
            stringArray[0] = variable_total;
            var listaHallazgo = new Array();
            var hallazgo = new Object();
            hallazgo.Halla_Descripcion = stringArray[0];
            listaHallazgo.push(hallazgo);

            // Construir objeto JSON
            var EDAccion = {
                //Halla_Sede: stringArray[0],
                HallazgoLista: listaHallazgo
            };

            //PopupPosition();
            $.ajax({
                type: "POST",
                url: '/PlandeInspeccion/PostGuardar1',
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(EDAccion),
                success: function (data) {
                    //OcultarPopupposition();
                    $('input[type="checkbox"]').each(function () {
                        if ($(this).prop('checked')) {
                            $(this).closest('tr').remove();
                        }
                    });

                    if (data.probar == false) {
                        //No guardo
                        swal("Estimado Usuario", data.status);
                    }
                    else {
                        //Guardo
                        swal({
                            title: "Estimado Usuario",
                            text: "Se ha generado Acccion Correctiva No. :" + data.Idaccionmostrar + " , Consultar el Modulo de Acciones Correctivas para completar el diligenciamiento",
                            type: "success",
                            confirmButtonColor: '#7E8A97',
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false
                        },
                    function (isConfirm) {
                        if (isConfirm) {
                            $('.confirm').on('click', function () {
                                window.location.href = "../PlandeInspeccion/ListaPlanAccionesParaCorrectivas";
                            });
                        }
                    });
                    }
                },
                error: function (data) {
                    OcultarPopupposition();
                }
            });

        }
        else {
            swal({ title: "Estimado Usuario", text: "Debe seleccionar mínimo un Item de la lista", type: "warning", confirmButtonColor: '#7E8A97' })
        }
    }
}





///funcion para listar las condiciones Inseguras por Inspeccion.
$("#btnlistadocondiciones").click(function (e) {
    PopupPosition();
    e.preventDefault();
    $.ajax({
        url: urlBase + '/PlandeInspeccion/ListarCondicionesPorInspeccion',  //controlador/metodo que esta en el controlador
        data: {},
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            window.location.href = "../PlandeInspeccion/listarCondicionesPorInspeccion";

        },
        error: function (result) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'La Inspección no tiene Condiciones Inseguras registradas',
                confirmButtonColor: '#7E8A97'
            });
        }
    });
});

$("#btnlistadoplanes").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: urlBase + '/PlandeInspeccion/ListarPlanesAccion',  //controlador/metodo que esta en el controlador
        data: {},
        type: 'POST',
        success: function (result) {
            if (result) {
                $("#ModalPlanes").modal("show");
                $('#tablaPlanes').empty();
                $('#tablaPlanes').append
                //('<table class="table table-bordered table-hover"><tr class="titulos_tabla"><td><b>Descripción Condición</b></td> <td><b>Actividad</b></td><br> <td><b>Responsable</b></td> <td><b>Fecha Final</b></td><td><b>Estado</b></td> </tr></table>');
                $.each(result.Data, function (ind, element) {
                    var fechaConvertidas = moment(result.Data[ind].FechaFinPlanAccionED).format("DD/MM/YYYY");
                    var elemento = '<tr name="paci">' +
                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="CondicionesInsegurasED" value="' + element.CondicionesInsegurasED + '">' + element.CondicionesInsegurasED + '</td>' +
                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="ActividadPlanAccionInspeccionED" value="' + element.ActividadPlanAccionInspeccionED + '">' + element.ActividadPlanAccionInspeccionED + '</td>' +
                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="ResponsablePlanAccionED" value="' + element.ResponsablePlanAccionED + '">' + element.ResponsablePlanAccionED + '</td>' +
                                    '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="FechaFinPlanAccionED" value="' + fechaConvertidas + '">' + fechaConvertidas + '</td>' +
                                    '<td style="background-color:#229954;color:white;vertical-align:middle;border:solid 1px #808080; text-align:center;font-size:15px"><input type="hidden" name="EstadoPlanAccionED" value="' + element.EstadoPlanAccionED + '">' + element.EstadoPlanAccionED + '</td>' +
                                   '</tr>'
                    $('#tablaPlanes').append(elemento)
                    $("#nuevainspeccion").css("display", "block");
                })
                paginador("#tablaPlanes", "tr[name = paci]", "#paginador5")
            }
        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presento un error, intente mas tarde',
                confirmButtonColor: '#7E8A97'
            });

        }

    });
});

///Funcion que permite grabar la lista de Actividades que Requieren Accion Correctiva o Preventiva

$("#btncorrectivas").click(function (e) {
    e.preventDefault();
    var seleccionados = new Array();
    var selected = '';
    var buscaCheck =
    $("#tablaPlanes").find("input[type=checkbox]").each(function () {
        if ($(this).prop('checked')) {
            var idTr = $(this).attr('class');
            var accionescorrect = new Object();
            accionescorrect.pkplan = $('#pkplan' + idTr).val();
            accionescorrect.pkplanea = $('#pkplanea' + idTr).val();
            accionescorrect.actividad = $('#actividad' + idTr).val();
            seleccionados.push(accionescorrect);
        }
    });
    var acciones = {
        verificador: $('#verificador').val(),
        seguimiento: $('#seguimiento').val(),
        accioncorrectiva: $('#accioncorrectiva').val(),
        correctivas: seleccionados,
    }
    if (seleccionados.length > 0 & acciones.verificador != "" & acciones.accioncorrectiva != "" & acciones.seguimiento != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/PlandeInspeccion/GuardarPlanAccionesCorrectivas',  //controlador/metodo que esta en el controlador
            data: acciones,
            type: 'POST',
            success: function (result) {
                if (result.Data.length > 0) {
                    OcultarPopupposition();
                    $("#tablaPlanes").find("input[type=checkbox]").each(function () {
                        if ($(this).prop('checked')) {
                            $(this).closest('tr').remove();
                        }
                    });
                    swal({
                        title: "Estimado Usuario",
                        text: "Información registrada satisfactoriamente",
                        type: "success",
                        showCancelButton: false,
                        confirmButtonColor: '#7E8A97',
                        confirmButtonText: "Continuar",
                        cancelButtonText: "Cancelar",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    },
                     function (isConfirm) {
                         if (isConfirm) {
                             window.location.href = "../PlandeInspeccion/ListaPlanAccionesParaCorrectivas";

                         } else {
                             swal("Cancelar", "Terminar:)", "error");
                         }
                     });
                    $("#resultadoverificar").css("display", "none");
                    //$formularioparacorrectiva.html(result);
                    OcultarPopupposition();
                }
                if (result.Data == 0) {

                    swal({ title: "Estimado Usuario", text: "El Item seleccionado fue registrado anteriormente", type: "warning", confirmButtonColor: '#7E8A97' })
                    OcultarPopupposition();
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó, un error, intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
    else {
        swal({ title: "Estimado Usuario", text: "Debe seleccionar mínimo una Condición Insegura de la lista e ingresar los datos solicitados", type: "warning", confirmButtonColor: '#7E8A97' })

    }
});









///Funcion para Verificar los planes de Accion de la Inspección
function BuscarPlan() {
    validarbuscarplan();
    if ($("#verificarInspeccion").valid()) {
        PopupPosition();
        var fechain = $("#FechaInicial").val();
        var fechafin = $("#FechaFinal").val();
        if (fechain != "" && fechafin != "") {
            if ($.datepicker.parseDate('dd/mm/yy', fechafin) < ($.datepicker.parseDate('dd/mm/yy', fechain))) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'La Fecha Fin no puede ser menor que la Fecha Inicial',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
                return false;
            }
            else {
                var plan = {
                    IdSede: $("#idSede").val(),
                    FechaIniVer: $("#FechaInicial").val(),
                    FechaFinVer: $("#FechaFinal").val(),
                };
                $.ajax({
                    url: urlBase + '/PlandeInspeccion/BuscaInspeccionRangoFechas',  //controlador/metodo que esta en el controlador
                    data: plan,
                    type: 'POST',
                    success: function (result) {
                        OcultarPopupposition();
                        if (result.Data.length > 0) {
                            $("#resultadoverificar").slideDown();
                            //$("#resultadoverificar").trigger("reset");
                            $('#tablaPlanes').empty();
                            $('#tablaPlanes').append
                            //('<table class="table table-bordered table-hover"><tr class="titulos_tabla"><td>Seleccionar</td><td>Consecutivo Inspección</td><td><b>Actividad</b></td><br> <td><b>Responsable</b></td> <td><b>Fecha Final</b></td><td><b>Estado</b></td> </tr></table>');
                            var contador = 0;
                            $.each(result.Data, function (ind, element) {
                                if (result.Data[ind].FechaCierrePlanAccionED != null) {
                                    var fechaConvertida = moment(result.Data[ind].FechaCierrePlanAccionED).format("DD/MM/YYYY");
                                }
                                else {
                                    fechaConvertida = "<b style='color:#FF7500;font-size:16px'>PLAN SIN CERRAR</b>"
                                }
                                var fechaConvertidas = moment(result.Data[ind].FechaFinPlanAccionED).format("DD/MM/YYYY");
                                if (element.EstadoPlanAccionED == "Abierto-Vigente" || element.EstadoPlanAccionED == "Cerrado-Vigente") {
                                    var elemento = '<tr name="paci">' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="Planes" class = ' + contador + ' >' + '</td>' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="ConsecutivoPlanInspeccionED" id = "Consecutivoplanea' + contador + '" value="' + element.ConsecutivoPlanInspeccionED + '">' + element.ConsecutivoPlanInspeccionED + '</td>' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="PkPlanAccionInspeccionED" id="pkplan' + contador + '"value="' + element.PkPlanAccionInspeccionED + '"><input type="hidden" name="ActividadPlanAccionInspeccionED" id = "actividad' + contador + '" value="' + element.ActividadPlanAccionInspeccionED + '">' + element.ActividadPlanAccionInspeccionED + '</td>' +
                                   '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="ResponsablePlanAccionED"  id = "reponsable' + contador + '"value="' + element.ResponsablePlanAccionED + '">' + element.ResponsablePlanAccionED + '</td>' +
                                   '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="FechaFinPlanAccionED" id = "fechafin' + contador + '" value="' + fechaConvertidas + '">' + fechaConvertidas + '</td>' +
                                   '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="fechaConvertida" id = "fechaConvertida' + contador + '" value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                   '<td style="color:white;background-color:#229954;border:solid 1px #808080"><input type="hidden" name="EstadoPlanAccionED" id = "estado' + contador + '" value="' + element.EstadoPlanAccionED + '">' + element.EstadoPlanAccionED + '</td>' +
                                  '</tr>'
                                    $('#tablaPlanes').append(elemento)
                                    contador = contador + 1;
                                }
                                else {
                                    if (element.EstadoPlanAccionED == "Abierto-Vencido" || element.EstadoPlanAccionED == "Cerrado-Vencido") {
                                        var elemento = '<tr name="paci">' +
                                       '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="Planes" class = ' + contador + ' >' + '</td>' +
                                       '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="ConsecutivoPlanInspeccionED" id = "Consecutivoplanea' + contador + '" value="' + element.ConsecutivoPlanInspeccionED + '">' + element.ConsecutivoPlanInspeccionED + '</td>' +
                                       '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="PkPlanAccionInspeccionED" id="pkplan' + contador + '"value="' + element.PkPlanAccionInspeccionED + '"><input type="hidden" name="ActividadPlanAccionInspeccionED" id = "actividad' + contador + '" value="' + element.ActividadPlanAccionInspeccionED + '">' + element.ActividadPlanAccionInspeccionED + '</td>' +
                                       '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="ResponsablePlanAccionED"  id = "reponsable' + contador + '"value="' + element.ResponsablePlanAccionED + '">' + element.ResponsablePlanAccionED + '</td>' +
                                       '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="FechaFinPlanAccionED" id = "fechafin' + contador + '" value="' + fechaConvertidas + '">' + fechaConvertidas + '</td>' +
                                      '<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center"><input type="hidden" name="fechaConvertida" id = "fechaConvertida' + contador + '" value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                       '<td style="color:white;background-color:#F3333F;border:solid 1px #808080""><input type="hidden" name="EstadoPlanAccionED" id = "estado' + contador + '" value="' + element.EstadoPlanAccionED + '">' + element.EstadoPlanAccionED + '</td>' +
                                      '</tr>'
                                        $('#tablaPlanes ').append(elemento)
                                        contador = contador + 1;
                                    }
                                }
                            });
                            paginador("#tablaPlanes", "tr[name = paci]", "#paginador3")

                        } if (result.Data.length == 0) {
                            swal({ title: "Estimado Usuario", text: "No se encontraron registros", type: "warning", confirmButtonColor: '#7E8A97' },
                              function () {
                                  $("#resultadoverificar").slideUp();
                              });
                        }
                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presento un error, intente mas tarde',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                        $("#resultadoverificar").remove();
                    }
                });
            }



        }

    }
}






/////Funcion para seleccionar las herramientas segun un tipo de Elemento Buscar por Tipo EHM
function jsFunction() {
    var myselect = document.getElementById("TipoElemento");
    var idselect = myselect.options[myselect.selectedIndex].value;

    if (idselect != "Todos") {
        $('#ListaEHM').find('option').each(function () {
            var option = $(this);
            option.css("display", "none");
        });

        $('#ListaEHM').find('option').each(function () {
            var option = $(this);
            var tipo = option.attr('tipo');
            if (tipo == idselect) {
                option.css("display", "block");
            }
            else {
                option.css("display", "none");
            }
        });
    }
    else {
        $('#ListaEHM').find('option').each(function () {
            var option = $(this);
            option.css("display", "block");
        });
    }

}




//Funcion para validar que ingresen los datos correctos de Busqueda en Verificar las Inspecciones.
function validarbuscarplan() {
    $("#verificarInspeccion").validate({
        errorClass: "error",
        rules: {
            idSede: {
                required: true
            },
            FechaInicial: {
                required: true
            },
            FechaFinal: {
                required: true
            },

        },
        messages: {
            idSede: {
                required: "Seleccione una Sede"
            },
            FechaInicial: {
                required: "Seleccione Fecha Inicial, no puede estar vacía"
            },
            FechaFinal: {
                required: "Seleccione Fecha Final, no puede estar vacía"
            },


        }
    });

}


function validarcheckcondiciones() {
    $("#listadocondiciones").validate({
        errorClass: "error",
        rules: {
            checkcondicion: { required: true },
        },
        messages: {
            checkcondicion: { required: "Debe seleccionar una Condición para Generar Plan de Acción" }
        },


    });
}

//funcion para Validar que todos los campos esten Diligenciados en la Creacion de la Inspeccion
function FormularioGenerarInspeccion() {
    $("#generarcondicion").validate({
        errorClass: "error",
        rules: {
            RazonSocial: {
                required: true
            },
            idempresa: {
                required: true
            },
            idplaninspeccion: {
                required: true
            },
            idSede: {
                required: true
            },
            idProceso: {
                required: true
            },

            TipoElemento: {
                required: true
            },
            ListaEHM: { required: true },
            responsableplaninspeccion: {
                required: true
            },
            fechaplaninspeccion: {
                required: true
            },
            hora: {
                required: true
            },
            FechaR: {
                required: true
            },
            area: {
                required: true
            },
            responsable: {
                required: true
            },
            asistente: {
                required: true
            },
            DescribeInspeccion: {
                required: true
            },
            item_diasdesde: {
                required: true
            },
        },
        messages: {
            RazonSocial: {
                required: "Campo no puede estar vacío"
            },
            idempresa: {
                required: "Campo no puede estar vacío"
            },
            idplaninspeccion: {
                required: "Campo no puede estar vacío"
            },
            idSede: {
                required: "Seleccione una Sede"
            },
            idProceso: {
                required: "Seleccione un Proceso"
            },
            responsableplaninspeccion: {
                required: "Campo no puede estar vacío"
            },

            TipoElemento: { required: "Seleccione un tipo de elemento" },
            ListaEHM: { required: "Seleccione un Item" },
            fechaplaninspeccion: {
                required: "Campo no puede estar vacío"
            },
            hora: {
                required: "Campo no puede estar vacío"
            },
            FechaR: {
                required: "Campo no puede estar vacío"
            },
            area: {
                required: "Campo no puede estar vacío"
            },
            responsable: {
                required: "Campo no puede estar vacío"
            },
            asistente: {
                required: "Campo no puede estar vacío"
            },
            DescribeInspeccion: {
                required: "Campo no puede estar vacío"
            },
            item_diasdesde: {
                required: "Campo no puede estar vacío"
            },
        }
    });
}


//Funcion para consultar las clases de peligros por cada tipo de peligro
function ConsultarClasesPeligro(idtipoPeligro) {

    var $idtipoPeligro = $("#tipopeligro");
    var $idClasePeligro = $("#FK_Clasificacion_De_Peligro");
    var $inputOtro = $("#inputOtro");
    var $inputClasificacion = $("#inputClasificacion");
    if (IDOTRO == $idtipoPeligro.val()) {
        $inputOtro.removeAttr("hidden");
        $inputClasificacion.attr("hidden", "hidden");
        $idClasePeligro.find("option").remove();//Removemos las opciones anteriores 
        $idClasePeligro.append(new Option("Otro", 46));//agregamos la opcion de otro

    }
    else {
        $inputOtro.attr("hidden", "hidden");
        $inputClasificacion.removeAttr("hidden");
        $.ajax({
            type: 'GET',
            url: urlBase + '/PlandeInspeccion/ConsultarClasesPeligro',
            data: { Pk_Tipo_Peligro: idtipoPeligro },
            success: function (result) {

                if (result) {
                    $("#FK_Clasificacion_De_Peligro").empty();
                    $("#FK_Clasificacion_De_Peligro").append("<option value=\"\">--Seleccionar Clasificacion Peligro--</option>");
                    $.each(result, function (k, v) {
                        $("#FK_Clasificacion_De_Peligro").append("<option value=\"" + v.PK_ClasesPeligros + "\">" + v.ClasesDescription + "</option>");
                    });
                }
            }
        });
    }
}

function insertarClasesPeligros() {
    var textoPeligro = $idClasePeligro.find("option:selected").text();
    $("input[name = VisualizadorClasificacion]").each(function (ind, element) {
        $(element).val(textoPeligro);
    });
}

function EliminarGestionDelCambio() {
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    if (selectedIDs != 0) {

        $.ajax({
            url: urlBase + '/GestionDelCambio/Eliminargestcabscript',
            data: {
                idgestion: selectedIDs
            },
            type: 'POST',
            success: function (result) {
                if (result.success) {
                    $('input:checkbox.checkBox').each(function () {
                        if ($(this).prop('checked')) {

                            $(this).closest('tr').remove();
                            //utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje'));
                            swal({
                                type: 'success',
                                title: 'Estimado Usuario',
                                text: 'El registro se eliminó satisfactoriamente',
                                timer: 4000,
                                confirmButtonColor: '#7E8A97'
                            })
                        }
                    });
                }

            }
        });
    }
    else {

    swal({
        type: 'warning',
        title: 'Estimado Usuario',
        text: 'Por favor seleccione un registro',
        timer: 4000,
        confirmButtonColor: '#7E8A97'
    })
}
}










$("#listaactividades").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: urlBase + '/PlandeInspeccion/ListaTodasLasCorrectivas',  //controlador/metodo que esta en el controlador
        data: {},
        type: 'POST',
        success: function (result) {

            if (result) {
                $('#todasCorrectivas').empty();
                $('#todasCorrectivas').append
                //('<table class="table table-bordered table-hover"><tr class="titulos_tabla"><br> <td><b>Nombre Verificador</b></td> <td><b>Actividad Plan Acción</b></td> <td><b>Requiere Acción Correctiva?</b></td></table>');
                $.each(result.Data, function (ind, element) {
                    var elemento = '<tr name="pact">' +

                                    //'<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="PkplanAccionCorrectivaED" value="' + element.PkplanAccionCorrectivaED + '">' + element.PkplanAccionCorrectivaED + '</td>' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:white"><input type="hidden" name="DescribeCondicionED" value="' + element.DescribeCondicionED + '">' + element.DescribeCondicionED + '</td>' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:white"><input type="hidden" name="NombreVerificadorED" value="' + element.NombreVerificadorED + '">' + element.NombreVerificadorED + '</td>' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:white"><input type="hidden" name="InformacionActividadED" value="' + element.InformacionActividadED + '">' + element.InformacionActividadED + '</td>' +
                                    '<td style="border:solid 1px #808080;color:black;background-color:white"><input type="hidden" name="RespuestaED" value="' + element.RespuestaED + '">' + element.RespuestaED + '</td>' +

                                   '</tr>'
                    $('#todasCorrectivas').append(elemento)
                })
                paginador("#todasCorrectivas", "tr[name = pact]", "#paginador1")
            }



        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error, intente más tarde',
                confirmButtonColor: '#7E8A97'
            });

        }
    });
});
function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        //language: {
        //        paginate: {
        //            next: '&#8594;'; // or '→'
        //            previous: '&#8592;' // or '←' 
        //        }
        //}
        // Consider adding an ID to your table
        // incase a second table ever enters the picture.
        var items = $(idTabla).find(nameTr);
        var numItems = items.length;
        var perPage = 5;

        // Only show the first 10 (or first `per_page`) items initially.
        items.slice(perPage).hide();

        // Now setup the pagination using the `.pagination-page` div.
        $(pagination).pagination({

            prevText: '<i class="fa fa-chevron-left"><i class="fa fa-chevron-left">',
            nextText: '<i class="fa fa-chevron-right"><i class="fa fa-chevron-right">',
            items: numItems,
            itemsOnPage: perPage,
            cssStyle: "compact-theme",

            // This is the actual page changing functionality.
            onPageClick: function (pageNumber) {
                // We need to show and hide `tr`s appropriately.
                var showFrom = perPage * (pageNumber - 1);
                var showTo = showFrom + perPage;

                // We'll first hide everything...
                items.hide()
                     // ... and then only show the appropriate rows.
                     .slice(showFrom, showTo).show();
            }
        });

        // EDIT: Let's cover URL fragments (i.e. #page-3 in the URL).
        // More thoroughly explained (including the regular expression) in:
        // https://github.com/bilalakil/bin/tree/master/simplepagination/page-fragment/index.html

        // We'll create a function to check the URL fragment
        // and trigger a change of page accordingly.
        function checkFragment() {
            // If there's no hash, treat it like page 1.
            var hash = window.location.hash || "#page-1";

            // We'll use a regular expression to check the hash string.
            //hash = hash.match(/^#page-(\d+)$/);
            //if (hash) {
            //    // The `selectPage` function is described in the documentation.
            //    // We've captured the page number in a regex group: `(\d+)`.
            //    $(pagination).pagination("selectPage", parseInt(hash[1]));
            //}
        };

        // We'll call this function whenever back/forward is pressed...
        $(window).bind("popstate", checkFragment);

        // ... and we'll also call it when the page has loaded
        // (which is right now).
        checkFragment();
    });
}
$('#checkAll').click(function () {
    $('input:checkbox').prop('checked', this.checked);
});





function BuscarClasificacionPeligro(PKGestionDelCambio) {
    $.ajax({
        url: urlBase + '/GestionDelCambio/BuscarClasificacionPeligroDescripcion',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            //Fk_Sede: $idSede.val()

            PKGestionDelCambio: PKGestionDelCambio,

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $("#FK_Clasificacion_De_Peligro").val(result.Municipio.Descripcion_Clase_De_Peligro);
                $("#FK_Tipo_De_Peligro").val(result.Municipio.Descripcion_Del_Peligro);

            }
        }
    });
}




function validarGestionDelCambioAgregar() {
    var hoy = new Date();
    var fecha = $("#idFecha").val()
    var FechaSeguimiento = $("#idFechaSeguimiento").val()
    var dia = hoy.getDate();
    var mes = hoy.getMonth() + 1;
    var anio = hoy.getFullYear();
    var FechaEjecucion = $("#idFechaEjecucion").val()

    if (mes < 10) {
        fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
    } else {

        fecha_actual = String(dia + "/" + mes + "/" + anio);
    }
    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fecha) | $.datepicker.parseDate('dd/mm/yy', fecha_actual) > $.datepicker.parseDate('dd/mm/yy', fecha) && $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento) != "") {


        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'La fecha de registro debe corresponder a la fecha  actual',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFecha").val("")
        $("#idFecha").focus();
}
    else
    if ($.datepicker.parseDate('dd/mm/yy', fecha) > $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento) && $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento) != "") {


        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'La fecha de seguimiento no debe ser inferior a la fecha de registro ',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFechaSeguimiento").val("")
        $("#idFechaSeguimiento").focus();
    }
    else
        if ($.datepicker.parseDate('dd/mm/yy', FechaEjecucion) < $.datepicker.parseDate('dd/mm/yy', fecha) | $.datepicker.parseDate('dd/mm/yy', FechaEjecucion) > $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento)) {


            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de ejecución debe ser mayor a la fecha de registro y menor a la fecha de seguimiento ',
                confirmButtonColor: '#7E8A97'
            });
            $("#idFechaEjecucion").val("")
            $("#idFechaEjecucion").focus();
        }





    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
    }, "Solo se permite el ingreso de letras");

    $("#idAgregarGestionDelCambio").validate({

        //errorClass: "error",
        rules: {

            Fecha: { required: true },
            DescripcionDeCambio: { required: true},
            FK_Tipo_De_Peligro: { required: true},
            FK_Clasificacion_De_Peligro: { required: true},
            RequisitoLegal: { required: true},
            Recomendaciones: { required: true},
            FechaEjecucion: { required: true},
            FechaSeguimiento: { required: true},
            FK_Id_Rol: { required: true}


        },
        messages: {

            Fecha: {
                required: "Se debe ingresar la fecha de registro"
            },
            DescripcionDeCambio: {
                required: "Se debe ingresar la descripción del cambio",
                //maxlength: "La longitud máxima es de 200 caracteres",
            },
            FK_Tipo_De_Peligro: {
                required: "Se debe ingresar la clasificación del peligro",
             
                //maxlength: "La longitud máxima del numero es de 100 caracteres",
            },


            FK_Clasificacion_De_Peligro: {
                required: "Se debe ingresar el tipo de peligro",
                //maxlength: "La longitud máxima es de 100 caracteres",
            },


            RequisitoLegal: {
                required: "Se debe ingresar el requisito legal",
                //maxlength: "La longitud máxima es de 200 caracteres",
            },

            Recomendaciones: {
                required: "Se debe ingresar las recomendaciones",
                //maxlength: "La longitud máxima es de 1000 caracteres",
            },

            FechaEjecucion: {
                required: "Se debe ingresar la fecha de ejecución"
            },

            FechaSeguimiento: {
                required: "Se debe ingresar la fecha de seguimiento",
            },

            FK_Id_Rol: {
                required: "Se debe ingresar a quien se le va comunicar el cambio",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },





        }

    });

}







function validarGestionDelCambioModificar() {
    var hoy = new Date();
    var fecha = $("#idFecha").val()
    var FechaSeguimiento = $("#idFechaSeguimiento").val()
    
    var FechaEjecucion = $("#idFechaEjecucion").val()


    var dia = hoy.getDate();
    var mes = hoy.getMonth() + 1;
    var anio = hoy.getFullYear();

    if (mes < 10) {
        fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
    } else {

        fecha_actual = String(dia + "/" + mes + "/" + anio);
    }
    //if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fecha) | $.datepicker.parseDate('dd/mm/yy', fecha_actual) > $.datepicker.parseDate('dd/mm/yy', fecha)) {


    //    swal({
    //        type: 'warning',
    //        title: 'Estimado usuario:',
    //        text: 'Señor usuario recuerde que la fecha ingresada deber corresponder a la fecha  actual',
    //        confirmButtonColor: '#7E8A97'
    //    });
    //    $("#idFecha").val("")
    //}
    //else
    if ($.datepicker.parseDate('dd/mm/yy', fecha) > $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento) && $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento) !="") {


        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'La fecha de seguimiento no debe ser inferior a la fecha de registro ',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFechaSeguimiento").val("")
    }
    else 
        if ($.datepicker.parseDate('dd/mm/yy', FechaEjecucion) < $.datepicker.parseDate('dd/mm/yy', fecha) | $.datepicker.parseDate('dd/mm/yy', FechaEjecucion) > $.datepicker.parseDate('dd/mm/yy', FechaSeguimiento)) {


            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de ejecución debe ser mayor a la fecha de registro y menor a la fecha de seguimiento ',
                confirmButtonColor: '#7E8A97'
            });
            $("#idFechaEjecucion").val("")
        }


    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
    }, "Solo se permite el ingreso de letras");

    $("#idModificarDelCambio").validate({

        //errorClass: "error",
        rules: {

            Fecha: { required: true },
            DescripcionDeCambio: { required: true},
            FK_Tipo_De_Peligro: { required: true},
            FK_Clasificacion_De_Peligro: { required: true},
            RequisitoLegal: { required: true},
            Recomendaciones: { required: true},
            FechaEjecucion: { required: true },
            FechaSeguimiento: { required: true },
            FK_Id_Rol: { required: true}


        },
        messages: {

            Fecha: {
                required: "Se debe ingresar la fecha de registro"
            },
            DescripcionDeCambio: {
                required: "Se debe ingresar la descripción del cambio",               
            },
            FK_Tipo_De_Peligro: {
                required: "Se debe ingresar la clasificación del peligro",              
            },


            FK_Clasificacion_De_Peligro: {
                required: "Se debe ingresar el tipo de peligro",
                //maxlength: "La longitud máxima es de 100 caracteres",
            },


            RequisitoLegal: {
                required: "Se debe ingresar el requisito legal",
                //maxlength: "La longitud máxima es de 200 caracteres",
            },

            Recomendaciones: {
                required: "Se debe ingresar las recomendaciones",
                //maxlength: "La longitud máxima es de 1000 caracteres",
            },

            FechaEjecucion: {
                required: "Se debe ingresar la fecha de ejecución"
            },

            FechaSeguimiento: {
                required: "Se debe ingresar la fecha de seguimiento",
            },

            FK_Id_Rol: {
                required: "Se debe ingresar a quien se le va comunicar el cambio",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },





        }

    });

}




////Funcion para consultar las clases de peligros por cada tipo de peligro
//function ConsultarClasesPeligrosAplicacion(idClasificacion) {
//    var $inputOtro = $("#inputOtro");
//    var $inputClasificacion = $("#inputClasificacion");
//    if (IDOTRO == $idtipoPeligro.val()) {
//        $inputOtro.removeAttr("hidden");
//        $inputClasificacion.attr("hidden", "hidden");
//        $idClasePeligro.find("option").remove();//Removemos las opciones anteriores 
//        $idClasePeligro.append(new Option("CampoOtro", 46));//agregamos la opcion de otro
//    }
//    else {
//        $inputOtro.attr("hidden", "hidden");
//        $inputClasificacion.removeAttr("hidden");
//        $.ajax({
//            url: urlBase + '/ClasificacionDePeligros/ConsultarClasesPeligros',
//            data: {
//                Pk_Tipo_Peligro: $idtipoPeligro.val()
//            },
//            type: 'POST',
//            success: function (result) {
//                if (result) {
//                    $idClasePeligro.find("option").remove();//Removemos las opciones anteriores 
//                    $idClasePeligro.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar
//                    //  $nameTipoDeCalificacion.prop("checked", "");

//                    $.each(result, function (ind, element) {
//                        $idClasePeligro.append(new Option(element.ClasesDescription, element.PK_ClasesPeligros));//agregamos las opciones consultadas
//                    })
//                    var textoPeligro = $idtipoPeligro.find("option:selected").text();
//                    $("input[name = VisualizadorPeligro]").each(function (ind, element) {
//                        $(element).val(textoPeligro);
//                    });
//                    if (idClasificacion != "") {
//                        $("#FK_Clasificacion_De_Peligro").val(idClasificacion);// nueva linea

//                    }
//                }
//            }
//        });
//    }
//}


ConstruirDatePickerPorElemento('idFecha');
ConstruirDatePickerPorElemento('idFechaEjecucion');
ConstruirDatePickerPorElemento('idFechaSeguimiento');