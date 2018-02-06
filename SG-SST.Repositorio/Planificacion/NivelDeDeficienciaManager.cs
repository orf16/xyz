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
    public class NivelDeDeficienciaManager : INivelDeDeficiencia
    {
        public List<EDNivelDeDeficiencia> ObtenerNivelesDeDeficiencia(bool FLAG_Cuantitativa)
        {
            List<EDNivelDeDeficiencia> nivelesDeDeficiencia = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                nivelesDeDeficiencia = (from p in contex.Tbl_Nivel_De_Deficiencia

                                        where p.FLAG_Cuantitativa == FLAG_Cuantitativa
                                        select new EDNivelDeDeficiencia
                                  {
                                      PK_Nivel_De_Deficiencia = p.PK_Nivel_De_Deficiencia,
                                      Valor_Deficiencia = p.Valor_Deficiencia,
                                      Descripcion_Deficiciencia = p.Descripcion_Deficiciencia
                                  }).ToList();
            }

            return nivelesDeDeficiencia;
        }
    }
}
