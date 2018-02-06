var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

$(document).ready(function () {
    $('#descargarExcelComparativo').hide();
    var indi = $("#frmindicadores")
    indi.validate({
        rules: {
            AnioSeleccionado: {
                required: true
            },
            PrimerAnio: {
                required: true
            },
            SegundoAnio: {
                required: true
            },
            ConstanteSeleccionada: {
                required: true
            }           
        },
        messages: {
            AnioSeleccionado: {
                required: "Debe seleccionar un año de gestion"
            },
            PrimerAnio: {
                required: "Debe seleccionar un año de gestion"
            },
            SegundoAnio: {
                required: "Debe seleccionar un año de gestion"
            },
            ConstanteSeleccionada: {
                required: "Debe seleccionar un valor de constante"
            }           
        }
    });
    var constante = $("#ConstanteSeleccionada").val();
    var anioSel = $("#AnioSeleccionado").val();

    $('#4').change(function () {
        if ($(this).is(":checked")) {
            $("#descargarTablaExcel").hide();
            $('.campos-comparacion-indicadores').show();
            $('.campos-consultar-comparacion-indicadores').show();
            $('#consultarIndicador').hide();
            $('#AnioSeleccionado').hide();
            $('#agestion').hide();
            $('#descargarExcelComparativo').hide();
            $('#panelAcumulado').hide();
            $('#banneracumulado').hide();               
            $('#panelIndicador').hide();
            $('#bannerindicador').hide();            
        }
        else {
            $('#Indicadores').empty();
            $('#consultarIndicador').show();
            $('.campos-comparacion-indicadores').hide();
            $('.campos-consultar-comparacion-indicadores').hide();
            $('#AnioSeleccionado').show();
            $('#agestion').show();
            $('#descargarExcelComparativo').hide();
            $('#panelAcumulado').hide();
            $('#banneracumulado').hide();            
            $('#panelIndicador').hide();
            $('#bannerindicador').hide();            
        }
    });
});

function MostrarIndicadorAusentismo() {

   
    var IdContingencia = $('input:radio[id=tipoComparacion]:checked').val();
    var  AnioSeleccionado= $("#AnioSeleccionado").val();
    var ConstanteSeleccionada= $("#ConstanteSeleccionada").val();
    var IdEmpresaUsuaria = $('#IdEmpresaUsuaria').val();
    var  PrimerAnio= $("#PrimerAnio").val();
    var SegundoAnio = $("#SegundoAnio").val();

    if (IdContingencia == 1) {
        contigenciaTexto = "ENFERMEDAD GENERAL";
    }
    else if (IdContingencia == 2) {
        contigenciaTexto = "ENFERMEDAD LABORAL";
    }
    else if (IdContingencia == 3) {
        contigenciaTexto = "ACCIDENTE DE TRABAJO";
    }


    if (AnioSeleccionado == "") {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'El párametro año gestión es obligatorio',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }


    if (ConstanteSeleccionada == "") {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'El párametro Constante K es obligatorio',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }

    if (IdContingencia == null)
    {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'Por favor seleccione una  contigencia',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }

  

    $.ajax({
        data: { AnioSeleccionado: AnioSeleccionado, IdContingencia: IdContingencia,contigenciaTexto:contigenciaTexto, ConstanteSeleccionada: ConstanteSeleccionada, IdEmpresaUsuaria: IdEmpresaUsuaria },
        url: urlBase + '/ReportesAplicacion/IndicadoresAusentismo',
        type: 'POST'
    });
    location.reload();
    OcultarPopupposition();
   

}

