using SG_SST.Models;
using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Services.Empresas.IServices
{
    interface IEmpresaServicios
    {
            

        Organigrama ObtenerOrganigrama(int Pk_Id_Empresa);

        bool AgregarEmpleadoOrg(EmpleadoOrg empleadoorg,int Pk_Id_Empresa);

        bool GuardarEmpresa(Empresa Empresas, Sede sede, SedeMunicipio sedemunicipio);
    }



}
