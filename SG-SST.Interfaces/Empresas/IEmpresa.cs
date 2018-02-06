using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Empresas
{
    public interface IEmpresa
    {

        List<EDMunicipio> ObtenerMunicipiosporDepartamento(int id);

        List<EDDepartamento> ObtenerDepartamentos();
        
        List<EDSede> ObtenernerSedesPorEmpresa(string Nit);

        EDSede ObtenernerSedesPorEmpresa(int IdSede);
        List<EDCIIU> ObtenerActividadesEconomicas();
        List<EDEmpresa_Usuaria> ObtenerEmpresasUsuariasPorEmpresa(string Nit);
        void GuardarRolesParaNuevaEmpresa();
        EDEmpresas GuardarEmpresaYSusRelaciones(EDEmpresas empresa);
        void GuardarSedePrincipal(EDSede sede);
        EDEmpresaEvaluar ObtenerDatosEmpresaEvaluar(string Nit, string Responsable);
        List<EDTipoDocumento> ObtenerTiposDocumento();
        List<EDOcupacion> ObtenerOpucaciones();
        List<EDProceso> ObtenerProcesosPorEmpres(string Nit);
        List<EDProceso> ObtenerProcesosPorEmpresaprnivel(string Nit);
        List<EDEmpresas> ObtenerEmpresasRegistradas();
        int ValidarProceso(int idProceso, string nit);
        int ValidarOcupacion(int idOcupacion);
        int ValidarSede(int idSede, string nit);
        int ValidarTipoDocumento(int idTipoDoc);
        EDEmpresas GuardarLogoEmpresa(EDEmpresas logo);
        EDEmpresas ObtenerLogoEmpresa(string nitempresa);
    }
}
