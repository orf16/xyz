var urlBase = ""
var $idtipoPeligro = $("#FK_Tipo_De_Peligro");
var $idClasePeligro = $("#FK_Clasificacion_De_Peligro");

var $idNivelDeficiencia = $("#FK_Nivel_De_Deficiencia");
var $nameTipoDeCalificacion = $("*[name = FLG_Tipo_De_Calificacion]");
var $formularioMetodologia = $("#formulariosMetodologias");
//var $idTipoDeMetodologia = $("#id_metodologiaSeleccionada");
//var $idSede = $("#id_sedeSeleccionada");
var $idTipoDeMetodologia = $("#PK_Metodologia");
var $idSede = $("#Pk_Id_Sede");
var $idNivelDeficiencia = $("#FK_Nivel_De_Deficiencia");
var $idNivelExposicion = $("#FK_Nivel_De_Exposicion");
var $idNivelProbabilidad = $("#Nivel_De_Probablidad");
var $idInterpretacion = $("#InterpretacionProbalidad");
var $idConsecuencia = $("#FK_Consecuencia");
var $idNivelDeRiesgo = $("#Nivel_De_Riesgo");
var $idResultadoRiesgo = $("#ResultadoIntepretacionRiesgo");
var $idInterpretacionRiesgo = $("#interpretacionDeRiesgo");
var $idEstimacionRiesgo = $("#Estimacion_Riesgo");
var $idProbabilidad = $("#FK_Probabilidad");
var $idConsecuenciaPersona = $("#consecuenciaPersona");
var urlPerfil = '/PerfilSocioDemoGrafico';
//Módulo Perfilsociodemográfico
var $idMunicipio = $("#Fk_Id_Municipio");
var $idDepartamento = $("#Fk_Id_Departamento");
var $idMunicipioSede = $("#IdMunicipio_Sede");
//var $idSede = $("#Fk_Sede");

var NIVELCONSECUENCIANOAPLICA = 39 // id del nivel de consecuencia de no aplica en la base de datos
var NIVELEXPOSICIONNOAPLICA = 5 // id del nivel de exposicion de no aplica en la base de datos
var NIVELDEFICIENCIANOAPLICA = 4; // id del nivel de deficiencia  de no aplica en la base de datos

var validacion = 0;
var validarnombrematrizreqleg = 0;
var validarrequisitoscheck = 0;

//Modulo Requisitos legales
var $idActividadEconomica = $("#FK_Actividad_Economica");
var $idtxtNombreMatriz = $("#idNombreMatriz");

function Seleccionarcheckbox() {
    // $('input:checkbox').prop('checked', this.checked);

    if ($("#checkAll").prop("checked")) {
        $('input:checkbox.checkBox').each(function () {
            $(this).prop('checked', true);
        });
    }
    else {
        $('input:checkbox.checkBox').each(function () {
            $(this).prop('checked', false);
        });
    }
    //if ($(this).is(':checked')) {
    //    //$("input[type=checkbox]").prop('checked', true); //todos los check
    //    $("#checkAll input[type=checkbox]").prop('checked', true); //solo los del objeto #diasHabilitados
    //} else {
    //    //$("input[type=checkbox]").prop('checked', false);//todos los check
    //    $("#checkAll input[type=checkbox]").prop('checked', false);//solo los del objeto #diasHabilitados
    //}
}


function SeleccionarTodosEmpleados() {
    if ($("#checkEmpleado").prop("checked")) {
        $('input[name=list]').each(function (ind, element) {
            $(element).attr('checked', true);
        });




    }
    else {
        $('input[name=list]').each(function (ind, element) {
            $(element).attr('checked', false);
        });
    }
}


function inicializarVariables() {
    $idtipoPeligro = $("#FK_Tipo_De_Peligro");
    $idClasePeligro = $("#FK_Clasificacion_De_Peligro");
    $idNivelDeficiencia = $("#FK_Nivel_De_Deficiencia");
    $nameTipoDeCalificacion = $("*[name = FLG_Tipo_De_Calificacion]");
    $formularioMetodologia = $("#formulariosMetodologias");
    //  $idTipoDeMetodologia = $("#id_metodologiaSeleccionada");
    //$idSede = $("#id_sedeSeleccionada");
    $idTipoDeMetodologia = $("#PK_Metodologia");
    $idSede = $("#Pk_Id_Sede");
    $idNivelDeficiencia = $("#FK_Nivel_De_Deficiencia");
    $idNivelExposicion = $("#FK_Nivel_De_Exposicion");
    $idNivelProbabilidad = $("#Nivel_De_Probablidad");
    $idInterpretacion = $("#InterpretacionProbalidad");
    $idConsecuencia = $("#FK_Consecuencia");
    $idNivelDeRiesgo = $("#Nivel_De_Riesgo");
    $idResultadoRiesgo = $("#ResultadoIntepretacionRiesgo");
    $idInterpretacionRiesgo = $("#interpretacionDeRiesgo");
    $idEstimacionRiesgo = $("#Estimacion_Riesgo");
    $idProbabilidad = $("#FK_Probabilidad");
    $idConsecuenciaPersona = $("#consecuenciaPersona");
}


try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

var IDOTRO = 8;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de peligro para el peligro de tipo otro
var IDMETODOLOGIAGTC45 = 1;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de Metodología gtc45
var IDMETODOLOGIARAM = 2;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de Metodología ram
var IDMETODOLOGIAINSHT = 3;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de Metodología INSHT



$(function () {

    ConstruirDatePickerPorElemento("Fecha_Actualizacion");
    ConstruirDatePickerPorElemento("Fecha_Seguimiento_Control");
    ConstruirDatePickerPorElemento("idFechaPublicacion");



    //ConstruirDatePickerPorElemento("FechaIngresoUltimoCargo");
    desahabilitarRadioGrupoEtareo();

    $('#PK_Numero_Documento_Empl').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $('#AntecedentesExpLaboral').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });



    $('#Cargo').on('input', function (e) {
        if (!/^[ a-záéíóúüñ_-]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-záéíóúüñ_-]+/ig, "");
        }
    });




    $.datepicker.setDefaults($.datepicker.regional["es"]);
    $('#FechaIngresoUltimoCargo').datepicker({
        firstDay: 1,
        format: "dd/mm/yyyy",
        language: 'es',
        autoclose: true,
        changeMonth: true,
        changeYear: true
    });

});


