using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SG_SST.Repositories.LiderazgoGerencial.Repositories;
using SG_SST.Logica.Planificacion;
using SG_SST.Models.LiderazgoGerencial;
using SG_SST.Models.Empresas;
using SG_SST.Models;
using System.Collections.Generic;
using System.Linq;

namespace SG_SST.LogicaTest
{
    [TestClass]
    public class RolPorResponsabilidadRepositorioTest
    {
        [TestMethod]
        public void GetObligacionesEmpleadoresTest()
        {
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            List<ObligacionesEmpleadores> list=logica.GetObligacionesEmpleadores();
            Assert.IsNotNull(list, "La petición falló, el objeto es nulo");
            Assert.IsTrue(list.Count > 0, "La petición falló, la lista de estándares esta vacía");
        }

        [TestMethod]
        public void GetObligacionesArlTest()
        {
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            List<ObligacionesArl> list = logica.GetObligacionesArl();
            Assert.IsNotNull(list, "La petición falló, el objeto es nulo");
            Assert.IsTrue(list.Count > 0, "La petición falló, la lista de estándares esta vacía");
        }

        [TestMethod]
        public void RolesPorEmpresaTest()
        {
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            List<Rol> list = logica.RolesPorEmpresa(1);
            Assert.IsNotNull(list, "La petición falló, el objeto es nulo");
            Assert.IsTrue(list.Count > 0, "La petición falló, la lista de estándares esta vacía");
        }

        [TestMethod]
        public void CrearRolYResponsabilidadesPreestablecidosTest()
        {
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            bool metodo = logica.CrearRolYResponsabilidadesPreestablecidos(10);
            Assert.IsNotNull(metodo, "La petición falló, el objeto es nulo");
            Assert.IsTrue(metodo, "La petición falló, no se crearon los datos");
        }

        [TestMethod]
        public void EliminarRolYResponsabilidadesTest()
        {
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            bool metodo = logica.EliminarRolYResponsabilidades(2102);
            Assert.IsNotNull(metodo, "La petición falló, el objeto es nulo");
            Assert.IsTrue(metodo, "La petición falló, no se borraron los datos");
        }

        [TestMethod]
        public void EditarRolYResponsabilidadesTest()
        {
            SG_SSTContext db = new SG_SSTContext();
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            Rol rol= new Rol();
            rol.Pk_Id_Rol = 20;
            rol.Descripcion = "TRABAJADORES";
            List<Responsabilidades>responsabilidad  = new List<Responsabilidades>();
            Responsabilidades Respon= new Responsabilidades();
            Respon.Descripcion="Prueba responsabilidad";
            responsabilidad.Add(Respon);
            List<RendicionDeCuentas> rendicionDeCuenta=new  List<RendicionDeCuentas>();
            RendicionDeCuentas rendicion= new RendicionDeCuentas() ;
            rendicion.Descripcion="Prueba rendicion";
            rendicionDeCuenta.Add(rendicion);
            int[] responsaEliminadas=null;
            int[] rendiciEliminadas=null;
            int Pk_Id_Empresa=1;
            bool metodo = logica.EditarRolYResponsabilidades(rol,responsabilidad, rendicionDeCuenta,responsaEliminadas,
            rendiciEliminadas,Pk_Id_Empresa);
            Assert.IsNotNull(metodo, "La petición falló, el objeto es nulo");
            Assert.IsTrue(metodo, "La petición falló, no se editaron los datos");
        }

         [TestMethod]
        public void GuardarRolYResponsabilidadesTest()
        {
            SG_SSTContext db = new SG_SSTContext();
            RolPorResponsabilidadRepositorio logica = new RolPorResponsabilidadRepositorio();
            Rol rol = new Rol();
            rol.Pk_Id_Rol = 20;
            rol.Descripcion = "TRABAJADORES";
            List<Responsabilidades> responsabilidad = new List<Responsabilidades>();
            Responsabilidades Respon = new Responsabilidades();
            Respon.Descripcion = "Prueba responsabilidad dos";
            responsabilidad.Add(Respon);
            List<RendicionDeCuentas> rendicionDeCuenta = new List<RendicionDeCuentas>();
            RendicionDeCuentas rendicion = new RendicionDeCuentas();
            rendicion.Descripcion = "Prueba rendicion dos";
            rendicionDeCuenta.Add(rendicion);
            int Pk_Id_Empresa = 1;
            bool metodo = logica.GuardarRolYResponsabilidades(rol, responsabilidad, rendicionDeCuenta, Pk_Id_Empresa);
            Assert.IsNotNull(metodo, "La petición falló, el objeto es nulo");
            Assert.IsTrue(metodo, "La petición falló, no se editaron los datos");
        }


         [TestMethod]
         public void ConectividadTbl_ResponsabilidadesTest()
         {
             SG_SSTContext db = new SG_SSTContext();
             List<Responsabilidades> Responsabilidades = db.Tbl_Responsabilidades.ToList();
             Assert.IsNotNull(Responsabilidades, "La petición falló, el objeto es nulo");
             Assert.IsTrue(Responsabilidades.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ConectividadTbl_Responsabilidades_Por_RolTest()
         {
             SG_SSTContext db = new SG_SSTContext();
             List<ResponsabilidadesPorRol> ResponsabilidadesPorRol = db.Tbl_Responsabilidades_Por_Rol.ToList();
             Assert.IsNotNull(ResponsabilidadesPorRol, "La petición falló, el objeto es nulo");
             Assert.IsTrue(ResponsabilidadesPorRol.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }


         [TestMethod]
         public void ConectividadTbl_RendicionDeCuentasTest()
         {
             SG_SSTContext db = new SG_SSTContext();
             List<RendicionDeCuentas> RendicionDeCuentas = db.Tbl_RendicionDeCuentas.ToList();
             Assert.IsNotNull(RendicionDeCuentas, "La petición falló, el objeto es nulo");
             Assert.IsTrue(RendicionDeCuentas.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }


         [TestMethod]
         public void ConectividadTbl_Rendicion_Cuenta_Por_RolTest()
         {
             SG_SSTContext db = new SG_SSTContext();
             List<RendicionDeCuentasPorRol> RendicionDeCuentasPorRol = db.Tbl_Rendicion_Cuenta_Por_Rol.ToList();
             Assert.IsNotNull(RendicionDeCuentasPorRol, "La petición falló, el objeto es nulo");
             Assert.IsTrue(RendicionDeCuentasPorRol.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ConectividadTbl_Obligaciones_Empleadores()
         {
             SG_SSTContext db = new SG_SSTContext();
             List<ObligacionesEmpleadores> ObligacionesEmpleadores = db.Tbl_Obligaciones_Empleadores.ToList();
             Assert.IsNotNull(ObligacionesEmpleadores, "La petición falló, el objeto es nulo");
             Assert.IsTrue(ObligacionesEmpleadores.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }

         [TestMethod]
         public void ConectividadTbl_Obligaciones_Arl()
         {
             SG_SSTContext db = new SG_SSTContext();
             List<ObligacionesArl> ObligacionesArl = db.Tbl_Obligaciones_Arl.ToList();
             Assert.IsNotNull(ObligacionesArl, "La petición falló, el objeto es nulo");
             Assert.IsTrue(ObligacionesArl.Count > 0, "La petición falló, la lista de estándares esta vacía");
         }
          
          
          
          
       



    }
}
