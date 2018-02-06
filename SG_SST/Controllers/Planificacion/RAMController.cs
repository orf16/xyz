// <copyright file="RAMController.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez</author>
// <date>19/01/2017</date>
// <summary>Controlador que me gestiona las acciones de la metodologia  RAM a realizar</summary>

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
    public class RAMController : BaseController
    {
        IConsecuenciasServicios consecuenciasServicios = new ConsecuenciasServicios();
       
        // GET: RAM
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
        /// Controlador que me retorna las consecuencias por grupo 
        /// </summary>
        /// <param name="PK_Grupo">id o clave primaria del grupo</param>
        /// <returns></returns>
        public ActionResult ConsecuenciasPorGrupo(int PK_Grupo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            List<Consecuencia> consecuenciaList = consecuenciasServicios.ObtenerConsecuenciasPorGrupo(PK_Grupo);
            if (consecuenciaList.Count > 0)
            {
                return Json(
                   consecuenciaList.Select(consecuencia => new
                   {
                       PK_Consecuencia= consecuencia.PK_Consecuencia,
                       PK_ConsecuenciaDescription = consecuencia.Descripcion_Consecuencia                      
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}