


///Funcion que me permite validar que se haya seleccionado un archivo
function ValidarManualesAdq() {
    $("#FormularioManualesAdq").validate({
        rules: {
            File:
            {
                required: true
            }
        },
        messages: {
            File:
            {
                required: "Se debe seleccionar una archivo"
            }
        }
    });
}

//funcion que me permite eliminar un manual de adquisicion de bienes
function EliminarManualAdq(element, PK_ManualGuiaAdBienes) {
    $div = $(element).closest("tr");
    $.ajax({
        url: urlBase + '/AdquisicionBienes/EliminarManualAdqBienes',
        data: {
            idManualAdq: PK_ManualGuiaAdBienes
        },
        type: 'POST',
        success: function (result) {

            if (result.success) {
                $("#modalesEliminados").append($("#modalEliminar" + PK_ManualGuiaAdBienes));
                $div.remove();

                swal(
                    'Estimado Usuario',
                    'Se ha eliminado el Manual de Adquisición de Bienes o Contrataciones',
                    'success'
                    )
            } else {
                swal(
                   'Estimado Usuario',
                   'No ha sido posible eliminar el Manual de Adquisición de Bienes o Contrataciones',
                   'warning'
                   )
            }
        }
    });
}

function ValidarCrearProductoCriterio() {
    $('#grabarProductoCriterio').validate({
        errorClass: "error",
        rules: {
            Tipo_Servicio: { required: true, },
            Pk_Id_Criterio1: { required: true }
        },
        messages: {
            Tipo_Servicio: {
                required: "Se debe ingresar un Tipo de Servicio y Producto"
            },
            Pk_Id_Criterio1: {
                required: "Se debe seleccionar un Criterio"
            }
        }
    });
}

function EditarProductoCriterio(id) {
    utils.showLoading();
    $.ajax({
        url: urlBase + '/CriteriosSST/EditarProductoCriterioSel',
        data: {
            id: id,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $("#panel").html("")
                $("#panel").html(result)
                $("#Tipo_Servicio").focus()
            }
        }
    });
}

//funcion para guardar la edicion del Producto por los CriteriosSST
function EdicionProductoCriterio() {
    utils.showLoading();
    var productocriterio = new Object();
    productocriterio.Tipo_Servicio = $("#Tipo_Servicio").val()
    productocriterio.Pk_id_Tipo_Servicio = $("#Pk_Id_Tipo_Servicio").val()
    productocriterio.CritAnteriores = $("#IdCriteriosAnteriores").val()
    productocriterio.Pk_Id_Criterio1 = $("#IdCriterios").val()
    $.ajax({
        url: urlBase + '/CriteriosSST/EdicionProductoPorCriterios',
        data: {
            productocriterio: productocriterio,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                
                swal({
                    type: 'success',
                    title: 'Estimado usuario:',
                    text: 'Tipo de Servicio y Producto editado satisfactoriamente',
                    timer: 5000,
                    confirmButtonColor: '#7E8A97'
                });
                //window.location.reload(true);
            }
            else {
                swal(
                'Estimado Usuario',
                'Tipo de Servicio y Producto NO se pudo editar',
                'error'
                )
            }
        }
    });
}

function consultarProductosPorCriterio() {
    var idProducto = $("#Pk_Id_Productos");
    var criterios = 1;
    var esta = 0;
    var seleccionado = $("#Pk_Id_Productos").val();
    var acti = $("#TablaProcuctoCriter td#id_servicioProdu").length;
    if (acti != 0) {
        $("#TablaProcuctoCriter td#id_servicioProdu").each(function (ind, element) {
            var cero = 0;
            if ($(element).find("input").val() == seleccionado) {
                esta = esta + 1;
                return;
            }
        });
        parseInt(esta, 10);
        if (esta == 0) {
            $.ajax({
                url: urlBase + '/CriteriosSST/ObtenerProductoPorCriterios',
                data: {
                    Pk_Id_Producto: idProducto.val()
                },
                type: 'POST',
                success: function (result) {
                    if (result) {
                        $("#TablaProcuctoCrit").append(result);
                        calificacionProveedor();
                    } else {
                        swal(
                           'Estimado Usuario',
                           'No ha sido posible cargar el Producto y los Criterios',
                           'error'
                           )
                    }
                }
            });
        } else {
            swal(
                'Estimado Usuario',
                'Ese Producto ya ha sido calificado',
                'error'
                )                   
        }
    } else {
        $.ajax({
            url: urlBase + '/CriteriosSST/ObtenerProductoPorCriterios',
            data: {
                Pk_Id_Producto: idProducto.val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $("#TablaProcuctoCrit").append(result);
                    calificacionProveedor();
                } else {
                    swal(
                       'Estimado Usuario',
                       'No ha sido posible cargar el Producto y los Criterios',
                       'error'
                       )
                }
            }
        });
    }   
}

