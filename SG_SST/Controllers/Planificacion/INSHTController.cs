// <copyright file="INSHTController.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez</author>
// <date>13/01/2017</date>
// <summary>Controlador que me gestiona las acciones de la metodologia INSHT a realizar</summary>

namespace SG_SST.Controllers.Planificacion
{
    using SG_SST.Controllers.Base;
    using SG_SST.Models.Planificacion;
    using SG_SST.Services.Planificacion.IServices;
    using SG_SST.Services.Planificacion.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class INSHTController : BaseController
    {
        IINSHTServicios INSHTServicios = new INSHTServicios();
       
        // GET: INSHT
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View();
        }

        /// <summary>
        /// Metodo que me retonar la interpretacion de la estimacion del riesgo que es la combinacin de la probabilidad 
        /// y la consecuencia
        /// </summary>
        /// <param name="Pk_Probabilidad">clave primaria de la probabilidad</param>
        /// <param name="PK_Consecuencia">clave primara de la consecuencia</param>
        /// <returns></returns>
        public ActionResult EstimacionDelRiesgo(int Pk_Probabilidad, int PK_Consecuencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            EstimacionDeRiesgo estimacionDelRiesgo = INSHTServicios.ObtenerEstimacionDelRiesgo(Pk_Probabilidad, PK_Consecuencia);
            if (estimacionDelRiesgo != null )
            {                
                return Json(
                   new
                   {
                       Estimacion_Del_Riesgo = estimacionDelRiesgo.Detalle_Estimacion,
                       PK_Estimacion_De_Riesgo = estimacionDelRiesgo.PK_Estimacion_De_Riesgo,
                       EsNoAceptable = estimacionDelRiesgo.RiesgoNoAceptable,
                       valorRiesgo = estimacionDelRiesgo.ValorDelRiesgo
                   }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            } 
        }


    }
}