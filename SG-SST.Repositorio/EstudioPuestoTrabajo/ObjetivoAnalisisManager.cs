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
    public class ObjetivoAnalisisManager : IObjetivoAnalisis
    {
        public List<EDObjetivoAnalisis> ObtenerObjetivosAnalisis()
        {
            List<EDObjetivoAnalisis> objetivosanalisis = null;
            using (SG_SSTContext datos = new SG_SSTContext())
            {
                objetivosanalisis = (from m in datos.Tbl_ObjetivoAnalisis
                                   select new EDObjetivoAnalisis
                                   {
                                       IdObjetivoAnlaisis = m.Pk_Id_ObjetivoAnalisis,
                                       NombreObjetivoAnalisis = m.Nombre_ObjetivoAnalisis
                                   }
                                ).ToList();

                return objetivosanalisis;
            }
        }
    }
}