//Funcion para consultar las clases de peligros por cada tipo de peligro
function ConsultarClasesPeligros(idClasificacion) {
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
            url: urlBase + '/ClasificacionDePeligros/ConsultarClasesPeligros',
            data: {
                Pk_Tipo_Peligro: $idtipoPeligro.val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $idClasePeligro.find("option").remove();//Removemos las opciones anteriores 
                    $idClasePeligro.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar
                    //  $nameTipoDeCalificacion.prop("checked", "");

                    $.each(result, function (ind, element) {
                        $idClasePeligro.append(new Option(element.ClasesDescription, element.PK_ClasesPeligros));//agregamos las opciones consultadas
                    })
                    var textoPeligro = $idtipoPeligro.find("option:selected").text();
                    $("input[name = VisualizadorPeligro]").each(function (ind, element) {
                        $(element).val(textoPeligro);
                    });
                    if (idClasificacion != "") {
                        $("#FK_Clasificacion_De_Peligro").val(idClasificacion);// nueva linea

                    }
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


function MostrarTipoCalificacion(checkbox) {

    if ($(checkbox).is(':checked')) {
        $("#RadioBtnTipoCalificacion").removeAttr("hidden");
    } else {
        $("#RadioBtnTipoCalificacion").attr("hidden", "hidden");
        $nameTipoDeCalificacion.prop("checked", "");
        ConsultarNivlesDeDeficiencia($("#flg_tipo_de_calificacion2"));
    }

}

//Funcion para consultar los nivels de defiencia
function ConsultarNivlesDeDeficiencia(chBoxTipoDecalificacion) {

    $.ajax({
        url: urlBase + '/NivelDeDeficiencias/ConsultarNivelesDeDeficiencia',
        data: {
            FLAG_Cuantitativa: $(chBoxTipoDecalificacion).val(),//tipo de calificacion            
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $idNivelDeficiencia.find("option").remove();//Removemos las opciones anteriores 
                $idNivelDeficiencia.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar
                $.each(result, function (ind, element) {
                    if (element.NivelDeDeficienciaDescription == null) {
                        $idNivelDeficiencia.append(new Option(element.ValorDeficiencia, element.PK_NivelDeDeficiencia));//agregamos las opciones consultadas
                    } else {
                        $idNivelDeficiencia.append(new Option(element.NivelDeDeficienciaDescription, element.PK_NivelDeDeficiencia));//agregamos las opciones consultadas
                    }

                })
            }
        }
    });

}

//Funcion para cargar los diferentes formularios de las  metodologias para el registro de los peligros
function CargarFormularioMetodologias() {
    FormularioGenerarMetodologia();
    if ($("#GenerarMetodologia").valid()) {
        utils.showLoading();
        $.ajax({
            url: urlBase + '/Metodologia/ObtenerFormularioMetodologia',
            data: {
                PK_TipoMedologia: $idTipoDeMetodologia.val(),
                Pk_Sede: $idSede.val()
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $formularioMetodologia.html(result);
                    inicializarVariables();
                    inicializarAyudas();

                }
            }
        });
    }
}

//Funcion para cargar el nivel de probalidad y la interpretacion
function CargarNivelProbalidad() {
    $visualizadorNivelProbabilidad = $("#Visualizador_Nivel_De_Probablidad");
    if ($idNivelDeficiencia.val() == NIVELDEFICIENCIANOAPLICA) {
        $idNivelExposicion.val(NIVELEXPOSICIONNOAPLICA);
    }

    if ($idNivelDeficiencia.val() != "" && $idNivelExposicion.val() != "") {
        $.ajax({
            url: urlBase + '/GTC45/NivelProbabilidad',
            data: {
                PK_Deficiencia: $idNivelDeficiencia.val(),
                PK_Exposicion: $idNivelExposicion.val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $idNivelProbabilidad.val(result.Valor_Probablidad);
                    $visualizadorNivelProbabilidad.val(result.Valor_Probablidad);
                    $idInterpretacion.val(result.interpretacion);
                    if (result.Valor_Probablidad == 0) {
                        $idConsecuencia.val(NIVELCONSECUENCIANOAPLICA);
                        $visualizadorNivelProbabilidad.val("No Aplica");
                    } else {
                        $visualizadorNivelProbabilidad.val(result.Valor_Probablidad);
                    }

                    CargarNivelDeRiesgo();
                }
            }
        });
    } else {
        $visualizadorNivelProbabilidad.val("");
        $idInterpretacion.val("");
        $visualizadorNivelDeRiesgo.val("");
        $idResultadoRiesgo.val("");
        $idInterpretacionRiesgo.val("");
    }


}

//Funcion para cargar  el nivel de riesgo,resultado e interpretacionn
function CargarNivelDeRiesgo() {
    $visualizadorNivelDeRiesgo = $("#visualizador_Nivel_De_Riesgo");
    $.ajax({
        url: urlBase + '/GTC45/NivelDeRiesgo',
        data: {
            PK_Consecuencia: $idConsecuencia.val(),
            Valor_Probabilidad: $idNivelProbabilidad.val()
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $idNivelDeRiesgo.val(result.Nivel_De_Riesgo);
                if (result.Nivel_De_Riesgo == 0) {
                    $visualizadorNivelDeRiesgo.val("No Aplica");
                }
                else {
                    $visualizadorNivelDeRiesgo.val(result.Nivel_De_Riesgo);
                }
                $idResultadoRiesgo.val(result.Resultado);
                $idInterpretacionRiesgo.val(result.Interpretacion)
            }
        }
    });
}



