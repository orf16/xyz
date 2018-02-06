// <copyright file="GTC45Controller.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez</author>
// <date>13/01/2017</date>
// <summary>Controlador que me gestiona las acciones de la GTC45 a realizar</summary>

namespace SG_SST.Controllers.Planificacion
{
    using SG_SST.Controllers.Base;
    using SG_SST.Models.Planificacion;
    using SG_SST.Services.Planificacion.IServices;
    using SG_SST.Services.Planificacion.Services;
    using System.Web.Mvc;

    public class GTC45Controller : BaseController
    {
        IGTC45Servicio gtc45Servicio = new GTC45Servicio();
        IConsecuenciasServicios consecuenciasServicios = new ConsecuenciasServicios();
       

        // GET: GTC45
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
        /// Metodo que me retonar el valor y la interpretacion de probablidad 
        /// </summary>
        /// <param name="PK_Deficiencia">pk o id del nivel de deficiencia</param>
        /// <param name="PK_Exposicion">pk o id del nivel de exposicion</param>
        /// <returns>json con el valor de la probabilidad y la interpretacion</returns>
        public ActionResult NivelProbabilidad(int PK_Deficiencia, int PK_Exposicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            int valorDeProbabilidad = gtc45Servicio.CalcularNivelProbabilidad(PK_Deficiencia, PK_Exposicion);
            string interpretacionProbabilidad = "";
            if (valorDeProbabilidad > -1)
            {
                interpretacionProbabilidad = gtc45Servicio.ConsultarInterpretacionDeProbabilidad(valorDeProbabilidad);
                return Json(
                   new
                   {
                       Valor_Probablidad = valorDeProbabilidad,
                       interpretacion = interpretacionProbabilidad
                   }
                , JsonRequestBehavior.AllowGet);
            }
            else {             
                return Json(false, JsonRequestBehavior.AllowGet);
            }                
        
        }

        /// <summary>
        /// Metodo que me retornar el nivel de riesgo,el resultado de la interpretacion y el detalle de 
        /// la interpretacion
        /// </summary>
        /// <param name="PK_Consecuencia">clave primaria del la consecuencia</param>
        /// <param name="Valor_Probabilidad"> valor de la probabilidad</param>
        /// <returns>json con el nivel de riesgo,resultado e interpretacion</returns>
        public ActionResult NivelDeRiesgo(int PK_Consecuencia, int Valor_Probabilidad)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            int valorConsecuencia = consecuenciasServicios.ObtenerValorConsecuencia(PK_Consecuencia);
            if (valorConsecuencia > -1)
            {
                int nivelDeRiesgo = valorConsecuencia * Valor_Probabilidad;
                InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo = 
                    gtc45Servicio.ObtenerInterpretacionDeRiesgo(nivelDeRiesgo);
                if (interpretacionNivelDeRiesgo != null) { 
                    return Json(
                       new
                       {
                           Nivel_De_Riesgo =nivelDeRiesgo,
                           Resultado = interpretacionNivelDeRiesgo.Resultado,
                           Interpretacion = interpretacionNivelDeRiesgo.Interpretacion
                       }
                    , JsonRequestBehavior.AllowGet);
                }
            }           
            return Json(false, JsonRequestBehavior.AllowGet);
            
        }



    }
}