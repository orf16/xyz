using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Calendario
{
    [Table("Tbl_DiaFestivo")]
    public class DiaFestivo
    {
        [Key]
        public int PK_Id_DiaFestivo { get; set; }

        public int Anio { get; set; }

        public int Mes { get; set; }

        public int Dia { get; set; }

        public DateTime Fecha { get; set; }
    }
}
