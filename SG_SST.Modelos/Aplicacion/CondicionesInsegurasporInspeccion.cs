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
    [Table("Tbl_CondicionesInsegurasporInspeccion")]
    public class CondicionesInsegurasporInspeccion
    {
        [Key]
        public int Pk_Id_CondicionInseguraporInspeccion { get; set; }

        [ForeignKey("CondicionInsegura")]
        public int Fk_Id_CondicionInsegura { get; set; }
        [ForeignKey("Pk_Id_CondicionInsegura")]
        public virtual CondicionInsegura CondicionInsegura { get; set; }


        [ForeignKey("Inspecciones")]
        public int Fk_Id_Inspecciones { get; set; }
        [ForeignKey("Pk_Id_Inspecciones")]
        public virtual Inspecciones Inspecciones { get; set; }

      


    }
}
