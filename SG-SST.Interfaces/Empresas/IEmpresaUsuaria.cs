using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Interfaces.Empresas
{
    public interface IEmpresaUsuaria
    {
        List<EDDepartamento> ObtenerDepartamentos();
        List<EDMunicipio> ObtenerMunicipio(int PK_Departamento);
        List<EDMunicipio> ObtenerMunicipiosConDetps();
        List<EDTipoDocumento> ObtenerDocumentos();

        string DescripcionDepartamento(string idDepartamento);
        string DescripcionMunicipio(string idDepartamento , string idMunicipio);

        System.Data.DataTable EmpresaUsuaria();

        bool EliminarEmpresasUsuarias(string DocumentoEmpresa);

        bool ExisteEmpresaUsuaria(string DocumentoEmpresa , string DocumentoEmpresaUsuaria);

        bool GuardarEmpresasUsuarias(System.Data.DataTable drEmpresasUsuarias);



    }
}
