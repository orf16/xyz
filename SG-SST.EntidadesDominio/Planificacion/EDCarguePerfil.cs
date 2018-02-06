using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDCarguePerfil
    {

        public string path { get; set; }
        public string Message { get; set; }
        public string NitEmpresa { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }

        public string Direccion { get; set; }

        public string Documento { get; set; }

        public string SiglaTipoDocumentoEmpresa { get; set; }

        public string rutaServicio { get; set; }

        public string NitEmpresaU { get; set; }

        public string tipoDocumeto { get; set; }

        public int total { get; set; }



    }
}
