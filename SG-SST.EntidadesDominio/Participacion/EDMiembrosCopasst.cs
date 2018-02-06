using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDMiembrosCopasst
    {
        public int? PK_Id_Acta { get; set; }
        public int Consecutivo_Acta { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        public string NombreEmpresa { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Fk_Id_TipoPrioridadMiembro { get; set; }
        public string Des_TipoPrioridadMiembro { get; set; }
        public int? Fk_Id_TipoPrincipal { get; set; }
        public string Des_TipoPrincipal { get; set; }
        public string TipoRepresentante { get; set; }
     }
}
