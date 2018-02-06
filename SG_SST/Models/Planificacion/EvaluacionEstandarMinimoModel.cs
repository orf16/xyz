using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Planificacion
{
    public class EvaluacionEstandarMinimoModel
    {
        public CicloModel CicloActual { get; set; }
        public IEnumerable<CicloModel> Ciclos { get; set; }
        public ValoracionFinalModel CalificacionFinal { get; set; }
    }

    public class CicloModel
    {
        public int IdCiclo { get; set; }
        public string Nombre { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal StandPorEvaluar { get; set; }
        public int CantidadCriterios { get; set; }
        public EstandarModel EstandarActual { get; set; }
        public IEnumerable<EstandarModel> Estandares { get; set; }

        //para reportes
        public decimal PorcenRespondido { get; set; }
        public decimal PorcenObtenido { get; set; }
    }
    public class EstandarModel
    {
        public int Id_Estandar { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje_Max { get; set; }
        public decimal CalificacionFinal { get; set; }
        public SubEstandarModel SubEstandarActual { get; set; }
        public IEnumerable<SubEstandarModel> SubEstandares { get; set; }
    }
    public class SubEstandarModel
    {
        public int Id_SubEstandar { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_Corta { get; set; }
        public int Procentaje_Max { get; set; }
        public CriterioModel CriterioActual { get; set; }
        public IEnumerable<CriterioModel> Criterios { get; set; }

    }
    public class CriterioModel
    {
        public int Id_Criterio { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Numeral { get; set; }
        public string Marco_Legal { get; set; }
        public string Modo_Verificacion { get; set; }
    }

    public class ValoracionFinalModel
    {
        public int IdValoracionFinal { get; set; }
        public string Valoracion { get; set; }
        public string Accion { get; set; }
        public string CriterioValoracion { get; set; }        
    }

    public class EvaluacionPositivaModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string anioseleccionado { get; set; }        
        public string RazonSocial { get; set; }          
        public List<SelectListItem> Anios { get; set; }   
        public string Mensaje { get; set; }   
        public string url { get; set; }    
            
    }   
}