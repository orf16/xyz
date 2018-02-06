// <copyright file="Peligro.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion de los peligros registrados.</summary>

namespace SG_SST.Models.Planificacion
{
    using Aplicacion;
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Peligro")]
    public class Peligro
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del peligro.
        /// </summary>
        [Key]
        public int PK_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del profesional que elaboro la metodologia.
        /// </summary>
        public string Nombre_Del_Profesional { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de documento del profesional que elaboro la metodologia.
        /// </summary>
        public string Numero_De_Documento { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de la licencia del profesional que elaboro la metodologia.
        /// </summary>
        public string Numero_De_Licencia_SST { get; set; }

        /// <summary>
        /// Obtiene y establece la fecha en la que se realizo la metodologia.
        /// </summary>
        public DateTime Fecha_De_Evaluacion { get; set; }

        /// <summary>
        /// Obtiene y establece el lugar donde se presenta el peligro.
        /// </summary>
        public string Lugar { get; set; }

        /// <summary>
        /// Obtiene y establece la actividad del proceso.
        /// </summary>
        public string Actividad { get; set; }

        /// <summary>
        /// Obtiene y establece la tarea del proceso.
        /// </summary>
        public string Tarea { get; set; }

        /// <summary>
        /// Obtiene y establece si la tarea es rutinaria.
        /// </summary>
        public bool FLG_Rutinaria { get; set; }

        /// <summary>
        /// Obtiene y establece la fuente generadora de peligor.
        /// </summary>
        public string Fuente_Generadora_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece la fuente generadora de peligro cuando no esta clasficada.
        /// </summary>
        public string Otro { get; set; }

        /// <summary>
        /// Obtiene y establece la fuente de peligro.
        /// </summary>
        public string Fuente { get; set; }

        /// <summary>
        /// Obtiene y establece el medio para controlar el peligro.
        /// </summary>
        public string Medio { get; set; }   

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
        /// Obtiene y establece las acciones que debe realizar el trabajador para prevenir el peligro.
        /// </summary>
        public string Accion_De_Prevencion { get; set; }

        

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  clasificacion de peligro.
        /// </summary>
        [ForeignKey("ClasificacionDePeligro")]
        public int FK_Clasificacion_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de clasificacion de peligro.
        /// </summary>
        [ForeignKey("PK_Clasificacion_De_Peligro")]
        public virtual ClasificacionDePeligro ClasificacionDePeligro { get; set; }


        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  sede.
        /// </summary>
        [ForeignKey("Sede")]
        public int FK_Sede { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de sede.
        /// </summary>
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }


        [ForeignKey("Procesos")]
        public int FK_Proceso { get; set; }

        [ForeignKey("Pk_Id_Proceso")]
        public virtual Proceso Procesos { get; set; }


        /// <summary>
        /// Obtiene y establece una coleccion de GTC45.
        /// </summary>
        public ICollection<GTC45> GTC45 { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de PersonaExpuesta.
        /// </summary>
        public ICollection<PersonaExpuesta> PersonaExpuestas { get; set; }


        /// <summary>
        /// Obtiene y establece una coleccion de ConsecuenciaPorPeligro.
        /// </summary>
        public ICollection<ConsecuenciaPorPeligro> ConsecuenciasPorPeligros { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de Peligros para equipo, maquinaria y herramientas.
        /// </summary>
        public ICollection<PeligroEMH> PeligroEMHs { get; set; }

    }
}