//Funcion para cargar  la estimacion del riesgo para la metodologia INSHT
function CargarEstimacionDeRiesgo() {
    var $liRiesgosNoAceptables = $("#liRiesgosNoAceptables");
    var $liBtnSgtDeterminacionCtrls = $("#btnSiguienteDeteminacionControles");
    var $liBtnAntRiesgoNoCtrls = $("#BtnAntRiesgosNoControlados");
    var $VisualizadorEstimacionDeRiesgos = $("#Visualizador_Estimacion_Riesgo");
    if ($idProbabilidad.val() != "" && $idConsecuencia.val() != "") {
        $.ajax({
            url: urlBase + '/INSHT/EstimacionDelRiesgo',
            data: {
                Pk_Probabilidad: $idProbabilidad.val(),
                PK_Consecuencia: $idConsecuencia.val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $idEstimacionRiesgo.val(result.Estimacion_Del_Riesgo);
                    $VisualizadorEstimacionDeRiesgos.val(result.Estimacion_Del_Riesgo);
                    if (result.EsNoAceptable) {
                        $liRiesgosNoAceptables.removeAttr("class");
                        $liBtnSgtDeterminacionCtrls.attr("onclick", "validarCamposInformacioGeneral(5)");
                        $liBtnAntRiesgoNoCtrls.attr("onclick", "AnteriorPanel(5)");
                    } else {
                        $liRiesgosNoAceptables.attr("class", "hidden");
                        $liBtnSgtDeterminacionCtrls.attr("onclick", "validarCamposInformacioGeneral(6)");
                        $liBtnAntRiesgoNoCtrls.attr("onclick", "AnteriorPanel(4)");
                    }
                }
            }
        });
    } else {
        $VisualizadorEstimacionDeRiesgos.val("");
    }
}



//Funcion para consultar las consecuencias por grupos de la metodologia de tipo RAM
function ConsultarConsecuenciasPorGrupo(pk_grupo) {
    $.ajax({
        url: urlBase + '/RAM/ConsecuenciasPorGrupo',
        data: {
            PK_Grupo: pk_grupo
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $.each(result, function (ind, element) {
                    $idConsecuenciaPersona.append(new Option(element.PK_ConsecuenciaDescription, element.PK_Consecuencia));//agregamos las opciones consultadas
                })
            }
        }
    });
}


//Funcion para cargar  la estimacion del riesgo para la metodologia RAM para cada uno de los grupos
var ValoriesgoMaximo = 0;
function CargarEstimacionDeRiesgoRAM(elemento) {

    var $divGrupo = $(elemento).closest("div[name = Grupo]");
    var $probabilidadGrupo = $divGrupo.find("select[name ^=FK_Probabilidad]");
    var $consecuenciaGrupo = $divGrupo.find("select[name ^=consecuencia]");
    var $detalleEstimacion = $divGrupo.find("input[name = DetalleEstimacion_Riesgo]");
    var $RiesgoMaximo = $("#Nivel_De_Riesgo");
    var $visualizadorRiesgoMaximo = $("#Visualizador_Nivel_De_Riesgo")
    if ($probabilidadGrupo.val() != "" && $consecuenciaGrupo.val() != "") {
        $.ajax({
            url: urlBase + '/INSHT/EstimacionDelRiesgo',
            data: {
                Pk_Probabilidad: $probabilidadGrupo.val(),
                PK_Consecuencia: $consecuenciaGrupo.val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $detalleEstimacion.val(result.Estimacion_Del_Riesgo);
                    $divGrupo.find("input[name = PK_Estimacion_Riesgo]").val(result.PK_Estimacion_De_Riesgo);
                    if (ValoriesgoMaximo < result.valorRiesgo) {
                        ValoriesgoMaximo = result.valorRiesgo;
                        $RiesgoMaximo.val(result.Estimacion_Del_Riesgo);
                        $visualizadorRiesgoMaximo.val(result.Estimacion_Del_Riesgo);

                    }
                }
            }
        });
    } else {
        $detalleEstimacion.val("");
    }
}

function EditarNameConsecuenciasYProbabilidadesRAM() {
    var $idDiv = $("#step3");
    $idDiv.find("input[name = PK_Estimacion_Riesgo]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].FK_Estimacion_De_Riesgo");
    });

    $idDiv.find("select[name ^= FK_Probabilidad]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].FK_Probabilidad");
    });
    $idDiv.find("select[name ^= consecuencia]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].FK_Consecuencia");
    });

    $idDiv.find("input[name = PK_Probabilidad_Por_PersonaExpuesta]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].PK_Probabilidad_Por_PersonaExpuesta");
    });

    $idDiv.find("input[name = PK_Consecuencia_Por_Peligro]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].PK_Consecuencia_Por_Peligro");
    });

    $idDiv.find("input[name = PK_Estimacion_Riesgo_Por_RAM]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].PK_Estimacion_Riesgo_Por_RAM");
    });

    $idDiv.find("input[name = FK_RAM]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].FK_RAM");
    });

    $idDiv.find("input[name = FK_Persona_Expuesta]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].FK_Persona_Expuesta");
    });

    $idDiv.find("input[name = FK_Peligro]").each(function (ind, elemento) {
        $(elemento).attr("name", "[" + ind + "].FK_Peligro");
    });


}



function EliminarPeligro(element, idPeligro) {
    // $div = $("#mensaje");
    //$btnAbrirModal = $(element).closest("td").find("button[name=btnAbrirModal]");
    //$btnEditarPeligro = $(element).closest("td").find("a[name=editarPeligro]");    
    $div = $(element).closest("tr");
    $.ajax({
        url: urlBase + '/Metodologia/EliminarPeligro',
        data: {
            Pk_Peligro: idPeligro
        },
        type: 'POST',
        success: function (result) {

            if (result.success) {
                utils.showMessage(result.mesansaje, "success", "");
                $("#modalesEliminados").append($("#modalEliminar" + idPeligro));
                $div.remove();
            } else {
                utils.showMessage(result.mesansaje, "error", "");
            }
        }
    });
}

function totalDeExpuestos() {
    var $PlantaDirecto = $("#Planta_Directo");
    var $Contratista = $("#Contratista");
    var $Temporal = $("#Temporal");
    var $Estudiante_Pasante = $("#Estudiante_Pasante");
    var Visitante = $("#Visitante");
    var suma = 0;

    if ($PlantaDirecto.val() != "") {
        suma = suma + parseInt($PlantaDirecto.val());
    }
    if ($Contratista.val() != "") {
        suma = suma + parseInt($Contratista.val());
    }
    if ($Temporal.val() != "") {
        suma = suma + parseInt($Temporal.val());
    }
    if ($Estudiante_Pasante.val() != "") {
        suma = suma + parseInt($Estudiante_Pasante.val());
    }
    if (Visitante.val() != "") {
        suma = suma + parseInt(Visitante.val());
    }

    $("#Total").val(suma);
}

