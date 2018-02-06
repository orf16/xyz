// <copyright file="ProbabilidadesServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>19/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IProbabilidadesServicios y servicios para las
// la gestion de las probabilidades  de los  peligros</summary>

namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProbabilidadesServicios : IProbabilidadesServicios
    {
        IProbabilidadesRepositorio probabilidadesRepositorio; 
        public ProbabilidadesServicios() {
            probabilidadesRepositorio = new ProbabilidadesRepositorio();
        }

        internal SG_SST.Services.Planificacion.IServices.IProbabilidadesServicios IProbabilidadesServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public SG_SST.Repositories.Planificacion.Repositories.ProbabilidadesRepositorio ProbabilidadesRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public List<Probabilidad> ObtenerProbabilidades(int PK_TipoMedologia) 
        {
            return probabilidadesRepositorio.ObtenerProbabilidades(PK_TipoMedologia);
        }


    }
}