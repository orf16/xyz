using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Revision
{
    [Table("Tbl_ItemRevision")]
    public class ItemRevision
    {
        [Key]
        public int PK_Id_ItemRevision { get; set; }

        public string Tema { get; set; }

    }
}
