using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Empresas;
using SG_SST.InterfazManager.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Empresas
{    
    public class LNEmpresa
    {
        private static IEmpresa em = IMEmpresa.Empresa();
        private static ISedeRepositorio sd = IMEmpresa.EmpresaSede ();

        public List<EDMunicipio> ObtenerMunicipiosporDepartamento(int id)
        {

            return em.ObtenerMunicipiosporDepartamento(id);
        }

        public List<EDDepartamento>ObtenerDepartamentos()
        {
            return em.ObtenerDepartamentos();

        }
        public List<EDCIIU> ObtenerActividadesEconomicas()
        {
            return em.ObtenerActividadesEconomicas();
        }
        public List<EDSede> ObtenerSedesPorNit(string Nit)
        {
            return em.ObtenernerSedesPorEmpresa(Nit);
        }

        public EDSede ObtenerSedesPorIdSede(int IdSede)
        {
            return em.ObtenernerSedesPorEmpresa(IdSede);
        }

        public List<EDEmpresa_Usuaria> ObtenerEmpresasUsuariasPorEmpresa(string Nit)
        {
            return em.ObtenerEmpresasUsuariasPorEmpresa(Nit);
        }

       public List<EDProceso> ObtenerProcesosPorEmpresaprnivel(string Nit)
        {
            return em.ObtenerProcesosPorEmpresaprnivel(Nit);
        }
        public List<EDSedeMunicipio> ObtenerSedesPorEmpresa(int IdEmpresa)
        {
            return sd.SedesMunicipioPorEmpresa(IdEmpresa);
        }

        public void GuardarRolesParaNuevaEmpresa()
        {
            em.GuardarRolesParaNuevaEmpresa();
        }
        public EDEmpresas GuardarEmpresaYSusRelaciones(EDEmpresas empresas)
        {
            EDEmpresas mp = em.GuardarEmpresaYSusRelaciones(empresas);

            if (mp.Id_Empresa>0)
            {
                return mp;
            }
            else
            {
                return null;
            }

            //return em.GuardarEmpresa(empresas);
        }

        public void GuardarSedePrincipal(EDSede sede)
        {
            em.GuardarSedePrincipal(sede);
        }
        public EDEmpresaEvaluar ObtenerDatosEmpresaEvaluar(string Nit, string Responsable)
        {
            return em.ObtenerDatosEmpresaEvaluar(Nit, Responsable);
        }

        public List<EDEmpresas> ObtenerEmpresasRegistradas()
        {
            return em.ObtenerEmpresasRegistradas();
        }
        //public EDEmpresas modificarEmpresa(int Pk_Id_Empresa)
        //{

        //    return em.modificarEmpresa(Pk_Id_Empresa);
        //}
        public EDEmpresas GuardarLogoEmpresa(EDEmpresas logo)
        {
            return em.GuardarLogoEmpresa(logo);
        }

        public EDEmpresas ObtenerLogoEmpresa(string nitempresa)
        {
            return em.ObtenerLogoEmpresa(nitempresa);
        }

    }
}
