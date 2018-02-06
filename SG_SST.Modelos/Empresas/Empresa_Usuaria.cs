using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Empresas_Usuarias")]
    public class Empresa_Usuaria
    {
        [Key]
        public int PK_Id_Empresa_Usuaria { get; set; }
        public string  Documento_Empresa { get; set; }
        public string  Documento_Empresa_Usuaria { get; set; }
        public int FK_Id_Tipo_Documento { get; set; }
        public string Razon_Social { get; set; }
        public string Direccion { get; set; }
        public int FK_Id_Departamento { get; set; }
        public int FK_Id_Municipio { get; set; }
    }
}
