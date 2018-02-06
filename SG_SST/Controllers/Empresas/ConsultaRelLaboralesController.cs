using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models.Empresa;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models.Login;
using Newtonsoft.Json;
using SG_SST.Logica.Empresas;

namespace SG_SST.Controllers.Empresas
{
    public class ConsultaRelLaboralesController : Base.BaseController
    {
        // GET: ConsultaRelLaborales
        LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
        public ActionResult Index(int id, string estado, string tipoCotizante)
        {
            List<EDEmpleadoRelLab> lstempTer = new List<EDEmpleadoRelLab>();
            string DocumentoEmpresa = "";
            return View(lstempTer);
        }

        public ActionResult ListEmpleadosRelLab(int id , string estado, string tipoCotizante)
        {
            string DocumentoEmpresa = "";
            return PartialView(Buscar(id , DocumentoEmpresa, estado, tipoCotizante));
        }


        public List<EDEmpleadoRelLab> Buscar(int pageIndex, string DocumentoEmpresa, string estado, string tipoCotizante)
        {
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            //asigna la empresa logueada
            if (Session["UsuarioSession"] != null && !String.IsNullOrWhiteSpace(Session["UsuarioSession"].ToString()))
            {
                UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
                var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
                var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
                if (usuarioSesion != null)
                {
                    eu.DocumentoEmpresa = usuario.NitEmpresa;
                }
            }
            int pageCount = 0;
            List<EDEmpleadoRelLab> lstempTer = lnRL.ListarRelacionesLabTerceros(estado, tipoCotizante, DocumentoEmpresa , pageIndex, 10, out pageCount);
            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            return lstempTer;
        }
    }
}