using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Interfaces.EstudioPuestoTrabajo;
using SG_SST.InterfazManager.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.EstudioPuestoTrabajo
{
    public class LNSeguimientoEstudioPuestoTrabajo
    {
        private static ISeguimientoEstudioPuestoTrabajo objsegestudiopt = IMSeguimientoEstudioPuestoTrabajo.SeguimientoEstudioPT();

        public EDSeguimientoEstudioPuestoTrabajo GuardarSeguimiento(EDSeguimientoEstudioPuestoTrabajo seguimiento)
        {
            bool resultado = objsegestudiopt.GuardarSeguimiento(seguimiento);
            if (resultado)
                seguimiento.Result = "SUCCESS";
            else
                seguimiento.Result= "FAIL";

            return seguimiento;
        }
    }
}
