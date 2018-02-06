using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Empleado
{

     [Table("Tbl_TipoCotizante")]
    public class TipoCotizante
    {

        [Key]
        public int Pk_Id_Cotizante { get; set; }
        public string Descripcion { get; set; }




    }
}