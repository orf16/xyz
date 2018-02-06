
namespace SG_SST.Repositories.LiderazgoGerencial.IRepositories
{
    using SG_SST.Models.LiderazgoGerencial;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IPresupuestoRepositorio
    {

        /// <summary>
        /// Repositorio que me guarda el presupuesto, se realiza el guardado del presupuesto guardando las
        /// actividades del prespuesto
        /// </summary>
        /// <param name="actividadesPresupuesto">activivdades del presupuesto</param>
        /// <returns>retorna verdadero si fue exitoso el guardado del presupuesto o falso si no lo fue</returns>
        bool GuardarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto);

        /// <summary>
        /// Repositorio que elimina el presupuesto
        /// </summary>
        /// <param name="actividadesPresupuesto">activivdades del presupuesto</param>
        /// <returns>retorna verdadero si fue exitoso el eliminado del presupuesto o falso si no lo fue</returns>
        bool EliminarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto, int PK_Presupuesto);

        /// <summary>
        /// Respositorio que me retorna los presupuestos de una sede por un periodo determinado
        /// </summary>
        /// <param name="pk_Sede">Clave primaria de la sede</param>
        /// <param name="periodo">Año del presupuesto</param>
        /// <returns></returns>
        List<PresupuestoPorAnio> ObtenerPresupuestosSedePorAnio(int pk_Sede,int periodo);

        /// <summary>
        /// Repositorio que me retorna todas la actividades de un presupuesto
        /// </summary>
        /// <param name="PK_PresupuestoPorAnio">id o pk del presupuesto por año</param>
        /// <returns>LIsta de actividades del presupuesto</returns>
        List<ActividadPresupuesto> ObtenerActividadesPorPresupuesto(int PK_PresupuestoPorAnio);

        /// <summary>
        /// Repositorioa que retonar el presupuesto por año
        /// </summary>
        /// <param name="PK_PresupuestoPorAnio">id o pk del presupuesto por año</param>
        /// <returns></returns>
        PresupuestoPorAnio ObtenerPresupuestoPorAnio(int PK_PresupuestoPorAnio);

        /// <summary>
        /// Repositorio que obtiene los presupuesto por mes de una actividad
        /// </summary>
        /// <param name="pkActividad">Pk o id de la activdad</param>
        /// <returns>retorna una lista de presupuesto por mes</returns>
        List<PresupuestoPorMes> ObtenerPresupuestoPorMesActividad(int pkActividad);

        /// <summary>
        /// Repositorio que obtiene una actividad del presupuesto
        /// </summary>
        /// <param name="pkActividad">Pk o id de la activdad</param>
        /// <returns>retorna una actividad del presupuesto</returns>
        ActividadPresupuesto ObtenerActividadPresupuesto(int pkActividad);

        /// <summary>
        /// Repositorio que elimina una actividad del presupuesto
        /// </summary>
        /// <param name="pkActividad">Pk o id de la activdad</param>
        /// <returns>retorna verdadero si fue posible eliminar la actividad, false en caso contrario</returns>
        bool EliminarActividad(int pkActividad);

    }
}
