using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.Models;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Planificacion
{
    public class ObjetivoSSTManager : IObjetivoSST
    {
        public List<EDObjetivoSST> ObtenerObjetivos(int IdEmpresa)
        {
            List<EDObjetivoSST> Objetivos = new List<EDObjetivoSST>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Objetivos = (from o in context.Tbl_Objetivos_SST
                             join e in context.Tbl_Empresa on o.FK_Id_Empresa equals e.Pk_Id_Empresa
                             where e.Pk_Id_Empresa == IdEmpresa
                             select new EDObjetivoSST
                             {
                                 Id_Objetivo_Empresa = o.PK_Id_Objetivo_Empresa,
                                 Meta = o.Meta,
                                 Objetivo = o.Objetivo
                             }).ToList();
            }
            return Objetivos;
        }

        public List<EDObjetivoSST> GuardarObjetivo(EDObjetivoSST Objetivo)
        {
            List<EDObjetivoSST> Objetivos = new List<EDObjetivoSST>();
            using (SG_SSTContext context = new SG_SSTContext())
            {

                ObjetivoSST objetivo = new ObjetivoSST();
                objetivo.FK_Id_Empresa = Objetivo.Id_Empresa;
                objetivo.Meta = Objetivo.Meta;
                objetivo.Objetivo = Objetivo.Objetivo;

                context.Tbl_Objetivos_SST.Add(objetivo);
                context.SaveChanges();

                Objetivos = (from o in context.Tbl_Objetivos_SST
                             join e in context.Tbl_Empresa on o.FK_Id_Empresa equals e.Pk_Id_Empresa
                             where e.Pk_Id_Empresa == Objetivo.Id_Empresa
                             select new EDObjetivoSST
                             {
                                 Id_Objetivo_Empresa = o.PK_Id_Objetivo_Empresa,
                                 Meta = o.Meta,
                                 Objetivo = o.Objetivo
                             }).ToList();
            }
            return Objetivos;
        }

        public List<EDObjetivoSST> EliminarObjetivo(List<EDObjetivoSST> Objetivos)
        {
            List<EDObjetivoSST> ObjetivosResult = new List<EDObjetivoSST>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var obj in Objetivos)
                        {
                            ObjetivoSST objetivo = context.Tbl_Objetivos_SST.Where(o => o.PK_Id_Objetivo_Empresa.Equals(obj.Id_Objetivo_Empresa)).Select(o => o).FirstOrDefault();
                            context.Tbl_Objetivos_SST.Remove(objetivo);
                            context.SaveChanges();
                        }
                        Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        return ObjetivosResult;
                    }
                }
            }

            using (SG_SSTContext context = new SG_SSTContext())
            {
                int id = Objetivos[0].Id_Empresa;
                ObjetivosResult = (from o in context.Tbl_Objetivos_SST
                                   join e in context.Tbl_Empresa on o.FK_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Pk_Id_Empresa == id
                                   select new EDObjetivoSST
                                   {
                                       Id_Objetivo_Empresa = o.PK_Id_Objetivo_Empresa,
                                       Meta = o.Meta,
                                       Objetivo = o.Objetivo
                                   }).ToList();

            }
            return ObjetivosResult;
        }
    }
}
