using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SG_SST.Models.Ausentismo
{
    public class AlertasModel
    {
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Documento { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Contingencia { get; set; }
        public string Diagnostico { get; set; }
        public decimal Dias_Ausencia { get; set; }
        public decimal Costo { get; set; }
        public int AnioSeleccionado { get; set; }
        public string RazonSocial { get; set; }
        public int IdEmpresaUsuaria { get; set; }
        public List<SelectListItem> EmpresasUsuarias { get; set; }
        public List<SelectListItem> Anios { get; set; }
        public List<SelectListItem> ConfigurarAnios()
        {
            return Enumerable.Range(DateTime.Now.Year - 9, 10)
                .OrderByDescending(x => x).
                Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                }).ToList();           
        }
    }
}

