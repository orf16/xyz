using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SG_SST.Repositories.Organizacion.Repositories;
using System.Collections.Generic;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Empresas;

namespace SG_SST.LogicaTest.Organizacion
{
    [TestClass]
    public class CompetenciaRepositorioTest
    {
        private CompetenciaRepositorio competenciaRepositorio;
        [TestMethod]
        public void ObtenerRol_RolSessionExist_GetRol()
        {
            competenciaRepositorio = new CompetenciaRepositorio();
            int rol = 1086;
            int SessionEmp = 9;
            List<Rol> roles= competenciaRepositorio.ObtenerRol(rol,SessionEmp);
            Assert.IsNotNull(roles, "La petición falló, el objeto es nulo");
            Assert.IsTrue(roles.Count > 0, "La petición falló, la lista de estándares esta vacía");
        }
        [TestMethod]
        public void ObtenerRolPorTematicaPorRol_RolExist_GetRolPorTematica()
        {
            competenciaRepositorio = new CompetenciaRepositorio();
            int rol = 1080;
            List<RolPorTematica> rolPorTematica = competenciaRepositorio.ObtenerRolPorTematicaPorRol(rol);
            Assert.IsNotNull(rolPorTematica, "La petición falló, el objeto es nulo");
            Assert.IsTrue(rolPorTematica.Count > 0, "La petición falló, la lista de estándares esta vacía");
        }
         [TestMethod]
        public void ObtenerCargoPorRolPorRol_RolExist_GetCargoPorRol()
        {
            competenciaRepositorio = new CompetenciaRepositorio();
            int rol = 1080;
            List<CargoPorRol> cargoPorRol = competenciaRepositorio.ObtenerCargoPorRolPorRol(rol);
            Assert.IsNotNull(cargoPorRol, "La petición falló, el objeto es nulo");
            Assert.IsTrue(cargoPorRol.Count > 0, "La petición falló, la lista de estándares esta vacía");
        }

