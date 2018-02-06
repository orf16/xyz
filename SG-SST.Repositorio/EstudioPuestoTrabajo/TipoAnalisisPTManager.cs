using SG_SST.Interfaces.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Models;

namespace SG_SST.Repositorio.EstudioPuestoTrabajo
{
    public class TipoAnalisisPTManager : ITipoAnalisisPT
    {
        public List<EDTipoAnalisisPuestoTrabajo> ObtenerTiposAnalisisPT()
        {
            List<EDTipoAnalisisPuestoTrabajo> tiposanalisispt = null;
            using (SG_SSTContext datos = new SG_SSTContext())
            {
                tiposanalisispt = (from m in datos.Tbl_Tipo_Analisis_Puesto_Trabajo
                                     select new EDTipoAnalisisPuestoTrabajo
                                     {
                                         IdTipoAnalisisPT = m.Pk_Id_Tipo_Analisis_Puesto_Trabajo,
                                         NombreTipoAnalisisPT = m.Nombre_Tipo_Analisis_Puesto_Trabajo
                                     }
                                ).ToList();

                return tiposanalisispt;
            }
        }
    }
}