/**bloque para inicializar todas las ayudas de las metodologias*/

function inicializarAyudas() {

    var AyudaNivelDeficienciaHtml = $("#ayudasNivlesDeficiencia").html();
    var AyudaNivelDeExposicionHtml = $("#ayudasNivlesDeExposicion").html();
    var AyudaInterpraciontionProbabilidadHtml = $("#ayudasInterpretacionNivelProbabilidad").html();
    var AyudaNivelesConsecuenciaGtc45Html = $("#ayudasNivelConsecuencia").html();
    var AyudaInterpretacionNivelRiesgoHtml = $("#ayudasInterpretacionNivelRiesgo").html();
    var AyudaAceptabilidadRiesgoHtml = $("#ayudasAceptabilidadRiesgo").html();

    var AyudasNivelProbabilidadINSHTHtml = $("#ayudasNivelProbabilidadINSHT").html();
    var AyudasNivelEstimacionRiesgoHtml = $("#ayudasEstimacionRiesgoINSHT").html();
    var AyudaNivelesConsecuenciaINSHTHtml = $("#ayudasNivelConsecuenciaINSHT").html();

    var AyudasConsecuenciasPersonas = $("#ayudasConsecuenciaPersonas").html();
    var AyudasConsecuenciasCliente = $("#ayudasConsecuenciaCliente").html();
    var AyudasConsecuenciasEconomica = $("#ayudasConsecuenciaEconomica").html();
    var AyudasConsecuenciasAmbiental = $("#ayudasConsecuenciaAmbiental").html();
    var AyudasConsecuenciasIEmpresa = $("#ayudasConsecuenciaEmpresa").html();
    var AyudasValorReisgoHtml = $("#ayudasValorRiesgo").html();
    var AyudasNivelProbabilidadRAMHtml = $("#ayudasNivelProbabilidadRAM").html();

    $('#FK_Nivel_De_Deficiencia').tooltip({ title: AyudaNivelDeficienciaHtml, html: true, placement: "top" });
    $('#FK_Nivel_De_Exposicion').tooltip({ title: AyudaNivelDeExposicionHtml, html: true, placement: "top" });
    $('#InterpretacionProbalidad').tooltip({ title: AyudaInterpraciontionProbabilidadHtml, html: true, placement: "top" });
    if ($('#FK_Consecuencia').closest("div[name=consecuenciaInsht]").length > 0) {
        $('#FK_Consecuencia').tooltip({ title: AyudaNivelesConsecuenciaINSHTHtml, html: true, placement: "top" });
    } else {
        $('#FK_Consecuencia').tooltip({ title: AyudaNivelesConsecuenciaGtc45Html, html: true, placement: "top" });
    }

    $('#ResultadoIntepretacionRiesgo').tooltip({ title: AyudaInterpretacionNivelRiesgoHtml, html: true, placement: "top" });
    $('#interpretacionDeRiesgo').tooltip({ title: AyudaAceptabilidadRiesgoHtml, html: true, placement: "top" });
    $('#FK_Probabilidad').tooltip({ title: AyudasNivelProbabilidadINSHTHtml, html: true, placement: "top" });
    $('#Visualizador_Estimacion_Riesgo').tooltip({ title: AyudasNivelEstimacionRiesgoHtml, html: true, placement: "top" });
    $("select[name ^= FK_Probabilidad]").tooltip({ title: AyudasNivelProbabilidadRAMHtml, html: true, placement: "top" });

    $('#consecuenciaPersona').tooltip({ title: AyudasConsecuenciasPersonas, html: true, placement: "top", trigger: "focus" });
    $('#consecuenciasClientes').tooltip({ title: AyudasConsecuenciasCliente, html: true, placement: "top", trigger: "focus" });
    $('#consecuenciasEconomica').tooltip({ title: AyudasConsecuenciasEconomica, html: true, placement: "top", trigger: "focus" });
    $('#consecuenciasAmbiental').tooltip({ title: AyudasConsecuenciasAmbiental, html: true, placement: "top", trigger: "focus" });
    $('#consecuenciasImagenE').tooltip({ title: AyudasConsecuenciasIEmpresa, html: true, placement: "top", trigger: "focus" });

    $("input[name = DetalleEstimacion_Riesgo]").tooltip({ title: AyudasValorReisgoHtml, html: true, placement: "top", trigger: "focus" });

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



//buscar por peligro -Módulo requisitos legales otros
function BuscarPeligroRequisitosLegales(element) {

    var stringBusqueda = $(element).val()

    $.ajax({
        url: urlBase + '/RequisitosLegalesOtros/Busqueda_RequisitosLegales_Peligro',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            strPeligroBusqueda: stringBusqueda
        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusqueda').html(result)

            }
        }
    });
}






////Funcion para buscar los municipios por departamento
function BuscarMunicipios() {
    $.ajax({
        url: urlBase + '/General/ObtenerMunicipios',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            PK_Departamento: $idDepartamento.val()
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $idMunicipio.find("option").remove();//Removemos las opciones anteriores 
                $idMunicipio.append(new Option("-- Seleccionar --", ""));// agregamos la opcion de seleccionar              
                $.each(result, function (ind, element) {
                    $idMunicipio.append(new Option(element.NombreMunicipio, element.PK_Municipio));//agregamos las opciones consultadas
                })
            }
        }
    });
}

//funcion para permitir grabar cuando el riesgo controaldo
function GrabarRiesgoControlado(element) {
    if ($(element).val() == "true") {
        $("#siguientePanel").attr("hidden", "hidden");
        $("#guardarAhora").removeAttr("hidden");
    }
    else {
        $("#guardarAhora").attr("hidden", "hidden"); k
        $("#siguientePanel").removeAttr("hidden");
    }
}

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





function BusquedaPorActividadEconomica() {


    if ($idActividadEconomica.val() != 0) {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/Busqueda_PorActividadEconomica',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                Actividad_Economica: $idActividadEconomica.val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result)

                }
            }
        });

    }//END IF
    else {
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor seleccione el sector económico a consultar',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        })




    }




}

