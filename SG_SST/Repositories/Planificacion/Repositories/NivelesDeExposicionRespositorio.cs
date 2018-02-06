// <copyright file="NivelesDeExposicionRespositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz INivelesDeExposicionRespositorio y repositorio para las
// la gestion de las clases de NivelesDeExposicion con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class NivelesDeExposicionRespositorio : INivelesDeExposicionRespositorio
    {

        SG_SSTContext db;
        public NivelesDeExposicionRespositorio()
        {
            db = new SG_SSTContext();
        }

        public int ObtenerValorExposicion(int PK_Exposicion)
        {
            NivelDeExposicion nivelDeExposicion = db.Tbl_Nivel_De_Exposicion.Find(PK_Exposicion);

            if (nivelDeExposicion != null)
            {
                return nivelDeExposicion.Valor_Exposicion;
            }
            // se retorna -1 cuando no se encuentra el valor de la Exposicion 
            return -1;
        }

    }
}