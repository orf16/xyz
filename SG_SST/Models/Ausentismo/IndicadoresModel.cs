using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SG_SST.Models.Ausentismo
{
    public class IndicadoresModel
    {
        public string IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string IdEmpresaUsuaria { get; set; }
        public List<SelectListItem> EmpresasUsuarias { get; set; }
        public int IdContingencia  { get; set; }
        public string Contingencia { get; set; }
        public int TipoContingeciaComparar { get; set; }
        public int PrimerAnio { get; set; }
        public int SegundoAnio { get; set; }

        public Nullable<int> Mes { get; set; }
        public Nullable<decimal> TotalMes { get; set; }
        public VariablesIndicadoresModel Ene { get; set; }
        public VariablesIndicadoresModel Feb { get; set; }
        public VariablesIndicadoresModel Mar { get; set; }
        public VariablesIndicadoresModel Abr { get; set; }
        public VariablesIndicadoresModel May { get; set; }
        public VariablesIndicadoresModel Jun { get; set; }
        public VariablesIndicadoresModel Jul { get; set; }
        public VariablesIndicadoresModel Ago { get; set; }
        public VariablesIndicadoresModel Sep { get; set; }
        public VariablesIndicadoresModel Oct { get; set; }
        public VariablesIndicadoresModel Nov { get; set; }
        public VariablesIndicadoresModel Dic { get; set; }
        public int AnioSeleccionado { get; set; }
        public List<SelectListItem> Anios { get; set; }
        public int ConstanteSeleccionada { get; set; }
        public List<SelectListItem> Constante { get; set; }
        
        public List<SelectListItem> Configurconstante()
        {
            return new List<SelectListItem>()
            {
                                                                                             
                new SelectListItem() { Value = "200000", Text = "200.000" },
                new SelectListItem() { Value = "220000", Text = "220.000" },
                new SelectListItem() { Value = "240000", Text = "240.000" },
                new SelectListItem() { Value = "1000000", Text = "1.000.000" },
            };
        }
    }
    public class VariablesIndicadoresModel
    {
        public decimal VariableIF { get; set; }
        public decimal VariableIS { get; set; }
        public decimal VaribleILI { get; set; }
        public decimal Tasa { get; set; }

    }

    public class AcumuladoTotalContingenciasModel
    {
        public string Mes { get; set; }
        public int EventosPorMes { get; set; }
        public decimal DiasAusenciaPorMes { get; set; }
        public decimal VariableIF { get; set; }
        public decimal VariableIS { get; set; }
        public decimal VariableILI { get; set; }
        public decimal Tasa { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public decimal NumeroTrabajadores { get; set; }
        public TotalAcumuladoModel TotalPeriodo { get; set; }
    }
    public class TotalAcumuladoModel
    {
        public decimal TotalVariableIF { get; set; }
        public decimal TotalVariableIS { get; set; }
        public decimal TotalVariableILI { get; set; }
        public decimal TotalTasa { get; set; }
        public decimal TotalHorasTrabajadas { get; set; }
        public decimal TotalNumeroTrabajadores { get; set; }
    }
}