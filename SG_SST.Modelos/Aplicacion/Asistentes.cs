using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_Asistentes")]
    public class Asistentes
    {
        [Key]
        public int Pk_Id_Asistente { get; set; }

        public string Nombre_Asistente { get; set; }

        public ICollection<AsistentesporInspeccion> AsistentesporInspeccion { get; set; }

    }
}
