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


    [Table("Tbl_ImagenesReportes")]
    public class ImagenesReportes
    {
        [Key]
        public int PK_ImagenesReportes { get; set; }

        public string ruta { get; set; }

        [ForeignKey("Reporte")]
        public int FK_Id_Reportes { get; set; }

        [ForeignKey("PK_Id_Reportes")]
        public virtual Reporte Reporte { get; set; }

    }
}