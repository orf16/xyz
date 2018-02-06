using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.EnfermedadesLaborales
{
    [Table("Tbl_EnfermedadesLaboralesDiagnosticadas")]
    public class EnfermedadLaboral
    {
        [Key]
        public int Pk_Id_EnfermedadLaboral { get; set; }
        public int  CodigoEmpleado { get; set; }
        public int CodigoDiagnosticoCIIE10 { get; set; }
        public string Diagnostico { get; set; }
        public string RutaDocumentoFUREL { get; set; }
        public string RutaCartaEnviadaEPS { get; set; }
        public DateTime FechaEnvioDocumentosEPS { get; set; }
        public virtual ICollection<DocumentoEnviadoEPS> DocumentosEnviadosEPS { get; set; }
        public virtual ICollection<InstanciaEnfermedadLaboral> InstanciasEnfermedadLaboral { get; set; }
    }
}
