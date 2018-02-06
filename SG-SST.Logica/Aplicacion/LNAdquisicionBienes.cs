
namespace SG_SST.Logica.Aplicacion
{
    using OfficeOpenXml;
    using SG_SST.EntidadesDominio.Aplicacion;
    using SG_SST.Interfaces.Aplicacion;
    using SG_SST.InterfazManager.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class LNAdquisicionBienes
    {
        private static IAdquicisionBienes AdquicisionBienes = IMAdquisicionBienes.AdquisicionBienes();

        public bool GuardarManualAdquisiones(EDManualAdquisicion documento)
        {
            return AdquicisionBienes.GuardarManualAdquisiones(documento);
        }

        public List<EDManualAdquisicion> ObtenerManualAdquisiones(int idEmpresa)
        {
            return AdquicisionBienes.ObtenerManualAdquisiones(idEmpresa);
        }

        public string ObtenerManualAdquisionBienes(int idManualAdq)
        {
            return AdquicisionBienes.ObtenerManualAdquisionBienes(idManualAdq);
        }


        public bool EliminarManualAdqBienes(int idManualAdq)

        {
            return AdquicisionBienes.EliminarManualAdqBienes(idManualAdq);
        }
    }
}
