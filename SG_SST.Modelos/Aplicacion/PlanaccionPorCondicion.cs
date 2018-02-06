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
    [Table("Tbl_PlanAccionporCondicion")]
    public class PlanaccionPorCondicion
    {
        [Key]
        public int Pk_Id_PlanaccionporCondicion { get; set; }


        [ForeignKey("PlanAccionInspeccion")]
        public int? Fk_Id_PlanAcccionInspeccion { get; set; }
        [ForeignKey("Pk_Id_PlanAcccionInspeccion")]
        public virtual PlanAccionInspeccion PlanAccionInspeccion { get; set; }


        [ForeignKey("CondicionInsegura")]
        public int? Fk_Id_CondicionInsegura { get; set; }
        [ForeignKey("Pk_Id_CondicionInsegura")]
        public virtual CondicionInsegura CondicionInsegura { get; set; }
    }
}
