var idActividadPrincipalCont = 0;
var respoEliminadas = new Array();
var rendiEliminadas = new Array();
var contadorActiviadesEditar = 0;
var addRespoCont = 0;
var addRendiCont = 0;
var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};
//funcion para agregar una nueva actividad
function agregarActividad(esActividadEditar) {

    var $filaActividad = $("#filaActividad").clone();
    // $filaActividad.find("input[name=nomAct]").attr("name","nombreActividad");
    $filaActividad.find("input[type=decimal]").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                valor = value.replace(/\D/g, "")
                            .replace(/([0-9])([0-9]{2})$/, '$1,$2')
                            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
                stringValor = "$".concat(valor)
                return stringValor;
            });
        }
    });

    $filaActividad.removeAttr("id");
    if (esActividadEditar)// Preguntamos si la actividad a agregar esta siendo agregada desde el editar el presupuesto
    {
        $filaActividad.find("a").attr("data-target", "#modalActSinGuardar" + contadorActiviadesEditar + "");// modificamos el atributo data target para llamar al modal indicado
        $filaActividad.find("div[name=modalActSinGuardar]").attr("id", "modalActSinGuardar" + contadorActiviadesEditar + "");//  modificamos el atributo name para que pueda ser llamado 
        contadorActiviadesEditar++;
    }

    $("#actividades").append($filaActividad);
}

//funcion  para agrupar actividades
function agruparActividades1(incial) {
    if ($("#principales").val() >= 0) {
        idActividadPrincipalCont = parseInt(idActividadPrincipalCont) + parseInt($("#principales").val());
    }
    var actividadesParaAgrupar = $("#actividades").find("input[name='agruparActividades']:checked");
    if (actividadesParaAgrupar.length > 0) {
        var $filaActividadPrincipal = $("#filaActividadPrincipal").clone();
        $filaActividadPrincipal.attr("name", idActividadPrincipalCont);
        $filaActividadPrincipal.find("a").attr("onclick", "habilitarFilas(" + idActividadPrincipalCont + ")");
        $filaActividadPrincipal.find("input").attr("name", "DescripcionActividad");
        $filaActividadPrincipal.removeAttr("id");

        actividadesParaAgrupar.each(function (ind, element) {
            $rowActividad = $(element).closest("tr");
            $rowActividad.attr("name", idActividadPrincipalCont);

            $("#actividades").prepend($rowActividad);
            $(element).remove();
        });

        $("#actividades").prepend($filaActividadPrincipal);
        idActividadPrincipalCont++;
    }
    else {
        $("#ModalAgrupar").modal("show");
    }

}

///funcion para mostrar las actividades agrupadas
function habilitarFilas(nameFilas) {
    if ($("#actividades").find("tr[name=" + nameFilas + "]:hidden").length == 0) {
        $("#actividades").find("tr[name=" + nameFilas + "]").each(function (ind, element) {
            if (ind > 0) {
                $(element).attr("hidden", "hidden");
            }
        });
    } else {
        $("#actividades").find("tr[name=" + nameFilas + "]").each(function (ind, element) {
            if (ind > 0) {
                $(element).removeAttr("hidden");
            }
        });
    }
}

