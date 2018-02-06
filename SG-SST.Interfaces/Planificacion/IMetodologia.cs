using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
     public interface IMetodologia
    {
        /// <summary>
        /// Definicion del metdodo para obtener la metodologias existentes
        /// </summary>
        /// <returns></returns>
        List<EDMetodologia> ObtenerMedologias();

        /// <summary>
        /// Definicion del metdodo para obtener la metodologias existentes  por cada sede 
        /// </summary>
        /// <returns></returns>
        List<EDMetodologia> ObtenerMedologias(int id_Sede);

        List<EDPeligroIdentificadoApp> ObtenerPeligrosIdentificadosApp(int id_Sede, int idMetodologia);

        List<EDPeligroIdentificadoApp> ObtenerPeligrosIdentificadosFiltroApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar, string actividad);

        List<EDValoracionDeRiesgosApp> ValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro);

        List<EDProceso> ProcesosMetodologiaApp(int id_Sede, int idMetodologia);

        List<EDZonaLugar> ZonLuagarMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso);

        List<EDActividadApp> ActividadMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar);

        List<EDDetalleValoracionRiesgoApp> DetalleValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro, string intepretacionRiesgo);
    }
}
