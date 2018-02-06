// <copyright file="NivelDeDeficienciaServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>11/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz INivelDeDeficienciaServicios y servicios para las
// la gestion de las clases de los niveles de defiencia</summary>

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

    public class NivelDeDeficienciaServicios : INivelDeDeficienciaServicios
    {
        INivelDeDeficienciaRepositorio nivelDeDeficienciaRepositorio;

        public NivelDeDeficienciaServicios ()
        {
            nivelDeDeficienciaRepositorio = new NivelDeDeficienciaRepositorio();
        }

        public SG_SST.Repositories.Planificacion.Repositories.NivelDeDeficienciaRepositorio NivelDeDeficienciaRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public List<NivelDeDeficiencia> ConsultarNivelesDeDeficiencia(bool FLAG_Cuantitativa) 
        {
            return nivelDeDeficienciaRepositorio.ConsultarNivelesDeDeficiencia(FLAG_Cuantitativa);
        }

    }
}