function calificacionProveedor(id) {
    var acti = $("#TablaProcuctoCriter td#nameCrit").length;
    //var producto = $("#")
    var cumple = 0;
    var cumpleProd = 0;
    var esta = 0;
    var seleccionado = 0;
    var cantidad = 0;
    var califica = 0;
    if (id == null)
    {
        seleccionado = $("#Pk_Id_Productos").val()
    }
    else{
        seleccionado = id
    }
    //idServicioProducto
    $("#TablaProcuctoCriter td").find("#ddlViewBy").each(function (ind, element) {
        if ($(element).find("option:selected").text() == "Cumple")
        {
            cumple = cumple + 1;
        }
    })
    result = parseInt(cumple, 10) / parseInt(acti, 10);
    //$("#calif").val(result.toFixed(2) * 100);
    
    $("#TablaProcuctoCriter tr#datos").each(function (ind, element) {
        var cero = 0;
        if ($(element).find("input[name='idServicioProd']").val() == seleccionado) {
            esta = esta + 1;
            //return;
            if ($(element).find("option:selected").text() == "Cumple") {
                cumpleProd = cumpleProd + 1;
            }
        }
    });
    $("#TablaProcuctoCriter td#id_servicioProdu").each(function (ind, element) {
        var cero = 0;
        if ($(element).find("input").val() == seleccionado) {
            resultProd = parseInt(cumpleProd, 10) / parseInt(esta, 10);
            $(element).find("input[name='califProducto']").val(resultProd.toFixed(2) * 100);
        }
    });
    $("#TablaProcuctoCriter td#id_servicioProdu").each(function (ind, element) {
        //var califica = 0;
        var calificacion = $(element).find("input[name='califProducto']").val();
        califica = parseInt(calificacion, 10) + parseInt(califica, 10);
        cantidad = cantidad + 1;
    });
    resultado = parseInt(califica, 10) / parseInt(cantidad, 10);
    $("#calif").val(resultado.toFixed(2));
}



function ValidarGuardarSeleccionEvaluacion() {
    SeleccionEvaluacion();    
    if ($('#grabarSeleccionEvaluacion').valid())
    {            
        $("#TablaProcuctoCrit").find("tr[name='datos']").each(function (ind, fila) {               
            $(fila).find("input[name='IdProductoCriterios']").attr("name", "ListaProCritPorCalf[" + ind + "].IdProductoCriterios");
            $(fila).find("input[name='idServicioProducto']").attr("name", "ListaProCritPorCalf[" + ind + "].idServicioProducto");
            if ($(fila).find("input[name='califProducto']").length != 0)
            {
                $(fila).find("input[name='califProducto']").val($(fila).find("input[name='califProducto']").val().replace(".", ","));
            }
            $(fila).find("input[name='califProducto']").attr("name", "ListaProCritPorCalf[" + ind + "].califProducto");
            $(fila).find("select[name='ddlViewBy']").attr("name", "ListaProCritPorCalf[" + ind + "].ddlViewBy");
        });
        $("#calif").val($("#calif").val().replace(".", ","));
        $("#grabarSeleccionEvaluacion").submit();
    }
}

function EditarSeleccionEvaluacion() {
    SeleccionEvaluacion();
    if ($('#grabarSeleccionEvaluacion').valid()) {
        $("#TablaProcuctoCrit").find("tr[name='datos']").each(function (ind, fila) {
            $(fila).find("input[name='IdProductoCriterios']").attr("name", "ListaProCritPorCalf[" + ind + "].IdProductoCriterios");
            $(fila).find("input[name='idServicioProducto']").attr("name", "ListaProCritPorCalf[" + ind + "].idServicioProducto");
            $(fila).find("select[name='ddlViewBy']").attr("name", "ListaProCritPorCalf[" + ind + "].ddlViewBy");
        });
        $("#calif").val($("#calif").val().replace(".", ","));
        $("#grabarSeleccionEvaluacion").submit();
    }
}

function SeleccionEvaluacion() {
    $('#grabarSeleccionEvaluacion').validate({
        errorClass: "error",
        rules: {
            fechapi: { required: true, },
            nameProveedor: { required: true },
            nitProveedor: { required: true },
            Pk_Id_Productos: { required: true },
            observacion: { required: true },
            files: { required: true },
        },
        messages: {
            fechapi: {
                required: "Debe ingresar una Fecha de Calificación"
            },
            nameProveedor: {
                required: "Debe ingresar un Proveedor"
            },
            nitProveedor: {
                required: "Debe ingresar el Nit del Proveedor"
            },
            Pk_Id_Productos: {
                required: "Debe calificar al menos un Producto"
            },
            observacion: {
                required: "Debe ingresar una Observación"
            },
            files: {
                required: "Debe ingresar un Archivo Anexo"
            }
        }
    });
}

function eliminarTrProducto(value)
{
    $("#TablaProcuctoCriter tr#datos").each(function (ind, element) {       
        if ($(element).find("input[name='idServicioProd']").val() == value) {       
            $(element).remove();
            swal(
                'Estimado Usuario',
                'Tipo de Servicio y Producto removido satisfactoriamente para calificar',
                'success'
                )
        }
    });
    calificacionProveedor();
}

