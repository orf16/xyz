using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Empresas;
using SG_SST.InterfazManager.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Empresas
{
    public class LNEmpresaUsuaria
    {

        public List<EDDepartamento> lstDepartamentos = new List<EDDepartamento>();
        public List<EDMunicipio> lstMunicipios = new List<EDMunicipio>();
        public List<EDTipoDocumento> lstDocumentos = new List<EDTipoDocumento>();
        private static IEmpresaUsuaria em = IMEmpresa.EmpresaUsuaria();



        public LNEmpresaUsuaria()
        {

            lstDepartamentos = em.ObtenerDepartamentos();
            lstMunicipios = new List<EDMunicipio>();
            lstDocumentos = em.ObtenerDocumentos();
        }

        public List<EDMunicipio> DevuelveMunicipios(int pk_Departamento)
        {
            return em.ObtenerMunicipio(pk_Departamento);
        }

        public bool GrabarEmpresasUsuarias(List<EDEmpresa_UsuariaA> lstempUsu, out string mensajes_validaciones)
        {
            bool rta = false;
            System.Data.DataTable dtEmpresasUsuarias = em.EmpresaUsuaria();
            foreach (EDEmpresa_UsuariaA eu in lstempUsu)
            {
                if (!em.ExisteEmpresaUsuaria(eu.DocumentoEmpresa, eu.DocumentoEmpresaUsuaria))
                    dtEmpresasUsuarias.Rows.Add(null, eu.DocumentoEmpresa, eu.DocumentoEmpresaUsuaria, 0, eu.RazonSocial, eu.Direccion, eu.IdDepartamento, eu.Id_Municipio);

            }
            if (em.GuardarEmpresasUsuarias(dtEmpresasUsuarias))
            {
                mensajes_validaciones = "Información almacenada correctamente.";
                rta = true;
            }
            else
            {
                mensajes_validaciones = "Error guardando las empresas. Verifique";
                rta = false;
            }
            return rta;
        }

        public bool EliminaEmpresasUsuarias(string DocumentoEmpresa)
        {
            bool rta = false;
            if (em.EliminarEmpresasUsuarias(DocumentoEmpresa))
            {
                rta = true;
            }
            else
            {
                rta = false;
            }
            return rta;
        }



    }
}
