using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Comunicaciones
{
     [Table("Tbl_ComunicacionesExternas")]
    public class ComunicacionesExternas
    {
        [Key]
        public int PK_Id_Comunicado { get; set; }
        public string Titulo { get; set; }
        public string Asunto { get; set; }
        public string CuerpoMensaje { get; set; }
        public string Destinatarios { get; set; }
        public string EstadoComunicado { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }
        public string NitEmpresa { get; set; }
    }
}
