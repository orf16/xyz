using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class AlertasManager : IAlertas
    {
        [DbFunction("AusentismoModel.Store", "Alertas_Ausentismo")]
        public static decimal? Alertas_Ausentismo(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        /// <summary>
        /// Obtiene el listado de empresas
        /// </summary>
        /// <returns></returns>
        public List<EDAlertas> ConsultarAlertas(int anio)
        {
            try
            {
                using (var context = new SG_SSTContext())
                {
                    return context.Database.SqlQuery<EDAlertas>("SELECT * FROM Alertas_Ausentismo(@anio)", 
                        new SqlParameter { ParameterName = "anio", Value = anio }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }        
    }
}