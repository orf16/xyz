// <copyright file="ClasificacionDePeligrosServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IClasificacionDePeligrosServicios y servicios para las
// la gestion de las clases de peligros</summary>

namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;
    using System.Collections.Generic;

    public class ClasificacionDePeligrosServicios : IClasificacionDePeligrosServicios
    {
        IClasificacionDePeligrosRepositorio clasesDePeligrosRepositorio;
        public ClasificacionDePeligrosServicios()
        {
            clasesDePeligrosRepositorio = new ClasificacionDePeligrosRepositorio();
        }

        internal SG_SST.Services.Planificacion.IServices.IClasificacionDePeligrosServicios IClasificacionDePeligrosServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<ClasificacionDePeligro> ConsultarClasesDePeligros(int Pk_Tipo_Peligro)
        {
            return clasesDePeligrosRepositorio.ConsultarClasesDePeligros(Pk_Tipo_Peligro);
        }
    }
}