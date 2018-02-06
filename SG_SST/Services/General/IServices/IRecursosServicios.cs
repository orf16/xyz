// <copyright file="IRecursosServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>20/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los servicios o logica
//a nivel general de la aplicacion y que no pertencen a un modulo  en especifico.</summary>

namespace SG_SST.Services.General.IServices
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    interface IRecursosServicios
    {
        /// <summary>
        /// Defincion del metodo  para obtener los municipios de un departamento
        /// consultado por el id o pk del departamento
        /// </summary>
        /// <param name="PK_Departamento">clave primaria de Departamento</param>
        /// <returns>LIsta de obtjetos de tipo de departamento</returns>
        List<Municipio> ObtenetMunicipios(int PK_Departamento);

        /// <summary>
        /// Defincion del metodo o repositorio para obtener los departamentos       
        /// </summary>    
        /// <returns>LIsta de obtjetos de tipo de departamento</returns>
        List<Departamento> ObtenerDepartamentos();

        /// <summary>
        /// Definicion del metodo que me retorna un stream para exportar a un tipo de archivo
        /// </summary>
        /// <typeparam name="Exportar">cualquier tipo de objeto que se quiere exportar
        /// se debe tener en cuenta que el objeto debe contar con un contructor si parametros</typeparam>
        /// <param name="datosAExportar"> datos a exportar </param>
        /// <returns>stream a escribir en el documento</returns>
        MemoryStream ExportarAExcel<Exportar>(List<Exportar> datosAExportar);


        /// <summary>
        /// Definicion del metodo que me retorna una lista de opciones de los años
        /// </summary>
        /// <param name="anioInicial">año inicial</param>
        /// <param name="anioFinal">año final</param>
        /// <returns></returns>
        List<SelectListItem> ObtenerPeriodosAnios(int anioInicial, int anioFinal);

    }
}
