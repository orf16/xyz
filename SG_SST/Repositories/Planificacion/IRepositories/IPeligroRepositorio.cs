// <copyright file="IPeligroRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>21/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de los peligros.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using SG_SST.Dtos.Planificacion;
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IPeligroRepositorio
    {
        /// <summary>
        /// Definincion del metodo o repositorio que me retonar una lista de objetos de tipo Peligro
        /// buscandolos por el id de la sede y el id del tipo de metodologia.
        /// </summary>
        /// <param name="IdSede">pk o id de la sede.</param>
        /// <param name="IdMetodologia">pk o id del tipo de sede.</param>
        /// <returns></returns>
        List<Peligro> ObtenerPeligros(int IdSede, int IdMetodologia);

        /// <summary>
        /// Definicion del metodo que me retorna las sedes que tienes matriz creadas por empresa
        /// </summary>
        /// <returns></returns>
        List<PeligrosPorSede> ObtenerMatrizPorSedeEmpresa(int Pk_Id_Empresa);

        /// <summary>
        /// Definicion del Metodo que me graba un peligro y me retorna verdadero si fue posible grabar
        /// el peligro o falso sino fue posible
        /// </summary>
        /// <param name="peligro">Peligro a grabarle a la sede </param>
        /// <returns>retorna si fue exitosa o no el guardado del peligro</returns>
        bool GuardarPeligro(Peligro peligro);
      

        /// <summary>
        /// Definicion del Metodo que me Elimina los peligros  y me retorna verdadero si fue posible elimina
        /// el peligro o falso sino fue posible
        /// </summary>
        /// <param name="IdSede">pk o id de la sede.</param>
        /// <param name="IdMetodologia">pk o id del tipo de sede.</param>
        /// <returns>retorna si fue exitosa o no el Eliminado de los peligros de la sede y el tipo de metodologia</returns>
        bool EliminarPeligros(int IdSede, int IdMetodologia);

        /// <summary>
        /// Definicion del metodo que me retorna un peligro buscandolo por su id o clave primaria
        /// </summary>
        /// <param name="Pk_Peligro">id o clave primaria</param>
        /// <returns>Peligro</returns>
        Peligro ObtenerPeligro(int Pk_Peligro);

        /// <summary>
        /// Definicion del Metodo que me Elimina  un peligro y me retorna verdadero si fue posible eliminar
        /// el peligro o falso sino fue posible
        /// </summary>
        /// <param name="peligro">Peligro a eliminar</param>
        /// <returns>retorna si fue exitosa o no la Eliminada del Peligro</returns>
        bool EliminarPeligro(int Pk_Peligro);
    }
}
