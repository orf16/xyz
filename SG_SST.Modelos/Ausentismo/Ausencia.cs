using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_Ausencias")]
    public  class Ausencia
    {
        [Key]
        public int Pk_Id_Ausencias { get; set; }
        public int FK_Id_Ausencias_Padre { get; set; }
        public string NombrePersona { get; set; }
        public string Documento_Persona { get; set; }
        public string NitEmpresa { get; set; }
        [ForeignKey("Contingencia")]
        public int FK_Id_Contingencia { get; set; }
        [ForeignKey("Diagnostico")]
        public int FK_Id_Diagnostico { get; set; }
        public int FK_Id_Sede { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public decimal DiasAusencia { get; set; }
        public decimal Costo { get; set; }
        public decimal Factor_Prestacional { get; set; }
        public string Observaciones { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public int FK_Id_Departamento { get; set; }
        public int FK_Id_Municipio { get; set; }
        public int FK_Id_Proceso { get; set; }
        public string Sexo { get; set; }
        public string Tipo_Vinculacion { get; set; }
        public int FK_Id_Ocupacion { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Eps { get; set; }
        public int FK_Id_EmpresaUsuaria { get; set; }

        [ForeignKey("PK_Id_Contingencia")]
        public virtual Contingencia Contingencia { get; set; }
        [ForeignKey("PK_Id_Diagnostico")]
        public virtual Diagnostico Diagnostico { get; set; }       
    }
}
