// <copyright file="ProbabilidadesRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>12/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IProbabilidadesRepositorio y repositorio para las
// la gestion de las probabilidades con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProbabilidadesRepositorio : IProbabilidadesRepositorio
    {
        SG_SSTContext db;

        public ProbabilidadesRepositorio()
        {
            db = new SG_SSTContext();
        }

     
        public List<Probabilidad> ObtenerProbabilidades(int PK_TipoMedologia)
        {
            return db.Tbl_Probabilidades.Where(p => p.FK_Metodologia == PK_TipoMedologia).ToList();
        }
    }
}