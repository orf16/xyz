using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDTipoDocumento
    {
        public int Id_Tipo_Documento { get; set; }
        public string Descripcion { get; set; }
        public string Sigla { get; set; }
        public bool AplicaPersonas { get; set; }
        public bool AplicaEmpresas { get; set; }

        public List<EDTipoDocumento> TiposDocumentos { get; set; }
    }
}