//MODULO REQUISITOS LEGALES - SELECCIONAR REQUISITOS LEGALES PARA CREAR MATRIZ
function AgregarRequisitoLegalMatriz() {
    //PopupPosition();
    $.ajax({
        url: urlBase + '/RequisitosLegalesOtros/AgregarRequisitoMatriz', // se crea la matriz
        data: {

            NombreMatriz: $idtxtNombreMatriz.val()

        },
        type: 'POST',
        success: function (result) {
            if (result.success) {
                //validacion = 1; 

                //$(this).prop('checked', false);

                Agregar_RequisitoMatriz();//se guarda en base de datos los requisitos seleccionados
                //$('#myModal1').modal('hide');
                //OcultarPopupposition();
            }
            else {
                //utils.showMessage("Seleccione un archivo para modificar", "error", $('#divMensaje'));
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Ya existe una matriz con ese nombre',
                    timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
                //OcultarPopupposition();
                //$('#myModal').modal('hide');
            }
            //document.getElementById("txtNombreMatriz").value = "";
            $("#idtxtNombreMatriz").val("");
            $("#idtxtNombreMatriz").trigger("reset");

        }

    });



}

//MODULO REQUISITOS LEGALES - SELECCIONAR REQUISITOS LEGALES PARA CREAR MATRIZ
//MODULO REQUISITOS LEGALES - SELECCIONAR REQUISITOS LEGALES PARA CREAR MATRIZ
function Agregar_RequisitoMatriz() {
    PopupPosition();
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    $.ajax({
        url: urlBase + '/RequisitosLegalesOtros/AgregarTablaRequisitos',
        data: {
            customerIDs: selectedIDs,
            idactividadeconomica: $idActividadEconomica.val()
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {

                OcultarPopupposition();
                $('#idNombreMatriz').val("");
                $('.myModal1').on('hidden.bs.modal', function () {
                    $(this).find('form')[0].reset(); //para borrar todos los datos que tenga los input, textareas, select.
                    $("#idNombreMatriz.error").remove();  //lo utilice para borrar la etiqueta de error del jquery validate
                });

                $('input:checkbox.checkBox').each(function () {
                    //$('#myModal1').modal('hide');
                    $(this).prop('checked', false);

                    //$('#checkAll').attr('checked', false);            
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Matriz creada satisfactoriamente',
                        //timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                });

            }
            else {
                $(this).prop('checked', false);
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Por favor seleccione un Requisito Legal para agregar a la matriz',
                    //timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
            }

        }
    });
}

function obtenerLugares() {

    $("#lugares").attr("hidden", "hidden");

    $("#Lugares").removeAttr("hidden");
}


ConstruirDatePickerPorElemento('idFechaPublicación');
//metodo para realizar busqueda en vista AGREGARNUEVOREQUISIREQUISITOLEGAL
function buscadorRequisitoslegales(PKMatriz, FKActividadEconomica) {

    var stringTipoNorma = $("#idTipoNorma").val()

    var dateFechaPublicacion = $("#idFechaPublicación").val()
    var stringEnte = $("#idEnte").val()
    var stringDescripción = $("#idDescripción").val()


    //if (stringTipoNorma != "" && dateFechaPublicacion != "" && stringEnte !== "" && stringDescripción !== "") {






    if (stringTipoNorma != "" && dateFechaPublicacion != "" && stringEnte == "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormafechapublicacion',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                fechapublicacion: dateFechaPublicacion,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });


    }//end if

    else if (stringTipoNorma != "" && dateFechaPublicacion == "" && stringEnte != "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormaEnteReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                EnteReq: stringEnte,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma != "" && dateFechaPublicacion == "" && stringEnte == "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormaDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                DescripcionReq: stringDescripción,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma == "" && dateFechaPublicacion != "" && stringEnte != "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalfechapublicacionEnteReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                fechapublicacion: dateFechaPublicacion,
                EnteReq: stringEnte,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if

    else if (stringTipoNorma == "" && dateFechaPublicacion == "" && stringEnte != "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalEnteReqDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                DescripcionReq: stringDescripción,
                EnteReq: stringEnte,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if



    else if (stringTipoNorma != "" && dateFechaPublicacion == "" && stringEnte == "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNorma',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }


            }
        });

    }//end if









    else if (stringTipoNorma == "" && dateFechaPublicacion != "" && stringEnte == "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalfechapublicacion',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                fechapublicacion: dateFechaPublicacion,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma == "" && dateFechaPublicacion != "" && stringEnte == "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalfechapublicacion',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                fechapublicacion: dateFechaPublicacion,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma == "" && dateFechaPublicacion == "" && stringEnte != "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalEnteReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                EnteReq: stringEnte,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if




    else if (stringTipoNorma == "" && dateFechaPublicacion == "" && stringEnte == "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                DescripcionReq: stringDescripción,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if



    else if (stringTipoNorma == "" && dateFechaPublicacion != "" && stringEnte == "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalfechapublicacionDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                DescripcionReq: stringDescripción,
                fechapublicacion: dateFechaPublicacion,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma != "" && dateFechaPublicacion != "" && stringEnte != "" && stringDescripción == "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                fechapublicacion: dateFechaPublicacion,
                EnteReq: stringEnte,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma != "" && dateFechaPublicacion == "" && stringEnte != "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                EnteReq: stringEnte,
                DescripcionReq: stringDescripción,
                FK_ActividadEconomica: FKActividadEconomica

            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma != "" && dateFechaPublicacion != "" && stringEnte == "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                fechapublicacion: dateFechaPublicacion,
                DescripcionReq: stringDescripción,
                FK_ActividadEconomica: FKActividadEconomica
            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


    else if (stringTipoNorma == "" && dateFechaPublicacion != "" && stringEnte != "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                fechapublicacion: dateFechaPublicacion,
                EnteReq: stringEnte,
                DescripcionReq: stringDescripción,
                FK_ActividadEconomica: FKActividadEconomica

            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if



    else if (stringTipoNorma != "" && dateFechaPublicacion != "" && stringEnte != "" && stringDescripción != "") {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                TipoNorma: stringTipoNorma,
                fechapublicacion: dateFechaPublicacion,
                EnteReq: stringEnte,
                DescripcionReq: stringDescripción,
                FK_ActividadEconomica: FKActividadEconomica

            },
            type: 'POST',
            success: function (result) {
                if (result.success == false) {
                    OcultarPopupposition();
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentran registros para el criterio de búsqueda',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })

                }
                else {
                    OcultarPopupposition();
                    $('#IDscBusqueda').html(result);

                }
            }
        });

    }//end if


        //}
    else if (stringTipoNorma == "" && dateFechaPublicacion == "" && stringEnte == "" && stringDescripción == "") {

        //utils.showMessage("Seleccione un archivo para eliminar", "error", $('#divMensaje'));
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor ingrese un ítem para realizar la búsqueda',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        })



    }





}



//se agrega un nuevo requisito legal a la matriz seleccionada
function Agregar_NuevoRequisitoMatriz(PKMatriz, FKActividadEconomica) {

    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());


        }
    });


    if (selectedIDs != 0) {
        PopupPosition();
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/AgregarNuevosRegistrosTablaRequisitos',
            data: {

                customerIDs: selectedIDs,
                //idactividadeconomica: $idActividadEconomica.val(),
                PK_Matriz: PKMatriz,
                FK_ActividadEconomica: FKActividadEconomica

            },
            type: 'POST',
            success: function (result) {
                if (result.success) {
                    $('input:checkbox.checkBox').each(function () {

                        $(this).prop('checked', false);
                        $('#checkAll').attr('checked', false);


                        OcultarPopupposition();
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Requisito Legal agregado satisfactoriamente',
                            //timer: 4000,
                            confirmButtonColor: '#7E8A97'
                        })

                        //location.reload();
                    });
                    //PopupPosition();
                    $.ajax({
                        url: urlBase + '/RequisitosLegalesOtros/BusquedaRequisitoLegalTipoNormafechapublicacion',//primero el modulo/controlador/metodo que esta en el controlador
                        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                            TipoNorma: null,
                            fechapublicacion: null,
                            FK_ActividadEconomica: null
                        },
                        type: 'POST',
                        success: function (result) {
                            $('#IDscBusqueda').html(result);
                        }
                    });




                }

                else {
                    $(this).prop('checked', false);
                    //utils.showMessage("Seleccione un archivo para eliminar", "error", $('#divMensaje'));
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Se generó un error al agregar el Requisito Legal',
                        //timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })
                }


            }
        });


    }



    else {

        //utils.showMessage("Seleccione un archivo para eliminar", "error", $('#divMensaje'));
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor seleccione un Requisito para agregar a la matriz',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        })
    }






}




