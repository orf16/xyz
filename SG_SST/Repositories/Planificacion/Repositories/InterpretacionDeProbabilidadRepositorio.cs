// <copyright file="InterpretacionDeProbabilidadRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IInterpretacionDeProbabilidadRepositorio y repositorio para las
// la gestion de las clases InterpretacionDeProbabilidadRepositorio con la base de datos</summary>


namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class InterpretacionDeProbabilidadRepositorio : IInterpretacionDeProbabilidadRepositorio
    {
        SG_SSTContext db;

        public InterpretacionDeProbabilidadRepositorio()
        {
            db = new SG_SSTContext();
        }       

        public string ConsultarInterpretacion(int valor_De_Probalidad)
        {
            var interpretacion = "";
            List<InterpretacionDeProbabilidad> interpretacionProbalidadList = db.Tbl_Interpretacion_De_Probabilidad.
                 Where(idp => idp.Nivel_Superior >= valor_De_Probalidad && idp.Nivel_Inferior <= valor_De_Probalidad).ToList();

            if (interpretacionProbalidadList.Count()>0)
            {
                interpretacion = interpretacionProbalidadList.FirstOrDefault().Interpretacion;
            }
            return interpretacion;
        }
    }
}