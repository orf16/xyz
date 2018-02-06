using SG_SST.Interfaces.Planificacion;
using SG_SST.Repositorio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Planificacion
{
    public class IMIdentificacionPeligros
    {
        public static IMetodologia Metodologia()
        {
            return new MetodologiaManager();
        }

        public static ITipoDePeligro TipoDePeligro() 
        {
            return new TipoDePeligroManager();            
        }

        public static IClasificacionPeligros ClasificacionPeligros()
        {
            return new ClasificacionDePeligroManager();
        }
        public static INivelesDeExposicion NivelDeExposicion() 
        {
            return new NivelesDeExposicionManager();
        }

        public static IConsecuencias Consecuencias() 
        {
            return new ConsecuenciasManager();
        }

        public static IProbabilidad Probabilidad()
        {
            return new ProbabilidadManager();
        }

        public static INivelDeDeficiencia NivelDeDeficiencia() 
        {
            return new NivelDeDeficienciaManager();
        }
    }
}
