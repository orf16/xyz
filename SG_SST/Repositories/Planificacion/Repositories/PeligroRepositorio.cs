// <copyright file="PeligroRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>12/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IPeligroRepositorio y repositorio para las
// la gestion de los peligros con la base de datos</summary>

namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.Entity;
    using SG_SST.Dtos.Planificacion;
    using SG_SST.Models.Empresas;
   // using SG_SST.Utilidades.Traza;

    public class PeligroRepositorio : IPeligroRepositorio
    {

        SG_SSTContext db;

        public PeligroRepositorio()
        {
            db = new SG_SSTContext();
        }

        public PeligroRepositorio(SG_SSTContext db)
        {
            this.db = db;
        }

    


        public List<Peligro> ObtenerPeligros(int IdSede, int IdMetodologia)
        {

            List<Peligro> peligros = db.Tbl_Peligro.Include(p => p.GTC45)
                                                   .Include(p => p.PersonaExpuestas.Select(pe => pe.INSHT))
                                                   .Include(p => p.PersonaExpuestas.Select(pe => pe.RAM.Select(exr => exr.EstimacionDeRiesgosPorRAM)))
                                                   .Include(p => p.PersonaExpuestas.Select(pe => pe.ProbabilidadesPorPersonasExpuestas))
                                                   .Include(p => p.ConsecuenciasPorPeligros)
                                                   .Where(p => p.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Grupo.FK_Metodologia == IdMetodologia && p.FK_Sede == IdSede).ToList();


            return peligros;

        }

        public List<PeligrosPorSede> ObtenerMatrizPorSedeEmpresa(int Pk_Id_Empresa)
        {
          
            Empresa empresa = db.Tbl_Empresa.Find(Pk_Id_Empresa);
            List<PeligrosPorSede> peligros = new List<PeligrosPorSede>();
            if (empresa != null)
            {
                int pkEmpresa = empresa.Pk_Id_Empresa;
                List<Peligro> peligrosDeSedes = db.Tbl_Peligro.Include(p => p.ConsecuenciasPorPeligros).Where(p => p.Sede.Fk_Id_Empresa == pkEmpresa).ToList();
                if (peligrosDeSedes.Count() > 0)
                {
                    List<PeligrosPorSede> peligrosSede =
                               peligrosDeSedes.Select(peligro =>
                                   new PeligrosPorSede(peligro.FK_Sede,
                                                       peligro.Sede.Nombre_Sede,
                                                       peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Grupo.FK_Metodologia,
                                                       peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Grupo.Metodologia.Descripcion_Metodologia)).ToList();

                    foreach (PeligrosPorSede pxs in peligrosSede)
                    {
                        if (!peligros.Exists(p => p.IdSede == pxs.IdSede && p.IdMetodologia == pxs.IdMetodologia))
                        {
                            peligros.Add(pxs);
                        }
                    }

                }

            }
            return peligros;
        }


        public bool GuardarPeligro(Peligro peligro)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (peligro.PK_Peligro > 0)
                    {
                        ModificarPeligro(peligro);
                    }
                    else
                    {
                        db.Tbl_Peligro.Add(peligro);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //RegistroInformacion.EnviarError<PeligroRepositorio>(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private void ModificarPeligro(Peligro peligro)
        {
            if (peligro.ConsecuenciasPorPeligros != null && peligro.ConsecuenciasPorPeligros.Count() > 0)
            {
                foreach (ConsecuenciaPorPeligro cp in peligro.ConsecuenciasPorPeligros)
                {
                    db.Entry(cp).State = EntityState.Modified;
                }
            }
            if (peligro.GTC45 != null && peligro.GTC45.Count() > 0)
            {
                foreach (GTC45 gtc in peligro.GTC45)
                {
                    db.Entry(gtc).State = EntityState.Modified;
                }
            }
            if (peligro.PersonaExpuestas != null && peligro.PersonaExpuestas.Count() > 0)
            {
                foreach (PersonaExpuesta pe in peligro.PersonaExpuestas)
                {
                    if (pe.INSHT != null && pe.INSHT.Count() > 0)
                    {
                        foreach (INSHT insht in pe.INSHT)
                        {
                            db.Entry(insht).State = EntityState.Modified;
                        }
                    }
                    if (pe.ProbabilidadesPorPersonasExpuestas != null && pe.ProbabilidadesPorPersonasExpuestas.Count() > 0)
                    {
                        foreach (ProbabilidadPorPersonaExpuesta pppe in pe.ProbabilidadesPorPersonasExpuestas)
                        {
                            db.Entry(pppe).State = EntityState.Modified;
                        }
                    }
                    if (pe.RAM != null && pe.RAM.Count() > 0)
                    {
                        foreach (RAM ram in pe.RAM)
                        {
                            if (ram.EstimacionDeRiesgosPorRAM != null && ram.EstimacionDeRiesgosPorRAM.Count() > 0)
                            {
                                foreach (EstimacionDeRiesgoPorRAM edrpr in ram.EstimacionDeRiesgosPorRAM)
                                {
                                    db.Entry(edrpr).State = EntityState.Modified;
                                }
                            }
                            db.Entry(ram).State = EntityState.Modified;
                        }
                    }
                    db.Entry(pe).State = EntityState.Modified;
                }
            }
            db.Entry(peligro).State = EntityState.Modified;
        }


        public bool EliminarPeligros(int IdSede, int IdMetodologia)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<Peligro> peligros = ObtenerPeligros(IdSede, IdMetodologia);
                    db.Tbl_Peligro.RemoveRange(peligros);

                    db.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {                   
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Peligro ObtenerPeligro(int Pk_Peligro)
        {
            return db.Tbl_Peligro.Include(p => p.GTC45)
                                .Include(p => p.PersonaExpuestas.Select(pe => pe.INSHT))
                                .Include(p => p.PersonaExpuestas.Select(pe => pe.RAM.Select(exr => exr.EstimacionDeRiesgosPorRAM)))
                                .Include(p => p.PersonaExpuestas.Select(pe => pe.ProbabilidadesPorPersonasExpuestas))
                                .Include(p => p.ConsecuenciasPorPeligros).First(p => p.PK_Peligro == Pk_Peligro);
        }

        public bool EliminarPeligro(int Pk_Peligro)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Tbl_Peligro.Remove(ObtenerPeligro(Pk_Peligro));
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {                    
                    transaction.Rollback();
                    return false;
                }
            }
        }

    }
}