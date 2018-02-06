using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_PeligroEMH")]
    public class PeligroEMH
    {
        [Key]
        public int Pk_Id_PeligroEMH { get; set; }

        [ForeignKey("Peligro")]
        public int Fk_Id_Peligro { get; set; }

        [ForeignKey("PK_Peligro")]
        public virtual Peligro Peligro { get; set; }

        [ForeignKey("AdmoEMH")]
        public int Fk_Id_AdmoEMH { get; set; }
        [ForeignKey("PK_AdmoEMH")]
        public virtual AdmoEMH AdmoEMH { get; set; }
    }
}
