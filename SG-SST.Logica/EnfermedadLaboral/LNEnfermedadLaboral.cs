using SG_SST.EntidadesDominio.EnfermedadLaboral;
using SG_SST.Interfaces.EnfermedadLaboral;
using SG_SST.InterfazManager.EnfermedadLaboral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.EnfermedadLaboral
{
    public class LNEnfermedadLaboral
    {
        private static IEnfermedadLaboral enfLab = IMEnfermedadLaboral.EnfermedadLaboral();
        public EDEnfermedadLaboral RegistrarEnfermedadLaboralDiagnosticada(EDEnfermedadLaboral enfermedadARegistrar)
        {
            var resultado = enfLab.RegistrarEnfermedadLaboralDiagnosticada(enfermedadARegistrar);
            if (resultado != null)
                return resultado;
            else
                return null;
        }
    }
}
