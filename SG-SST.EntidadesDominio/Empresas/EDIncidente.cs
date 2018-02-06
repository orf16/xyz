using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.EntidadesDominio.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDIncidente
    {
        /// <summary>
        /// Establece los valores por defecto.
        /// </summary>
        public void Limpiar()
        {
            General_tipo_documento = new EDTipoDocumento();
            General_sede = new EDSede();
            Persona_usuario_sistema = new EDUsuarioSistema();
            Persona_vinculacion_laboral = new EDVinculacionLaboral();
            Persona_tipo_documento = new EDTipoDocumento();
            Persona_zona = new EDZonaLugar();
            Persona_tipo_jornada = new EDTipoJornada();
            Incidente_tipo_incidente = new EDTipoIncidente();
            Incidente_departamento = new EDDepartamento();
            Incidente_municipio = new EDMunicipio();
            Incidente_zona_incidente = new EDZonaLugar();
            Incidente_sitio_incidente = new EDSitioIncidente();
            Incidente_consecuencia = new EDIncidenteConsecuencia();
            Incidente_fecha_diligenciamiento = DateTime.Now;

            // Algunos valores por defecto.
            General_mismos_datos_sede_principal = true;
            Incidente_realizaba_labor_habitual = true;
            Incidente_ocurre_dentro_empresa = true;

        }

        public int Pk_Id_Incidente { get; set; }

        #region I. Identificación general del empleador

        public string General_usuario_empresa_nit { get; set; }

        public int General_actividad_economica_id { get; set; }
        public string General_actividad_economica { get; set; }

        public string General_codigo { get; set; }

        public string General_razon_social { get; set; }

        public int FK_id_tipo_documento_general { get; set; }
        public EDTipoDocumento General_tipo_documento { get; set; }

        public string General_numero_identificación { get; set; }

        public string General_sede_principal_direccion { get; set; }

        public string General_sede_principal_telefono { get; set; }

        public string General_correo_electronico { get; set; }

        public int General_sede_principal_departamento_id { get; set; }
        public string General_sede_principal_departamento { get; set; }

        public int General_sede_principal_zona_id { get; set; }
        public string General_sede_principal_zona { get; set; }

        public int General_sede_principal_municipio_id { get; set; }
        public string General_sede_principal_municipio { get; set; }

        public bool General_mismos_datos_sede_principal { get; set; }

        public int FK_id_sede_general { get; set; }

        public EDSede General_sede { get; set; }

        
        #endregion

        #region II. Información de la persona que reporta el incidente

        public EDUsuarioSistema Persona_usuario_sistema { get; set; }

        public int FK_id_vinculacionlaboral_persona { get; set; }

        public EDVinculacionLaboral Persona_vinculacion_laboral { get; set; }

        public string Persona_primer_apellido { get; set; }

        public string Persona_segundo_apellido { get; set; }

        public string Persona_primer_nombre { get; set; }

        public string Persona_segundo_nombre { get; set; }

        public int FK_id_tipo_documento_persona { get; set; }

        public EDTipoDocumento Persona_tipo_documento { get; set; }

        public string Persona_numero_identificacion { get; set; }

        public DateTime Persona_fecha_nacimiento { get; set; }

        public string Persona_genero { get; set; }

        public string Persona_direccion { get; set; }

        public string Persona_telefono { get; set; }

        public int Persona_departamento_id { get; set; }

        public string Persona_departamento { get; set; }

        public int Persona_municipio_id { get; set; }

        public string Persona_municipio { get; set; }

        public int FK_id_zonalugar_persona { get; set; }

        public EDZonaLugar Persona_zona { get; set; }

        public string Persona_ocupacion_habitual { get; set; }
        public int Persona_ocupacion_habitual_id { get; set; }

        public DateTime Persona_fecha_ingreso_empresa { get; set; }

        public int FK_id_jornada_habitual_persona { get; set; }

        public EDTipoJornada Persona_tipo_jornada { get; set; }

        #endregion

        #region III. Información del incidente

        public DateTime Incidente_fecha { get; set; }
        public string strIncidente_fecha { get; set; }

        public int cantidadRegistros { get; set; }

        /// <summary>
        /// Maneja la hora del incidente, que en el formulario debe presentarse como un campo aparte.
        /// </summary>
        public string Incidente_fecha_hora
        {
            get
            {
                return Incidente_fecha.ToString("HH:mm");
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
                    Incidente_fecha.ToString("dd/MM/yyyy"),
                    Hora, Minuto);

                DateTime Fecha;
                DateTime.TryParseExact(StrFecha, "dd/MM/yyyy HH:mm", null,
                    System.Globalization.DateTimeStyles.None, out Fecha);
            }
        }

        /// <summary>
        /// Dos letras iniciales del día de la semana en que ocurrió el incidente.
        /// </summary>
        public string Incidente_dia_semana
        {
            get
            {
                var CI = new System.Globalization.CultureInfo("es-CO");
                return CI.DateTimeFormat.DayNames[(int)Incidente_fecha.DayOfWeek].ToUpper().Substring(0, 2);
            }
            set { }
        }

        /// <summary>
        /// Jornada en que sucede el evento. True: Normal, False: Extra.
        /// </summary>
        public bool Incidente_jornada_normal { get; set; }

        public bool Incidente_realizaba_labor_habitual { get; set; }

        public string Incidente_nombre_labor { get; set; }

        /// <summary>
        /// Tiempo previo al incidente, en minutos.
        /// </summary>
        public int Incidente_tiempo_previo_al_incidente { get; set; }

        /// <summary>
        /// Tiempo previo al incidente, presentado en HH:mm
        /// </summary>
        public string Incidente_tiempo_previo_al_incidente_HHMM
        {
            get
            {
                var Horas = Incidente_tiempo_previo_al_incidente / 60;
                var Minutos = Incidente_tiempo_previo_al_incidente - (Horas * 60);
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
                Incidente_tiempo_previo_al_incidente = Hora * 60 + Minuto;
            }
        }

        public int FK_id_incidente_tipo_incidente { get; set; }

        public EDTipoIncidente Incidente_tipo_incidente { get; set; }

        public int FK_id_departamento_incidente { get; set; }

        public EDDepartamento Incidente_departamento { get; set; }

          public int FK_id_municipio_incidente { get; set; }

        public EDMunicipio Incidente_municipio { get; set; }

       
        public int FK_id_zonalugar_incidente { get; set; }

        public EDZonaLugar Incidente_zona_incidente { get; set; }

        public bool Incidente_ocurre_dentro_empresa { get; set; }

        public int FK_id_sitio_incidente { get; set; }

        public EDSitioIncidente Incidente_sitio_incidente { get; set; }

        public string Incidente_sitio_incidente_nombre { get; set; }

        public string Incidente_sitio_incidente_otro { get; set; }

        public int FK_id_consecuencia_incidente { get; set; }

        public EDIncidenteConsecuencia Incidente_consecuencia { get; set; }

        public string Incidente_consecuencia_texto { get; set; }


        public string Incidente_descripcion { get; set; }

        public DateTime Incidente_fecha_diligenciamiento { get; set; }

        #endregion

        /// <summary>
        /// Fecha inicial
        /// </summary>
        public DateTime IncidenteFechaInicial { get; set; }
        /// <summary>
        /// Fecha final
        /// </summary>
        public DateTime IncidenteFechaFinal { get; set; }

    }

    /// <summary>
    /// Modelo para enviar los parámetros de consulta de incidentes.
    /// </summary>
    public class EDIncidente_Modelo_Consulta
    {
        /// <summary>
        /// Fecha inicial
        /// </summary>
        public DateTime? IncidenteFechaInicial { get; set; }
        /// <summary>
        /// Fecha final
        /// </summary>
        public DateTime? IncidenteFechaFinal { get; set; }
        /// <summary>
        /// Número de identifiación de la persona 
        /// </summary>
        public string PersonaNumeroIdentificacion { get; set; }
        /// <summary>
        /// Posible consecuencia del incidente
        /// </summary>
        public int? IncidentePosibleConsecuenciaID { get; set; }
        /// <summary>
        /// Sede donde ocurrió el incidente
        /// </summary>
        public int? IncidenteSedeID { get; set; }
        /// <summary>
        /// Tipo de incidente
        /// </summary>
        public int? IncidenteTipoIncidenteID { get; set; }
        /// <summary>
        /// Lugar donde ocurrió el incidente.
        /// True: Dentro de la empresa. False: Fuera de la empresa.
        /// </summary>
        public bool? IncidenteLugarIncidente { get; set; }
        /// <summary>
        /// Sitio del incidente
        /// </summary>
        public int? IncidenteSitioID { get; set; }
        /// <summary>
        /// EL ID del incidente, si se conoce.
        /// </summary>
        public int? IncidenteID { get; set; }
        /// <summary>
        /// Banderra para consultar los datos de llando de la tabla o el excel
        /// </summary>
        public bool ConsultarTodo { get; set; }

        public string Nit_Empresa { get; set; }

        public int numPagina { get; set; }

        public int cantidadPorPagina { get; set; }

        public int numPaginas { get; set; }
    }

    /// <summary>
    /// Listas de datos básicas para utilizar en los formularios.
    /// </summary>
    public class EDIncidente_Listas_Basicas
    {
        public List<EDDepartamento> Departamentos { get; set; }
        public List<EDTipoDocumento> TiposDocumento { get; set; }
        public List<EDZonaLugar> Zonas { get; set; }
        public List<EDVinculacionLaboral> VinculacionLaboral { get; set; }
        public List<EDTipoJornada> Jornadas { get; set; }
        public List<EDTipoIncidente> TiposIncidente { get; set; }
        public List<EDSitioIncidente> SitiosIncidente { get; set; }
        public List<EDIncidenteConsecuencia> IncidenteConsecuencias { get; set; }
        public List<EDSede> Sedes { get; set; }

        public List<string> DiasSemana { get; set; }

       


    }
}
