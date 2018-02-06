using SG_SST.EntidadesDominio.PlanTrabajoAnual;
using SG_SST.Interfaces.PlanTrabajoAnual;
using SG_SST.Models;
using SG_SST.Models.PlanCapacitacion;
using SG_SST.Models.PlanTrabajoAnual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.PlanTrabajoAnual
{
    public class PlanEmpresaManager : IPlanEmpresa
    {
        public EDPlanEmpresa GuardarPlanEmpresa(EDPlanEmpresa planempresa)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        PlanEmpresa empre = new PlanEmpresa()
                        {
                            pk_id_plan_empresa = planempresa.pk_id_plan_empresa,
                            IdSede = planempresa.IdSede,
                            FechaDesde = planempresa.FechaDesde,
                            FechaHasta = planempresa.FechaHasta,
                            Vigencia = planempresa.Vigencia,
                            //ObjetivosDescripcion = planempresa.ObjetivosDescripcion,
                            //ObjetivosMetas = planempresa.ObjetivosMetas,
                            //Actividad = planempresa.Actividad,
                            //Responsable = planempresa.Responsable,
                            //RecursosHumanos = planempresa.RecursosHumanos,
                            //RecursosTecnologico = planempresa.RecursosTecnologico,
                            //RecursosFinanciero = planempresa.RecursosFinanciero,
                            //FechaProg = planempresa.FechaProg,
                            //HoraProgIni = planempresa.HoraProgIni,
                            //HoraProgFin = planempresa.HoraProgFin,
                            //Estado = planempresa.Estado,
                            PorcentajeEjecucion = planempresa.PorcentajeEjecucion,
                            RepresentanteLegal = planempresa.RepresentanteLegal,
                            RepresentanteSGSST = planempresa.RepresentanteSGSST
                        };
                        context.Tbl_Plan_Empresa.Add(empre);
                        context.SaveChanges();
                        Transaction.Commit();
                        planempresa.pk_id_plan_empresa = empre.pk_id_plan_empresa;

                    }
                    catch
                    {
                        Transaction.Rollback();
                        return planempresa;
                    }
                }
            }
            return planempresa;
        }
    }
}
