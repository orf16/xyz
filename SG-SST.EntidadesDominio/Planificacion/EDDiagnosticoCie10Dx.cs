using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDDiagnosticoCie10Dx
    {
       
        public int idDiagnosticoCie10Dx { get; set; }
     
        public int FK_DxCondicionesDeSalud { get; set; }

        public int NumeroTrabajadoresConDiagnostico { get; set; }

        public int IdDiagnostico { get; set; }

        public string NombreDiagnosticoCIE10 { get; set; }

        public double porcentajeDiagnostico { get; set; }
    }
}
