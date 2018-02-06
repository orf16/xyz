using SG_SST.Controllers.Base;
using SG_SST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SG_SST.Controllers.IncidentesConsulta
{
    public class IncidentesConsultaController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            return View();
        }
    }
}
