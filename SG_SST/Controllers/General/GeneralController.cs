using SG_SST.Models.Empresas;
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.General
{
    public class GeneralController : Controller
    {
        IRecursosServicios recursosServicios = new RecursosServicios();

        // GET: General
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerMunicipios(int PK_Departamento) 
            {
            List<Municipio> municipiosList = recursosServicios.ObtenetMunicipios(PK_Departamento);
            if (municipiosList.Count != 0)
            {
                return Json(
                   municipiosList.Select(municipio => new
                   {
                       PK_Municipio = municipio.Pk_Id_Municipio,
                       NombreMunicipio = municipio.Nombre_Municipio,
                      
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