
namespace SG_SST.Services.LiderazgoGerencial.Iservices
{
    using SG_SST.Dtos.LiderazgoGerencial;
    using SG_SST.Models.LiderazgoGerencial;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IPresupuestoServicios
    {
        /// <summary>
        /// Servicio que me guarda el presupuesto, se realiza el guardado del presupuesto guardando las
        /// actividades del prespuesto
        /// </summary>
        /// <param name="actividadesPresupuesto">activivdades del presupuesto</param>
        /// <returns>retorna verdadero si fue exitoso el guardado del presupuesto o falso si no lo fue</returns>
        bool GuardarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto);

        /// <summary>
        /// Servicio que elimina el presupuesto
        /// </summary>
        /// <param name="actividadesPresupuesto">activivdades del presupuesto</param>
        /// <returns>retorna verdadero si fue exitoso el eliminado del presupuesto o falso si no lo fue</returns>
        bool EliminarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto, int PK_Presupuesto);

        /// <summary>
        /// Servicio que me retorna los presupuestos de una sede por un periodo determinado
        /// </summary>
        /// <param name="pk_Sede">Clave primaria de la sede</param>
        /// <param name="periodo">Año del presupuesto</param>
        /// <returns></returns>
        List<PresupuestoPorAnio> ObtenerPresupuestosSedePorAnio(int pk_Sede, int periodo);


        /// <summary>
        /// Servicio que me retorna todas la actividades de un presupuesto
        /// </summary>
        /// <param name="PK_PresupuestoPorAnio">id o pk del presupuesto por año</param>
        /// <returns>LIsta de actividades del presupuesto</returns>
        List<ActividadPresupuesto> ObtenerActividadesPorPresupuesto(int PK_PresupuestoPorAnio);


        /// <summary>
        /// Servicio que retonar el presupuesto por año
        /// </summary>
        /// <param name="PK_PresupuestoPorAnio">id o pk del presupuesto por año</param>
        /// <returns></returns>
        PresupuestoPorAnio ObtenerPresupuestoPorAnio(int PK_PresupuestoPorAnio);

        /// <summary>
        /// Servicio que me retorna una lista de actividades filtradas por fecha e intervalo de tiempo
        /// </summary>
        /// <param name="IDPresupuestoAnio">pk o ide del prespuesto del año</param>
        /// <param name="fecha">fecha que se divide en mensual, trimestral,semestral o anual</param>
        /// <param name="intervaloDeTiempo">el cual se dividi el tiempo</param>
        /// <returns></returns>
        List<ActividadPresupuesto> CrearInformePresupuesto(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo);

        /// <summary>
        /// Servicio que me retonar un dto con todas las actividades a exportar a excel
        /// </summary>
        /// <param name="IDPresupuestoAnio"></param>
        /// <param name="fecha"></param>
        /// <param name="intervaloDeTiempo"></param>
        /// <returns></returns>
        List<InformePresupuestoDTO> GenerarExcel(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo, string nombreIntervaloTiempo);

        /// <summary>
        /// Repositorio que elimina una actividad del presupuesto
        /// </summary>
        /// <param name="pkActividad">Pk o id de la activdad</param>
        /// <returns>retorna verdadero si fue posible eliminar la actividad, false en caso contrario</returns>
        bool EliminarActividad(int pkActividad);
    }
}