function GuardarActividades() {
    $("#RubroTotal").val(quitarFormatoNumero($("#RubroTotal").val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor
    for (i = 0; i <= idActividadPrincipalCont; i++) {
        $filasAgrupadas = $("#actividades").find("tr[name=" + i + "]");
        var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {
            if (ind == 0) {
                $(fila).find("input").attr("name", "[" + i + "].DescripcionActividad");
            } else {
                $(fila).find("input[type=text]").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].DescripcionActividad");
                $(fila).find("input[type=decimal]").each(function (ind2, mes) {
                    $(mes).val(quitarFormatoNumero($(mes).val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor
                    $(mes).attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].PresupuestoMes");
                    var numeroMes = ind2 + 1
                    var inputMes = '<input type="hidden" value="' + numeroMes + '" name="[' + i + '].actividadesPresupuesto[' + indexFila + '].presupuestosPorMes[' + ind2 + '].Mes"  />';
                    $(mes).closest("td").append(inputMes);
                });
                indexFila++;
            }
        });
    }
    nombrarActividadesPrincipales();
}

function EditarPresupuesto() {
    $("#RubroTotal").val(quitarFormatoNumero($("#RubroTotal").val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor
    var i = 0;
    $("#actividades").find("tr").each(function (index, tr) {
        $filasAgrupadas = $("#actividades").find("tr[name=" + index + "]");
        var indexFila = 0;
        if ($filasAgrupadas.length > 0) {
            $filasAgrupadas.each(function (ind, fila) {
                if (ind == 0) {
                    $(fila).find("input[name*='DescripcionActividad']").attr("name", "[" + i + "].DescripcionActividad");
                    $(fila).find("input[name*='PK_Actividad_Presupuesto']").attr("name", "[" + i + "].PK_Actividad_Presupuesto");
                } else {
                    $(fila).find("input[name*='DescripcionActividad']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].DescripcionActividad");
                    $(fila).find("input[name*='PK_Actividad_Presupuesto']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].PK_Actividad_Presupuesto");
                    $(fila).find("input[name*='FK_Actividad_Presupuesto']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].FK_Actividad_Presupuesto");
                    $(fila).find("input[type=decimal]").each(function (ind2, mes) {
                        $(mes).val(quitarFormatoNumero($(mes).val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor
                        $(mes).closest("td").find("input[name*='.PresupuestoMes']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].PresupuestoMes");
                        $(mes).closest("td").find("input[name*='.Mes']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].Mes");
                        $(mes).closest("td").find("input[name*='PK_Prepuesto_Por_Mes']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].PK_Prepuesto_Por_Mes");
                        $(mes).closest("td").find("input[name*='FK_Actividad_Presupuesto']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].FK_Actividad_Presupuesto");
                        $(mes).closest("td").find("input[name*='FK_Presupuesto']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].FK_Presupuesto");
                        $(mes).closest("td").find("input[name*='PresupuestoEjecutadoPorMes']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].PresupuestoEjecutadoPorMes");
                        $(mes).closest("td").find("input[name*='ComentarioPresupuestoMesEjecutado']").attr("name", "[" + i + "].actividadesPresupuesto[" + indexFila + "].presupuestosPorMes[" + ind2 + "].ComentarioPresupuestoMesEjecutado");
                    });
                    indexFila++;
                }
            });
            i++;
        }
        if ($(tr).find("input[name='agruparActividades']").length > 0) {
            $(tr).find("input[name*='DescripcionActividad']").attr("name", "[" + i + "].DescripcionActividad");
            $(tr).find("input[name*='PK_Actividad_Presupuesto']").attr("name", "[" + i + "].PK_Actividad_Presupuesto");
            $(tr).find("input[type=decimal]").each(function (ind2, mes) {
                $(mes).val(quitarFormatoNumero($(mes).val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor
                $(mes).closest("td").find("input[name*='.PresupuestoMes']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].PresupuestoMes");
                $(mes).closest("td").find("input[name*='.Mes']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].Mes");
                $(mes).closest("td").find("input[name*='PK_Prepuesto_Por_Mes']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].PK_Prepuesto_Por_Mes");
                $(mes).closest("td").find("input[name*='FK_Actividad_Presupuesto']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].FK_Actividad_Presupuesto");
                $(mes).closest("td").find("input[name*='FK_Presupuesto']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].FK_Presupuesto");
                $(mes).closest("td").find("input[name*='PresupuestoEjecutadoPorMes']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].PresupuestoEjecutadoPorMes");
                $(mes).closest("td").find("input[name*='ComentarioPresupuestoMesEjecutado']").attr("name", "[" + i + "].presupuestosPorMes[" + ind2 + "].ComentarioPresupuestoMesEjecutado");
            });
            i++;
        } else {
            $('#TablaPresupuesto').find("input[type=decimal]").each(function (ind, element) {
                $(element).val(quitarFormatoNumero($(element).val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor
            });

        }
    });



}


///funcion para nombrar el name de las actividades principales
function nombrarActividadesPrincipales() {
    var actividadesSinAgrupar = $("#actividades").find("input[name='agruparActividades']");
    actividadesSinAgrupar.each(function (ind, element) {
        $rowActividad = $(element).closest("tr");
        $rowActividad.find("input[type=text]").attr("name", "[" + idActividadPrincipalCont + "].DescripcionActividad");
        $rowActividad.find("input[type=decimal]").each(function (ind2, element) {
            // valorPres = quitarFormatoNumero($(element).val())
            // $(element).val(valorPres.replace(".", ","));
            $(element).val(quitarFormatoNumero($(element).val()).replace(".", ","));// le doy formato al numero para que me lo reciba el servidor

            $(element).attr("name", "[" + idActividadPrincipalCont + "].presupuestosPorMes[" + ind2 + "].PresupuestoMes");
            var numeroMes = ind2 + 1
            var inputMes = '<input type="hidden" value="' + numeroMes + '" name="[' + idActividadPrincipalCont + '].presupuestosPorMes[' + ind2 + '].Mes"  />';
            $(element).closest("td").append(inputMes);
        });
        idActividadPrincipalCont++;
    });
}

//Funcion para cargar la tabla para generar el presupuesto
function CargarFormularioPresupuesto() {
    utils.showLoading();
    $.ajax({
        url: urlBase + '/Presupuesto/GenerarPresupuesto',
        data: {
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $("#TablaPresupuesto").html(result);
                // $('#TablaPresupuesto').find("input[type=decimal]").numeric(",");


                $('#TablaPresupuesto').find("input[type=decimal]").on({
                    "focus": function (event) {
                        $(event.target).select();
                    },
                    "keyup": function (event) {
                        $(event.target).val(function (index, value) {
                            valor = value.replace(/\D/g, "")
                                        .replace(/([0-9])([0-9]{2})$/, '$1,$2')
                                        .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
                            stringValor = "$".concat(valor)
                            return stringValor;
                        });
                    }
                });

            }
        }
    });
}

//funcion para agregar una nueva responsabilidad
function agregarResponsabilidad() {
    addRespoCont = addRespoCont + 1;
    var $nuevaResponsabilidad = $("#divv").clone();
    $nuevaResponsabilidad.removeAttr("id");
    $nuevaResponsabilidad.find("input[name = Responsab]").val("");
    $("#divresponsabilidadClon").append($nuevaResponsabilidad);
    $("#divresponsabilidadClon").find("input[id = Responsab]").attr("id", "Responsab" + addRespoCont)
    $("#divresponsabilidadClon").find("a[name = agregarResp]").find("i[name=icono]").attr("class", "glyphicon glyphicon-minus")
    $("#divresponsabilidadClon").find("a[name = agregarResp]").attr("title", "Eliminar Responsabilidad")
    $("#divresponsabilidadClon").find("a[name = agregarResp]").attr("onclick", "eliminarResponsabilidad(this)")

}

function agregarResponsabilidadEdicion() {
    var $nuevaResponsabilidadEdi = $("#divv").clone();
    $nuevaResponsabilidadEdi.removeAttr("id");
    $nuevaResponsabilidadEdi.find("input[name=Responsab]").val("")
    $nuevaResponsabilidadEdi.find("input[name=Pk_Id_Responsabilidades]").val("")
    $("#divresponsabilidadClon").append($nuevaResponsabilidadEdi);
    $("#divresponsabilidadClon").find("a[name = agregarResp]").find("i[name=icono]").attr("class", "glyphicon glyphicon-minus")
    $("#divresponsabilidadClon").find("a[name = agregarResp]").attr("title", "Eliminar Responsabilidad")
    $("#divresponsabilidadClon").find("a[name = agregarResp]").attr("onclick", "eliminarResponsabilidadEdicion(this)")
}

//Funcion para eliminar una Responsabilidad
function eliminarResponsabilidad(element) {
    $(element).closest("div[name = divv]").remove();
    addRespoCont = addRespoCont - 1;
}

function eliminarResponsabilidadEdicion(element) {
    respoEliminadas.push($(element).closest("div[name = divv]").find("input[name = Pk_Id_Responsabilidades]").val())
    $(element).closest("div[name = divv]").remove();
}

//funcion para agregar una nueva rendición de cuenta
function agregarRendicion() {
    addRendiCont = addRendiCont + 1;
    var $nuevaRendicion = $("#divvRend").clone();
    $nuevaRendicion.removeAttr("id");
    $nuevaRendicion.find("input[name = Rendicion]").val("");
    $("#divrendicionClon").append($nuevaRendicion);
    $("#divrendicionClon").find("input[id = Rendicion]").attr("id", "Rendicion" + addRendiCont)
    $("#divrendicionClon").find("a[name = agregarRend]").find("i[name=iconoRend]").attr("class", "glyphicon glyphicon-minus")
    $("#divrendicionClon").find("a[name = agregarRend]").attr("title", "Eliminar Rendición")
    $("#divrendicionClon").find("a[name = agregarRend]").attr("onclick", "eliminarRendicion(this)")
}

//funcion para agregar una nueva rendición de cuenta
function agregarRendicionEditar() {
    var $nuevaRendicionEdit = $("#divvRend").clone();
    $nuevaRendicionEdit.removeAttr("id");
    $nuevaRendicionEdit.find("input[name = Rendicion]").val("");
    $nuevaRendicionEdit.find("input[name = Pk_Id_RendicionDeCuentas]").val("");
    $("#divrendicionClonEdit").append($nuevaRendicionEdit);
    $("#divrendicionClonEdit").find("a[name = agregarRend]").find("i[name=iconoRend]").attr("class", "glyphicon glyphicon-minus")
    $("#divrendicionClonEdit").find("a[name = agregarRend]").attr("title", "Eliminar Rendición")
    $("#divrendicionClonEdit").find("a[name = agregarRend]").attr("onclick", "eliminarRendicionEditar(this)")
}

//Funcion para eliminar una Rendicion
function eliminarRendicion(element) {
    $(element).closest("div[name = divvRend]").remove();
    addRendiCont = addRendiCont - 1;

}

function eliminarRendicionEditar(element) {
    rendiEliminadas.push($(element).closest("div[name = divvRend]").find("input[name = Pk_Id_RendicionDeCuentas]").val())
    $(element).closest("div[name = divvRend]").remove();
}

//funcion para guardar la edicion del rol, responsabilidades y rendicion de cuentas
function EditarRolPorResp() {
    utils.showLoading();
    var rol = new Object();
    rol.Descripcion = $("#NameRol").val()
    rol.Pk_Id_Rol = $("#Pk_Id_Rol").val()
    var responsabilidades = new Array();
    $("#rol").find("div[name = divresponsabilidad]").each(function (int, element) {
        var respobilidad = new Object();
        respobilidad.Descripcion = $(element).find("input[name = Responsab]").val()
        respobilidad.Pk_Id_Responsabilidades = $(element).find("input[name = Pk_Id_Responsabilidades]").val()
        responsabilidades.push(respobilidad)
    })
    var rendicionCuenta = new Array();
    $("#rol").find("div[name = divrendicion]").each(function (int, element) {
        var rendici = new Object();
        rendici.Descripcion = $(element).find("input[name = Rendicion]").val()
        rendici.Pk_Id_RendicionDeCuentas = $(element).find("input[name = Pk_Id_RendicionDeCuentas]").val()
        rendicionCuenta.push(rendici)
    })
    $.ajax({
        url: urlBase + '/Rol/EditatRol',
        data: {
            rol: rol,
            responsabilidad: responsabilidades,
            rendicionDeCuenta: rendicionCuenta,
            responsaEliminadas: respoEliminadas,
            rendiciEliminadas: rendiEliminadas,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                respoEliminadas.length = 0;
                rendiEliminadas.length = 0;
                swal(
                'Señor usuario',
                'Rol editado con exito',
                'success'
                )
                window.location.reload(true);
            }
        }
    });
}

//Funcion para cargar la tabla para mostrar los roles
function CargarFormularioRol() {
    utils.showLoading();
    var rol = new Object();
    rol.Descripcion = $("#NameRol").val()
    var responsabilidades = new Array();
    $("#rol").find("input[name = Responsab]").each(function (int, element) {
        var respobilidad = new Object();
        respobilidad.Descripcion = $(element).val()
        responsabilidades.push(respobilidad)
    })
    var rendicionCuenta = new Array();
    $("#rol").find("input[name = Rendicion]").each(function (int, element) {
        var rendici = new Object();
        rendici.Descripcion = $(element).val()
        rendicionCuenta.push(rendici)
    })
    $.ajax({
        url: urlBase + '/Rol/CrearRolResponsabilidad',
        data: {
            rol: rol,
            responsabilidad: responsabilidades,
            rendicion: rendicionCuenta,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $("#TablaRol").html(result);
                $("#NameRol").val("");
                $("#Responsab").val("");
                $("#Rendicion").val("");
                $("#divresponsabilidadClon").html("");
                $("#divrendicionClon").html("");
                $("#divPlusClon").html("");
                $("#divPlusRenClon").html("");
            }
            else {
                swal(
                'Estimado Usuario',
                'Ese rol ya existe',
                'error'
                )
            }
        }
    });
}

function EditarRolResp(id) {
    utils.showLoading();
    $.ajax({
        url: urlBase + '/Rol/CrearEditRolResponsabilidad',
        data: {
            id: id,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $("#panel").html("")
                $("#panel").html(result)
                $("#NameRol").focus()
            }
        }
    });
}

///  funcion para cargar los presupuestos por año
function CargarFormularioBusquedaPrespuestoPorAnio(esInforme) {
    utils.showLoading();
    $.ajax({
        url: urlBase + '/Presupuesto/BuscarPresupuestoSedePorPeriodo',
        data: {
            Pk_Id_Sede: $("#FK_Sede").val(),
            Periodo: $("#Periodo").val(),
            informe: esInforme
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $("#TablaPresupuesto").html(result);
                $("input[name=rtotal]").each(function (ind, element) {
                    var numero = parseFloat($(element).val().replace(",", "."));
                    $(element).closest("td").html(darFormato(numero.toFixed(2)));
                })
            }
        }
    });
}


///  funcion para cargar el informe por periodo,tiempo e intervalo de tiempo
function CargarInformePresupuesto() {
    var IDPresupuestoAnio = $("#IDPresupuestoAnio").val();
    var fecha = $("#fecha").val();
    var intervaloDeTiempo = $("#intervaloDeTiempo").val();
    utils.showLoading();
    $.ajax({
        url: urlBase + '/Presupuesto/GenerarInformePresupuesto',
        data: {
            IDPresupuestoAnio: IDPresupuestoAnio,
            fecha: fecha,
            intervaloDeTiempo: intervaloDeTiempo,
            nombreIntervaloTiempo: $("#intervaloDeTiempo").find("option:selected").text()
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                $("#TablaPresupuesto").html(result);
                $("#NombreColumna").html($("#intervaloDeTiempo").find("option:selected").text());

                $("#TablaPresupuesto").find("td[name = presupuestoinforme]").each(function (ind, element) {
                    var numero = parseFloat($(element).text().trim())
                    $(element).html(darFormato(numero.toFixed(2)));
                });


                construirGrafico(IDPresupuestoAnio, fecha, intervaloDeTiempo);
            }
        }
    });
}

function construirGrafico(IDPresupuestoAnio, fecha, intervaloDeTiempo) {
    utils.showLoading();
    $.ajax({
        url: urlBase + '/Presupuesto/obtenerDataInformePresupuesto',
        data: {
            IDPresupuestoAnio: IDPresupuestoAnio,
            fecha: fecha,
            intervaloDeTiempo: intervaloDeTiempo,
        },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                var ctx = document.getElementById("canvas");
                var planeado = 0;
                var ejecutado = 0;
                var disponible = 0;
                $.each(result, function (ind, element) {
                    planeado = planeado + parseFloat(element.presupuesto);
                    ejecutado = ejecutado + parseFloat(element.prespuestoEjecutado);
                    disponible = disponible + parseFloat(element.presupuestoDisponible);
                })
                
                var data = {
                    labels: [
                        "Ejecutado",
                        "Planeado",
                        "Saldo"
                    ],
                    datasets: [
                        {
                            data: [ejecutado, planeado, disponible],
                            backgroundColor: [
                                "#E5E8E8",
                                "#82E0AA",
                                "#85C1E9"
                            ],
                            hoverBackgroundColor: [
                                "#E5E8E8",
                                "#82E0AA",
                                "#85C1E9"
                            ]
                        }]
                }
                var myPieChart = new Chart(ctx, {
                    type: 'pie',
                    data: data,
                    options: {                       
                        tooltips: {
                            callbacks: {
                                label: function (tooltipItem, data) {
                                    var allData = data.datasets[tooltipItem.datasetIndex].data;
                                    var tooltipLabel = data.labels[tooltipItem.index];
                                    var tooltipData = allData[tooltipItem.index];
                                    var total = 0;
                                    for (var i in allData) {
                                        total += allData[i];
                                    }
                                    var tooltipPercentage = Math.round((tooltipData / total) * 100);
                                    var tooltipValor = parseFloat(tooltipData);

                                    return tooltipLabel + ': ' + darFormato(tooltipValor.toFixed(2)) + ' (' + tooltipPercentage + '%)';
                                }
                            }
                        }

                    }
                });

               
                var numeroEjecutado = parseFloat(ejecutado);
                var numeroPlaneado = parseFloat(planeado);
                var numeroSaldo = parseFloat(disponible);                
                $("#thTotalEjecutado").html(darFormato(numeroEjecutado.toFixed(2)));
                $("#thToltalPlaneado").html(darFormato(numeroPlaneado.toFixed(2)));
                $("#thSaldo").html(darFormato(numeroSaldo.toFixed(2)));

            }
        }
    });

}

// funcion para sumar el total por mes y por actividad
function sumarPresupuestoMes(element) {
    var sumatotalEnero = 0;
    var sumatotalFebrero = 0;
    var sumaTotalMarzo = 0;
    var sumaTotalAbril = 0;
    var sumaTotalMayo = 0;
    var sumaTotalJunio = 0;
    var sumaTotalJulio = 0;
    var sumaTotalAgosto = 0;
    var sumaTotalSeptiembre = 0;
    var sumaTotalOctubre = 0;
    var sumaTotalNoviembre = 0;
    var sumaTotalDiciembre = 0;
    var sumaTotal = 0;
    var sumaTotalActividad = 0;

    $("td[name=enero]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            //sumatotalEnero = sumatotalEnero + parseFloat($(element).val().replace(",", "."));
            sumatotalEnero = sumatotalEnero + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=febrero]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumatotalFebrero = sumatotalFebrero + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=marzo]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalMarzo = sumaTotalMarzo + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=abril]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalAbril = sumaTotalAbril + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=mayo]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalMayo = sumaTotalMayo + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=junio]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalJunio = sumaTotalJunio + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=julio]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalJulio = sumaTotalJulio + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=agosto]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalAgosto = sumaTotalAgosto + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=septiembre]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalSeptiembre = sumaTotalSeptiembre + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=octubre]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalOctubre = sumaTotalOctubre + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    $("td[name=noviembre]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalNoviembre = sumaTotalNoviembre + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });
    $("td[name=diciembre]").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            sumaTotalDiciembre = sumaTotalDiciembre + parseFloat(quitarFormatoNumero($(element).val()));
            sumaTotal = sumaTotal + parseFloat($(element).val().replace(",", "."));
        }
    });

    //$("#sumaEnero").val(sumatotalEnero.toFixed(2).replace(".", ","));
    $("#sumaEnero").val(darFormato(sumatotalEnero.toFixed(2)));
    // $("#sumaFebrero").val(sumatotalFebrero.toFixed(2).replace(".", ","));
    $("#sumaFebrero").val(darFormato(sumatotalFebrero.toFixed(2)));
    $("#sumaMarzo").val(darFormato(sumaTotalMarzo.toFixed(2)));
    $("#sumaAbril").val(darFormato(sumaTotalAbril.toFixed(2)));
    $("#sumaMayo").val(darFormato(sumaTotalMayo.toFixed(2)));
    $("#sumaJunio").val(darFormato(sumaTotalJunio.toFixed(2)));
    $("#sumaJulio").val(darFormato(sumaTotalJulio.toFixed(2)));
    $("#sumaAgosto").val(darFormato(sumaTotalAgosto.toFixed(2)));
    $("#sumaSeptiembre").val(darFormato(sumaTotalSeptiembre.toFixed(2)));
    $("#sumaOctubre").val(darFormato(sumaTotalOctubre.toFixed(2)));
    $("#sumaNoviembre").val(darFormato(sumaTotalNoviembre.toFixed(2)));
    $("#sumaDiciembre").val(darFormato(sumaTotalDiciembre.toFixed(2)));


    $(element).closest("tr").find("input[type=decimal]").each(function (ind, element) {
        if ($(element).val() != "") {
            //  sumaTotalActividad = sumaTotalActividad + parseFloat($(element).val().replace(",", "."));
            sumaTotalActividad = sumaTotalActividad + parseFloat(quitarFormatoNumero($(element).val()));
        }
    });
    // $(element).closest("tr").find("input[name=total]").val(sumaTotalActividad.toFixed(2).replace(".", ","));
    $(element).closest("tr").find("input[name=total]").val(darFormato(sumaTotalActividad.toFixed(2)));
    $("#sumaTotal").val(darFormato((sumatotalEnero + sumatotalFebrero + sumaTotalMarzo + sumaTotalAbril + sumaTotalMayo + sumaTotalJunio
        + sumaTotalJulio + sumaTotalAgosto + sumaTotalSeptiembre + sumaTotalOctubre + sumaTotalNoviembre + sumaTotalDiciembre).toFixed(2)));

    var rubro = parseFloat(quitarFormatoNumero($("#RubroTotal").val()));
    var totalActividad = parseFloat(quitarFormatoNumero($("#sumaTotal").val()));
    if (rubro < totalActividad) {
        $("#ModalGuardar").modal("show");
        $("#guardarActividad").attr("disabled", true);
        $("#guardarActividad").attr("style", "display:none;");
        $("#guardarActividadModal").removeAttr("style");

    }
    else {
        $("#guardarActividad").attr("disabled", false);
        $("#guardarActividad").removeAttr("style");
        $("#guardarActividadModal").attr("style", "display:none;");
    }

}

