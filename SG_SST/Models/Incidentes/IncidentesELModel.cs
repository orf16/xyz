using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Incidentes
{
    public class IncidentesELModel
    {
        public int PK_Incidentes_EL_Id { get; set; }
        public string EnfLabCalificadaI { get; set;}
        public string FechaInvestigacionI { get; set;}
        public List<Departamento> DepartamentoI { get; set; }///REQUERIDO
        public List<Municipio> MunicipioI { get; set; }///REQUERIDO
        public int pk_DepartamentoI { get; set; }///REQUERIDO
        public int pk_MunicipioI { get; set; }///REQUERIDO
        public string DireccionI { get; set;}
        public string NombresApellidosI { get; set;}
        public string ProfesionalI { get; set;}
        public string NoLicenciaI { get; set;}
        public string FotografiasI { get; set;}
        public string IlustracionesI { get; set;}
        public string DiagramasI { get; set;}
        public string OtrosCualesI { get; set;}


        public string EmpleadorII { get; set;}
        public string ContratanteII { get; set;}
        public string IndependienteII { get; set;}
        public string CooperativaII { get; set;}
        public string AgremiacionII { get; set;}
        public string AsociacionII { get; set;}
        public string CodActividadII { get; set;}
        public string ActividadPrincipalII { get; set;}
        public string RazonSocialII { get; set;}
        public string TipoIdentificacionII { get; set;}
        public string NumIdentificacionII { get; set;}

        public string DireccionPrincipalII { get; set;}
        public string TelefonoPpalII { get; set;}
        public string FaxII { get; set;}
        public List<Departamento> DeptoPpalII { get; set; }///REQUERIDO
        public List<Municipio> McpioPpalII { get; set; }///REQUERIDO
        public int pk_DeptoPpalII { get; set; }///REQUERIDO
        public int pk_McpioPpalII { get; set; }///REQUERIDO
        public string EmailII { get; set;}
        public string ZonaPpalII { get; set;}
        public string CentroTrabajoPpalII { get; set;}
         
        public string CentroCostoTelefonoII { get; set;}
        public string CentroCostoFaxII { get; set;}
        public string CodActEconoPpalII { get; set;}
        public string ActEconoCentroTrabajoII { get; set;}
        public List<Departamento> DeptoEmpleadorII { get; set; }///REQUERIDO
        public List<Municipio> McpioEmpleadorII { get; set; }///REQUERIDO
        public int pk_DeptoEmpleadorII { get; set; }///REQUERIDO
        public int pk_McpioEmpleadorII { get; set; }///REQUERIDO
        public string EmailEmpleadorII { get; set;}
        public string ZonaEmpleadorII { get; set;}
        public List<Departamento> DeptoCentroTrabajoII { get; set; }///REQUERIDO
        public List<Municipio> McpioCentroTrabajoII { get; set; }///REQUERIDO
        public int pk_DeptoCentroTrabajoII { get; set; }///REQUERIDO
        public int pk_McpioCentroTrabajoII { get; set; }///REQUERIDO

        public string PlantaIII { get; set;}
        public string MisionIII { get; set;}
        public string CooperadorIII { get; set;}
        public string EstudianteIII { get; set;}
        public string IndependienteIII { get; set;}
        public string TipoIdentificacionIII { get; set;}
        public string NumIdentificacionIII { get; set;}
        public string PrimerApellidoIII { get; set;}
        public string SegundoApellidoIII { get; set;}
        public string PrimerNombreIII { get; set;}
        public string SegundoNombreIII { get; set;}
        public string FechaNacimientoIII { get; set;}
        public string SexoIII { get; set;}
        public string EPSAfiliadoIII { get; set;}
        public string AFPAfiliadoIII { get; set;}
        public string ARLAfiliadoIII { get; set;}
        public string TelefonoIII { get; set;}
        public string FaxIII  { get; set;}
        public string EmailTrabajadorIII { get; set;}
        public string DireccionTrabajadorIII { get; set;}
        public string ZonaIII { get; set;}
         

        public string CargoIV { get; set;}
        public string AntiguedadCargoAIV { get; set;}
        public string AntiguedadCargoMIV { get; set;}
        public string CodOcupacionIV { get; set;}
        public string OcupacionHabitualIV { get; set;}
        public string FechaMuerteIV { get; set;}
        public string AreaActualIV { get; set;}
        public string NombreCargoIV { get; set;}
        public string AntiguedadCargoAnioIV { get; set;}
        public string AntiguedadCargoMesesIV { get; set;}
        public string DiurnoIV { get; set;}
        public string NocturnoIV { get; set;}
        public string MixtoIV { get; set;}
        public string TurnosIV { get; set;}
        public string CondicionesPuestoTrabajoIV { get; set;}

        public string TareasCargo1IV { get; set;}
        public string DedicacionJL1IV { get; set;}
        public string DedicacionJL11IV { get; set;}
        public string DedicacionJL12IV { get; set;}
        public string RelacionMuyProbable1IV { get; set;}
        public string RelacionProbable1IV { get; set;}
        public string RelacionPocoProbable1IV { get; set;}
        public string DedicacionJL21IV { get; set;}
        public string DedicacionJL22IV { get; set;}
        public string DedicacionJL23IV { get; set;}
        public string RelacionMuyProbable2IV { get; set;}
        public string RelacionProbable2IV { get; set;}
        public string RelacionPocoProbable2IV { get; set; }

        public string TareasCargo3IV { get; set;}
        public string DedicacionJL31IV { get; set;}
        public string DedicacionJL32IV { get; set;}
        public string DedicacionJL33IV { get; set;}
        public string RelacionMuyProbable3IV { get; set;}
        public string RelacionProbable3IV { get; set;}
        public string RelacionPocoProbable3IV { get; set;}

        public string TareasCargo4IV { get; set;}
        public string DedicacionJL41IV { get; set;}
        public string DedicacionJL42IV { get; set;}
        public string DedicacionJL43IV { get; set;}
        public string RelacionMuyProbable4IV { get; set;}
        public string RelacionProbable4IV { get; set;}
        public string RelacionPocoProbable4IV { get; set;}
         
        public string FormacionInformacionIV { get; set;}
        public string ProteccionColectivaIV { get; set;}
        public string EPPIV { get; set;}  
        public string DiseñoPuestoTrabajoIV { get; set;} 
        public string OrganizacionTrabajoIV { get; set;}
        public string PreventivasIV { get; set;}
        public string ImplementadasIV { get; set;}
        public string DescripcionIV { get; set;}

        public string CodigoCie1 { get; set;}
        public string CodigoCie2 { get; set;}
        public string CodigoCie3 { get; set;}
        public string CodigoCie4 { get; set;}
        public string DiagnosticoIV1 { get; set;}
        public string DiagnosticoIV2 { get; set;}
        public string DiagnosticoIV3 { get; set;}
        public string DiagnosticoIV4 { get; set;}


        public string FechaOrigenELV { get; set;}
        public string OrigenLaboralIV { get; set;}
        public string NoTrabajadoresV { get; set;}
        public string CargosSimilaresV { get; set;}

        public string NombresApellidosV1 { get; set;}
        public string NombresApellidosV2 { get; set;}
        public string NombresApellidosV3 { get; set;}
        public string NombresApellidosV4 { get; set;}

        public string AnioDiagnosticoV1 { get; set;}
        public string AnioDiagnosticoV2 { get; set;}
        public string AnioDiagnosticoV3 { get; set;}
        public string AnioDiagnosticoV4 { get; set;}

         
        public string PuestoEmpresaVI1 { get; set;}
        public string AgentesBiologicosVI1 { get; set;}
        public string FrasesVI1 { get; set;}
        public string RutinariaVI1 { get; set;}
        public string NORutinariaVI1 { get; set;}
        public string TiempoExposicionMesesVI1 { get; set;}
        public string TiempoExposicionHorasVI1 { get; set;}
        public string TLVCorregidoVI1 { get; set;}
        public string ConcentracionHalladaVI1 { get; set;}
        public string FechaMediacionDiaV1 { get; set;}
        public string FechaMediaMesV1 { get; set;}
        public string FechaMediaAnioV1 { get; set;}
        public string NivelRiesgoV1 { get; set;}
        public string ViaEntradaV1 { get; set;}

        public string PuestoEmpresaVI2 { get; set;}
        public string AgentesBiologicosVI2 { get; set;}
        public string FrasesVI2 { get; set;}
        public string RutinariaVI2 { get; set;}
        public string NORutinariaVI2 { get; set;}
        public string TiempoExposicionMesesVI2 { get; set;}
        public string TiempoExposicionHorasVI2 { get; set;}
        public string TLVCorregidoVI2 { get; set;}
        public string ConcentracionHalladaVI2 { get; set;}
        public string FechaMediacionDiaV2 { get; set;}
        public string FechaMediaMesV2 { get; set;}
        public string FechaMediaAnioV2 { get; set;}
        public string NivelRiesgoV2 { get; set;}
        public string ViaEntradaV2 { get; set;}

        public string PuestoEmpresaVI3 { get; set;}
        public string AgentesBiologicosVI3 { get; set;}
        public string FrasesVI3 { get; set;}
        public string RutinariaVI3 { get; set;}
        public string NORutinariaVI3 { get; set;}
        public string TiempoExposicionMesesVI3 { get; set;}
        public string TiempoExposicionHorasVI3 { get; set;}
        public string TLVCorregidoVI3 { get; set;}
        public string ConcentracionHalladaVI3 { get; set;}
        public string FechaMediacionDiaV3 { get; set;}
        public string FechaMediaMesV3 { get; set;}
        public string FechaMediaAnioV3 { get; set;}
        public string NivelRiesgoV3 { get; set;}
        public string ViaEntradaV3 { get; set;}

        public string PuestoEmpresaVI4 { get; set;}
        public string AgentesBiologicosVI4 { get; set;}
        public string FrasesVI4 { get; set;}
        public string RutinariaVI4 { get; set;}
        public string NORutinariaVI4 { get; set;}
        public string TiempoExposicionMesesVI4 { get; set;}
        public string TiempoExposicionHorasVI4 { get; set;}
        public string TLVCorregidoVI4 { get; set;}
        public string ConcentracionHalladaVI4 { get; set;}
        public string FechaMediacionDiaVI4 { get; set;}
        public string FechaMediaMesVI4 { get; set;}
        public string FechaMediaAnioVI4 { get; set;}
        public string NivelRiesgoVI4 { get; set;}
        public string ViaEntradaVI4 { get; set;}

        public string OficioEmpresa2V1 { get; set;}
        public string AgenteRelBiologico2VI { get; set;}
        public string FuenteAgente2VI1 { get; set;}
        public string MecanismoTransmicion2VI1 { get; set;}
        public string TipoActividadRutinaria2VI1 { get; set;}
        public string TipoActividadNoRutinaria2VI1 { get; set;}
        public string TiempoExposicionMeses2VI1 { get; set;}
        public string TiempoExposicionHoras2VI1 { get; set;}
        public string ConcentracionHallada2VI1 { get; set;}
        public string NivelRiesgo2VI1 { get; set; }
        public string Dia2VI1 { get; set; }
        public string Mes2VI1 { get; set; }
        public string Anio2VI1 { get; set; }
        public string FrecRiesgo2VI1 { get; set; }

        public string OficioEmpresa2V2 { get; set; }
        public string AgenteRelBiologico2VI2 { get; set; }
        public string FuenteAgente2VI2 { get; set; }
        public string MecanismoTransmicion2VI2 { get; set; }
        public string TipoActividadRutinaria2VI2 { get; set; }
        public string TipoActividadNoRutinaria2VI2 { get; set; }
        public string TiempoExposicionMeses2VI2 { get; set; }
        public string TiempoExposicionHoras2VI2 { get; set; }
        public string ConcentracionHallada2VI2 { get; set; }
        public string NivelRiesgo2VI2 { get; set; }
        public string Dia2VI2 { get; set; }
        public string Mes2VI2 { get; set; }
        public string Anio2VI2 { get; set; }
        public string FrecRiesgo2VI2 { get; set; }

        public string OficioEmpresa2V3 { get; set; }
        public string AgenteRelBiologico2VI3 { get; set; }
        public string FuenteAgente2VI3 { get; set; }
        public string MecanismoTransmicion2VI3 { get; set; }
        public string TipoActividadRutinaria2VI3 { get; set; }
        public string TipoActividadNoRutinaria2VI3 { get; set; }
        public string TiempoExposicionMeses2VI3 { get; set; }
        public string TiempoExposicionHoras2VI3 { get; set; }
        public string ConcentracionHallada2VI3 { get; set; }
        public string NivelRiesgo2VI3 { get; set; }
        public string Dia2VI3 { get; set; }
        public string Mes2VI3 { get; set; }
        public string Anio2VI3 { get; set; }
        public string FrecRiesgo2VI3 { get; set; }

        public string OficioEmpresa2V4 { get; set; }
        public string AgenteRelBiologico2VI4 { get; set; }
        public string FuenteAgente2VI4 { get; set; }
        public string MecanismoTransmicion2VI4 { get; set; }
        public string TipoActividadRutinaria2VI4 { get; set; }
        public string TipoActividadNoRutinaria2VI4 { get; set; }
        public string TiempoExposicionMeses2VI4 { get; set; }
        public string TiempoExposicionHoras2VI4 { get; set; }
        public string ConcentracionHallada2VI4 { get; set; }
        public string NivelRiesgo2VI4 { get; set; }
        public string Dia2VI4 { get; set; }
        public string Mes2VI4 { get; set; }
        public string Anio2VI4 { get; set; }
        public string FrecRiesgo2VI4 { get; set; }
        public string ExpoAccidentales2VI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CargoOficioPuesto3VI {get; set;}
        public string Fuentes3VI {get; set;}
        public string Meses3VI {get; set;}
        public string HorasDia3VI {get; set;}
        public string NivelAmbiental3VI {get; set;}
        public string FMDia3VI {get; set;}
        public string FMMes3VI {get; set;}
        public string FMAnio3VI {get; set;}
        public string DosisRuido3VI {get; set;}
        public string FecMedDia3VI {get; set;}
        public string FecMedMes3VI {get; set;}
        public string FecMEdAnio3VI {get; set;}
        public string ExpSusQuimimcas3VI {get; set;}
        public string ExpoAccPrevias3VI { get; set; }

        public string CargoOficioPuesto3VI1 { get; set; }
        public string Fuentes3VI1 { get; set; }
        public string Meses3VI1 { get; set; }
        public string HorasDia3VI1 { get; set; }
        public string NivelAmbiental3VI1 { get; set; }
        public string FMDia3VI1 { get; set; }
        public string FMMes3VI1 { get; set; }
        public string FMAnio3VI1 { get; set; }
        public string DosisRuido3VI1 { get; set; }
        public string FecMedDia3VI1 { get; set; }
        public string FecMedMes3VI1 { get; set; }
        public string FecMEdAnio3VI1 { get; set; }
        public string ExpSusQuimimcas3VI1 { get; set; }
        public string ExpoAccPrevias3VI1 { get; set; }

        public string CargoOficioPuesto3VI2 { get; set; }
        public string Fuentes3VI2 { get; set; }
        public string Meses3VI2 { get; set; }
        public string HorasDia3VI2 { get; set; }
        public string NivelAmbiental3VI2 { get; set; }
        public string FMDia3VI2 { get; set; }
        public string FMMes3VI2 { get; set; }
        public string FMAnio3VI2 { get; set; }
        public string DosisRuido3VI2 { get; set; }
        public string FecMedDia3VI2 { get; set; }
        public string FecMedMes3VI2 { get; set; }
        public string FecMEdAnio3VI2 { get; set; }
        public string ExpSusQuimimcas3VI2 { get; set; }
        public string ExpoAccPrevias3VI2 { get; set; }


        public string CargoOficioPuesto3VI3 { get; set; }
        public string Fuentes3VI3 { get; set; }
        public string Meses3VI3 { get; set; }
        public string HorasDia3VI3 { get; set; }
        public string NivelAmbiental3VI3 { get; set; }
        public string FMDia3VI3 { get; set; }
        public string FMMes3VI3 { get; set; }
        public string FMAnio3VI3 { get; set; }
        public string DosisRuido3VI3 { get; set; }
        public string FecMedDia3VI3 { get; set; }
        public string FecMedMes3VI3 { get; set; }
        public string FecMEdAnio3VI3 { get; set; }
        public string ExpSusQuimimcas3VI3 { get; set; }
        public string ExpoAccPrevias3VI3 { get; set; }

        public string CargoOficio4VI1 { get; set; }
        public string DescActividad4VI1 { get; set; }
        public string Duracion4VI1 { get; set; }
        public string FrecActividad4VI1 { get; set; }
        public string TipoTrabajoActividad4VI1 { get; set; }
        public string WBTG4VI1 { get; set; }
        public string TipoExpMeses4VI1 { get; set; }
        public string TipoExpHD4VI1 { get; set; }
        public string TasaTrabajo4VI1 { get; set; }
        public string FechaMedDia4VI1 { get; set; }
        public string FechaMedMes4VI1 { get; set; }
        public string FechaMedAnio4VI1 { get; set; }

        public string CargoOficio4VI2{ get; set; }
        public string DescActividad4VI2 { get; set; }
        public string Duracion4VI2 { get; set; }
        public string FrecActividad4VI2 { get; set; }
        public string TipoTrabajoActividad4VI2 { get; set; }
        public string WBTG4VI2 { get; set; }
        public string TipoExpMeses4VI2 { get; set; }
        public string TipoExpHD4VI2 { get; set; }
        public string TasaTrabajo4VI2 { get; set; }
        public string FechaMedDia4VI2 { get; set; }
        public string FechaMedMes4VI2 { get; set; }
        public string FechaMedAnio4VI2 { get; set; }


        public string CargoOficio4VI3 { get; set; }
        public string DescActividad4VI3 { get; set; }
        public string Duracion4VI3 { get; set; }
        public string FrecActividad4VI3 { get; set; }
        public string TipoTrabajoActividad4VI3 { get; set; }
        public string WBTG4VI3 { get; set; }
        public string TipoExpMeses4VI3 { get; set; }
        public string TipoExpHD4VI3 { get; set; }
        public string TasaTrabajo4VI3 { get; set; }
        public string FechaMedDia4VI3 { get; set; }
        public string FechaMedMes4VI3 { get; set; }
        public string FechaMedAnio4VI3 { get; set; }

        public string CargoOficio4VI4 { get; set; }
        public string DescActividad4VI4 { get; set; }
        public string Duracion4VI4 { get; set; }
        public string FrecActividad4VI4 { get; set; }
        public string TipoTrabajoActividad4VI4 { get; set; }
        public string WBTG4VI4 { get; set; }
        public string TipoExpMeses4VI4 { get; set; }
        public string TipoExpHD4VI4 { get; set; }
        public string TasaTrabajo4VI4 { get; set; }
        public string FechaMedDia4VI4 { get; set; }
        public string FechaMedMes4VI4 { get; set; }
        public string FechaMedAnio4VI4 { get; set; }

        public string RadCargoEmpresa5VI1 { get; set; }
        public string RadDescripcionFuente5VI1 { get; set; }
        public string RadDescripcionAct5VI1 { get; set; }
        public string RadCondiciones5VI1 { get; set; }
        public string RadTEDia5VI1 { get; set; }
        public string RadTEMes5VI1 { get; set; }
        public string RadTEAnio5VI1 { get; set; }
        public string RadEvalAmbiental5VI1 { get; set; }
        public string RadFMDia5VI1 { get; set; }
        public string RadFMMes5VI1 { get; set; }
        public string RadFMAnio5VI1 { get; set; }

        public string RadCargoEmpresa5VI2 { get; set; }
        public string RadDescripcionFuente5VI2 { get; set; }
        public string RadDescripcionAct5VI2 { get; set; }
        public string RadCondiciones5VI2 { get; set; }
        public string RadTEDia5VI2 { get; set; }
        public string RadTEMes5VI2 { get; set; }
        public string RadTEAnio5VI2 { get; set; }
        public string RadEvalAmbiental5VI2 { get; set; }
        public string RadFMDia5VI2 { get; set; }
        public string RadFMMes5VI2 { get; set; }
        public string RadFMAnio5VI2 { get; set; }


        public string RadCargoEmpresa5VI3 { get; set; }
        public string RadDescripcionFuente5VI3 { get; set; }
        public string RadDescripcionAct5VI3 { get; set; }
        public string RadCondiciones5VI3 { get; set; }
        public string RadTEDia5VI3 { get; set; }
        public string RadTEMes5VI3 { get; set; }
        public string RadTEAnio5VI3 { get; set; }
        public string RadEvalAmbiental5VI3 { get; set; }
        public string RadFMDia5VI3 { get; set; }
        public string RadFMMes5VI3 { get; set; }
        public string RadFMAnio5VI3 { get; set; }


        public string RadCargoEmpresa5VI4 { get; set; }
        public string RadDescripcionFuente5VI4 { get; set; }
        public string RadDescripcionAct5VI4 { get; set; }
        public string RadCondiciones5VI4 { get; set; }
        public string RadTEDia5VI4 { get; set; }
        public string RadTEMes5VI4 { get; set; }
        public string RadTEAnio5V4 { get; set; }
        public string RadEvalAmbiental5VI4 { get; set; }
        public string RadFMDia5VI4 { get; set; }
        public string RadFMMes5VI4 { get; set; }
        public string RadFMAnio5VI4 { get; set; }

        public string VibCargoEmpresa6VI1 { get; set; }
        public string VibDescFuente6VI1 { get; set; }
        public bool BooTipoVibCE6VI1 { get; set; }
        public bool BooTipoVibMB6VI1 { get; set; }
        public string TiempoExpMeses6VI1 { get; set; }
        public string TiempoExpHD6VI1 { get; set; }
        public string VCE6VI1 { get; set; }
        public string VMB6VI1 { get; set; }
        public string AceTotal6VI1 { get; set; }
        public string EjeDominante6VI1 { get; set; }
        public string AceEjeDominante6VI1 { get; set; }
        public string Frecuencia6VI1 { get; set; }
        public string Aceleracion6VI1 { get; set; }
        public string FechaMedDia6VI1 { get; set; }
        public string FechaMedMes6VI1 { get; set; }
        public string FechaMedAnio6VI1 { get; set; }
        public bool BooExpoRuido6VI1 { get; set; }

        public string VibCargoEmpresa6VI2 { get; set; }
        public string VibDescFuente6VI2 { get; set; }
        public bool BooTipoVibCE6VI2 { get; set; }
        public bool BooTipoVibMB6VI2 { get; set; }
        public string TiempoExpMeses6VI2 { get; set; }
        public string TiempoExpHD6VI2 { get; set; }
        public string VCE6VI2 { get; set; }
        public string VMB6VI2 { get; set; }
        public string AceTotal6VI2 { get; set; }
        public string EjeDominante6VI2 { get; set; }
        public string AceEjeDominante6VI2 { get; set; }
        public string Frecuencia6VI2 { get; set; }
        public string Aceleracion6VI2 { get; set; }
        public string FechaMedDia6VI2 { get; set; }
        public string FechaMedMes6VI2 { get; set; }
        public string FechaMedAnio6VI2 { get; set; }
        public bool BooExpoRuido6VI2 { get; set; }

        public string VibCargoEmpresa6VI3 { get; set; }
        public string VibDescFuente6VI3 { get; set; }
        public bool BooTipoVibCE6VI3 { get; set; }
        public bool BooTipoVibMB6VI3 { get; set; }
        public string TiempoExpMeses6VI3 { get; set; }
        public string TiempoExpHD6VI3 { get; set; }
        public string VCE6VI3 { get; set; }
        public string VMB6VI3 { get; set; }
        public string AceTotal6VI3 { get; set; }
        public string EjeDominante6VI3 { get; set; }
        public string AceEjeDominante6VI3 { get; set; }
        public string Frecuencia6VI3 { get; set; }
        public string Aceleracion6VI3 { get; set; }
        public string FechaMedDia6VI3 { get; set; }
        public string FechaMedMes6VI3 { get; set; }
        public string FechaMedAnio6VI3 { get; set; }
        public bool BooExpoRuido6VI3 { get; set; }

        public string VibCargoEmpresa6VI4 { get; set; }
        public string VibDescFuente6VI4 { get; set; }
        public bool BooTipoVibCE6VI4 { get; set; }
        public bool BooTipoVibMB6VI4 { get; set; }
        public string TiempoExpMeses6VI4 { get; set; }
        public string TiempoExpHD6VI4 { get; set; }
        public string VCE6VI4 { get; set; }
        public string VMB6VI4 { get; set; }
        public string AceTotal6VI4 { get; set; }
        public string EjeDominante6VI4 { get; set; }
        public string AceEjeDominante6VI4 { get; set; }
        public string Frecuencia6VI4 { get; set; }
        public string Aceleracion6VI4 { get; set; }
        public string FechaMedDia6VI4 { get; set; }
        public string FechaMedMes6VI4 { get; set; }
        public string FechaMedAnio6VI4 { get; set; }
        public bool BooExpoRuido6VI4 { get; set; }

        public string VibCargoEmpresa6VI5 { get; set; }
        public string VibDescFuente6VI5 { get; set; }
        public bool BooTipoVibCE6VI5 { get; set; }
        public bool BooTipoVibMB6VI5 { get; set; }
        public string TiempoExpMeses6VI5 { get; set; }
        public string TiempoExpHD6VI5 { get; set; }
        public string VCE6VI5 { get; set; }
        public string VMB6VI5 { get; set; }
        public string AceTotal6VI5 { get; set; }
        public string EjeDominante6VI5 { get; set; }
        public string AceEjeDominante6VI5 { get; set; }
        public string Frecuencia6VI5 { get; set; }
        public string Aceleracion6VI5 { get; set; }
        public string FechaMedDia6VI5 { get; set; }
        public string FechaMedMes6VI5 { get; set; }
        public string FechaMedAnio6VI5 { get; set; }
        public bool BooExpoRuido6VI5 { get; set; }

        public string VibCargoEmpresa6VI6 { get; set; }
        public string VibDescFuente6VI6 { get; set; }
        public bool BooTipoVibCE6VI6 { get; set; }
        public bool BooTipoVibMB6VI6 { get; set; }
        public string TiempoExpMeses6VI6 { get; set; }
        public string TiempoExpHD6VI6 { get; set; }
        public string VCE6VI6 { get; set; }
        public string VMB6VI6 { get; set; }
        public string AceTotal6VI6 { get; set; }
        public string EjeDominante6VI6 { get; set; }
        public string AceEjeDominante6VI6 { get; set; }
        public string Frecuencia6VI6 { get; set; }
        public string Aceleracion6VI6 { get; set; }
        public string FechaMedDia6VI6 { get; set; }
        public string FechaMedMes6VI6 { get; set; }
        public string FechaMedAnio6VI6 { get; set; }
        public bool BooExpoRuido6VI6 { get; set; }

        public string DescEventoInv6VI { get; set; }


        public string FrecPresAltoVI1 { get; set; }
        public string FrecPresMedioVI1 { get; set; }
        public string FrecPresBajoVI1 { get; set; }
        public string TiempoExpAltoVI1 { get; set; }
        public string TiempoExpMedioVI1 { get; set; }
        public string TiempoExpBajoVI1 { get; set; }
        public string IntensidadAltoVI1 { get; set; }
        public string IntensidadMedioVI1 { get; set; }
        public string IntensidadBajoVI1 { get; set; }
        public string VarPsicoObservacionesVI1 { get; set; }

        public string FrecPresAltoVI2 { get; set; }
        public string FrecPresMedioVI2 { get; set; }
        public string FrecPresBajoVI2 { get; set; }
        public string TiempoExpAltoVI2 { get; set; }
        public string TiempoExpMedioVI2 { get; set; }
        public string TiempoExpBajoVI2 { get; set; }
        public string IntensidadAltoVI2 { get; set; }
        public string IntensidadMedioVI2 { get; set; }
        public string IntensidadBajoVI2 { get; set; }
        public string VarPsicoObservacionesVI2 { get; set; }


        public string FrecPresAltoVI3 { get; set; }
        public string FrecPresMedioVI3 { get; set; }
        public string FrecPresBajoVI3 { get; set; }
        public string TiempoExpAltoVI3 { get; set; }
        public string TiempoExpMedioVI3 { get; set; }
        public string TiempoExpBajoVI3 { get; set; }
        public string IntensidadAltoVI3 { get; set; }
        public string IntensidadMedioVI3 { get; set; }
        public string IntensidadBajoVI3 { get; set; }
        public string VarPsicoObservacionesVI3 { get; set; }

        public string FrecPresAltoVI4 { get; set; }
        public string FrecPresMedioVI4 { get; set; }
        public string FrecPresBajoVI4 { get; set; }
        public string TiempoExpAltoVI4 { get; set; }
        public string TiempoExpMedioVI4 { get; set; }
        public string TiempoExpBajoV4 { get; set; }
        public string IntensidadAltoVI4 { get; set; }
        public string IntensidadMedioVI4 { get; set; }
        public string IntensidadBajoVI4 { get; set; }
        public string VarPsicoObservacionesVI4 { get; set; }

        public string IntensidadAltaVI1 { get; set; }
        public string IntensidadMediaVI1 { get; set; }
        public string IntensidadBajaVI1 { get; set; }
        public string IntensidadObservVI1 { get; set; }

        public string IntensidadAltaVI2 { get; set; }
        public string IntensidadMediaVI2 { get; set; }
        public string IntensidadBajaVI2 { get; set; }
        public string IntensidadObservVI2 { get; set; }

        public string NivelRiesgoLabVI { get; set; }
        public string NivelRiesgoExtralabVI { get; set; }
        public string NivelRiesgoIndiviVI { get; set; }
        public string NivelEstresVI { get; set; }

        public bool BooPostPieProlongada { get; set; }
        public bool BooPostPieSedente { get; set; }
        public bool BooPosturaIncomodaBrazosManos { get; set; }
        public bool BooEsfuerzoBrazosManos { get; set; }
        public bool BooPosturaIncomodaEspalda { get; set; }
        public bool BooLabRepetitivaColumna { get; set; }
        public bool BooLabRepetitivaBrazoMuMano { get; set; }
        public bool BooPeriodoRecuperacionFisica { get; set; }
        public bool BooEsfuerzoManos { get; set; }
        public bool BooEsfuerzoCuerpo { get; set; }
        public bool BooManipulacionCargas { get; set; }
        public string DMEResumen { get; set; }

        public bool BooCauRelPrevVI1 { get; set; }	
        public string CauRelPrevVI1 { get; set; }
        public bool BooCauRelPrevVI2 { get; set; }	
        public string CauRelPrevVI2 { get; set; }
        public bool BooCauRelPrevVI3 { get; set; }	
        public string CauRelPrevVI3 { get; set; }
        public bool BooCauRelPrevVI4 { get; set; }	
        public string CauRelPrevVI4 { get; set; }
        public bool BooCauRelPrevVI5 { get; set; }	
        public string CauRelPrevVI5 { get; set; }
        public bool BooCauRelPrevVI6 { get; set; }	
        public string CauRelPrevVI6 { get; set; }
        public bool BooCauRelPrevVI7 { get; set; }	
        public string CauRelPrevVI7 { get; set; }
        public bool BooCauRelPrevVI8 { get; set; }	
        public string CauRelPrevVI8 { get; set; }
        public bool BooCauRelPrevVI9 { get; set; }	
        public string CauRelPrevVI9 { get; set; }
        public bool BooCauRelPrevVI10 { get; set; }	
        public string CauRelPrevVI10 { get; set; }
        public bool BooCauRelPrevVI11 { get; set; }	
        public string CauRelPrevVI11 { get; set; }
        public bool BooCauRelPrevVI12 { get; set; }
        public string CauRelPrevVI12 { get; set; }

        public string OtrosDatosInteresVI { get; set; }
        public string CausasEnfermedadLaboralVI { get; set; }

        public string MedidasPreventivasVII1 { get; set; }
        public string ResponsableImplementacionVII1 { get; set; }
        public string FechaEjeMesVII1 { get; set; }
        public string FechaEjeDiaVII1 { get; set; }

        public string MedidasPreventivasVII2 { get; set; }
        public string ResponsableImplementacionVII2 { get; set; }
        public string FechaEjeMesVII3 { get; set; }
        public string FechaEjeDiaVII3 { get; set; }

        public string MedidasPreventivasVII4 { get; set; }
        public string ResponsableImplementacionVII4 { get; set; }
        public string FechaEjeMesVII4 { get; set; }
        public string FechaEjeDiaVII4 { get; set; }

        public string MedidasPreventivasVII5 { get; set; }
        public string ResponsableImplementacionVII5 { get; set; }
        public string FechaEjeMesVII5 { get; set; }
        public string FechaEjeDiaVII5 { get; set; }

        public string MedidasPreventivasVII6 { get; set; }
        public string ResponsableImplementacionVII6 { get; set; }
        public string FechaEjeMesVII6 { get; set; }
        public string FechaEjeDiaVII6 { get; set; }

        public string MedidasPreventivasVII7 { get; set; }
        public string ResponsableImplementacionVII7 { get; set; }
        public string FechaEjeMesVII7 { get; set; }
        public string FechaEjeDiaVII7 { get; set; }

        public string MedidasPreventivasVII8 { get; set; }
        public string ResponsableImplementacionVII8 { get; set; }
        public string FechaEjeMesVII8 { get; set; }
        public string FechaEjeDiaVII8 { get; set; }

        public string TipoIdentificacionVIII1 { get; set; }
        public string JefeInmediatoVIII1 { get; set; }
        public string CargoVIII1 { get; set; }
        public string NumeroIdentificacionVIII1 { get; set; }

        public string TipoIdentificacionVIII2 { get; set; }
        public string JefeInmediatoVIII2 { get; set; }
        public string CargoVIII2 { get; set; }
        public string NumeroIdentificacionVIII2 { get; set; }

        public string TipoIdentificacionVIII3 { get; set; }
        public string JefeInmediatoVIII3 { get; set; }
        public string CargoVIII3 { get; set; }
        public string NumeroIdentificacionVIII3 { get; set; }

        public string TipoIdentificacionVIII4 { get; set; }
        public string JefeInmediatoVIII4 { get; set; }
        public string CargoVIII4 { get; set; }
        public string NumeroIdentificacionVIII4 { get; set; }

        public string TipoIdentificacionVIII5 { get; set; }
        public string JefeInmediatoVIII5 { get; set; }
        public string CargoVIII5 { get; set; }
        public string NumeroIdentificacionVIII5 { get; set; }

        public string TipoIdentificacionVIII6 { get; set; }
        public string JefeInmediatoVIII6 { get; set; }
        public string CargoVIII6 { get; set; }
        public string NumeroIdentificacionVIII6 { get; set; }


        public string TipoIdentificacionVIII7 { get; set; }
        public string EspecialistaOcupacionalVIII { get; set; }
        public string LicenciaNumVIII1 { get; set; }
        public string LicenciaAnioVIII1 { get; set; }
        public string NumeroIdentificacionVIII7 { get; set; }


        public string TipoIdentificacionVIII8 { get; set; }
        public string RepresentanteArlVIII8 { get; set; }
        public string LicenciaNumeroVIII8 { get; set; }
        public string LicenciaAnioVIII8 { get; set; }
        public string NumeroIdentificacionVIII8 { get; set; }
        public string EmpresaRepresentaVIII8 { get; set; }
        public string NitVIII8 { get; set; }

        public string FechaRemisionIX { get; set; }
        public string NoFoliosIX { get; set; }
        public bool TipoIdentificacionIX { get; set; }
        public string NombreApellidoIX { get; set; }
        public string CargoIX { get; set; }
        public string NumeroIdentificacionIX { get; set; }

        public string FechaRemisionARLIX { get; set; }
        public string FecRemisionTerritorialIX     { get; set; }
        public string DisponibleRemisionARLIX { get; set; }
        public string ResponsablesRemisionARLIX { get; set; }
        public string CargoARLIX { get; set; }

        public bool TipoIdentificacionX { get; set; }
        public string ResponsableVerficiacionX { get; set; }
        public string CargoX { get; set; }
        public string NumeroIdentificacionX { get; set; }

        public bool MedidasIntervencionX { get; set; }
        public string ObsevacionesX { get; set; }
        public string FechaVerficacionX { get; set; }


        public bool TipoIdentificacionXI { get; set; }
        public string ResponsableVerficiacionXI { get; set; }
        public string CargoXI { get; set; }
        public string NumeroIdentificacionXI { get; set; }

        public bool MedidasIntervencionXI { get; set; }
        public string ObsevacionesARLXI { get; set; }
        public string FechaVerficacionXI { get; set; }

    }
}