function MostrarGraficoProveedor(elemento,id){
    utils.showLoading();
    var proveedores = new Array();
    var nombre = new Array();
    $.ajax({
        url: urlBase + '/CriteriosSST/MostrarGrafico',
        data: {
            idProveedor: id,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $(elemento).closest("tr").find("tbody[name='TblProveedor']").html("");
                $.each(result.success, function (ind, element) {                                      
                    var $tr = "<tr><td style='border-right: 2px solid lightslategray; vertical-align:middle'>" + element.NumeroCalificion + "</td>";
                    $tr = $tr + '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.fecha + "</td>";
                    $tr = $tr + '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.calif.toFixed(2) + "%" + "</td> </tr>";
                    $(elemento).closest("tr").find("tbody[name='TblProveedor']").append($tr);
                })
                var pc = $(elemento).closest("tr[name='tblProv']").find("canvas[name='graficadetalle']");
                $.each(result.success, function (ind, element) {
                    proveedores.push(element.calif.toFixed(2));
                    nombre.push("Calificación " + element.NumeroCalificion);
                })
                var data = {
                    labels: nombre,
                    datasets: [
                        {
                            label: '# de Calificaciones',
                            data: proveedores,
                            backgroundColor: generarColorAleatorio(result.success.length)
                        }]
                }
                var myPieChart = new Chart(pc, {
                    type: 'bar',
                    data: data,
                    options: {
                        title: {
                            display: true,
                            text: 'Porcentaje Calificación',
                            top: 'bottom',
                            fontSize: 12
                        },
                        scales: {
                            yAxes: [{
                                ticks: {
                                    min: 0,
                                    max: 100,
                                    callback: function (value) {
                                        return value + "%"
                                    }
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: "Porcentaje"
                                }
                            }]
                        }
                    }
                });
            }
        }
    });
}


function generarColorAleatorio(tamaño)
{
    var colores = new Array();
    for (var i = 0; i < tamaño; i++)
    {
        var nivel = 20 + 100 * i;
        colores.push("rgba(" + nivel + "," + nivel + "," + nivel + ",0.5)");        
    }
    return colores;
}

function ValidarGuardarProveedor()
{
    $('#EditarProveedores').validate({
        errorClass: "error",
        rules: {
            fechapi: { required: true, },
            frecuenciaEvaluacion: { required: true },
            CalificacionHistorico: {
                required: true,
                maxlength: 3,
                min: 1
            }
        },
        messages: {
            fechapi: {
                required: "Debe ingresar Vigencia de Contrato"
            },
            frecuenciaEvaluacion: {
                required: "Debe seleccionar una Frecuencia de Evaluación"
            },
            CalificacionHistorico: {
                required: "Debe calificar el Proveedor",
                maxlength: "La longitud máxima del número es de 3 caracteres",
                min: "Solo se permite el ingreso de números Positivos"
            }
        }
    });
}

function validacioCargarAnexos() {
    if (typeof FileReader !== "undefined") {
        var file_list = document.getElementById('files').files;
        for (var i = 0, file; file = file_list[i]; i++) {
            file = file_list[i];
            var fileExtension = file.name.split('.')[file.name.split('.').length - 1].toLowerCase();
            var size = file_list[i].size;            
            if (size > 10800332.8) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe cargar un archivo con peso menor a 10 Mb',
                    //timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
                document.getElementById("files").value = "";
                return false;
            }
            if (fileExtension != "jpg" && fileExtension != "png" && fileExtension != "pdf" && fileExtension != "doc" && fileExtension != "ppt" && fileExtension != "xls" && fileExtension != "docx" && fileExtension != "pptx" && fileExtension != "xlsx")
            {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe cargar un archivo  con extensión valida',
                    //timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
                document.getElementById("files").value = "";
                return false;
            }

        }
    }
}

//funcion que solo permite el ingreso de numero en los campos 
function darFormatoSoloNumeros(idCampo) {
    $('#' + idCampo).on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}

function validaciontamañoManual() {
    if (typeof FileReader !== "undefined") {
        var size = document.getElementById('File').files[0].size;
        var file = document.getElementById('File').files[0];
        var fileExtension = file.name.split('.')[file.name.split('.').length - 1].toLowerCase();
        if (size > 10800332.8) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe cargar un archivo con peso menor a 10 Mb',
                //timer: 4000,
                confirmButtonColor: '#7E8A97'
            })
            document.getElementById("File").value = "";
            return false;
        }
        if (fileExtension != "jpg" && fileExtension != "png" && fileExtension != "pdf" && fileExtension != "doc" && fileExtension != "ppt" && fileExtension != "xls" && fileExtension != "docx" && fileExtension != "pptx" && fileExtension != "xlsx") {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe cargar un archivo  con extensión valida',
                //timer: 4000,
                confirmButtonColor: '#7E8A97'
            })
            document.getElementById("File").value = "";
            return false;
        }
    }
}
