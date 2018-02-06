using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_EmpresaTercero")]
    public class EmpresaTercero
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de la tabla empresa.
        /// </summary>
        [Key]
        public string PK_Nit_Empresa { get; set; }

        [Display(Name = "Razón Social")]
        public string Razon_Social { get; set; }

    }
}
