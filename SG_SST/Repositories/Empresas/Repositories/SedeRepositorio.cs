// <copyright file="SedeRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>25/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz ISedeRepositorio y repositorio para las
// la gestion de las sedes con la base de datos</summary>

namespace SG_SST.Repositories.Empresas.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.Empresas.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SG_SST.Controllers.Base;

    public class SedeRepositorio : ISedeRepositorio
    {
        SG_SSTContext db;

        public SedeRepositorio()
        {
            db = new SG_SSTContext();
        }

        public SedeRepositorio(SG_SSTContext db)
        {
            this.db = db;
        }

    


        public SedeMunicipio ObtenerSedePorMunicipio(int Pk_Sede)
        {

            List<SedeMunicipio> sedesMunicipios = db.Tbl_SedeMunicipio.Where(sd => sd.Fk_id_Sede == Pk_Sede).ToList();
            if (sedesMunicipios.Count() > 0)
            {
                return sedesMunicipios.FirstOrDefault();
            }
            return null;
        }

        public List<Sede> SedesPorEmpresa(int Pk_Id_Empresa)
        {
            return db.Tbl_Sede.Where(s => s.Fk_Id_Empresa == Pk_Id_Empresa).ToList();
        
        }


        public List<SedeMunicipio> SedesMunicipioPorEmpresa(int Pk_Id_Empresa)
        {
            return db.Tbl_SedeMunicipio.Where(s => s.Sede.Fk_Id_Empresa == Pk_Id_Empresa).ToList();

        }
    }
}