function EliminarMatrices() {
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });
    if (selectedIDs != 0) {
        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/EliminarMatriz',
            data: {
                PKMatriz: selectedIDs
            },
            type: 'POST',
            success: function (result) {
                if (result.success) {
                    $('input:checkbox.checkBox').each(function () {
                        if ($(this).prop('checked')) {
                            $(this).closest('tr').remove();
                            $('#checkAll').attr('checked', false);
                            //utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje'));
                            swal({
                                type: 'success',
                                title: 'Estimado Usuario',
                                text: 'La matriz fue eliminada satisfactoriamente',
                                //timer: 4000,
                                confirmButtonColor: '#7E8A97'
                            })
                            //$('#modalEliminar').modal('hide');
                        }
                        //$(this).prop('checked', false);
                    });
                }
            }
        });
    }
    else {
        //utils.showMessage("Seleccione un archivo para eliminar", "error", $('#divMensaje'));
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor seleccione una Matriz para eliminar',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        })
    }
}


function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        language: {
                paginate: {
                    next: '&#8594;'; // or '→'
                    previous: '&#8592;' // or '←' 
                }
        }
        // Consider adding an ID to your table
        // incase a second table ever enters the picture.
        var items = $(idTabla).find(nameTr);
        var numItems = items.length;
        var perPage = 10;

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





function Eliminar_RequisitosLegalesOtros() {

    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    if (selectedIDs != 0) {




        $.ajax({
            url: urlBase + '/RequisitosLegalesOtros/Eliminar_RequisitosLegalesOtros',
            data: {
                PK_RequisitosLegales: selectedIDs
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
                                text: 'Requisito eliminado satisfactoriamente',
                                timer: 4000,
                                confirmButtonColor: '#7E8A97'
                            })

                            //$('#modalEliminar').modal('hide');

                        }
                    });
                }

            }
        });

    }
    else {

        //utils.showMessage("Seleccione un archivo para eliminar", "error", $('#divMensaje'));
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor seleccione un Requisito para eliminar',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        })



    }




}



