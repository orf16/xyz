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
    [Table("Tbl_AsistentesporInspeccion")]
    public class AsistentesporInspeccion
    {
        [Key]
       public int Pk_Id_AsistenteporInspeccion { get; set; }


       [ForeignKey("Asistentes")]
        
       public int Fk_Id_Asistente { get; set; }
       [ForeignKey("Pk_Id_Asistente")]
       public virtual Asistentes Asistentes { get; set; }


       [ForeignKey("Inspecciones")]
       public int Fk_Id_Inspeccion { get; set; }
       [ForeignKey("Pk_Id_Inspeccion")]
       public virtual Inspecciones Inspecciones { get; set; }


    }
}
