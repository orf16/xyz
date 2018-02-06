using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SG_SST.Models.Revision
{
    [Table("Tbl_ParticipanteRevision")]
    public class ParticipanteRevision
    {
        [Key]
        public int PK_Id_ParticipanteRevision { get; set; }

        public string Nombre { get; set; }

        public string Documento { get; set; }

        public string Cargo { get; set; }

        [ForeignKey("Tbl_ActaRevision")]
        public int FK_ActaRevision { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_Id_ActaRevision")]
        public virtual ActaRevision Tbl_ActaRevision { get; set; }
    }
}
