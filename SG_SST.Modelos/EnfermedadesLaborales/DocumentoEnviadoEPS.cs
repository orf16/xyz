using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.EnfermedadesLaborales
{
    [Table("Tbl_DocumentosEnviadosEPS")]
    public class DocumentoEnviadoEPS
    {
        [Key]
        public int Pk_Id_DocumentoEnviadoEPS { get; set; }
        public string RutaDocumentoEnviadoEPS { get; set; }
        public DateTime FechaRegistroDocumento { get; set; }

        [ForeignKey("EnfermedadLaboral")]
        public int Fk_Id_EnfermedadLaboral { get; set; }

        [ForeignKey("Pk_Id_EnfermedadLaboral")]
        public virtual EnfermedadLaboral EnfermedadLaboral { get; set; }
    }
}
