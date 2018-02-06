using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Aplicacion
{
    public class PromocionPrevencionManager : IPromocioPrevencion
    {
        public List<EDPlanVial> ConsultarPlanesVial(List<EDSede> ListaSedes)
        {
            List<EDPlanVial> NuevaListaPlanVial = new List<EDPlanVial>();

            foreach (var item1 in ListaSedes)
            {
                int Pk_Sede = item1.IdSede;
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_PlanVial
                                    where s.Fk_Id_Sede == Pk_Sede
                                    select s).ToList<PlanVial>();
                    if (Listavar != null)
                    {
                        Listavar = Listavar.OrderBy(s => s.Id_Consecutivo).ToList();
                        foreach (var item in Listavar)
                        {
                            EDPlanVial EDPlanVial = new EDPlanVial();
                            EDPlanVial.Pk_Id_SegVial = item.Pk_Id_SegVial;
                            EDPlanVial.Id_Consecutivo = item.Id_Consecutivo;
                            EDPlanVial.Fecha_Registro = item.Fecha_Registro;
                            EDPlanVial.Estado = item.Estado;
                            EDPlanVial.Version = item.Version;
                            EDPlanVial.Fk_Id_Sede = item.Fk_Id_Sede;
                            EDPlanVial.NombreSede = item1.NombreSede;
                            NuevaListaPlanVial.Add(EDPlanVial);
                        }
                    }
                }
            }
            NuevaListaPlanVial = NuevaListaPlanVial.OrderBy(s => s.Id_Consecutivo).ToList();
            return NuevaListaPlanVial;
        }
        private int RegistroNuevo(int IdEmpresa)
        {
            int IdNuevo = 0;
            List<PlanVial> ListaPlanVial = new List<PlanVial>();
            PlanVial PlanVial = new PlanVial();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var PlanVial_v = (from s in db.Tbl_PlanVial
                                join d in db.Tbl_Sede on s.Fk_Id_Sede equals d.Pk_Id_Sede
                                 where d.Fk_Id_Empresa== IdEmpresa
                                select s).ToList<PlanVial>();

                if (PlanVial_v != null)
                {
                    ListaPlanVial = PlanVial_v;
                    if (ListaPlanVial.Count>0)
                    {
                        IdNuevo = ListaPlanVial.Max(s => s.Id_Consecutivo);
                    }
                }
            }
            
            return IdNuevo;
        }
        public bool CrearPlan(int IdSede, int IdEmpresa, List<EDSegVialParametro> PlanVialParam)
        {
            bool ProbarGuardado = false;         
            PlanVial PlanVial = new PlanVial();

            List<SegVialDetalle> ListaDetalles = new List<SegVialDetalle>();
            List<SegVialResultado> ListaResultados = new List<SegVialResultado>();

            PlanVial.Estado = false;
            PlanVial.Fecha_Registro = DateTime.Today;
            PlanVial.Id_Consecutivo = RegistroNuevo(IdEmpresa)+1;
            PlanVial.Fk_Id_Sede = IdSede;
            PlanVial.Version = 1;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialDetalle
                                join d in db.Tbl_SegVialParametro on s.Fk_Id_SegVialPilar equals d.Pk_Id_SegVialParametro
                                join e in db.Tbl_SegVialPilar on d.Fk_Id_SegVialPilar equals e.Pk_Id_SegVialPilar
                                where e.Version==1
                                select s).ToList<SegVialDetalle>();
                if (Listavar != null)
                {
                    ListaDetalles = Listavar;
                }
            

            foreach (var item in ListaDetalles)
            {
                if (item.SegVialParametro.Fk_Id_Empresa!= 0)
                {
                    if (item.SegVialParametro.Fk_Id_Empresa== IdEmpresa && item.SegVialParametro.disabled==true)
                    {
                        SegVialResultado SegVialResultado = new SegVialResultado();
                        SegVialResultado.Pk_Id_SegVialResultado = 0;
                        SegVialResultado.Aplica = false;
                        SegVialResultado.Aplica_s = 0;
                        SegVialResultado.Existencia = false;
                        SegVialResultado.Existencia_s = 0;
                        SegVialResultado.Responde = false;
                        SegVialResultado.Responde_s = 0;
                        SegVialResultado.ValorObtenido = (decimal)0.00;
                        SegVialResultado.Observaciones = null;
                        SegVialResultado.PlanVial = PlanVial;
                        SegVialResultado.Fk_Id_SegVialParametroDetalle = item.Pk_Id_SegVialParametroDetalle;

                        ListaResultados.Add(SegVialResultado);
                    }
                }
                else
                {
                    SegVialResultado SegVialResultado = new SegVialResultado();
                    SegVialResultado.Pk_Id_SegVialResultado = 0;
                    SegVialResultado.Aplica = false;
                    SegVialResultado.Aplica_s = 0;
                    SegVialResultado.Existencia = false;
                    SegVialResultado.Existencia_s = 0;
                    SegVialResultado.Responde = false;
                    SegVialResultado.Responde_s = 0;
                    SegVialResultado.ValorObtenido = (decimal)0.00;
                    SegVialResultado.Observaciones = null;
                    SegVialResultado.PlanVial = PlanVial;
                    SegVialResultado.Fk_Id_SegVialParametroDetalle = item.Pk_Id_SegVialParametroDetalle;

                    ListaResultados.Add(SegVialResultado);
                }
                
            }
            }
            using (SG_SSTContext db = new SG_SSTContext())
            {
                db.Tbl_PlanVial.Add(PlanVial);
                foreach (var item in ListaResultados)
                {
                    db.Tbl_SegVialResultado.Add(item);
                }
                try
                {
                    db.SaveChanges();
                    ProbarGuardado = true;
                }
                catch (Exception ex)
                {
                }
            }
            return ProbarGuardado;
        }

        public bool CrearParametro(EDSegVialParametro EDSegVialParametro)
        {
            List<SegVialDetalle> ListaDetalles = new List<SegVialDetalle>();
            SegVialParametro SegVialParametro = new SegVialParametro();
            bool ProbarGuardado = false;

            SegVialParametro.disabled = true;
            SegVialParametro.Numero = 1;
            SegVialParametro.Numeral = "1";
            SegVialParametro.ParametroDef = EDSegVialParametro.ParametroDef;
            SegVialParametro.Valor_Parametro = EDSegVialParametro.Valor_Parametro;
            SegVialParametro.Fk_Id_SegVialPilar = 6;
            SegVialParametro.Fk_Id_Empresa = EDSegVialParametro.Fk_Id_Empresa;

            foreach (var item1 in EDSegVialParametro.ListaDetalles)
            {
                SegVialDetalle SegVialDetalle = new SegVialDetalle();
                SegVialDetalle.Numeral = "1";
                SegVialDetalle.VariableDesc = item1.VariableDesc;
                SegVialDetalle.CriterioAval = item1.CriterioAval;
                SegVialDetalle.SegVialParametro = SegVialParametro;
                ListaDetalles.Add(SegVialDetalle);
            }


            using (SG_SSTContext db = new SG_SSTContext())
            {
                db.Tbl_SegVialParametro.Add(SegVialParametro);
                foreach (var item in ListaDetalles)
                {
                    db.Tbl_SegVialDetalle.Add(item);
                }
                try
                {
                    db.SaveChanges();
                    ProbarGuardado = true;
                }
                catch (Exception ex)
                {
                }
            }

            return ProbarGuardado;
        }


        public bool ExisteParametroResultado(int pkParametro, int IdEmpresa)
        {
            SegVialParametro SegVialParametro = new SegVialParametro();
            bool ProbarGuardado = true;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialParametro
                                join d in db.Tbl_SegVialDetalle on s.Pk_Id_SegVialParametro equals d.Fk_Id_SegVialPilar
                                join e in db.Tbl_SegVialResultado on d.Pk_Id_SegVialParametroDetalle equals e.Fk_Id_SegVialParametroDetalle
                                where s.Pk_Id_SegVialParametro == pkParametro && s.Fk_Id_Empresa== IdEmpresa
                                select s).FirstOrDefault<SegVialParametro>();
                if (Listavar != null)
                {
                    ProbarGuardado = false;
                    return ProbarGuardado;
                }
            }
            return ProbarGuardado;
        }
        public bool EliminarParametro(int pkParametro, int IdEmpresa)
        {
            SegVialParametro SegVialParametro = new SegVialParametro();
            bool ProbarGuardado = false;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialParametro
                                where s.Pk_Id_SegVialParametro == pkParametro && s.Fk_Id_Empresa == IdEmpresa
                                select s).FirstOrDefault<SegVialParametro>();
                if (Listavar != null)
                {
                    db.Entry(Listavar).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ProbarGuardado = true;
                }
            }
            return ProbarGuardado;
        }
        public bool OcultarParametro(int pkParametro, int IdEmpresa)
        {
            SegVialParametro SegVialParametro = new SegVialParametro();
            bool ProbarGuardado = false;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialParametro
                                where s.Pk_Id_SegVialParametro == pkParametro && s.Fk_Id_Empresa == IdEmpresa
                                select s).FirstOrDefault<SegVialParametro>();
                if (Listavar != null)
                {
                    Listavar.disabled = false;
                    db.Entry(Listavar).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ProbarGuardado = true;
                }
            }
            return ProbarGuardado;
        }

        public List<EDSegVialResultado> ConsultarPlanVialResultado(int IdSegVial, List<EDSede> ListaSedes, int IdEmpresa)
        {
            
            List<SegVialDetalle> ListaDetalles = new List<SegVialDetalle>();



            List<EDSegVialResultado> NuevaListaResultados = new List<EDSegVialResultado>();
            List<EDPlanVial> NuevaListaPlanVial = new List<EDPlanVial>();
            EDPlanVial EDPlanVial_c = new EDPlanVial();
            
            foreach (var item1 in ListaSedes)
            {
                int Pk_Sede = item1.IdSede;
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_PlanVial
                                    where s.Fk_Id_Sede == Pk_Sede
                                    select s).ToList<PlanVial>();
                    if (Listavar != null)
                    {
                        Listavar = Listavar.OrderBy(s => s.Id_Consecutivo).ToList();
                        foreach (var item in Listavar)
                        {
                            EDPlanVial EDPlanVial = new EDPlanVial();
                            EDPlanVial.Pk_Id_SegVial = item.Pk_Id_SegVial;
                            EDPlanVial.Id_Consecutivo = item.Id_Consecutivo;
                            EDPlanVial.Fecha_Registro = item.Fecha_Registro;
                            EDPlanVial.Estado = item.Estado;
                            EDPlanVial.Version = item.Version;
                            EDPlanVial.Fk_Id_Sede = item.Fk_Id_Sede;
                            EDPlanVial.NombreSede = item1.NombreSede;
                            NuevaListaPlanVial.Add(EDPlanVial);
                        }
                    }
                    else
                    {

                    }
                }
            }

            List<EDSegVialParametro> ListaParametros = ConsultarParametros6(IdEmpresa, IdSegVial);



            EDPlanVial_c = NuevaListaPlanVial.Where(s => s.Pk_Id_SegVial == IdSegVial).FirstOrDefault();
            if (EDPlanVial_c != null)
            {
                if (EDPlanVial_c.Pk_Id_SegVial != 0)
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        var Listavar = (from s in db.Tbl_SegVialResultado
                                        where s.Fk_Id_PlanVial == EDPlanVial_c.Pk_Id_SegVial
                                        select s).ToList<SegVialResultado>();
                        if (Listavar!=null)
                        {
                            foreach (var item in Listavar)
                            {
                                
                                SegVialDetalle DetalleParametro_m = item.SegVialDetalle;
                                PlanVial PlanVial_m = item.PlanVial;
                                SegVialParametro Parametro_m = DetalleParametro_m.SegVialParametro;
                                SegVialPilar Pilar_m = Parametro_m.SegVialPilar;

                                EDSegVialPilar Pilar = new EDSegVialPilar();
                                EDSegVialParametro Parametro = new EDSegVialParametro();
                                EDSegVialDetalle DetalleParametro = new EDSegVialDetalle();
                                EDPlanVial PlanVial = new EDPlanVial();

                                Pilar.Descripcion = Pilar_m.Descripcion;
                                Pilar.Pk_Id_SegVialPilar = Pilar_m.Pk_Id_SegVialPilar;
                                Pilar.Valor_Ponderado = Pilar_m.Valor_Ponderado;
                                Pilar.Version = Pilar_m.Version;
                                Pilar.Descripcion = Pilar_m.Descripcion;

                                EDSegVialParametro parametro6 = ListaParametros.Where(s => s.Pk_Id_SegVialParametro == Parametro_m.Pk_Id_SegVialParametro).FirstOrDefault();
                                if (parametro6!=null)
                                {
                                    Parametro.Fk_Id_SegVialPilar = Parametro_m.Fk_Id_SegVialPilar;
                                    Parametro.Numeral = parametro6.Numeral;
                                    Parametro.Numero = parametro6.Numero;
                                    Parametro.ParametroDef = Parametro_m.ParametroDef;
                                    Parametro.Pk_Id_SegVialParametro = Parametro_m.Pk_Id_SegVialParametro;
                                    Parametro.Valor_Parametro = Parametro_m.Valor_Parametro;

                                    EDSegVialDetalle detalle6 = parametro6.ListaDetalles.Where(s => s.Pk_Id_SegVialParametroDetalle == DetalleParametro_m.Pk_Id_SegVialParametroDetalle).FirstOrDefault();
                                    if (detalle6!=null)
                                    {
                                        DetalleParametro.CriterioAval = DetalleParametro_m.CriterioAval;
                                        DetalleParametro.Fk_Id_SegVialPilar = DetalleParametro_m.Fk_Id_SegVialPilar;
                                        DetalleParametro.Numeral = detalle6.Numeral;
                                        DetalleParametro.Pk_Id_SegVialParametroDetalle = DetalleParametro_m.Pk_Id_SegVialParametroDetalle;
                                        DetalleParametro.VariableDesc = DetalleParametro_m.VariableDesc;
                                    }
                                    else
                                    {
                                        DetalleParametro.CriterioAval = DetalleParametro_m.CriterioAval;
                                        DetalleParametro.Fk_Id_SegVialPilar = DetalleParametro_m.Fk_Id_SegVialPilar;
                                        DetalleParametro.Numeral = DetalleParametro_m.Numeral;
                                        DetalleParametro.Pk_Id_SegVialParametroDetalle = DetalleParametro_m.Pk_Id_SegVialParametroDetalle;
                                        DetalleParametro.VariableDesc = DetalleParametro_m.VariableDesc;
                                    }


                                }
                                else
                                {
                                    Parametro.Fk_Id_SegVialPilar = Parametro_m.Fk_Id_SegVialPilar;
                                    Parametro.Numeral = Parametro_m.Numeral;
                                    Parametro.Numero = Parametro_m.Numero;
                                    Parametro.ParametroDef = Parametro_m.ParametroDef;
                                    Parametro.Pk_Id_SegVialParametro = Parametro_m.Pk_Id_SegVialParametro;
                                    Parametro.Valor_Parametro = Parametro_m.Valor_Parametro;

                                    DetalleParametro.CriterioAval = DetalleParametro_m.CriterioAval;
                                    DetalleParametro.Fk_Id_SegVialPilar = DetalleParametro_m.Fk_Id_SegVialPilar;
                                    DetalleParametro.Numeral = DetalleParametro_m.Numeral;
                                    DetalleParametro.Pk_Id_SegVialParametroDetalle = DetalleParametro_m.Pk_Id_SegVialParametroDetalle;
                                    DetalleParametro.VariableDesc = DetalleParametro_m.VariableDesc;
                                }




                                



                                PlanVial.Fk_Id_Sede = PlanVial_m.Fk_Id_Sede;
                                PlanVial.Fecha_Registro = PlanVial_m.Fecha_Registro;
                                PlanVial.Estado = PlanVial_m.Estado;
                                PlanVial.Version = PlanVial_m.Version;



                                EDSegVialResultado EDSegVialResultado = new EDSegVialResultado();
                                EDSegVialResultado.Pk_Id_SegVialResultado = item.Pk_Id_SegVialResultado;
                                EDSegVialResultado.Fk_Id_PlanVial = item.Fk_Id_PlanVial;
                                EDSegVialResultado.Aplica = item.Aplica;
                                EDSegVialResultado.Aplica_s = item.Aplica_s;
                                EDSegVialResultado.Existencia = item.Existencia;
                                EDSegVialResultado.Existencia_s = item.Existencia_s;
                                EDSegVialResultado.Responde = item.Responde;
                                EDSegVialResultado.Responde_s = item.Responde_s;
                                EDSegVialResultado.ValorObtenido = item.ValorObtenido;
                                EDSegVialResultado.Observaciones = item.Observaciones;
                                EDSegVialResultado.Fk_Id_SegVialParametroDetalle = item.Fk_Id_SegVialParametroDetalle;
                                EDSegVialResultado.Pilar = Pilar;
                                EDSegVialResultado.Parametro = Parametro;
                                EDSegVialResultado.DetalleParametro = DetalleParametro;
                                EDSegVialResultado.PlanVial = PlanVial;
                                NuevaListaResultados.Add(EDSegVialResultado);
                            }
                            

                        }
                    }
                }
            }

            return NuevaListaResultados;
        }
        public List<EDSegVialPilar> ConsultarPlanVialPilares(int IdSegVial, List<EDSede> ListaSedes)
        {
            List<EDSegVialPilar> NuevaListaPilar = new List<EDSegVialPilar>();
            List<EDPlanVial> NuevaListaPlanVial = new List<EDPlanVial>();
            EDPlanVial EDPlanVial_c = new EDPlanVial();
            foreach (var item1 in ListaSedes)
            {
                int Pk_Sede = item1.IdSede;
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_PlanVial
                                    where s.Fk_Id_Sede == Pk_Sede
                                    select s).ToList<PlanVial>();
                    if (Listavar != null)
                    {
                        Listavar = Listavar.OrderBy(s => s.Id_Consecutivo).ToList();
                        foreach (var item in Listavar)
                        {
                            EDPlanVial EDPlanVial = new EDPlanVial();
                            EDPlanVial.Pk_Id_SegVial = item.Pk_Id_SegVial;
                            EDPlanVial.Id_Consecutivo = item.Id_Consecutivo;
                            EDPlanVial.Fecha_Registro = item.Fecha_Registro;
                            EDPlanVial.Estado = item.Estado;
                            EDPlanVial.Version = item.Version;
                            EDPlanVial.Fk_Id_Sede = item.Fk_Id_Sede;
                            EDPlanVial.NombreSede = item1.NombreSede;
                            NuevaListaPlanVial.Add(EDPlanVial);
                        }
                    }
                    else
                    {

                    }
                }
            }

            EDPlanVial_c = NuevaListaPlanVial.Where(s => s.Pk_Id_SegVial == IdSegVial).FirstOrDefault();
            if (EDPlanVial_c != null)
            {
                if (EDPlanVial_c.Pk_Id_SegVial != 0)
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        var Listavar = (from s in db.Tbl_SegVialPilar
                                        join d in db.Tbl_SegVialParametro on s.Pk_Id_SegVialPilar equals d.Fk_Id_SegVialPilar
                                        join e in db.Tbl_SegVialDetalle on d.Pk_Id_SegVialParametro equals e.Fk_Id_SegVialPilar
                                        join f in db.Tbl_SegVialResultado on e.Pk_Id_SegVialParametroDetalle equals f.Fk_Id_SegVialParametroDetalle
                                        where f.Fk_Id_PlanVial== EDPlanVial_c.Pk_Id_SegVial
                                        select s).ToList<SegVialPilar>().Distinct();
                        if (Listavar != null)
                        {
                            foreach (var item in Listavar)
                            {
                                EDSegVialPilar EDSegVialPilar = new EDSegVialPilar();
                                EDSegVialPilar.Pk_Id_SegVialPilar = item.Pk_Id_SegVialPilar;
                                EDSegVialPilar.Descripcion = item.Descripcion;
                                EDSegVialPilar.Version = item.Version;
                                EDSegVialPilar.Valor_Ponderado = item.Valor_Ponderado;

                                NuevaListaPilar.Add(EDSegVialPilar);
                            }


                        }
                    }
                }
            }

            return NuevaListaPilar;
        }

        public List<EDSegVialParametro> ConsultarParametros(int fk_empresa)
        {
            List<EDSegVialParametro> NuevaListaEDPlanVialpil = new List<EDSegVialParametro>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialParametro
                                where s.Fk_Id_Empresa == fk_empresa && s.disabled==true
                                select s).ToList<SegVialParametro>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDSegVialParametro EDSegVialParametro = new EDSegVialParametro();

                        EDSegVialParametro.Pk_Id_SegVialParametro = item.Pk_Id_SegVialParametro;
                        EDSegVialParametro.Numero = item.Numero;
                        EDSegVialParametro.Numeral = item.Numeral;
                        EDSegVialParametro.ParametroDef = item.ParametroDef;
                        EDSegVialParametro.Valor_Parametro = item.Valor_Parametro;
                        EDSegVialParametro.Fk_Id_SegVialPilar = item.Fk_Id_SegVialPilar;
                        EDSegVialParametro.Fk_Id_Empresa = item.Fk_Id_Empresa;
                        EDSegVialParametro.Pk_Id_SegVialParametro = item.Pk_Id_SegVialParametro;
                        EDSegVialParametro.disabled = item.disabled;
                        List<SegVialDetalle> ListaDetalles = new List<SegVialDetalle>();

                        if (item.SegVialDetalles!=null)
                        {
                            if (item.SegVialDetalles.Count>0)
                            {
                                ListaDetalles = item.SegVialDetalles.ToList();
                                foreach (var item1 in ListaDetalles)
                                {
                                    EDSegVialDetalle EDSegVialDetalle = new EDSegVialDetalle();
                                    EDSegVialDetalle.Pk_Id_SegVialParametroDetalle = item1.Pk_Id_SegVialParametroDetalle;
                                    EDSegVialDetalle.Numeral = item1.Numeral;
                                    EDSegVialDetalle.VariableDesc = item1.VariableDesc;
                                    EDSegVialDetalle.CriterioAval = item1.CriterioAval;
                                    EDSegVialDetalle.Fk_Id_SegVialPilar = item1.Fk_Id_SegVialPilar;
                                }
                            }
                        }

                        NuevaListaEDPlanVialpil.Add(EDSegVialParametro);
                    }
                }
                else
                {

                }
            }
            return NuevaListaEDPlanVialpil;
        }

        public List<EDSegVialDetalle> ConsultarVariables(int pkparam)
        {
            List<EDSegVialDetalle> NuevaListaEDPlanVialpil = new List<EDSegVialDetalle>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialDetalle
                                where s.Fk_Id_SegVialPilar == pkparam
                                select s).ToList<SegVialDetalle>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDSegVialDetalle EDSegVialDetalle = new EDSegVialDetalle();

                        EDSegVialDetalle.Pk_Id_SegVialParametroDetalle = item.Pk_Id_SegVialParametroDetalle;
                        EDSegVialDetalle.Numeral = item.Numeral;
                        EDSegVialDetalle.VariableDesc = item.VariableDesc;
                        EDSegVialDetalle.CriterioAval = item.CriterioAval;
                        EDSegVialDetalle.Fk_Id_SegVialPilar = item.Fk_Id_SegVialPilar;
                        NuevaListaEDPlanVialpil.Add(EDSegVialDetalle);
                    }
                }
 
            }
            return NuevaListaEDPlanVialpil;
        }

        public List<EDSegVialParametro> ConsultarParametros6(int fk_empresa, int pkPlan)
        {
            List<EDSegVialParametro> NuevaListaEDPlanVialpil = new List<EDSegVialParametro>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialParametro
                                join d in db.Tbl_SegVialDetalle on s.Pk_Id_SegVialParametro equals d.Fk_Id_SegVialPilar
                                join e in db.Tbl_SegVialResultado on d.Pk_Id_SegVialParametroDetalle equals e.Fk_Id_SegVialParametroDetalle
                                join f in db.Tbl_PlanVial on e.Fk_Id_PlanVial equals f.Pk_Id_SegVial
                                where s.Fk_Id_Empresa == fk_empresa && f.Pk_Id_SegVial== pkPlan
                                select s).ToList<SegVialParametro>().Distinct();
                if (Listavar != null)
                {
                    int cont1 = 1;
                    foreach (var item in Listavar)
                    {
                        EDSegVialParametro EDSegVialParametro = new EDSegVialParametro();

                        EDSegVialParametro.Pk_Id_SegVialParametro = item.Pk_Id_SegVialParametro;
                        EDSegVialParametro.Numero = cont1;
                        item.Numeral = "6." + cont1.ToString();
                        EDSegVialParametro.Numeral = item.Numeral;
                        EDSegVialParametro.ParametroDef = item.ParametroDef;
                        EDSegVialParametro.Valor_Parametro = item.Valor_Parametro;
                        EDSegVialParametro.Fk_Id_SegVialPilar = item.Fk_Id_SegVialPilar;
                        EDSegVialParametro.Fk_Id_Empresa = item.Fk_Id_Empresa;
                        EDSegVialParametro.Pk_Id_SegVialParametro = item.Pk_Id_SegVialParametro;
                        EDSegVialParametro.disabled = item.disabled;
                        EDSegVialParametro.ListaDetalles = new List<EDSegVialDetalle>();
                        List<SegVialDetalle> ListaDetalles = new List<SegVialDetalle>();
                        string numeral = item.Numeral;
                        var Listavar1 = (from s in db.Tbl_SegVialDetalle
                                        where s.Fk_Id_SegVialPilar == item.Pk_Id_SegVialParametro
                                         select s).ToList<SegVialDetalle>().Distinct();

                        if (Listavar1 != null)
                        {
                            if (Listavar1.Count() > 0)
                            {
                                ListaDetalles = Listavar1.ToList();
                                int cont = 1;
                                foreach (var item1 in ListaDetalles)
                                {
                                    EDSegVialDetalle EDSegVialDetalle = new EDSegVialDetalle();
                                    EDSegVialDetalle.Pk_Id_SegVialParametroDetalle = item1.Pk_Id_SegVialParametroDetalle;
                                    EDSegVialDetalle.Numeral = numeral + "."+ cont.ToString();
                                    EDSegVialDetalle.VariableDesc = item1.VariableDesc;
                                    EDSegVialDetalle.CriterioAval = item1.CriterioAval;
                                    EDSegVialDetalle.Fk_Id_SegVialPilar = item1.Fk_Id_SegVialPilar;
                                    
                                    EDSegVialParametro.ListaDetalles.Add(EDSegVialDetalle);
                                    cont++;
                                }
                            }
                        }

                        NuevaListaEDPlanVialpil.Add(EDSegVialParametro);
                        cont1++;
                    }
                }
                else
                {

                }
            }
            return NuevaListaEDPlanVialpil;
        }
        public bool GuardarEvaluacion(List<SegVialResultado> ListaResultados)
        {
            bool ProbarGuardado = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                foreach (var item in ListaResultados)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                    ProbarGuardado = true;
                }
                catch (Exception ex)
                {
                }
            }
            return ProbarGuardado;
        }

        public bool VerificarEstado(int IdSegVial)
        {
            bool ProbarEstado = false;
            int contNolleno = 0;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_SegVialResultado
                                where s.Fk_Id_PlanVial == IdSegVial
                                select s).ToList<SegVialResultado>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        if (item.Aplica_s==0 || item.Existencia_s==0 || item.Responde_s==0)
                        {
                            contNolleno++;
                        }
                    }

                }
            }
            if (contNolleno>0)
            {
                ProbarEstado = false;
            }
            else
            {
                ProbarEstado = true;
            }
            return ProbarEstado;
        }

        public List<EDSede> ObtenernerSedesPorEmpresa(int fk_empresa)
        {
            List<EDSede> Sedes = new List<EDSede>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Sedes = (from s in context.Tbl_Sede
                         join e in context.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                         where e.Pk_Id_Empresa == fk_empresa
                         select new EDSede
                         {
                             DireccionSede = s.Direccion_Sede,
                             IdEmpresa = e.Pk_Id_Empresa,
                             IdSede = s.Pk_Id_Sede,
                             NombreSede = s.Nombre_Sede,
                             Sector = s.Sector
                         }).ToList();
            }
            return Sedes;
        }
    }
}
