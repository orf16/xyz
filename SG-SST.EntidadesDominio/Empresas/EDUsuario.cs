using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
   public class EDUsuario
    {
      public int PkUsuarioED { get; set; }

      public int NumeroDocumentoED { get; set; }
      public  int TipoDocumentoED { get; set; }
      public string NombreUsuarioED { get; set; }
      public string NitEmpresaED { get; set; }
      public int FkEmpresaED { get; set; }

      public string ImagenFirmausuarioED { get; set; }
    }
}
