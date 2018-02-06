using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SG_SST.Models.Incidentes
{
    public class IncidentesATModel
    {
        /// <summary>
        /// I. Informe sobre la Investigacion
        /// </summary>
        /// 
        public int PK_Incidentes_AT_Id { get; set; }
        public bool boIncidente { get; set; }///REQUERIDO
        public bool AccidenteTrabajo { get; set; }///REQUERIDO
        public bool Leve { get; set; }///REQUERIDO
        public bool Grave { get; set; }///REQUERIDO
        public bool Mortal { get; set; }///REQUERIDO
        public string FechaInvestigacionI { get; set; }///REQUERIDO
        public List<Departamento> DepartamentoI { get; set; }///REQUERIDO
        public int pk_DepartamentoI { get; set; }///REQUERIDO
        public List<Municipio> MunicipioI { get; set; }///REQUERIDO
        public int pk_MunicipioI { get; set; }///REQUERIDO
        public string DireccionI { get; set; }///REQUERIDO
        public string HoraInicialI { get; set; }///REQUERIDO
        public string HoraFinalI { get; set; }///REQUERIDO
        public string ResponsablesI { get; set; }///REQUERIDO
        public bool FotografiasI { get; set; }///REQUERIDO
        public bool VideosI { get; set; }///REQUERIDO
        public bool CintasAudioI { get; set; }///REQUERIDO
        public bool IlustracionesI { get; set; }///REQUERIDO
        public bool DiagramasI { get; set; }///REQUERIDO
        public bool OtrosI { get; set; }///REQUERIDO
        public string CualesI { get; set; }///REQUERIDO




        /// <summary>
        /// II. Identificacion General del Empleador
        /// </summary>
        /// 
        public bool TipoVinculacionII { get; set; }
        public string TipoIdentificacionII { get; set; }
        public string ActividadEconomicaII { get; set; }
        public string NumeroIdentificacionII { get; set; }
        public string NombreRazonSocialII { get; set; }
        public string DireccionPpalII { get; set; }
        public string TelefonoII { get; set; }
        public string FaxII { get; set; }
        public string st_DepartamentoII { get; set; }///REQUERIDO
        public string st_MunicipioII { get; set; }///REQUERIDO

        public string EmailII { get; set; }
        public string ZonaUrbanaII { get; set; }
        public bool SedePrincipalII { get; set; }
        public string  ActEconoCentroTrabajoII { get; set; }
        public int pk_ActEconoCentroTrabajoII { get; set; }///REQUERIDO
        public string CentroCostoTelefonoII { get; set; }
        public string CentroCostoFaxII { get; set; }
        public string DireccionCentroTrabajoII { get; set; }
        public string ZonaII { get; set; }
        public List<Departamento> DeptoCentroCostoII { get; set; }
        public List<Municipio> MncpioCentroCostoII { get; set; }
        public int pk_DeptoCentroCostoII { get; set; }///REQUERIDO
        public int pk_MncpioCentroCostoII { get; set; }///REQUERIDO
                                                       ///


        public List<Departamento> DepartamentoIV { get; set; }
        public List<Municipio> MncpioIV { get; set; }
        public int pk_DepartamentoIV { get; set; }///REQUERIDO
        public int pk_MncpioIV { get; set; }///REQUERIDO
                                            ///
       
        public bool TipoVinculacionIII { get; set;}
        public string NumeroIdentificacionIII { get; set;}
        public string PrimerApellidoIII { get; set;}
        public string SegundoApellidoIII { get; set;}
        public string PrimerNombreIII { get; set;}
        public string FechaNacimientoIII { get; set;}
        public string SexoIII { get; set;}
        public string EPSIII { get; set;}
        public string AFPIII { get; set;}
        public string ARLIII { get; set;}
        public string TelefonoIII { get; set;}
        public string FaxIII { get; set;}
        public string EmailIII { get; set;}
        public string DireccionCentroTrabajoIII { get; set;}
        public string ZonaIII { get; set; }
        public string CargoIII { get; set;}
        public string OcupacionIII { get; set;}
        public string FechaIngresoIII { get; set;}
        public string TiempoOcupacionAIII { get; set;}
        public string TiempoOcupacionMIII { get; set;}
        public string AntiguedadAIII { get; set;}
        public string AntiguedadMIII { get; set;}
        public bool DiurnoIII { get; set;}
        public bool NocturnoIII { get; set;}
        public bool MixtoIII { get; set;}
        public bool TurnosIII { get; set;}
        public string SalarioHonorariosIII { get; set;}
        public string FechaMuerteIII { get; set;}
        public string AtencionOportunaIII {get; set;}

        
        
        /// <summary>
        /// MODULO IV
        /// </summary>
        public string FechaOcurrenciaIV { get; set;}
        public string HoraOcurrenciaIV { get; set;}
        public bool JornadaIV { get; set;}
        public bool ExtraIV { get; set;}
        public bool LunesIV { get; set;}
        public bool MartesIV { get; set;}
        public bool MiercolesIV { get; set;}
        public bool JuevesIV { get; set;}
        public bool ViernesIV { get; set;}
        public bool SabadoIV { get; set;}
        public bool DomingoIV { get; set;}
        public bool LaborHabitualIV { get; set;}
        public string LaborHabitualIVS { get; set;}
        public bool LaborHabitualIVN { get; set;}
        public string TipoIncidenteIV { get; set;}
        public string EspecTipoIncidenteIV { get; set;}
        public string IPSAtendioIV { get; set;}
        public string ZonaIV { get; set; }
        public string TiempoLaboradoPrevioIV { get; set;}
        public string LugarExactoIV { get; set;}
        public string SitioExactoIV { get; set;}
        public bool OtroSitioIV { get; set;}
        public string EspecifiqueIV { get; set;}



        public bool EventosSimilaresV { get; set;}
        public string NumeroPersonasV { get; set;}
        public bool OtrosIncidentesV { get; set;}
        public bool EventoSimilarV { get; set;}
        public bool CondicionPrioritariaV { get; set;}
        public bool TrabajadorInvolucradoV { get; set;}
        public bool PanoramaRiesgoV { get; set;}
        public string DescripcionAccidenteV { get; set;}


        public string AgenteVI { get; set;}
        public string MaterialVI { get; set;}
        public string ModeloVI { get; set;}
        public string ReferenciaVI { get; set;}
        public string PesoVI { get; set;}
        public string PesoUnidadMedidaVI { get; set;}
        public string AlturaVI { get; set;}
        public string AnchoVI { get; set;}
        public string VolumenVI { get; set;}
        public string ProfundidadVI { get; set;}
        public string VelocidadVI { get; set;}
        public string TiempoUsoVI { get; set;}
        public string FechaMantenimientoVI { get; set;}
        public bool ReparadoVI { get; set;}
        public string ExplosivosVI { get; set;}
        public string ExplosivosUnidadMedidaVI { get; set;}
        public string GasesVI { get; set;}
        public string GasesCantidadVI { get; set;}
        public string TemperaturaUnidadMedidaVI { get; set;}
        public string SustanciaUnidadMedidaVI { get; set;}
        public string SustanciaCantidadVI { get; set;}
        public string VoltajeElectricoVI { get; set;}
        public string VoltajeElectricoUnidadMedidaVI { get; set;}
        public string UnidadMedidaVI { get; set;}
        public string DetallesAdicionalesVI { get; set;}
        public bool EPPVI { get; set;}
        public bool TrabajadorEPPVI { get; set;}
        public string ObservacionesVI { get; set;}


        public string CodTipoLesionVII { get; set;}
        public string TipoLesionVII { get; set;}
        public string CodigoParteCuerpoAfectadaVII { get; set;}
        public string CodMecaAccideneteVII { get; set;}
        public string MecanismoAccidenteVII { get; set;}
        public string CodAgenteAccideneteVII { get; set;}
        public string AgenteAccidenteVII { get; set;}
        public string CodFactoresPersonalesVII1 { get; set;}
        public string FactoresPersonalesVII1 { get; set;}
        public string CodFactoresPersonalesVII2 { get; set;}
        public string FactoresPersonalesVII2 { get; set;}
        public string CodActoSubestandarVII1 { get; set;}
        public string ActosSubestandarVII1 { get; set;}
        public string CodActoSubestandarVII2 { get; set;}
        public string ActosSubestandarVII2 { get; set;}
        public string CodFactoresTrabajoVII1 { get; set;}
        public string FactoresTrabajoVII1 { get; set;}
        public string CodFactoresTrabajoVII2 { get; set;}
        public string FactoresTrabajoVII2 { get; set;}
        public string CodCondAmbientalesVII1 { get; set;}
        public string CondAmbientalesVII1 { get; set;}
        public string CodCondAmbientalesVII2 { get; set;}
        public string CondAmbientalesVII2 { get; set;}
        public string TipoIdentJefeInmediantoIX { get; set;}
        public string NumIdentJefeInmediatoIX { get; set;}
        public string JefeInmediatoNombresIX { get; set;}
        public string JefeInmediatoCargoIX { get; set;}


        public string DescripcionAnalisisIX { get; set;}
        public bool TipoIdentEncargadoPSOIX { get; set;}
        public string NumIdentPSOIX { get; set;}
        public string EncargadoPSONombresIX { get; set;}
        public string EncargadoPSOCargoIX { get; set;}
        public bool TipoIdentCOPASOIX { get; set;}
        public string COPASONumIdentIX { get; set;}
        public string COPASONombresCompletosIX { get; set;}
        public string COPASOCargoIX { get; set;}
        public bool TipoIdentEncargadosPSOIX { get; set;}
        public string NumeroIdentBrigadistaIX { get; set;}
        public string BrigadistaNombresIX { get; set;}
        public string BrigadistaCargoIX { get; set;}
        public bool TipoIdentParticipanteIX { get; set;}
        public string NumIdentParticipanteIX { get; set;}
        public string ParticipanteNombreIX { get; set;}
        public string ParticipanteCargoIX { get; set;}
        public bool TipoIdentAnalisisIX { get; set;}
        public string NumIdentAnalisisIX { get; set;}
        public string CargoAnalisisIX { get; set;}
        public string EmpresaRepresentaIX { get; set;}
        public string ObservacionEspecialistaIX { get; set;}



        public bool CausasInmediatasTipoC1X { get; set;}
        public bool CausasInmediatasTipoP1X { get; set;}
        public string MedidasIntervencion1X { get; set;}
        public bool TipoF1X { get; set;}
        public bool TipoM1X { get; set;}
        public bool TipoT1X { get; set;}
        public string RespImplementacion1X { get; set;}
        public string FechaImplementacion1X { get; set;}

        public bool CausasInmediatasTipoC2X { get; set;}
        public bool CausasInmediatasTipoP2X { get; set;}
        public string MedidasIntervencion2X { get; set;}
        public bool TipoF2X { get; set;}
        public bool TipoM2X { get; set;}
        public bool TipoT2X { get; set;}
        public string RespImplementacion2X { get; set;}
        public string FechaImplementacion2X { get; set;}

        public bool CausasInmediatasTipoC3X { get; set;}
        public bool CausasInmediatasTipoP3X { get; set;}
        public string MedidasIntervencion3X { get; set;}
        public bool TipoF3X { get; set;}
        public bool TipoM3X { get; set;}
        public bool TipoT3X { get; set;}
        public string RespImplementacion3X { get; set;}
        public string FechaImplementacion3X { get; set;}

        public bool CausasBasicasTipoC1X { get; set;}
        public bool CausasBasicasTipoP1X { get; set;}
        public string BasicasInmediatas1X { get; set;}
        public bool BasicasF1X { get; set;}
        public bool BasicasM1X { get; set;}
        public bool BasicasT1X { get; set;}
        public string BasicasRespImplementacion1X { get; set;}
        public string BasicasFechaImplementacion1X { get; set;}

        public bool CausasBasicasTipoC2X { get; set;}
        public bool CausasBasicasTipoP2X { get; set;}
        public string BasicasInmediatas2X { get; set;}
        public bool BasicasF2X { get; set;}
        public bool BasicasM2X { get; set;}
        public bool BasicasT2X { get; set;}
        public string BasicasRespImplementacion2X { get; set;}
        public string BasicasFechaImplementacion2X { get; set;}

        public bool CausasBasicasTipoC3X { get; set;}
        public bool CausasBasicasTipoP3X { get; set;}
        public string BasicasInmediatas3X { get; set;}
        public bool BasicasF3X { get; set;}
        public bool BasicasM3X { get; set;}
        public bool BasicasT3X { get; set;}
        public string BasicasRespImplementacion3X { get; set;}
        public string BasicasFechaImplementacion3X { get; set;}

        public string FechaRemisionXI { get; set;}
        public string NoFoliosXI { get; set;}
        public string TipoIdentificacionXI { get; set; }
        public string NumeroIdentificacionXI { get; set;}
        public string NombresXI { get; set;}
        public string CargoXI { get; set;}
        public string RecomendacionesARLXI { get; set;}
        public string RemisionInformeARLXI { get; set;}
        public string RemisionMinisterioTrabajoXI { get; set;}

        
        public string TipoIdentificacionXII { get; set;}
        public string NumeroIdentificacionXII { get; set;}
        public string NombresXII { get; set;}
        public string CargoXII { get; set;}
        public bool MedidasIntervencionXII { get; set;}
        public string ObservacionesXII  { get; set;}

       /* AnexoFechaIncidente
        AnexoFechaTestimonio
        AnexoTipoIdentificacion
        AnexoNumIdentificacion
        AnexoNombres
        AnexoCargo
        AnexoDondeSucedio
        AnexoPrevenir
        AnexoAdicionar
        AnexoFirma*/


 
    }
}