using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.ComunicadosAPP
{
    [Table("Tbl_ComunicadosAPP")]
    public class ComunicacionesAPP
    {

        [Key]
        public int IDComunicadosAPP { get; set; }
        public string Titulo { get; set; }
        public string Asunto { get; set; }
        public string AsuntoAPP { get; set; }
        public string Destinatarios { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }
        public string Estado { get; set; }
        public string NitEmpresa { get; set; }
        
    }
}
