// <copyright file="NivelDeDeficienciaRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz INivelDeDeficienciaRepositorio y repositorio para las
// la gestion de las clases de peligros con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class NivelDeDeficienciaRepositorio : INivelDeDeficienciaRepositorio
    {
        SG_SSTContext db;
        public NivelDeDeficienciaRepositorio()
        {
            db = new SG_SSTContext();
        }

      
       
    
        public List<NivelDeDeficiencia> ConsultarNivelesDeDeficiencia(bool FLAG_Cuantitativa)
        {          
                return db.Tbl_Nivel_De_Deficiencia.Where(ndd => ndd.FLAG_Cuantitativa == FLAG_Cuantitativa).ToList();            
        }


        public int ObtenerValorDeficiencia(int PK_Deficiencia)
        {                        
            NivelDeDeficiencia nivelDeficiencia  = db.Tbl_Nivel_De_Deficiencia.Find(PK_Deficiencia);
            if (nivelDeficiencia != null)
            {
                return nivelDeficiencia.Valor_Deficiencia;
            }
            // se retorna -1 cuando no se encuentra el valor de la deficiencia
            return -1;
        }








    }
}