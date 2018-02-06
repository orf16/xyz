using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.Dtos.Empresas;
using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.Models.Incidentes;
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SG_SST.Controllers.Incidentes
{
    public class IncidentesATController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        IRecursosServicios recursosServicios = new RecursosServicios();
        public ActionResult CrearIncidenteAT() 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            var incidentesmodel = new IncidentesATModel();
            incidentesmodel.DepartamentoI = recursosServicios.ObtenerDepartamentos();
            incidentesmodel.MunicipioI = recursosServicios.ObtenetMunicipios(1);
            //incidentesmodel.DepartamentoII = recursosServicios.ObtenerDepartamentos();
            //incidentesmodel.MunicipioII = recursosServicios.ObtenetMunicipios(1);
            incidentesmodel.DeptoCentroCostoII = recursosServicios.ObtenerDepartamentos();
            incidentesmodel.MncpioCentroCostoII = recursosServicios.ObtenetMunicipios(1);
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaEmpresa"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpDoc", "ni");
            request.AddParameter("doc", usuarioActual.NitEmpresa);
            request.AddParameter("color", "orange");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            IRestResponse<List<EmpresaSiarpDTO>> response = cliente.Execute<List<EmpresaSiarpDTO>>(request);
            var result = response.Content;
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaSiarpDTO>>(result);
            incidentesmodel.DepartamentoIV = recursosServicios.ObtenerDepartamentos();
            incidentesmodel.MncpioIV = recursosServicios.ObtenetMunicipios(1);
            var empresa = db.Tbl_Empresa.Where(x => x.Nit_Empresa == usuarioActual.NitEmpresa).SingleOrDefault();
            incidentesmodel.ActividadEconomicaII = empresa.Descripcion_Actividad;
            incidentesmodel.NumeroIdentificacionII = empresa.Nit_Empresa;
            incidentesmodel.NombreRazonSocialII = empresa.Razon_Social;
            incidentesmodel.DireccionPpalII = empresa.Direccion;
            incidentesmodel.TelefonoII = empresa.Telefono.Value.ToString();
            incidentesmodel.FaxII = empresa.Fax.Value.ToString();
            incidentesmodel.st_DepartamentoII = respuesta[0].departamento;
            incidentesmodel.st_MunicipioII = respuesta[0].municipio;
            incidentesmodel.EmailII =respuesta[0].emailEmpresa;
            //incidentesmodel.ZonaUrbanaII =respuesta[0].zona;


            return View(incidentesmodel);
        }

        [HttpGet]
        public JsonResult ObtenerMunicipiosxDepto(int pk_id_depto)
        {
            List<Municipio> listMunicipio = recursosServicios.ObtenetMunicipios(pk_id_depto);
            return Json(listMunicipio, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarIncidenteAT(IncidentesATModel frmIncidentesAT) 
        {
            int pk_incidentes = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                var incidente_at = db.Tbl_IncidentesAT.Where(x => x.PK_Incidentes_AT == frmIncidentesAT.PK_Incidentes_AT_Id).SingleOrDefault();
                db.Tbl_IncidentesAT.Remove(incidente_at);
                db.SaveChanges();

                IncidentesAT incidentes = new IncidentesAT()
                {
                    Incidente = frmIncidentesAT.boIncidente,
                    AccidenteTrabajo = frmIncidentesAT.AccidenteTrabajo,
                    Leve = frmIncidentesAT.Leve,
                    Grave = frmIncidentesAT.Grave,
                    Mortal = frmIncidentesAT.Mortal,
                    FechaInvestigacionI = frmIncidentesAT.FechaInvestigacionI,
                    pk_DepartamentoI = frmIncidentesAT.pk_DepartamentoI,
                    pk_MunicipioI = frmIncidentesAT.pk_MunicipioI,
                    DireccionI = frmIncidentesAT.DireccionI,
                    HoraInicialI = frmIncidentesAT.HoraInicialI,
                    HoraFinalI = frmIncidentesAT.HoraFinalI,
                    ResponsablesI = frmIncidentesAT.ResponsablesI,
                    FotografiasI = frmIncidentesAT.FotografiasI,
                    VideosI = frmIncidentesAT.VideosI,
                    CintasAudioI = frmIncidentesAT.CintasAudioI,
                    IlustracionesI = frmIncidentesAT.IlustracionesI,
                    DiagramasI = frmIncidentesAT.DiagramasI,
                    OtrosI = frmIncidentesAT.OtrosI,
                    CualesI = frmIncidentesAT.CualesI,

                    TipoVinculacionII = frmIncidentesAT.TipoVinculacionII,
                    TipoIdentificacionII = frmIncidentesAT.TipoIdentificacionII,
                    ActividadEconomicaII = frmIncidentesAT.ActividadEconomicaII,
                    NumeroIdentificacionII = frmIncidentesAT.NumeroIdentificacionII,
                    NombreRazonSocialII = frmIncidentesAT.NombreRazonSocialII,
                    DireccionPpalII = frmIncidentesAT.DireccionPpalII,
                    TelefonoII = frmIncidentesAT.TelefonoII,
                    FaxII = frmIncidentesAT.FaxII,
                    //pk_DepartamentoII = frmIncidentesAT.pk_DepartamentoII,
                    //pk_MunicipioII = frmIncidentesAT.pk_MunicipioII,
                    EmailII = frmIncidentesAT.EmailII,
                    ZonaUrbanaII = frmIncidentesAT.ZonaUrbanaII,
                    SedePrincipalII = frmIncidentesAT.SedePrincipalII,
                    ActEconoCentroTrabajoII = frmIncidentesAT.ActEconoCentroTrabajoII,
                    pk_ActEconoCentroTrabajoII = frmIncidentesAT.pk_ActEconoCentroTrabajoII,
                    CentroCostoTelefonoII = frmIncidentesAT.CentroCostoTelefonoII,
                    CentroCostoFaxII = frmIncidentesAT.CentroCostoFaxII,
                    DireccionCentroTrabajoII = frmIncidentesAT.DireccionCentroTrabajoII,
                    ZonaII = frmIncidentesAT.ZonaII,
                    pk_DeptoCentroCostoII = frmIncidentesAT.pk_DeptoCentroCostoII,
                    pk_MncpioCentroCostoII = frmIncidentesAT.pk_MncpioCentroCostoII,

                    pk_DepartamentoIV = frmIncidentesAT.pk_DepartamentoIV,
                    pk_MncpioIV = frmIncidentesAT.pk_MncpioIV,
	
                    TipoVinculacionIII = frmIncidentesAT.TipoVinculacionIII,
                    NumeroIdentificacionIII = frmIncidentesAT.NumeroIdentificacionIII,
                    PrimerApellidoIII = frmIncidentesAT.PrimerApellidoIII,
                    SegundoApellidoIII = frmIncidentesAT.SegundoApellidoIII,
                    PrimerNombreIII = frmIncidentesAT.PrimerNombreIII,
                    FechaNacimientoIII = frmIncidentesAT.FechaNacimientoIII,
                    SexoIII = frmIncidentesAT.SexoIII,
                    EPSIII = frmIncidentesAT.EPSIII,
                    AFPIII = frmIncidentesAT.AFPIII,
                    ARLIII = frmIncidentesAT.ARLIII,
                    TelefonoIII = frmIncidentesAT.TelefonoIII,
                    FaxIII = frmIncidentesAT.FaxIII,
                    EmailIII = frmIncidentesAT.EmailIII,
                    DireccionCentroTrabajoIII = frmIncidentesAT.DireccionCentroTrabajoIII,
                    ZonaIII = frmIncidentesAT.ZonaIII,
                    CargoIII = frmIncidentesAT.CargoIII,
                    OcupacionIII = frmIncidentesAT.OcupacionIII,
                    FechaIngresoIII = frmIncidentesAT.FechaIngresoIII,
                    TiempoOcupacionAIII = frmIncidentesAT.TiempoOcupacionAIII,
                    TiempoOcupacionMIII = frmIncidentesAT.TiempoOcupacionMIII,
                    AntiguedadAIII = frmIncidentesAT.AntiguedadAIII,
                    AntiguedadMIII = frmIncidentesAT.AntiguedadMIII,
                    DiurnoIII = frmIncidentesAT.DiurnoIII,
                    NocturnoIII = frmIncidentesAT.NocturnoIII,
                    MixtoIII = frmIncidentesAT.MixtoIII,
                    TurnosIII = frmIncidentesAT.TurnosIII,
                    SalarioHonorariosIII = frmIncidentesAT.SalarioHonorariosIII,
                    FechaMuerteIII = frmIncidentesAT.FechaMuerteIII,
                    AtencionOportunaIII = frmIncidentesAT.AtencionOportunaIII,

                    FechaOcurrenciaIV = frmIncidentesAT.FechaOcurrenciaIV,
                    HoraOcurrenciaIV = frmIncidentesAT.HoraOcurrenciaIV,
                    JornadaIV = frmIncidentesAT.JornadaIV,
                    ExtraIV = frmIncidentesAT.ExtraIV,
                    LunesIV = frmIncidentesAT.LunesIV,
                    MartesIV = frmIncidentesAT.MartesIV,
                    MiercolesIV = frmIncidentesAT.MiercolesIV,
                    JuevesIV = frmIncidentesAT.JuevesIV,
                    ViernesIV = frmIncidentesAT.ViernesIV,
                    SabadoIV = frmIncidentesAT.SabadoIV,
                    DomingoIV = frmIncidentesAT.DomingoIV,
                    LaborHabitualIV = frmIncidentesAT.LaborHabitualIV,
                    LaborHabitualIVS = frmIncidentesAT.LaborHabitualIVS,
                    LaborHabitualIVN = frmIncidentesAT.LaborHabitualIVN,
                    TipoIncidenteIV = frmIncidentesAT.TipoIncidenteIV,
                    EspecTipoIncidenteIV = frmIncidentesAT.EspecTipoIncidenteIV,
                    IPSAtendioIV = frmIncidentesAT.IPSAtendioIV,
                    ZonaIV = frmIncidentesAT.ZonaIV,
                    TiempoLaboradoPrevioIV = frmIncidentesAT.TiempoLaboradoPrevioIV,
                    LugarExactoIV = frmIncidentesAT.LugarExactoIV,
                    SitioExactoIV = frmIncidentesAT.SitioExactoIV,
                    OtroSitioIV = frmIncidentesAT.OtroSitioIV,
                    EspecifiqueIV = frmIncidentesAT.EspecifiqueIV,

                    EventosSimilaresV = frmIncidentesAT.EventosSimilaresV,
                    NumeroPersonasV = frmIncidentesAT.NumeroPersonasV,
                    OtrosIncidentesV = frmIncidentesAT.OtrosIncidentesV,
                    EventoSimilarV = frmIncidentesAT.EventoSimilarV,
                    CondicionPrioritariaV = frmIncidentesAT.CondicionPrioritariaV,
                    TrabajadorInvolucradoV = frmIncidentesAT.TrabajadorInvolucradoV,
                    PanoramaRiesgoV = frmIncidentesAT.PanoramaRiesgoV,
                    DescripcionAccidenteV = frmIncidentesAT.DescripcionAccidenteV,

                    AgenteVI = frmIncidentesAT.AgenteVI,
                    MaterialVI = frmIncidentesAT.MaterialVI,
                    ModeloVI = frmIncidentesAT.ModeloVI,
                    ReferenciaVI = frmIncidentesAT.ReferenciaVI,
                    PesoVI = frmIncidentesAT.PesoVI,
                    PesoUnidadMedidaVI = frmIncidentesAT.PesoUnidadMedidaVI,
                    AlturaVI = frmIncidentesAT.AlturaVI,
                    AnchoVI = frmIncidentesAT.AnchoVI,
                    VolumenVI = frmIncidentesAT.VolumenVI,
                    ProfundidadVI = frmIncidentesAT.ProfundidadVI,
                    VelocidadVI = frmIncidentesAT.VelocidadVI,
                    TiempoUsoVI = frmIncidentesAT.TiempoUsoVI,
                    FechaMantenimientoVI = frmIncidentesAT.FechaMantenimientoVI,
                    ReparadoVI = frmIncidentesAT.ReparadoVI,
                    ExplosivosVI = frmIncidentesAT.ExplosivosVI,
                    ExplosivosUnidadMedidaVI = frmIncidentesAT.ExplosivosUnidadMedidaVI,
                    GasesVI = frmIncidentesAT.GasesVI,
                    GasesCantidadVI = frmIncidentesAT.GasesCantidadVI,
                    TemperaturaUnidadMedidaVI = frmIncidentesAT.TemperaturaUnidadMedidaVI,
                    SustanciaUnidadMedidaVI = frmIncidentesAT.SustanciaUnidadMedidaVI,
                    SustanciaCantidadVI = frmIncidentesAT.SustanciaCantidadVI,
                    VoltajeElectricoVI = frmIncidentesAT.VoltajeElectricoVI,
                    VoltajeElectricoUnidadMedidaVI = frmIncidentesAT.VoltajeElectricoUnidadMedidaVI,
                    UnidadMedidaVI = frmIncidentesAT.UnidadMedidaVI,
                    DetallesAdicionalesVI = frmIncidentesAT.DetallesAdicionalesVI,
                    EPPVI = frmIncidentesAT.EPPVI,
                    TrabajadorEPPVI = frmIncidentesAT.TrabajadorEPPVI,
                    ObservacionesVI = frmIncidentesAT.ObservacionesVI,


                    CodTipoLesionVII = frmIncidentesAT.CodTipoLesionVII,
                    TipoLesionVII = frmIncidentesAT.TipoLesionVII,
                    CodigoParteCuerpoAfectadaVII = frmIncidentesAT.CodigoParteCuerpoAfectadaVII,
                    CodMecaAccideneteVII = frmIncidentesAT.CodMecaAccideneteVII,
                    MecanismoAccidenteVII = frmIncidentesAT.MecanismoAccidenteVII,
                    CodAgenteAccideneteVII = frmIncidentesAT.CodAgenteAccideneteVII,
                    AgenteAccidenteVII = frmIncidentesAT.AgenteAccidenteVII,
                    CodFactoresPersonalesVII1 = frmIncidentesAT.CodFactoresPersonalesVII1,
                    FactoresPersonalesVII1 = frmIncidentesAT.FactoresPersonalesVII1,
                    CodFactoresPersonalesVII2 = frmIncidentesAT.CodFactoresPersonalesVII2,
                    FactoresPersonalesVII2 = frmIncidentesAT.FactoresPersonalesVII2,
                    CodActoSubestandarVII1 = frmIncidentesAT.CodActoSubestandarVII1,
                    ActosSubestandarVII1 = frmIncidentesAT.ActosSubestandarVII1,
                    CodActoSubestandarVII2 = frmIncidentesAT.CodActoSubestandarVII2,
                    ActosSubestandarVII2 = frmIncidentesAT.ActosSubestandarVII2,
                    CodFactoresTrabajoVII1 = frmIncidentesAT.CodFactoresTrabajoVII1,
                    FactoresTrabajoVII1 = frmIncidentesAT.FactoresTrabajoVII1,
                    CodFactoresTrabajoVII2 = frmIncidentesAT.CodFactoresTrabajoVII2,
                    FactoresTrabajoVII2 = frmIncidentesAT.FactoresTrabajoVII2,
                    CodCondAmbientalesVII1 = frmIncidentesAT.CodCondAmbientalesVII1,
                    CondAmbientalesVII1 = frmIncidentesAT.CondAmbientalesVII1,
                    CodCondAmbientalesVII2 = frmIncidentesAT.CodCondAmbientalesVII2,
                    CondAmbientalesVII2 = frmIncidentesAT.CondAmbientalesVII2,
                    TipoIdentJefeInmediantoIX = frmIncidentesAT.TipoIdentJefeInmediantoIX,
                    NumIdentJefeInmediatoIX = frmIncidentesAT.NumIdentJefeInmediatoIX,
                    JefeInmediatoNombresIX = frmIncidentesAT.JefeInmediatoNombresIX,
                    JefeInmediatoCargoIX = frmIncidentesAT.JefeInmediatoCargoIX,


                    DescripcionAnalisisIX = frmIncidentesAT.DescripcionAnalisisIX,
                    TipoIdentEncargadoPSOIX = frmIncidentesAT.TipoIdentEncargadoPSOIX,
                    NumIdentPSOIX = frmIncidentesAT.NumIdentPSOIX,
                    EncargadoPSONombresIX = frmIncidentesAT.EncargadoPSONombresIX,
                    EncargadoPSOCargoIX = frmIncidentesAT.EncargadoPSOCargoIX,
                    TipoIdentCOPASOIX = frmIncidentesAT.TipoIdentCOPASOIX,
                    COPASONumIdentIX = frmIncidentesAT.COPASONumIdentIX,
                    COPASONombresCompletosIX = frmIncidentesAT.COPASONombresCompletosIX,
                    COPASOCargoIX = frmIncidentesAT.COPASOCargoIX,
                    TipoIdentEncargadosPSOIX = frmIncidentesAT.TipoIdentEncargadosPSOIX,
                    NumeroIdentBrigadistaIX = frmIncidentesAT.NumeroIdentBrigadistaIX,
                    BrigadistaNombresIX = frmIncidentesAT.BrigadistaNombresIX,
                    BrigadistaCargoIX = frmIncidentesAT.BrigadistaCargoIX,
                    TipoIdentParticipanteIX = frmIncidentesAT.TipoIdentParticipanteIX,
                    NumIdentParticipanteIX = frmIncidentesAT.NumIdentParticipanteIX,
                    ParticipanteNombreIX = frmIncidentesAT.ParticipanteNombreIX,
                    ParticipanteCargoIX = frmIncidentesAT.ParticipanteCargoIX,
                    TipoIdentAnalisisIX = frmIncidentesAT.TipoIdentAnalisisIX,
                    NumIdentAnalisisIX = frmIncidentesAT.NumIdentAnalisisIX,
                    CargoAnalisisIX = frmIncidentesAT.CargoAnalisisIX,
                    EmpresaRepresentaIX = frmIncidentesAT.EmpresaRepresentaIX,
                    ObservacionEspecialistaIX = frmIncidentesAT.ObservacionEspecialistaIX,



                    CausasInmediatasTipoC1X = frmIncidentesAT.CausasInmediatasTipoC1X,
                    CausasInmediatasTipoP1X = frmIncidentesAT.CausasInmediatasTipoP1X,
                    MedidasIntervencion1X = frmIncidentesAT.MedidasIntervencion1X,
                    TipoF1X = frmIncidentesAT.TipoF1X,
                    TipoM1X = frmIncidentesAT.TipoM1X,
                    TipoT1X = frmIncidentesAT.TipoT1X,
                    RespImplementacion1X = frmIncidentesAT.RespImplementacion1X,
                    FechaImplementacion1X = frmIncidentesAT.FechaImplementacion1X,

                    CausasInmediatasTipoC2X = frmIncidentesAT.CausasInmediatasTipoC2X,
                    CausasInmediatasTipoP2X = frmIncidentesAT.CausasInmediatasTipoP2X,
                    MedidasIntervencion2X = frmIncidentesAT.MedidasIntervencion2X,
                    TipoF2X = frmIncidentesAT.TipoF2X,
                    TipoM2X = frmIncidentesAT.TipoM2X,
                    TipoT2X = frmIncidentesAT.TipoT2X,
                    RespImplementacion2X = frmIncidentesAT.RespImplementacion2X,
                    FechaImplementacion2X = frmIncidentesAT.FechaImplementacion2X,

                    CausasInmediatasTipoC3X = frmIncidentesAT.CausasInmediatasTipoC3X,
                    CausasInmediatasTipoP3X = frmIncidentesAT.CausasInmediatasTipoP3X,
                    MedidasIntervencion3X = frmIncidentesAT.MedidasIntervencion3X,
                    TipoF3X = frmIncidentesAT.TipoF3X,
                    TipoM3X = frmIncidentesAT.TipoM3X,
                    TipoT3X = frmIncidentesAT.TipoT3X,
                    RespImplementacion3X = frmIncidentesAT.RespImplementacion3X,
                    FechaImplementacion3X = frmIncidentesAT.FechaImplementacion3X,

                    CausasBasicasTipoC1X = frmIncidentesAT.CausasBasicasTipoC1X,
                    CausasBasicasTipoP1X = frmIncidentesAT.CausasBasicasTipoP1X,
                    BasicasInmediatas1X = frmIncidentesAT.BasicasInmediatas1X,
                    BasicasF1X = frmIncidentesAT.BasicasF1X,
                    BasicasM1X = frmIncidentesAT.BasicasM1X,
                    BasicasT1X = frmIncidentesAT.BasicasT1X,
                    BasicasRespImplementacion1X = frmIncidentesAT.BasicasRespImplementacion1X,
                    BasicasFechaImplementacion1X = frmIncidentesAT.BasicasFechaImplementacion1X,

                    CausasBasicasTipoC2X = frmIncidentesAT.CausasBasicasTipoC2X,
                    CausasBasicasTipoP2X = frmIncidentesAT.CausasBasicasTipoP2X,
                    BasicasInmediatas2X = frmIncidentesAT.BasicasInmediatas2X,
                    BasicasF2X = frmIncidentesAT.BasicasF2X,
                    BasicasM2X = frmIncidentesAT.BasicasM2X,
                    BasicasT2X = frmIncidentesAT.BasicasT2X,
                    BasicasRespImplementacion2X = frmIncidentesAT.BasicasRespImplementacion2X,
                    BasicasFechaImplementacion2X = frmIncidentesAT.BasicasFechaImplementacion2X ,

                    CausasBasicasTipoC3X = frmIncidentesAT.CausasBasicasTipoC3X,
                    CausasBasicasTipoP3X = frmIncidentesAT.CausasBasicasTipoP3X,
                    BasicasInmediatas3X = frmIncidentesAT.BasicasInmediatas3X,
                    BasicasF3X = frmIncidentesAT.BasicasF3X,
                    BasicasM3X = frmIncidentesAT.BasicasM3X,
                    BasicasT3X = frmIncidentesAT.BasicasT3X,
                    BasicasRespImplementacion3X = frmIncidentesAT.BasicasRespImplementacion3X,
                    BasicasFechaImplementacion3X = frmIncidentesAT.BasicasFechaImplementacion3X,

                    FechaRemisionXI = frmIncidentesAT.FechaRemisionXI,
                    NoFoliosXI = frmIncidentesAT.NoFoliosXI,
                    TipoIdentificacionXI  = frmIncidentesAT.TipoIdentificacionXI,
                    NumeroIdentificacionXI = frmIncidentesAT.NumeroIdentificacionXI,
                    NombresXI = frmIncidentesAT.NombresXI,
                    CargoXI = frmIncidentesAT.CargoXI,
                    RecomendacionesARLXI = frmIncidentesAT.RecomendacionesARLXI,
                    RemisionInformeARLXI = frmIncidentesAT.RemisionInformeARLXI,
                    RemisionMinisterioTrabajoXI = frmIncidentesAT.RemisionMinisterioTrabajoXI,


                    TipoIdentificacionXII = frmIncidentesAT.TipoIdentificacionXII,
                    NumeroIdentificacionXII = frmIncidentesAT.NumeroIdentificacionXII,
                    NombresXII = frmIncidentesAT.NombresXII,
                    CargoXII = frmIncidentesAT.CargoXII,
                    MedidasIntervencionXII = frmIncidentesAT.MedidasIntervencionXII,
                    ObservacionesXII  = frmIncidentesAT.ObservacionesXII
                };
                db.Tbl_IncidentesAT.Add(incidentes);
                db.SaveChanges();
                Transaction.Commit();
                pk_incidentes = incidentes.PK_Incidentes_AT;
            }


            return Json(pk_incidentes, JsonRequestBehavior.AllowGet);
        }

        

    }
}