function MostrarIndicadorAusComparar() {


    var IdContingencia = $('input:radio[id=tipoComparacion]:checked').val();
    var AnioSeleccionado = $("#AnioSeleccionado").val();
    var ConstanteSeleccionada = $("#ConstanteSeleccionada").val();
    var IdEmpresaUsuaria = $('#IdEmpresaUsuaria').val();
    var PrimerAnio = $("#PrimerAnio").val();
    var SegundoAnio = $("#SegundoAnio").val();
    var contigenciaTexto = "";


    if (IdContingencia == 1)
    {
        contigenciaTexto = "ENFERMEDAD GENERAL";
    }
    else if (IdContingencia == 2) {
        contigenciaTexto = "ENFERMEDAD LABORAL";
    }
    else if (IdContingencia == 3) {
        contigenciaTexto = "ACCIDENTE DE TRABAJO";
    }

    if (ConstanteSeleccionada == "") {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'El párametro Constante K es obligatorio',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }

    if (IdContingencia == null) {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'Por favor seleccione una  contigencia',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }

    if (PrimerAnio == "") {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'El párametro primer año es obligatorio',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }

    if (SegundoAnio == "") {
        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'El párametro según año es obligatorio',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }
    $.ajax({
        data: { PrimerAnio: PrimerAnio,SegundoAnio:SegundoAnio, IdContingencia: IdContingencia,contigenciaTexto:contigenciaTexto, ConstanteSeleccionada: ConstanteSeleccionada, IdEmpresaUsuaria: IdEmpresaUsuaria },
        url: urlBase + '/ReportesAplicacion/IndicadoresAusentismoComparacion',
        type: 'POST'
    });
    location.reload();
    OcultarPopupposition();

}
function SeleccionarReporteAplicacion() {
    var TipoReporte = $("#TipoReporte").val();
  
    var periodo = $("#anio").val();
    var idSede = $("#FKSede").val();
    var sedeTexto = $("#FKSede option:selected").html();
    var estado = $("#Estado").val();
    var anio = $("#anio").val();
    if (TipoReporte == "") {

        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'Por favor seleccione un tipo de Estadística',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }


    switch (TipoReporte) {



        case "ReportePresupuesto":


            if (periodo == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro periodo es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            if (idSede == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro sede es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

    }

    if ($("#formReportesAplicacion").valid()) {
        PopupPosition();

      
        switch(TipoReporte) {
            case "ReporteAusentismo":
                 $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReporteAusentismo',
                    type: 'POST'
                 });
                 location.reload();
                 OcultarPopupposition();
                break;
            case "ReportePresupuesto":
                $.ajax({
                    data: { periodo: periodo, idSede: idSede, sedeTexto: sedeTexto },
                    url: urlBase + '/ReportesAplicacion/ReportePresupuesto',                 
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();

                break;
            case "ReporteCompetencias":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReporteCompetencias',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "ReporteMetodologiaInsht":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReporteMetodologiaInsht',         
                    type: 'POST', 
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "ReportePlanTrabajo":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReportePlanTrabajo',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "ReporteDiagnosticoSalud":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReporteDiagnosticoSalud',       
                    type: 'POST',           
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "AccionesCorrectivas":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/AccionesCorrectivas',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "GestionCambio":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/GestionCambio',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "PlanCapacitacion":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/PlanCapacitacion',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "Incidentes":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/Incidentes',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "InspeccionesSeguridad":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/InspeccionesSeguridad',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "PerfilSocio":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/PerfilSocio',
                    type: 'POST',
                    success: function (result) {
                    }
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "IdentificacionPeligro":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/IdentificacionPeligro',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "MetodologiaRam":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/MetodologiaRam',        
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "PuestosTrabajo":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/PuestosTrabajo',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "PlanEmergenciaAccion":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/PlanEmergenciaAccion',
                    type: 'POST', 
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "PlanEmergenciaGeneral":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/PlanEmergenciaGeneral',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "ActosCondicionesInseguras":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ActosCondicionesInseguras',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "AdquisicionesBienes":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/AdquisicionesBienes',
                    type: 'POST',
                });
                location.reload();
                OcultarPopupposition();
                break;
            case "RelacionesLaborales":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/RelacionesLaborales',
                    type: 'POST',
                 
                });
                location.reload();
                OcultarPopupposition();
                break;

                //Nuevo


            case "MedidasPrevencion":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/MedidasDePrevencion',
                    type: 'POST',

                });
                location.reload();
                OcultarPopupposition();
                break;


            case "PlanesDeAccion":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/PlanesDeAccion',
                    type: 'POST',

                });
                location.reload();
                OcultarPopupposition();
                break;

            case "ReporteEnfermedadLaboral":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReporteEnfermedadLaboral',
                    type: 'POST',

                });
                location.reload();
                OcultarPopupposition();
                break;

            case "InvestigacionATEL":
                $.ajax({
                    url: urlBase + '/ReportesAplicacion/ReporteIncidenteAT',
                    type: 'POST',

                });
                location.reload();
                OcultarPopupposition();
                break;

            case "ActividadesComunicaciones":
                $.ajax({

                    data: { anio: anio, estado:estado },
                    url: urlBase + '/ReportesAplicacion/ReporteActividadesComunicaciones',
                    type: 'POST',

                });
                location.reload();
                OcultarPopupposition();
                break;

        }
        
    }

  
}
function SeleccionarReporteIndicadorSistema() {

    var TipoReporte = $("#TipoReporte").val();

    var periodo = $("#anio").val();
    var idSede = $("#FKSede").val();

    switch (TipoReporte) {


        case "ReporteAusentismo":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ReportePresupuesto":
            $("#param").show();
            $("#SedeInd").show();
            $("#divAnio").show();
            $("#divEvento").hide();
            break;


        case "ReporteCompetencias":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;


        case "ReporteMetodologiaInsht":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;


        case "ReportePlanTrabajo":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ReporteDiagnosticoSalud":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "AccionesCorrectivas":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "GestionCambio":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;


        case "PlanCapacitacion":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;


        case "Incidentes":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;
        case "InspeccionesSeguridad":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;
        case "PerfilSocio":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "IdentificacionPeligro":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "MetodologiaRam":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "PuestosTrabajo":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "PlanEmergenciaAccion":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "PlanEmergenciaGeneral":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ActosCondicionesInseguras":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "AdquisicionesBienes":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "RelacionesLaborales":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ComunicacionesExternas":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ComunicacionesInternas":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "MedidasPrevencion":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "PlanesDeAccion":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ReporteEnfermedadLaboral":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "InvestigacionATEL":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ComunicacionesAPP":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").hide();
            $("#divEvento").hide();
            break;

        case "ActividadesComunicaciones":
            $("#param").hide();
            $("#SedeInd").hide();
            $("#divAnio").show();
            $("#divEstado").show();
            break;

            

    }

}
function SeleccionarReporteIndicador() {

    var TipoIndicador = $("#TipoReporteInd").val();

 

    switch (TipoIndicador) {
        case "ReporteSST":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;
        case "CondicionActosInseguros":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "PlanTrabajoAnual":
     
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();

     
            break;

        case "EstandaresMinimos":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;


        case "AccidentesDeTrabajo":
            $("#divConstanteK").show();
            $("#SedeInd").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "TasaAccidentalidad":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "CapacitacionEntrenamiento":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "FrecuenciaAusentismo":
            $("#SedeInd").hide();
            $("#divConstanteK").show();
            $("#divContigencia").show();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "SeveridadAusentismo":
            $("#SedeInd").hide();
            $("#divConstanteK").show();
            $("#divContigencia").show();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;


        case "SeveridadAccidenteTrabajo":
            $("#SedeInd").hide();
            $("#divConstanteK").show();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "LesionesIncapacitantes":
            $("#SedeInd").hide();
            $("#divConstanteK").show();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "CumplimientoRequisitosLegales":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "ComiteCoppast":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;



        case "DxSalud":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").show();
            $("#divAnio").show();
            $("#divEstado").hide();
            break;

        case "PerfilSocioD":
            $("#divAnio").hide();
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").show();
            $("#divEstado").hide();
            break;

        case "Comunicaciones":
            $("#divAnio").show();
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#procesoInd").hide();
            $("#divEstado").show();
            break;

    }

}


