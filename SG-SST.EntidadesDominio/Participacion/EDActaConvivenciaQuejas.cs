using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDActaConvivenciaQuejas
    {
        public int PK_Id_Queja { get; set; }
        public int Consecutivo_Queja { get; set; }
        public int Consecutivo_Caso { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreRefiereSituacion { get; set; }
        public string AspectosNoResueltos { get; set; }
        public string Compromisos { get; set; }
        public int Fk_Id_Acta { get; set; }
        public int Fk_Id_Sede { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public List<EDResponsablesQuejas> ResponsablesQuejas { get; set; }
        public List<EDAccionesActaQuejas> AccionesActaQuejas { get; set; }
    }
}
