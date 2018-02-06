// <copyright file="EstimacionDeRiesgosRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>17/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IEstimacionDeRiesgosRepositorio y repositorio para las
// la gestion de las clase EstimacionDeRiesgos con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class EstimacionDeRiesgosRepositorio : IEstimacionDeRiesgosRepositorio
    {
        SG_SSTContext db;
        public EstimacionDeRiesgosRepositorio()
        {
            db = new SG_SSTContext();
        }
        
        public EstimacionDeRiesgo ObtenerDetalleEstimacion(int Pk_Probabilidad, int PK_Consecuencia)
        {
            EstimacionDeRiesgo detalle =null;
            List<EstimacionDeRiesgo> estimacionDeRiesgosList = db.Tbl_Estimacion_De_Riesgo
                 .Where(edr => edr.FK_Probabilidad == Pk_Probabilidad && edr.FK_Consecuencia == PK_Consecuencia).ToList();
            if (estimacionDeRiesgosList.Count() > 0)
            {
                detalle = estimacionDeRiesgosList.FirstOrDefault();
            }
            return detalle;
        }
    }
}