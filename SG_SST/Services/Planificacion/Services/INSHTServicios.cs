// <copyright file="INSHTServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IINSHTServicios y servicios para las
// la gestion de de los peligros de metodologia  INSHT</summary>

namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;

    public class INSHTServicios : IINSHTServicios
    {
        IEstimacionDeRiesgosRepositorio estimacionDeRiesgosRepositorio;
        public INSHTServicios()
        {
            estimacionDeRiesgosRepositorio = new EstimacionDeRiesgosRepositorio();
        }

        internal SG_SST.Services.Planificacion.IServices.IINSHTServicios IINSHTServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public SG_SST.Repositories.Planificacion.Repositories.EstimacionDeRiesgosRepositorio EstimacionDeRiesgosRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public EstimacionDeRiesgo ObtenerEstimacionDelRiesgo(int Pk_Probabilidad, int PK_Consecuencia)
        {
            return estimacionDeRiesgosRepositorio.ObtenerDetalleEstimacion(Pk_Probabilidad, PK_Consecuencia);
        }
    }
}