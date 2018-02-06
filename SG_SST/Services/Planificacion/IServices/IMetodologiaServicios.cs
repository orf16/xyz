// <copyright file="IMetodologiaServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>21/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos(Servicios) a implementar para la gestion del las metodologias.</summary>

namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Dtos.Planificacion;
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IMetodologiaServicios
    {
        /// <summary>
        /// definincion del metodo o servicio que me retonar una lista de objetos de tipo matriz GTC45
        /// buscandolos por el id de la sede y el id del tipo de metodologia
        /// </summary>
        /// <param name="IdSede">pk o id de la sede</param>
        /// <param name="IdMetodologia">pk o id del tipo de sede</param>
        /// <returns></returns>
        List<MatrizGTC45> ObtenerMatrizGTC45(int IdSede, int IdMetodologia);

        /// <summary>
        /// Definicion del metodo que me retorna las sedes que tienes matriz creadas por empresa
        /// </summary>
        /// <returns></returns>
        List<PeligrosPorSede> ObtenerMatrizPorSedeEmpresa(int Pk_Id_Empresa);

        /// <summary>
        /// definincion del metodo o servicio que me retonar una lista de objetos de tipo matriz INSTH
        /// buscandolos por el id de la sede y el id del tipo de metodologia
        /// </summary>
        /// <param name="IdSede">pk o id de la sede</param>
        /// <param name="IdMetodologia">pk o id del tipo de sede</param>
        List<MatrizINSTH> ObtenerMatrizINSTH(int IdSede, int IdMetodologia);

        /// <summary>
        /// definincion del metodo o servicio que me retonar una lista de objetos de tipo matriz RAM
        /// buscandolos por el id de la sede y el id del tipo de metodologia
        /// </summary>
        /// <param name="IdSede">pk o id de la sede</param>
        /// <param name="IdMetodologia">pk o id del tipo de sede</param>
        List<MatrizRAM> ObtenerMatrizRAM(int IdSede, int IdMetodologia);

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
        /// Definicion del Metodo que me Elimina  un peligro y me retorna verdadero si fue posible eliminar
        /// el peligro o falso sino fue posible
        /// </summary>
        /// <param name="peligro">Peligro a eliminar</param>
        /// <returns>retorna si fue exitosa o no la Eliminada del Peligro</returns>
        bool EliminarPeligro(int Pk_Peligro);

        /// <summary>
        /// Definicion del metodo que me retorna un objeto del tipo de matrizeditgtc45
        /// </summary>
        /// <param name="PK_Peligro">id o clave primaria del peligro a editar</param>
        /// <returns>obejto de tipo matrizeditgtc45</returns>
        MatrizEditGTC45 ObtenerMatrizEditGTC45(int PK_Peligro);


        /// <summary>
        /// Definicion del metodo que me retorna un objeto del tipo de MatrizEditINSTH
        /// </summary>
        /// <param name="PK_Peligro">id o clave primaria del peligro a editar</param>
        /// <returns>obejto de tipo matrizeditINSTH</returns>
        MatrizEditINSTH ObtenerMatrizEditINSTH(int PK_Peligro);

        /// <summary>
        /// Definicion del metodo que me retorna un objeto del tipo de MatrizEditRAM
        /// </summary>
        /// <param name="PK_Peligro">id o clave primaria del peligro a editar</param>
        /// <returns>obejto de tipo matrizeditRAM</returns>
        MatrizEditRAM ObtenerMatrizEditRAM(int PK_Peligro);
    }
}
