using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDEPPSuministro
    {
        public int Pk_Id_SuministroEPP { get; set; }
        public string CedulaTrabajador { get; set; }
        public string NombreTrabajador { get; set; }
        public int Fk_Id_Cargo { get; set; }
        public int Fk_Id_Proceso { get; set; }
        public int Fk_Id_Sede { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public List<EDEPPSuministroEPP> ListaEPPSuministros { get; set; }
        public string ProcesoNombre { get; set; }
        public string SedeNombre { get; set; }
        public string CargoNombre { get; set; }
        public DateTime Fecha { get; set; }

    }
}
