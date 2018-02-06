using SG_SST.Models.Ausentismo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_EstudioPuestoTrabajo")]
    public class EstudioPuestoTrabajo
    {
        [Key]
        public int Pk_Id_EstudioPuestoTrabajo { get; set; }
        [MaxLength(15)]
        public string Numero_Identificacion { get; set; }
        public string Trabajador_Primer_Apellido { get; set; }
        public string Trabajador_Segundo_Apellido { get; set; }
        public string Trabajador_Primer_Nombre { get; set; }
        public string Trabajador_Segundo_Nombre { get; set; }
        public string Cargo_Empleado { get; set; }

        [ForeignKey("Sede")]
        public int FK_Id_Sede { get; set; }

        [ForeignKey("Proceso")]
        public int FK_Id_Proceso { get; set; }

        [ForeignKey("Diagnostico")]
        public int FK_Id_Diagnostico { get; set; }

        [ForeignKey("ObjetivoAnalisis")]
        public int FK_Id_ObjetivoAnalisis { get; set; }

        [ForeignKey("TipoAnalisisPuestoTrabajo")]
        public int FK_Id_Tipo_Analisis_Puesto_Trabajo { get; set; }

        public DateTime FechaAnalisis { get; set; }

        [ForeignKey("Pk_Id_Tipo_Analisis_Puesto_Trabajo")]
        public virtual TipoAnalisisPuestoTrabajo TipoAnalisisPuestoTrabajo { get; set; }

        [ForeignKey("PK_Id_ObjetivoAnalisis")]
        public virtual ObjetivoAnalisis ObjetivoAnalisis { get; set; }

        [ForeignKey("PK_Id_Diagnostico")]
        public virtual Diagnostico Diagnostico { get; set; }

        [ForeignKey("PK_Id_Sede")]
        public virtual Sede Sede { get; set; }

        [ForeignKey("PK_Id_Proceso")]
        public virtual Proceso Proceso { get; set; }

        public ICollection<SeguimientoEstudioPuestoTrabajo> SeguimientoEstudioPuestoTrabajo { get; set; }

        public ICollection<ArchivosEstudioPuestoTrabajo> ArchivosEstudioPuestoTrabajo { get; set; }
    }
}
