// <copyright file="ISedeServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>25/01/2017</date>
// <summary>Interfaz que contiene la definicion de los metodos(Servicios) a implementar para las sedes.</summary>

namespace SG_SST.Services.Empresas.IServices
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface ISedeServicios
    {
        /// <summary>
        /// Obtiene o retornar un objeto de tipo de sedepormunicipio buscandolo por el id de la sede
        /// </summary>
        /// <param name="Pk_Sede"> id o clave primaria de sede</param>
        /// <returns>Objeto de tipo sede por municipio</returns>
        SedeMunicipio ObtenerSedePorMunicipio(int Pk_Sede);

        /// <summary>
        /// Metodo queme retorna todas las sedes de la empresa
        /// </summary>
        /// <returns></returns>
        List<Sede> SedesPorEmpresa(int Pk_Id_Empresa);

        /// <summary>
        /// Metodo que me retorna o obtiene una lista de sedespormunicipio
        /// </summary>
        /// <returns></returns>
        List<SedeMunicipio> SedesMunicipioPorEmpresa(int Pk_Id_Empresa);
    }
}
