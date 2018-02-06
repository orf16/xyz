using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_PreguntasSeguridad")]
    public class PreguntaSeguridad
    {
        [Key]
        public int Pk_Id_PreguntaSeguridad { get; set; }
        public string NombrePreguntaSeguridad { get; set; }
        public string Descricion { get; set; }
    }
}
