using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Controllers.Base;
using SG_SST.Models.Comunicaciones;

namespace SG_SST.Controllers.Comunicaciones
{
    public class PublicacionController : Controller
    {
        private SG_SSTContext db = new SG_SSTContext();

        [HttpGet]
        public JsonResult GenerarEncuesta(int PK_Id_Encuesta)
        {
            var comunicaciones = db.Tbl_ComunicacionesInternas.Where(x => x.PK_Id_Encuesta == PK_Id_Encuesta).SingleOrDefault();
            return Json(comunicaciones.URL, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PublicarEncuesta(string formdata)
        {
            var comunicaciones = db.Tbl_ComunicacionesInternas.Where(x => x.TokenPublico == formdata).SingleOrDefault();
            ComunicacionesPublicas objComunicacionesPublicas = new ComunicacionesPublicas()
            {
                fk_pk_id_encuesta = comunicaciones.PK_Id_Encuesta,
                contenido = comunicaciones.CuerpoHTML,
                titulo = comunicaciones.Titulo
            };
            return View(objComunicacionesPublicas);
        }

    }
}
