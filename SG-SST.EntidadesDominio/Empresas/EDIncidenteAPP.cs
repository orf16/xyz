using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SG_SST.EntidadesDominio.Empresas
{
  public  class EDIncidenteAPP
    {

        public int Pk_Id_Incidente { get; set; }
        public bool General_mismos_datos_sede_principal { get; set; }

        public int FK_id_sede_general { get; set; }

        public int FK_id_usuariosistema_persona { get; set; }

        public int FK_id_vinculacionlaboral_persona { get; set; }

        public string Persona_primer_apellido { get; set; }

        public string Persona_segundo_apellido { get; set; }

        public string Persona_primer_nombre { get; set; }

        public string Persona_segundo_nombre { get; set; }

        public int FK_id_tipo_documento_persona { get; set; }

        public string Persona_numero_identificacion { get; set; }

        public DateTime Persona_fecha_nacimiento { get; set; }

        public string Persona_genero { get; set; }

        public string Persona_telefono { get; set; }

        public int FK_id_zonalugar_persona { get; set; }

        public string Persona_ocupacion_habitual { get; set; }

        public DateTime Persona_fecha_ingreso_empresa { get; set; }

        public int FK_id_jornada_habitual_persona { get; set; }

        public DateTime Incidente_fecha { get; set; }

        public bool Incidente_jornada_normal { get; set; }

        public bool Incidente_realizaba_labor_habitual { get; set; }

        public string Incidente_nombre_labor { get; set; }

        public int Incidente_tiempo_previo_al_incidente { get; set; }

        public int FK_id_incidente_tipo_incidente { get; set; }

        public int FK_id_departamento_incidente { get; set; }

        public int FK_id_municipio_incidente { get; set; }

        public int FK_id_zonalugar_incidente { get; set; }

        public bool Incidente_ocurre_dentro_empresa { get; set; }

        public int FK_id_sitio_incidente { get; set; }

        public string Incidente_sitio_incidente_otro { get; set; }

        public int FK_id_consecuencia_incidente { get; set; }

        public string Incidente_descripcion { get; set; }

        public DateTime Incidente_fecha_diligenciamiento { get; set; }

        public string Persona_direccion { get; set; }

        public int General_actividad_economica_id { get; set; }

        public int FK_id_tipo_documento_general { get; set; }

        public string General_numero_identificacion { get; set; }

        public int General_sede_principal_zona_id { get; set; }

        public int Persona_departamento_id { get; set; }

        public int Persona_municipio_id { get; set; }

        public string Dia_Semana_Incidente { get; set; }

        public int FK_id_sede_no_principal { get; set; }

        public string afiliadoempresaactivo { get; set; }

  

        public string incidenteOcurreEnLaEmpresa { get; set; }

        public string Incidenterealizabalaborhabitual { get; set; }

        public string Incidentejornadanormal { get; set; }

        public int General_sede_principal_municipio_id { get; set; }

        public string municipioTrabajador { get; set; }

        public string departamentoTrabajador { get; set; }

        public string strIncidente_fecha { get; set; }

    }
}
