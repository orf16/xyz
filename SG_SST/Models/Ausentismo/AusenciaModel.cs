using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SG_SST.Models.Ausentismo
{
    public class AusenciaModel
    {
        public string RazonSocial { get; set; }
        public string IdEmpresaUsuaria { get; set; }
        public AfiliadoModel DatosTrabajor { get; set; }
        public int IdAusencia { get; set; }
        public string Tipo { get; set; }
        public string NombrePersona { get; set; }
        public string Documento { get; set; }
        public string IdEmpresa { get; set; }
        public string Departamento { get; set; }
        public int idDepartamento { get; set; }
        public string Municipio { get; set; }
        public int idMunicipio { get; set; }
        public int IdContingencia { get; set; }
        public int IdDiagnostico { get; set; }
        public string FechaRegistro { get; set; }

        public string Sede { get; set; }
        public int idSede { get; set; }
        public int idProceso { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFinalizacion { get; set; }
        public string DiasAusencia { get; set; }
        public string Costo { get; set; }
        public string Sexo { get; set; }
        public string TipoVinculacion { get; set; }
        public int idOcupacion { get; set; }
        public string Eps { get; set; }
        public Nullable<int> Edad { get; set; }
        public string FactorPrestacional { get; set; }
        public string Observaciones { get; set; }
        public int AnioSeleccionado { get; set; }
        public ContingeciaModel Contingencia { get; set; }        
        public DiagnosticoModel Diagnostico { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public List<SelectListItem> EmpresasUsuarias { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Municipios { get; set; }
        public List<SelectListItem> Contingecias { get; set; }

        public List<SelectListItem> Anios { get; set; }
        public List<SelectListItem> Procesos { get; set; }
        public List<SelectListItem> HorasConfigurables { get; set; }

        public HttpPostedFileBase file { get; set; }
    }

    public class ContingeciaModel
    {
        public int IdContingenciaSeleccionada { get; set; }
        public string TipoContingencia { get; set; }
        public List<SelectListItem> Contingencias { get; set; }
    }

    public class DiagnosticoModel
    {        
        public int IdDiagnoticoSeleccionado { get; set; }
        public string TipoDiagnostico { get; set; }
        public List<SelectListItem> Diagnosticos { get; set; }
    }

    public class EmpresasUsuariasModel
    {        
        public int IdEmpresaUsuaria { get; set; }
        public string RazonSocialUsuaria { get; set; }        
    }

  
}