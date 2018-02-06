

namespace SG_SST.Models.Planificacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Planificacion;
    using SG_SST.Models.Empresas;
    using System.Data.Entity.SqlServer;
    using System;


    [Table("Tbl_Requisitos_Legales_Posipedia")]
    public class RequisitosLegalesPosipedia
    {
        [Key]
        public int PK_RequisitosLegalesOtros { get; set; }


        public string Tipo_Norma { get; set; }

        public string Numero_Norma { get; set; }

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






        [ForeignKey("Tbl_Actividad_Economica")]
        public int FK_Actividad_Economica { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_Actividad_Economica")]
        public virtual ActividadEconomica Tbl_Actividad_Economica { get; set; }


    }
}



