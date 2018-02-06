using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public  class EDAnalisis
    {
        public int Pk_Id_Analisis { get; set; }
        public int Id_Analisis { get; set; }
        public short Tipo { get; set; }
        public string ValorTxt { get; set; }
        public int Parent_Id { get; set; }

        public int Fk_Id_Accion { get; set; }
    }
}
