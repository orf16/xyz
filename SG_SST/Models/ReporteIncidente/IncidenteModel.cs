using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.ReporteIncidente
{

    public class IncidenteModel
    {
        /// <summary>
        /// Establece los valores por defecto.
        /// </summary>
       public int Id_Incidente { get; set; }

        #region I. Identificación general del empleador
        public string ActividadEconomica { get; set; }
        public int CodActividadEconomica { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoDocumentoEmpresa { get; set; }
        public string TipoDocumentoEmpresa { get; set; }
        public string NitEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdDepartamentoEmp { get; set; }
        public string DepartamentoEmp { get; set; }
        public int IdMunicipioEmp { get; set; }
        public string MunicipioEmp { get; set; }
        public int IdZonaEmpresa { get; set; }
        public string ZonaEmpresa { get; set; }
        public bool EsSedePrincipal { get; set; }
        public int IdSede { get; set; }
        public string NombreSede { get; set; }
        public string DireccionSede { get; set; }
        public string TelefonoSede { get; set; }
        public int IdDepartamentoSede { get; set; }
        public int IdMunicipioSede { get; set; }
        public int IdZonaSede { get; set; }

        public string DepartamentoSede { get; set; }
        public string MunicipioSede { get; set; }

        #endregion

        #region II. Información de la persona que reporta el incidente

        public int IdVinculacionLab { get; set; }
        public string VinculacionLab { get; set; }
        public int IdTipoDocumentoEmpleado { get; set; }
        public string TipoDocumentoEmpleado { get; set; }
        public string DocumentoEmpleado { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundNombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string DireccionEmpleado { get; set; }
        public string TelefonoEmpleado { get; set; }
        public int IdDepartamentoEmpleado { get; set; }
        public string  DepartamentoEmpleado { get; set; }
        public int IdMunicipioEmpleado { get; set; }
        public string MunicipioEmpleado { get; set; }
        public int IdZonaEmpleado { get; set; }
        public int IdOcupacion { get; set; }
        public string Ocupacion { get; set; }
        public DateTime FechaIngreso { get;  set; }
        public int IdJornadaHabitual { get; set; }
        
        #endregion

        #region III. Información del incidente

        public DateTime FechaIncidente { get; set; }

        public DateTime fechaIncidente;

        public string _HoraIncidente { get; set; }
        /// <summary>
        /// Maneja la hora del incidente, que en el formulario debe presentarse como un campo aparte.
        /// </summary>
        public string HoraIncidente
        {
            get
            {
                return FechaIncidente.ToString("HH:mm");
            }
            set
            {
                // Validar la hora.
                if (string.IsNullOrWhiteSpace(value)
                    || !value.Contains(":"))
                    return;
                var Partes = value.Split(':');
                int Hora, Minuto;
                if (!int.TryParse(Partes[0], out Hora))
                    Hora = 0;
                if (!int.TryParse(Partes[1], out Minuto))
                    Minuto = 0;
                if (Hora < 0 || Hora > 23)
                    Hora = 0;
                if (Minuto < 0 || Minuto > 59)
                    Minuto = 0;

                // Establecer la hora en la propiedad original.
                var StrFecha = string.Format("{0} {1:D2}:{2:D2}",
                    FechaIncidente.ToString("dd/MM/yyyy"),
                    Hora, Minuto);

                //DateTime Fecha;
                DateTime.TryParseExact(StrFecha, "dd/MM/yyyy HH:mm", null,
                    System.Globalization.DateTimeStyles.None, out fechaIncidente);
            }
        }

        /// <summary>
        /// Dos letras iniciales del día de la semana en que ocurrió el incidente.
        /// </summary>
        public string DiaSemanaIncidente
        {
            get
            {
                var CI = new System.Globalization.CultureInfo("es-CO");
                return CI.DateTimeFormat.DayNames[(int)FechaIncidente.DayOfWeek].ToUpper().Substring(0, 2);
            }
            set
            {
                diaSemanaIncidente = value;
            }
        }

        public string diaSemanaIncidente;

        /// <summary>
        /// Jornada en que sucede el evento. True: Normal, False: Extra.
        /// </summary>
        public bool EsJornadaNormal { get; set; }

        public bool RealizabaLaborHabitual { get; set; }

        public string DescripcionLabor { get; set; }

        /// <summary>
        /// Tiempo previo al incidente, en minutos.
        /// </summary>
        public int TiempoPrevioIncidente { get; set; }

        /// <summary>
        /// Tiempo previo al incidente, presentado en HH:mm
        /// </summary>
        public string Incidente_tiempo_previo_al_incidente_HHMM
        {
            get
            {
                var Horas = TiempoPrevioIncidente / 60;
                var Minutos = TiempoPrevioIncidente - (Horas * 60);
                return string.Format("{0:D2}:{1:D2}", Horas, Minutos);
            }
            set
            {
                // Validar la hora.
                if (string.IsNullOrWhiteSpace(value)
                    || !value.Contains(":"))
                    return;
                var Partes = value.Split(':');
                int Hora, Minuto;
                if (!int.TryParse(Partes[0], out Hora))
                    Hora = 0;
                if (!int.TryParse(Partes[1], out Minuto))
                    Minuto = 0;
                if (Hora < 0 || Hora > 23)
                    Hora = 0;
                if (Minuto < 0 || Minuto > 59)
                    Minuto = 0;

                // Establecer la hora en la propiedad original.
                TiempoPrevioIncidente = Hora * 60 + Minuto;
            }
        }

        public int IdTipoIncidente { get; set; }
        public string TipoIncidente { get; set; }

        public int IdDepartamentoIncidente { get; set; }
        public int IdMunicipioIncidente { get; set; }
        public int IdZonaIncidente { get; set; }
        public bool EsDentroEmpresa { get; set; }
        public string lugarIncidente { get; set; }
        public int IdSitioIncidente { get; set; }   
        public string SitioIncidente { get; set; }     
        public string OtroSitio { get; set; }
        public int IdConsecuencia { get; set; }
        public string Consecuencia { get; set; }
        public string DescripcionIncidente { get; set; }        
        public DateTime FechaCreacionIncidente { get; set; }

        public string DepartamentoIncidente { get; set; }
        public string MunicipioIncidente { get; set; }

        #endregion

        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public int numPagina { get; set; }


        public List<SelectListItem> TiposDeDocumento { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Municipios { get; set; }
        public List<SedeModel> Sedes { get; set; }
        public List<SelectListItem> Zonas { get; set; }
        public List<SelectListItem> VinculacionLaboral { get; set; }
        public List<SelectListItem> Jornadas { get; set; }
        public List<SelectListItem> DiasSemana { get; set; }
        public List<SelectListItem> TiposIncidente { get; set; }
        public List<SelectListItem> SitiosIncidente { get; set; }
        public List<SelectListItem> ConsecuenciasIncidente { get; set; }
        public List<SelectListItem> LugaresIncidente { get; set; }

        public string idLugarIncidente { get; set; }
        public List<SelectListItem> GetLugaresIncidente()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem () { Value = "1", Text = "Dentro de la empresa"},
                new SelectListItem () { Value = "2", Text = "Fuera de la empresa" }
        };
        }
    }

    public class SedeModel
    {
        public int IdSede { get; set; }
        public string NombreSede { get; set; }
        public string Dreccion { get; set; }
        public int Telefono { get; set; }
        public string Zona { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
    }


   

    public class Incidente
    {
        public string tipoDoc { get; set; }
        public string idPersona { get; set; }
        public string nomVinLaboral { get; set; }
        public int idOcupacion { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string fechaNacimiento { get; set; }
        public string ocupacion { get; set; }
        public string cargo { get; set; }
        public string emailPersona { get; set; }
        public string dirPersona { get; set; }
        public string telPersona { get; set; }
        public string sexoPersona { get; set; }
        public string tipoDocEmp { get; set; }
        public string documentoEmp { get; set; }
        public string idRepresentante { get; set; }
        public string razonSocial { get; set; }
        public string dirEmpresa { get; set; }
        public int idDeparEmpresa { get; set; }
        public int idMuniEmpresa { get; set; }
        public string nomDepEmpresa { get; set; }
        public string nomMunEmpresa { get; set; }
        public int idSeccional { get; set; }
        public int idActividadEconomica { get; set; }
        public int idSectordEconomica { get; set; }
        public string riesgo { get; set; }
        public int numeroTrabajadores { get; set; }
        public string emailEmpresa { get; set; }
        public string telefonoEmpresa { get; set; }
        public string fechaAfiliacionEfectiva { get; set; }
        public string estadoEmpresa { get; set; }
        public string paginaWebEmpresa { get; set; }
        public string faxEmpresa { get; set; }
        public string idZona { get; set; }
        public string actividadEconomica { get; set; }
        public int idDepAfiliado { get; set; }
        public int idMunAfiliado { get; set; }
        public string nomDepAfiliado { get; set; }
        public string nomMunAfiliado { get; set; }
        public string fechaInicioVinculacion { get; set; }
        public string fechaFinVinculacion { get; set; }
        public string estadoPersona { get; set; }
        public int idAfp { get; set; }
        public string nombreAfp { get; set; }
        public string idEps { get; set; }
        public string nombreEps { get; set; }
        public string idArl { get; set; }
        public string nombreArl { get; set; }
        public decimal salario { get; set; }       
    }
}