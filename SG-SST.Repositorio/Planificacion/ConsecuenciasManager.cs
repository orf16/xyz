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
    public class ConsecuenciasManager : IConsecuencias
    {
        public List<EDConsecuencia> ObtenerConsecuencias(int PK_TipoMedologia)
        {
            List<EDConsecuencia> consecuencias = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                consecuencias = (from c in contex.Tbl_Consecuencias
                                 join g in contex.Tbl_Grupos on c.FK_Grupo equals g.PK_Grupo
                                 join m in contex.Tbl_Metodologia on g.FK_Metodologia equals m.PK_Metodologia
                                 where m.PK_Metodologia == PK_TipoMedologia
                                 select new EDConsecuencia
                                  {
                                      PK_Consecuencia = c.PK_Consecuencia,
                                      Valor_Consecuencia = c.Valor_Consecuencia,
                                      Descripcion_Consecuencia = c.Descripcion_Consecuencia
                                  }).ToList();
            }

            return consecuencias;

        }

        public List<EDConsecuencia> ObtenerConsecuenciasPorGrupo(int PK_Grupo) 
        {
            List<EDConsecuencia> consecuencias = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                consecuencias = (from c in contex.Tbl_Consecuencias
                                 where c.FK_Grupo == PK_Grupo
                                 select new EDConsecuencia
                                 {
                                     PK_Consecuencia = c.PK_Consecuencia,
                                     Valor_Consecuencia = c.Valor_Consecuencia,
                                     Descripcion_Consecuencia = c.Descripcion_Consecuencia
                                 }).ToList();
            }

            return consecuencias;                  
        }

    }
}
