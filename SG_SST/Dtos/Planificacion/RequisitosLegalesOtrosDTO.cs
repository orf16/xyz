using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Planificacion
{
    public class RequisitosLegalesOtrosDTO
    {
        public RequisitosLegalesOtrosDTO()
        {
        }

        public RequisitosLegalesOtrosDTO(
            string Numero_Norma,
            DateTime Fecha_Publicacion,
            string Ente,
            string Articulo,
            string Descripcion,
            string Sugerencias,
           string Clase_De_Peligro,
            string Peligro,
            string Aspectos,
            string Impactos,
            string Evidencia_Cumplimiento,
          string Descripcion_Cumplimiento_Evaluacion,
            string Hallazgo,
            string Descripcion_Estado_RequisitoslegalesOtros,
            string Responsable,
            DateTime Fecha_Seguimiento_Control,
            DateTime Fecha_Actualizacion
            )
        {
            this.Numero_Norma = Numero_Norma;
            this.Fecha_Publicacion = Fecha_Publicacion;
            this.Ente = Ente;
            this.Articulo = Articulo;
            this.Descripcion = Descripcion;
            this.Clase_De_Peligro = Clase_De_Peligro;
            this.Peligro = Peligro;
            this.Aspectos = Aspectos;
            this.Impactos = Impactos;
            this.Evidencia_Cumplimiento = Evidencia_Cumplimiento;
            this.Descripcion_Cumplimiento_Evaluacion = Descripcion_Cumplimiento_Evaluacion;
            this.Hallazgo = Hallazgo;
            this.Descripcion_Estado_RequisitoslegalesOtros = Descripcion_Estado_RequisitoslegalesOtros;
            this.Responsable = Responsable;
            this.Fecha_Seguimiento_Control = Fecha_Seguimiento_Control;
            this.Fecha_Actualizacion = Fecha_Actualizacion;
        }

        public string Numero_Norma { get; set; }
        public DateTime Fecha_Publicacion { get; set; }
        public string Ente { get; set; }
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        public string Sugerencias { get; set; }
        public string Clase_De_Peligro { get; set; }
        public string Peligro { get; set; }
        public string Aspectos { get; set; }
        public string Impactos { get; set; }
        public string Evidencia_Cumplimiento { get; set; }
        public string Descripcion_Cumplimiento_Evaluacion { get; set; }
        public string Hallazgo { get; set; }
        public string Descripcion_Estado_RequisitoslegalesOtros { get; set; }
        public string Responsable { get; set; }
        public DateTime Fecha_Seguimiento_Control { get; set; }
        public DateTime Fecha_Actualizacion { get; set; }
    }
}
