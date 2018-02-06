using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_AdministracionEMHInspecciones")]
    public class EHMInspecciones
    {
        [Key]
        public int Pk_Id_EHMInspecciones { get; set; }

        //Relacion Inspecciones
        [ForeignKey("Inspecciones")]
        public int Fk_Id_Inspecciones { get; set; }
        [ForeignKey("PK_Inspecciones")]
        public virtual Inspecciones Inspecciones { get; set; }

        //Relacion Administracion equipo, maquinaria y herramienta
        [ForeignKey("AdmoEMH")]
        public int Fk_Id_AdmoEMH { get; set; }
        [ForeignKey("PK_AdmoEMH")]
        public virtual AdmoEMH AdmoEMH { get; set; }
    }
}
