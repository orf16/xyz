using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SG_SST.Models.Ausentismo
{
    public class ReportesModel
    {
        public string CONTINGENCIA { get; set; }
        public string Departamento { get; set; }
        public string Enfermedades { get; set; }
        public string Eps { get; set; }
        public string Ocupacion { get; set; }
        public string Sede { get; set; }
        public string Sexo { get; set; }
        public string Descripcion { get; set; }
        public string Evento { get; set; }
        public string RazonSocial { get; set; }
        
        public int anio { get; set; }
        public int idOrigen {get;set;}
        public int IdEmpresaUsuaria { get; set; }
        public int idSede { get; set; }
        public int IdDepartamento { get; set; }
        public string IdReporte { get; set; }
        public string Reporte { get; set; }


        public Nullable<decimal> Ene { get; set; }
        public Nullable<decimal> Feb { get; set; }
        public Nullable<decimal> Mar { get; set; }
        public Nullable<decimal> Abr { get; set; }
        public Nullable<decimal> May { get; set; }
        public Nullable<decimal> Jun { get; set; }
        public Nullable<decimal> Jul { get; set; }
        public Nullable<decimal> Ago { get; set; }
        public Nullable<decimal> Sep { get; set; }
        public Nullable<decimal> Oct { get; set; }
        public Nullable<decimal> Nov { get; set; }
        public Nullable<decimal> Dic { get; set; }
        public Nullable<decimal> Total { get; set; }
        
        public string strEne { get; set; }
        public string strFeb { get; set; }
        public string strMar { get; set; }
        public string strAbr { get; set; }
        public string strMay { get; set; }
        public string strJun { get; set; }
        public string strJul { get; set; }
        public string strAgo { get; set; }
        public string strSep { get; set; }
        public string strOct { get; set; }
        public string strNov { get; set; }
        public string strDic { get; set; }
        public string strTotal { get; set; }
        public int AnioSeleccionado { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public List<SelectListItem> Anios { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> EmpresasUsuarias { get; set; }

        public List<SelectListItem> Reportes { get; set; }
        public List<SelectListItem> GetResportes()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() { Value = "AC", Text = "Días Ausentismo por contingencia" },
                new SelectListItem() { Value = "NC", Text = "Número de eventos por contingencia" },
                new SelectListItem() { Value = "ADP", Text = "Dias ausentismo por Departamentos" },
                new SelectListItem() { Value = "DCIE", Text = "Dias ausentismo por capitulos de enfermedades CIE10" },               
                new SelectListItem() { Value = "NCIE", Text = "Número de eventos por capitulos de enfermedades CIE10" },
                new SelectListItem() { Value = "DP", Text = "Dias de ausentismo por proceso" },
                new SelectListItem() { Value = "DS", Text = "Dias de ausentismo por Sede" },
                new SelectListItem() { Value = "PC", Text = "Costos por contingencias" },
                new SelectListItem() { Value = "AEPS", Text = "Ausentismos por EPS" },
                new SelectListItem() { Value = "ASX", Text = "Ausentismos por sexo" },
                new SelectListItem() { Value = "AV", Text = "Ausentismos por tipo vinculacion" },
                new SelectListItem() { Value = "AO", Text = "Ausentismos por ocupacion CIUO" },
                new SelectListItem() { Value = "AET", Text = "Ausentismos por Grupos Etarios" }
            };
        }        
    }
}