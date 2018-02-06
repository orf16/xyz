using SG_SST.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.EnfermedadLaboral
{
    /// <summary>
    /// 
    /// </summary>
    public class EnfermedadLaboralModel
    {
        #region Datos del Trabajador
        public int UsuarioQuienRegistraELD { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreTrabajador { get; set; }
        public string FechaNacimiento { get; set; }
        public string Geneero { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int IdMunicipio { get; set; }
        public string Municipio { get; set; }
        public string Cargo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Diagnostico { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string DiagnosticoCIIE10 { get; set; }

        [ValidaArchivos]
        public HttpPostedFileBase Furel { get; set; }

        public string RutaDocumentoFurel { get; set; }
        #endregion
        #region Documentación
        [ValidaArchivos]
        public HttpPostedFileBase CartaEPS { get; set; }
        public string RutaCartaEPS { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaDocumentosEPS { get; set; }

        [ValidaArchivos]
        public HttpPostedFileBase TipoDocumentoCalificacion { get; set; }
        public List<string> RutaTiposDocumentoCalificacion { get; set; }

        public HttpPostedFileBase[] TiposDocumentosCalificacion { get; set; }
        #endregion
        #region Instancias
        public InstanciaRegistrada InstanciaARegistrar { get; set; }
        public List<InstanciaRegistrada> InstanciasRegistradas { get; set; }
        #endregion
        #region Listas formulario
        public List<SelectListItem> DiagnosticosCIIE10 { get; set; }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class InstanciaRegistrada
    {
        public int IdInstancia { get; set; }
        public string Nombre { get; set; }
        public int EstadoInstancia { get; set; }
        public string NombreEstadoInstancia { get; set; }
        public string QuienCalifica { get; set; }
        public DateTime FechaCalificacion { get; set; }
        private List<SelectListItem> _estadosInstancia;
        public List<SelectListItem> EstadosInstancia
        {
            get
            {
                return _estadosInstancia;
            }
            set
            {
                _estadosInstancia = new List<SelectListItem>
                    {
                        new SelectListItem() { Value = "", Text = "Seleccione una opción" },
                        new SelectListItem() { Value = "1", Text = "En Estudio" },
                        new SelectListItem() { Value = "2", Text = "Laboral" },
                        new SelectListItem() { Value = "3", Text = "Común" }
                    };
            }
        }
    }
}