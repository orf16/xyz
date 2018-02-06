// <copyright file="GobiernoController.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Correa</author>
// <date>03/01/2017</date>
// <summary>Controlador que gestiona la mision de la empresa</summary>
namespace SG_SST.Controllers.Empresas
{
    using SG_SST.Models.Empresas;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Services.Empresas.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.IO;
    using SG_SST.Models;
    using SG_SST.Controllers.Base;
    public class GobiernoController : BaseController
    {
        IGobiernoServicios gs;/// Defino variable gs
       
        private SG_SSTContext db = new SG_SSTContext();


        // GET: Gobierno
        public ActionResult Index()//index de gobierno
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult misionM()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.texto = "mision";
            return View("misionM");
        }

        public ActionResult visionV()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            return View("visionV");
        }

        public ActionResult Gobierno(Gobierno gobierno)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            gs.GrabarGobierno(gobierno, usuarioActual.IdEmpresa);
            return RedirectToAction("misionM");
        }


        //metodo que recibe el submit de la vista
        public ActionResult Mision(string mision)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            
            bool respuesta = gs.GrabarMision(mision, usuarioActual.IdEmpresa);
            if (respuesta)
            {
                // se grabo mision con exito
               


                ViewBag.Message = "Mision Almacenada Exitosamente.!";
                return View("misionM");
            }
            else
            {
                ViewBag.Message = false;
                return View("misionM");
            }
        }
        public ActionResult Gobiernov(Gobierno gobiernov)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            var NitEmpres = Convert.ToInt32(usuarioActual.NitEmpresa);
            gs.GrabarGobiernoV(gobiernov, usuarioActual.IdEmpresa, NitEmpres);
            return RedirectToAction("visionV");
        }

        public ActionResult Vision(string vision)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            var NitEmpres = Convert.ToInt32(usuarioActual.NitEmpresa);
            bool respuesta = gs.GrabarVision(vision, usuarioActual.IdEmpresa, NitEmpres );
            if (respuesta)
            {
                ViewBag.Message = "Vision Almacenada Exitosamente.!";
                return View("visionV");
            }
            else
            {
                
                return View("visionV");
            }
        }
        //Metodo para Cargar la mision en el Modal de registro
        public ActionResult CargarMision()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            string mision = gs.ObtenerMision(usuarioActual.IdEmpresa);
            if (mision != "")
            {
                return Json(new { success = true, Mision = mision }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        //Metodo para Cargar la vision en el Modal de registro
        public ActionResult CargarVision()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            string vision = gs.ObtenerVision(usuarioActual.IdEmpresa);
            if (vision != "")
            {
                return Json(new { success = true, Vision = vision }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
       
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string mision)
        //{
        //    using(SG_SSTContext con= new SG_SSTContext())
        //    {
        //          Gobierno gobierno = db.Tbl_Gobierno.Find(mision);
           
        //    db.Tbl_Gobierno.Remove(gobierno);
        //    db.SaveChanges();
         
        //    ViewBag.Message = "La Mision ha sido eliminada del Sistema";
        //    return View("misionM");
        //    }
          
        //}

       //Metodo para Eliminar la mision
        public ActionResult EliminarMision()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            ViewBag.mensaje = gs.EliminarMision(usuarioActual.IdEmpresa);
            ViewBag.mensaje="Se ha eliminado la Mision.";
            return View("misionM");
           
        }

        public ActionResult EliminarVision()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new GobiernoServicios();
            ViewBag.mensaje = gs.EliminarVision(usuarioActual.IdEmpresa);
            ViewBag.mensaje = "Se ha eliminado la Vision.";
            return View("visionV");
        }

    }



}