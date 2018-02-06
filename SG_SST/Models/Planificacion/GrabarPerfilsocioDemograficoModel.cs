using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Planificacion
{
    public class GrabarPerfilsocioDemograficoModel
    {
        public int Tipo_Documento { get; set; }
        public int NumeroDocumentoEmpleado { get; set; }
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public int Empresa { get; set; }


    }
}