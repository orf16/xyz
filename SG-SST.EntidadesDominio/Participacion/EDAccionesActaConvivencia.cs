using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDAccionesActaConvivencia
    {
        public int? PK_Id_Acta { get; set; }
        public int Consecutivo_Acta { get; set; }
        public int IdSede { get; set; }
        public int Pk_Id_AccionActaConvivencia { get; set; }
        public DateTime FechaProbable { get; set; }
        public string AccionARealizar { get; set; }
        public string Responsable { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
    }
}
