using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Seguimiento_Estudio_Puesto_Trabajo")]
    public class SeguimientoEstudioPuestoTrabajo
    {
        [Key]
        public int PK_Id_Seguimiento_Estudio_Puesto_Trabajo { get; set; }

        public string Actividad { get; set; }

        public DateTime Fecha { get; set; }

        public string Responsable { get; set; }

        
    }
}
