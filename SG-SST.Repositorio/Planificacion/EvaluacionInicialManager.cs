using SG_SST.Interfaces.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models.Planificacion;
using SG_SST.Models;

namespace SG_SST.Repositorio.Planificacion
{
    public class EvaluacionInicialManager : IEvaluacionInicial
    {
        /// <summary>
        /// Consulta la empresa que se está evaluado.
        /// Se actualizan los datos de la empresa que se está evaluando.
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        public EDEmpresaEvaluar ConsultaEmpresaEvaluar(EDEmpresaEvaluar Empresa)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                EDEmpresaEvaluar emp = new EDEmpresaEvaluar();

                var empActualizar = (from eval in context.Tbl_Empresas_Evaluar
                                     join em in context.Tbl_Empresa on eval.Fk_Id_Empresa equals em.Pk_Id_Empresa
                                     where em.Nit_Empresa.Trim().Equals(Empresa.Nit.Trim())
                                     select eval).FirstOrDefault();
                if (empActualizar != null) {
                    empActualizar.Num_Licencia_SOSL = Empresa.NumLicenciaSOSL;
                    empActualizar.Responsable_SGSST = Empresa.ResponsableSGSST;
                    empActualizar.Fecha_Diligencia_Eval_Inicial = Empresa.FechaDiligencia;
                    empActualizar.Elaborado_Por = Empresa.ElaboradoPor;
                    empActualizar.CodSede = Empresa.CodSede;
                    context.SaveChanges();

                    var p = (from eval in context.Tbl_Empresas_Evaluar
                             join em in context.Tbl_Empresa on eval.Fk_Id_Empresa equals em.Pk_Id_Empresa
                             where em.Nit_Empresa.Trim().Equals(Empresa.Nit.Trim())
                             select new
                             {
                                 Pk_Id_Empresa_Evaluar = eval.Pk_Id_Empresa_Evaluar,
                                 Razon_Social = em.Razon_Social,
                                 Nit = em.Nit_Empresa,
                                 Num_Licencia_SOSL = eval.Num_Licencia_SOSL,
                                 Responsable_SGSST = eval.Responsable_SGSST,
                                 Fecha_Diligencia = eval.Fecha_Diligencia_Eval_Inicial,
                                 Elaborado_Por = eval.Elaborado_Por,
                                 Cod_Actividad_Economica = em.Codigo_Actividad,
                                 Cod_Sede = eval.CodSede
                             }).FirstOrDefault();

                    Empresa.IdEmpresaEvaluar = p.Pk_Id_Empresa_Evaluar;
                    Empresa.RazonSocial = p.Razon_Social;
                    Empresa.Nit = p.Nit;
                    Empresa.NumLicenciaSOSL = p.Num_Licencia_SOSL;
                    Empresa.ResponsableSGSST = p.Responsable_SGSST;
                    Empresa.FechaDiligencia = p.Fecha_Diligencia.Value;
                    Empresa.ElaboradoPor = p.Elaborado_Por;
                    Empresa.CodActividadEconomica = p.Cod_Actividad_Economica;
                    Empresa.CodSede = p.Cod_Sede;
                }
            }
            return Empresa;
        }

        public bool EliminarAspectosEvalEmpresa(EDEmpresaEvaluar Empresa)
        {
            bool result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<Evaluacion_Inicial_Aspecto> EvalEmpresa = (from e in context.Tbl_Empresas_Evaluar
                                                                        join ea in context.Tbl_Empresa_Aspectos on e.Pk_Id_Empresa_Evaluar equals ea.Fk_Id_Empresa_Evaluar
                                                                        join ei in context.Tbl_Evaluacion_Inicial_Aspectos on ea.Pk_Id_Empresa_Aspecto equals ei.Fk_Id_Empresa_Aspecto
                                                                        where e.Pk_Id_Empresa_Evaluar == Empresa.IdEmpresaEvaluar
                                                                        select ei).ToList();

                        if (EvalEmpresa.Count > 0)
                        {
                            foreach (Evaluacion_Inicial_Aspecto item in EvalEmpresa)
                            {
                                context.Tbl_Evaluacion_Inicial_Aspectos.Remove(item);
                            }
                        }

                        context.SaveChanges();

                        List<Empresa_Aspecto> EmpresaAspectos = (from e in context.Tbl_Empresas_Evaluar
                                                                 join ea in context.Tbl_Empresa_Aspectos on e.Pk_Id_Empresa_Evaluar equals ea.Fk_Id_Empresa_Evaluar
                                                                 where e.Pk_Id_Empresa_Evaluar == Empresa.IdEmpresaEvaluar
                                                                 select ea).ToList();

                        if (EmpresaAspectos.Count > 0)
                        {
                            foreach (Empresa_Aspecto item in EmpresaAspectos)
                            {
                                context.Tbl_Empresa_Aspectos.Remove(item);
                                context.Tbl_Aspectos.Remove(context.Tbl_Aspectos.Where(a => a.Pk_Id_Aspecto == item.Fk_Id_Aspecto).Select(a => a).FirstOrDefault());
                            }
                        }
                        context.SaveChanges();
                        Transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }
        public EDEmpresaEvaluar CrearEmpresaEvaluar(EDEmpresaEvaluar Empresa)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var empresa = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Trim().Equals(Empresa.Nit.Trim())).Select(e => e).FirstOrDefault();

                        Empresa_Evaluar empresaEval = new Empresa_Evaluar
                        {
                            //Nit = Empresa.Nit,
                            //Razon_Social = Empresa.RazonSocial,
                            Fk_Id_Empresa = empresa.Pk_Id_Empresa,
                            Responsable_SGSST = Empresa.ResponsableSGSST,
                            Elaborado_Por = Empresa.ElaboradoPor,
                            Fecha_Diligencia_Eval_Inicial = Empresa.FechaDiligencia,
                            //Cod_Actividad_Economica = Empresa.CodActividadEconomica,
                            CodSede = Empresa.CodSede,
                            Num_Licencia_SOSL = Empresa.NumLicenciaSOSL
                        };
                        context.Tbl_Empresas_Evaluar.Add(empresaEval);
                        context.SaveChanges();
                        Transaction.Commit();
                        Empresa.IdEmpresaEvaluar = empresaEval.Pk_Id_Empresa_Evaluar;
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        Empresa = null;
                    }
                }
                return Empresa;
            }
        }
        public EDAspecto CrearAspecto(EDAspecto Aspecto)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Aspecto asp = new Aspecto
                        {
                            Descripcion = Aspecto.Aspecto,
                            Observacion = Aspecto.Observacion,
                            Fk_Id_Valoracion_Aspecto = Aspecto.IdValorizacion
                        };

                        context.Tbl_Aspectos.Add(asp);
                        context.SaveChanges();
                        Transaction.Commit();
                        Aspecto.IdAspecto = asp.Pk_Id_Aspecto;
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        Aspecto = null;
                    }
                }
            }
            return Aspecto;
        }
        public int CrearEmpresaAspecto(int IdEmpresaEvaluar, int IdAspecto)
        {
            int idEmpresaAspecto = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Empresa_Aspecto emasp = new Empresa_Aspecto
                        {
                            Fk_Id_Aspecto = IdAspecto,
                            Fk_Id_Empresa_Evaluar = IdEmpresaEvaluar
                        };

                        context.Tbl_Empresa_Aspectos.Add(emasp);
                        context.SaveChanges();
                        Transaction.Commit();
                        idEmpresaAspecto = emasp.Pk_Id_Empresa_Aspecto;
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                    }
                }
            }
            return idEmpresaAspecto;
        }
        public bool CrearCalificacionInicialEmpresa(int IdEmpresaAspecto, decimal valor)
        {
            bool result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Evaluacion_Inicial_Aspecto eval = new Evaluacion_Inicial_Aspecto
                        {
                            Fk_Id_Empresa_Aspecto = IdEmpresaAspecto,
                            Valor_Valoracion = valor
                        };

                        context.Tbl_Evaluacion_Inicial_Aspectos.Add(eval);
                        context.SaveChanges();
                        Transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }
        public bool EliminarAspecto(EDAspecto Aspecto)
        {
            bool result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Aspecto ap = new Aspecto
                {
                    Pk_Id_Aspecto = Aspecto.IdAspecto,
                    Descripcion = Aspecto.Aspecto,
                    Observacion = Aspecto.Observacion,
                    Fk_Id_Valoracion_Aspecto = Aspecto.IdValorizacion
                };
                context.Tbl_Aspectos.Remove(ap);
                context.SaveChanges();
                result = true;
            }
            return result;
        }
        public decimal ObtenerResultadoEvalInivical(EDEmpresaEvaluar Empresa)
        {
            decimal Calificacion = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                List<Evaluacion_Inicial_Aspecto> EvalEmpresa = (from e in context.Tbl_Empresas_Evaluar
                                                                join ea in context.Tbl_Empresa_Aspectos on e.Pk_Id_Empresa_Evaluar equals ea.Fk_Id_Empresa_Evaluar
                                                                join ei in context.Tbl_Evaluacion_Inicial_Aspectos on ea.Pk_Id_Empresa_Aspecto equals ei.Fk_Id_Empresa_Aspecto
                                                                where e.Pk_Id_Empresa_Evaluar == Empresa.IdEmpresaEvaluar
                                                                select ei).ToList();
                Calificacion = EvalEmpresa.AsEnumerable().Sum(ev => ev.Valor_Valoracion);
            }
            return Calificacion;
        }

        public List<EDAspecto> ConsultarAspectosPorEmpresa(EDEmpresaEvaluar Empresa)
        {
            List<EDAspecto> AspectosEmpresa = new List<EDAspecto>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                AspectosEmpresa = (from ea in context.Tbl_Empresa_Aspectos
                                   join a in context.Tbl_Aspectos on ea.Fk_Id_Aspecto equals a.Pk_Id_Aspecto
                                   where ea.Fk_Id_Empresa_Evaluar == Empresa.IdEmpresaEvaluar
                                   select new EDAspecto
                                   {
                                       IdAspecto = a.Pk_Id_Aspecto,
                                       Aspecto = a.Descripcion,
                                       Observacion = a.Observacion,
                                       IdValorizacion = a.Fk_Id_Valoracion_Aspecto

                                   }).ToList();
            }
            return AspectosEmpresa;
        }


        public List<EDAspecto> ObtenerAspectosBase()
        {
            List<EDAspecto> AspectosBase = new List<EDAspecto>(); 
            using (SG_SSTContext context = new SG_SSTContext ())
            {
                AspectosBase = context.Tbl_Aspecto_Base.Select(ab => new EDAspecto
                    {
                        IdAspecto = ab.PK_Id_Aspecto_Base,
                        Aspecto = ab.AspectoBase 
                    }).ToList ();
            }

            return AspectosBase;
        }
    }
}
