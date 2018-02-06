// <copyright file="ClasificacionDePeligrosRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IClasificacionDePeligrosRepositorio y repositorio para las
// la gestion de las clases de peligros con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System.Collections.Generic;
    using System.Linq;

    public class ClasificacionDePeligrosRepositorio : IClasificacionDePeligrosRepositorio
    {
        SG_SSTContext db;
        public ClasificacionDePeligrosRepositorio()
        {
            db = new SG_SSTContext();
        }

        public List<ClasificacionDePeligro> ConsultarClasesDePeligros(int Pk_Tipo_Peligro)
        {
            return db.Tbl_Clasificacion_De_Peligro.Where(cp => cp.FK_Tipo_De_Peligro == Pk_Tipo_Peligro).ToList();
        }


    }
}