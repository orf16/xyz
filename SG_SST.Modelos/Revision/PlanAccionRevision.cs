using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Revision
{
    [Table("Tbl_PlanAccionRevision")]
    public class PlanAccionRevision
    {
        [Key]
        public int PK_Id_PlanAccion { get; set; }


        [ForeignKey("Tbl_Acta")]
        public int FK_Acta { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_Id_Acta")]
        public virtual ActaRevision Tbl_Acta { get; set; }

        public string Actividad { get; set; }

        public string Responsable { get; set; }

        public DateTime Fecha { get; set; }

        public string Num_Acta { get; set; }

    }
}
