using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IPromocioPrevencion
    {
        List<EDPlanVial> ConsultarPlanesVial(List<EDSede> ListaSedes);
        bool CrearPlan(int IdSede, int IdEmpresa, List<EDSegVialParametro> PlanVial);
        bool CrearParametro(EDSegVialParametro EDSegVialParametro);
        List<EDSegVialResultado> ConsultarPlanVialResultado(int IdSegVial, List<EDSede> ListaSedes, int IdEmpresa);
        List<EDSegVialPilar> ConsultarPlanVialPilares(int IdSegVial, List<EDSede> ListaSedes);
        bool GuardarEvaluacion (List<SegVialResultado> ListaResultados);
        bool ExisteParametroResultado(int pkParametro, int IdEmpresa);
        bool VerificarEstado(int IdSegVial);
        List<EDSede> ObtenernerSedesPorEmpresa(int fk_empresa);
        bool EliminarParametro(int pkParametro, int IdEmpresa);
        bool OcultarParametro(int pkParametro, int IdEmpresa);
        List<EDSegVialParametro> ConsultarParametros(int fk_empresa);
        List<EDSegVialDetalle> ConsultarVariables(int pkparam);
    }
}
