using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_Tipo_Reporte")]
    public class TipoReporte
    {
        [Key]
        public int Pk_Id_Tipo_Reporte { get; set; }

        [Display(Name = "Tipo Reporte")]
        public string Descripcion_Tipo_Reporte { get; set; }
    }
}
