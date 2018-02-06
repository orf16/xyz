using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Comunicaciones
{
    [Table("Tbl_ComunicacionesCargos")]
    public class ComunicacionesCargos
    {
        [Key]
        public int Pk_Id_ComunicacionesCargos { get; set; }
        public int Numero_Documento { get; set; }
        public string Nombre_Completo_Empleado { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public int NitEmpresa { get; set; }

    }
}

