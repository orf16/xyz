// <copyright file="Recursos.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>20/01/2017</date>
// <summary>Clase  que contiene la implementacion de la interfazIRecursos  para la realizar las operaciones con la base datos
// para las consultas a nivel general de la aplicacion y que no pertencen a un modulo  en especifico.</summary>

namespace SG_SST.Repositories.General.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.General.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml.Serialization;
    public class RecursosRepositorio : IRecursosRepositorio
    {
        SG_SSTContext db;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RecursosRepositorio()
        {
            db = new SG_SSTContext();
        }

        internal SG_SST.Repositories.General.IRepositories.IRecursosRepositorio IRecursosRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Municipio> ObtenetMunicipios(int PK_Departamento)
        {
            return db.Tbl_Municipio.Where(m => m.Fk_Nombre_Departamento == PK_Departamento).ToList();
        }

        public List<Departamento> ObtenerDepartamentos()
        {
            return db.Tbl_Departamento.ToList();
        }

        public MemoryStream ExportarAExcel<Exportar>(List<Exportar> datosAExportar)
        {
            var stream = new MemoryStream(); 
            var tipo  = datosAExportar.GetType();
            var serialicer = new XmlSerializer(datosAExportar.GetType()); 
            //Lo transformo en un XML y lo guardo en memoria
            serialicer.Serialize(stream, datosAExportar);
            stream.Position =(0);           
            return stream;          
        }



        public List<SelectListItem> ObtenerPeriodosAnios(int anioInicial, int anioFinal)
        {
            List<SelectListItem> Periodo = new List<SelectListItem>();
            for (int i = anioInicial; i <= anioFinal; i++)
            {
                Periodo.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return Periodo;
        }

    }
}