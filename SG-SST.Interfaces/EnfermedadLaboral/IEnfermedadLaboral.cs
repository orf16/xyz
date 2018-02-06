using SG_SST.EntidadesDominio.EnfermedadLaboral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.EnfermedadLaboral
{
    public interface IEnfermedadLaboral
    {
        EDEnfermedadLaboral RegistrarEnfermedadLaboralDiagnosticada(EDEnfermedadLaboral enfermedadARegistrar);
    }
}
