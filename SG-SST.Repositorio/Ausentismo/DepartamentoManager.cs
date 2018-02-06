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
    public class DepartamentoManager : IDepartamento
    {
        /// <summary>
        /// Obtiene el listado de empresas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDDepartamento> ObtenerDepartamento()
        {
            try
            {
                List<EDDepartamento> departamento = null;
                using (var context = new SG_SSTContext())
                {
                    departamento = context.Tbl_Departamento.Select(c => new EDDepartamento()
                    {
                        IdDepartamento = c.Pk_Id_Departamento,
                        Nombre = c.Nombre_Departamento,
                    }).ToList();
                }
                return departamento;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int ValidarDepartamento(int idDepartamento)
        {
            int result = 0;
            try
            {                
                using (var context = new SG_SSTContext())
                {
                    var  departamento = context.Tbl_Departamento.Where(d => d.Pk_Id_Departamento == idDepartamento).Select(d => d).FirstOrDefault();
                    if (departamento != null)
                        result = departamento.Pk_Id_Departamento;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }

}
