using SG_SST.EntidadesDominio.Participacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Net;

using SG_SST.EntidadesDominio.Planificacion;


//using SG_SST.Utilidades.Traza;
using System.Threading;


using SG_SST.EntidadesDominio.Ausentismo;
using Newtonsoft.Json;
namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDReporte
    {
        public int IdReportes { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public int IdConsecutivoReportes { get; set; }

        public DateTime fechaSistena { get; set; }

        public string nitEmpresa { get; set; }

        public int FKSede { get; set; }

        public List<int> sedes { get; set; }

        public int? FK_Proceso { get; set; }


        public int? Procesos { get; set; }

        public int FKTipoReporte { get; set; }

        public string AreaLugar { get; set; }

        public DateTime FechaOcurrencia { get; set; }

        public int CedulaQuienReporta { get; set; }

        public string NombreQuienReporta { get; set; }

        public string CargoQuienReporta { get; set; }

        public string DescripcionReporte { get; set; }
        public string CausaReporte { get; set; }
        public string SugerenciasReporte { get; set; }

        public string sede { get; set; }

        public string tipo { get; set; }

        public string nombreProceso { get; set; }

        public bool medioAcceso { get; set; }
        public int ConsecutivoReporte { get; set; }

        public DateTime fechaInicio { get; set; }

        public DateTime fechaFin { get; set; }


    

        public List<EDActividadesActosInseguros> actividades { get; set; }


        public List<EDImagenesReportes> imagenesReporte { get; set; }
    
        public List<string> imagenes { get; set; }

        public HttpPostedFileBase imagen;

        public int acceso { get; set; }
    }
}
