using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class ContingenciaManager : IContingencia
    {
        /// <summary>
        /// Obtiene el listado de contingencias
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDContingencia> ObtenerContingencia()
        {
            try
            {
                List<EDContingencia> contingencia = null;
                using (var context = new SG_SSTContext())
                {
                    contingencia = context.Tbl_Contingencias.Select(c => new EDContingencia()
                    {
                        IdContingencia = c.PK_Id_Contingencia,
                        idTipoContingencia = c.FK_Tipo_Contingencia,
                        Detalle = c.Detalle
                    }).ToList();
                }
                return contingencia;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<EDContingencia> BuscarContingencia(string prefijo)
        {
            var context = new SG_SSTContext();
            return context.Tbl_Contingencias.Where(c => c.Detalle.StartsWith(prefijo)).Select(c => new EDContingencia()
            {
                IdContingencia = c.PK_Id_Contingencia,
                Detalle = c.Detalle
            }).ToList();
        }

        public int ValidarContingencia(int idContigencia)
        {
            int result = 0;
            using (var context = new SG_SSTContext())
            {
                var contingencia = context.Tbl_Contingencias.Where(c => c.PK_Id_Contingencia == idContigencia).Select(c => c).FirstOrDefault();
                if (contingencia != null)
                    result = contingencia.PK_Id_Contingencia;                
            }
            return result;
        }
    }

}
