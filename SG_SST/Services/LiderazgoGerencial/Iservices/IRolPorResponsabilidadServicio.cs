
namespace SG_SST.Services.LiderazgoGerencial.Iservices
{
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IRolPorResponsabilidadServicio
    {

        /// <summary>
        /// Servicio que me guarda el rol y las responsabilidades de cada rol.
        /// </summary>
        /// <param name="actividadesPresupuesto">string rol</param>
        /// <returns>retorna verdadero si fue exitoso el guardado del rol y las responsabilidades o falso si no lo fue</returns>
        bool GuardarRolYResponsabilidades(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicion, int Pk_Id_Empresa);

        /// <summary>
        /// Servicio que Edita el rol y las responsabilidades de cada rol.
        /// </summary>
        /// <param name="actividadesPresupuesto">string rol</param>
        /// <returns>retorna verdadero si fue exitoso la edicion del rol y las responsabilidades o falso si no lo fue</returns>
        bool EditarRolYResponsabilidades(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicionDeCuenta, int[] responsaEliminadas, int[] rendiciEliminadas, int Pk_Id_Empresa);
        
        /// <summary>
        /// Servicio que me elimina el rol y las responsabilidades de cada rol.
        /// </summary>
        /// <param name="actividadesPresupuesto">int id</param>
        /// <returns>retorna verdadero si fue exitoso el eliminado del rol y las responsabilidades o falso si no lo fue</returns>
        bool EliminarRolYResponsabilidades(int id);

        /// <summary>
        /// Servicio que crea el rol y las responsabilidades preestablecidos por empresa.
        /// </summary>
        /// <param name=""></param>
        /// <returns>retorna verdadero si fue exitoso la creación del rol y las responsabilidades o falso si no lo fue</returns>
        bool CrearRolYResponsabilidadesPreestablecidos(int id);

        /// <summary>
        /// servicio que me busca los roles por Empresa.
        /// </summary>
        /// <param name="actividadesPresupuesto">N/A</param>
        /// <returns>retorna una lista del roles por Empresa</returns>
        List<Rol> RolesPorEmpresa(int Pk_Id_Empresa);
        
        /// <summary>
        /// servicio que me busca las Obligaciones de las ARL.
        /// </summary>
        /// <param name="actividadesPresupuesto">N/A</param>
        /// <returns>retorna una lista de las obligaciones de las ARL</returns>
        List<ObligacionesArl> GetObligacionesArl();

        /// <summary>
        /// servicio que me busca las Obligaciones de los Empleadores.
        /// </summary>
        /// <param name="actividadesPresupuesto">N/A</param>
        /// <returns>retorna una lista de las obligaciones los Empleadores</returns>
        List<ObligacionesEmpleadores> GetObligacionesEmpleadores();
    }
}
