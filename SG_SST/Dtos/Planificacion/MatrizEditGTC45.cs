
namespace SG_SST.Dtos.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class MatrizEditGTC45 : MatrizGTC45
    {
        public MatrizEditGTC45(string Nombre_Del_Profesional, string Numero_De_Documento,string Numero_De_Licencia_SST,
            DateTime Fecha_De_Evaluacion,int idClasificacion,int idDescripcion,bool FLG_Tipo_De_Calificacion,
            int idNivelDeficiencia,int idNivelExposicion,int idNivelConsecuencia,int idGTC45,int idConsecuenciaPorPeligro,
            int Id_Peligro, string Proceso, string Zona_lugar, string Actividad, string Tarea, string Rutinaria,
            string Fuente_Generadora_De_Peligro, string Clasificacion, string Descripcion, string Efectos_Posibles,
            string Fuente, string Medio, string Trabajador, string Nivel_De_Deficiencia, string Nivel_De_Exposicion,
            int Nivel_De_Probablidad, string Interpretacion_Nivel_Probabilidad, string Nivel_De_Consecuencia,
            int Nivel_De_Riesgo, string Interpretacion_Nivel_Riesgo, string Aceptabilidad_Del_Riesgo,
            int Numero_De_Expuestos, string Peor_Consecuencia, string FLG_Requisito_Legal, string Eliminacion, string Sustitucion,
            string Controles_De_Ingenieria, string Controles_Administrativos, string Elementos_De_Proteccion, int idProceso, string Otro, bool FLG_Higienico)
            :base (Id_Peligro,Proceso, Zona_lugar, Actividad, Tarea, Rutinaria,
             Fuente_Generadora_De_Peligro, Clasificacion, Descripcion, Efectos_Posibles,
             Fuente, Medio,Trabajador,Nivel_De_Deficiencia, Nivel_De_Exposicion,
             Nivel_De_Probablidad,Interpretacion_Nivel_Probabilidad,  Nivel_De_Consecuencia,
             Nivel_De_Riesgo,Interpretacion_Nivel_Riesgo,  Aceptabilidad_Del_Riesgo,
             Numero_De_Expuestos,Peor_Consecuencia,FLG_Requisito_Legal,Eliminacion,Sustitucion,
             Controles_De_Ingenieria,Controles_Administrativos,Elementos_De_Proteccion)
        {
            this.Nombre_Del_Profesional = Nombre_Del_Profesional;
            this.Numero_De_Documento = Numero_De_Documento;
            this.Numero_De_Licencia_SST = Numero_De_Licencia_SST;
            this.Fecha_De_Evaluacion = Fecha_De_Evaluacion;
            this.idClasificacion = idClasificacion;
            this.idDescripcion = idDescripcion;
            this.FLG_Tipo_De_Calificacion = FLG_Tipo_De_Calificacion;
            this.idNivelDeficiencia = idNivelDeficiencia;
            this.idNivelExposicion = idNivelExposicion;
            this.idNivelConsecuencia = idNivelConsecuencia;
            this.idGTC45 = idGTC45;
            this.idConsecuenciaPorPeligro = idConsecuenciaPorPeligro;
            this.idProceso = idProceso;
            this.Otro = Otro;
            this.FLG_Higienico = FLG_Higienico;
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
        /// Obtiene y establece del tipo de calificacion.cuantitativo como verdadero(true) y cualitativo como falso(false)
        /// </summary>
        public bool FLG_Tipo_De_Calificacion { get; set; }

        /// <summary>
        /// Obtiene y establece el id del nivel de deficiencia 
        /// </summary>
        public int idNivelDeficiencia{ get; set; }

        /// <summary>
        /// Obtiene y establece el id del nivel de Exposicion
        /// </summary>
        public int idNivelExposicion { get; set; }

        /// <summary>
        /// Obtiene y establece el id del nivel de Consecuencia
        /// </summary>
        public int idNivelConsecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece el id del nivel de la metdologia GT45
        /// </summary>
        public int idGTC45 { get; set; }

        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro
        /// </summary>
        public int idConsecuenciaPorPeligro { get; set; }

        /// <summary>
        /// Obtiene y establece el id del proceso
        /// </summary>
        public int idProceso { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion cuando el tipo de peligro es otro.
        /// </summary>
        public string Otro { get; set; }

        /// <summary>
        /// Obtiene y establece si el peligro es higienico.
        /// </summary>
        public bool FLG_Higienico { get; set; }
        
        
    }
}