using SG_SST.EntidadesDominio.Participacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Participacion
{
    public interface IReporte
    {
        /// <summary>
        /// Repositorio que guarda el reporte
        /// </summary>
        /// <param name="Reporte">entidad de dominio con la informacion del reporte</param>
        /// <returns>me retorna la entidad de dominio con id o pk del reporte si fue exitoso el guardado
        /// de lo contrario retorna la entidad de dominio sin id o pk </returns>
        EDReporte GuardarReporte(EDReporte Reporte);

        /// <summary>
        /// Repositorio que me retorna el historico de los reportes de las sedes
        /// </summary>
        /// <param name="idEmpresa">pk o id de la empresa</param>
        /// <returns>Lista de reportes </returns>
        List<EDReporte> ObtenerReportesPorEmpresa(string nit);


        EDReporte GuardarReporteEditado(EDReporte Reporte);

        List<EDReporte> ObtenerReporteCondicionesInsegurasPorID(int id);

        List<EDImagenesReportes> ObtenerImagenesCondicionesInsegurasPorID(int id);

        List<EDActividadesActosInseguros> ObtenerActividadesCondicionesInsegurasPorID(int id);

        List<EDReporte> ObteneReportesPorBusqueda(EDReporte reporte);

        List<EDActividadesActosInseguros> ObtenerActividadesPorBusqueda(EDReporte reporte);

        //eliminar imagen

        bool ELiminarImagenReporte(int idImagen);

        EDImagenesReportes ObtenerImagen(int idImagen);
        //app

       
        List<EDTipoReporte> ConsultarTipoReporte();

        //EDReporteApp GuardarReporteAPP(EDReporteApp reporte);

        EDReporteApp GuardarReporteAPP(EDReporteApp reporte); 
    }
}
