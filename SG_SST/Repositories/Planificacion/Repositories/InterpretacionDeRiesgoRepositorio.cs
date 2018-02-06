// <copyright file="InterpretacionDeProbabilidadRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>16/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IInterpretacionDeRiesgoRepositorio y repositorio para las
// la gestion de las clases InterpretacionDeRiesgoRepositorio con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System.Collections.Generic;
    using System.Linq;

    public class InterpretacionDeRiesgoRepositorio : IInterpretacionDeRiesgoRepositorio
    {
        
        SG_SSTContext db;

        public InterpretacionDeRiesgoRepositorio()
        {
            db = new SG_SSTContext();
        }       

        public InterpretacionNivelDeRiesgo ObtenerInterpretacionDeRiesgo(int valor_Del_Riesgo) 
        {           
            List<InterpretacionNivelDeRiesgo> interpretacionRiesgoList = db.Tbl_Interpretacion_Nivel_Riesgo.
                 Where(idp => idp.Nivel_Superior >= valor_Del_Riesgo && idp.Nivel_Inferior <= valor_Del_Riesgo).ToList();
            if (interpretacionRiesgoList.Count() > 0)
            {
                return interpretacionRiesgoList.FirstOrDefault();
            }
            return null;
        }

    }
}