using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models;
using SG_SST.Interfaces.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Planificacion
{
    public class NivelesDeExposicionManager : INivelesDeExposicion
    {
        public List<EDNivelDeExposicion> ObtenerNivelesDeExposicion()
        {
            List<EDNivelDeExposicion> nve = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                nve = (from ne in contex.Tbl_Nivel_De_Exposicion
                       select new EDNivelDeExposicion
                       {
                           PK_Nivel_De_Exposicion = ne.PK_Nivel_De_Exposicion,
                           Valor_De_Exposicion = ne.Valor_Exposicion,
                           Descripcion_Exposicion = ne.Descripcion_Exposicion

                       }
                               ).ToList();
            }

            return nve;
        }

        public int ObtenerValorExposicion(int PK_Exposicion)
        {
            //NivelDeExposicion nivelDeExposicion = db.Tbl_Nivel_De_Exposicion.Find(PK_Exposicion);

            //if (nivelDeExposicion != null)
            //{
            //    return nivelDeExposicion.Valor_Exposicion;
            //}
            // se retorna -1 cuando no se encuentra el valor de la Exposicion 
            return -1;
        }
    }
}
