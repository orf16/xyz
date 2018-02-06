using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDHallazgo
    {
        public int Pk_Id_Hallazgo { get; set; }
        public string Halla_Norma { get; set; }
        public string Halla_Numeral { get; set; }
        public string Halla_Descripcion { get; set; }
        public string Halla_Proceso { get; set; }
        public int Estado { set; get; }
        public int Clave { set; get; }
        public int Fk_Id_Accion { get; set; }
        public int Fk_Id_Proceso { get; set; }

    }
}