function SeleccionarReporteIndicadorDatos() {

    var TipoIndicador = $("#TipoReporteInd").val();

    switch (TipoIndicador) {
        case "ReporteSST":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;
        case "CondicionActosInseguros":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").show();
            $("#procesoInd").hide();
            break;

        case "PlanTrabajoAnual":

            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();

            break;

        case "EstandaresMinimos":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;


        case "AccidentesDeTrabajo":
            $("#divConstanteK").hide();
            $("#SedeInd").show();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "TasaAccidentalidad":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "CapacitacionEntrenamiento":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "FrecuenciaAusentismo":
         
            $("#divConstanteK").hide();
            $("#divContigencia").show();
            $("#SedeInd").show();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "SeveridadAusentismo":
     
            $("#divConstanteK").hide();
            $("#divContigencia").show();
            $("#SedeInd").show();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;


        case "AccidenteDeTrabajo":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "LesionesIncapacitantes":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "CumplimientoRequisitosLegales":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "RequisitosLegales":
            $("#SedeInd").hide();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").hide();
            break;

        case "DxCondicionesSalud":
            $("#SedeInd").show();
            $("#divConstanteK").hide();
            $("#divContigencia").hide();
            $("#divTipoReporte").hide();
            $("#procesoInd").show();
            break;

    }

}

