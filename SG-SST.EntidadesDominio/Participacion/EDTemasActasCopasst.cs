using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDTemasActasCopasst
    {
        public int? PK_Id_Acta { get; set; }
        public int Consecutivo_Acta { get; set; }
        public int IdSede { get; set; }
        public int PK_Id_TemaActa { get; set; }
        public string Tema { get; set; }
        public string Observaciones { get; set; }
        public int Fk_Id_Sede { get; set; }
        public string NombreSede { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
    }
}
