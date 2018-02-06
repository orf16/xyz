using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.Repositories.Empresas.IRepositories;
using SG_SST.Repositories.Empresas.Repositories;
using SG_SST.Services.Empresas.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Services.Empresas.Services
{
    public class EmpresaServicios:IEmpresaServicios
    {
        IEmpresaRepositorio er;
        IEmpresaRepositorio empresaRepositorio;
        public EmpresaServicios()//Metodo Constructor
        {
            er = new EmpresaRepositorio();  
        }

        public EmpresaServicios(SG_SSTContext db)
        {
            er = new EmpresaRepositorio(db);  
        }
          


        public Organigrama ObtenerOrganigrama(int Pk_Id_Empresa)
        {
            Organigrama organigramas =er.ObtenerOrganigrama(Pk_Id_Empresa);//obtiene la consulta del RepositorioGobierno
            if(organigramas!=null)//pregunta que si lo que viene de la consulta no esta vacio 
            {
                return organigramas;//retorna el organigrama.
            }
            return null;//Sino Retorna Null
        }

        public bool AgregarEmpleadoOrg(EmpleadoOrg empleadoorg, int Pk_Id_Empresa)
        {
            return er.GuardarOrganigrama(empleadoorg,Pk_Id_Empresa);  
        }

        public bool GuardarEmpresa(Empresa Empresas,Sede sede,SedeMunicipio sedemunicipio)
        {

            return er.GuardarEmpresa(Empresas, sede, sedemunicipio);
        }
    }


}