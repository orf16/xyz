using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.EnfermedadesLaborales
{
    [Table("Tbl_InstanciasEnfermedadLaboralDiagnosticada")]
    public class InstanciaEnfermedadLaboral
    {
        [Key]
        public int Pk_Id_InstanciaEnfermedadLaboral { get; set; }
        public int EstadoInstancia { get; set; }
        public string QuienCalifica { get; set; }
        
        public DateTime FechaCalificacion { get; set; }

        [ForeignKey("EnfermedadLaboral")]
        public int Fk_Id_EnfermedadLaboral { get; set; }

        [ForeignKey("EstadoInstanciaRegistrada")]
        public int FK_Id_EstadoInstancia { get; set; }

        [ForeignKey("Pk_Id_EnfermedadLaboral")]
        public virtual EnfermedadLaboral EnfermedadLaboral { get; set; }
        [ForeignKey("PK_Id_EstadoInstancia")]
        public virtual EstadoInstanciaRegistrada EstadoInstanciaRegistrada { get; set; }
    }
}
