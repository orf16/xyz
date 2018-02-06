// <copyright file="ISedeRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>25/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
//del objeto de tipo sede.</summary>

namespace SG_SST.Interfaces.Empresas
{
    using SG_SST.EntidadesDominio.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface ISedeRepositorio
    {
        /// <summary>
        /// Obtiene o retornar un objeto de tipo de sedepormunicipio buscandolo por el id de la sede
        /// </summary>
        /// <param name="Pk_Sede"> id o clave primaria de sede</param>
        /// <returns>Objeto de tipo sede por municipio</returns>
        EDMunicipio ObtenerSedePorMunicipio(int Pk_Sede);

        /// <summary>
        /// Metodo queme retorna todas las sedes de la empresa
        /// </summary>
        /// <returns>lista de sedes</returns>
        List<EDSede> SedesPorEmpresa(int idEmpresa);

        /// <summary>
        /// Metodo que me retorna o obtiene una lista de sedespormunicipio
        /// </summary>
        /// <returns></returns>
        List<EDSedeMunicipio> SedesMunicipioPorEmpresa(int idEmpresa);
    }
}
