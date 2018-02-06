using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Aplicacion
{
    public class CrearInspeccionModel
    {
        public string RazonSocial { get; set; }
        public string idempresa { get; set; }
        public string Sede { get; set; }
        public int IDEmpresa { get; set; }
        public string Proceso { get; set; }
        public string area { get; set; }
        public int idSede { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public string responsable { get; set; }
        public int IdTipoInspeccion { get; set; }
        public string DescripcionTipoInspeccion { get; set; }
        public string DescribeInspeccion { get; set; }

        public int EstadoVm { get; set; }
        public List<int> elementos { get; set; }

        public int Consecutivo { get; set; }
        //public int IdInspeccion { get; set; }
        public List<InspeccionModel> tiposinspeccion { get; set; }
        public int idProceso { get; set; }
        public List<SelectListItem> Procesos { get; set; }
        public int idinspeccion { get; set; }
        public string hora { get; set; }
        public string descripcion { get; set; }
        public int idplaninspeccion { get; set; }
        public string responsableplaninspeccion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fechaplaninspeccion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecharealizacioninspeccion { get; set; }
        public List<ConfiguracionesModel> Configuraciones { get; set; }
        public List<ConfiguracionInspModel> ConfiguracionesI { get; set; }
        public List<AsistentesviewModel> Asistentes { get; set; }
        public List<ElementosEMHVM> Elementos { get; set; }

        public List<CondicionesInsegurasVM> Condiciones { get; set; }
        public List<PeligrosModel> peligrosos { get; set; }
        //public List<InspeccionModel> tiposinspeccion { get; set; }
        List<HttpPostedFile> imagenes { get; set; }
        public DateTime fechafin { get; set; }

        public DateTime fechaIn { get; set; }

        public DateTime Fecha { get; set; }
        

        public string Parametros { get; set; }
    }


    public class ConfiguracionesModel
    {
        public int idconfiguracion { get; set; }
        public string Descripcionconfiguracion { get; set; }
        public int diasdesde { get; set; }
        public int diashasta { get; set; }

    }



    public class ElementosEMHVM
    {
        public int Pk_Id_AdmoEMH { get; set; }
        public string TipoElemento { get; set; }
        public string NombreElemento { get; set; }
        public string Marca { get; set; }
    }
    
   public class AsistentesviewModel
   {
       public int idasistente { get; set; }
       public string nombreasistente { get; set; }
   }

  

   public class ConfiguracionInspModel
   {
       public int idconfiguracionViewModel { get; set; }
       public string DescripcionViewModel { get; set; }
       public int diasdesdeViewModel { get; set; }
       public int diashastaViewModel { get; set; }
   }

    public class PeligrosModel
    {
        public int idpeligro { get; set; }
        public string Describepeligro { get; set; }

    }

    public class CondicionesInsegurasVM
    {
        public int pkcondicionvm { get; set; }
        public string DescribeCondicionvm { get; set; }
        public string Ubicacionespecificavm { get; set; }
        public string Riesgopeligrovm { get; set; }
        public string ClasificacionRiesgovm { get; set; }
        public string Evidenciacondicionvm { get; set; }

        public int Configuracioncondicionvm { get; set; }

        public string Prioridad { get; set; }
        public string Describelaconfiguracion { get; set; }
        public int Diadesde { get; set; }
        public int Diahasta { get; set; }

        public string Diasdesde { get; set; }
        public string Diashasta { get; set; }



        public int IdTipoInspeccion { get; set; }
        public string DescripcionTipoInspeccion { get; set; }



        public int intpkinspeccion { get; set; }

        public string DescripcionInspeccion { get; set; }

        public string area { get; set; }
        public string Responsable { get; set; }
        public List<SelectListItem> tiposinspeccion { get; set; }

        public List<ConfiguracionInspeccion> ConfiguracionesPI { get; set; }


    }
}