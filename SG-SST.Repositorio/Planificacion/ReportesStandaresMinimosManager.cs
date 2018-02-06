using SG_SST.Interfaces.Planificacion;
using SG_SST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Planificacion
{
    public class ReportesStandaresMinimosManager : IReportesEstandaresMinimos
    {
        public int ObtenerCantidadPreguntasAlmomento(int idCclo, string Nit)
        {
            int cantidad = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                cantidad = (from tc in context.Tbl_Ciclos
                            join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                            join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                            join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                            join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio
                            join emeval in context.Tbl_Empresas_Evaluar on teval.Fk_Id_Empresa_Evaluar equals emeval.Pk_Id_Empresa_Evaluar
                            join emp in context.Tbl_Empresa on emeval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where tc.Pk_Id_Ciclo == idCclo && emp.Nit_Empresa.Trim ().Equals (Nit.Trim())
                            select teval.Fk_Id_Criterio).ToList().Count();
            }
            return cantidad;
        }

        public decimal ObtenerPorcentajeObtenidoAlmomento(int idCclo, string Nit)
        {
            decimal cantidad = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                cantidad = (from tc in context.Tbl_Ciclos
                            join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                            join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                            join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                            join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio
                            //join tconf in context.Tbl_Config_Valoracion_SubEstandares on teval.Fk_Id_Config_Valoracion_SubEstand equals tconf.Pk_Id_Config_Valoracion_SubEstand
                            join emeval in context.Tbl_Empresas_Evaluar on teval.Fk_Id_Empresa_Evaluar equals emeval.Pk_Id_Empresa_Evaluar
                            join emp in context.Tbl_Empresa on emeval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where tc.Pk_Id_Ciclo == idCclo && emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select teval.Valor_Calificacion).ToList().Sum();
            }
            return cantidad;
        }

        public decimal ObtenerPorcentajeObtenidoEstandar(int idCclo, int idEstandar, string Nit)
        {
            decimal cantidad = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                cantidad = (from tc in context.Tbl_Ciclos
                            join ts in context.Tbl_Estandares on tc.Pk_Id_Ciclo equals ts.Fk_Id_Ciclo
                            join tsb in context.Tbl_SubEstandares on ts.Pk_Id_Estandar equals tsb.Fk_Id_Estandar
                            join tcr in context.Tbl_Criterios on tsb.Pk_Id_SubEstandar equals tcr.Fk_Id_SubEstandar
                            join teval in context.Tbl_Evaluacion_Estandares_Minimos on tcr.Pk_Id_Criterio equals teval.Fk_Id_Criterio
                            //join tconf in context.Tbl_Config_Valoracion_SubEstandares on teval.Fk_Id_Config_Valoracion_SubEstand equals tconf.Pk_Id_Config_Valoracion_SubEstand
                            join emeval in context.Tbl_Empresas_Evaluar on teval.Fk_Id_Empresa_Evaluar equals emeval.Pk_Id_Empresa_Evaluar
                            join emp in context.Tbl_Empresa on emeval.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                            where tc.Pk_Id_Ciclo == idCclo && ts.Pk_Id_Estandar == idEstandar && emp.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select teval.Valor_Calificacion).ToList().Sum(v => v);
            }
            return cantidad;
        }    
    }
}
