// <copyright file="SedeRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>25/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz ISedeRepositorio y repositorio para las
// la gestion de las sedes con la base de datos</summary>

namespace SG_SST.Repositorio.Empresas
{
    using SG_SST.EntidadesDominio.Empresas;
    using SG_SST.Interfaces.Empresas;
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class SedeRepositorioManager : ISedeRepositorio
    {       

        public EDMunicipio ObtenerSedePorMunicipio(int Pk_Sede)
        {
            EDMunicipio sedesMunicipio = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                sedesMunicipio = (from m in contex.Tbl_Municipio
                                  join sdm in contex.Tbl_SedeMunicipio on m.Pk_Id_Municipio equals sdm.Fk_Id_Municipio
                                  join sd in contex.Tbl_Sede on sdm.Fk_id_Sede equals sd.Pk_Id_Sede
                                  where sd.Pk_Id_Sede == Pk_Sede
                                  select new EDMunicipio
                                  {
                                      IdMunicipio = m.Pk_Id_Municipio,
                                      NombreMunicipio = m.Nombre_Municipio,
                                      Sede = (new EDSede
                                      {
                                          IdSede = sd.Pk_Id_Sede,
                                          NombreSede = sd.Nombre_Sede,
                                          DireccionSede = sd.Direccion_Sede
                                      })
                                  }).FirstOrDefault();
            }

            return sedesMunicipio;             
 
           
        }

        public List<EDSede> SedesPorEmpresa(int idEmpresa) 
        {
            List<EDSede> Sedes = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                Sedes = (from sd in contex.Tbl_Sede
                         where sd.Fk_Id_Empresa == idEmpresa
                         select new EDSede
                                      {
                                          IdSede = sd.Pk_Id_Sede,
                                          NombreSede = sd.Nombre_Sede,
                                          DireccionSede = sd.Direccion_Sede
                                      }).ToList();                
            }

            return Sedes;        
        }


        public List<EDSedeMunicipio> SedesMunicipioPorEmpresa(int idEmpresa)
        {
            List<EDSedeMunicipio> SedesMunicipio = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                SedesMunicipio = (from m in contex.Tbl_Municipio
                                  join sdm in contex.Tbl_SedeMunicipio on m.Pk_Id_Municipio equals sdm.Fk_Id_Municipio
                                  join sd in contex.Tbl_Sede on sdm.Fk_id_Sede equals sd.Pk_Id_Sede
                                  where sd.Fk_Id_Empresa == idEmpresa
                                  select new EDSedeMunicipio
                                  {
                                      Municipio = (new EDMunicipio
                                      {
                                          IdMunicipio = m.Pk_Id_Municipio,
                                          NombreMunicipio = m.Nombre_Municipio,
                                      }),
                                      Sede = (new EDSede
                                      {
                                          IdSede = sd.Pk_Id_Sede,
                                          NombreSede = sd.Nombre_Sede,
                                          DireccionSede = sd.Direccion_Sede
                                      })
                                  }).ToList();                     
            }

            return SedesMunicipio;
        }

        
    }
}