function PaginadorBuscadorRequisitos(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        language: {
                paginate: {
                    next: '&#8594;'; // or '→'
                    previous: '&#8592;' // or '←' 
                }
        }
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



//function validarRequisitoslegalesAgregarMatrizCreada() {

//    jQuery.validator.addMethod("lettersonly", function (value, element) {
//        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
//    }, "Solo se permite el ingreso de letras");

//    $("#idAgregarRegistroMatrizCreada").validate({

//        //errorClass: "error",
//        rules: {


//            Tipo_Norma: {
//                required: true


//            },
//            Numero_Norma: { required: true },
//            FechaPublicacion: { required: true },
//            Ente: { required: true },
//            Articulo: { required: true },
//            Descripcion: { required: true },
//            Sugerencias: {
//                required: true,
//                lettersonly: true

//            },

//            Clase_De_Peligro: { required: true },
//            Peligro: { required: true, maxlength: 20 },
//            Aspectos: { required: true },
//            Impactos: { required: true },


//            Evidencia_Cumplimiento: { required: true },
//            FK_Cumplimiento_Evaluacion: { required: true },
//            Hallazgo: { required: true },
//            FK_Estado_RequisitoslegalesOtros: { required: true },
//            Responsable: { required: true },
//            Fecha_Seguimiento_Control: { required: true, },
//            Fecha_Actualizacion: { required: true }

//        },
//        messages: {

//            Tipo_Norma: {
//                required: "Se debe ingresar el Tipo de Norma",
//                //maxlength: "La longitud máxima es de 50 caracteres",

//            },
//            Numero_Norma: {
//                required: "Se debe ingresar el Número Norma",
//                //maxlength: "La longitud máxima del número es de 50 caracteres",
//            },
//            FechaPublicacion: {
//                required: "Se debe ingresar la Fecha Publicacion"
//            },

//            Ente: {
//                required: "Se debe ingresar el Ente correspondiente",
//                //maxlength: "La longitud máxima es de 20 caracteres",
//            },
//            Articulo: {
//                required: "Se debe ingresar el Artículo correspondiente",
//                //maxlength: "La longitud máxima es de 20 caracteres",
//            },

//            Descripcion: {
//                required: "Se debe ingresar la Descripción",
//                maxlength: "La longitud máxima es de 100 caracteres",
//            },

//            Sugerencias: {
//                required: "Se deben ingresar las Sugerencias",
//                //maxlength: "La longitud máxima es de 50 caracteres",
//            },

//            Clase_De_Peligro: {
//                required: "Se debe ingresar la Clase de Peligro",
//                //maxlength: "La longitud máxima es de 20 caracteres",
//            },

//            Peligro: {
//                required: "Se debe ingresar el Peligro",
//                //maxlength: "La longitud máxima es de 20 caracteres",
//            },

//            Aspectos: {
//                required: "Se debe ingresar el Aspecto"
//            },
//            Impactos: {
//                required: "Se debe ingresar el Impacto"
//            },


//            Evidencia_Cumplimiento: {
//                required: "Se debe ingresar la Evidencia"
//            },
//            FK_Cumplimiento_Evaluacion: {
//                required: "Se debe ingresar el Cumplimiento"
//            },
//            Hallazgo: {
//                required: "Se debe ingresar el Hallazgo"
//            },

//            FK_Estado_RequisitoslegalesOtros: {
//                required: "Se debe ingresar el Estado del Requisito Legal"
//            },
//            Responsable: {
//                required: "Se debe ingresar el Responsable"
//            },

//            Fecha_Seguimiento_Control: {
//                required: "Se debe ingresar la Fecha de Seguimiento del Control"
//            },

//            Fecha_Actualizacion: {
//                required: "Se deben ingresar la Fecha de Actualización"
//            }




//        }

//    });

//}




$('#someId').on('keyUp', function () {
    var text = $('#someId').val();
    if (text.length > 0)
        $('#someId').attr('disabled', 'disabled');

});




//$('#checkAll').click(function () { *****************VALIDARMETODO**************************

//        if ($(this).is(':checked')) {
//        //$("input[type=checkbox]").prop('checked', true); //todos los check
//            $('input:checkbox').prop('checked', true);
//    } else {
//        //$("input[type=checkbox]").prop('checked', false);//todos los check
//            $('input:checkbox').prop('checked', false);
//    }




//});
//$("#checkAll").change(function () {
//    if ($(this).is(':checked')) {
//        //$("input[type=checkbox]").prop('checked', true); //todos los check
//        $("#checkAll input[type=checkbox]").prop('checked', true); //solo los del objeto #diasHabilitados
//    } else {
//        //$("input[type=checkbox]").prop('checked', false);//todos los check
//        $("#checkAll input[type=checkbox]").prop('checked', false);//solo los del objeto #diasHabilitados
//    }
//});



//funcion para formatear el texto de los mensajes de Jquery Validate
$(document).ready(function () {
    $.extend(jQuery.validator.messages, {
        minlength: $.validator.format("Debe ingresar mínimo {0} caracteres"),

    });
})


function validarNombreMatriz(idactividadeconomica) {

    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    //if(validacion == 0){// validacion para que no muestre mensaje de validacion indicando que se deben seleccionar los qrequisitos legales
    for (var i = 0; i < 1; i++) {
        var variable = selectedIDs;
    }
    if (variable == undefined || variable == 0) {
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Debe seleccionar los Requisitos Legales para crear la matriz',
            confirmButtonColor: '#7E8A97'
        });

    } else


        //jQuery.validator.addMethod("lettersonly", function (value, element) {
        //    return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
        //}, "Solo se permite el ingreso de letras");
        var idNombreMatriz = $("#idNombreMatriz").val();
    if (idNombreMatriz != '' && idNombreMatriz.length <= 50) {
        AgregarRequisitoLegalMatriz(idactividadeconomica);

    }
    else {

        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'El nombre de la matriz no puede ir en blanco o exceder los 50 caracteres',
            confirmButtonColor: '#7E8A97'
        });

    }

    //$('#myModal1').modal('hide'); 


}


//Cuando se presiona <ENTER>
$('#idNombreMatriz').on('keydown', function (e) {


    var keyCode = e.keyCode || e.which;
    if (keyCode == 13 || keyCode == 9) {
        var idNombreMatriz = $("#idNombreMatriz").val();
        if (idNombreMatriz != '' && idNombreMatriz.length <= 50) {
        } else
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'El nombre de la matriz no puede ir en blanco o exceder los 50 caracteres',
                confirmButtonColor: '#7E8A97'
            });
        return false;
    }
});

ConstruirDatePickerPorElemento('idFechaPublicación');
ConstruirDatePickerPorElemento('idFecha_Seguimiento_Control');
ConstruirDatePickerPorElemento('idFecha_Actualizacion');



