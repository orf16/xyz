using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Planificacion
{
    public class MatrizRAM:Object
    {
        public MatrizRAM() { }
        public MatrizRAM(int Id_Peligro,string Proceso, string Zona_lugar, string Actividad, string Tarea, string Rutinaria,
            string Fuente_Generadora_De_Peligro, string Clasificacion, string Descripcion,int PlantaDirecto,
            int HorasDeExposicionPlanta,int Contratista,int HorasDeExposicionContratista,int Temporal,
            int HorasDeExposicionTemporal,int EstudiantePasante,int HorasDeExposicionEstudiante,
            int Visitante,int HorasDeExposicionVisitante,
            string ConsecuenciasReales,string ConsecuenciasPontenciales,
            string Fuente, string Medio, string Trabajador,
            string Nivel_De_ProbablidadPersona, string Nivel_De_ConsecuenciaPersona,string EstimacionRiesgoPersona,
            string Nivel_De_ProbablidadCliente, string Nivel_De_ConsecuenciaCliente, string EstimacionRiesgoCliente,
            string Nivel_De_ProbablidadEconomica, string Nivel_De_ConsecuenciaEconomica, string EstimacionRiesgoEconomica,
            string Nivel_De_ProbablidadEmpresa, string Nivel_De_ConsecuenciaEmpresa, string EstimacionRiesgoEmpresa,
            string Nivel_De_ProbablidadAmbiental, string Nivel_De_ConsecuenciaAmbiental, string EstimacionRiesgoAmbiental,
            string NivelDeRiesgo,
            string Eliminacion, string Sustitucion,
            string Controles_De_Ingenieria, string Controles_Administrativos, string Elementos_De_Proteccion)
        {
            this.Id_Peligro = Id_Peligro;
            this.Proceso = Proceso;
            this.Zona_lugar = Zona_lugar;
            this.Actividad = Actividad;
            this.Tarea = Tarea;
            this.Rutinaria = Rutinaria;
            this.Fuente_Generadora_De_Peligro = Fuente_Generadora_De_Peligro;
            this.Clasificacion = Clasificacion;
            this.Descripcion = Descripcion;
            this.PlantaDirecto = PlantaDirecto;
            this.HorasDeExposicionPlanta = HorasDeExposicionPlanta;
            this.Contratista = Contratista;
            this.HorasDeExposicionContratista = HorasDeExposicionContratista;
            this.Temporal = Temporal;
            this.HorasDeExposicionTemporal = HorasDeExposicionTemporal;
            this.EstudiantePasante = EstudiantePasante;
            this.HorasDeExposicionEstudiante = HorasDeExposicionEstudiante;
            this.Visitante = Visitante;
            this.HorasDeExposicionVisitante = HorasDeExposicionVisitante;
            this.ConsecuenciasReales = ConsecuenciasReales;
            this.ConsecuenciasPontenciales = ConsecuenciasPontenciales;
            this.Fuente = Fuente;
            this.Medio = Medio;
            this.Trabajador = Trabajador;

            this.Nivel_De_ProbablidadPersona = Nivel_De_ProbablidadPersona;
            this.Nivel_De_ConsecuenciaPersona = Nivel_De_ConsecuenciaPersona;
            this.EstimacionRiesgoPersona = EstimacionRiesgoPersona;

            this.Nivel_De_ProbablidadCliente = Nivel_De_ProbablidadCliente;
            this.Nivel_De_ConsecuenciaCliente = Nivel_De_ConsecuenciaCliente;
            this.EstimacionRiesgoCliente = EstimacionRiesgoCliente;

            this.Nivel_De_ProbablidadEconomica = Nivel_De_ProbablidadEconomica;
            this.Nivel_De_ConsecuenciaEconomica = Nivel_De_ConsecuenciaEconomica;
            this.EstimacionRiesgoEconomica = EstimacionRiesgoEconomica;

            this.Nivel_De_ProbablidadEmpresa = Nivel_De_ProbablidadEmpresa;
            this.Nivel_De_ConsecuenciaEmpresa = Nivel_De_ConsecuenciaEmpresa;
            this.EstimacionRiesgoEmpresa = EstimacionRiesgoEmpresa;

            this.Nivel_De_ProbablidadAmbiental = Nivel_De_ProbablidadAmbiental;
            this.Nivel_De_ConsecuenciaAmbiental = Nivel_De_ConsecuenciaAmbiental;
            this.EstimacionRiesgoAmbiental = EstimacionRiesgoAmbiental;

            this.NivelDeRiesgo = NivelDeRiesgo;

            this.Eliminacion = Eliminacion;
            this.Sustitucion = Sustitucion;
            this.Controles_De_Ingenieria = Controles_De_Ingenieria;
            this.Controles_Administrativos = Controles_Administrativos;
            this.Elementos_De_Proteccion = Elementos_De_Proteccion;
           
        }

        /// <summary>
        /// Obtiene y establece el id del proceso 
        /// </summary>
        public int Id_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece el proceso 
        /// </summary>
        public string Proceso { get; set; }

        /// <summary>
        /// Obtiene y establece las zonas o lugares de la empresa.
        /// </summary>
        public string Zona_lugar { get; set; }

        /// <summary>
        /// Obtiene y establece las actividades para la gestión del proceso.
        /// </summary>
        public string Actividad { get; set; }

        /// <summary>
        /// Obtiene y establece las tareas para la gestión de la actividad..
        /// </summary>
        public string Tarea { get; set; }

        /// <summary>
        /// Obtiene y establece si la tarea es rutinaria o no.
        /// </summary>
        public string Rutinaria { get; set; }

        /// <summary>
        /// Obtiene y establece la fuente generadora de peligro.
        /// </summary>
        public string Fuente_Generadora_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece la clasificación del peligro.
        /// </summary>
        public string Clasificacion { get; set; }

        /// <summary>
        /// Obtiene y establece la descripción del peligro.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de personas de planta de la empresa expuestas al peligro.
        /// </summary>
        [Display(Name = "Cantidad")]
        public int PlantaDirecto { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal directo.
        /// </summary>
        [Display(Name = "Horas de Exposicion")]
        public int HorasDeExposicionPlanta { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de personas contratista de la empresa expuestas al peligro.
        /// </summary>
         [Display(Name = "Cantidad")]
        public int Contratista { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal contratista.
        /// </summary>
        [Display(Name = "Horas de Exposicion")]
        public int HorasDeExposicionContratista { get; set; }

        /// <summary>
        /// Obtiene y establece  la cantidad de personas temporales de la empresa expuestas al peligro.
        /// </summary>
        [Display(Name = "Cantidad")]
        public int Temporal { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal Temporal.
        /// </summary>
         [Display(Name = "Horas de Exposicion")]
        public int HorasDeExposicionTemporal { get; set; }

        /// <summary>
        /// Obtiene y establece  la cantidad de personas estudiantes o pasantes de la empresa expuestas al peligro.
        /// </summary>
         [Display(Name = "Cantidad")]
        public int EstudiantePasante { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal Estudiante.
        /// </summary>
        [Display(Name = "Horas de Exposicion")]
        public int HorasDeExposicionEstudiante { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de personas visitantes a la empresa expuestas al peligro.
        /// </summary>
         [Display(Name = "Cantidad")]
        public int Visitante { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal Visitante.
        /// </summary>
        [Display(Name = "Horas de Exposicion")]
        public int HorasDeExposicionVisitante { get; set; }

        /// <summary>
        /// Obtiene y establece las consecuencias reales
        /// </summary>
        public string ConsecuenciasReales { get; set; }

        /// <summary>
        /// Obtiene y establece las consecuencias reales
        /// </summary>
        public string ConsecuenciasPontenciales { get; set; }


        /// <summary>
        /// Obtiene y establece la fuente de peligro.
        /// </summary>
        public string Fuente { get; set; }

        /// <summary>
        /// Obtiene y establece el medio para controlar el peligro.
        /// </summary>
        public string Medio { get; set; }

        /// <summary>
        /// Obtiene y establece las acciones que debe realizar el trabajador para prevenir el peligro.
        /// </summary>
        public string Trabajador{ get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad .
        /// </summary>
        [Display(Name = "Probabilidad")]
        public string Nivel_De_ProbablidadPersona { get; set; } 

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
       [Display(Name = "Consecuencia")]
        public string Nivel_De_ConsecuenciaPersona { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
        [Display(Name = "Valor del Riesgo")]
        public string EstimacionRiesgoPersona { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad .
        /// </summary>
        [Display(Name = "Probabilidad")]
        public string Nivel_De_ProbablidadCliente { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
        [Display(Name = "Consecuencia")]
        public string Nivel_De_ConsecuenciaCliente { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
        [Display(Name = "Valor del Riesgo")]
        public string EstimacionRiesgoCliente { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad .
        /// </summary>
        [Display(Name = "Probabilidad")]
        public string Nivel_De_ProbablidadEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
       [Display(Name = "Consecuencia")]
        public string Nivel_De_ConsecuenciaEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
        [Display(Name = "Valor del Riesgo")]
        public string EstimacionRiesgoEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad .
        /// </summary>
        [Display(Name = "Probabilidad")]
        public string Nivel_De_ProbablidadEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
       [Display(Name = "Consecuencia")]
        public string Nivel_De_ConsecuenciaEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
        [Display(Name = "Valor del Riesgo")]
        public string EstimacionRiesgoEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad .
        /// </summary>
        [Display(Name = "Probabilidad")]
        public string Nivel_De_ProbablidadAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
        [Display(Name = "Consecuencia")]
        public string Nivel_De_ConsecuenciaAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
       [Display(Name = "Valor del Riesgo")]
        public string EstimacionRiesgoAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de riesgo.
        /// </summary>
        public string NivelDeRiesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la información que al eliminar el elemento ayude a eliminar el peligro..
        /// </summary>
        public string Eliminacion { get; set; }

        /// <summary>
        /// Obtiene y establece la información de sustitución de elementos para disminuir el peligro.
        /// </summary>
        public string Sustitucion { get; set; }

        /// <summary>
        /// Obtiene y establece los controles de ingeniería.
        /// </summary>
        public string Controles_De_Ingenieria { get; set; }

        /// <summary>
        /// Obtiene y establece los controles administrativos.
        /// </summary>
        public string Controles_Administrativos { get; set; }

        /// <summary>
        /// Obtiene y establece los elementos de protección personal.
        /// </summary>
        public string Elementos_De_Proteccion { get; set; }
        
    }
}