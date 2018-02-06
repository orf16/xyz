
namespace SG_SST.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using SG_SST.Models.Empresas;

     [Table("Tbl_Organigrama")]
    public class Organigrama
    {
        [Key]
        public int Pk_Id_Organigrama { get; set; }
        //public string Nombre_Organigrama { get; set; }

        public string Descripcion_Organigrama { get; set; }

        //public int Fk_id_Gobierno { get; set; }

        public string Imagen_Organigrama { get; set; }

        [ForeignKey("Empresa")]
        public int  Fk_Id_Empresa { get; set; }
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

        public ICollection<EmpleadoOrg> EmpleadosOrg { get; set; }
       

    }
}