function SeleccionarDatosInd() {

    var anio = $("#anio").val();

    var idSede = $("#FKSede").val();
    var sedeTexto = $("#FKSede option:selected").html();
    var TipoIndicador = $("#TipoReporteInd").val();
    var constanteK = $("#ConstanteK").val();
    var contigencia = $("#Contigencia").val();
    var contigenciaTexto = $("#Contigencia option:selected").html();
    var tipoReporte = $("#TipoReporte").val();
    var idProceso = $("#Procesos").val();

    // var contigenciaTexto = $("#Contigencia").text();

    if (TipoIndicador == "") {

        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'Por favor seleccione un tipo de reporte',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }

    switch (TipoIndicador) {

    

        case "ReporteSST":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

   

      

        case "CondicionActosInseguros":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;
            
        case "PlanTrabajoAnual":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

        case "EstandaresMinimos":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;


        case "AccidentesDeTrabajo":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

        case "TasaAccidentalidad":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

        case "CapacitacionEntrenamiento":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;
            
        case "FrecuenciaAusentismo":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            if (contigencia == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro contingencia es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            
            break;


        case "SeveridadAusentismo":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            if (contigencia == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parámetro contingencia es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            
            break;
   


    }


    







    if ($("#formReportesIndicadores").valid()) {
        PopupPosition();
        switch (TipoIndicador) {
            case "ReporteSST":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/ReporteAccionCorrectivaPreventivaDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "CondicionActosInseguros":
                $.ajax({
                    data: { anio: anio, tipoReporte: tipoReporte, idSede: idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorCondicionesInsegurasDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "PlanTrabajoAnual":


                $.ajax({
                    data: { anio: anio, idSede: idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorPlanTrabajoAnualDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "EstandaresMinimos":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/IndicadorEstandaresMinimosDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "AccidentesDeTrabajo":
                $.ajax({
                    data: { anio: anio,idSede:idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorAccidentesDeTrabajoDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "TasaAccidentalidad":
                $.ajax({
                    data: { anio: anio,idSede:idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorTasaAccidentalidadDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "CapacitacionEntrenamiento":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/IndicadorCapacitacionEntrenamientoDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "FrecuenciaAusentismo":
                $.ajax({
                    data: { anio: anio, idSede: idSede, contigencia: contigencia },
                    url: urlBase + '/ReportesAplicacion/IndicadorFrecuenciaAusentismoDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "SeveridadAusentismo":
                $.ajax({
                    data: { anio: anio, contigencia: contigencia,idSede:idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorSeveridadAusentismoDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "AccidenteDeTrabajo":
                $.ajax({
                    data: { anio: anio,idSede:idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorAccidenteDeTrabajoDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "RequisitosLegales":
                $.ajax({
                    data: { anio: anio},
                    url: urlBase + '/ReportesAplicacion/IndicadorCumplimientoRequisitosLegalesDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "LesionesIncapacitantes":
                $.ajax({
                    data: { anio: anio, idSede:idSede },
                    url: urlBase + '/ReportesAplicacion/IndicadorLesionesIncapacitantesDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "DxCondicionesSalud":
                $.ajax({
                    data: { anio: anio, idSede: idSede,idProceso:idProceso },
                    url: urlBase + '/ReportesAplicacion/IndicadorDxCondicionesSaludDatos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;




        }
    }
}


function SeleccionarReporteInd() {

    var anio = $("#anio").val();

    var idSede = $("#FKSede").val();
    var sedeTexto = $("#FKSede option:selected").html();
    var TipoIndicador = $("#TipoReporteInd").val();
    var constanteK = $("#ConstanteK").val();
    var contigencia = $("#Contigencia").val();
    var contigenciaTexto = $("#Contigencia option:selected").html();
    var idProceso = $("#Procesos").val();
    var procesoTexto = $("#Procesos option:selected").html();
    var estado = $("#Estado").val();
  // var contigenciaTexto = $("#Contigencia").text();


    if (idProceso == "")
    {
        procesoTexto = "";
    }
    if (idSede == "")
    {
        sedeTexto = "";
    }
    if (TipoIndicador == "") {

        swal({
            type: 'warning',
            title: 'Estimado Usuario:',
            text: 'Por favor seleccione un indicador',
            confirmButtonColor: '#7E8A97'
        });

        return false;
    }
   
    switch (TipoIndicador) {

        case "PlanTrabajoAnual":

            if (idSede == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Falta párametro por llenar',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            break;

        case "AccidentesDeTrabajo":


            if (constanteK == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Falta párametro por llenar',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            break;

        case "FrecuenciaAusentismo":
      

            if (constanteK == "" || contigencia=="") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Falta párametro por llenar',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

        case "SeveridadAusentismo":


            if (constanteK == "" || contigencia == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Falta párametro por llenar',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;


        case "SeveridadAccidenteTrabajo":


            if (constanteK == "" ) {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Falta párametro por llenar',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;


        case "LesionesIncapacitantes":


            if (constanteK == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'Falta párametro por llenar',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;

        case "ComiteCoppast":


            if (idSede == "" ) {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El párametro sede es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El párametro anio es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }

            break;

        case "DxSalud":


            if (anio == "") {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario:',
                    text: 'El parametro año es obligatorio',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
            break;
    }


 

    if ($("#formReportesIndicadores").valid()) {
        PopupPosition();
        switch (TipoIndicador) {
            case "ReporteSST":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/ReporteAccionCorrectivaPreventiva',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "CondicionActosInseguros":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/IndicadorCondicionesInseguras',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "PlanTrabajoAnual":
               

                $.ajax({
                    data: { anio: anio, idSede: idSede, sedeTexto: sedeTexto },
                    url: urlBase + '/ReportesAplicacion/IndicadorPlanTrabajoAnual',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "EstandaresMinimos":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/IndicadorEstandaresMinimos',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "AccidentesDeTrabajo":
                $.ajax({
                    data: { anio: anio, constanteK: constanteK },
                    url: urlBase + '/ReportesAplicacion/IndicadorAccidentesDeTrabajo',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "TasaAccidentalidad":
                $.ajax({
                    data: { anio: anio},
                    url: urlBase + '/ReportesAplicacion/IndicadorTasaAccidentalidad',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "CapacitacionEntrenamiento":
                $.ajax({
                    data: { anio: anio },
                    url: urlBase + '/ReportesAplicacion/IndicadorCapacitacionEntrenamiento',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "FrecuenciaAusentismo":
                $.ajax({
                    data: { anio: anio, constanteK: constanteK, contigencia: contigencia, contigenciaTexto: contigenciaTexto },
                    url: urlBase + '/ReportesAplicacion/IndicadorFrecuenciaAusentismo',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "SeveridadAusentismo":
                $.ajax({
                    data: { anio: anio, constanteK: constanteK, contigencia: contigencia, contigenciaTexto: contigenciaTexto },
                    url: urlBase + '/ReportesAplicacion/IndicadorSeveridadAusentismo',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "AccidenteDeTrabajo":
                $.ajax({
                    data: { anio: anio, constanteK: constanteK, contigencia: contigencia },
                    url: urlBase + '/ReportesAplicacion/IndicadorAccidenteDeTrabajo',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "SeveridadAccidenteTrabajo":
                $.ajax({
                    data: { anio: anio, constanteK: constanteK },
                    url: urlBase + '/ReportesAplicacion/IndicadorSeveridadAccidenteTrabajo',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "LesionesIncapacitantes":
                $.ajax({
                    data: { anio: anio, constanteK: constanteK },
                    url: urlBase + '/ReportesAplicacion/IndicadorLesionesIncapacitantes',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "CumplimientoRequisitosLegales":
                $.ajax({
                    data: { anio: anio},
                    url: urlBase + '/ReportesAplicacion/IndicadorCumplimientoRequisitosLegales',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "ComiteCoppast":
                $.ajax({
                    data: { anio: anio, idSede: idSede, sedeTexto: sedeTexto},
                    url: urlBase + '/ReportesAplicacion/IndicadorComiteCoppast',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;


            case "PerfilSocioD":
                $.ajax({
                    data: {  idSede: idSede, sedeTexto: sedeTexto,idProceso:idProceso,procesoTexto:procesoTexto },
                    url: urlBase + '/ReportesAplicacion/IndicadorPerfilSocioDemografico',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;

            case "Comunicaciones":
                $.ajax({
                    data: { anio: anio , estado:estado},
                    url: urlBase + '/ReportesAplicacion/IndicadorComunicaciones',
                    type: 'POST'
                });
                location.reload();
                OcultarPopupposition();
                break;


        }
    }
}


function seleccionarGraficasAunsentismo() {



    var anio = $("#anio").val();
    var idOrigen = $("#tipoOrigen").val();
    var IdEmpresaUsuaria = $("#IdEmpresaUsuaria").val();
    var idSede = $("#Sede").val();
    var IdDepartamento = $("#Departamento").val();
    var IdReporte = $("#IdReporte").val();

 
    if (anio == "")
    {
        
            swal({
                type: 'warning',
                title: 'Estimado Usuario:',
                text: 'Por favor seleccione el año',
                confirmButtonColor: '#7E8A97'
            });

            return false;
    }

  
    
    switch (IdReporte) {
       
        case "AC":
        
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },
                url: urlBase + '/ReportesAplicacion/ReporteDiasAunsentismoPorContigencia',
                type: 'POST',
            
            });
           
         location.reload(true);

            OcultarPopupposition();
            break;
        case "NC":
          
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteNumeroDeEventosPorContigencia',
                type: 'POST'
            });
        location.reload(true);

            OcultarPopupposition();
            break;
        case "ADP":
        
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteDiasAusentismoPorDepartamentos',
                type: 'POST'
            });
           location.reload(true);

            OcultarPopupposition();
            break;
        case "DCIE":
            
       
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteDiasAusentismoPorEnfermedadesCIE10',
                type: 'POST'
            });
          location.reload(true);
   
      
            OcultarPopupposition();
            break;

        case "NCIE":
         
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },
                url: urlBase + '/ReportesAplicacion/ReporteEventosPorEnfermedadesCIE10',
                type: 'POST'
            });
            location.reload(true);
           
            OcultarPopupposition();

            break;

        case "DP":
            $("IDReportesAus").removeAttr("hidden");
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteDiasAusentismoPorProceso',
                type: 'POST'
            });
            location.reload(true);
       
            OcultarPopupposition();
            break;

        case "DS":
     
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteDiasAusentismoPorSede',
                type: 'POST'
            });
            location.reload(true);
        
            OcultarPopupposition();
            break;

        case "PC":
         
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteCostoPorContigencia',
                type: 'POST'
            });
            location.reload(true);
   
            OcultarPopupposition();
            break;
        case "AEPS":
         
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },
                url: urlBase + '/ReportesAplicacion/ReporteAusentismoPorEPS',
                type: 'POST'
            });
           location.reload(true);
           
            OcultarPopupposition();
            break;
        case "ASX":
            
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },
                url: urlBase + '/ReportesAplicacion/ReporteAusentismoPorSexo',
                type: 'POST'
            });
           location.reload(true);
        
            OcultarPopupposition();
            break;
        case "AV":
            $("IDReportesAus").removeAttr("hidden");
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteAusentismoPorTipoVinculacion',
                type: 'POST'
            });
            location.reload(true);
            
            OcultarPopupposition();
            break;
        case "AO":
       
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteAusentismoPorOcupacionCIUO',
                type: 'POST'
            });
            location.reload(true);
          

            OcultarPopupposition();
            break;
        case "AET":
       
            $.ajax({
                data: { anio: anio, idorigen: idOrigen, idEmpresa: IdEmpresaUsuaria, idSede: idSede, idDepartamento: IdDepartamento },

                url: urlBase + '/ReportesAplicacion/ReporteAusentismoPorGrupoEtareo',
                type: 'POST'
            });
           location.reload(true);
            OcultarPopupposition();
 
            break;

    }
}