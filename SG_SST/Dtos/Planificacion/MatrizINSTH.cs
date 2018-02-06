using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Planificacion
{
    public class MatrizINSTH
    {
        public MatrizINSTH() { }
        public MatrizINSTH(int Id_Peligro, string Proceso, string Zona_lugar, string Actividad, string Tarea, string Rutinaria,
            string Fuente_Generadora_De_Peligro, string Clasificacion, string Descripcion,int PlantaDirecto,
            int HorasDeExposicionPlanta,int Contratista,int HorasDeExposicionContratista,int Temporal,
            int HorasDeExposicionTemporal,int EstudiantePasante,int HorasDeExposicionEstudiante,
            int Visitante,int HorasDeExposicionVisitante,          
            string Fuente, string Medio, string Trabajador,
            string Nivel_De_Probablidad, string Nivel_De_Consecuencia,
            string EstimacionRiesgo, string Eliminacion, string Sustitucion,
            string Controles_De_Ingenieria, string Controles_Administrativos, string Elementos_De_Proteccion,
            string MedidasDeControl, string ProcedimientosDeTrabajo, string Informacion, string Formacion,
            string RiesgoControlado, string AccionRequerida, string Responsable, DateTime FechaFinalizacion, DateTime FechaDeComprobacion)
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
            this.Fuente = Fuente;
            this.Medio = Medio;
            this.Trabajador = Trabajador;  
            this.Nivel_De_Probablidad = Nivel_De_Probablidad;           
            this.Nivel_De_Consecuencia = Nivel_De_Consecuencia;
            this.EstimacionRiesgo = EstimacionRiesgo;          
            this.Eliminacion = Eliminacion;
            this.Sustitucion = Sustitucion;
            this.Controles_De_Ingenieria = Controles_De_Ingenieria;
            this.Controles_Administrativos = Controles_Administrativos;
            this.Elementos_De_Proteccion = Elementos_De_Proteccion;
            this.MedidasDeControl = MedidasDeControl;
            this.ProcedimientosDeTrabajo = ProcedimientosDeTrabajo;
            this.Informacion = Informacion;
            this.Formacion = Formacion;
            this.RiesgoControlado = RiesgoControlado;
            this.AccionRequerida = AccionRequerida;
            this.Responsable = Responsable;
            this.FechaFinalizacion = FechaFinalizacion;
            this.FechaDeComprobacion = FechaDeComprobacion;
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
        public string Nivel_De_Probablidad { get; set; } 

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
        public string Nivel_De_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
        public string EstimacionRiesgo { get; set; }   
        
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

        /// <summary>
        /// Obtiene y establece la información de las medidas de control para combatir el peligro
        /// </summary>
        public string MedidasDeControl { get; set; }

        /// <summary>
        /// Obtiene y establece los procedimientos de trabajo para combatir el peligro.
        /// </summary>
        public string ProcedimientosDeTrabajo { get; set; }

        /// <summary>
        /// Obtiene y establece la información para combatir el peligro..
        /// </summary>
        public string Informacion { get; set; }

        /// <summary>
        /// Obtiene y establece la formación.
        /// </summary>
        public string Formacion { get; set; }

        /// <summary>
        /// Obtiene y establece si el peligro está controlado o no, las opciones de la lista son si(verdadero) y no (falso).
        /// </summary>
        public string RiesgoControlado { get; set; }


        /// <summary>
        /// Obtiene y establece la acción requerida para controlar el peligro.
        /// </summary>
        public string AccionRequerida { get; set; }

        /// <summary>
        /// Obtiene y establece el responsable del plan de acción para controlar el peligro.
        /// </summary>
        public string Responsable { get; set; }

        /// <summary>
        /// Obtiene y establece  la fecha de finalización del plan de acción para controlar el peligro.
        /// </summary>
        public DateTime FechaFinalizacion { get; set; }

        /// <summary>
        /// Obtiene y establece  la fecha de comprobación de la eficacia de la acción del plan de acción para controlar el peligro.
        /// </summary>
        public DateTime FechaDeComprobacion { get; set; }
    }
}