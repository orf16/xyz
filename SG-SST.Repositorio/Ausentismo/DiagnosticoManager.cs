using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class DiagnosticoManager : IDiagnostico
    {
        /// <summary>
        /// Obtiene el listado de diagnosticos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDDiagnostico> ObtenerDiagnostico()
        {
            try
            {
                List<EDDiagnostico> diagnostico = null;
                using (var context = new SG_SSTContext())
                {
                    diagnostico = context.Tbl_Diagnosticos.Where(c => c.PK_Id_Diagnostico > 0).Select(c => new EDDiagnostico()
                    {
                        IdDiagnostico = c.PK_Id_Diagnostico,
                        Codigo = c.Codigo_CIE,
                        Descripcion = c.Descripcion
                    }).ToList();
                }
                return diagnostico;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Busca un diagnóstico por el criterio de búsqueda.
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        public List<EDDiagnostico> BuscarDiagnostico(string prefijo)
        {
            var context = new SG_SSTContext();
            return context.Tbl_Diagnosticos.Where(d => d.Descripcion.Contains(prefijo) || d.Codigo_CIE.Contains(prefijo)).Select(d => new EDDiagnostico()
            {
                IdDiagnostico = d.PK_Id_Diagnostico,
                Codigo = d.Codigo_CIE,
                Descripcion = d.Descripcion
            }).ToList();
        }


        public IEnumerable<EDDiagnostico> ObtenerDiagnostico(string idEmpresa)
        {
            try
            {
                List<EDDiagnostico> diagnostico = null;
                using (var context = new SG_SSTContext())
                {
                    diagnostico = (from au in context.Tbl_Ausencias
                                   join dg in context.Tbl_Diagnosticos on au.FK_Id_Diagnostico equals dg.PK_Id_Diagnostico
                                   where dg.PK_Id_Diagnostico > 0 && au.NitEmpresa.Trim().Equals(idEmpresa.Trim())
                                   select new EDDiagnostico()
                                   {
                                       IdDiagnostico = dg.PK_Id_Diagnostico,
                                       Codigo = dg.Codigo_CIE,
                                       Descripcion = dg.Descripcion
                                   }).Distinct().ToList();
                }
                return diagnostico;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public EDDiagnostico ObtenerGiagnosticoPorCodigo(string codigo)
        {
            EDDiagnostico diagnostico = new EDDiagnostico();
            using (SG_SSTContext context = new SG_SSTContext ())
            {
                diagnostico = context.Tbl_Diagnosticos.Where(d => d.Codigo_CIE.Trim().ToUpper().Equals(codigo.Trim().ToUpper()))
                    .Select(d => new EDDiagnostico
                    {
                        IdDiagnostico = d.PK_Id_Diagnostico,
                        Descripcion = d.Descripcion,
                        Codigo = d.Codigo_CIE
                    }).FirstOrDefault();
            }

            return diagnostico;
        }
    }
}