function quitarFormatoNumero(Numero) {


    valorNumero = Numero.split(".").join("");
    valorNumero = valorNumero.replace("$", "");
    valorNumero = valorNumero.replace(",", ".");
    if (valorNumero != "" && valorNumero != null) {
        return valorNumero
    }
    else {
        return "0";
    }
}

function darFormato(Numero) {
    var valor = parseFloat(Numero);
    var isNegativo = false;
    if (valor < 0) {
        isNegativo = true;
    }

    stringValor = Numero.toString().replace(/\D/g, "")
                            .replace(/([0-9])([0-9]{2})$/, '$1,$2')
                            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
    //  stringValor = "$".concat(stringValor)
    if (stringValor != "" && stringValor != null) {
        if (isNegativo) {

            return "$ -".concat(stringValor)
        }
        return "$ ".concat(stringValor)
    }
    else {
        return "0";
    }
}

//funcion para guardar el comentario en un input type hidden para poderlo guardar 
function ocultarComentario(elemento) {
    var comentario = $(elemento).closest("td").find("textarea").val();
    //$(elemento).closest("td").find("input[name*='ComentarioPresupuestoMesEjecutado']").val($(elemento).val());
    $(elemento).closest("td").find("input[name*='ComentarioPresupuestoMesEjecutado']").val(comentario);
    $(elemento).closest("td").find("button[name='botonDeComentario']").attr("title", comentario);
    

}

