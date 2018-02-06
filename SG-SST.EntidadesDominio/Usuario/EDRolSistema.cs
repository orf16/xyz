using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Usuario
{
    public class EDRolSistema
    {
        public int IdRolSistema { get; set; }
        public string NombreRol { get; set; }
        public string Sigla { get; set; }
        public int CantidadMaxUsuarios { get; set; }
        public bool Activo { get; set; }
    }
}
