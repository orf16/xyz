// <copyright file="GTC45Servicio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IGTC45Servicio y servicios para las
// la gestion de  los peligros de metodologia gtc45</summary>

namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;

    public class GTC45Servicio : IGTC45Servicio
    {
        INivelDeDeficienciaRepositorio nivelDeDeficienciaReposotorio;
        INivelesDeExposicionRespositorio nivelesDeExposicionRespositorio;
        IInterpretacionDeProbabilidadRepositorio interpretacionDeProbabilidadRepositorio;
        IInterpretacionDeRiesgoRepositorio interpretacionDeRiesgoRepositorio;

        public GTC45Servicio()
        {
            nivelDeDeficienciaReposotorio = new NivelDeDeficienciaRepositorio();
            nivelesDeExposicionRespositorio = new NivelesDeExposicionRespositorio();
            interpretacionDeProbabilidadRepositorio = new InterpretacionDeProbabilidadRepositorio();
            interpretacionDeRiesgoRepositorio = new InterpretacionDeRiesgoRepositorio();
        }

        internal SG_SST.Services.Planificacion.IServices.IGTC45Servicio IGTC45Servicio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int CalcularNivelProbabilidad(int PK_Deficiencia, int PK_Exposicion)
        {
            int valorDeficiencia = nivelDeDeficienciaReposotorio.ObtenerValorDeficiencia(PK_Deficiencia);
            int valorExposicion = nivelesDeExposicionRespositorio.ObtenerValorExposicion(PK_Exposicion);
            return valorDeficiencia * valorExposicion;
        }

        public string ConsultarInterpretacionDeProbabilidad(int valor_De_Probalidad) 
        {
            return interpretacionDeProbabilidadRepositorio.ConsultarInterpretacion(valor_De_Probalidad);
        }


        public InterpretacionNivelDeRiesgo ObtenerInterpretacionDeRiesgo(int valor_Del_Riesgo) 
        {
            return interpretacionDeRiesgoRepositorio.ObtenerInterpretacionDeRiesgo(valor_Del_Riesgo);
        }
    }
}