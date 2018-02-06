// <copyright file="MatrixGTC45.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Objeto que contiene la informacion de una matrix de la metodologia GTC45 .</summary>

namespace SG_SST.Dtos.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    

    public class MatrizGTC45
    {
        public MatrizGTC45() { 
        }
        public MatrizGTC45(int Id_Peligro,string Proceso, string Zona_lugar, string Actividad, string Tarea, string Rutinaria,
            string Fuente_Generadora_De_Peligro, string Clasificacion, string Descripcion, string Efectos_Posibles,
            string Fuente, string Medio, string Trabajador, string Nivel_De_Deficiencia, string Nivel_De_Exposicion,
            int Nivel_De_Probablidad, string Interpretacion_Nivel_Probabilidad, string Nivel_De_Consecuencia,
            int Nivel_De_Riesgo, string Interpretacion_Nivel_Riesgo, string Aceptabilidad_Del_Riesgo,
            int Numero_De_Expuestos, string Peor_Consecuencia, string FLG_Requisito_Legal, string Eliminacion, string Sustitucion,
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
            this.Efectos_Posibles = Efectos_Posibles;
            this.Fuente = Fuente;
            this.Medio = Medio;
            this.Trabajador = Trabajador;
            this.Nivel_De_Deficiencia = Nivel_De_Deficiencia;
            this.Nivel_De_Exposicion = Nivel_De_Exposicion;
            this.Nivel_De_Probablidad = Nivel_De_Probablidad;
            this.Interpretacion_Nivel_Probabilidad = Interpretacion_Nivel_Probabilidad;
            this.Nivel_De_Consecuencia = Nivel_De_Consecuencia;
            this.Nivel_De_Riesgo = Nivel_De_Riesgo;
            this.Interpretacion_Nivel_Riesgo = Interpretacion_Nivel_Riesgo;
            this.Aceptabilidad_Del_Riesgo = Aceptabilidad_Del_Riesgo;
            this.Numero_De_Expuestos = Numero_De_Expuestos;
            this.Peor_Consecuencia = Peor_Consecuencia;
            this.FLG_Requisito_Legal = FLG_Requisito_Legal;
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
        /// Obtiene y establece los efectos del peligro.
        /// </summary>
        public string Efectos_Posibles { get; set; }

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
        /// Obtiene y establece el nivel de deficiencia.
        /// </summary>
        public string Nivel_De_Deficiencia { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de Nivel_De_Exposicion.
        /// </summary>
        public string Nivel_De_Exposicion { get; set; }   

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad .
        /// </summary>
        public int Nivel_De_Probablidad { get; set; }

        /// <summary>
        /// Obtiene y establece la interpretacion del nivel de probabilidad.
        /// </summary>
        public string Interpretacion_Nivel_Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de Consecuencia .
        /// </summary>
        public string Nivel_De_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de riesgo .
        /// </summary>
        public int Nivel_De_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la interpretacion del nivel de riesgo.
        /// </summary>
        public string Interpretacion_Nivel_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la aceptabilidad del riesgo.
        /// </summary>
        public string Aceptabilidad_Del_Riesgo{ get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de trabajadores expuesto al peligro   
        /// </summary>
        public int Numero_De_Expuestos { get; set; }

        /// <summary>
        /// Obtiene y establece la peor consecuencia al estar expuesto al peligro..
        /// </summary>
        public string Peor_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece si existe requisito legal para el peligro.
        /// </summary>
        public string FLG_Requisito_Legal { get; set; }

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