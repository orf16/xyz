namespace SG_SST.Models.Aplicacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Maestro_Planeación_Inspeccion")]
    public class MaestroTipoInspeccion
    {
        [Key]
        public int Pk_Id_Maestro_Tipo_Inspeccion { get; set; }

        [Display(Name = "Tipo Inspección")]
        public string Descripcion_Tipo_Inspeccion { get; set; }
        public ICollection<PlaneacionInspeccion> planeacioninspeccion { get; set; }
       
    }
}
