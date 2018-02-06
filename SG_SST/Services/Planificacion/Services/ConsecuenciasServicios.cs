// <copyright file="ConsecuenciasServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IConsecuenciasServicios y servicios para las
// la gestion de las consecuencias de peligros</summary>

namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;
    using System.Collections.Generic;

    public class ConsecuenciasServicios : IConsecuenciasServicios
    {
        IConsecuenciasRepositorio consecuenciasRepositorio;
        public ConsecuenciasServicios()
        {
            consecuenciasRepositorio = new ConsecuenciasRepositorio();
        }    


        public List<Consecuencia> ObtenerConsecuencias(int PK_TipoMedologia)
        {
            return consecuenciasRepositorio.ObtenerConsecuencias(PK_TipoMedologia);
        }

        public int ObtenerValorConsecuencia(int PK_Consecuencia)
        {
            return consecuenciasRepositorio.ObtenerValorConsecuencia(PK_Consecuencia);
        }

        public List<Consecuencia> ObtenerConsecuenciasPorGrupo(int PK_Grupo)
        {
            return consecuenciasRepositorio.ObtenerConsecuenciasPorGrupo(PK_Grupo);
        }
    }
}