using SG_SST.Interfaces.Planificacion;
using System.Collections.Generic;
using System.Linq;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models.Planificacion;
using System;
using SG_SST.Enumeraciones;
using SG_SST.Models;


namespace SG_SST.Repositorio.Planificacion
{
    public class EvaluacionStandMinimosManager : IEvaluacionEstandMinimos
    {
        /// <summary>
        /// Guarda los datos de la evaluación estándares mínimos.
        /// Se debe tener en cuenta que debe existir. Tambien tener en cuenta
        /// que la empresa debe existir previamente para poder ser evaluada
        /// </summary>
        /// <param name="EvaluacionEstandar"></param>
        /// <returns></returns>
        public EDEvaluacionEstandarMinimo GuardarEvaluacionEstandar(EDEvaluacionEstandarMinimo EvaluacionEstandar)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var empr = (from eval in context.Tbl_Empresas_Evaluar
                            join emp in context.Tbl_Empresa on eval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where emp.Nit_Empresa.Trim().Equals(EvaluacionEstandar.Nit.Trim())
                            select eval).FirstOrDefault();
                if (empr == null)
                {
                    var empresa = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Trim().Equals(EvaluacionEstandar.Nit.Trim())).Select(e => e).FirstOrDefault();
                    if (empresa != null)
                    {
                        var empEval = new Empresa_Evaluar();
                        empEval.Fk_Id_Empresa = empresa.Pk_Id_Empresa;
                        empEval.Fecha_Diligencia_Eval_EstMin = DateTime.Now;
                        context.Tbl_Empresas_Evaluar.Add(empEval);
                        empr = empEval;
                        context.SaveChanges();
                    }
                }

                EvaluacionEstandar.IdEmpresaEvaluar = empr.Pk_Id_Empresa_Evaluar;
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int codjustifica = 0;
                        Config_Valoracion_SubEstand Configuracion = null;
                        //Obtenemos el id del subestandar que se esta evaluando de acuerdo al criterio
                        var idSubEStandar = context.Tbl_Criterios.Where(c => c.Pk_Id_Criterio == EvaluacionEstandar.IdCriterio).Select(c => c.Fk_Id_SubEstandar).FirstOrDefault();

                        //Se verifica si la valoracion es no aplica, si as asi se debe verificar si el no aplica viene con justificacion
                        if (EvaluacionEstandar.IdValoracionCriterio == (int)EnumPlanificacion.ValoracionStandares.NoAplica)
                        {
                            if (string.IsNullOrEmpty(EvaluacionEstandar.Justificacion))
                                codjustifica = (int)EnumPlanificacion.ValoracionStandares.NoJustifica;
                            else
                                codjustifica = (int)EnumPlanificacion.ValoracionStandares.Justifica;
                        }

