// <copyright file="PeligrosPorMetodologias.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Objeto que contiene la informacion basica de un peligro como la  sede, el nombre de la metdologia
// a la que pertenece, el id del peligro y el id de la sede .</summary>

namespace SG_SST.Dtos.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class PeligrosPorSede
    {
        public PeligrosPorSede(int IdSede, string NombreSede, int IdMetodologia, string NombreMetodologia)
        {
            this.IdSede = IdSede;
            this.NombreSede = NombreSede;
            this.IdMetodologia = IdMetodologia;
            this.NombreMetodologia = NombreMetodologia;
        }

        /// <summary>
        /// Obtiene y establece el id de la sede.
        /// </summary> 
        public int IdSede { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre de la sede
        /// </summary>
        public string NombreSede { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre de la sede
        /// </summary>
        public int IdMetodologia { get; set; }

        /// <summary>
        /// Obtiene y establece  el nombre de la metodologia
        /// </summary>
        public string NombreMetodologia { get; set; }
    }
}