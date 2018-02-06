using Microsoft.VisualStudio.TestTools.UnitTesting;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Planificacion;
//using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.LogicaTest
{

    [TestClass]
    public class PlanificacionTest
    {
        [TestMethod]
        public void GuardarEvaluacionEstandaresLogicaTest()
        {
            EDEvaluacionEstandarMinimo eval = new EDEvaluacionEstandarMinimo
            {
                IdCriterio = 2,
                IdEmpresaEvaluar = 3,
                IdValoracionCriterio = 2,
                IdCiclo = 1,
                Justificacion = "No tiene",
                Actividades = CrearActividades()
            };

            LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
            EDCiclo ciclo = logica.GuardarEvaluacionEstandar(eval);
            Assert.IsNotNull(ciclo, "La peticion fallo, el objeto es nulo");
            Assert.IsTrue(ciclo.Id_Ciclo > 0, "La peticion fallo, la lista de estandares esta vacia");
        }

        private List<EDActividad> CrearActividades()
        {
            List<EDActividad> Actividades = new List<EDActividad>();
            Actividades.Add(
                new EDActividad
                {
                    Descripcion = "Tiene que hacer el tabajo bien para que las coscas salga bien debe hacerlo bien y si no mejor no lo haga",
                    Responsable = "Wilson Pedraza",
                    FechaFin = DateTime.Now
                });

            Actividades.Add(
               new EDActividad
               {
                   Descripcion = "Tambien Tiene que hacer el tabajo bien es mejor que todo salba bien escribo cualquier maricada para probar el ancheo de una celda cuando exporte el archivo a excel",
                   Responsable = "Hector Victoria",
                   FechaFin = DateTime.Now
               });

            return Actividades;
        }

        [TestMethod]
        public void GenerarExcelPlanAccionLogicaTest()
        {            
            LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
            byte [] archivo = logica.ObtenerActividadesExcel("3");
            Assert.IsNotNull(archivo, "La peticion fallo, el objeto es nulo");
            Assert.IsTrue(archivo.Length  > 0, "La peticion fallo, la lista de estandares esta vacia");
        }

        [TestMethod]
        public void GenerarExcelCalificacionEstandresPorClicloLogicaTest()
        {
            LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
            byte[] archivo = logica.ObtenerExcelCalificacionEstandresPorCliclo("860123006", 2);
            Assert.IsNotNull(archivo, "La peticion fallo, el objeto es nulo");
            Assert.IsTrue(archivo.Length > 0, "La peticion fallo, la lista de estandares esta vacia");
        }

        [TestMethod]
        public void GenerarExcelPorcentajeDeRespuestasLogicaTest()
        {
            LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
            byte[] archivo = logica.ObtenerExcelPorcentajeDeRespuestas("860123006");
            Assert.IsNotNull(archivo, "La peticion fallo, el objeto es nulo");
            Assert.IsTrue(archivo.Length > 0, "La peticion fallo, la lista de estandares esta vacia");
        }

        [TestMethod]
        public void GenerarExcelPorcentajeObtenidoLogicaTest()
        {
            LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
            byte[] archivo = logica.ObtenerExcelPorcentajeObtenido("860123006");
            Assert.IsNotNull(archivo, "La peticion fallo, el objeto es nulo");
            Assert.IsTrue(archivo.Length > 0, "La peticion fallo, la lista de estandares esta vacia");
        }

        [TestMethod]
        public void ObtenerCiclosServiceTest()
        {
            //string urlService = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
            //string CapacidadEvaluacionEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadEvaluacionEstandaresMinimos"];

            //ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "123456");
            //var resul = ServiceClient.ObtenerArrayJsonRestFul<EDCiclo>(urlService, CapacidadEvaluacionEstandaresMinimos, RestSharp.Method.GET);

            //LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
            //byte[] archivo = logica.ObtenerExcelPorcentajeObtenido("860123006");
            //Assert.IsNotNull(archivo, "La peticion fallo, el objeto es nulo");
            //Assert.IsTrue(archivo.Length > 0, "La peticion fallo, la lista de estandares esta vacia");
        }

    }
}