                        //Obtenemos el id de la configuracion que tiene el valor de calificacion
                        if (codjustifica > 0)
                            Configuracion = context.Tbl_Config_Valoracion_SubEstandares.Where(cf => cf.Fk_Id_SubEstandar == idSubEStandar
                                                                                               && cf.Fk_Id_Valoracion_Criterio == EvaluacionEstandar.IdValoracionCriterio
                                                                                               && cf.Id_Dpendiente == codjustifica)
                                                                                          .Select(cf => cf).FirstOrDefault();
                        else
                            Configuracion = context.Tbl_Config_Valoracion_SubEstandares.Where(cf => cf.Fk_Id_SubEstandar == idSubEStandar
                                                                                               && cf.Fk_Id_Valoracion_Criterio == EvaluacionEstandar.IdValoracionCriterio)
                                                                                          .Select(cf => cf).FirstOrDefault();
                        if (Configuracion.Pk_Id_Config_Valoracion_SubEstand > 0)
                        {
                            decimal valorCriterio = context.Tbl_Criterios.Where(cr => cr.Pk_Id_Criterio == EvaluacionEstandar.IdCriterio).Select(cr => cr.Valor).FirstOrDefault();
                            if (Configuracion.Valor < 1)
                                valorCriterio = 0;

                            Evaluacion_Estandar_Minimo evaluacion = new Evaluacion_Estandar_Minimo
                            {
                                Fk_Id_Config_Valoracion_SubEstand = Configuracion.Pk_Id_Config_Valoracion_SubEstand,
                                Fk_Id_Criterio = EvaluacionEstandar.IdCriterio,
                                Fk_Id_Empresa_Evaluar = empr.Pk_Id_Empresa_Evaluar,
                                Valor_Calificacion = valorCriterio
                            };

                            if (codjustifica == (int)EnumPlanificacion.ValoracionStandares.Justifica)
                            {
                                evaluacion.Justificacion = EvaluacionEstandar.Justificacion;
                            }

                            context.Tbl_Evaluacion_Estandares_Minimos.Add(evaluacion);
                            context.SaveChanges();
                            EvaluacionEstandar.IdEvalEstandarMinimo = evaluacion.Pk_Id_Eval_Estandar_Minimo;

                            if (EvaluacionEstandar.IdValoracionCriterio == (int)EnumPlanificacion.ValoracionStandares.NoCumple)
                            {
                                foreach (var actividad in EvaluacionEstandar.Actividades)
                                {
                                    ActividadEvaluacion act = new ActividadEvaluacion
                                    {
                                        Descripcion = actividad.Descripcion,
                                        Responsable = actividad.Responsable,
                                        FechaFin = actividad.FechaFin
                                    };

                                    context.Tbl_Actividades_Evaluacion.Add(act);
                                    context.SaveChanges();

                                    Actividad_Criterio accr = new Actividad_Criterio
                                    {
                                        Fk_Id_Actividad = act.Pk_Id_Actividad,
                                        Fk_Id_Eval_Estandar_Minimo = EvaluacionEstandar.IdEvalEstandarMinimo
                                    };

                                    context.Tbl_Actividades_Criterio.Add(accr);
                                    context.SaveChanges();
                                }
                            }
                        }
                        else
                            return EvaluacionEstandar;
                        Transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Transaction.Rollback();
                    }
                }
            }
            return EvaluacionEstandar;
        }

        public EDCiclo ObtenerStandares(int idCiclo)
        {
            EDCiclo ciclo = new EDCiclo();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ciclo = (from c in context.Tbl_Ciclos
                         where c.Pk_Id_Ciclo == idCiclo
                         select new EDCiclo
                         {
                             Id_Ciclo = c.Pk_Id_Ciclo,
                             Nombre = c.Nombre,
                             Porcentaje_Max = c.Porcentaje_Max,
                             Estandares = (from ts in context.Tbl_Estandares
                                           where ts.Fk_Id_Ciclo == c.Pk_Id_Ciclo
                                           select new EDEstandar
                                           {
                                               Id_Estandar = ts.Pk_Id_Estandar,
                                               Descripcion = ts.Descripcion,
                                               Porcentaje_Max = ts.Porcentaje_Max
                                           }).ToList()

                         }).FirstOrDefault();
            }

            return ciclo;
        }

        public List<EDCiclo> ObtenerCiclos()
        {
            List<EDCiclo> ciclos = new List<EDCiclo>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ciclos = (from c in context.Tbl_Ciclos
                          select new EDCiclo
                          {
                              Id_Ciclo = c.Pk_Id_Ciclo,
                              Nombre = c.Nombre,
                              Porcentaje_Max = c.Porcentaje_Max
                          }).ToList();
            }

            return ciclos;
        }



        public EDCiclo ObtenerEstandarPorCiclo(int idCiclo, int idCriterioActual, int idEmpresa)
        {
            EDCiclo ciclo = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                bool encuentraID = true;
                int idSiguienteCriterio = 0;
                int intentos = 0;
                do
                {
                    idCriterioActual++;
                    idSiguienteCriterio = context.Tbl_Criterios.Where(cr => cr.Pk_Id_Criterio == idCriterioActual).Select(cr => cr.Pk_Id_Criterio).FirstOrDefault();
                    if (idSiguienteCriterio > 0)
                        encuentraID = false;
                    if (intentos >= 10)
                        encuentraID = false;
                    intentos++;
                } while (encuentraID);

                var idSubestandar = context.Tbl_Criterios.Where(cr => cr.Pk_Id_Criterio == idSiguienteCriterio).Select(cr => cr.Fk_Id_SubEstandar).FirstOrDefault();
                var idEstandar = context.Tbl_SubEstandares.Where(sb => sb.Pk_Id_SubEstandar == idSubestandar).Select(sb => sb.Fk_Id_Estandar).FirstOrDefault();

                ciclo = (from c in context.Tbl_Ciclos
                         where c.Pk_Id_Ciclo == idCiclo
                         select new EDCiclo
                         {
                             Id_Ciclo = c.Pk_Id_Ciclo,
                             Nombre = c.Nombre,
                             Estandar = (from e in context.Tbl_Estandares
                                         where e.Pk_Id_Estandar == idEstandar
                                         select new EDEstandar
                                         {
                                             Id_Estandar = e.Pk_Id_Estandar,
                                             Descripcion = e.Descripcion,
                                             SubEstandar = (from sb in context.Tbl_SubEstandares
                                                            where sb.Pk_Id_SubEstandar == idSubestandar
                                                            select new EDSubEstandar
                                                            {
                                                                Id_SubEstandar = sb.Pk_Id_SubEstandar,
                                                                Descripcion = sb.Descripcion,
                                                                Criterio = (from cr in context.Tbl_Criterios
                                                                            where cr.Pk_Id_Criterio == idSiguienteCriterio
                                                                            select new EDCriterio
                                                                            {
                                                                                Id_Criterio = cr.Pk_Id_Criterio,
                                                                                Descripcion = cr.Descripcion,
                                                                                Marco_Legal = cr.Marco_Legal,
                                                                                Modo_Verificacion = cr.Modo_Verificacion,
                                                                                Numeral = cr.Numeral
                                                                            }).FirstOrDefault()
                                                            }).FirstOrDefault()
                                         }).FirstOrDefault()
                         }).FirstOrDefault();
            }
            return ciclo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCiclo"></param>
        /// <param name="Nit"></param>
        /// <returns></returns>
        public EDCiclo ObtenerEstandarPorCiclo(int idCiclo, string Nit)
        {
            EDCiclo ciclo = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var empr = (from eval in context.Tbl_Empresas_Evaluar
                            join emp in context.Tbl_Empresa on eval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select eval).FirstOrDefault();
                //var emp = context.Tbl_Empresas_Evaluar.Where(e => e.Nit.Trim().Equals(Nit.Trim())).Select(e => e).FirstOrDefault();
                var criteriosEvaluados = new List<int>();
                if (empr != null)
                {
                    criteriosEvaluados = (from tcr in context.Tbl_Criterios
                                          join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                                          from ljoin in lefjoin.DefaultIfEmpty()
                                          where ljoin.Fk_Id_Empresa_Evaluar == empr.Pk_Id_Empresa_Evaluar
                                          select ljoin.Fk_Id_Criterio).ToList();
                }
                ciclo = (from c in context.Tbl_Ciclos
                         join e in context.Tbl_Estandares on c.Pk_Id_Ciclo equals e.Fk_Id_Ciclo
                         join sb in context.Tbl_SubEstandares on e.Pk_Id_Estandar equals sb.Fk_Id_Estandar
                         join cr in context.Tbl_Criterios on sb.Pk_Id_SubEstandar equals cr.Fk_Id_SubEstandar
                         join teval in context.Tbl_Evaluacion_Estandares_Minimos on cr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                         from ljoin in lefjoin.DefaultIfEmpty()
                         where c.Pk_Id_Ciclo == idCiclo && !criteriosEvaluados.Contains(cr.Pk_Id_Criterio)
                         select new EDCiclo
                         {
                             Id_Ciclo = c.Pk_Id_Ciclo,
                             Nombre = c.Nombre,
                             Estandar = new EDEstandar
                             {
                                 Id_Estandar = e.Pk_Id_Estandar,
                                 Descripcion = e.Descripcion,
                                 SubEstandar = new EDSubEstandar
                                 {
                                     Id_SubEstandar = sb.Pk_Id_SubEstandar,
                                     Descripcion = sb.Descripcion,
                                     Criterio = new EDCriterio
                                     {
                                         Id_Criterio = cr.Pk_Id_Criterio,
                                         Descripcion = cr.Descripcion,
                                         Marco_Legal = cr.Marco_Legal,
                                         Modo_Verificacion = cr.Modo_Verificacion,
                                         Numeral = cr.Numeral
                                     }
                                 }
                             }
                         }).FirstOrDefault();
            }
            return ciclo;
        }

        public int ObtenerCantidaEstdPorEvaluas(int idCiclo, string Nit)
        {
            int cantidadSinEval = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var empr = (from eval in context.Tbl_Empresas_Evaluar
                            join emp in context.Tbl_Empresa on eval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select eval).FirstOrDefault();
                //var emp = context.Tbl_Empresas_Evaluar.Where(e => e.Nit.Trim().Equals(Nit.Trim())).Select(e => e).FirstOrDefault();
                //var emp = context.Tbl_Empresas_Evaluar.Where(e => e.Nit.Trim().Equals(Nit.Trim())).Select(e => e).FirstOrDefault();
                List<int> criteriosEvaluados = null;
                if (empr != null)
                {
                    criteriosEvaluados = (from tcr in context.Tbl_Criterios
                                          join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                                          from ljoin in lefjoin.DefaultIfEmpty()
                                          where ljoin.Fk_Id_Empresa_Evaluar == empr.Pk_Id_Empresa_Evaluar
                                          select ljoin.Fk_Id_Criterio).ToList();
                    cantidadSinEval = (from tc in context.Tbl_Ciclos
                                       join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                                       join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                                       join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                                       join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                                       from ljoin in lefjoin.DefaultIfEmpty()
                                       where tc.Pk_Id_Ciclo == idCiclo
                                       && !criteriosEvaluados.Contains(tcr.Pk_Id_Criterio)
                                       select tcr.Pk_Id_Criterio).Distinct().ToList().Count();
                }
                else
                {
                    cantidadSinEval = (from tc in context.Tbl_Ciclos
                                       join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                                       join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                                       join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                                       join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                                       from ljoin in lefjoin.DefaultIfEmpty()
                                       where tc.Pk_Id_Ciclo == idCiclo
                                       select tcr.Pk_Id_Criterio).Distinct().ToList().Count();
                }
            }
            return cantidadSinEval;
        }


        public int ObtenerCantidadCriteriosPorEstandar(int idCiclo)
        {
            int cantidad = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                cantidad = (from tc in context.Tbl_Ciclos
                            join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                            join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                            join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                            where tc.Pk_Id_Ciclo == idCiclo
                            select tcr.Pk_Id_Criterio).Distinct().ToList().Count();
            }
            return cantidad;
        }

        public EDValoracionFinal ObtenerCalificacion(string Nit)
        {
            EDValoracionFinal calificacionFinal = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var empr = (from eval in context.Tbl_Empresas_Evaluar
                            join emp in context.Tbl_Empresa on eval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select eval).FirstOrDefault();
                if (empr == null)
                    return null;
                //var emp = context.Tbl_Empresas_Evaluar.Where(e => e.Nit.Trim().Equals(Nit.Trim())).Select(e => e).FirstOrDefault();

                var calificacion = (from teval in context.Tbl_Evaluacion_Estandares_Minimos
                                    where teval.Fk_Id_Empresa_Evaluar == empr.Pk_Id_Empresa_Evaluar
                                    select teval.Valor_Calificacion).Sum();

                calificacionFinal = (from vf in context.Tbl_Valoracion_Final
                                     where vf.Rango_Inicial <= calificacion && vf.Rango_Final >= calificacion
                                     select new EDValoracionFinal
                                     {
                                         IdValoracionFinal = vf.Pk_Id_Valoracion_Final,
                                         Accion = vf.Accion,
                                         Valoracion = vf.Valoracion,
                                         CriterioValoracion = vf.CriterioEvaluacion
                                     }).FirstOrDefault();

            }

            return calificacionFinal;
        }

        public List<EDCiclo> ObtenerDatosInformeExcel(string Nit)
        {
            List<EDCiclo> Ciclos = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var empr = (from eval in context.Tbl_Empresas_Evaluar
                            join emp in context.Tbl_Empresa on eval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select eval).FirstOrDefault();
                if (empr == null)
                    return null;

                //var emp = context.Tbl_Empresas_Evaluar.Where(e => e.Nit.Trim().Equals(Nit.Trim())).Select(e => e).FirstOrDefault();

                Ciclos = context.Tbl_Ciclos.Select(c => new EDCiclo
                {
                    Id_Ciclo = c.Pk_Id_Ciclo,
                    Estandares = c.Estandares.Select(es => new EDEstandar
                    {
                        Id_Estandar = es.Pk_Id_Estandar,
                        SubEstandares = es.SubEstandares.Select(sb => new EDSubEstandar
                        {
                            Id_SubEstandar = sb.Pk_Id_SubEstandar,
                            Criterios = (from cr in sb.Criterios
                                         join tesm in context.Tbl_Evaluacion_Estandares_Minimos on cr.Pk_Id_Criterio equals tesm.Fk_Id_Criterio
                                         join tcvs in context.Tbl_Config_Valoracion_SubEstandares on tesm.Fk_Id_Config_Valoracion_SubEstand equals tcvs.Pk_Id_Config_Valoracion_SubEstand
                                         where tesm.Fk_Id_Empresa_Evaluar == empr.Pk_Id_Empresa_Evaluar
                                         select new EDCriterio
                                         {
                                             Id_Criterio = cr.Pk_Id_Criterio,
                                             Descripcion_Corta = cr.Descripcion_Corta,
                                             Evaluacion = new EDEvaluacionEstandarMinimo
                                             {
                                                 IdValoracionCriterio = tcvs.Fk_Id_Valoracion_Criterio,
                                                 Justificacion = (string.IsNullOrEmpty(tesm.Justificacion) ? "0" : "1")
                                             }

                                         }).ToList(),
                            CalTotal = (from cr in sb.Criterios
                                        join tesm in context.Tbl_Evaluacion_Estandares_Minimos on cr.Pk_Id_Criterio equals tesm.Fk_Id_Criterio
                                        //join tcvs in context.Tbl_Config_Valoracion_SubEstandares on tesm.Fk_Id_Config_Valoracion_SubEstand equals tcvs.Pk_Id_Config_Valoracion_SubEstand
                                        where tesm.Fk_Id_Empresa_Evaluar == empr.Pk_Id_Empresa_Evaluar
                                        select tesm.Valor_Calificacion).Sum()
                        }).ToList()

                    }).ToList()
                }).ToList();
            }

            return Ciclos;

        }

        public List<EDCiclo> ObtenerDatosInicialesInformeExcel()
        {
            List<EDCiclo> Ciclos = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Ciclos = (from c in context.Tbl_Ciclos
                          select new EDCiclo
                          {
                              Id_Ciclo = c.Pk_Id_Ciclo,
                              Nombre = c.Nombre,
                              Estandares = c.Estandares.Select(es => new EDEstandar
                              {
                                  Id_Estandar = es.Pk_Id_Estandar,
                                  Descripcion = es.Descripcion,
                                  SubEstandares = (from sb in es.SubEstandares
                                                       //join tcvs in context.Tbl_Config_Valoracion_SubEstandares on sb.Pk_Id_SubEstandar equals tcvs.Fk_Id_SubEstandar
                                                       ///where tcvs.Fk_Id_Valoracion_Criterio == (int)EnumPlanificacion.ValoracionStandares.CumpleTotalMente
                                                   select new EDSubEstandar
                                                   {
                                                       Id_SubEstandar = sb.Pk_Id_SubEstandar,
                                                       Descripcion_Corta = sb.Descripcion_Corta,
                                                       Procentaje_Max = sb.Procentaje_Max,
                                                       Criterios = sb.Criterios.Select(cr => new EDCriterio
                                                       {
                                                           Id_Criterio = cr.Pk_Id_Criterio,
                                                           Descripcion_Corta = cr.Descripcion_Corta,
                                                           ValPorPregunta = cr.Valor.ToString()
                                                       }).ToList()
                                                   }).ToList()

                              }).ToList()
                          }).ToList();
            }
            return Ciclos;
        }


        public List<EDActividad> ObtenerActividades(string Nit)
        {
            List<EDActividad> Actividades = new List<EDActividad>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var empr = (from eval in context.Tbl_Empresas_Evaluar
                            join emp in context.Tbl_Empresa on eval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select eval).FirstOrDefault();
                if (empr == null)
                    return null;

                //var emp = context.Tbl_Empresas_Evaluar.Where(e => e.Nit.Trim().Equals(Nit.Trim())).Select(e => e).FirstOrDefault();
                Actividades = (from eval in context.Tbl_Evaluacion_Estandares_Minimos
                               join ac in context.Tbl_Actividades_Criterio on eval.Pk_Id_Eval_Estandar_Minimo equals ac.Fk_Id_Eval_Estandar_Minimo
                               join ae in context.Tbl_Actividades_Evaluacion on ac.Fk_Id_Actividad equals ae.Pk_Id_Actividad
                               where eval.Fk_Id_Empresa_Evaluar == empr.Pk_Id_Empresa_Evaluar
                               select new EDActividad
                               {
                                   Descripcion = ae.Descripcion,
                                   Responsable = ae.Responsable,
                                   FechaFin = ae.FechaFin
                               }).ToList();
            }
            return Actividades;
        }


        public int ObtenerCantidaEstdPorEvaluas(int idCiclo, int idEmpresa)
        {
            int cantidadSinEval = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {

                var criteriosEvaluados = (from tcr in context.Tbl_Criterios
                                          join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                                          from ljoin in lefjoin.DefaultIfEmpty()
                                          where ljoin.Fk_Id_Empresa_Evaluar == idEmpresa
                                          select ljoin.Fk_Id_Criterio).Distinct().ToList();

                cantidadSinEval = (from tc in context.Tbl_Ciclos
                                   join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                                   join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                                   join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                                   join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio into lefjoin
                                   from ljoin in lefjoin.DefaultIfEmpty()
                                   where tc.Pk_Id_Ciclo == idCiclo
                                   && !criteriosEvaluados.Contains(tcr.Pk_Id_Criterio)
                                   select tcr.Pk_Id_Criterio).Distinct().ToList().Count();

            }
            return cantidadSinEval;
        }



    }
}
