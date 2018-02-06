using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Logica.Aplicacion;
using SG_SST.Models.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Cuestionario
{
    public class BateriaController : Controller
    {
        LNBateria LNBateria = new LNBateria();
        [HttpGet]
        public ActionResult IntralaboralA(string formdata,string pagina, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            ViewBag.key = formdata;
            ViewBag.form = form;
            ViewBag.check9a = "";
            ViewBag.check9b = "";
            ViewBag.check10a = "";
            ViewBag.check10b = "";

            int paginaInt = 0;
            int FormInt = 0;
            if (int.TryParse(pagina, out paginaInt) && int.TryParse(form, out FormInt))
            {
                if (paginaInt!=0 && FormInt!=0)
                {
                    if (FormInt!=1)
                    {
                        return HttpNotFound();
                    }
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario!=null)
                    {
                        bool abierto= CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Pagina = paginaInt;
                        LNBateria.EditarSegunCheck(EDBateriaUsuario.Pk_Id_BateriaUsuario, EDBateriaUsuario.CheckPag9, EDBateriaUsuario.CheckPag10);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;

                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            bool TieneExtra = LNBateria.TieneExtra(EDBateriaUsuario);
                            if (TieneExtra)
                            {
                                return RedirectToAction("Extralaboral", new { formdata = formdata, pagina = "1", form = "1" });
                            }
                            else
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = EDBateriaUsuario.NombreEncuesta });
                            }
                            
                        }
                        else
                        {
                            //Cedula Confirmada??
                            bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuario);
                            if (CedulaConf==false)
                            {
                                return RedirectToAction("Index", new { formdata = formdata, form= form });
                            }
                            //Acepto Participar??
                            bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                            if (Aceptacion == false)
                            {
                                return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                            }
                            if (EDBateriaUsuario.ConfirmacionParticipacion == "Rechazado")
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = EDBateriaUsuario.NombreEncuesta });
                            }


                            //LLeno la encuesta Principal??
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            if (EDBateriaInicial!=null)
                            {
                                int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                                if (EncuestaInicialPag != 5)
                                {
                                    ViewBag.key = formdata;
                                    ViewBag.form = form;
                                    ViewBag.pagina = EncuestaInicialPag.ToString();
                                    return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                                }
                                else
                                {
                                    //Ir a Cuestionario Forma A
                                    int requestPage = paginaInt;
                                    int allowedPage = LNBateria.PaginaIntralaboralA(formdata, EDBateriaUsuario);
                                    if (requestPage<= allowedPage)
                                    {
                                        if (EDBateriaUsuario.CheckPag9!=null)
                                        {
                                            if (EDBateriaUsuario.CheckPag9=="Si")
                                            {
                                                ViewBag.check9a = "checked=\"checked\"";
                                            }
                                            if (EDBateriaUsuario.CheckPag9 == "No")
                                            {
                                                ViewBag.check9b = "checked=\"checked\"";
                                                foreach (var item in ListaCuestionario)
                                                {
                                                    if (item.Orden >= 106 && item.Orden <= 114)
                                                    {
                                                        item.Valor = 0;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var item in ListaCuestionario)
                                            {
                                                if (item.Orden>= 106 && item.Orden <= 114)
                                                {
                                                    item.Valor = 0;
                                                }
                                            }
                                        }
                                        if (EDBateriaUsuario.CheckPag10 != null)
                                        {
                                            if (EDBateriaUsuario.CheckPag10 == "Si")
                                            {
                                                ViewBag.check10a = "checked=\"checked\"";
                                            }
                                            if (EDBateriaUsuario.CheckPag10 == "No")
                                            {
                                                foreach (var item in ListaCuestionario)
                                                {
                                                    if (item.Orden >= 115 && item.Orden <= 123)
                                                    {
                                                        item.Valor = 0;
                                                    }
                                                }
                                                ViewBag.check10b = "checked=\"checked\"";
                                            }
                                        }
                                        else
                                        {
                                            foreach (var item in ListaCuestionario)
                                            {
                                                if (item.Orden >= 115 && item.Orden <= 123)
                                                {
                                                    item.Valor = 0;
                                                }
                                            }
                                        }
                                        return View(ListaCuestionario);
                                    }
                                    else
                                    {
                                        return RedirectToAction("IntralaboralA", new { formdata = formdata, pagina = allowedPage, form = form });
                                    }
                                    
                                }
                            }
                            else
                            {
                                //Ir a la encuesta inicial
                                return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                            }
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult IntralaboralB(string formdata, string pagina, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            ViewBag.key = formdata;
            ViewBag.form = form;
            ViewBag.check9a = "";
            ViewBag.check9b = "";
            ViewBag.check10a = "";
            ViewBag.check10b = "";

            int paginaInt = 0;
            int FormInt = 0;
            if (int.TryParse(pagina, out paginaInt) && int.TryParse(form, out FormInt))
            {
                if (paginaInt != 0 && FormInt != 0)
                {
                    if (FormInt != 2)
                    {
                        return HttpNotFound();
                    }
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Pagina = paginaInt;
                        LNBateria.EditarSegunCheck(EDBateriaUsuario.Pk_Id_BateriaUsuario, EDBateriaUsuario.CheckPag9, EDBateriaUsuario.CheckPag10);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;

                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            bool TieneExtra = LNBateria.TieneExtra(EDBateriaUsuario);
                            if (TieneExtra)
                            {
                                return RedirectToAction("Extralaboral", new { formdata = formdata, pagina = "1", form = "2" });
                            }
                            else
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = EDBateriaUsuario.NombreEncuesta });
                            }
                        }
                        else
                        {
                            //Cedula Confirmada??
                            bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuario);
                            if (CedulaConf == false)
                            {
                                return RedirectToAction("Index", new { formdata = formdata, form = form });
                            }
                            //Acepto Participar??
                            bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                            if (Aceptacion == false)
                            {
                                return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                            }
                            if (EDBateriaUsuario.ConfirmacionParticipacion == "Rechazado")
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B" });
                            }


                            //LLeno la encuesta Principal??
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            if (EDBateriaInicial != null)
                            {
                                int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                                if (EncuestaInicialPag != 5)
                                {
                                    ViewBag.key = formdata;
                                    ViewBag.form = form;
                                    ViewBag.pagina = EncuestaInicialPag.ToString();
                                    return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                                }
                                else
                                {
                                    //Ir a Cuestionario Forma B
                                    int requestPage = paginaInt;
                                    int allowedPage = LNBateria.PaginaIntralaboralB(formdata, EDBateriaUsuario);
                                    if (requestPage <= allowedPage)
                                    {
                                        if (EDBateriaUsuario.CheckPag9 != null)
                                        {
                                            if (EDBateriaUsuario.CheckPag9 == "Si")
                                            {
                                                ViewBag.check9a = "checked=\"checked\"";
                                            }
                                            if (EDBateriaUsuario.CheckPag9 == "No")
                                            {
                                                ViewBag.check9b = "checked=\"checked\"";
                                                foreach (var item in ListaCuestionario)
                                                {
                                                    if (item.Orden >= 106 && item.Orden <= 114)
                                                    {
                                                        item.Valor = 0;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var item in ListaCuestionario)
                                            {
                                                if (item.Orden >= 106 && item.Orden <= 114)
                                                {
                                                    item.Valor = 0;
                                                }
                                            }
                                        }
                                        if (EDBateriaUsuario.CheckPag10 != null)
                                        {
                                            if (EDBateriaUsuario.CheckPag10 == "Si")
                                            {
                                                ViewBag.check10a = "checked=\"checked\"";
                                            }
                                            if (EDBateriaUsuario.CheckPag10 == "No")
                                            {
                                                foreach (var item in ListaCuestionario)
                                                {
                                                    if (item.Orden >= 115 && item.Orden <= 123)
                                                    {
                                                        item.Valor = 0;
                                                    }
                                                }
                                                ViewBag.check10b = "checked=\"checked\"";
                                            }
                                        }
                                        else
                                        {
                                            foreach (var item in ListaCuestionario)
                                            {
                                                if (item.Orden >= 115 && item.Orden <= 123)
                                                {
                                                    item.Valor = 0;
                                                }
                                            }
                                        }
                                        return View(ListaCuestionario);
                                    }
                                    else
                                    {
                                        return RedirectToAction("IntralaboralB", new { formdata = formdata, pagina = allowedPage, form = form });
                                    }

                                }
                            }
                            else
                            {
                                //Ir a la encuesta inicial
                                return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                            }
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Extralaboral(string formdata, string pagina, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            EDBateriaUsuario EDBateriaUsuarioPrincipal = new EDBateriaUsuario();
            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            ViewBag.key = formdata;
            ViewBag.form = form;
            ViewBag.check9a = "";
            ViewBag.check9b = "";
            ViewBag.check10a = "";
            ViewBag.check10b = "";

            int paginaInt = 0;
            int FormInt = 0;
            if (int.TryParse(pagina, out paginaInt) && int.TryParse(form, out FormInt))
            {
                if (paginaInt != 0 && FormInt != 0)
                {
                    if (FormInt != 1)
                    {
                        if (FormInt != 2)
                        {
                            return HttpNotFound();
                        }
                        
                    }


                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(formdata, FormInt);
                    EDBateriaUsuarioPrincipal= LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        //Sesion Abierta
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }

                        ViewBag.Pagina = paginaInt;
                        LNBateria.EditarSegunCheck(EDBateriaUsuario.Pk_Id_BateriaUsuario, EDBateriaUsuario.CheckPag9, EDBateriaUsuario.CheckPag10);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 3);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;

                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            if (FormInt==1)
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral Forma A y Extralaboral" });
                            }
                            if (FormInt == 2)
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral Forma B y Extralaboral" });
                            }
                            return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral" });
                        }
                        else
                        {
                            //Cedula Confirmada??
                            bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuarioPrincipal);
                            if (CedulaConf == false)
                            {
                                return RedirectToAction("Index", new { formdata = formdata, form = form });
                            }
                            //Acepto Participar??
                            bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                            if (Aceptacion == false)
                            {
                                return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                            }
                            if (EDBateriaUsuario.ConfirmacionParticipacion == "Rechazado")
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral" });
                            }


                            //LLeno la encuesta Principal??
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            if (EDBateriaInicial != null)
                            {
                                int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                                if (EncuestaInicialPag != 5)
                                {
                                    ViewBag.key = formdata;
                                    ViewBag.form = form;
                                    ViewBag.pagina = EncuestaInicialPag.ToString();
                                    return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                                }
                                else
                                {
                                    //Ir a Cuestionario Forma B
                                    int requestPage = paginaInt;
                                    int allowedPage = LNBateria.PaginaExtralaboral(formdata, EDBateriaUsuario);
                                    if (requestPage <= allowedPage)
                                    {
                                        return View(ListaCuestionario);
                                    }
                                    else
                                    {
                                        return RedirectToAction("Extralaboral", new { formdata = formdata, pagina = allowedPage, form = form });
                                    }
                                }
                            }
                            else
                            {
                                //Ir a la encuesta inicial
                                return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                            }
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Estres(string formdata, string pagina, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            ViewBag.key = formdata;
            ViewBag.form = form;
            ViewBag.check9a = "";
            ViewBag.check9b = "";
            ViewBag.check10a = "";
            ViewBag.check10b = "";

            int paginaInt = 0;
            int FormInt = 0;
            if (int.TryParse(pagina, out paginaInt) && int.TryParse(form, out FormInt))
            {
                if (paginaInt != 0 && FormInt != 0)
                {
                    if (FormInt != 4)
                    {
                        return HttpNotFound();
                    }
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }

                        ViewBag.Pagina = paginaInt;
                        LNBateria.EditarSegunCheck(EDBateriaUsuario.Pk_Id_BateriaUsuario, EDBateriaUsuario.CheckPag9, EDBateriaUsuario.CheckPag10);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;

                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Estrés" });
                        }
                        else
                        {
                            //Cedula Confirmada??
                            bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuario);
                            if (CedulaConf == false)
                            {
                                return RedirectToAction("Index", new { formdata = formdata, form = form });
                            }
                            //Acepto Participar??
                            bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                            if (Aceptacion == false)
                            {
                                return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                            }
                            if (EDBateriaUsuario.ConfirmacionParticipacion == "Rechazado")
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Estrés" });
                            }


                            //LLeno la encuesta Principal??
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            if (EDBateriaInicial != null)
                            {
                                int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                                if (EncuestaInicialPag != 5)
                                {
                                    ViewBag.key = formdata;
                                    ViewBag.form = form;
                                    ViewBag.pagina = EncuestaInicialPag.ToString();
                                    return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                                }
                                else
                                {
                                    //Ir a Cuestionario Forma B
                                    int requestPage = paginaInt;
                                    int allowedPage = LNBateria.PaginaEstres(formdata, EDBateriaUsuario);
                                    if (requestPage <= allowedPage)
                                    {
                                        return View(ListaCuestionario);
                                    }
                                    else
                                    {
                                        return RedirectToAction("Estres", new { formdata = formdata, pagina = allowedPage, form = form });
                                    }
                                }
                            }
                            else
                            {
                                //Ir a la encuesta inicial
                                return RedirectToAction("Inicial", new { formdata = formdata, form = form });
                            }
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult IntralaboralAload(string formdata, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();


            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {

                if (FormInt != 0)
                {

                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }
                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A" });
                        }

                        ViewBag.Pagina = "0";
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;


                        if (EncuestaCompleta)
                        {
                            //Redirigir a pagina de exito;
                        }
                        else
                        {
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                            if (EncuestaInicialPag != 5)
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                ViewBag.pagina = EncuestaInicialPag.ToString();
                                return View(EDBateriaInicial);
                            }
                            else
                            {
                                int pagC = LNBateria.PaginaIntralaboralA(formdata, EDBateriaUsuario);
                                ViewBag.Pagina = pagC.ToString();
                                return RedirectToAction("IntralaboralA", new { formdata = formdata, pagina= pagC, form = form });
                                //consultar en que pregunta quedo
                                return View(ListaCuestionario);
                            }
                        }


                        //Esta completa la encuesta?
                        // SI:Ir a pagina de success
                        //NO: Preguntar: Si lleno la encuesta inicial?
                        // SI: consultar la pregunta en la que quedo y cargar - NO: ir a formulario inicial


                        //Entrar segun la pagina en la que quedo
                        return View(ListaCuestionario);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult IntralaboralBload(string formdata, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();


            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {

                if (FormInt != 0)
                {

                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }
                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B" });
                        }

                        ViewBag.Pagina = "0";
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;


                        if (EncuestaCompleta)
                        {
                            //Redirigir a pagina de exito;
                        }
                        else
                        {
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                            if (EncuestaInicialPag != 5)
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                ViewBag.pagina = EncuestaInicialPag.ToString();
                                return View(EDBateriaInicial);
                            }
                            else
                            {
                                int pagC = LNBateria.PaginaIntralaboralB(formdata, EDBateriaUsuario);
                                ViewBag.Pagina = pagC.ToString();
                                return RedirectToAction("IntralaboralB", new { formdata = formdata, pagina = pagC, form = form });
                            }
                        }
                        return View(ListaCuestionario);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Extralaboralload(string formdata, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();


            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {

                if (FormInt != 0)
                {

                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }
                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral" });
                        }

                        ViewBag.Pagina = "0";
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;


                        if (EncuestaCompleta)
                        {
                            //Redirigir a pagina de exito;
                        }
                        else
                        {
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                            if (EncuestaInicialPag != 5)
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                ViewBag.pagina = EncuestaInicialPag.ToString();
                                return View(EDBateriaInicial);
                            }
                            else
                            {
                                int pagC = LNBateria.PaginaIntralaboralB(formdata, EDBateriaUsuario);
                                ViewBag.Pagina = pagC.ToString();
                                return RedirectToAction("Extralaboral", new { formdata = formdata, pagina = pagC, form = form });
                            }
                        }
                        return View(ListaCuestionario);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Estresload(string formdata, string form)
        {
            EDCuestionarioA EDCuestionarioA = new EDCuestionarioA();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();


            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.Pagina = 0;
            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {

                if (FormInt != 0)
                {

                    EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                    if (EDBateriaUsuario != null)
                    {
                        bool abierto = CerradoAbierto(EDBateriaUsuario);
                        if (!abierto)
                        {
                            return HttpNotFound();
                        }
                        bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                        if (EncuestaCompleta)
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = "Cuestionario de Factores de Estrés" });
                        }

                        ViewBag.Pagina = "0";
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, FormInt);
                        EDCuestionarioA.ListaCuestionario = ListaCuestionario;
                        int IdUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;


                        if (EncuestaCompleta)
                        {
                            //Redirigir a pagina de exito;
                        }
                        else
                        {
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            int EncuestaInicialPag = LNBateria.NumeroPagina(EDBateriaInicial);
                            if (EncuestaInicialPag != 5)
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                ViewBag.pagina = EncuestaInicialPag.ToString();
                                return View(EDBateriaInicial);
                            }
                            else
                            {
                                int pagC = LNBateria.PaginaIntralaboralB(formdata, EDBateriaUsuario);
                                ViewBag.Pagina = pagC.ToString();
                                return RedirectToAction("Estres", new { formdata = formdata, pagina = pagC, form = form });
                            }
                        }
                        return View(ListaCuestionario);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Inicial(string formdata, string form)
        {
            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
            ViewBag.key = "";
            ViewBag.form = "";
            ViewBag.pagina = "";
            string NombreForm = "";

            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {

                EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, FormInt);
                if (EDBateriaUsuario != null)
                {
                    bool abierto = CerradoAbierto(EDBateriaUsuario);
                    if (!abierto)
                    {
                        return HttpNotFound();
                    }
                    bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);

                    if (FormInt == 1)
                    {
                        NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                    }
                    if (FormInt == 2)
                    {
                        NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                    }
                    if (FormInt == 3)
                    {
                        NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    }
                    if (FormInt == 4)
                    {
                        NombreForm = "Cuestionario de Factores de Estrés";
                    }
                    if (EncuestaCompleta)
                    {
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario != 0)
                            {
                                bool EncuestaCompleta1 = LNBateria.EncuestaCompleta(EDBateriaUsuarioExtra);
                                if (EncuestaCompleta1)
                                {
                                    return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                                }
                            }
                            else
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                            }
                        }
                        else
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                        }
                    }
                    bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuario);
                    if (CedulaConf == false)
                    {
                        return RedirectToAction("Index", new { formdata = formdata, form = form });
                    }
                    //Acepto Participar??
                    bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                    if (Aceptacion == false)
                    {
                        return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                    }
                    else
                    {
                        if (EDBateriaUsuarioExtra != null && EncuestaCompleta)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario != 0)
                            {
                                bool Aceptacion1 = ConfirmarParticipacion(EDBateriaUsuarioExtra);
                                if (!Aceptacion1)
                                {
                                    return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                                }
                            }
                        }
                        if (EncuestaCompleta)
                        {
                            //Devuelve la encuesta secundaria
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario);
                            if (EDBateriaInicial != null)
                            {

                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                int Rpage = LNBateria.NumeroPagina(EDBateriaInicial);
                                ViewBag.pagina = Rpage.ToString();
                                if (Rpage == 5)
                                {
                                    return RedirectToAction("Extralaboralload", new { formdata = formdata, form = form });
                                }
                                return View(EDBateriaInicial);
                            }
                            else
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                ViewBag.pagina = "1";
                                return View(EDBateriaInicial);
                            }
                        }
                        else
                        {
                            //Devuelve la encuesta principal
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            if (EDBateriaInicial != null)
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                int Rpage = LNBateria.NumeroPagina(EDBateriaInicial);
                                ViewBag.pagina = Rpage.ToString();
                                if (Rpage == 5)
                                {
                                    if (FormInt == 1)
                                    {
                                        return RedirectToAction("IntralaboralAload", new { formdata = formdata, form = form });
                                    }
                                    if (FormInt == 2)
                                    {
                                        return RedirectToAction("IntralaboralBload", new { formdata = formdata, form = form });
                                    }
                                    if (FormInt == 3)
                                    {
                                        return RedirectToAction("Extralaboralload", new { formdata = formdata, form = form });
                                    }
                                    if (FormInt == 4)
                                    {
                                        return RedirectToAction("Estresload", new { formdata = formdata, form = form });
                                    }
                                }
                                return View(EDBateriaInicial);
                            }
                            else
                            {
                                ViewBag.key = formdata;
                                ViewBag.form = form;
                                ViewBag.pagina = "1";

                                return View(EDBateriaInicial);
                            }
                        }
                    }
                }
                else
                {
                    return HttpNotFound();
                }

            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult InicialExtra(string formdata, string form)
        {
            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            ViewBag.key = "";
            ViewBag.form = "";
            ViewBag.pagina = "";
            string NombreForm = "";

            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {

                EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(formdata, FormInt);
                if (EDBateriaUsuario != null)
                {
                    bool abierto = CerradoAbierto(EDBateriaUsuario);
                    if (!abierto)
                    {
                        return HttpNotFound();
                    }
                    bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";

                    if (EncuestaCompleta)
                    {
                        return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                    }
                    //Acepto Participar??
                    bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                    if (Aceptacion == false)
                    {
                        return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                    }

                    EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                    if (EDBateriaInicial != null)
                    {

                        ViewBag.key = formdata;
                        ViewBag.form = form;
                        int Rpage = LNBateria.NumeroPagina(EDBateriaInicial);
                        ViewBag.pagina = Rpage.ToString();
                        if (Rpage == 5)
                        {
                            return RedirectToAction("Extralaboralload", new { formdata = formdata, form = form });
                        }
                        return View(EDBateriaInicial);
                    }
                    else
                    {
                        ViewBag.key = formdata;
                        ViewBag.form = form;
                        ViewBag.pagina = "1";

                        return View(EDBateriaInicial);
                    }


                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpGet]
        public ActionResult InicialPagina(string formdata, string form, string pag)
        {
            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
            ViewBag.key = "";
            ViewBag.form = "";
            ViewBag.pagina = "";
            string NombreForm = "";

            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {
                if (FormInt == 1)
                {
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                }
                if (FormInt == 2)
                {
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                }
                if (FormInt == 3)
                {
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                }
                if (FormInt == 4)
                {
                    NombreForm = "Cuestionario de Factores de Estrés";
                }

                EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, FormInt);

                if (EDBateriaUsuario != null)
                {
                    bool abierto = CerradoAbierto(EDBateriaUsuario);
                    if (!abierto)
                    {
                        return HttpNotFound();
                    }
                    bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                    if (EncuestaCompleta)
                    {
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario != 0)
                            {
                                bool EncuestaCompleta1 = LNBateria.EncuestaCompleta(EDBateriaUsuarioExtra);
                                if (EncuestaCompleta1)
                                {
                                    return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                                }
                            }
                            else
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                            }
                        }
                        else
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                        }
                    }
                    bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuario);
                    if (CedulaConf == false)
                    {
                        return RedirectToAction("Index", new { formdata = formdata, form = form });
                    }
                    //Acepto Participar??
                    EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                    
                    bool Aceptacion = ConfirmarParticipacion(EDBateriaUsuario);
                    if (Aceptacion == false)
                    {
                        return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                    }
                    else
                    {
                        if (EDBateriaUsuarioExtra != null && EncuestaCompleta)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario != 0)
                            {
                                bool Aceptacion1 = ConfirmarParticipacion(EDBateriaUsuarioExtra);
                                if (!Aceptacion1)
                                {
                                    return RedirectToAction("Inicializar", new { formdata = formdata, form = form });
                                }
                                else
                                {
                                    EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario);
                                }
                            }
                        }
                    }

                    

                    ViewBag.key = formdata;
                    ViewBag.form = form;
                    ViewBag.pagina = pag;
                    int pagint = 0;
                    if (int.TryParse(pag, out pagint))
                    {
                        int pageRequest = pagint;
                        int pageAllowed = LNBateria.NumeroPagina(EDBateriaInicial);
                        if (pageRequest <= pageAllowed)
                        {
                            return View("Inicial", EDBateriaInicial);
                            
                        }
                        else
                        {
                            return RedirectToAction("InicialPagina", new { formdata = formdata, form = form, pag = pageAllowed.ToString() });
                        }
                        
                    }
                    else
                    {
                        return HttpNotFound();
                    }


                    
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpGet]
        public ActionResult Inicializar(string formdata, string form)
        {
            string NombreForm = "";
            ViewBag.Extra = false;
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {
                List<EDBateria> Baterias = LNBateria.ConsultarBaterias();
                EDBateria Bateriausada = Baterias.Where(s => s.Pk_Id_Bateria == FormInt).FirstOrDefault();
                string textoInformacion = Bateriausada.Descripcion;
                string textoAceptacion = "El presente documento se elabora de conformidad con la Resolución 2646 de 2008, mediante la cual se establecen disposiciones y se definen responsabilidades para la identificación, evaluación, prevención, intervención y monitoreo permanente de la exposición a factores de riesgo psicosocial en el trabajo y para la determinación del origen de las patologías causadas por el estrés laboral. Debido a lo anterior, para usted poder participar en esta evaluación, debe estar de acuerdo con los siguientes puntos, en caso contrario puede retirarse del proceso de evaluación, no obstante, es importante que tenga en cuenta que la salud laboral es responsabilidad no sólo del empleador sino que es una obligación y un derecho del trabajador en pro de velar por el cuidado de su salud y la prevención de los riesgos a los cuales está expuesto en el ambiente laboral.";
                string textoAceptacion1 = "1. Esta evaluación forma parte de las actividades del Sistema de Gestión de Seguridad y Salud en el Trabajo de la empresa, para dar cumplimiento a lo establecido en la resolución 2646 de 2008. 2. La evaluación consiste en la aplicación de cuestionarios donde se realizarán algunas preguntas sobre las condiciones del trabajo al interior de la organización, las condiciones de vida del trabajador por fuera de la organización, esto para evaluar el riesgo psicosocial. 3. La información recolectada a través de diferentes instrumentos ayudará al empleador a tomar mejores decisiones sobre acciones de intervención y control de los posibles factores de riesgo que se identifiquen dentro del ámbito psicosocial. 4. La información recolectada es de carácter confidencial y es sometida a reserva informe con lo establecido en la ley 1090 de 2006, esta información será conocida y manejada por un psicólogo con posgrado en salud ocupacional, con licencia vigente de prestación de servicios en psicología ocupacional para la identificación, análisis e intervención, quienes harán uso responsable de la información de acuerdo a la normatividad 5. De acuerdo a la resolución 8430 de 1993 este procedimiento es categorizado como un procedimiento sin riesgo alguno para las condiciones biologicas, fisiológicas, psicológicas o sociales del evaluado.";
                string textoAceptacion2 = "Al dar click en la opción SI DESEO DILIGENCIAR LA ENCUESTA, declaro y certifico que deseo participar voluntariamente en este proceso evaluativo de factores de riesgo psicosocial y doy constancia que he entendido las condiciones y objetivos de la evaluación que se va a realizar, que estoy satisfecho(a) con la información entregada sobre el proceso de evaluación, que la he recibido en un lenguaje claro y sencillo, y me han dado la oportunidad de preguntar y resolver las dudas a satisfacción a través del Psicólogo con posgrado en salud ocupacional, con licencia vigente de prestación de servicios en psicología ocupacional de la empresa. Si no estoy de acuerdo puedo dar click en la opción NO DESEO REALIZAR LA ENCUESTA, y el sistema finalizará su participación.";
                string textoInstrucciones = Bateriausada.ModalidadesAplicacion;

                ViewBag.Info = textoInformacion;
                ViewBag.acep = textoAceptacion;
                ViewBag.acep1 = textoAceptacion1;
                ViewBag.acep2 = textoAceptacion2;
                ViewBag.instr = textoInstrucciones;
                ViewBag.extraStr = "";

                ViewBag.key = formdata;
                ViewBag.form = form;

                if (FormInt == 1)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                }
                if (FormInt == 2)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                }
                if (FormInt == 3)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                }
                if (FormInt == 4)
                {
                    ViewBag.formT = "Cuestionario de Factores de Estrés";
                    NombreForm = "Cuestionario de Factores de Estrés";
                }
                
                EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, FormInt);
                if (EDBateriaUsuario!=null)
                {
                    bool TieneExtra = LNBateria.TieneExtra(EDBateriaUsuario);
                    if (TieneExtra)
                    {
                        ViewBag.Extra = true;
                        ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        ViewBag.extraStr = "true";
                    }
                    
                    bool abierto = CerradoAbierto(EDBateriaUsuario);
                    if (!abierto)
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
                bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                if (EncuestaCompleta)
                {
                    if (EDBateriaUsuarioExtra!=null)
                    {
                        if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario!=0)
                        {
                            bool EncuestaCompleta1 = LNBateria.EncuestaCompleta(EDBateriaUsuarioExtra);
                            if (EncuestaCompleta1)
                            {
                                return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                            }
                        }
                        else
                        {
                            return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                        }
                    }
                    else
                    {
                        return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                    }
                    
                }
                bool CedulaConf = ConfirmarIdentidad(EDBateriaUsuario);
                if (CedulaConf == false)
                {
                    return RedirectToAction("Index", new { formdata = formdata, form = form });
                }

            }

            

            return View();
        }
        [HttpGet]
        public ActionResult Index(string formdata, string form)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            int FormInt = 0;
            string NombreForm = "";
            if (int.TryParse(form, out FormInt))
            {
                ViewBag.key = formdata;
                ViewBag.form = form;

                if (FormInt == 1)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                }
                if (FormInt == 2)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                }
                if (FormInt == 3)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    NombreForm = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                }
                if (FormInt == 4)
                {
                    ViewBag.formT = "Cuestionario de Factores de Estrés";
                    NombreForm = "Cuestionario de Factores de Estrés";
                }
                EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(formdata, FormInt);
                if (EDBateriaUsuario!=null)
                {
                    bool abierto = CerradoAbierto(EDBateriaUsuario);
                    if (!abierto)
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
                

                bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                if (EncuestaCompleta)
                {
                    return RedirectToAction("EncuestaTerminada", new { formT = NombreForm });
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult InicioPublico(string formdata, string form)
        {
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            int FormInt = 0;
            if (int.TryParse(form, out FormInt))
            {
                ViewBag.key = formdata;
                ViewBag.form = form;

                if (FormInt == 1)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                }
                if (FormInt == 2)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                }
                if (FormInt == 3)
                {
                    ViewBag.formT = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                }
                if (FormInt == 4)
                {
                    ViewBag.formT = "Cuestionario de Factores de Estrés";
                }
                EDBateriaGestion = LNBateria.ConsultarGestionKey(formdata, FormInt);
                if (EDBateriaGestion!=null)
                {
                    if (EDBateriaGestion.EstadoInt==2 || EDBateriaGestion.EstadoInt == 4)
                    {
                        return View(EDBateriaGestion);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult GuardarControlFormulario(EDBateriaResultado Control1, EDBateriaResultado Control, List<EDBateriaResultado> ListaEvaluacion)
        {
            string Estado = "No se pudo guardar la encuesta";
            bool Probar = false;
            int numpag = Control.Orden;
            string form = Control.ValorS;
            string key = Control.key;
            int extra=Control.DimensionInt;
            int NumErrores = 0;
            List<string> ListaErrores = new List<string>();
            #region FormaA
            if (form == "1" && extra==0)
            {
                string radiopag9 = Control1.ValorS;
                string radiopag10 = Control1.key;
                int intform = 0;
                if (int.TryParse(form, out intform))
                {
                    Probar = LNBateria.GuardarEncuesta(ListaEvaluacion, key, intform);
                    EDBateriaUsuario EDBateriaUsuarioProbar = LNBateria.ConsultarConvocadoKey(key, intform);
                    EDBateriaUsuario EDBateriaUsuarioProbar1 = LNBateria.ConsultarConvocadoKeyExtra(key, intform);

                    if (Probar)
                    {
                        List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
                        List<EDBateriaCuestionario> ListaValidar = new List<EDBateriaCuestionario>();
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                        //esta completa??
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, intform);
                        if (numpag == 1)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 1 && s.Orden <= 12).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 2)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 13 && s.Orden <= 23).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 3)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 24 && s.Orden <= 38).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 4)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 39 && s.Orden <= 52).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 5)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 53 && s.Orden <= 62).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 6)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 63 && s.Orden <= 75).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 7)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 76 && s.Orden <= 89).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 8)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 90 && s.Orden <= 105).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        int errores9 = 0;
                        if (numpag == 9)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 106 && s.Orden <= 114).ToList();
                            if (radiopag9 == null)
                            {
                                NumErrores++;
                                errores9++;
                                Estado = "Por favor diligencie si en su trabajo debe brindar servicio a clientes o usuarios";
                            }
                            else
                            {
                                if (radiopag9 != string.Empty)
                                {
                                    if (radiopag9 == "Si")
                                    {
                                        foreach (var item in ListaValidar)
                                        {
                                            if (item.Valor == 0)
                                            {
                                                ListaErrores.Add(item.Orden.ToString() + "N");
                                                NumErrores++;
                                                errores9++;
                                                Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                            }
                                        }
                                    }
                                    if (radiopag9 == "No")
                                    {

                                    }
                                }
                                else
                                {
                                    NumErrores++;
                                    errores9++;
                                    Estado = "Por favor diligencie si en su trabajo debe brindar servicio a clientes o usuarios";
                                }
                            }
                            if (errores9==0)
                            {
                                LNBateria.EditarCheck9y10(EDBateriaUsuario.Pk_Id_BateriaUsuario, 9, radiopag9);
                            }
                            else
                            {
                                LNBateria.EditarCheck9y10(EDBateriaUsuario.Pk_Id_BateriaUsuario, 9, null);
                            }
                        }
                        int errores10 = 0;
                        if (numpag == 10)
                        {

                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 115 && s.Orden <= 123).ToList();
                            if (radiopag10 == null)
                            {
                                NumErrores++;
                                errores10++;
                                Estado = "Por favor diligencie si usted es jefe de otras personas en el trabajo:";
                            }
                            else
                            {
                                if (radiopag10 != string.Empty)
                                {
                                    if (radiopag10 == "Si")
                                    {
                                        foreach (var item in ListaValidar)
                                        {
                                            if (item.Valor == 0)
                                            {

                                                ListaErrores.Add(item.Orden.ToString() + "N");
                                                NumErrores++;
                                                errores10++;
                                                Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                            }
                                        }
                                    }
                                    if (radiopag10 == "No")
                                    {

                                    }

                                }
                                else
                                {
                                    NumErrores++;
                                    errores10++;
                                    Estado = "Por favor diligencie si en su trabajo debe brindar servicio a clientes o usuarios";
                                }
                            }
                            if (errores10 == 0)
                            {
                                LNBateria.EditarCheck9y10(EDBateriaUsuario.Pk_Id_BateriaUsuario, 10, radiopag10);
                            }
                            else
                            {
                                LNBateria.EditarCheck9y10(EDBateriaUsuario.Pk_Id_BateriaUsuario, 10, null);
                            }
                            if (NumErrores == 0)
                            {
                                bool terminar = LNBateria.TerminarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            }
                        }
                    }
                }

            }

            #endregion
            #region FormaB
            if (form == "2" && extra == 0)
            {
                string radiopag9 = Control1.ValorS;
                //string radiopag10 = Control1.key;
                int intform = 0;
                if (int.TryParse(form, out intform))
                {
                    Probar = LNBateria.GuardarEncuesta(ListaEvaluacion, key, intform);
                    if (Probar)
                    {
                        List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
                        List<EDBateriaCuestionario> ListaValidar = new List<EDBateriaCuestionario>();
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, intform);
                        if (numpag == 1)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 1 && s.Orden <= 12).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 2)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 13 && s.Orden <= 22).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 3)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 23 && s.Orden <= 35).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 4)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 36 && s.Orden <= 44).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 5)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 45 && s.Orden <= 54).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 6)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 55 && s.Orden <= 68).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 7)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 69 && s.Orden <= 78).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 8)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 79 && s.Orden <= 88).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        int errores9 = 0;
                        if (numpag == 9)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 89 && s.Orden <= 97).ToList();
                            if (radiopag9 == null)
                            {
                                NumErrores++;
                                errores9++;
                                Estado = "Por favor diligencie si en su trabajo debe brindar servicio a clientes o usuarios";
                            }
                            else
                            {
                                if (radiopag9 != string.Empty)
                                {
                                    if (radiopag9 == "Si")
                                    {
                                        foreach (var item in ListaValidar)
                                        {
                                            if (item.Valor == 0)
                                            {
                                                ListaErrores.Add(item.Orden.ToString() + "N");
                                                NumErrores++;
                                                errores9++;
                                                Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                            }
                                        }
                                    }
                                    if (radiopag9 == "No")
                                    {

                                    }
                                }
                                else
                                {
                                    NumErrores++;
                                    errores9++;
                                    Estado = "Por favor diligencie si en su trabajo debe brindar servicio a clientes o usuarios";
                                }
                            }
                            if (errores9 == 0)
                            {
                                LNBateria.EditarCheck9y10(EDBateriaUsuario.Pk_Id_BateriaUsuario, 9, radiopag9);
                            }
                            else
                            {
                                LNBateria.EditarCheck9y10(EDBateriaUsuario.Pk_Id_BateriaUsuario, 9, null);
                            }
                            if (NumErrores == 0)
                            {
                                bool terminar = LNBateria.TerminarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            }
                        }
                    }
                }

            }

            #endregion
            #region Extra
            if (form == "1" && extra == 1)
            {
                string radiopag9 = Control1.ValorS;
                int intform = 0;
                if (int.TryParse(form, out intform))
                {
                    Probar = LNBateria.GuardarEncuestaExtra(ListaEvaluacion, key, intform);
                    if (Probar)
                    {
                        List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
                        List<EDBateriaCuestionario> ListaValidar = new List<EDBateriaCuestionario>();
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(key, intform);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 3);
                        if (numpag == 1)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 1 && s.Orden <= 13).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {
                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 2)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 14 && s.Orden <= 27).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {
                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 3)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 28 && s.Orden <= 31).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {
                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }
                            if (NumErrores == 0)
                            {
                                bool terminar = LNBateria.TerminarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            }
                        }
                    }
                }

            }
            if (form == "2" && extra == 1)
            {
                string radiopag9 = Control1.ValorS;
                int intform = 0;
                if (int.TryParse(form, out intform))
                {
                    Probar = LNBateria.GuardarEncuestaExtra(ListaEvaluacion, key, intform);
                    if (Probar)
                    {
                        List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
                        List<EDBateriaCuestionario> ListaValidar = new List<EDBateriaCuestionario>();
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(key, intform);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 3);
                        if (numpag == 1)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 1 && s.Orden <= 13).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {
                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 2)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 14 && s.Orden <= 27).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {
                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }

                        }
                        if (numpag == 3)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 28 && s.Orden <= 31).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {
                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }
                            if (NumErrores == 0)
                            {
                                bool terminar = LNBateria.TerminarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            }
                        }
                    }
                }

            }
            #endregion
            #region Estres
            if (form == "4" && extra == 0)
            {
                string radiopag9 = Control1.ValorS;
                //string radiopag10 = Control1.key;
                int intform = 0;
                if (int.TryParse(form, out intform))
                {
                    Probar = LNBateria.GuardarEncuesta(ListaEvaluacion, key, intform);
                    if (Probar)
                    {
                        List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
                        List<EDBateriaCuestionario> ListaValidar = new List<EDBateriaCuestionario>();
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                        ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, intform);
                        if (numpag == 1)
                        {
                            ListaValidar = ListaCuestionario.Where(s => s.Orden >= 1 && s.Orden <= 31).ToList();
                            foreach (var item in ListaValidar)
                            {
                                if (item.Valor == 0)
                                {

                                    ListaErrores.Add(item.Orden.ToString() + "N");
                                    NumErrores++;
                                    Estado = "No puede continuar con las siguientes preguntas, debe terminar de contestar las preguntas de esta sección de la encuesta. " + NumErrores.ToString() + " Pregunta(s) sin contestar";
                                }
                            }
                            if (NumErrores == 0)
                            {
                                bool terminar = LNBateria.TerminarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            }
                        }

                    }
                }

            }

            #endregion
            return Json(new { Estado, Probar, NumErrores, ListaErrores });
        }
        [HttpPost]
        public ActionResult RechazoEncuesta(List<String> values)
        {

            string Estado = "";
            bool Probar = false;
            string form = values[0];
            string key = values[1];
            int intform = 0;
            if (int.TryParse(form, out intform) && key!=null)
            {
                if (values[2]!=null)
                {
                    if (values[2]!="")
                    {
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(key, intform);
                        bool terminar = LNBateria.TerminarEncuestaRechazo(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                    }
                    else
                    {
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(key, intform);
                        bool terminar = LNBateria.TerminarEncuestaRechazo(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                        if (EDBateriaUsuarioExtra!=null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario!=0)
                            {
                                terminar = LNBateria.TerminarEncuestaRechazo(EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario);
                            }
                        }
                    }
                }
                else
                {
                    EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                    EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(key, intform);
                    bool terminar = LNBateria.TerminarEncuestaRechazo(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                    if (EDBateriaUsuarioExtra != null)
                    {
                        if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario != 0)
                        {
                            terminar = LNBateria.TerminarEncuestaRechazo(EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario);
                        }
                    }
                }
                
            }

            return Json(new { Estado, Probar});
        }
        [HttpPost]
        public ActionResult AceptacionEncuesta(List<String> values)
        {

            string Estado = "";
            bool Probar = false;
            string form = values[0];
            string key = values[1];
            int intform = 0;
            if (int.TryParse(form, out intform) && key != null)
            {
                if (values[2] != null)
                {
                    if (values[2]!="")
                    {
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKeyExtra(key, intform);
                        bool terminar = LNBateria.AceptarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                    }
                    else
                    {
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                        bool terminar = LNBateria.AceptarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                    }
                }
                else
                {
                    EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                    bool terminar = LNBateria.AceptarEncuesta(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                }
                
            }

            return Json(new { Estado, Probar });
        }
        [HttpPost]
        public ActionResult GuardarInicial(EDBateriaInicial EDBateriaInicial, EDBateriaInicial Control)
        {
            string Estado = "No se pudo guardar el formulario";
            string key = Control.Nombre;
            string formdata = Control.Sexo;
            string pagina = Control.Profesion;
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            int FormInt = 0;
            int pag = 0;
            bool[] Probar = new bool[5] { false, false, false, false, false };
            string[] ValidacionStr = new string[24];
            bool[] Validacion = new bool[24];
            for (int i = 0; i < 24; i++)
            {
                ValidacionStr[i] = "";
                Validacion[i] = true;
            }

            ValidacionStr[0] = "Elija su último nivel de estudios";
            ValidacionStr[1] = "Digite su ocupación";
            ValidacionStr[2] = "Digite su año de nacimiento";
            ValidacionStr[3] = "Elija su estado civil";

            ValidacionStr[4] = "Digite el último nivel de estudios que alcanzó";
            ValidacionStr[5] = "Digite su ocupación o profesión";
            ValidacionStr[6] = "Digite la ciudad o municipio de su residencia actual";
            ValidacionStr[7] = "Digite el departamento de su residencia actual";
            ValidacionStr[8] = "Elija el estrato de los servicios públicos de su vivienda";
            ValidacionStr[9] = "Elija su tipo de vivienda";
            ValidacionStr[10] = "Digite el número de personas que dependen económicamente de usted";

            ValidacionStr[11] = "Digite la ciudad o municipio del lugar donde trabaja actualmente";
            ValidacionStr[12] = "Digite el departamento del lugar donde trabaja actualmente";
            ValidacionStr[13] = "Elija la opción cuántos años que trabaja en esta empresa";
            ValidacionStr[14] = "Elijió 'Si lleva más de un año' por favor digite el número de años (debe ser mayor o igual a 2 años)";
            ValidacionStr[15] = "Digite el nombre del cargo que ocupa en la empresa";
            ValidacionStr[16] = "Seleccione el tipo de cargo que más se parece al que usted desempeña";
            ValidacionStr[17] = "Elija la opción cuántos años desempeña el cargo u oficio actual en esta empresa";
            ValidacionStr[18] = "Elijió 'Si lleva más de un año' por favor digite el número de años (debe ser mayor o igual a 2 años)";
            ValidacionStr[19] = "Digite el nombre del departamento, área o sección de la empresa en el que trabaja";

            ValidacionStr[20] = "Elija el tipo de contrato que tiene actualmente ";
            ValidacionStr[21] = "Digite las horas diarias de trabajo están establecidas habitualmente por la empresa para su cargo";
            ValidacionStr[22] = "Elija el tipo de salario que recibe";


            if (int.TryParse(formdata, out FormInt) && int.TryParse(pagina, out pag))
            {
                

                EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, FormInt);
                bool EncuestaCompleta = LNBateria.EncuestaCompleta(EDBateriaUsuario);
                if (EncuestaCompleta)
                {
                    EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
                    EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(key, FormInt);
                    EDBateriaInicial.Fk_Id_BateriaUsuario = EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario;
                }
                else
                {
                    EDBateriaInicial.Fk_Id_BateriaUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
                }


                bool existe = LNBateria.ConsultarInicialbool(EDBateriaInicial.Fk_Id_BateriaUsuario);

                #region validarInts
                if (EDBateriaInicial.AnioNacS != null)
                {
                    int valint = 0;
                    if (int.TryParse(EDBateriaInicial.AnioNacS, out valint))
                    {
                        if (valint.ToString().Length != 4)
                        {
                            EDBateriaInicial.AnioNacS = null;
                            ValidacionStr[2] = "La fecha de nacimiento debe tener por lo menos 4 digitos";
                        }
                        else
                        {
                            EDBateriaInicial.AñoNac = valint;
                        }
                    }
                    else
                    {
                        EDBateriaInicial.AnioNacS = null;
                    }
                }
                if (EDBateriaInicial.PersonasDependenS != null)
                {
                    int valint = 0;
                    if (int.TryParse(EDBateriaInicial.PersonasDependenS, out valint))
                    {
                        EDBateriaInicial.PersonasDependen = valint;
                    }
                    else
                    {
                        EDBateriaInicial.PersonasDependenS = null;
                    }
                }


                if (EDBateriaInicial.AñosConEmpresa == "Si lleva más de un año, anote cuántos años:")
                {
                    if (EDBateriaInicial.AñosConEmpresaNumS != null)
                    {
                        int valint = 0;
                        if (int.TryParse(EDBateriaInicial.AñosConEmpresaNumS, out valint))
                        {
                            if (valint >= 2)
                            {
                                EDBateriaInicial.AñosConEmpresaNum = valint;
                            }
                            else
                            {
                                EDBateriaInicial.AñosConEmpresaNumS = null;
                            }

                        }
                        else
                        {
                            EDBateriaInicial.AñosConEmpresaNumS = null;
                        }
                    }
                }
                else
                {
                    EDBateriaInicial.AñosConEmpresaNumS = "";
                    EDBateriaInicial.AñosConEmpresaNum = 0;
                }

                if (EDBateriaInicial.AñosOficio == "Si lleva más de un año, anote cuántos años:")
                {
                    if (EDBateriaInicial.AñosOficioNumS != null)
                    {
                        int valint = 0;
                        if (int.TryParse(EDBateriaInicial.AñosOficioNumS, out valint))
                        {
                            if (valint >= 2)
                            {
                                EDBateriaInicial.AñosOficioNum = valint;
                            }
                            else
                            {
                                EDBateriaInicial.AñosOficioNumS = null;
                            }

                        }
                        else
                        {
                            EDBateriaInicial.AñosOficioNumS = null;
                        }
                    }
                }
                else
                {
                    EDBateriaInicial.AñosOficioNumS = "";
                    EDBateriaInicial.AñosOficioNum = 0;
                }

                if (EDBateriaInicial.AñosOficioNumS != null)
                {
                    int valint = 0;
                    if (int.TryParse(EDBateriaInicial.AñosOficioNumS, out valint))
                    {
                        if (valint >= 2)
                        {
                            EDBateriaInicial.AñosOficioNum = valint;
                        }
                        else
                        {
                            EDBateriaInicial.AñosOficioNumS = null;
                        }

                    }
                    else
                    {
                        EDBateriaInicial.AñosOficioNumS = null;
                    }
                }
                if (EDBateriaInicial.HorasEstablecidasS != null)
                {
                    int valint = 0;
                    if (int.TryParse(EDBateriaInicial.HorasEstablecidasS, out valint))
                    {
                        if (valint > 0)
                        {
                            EDBateriaInicial.HorasEstablecidas = valint;
                        }
                        else
                        {
                            EDBateriaInicial.HorasEstablecidasS = null;
                            ValidacionStr[21] = "las horas diarias de trabajo deben ser mayores a 0";
                        }
                    }
                    else
                    {
                        EDBateriaInicial.HorasEstablecidasS = null;
                    }
                }
                #endregion

                if (existe)
                {

                    Probar = LNBateria.ActualizarInicial(EDBateriaInicial);
                }
                else
                {
                    if (EDBateriaInicial.Fk_Id_BateriaUsuario != 0)
                    {
                        Probar = LNBateria.GuardarInicial(EDBateriaInicial);
                    }
                }

                if (pagina == "1")
                {
                    if (Probar[0] && Probar[1])
                    {
                        Estado = "exito 1";
                    }
                    else
                    {
                        #region val1
                        if (EDBateriaInicial.Nombre == null)
                        {
                            Validacion[0] = false;
                        }
                        if (EDBateriaInicial.Sexo == null)
                        {
                            Validacion[1] = false;
                        }
                        if (EDBateriaInicial.AñoNac == 0)
                        {
                            Validacion[2] = false;
                        }
                        else
                        {
                            if (EDBateriaInicial.AñoNac.ToString().Length != 4)
                            {
                                Validacion[2] = false;

                            }
                        }
                        if (EDBateriaInicial.EstadoCivil == null)
                        {
                            Validacion[3] = false;
                        }
                        #endregion
                        Estado = "No guardo 1 ";
                    }
                }
                if (pagina == "2")
                {
                    if (Probar[0] && Probar[2])
                    {
                        Estado = "exito 2";
                    }
                    else
                    {
                        #region val2
                        if (EDBateriaInicial.NivEstudios == null)
                        {
                            Validacion[4] = false;
                        }
                        if (EDBateriaInicial.Profesion == null)
                        {
                            Validacion[5] = false;
                        }
                        if (EDBateriaInicial.ResidenciaMun == null)
                        {
                            Validacion[6] = false;
                        }
                        if (EDBateriaInicial.ResidenciaDep == null)
                        {
                            Validacion[7] = false;
                        }
                        if (EDBateriaInicial.Estrato == null)
                        {
                            Validacion[8] = false;
                        }
                        if (EDBateriaInicial.TipoVivienda == null)
                        {
                            Validacion[9] = false;
                        }
                        if (EDBateriaInicial.PersonasDependenS == null)
                        {
                            Validacion[10] = false;
                        }
                        #endregion
                        Estado = "No guardo 2";
                    }
                }
                if (pagina == "3")
                {
                    if (Probar[0] && Probar[3])
                    {
                        Estado = "exito 2";
                    }
                    else
                    {
                        #region val3
                        if (EDBateriaInicial.LugarTrabajoMun == null)
                        {
                            Validacion[11] = false;
                        }
                        if (EDBateriaInicial.LugarTrabajoDep == null)
                        {
                            Validacion[12] = false;
                        }
                        if (EDBateriaInicial.AñosConEmpresa == null)
                        {
                            Validacion[13] = false;
                        }
                        else
                        {
                            if (EDBateriaInicial.AñosConEmpresa == "Si lleva más de un año, anote cuántos años:")
                            {
                                if (EDBateriaInicial.AñosConEmpresaNumS == null)
                                {
                                    Validacion[14] = false;
                                }
                            }
                        }
                        if (EDBateriaInicial.CargoConEmpresa == null)
                        {
                            Validacion[15] = false;
                        }
                        if (EDBateriaInicial.TipoCargo == null)
                        {
                            Validacion[16] = false;
                        }
                        if (EDBateriaInicial.AñosOficio == null)
                        {
                            Validacion[17] = false;
                        }
                        else
                        {
                            if (EDBateriaInicial.AñosOficio == "Si lleva más de un año, anote cuántos años:")
                            {
                                if (EDBateriaInicial.AñosOficioNumS == null)
                                {
                                    Validacion[18] = false;
                                }
                            }
                        }
                        if (EDBateriaInicial.AreaEmpresa == null)
                        {
                            Validacion[19] = false;
                        }

                        #endregion
                        Estado = "No guardo 2";
                    }
                }
                if (pagina == "4")
                {
                    if (Probar[0] && Probar[4])
                    {

                        Estado = "exito 2";
                    }
                    else
                    {
                        if (EDBateriaInicial.TipoContrato == null)
                        {
                            Validacion[20] = false;
                        }
                        if (EDBateriaInicial.HorasEstablecidas == 0)
                        {
                            Validacion[21] = false;
                        }
                        if (EDBateriaInicial.TipoSalario == null)
                        {
                            Validacion[22] = false;
                        }
                        Estado = "No guardo 2";
                    }
                }


            }
            else
            {
                return Json(new { Estado, Probar, ValidacionStr, Validacion });
            }

            return Json(new { Estado, Probar, ValidacionStr, Validacion });

        }
        [HttpGet]
        public ActionResult EncuestaTerminada(string formT)
        {
            ViewBag.formT = formT;
            return View();
        }
        [HttpPost]
        public ActionResult ComprobarIdentidad(List<String> values)
        {

            string Estado = "El número de documento suministrado no coincide con el de la persona invitada, por favor digite de nuevo su documento de identidad";
            bool Probar = false;
            string form = values[0];
            string key = values[1];
            string cedula = values[2];

            int intform = 0;
            if (int.TryParse(form, out intform) && key != null)
            {
                EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoKey(key, intform);
                Probar = ConfirmarCedula(cedula, EDBateriaUsuario);
                if (Probar)
                {
                    LNBateria.RecibirEditarDocumento(EDBateriaUsuario.Pk_Id_BateriaUsuario, cedula);
                    Estado = "El número de documento coincide con el invitado, muchas gracias por aceptar la invitación";
                }
            }

            return Json(new { Estado, Probar });
        }
        [HttpPost]
        public ActionResult ComprobarIdentidadPublico(List<String> values)
        {
            string Estado = "Ocurrio un error al cargar la página";
            int Paso = 0;
            bool Probar = false;
            bool[] boolValidacion = new bool[4];
            string[] Validacion = new string[4];
            string form = values[0];
            string key = values[1];
            string cedula = values[2];
            string nombre = values[3];
            string Nit = values[4];
            string url = "";

            

            boolValidacion[0] = false;
            boolValidacion[1] = false;
            boolValidacion[2] = false;
            boolValidacion[3] = true;

            Validacion[0] = "Por favor digite la cédula o documento de identidad";
            Validacion[1] = "Por favor digite su nombre";
            Validacion[2] = "Por favor digite el NIT sin digito de verificación de la empresa que lo ha invitado";
            Validacion[3] = "El NIT de la empresa digitado no coincide con el NIT de la empresa que realiza el cuestionario, verifique y digite por favor el número del NIT sin digito de verificación de la empresa que lo ha invitado";

            if (cedula!=null)
            {
                if (cedula.Replace(" ", "").Trim() != string.Empty)
                {
                    cedula = cedula.Trim().Replace(" ", "");
                    boolValidacion[0] = true;
                }
                else
                {
                    Estado = "Hay campos sin diligenciar, por favor digite la información que no ha suministrado";
                }
            }
            else
            {
                Estado = "Hay campos sin diligenciar, por favor digite la información que no ha suministrado";
            }
            if (nombre != null)
            {

                if (nombre.Replace(" ", "").Trim() != string.Empty)
                {
                    boolValidacion[1] = true;
                }
                else
                {
                    Estado = "Hay campos sin diligenciar, por favor digite la información que no ha suministrado";
                }
            }
            else
            {
                Estado = "Hay campos sin diligenciar, por favor digite la información que no ha suministrado";
            }
            if (Nit != null)
            {
                if (Nit.Replace(" ", "").Trim() != string.Empty)
                {
                    Nit = Nit.Trim().Replace(" ", "");
                    boolValidacion[2] = true;
                    boolValidacion[3] = true;
                }
                else
                {
                    Estado = "Hay campos sin diligenciar, por favor digite la información que no ha suministrado";
                }
                
            }
            else
            {
                Estado = "Hay campos sin diligenciar, por favor digite la información que no ha suministrado";
            }




            int intform = 0;
            if (int.TryParse(form, out intform) && key != null)
            {
                EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestionKey(key, intform);
                if (EDBateriaGestion!=null)
                {
                    int fkidEmpresa = EDBateriaGestion.Fk_Id_Empresa;
                    bool probarempresa = LNBateria.EmpresaCoincide(Nit, fkidEmpresa);
                    if (probarempresa)
                    {
                        EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocadoCedula(cedula, EDBateriaGestion.Pk_Id_BateriaGestion);

                        if (EDBateriaUsuario!=null)
                        {
                            //Redirigir al ultimo paso
                            Paso = 1;
                            Probar = true;
                            Estado = "La autenticación es correcta, ahora puede continuar con el diligenciamiento de la encuesta";
                            string formdata = EDBateriaUsuario.TokenPrivado;
                            if (intform == 1)
                            {
                                var urlport = new Uri(Url.Action("IntralaboralA", "Bateria", new { formdata = formdata, pagina = "1", form = form }, Request.Url.Scheme));
                                var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                url = urloutport;
                            }
                            if (intform == 2)
                            {
                                var urlport = new Uri(Url.Action("IntralaboralB", "Bateria", new { formdata = formdata, pagina = "1", form = form }, Request.Url.Scheme));
                                var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                url = urloutport;
                            }
                            if (intform == 3)
                            {
                                var urlport = new Uri(Url.Action("Extralaboral", "Bateria", new { formdata = formdata, pagina = "1", form = form }, Request.Url.Scheme));
                                var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                url = urloutport;
                            }
                            if (intform == 4)
                            {
                                var urlport = new Uri(Url.Action("Estres", "Bateria", new { formdata = formdata, pagina = "1", form = form }, Request.Url.Scheme));
                                var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                url = urloutport;
                            }
                            return Json(new { Estado, Probar, Paso, boolValidacion, Validacion, url });
                        }
                        else
                        {
                            //Crear Usuario
                            Paso = 2;
                            Probar = true;
                            Estado = "La autenticación es correcta, ahora puede continuar con el diligenciamiento de la encuesta";
                            string token = RandomString(24);
                            EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
                            EDBateriaUsuario EDBateriaUsuarioCrear = new EDBateriaUsuario();
                            EDBateriaUsuarioCrear.Id_Empresa = EDBateriaGestion.Fk_Id_Empresa;
                            EDBateriaUsuarioCrear.TokenPrivado = token;
                            EDBateriaUsuarioCrear.EstadoEnvio = 1;
                            EDBateriaUsuarioCrear.Nombre = nombre.ToUpper();
                            EDBateriaUsuarioCrear.NumeroIdentificacion = cedula;
                            EDBateriaUsuarioCrear.TipoDocumento = "N/A";
                            EDBateriaUsuarioCrear.Fk_Id_BateriaGestion = EDBateriaGestion.Pk_Id_BateriaGestion;
                            EDBateriaUsuarioCrear.TipoConv = 5;
                            EDBateriaUsuarioCrear.NumeroIntentos = 0;
                            EDBateriaUsuarioCrear.RegistroOperacion = null;
                            EDBateriaUsuarioCrear.DocumentoDigitado = cedula;
                            EDBateriaUsuarioCrear.ConfirmacionParticipacion = null;
                            EDBateriaUsuarioCrear.link = null;
                            EDBateriaUsuarioCrear.FechaEnvio = null;
                            EDBateriaUsuarioCrear.FechaRespuesta = null;

                            Probar = LNBateria.CrearConvocadoPublico(EDBateriaUsuarioCrear);
                            if (Probar)
                            {
                                if (EDBateriaGestion.bateriaExtra == 3)
                                {
                                    EDBateriaUsuarioExtra.Id_Empresa = EDBateriaGestion.Fk_Id_Empresa;
                                    EDBateriaUsuarioExtra.TokenPrivado = token;
                                    EDBateriaUsuarioExtra.EstadoEnvio = 1;
                                    EDBateriaUsuarioExtra.Nombre = nombre.ToUpper();
                                    EDBateriaUsuarioExtra.NumeroIdentificacion = cedula;
                                    EDBateriaUsuarioExtra.TipoDocumento = "N/A";
                                    EDBateriaUsuarioExtra.Fk_Id_BateriaGestion = EDBateriaGestion.Pk_Id_BateriaGestion;
                                    EDBateriaUsuarioExtra.TipoConv = 5;
                                    EDBateriaUsuarioExtra.NumeroIntentos = 1;
                                    EDBateriaUsuarioExtra.RegistroOperacion = null;
                                    EDBateriaUsuarioExtra.DocumentoDigitado = cedula;
                                    EDBateriaUsuarioExtra.ConfirmacionParticipacion = null;
                                    EDBateriaUsuarioExtra.link = null;
                                    EDBateriaUsuarioExtra.FechaEnvio = null;
                                    EDBateriaUsuarioExtra.FechaRespuesta = null;
                                    Probar = LNBateria.CrearConvocadoPublico(EDBateriaUsuarioExtra);
                                }

                                if (intform == 1)
                                {
                                    var urlport = new Uri(Url.Action("IntralaboralA", "Bateria", new { formdata = token, pagina = "1", form = form }, Request.Url.Scheme));
                                    var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                    url = urloutport;
                                }
                                if (intform == 2)
                                {
                                    var urlport = new Uri(Url.Action("IntralaboralB", "Bateria", new { formdata = token, pagina = "1", form = form }, Request.Url.Scheme));
                                    var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                    url = urloutport;
                                }
                                if (intform == 4)
                                {
                                    var urlport = new Uri(Url.Action("Estres", "Bateria", new { formdata = token, pagina = "1", form = form }, Request.Url.Scheme));
                                    var urloutport = urlport.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                                    url = urloutport;
                                }
                                return Json(new { Estado, Probar, Paso, boolValidacion, Validacion, url });
                            }

                        }
                    }
                    else
                    {
                        boolValidacion[3] = false;
                    }
                }
                else
                {

                }
            }

            return Json(new { Estado, Probar, Paso, boolValidacion, Validacion, url });
        }
        private bool ConfirmarCedula(string texto, EDBateriaUsuario EDBateriaUsuario)
        {
            bool Probar = false;
            string cedula = texto.Trim();
            if (EDBateriaUsuario!=null)
            {
                if (EDBateriaUsuario.NumeroIdentificacion== cedula)
                {
                    Probar = true;
                }
            }
            return Probar;
        }
        private bool ConfirmarSesion(string texto, EDBateriaGestion EDBateriaGestion)
        {
            bool Probar = false;
            string cedula = texto.Trim();
            if (EDBateriaGestion != null)
            {
                if (EDBateriaGestion.EstadoInt == 1)
                {
                    Probar = true;
                }
            }
            return Probar;
        }
        private bool ConfirmarIdentidad(EDBateriaUsuario EDBateriaUsuario)
        {
            bool Probar = false;
            if (EDBateriaUsuario != null)
            {
                if (EDBateriaUsuario.DocumentoDigitado == EDBateriaUsuario.NumeroIdentificacion)
                {
                    Probar = true;
                }
            }
            return Probar;
        }
        private bool ConfirmarParticipacion(EDBateriaUsuario EDBateriaUsuario)
        {
            bool Probar = false;
            if (EDBateriaUsuario != null)
            {
                if (EDBateriaUsuario.ConfirmacionParticipacion == "Aceptado" || EDBateriaUsuario.ConfirmacionParticipacion == "Rechazado")
                {
                    Probar = true;
                }
            }
            return Probar;
        }

        private bool CerradoAbierto(EDBateriaUsuario EDBateriaUsuario)
        {
            bool probar = false;
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            EDBateriaGestion = LNBateria.ConsultarGestionKey1(EDBateriaUsuario);
            if (EDBateriaGestion != null)
            {
                if (EDBateriaGestion.EstadoInt == 2 || EDBateriaGestion.EstadoInt == 4)
                {
                    probar = true;
                    return probar;
                }
                else
                {
                    return probar;
                }
            }
            else
            {
                return probar;
            }

            return probar;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "123456789abcdefghijklmnopqrstvwxyzABCDEFGHIJKLMOPQRSTVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



    }
}