
namespace SG_SST.Repositories.LiderazgoGerencial.IRepositories
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Repositories.LiderazgoGerencial.IRepositories;
    //using SG_SST.Utilidades.Traza;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.Entity;
    interface IRolPorResponsabilidadRepositorio
    {

        /// <summary>
        /// Repositorio que me guarda rol y responsabilidades.
        /// </summary>
        /// <param name="actividadesPresupuesto">string rol</param>
        /// <returns>retorna verdadero si fue exitoso el guardado del rol y las responsabilidades o falso si no lo fue</returns>
        bool GuardarRolYResponsabilidades(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicion, int Pk_Id_Empresa);

        /// <summary>
        /// Repositorio que Edita el rol y las responsabilidades de cada rol.
        /// </summary>
        /// <param name="actividadesPresupuesto">string rol</param>
        /// <returns>retorna verdadero si fue exitoso la edicion del rol y las responsabilidades o falso si no lo fue</returns>
        bool EditarRolYResponsabilidades(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicionDeCuenta, int[] responsaEliminadas,
            int[] rendiciEliminadas, int Pk_Id_Empresa);

        /// <summary>
        /// Repositorio que me elimina el rol, las responsabilidades y la rendicion de cuentas.
        /// </summary>
        /// <param name="actividadesPresupuesto">int id</param>
        /// <returns>retorna verdadero si fue exitoso el eliminado del rol, las responsabilidades y la rendicion de cuentas o falso si no lo fue</returns>
        bool EliminarRolYResponsabilidades(int id);

        /// <summary>
        /// Repositorio que crea el rol y las responsabilidades preestablecidos por empresa.
        /// </summary>
        /// <param name=""></param>
        /// <returns>retorna verdadero si fue exitoso la creación del rol y las responsabilidades, o falso si no lo fue</returns>
        bool CrearRolYResponsabilidadesPreestablecidos(int id);

        /// <summary>
        /// Repositorio que me busca los roles por Empresa.
        /// </summary>
        /// <param name="actividadesPresupuesto">N/A</param>
        /// <returns>retorna una lista del roles por Empresa</returns>
        List<Rol> RolesPorEmpresa(int Pk_Id_Empresa);

        /// <summary>
        /// Repositorio que me busca las Obligaciones de las ARL.
        /// </summary>
        /// <param name="actividadesPresupuesto">N/A</param>
        /// <returns>retorna una lista de las obligaciones de las ARL</returns>
        List<ObligacionesArl> GetObligacionesArl();

        /// <summary>
        /// Repositorio que me busca las Obligaciones de los Empleadores.
        /// </summary>
        /// <param name="actividadesPresupuesto">N/A</param>
        /// <returns>retorna una lista de las obligaciones los Empleadores</returns>
        List<ObligacionesEmpleadores> GetObligacionesEmpleadores();
    }
}
