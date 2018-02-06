
namespace SG_SST.Models.Organizacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;
    using Aplicacion;

    [Table("Tbl_Cargo")]
    public class Cargo
    {
        [Key]
        public int Pk_Id_Cargo { get; set; }
        public String Nombre_Cargo { get; set; }
        public ICollection<CargoPorRol> CargoPorRol { get; set; }
        public ICollection<EPPSuministro> EPPSuministros { get; set; }
    }
}
