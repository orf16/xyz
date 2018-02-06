using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Models;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Empresas;
using System.Web.Mvc;

namespace SG_SST.Repositories.Organizacion.IRepositories
{
    interface ICompetenciaRepositorio
    {
        /// <summary>
        /// Definicion del Metodo que me graba una fase 
        /// </summary>
        /// <param name="fase">Grabar Fase </param>
        /// <returns>retorna si fue exitosa o no el guardadola fase</returns>
        bool GrabarCompetencia(List<int> Fk_Id_Rol, List<int> Fk_Id_Cargo,
         string idEmpleados, List<Tematica> Id_TematicaPos, int editar, int SessionEmp);

        List<Rol> ObtenerRol(int rol,int SessionEmp);

        List<RolPorTematica> ObtenerRolPorTematicaPorRol(int rol);

        List<CargoPorRol> ObtenerCargoPorRolPorRol(int rol);

        List<CargoPorRol> ObteneCargoPorRolPorCargo(int cargo);

        List<Rol> ObtenerRolPorCargo(int cargo, int SessionEmp);

        List<RolPorTematica> ObtenerRolPorTematicaPorCargo(int cargo);

        List<CargoPorRol> ObteneCargoPorRolPorRolCargo(int rol, int cargo);

        List<Rol> ObtenerRolPorRolCargo(int rol, int cargo);

        List<RolPorTematica> ObtenerRolPorTematicaPorRolCargo(int rol, int cargo);

        List<RolPorTematica> ObtenerRolPorTematicaPorTematica(int idbusqueda,int SessionEmp);

        List<RolPorTematica> ObtenerRolPorTematicaPorTematicaRol(int idbusqueda, int rol);

        List<RolPorTematica> ObtenerRolPorTematicaPorTemaCargo(int idbusqueda, int cargo, int SessionEmp);

        List<Rol> ObtenerRoles(int SessionEmp);

        List<Cargo> ObtenerCargos(string NitEmpresa);

        List<Tematica> GuardarTematicaE(Tematica tematic, int SessionEmp);

        List<Tematica> ObtenerTematica(string Busqueda);

        List<Tematica> ObtenerTematicaEmp(string Busqueda, int SessionEmp);

        List<Tematica> ObtenerCompetenciaPorTematica(string prefijo, int SessionEmp);

        bool EliminarCompetencia(int rol);

        List<Rol> ObtenerRolesLibres(int SessionEmp);

        List<Tematica> ObtenerTematicaPosipedia();

        List<Tematica> ObtenerTematicaEmpresa(int SessionEmp);

        SelectList ObtenerRolesLibresEditar(int rol, int SessionEmp);

        List<Tematica> ObtenerTematicaEmpresaSeleccionadas(int rol);

        List<Tematica> ObtenerTematicaPosipediaSeleccionadas(int rol);

        List<int> ObtenerCargosSeleccionados(int rol);

        List<EmpleadoPorTematica> ObtenerEmpleadoPorRol(int rol);

    }
}