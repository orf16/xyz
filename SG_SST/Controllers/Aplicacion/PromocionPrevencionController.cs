using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Aplicacion;
using SG_SST.Logica.Empresas;
using SG_SST.Models.Aplicacion;
using SG_SST.Repositories.Empresas.IRepositories;
using SG_SST.Repositories.Empresas.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Aplicacion
{
    public class PromocionPrevencionController : BaseController
    {
        LNPromocionPrevencion LNAcciones = new LNPromocionPrevencion();
        LNProcesos LNProcesos = new LNProcesos();
        LNPromocionPrevencion LNPromocionPrevencion = new LNPromocionPrevencion();
        private ISedeRepositorio sedeRepositorio = new SedeRepositorio();

        [HttpGet]
        public ActionResult PlanSeguridadVial()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.RazonSocial = usuarioActual.RazonSocialEmpresa;
            ViewBag.Nit = usuarioActual.NitEmpresa;
            var ListaSedes = new List<EDSede>();
            ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede",0);
            //ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", 0);
            List<EDPlanVial> PlanVial = LNPromocionPrevencion.ConsultarPlanesVial(ListaSedes);
            foreach (var item in PlanVial)
            {
                bool Estado = LNPromocionPrevencion.VerificarEstado(item.Pk_Id_SegVial);
                item.Estado = Estado;
            }
            return View(PlanVial);
        }
        [HttpPost]
        public ActionResult CrearPlan(EDPlanVial EDPlanVial)
        {
            bool Probar = false;
            string Estado = "Por favor seleccione la sede";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar });
            }
            int Pk_Id_Sede = EDPlanVial.Fk_Id_Sede;
            if (Pk_Id_Sede != 0)
            {
                List<EDSegVialParametro> PlanVial = LNPromocionPrevencion.ConsultarParametros(usuarioActual.IdEmpresa);
                decimal suma = 0;
                foreach (var item in PlanVial)
                {
                    suma += item.Valor_Parametro;
                }
                suma = Math.Round(suma, 1);
                bool cumpleagregado = false;
                if (PlanVial.Count>0)
                {
                    if (suma==100)
                    {
                        cumpleagregado = true;
                    }
                }
                else
                {
                    if (suma == 0)
                    {
                        cumpleagregado = true;
                    }
                }

                if (cumpleagregado)
                {
                    bool ProbarGuardado = LNPromocionPrevencion.CrearPlan(Pk_Id_Sede, usuarioActual.IdEmpresa, PlanVial);
                    if (ProbarGuardado)
                    {
                        Probar = true;
                        return Json(new { url = Url.Action("PlanSeguridadVial", "PromocionPrevencion"), Estado, Probar });
                    }
                }
                else
                {
                    Estado = "No se puede crear una evaluación del plan estratégico de seguridad vial, no ha completado el registro del pilar VALORES AGREGADOS O INNOVACIONES";
                }
            }
            else
            {
                Estado = "No ha seleccionado una sede, por favor seleccione la sede y vuelva a intentar";
                return Json(new { Estado, Probar });
            }
            return Json(new { Estado, Probar });
        }
        [HttpPost]
        public ActionResult IrFormularioSegVial(string IdSegVial)
        {
            bool Probar = false;
            string Estado = "Por favor seleccione la sede";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar });
            }

            return View();
        }
        [HttpGet]
        public ActionResult SeguridadVialFormulario(string IdSegVial)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.finalizado = true;


            ViewBag.RazonS = "";
            ViewBag.Sede = "";
            ViewBag.FechaRegistro = "";
            ViewBag.HeadFix = "";

            string HeaderFixed = "";

            List<int> ContadorFilasVariables = new List<int>();
            List<int> IdVariables = new List<int>();
            List<string> ListaTextoVariables = new List<string>();

            ViewBag.ListaVartexto = ListaTextoVariables;
            ViewBag.ListaIdVar = IdVariables;
            ViewBag.ListaContVar = ContadorFilasVariables;

            var ListaSedes = new List<EDSede>();
            ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);
            List<EDSegVialResultado> ListaResultados = new List<EDSegVialResultado>();
            List<EDSegVialPilar> ListaPilares = new List<EDSegVialPilar>();
            List<EDSegVialParametro> ListaParametros = new List<EDSegVialParametro>();
            EDSegVialFormulario Formulario = new EDSegVialFormulario();
            int IdIntSegVial = 0;
            bool SinPilar = true;
            if (int.TryParse(IdSegVial, out IdIntSegVial))
            {
                ListaResultados = LNPromocionPrevencion.ConsultarPlanVialResultado(IdIntSegVial, ListaSedes, usuarioActual.IdEmpresa);
                ViewBag.RazonS = usuarioActual.RazonSocialEmpresa;

                List<EDSegVialPilar> ListaPilaresConsulta = new List<EDSegVialPilar>();
                List<EDSegVialParametro> ListaParametroConsulta = new List<EDSegVialParametro>();
                ListaPilaresConsulta = LNPromocionPrevencion.ConsultarPlanVialPilares(IdIntSegVial, ListaSedes);
                ListaPilares = ListaPilaresConsulta.Distinct().ToList();
                foreach (var item in ListaResultados)
                {
                    ListaParametros.Add(item.Parametro);
                    ViewBag.Sede = ListaSedes.Where(s => s.IdSede == item.PlanVial.Fk_Id_Sede).FirstOrDefault().NombreSede;
                    if (item.PlanVial.Fecha_Registro != null)
                    {
                        DateTime FechaR = item.PlanVial.Fecha_Registro;
                        ViewBag.FechaRegistro = FechaR.ToString("dd/MM/yyyy");
                    }

                }
                var ListaParametroConsulta1 = ListaParametros.Distinct();
                //Listado general del formulario
                Formulario.ListaResultados = ListaResultados;
                //Lista De pilares para crear tabs
                Formulario.ListaPilares = ListaPilares;
                //Pilar En el que esta actualmente el usuario

                foreach (var item in ListaPilaresConsulta)
                {
                    int NumeroPilar1 = item.Pk_Id_SegVialPilar;
                    if (HeaderFixed == "")
                    {
                        HeaderFixed = HeaderFixed + NumeroPilar1.ToString();
                    }
                    else
                    {
                        HeaderFixed = HeaderFixed + "," + NumeroPilar1.ToString();
                    }
                }
                foreach (var item in ListaPilaresConsulta)
                {
                    int NumeroPilar1 = item.Pk_Id_SegVialPilar;


                    List<EDSegVialResultado> ListaParam = ListaResultados.Where(s => s.Parametro.Fk_Id_SegVialPilar == NumeroPilar1).ToList();
                    bool Cumple = false;
                    foreach (var item1 in ListaParam)
                    {
                        if (item1.Aplica_s == 0 && item1.Existencia_s == 0 && item1.Responde_s == 0)
                        {
                            Formulario.PilarActual = item1.Pilar.Pk_Id_SegVialPilar;
                            Cumple = true;
                            ViewBag.finalizado = false;
                            SinPilar = false;
                            break;
                        }
                    }
                    if (Cumple)
                    {
                        break;
                    }
                }
                if (SinPilar)
                {
                    Formulario.PilarActual = 3000;
                }

                Formulario.IdEvaluacion = IdIntSegVial;

                #region CalculoValoresObtenidos
                foreach (var item in ListaResultados)
                {
                    if (item.Aplica_s == 1 && item.Existencia_s == 1 && item.Responde_s == 1)
                    {
                        decimal valorParametro = item.Parametro.Valor_Parametro;
                        List<EDSegVialResultado> numeroVariables = ListaResultados.Where(s => s.Parametro.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).ToList();
                        int NumVar = numeroVariables.Count();
                        decimal valorVariable = valorParametro / NumVar;

                        if (item.Aplica == true && item.Existencia == true && item.Responde == true)
                        {
                            item.ValorObtenido = valorVariable;
                            EDSegVialParametro EDSegVialParametro = ListaParametroConsulta1.Where(s => s.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).FirstOrDefault();
                            EDSegVialParametro.Valor_obtenido = EDSegVialParametro.Valor_obtenido + valorVariable;
                            EDSegVialParametro.Mostrar = true;

                            EDSegVialPilar EDSegVialPilar = ListaPilares.Where(s => s.Pk_Id_SegVialPilar == item.Pilar.Pk_Id_SegVialPilar).FirstOrDefault();
                            EDSegVialPilar.ValorObtenido = EDSegVialPilar.ValorObtenido + valorVariable;
                            EDSegVialPilar.ValorResultado = EDSegVialPilar.ValorObtenido * EDSegVialPilar.Valor_Ponderado;
                            EDSegVialPilar.Mostrar = true;
                        }
                        else
                        {
                            EDSegVialParametro EDSegVialParametro = ListaParametroConsulta1.Where(s => s.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).FirstOrDefault();
                            EDSegVialParametro.Mostrar = true;

                            EDSegVialPilar EDSegVialPilar = ListaPilares.Where(s => s.Pk_Id_SegVialPilar == item.Pilar.Pk_Id_SegVialPilar).FirstOrDefault();
                            EDSegVialPilar.Mostrar = true;
                        }
                    }
                }


                #endregion

                List<string> ListaVariables = new List<string>();


                List<EDSegVialResultado> ListaResultadosVar = new List<EDSegVialResultado>();
                int contRow = 0;
                foreach (var item1 in Formulario.ListaResultados)
                {
                    EDSegVialResultado EDSegVialResultado = new EDSegVialResultado();
                    EDSegVialDetalle EDSegVialDetalle = new EDSegVialDetalle();
                    EDSegVialResultado.DetalleParametro = EDSegVialDetalle;
                    EDSegVialResultado.DetalleParametro.CriterioAval = item1.DetalleParametro.CriterioAval;
                    EDSegVialResultado.DetalleParametro.Fk_Id_SegVialPilar = item1.DetalleParametro.Fk_Id_SegVialPilar; ;
                    EDSegVialResultado.DetalleParametro.Pk_Id_SegVialParametroDetalle = item1.DetalleParametro.Pk_Id_SegVialParametroDetalle;
                    EDSegVialResultado.DetalleParametro.VariableDesc = item1.DetalleParametro.VariableDesc;
                    ListaResultadosVar.Add(EDSegVialResultado);
                }
                foreach (var item1 in ListaResultadosVar)
                {
                    ListaVariables.Add(item1.DetalleParametro.VariableDesc);
                    item1.DetalleParametro.Pk_Id_SegVialParametroDetalle = contRow;
                    contRow++;
                }
                var ListaVariables1 = ListaVariables.Distinct();
                foreach (var item1 in ListaVariables1)
                {
                    ListaTextoVariables.Add(ListaResultadosVar.Where(s => s.DetalleParametro.VariableDesc == item1).First().DetalleParametro.VariableDesc);
                    int Primero = ListaResultadosVar.Where(s => s.DetalleParametro.VariableDesc == item1).First().DetalleParametro.Pk_Id_SegVialParametroDetalle;
                    int Ultimo = ListaResultadosVar.Where(s => s.DetalleParametro.VariableDesc == item1).Last().DetalleParametro.Pk_Id_SegVialParametroDetalle;
                    int Diferencia = Ultimo - Primero + 1;
                    ContadorFilasVariables.Add(Diferencia);
                    IdVariables.Add(Formulario.ListaResultados.Where(s => s.DetalleParametro.VariableDesc == item1).First().DetalleParametro.Pk_Id_SegVialParametroDetalle);
                }
                ViewBag.ListaVartexto = ListaTextoVariables;
                ViewBag.ListaIdVar = IdVariables;
                ViewBag.ListaContVar = ContadorFilasVariables;
                ViewBag.HeadFix = HeaderFixed;
            }
            return View(Formulario);
        }
        [HttpPost]
        public ActionResult GuardarParcialFormulario(SegVialResultado Control, List<SegVialResultado> ListaEvaluacion)
        {
            string Estado = "No se pudo guardar el plan de evaluacion";
            bool Probar = false;

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar });
            }

            if (ModelState.IsValid)
            {
                Probar = LNPromocionPrevencion.GuardarEvaluacion(ListaEvaluacion);
                if (Probar)
                {
                    Estado = "Plan del evaluacion guardado";
                }
            }
            return Json(new { Estado, Probar });
        }
        [HttpPost]
        public ActionResult GuardarControlFormulario(SegVialResultado Control, List<SegVialResultado> ListaEvaluacion)
        {

            string Estado = "No se pudo guardar el plan de evaluacion";
            bool Probar = false;
            int PilarActual = Control.Pk_Id_SegVialResultado;
            string NombrePilar = "";
            int IdIntSegVial = Control.Fk_Id_PlanVial;
            int NumErrores = 0;
            List<string> ListaErrores = new List<string>();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, NumErrores, ListaErrores, IdIntSegVial });
            }

            var ListaSedes = new List<EDSede>();
            ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);

            List<EDSegVialResultado> ListaResultados = new List<EDSegVialResultado>();

            if (ModelState.IsValid)
            {
                Probar = LNPromocionPrevencion.GuardarEvaluacion(ListaEvaluacion);
                if (Probar)
                {
                    ListaResultados = LNPromocionPrevencion.ConsultarPlanVialResultado(IdIntSegVial, ListaSedes, usuarioActual.IdEmpresa);
                    #region CalculoValoresObtenidos
                    foreach (var item in ListaResultados)
                    {

                        if (item.Pilar.Pk_Id_SegVialPilar == PilarActual)
                        {
                            if (item.Aplica_s == 0 || item.Existencia_s == 0 || item.Responde_s == 0)
                            {
                                NumErrores++;
                                NombrePilar = item.Pilar.Descripcion;
                                ListaErrores.Add(item.Pk_Id_SegVialResultado.ToString());
                            }
                        }
                    }
                    #endregion
                    if (NumErrores == 0)
                    {
                        Estado = "Plan del evaluacion guardado";
                    }
                    else
                    {
                        Estado = "Existen variables del formulario sin contestar del pilar " + NombrePilar + ", por favor revise y diligencie el formulario";
                    }

                }
            }

            return Json(new { Estado, Probar, NumErrores, ListaErrores, IdIntSegVial });
        }

        [HttpPost]
        public ActionResult AsignacionExcel(string IdSegVial)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.finalizado = true;
            ViewBag.RazonS = "";
            ViewBag.Sede = "";
            ViewBag.FechaRegistro = "";

            string RazonSocial = usuarioActual.RazonSocialEmpresa;
            string Sede = "";
            string FechaRegistro = "";
            string Nit = usuarioActual.NitEmpresa;

            var ListaSedes = new List<EDSede>();
            ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);
            List<EDSegVialResultado> ListaResultados = new List<EDSegVialResultado>();
            List<EDSegVialPilar> ListaPilares = new List<EDSegVialPilar>();
            List<EDSegVialParametro> ListaParametros = new List<EDSegVialParametro>();
            EDSegVialFormulario Formulario = new EDSegVialFormulario();
            int IdIntSegVial = 0;
            bool SinPilar = true;
            if (int.TryParse(IdSegVial, out IdIntSegVial))
            {
                ListaResultados = LNPromocionPrevencion.ConsultarPlanVialResultado(IdIntSegVial, ListaSedes, usuarioActual.IdEmpresa);
                ViewBag.RazonS = usuarioActual.RazonSocialEmpresa;

                List<EDSegVialPilar> ListaPilaresConsulta = new List<EDSegVialPilar>();
                List<EDSegVialParametro> ListaParametroConsulta = new List<EDSegVialParametro>();
                ListaPilaresConsulta = LNPromocionPrevencion.ConsultarPlanVialPilares(IdIntSegVial, ListaSedes);
                ListaPilares = ListaPilaresConsulta.Distinct().ToList();
                foreach (var item in ListaResultados)
                {
                    ListaParametros.Add(item.Parametro);
                    ViewBag.Sede = ListaSedes.Where(s => s.IdSede == item.PlanVial.Fk_Id_Sede).FirstOrDefault().NombreSede;
                    Sede = ListaSedes.Where(s => s.IdSede == item.PlanVial.Fk_Id_Sede).FirstOrDefault().NombreSede;
                    if (item.PlanVial.Fecha_Registro != null)
                    {
                        DateTime FechaR = item.PlanVial.Fecha_Registro;
                        ViewBag.FechaRegistro = FechaR.ToString("dd/MM/yyyy");
                        FechaRegistro= FechaR.ToString("dd/MM/yyyy");
                    }

                }
                var ListaParametroConsulta1 = ListaParametros.Distinct();
                //Listado general del formulario
                Formulario.ListaResultados = ListaResultados;
                //Lista De pilares para crear tabs
                Formulario.ListaPilares = ListaPilares;
                //Pilar En el que esta actualmente el usuario

                foreach (var item in ListaPilaresConsulta)
                {
                    int NumeroPilar1 = item.Pk_Id_SegVialPilar;
                    List<EDSegVialResultado> ListaParam = ListaResultados.Where(s => s.Parametro.Fk_Id_SegVialPilar == NumeroPilar1).ToList();
                    bool Cumple = false;
                    foreach (var item1 in ListaParam)
                    {
                        if (item1.Aplica_s == 0 && item1.Existencia_s == 0 && item1.Responde_s == 0)
                        {
                            Formulario.PilarActual = item1.Pilar.Pk_Id_SegVialPilar;
                            Cumple = true;
                            ViewBag.finalizado = false;
                            SinPilar = false;
                            break;
                        }
                    }
                    if (Cumple)
                    {
                        break;
                    }
                }
                if (SinPilar)
                {
                    Formulario.PilarActual = 3000;
                }

                Formulario.IdEvaluacion = IdIntSegVial;

                #region CalculoValoresObtenidos
                foreach (var item in ListaResultados)
                {
                    if (item.Aplica_s == 1 && item.Existencia_s == 1 && item.Responde_s == 1)
                    {
                        decimal valorParametro = item.Parametro.Valor_Parametro;
                        List<EDSegVialResultado> numeroVariables = ListaResultados.Where(s => s.Parametro.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).ToList();
                        int NumVar = numeroVariables.Count();
                        decimal valorVariable = valorParametro / NumVar;

                        if (item.Aplica == true && item.Existencia == true && item.Responde == true)
                        {
                            item.ValorObtenido = valorVariable;
                            EDSegVialParametro EDSegVialParametro = ListaParametroConsulta1.Where(s => s.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).FirstOrDefault();
                            EDSegVialParametro.Valor_obtenido = EDSegVialParametro.Valor_obtenido + valorVariable;
                            EDSegVialParametro.Mostrar = true;

                            EDSegVialPilar EDSegVialPilar = ListaPilares.Where(s => s.Pk_Id_SegVialPilar == item.Pilar.Pk_Id_SegVialPilar).FirstOrDefault();
                            EDSegVialPilar.ValorObtenido = EDSegVialPilar.ValorObtenido + valorVariable;
                            EDSegVialPilar.ValorResultado = EDSegVialPilar.ValorObtenido * EDSegVialPilar.Valor_Ponderado;
                            EDSegVialPilar.Mostrar = true;
                        }
                        else
                        {
                            EDSegVialParametro EDSegVialParametro = ListaParametroConsulta1.Where(s => s.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).FirstOrDefault();
                            EDSegVialParametro.Mostrar = true;

                            EDSegVialPilar EDSegVialPilar = ListaPilares.Where(s => s.Pk_Id_SegVialPilar == item.Pilar.Pk_Id_SegVialPilar).FirstOrDefault();
                            EDSegVialPilar.Mostrar = true;
                        }
                    }
                }


                #endregion

                using (XLWorkbook wb = new XLWorkbook())
                {
                    
                    
                    
                    var ws0 = wb.Worksheets.Add("DATOS EMPRESA");

                    #region Pilares

                    foreach (var item in Formulario.ListaPilares)
                    {
                        String Descripcion = item.Descripcion.ToString();
                        if (Descripcion.Length >= 31)
                        {
                            Descripcion = Descripcion.Substring(0, 31);
                        }
                        var ws = wb.Worksheets.Add(Descripcion);
                        List<EDSegVialResultado> ListaResultado = Formulario.ListaResultados.Where(s=>s.Pilar.Pk_Id_SegVialPilar== item.Pk_Id_SegVialPilar).ToList();
                        int cont1 = 1;

                        
                        //identificar merge de parametros
                        cont1 = 5;
                        int IdtoMerge = 0;
                        int check = 0;
                        int first = 0;
                        int last = 0;
                        int contMerge = 0;
                        int contR = 1;

                        foreach (var item1 in ListaResultado)
                        {
                            if (check==0)
                            {
                                IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                check = 1;
                                first = cont1;
                            }
                            else
                            {
                                if (contR== ListaResultado.Count())
                                {
                                    IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                    first = cont1;
                                    contMerge++;
                                }
                                else
                                {
                                    if (item1.Parametro.Pk_Id_SegVialParametro == IdtoMerge)
                                    {
                                        last = cont1;
                                    }
                                    else
                                    {
                                        IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                        first = cont1;
                                        contMerge++;
                                    }
                                }
                                
                            }
                            cont1++;
                            contR++;
                        }

                        int[,] MergeArray = new int[2, contMerge];
                        string[] ParametroArray = new string[contMerge];
                        string[] NumeralArray = new string[contMerge];
                        decimal[] EsperadoArray = new decimal[contMerge];
                        int[] PartesArray = new int[contMerge];
                        cont1 = 5;
                        IdtoMerge = 0;
                        check = 0;
                        first = 0;
                        last = 0;
                        contMerge = 0;
                        contR = 1;
                        string NombreParam = "";
                        string NumeralParam = "";
                        decimal Esperado = 0;

                        foreach (var item1 in ListaResultado)
                        {
                            if (check == 0)
                            {
                                IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                check = 1;
                                first = cont1;
                                NombreParam = item1.Parametro.ParametroDef;
                                NumeralParam = item1.Parametro.Numeral;
                                Esperado = item1.Parametro.Valor_Parametro;
                            }
                            else
                            {
                                if (contR == ListaResultado.Count())
                                {
                                    IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                    MergeArray[0, contMerge] = first;
                                    MergeArray[1, contMerge] = contR+4;
                                    ParametroArray[contMerge] = item1.Parametro.ParametroDef;
                                    NumeralArray[contMerge] = item1.Parametro.Numeral;
                                    EsperadoArray[contMerge] = item1.Parametro.Valor_Parametro;
                                    first = cont1;
                                    contMerge++;
                                }
                                else
                                {
                                    if (item1.Parametro.Pk_Id_SegVialParametro == IdtoMerge)
                                    {
                                        last = cont1;
                                    }
                                    else
                                    {
                                        IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                        MergeArray[0, contMerge] = first;
                                        MergeArray[1, contMerge] = last;
                                        ParametroArray[contMerge] = NombreParam;
                                        NumeralArray[contMerge] = NumeralParam;
                                        EsperadoArray[contMerge] = Esperado;
                                        first = cont1;
                                        NombreParam = item1.Parametro.ParametroDef;
                                        NumeralParam = item1.Parametro.Numeral;
                                        Esperado = item1.Parametro.Valor_Parametro;
                                        contMerge++;
                                    }
                                }
                                
                            }
                            cont1++;
                            contR++;
                        }

                        for (int i = 0; i < contMerge; i++)
                        {
                            ws.Range(MergeArray[0, i], 1, MergeArray[1, i], 1).Merge();
                            ws.Range(MergeArray[0, i], 2, MergeArray[1, i], 2).Merge();
                            ws.Range(MergeArray[0, i], 12, MergeArray[1, i], 12).Merge();


                            ws.Cell(MergeArray[0, i], 2).Value = ParametroArray[i];
                            ws.Cell(MergeArray[0, i], 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(MergeArray[0, i], 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(MergeArray[0, i], 2).Style.Alignment.WrapText = true;
                            ws.Cell(MergeArray[0, i], 2).Style.Font.Bold = true;


                            ws.Cell(MergeArray[0, i], 1).Value = "'"+ NumeralArray[i];
                            ws.Cell(MergeArray[0, i], 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(MergeArray[0, i], 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(MergeArray[0, i], 1).Style.Alignment.WrapText = true;


                            ws.Cell(MergeArray[0, i], 12).Value = EsperadoArray[i];
                            ws.Cell(MergeArray[0, i], 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(MergeArray[0, i], 12).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(MergeArray[0, i], 12).Style.Alignment.WrapText = true;
                            ws.Cell(MergeArray[0, i], 12).Style.NumberFormat.Format = "#,##0.0";

                            for (int i1 = MergeArray[0, i]; i1 < MergeArray[1, i]+1; i1++)
                            {
                                decimal valEsp = EsperadoArray[i];
                                int numeroRegistros = MergeArray[1, i] - MergeArray[0, i]+1;
                                decimal valEspDetalle = valEsp / numeroRegistros;
                                ws.Cell(i1, 13).Value = valEspDetalle;
                            }


                        }
                        cont1 = 5;
                        contR = 1;
                        foreach (var item1 in ListaResultado)
                        {
                            string Aplica = "";
                            string Aplica1 = "";
                            string Existencia = "";
                            string Existencia1 = "";
                            string Responde = "";
                            string Responde1 = "";
                            if (item1.Aplica_s == 1)
                            {
                                if (item1.Aplica == true)
                                {
                                    Aplica = "X";
                                }
                                else
                                {
                                    Aplica1 = "X";
                                }
                            }
                            if (item1.Existencia_s == 1)
                            {
                                if (item1.Existencia == true)
                                {
                                    Existencia = "X";
                                }
                                else
                                {
                                    Existencia1 = "X";
                                }
                            }
                            if (item1.Responde_s == 1)
                            {
                                if (item1.Responde == true)
                                {
                                    Responde = "X";
                                }
                                else
                                {
                                    Responde1 = "X";
                                }
                            }

                            //numeral variable
                            ws.Cell(cont1, 3).Value = "'"+item1.DetalleParametro.Numeral;
                            ws.Cell(cont1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 3).Style.Alignment.WrapText = true;
                            //Criterio Aval
                            ws.Cell(cont1, 5).Value = "'" + item1.DetalleParametro.CriterioAval;
                            ws.Cell(cont1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 5).Style.Alignment.WrapText = true;

                            //Aplica & Responde & Existe
                            ws.Cell(cont1, 6).Value = "'" + Aplica;
                            ws.Cell(cont1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 6).Style.Alignment.WrapText = true;

                            ws.Cell(cont1, 7).Value = "'" + Aplica1;
                            ws.Cell(cont1, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 7).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 7).Style.Alignment.WrapText = true;

                            ws.Cell(cont1, 8).Value = "'" + Existencia;
                            ws.Cell(cont1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 8).Style.Alignment.WrapText = true;

                            ws.Cell(cont1, 9).Value = "'" + Existencia1;
                            ws.Cell(cont1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 9).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 9).Style.Alignment.WrapText = true;

                            ws.Cell(cont1, 10).Value = "'" + Responde;
                            ws.Cell(cont1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 10).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 10).Style.Alignment.WrapText = true;

                            ws.Cell(cont1, 11).Value = "'" + Responde1;
                            ws.Cell(cont1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 11).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 11).Style.Alignment.WrapText = true;

                            
                            ws.Cell(cont1, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 13).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 13).Style.Alignment.WrapText = true;
                            ws.Cell(cont1, 13).Style.NumberFormat.Format = "#,##0.00";

                            ws.Cell(cont1, 14).Value = item1.ValorObtenido;
                            ws.Cell(cont1, 14).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 14).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 14).Style.Alignment.WrapText = true;
                            ws.Cell(cont1, 14).Style.NumberFormat.Format = "#,##0.0";

                            ws.Cell(cont1, 15).Value = item1.Observaciones;
                            ws.Cell(cont1, 15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(cont1, 15).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(cont1, 15).Style.Alignment.WrapText = true;
                            cont1++;
                        }

                        for (int i = 5; i < cont1; i++)
                        {
                            for (int i1 = 1; i1 < 16; i1++)
                            {
                                ws.Cell(i, i1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }

                        ws.Range(cont1+1, 2, cont1 + 1, 11).Merge();
                        ws.Range(cont1 + 1, 2, cont1 + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(cont1 + 1, 2, cont1 + 1, 11).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Range(cont1 + 1, 2, cont1 + 1, 11).Style.Alignment.WrapText = true;
                        ws.Range(cont1 + 1, 2, cont1 + 1, 11).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws.Range(cont1 + 1, 2, cont1 + 1, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Range(cont1 + 1, 2, cont1 + 1, 11).Style.Font.Bold = true;
                        ws.Cell(cont1+1, 2).Value = "TOTAL";


                        var sum = ws.Evaluate("SUM(L5:L"+ cont1 + ")");
                        var sum2 = ws.Evaluate("SUM(M5:M" + cont1 + ")");
                        var sum3 = ws.Evaluate("SUM(N5:N" + cont1 + ")");

                        ws.Cell(cont1 + 1, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(cont1 + 1, 12).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Cell(cont1 + 1, 12).Style.Alignment.WrapText = true;
                        ws.Cell(cont1 + 1, 12).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws.Cell(cont1 + 1, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cont1 + 1, 12).Style.Font.Bold = true;
                        ws.Cell(cont1 + 1, 12).Value = sum;
                        ws.Cell(cont1 + 1, 12).Style.NumberFormat.Format = "#,##0.0";

                        ws.Cell(cont1 + 1, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(cont1 + 1, 13).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Cell(cont1 + 1, 13).Style.Alignment.WrapText = true;
                        ws.Cell(cont1 + 1, 13).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws.Cell(cont1 + 1, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cont1 + 1, 13).Style.Font.Bold = true;
                        ws.Cell(cont1 + 1, 13).Value = sum2;
                        ws.Cell(cont1 + 1, 13).Style.NumberFormat.Format = "#,##0.0";

                        ws.Cell(cont1 + 1, 14).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(cont1 + 1, 14).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Cell(cont1 + 1, 14).Style.Alignment.WrapText = true;
                        ws.Cell(cont1 + 1, 14).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws.Cell(cont1 + 1, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cont1 + 1, 14).Style.Font.Bold = true;
                        ws.Cell(cont1 + 1, 14).Value = sum3;
                        ws.Cell(cont1 + 1, 14).Style.NumberFormat.Format = "#,##0.0";

                        cont1 = 5;
                        contR = 1;
                        List<string> ListaVariables = new List<string>();
                        foreach (var item1 in ListaResultado)
                        {
                            ListaVariables.Add(item1.DetalleParametro.VariableDesc);
                            item1.DetalleParametro.Pk_Id_SegVialParametroDetalle = cont1;
                            cont1++;
                            contR++;
                        }
                        var ListaVariables1 = ListaVariables.Distinct();
                        foreach (var item1 in ListaVariables1)
                        {
                            string texto= ListaResultado.Where(s => s.DetalleParametro.VariableDesc == item1).First().DetalleParametro.VariableDesc;
                            int Primero = ListaResultado.Where(s=>s.DetalleParametro.VariableDesc== item1).First().DetalleParametro.Pk_Id_SegVialParametroDetalle;
                            int Ultimo = ListaResultado.Where(s => s.DetalleParametro.VariableDesc == item1).Last().DetalleParametro.Pk_Id_SegVialParametroDetalle;

                            ws.Range(Primero, 4, Ultimo, 4).Merge();
                            ws.Cell(Primero, 4).Value = texto;
                            ws.Range(Primero, 4, Ultimo, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Range(Primero, 4, Ultimo, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Range(Primero, 4, Ultimo, 4).Style.Alignment.WrapText = true;
                        }

                        

                        var fil1 = ws.Row(1);
                        fil1.Height = 27;
                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 1).Style.Font.FontSize = 16;
                        ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Cell(1, 1).Value = "EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                        ws.Range(1,1, 1,15).Merge();
                        

                        var fil2 = ws.Row(2);
                        fil2.Height = 27;
                        ws.Cell(2, 1).Style.Font.Bold = true;
                        ws.Cell(2, 1).Style.Font.FontSize = 16;
                        ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Cell(2, 1).Value = item.Descripcion;
                        ws.Range(2, 1, 2, 15).Merge();
                        ws.Cell(2, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws.Range(2, 1, 2, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        var fil3 = ws.Row(3);
                        fil3.Height = 50;
                        ws.Range(3, 1, 3, 15).Style.Font.Bold = true;
                        ws.Range(3, 1, 3, 15).Style.Font.FontSize = 12;
                        ws.Range(3, 1, 3, 15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(3, 1, 3, 15).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws.Range(3, 1, 3, 15).Value = item.Descripcion;
                        ws.Range(3, 1, 3, 15).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);

                        ws.Cell(3, 1).Value = "No.";
                        ws.Cell(3, 2).Value = "PARÁMETRO - DEFINICIÓN";
                        ws.Cell(3, 3).Value = "";
                        ws.Cell(3, 4).Value = "VARIABLES";
                        ws.Cell(3, 5).Value = "CRITERIO DE AVAL";
                        ws.Cell(3, 6).Value = "APLICA ";
                        ws.Cell(3, 7).Value = "";
                        ws.Range(3, 6, 3, 7).Merge();
                        ws.Cell(3, 8).Value = "Evidencias de su existencia";
                        ws.Cell(3, 9).Value = "";
                        ws.Range(3, 8, 3, 9).Merge();
                        ws.Cell(3, 10).Value = "Responde a los requerimientos";
                        ws.Range(3, 10, 3, 11).Merge();
                        ws.Cell(3, 11).Value = "";
                        ws.Cell(3, 12).Value = "Valor del Parámetro";
                        ws.Cell(3, 13).Value = "Valor de la variable";
                        ws.Cell(3, 14).Value = "Valor Obtenido";
                        ws.Cell(3, 15).Value = "Valor Obtenido parametro";
                        ws.Cell(3, 15).Value = "Observaciones";

                        for (int i = 1; i < 16; i++)
                        {
                            ws.Cell(3, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            ws.Cell(3, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(3, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(3, i).Style.Alignment.WrapText = true;
                        }
                        var col1 = ws.Column(1);
                        col1.Width = 6;
                        var col2 = ws.Column(2);
                        col2.Width = 45;
                        var col3 = ws.Column(3);
                        col3.Width = 8;
                        var col4 = ws.Column(4);
                        col4.Width = 32;
                        var col5 = ws.Column(5);
                        col5.Width = 32;

                        var col6 = ws.Column(6);
                        col6.Width = 5;
                        var col7 = ws.Column(7);
                        col7.Width = 5;
                        var col8 = ws.Column(8);
                        col8.Width = 5;
                        var col9 = ws.Column(9);
                        col9.Width = 5;
                        var col10 = ws.Column(10);
                        col10.Width = 10;
                        var col11 = ws.Column(11);
                        col11.Width = 10;

                        for (int i = 12; i < 15; i++)
                        {
                            var coli = ws.Column(i);
                            coli.Width = 13;
                        }
                        var col15 = ws.Column(15);
                        col15.Width = 20;

                        ws.Range(4, 1, 4, 15).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws.Range(4, 1, 4, 5).Merge();
                        ws.Range(4, 1, 4, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        bool Intercalar = false;
                        for (int i = 6; i < 12; i++)
                        {
                            ws.Cell(4, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            ws.Cell(4, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Cell(4, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(4, i).Style.Font.Bold = true;
                            ws.Cell(4, i).Style.Font.FontSize = 12;
                            if (Intercalar)
                            {
                                ws.Cell(4, i).Value = "NO";
                                Intercalar = false;
                            }
                            else
                            {
                                ws.Cell(4, i).Value = "SI";
                                Intercalar = true;
                            }
                        }
                        ws.Range(4, 12, 4, 15).Merge();
                        ws.Range(4, 12, 4, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws.SheetView.FreezeRows(4);

                    }
                    #endregion
                    var ws1 = wb.Worksheets.Add("RESULTADO");


                    #region resultado

                    
                    var fila1 = ws1.Row(1);
                    fila1.Height = 27;
                    ws1.Cell(1, 1).Style.Font.Bold = true;
                    ws1.Cell(1, 1).Style.Font.FontSize = 16;
                    ws1.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws1.Cell(1, 1).Value = "EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                    ws1.Range(1, 1, 1, 5).Merge();


                    var fila2 = ws1.Row(2);
                    fila2.Height = 32;
                    ws1.Range(2, 1, 2, 2).Merge();
                    for (int i = 1; i < 6; i++)
                    {
                        ws1.Cell(2, i).Style.Font.Bold = true;
                        ws1.Cell(2, i).Style.Font.FontSize = 12;
                        ws1.Cell(2, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell(2, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws1.Cell(2, i).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws1.Cell(2, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws1.Cell(2, i).Style.Alignment.WrapText = true;
                    }

                    ws1.Cell(2, 1).Value = "PILAR";
                    ws1.Cell(2, 3).Value = "VALOR OBTENIDO";
                    ws1.Cell(2, 4).Value = "VALOR PONDERADO";
                    ws1.Cell(2, 5).Value = "RESULTADO";

                    var acol1 = ws1.Column(1);
                    acol1.Width = 4;
                    var acol2 = ws1.Column(2);
                    acol2.Width = 59;
                    var acol3 = ws1.Column(3);
                    acol3.Width = 17;
                    var acol4 = ws1.Column(4);
                    acol4.Width = 17;
                    var acol5 = ws1.Column(5);
                    acol5.Width = 17;
                    int contR1 = 1;
                    int cont11 = 3;
                    decimal totalRes = 0;
                    decimal totalPond = 0;
                    decimal totalObt = 0;

                    foreach (var item in Formulario.ListaPilares)
                    {
                        int NumPilar = contR1;
                        string DescPilar = item.Descripcion;
                        decimal Obtenido = item.ValorObtenido;
                        decimal Ponderado = item.Valor_Ponderado;
                        decimal Resultado = Obtenido * Ponderado;
                        totalRes = totalRes + Resultado;
                        totalPond = totalPond + Ponderado;
                        totalObt = totalObt + Obtenido;

                        ws1.Cell(cont11, 1).Value = NumPilar;
                        ws1.Cell(cont11, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell(cont11, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws1.Cell(cont11, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws1.Cell(cont11, 1).Style.Alignment.WrapText = true;

                        ws1.Cell(cont11, 2).Value = DescPilar;
                        ws1.Cell(cont11, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell(cont11, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws1.Cell(cont11, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws1.Cell(cont11, 2).Style.Alignment.WrapText = true;

                        ws1.Cell(cont11, 3).Value = Obtenido;
                        ws1.Cell(cont11, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell(cont11, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws1.Cell(cont11, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws1.Cell(cont11, 3).Style.Alignment.WrapText = true;
                        ws1.Cell(cont11, 3).Style.NumberFormat.Format = "#,##0.0";

                        ws1.Cell(cont11, 4).Value = Ponderado;
                        ws1.Cell(cont11, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell(cont11, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws1.Cell(cont11, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws1.Cell(cont11, 4).Style.Alignment.WrapText = true;
                        ws1.Cell(cont11, 4).Style.NumberFormat.Format = "0.0%";

                        ws1.Cell(cont11, 5).Value = Resultado;
                        ws1.Cell(cont11, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell(cont11, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws1.Cell(cont11, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws1.Cell(cont11, 5).Style.Alignment.WrapText = true;
                        ws1.Cell(cont11, 5).Style.NumberFormat.Format = "#,##0.0";

                        cont11++;
                        contR1++;
                    }


                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Merge();
                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Style.Alignment.WrapText = true;
                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws1.Range(cont11 + 1, 1, cont11 + 1, 2).Style.Font.Bold = true;
                    ws1.Cell(cont11 + 1, 1).Value = "TOTAL";


                    ws1.Cell(cont11 + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell(cont11 + 1, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws1.Cell(cont11 + 1, 3).Style.Alignment.WrapText = true;
                    ws1.Cell(cont11 + 1, 3).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                    ws1.Cell(cont11 + 1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws1.Cell(cont11 + 1, 3).Style.Font.Bold = true;
                    ws1.Cell(cont11 + 1, 3).Value = totalObt;
                    ws1.Cell(cont11 + 1, 3).Style.NumberFormat.Format = "#,##0.0";

                    ws1.Cell(cont11 + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell(cont11 + 1, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws1.Cell(cont11 + 1, 4).Style.Alignment.WrapText = true;
                    ws1.Cell(cont11 + 1, 4).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                    ws1.Cell(cont11 + 1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws1.Cell(cont11 + 1, 4).Style.Font.Bold = true;
                    ws1.Cell(cont11 + 1, 4).Value = totalPond;
                    ws1.Cell(cont11 + 1, 4).Style.NumberFormat.Format = "0.0%";

                    ws1.Cell(cont11 + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell(cont11 + 1, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws1.Cell(cont11 + 1, 5).Style.Alignment.WrapText = true;
                    ws1.Cell(cont11 + 1, 5).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                    ws1.Cell(cont11 + 1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws1.Cell(cont11 + 1, 5).Style.Font.Bold = true;
                    ws1.Cell(cont11 + 1, 5).Value = totalRes;
                    ws1.Cell(cont11 + 1, 5).Style.NumberFormat.Format = "#,##0.0";
                    #endregion


                    var filb1 = ws0.Row(1);
                    filb1.Height = 27;
                    ws0.Cell(1, 1).Style.Font.Bold = true;
                    ws0.Cell(1, 1).Style.Font.FontSize = 16;
                    ws0.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws0.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws0.Cell(1, 1).Value = "EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                    ws0.Range(1, 1, 1, 5).Merge();

                    var filb2 = ws0.Row(2);
                    filb2.Height = 32;
                    for (int i = 1; i < 5; i++)
                    {
                        ws0.Cell(2, i).Style.Font.Bold = true;
                        ws0.Cell(2, i).Style.Font.FontSize = 12;
                        ws0.Cell(2, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws0.Cell(2, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws0.Cell(2, i).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                        ws0.Cell(2, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws0.Cell(2, i).Style.Alignment.WrapText = true;
                    }
                    for (int i = 1; i < 5; i++)
                    {
                        ws0.Cell(3, i).Style.Font.FontSize = 12;
                        ws0.Cell(3, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws0.Cell(3, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        ws0.Cell(3, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws0.Cell(3, i).Style.Alignment.WrapText = true;
                    }

                    ws0.Cell(2, 1).Value = "FECHA DE REGISTRO";
                    ws0.Cell(2, 2).Value = "NIT";
                    ws0.Cell(2, 3).Value = "RAZÓN SOCIAL";
                    ws0.Cell(2, 4).Value = "SEDE EVALUADA";

                    var bcol1 = ws0.Column(1);
                    bcol1.Width = 17;
                    var bcol2 = ws0.Column(2);
                    bcol2.Width = 17;
                    var bcol3 = ws0.Column(3);
                    bcol3.Width = 50;
                    var bcol4 = ws0.Column(4);
                    bcol4.Width = 30;

                    ws0.Cell(3, 1).Value = FechaRegistro;
                    ws0.Cell(3, 2).Value = Nit;
                    ws0.Cell(3, 3).Value = RazonSocial;
                    ws0.Cell(3, 4).Value = Sede;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaResultadoSegVial" + Fecha + ".xlsx");
                    }
                    
                }

                if (Formulario.ListaResultados.Count==0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Hoja 1");
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaResultadoSegVial" + Fecha + ".xlsx");
                        }
                    }

                }
                else
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Hoja 1");
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaResultadoSegVial" + Fecha + ".xlsx");
                        }
                    }
                }
                

            }
            else
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Hoja 1");
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaResultadoSegVial" + Fecha + ".xlsx");
                    }
                }
            }


        }

        [HttpPost]
        public ActionResult AsignacionExcelEPPplus(string IdSegVial)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.finalizado = true;
            ViewBag.RazonS = "";
            ViewBag.Sede = "";
            ViewBag.FechaRegistro = "";

            string RazonSocial = usuarioActual.RazonSocialEmpresa;
            string Sede = "";
            string FechaRegistro = "";
            string Nit = usuarioActual.NitEmpresa;

            var ListaSedes = new List<EDSede>();
            ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);
            List<EDSegVialResultado> ListaResultados = new List<EDSegVialResultado>();
            List<EDSegVialPilar> ListaPilares = new List<EDSegVialPilar>();
            List<EDSegVialParametro> ListaParametros = new List<EDSegVialParametro>();

            List<EDSegVialPilar> ListaPilaresRep = new List<EDSegVialPilar>();
            List<EDSegVialParametro> ListaParametrosRep = new List<EDSegVialParametro>();

            List<EDSegVialPilar> ListaPilaresRep1 = new List<EDSegVialPilar>();
            List<EDSegVialParametro> ListaParametrosRep1 = new List<EDSegVialParametro>();

            EDSegVialFormulario Formulario = new EDSegVialFormulario();
            int IdIntSegVial = 0;
            bool SinPilar = true;
            if (int.TryParse(IdSegVial, out IdIntSegVial))
            {
                ListaResultados = LNPromocionPrevencion.ConsultarPlanVialResultado(IdIntSegVial, ListaSedes, usuarioActual.IdEmpresa);
                ViewBag.RazonS = usuarioActual.RazonSocialEmpresa;

                List<EDSegVialPilar> ListaPilaresConsulta = new List<EDSegVialPilar>();
                List<EDSegVialParametro> ListaParametroConsulta = new List<EDSegVialParametro>();
                ListaPilaresConsulta = LNPromocionPrevencion.ConsultarPlanVialPilares(IdIntSegVial, ListaSedes);
                ListaPilares = ListaPilaresConsulta.Distinct().ToList();
                foreach (var item in ListaResultados)
                {
                    ListaParametros.Add(item.Parametro);
                    ViewBag.Sede = ListaSedes.Where(s => s.IdSede == item.PlanVial.Fk_Id_Sede).FirstOrDefault().NombreSede;
                    Sede = ListaSedes.Where(s => s.IdSede == item.PlanVial.Fk_Id_Sede).FirstOrDefault().NombreSede;
                    if (item.PlanVial.Fecha_Registro != null)
                    {
                        DateTime FechaR = item.PlanVial.Fecha_Registro;
                        ViewBag.FechaRegistro = FechaR.ToString("dd/MM/yyyy");
                        FechaRegistro = FechaR.ToString("dd/MM/yyyy");
                    }

                }
                var ListaParametroConsulta1 = ListaParametros.Distinct();
                //Listado general del formulario
                Formulario.ListaResultados = ListaResultados;
                //Lista De pilares para crear tabs
                Formulario.ListaPilares = ListaPilares;
                //Pilar En el que esta actualmente el usuario

                foreach (var item in ListaPilaresConsulta)
                {
                    int NumeroPilar1 = item.Pk_Id_SegVialPilar;
                    List<EDSegVialResultado> ListaParam = ListaResultados.Where(s => s.Parametro.Fk_Id_SegVialPilar == NumeroPilar1).ToList();
                    bool Cumple = false;
                    foreach (var item1 in ListaParam)
                    {
                        if (item1.Aplica_s == 0 && item1.Existencia_s == 0 && item1.Responde_s == 0)
                        {
                            Formulario.PilarActual = item1.Pilar.Pk_Id_SegVialPilar;
                            Cumple = true;
                            ViewBag.finalizado = false;
                            SinPilar = false;
                            break;
                        }
                    }
                    if (Cumple)
                    {
                        break;
                    }
                }
                if (SinPilar)
                {
                    Formulario.PilarActual = 3000;
                }

                Formulario.IdEvaluacion = IdIntSegVial;
                #region CalculoValoresObtenidos
                foreach (var item in ListaResultados)
                {
                    if (item.Aplica_s == 1 && item.Existencia_s == 1 && item.Responde_s == 1)
                    {
                        decimal valorParametro = item.Parametro.Valor_Parametro;
                        List<EDSegVialResultado> numeroVariables = ListaResultados.Where(s => s.Parametro.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).ToList();
                        int NumVar = numeroVariables.Count();
                        decimal valorVariable = valorParametro / NumVar;

                        if (item.Aplica == true && item.Existencia == true && item.Responde == true)
                        {
                            item.ValorObtenido = valorVariable;
                            EDSegVialParametro EDSegVialParametro = ListaParametroConsulta1.Where(s => s.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).FirstOrDefault();
                            EDSegVialParametro.Valor_obtenido = EDSegVialParametro.Valor_obtenido + valorVariable;
                            EDSegVialParametro.Mostrar = true;

                            EDSegVialPilar EDSegVialPilar = ListaPilares.Where(s => s.Pk_Id_SegVialPilar == item.Pilar.Pk_Id_SegVialPilar).FirstOrDefault();
                            EDSegVialPilar.ValorObtenido = EDSegVialPilar.ValorObtenido + valorVariable;
                            EDSegVialPilar.ValorResultado = EDSegVialPilar.ValorObtenido * EDSegVialPilar.Valor_Ponderado;
                            EDSegVialPilar.Mostrar = true;
                        }
                        else
                        {
                            EDSegVialParametro EDSegVialParametro = ListaParametroConsulta1.Where(s => s.Pk_Id_SegVialParametro == item.Parametro.Pk_Id_SegVialParametro).FirstOrDefault();
                            EDSegVialParametro.Mostrar = true;

                            EDSegVialPilar EDSegVialPilar = ListaPilares.Where(s => s.Pk_Id_SegVialPilar == item.Pilar.Pk_Id_SegVialPilar).FirstOrDefault();
                            EDSegVialPilar.Mostrar = true;
                        }
                    }
                }


                #endregion

                foreach (var item in ListaResultados)
                {
                    ListaPilaresRep.Add(item.Pilar);
                }
                foreach (var item in ListaResultados)
                {
                    ListaParametrosRep.Add(item.Parametro);
                }


                foreach (var item in ListaPilaresRep)
                {
                    int id = item.Pk_Id_SegVialPilar;
                    var elemento = ListaPilaresRep1.Where(s => s.Pk_Id_SegVialPilar == id).FirstOrDefault();
                    if (elemento==null)
                    {
                        ListaPilaresRep1.Add(item);
                    }
                }
                foreach (var item in ListaParametrosRep)
                {
                    int id = item.Pk_Id_SegVialParametro;
                    var lista = ListaParametrosRep.Where(s => s.Pk_Id_SegVialParametro == id).ToList();
                    
                    EDSegVialParametro param = new EDSegVialParametro();
                    decimal valormax = 0;
                    foreach (var item1 in lista)
                    {
                        if (item1.Valor_obtenido>= valormax)
                        {
                            valormax = item1.Valor_obtenido;
                            param = item1;
                        }
                    }
                    var elemento = ListaParametrosRep1.Where(s => s.Pk_Id_SegVialParametro== id).FirstOrDefault();
                    if (elemento == null)
                    {
                        ListaParametrosRep1.Add(item);
                    }
                }

                foreach (var item in ListaPilaresRep1)
                {
                    int id = item.Pk_Id_SegVialPilar;
                    var lista = ListaParametrosRep1.Where(s => s.Fk_Id_SegVialPilar == id).ToList().Distinct().ToList();
                    if (lista!=null)
                    {
                        item.ListaParametros = lista;
                    }
                }



                //Response.Clear();
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("Logs.xlsx", System.Text.Encoding.UTF8));

                using (ExcelPackage wb = new ExcelPackage())
                {
                    
                    ExcelWorksheet ws0 = wb.Workbook.Worksheets.Add("DATOS EMPRESA");
                    Color graycolor = ColorTranslator.FromHtml("#acb3a8");
                    #region Pilares

                    foreach (var item in Formulario.ListaPilares)
                    {
                        String Descripcion = item.Descripcion.ToString();
                        if (Descripcion.Length >= 31)
                        {
                            Descripcion = Descripcion.Substring(0, 31);
                        }
                        var ws = wb.Workbook.Worksheets.Add(Descripcion);
                        List<EDSegVialResultado> ListaResultado = Formulario.ListaResultados.Where(s => s.Pilar.Pk_Id_SegVialPilar == item.Pk_Id_SegVialPilar).ToList();
                        int cont1 = 1;


                        //identificar merge de parametros
                        cont1 = 5;
                        int IdtoMerge = 0;
                        int check = 0;
                        int first = 0;
                        int last = 0;
                        int contMerge = 0;
                        int contR = 1;

                        foreach (var item1 in ListaResultado)
                        {
                            if (check == 0)
                            {
                                IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                check = 1;
                                first = cont1;
                            }
                            else
                            {
                                if (contR == ListaResultado.Count())
                                {
                                    IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                    first = cont1;
                                    contMerge++;
                                }
                                else
                                {
                                    if (item1.Parametro.Pk_Id_SegVialParametro == IdtoMerge)
                                    {
                                        last = cont1;
                                    }
                                    else
                                    {
                                        IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                        first = cont1;
                                        contMerge++;
                                    }
                                }

                            }
                            cont1++;
                            contR++;
                        }

                        int[,] MergeArray = new int[2, contMerge];
                        string[] ParametroArray = new string[contMerge];
                        string[] NumeralArray = new string[contMerge];
                        decimal[] EsperadoArray = new decimal[contMerge];
                        int[] PartesArray = new int[contMerge];
                        cont1 = 5;
                        IdtoMerge = 0;
                        check = 0;
                        first = 0;
                        last = 0;
                        contMerge = 0;
                        contR = 1;
                        string NombreParam = "";
                        string NumeralParam = "";
                        decimal Esperado = 0;

                        foreach (var item1 in ListaResultado)
                        {
                            if (check == 0)
                            {
                                IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                check = 1;
                                first = cont1;
                                NombreParam = item1.Parametro.ParametroDef;
                                NumeralParam = item1.Parametro.Numeral;
                                Esperado = item1.Parametro.Valor_Parametro;
                            }
                            else
                            {
                                if (contR == ListaResultado.Count())
                                {
                                    IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                    MergeArray[0, contMerge] = first;
                                    MergeArray[1, contMerge] = contR + 4;
                                    ParametroArray[contMerge] = item1.Parametro.ParametroDef;
                                    NumeralArray[contMerge] = item1.Parametro.Numeral;
                                    EsperadoArray[contMerge] = item1.Parametro.Valor_Parametro;
                                    first = cont1;
                                    contMerge++;
                                }
                                else
                                {
                                    if (item1.Parametro.Pk_Id_SegVialParametro == IdtoMerge)
                                    {
                                        last = cont1;
                                    }
                                    else
                                    {
                                        IdtoMerge = item1.Parametro.Pk_Id_SegVialParametro;
                                        MergeArray[0, contMerge] = first;
                                        MergeArray[1, contMerge] = last;
                                        ParametroArray[contMerge] = NombreParam;
                                        NumeralArray[contMerge] = NumeralParam;
                                        EsperadoArray[contMerge] = Esperado;
                                        first = cont1;
                                        NombreParam = item1.Parametro.ParametroDef;
                                        NumeralParam = item1.Parametro.Numeral;
                                        Esperado = item1.Parametro.Valor_Parametro;
                                        contMerge++;
                                    }
                                }

                            }
                            cont1++;
                            contR++;
                        }

                        for (int i = 0; i < contMerge; i++)
                        {
                            ws.Cells[MergeArray[0, i], 1, MergeArray[1, i], 1].Merge = true;
                            ws.Cells[MergeArray[0, i], 2, MergeArray[1, i], 2].Merge = true;
                            ws.Cells[MergeArray[0, i], 12, MergeArray[1, i], 12].Merge = true;


                            ws.Cells[MergeArray[0, i], 2].Value = ParametroArray[i];
                            ws.Cells[MergeArray[0, i], 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[MergeArray[0, i], 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[MergeArray[0, i], 2].Style.WrapText = true;
                            ws.Cells[MergeArray[0, i], 2].Style.Font.Bold = true;


                            ws.Cells[MergeArray[0, i], 1].Value =  NumeralArray[i];
                            ws.Cells[MergeArray[0, i], 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[MergeArray[0, i], 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[MergeArray[0, i], 1].Style.WrapText = true;


                            ws.Cells[MergeArray[0, i], 12].Value = EsperadoArray[i];
                            ws.Cells[MergeArray[0, i], 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[MergeArray[0, i], 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[MergeArray[0, i], 12].Style.WrapText = true;
                            ws.Cells[MergeArray[0, i], 12].Style.Numberformat.Format = "#,##0.0";

                            for (int i1 = MergeArray[0, i]; i1 < MergeArray[1, i] + 1; i1++)
                            {
                                decimal valEsp = EsperadoArray[i];
                                int numeroRegistros = MergeArray[1, i] - MergeArray[0, i] + 1;
                                decimal valEspDetalle = valEsp / numeroRegistros;
                                ws.Cells[i1, 13].Value = valEspDetalle;
                            }


                        }
                        cont1 = 5;
                        contR = 1;
                        foreach (var item1 in ListaResultado)
                        {
                            string Aplica = "";
                            string Aplica1 = "";
                            string Existencia = "";
                            string Existencia1 = "";
                            string Responde = "";
                            string Responde1 = "";
                            if (item1.Aplica_s == 1)
                            {
                                if (item1.Aplica == true)
                                {
                                    Aplica = "X";
                                }
                                else
                                {
                                    Aplica1 = "X";
                                }
                            }
                            if (item1.Existencia_s == 1)
                            {
                                if (item1.Existencia == true)
                                {
                                    Existencia = "X";
                                }
                                else
                                {
                                    Existencia1 = "X";
                                }
                            }
                            if (item1.Responde_s == 1)
                            {
                                if (item1.Responde == true)
                                {
                                    Responde = "X";
                                }
                                else
                                {
                                    Responde1 = "X";
                                }
                            }

                            //numeral variable
                            ws.Cells[cont1, 3].Value =  item1.DetalleParametro.Numeral;
                            ws.Cells[cont1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 3].Style.WrapText = true;
                            //Criterio Aval
                            ws.Cells[cont1, 5].Value = item1.DetalleParametro.CriterioAval;
                            ws.Cells[cont1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 5].Style.WrapText = true;

                            //Aplica & Responde & Existe
                            ws.Cells[cont1, 6].Value =  Aplica;
                            ws.Cells[cont1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 6].Style.WrapText = true;

                            ws.Cells[cont1, 7].Value =  Aplica1;
                            ws.Cells[cont1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 7].Style.WrapText = true;

                            ws.Cells[cont1, 8].Value =  Existencia;
                            ws.Cells[cont1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 8].Style.WrapText = true;

                            ws.Cells[cont1, 9].Value =  Existencia1;
                            ws.Cells[cont1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 9].Style.WrapText = true;

                            ws.Cells[cont1, 10].Value =  Responde;
                            ws.Cells[cont1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 10].Style.WrapText = true;

                            ws.Cells[cont1, 11].Value =  Responde1;
                            ws.Cells[cont1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 11].Style.WrapText = true;


                            ws.Cells[cont1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 13].Style.WrapText = true;
                            ws.Cells[cont1, 13].Style.Numberformat.Format = "#,##0.00";

                            ws.Cells[cont1, 14].Value = item1.ValorObtenido;
                            ws.Cells[cont1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 14].Style.WrapText = true;
                            ws.Cells[cont1, 14].Style.Numberformat.Format = "#,##0.0";

                            ws.Cells[cont1, 15].Value = item1.Observaciones;
                            ws.Cells[cont1, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[cont1, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[cont1, 15].Style.WrapText = true;
                            cont1++;
                        }

                        for (int i = 5; i < cont1; i++)
                        {
                            for (int i1 = 1; i1 < 16; i1++)
                            {
                                ws.Cells[i, i1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                ws.Cells[i, i1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                ws.Cells[i, i1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[i, i1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            }
                        }

                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Merge = true;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.WrapText = true;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 2, cont1 + 1, 11].Style.Font.Bold = true;
                        ws.Cells[cont1 + 1, 2].Value = "TOTAL";

                        ws.Cells[cont1 + 1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[cont1 + 1, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[cont1 + 1, 12].Style.WrapText = true;
                        ws.Cells[cont1 + 1, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[cont1 + 1, 12].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws.Cells[cont1 + 1, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 12].Style.Font.Bold = true;
                        ws.Cells[cont1 + 1, 12].Formula = "SUM(L5:L" + cont1 + ")";
                        ws.Cells[cont1 + 1, 12].Style.Numberformat.Format = "#,##0.0";

                        ws.Cells[cont1 + 1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[cont1 + 1, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[cont1 + 1, 13].Style.WrapText = true;
                        ws.Cells[cont1 + 1, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[cont1 + 1, 13].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws.Cells[cont1 + 1, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 13].Style.Font.Bold = true;
                        ws.Cells[cont1 + 1, 13].Formula = "SUM(M5:M" + cont1 + ")";
                        ws.Cells[cont1 + 1, 13].Style.Numberformat.Format = "#,##0.0";

                        ws.Cells[cont1 + 1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[cont1 + 1, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[cont1 + 1, 14].Style.WrapText = true;
                        ws.Cells[cont1 + 1, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[cont1 + 1, 14].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws.Cells[cont1 + 1, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws.Cells[cont1 + 1, 14].Style.Font.Bold = true;
                        ws.Cells[cont1 + 1, 14].Formula = "SUM(N5:N" + cont1 + ")";
                        ws.Cells[cont1 + 1, 14].Style.Numberformat.Format = "#,##0.0";

                        cont1 = 5;
                        contR = 1;
                        List<string> ListaVariables = new List<string>();
                        foreach (var item1 in ListaResultado)
                        {
                            ListaVariables.Add(item1.DetalleParametro.VariableDesc);
                            item1.DetalleParametro.Pk_Id_SegVialParametroDetalle = cont1;
                            cont1++;
                            contR++;
                        }
                        var ListaVariables1 = ListaVariables.Distinct();
                        foreach (var item1 in ListaVariables1)
                        {
                            string texto = ListaResultado.Where(s => s.DetalleParametro.VariableDesc == item1).First().DetalleParametro.VariableDesc;
                            int Primero = ListaResultado.Where(s => s.DetalleParametro.VariableDesc == item1).First().DetalleParametro.Pk_Id_SegVialParametroDetalle;
                            int Ultimo = ListaResultado.Where(s => s.DetalleParametro.VariableDesc == item1).Last().DetalleParametro.Pk_Id_SegVialParametroDetalle;

                            ws.Cells[Primero, 4, Ultimo, 4].Merge = true;
                            ws.Cells[Primero, 4].Value = texto;
                            ws.Cells[Primero, 4, Ultimo, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[Primero, 4, Ultimo, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[Primero, 4, Ultimo, 4].Style.WrapText = true;
                        }



                        var fil1 = ws.Row(1);
                        fil1.Height = 27;
                        ws.Cells[1, 1].Style.Font.Bold = true;
                        ws.Cells[1, 1].Style.Font.Size = 16;
                        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[1, 1].Value = "EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                        ws.Cells[1, 1, 1, 15].Merge = true;


                        var fil2 = ws.Row(2);
                        fil2.Height = 27;
                        ws.Cells[2, 1].Style.Font.Bold = true;
                        ws.Cells[2, 1].Style.Font.Size = 16;
                        ws.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[2, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[2, 1].Value = item.Descripcion;
                        ws.Cells[2, 1, 2, 15].Merge = true;
                        ws.Cells[2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[2, 1].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws.Cells[2, 1, 2, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[2, 1, 2, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[2, 1, 2, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[2, 1, 2, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        var fil3 = ws.Row(3);
                        fil3.Height = 50;
                        ws.Cells[3, 1, 3, 15].Style.Font.Bold = true;
                        ws.Cells[3, 1, 3, 15].Style.Font.Size = 12;
                        ws.Cells[3, 1, 3, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[3, 1, 3, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[3, 1, 3, 15].Value = item.Descripcion;
                        ws.Cells[3, 1, 3, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[3, 1, 3, 15].Style.Fill.BackgroundColor.SetColor(graycolor);

                        ws.Cells[3, 1].Value = "No.";
                        ws.Cells[3, 2].Value = "PARÁMETRO - DEFINICIÓN";
                        ws.Cells[3, 3].Value = "";
                        ws.Cells[3, 4].Value = "VARIABLES";
                        ws.Cells[3, 5].Value = "CRITERIO DE AVAL";
                        ws.Cells[3, 6].Value = "APLICA ";
                        ws.Cells[3, 7].Value = "";
                        ws.Cells[3, 6, 3, 7].Merge = true;
                        ws.Cells[3, 8].Value = "Evidencias de su existencia";
                        ws.Cells[3, 9].Value = "";
                        ws.Cells[3, 8, 3, 9].Merge = true;
                        ws.Cells[3, 10].Value = "Responde a los requerimientos";
                        ws.Cells[3, 10, 3, 11].Merge = true;
                        ws.Cells[3, 11].Value = "";
                        ws.Cells[3, 12].Value = "Valor del Parámetro";
                        ws.Cells[3, 13].Value = "Valor de la variable";
                        ws.Cells[3, 14].Value = "Valor Obtenido";
                        ws.Cells[3, 15].Value = "Valor Obtenido parametro";
                        ws.Cells[3, 15].Value = "Observaciones";

                        for (int i = 1; i < 16; i++)
                        {
                            ws.Cells[3, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws.Cells[3, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[3, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[3, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ws.Cells[3, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[3, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[3, i].Style.WrapText = true;
                        }
                        var col1 = ws.Column(1);
                        col1.Width = 6;
                        var col2 = ws.Column(2);
                        col2.Width = 45;
                        var col3 = ws.Column(3);
                        col3.Width = 8;
                        var col4 = ws.Column(4);
                        col4.Width = 32;
                        var col5 = ws.Column(5);
                        col5.Width = 32;

                        var col6 = ws.Column(6);
                        col6.Width = 5;
                        var col7 = ws.Column(7);
                        col7.Width = 5;
                        var col8 = ws.Column(8);
                        col8.Width = 5;
                        var col9 = ws.Column(9);
                        col9.Width = 5;
                        var col10 = ws.Column(10);
                        col10.Width = 10;
                        var col11 = ws.Column(11);
                        col11.Width = 10;

                        for (int i = 12; i < 15; i++)
                        {
                            var coli = ws.Column(i);
                            coli.Width = 13;
                        }
                        var col15 = ws.Column(15);
                        col15.Width = 20;

                        ws.Cells[4, 1, 4, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[4, 1, 4, 15].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws.Cells[4, 1, 4, 5].Merge = true;
                        ws.Cells[4, 1, 4, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[4, 1, 4, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[4, 1, 4, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[4, 1, 4, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        bool Intercalar = false;
                        for (int i = 6; i < 12; i++)
                        {
                            ws.Cells[4, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws.Cells[4, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[4, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[4, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ws.Cells[4, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[4, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[4, i].Style.Font.Bold = true;
                            ws.Cells[4, i].Style.Font.Size = 12;
                            if (Intercalar)
                            {
                                ws.Cells[4, i].Value = "NO";
                                Intercalar = false;
                            }
                            else
                            {
                                ws.Cells[4, i].Value = "SI";
                                Intercalar = true;
                            }
                        }
                        ws.Cells[4, 12, 4, 15].Merge = true;
                        ws.Cells[4, 12, 4, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[4, 12, 4, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[4, 12, 4, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[4, 12, 4, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws.View.FreezePanes(5, 1);


                    }
                    #endregion
                    ExcelWorksheet ws1 = wb.Workbook.Worksheets.Add("RESULTADO");
                    #region resultado


                    var fila1 = ws1.Row(1);
                    fila1.Height = 27;
                    ws1.Cells[1, 1].Style.Font.Bold = true;
                    ws1.Cells[1, 1].Style.Font.Size = 16;
                    ws1.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws1.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws1.Cells[1, 1].Value = "EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                    ws1.Cells[1, 1, 1, 5].Merge=true;


                    var fila2 = ws1.Row(2);
                    fila2.Height = 32;
                    ws1.Cells[2, 1, 2, 2].Merge=true;
                    for (int i = 1; i < 6; i++)
                    {
                        ws1.Cells[2, i].Style.Font.Bold = true;
                        ws1.Cells[2, i].Style.Font.Size = 12;
                        ws1.Cells[2, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[2, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws1.Cells[2, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[2, i].Style.Fill.BackgroundColor.SetColor(graycolor);


                        ws1.Cells[2, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[2, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[2, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[2, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[2, i].Style.WrapText = true;
                    }

                    ws1.Cells[2, 1].Value = "PILAR";
                    ws1.Cells[2, 3].Value = "VALOR OBTENIDO";
                    ws1.Cells[2, 4].Value = "VALOR PONDERADO";
                    ws1.Cells[2, 5].Value = "RESULTADO";

                    var acol1 = ws1.Column(1);
                    acol1.Width = 4;
                    var acol2 = ws1.Column(2);
                    acol2.Width = 59;
                    var acol3 = ws1.Column(3);
                    acol3.Width = 17;
                    var acol4 = ws1.Column(4);
                    acol4.Width = 17;
                    var acol5 = ws1.Column(5);
                    acol5.Width = 17;
                    int contR1 = 1;
                    int cont11 = 3;
                    decimal totalRes = 0;
                    decimal totalPond = 0;
                    decimal totalObt = 0;

                    foreach (var item in Formulario.ListaPilares)
                    {
                        int NumPilar = contR1;
                        string DescPilar = item.Descripcion;
                        decimal Obtenido = item.ValorObtenido;
                        decimal Ponderado = item.Valor_Ponderado;
                        decimal Resultado = Obtenido * Ponderado;
                        totalRes = totalRes + Resultado;
                        totalPond = totalPond + Ponderado;
                        totalObt = totalObt + Obtenido;

                        ws1.Cells[cont11, 1].Value = NumPilar;
                        ws1.Cells[cont11, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[cont11, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws1.Cells[cont11, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 1].Style.WrapText = true;

                        ws1.Cells[cont11, 2].Value = DescPilar;
                        ws1.Cells[cont11, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[cont11, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws1.Cells[cont11, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 2].Style.WrapText = true;

                        ws1.Cells[cont11, 3].Value = Obtenido;
                        ws1.Cells[cont11, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[cont11, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws1.Cells[cont11, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 3].Style.WrapText = true;
                        ws1.Cells[cont11, 3].Style.Numberformat.Format = "#,##0.0";

                        ws1.Cells[cont11, 4].Value = Ponderado;
                        ws1.Cells[cont11, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[cont11, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws1.Cells[cont11, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 4].Style.WrapText = true;
                        ws1.Cells[cont11, 4].Style.Numberformat.Format = "0.0%";

                        ws1.Cells[cont11, 5].Value = Resultado;
                        ws1.Cells[cont11, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[cont11, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws1.Cells[cont11, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[cont11, 5].Style.WrapText = true;
                        ws1.Cells[cont11, 5].Style.Numberformat.Format = "#,##0.0";

                        cont11++;
                        contR1++;
                    }


                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Merge=true;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.WrapText = true;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 1, cont11 + 1, 2].Style.Font.Bold = true;
                    ws1.Cells[cont11 + 1, 1].Value = "TOTAL";


                    ws1.Cells[cont11 + 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws1.Cells[cont11 + 1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws1.Cells[cont11 + 1, 3].Style.WrapText = true;
                    ws1.Cells[cont11 + 1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws1.Cells[cont11 + 1, 3].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws1.Cells[cont11 + 1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 3].Style.Font.Bold = true;
                    ws1.Cells[cont11 + 1, 3].Value = totalObt;
                    ws1.Cells[cont11 + 1, 3].Style.Numberformat.Format = "#,##0.0";

                    ws1.Cells[cont11 + 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws1.Cells[cont11 + 1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws1.Cells[cont11 + 1, 4].Style.WrapText = true;
                    ws1.Cells[cont11 + 1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws1.Cells[cont11 + 1, 4].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws1.Cells[cont11 + 1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 4].Style.Font.Bold = true;
                    ws1.Cells[cont11 + 1, 4].Value = totalPond;
                    ws1.Cells[cont11 + 1, 4].Style.Numberformat.Format = "0.0%";
                    
                    ws1.Cells[cont11 + 1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws1.Cells[cont11 + 1, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws1.Cells[cont11 + 1, 5].Style.WrapText = true;
                    ws1.Cells[cont11 + 1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws1.Cells[cont11 + 1, 5].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws1.Cells[cont11 + 1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws1.Cells[cont11 + 1, 5].Style.Font.Bold = true;
                    ws1.Cells[cont11 + 1, 5].Value = totalRes;
                    ws1.Cells[cont11 + 1, 5].Style.Numberformat.Format = "#,##0.0";

                    var Chart = ws1.Drawings.AddChart("chart", eChartType.Pie);
                    var series = Chart.Series.Add("E3: E8","B3: B8");
                    Chart.Border.Fill.Color = System.Drawing.Color.Gray;
                    Chart.Title.Text = "RESULTADOS EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                    Chart.SetSize(768, 491);
                    Chart.ShowDataLabelsOverMaximum = true;
                    Chart.SetPosition(1, 0, 7, 0);

                    #endregion
                    ExcelWorksheet ws2 = wb.Workbook.Worksheets.Add("REPORTE");
                    #region reporte

                    var ccol1 = ws2.Column(1);
                    ccol1.Width = 50;
                    var ccol2 = ws2.Column(2);
                    ccol2.Width = 30;
                    var ccol3 = ws2.Column(3);
                    ccol3.Width = 30;


                    int rowinit = 1;
                    foreach (var item in ListaPilaresRep1)
                    {
                        var filc1 = ws2.Row(rowinit);
                        filc1.Height = 27;
                        ws2.Cells[rowinit, 1].Style.Font.Bold = true;
                        ws2.Cells[rowinit, 1].Style.Font.Size = 16;
                        ws2.Cells[rowinit, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws2.Cells[rowinit, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws2.Cells[rowinit, 1].Value = item.Descripcion;
                        ws2.Cells[rowinit, 1, rowinit, 3].Merge = true;

                        rowinit++;

                        var filc2 = ws2.Row(rowinit);
                        filc2.Height = 32;
                        for (int i = 1; i < 4; i++)
                        {
                            ws2.Cells[rowinit, i].Style.Font.Bold = true;
                            ws2.Cells[rowinit, i].Style.Font.Size = 12;
                            ws2.Cells[rowinit, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws2.Cells[rowinit, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws2.Cells[rowinit, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws2.Cells[rowinit, i].Style.Fill.BackgroundColor.SetColor(graycolor);
                            ws2.Cells[rowinit, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.WrapText = true;
                        }
                        ws2.Cells[rowinit, 1].Value = "PARAMETRO";
                        ws2.Cells[rowinit, 2].Value = "VALOR ESPERADO";
                        ws2.Cells[rowinit, 3].Value = "VALOR OBTENIDO";
                        int rowtitulo = rowinit;



                        rowinit++;
                        int iniciosum = rowinit;
                        foreach (var item1 in item.ListaParametros)
                        {
                            for (int i1 = 1; i1 < 4; i1++)
                            {
                                ws2.Cells[rowinit, i1].Style.Font.Size = 12;
                                ws2.Cells[rowinit, i1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws2.Cells[rowinit, i1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                ws2.Cells[rowinit, i1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                ws2.Cells[rowinit, i1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                ws2.Cells[rowinit, i1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws2.Cells[rowinit, i1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                ws2.Cells[rowinit, i1].Style.WrapText = true;
                            }
                            ws2.Cells[rowinit, 1].Value = item1.ParametroDef;
                            ws2.Cells[rowinit, 2].Value = item1.Valor_Parametro;
                            ws2.Cells[rowinit, 3].Value = item1.Valor_obtenido;

                            ws2.Cells[rowinit, 2].Style.Numberformat.Format = "#,##0.0";
                            ws2.Cells[rowinit, 3].Style.Numberformat.Format = "#,##0.0";

                            rowinit++;
                        }

                        var filc3 = ws2.Row(rowinit);
                        filc3.Height = 16;
                        for (int i = 1; i < 4; i++)
                        {
                            ws2.Cells[rowinit, i].Style.Font.Bold = true;
                            ws2.Cells[rowinit, i].Style.Font.Size = 12;
                            ws2.Cells[rowinit, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws2.Cells[rowinit, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws2.Cells[rowinit, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws2.Cells[rowinit, i].Style.Fill.BackgroundColor.SetColor(graycolor);
                            ws2.Cells[rowinit, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws2.Cells[rowinit, i].Style.WrapText = true;
                        }
                        ws2.Cells[rowinit, 1].Value = "TOTAL";
                        ws2.Cells[rowinit, 2].Value = "VALOR ESPERADO";
                        ws2.Cells[rowinit, 3].Value = "VALOR OBTENIDO";

                        ws2.Cells[rowinit, 2].Style.Numberformat.Format = "#,##0.0";
                        ws2.Cells[rowinit, 2].Formula = "SUM(B"+ iniciosum.ToString() + ":B" + (rowinit - 1).ToString() + ")";

                        ws2.Cells[rowinit, 3].Style.Numberformat.Format = "#,##0.0";
                        ws2.Cells[rowinit, 3].Formula = "SUM(C" + iniciosum.ToString() + ":C" + (rowinit - 1).ToString() + ")";
                        rowinit++;
                        rowinit++;
                        

                        //var Charto2 = ws2.Drawings.AddChart("chart"+ rowinit + "", eChartType.BarClustered);
                        //var seriesto = Chart.Series.Add("C" + rowtitulo.ToString() + ": D" + rowtitulo.ToString() + "", "C" + iniciosum.ToString() + ": D" + (rowinit - 1).ToString() + "");
                        //Charto2.Border.Fill.Color = System.Drawing.Color.Gray;
                        //Charto2.Title.Text = item.Descripcion;
                        //Charto2.SetSize(500, 350);
                        //Charto2.ShowDataLabelsOverMaximum = true;
                        //Charto2.SetPosition(rowinit, 0, 7, 0);
                    }
                    #endregion



                    var filb1 = ws0.Row(1);
                    filb1.Height = 27;
                    ws0.Cells[1, 1].Style.Font.Bold = true;
                    ws0.Cells[1, 1].Style.Font.Size = 16;
                    ws0.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws0.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws0.Cells[1, 1].Value = "EVALUACIÓN DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL";
                    ws0.Cells[1, 1, 1, 4].Merge = true;


                    var filb2 = ws0.Row(2);
                    filb2.Height = 32;
                    for (int i = 1; i < 5; i++)
                    {
                        ws0.Cells[2,i].Style.Font.Bold = true;
                        ws0.Cells[2, i].Style.Font.Size = 12;
                        ws0.Cells[2, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws0.Cells[2, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws0.Cells[2, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws0.Cells[2, i].Style.Fill.BackgroundColor.SetColor(graycolor);
                        ws0.Cells[2, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[2, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[2, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[2, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[2, i].Style.WrapText = true;
                    }

                    for (int i = 1; i < 5; i++)
                    {
                        ws0.Cells[3,i].Style.Font.Size = 12;
                        ws0.Cells[3, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws0.Cells[3, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws0.Cells[3, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[3, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[3, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[3, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[3, i].Style.WrapText = true;
                    }

                    ws0.Cells[2, 1].Value = "FECHA DE REGISTRO";
                    ws0.Cells[2, 2].Value = "NIT";
                    ws0.Cells[2, 3].Value = "RAZÓN SOCIAL";
                    ws0.Cells[2, 4].Value = "SEDE EVALUADA";

                    var bcol1 = ws0.Column(1);
                    bcol1.Width = 17;
                    var bcol2 = ws0.Column(2);
                    bcol2.Width = 17;
                    var bcol3 = ws0.Column(3);
                    bcol3.Width = 50;
                    var bcol4 = ws0.Column(4);
                    bcol4.Width = 30;

                    ws0.Cells[3, 1].Value = FechaRegistro;
                    ws0.Cells[3, 2].Value = Nit;
                    ws0.Cells[3, 3].Value = RazonSocial;
                    ws0.Cells[3, 4].Value = Sede;


                    //var ms = new System.IO.MemoryStream();
                    //wb.SaveAs(ms);
                    //ms.WriteTo(Response.OutputStream);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaResultadoSegVial" + Fecha + ".xlsx");
                    }



                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult ValoresAgregados()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.RazonSocial = usuarioActual.RazonSocialEmpresa;
            ViewBag.Nit = usuarioActual.NitEmpresa;
            var ListaSedes = new List<EDSede>();
            ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", 0);
            //ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", 0);
            List<EDSegVialParametro> PlanVial = LNPromocionPrevencion.ConsultarParametros(usuarioActual.IdEmpresa);
            decimal suma = 0;
            foreach (var item in PlanVial)
            {
                suma += item.Valor_Parametro;
            }
            suma = Math.Round(suma, 1);



            ViewBag.Total = suma;
            return View(PlanVial);
        }

        [HttpGet]
        public ActionResult ListaVariables(string IdParametro)
        {
            List<EDSegVialDetalle> ListaVariables = new List<EDSegVialDetalle>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int IdParametroint = 0;
            if (int.TryParse(IdParametro,out IdParametroint))
            {
                ViewBag.RazonSocial = usuarioActual.RazonSocialEmpresa;
                ViewBag.Nit = usuarioActual.NitEmpresa;
                var ListaSedes = new List<EDSede>();
                ListaSedes = LNPromocionPrevencion.ObtenernerSedesPorEmpresa(usuarioActual.IdEmpresa);
                ViewBag.Pk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", 0);
                List<EDSegVialParametro> PlanVial = LNPromocionPrevencion.ConsultarParametros(usuarioActual.IdEmpresa);
                EDSegVialParametro EDSegVialParametro = new EDSegVialParametro();
                EDSegVialParametro = PlanVial.Where(s => s.Pk_Id_SegVialParametro == IdParametroint).FirstOrDefault();
                if (EDSegVialParametro!=null)
                {
                    ListaVariables = LNPromocionPrevencion.ConsultarVariables(EDSegVialParametro.Pk_Id_SegVialParametro);
                }
            }

            return View(ListaVariables);
        }

        [HttpGet]
        public ActionResult AgregarParametro()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDSegVialParametro> PlanVial = LNPromocionPrevencion.ConsultarParametros(usuarioActual.IdEmpresa);
            decimal suma = 0;
            foreach (var item in PlanVial)
            {
                suma += item.Valor_Parametro;
            }
            suma = Math.Round(suma, 1);



            ViewBag.Total = suma;
            return View(PlanVial);
        }


        [HttpPost]
        public ActionResult AgregarParametro(EDSegVialParametro EDSegVialParametro, List<EDSegVialDetalle> Lista)
        {
            bool Probar = false;
            string Estado = "No se ha creado el parametro, revise la información suministrada";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar });
            }

            if (EDSegVialParametro!=null)
            {
                if (EDSegVialParametro.Numeral!=null)
                {
                    string valordigitado = EDSegVialParametro.Numeral;
                    string valorComa = EDSegVialParametro.Numeral.Replace(".", ",");
                    string valorPunto = EDSegVialParametro.Numeral.Replace(",", ".");

                    decimal deccoma = 0;
                    decimal decpunto = 0;
                    decimal decdigitado = 0;
                    decimal decguardar = 0;

                    if (decimal.TryParse(valordigitado, out decdigitado))
                    {

                    }
                    if (decimal.TryParse(valorComa, out deccoma))
                    {
                        if (valorComa== deccoma.ToString())
                        {
                            decguardar = deccoma;
                        }
                        if (valorPunto == deccoma.ToString())
                        {
                            decguardar = deccoma;
                        }
                    }
                    if (decimal.TryParse(valorPunto, out decpunto))
                    {
                        if (valorComa == decpunto.ToString())
                        {
                            decguardar = decpunto;
                        }
                        if (valorPunto == decpunto.ToString())
                        {
                            decguardar = decpunto;
                        }
                    }

                    if (decguardar > 0 && EDSegVialParametro.ParametroDef!=null)
                    {
                        decguardar=Math.Round(decguardar, 1);
                        if (decguardar<=0)
                        {
                            Estado = "El valor del parametro que desea agregar no puede ser inferior o igual a 0";
                            return Json(new { Estado, Probar });
                        }
                        EDSegVialParametro EDSegVialParametro1 = new EDSegVialParametro();
                        EDSegVialParametro1.Valor_Parametro = decguardar;
                        EDSegVialParametro1.disabled = true;
                        EDSegVialParametro1.Numeral = "";
                        EDSegVialParametro1.ParametroDef = EDSegVialParametro.ParametroDef;
                        EDSegVialParametro1.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                        EDSegVialParametro1.ListaDetalles = Lista;
                        if (EDSegVialParametro1.ListaDetalles==null)
                        {
                            Estado = "Para guardar un parametro debe tener al menos una variable, por favor agrege las variables que componen este parametro";
                            return Json(new { Estado, Probar });
                        }
                        else
                        {
                            if (EDSegVialParametro1.ListaDetalles.Count==0)
                            {
                                Estado = "Para guardar un parametro debe tener al menos una variable, por favor agrege las variables que componen este parametro";
                                return Json(new { Estado, Probar });
                            }
                        }
                        List<EDSegVialParametro> PlanVial = LNPromocionPrevencion.ConsultarParametros(usuarioActual.IdEmpresa);
                        decimal suma = 0;
                        foreach (var item in PlanVial)
                        {
                            suma += item.Valor_Parametro;
                        }
                        suma += decguardar;
                        if (suma>100)
                        {
                            Estado = "La suma de los parámetros de este pilar no puede ser mayor a 100, verifique la tabla de parámetros ya guardados y digite por favor el valor del parámetro que desea guardar";
                            return Json(new { Estado, Probar });
                        }

                        bool ProbarGuardado = LNPromocionPrevencion.CrearParametro(EDSegVialParametro1);
                        if (ProbarGuardado)
                        {
                            Probar = true;
                            return Json(new { url = Url.Action("ValoresAgregados", "PromocionPrevencion"), Estado, Probar });
                        }
                    }
                    else
                    {
                        if (decguardar <= 0)
                        {
                            Estado = "El valor del parametro que desea agregar no puede ser inferior o igual a 0";
                            return Json(new { Estado, Probar });
                        }
                        return Json(new { Estado, Probar });
                    }


                }
                else
                {
                    Estado = "Por favor digite el valor de este parámetro";
                    return Json(new { Estado, Probar });
                }
            }

            return Json(new { Estado, Probar });
        }


        [HttpPost]
        public JsonResult EliminarParametro(string IdParametro)
        {

            bool probar = false;
            string resultado = "El parámetro no ha podido ser eliminado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdElemento = 0;
            bool probarNumero = int.TryParse(IdParametro, out IdElemento);
            if (IdElemento != 0)
            {
                bool ExisteResultado = LNPromocionPrevencion.ExisteParametroResultado(IdElemento, usuarioActual.IdEmpresa);
                if (!ExisteResultado)
                {
                    bool BorraElemento = LNPromocionPrevencion.OcultarParametro(IdElemento, usuarioActual.IdEmpresa);
                    if (BorraElemento == false)
                    {
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                    }
                    probar = true;
                    resultado = "El parámetro se ha eliminado correctamente";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bool BorraElemento = LNPromocionPrevencion.EliminarParametro(IdElemento, usuarioActual.IdEmpresa);
                    if (BorraElemento == false)
                    {
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                    }
                    probar = true;
                    resultado = "El parámetro se ha eliminado correctamente";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

                    
                }
                
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}



