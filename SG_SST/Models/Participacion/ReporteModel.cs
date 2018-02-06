using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Participacion
{
    public class ReporteModel
    {
        public int IdReportes { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public int IdConsecutivoReportes { get; set; }

        public DateTime fechaSistena { get; set; }

        public string nitEmpresa { get; set; }

        public int FKSede { get; set; }

        public int Procesos { get; set; }

        public int FKTipoReporte { get; set; }

        public string AreaLugar { get; set; }

        public DateTime FechaOcurrencia { get; set; }

        public int CedulaQuienReporta { get; set; }

        public string NombreQuienReporta { get; set; }

        public string CargoQuienReporta { get; set; }

        public string DescripcionReporte { get; set; }
        public string CausaReporte { get; set; }
        public string SugerenciasReporte { get; set; }

        public List<ActividadesModel> actividades { get; set; }

        public List<ImagenesReportesModel> imagenes { get; set; }
    }

    public class ActividadesModel
    {
        public int ID_ActividadActosInseguros { get; set; }

        public string nombreActividad { get; set; }

        public string RespActividad { get; set; }

        public DateTime FecEjecucion { get; set; }

    }

    public class ImagenesReportesModel
    {
        public int IDImagenesReportes { get; set; }

        public string rutaArchivo { get; set; }

    }


}