// <copyright file="IRecursosSedes.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Correa.</author>
// <date>16/03/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de los Recursos</summary>

namespace SG_SST.Repositories.Organizacion.IRepositories
{
using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    interface IRecursosSedesRepositorio
    {
        /// <summary>
        /// Definicion del metodo que me retorna los recursos asignados por sedes.
        /// </summary>
        /// <returns></returns>
        List<RecursoporSede> ObtenerRecursoSede(int Pk_Sede, int Periodo);


        List<RecursoporSede> validarRecursoSede(int Pk_Sede, int Periodo, int Pk_Id_fase, int Pk_Id_tiporecurso);
      
    }
}
