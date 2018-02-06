using SG_SST.EntidadesDominio.Empresas;
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
    public class LNMetodologia
    {
        private static IMetodologia metodologia = IMIdentificacionPeligros.Metodologia();
        private static ITipoDePeligro tipoDePeligro = IMIdentificacionPeligros.TipoDePeligro();
        private static INivelesDeExposicion nivelesDeExposicion = IMIdentificacionPeligros.NivelDeExposicion();
        private static IConsecuencias consecuencias = IMIdentificacionPeligros.Consecuencias();
        private static IProbabilidad probabilidades = IMIdentificacionPeligros.Probabilidad();
        private static INivelDeDeficiencia nivelesDeDeficiencia = IMIdentificacionPeligros.NivelDeDeficiencia();

        #region aplicacion
        public List<EDMetodologia> ObtenerMedologias()
        {
            return metodologia.ObtenerMedologias();
        }

        public List<EDMetodologia> ObtenerMedologias(int id_Sede)
        {
            return metodologia.ObtenerMedologias(id_Sede);
        }

        public List<EDTipoDePeligro> ObtenerTiposDePeligro()
        {
            return tipoDePeligro.ObtenerTiposDePeligro();
        }

        public List<EDNivelDeExposicion> ObtenerNivelesDeExposicion()
        {
            return nivelesDeExposicion.ObtenerNivelesDeExposicion();
        }

        public List<EDConsecuencia> ObtenerConsecuencias(int PK_TipoMedologia)
        {
            return consecuencias.ObtenerConsecuencias(PK_TipoMedologia);
        }

        public List<EDConsecuencia> ObtenerConsecuenciasPorGrupo(int PK_Grupo)
        {
            return consecuencias.ObtenerConsecuenciasPorGrupo(PK_Grupo);
        }

        public List<EDProbabilidad> ObtenerProbabilidades(int PK_TipoMedologia)
        {
            return probabilidades.ObtenerProbabilidades(PK_TipoMedologia);
        }

        public List<EDNivelDeDeficiencia> ObtenerNivelesDeDeficiencia(bool FLAG_Cuantitativa)
        {
            return nivelesDeDeficiencia.ObtenerNivelesDeDeficiencia(FLAG_Cuantitativa);
        }
        #endregion

        #region app

        public List<EDPeligroIdentificadoApp> ObtenerPeligrosIdentificadosApp(int id_Sede, int idMetodologia)
        {
            return metodologia.ObtenerPeligrosIdentificadosApp(id_Sede, idMetodologia);
        }

        public List<EDPeligroIdentificadoApp> ObtenerPeligrosIdentificadosFiltroApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar, string actividad)
        {
            return metodologia.ObtenerPeligrosIdentificadosFiltroApp(id_Sede, idMetodologia, id_Proceso, zonaLugar, actividad);
        }
        
        public List<EDValoracionDeRiesgosApp> ValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro)
        {
            return metodologia.ValoracionDeRiesgosApp(id_Sede,idMetodologia,idTipoPeligro);
        }

        public List<EDDetalleValoracionRiesgoApp> DetalleValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro, string intepretacionRiesgo)
        {
         return metodologia.DetalleValoracionDeRiesgosApp(id_Sede, idMetodologia, idTipoPeligro, intepretacionRiesgo);
        }

        public List<EDProceso> ProcesosMetodologiaApp(int id_Sede, int idMetodologia)
        {
            return metodologia.ProcesosMetodologiaApp(id_Sede, idMetodologia);
        }

        public List<EDZonaLugar> ZonLuagarMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso)
        {
            return metodologia.ZonLuagarMetodologiaApp(id_Sede, idMetodologia, id_Proceso);
        }

        public List<EDActividadApp> ActividadMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar)
        {
            return metodologia.ActividadMetodologiaApp(id_Sede, idMetodologia, id_Proceso, zonaLugar);
        }
        
        #endregion
    }
}
