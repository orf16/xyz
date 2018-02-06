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
    public class ProbabilidadManager : IProbabilidad
    {
        public List<EDProbabilidad> ObtenerProbabilidades(int PK_TipoMedologia) 
        {
            List<EDProbabilidad> probabilidades = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                probabilidades = (from p in contex.Tbl_Probabilidades
                                 join m in contex.Tbl_Metodologia on p.FK_Metodologia equals m.PK_Metodologia                           
                                 where m.PK_Metodologia == PK_TipoMedologia
                                  select new EDProbabilidad
                                 {
                                     PK_Probabilidad = p.PK_Probabilidad,
                                     Descripcion_Probabilidad = p.Descripcion_Probabilidad
                                 }).ToList();
            }

            return probabilidades;
        }
    }
}
