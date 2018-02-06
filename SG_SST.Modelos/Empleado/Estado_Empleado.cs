using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Empleado
{
      [Table("Tbl_Estado_Empl")]
    public class Estado_Empleado
    {

        [Key]
        public int PK_IDEmpleadoEst { get; set; }
        public string EstEmplead { get; set; }




    }
}