         [TestMethod]
         public void ObteneCargoPorRolPorCargo_CargoExist_GetCargoPorRol()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int cargo = 7;
             List<CargoPorRol> cargoPorRol = competenciaRepositorio.ObteneCargoPorRolPorCargo(cargo);
             Assert.IsNotNull(cargoPorRol, "La petición falló, el objeto es nulo");
             Assert.IsTrue(cargoPorRol.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }
         [TestMethod]
         public void ObtenerRolPorCargo_CargoExist_GetRol()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int cargo = 7;
             int SessionEmp=9;
             List<Rol> rol = competenciaRepositorio.ObtenerRolPorCargo(cargo, SessionEmp);
             Assert.IsNotNull(rol, "La petición falló, el objeto es nulo");
             Assert.IsTrue(rol.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerRolPorTematicaPorCargo_CargoExist_GetRolPorTematica()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int cargo = 7;
             List<RolPorTematica> rolPorTematica = competenciaRepositorio.ObtenerRolPorTematicaPorCargo(cargo);
             Assert.IsNotNull(rolPorTematica, "La petición falló, el objeto es nulo");
             Assert.IsTrue(rolPorTematica.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObteneCargoPorRolPorRolCargo_RolCargoExist_GetCargoPorRol()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int cargo = 7;
             int rol = 1080;
             List<CargoPorRol> cargoPorRol = competenciaRepositorio.ObteneCargoPorRolPorRolCargo(rol, cargo);
             Assert.IsNotNull(cargoPorRol, "La petición falló, el objeto es nulo");
             Assert.IsTrue(cargoPorRol.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerRolPorRolCargo_RolCargoExist_GetCargoPorRol()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int cargo = 7;
             int rol = 1080;
             List<Rol> roles = competenciaRepositorio.ObtenerRolPorRolCargo(rol, cargo);
             Assert.IsNotNull(roles, "La petición falló, el objeto es nulo");
             Assert.IsTrue(roles.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerRolPorTematicaPorRolCargo_RolCargoExist_GetRolPorTematica()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int cargo = 7;
             int rol = 1080;
             List<RolPorTematica> rolPorTematica = competenciaRepositorio.ObtenerRolPorTematicaPorRolCargo(rol, cargo);
             Assert.IsNotNull(rolPorTematica, "La petición falló, el objeto es nulo");
             Assert.IsTrue(rolPorTematica.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }
         [TestMethod]
         public void ObtenerRolPorTematicaPorTematica_IdTematicaExist_GetRolPorTematica()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int idbusqueda = 1;
             int SessionEmp=9;
             List<RolPorTematica> rolPorTematicaList = competenciaRepositorio.ObtenerRolPorTematicaPorTematica(idbusqueda, SessionEmp);
             Assert.IsNotNull(rolPorTematicaList, "La petición falló, el objeto es nulo");
             Assert.IsTrue(rolPorTematicaList.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerRolPorTematicaPorTematicaRol_IdTematicaRolExist_GetRolPorTematica()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int idbusqueda = 1;
             int rol = 1080;
             List<RolPorTematica> rolPorTematicaList = competenciaRepositorio.ObtenerRolPorTematicaPorTematicaRol(idbusqueda, rol);
             Assert.IsNotNull(rolPorTematicaList, "La petición falló, el objeto es nulo");
             Assert.IsTrue(rolPorTematicaList.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }


         [TestMethod]
         public void ObtenerRolPorTematicaPorTemaCargo_IdTematicaRolExist_GetRolPorTematica()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int idbusqueda = 1;
             int cargo = 7;
             int SessionEmp = 9;
             List<RolPorTematica> rolPorTematicaList = competenciaRepositorio.ObtenerRolPorTematicaPorTemaCargo(idbusqueda, cargo,SessionEmp);
             Assert.IsNotNull(rolPorTematicaList, "La petición falló, el objeto es nulo");
             Assert.IsTrue(rolPorTematicaList.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerRoles_RolExist_GetRoles()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int SessionEmp = 9;
             List<Rol> roles = competenciaRepositorio.ObtenerRoles(SessionEmp);
             Assert.IsNotNull(roles, "La petición falló, el objeto es nulo");
             Assert.IsTrue(roles.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerRolesLibres_RolExist_GetRoles()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int SessionEmp = 9;
             List<Rol> roles = competenciaRepositorio.ObtenerRolesLibres(SessionEmp);
             Assert.IsNotNull(roles, "La petición falló, el objeto es nulo");
             Assert.IsTrue(roles.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

        /*
         [TestMethod]
         public void ObtenerCargos_CargoExist_GetCargos()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             List<Cargo> cargos = competenciaRepositorio.ObtenerCargos() ;
             Assert.IsNotNull(cargos, "La petición falló, el objeto es nulo");
             Assert.IsTrue(cargos.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }*/

         [TestMethod]
         public void ObtenerCargosSeleccionados_RolCargoExist_GetIntCargos()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int rol = 1080;
             List<int> cargos = competenciaRepositorio.ObtenerCargosSeleccionados(rol);
             Assert.IsNotNull(cargos, "La petición falló, el objeto es nulo");
             Assert.IsTrue(cargos.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void GuardarTematicaE_Tematica_GetListTematicas()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             Tematica tematic=new Tematica();
             tematic.Tematicas = "Prueba unitaria";
             tematic.TipoTematica = 2;
             int SessionEmp=9;
             List<Tematica> tematicas = competenciaRepositorio.GuardarTematicaE(tematic, SessionEmp);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }
         [TestMethod]
         public void ObtenerTematicaPosipedia_TematicaExist_GetListTematicasPosipedia()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             List<Tematica> tematicas = competenciaRepositorio.ObtenerTematicaPosipedia();
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerTematicaPosipediaSeleccionadas_RolExist_GetSelectTematicasPosipedia()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int rol = 1080;
             List<Tematica> tematicas = competenciaRepositorio.ObtenerTematicaPosipediaSeleccionadas(rol);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }


         [TestMethod]
         public void ObtenerTematicaEmpresa_TematicaExist_GetListTematicasEmpresa()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int SessionEmp = 9;
             List<Tematica> tematicas = competenciaRepositorio.ObtenerTematicaEmpresa(SessionEmp);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerTematicaEmpresaSeleccionadas_RolExist_GetSelectTematicasEmpresa()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int rol = 1080;
             List<Tematica> tematicas = competenciaRepositorio.ObtenerTematicaEmpresaSeleccionadas(rol);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ObtenerTematica_TematicaExist_GetTematicasPosipediaContainsString()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             string Busqueda = "COPASST";
             List<Tematica> tematicas = competenciaRepositorio.ObtenerTematica(Busqueda);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }
         [TestMethod]
         public void ObtenerTematicaEmp_TematicaExist_GetTematicasEmpresaContainsString()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             string Busqueda = "documento";
             int SessionEmp = 9;
             List<Tematica> tematicas = competenciaRepositorio.ObtenerTematicaEmp(Busqueda, SessionEmp);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }
         [TestMethod]
         public void ObtenerCompetenciaPorTematica_CompetenciaExist_GetCompetenciaContainsString()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             string prefijo = "unidad";
             int SessionEmp = 9;
             List<Tematica> tematicas = competenciaRepositorio.ObtenerCompetenciaPorTematica(prefijo, SessionEmp);
             Assert.IsNotNull(tematicas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(tematicas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void EliminarCompetencia_RolExist_DeleteCompetencias()
         {
             competenciaRepositorio = new CompetenciaRepositorio();
             int rol = 1086;
             bool result = competenciaRepositorio.EliminarCompetencia(rol);
             Assert.IsTrue(result, "La petición falló, no se realizó la transacción");
         }






        






    }
}
