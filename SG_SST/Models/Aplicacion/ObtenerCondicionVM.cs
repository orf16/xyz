using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Aplicacion
{
    public class ObtenerCondicionVM
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecharealizacioninspeccion { get; set; }
        public string idempresa { get; set; }///Nit Empresa

        //public int IDEmpresa { get; set; }///
        public int Consecutivo { get; set; }
        public int idplaninspeccion { get; set; }
        public int idSede { get; set; }
        public int idProceso { get; set; }
        public int idinspeccion { get; set; }
        public string area { get; set; }
        public string hora { get; set; }
        public string DescribeInspeccion { get; set; }
        public List<int> elementos { get; set; }
        public string responsable { get; set; }
        public List<ConfiguracionesModel> Configuraciones { get; set; }
        public List<AsistentesviewModel> Asistentes { get; set; }
        public List<PeligrosModel> peligrosos { get; set; }
        public List<CondicionesInsegurasVM> Condiciones { get; set; }
        
    }
}