function validarRequisitoslegalesModificarRequisitoMat() {
    var hoy = new Date();
    var fechaPublicacion = $("#idFechaPublicacion").val()

    var FechaSeguimientoControl = $("#idFecha_Seguimiento_Control").val()
    var FechaActualizacion = $("#idFecha_Actualizacion").val()

    var dia = hoy.getDate();
    var mes = hoy.getMonth() + 1;
    var anio = hoy.getFullYear();

    if (mes < 10) {
        fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
    } else {

        fecha_actual = String(dia + "/" + mes + "/" + anio);
    }
    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaPublicacion)) {


        swal({
            type: 'warning',
            title: 'Estimado usuario',
            text: 'La fechas ingresadas no pueden ser superiores a la fecha actual',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFechaPublicacion").val("")
    }
    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', FechaSeguimientoControl)) {


        swal({
            type: 'warning',
            title: 'Estimado usuario',
            text: 'La fechas ingresadas no pueden ser superiores a la fecha actual',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFecha_Seguimiento_Control").val("")
    }
    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', FechaActualizacion)) {
        swal({
            type: 'warning',
            title: 'Estimado usuario',
            text: 'La fechas ingresadas no pueden ser superiores a la fecha actual',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFecha_Actualizacion").val("")
    }

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s,"",-]+$/i.test(value);
    }, "Sólo se permite el ingreso de letras");

    $("#idModificarRequisitoMatriz").validate({
        //errorClass: "error",
        rules: {
            Tipo_Norma: { required: true },
            Numero_Norma: { required: true },
            FechaPublicacion: { required: true },
            Ente: { required: true },
            Articulo: { required: true },
            Descripcion: { required: true },
            Sugerencias: {
                required: true,
                //lettersonly: true
            },
            Clase_De_Peligro: { required: true },
            Peligro: { required: true },
            Aspectos: { required: true },
            Impactos: { required: true },
            Evidencia_Cumplimiento: { required: true },
            FK_Cumplimiento_Evaluacion: { required: true },
            Hallazgo: { required: true },
            FK_Estado_RequisitoslegalesOtros: { required: true },
            Responsable: { required: true },
            Fecha_Seguimiento_Control: { required: true, },
            Fecha_Actualizacion: { required: true }
        },
        messages: {
            Tipo_Norma: {
                required: "Se debe ingresar el Tipo de Norma",
                //maxlength: "La longitud máxima es de 50 caracteres",
            },
            Numero_Norma: {
                required: "Se debe ingresar el Número Norma",
                //maxlength: "La longitud máxima del número es de 50 caracteres",
            },
            FechaPublicacion: {
                required: "Se debe ingresar la Fecha Publicación"
            },

            Ente: {
                required: "Se debe ingresar el Ente correspondiente",
                //maxlength: "La longitud máxima es de 200 caracteres",
            },
            Articulo: {
                required: "Se debe ingresar el Artículo correspondiente",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },
            Descripcion: {
                required: "Se debe ingresar la Descripción",
                //maxlength: "La longitud máxima es de 100 caracteres",
            },
            Sugerencias: {
                required: "Se deben ingresar las Sugerencias",
                //maxlength: "La longitud máxima es de 50 caracteres",
            },
            Clase_De_Peligro: {
                required: "Se debe ingresar la Clase de Peligro",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },
            Peligro: {
                required: "Se debe ingresar el Peligro",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },
            Aspectos: {
                required: "Se debe ingresar el Aspecto",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },
            Impactos: {
                required: "Se debe ingresar el Impacto",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },
            Evidencia_Cumplimiento: {
                required: "Se debe ingresar la Evidencia",
                //maxlength: "La longitud máxima es de 50 caracteres",
            },
            FK_Cumplimiento_Evaluacion: {
                required: "Se debe ingresar el Cumplimiento",
                //maxlength: "La longitud máxima es de 30 caracteres",
            },
            Hallazgo: {
                required: "Se debe ingresar el Hallazgo",
                //maxlength: "La longitud máxima es de 50 caracteres",
            },
            FK_Estado_RequisitoslegalesOtros: {
                required: "Se debe ingresar el Estado del Requisito Legal",
                //maxlength: "La longitud máxima es de 30 caracteres",
            },
            Responsable: {
                required: "Se debe ingresar el Responsable",
                //maxlength: "La longitud máxima es de 30 caracteres",
            },
            Fecha_Seguimiento_Control: {
                required: "Se debe ingresar la Fecha de Seguimiento del Control"
            },
            Fecha_Actualizacion: {
                required: "Se deben ingresar la Fecha de Actualización"
            }
        }
    });
}


function validarRequisitoslegalesAgregarMatrizCreada() {

    var hoy = new Date();
    var fechaPublicacion = $("#idFechaPublicacion").val()

    var FechaSeguimientoControl = $("#idFecha_Seguimiento_Control").val()
    var FechaActualizacion = $("#idFecha_Actualizacion").val()

    var dia = hoy.getDate();
    var mes = hoy.getMonth() + 1;
    var anio = hoy.getFullYear();

    if (mes < 10) {
        fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
    } else {

        fecha_actual = String(dia + "/" + mes + "/" + anio);
    }
    if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaPublicacion)) {


        swal({
            type: 'warning',
            title: 'Estimado usuario',
            text: 'La fecha de publicación no puede ser superior a la fecha actual',
            confirmButtonColor: '#7E8A97'
        });
        $("#idFechaPublicacion").val("")
    }

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s,"",-]+$/i.test(value);
    }, "Sólo se permite el ingreso de letras");

    $("#idGuardarRegistroMatrizCreada").validate({

        //errorClass: "error",
        rules: {


            Tipo_Norma: {
                required: true


            },
            Numero_Norma: { required: true },
            FechaPublicacion: { required: true },
            Ente: { required: true },
            Articulo: { required: true },
            Descripcion: { required: true },
            Sugerencias: {
                required: true,
                //lettersonly: true

            },

            Clase_De_Peligro: { required: true },
            Peligro: { required: true },
            Aspectos: { required: true },
            Impactos: { required: true }




        },
        messages: {

            Tipo_Norma: {
                required: "Se debe ingresar el Tipo de Norma",
                //maxlength: "La longitud máxima es de 50 caracteres",

            },
            Numero_Norma: {
                required: "Se debe ingresar el Número Norma",
                //maxlength: "La longitud máxima del número es de 50 caracteres",
            },
            FechaPublicacion: {
                required: "Se debe ingresar la Fecha Publicacion"
            },

            Ente: {
                required: "Se debe ingresar el Ente correspondiente",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },
            Articulo: {
                required: "Se debe ingresar el Artículo correspondiente",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },

            Descripcion: {
                required: "Se debe ingresar la Descripción",
                maxlength: "La longitud máxima es de 100 caracteres",
            },

            Sugerencias: {
                required: "Se deben ingresar las Sugerencias",
                //maxlength: "La longitud máxima es de 50 caracteres",
            },

            Clase_De_Peligro: {
                required: "Se debe ingresar la Clase de Peligro",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },

            Peligro: {
                required: "Se debe ingresar el Peligro",
                //maxlength: "La longitud máxima es de 20 caracteres",
            },

            Aspectos: {
                required: "Se debe ingresar el Aspecto"
            },
            Impactos: {
                required: "Se debe ingresar el Impacto"
            }



        }

    });

}






function Agregar_ReventanaMatriz() {

    swal({
        title: 'Enter your password',
        input: 'password',
        inputPlaceholder: 'Enter your password',
        inputAttributes: {
            'maxlength': 10,
            'autocapitalize': 'off',
            'autocorrect': 'off'
        }
    }).then(function (password) {
        if (password) {
            swal({
                type: 'success',
                html: 'Entered password: ' + password
            })
        }
    })



}