using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Planificacion
{
    public class MatrizEditRAM : MatrizRAM
    {
        public MatrizEditRAM(string Nombre_Del_Profesional, string Numero_De_Documento, string Numero_De_Licencia_SST,
            DateTime Fecha_De_Evaluacion, int idClasificacion, int idDescripcion,
            int idProbabilidadPersona,int idProbabilidadClientes,int idProbabilidadEconomica,int idProbabilidadEmpresa,
            int idProbabilidadAmbiental,
            int idConsecuenciaPersona,int idConsecuenciaClientes,int idConsecuenciaEconomica,int idConsecuenciaEmpresa,
            int idConsecuenciaAmbiental,
            int idvalorDeRiesgoPersona, int idvalorDeRiesgoClientes, int idvalorDeRiesgoEconomica, int idvalorDeRiesgoEmpresa,
            int idvalorDeRiesgoAmbiental,
            int idPersonaExpuesta,int idRAM,
            int idProbabilidadPersonaExpuestaPersona,int idProbabilidadPersonaExpuestaClientes,int idProbabilidadPersonaExpuestaEconomica, 
            int idProbabilidadPersonaExpuestaEmpresa,int idProbabilidadPersonaExpuestaAmbiental,
            int idConsecuenciaPorPeligroPersona, int idConsecuenciaPorPeligroClientes, int idConsecuenciaPorPeligroEconomica,
            int idConsecuenciaPorPeligroEmpresa,int idConsecuenciaPorPeligroAmbiental,
            int idEstimacionDeRiesgosPorRAMPersona,int idEstimacionDeRiesgosPorRAMClientes,int idEstimacionDeRiesgosPorRAMEconomica,
            int idEstimacionDeRiesgosPorRAMEmpresa,int idEstimacionDeRiesgosPorRAMAmbiental,
            int Id_Peligro, string Proceso, string Zona_lugar, string Actividad, string Tarea, string Rutinaria,
            string Fuente_Generadora_De_Peligro, string Clasificacion, string Descripcion, int PlantaDirecto,
            int HorasDeExposicionPlanta, int Contratista, int HorasDeExposicionContratista, int Temporal,
            int HorasDeExposicionTemporal, int EstudiantePasante, int HorasDeExposicionEstudiante,
            int Visitante, int HorasDeExposicionVisitante,
            string ConsecuenciasReales, string ConsecuenciasPontenciales,
            string Fuente, string Medio, string Trabajador,
            string Nivel_De_ProbablidadPersona, string Nivel_De_ConsecuenciaPersona, string EstimacionRiesgoPersona,
            string Nivel_De_ProbablidadCliente, string Nivel_De_ConsecuenciaCliente, string EstimacionRiesgoCliente,
            string Nivel_De_ProbablidadEconomica, string Nivel_De_ConsecuenciaEconomica, string EstimacionRiesgoEconomica,
            string Nivel_De_ProbablidadEmpresa, string Nivel_De_ConsecuenciaEmpresa, string EstimacionRiesgoEmpresa,
            string Nivel_De_ProbablidadAmbiental, string Nivel_De_ConsecuenciaAmbiental, string EstimacionRiesgoAmbiental,
            string NivelDeRiesgo,
            string Eliminacion, string Sustitucion,
            string Controles_De_Ingenieria, string Controles_Administrativos, string Elementos_De_Proteccion, int idProceso, string Otro)
            :base
            ( Id_Peligro, Proceso,  Zona_lugar,  Actividad,  Tarea,  Rutinaria,
             Fuente_Generadora_De_Peligro,  Clasificacion,  Descripcion, PlantaDirecto,
             HorasDeExposicionPlanta, Contratista, HorasDeExposicionContratista, Temporal,
             HorasDeExposicionTemporal, EstudiantePasante, HorasDeExposicionEstudiante,
             Visitante, HorasDeExposicionVisitante,
             ConsecuenciasReales, ConsecuenciasPontenciales,
             Fuente,  Medio,  Trabajador,
             Nivel_De_ProbablidadPersona,  Nivel_De_ConsecuenciaPersona, EstimacionRiesgoPersona,
             Nivel_De_ProbablidadCliente,  Nivel_De_ConsecuenciaCliente,  EstimacionRiesgoCliente,
             Nivel_De_ProbablidadEconomica,  Nivel_De_ConsecuenciaEconomica,  EstimacionRiesgoEconomica,
             Nivel_De_ProbablidadEmpresa,  Nivel_De_ConsecuenciaEmpresa,  EstimacionRiesgoEmpresa,
             Nivel_De_ProbablidadAmbiental,  Nivel_De_ConsecuenciaAmbiental,  EstimacionRiesgoAmbiental,
             NivelDeRiesgo,
             Eliminacion,  Sustitucion,
             Controles_De_Ingenieria,Controles_Administrativos,Elementos_De_Proteccion)
        {
            this.Nombre_Del_Profesional = Nombre_Del_Profesional;
            this.Numero_De_Documento = Numero_De_Documento;
            this.Numero_De_Licencia_SST = Numero_De_Licencia_SST;
            this.Fecha_De_Evaluacion = Fecha_De_Evaluacion;
            this.idClasificacion = idClasificacion;
            this.idDescripcion = idDescripcion;

            this.idProbabilidadPersona = idProbabilidadPersona;
            this.idProbabilidadClientes = idProbabilidadClientes;
            this.idProbabilidadEconomica = idProbabilidadEconomica;
            this.idProbabilidadEmpresa = idProbabilidadEmpresa;
            this.idProbabilidadAmbiental = idProbabilidadAmbiental;

            this.idConsecuenciaPersona = idConsecuenciaPersona;
            this.idConsecuenciaClientes = idConsecuenciaClientes;
            this.idConsecuenciaEconomica = idConsecuenciaEconomica;
            this.idConsecuenciaEmpresa = idConsecuenciaEmpresa;
            this.idConsecuenciaAmbiental = idConsecuenciaAmbiental;

            this.idvalorDeRiesgoPersona = idvalorDeRiesgoPersona;
            this.idvalorDeRiesgoClientes = idvalorDeRiesgoClientes;
            this.idvalorDeRiesgoEconomica = idvalorDeRiesgoEconomica;
            this.idvalorDeRiesgoEmpresa = idvalorDeRiesgoEmpresa;
            this.idvalorDeRiesgoAmbiental = idvalorDeRiesgoAmbiental;

            this.idPersonaExpuesta = idPersonaExpuesta;
            this.idRAM = idRAM;

            this.idProbabilidadPersonaExpuestaPersona = idProbabilidadPersonaExpuestaPersona;
            this.idProbabilidadPersonaExpuestaClientes = idProbabilidadPersonaExpuestaClientes;
            this.idProbabilidadPersonaExpuestaEconomica = idProbabilidadPersonaExpuestaEconomica;
            this.idProbabilidadPersonaExpuestaEmpresa = idProbabilidadPersonaExpuestaEmpresa;
            this.idProbabilidadPersonaExpuestaAmbiental = idProbabilidadPersonaExpuestaAmbiental;

            this.idConsecuenciaPorPeligroPersona = idConsecuenciaPorPeligroPersona;
            this.idConsecuenciaPorPeligroClientes = idConsecuenciaPorPeligroClientes;
            this.idConsecuenciaPorPeligroEconomica = idConsecuenciaPorPeligroEconomica;
            this.idConsecuenciaPorPeligroEmpresa = idConsecuenciaPorPeligroEmpresa;
            this.idConsecuenciaPorPeligroAmbiental = idConsecuenciaPorPeligroAmbiental;

            this.idEstimacionDeRiesgosPorRAMPersona = idEstimacionDeRiesgosPorRAMPersona;
            this.idEstimacionDeRiesgosPorRAMClientes = idEstimacionDeRiesgosPorRAMClientes;
            this.idEstimacionDeRiesgosPorRAMEconomica = idEstimacionDeRiesgosPorRAMEconomica;
            this.idEstimacionDeRiesgosPorRAMEmpresa = idEstimacionDeRiesgosPorRAMEmpresa;
            this.idEstimacionDeRiesgosPorRAMAmbiental = idEstimacionDeRiesgosPorRAMAmbiental;

            this.idProceso = idProceso;

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
        /// Obtiene y establece el id de la probabilidad Persona
        /// </summary>
        public int idProbabilidadPersona { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabilidad Clientes
        /// </summary>
        public int idProbabilidadClientes { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabilidad Economica
        /// </summary>
        public int idProbabilidadEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabilidad Empresa
        /// </summary>
        public int idProbabilidadEmpresa { get; set; }
        
        /// <summary>
        /// Obtiene y establece el id de la probabilidad Ambiental
        /// </summary>
        public int idProbabilidadAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la consecuencia persona
        /// </summary>
        public int idConsecuenciaPersona { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la consecuencia clientes
        /// </summary>
        public int idConsecuenciaClientes { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la consecuencia Economica
        /// </summary>
        public int idConsecuenciaEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la consecuencia Empresa
        /// </summary>
        public int idConsecuenciaEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la consecuencia Ambiental
        /// </summary>
        public int idConsecuenciaAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el valor del riesgo del grupo persona
        /// </summary>
        public int idvalorDeRiesgoPersona { get; set; }

        /// <summary>
        /// Obtiene y establece el valor del riesgo del grupo Clientes
        /// </summary>
        public int idvalorDeRiesgoClientes { get; set; }

        /// <summary>
        /// Obtiene y establece el valor del riesgo del grupo Economica
        /// </summary>
        public int idvalorDeRiesgoEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el valor del riesgo del grupo Empresa
        /// </summary>
        public int idvalorDeRiesgoEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el valor del riesgo del grupo Ambiental
        /// </summary>
        public int idvalorDeRiesgoAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la persona expuesta
        /// </summary>
        public int idPersonaExpuesta { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la peligro tipo RAM
        /// </summary>
        public int idRAM { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabidlidad por persona expuesta del grupo persona
        /// </summary>
        public int idProbabilidadPersonaExpuestaPersona { get; set; }


        /// <summary>
        /// Obtiene y establece el id de la probabidlidad por persona expuesta del grupo Cliente
        /// </summary>
        public int idProbabilidadPersonaExpuestaClientes { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabidlidad por persona expuesta del grupo Economica
        /// </summary>
        public int idProbabilidadPersonaExpuestaEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabidlidad por persona expuesta del grupo Empresa 
        /// </summary>
        public int idProbabilidadPersonaExpuestaEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el id de la probabidlidad por persona expuesta del grupo Ambiental 
        /// </summary>
        public int idProbabilidadPersonaExpuestaAmbiental { get; set; }


        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro Persona
        /// </summary>
        public int idConsecuenciaPorPeligroPersona { get; set; }

        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro Clientes
        /// </summary>
        public int idConsecuenciaPorPeligroClientes { get; set; }

        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro Economica
        /// </summary>
        public int idConsecuenciaPorPeligroEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro Empresa
        /// </summary>
        public int idConsecuenciaPorPeligroEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el id de las consecuencias por peligro Ambiental
        /// </summary>
        public int idConsecuenciaPorPeligroAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el id de  la estimacion de riesgos por Ram persona
        /// </summary>
        public int idEstimacionDeRiesgosPorRAMPersona { get; set; }

        /// <summary>
        /// Obtiene y establece el id de  la estimacion de riesgos por Ram Cliente
        /// </summary>
        public int idEstimacionDeRiesgosPorRAMClientes { get; set; }

        /// <summary>
        /// Obtiene y establece el id de  la estimacion de riesgos por Ram Economica
        /// </summary>
        public int idEstimacionDeRiesgosPorRAMEconomica { get; set; }

        /// <summary>
        /// Obtiene y establece el id de  la estimacion de riesgos por Ram Empresa
        /// </summary>
        public int idEstimacionDeRiesgosPorRAMEmpresa { get; set; }

        /// <summary>
        /// Obtiene y establece el id de  la estimacion de riesgos por Ram Ambiental
        /// </summary>
        public int idEstimacionDeRiesgosPorRAMAmbiental { get; set; }

        /// <summary>
        /// Obtiene y establece el id del proceso
        /// </summary>
        public int idProceso { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion cuando el tipo de peligro es otro.
        /// </summary>
        public string Otro { get; set; }
    }
}