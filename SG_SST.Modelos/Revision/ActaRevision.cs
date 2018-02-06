using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empresas;


namespace SG_SST.Models.Revision
{
     [Table("Tbl_ActaRevision")]
    public class ActaRevision
    {
        [Key]
         public int PK_Id_ActaRevision { get; set; }

        public string Nombre { get; set; }
                
        public int Num_Acta { get; set; }
        
        public DateTime Fecha_Creacion_Acta{ get; set; }

        public int Fk_Id_Empresa { get; set; }

        /// Obtiene y establece una clave foranea a sede
        /// </summary>
        [ForeignKey("Sede")]
        public int FK_Sede { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo Sede
        /// </summary>
        [ForeignKey("Pk_Id_Sede ")]        
        public virtual Sede Sede { get; set; }

        public DateTime Fecha_Inicial_Revision { get; set; }
        
        public DateTime Fecha_Final_Revision { get; set; }

        public string Elaborada { get; set; }

        public string Firma_Gerente_General { get; set; }

        public bool Firma_Representante_SGSST { get; set; }
        
        public bool Firma_Responsable_SGSST { get; set; }   

    }

}
