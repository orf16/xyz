using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDResponsablesQuejas
    {
        public int Pk_Id_Responsable { get; set; }
         public int Numero_Documento { get; set; }
         public string Nombre { get; set; }
        public int Fk_Id_Queja { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public int? PK_Id_Acta { get; set; }
    }
}
