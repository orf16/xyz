using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Comunicaciones
{
    [Table("Tbl_ComunicacionesInternas")]
    public class ComunicacionesInternas
    {
        [Key]
        public int PK_Id_Encuesta { get; set; }
        public string Titulo { get; set; }
        public string CuerpoHTML { get; set; }
        public string EstadoEncuesta { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }
        public string NitEmpresa { get; set; }
        public string URL { get; set; }
        public string TokenPublico { get; set; }
    }
}
