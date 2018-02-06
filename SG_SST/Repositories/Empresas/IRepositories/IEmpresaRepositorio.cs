using SG_SST.Models;
using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositories.Empresas.IRepositories
{
    interface IEmpresaRepositorio
    {   
        Organigrama ObtenerOrganigrama(int Pk_Id_Empresa);
        bool GuardarOrganigrama(EmpleadoOrg empleadoorg, int Pk_Id_Empresa);


        bool GuardarEmpresa(Empresa Empresas, Sede sede, SedeMunicipio sedemunicipio);

        void ModificarEmpresa(Empresa Empresas);
    }
}
