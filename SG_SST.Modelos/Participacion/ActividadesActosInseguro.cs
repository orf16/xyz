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

    [Table("Tbl_ActividadesActosInseguros")]
    public class ActividadesActosInseguros
    {
        [Key]
        public int PK_ID_ActividadActosInseguros { get; set; }

        public string NombreActividad { get; set;}
        public string ResponsableActividad { get; set; }

        public DateTime FechaEjecucion { get; set; }

        // Relacion entre la tabla ActividadActosInseguros y la tabla Reporte
        [ForeignKey("Reporte")]
        public int FK_Id_Reportes { get; set; }

        [ForeignKey("PK_Id_Reportes")]
        public virtual Reporte Reporte { get; set; }


    }
}