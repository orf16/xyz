using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using SG_SST.Models.Empresas;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Empleado;
using SG_SST.Models.Usuarios;
using SG_SST.Models.Planificacion;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Incidentes")]
    public class Incidente
    {
        [Key]
        public int Pk_Id_Incidente { get; set; }

        #region I. Identificación general del empleador

        public int General_actividad_economica_id { get; set; }

        public int FK_id_tipo_documento_general { get; set; }

        [ForeignKey("FK_id_tipo_documento_general")]
        [DisplayName("Tipo de identificación")]
        public virtual TipoDocumento General_tipo_documento { get; set; }

        [MaxLength(15)]
        [DisplayName("Nº (número de identificación)")]
        public string General_numero_identificación { get; set; }

        public int General_sede_principal_zona_id { get; set; }

        public int General_sede_principal_municipio_id { get; set; }

        [DisplayName("¿Son los datos de la Sede los mismos de la sede principal?")]        
        public bool General_mismos_datos_sede_principal { get; set; }

        public int FK_id_sede_general { get; set; }

        [ForeignKey("FK_id_sede_general")]
        [DisplayName("Sede")]
        public virtual Sede General_sede { get; set; }

        public int FK_id_sede_no_principal { get; set; }

        #endregion

        #region II. Información de la persona que reporta el incidente

        /// <summary>
        /// Usuario autenticado.
        /// </summary>
        public int FK_id_usuariosistema_persona { get; set; }
        public int FK_id_vinculacionlaboral_persona { get; set; }

        [ForeignKey("FK_id_vinculacionlaboral_persona")]
        public virtual VinculacionLaboral Persona_vinculacion_laboral { get; set; }

        [MaxLength(50)]
        [DisplayName("Primer apellido")]
        public string Persona_primer_apellido { get; set; }

        [MaxLength(50)]
        [DisplayName("Segundo apellido")]
        public string Persona_segundo_apellido { get; set; }

        [MaxLength(50)]
        [DisplayName("Primer nombre")]
        public string Persona_primer_nombre { get; set; }

        [MaxLength(50)]
        [DisplayName("Segundo nombre")]
        public string Persona_segundo_nombre { get; set; }

        public int FK_id_tipo_documento_persona { get; set; }

        [ForeignKey("FK_id_tipo_documento_persona")]
        [DisplayName("Tipo de identificación")]
        public virtual TipoDocumento Persona_tipo_documento { get; set; }

        [MaxLength(15)]
        [DisplayName("Nº (número de identificación)")]        
        public string Persona_numero_identificacion { get; set; }

        [DisplayName("Fecha de nacimiento")]        
        public DateTime Persona_fecha_nacimiento { get; set; }

        [MaxLength(1)]
        [DisplayName("Género")]        
        public string Persona_genero { get; set; }

        [MaxLength(15)]
        [DisplayName("Teléfono")]        
        public string Persona_telefono { get; set; }

        [MaxLength(50)]
        [DisplayName("Dirección")]        
        public string Persona_direccion { get; set; }        
        public int Persona_departamento_id { get; set; }        
        public int Persona_municipio_id { get; set; }

        public int FK_id_zonalugar_persona { get; set; }

        [ForeignKey("FK_id_zonalugar_persona")]
        [DisplayName("Zona")]        
        public virtual Planificacion.ZonaLugar Persona_zona { get; set; }

        [MaxLength(500)]
        [DisplayName("Ocupación habitual")]
        public string Persona_ocupacion_habitual { get; set; }

        [DisplayName("Fecha de ingreso a la empresa")]
        public DateTime Persona_fecha_ingreso_empresa { get; set; }

        public int FK_id_jornada_habitual_persona { get; set; }

        [ForeignKey("FK_id_jornada_habitual_persona")]
        [DisplayName("Jornada en que sucede")]
        public virtual TipoJornada Persona_tipo_jornada { get; set; }

        #endregion

        #region III. Información del incidente

        [DisplayName("Fecha del incidente")]        
        public DateTime Incidente_fecha { get; set; }

        public string Dia_Semana_Incidente { get; set; }

        /// <summary>
        /// Jornada en que sucede el evento. True: Normal, False: Extra.
        /// </summary>
        [DisplayName("Jornada en que sucede")]        
        public bool Incidente_jornada_normal { get; set; }

        [DisplayName("¿Estaba realizando su laboral habitual?")]        
        public bool Incidente_realizaba_labor_habitual { get; set; }

        [MaxLength(500)]
        [DisplayName("¿Cúal? (diligenciar solo en caso negativo)")]
        public string Incidente_nombre_labor { get; set; }

        /// <summary>
        /// Tiempo en minutos, presentado en HH:mm
        /// </summary>
        [DisplayName("Total tiempo laborado previo al incidente")]        
        public int Incidente_tiempo_previo_al_incidente { get; set; }

        public int FK_id_incidente_tipo_incidente { get; set; }

        [ForeignKey("FK_id_incidente_tipo_incidente")]
        [DisplayName("Tipo de incidente")]        
        public virtual TipoIncidente Incidente_tipo_incidente { get; set; }

        public int FK_id_departamento_incidente { get; set; }

        [ForeignKey("FK_id_departamento_incidente")]
        [DisplayName("Departamento del incidente")]        
        public virtual Departamento Incidente_departamento { get; set; }

        public int FK_id_municipio_incidente { get; set; }

        [ForeignKey("FK_id_municipio_incidente")]
        [DisplayName("Municipio del incidente")]        
        public virtual Municipio Incidente_municipio { get; set; }

        public int FK_id_zonalugar_incidente { get; set; }

        [ForeignKey("FK_id_zonalugar_incidente")]
        [DisplayName("Zona")]       
        public virtual ZonaLugar Incidente_zona_incidente { get; set; }

        [DisplayName("Lugar donde ocurrió el incidente")]        
        public bool Incidente_ocurre_dentro_empresa { get; set; }

        public int FK_id_sitio_incidente { get; set; }

        [ForeignKey("FK_id_sitio_incidente")]
        [DisplayName("Indique cuál sitio")]        
        public virtual SitioIncidente Incidente_sitio_incidente { get; set; }

        [MaxLength(500)]
        [DisplayName("Otro (especifique)")]
        public string Incidente_sitio_incidente_otro { get; set; }

        public int FK_id_consecuencia_incidente { get; set; }

        [ForeignKey("FK_id_consecuencia_incidente")]
        [DisplayName("Indique cuál sitio")]        
        public virtual IncidenteConsecuencia Incidente_consecuencia { get; set; }

        [MaxLength(2000)]
        [DisplayName("Descripción del incidente")]        
        public string Incidente_descripcion { get; set; }

        [DisplayName("Fecha de diligenciamiento del informe del incidente")]
        public DateTime Incidente_fecha_diligenciamiento { get; set; }

        #endregion

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDIncidente ObtenerED()
        {
            return new EDIncidente
            {
                // Llave primaria.
                Pk_Id_Incidente = Pk_Id_Incidente,

                // Llave foráneas.
                FK_id_consecuencia_incidente = FK_id_consecuencia_incidente,
                FK_id_departamento_incidente = FK_id_departamento_incidente,
                FK_id_incidente_tipo_incidente = FK_id_incidente_tipo_incidente,
                FK_id_jornada_habitual_persona = FK_id_jornada_habitual_persona,
                FK_id_municipio_incidente = FK_id_municipio_incidente,
                FK_id_sitio_incidente = FK_id_sitio_incidente,
                FK_id_tipo_documento_persona = FK_id_tipo_documento_persona,
                FK_id_vinculacionlaboral_persona = FK_id_vinculacionlaboral_persona,
                FK_id_zonalugar_incidente = FK_id_zonalugar_incidente,
                FK_id_zonalugar_persona = FK_id_zonalugar_persona,
                FK_id_sede_general = FK_id_sede_general,

                // Información general del incidente.
                General_mismos_datos_sede_principal = General_mismos_datos_sede_principal,
                //General_sede = General_sede.ObtenerED(),
                General_usuario_empresa_nit = "",
                FK_id_tipo_documento_general = FK_id_tipo_documento_general,
                General_actividad_economica = "",
                General_actividad_economica_id = General_actividad_economica_id,
                General_codigo = "",
                General_correo_electronico = "",
                General_numero_identificación = General_numero_identificación,
                General_razon_social = "",
                General_sede_principal_departamento = "",
                General_sede_principal_departamento_id = 0,
                General_sede_principal_direccion = "",
                General_sede_principal_municipio = "",
                General_sede_principal_municipio_id = General_sede_principal_municipio_id,
                General_sede_principal_telefono = "",
                General_sede_principal_zona = "",
                General_sede_principal_zona_id = General_sede_principal_zona_id,
                General_tipo_documento = General_tipo_documento.ObtenerED(),

                // Información del incidente.
                Incidente_consecuencia = Incidente_consecuencia.ObtenerED(),
                Incidente_departamento = Incidente_departamento.ObtenerED(),
                Incidente_descripcion = Incidente_descripcion,
                Incidente_fecha = Incidente_fecha,
                Incidente_fecha_diligenciamiento = Incidente_fecha_diligenciamiento,
                Incidente_jornada_normal = Incidente_jornada_normal,
                Incidente_municipio = Incidente_municipio.ObtenerED(),
                Incidente_nombre_labor = Incidente_nombre_labor,
                Incidente_ocurre_dentro_empresa = Incidente_ocurre_dentro_empresa,
                Incidente_realizaba_labor_habitual = Incidente_realizaba_labor_habitual,
                Incidente_sitio_incidente = Incidente_sitio_incidente.ObtenerED(),
                Incidente_sitio_incidente_otro = Incidente_sitio_incidente_otro,
                Incidente_tiempo_previo_al_incidente = Incidente_tiempo_previo_al_incidente,
                Incidente_tipo_incidente = Incidente_tipo_incidente.ObtenerED(),
                Incidente_zona_incidente = Incidente_zona_incidente.ObtenerED(),

                // Información de la persona que reporta el incidente.
                Persona_municipio_id = Persona_municipio_id,
                Persona_municipio = "",
                Persona_departamento_id = Persona_departamento_id,
                Persona_departamento = "",
                Persona_fecha_ingreso_empresa = Persona_fecha_ingreso_empresa,
                Persona_fecha_nacimiento = Persona_fecha_nacimiento,
                Persona_genero = Persona_genero,
                Persona_numero_identificacion = Persona_numero_identificacion,
                Persona_ocupacion_habitual = Persona_ocupacion_habitual,
                Persona_primer_apellido = Persona_primer_apellido,
                Persona_primer_nombre = Persona_primer_nombre,
                Persona_segundo_apellido = Persona_segundo_apellido,
                Persona_segundo_nombre = Persona_segundo_nombre,
                Persona_telefono = Persona_telefono,
                Persona_direccion = Persona_direccion,
                Persona_tipo_documento = Persona_tipo_documento.ObtenerED(),
                Persona_tipo_jornada = Persona_tipo_jornada.ObtenerED(),
                Persona_usuario_sistema = new EntidadesDominio.Usuario.EDUsuarioSistema(),
                Persona_vinculacion_laboral = Persona_vinculacion_laboral.ObtenerED(),
                Persona_zona = Persona_zona.ObtenerED(),
            };
        }
    }
}
