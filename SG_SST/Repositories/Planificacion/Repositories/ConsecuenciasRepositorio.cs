// <copyright file="ConsecuenciasRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>12/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IConsecuenciasRepositorio y repositorio para las
// la gestion de las consecuencias con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class ConsecuenciasRepositorio : IConsecuenciasRepositorio
    {

        SG_SSTContext db;
        public ConsecuenciasRepositorio()
        {
            db = new SG_SSTContext();
        }

    
        public List<Consecuencia> ObtenerConsecuencias(int PK_TipoMedologia)
        {
            return db.Tbl_Consecuencias.Where(c => c.Grupo.Metodologia.PK_Metodologia == PK_TipoMedologia).ToList();
        }

        public int ObtenerValorConsecuencia(int PK_Consecuencia)
        {
            int valorConsecuencia = -1;
            Consecuencia consecuencia = db.Tbl_Consecuencias.Find(PK_Consecuencia);
            if (consecuencia != null)
            {
                valorConsecuencia = consecuencia.Valor_Consecuencia;
            }
            return valorConsecuencia;
        }

        public List<Consecuencia> ObtenerConsecuenciasPorGrupo(int PK_Grupo)
        {
            return db.Tbl_Consecuencias.Where(c => c.FK_Grupo == PK_Grupo).ToList();
        }
    }
}