// <copyright file="IGTC45Servicio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos(Servicios) a implementar para la gestion del peligro del tipo de metodologia gtc45.</summary>

namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IGTC45Servicio
    {
        /// <summary>
        /// Definicion del metodo que me calcula el nivel de probabilidad, multiplicacion del nivel de deficiencia por el
        /// nivel de exposicion
        /// </summary>
        /// <param name="PK_Deficiencia">clave primaria del nivel de deficiencia</param>
        /// <param name="PK_Exposicion">clave primaria del nivel de exposicion </param>
        /// <returns>valor del nivel del probabilidad</returns>
        int CalcularNivelProbabilidad(int PK_Deficiencia, int PK_Exposicion);

        /// <summary>
        /// Definicion del mentodo que me retorna la interpretacion del nivel de probabilidad
        /// </summary>
        /// <param name="valor_De_Probalidad">Valor o cantidad de probabilidad</param>
        /// <returns>descripcion o interpretacion de probablidad</returns>
        string ConsultarInterpretacionDeProbabilidad(int valor_De_Probalidad);

        /// <summary>
        /// Definicion del metodo que un objeto de tipo interpretacion de riesgo.
        /// </summary>
        /// <param name="valor_De_Probalidad">valor o cantidad del riesgo</param>
        /// <returns>un objeto de tipo Intepretacion de riesgo</returns>
        InterpretacionNivelDeRiesgo ObtenerInterpretacionDeRiesgo(int valor_Del_Riesgo);

    }
}