function consultarIntervaloDeTiempo() {
    $tiempo = $("#fecha").val();
    $intervaloTiempo = $("#intervaloDeTiempo");
    $intervaloTiempo.find("option").remove();//Removemos las opciones anteriores 
    $intervaloTiempo.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar
    $intervaloTiempo.closest("div[name=interTiempo]").removeAttr("hidden");
    if ($tiempo == 1) {
        $intervaloTiempo.append(new Option("Enero", 1));//agregamos las opciones consultadas
        $intervaloTiempo.append(new Option("Febrero", 2));
        $intervaloTiempo.append(new Option("Marzo", 3));
        $intervaloTiempo.append(new Option("Abril", 4));
        $intervaloTiempo.append(new Option("Mayo", 5));
        $intervaloTiempo.append(new Option("Junio", 6));
        $intervaloTiempo.append(new Option("Julio", 7));
        $intervaloTiempo.append(new Option("Agosto", 8));
        $intervaloTiempo.append(new Option("Septiembre", 9));
        $intervaloTiempo.append(new Option("Octubre", 10));
        $intervaloTiempo.append(new Option("Noviembre", 11));
        $intervaloTiempo.append(new Option("Diciembre", 12));
    }
    if ($tiempo == 2) {
        $intervaloTiempo.append(new Option("Primer Trimestre", 1));//agregamos las opciones consultadas
        $intervaloTiempo.append(new Option("Segundo Trimestre", 2));
        $intervaloTiempo.append(new Option("Tercer Trimestre", 3));
        $intervaloTiempo.append(new Option("Cuarto Trimestre", 4));
    }
    if ($tiempo == 3) {
        $intervaloTiempo.append(new Option("Primer Semestre", 1));//agregamos las opciones consultadas
        $intervaloTiempo.append(new Option("Segundo Semestre", 2));
    }
    if ($tiempo == 4) {
        $intervaloTiempo.closest("div[name=interTiempo]").attr("hidden", "hidden");
        $intervaloTiempo.append(new Option("Anual", 1));//agregamos las opciones consultadas
        $intervaloTiempo.val(1);
    }
}

