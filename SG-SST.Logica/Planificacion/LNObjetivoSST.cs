using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Planificacion
{
    public class LNObjetivoSST
    {
        private static IObjetivoSST obj = IMObjetivoSST.Objetivo();

        public List<EDObjetivoSST> ObtenerObjetivos(int IdEmpresa)
        {
            return obj.ObtenerObjetivos(IdEmpresa);
        }

        public List<EDObjetivoSST> GuardarObjetivo(EDObjetivoSST objetivo)
        {
            return obj.GuardarObjetivo(objetivo);
        }

        public List<EDObjetivoSST> EliminarObjetivos(List<EDObjetivoSST> objetivos)
        {
            return obj.EliminarObjetivo(objetivos);
        }

    }
}
