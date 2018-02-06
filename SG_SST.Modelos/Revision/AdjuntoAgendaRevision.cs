using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Revision
{
    [Table("Tbl_AdjuntoAgendaRevision")]
    public class AdjuntoAgendaRevision
    {
        [Key]
        public int PK_Id_AdjuntoAgendaRevision { get; set; }


        [ForeignKey("Tbl_AgendaRevision")]
        public int FK_AgendaRevision { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_Id_AgendaRevision")]
        public virtual AgendaRevision Tbl_AgendaRevision { get; set; }

        public string Nombre_Archivo { get; set; }

    }
}
