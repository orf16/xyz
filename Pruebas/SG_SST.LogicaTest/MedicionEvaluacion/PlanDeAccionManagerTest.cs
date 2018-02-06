using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SG_SST.Repositorio.MedicionEvaluacion;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;
using System.Collections.Generic;

namespace SG_SST.LogicaTest.MedicionEvaluacion
{
    [TestClass]
    public class PlanDeAccionManagerTest
    {
        private PlanDeAccionManager planDeAccionManager= new PlanDeAccionManager();
        [TestMethod]
        public void consultarEstado_ListEDActividadPlanDeAccion_intEstado()
        {
            planDeAccionManager = new PlanDeAccionManager();
            List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
            EDActividadPlanDeAccion actividadPlanDeAccion= new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion=3;
            actividadPlanDeAccion.Fk_Plan_Inspección=1009;
            actividadPlanDeAccion.Fk_Id_Actividad=1;
            Actividades.Add(actividadPlanDeAccion);
            int estado = planDeAccionManager.consultarEstado(Actividades);
            Assert.AreNotEqual(-1, estado);

        }
        [TestMethod]
        public void ObtenerListaPlanDeAccion_Nit_ListEDPlanDeAccion()
        {
            planDeAccionManager = new PlanDeAccionManager();
            int nit = 800007813;
            List<EDPlanDeAccion> planDeAccion = planDeAccionManager.ObtenerListaPlanDeAccion(nit);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
            Assert.IsTrue(planDeAccion.Count > 0, "La petición falló, la lista de planes de acción esta vacía");

        }

        [TestMethod]
        public void ObtenerListaActividadEvaluacion_EmpresaPk_Id_ModuloPlanAccionNit_EDPlanDeAccion()
        {
            planDeAccionManager = new PlanDeAccionManager();
            int nit = 800007813;
            int Pk_Id_ModuloPlanAccion = 1;
            int empresa = 7;
            EDPlanDeAccion planDeAccion = planDeAccionManager.ObtenerListaActividadEvaluacion(empresa, Pk_Id_ModuloPlanAccion,nit);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
        }

        [TestMethod]
        public void ObtenerListaActividadAccion_EmpresaPk_Id_ModuloPlanAccionModulo_EDPlanDeAccion()
        {
            planDeAccionManager = new PlanDeAccionManager();
            int empresa = 9;
            int Pk_Id_ModuloPlanAccion = 2;
            string modulo = "Acciones correctivas y preventivas";
            List<EDPlanDeAccion> planDeAccion = planDeAccionManager.ObtenerListaActividadAccion(empresa, Pk_Id_ModuloPlanAccion, modulo);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
            Assert.IsTrue(planDeAccion.Count > 0, "La petición falló, la lista de planes de acción esta vacía");

        }
        [TestMethod]
        public void ObtenerListaActividadAuditoria_EmpresaPk_Id_ModuloPlanAccionModulo_EDPlanDeAccion()
        {
            planDeAccionManager = new PlanDeAccionManager();
            int empresa = 9;
            int Pk_Id_ModuloPlanAccion = 3;
            string modulo = "Auditoría";
            List<EDPlanDeAccion> planDeAccion = planDeAccionManager.ObtenerListaActividadAuditoria(empresa, Pk_Id_ModuloPlanAccion, modulo);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
            Assert.IsTrue(planDeAccion.Count > 0, "La petición falló, la lista de planes de acción esta vacía");

        }

        [TestMethod]
        public void GuardarPlanesDeAccion_EDActividadPlanDeAccion_EDActividadPlanDeAccionSave()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Fk_Plan_Inspección = 1012 ;
            actividadPlanDeAccion.Fk_Id_Actividad = 2005;
            actividadPlanDeAccion.FechaCierre= DateTime.Now;
            actividadPlanDeAccion.Observaciones="Prueba unitaria GuardarPlanesDeAccion";
            actividadPlanDeAccion = planDeAccionManager.GuardarPlanesDeAccion(actividadPlanDeAccion);
            Assert.IsNotNull(actividadPlanDeAccion,"La petición falló, el objeto es nulo");
        }

