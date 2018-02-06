
namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Gobierno")]
    public class Gobierno
    {
        /// <summary>
        /// Obtiene y establece el IDgobierno.
        /// </summary>
        [Key]
        public int Pk_Id_Gobierno { get; set; }

        /// <summary>
        /// Obtiene y establece el NitEmpresa.
        /// </summary>
        /// 
        [Display(Name="Nit Empresa")]
        public int Nit_Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece la Mision.
        /// </summary>
        /// 
       
        //[StringLength(200, MinimumLength = 10, ErrorMessage = "La Mision debe de tener Minimo 10 Caracteres y maximo 200")]
        public string Mision { get; set; }

        /// <summary>
        /// Obtiene y establece la Vision.
        /// </summary>
        //[StringLength(200, MinimumLength = 10, ErrorMessage = "La Vision debe de tener Minimo 10 Caracteres y maximo 200")]
        public string Vision { get; set; }


        //[ForeignKey("Organigrama")]
        //public int Fk_Id_Organigrama { get; set; }

        //[ForeignKey("Pk_Id_Organigrama")]
        //public virtual Organigrama Organigrama { get; set; }



        //[ForeignKey("Proceso")]
        //public int Fk_Id_Proceso { get; set; }

        //[ForeignKey("Pk_Id_Proceso")]
        //public virtual Proceso Proceso { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }

        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

    }
}