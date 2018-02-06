using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.InterfazManager.Aplicacion;
using SG_SST.Models.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Aplicacion
{
    public class LNPromocionPrevencion
    {
        private static IPromocioPrevencion app = IMPromocionPrevencion.IPromocioPrevencion();

        public List<EDPlanVial> ConsultarPlanesVial(List<EDSede> ListaSedes)
        {
            List<EDPlanVial> NuevaListaPlanVial = app.ConsultarPlanesVial(ListaSedes);
            return NuevaListaPlanVial;
        }
        public bool CrearPlan(int IdSede, int IdEmpresa, List<EDSegVialParametro> PlanVial)
        {
            bool ProbarGuardado = app.CrearPlan(IdSede, IdEmpresa, PlanVial);
            return ProbarGuardado;
        }
        public bool CrearParametro(EDSegVialParametro EDSegVialParametro)
        {
            bool ProbarGuardado = app.CrearParametro(EDSegVialParametro);
            return ProbarGuardado;
        }
        public bool ExisteParametroResultado(int pkParametro, int IdEmpresa)
        {
            bool ProbarGuardado = app.ExisteParametroResultado(pkParametro, IdEmpresa);
            return ProbarGuardado;
        }
        public bool EliminarParametro(int pkParametro, int IdEmpresa)
        {
            bool ProbarGuardado = app.EliminarParametro(pkParametro, IdEmpresa);
            return ProbarGuardado;
        }
        public bool OcultarParametro(int pkParametro, int IdEmpresa)
        {
            bool ProbarGuardado = app.OcultarParametro(pkParametro, IdEmpresa);
            return ProbarGuardado;
        }
        public List<EDSegVialResultado> ConsultarPlanVialResultado(int IdSegVial, List<EDSede> ListaSedes, int IdEmpresa)
        {
            List<EDSegVialResultado> NuevaListaPlanVialRes = app.ConsultarPlanVialResultado(IdSegVial, ListaSedes, IdEmpresa);
            return NuevaListaPlanVialRes;
        }
        public List<EDSegVialPilar> ConsultarPlanVialPilares(int IdSegVial, List<EDSede> ListaSedes)
        {
            List<EDSegVialPilar> NuevaListaPlanVialpil = app.ConsultarPlanVialPilares(IdSegVial, ListaSedes);
            return NuevaListaPlanVialpil;
        }
        public List<EDSegVialParametro> ConsultarParametros(int fk_empresa)
        {
            List<EDSegVialParametro> NuevaListaPlanVialpil = app.ConsultarParametros(fk_empresa);
            return NuevaListaPlanVialpil;
        }
        public bool GuardarEvaluacion(List<SegVialResultado> ListaResultados)
        {
            bool ProbarGuardado = app.GuardarEvaluacion(ListaResultados);
            return ProbarGuardado;
        }
        public bool VerificarEstado(int IdSegVial)
        {
            bool ProbarEstado = app.VerificarEstado(IdSegVial);
            return ProbarEstado;
        }
        public List<EDSede> ObtenernerSedesPorEmpresa(int fk_empresa)
        {
            List<EDSede> Sedes = app.ObtenernerSedesPorEmpresa(fk_empresa);
            return Sedes;
        }


        public List<EDSegVialDetalle> ConsultarVariables(int pkparam)
        {
            List<EDSegVialDetalle> NuevaListaEDPlanVialpil = app.ConsultarVariables(pkparam);

            return NuevaListaEDPlanVialpil;
        }
    }
}
