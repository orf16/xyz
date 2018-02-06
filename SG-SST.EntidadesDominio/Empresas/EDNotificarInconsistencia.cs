using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDNotificarInconsistencia
    {
        public int Id { get; set; }
        public int IDTipoInconsistencia { get; set; }
        public string Observacion { get; set; }
        public string Email_Gerente { get; set; }

        public bool Rta { get; set; }

        public string Nombre_Gerente { get; set; }
        public string empresa_sistema { get; set; }
        public string empresa_nit_sistema { get; set; }
        public string usuario_sistema { get; set; }

        public string nombrePlantilla { get; set; }

    }
}
