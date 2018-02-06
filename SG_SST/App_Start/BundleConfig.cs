using System.Web;
using System.Web.Optimization;

namespace SG_SST
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/stylesindex.css",
                "~/Content/css/animate.min.css",
                "~/Content/css/simplePagination.css",
                "~/Content/css/sweetalert.css",
                "~/Content/css/submenus.css",
                "~/Content/css/specialbuttons.css",
                "~/Content/Site.css",
                "~/Content/miga-de-pan.css",
                "~/fonts/font-awesome.min.css",
                "~/Content/bootstrap.min.css",
                "~/Content/jquery.fancybox.css",
                "~/Content/datepicker.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/jquery-ui.css",
                "~/Content/DataTables/css/dataTables.bootstrap.css",
                "~/Content/jquery.timepicker.css",
                "~/Content/jquery.fileupload.css",
                "~/Content/fullcalendar.css"));

         


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/ajax-loading.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/d3/d3.min.js",
                 "~/Scripts/load-image.all.min.js",
                "~/Scripts/jquery.fancybox.pack.js",
                "~/Scripts/Transversal/datepicker.js",
                "~/Scripts/Transversal/datepicker-es.js",
                "~/Scripts/Transversal/generales.js",
                "~/Scripts/jquery.base64.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/moment-with-locales.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                 "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.twbsPagination-1.3.1.js", 
                "~/Scripts/jquery.timepicker.js",
                "~/Scripts/jquery.fileupload.js",
                "~/Scripts/jquery.fileupload-ui.js",
                "~/Scripts/load-image.all.min.js",
               
                "~/Scripts/jquery.iframe-transport.js",
                "~/Scripts/fullcalendar.js",
                 "~/Scripts/jquery.multiselect.min.js",
                "~/Scripts/ui.dropdownchecklist.js",
                "~/Scripts/locale-all.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                //"~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));   
            //script simplePagination

            bundles.Add(new ScriptBundle("~/bundles/simplePagination").Include(
                "~/Scripts/flaviusmatis-simplePagination.js-e32c66e/jquery.simplePagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                //"~/js/jquery.min.js",
                "~/js/menumobile.js",
                "~/js/scripts.js",
                "~/js/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Planificacion").Include(
                "~/Scripts/Planificacion/planificacion.js",
                "~/Scripts/Planificacion/ValidacionesPlanificacion.js",
                "~/Scripts/Planificacion/DxSaludGeneral.js"));

            bundles.Add(new ScriptBundle("~/bundles/Empresas").Include(
                "~/Scripts/Empresas/empresa.js",
                "~/Scripts/Empresas/ValidacionCrearSede.js",
                "~/Scripts/Empresas/ValidarCrearUsuario.js",
                "~/Scripts/Empresas/ValidarCrearMision.js",
                "~/Scripts/Empresas/ValidarCrearVision.js",
                "~/Scripts/Empresas/ValidarConsultaSiarp.js"));

            bundles.Add(new ScriptBundle("~/bundles/Aplicacion").Include(
                "~/Scripts/Aplicacion/ValidarCrearMaestro.js",
                "~/Scripts/Aplicacion/ValidarCrearPlaneacion.js",
                "~/Scripts/Aplicacion/Aplicacion.js",
                "~/Scripts/Aplicacion/GrabarCondicion.js",
                "~/Scripts/Aplicacion/ValidarCargarManual.js",
                "~/Scripts/Aplicacion/GrabarPlan.js"));

            bundles.Add(new ScriptBundle("~/bundles/PlanEmpresa").Include(
                "~/Scripts/PlanEmpresa/PlanEmpresa.js",
                "~/Scripts/PlanEmpresa/Calendario.js"));

            bundles.Add(new ScriptBundle("~/bundles/Comunicaciones").Include(
               "~/Scripts/Comunicaciones/ComunicadosAPP.js",
               "~/Scripts/Comunicaciones/ComunicacionesInternas.js"));

            bundles.Add(new ScriptBundle("~/bundles/Incidentes").Include(
               "~/Scripts/Incidentes/IncidentesAT.js",
               "~/Scripts/Incidentes/IncidentesEL.js"));

            bundles.Add(new ScriptBundle("~/bundles/Politica").Include(
                "~/Scripts/Politica/Politica.js"));

            bundles.Add(new ScriptBundle("~/bundles/Utilidades").Include(
                "~/Scripts/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/UtilidadesExternas").Include(
                "~/Scripts/Base/Messages/notify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/LiderazgoGerencial").Include(
                "~/Scripts/LiderazgoGerencial/liderazgoGerencial.js",
                "~/Scripts/LiderazgoGerencial/ValidacionesLiderazgoGerencial.js",
                "~/Scripts/LiderazgoGerencial/ValidacionesCrearRol.js",
                "~/Scripts/jquery.numeric.js"));

            bundles.Add(new ScriptBundle("~/bundles/Organizacion").Include(
                "~/Scripts/Organizacion/ValidacionCrearRecursos.js",
                "~/Scripts/Organizacion/BuscarRecursosPorSede.js",
                "~/Scripts/Organizacion/Competencia.js",
                "~/Scripts/Organizacion/ValidacionCompetencia.js",
                "~/Scripts/Organizacion/Documentacion.js",
                "~/Scripts/Organizacion/ValidarCrearTipoRecurso.js",
                "~/Scripts/Organizacion/ValidarCrearFaseRecurso.js",
                "~/Scripts/Organizacion/ValidarBuscarRecursos.js"));

            bundles.Add(new ScriptBundle("~/bundles/Participacion").Include(
            "~/Scripts/Participacion/ReporteCondicionesInseguras.js",
            "~/Scripts/Participacion/Comites.js",
            "~/Scripts/Participacion/Consulta.js"));

           
            bundles.Add(new ScriptBundle("~/bundles/ChartEstadisticos").Include(
                "~/Scripts/Chart.js",
                "~/Scripts/Chart.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js", 
                "~/Scripts/DataTables/dataTables.tableTools.js",
                "~/Scripts/DataTables/dataTables.scroller.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/Revision").Include(
                "~/Scripts/Revision/Revision.js"));

            bundles.Add(new ScriptBundle("~/bundles/Reportes").Include(
                "~/Scripts/Reportes/Reportes.js"));

        }
    }
}
