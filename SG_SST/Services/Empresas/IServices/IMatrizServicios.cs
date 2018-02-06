// <copyright file="IMatrizServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>27/01/2017</date>
// <summary>Interfaz que contiene la definicion de los metodos(Servicios) a implementar para el analisis DOFA Y PEST.</summary>

namespace SG_SST.Services.Empresas.IServices
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IMatrizServicios
    {
        /// <summary>
        /// Retorna los tipos de analisis para este caso me retorna analisis DOFA y PEST
        /// </summary>
        /// <returns></returns>
        List<TipoAnalisis> ObtenerTiposDeAnalisis();

        /// <summary>
        /// Obtiene o retorna los tipo de elementos que perteneces a un tipo de analisis 
        /// </summary>
        /// <param name="Pk_TipoAnalisis">clave primaria del tipo de analisis </param>
        /// <returns>lista de elementos del analisis</returns>
        List<TipoElementoAnalisis> ObtenerTipoElementosPorAnalissis(int Pk_TipoAnalisis);

        /// <summary>
        /// Agrega y retorna el elemento guardado de la matriz
        /// </summary>
        /// <param name="elementoMatriz">elemento a agregar a la matriz</param>
        /// <returns></returns>
        ElementoMatriz AgregarElementoMatriz(ElementoMatriz elementoMatriz,int Pk_Id_Empresa);

        /// <summary>
        /// Metodo que elimina un elemento de la matriz 
        /// </summary>
        /// <param name="Pk_ElementoMatriz"> Clave primaria del elemento de la matriz </param>
        /// <returns></returns>
        bool EliminarElementoMatriz(int Pk_ElementoMatriz);

        /// <summary>
        /// Metodo que me retorna todos los elementos de una matriz por empresa
        /// </summary>
        /// <param name="PkTipoAnalisis"> Clave primaria del tipo de analisis </param>
        /// <returns>lista de elementos</returns>
        List<ElementoMatriz> ObtenerElementosMatriz(int PkTipoAnalisis,string nit);



        string ObtenerElementoDofa(int Pk_elementoMatriz);

        ElementoMatriz GrabarElementoMatrizEdicion(ElementoMatriz elementoMatriz);





    }
}
