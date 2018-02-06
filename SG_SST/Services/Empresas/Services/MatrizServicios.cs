// <copyright file="MatrizServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>27/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz MatrizServicios y servicios para las
// la gestion de las matrices de tipo dofa y pest</summary>

namespace SG_SST.Services.Empresas.Services
{
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.Empresas.IRepositories;
    using SG_SST.Repositories.Empresas.Repositories;
    using SG_SST.Services.Empresas.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class MatrizServicios : IMatrizServicios
    {

        IMatrizRepositorio matrizRepositorio;
        public MatrizServicios()
        {
            matrizRepositorio = new MatrizRepositorio();
        }

        internal SG_SST.Services.Empresas.IServices.IMatrizServicios IMatrizServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<TipoAnalisis> ObtenerTiposDeAnalisis()
        {
            return matrizRepositorio.ObtenerTiposDeAnalisis();
        }

        public List<TipoElementoAnalisis> ObtenerTipoElementosPorAnalissis(int Pk_TipoAnalisis)
        {
            return matrizRepositorio.ObtenerTipoElementosPorAnalissis(Pk_TipoAnalisis);
        }

        public ElementoMatriz AgregarElementoMatriz(ElementoMatriz elementoMatriz,int Pk_Id_Empresa)
        {
            return matrizRepositorio.AgregarElementoMatriz(elementoMatriz,Pk_Id_Empresa);

        }

        public bool EliminarElementoMatriz(int Pk_ElementoMatriz)
        {
            return matrizRepositorio.EliminarElementoMatriz(Pk_ElementoMatriz);
        }

        public List<ElementoMatriz> ObtenerElementosMatriz(int PkTipoAnalisis,string nit)
        {
            return matrizRepositorio.ObtenerElementosMatriz(PkTipoAnalisis,nit);
        }


        public string ObtenerElementoDofa(int Pk_elementoMatriz)
        {

            return matrizRepositorio.ObtenerElementoDofa(Pk_elementoMatriz);

        }


        public ElementoMatriz GrabarElementoMatrizEdicion(ElementoMatriz elementoMatriz)
        {

            return matrizRepositorio.GrabarElementoMatrizEdicion(elementoMatriz);

        }



    }
}