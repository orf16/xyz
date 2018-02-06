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
    public class LNArchivoEstudioPuestoTrabajo
    {
        private static IArchivoEstudioPuestoTrabajo objarchestudiopt = IMArchivoEstudioPuestoTrabajo.EstudioPT();

        public EDArchivoEstudioPuestoTrabajo GuardarArchivo(EDArchivoEstudioPuestoTrabajo archivo)
        {
            bool resultado = objarchestudiopt.GuardarArchivo(archivo);
            if (resultado)
                archivo.Result = "SUCCESS";
            else
                archivo.Result = "FAIL";

            return archivo;
        }
    }
}
