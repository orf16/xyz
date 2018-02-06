using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_ConfiguracionesHHT")]
    public class ConfiguracionHHT
    {
        [Key]
        public int id_Configuracion { get; set; }
        public string Documento_empresa { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Dias_Laborales { get; set; }
        public decimal Horas_Laborales { get; set; }
        public decimal Num_Trabajadores_XT { get; set; }
        public decimal Dias_Trabajados_DTM { get; set; }
        public decimal Horas_Hombre_HTD { get; set; }
        public decimal Horas_Extras_NHE { get; set; }
        public decimal Horas_Ausentismo_NHA { get; set; }
        public System.DateTime Fecha_Modificacion { get; set; }
        public decimal Total_HHT { get; set; }        
    }
}
