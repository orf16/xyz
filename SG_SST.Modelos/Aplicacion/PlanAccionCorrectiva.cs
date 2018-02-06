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
    [Table("Tbl_PlanAccionCorrectiva")]
    public class PlanAccionCorrectiva
    {
        [Key]
        public int Pk_Plan_Accion_Correctiva { get; set; }
        public string  Adjunto_Seguimiento { get; set; }
        public string Nombre_Verificador { get; set; }
        public string Respuesta { get; set; }


        [ForeignKey("PlanAccionInspeccion")]
        public int Fk_Id_PlanAcccionInspeccion { get; set; }

        [ForeignKey(" Pk_Id_PlanAcccionInspeccion")]
        public virtual PlanAccionInspeccion PlanAccionInspeccion { get; set; }
        

    }
}
