using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Ausentismo
{
    public class DiasLaborables
    {
        public int idSeleccionado { get; set; }
        public List<SelectListItem> LtsDiasLaborables { get; set; }
        
    }
    public class ConfiguracionModel
    {
        public int idConfiguracion { get; set; }        
        public string  Anio { get; set; }                
        public int XT { get; set; }
        public int DTM { get; set; }
        public int HTD { get; set; }
        public int NHE { get; set; }
        public int NHA { get; set; }
        public int Total { get; set; }
        public bool IsLunesViernes { get; set; }
        public bool IsLunesSabado { get; set; }
        public string IdEmpresaSeleccionada { get; set; }
        public string RazonSocial { get; set; }
        public string Mes { get; set; }
        public string FechaModificacion { get; set; }
        public List<SelectListItem> Empresas { get; set; }
        public List<SelectListItem> Anos { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int MesSeleccionado { get; set; }

        public List<SelectListItem> Meses { get; set; }
        public List<SelectListItem> ConfigurarMeses()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "ENERO" },
                new SelectListItem() { Value = "2", Text = "FEBRERO" },
                new SelectListItem() { Value = "3", Text = "MARZO" },
                new SelectListItem() { Value = "4", Text = "ABRIL" },
                new SelectListItem() { Value = "5", Text = "MAYO" },
                new SelectListItem() { Value = "6", Text = "JUNIO" },
                new SelectListItem() { Value = "7", Text = "JULIO" },
                new SelectListItem() { Value = "8", Text = "AGOSTO" },
                new SelectListItem() { Value = "9", Text = "SEPTIEMBRE" },
                new SelectListItem() { Value = "10", Text = "OCTUBRE" },
                new SelectListItem() { Value = "11", Text = "NOVIEMBRE" },
                new SelectListItem() { Value = "12", Text = "DICIEMBRE" },
            };
        }

        public List<SelectListItem> GetAnos()
        {
            int AnoInicial = 2011;
            int AnoActual = DateTime.Now.Year;
            List<SelectListItem> Anos = new List<SelectListItem>(); 
                 Anos.Add(new SelectListItem() { Value = "", Text = "Año...", Selected=true });
            for (int i = 0; i <= (AnoActual - AnoInicial); i++)
            {
                Anos.Add(new SelectListItem() { Value = (AnoInicial + i).ToString(), Text = (AnoInicial + i).ToString() });
            }
            return Anos;
        }
    }
}