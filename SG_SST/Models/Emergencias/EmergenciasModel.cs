using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Emergencias
{
    public class EmergenciasModel
    {
        public int IdSede { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public HttpPostedFileBase adjuntos { get; set; }
        public HttpPostedFileBase adjuntos1 { get; set; }
        public HttpPostedFileBase adjuntos2 { get; set; }
        public HttpPostedFileBase adjunto1 { get; set; }
        public HttpPostedFileBase adjunto2 { get; set; }
        public HttpPostedFileBase adjunto3 { get; set; }
        public HttpPostedFileBase adjunto4 { get; set; }


        public HttpPostedFileBase frenteaccion_adjunto1 { get; set; }
        public HttpPostedFileBase frenteaccion_adjunto2 { get; set; }
        public HttpPostedFileBase frenteaccion_adjunto3 { get; set; }
        public HttpPostedFileBase frenteaccion_adjunto4 { get; set; }
        public HttpPostedFileBase frenteaccion_adjunto5 { get; set; }

        //TAB 1
        public int pk_id_generalidades { get; set; }
        public int fk_id_sede_generalidades { get; set; }
        public string objetivos { get; set; }
        public string alcance { get; set; }

        //TAB 2
        public int pk_id_infogeneral { get; set; }
        public int fk_id_sede_infogeneral { get; set; }
        public string razon_social { get; set; }
        public string identificacion_sede { get; set; }
        public string direccion_sede { get; set; }
        public string telefono_sede { get; set; }
        public string correo_electronico { get; set; }
        public string departamento_sede { get; set; }
        public string municipio_sede { get; set; }
        public string lindero_norte { get; set; }
        public string lindero_sur { get; set; }
        public string lindero_oriente { get; set; }
        public string lindero_occidente { get; set; }
        public string acceso_principales { get; set; }
        public string acceso_alternas { get; set; }
        public string actividad_economica { get; set; }
        public string representante { get; set; }

        //
        public int pk_id_descocupacion { get; set; }
        public int fk_id_sede_descocupacion { get; set; }
        public string trabajadores_cantidad { get; set; }
        public string trabajadores_hdesde { get; set; }
        public string trabajadore_hhasta { get; set; }
        public string contratista_cantidad { get; set; }
        public string contratista_hdesde { get; set; }
        public string contratista_hhasta { get; set; }
        public string visitante_cantidad { get; set; }
        public string visitante_hdesde { get; set; }
        public string visitantte_hhasta { get; set; }
        public string cliente_cantidad { get; set; }
        public string cliente_hdesde { get; set; }
        public string cliente_hhasta { get; set; }
        public bool bo_tratamiento_especial { get; set; }
        public string cual { get; set; }

        //
        public int pk_id_cinstalaciones { get; set; }
        public int fk_id_sede_cinstalaciones { get; set; }
        public string ventilacion_mecanica { get; set; }
        public string ascensores { get; set; }
        public string sotanos { get; set; }
        public string red_hidraulica { get; set; }
        public string transformadores { get; set; }
        public string plantas_electricas { get; set; }
        public string escaleras { get; set; }
        public string zonas_parqueo { get; set; }
        public string areas_especiales { get; set; }

        //

        public int pk_id_elementos { get; set; }
        public int fk_id_sede_elementos { get; set; }
        public string estructurales_descripcion { get; set; }
        public string estructurales_ubicacion { get; set; }
        public string equipos_descripcion { get; set; }
        public string equipos_ubicacion { get; set; }
        public string insumos_descripcion { get; set; }
        public string insumos_ubicacion { get; set; }

        //TAB 3
        public string interno_img { get; set; }
        public int fk_id_sede_georeferenciacion { get; set; }
        public bool bo_externo { get; set; }
        public bool bo_colegio { get; set; }
        public bool bo_iglesia { get; set; }
        public bool bo_comercial { get; set; }
        public bool bo_centro_atencion { get; set; }
        public bool bo_parque { get; set; }
        public bool bo_otro { get; set; }
        public string tab3_cual { get; set; }
        public string punto_encuentro { get; set; }
        public string ubicacion_hidrantes { get; set; }
        public string punto_encuentro_img { get; set; }
        public string ubicacion_hidrantes_img { get; set; }

        // TAB 5 ROLES
        public int pk_id_roles { get; set; }
        public int fk_id_sede_roles { get; set; }
        public string nombre { get; set; }
        public string antes { get; set; }
        public string durante { get; set; }
        public string despues { get; set; }

        //TAB 7

        public int pk_id_frenteaccion { get; set; }
        public int fk_id_sede_frenteaccion { get; set; }
        public string plan_seguridadfisica { get; set; }
        public string plan_primerosaux { get; set; }
        public string plan_contraincendios { get; set; }
        public string plan_eval_pdf { get; set; }
        public string nombrecoordinador { get; set; }
        public string tab7_objetivos { get; set; }
        public string estructura { get; set; }
        public string proc_coordinacion { get; set; }
        public string proc_internos { get; set; }
        public string proc_externos { get; set; }
        public string mecanismos_alarma { get; set; }
        public string rutas_evac_pdf { get; set; }
        public string simulacros { get; set; }
        public string instructivo_evacuacion { get; set; }
        public string proc_retorno { get; set; }

        //TAB 8
        public int pk_id_proc_normalizados { get; set; }
        public int fk_id_sede_proc_normalizados { get; set; }
        public string nombre_proc { get; set; }
        public string responsable { get; set; }
        public string proc_antes { get; set; }
        public string proc_durante { get; set; }
        public string proc_despues { get; set; }
        public string proc_recursos { get; set; }

        // Niveles de Emergencia

        public int pk_id_nivelemergencia { get; set; }
        public int fk_id_sede_nivelemergencia { get; set; }
        public string nivel { get; set; }
        public string emergencia { get; set; }

        // Recursos Humanos
        public int pk_id_recursosh { get; set; }
        public int fk_id_sede_recursosh { get; set; }
        public string bpaux_nombre { get; set; }
        public string bcontra_nombre { get; set; }
        public string bevalresc_nombre { get; set; }

        // Recursos Tecnicos
        public int pk_id_recursostecnicos { get; set; }
        public int fk_id_sede_recursostecnicos { get; set; }
        public string tipo { get; set; }
        public string cantidad { get; set; }
        public string ubicacion { get; set; }

        // Consolidado
        public int pk_id_consolidado { get; set; }

    }

    public class NivelesRiesgoModel {
        public string tipo { get; set; }
        public string amenaza { get; set; }
        public string color_a { get; set; }
        public string color_p { get; set; }
        public string color_r { get; set; }
        public string color_s { get; set; }
        public string interpretacion { get; set; }

    }
}