function EliminarActividad(element, idActividad) {
    $div = $(element).closest("tr");
    name = $div.attr("name");
    if ($("#actividades").find("tr").length == 2 && $("#actividades").find("tr[name=" + name + "]").length == 2) {
        swal(
        'Estimado Usuario',
        'No es posible eliminar la ultima actividad del presupuesto',
        'error'
        )
    } else {
        if ($("#actividades").find("tr").length > 1) {
            utils.showLoading();
            $.ajax({
                url: urlBase + '/Presupuesto/EliminarActividad',
                data: {
                    pkActividad: idActividad
                },
                type: 'POST',
                success: function (result) {
                    utils.closeLoading();
                    if (result.success) {
                        utils.showMessage(result.mesansaje, "success", "");
                        $("#modalesEliminados").append($("#modalEliminar" + idActividad));   // se agrega el modal en un div diferente al tr para que cuando el tr sea eliminado el dom no se bloquee             
                        $div.remove();


                        var cantidad = $("#actividades").find("tr[name=" + name + "]").length;
                        if (cantidad == 1) /// si que sirve para eliminar la actividad principal cuando ya no tenga acividades secuandarias
                        {
                            $tr = $("#actividades").find("tr[name=" + name + "]");
                            idActividad = $("#actividades").find("tr[name=" + name + "]").find("input[name *=PK_Actividad_Presupuesto]").val();
                            if (idActividad) {
                                EliminarActividad($tr, idActividad);
                            } else {
                                $tr.remove();
                            }
                        }
                    } else {
                        utils.showMessage(result.mesansaje, "error", "");
                    }
                }
            });
        } else {
            swal(
            'Estimado Usuario',
            'No es posible eliminar la ultima actividad del presupuesto',
            'error'
            )
        }
    }
}

//funcion para eliminar las actividades que no estan guardadas en la base de datos
function EliminarActividadSinGuardar(btnElminarActividad) {

    $tr = $(btnElminarActividad).closest("tr");
    name = $tr.attr("name");
    if ($("#actividades").find("tr").length == 2 && $("#actividades").find("tr[name=" + name + "]").length == 2) {
        swal(
        'Estimado Usuario',
        'No es posible eliminar la ultima actividad del presupuesto',
        'error'
        )
    } else {
        if ($("#actividades").find("tr").length > 1) {
            $("#modalesEliminados").append($(btnElminarActividad).closest("tr").find("div[name*=modalActSinGuardar]"));
            $tr.remove();
            var cantidad = $("#actividades").find("tr[name=" + name + "]").length;
            if (cantidad == 1) /// si que sirve para eliminar la actividad principal cuando ya no tenga acividades secuandarias
            {
                $("#actividades").find("tr[name=" + name + "]").remove();
            }
        } else {
            swal(
            'Estimado Usuario',
            'No es posible eliminar la ultima actividad del presupuesto',
            'error'
            )
        }
    }
}
