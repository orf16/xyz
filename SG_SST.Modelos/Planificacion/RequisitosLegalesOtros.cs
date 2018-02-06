
namespace SG_SST.Models.Planificacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Planificacion;
    using SG_SST.Models.Empresas;
    using System.Data.Entity.SqlServer;
    using System;

    [Table("Tbl_Requisitos_legales_Otros")]
    public class RequisitosLegalesOtros
    {
        [Key]
        public int PK_RequisitosLegalesOtros { get; set; }


        public string Tipo_Norma { get; set; }

        public string Numero_Norma { get; set; }



       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime FechaPublicacion { get; set; }


        //[StringLength(20, ErrorMessage = "El campo ente es requerio")]
        public string Ente { get; set; }


        //[StringLength(20, ErrorMessage = "El campo Articulo es requerido")]
        public string Articulo { get; set; }


        //[StringLength(100, ErrorMessage = "El campo Descripcion  es requerio")]
        public string Descripcion { get; set; }


        //[StringLength(50, ErrorMessage = "El campo Sugerencias es requerio")]
        public string Sugerencias { get; set; }


        //[StringLength(20, ErrorMessage = "El campo Clase De Peligro es requerido")]
        public string Clase_De_Peligro { get; set; }


        //[StringLength(20, ErrorMessage = "El campo Peligro es requerido")]
        public string Peligro { get; set; }


        //[StringLength(20, ErrorMessage = "El campo Aspectos es requerido")]
        public string Aspectos { get; set; }


        // [StringLength(20, ErrorMessage = "El campo Impactos es requerido")]
        public string Impactos { get; set; }


        // [StringLength(50, ErrorMessage = "El campo Evidencia de Cumplimiento es requerido")]
        public string Evidencia_Cumplimiento { get; set; }


        //[StringLength(30, ErrorMessage = "El campo Cumplimiento es requerido")]
        [ForeignKey("Tbl_Cumplimiento_Evaluacion")]
        public int FK_Cumplimiento_Evaluacion { get; set; }

        [ForeignKey("PK_Cumplimiento_Evaluacion")]
        public virtual Cumplimiento_Evaluacion Tbl_Cumplimiento_Evaluacion { get; set; }


        //[StringLength(50, ErrorMessage = "El campo Hallazgo es requerido")]
        public string Hallazgo { get; set; }


        //[StringLength(30, ErrorMessage = "El campo Estado es requerido")]
        [ForeignKey("Tbl_Estado_RequisitoslegalesOtros")]
        public int FK_Estado_RequisitoslegalesOtros { get; set; }

        [ForeignKey("PK_Estado_RequisitoslegalesOtros")]
        public virtual Estado_RequisitoslegalesOtros Tbl_Estado_RequisitoslegalesOtros { get; set; }


        //[StringLength(30, ErrorMessage = "El campo Responsable es requerido")]
        public string Responsable { get; set; }



        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Seguimiento_Control { get; set; }




        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Actualizacion { get; set; }


      


        [ForeignKey("Tbl_Actividad_Economica")]
        public int FK_Actividad_Economica { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_Actividad_Economica")]
        public virtual ActividadEconomica Tbl_Actividad_Economica { get; set; }



    }
}
