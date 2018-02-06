
namespace SG_SST.Dtos.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class MatrizEditINSTH : MatrizINSTH
    {
        public MatrizEditINSTH(string Nombre_Del_Profesional, string Numero_De_Documento, string Numero_De_Licencia_SST,
            DateTime Fecha_De_Evaluacion, int idClasificacion, int idDescripcion, int idNivelConsecuencia,
            int idProbabilidad,int idConsecuenciaPorPeligro,int idPersonaExpuesta,int idProbabilidadPorPersonaExpuesta,
            int idINSHT,
            int Id_Peligro, string Proceso, string Zona_lugar, string Actividad, string Tarea, string Rutinaria,
            string Fuente_Generadora_De_Peligro, string Clasificacion, string Descripcion, int PlantaDirecto,
            int HorasDeExposicionPlanta, int Contratista, int HorasDeExposicionContratista, int Temporal,
            int HorasDeExposicionTemporal, int EstudiantePasante, int HorasDeExposicionEstudiante,
            int Visitante, int HorasDeExposicionVisitante,
            string Fuente, string Medio, string Trabajador,
            string Nivel_De_Probablidad, string Nivel_De_Consecuencia,
            string EstimacionRiesgo, string Eliminacion, string Sustitucion,
            string Controles_De_Ingenieria, string Controles_Administrativos, string Elementos_De_Proteccion,
            string MedidasDeControl, string ProcedimientosDeTrabajo, string Informacion, string Formacion,
            string RiesgoControlado, string AccionRequerida, string Responsable, DateTime FechaFinalizacion, DateTime FechaDeComprobacion,
            int idProceso, string FirmaResponsable, string Otro)
            : base(Id_Peligro, Proceso, Zona_lugar, Actividad, Tarea, Rutinaria,
             Fuente_Generadora_De_Peligro, Clasificacion, Descripcion, PlantaDirecto,
             HorasDeExposicionPlanta, Contratista, HorasDeExposicionContratista, Temporal,
             HorasDeExposicionTemporal, EstudiantePasante, HorasDeExposicionEstudiante,
             Visitante, HorasDeExposicionVisitante,
             Fuente, Medio, Trabajador,
             Nivel_De_Probablidad, Nivel_De_Consecuencia,
             EstimacionRiesgo, Eliminacion, Sustitucion,
             Controles_De_Ingenieria, Controles_Administrativos, Elementos_De_Proteccion,
             MedidasDeControl, ProcedimientosDeTrabajo, Informacion, Formacion,
             RiesgoControlado, AccionRequerida, Responsable, FechaFinalizacion, FechaDeComprobacion)
        {
            this.Nombre_Del_Profesional = Nombre_Del_Profesional;
            this.Numero_De_Documento = Numero_De_Documento;
            this.Numero_De_Licencia_SST = Numero_De_Licencia_SST;
            this.Fecha_De_Evaluacion = Fecha_De_Evaluacion;
            this.idClasificacion = idClasificacion;
            this.idDescripcion = idDescripcion;
            this.idNivelConsecuencia = idNivelConsecuencia;
            this.idProbabilidad = idProbabilidad;
            this.idConsecuenciaPorPeligro = idConsecuenciaPorPeligro;
            this.idPersonaExpuesta = idPersonaExpuesta;
            this.idProbabilidadPorPersonaExpuesta = idProbabilidadPorPersonaExpuesta;
            this.idINSHT = idINSHT;
            this.idProceso = idProceso;
            this.FirmaResponsable = FirmaResponsable;
            this.Otro = Otro;
        }
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
        /// Obtiene y establece el id de la clasificacion 
        /// </summary>
        public int idClasificacion { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la Descripcion 
        /// </summary>
        public int idDescripcion { get; set; }

        /// <summary>
        /// Obtiene y establece el id del nivel de Consecuencia
        /// </summary>
        public int idNivelConsecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabilidad 
        /// </summary>
        public int idProbabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro
        /// </summary>
        public int idConsecuenciaPorPeligro { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la persona expuesta
        /// </summary>
        public int idPersonaExpuesta { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabildiad por persona expuesta
        /// </summary>
        public int idProbabilidadPorPersonaExpuesta { get; set; }

        /// <summary>
        /// Obtiene y establece el id del INSHT
        /// </summary>
        public int idINSHT { get; set; }

        /// <summary>
        /// Obtiene y establece el id del proceso
        /// </summary>
        public int idProceso { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre de la firma responsable del plan de acción para controlar el peligro.
        /// </summary>
        public string FirmaResponsable { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion cuando el tipo de peligro es otro.
        /// </summary>
        public string Otro { get; set; }
    }
}