        [TestMethod]
        public void EliminarActividad_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Fk_Plan_Inspección = 1012;
            actividadPlanDeAccion.Fk_Id_Actividad = 2010;
            bool eliminado = planDeAccionManager.EliminarActividad(actividadPlanDeAccion);
            Assert.IsTrue(eliminado, "No se elimino la actividad del plan de acción");
        }

        [TestMethod]
        public void EliminarActividadEvaluacion_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 1;
            actividadPlanDeAccion.Fk_Plan_Inspección = 2451;
            actividadPlanDeAccion.Fk_Id_Actividad = 2447;
            bool eliminado = planDeAccionManager.EliminarActividadEvaluacion(actividadPlanDeAccion);
            Assert.IsTrue(eliminado, "No se elimino la actividad del plan de acción");
        }
        [TestMethod]
        public void EliminarActividadAccion_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 2;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Fk_Id_Actividad = 3;
            bool eliminado = planDeAccionManager.EliminarActividadAccion(actividadPlanDeAccion);
            Assert.IsTrue(eliminado, "No se elimino la actividad del plan de acción");
        }
        [TestMethod]
        public void EliminarActividadAuditoria_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 1012;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Fk_Id_Actividad = 2005;
            bool eliminado = planDeAccionManager.EliminarActividadAuditoria(actividadPlanDeAccion);
            Assert.IsTrue(eliminado, "No se elimino la actividad del plan de acción");
        }
        [TestMethod]
        public void EditarActividad_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 3210;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 1;
            actividadPlanDeAccion.Fk_Id_Actividad = 2450;
            actividadPlanDeAccion.Observaciones="Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Unit test";
            actividadPlanDeAccion.Responsable = "Samuel Villada";
            bool editado = planDeAccionManager.EditarActividad(actividadPlanDeAccion);
            Assert.IsTrue(editado, "No se editó la actividad del plan de acción");
        }
        [TestMethod]
        public void EditarActividadEvaluacion_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 3210;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 1;
            actividadPlanDeAccion.Fk_Id_Actividad = 2451;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Unit test";
            actividadPlanDeAccion.Responsable = "Simon Bedoya";
            bool editado = planDeAccionManager.EditarActividadEvaluacion(actividadPlanDeAccion);
            Assert.IsTrue(editado, "No se editó la actividad del plan de acción");
        }
        [TestMethod]
        public void EditarActividadAccion_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 2;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 2;
            actividadPlanDeAccion.Fk_Id_Actividad = 1;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Unit test";
            actividadPlanDeAccion.Responsable = "Simon Bedoya";
            bool editado = planDeAccionManager.EditarActividadAccion(actividadPlanDeAccion);
            Assert.IsTrue(editado, "No se editó la actividad del plan de acción");
        }
        [TestMethod]
        public void EditarActividadAuditoria_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 1012;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Fk_Id_Actividad = 2006;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Unit test";
            actividadPlanDeAccion.Responsable = "Simon Bedoya";
            bool editado = planDeAccionManager.EditarActividadAuditoria(actividadPlanDeAccion);
            Assert.IsTrue(editado, "No se editó la actividad del plan de acción");
        }
        [TestMethod]
        public void AdicionarActividad_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 1012;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.FechaFinalizacion = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Ingreso Unit test";
            actividadPlanDeAccion.Responsable = "Samuel Villada";
            bool adicionado = planDeAccionManager.AdicionarActividad(actividadPlanDeAccion);
            Assert.IsTrue(adicionado, "No se ingresó la actividad");
        }
        [TestMethod]
        public void AdicionarActividadEvaluacion_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 3210;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 1;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.FechaFinalizacion = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Ingreso Unit test";
            actividadPlanDeAccion.Responsable = "Samuel Villada";
            bool adicionado = planDeAccionManager.AdicionarActividadEvaluacion(actividadPlanDeAccion);
            Assert.IsTrue(adicionado, "No se ingresó la actividad");
        }
        [TestMethod]
        public void AdicionarActividadAccion_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 2;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 2;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.FechaFinalizacion = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Ingreso Unit test";
            actividadPlanDeAccion.Responsable = "Samuel Villada";
            bool adicionado = planDeAccionManager.AdicionarActividadAccion(actividadPlanDeAccion);
            Assert.IsTrue(adicionado, "No se ingresó la actividad");
        }
        [TestMethod]
        public void AdicionarActividadAuditoria_EDActividadPlanDeAccionExist_True()
        {
            planDeAccionManager = new PlanDeAccionManager();
            EDActividadPlanDeAccion actividadPlanDeAccion = new EDActividadPlanDeAccion();
            actividadPlanDeAccion.Fk_Plan_Inspección = 1012;
            actividadPlanDeAccion.Fk_Id_ModuloPlanAccion = 3;
            actividadPlanDeAccion.Observaciones = "Observaciones desde test unit";
            actividadPlanDeAccion.FechaCierre = DateTime.Now;
            actividadPlanDeAccion.FechaFinalizacion = DateTime.Now;
            actividadPlanDeAccion.Actividad = "Ingreso Unit test";
            actividadPlanDeAccion.Responsable = "Samuel Villada";
            bool adicionado = planDeAccionManager.AdicionarActividadAuditoria(actividadPlanDeAccion);
            Assert.IsTrue(adicionado, "No se ingresó la actividad");
        }
        [TestMethod]
        public void ConsultarListaPlanDeAccion_NitPk_Id_ModuloPlanAccionfechaInicialfechaFinal_ListEDPlanDeAccion()
        {
            int nit = 800007813;
            int Pk_Id_ModuloPlanAccion = 1;
            string fechaInicial = "01/01/2017";
            string fechaFinal = "01/12/2017";
            planDeAccionManager = new PlanDeAccionManager();
            List<EDPlanDeAccion>planDeAccion = planDeAccionManager.ConsultarListaPlanDeAccion(nit,Pk_Id_ModuloPlanAccion,fechaInicial,fechaFinal);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
            Assert.IsTrue(planDeAccion.Count > 0, "La petición falló, la lista de planes de acción esta vacía");
        }
        [TestMethod]
        public void ConsultarListaActividadEvaluacion_NitPk_Id_ModuloPlanAccionfechaInicialfechaFinal_ListEDPlanDeAccion()
        {
            int nit = 800007813;
            int Pk_Id_ModuloPlanAccion = 1;
            DateTime fechaInicial = DateTime.Parse("01/01/2017");
            DateTime fechaFinal =  DateTime.Parse("01/12/2017");
            int empresa = 7;
            planDeAccionManager = new PlanDeAccionManager();
            EDPlanDeAccion planDeAccion = planDeAccionManager.ConsultarListaActividadEvaluacion(empresa, Pk_Id_ModuloPlanAccion, nit, fechaInicial, fechaFinal);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
        }

        [TestMethod]
        public void ConsultarListaActividadAccion_NitPk_Id_ModuloPlanAccionfechaInicialfechaFinal_ListEDPlanDeAccion()
        {
            int Pk_Id_ModuloPlanAccion = 2;
            DateTime fechaInicial = DateTime.Parse("01/01/2017");
            DateTime fechaFinal = DateTime.Parse("01/12/2017");
            int empresa = 9;
            string modulo = "Acciones correctivas y preventivas";
            planDeAccionManager = new PlanDeAccionManager();
            List<EDPlanDeAccion> planDeAccion = planDeAccionManager.ConsultarListaActividadAccion(empresa, Pk_Id_ModuloPlanAccion, modulo, fechaInicial, fechaFinal);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
            Assert.IsTrue(planDeAccion.Count > 0, "La petición falló, la lista de planes de acción esta vacía");

        }

        [TestMethod]
        public void ConsultarListaActividadAuditoria_NitPk_Id_ModuloPlanAccionfechaInicialfechaFinal_ListEDPlanDeAccion()
        {
            int Pk_Id_ModuloPlanAccion = 3;
            DateTime fechaInicial = DateTime.Parse("01/01/2017");
            DateTime fechaFinal = DateTime.Parse("01/12/2017");
            int empresa = 9;
            string modulo = "Auditoría";
            planDeAccionManager = new PlanDeAccionManager();
            List<EDPlanDeAccion> planDeAccion = planDeAccionManager.ConsultarListaActividadAuditoria(empresa, Pk_Id_ModuloPlanAccion, modulo, fechaInicial, fechaFinal);
            Assert.IsNotNull(planDeAccion, "La petición falló, el objeto es nulo");
            Assert.IsTrue(planDeAccion.Count > 0, "La petición falló, la lista de planes de acción esta vacía");

        }
        [TestMethod]
        public void ObtenerModulos_NitPk_Id_ModuloPlanAccionfechaInicialfechaFinal_ListEDPlanDeAccion()
        {
            int nit = 800007813;
            planDeAccionManager = new PlanDeAccionManager();
            List<ModulosPlanAccion> ListaMod = planDeAccionManager.ObtenerModulos(nit);
            Assert.IsNotNull(ListaMod, "La petición falló, el objeto es nulo");
            Assert.IsTrue(ListaMod.Count > 0, "La petición falló, la lista de planes de acción esta vacía");

        }


    }
}
