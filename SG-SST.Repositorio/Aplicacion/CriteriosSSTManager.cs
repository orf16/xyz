
namespace SG_SST.Repositorio.Aplicacion
{
    using SG_SST.Audotoria;
    using SG_SST.EntidadesDominio.Aplicacion;
    using SG_SST.Interfaces.Aplicacion;
    using SG_SST.Models;
    using SG_SST.Models.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Text;
    //using System.
    using System.Threading.Tasks;
    using System.Configuration;
    public class CriteriosSSTManager : ICriterioSST
    {
   
        public List<EDCriteriosSST> ObtenerCriteriosSST()
        {
            List<EDCriteriosSST> Crit = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Crit = (from ma in context.Tbl_CriterioSST
                           select new EDCriteriosSST
                           {
                               IdCriterioSST = ma.PK_CriterioSST,
                               NombreCriterioSST = ma.Criterio,
                           }).ToList();
            }
            return Crit;
        }

        public List<EDProductoPorCriterioSSt> ObtenerCriteriosSSTSeleccionados(int id)
        {
            List<EDProductoPorCriterioSSt> CritSel = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                CritSel = (from ma in context.Tbl_Producto_Por_Criterio
                        where ma.Fk_Id_ServicioOProducto == id
                           select new EDProductoPorCriterioSSt
                        {
                            idCriterioSSt = ma.Fk_Id__CriterioSST, 
                        }).ToList();
            }
            return CritSel;
        }

        public bool GuardarProductoCriterio(EDProductoCriterio productoCriterio)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ServicioOProducto servicioOProducto = new ServicioOProducto();
                        servicioOProducto.Nombre_ServicioOProducto = productoCriterio.Tipo_Servicio;
                        servicioOProducto.FK_Empresa = productoCriterio.Fk_Empresa;
                        context.Tbl_ServicioOProducto.Add(servicioOProducto);
                        context.SaveChanges();
                        List<ProductoPorCriterio> productoPorCriterio = new List<ProductoPorCriterio>();                       
                        foreach (var ppc in productoCriterio.Pk_Id_Criterio1)
                        {                            
                            ProductoPorCriterio productoCriterios = new ProductoPorCriterio();
                            productoCriterios.Fk_Id_ServicioOProducto = servicioOProducto.PK_ServicioOProducto;
                            productoCriterios.Fk_Id__CriterioSST = ppc;
                            productoPorCriterio.Add(productoCriterios);
                        }
                        context.Tbl_Producto_Por_Criterio.AddRange(productoPorCriterio);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al guardar Productos y Criterios en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool GuardarSeleccionYEvaluacion(EDSeleccionYEvaluacion seleccionEvaluacion)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ProveedorContratista proveedorContratista = new ProveedorContratista();
                        if(context.Tbl_ProveedorContratista.Any(x => x.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && x.IdEmpresa == seleccionEvaluacion.IdEmpresa))
                        {
                            proveedorContratista = context.Tbl_ProveedorContratista.Where(v => v.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && v.IdEmpresa == seleccionEvaluacion.IdEmpresa).FirstOrDefault();
                        }
                        else
                        {                            
                            proveedorContratista.Nombre_ProveedorContratista = seleccionEvaluacion.nameProveedor;
                            proveedorContratista.Nit_ProveedorContratista = seleccionEvaluacion.nitProveedor;
                            proveedorContratista.IdEmpresa = seleccionEvaluacion.IdEmpresa;
                            proveedorContratista.VigenciaContrato = seleccionEvaluacion.vigencia;
                            context.Tbl_ProveedorContratista.Add(proveedorContratista);
                            context.SaveChanges();
                        }
                        var CantidadProvee = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(v => v.ProveedorContratista.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && v.ProveedorContratista.IdEmpresa == seleccionEvaluacion.IdEmpresa).ToList();
                        int NumeroCalif = CantidadProvee.Count();
                        CalificacionProveedor calificacionProveedor = new CalificacionProveedor();
                        calificacionProveedor.Fecha_Calificacion = seleccionEvaluacion.fechapi;
                        calificacionProveedor.ResultadoCalificacion = seleccionEvaluacion.calif;
                        calificacionProveedor.Observaciones = seleccionEvaluacion.observacion;
                        if (NumeroCalif == 0)
                        {
                            calificacionProveedor.NumeroCalificion = 1;
                        }
                        else
                        {
                            calificacionProveedor.NumeroCalificion = NumeroCalif + 1;
                        }

                        calificacionProveedor.ProveedorPorAnexo = new List<ProveedorPorAnexo>();
                        List<ArchivosAnexos> archAne = new List<ArchivosAnexos>();
                        ProveedorPorAnexo proveeAnex = new ProveedorPorAnexo();
                        foreach (var archi in seleccionEvaluacion.ListaArchivos)
                        {
                            ArchivosAnexos archiv = new ArchivosAnexos();
                            archiv.ProveedorPorAnexo = new List<ProveedorPorAnexo>();
                            ProveedorPorAnexo ppa = new ProveedorPorAnexo();
                            ppa.CalificacionProveedor = calificacionProveedor;
                            archiv.ProveedorPorAnexo.Add(ppa);
                            archiv.rutaAnexos = archi;
                            archAne.Add(archiv);
                        }
                        context.Tbl_Archivos_Anexos.AddRange(archAne);
                        context.SaveChanges();                       
                     
                        List<ProveedorPorProducto> provPro = new List<ProveedorPorProducto>();                        
                        foreach (var ppp in seleccionEvaluacion.ListaProCritPorCalf)
                        {                            
                            if (ppp.idServicioProducto != 0)
                            {
                                ProveedorPorProducto pvpp = new ProveedorPorProducto();
                                pvpp.Fk_Id_ProveedorContratista = proveedorContratista.PK_ProveedorContratista;
                                pvpp.Fk_Id_ServicioOProducto = ppp.idServicioProducto;
                                provPro.Add(pvpp);
                            }
                            
                        }
                        context.Tbl_Proveedor_Por_Producto.AddRange(provPro);
                        context.SaveChanges();

                        ProveedorPorNumeroCalificacion proveedorPorCalif = new ProveedorPorNumeroCalificacion();
                        proveedorPorCalif.Fk_Id_ProveedorContratista = proveedorContratista.PK_ProveedorContratista;
                        proveedorPorCalif.Fk_Id_CalificacionProveedor = calificacionProveedor.PK_CalificacionProveedor;
                        context.Tbl_Proveedor_Por_NumeroCalificacion.Add(proveedorPorCalif);
                        context.SaveChanges();

                        List<ProveedorPorProductoPorCriterio> proveedorPorProductoPorCriterio = new List<ProveedorPorProductoPorCriterio>();
                        foreach (var pcpc in seleccionEvaluacion.ListaProCritPorCalf)
                        {
                            ProveedorPorProductoPorCriterio proveedorPorProducto = new ProveedorPorProductoPorCriterio();
                            proveedorPorProducto.Fk_Id_ProveedorPorNumeroCalificacion = proveedorPorCalif.PK_ProveedorPorNumeroCalificacion;
                            proveedorPorProducto.Fk_Id_ProductoPorCriterio = pcpc.IdProductoCriterios;
                            proveedorPorProducto.Calificacion = pcpc.ddlViewBy;
                            proveedorPorProducto.CalificacionProducto = pcpc.califProducto;
                            proveedorPorProductoPorCriterio.Add(proveedorPorProducto);
                        }
                        context.Tbl_Proveedor_ProductoPorCriterio.AddRange(proveedorPorProductoPorCriterio);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al guardar el Contratista y la Calificacion de los CriteriosSST en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool EditarSeleccionYEvaluacion(EDSeleccionYEvaluacion seleccionEvaluacion)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        //ProveedorPorNumeroCalificacion ppncal = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(j => j.PK_ProveedorPorNumeroCalificacion == seleccionEvaluacion.PK_ProveedorPorNumeroCalificacion).FirstOrDefault();                                                                       
                        //var CantidadProvee = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(v => v.ProveedorContratista.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && v.ProveedorContratista.IdEmpresa == seleccionEvaluacion.IdEmpresa).ToList();
                        //int NumeroCalif = CantidadProvee.Count();                        
                        //CalificacionProveedor calificacionProveedor = new CalificacionProveedor();
                        //calificacionProveedor.Fecha_Calificacion = seleccionEvaluacion.fechapi;
                        //calificacionProveedor.ResultadoCalificacion = seleccionEvaluacion.calif;
                        //calificacionProveedor.Observaciones = seleccionEvaluacion.observacion;
                        //calificacionProveedor.PK_CalificacionProveedor = ppncal.Fk_Id_CalificacionProveedor;                        
                        //if (NumeroCalif == 0)
                        //{
                        //    calificacionProveedor.NumeroCalificion = 1;
                        //}
                        //else
                        //{
                        //    calificacionProveedor.NumeroCalificion = NumeroCalif + 1;
                        //}                       
                        //context.Entry(calificacionProveedor).State = EntityState.Modified;
                        //context.SaveChanges();

                        //Eliminar proveedor calificado.
                        ProveedorPorNumeroCalificacion proveCalf = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(j => j.PK_ProveedorPorNumeroCalificacion == seleccionEvaluacion.PK_ProveedorPorNumeroCalificacion).FirstOrDefault();
                        CalificacionProveedor calif = context.Tbl_CalificacionProveedor.Where(y => y.PK_CalificacionProveedor == proveCalf.Fk_Id_CalificacionProveedor).FirstOrDefault();
                        List<ProveedorPorAnexo> proAnex = context.Tbl_Proveedor_Por_Anexo.Where(x => x.Fk_Id_CalificacionProveedor == calif.PK_CalificacionProveedor).ToList();
                        int conta = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(m => m.Fk_Id_ProveedorContratista == proveCalf.Fk_Id_ProveedorContratista).ToList().Count();
                        ProveedorContratista provCont = context.Tbl_ProveedorContratista.Where(k => k.PK_ProveedorContratista == proveCalf.Fk_Id_ProveedorContratista).FirstOrDefault();
                        List<ArchivosAnexos> archivList = new List<ArchivosAnexos>();
                        foreach (var rem in proAnex)
                        {
                            ArchivosAnexos archivAn = context.Tbl_Archivos_Anexos.Where(t => t.PK_Archivos_Anexos == rem.Fk_Id_Archivos_Anexos).FirstOrDefault();
                            archivList.Add(archivAn);
                        }
                        context.Tbl_Archivos_Anexos.RemoveRange(archivList);
                        if (conta == 1)
                        {
                            context.Tbl_ProveedorContratista.Remove(provCont);
                        }
                        context.Tbl_CalificacionProveedor.Remove(calif);
                        context.SaveChanges();
                        
                        //Guardar nuevo proveedor calificado
                        ProveedorContratista proveedorContratista = new ProveedorContratista();
                        if (context.Tbl_ProveedorContratista.Any(x => x.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && x.IdEmpresa == seleccionEvaluacion.IdEmpresa))
                        {
                            proveedorContratista = context.Tbl_ProveedorContratista.Where(v => v.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && v.IdEmpresa == seleccionEvaluacion.IdEmpresa).FirstOrDefault();
                        }
                        else
                        {
                            proveedorContratista.Nombre_ProveedorContratista = seleccionEvaluacion.nameProveedor;
                            proveedorContratista.Nit_ProveedorContratista = seleccionEvaluacion.nitProveedor;
                            proveedorContratista.VigenciaContrato = seleccionEvaluacion.vigencia;
                            proveedorContratista.IdEmpresa = seleccionEvaluacion.IdEmpresa;
                            context.Tbl_ProveedorContratista.Add(proveedorContratista);
                            context.SaveChanges();
                        }
                        var CantidadProvee = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(v => v.ProveedorContratista.Nit_ProveedorContratista == seleccionEvaluacion.nitProveedor && v.ProveedorContratista.IdEmpresa == seleccionEvaluacion.IdEmpresa).ToList();
                        int NumeroCalif = CantidadProvee.Count();
                        CalificacionProveedor calificacionProveedor = new CalificacionProveedor();
                        calificacionProveedor.Fecha_Calificacion = seleccionEvaluacion.fechapi;
                        calificacionProveedor.ResultadoCalificacion = seleccionEvaluacion.calif;
                        calificacionProveedor.Observaciones = seleccionEvaluacion.observacion;
                        if (NumeroCalif == 0)
                        {
                            calificacionProveedor.NumeroCalificion = 1;
                        }
                        else
                        {
                            calificacionProveedor.NumeroCalificion = NumeroCalif + 1;
                        }

                        calificacionProveedor.ProveedorPorAnexo = new List<ProveedorPorAnexo>();
                        List<ArchivosAnexos> archAne = new List<ArchivosAnexos>();
                        ProveedorPorAnexo proveeAnex = new ProveedorPorAnexo();
                        foreach (var archi in seleccionEvaluacion.ListaArchivos)
                        {
                            ArchivosAnexos archiv = new ArchivosAnexos();
                            archiv.ProveedorPorAnexo = new List<ProveedorPorAnexo>();
                            ProveedorPorAnexo ppa = new ProveedorPorAnexo();
                            ppa.CalificacionProveedor = calificacionProveedor;
                            archiv.ProveedorPorAnexo.Add(ppa);
                            archiv.rutaAnexos = archi;
                            archAne.Add(archiv);
                        }
                        context.Tbl_Archivos_Anexos.AddRange(archAne);
                        context.SaveChanges();

                        List<ProveedorPorProducto> provPro = new List<ProveedorPorProducto>();
                        foreach (var ppp in seleccionEvaluacion.ListaProCritPorCalf)
                        {
                            if (ppp.idServicioProducto != 0)
                            {
                                ProveedorPorProducto pvpp = new ProveedorPorProducto();
                                pvpp.Fk_Id_ProveedorContratista = proveedorContratista.PK_ProveedorContratista;
                                pvpp.Fk_Id_ServicioOProducto = ppp.idServicioProducto;
                                provPro.Add(pvpp);
                            }

                        }
                        context.Tbl_Proveedor_Por_Producto.AddRange(provPro);
                        context.SaveChanges();

                        ProveedorPorNumeroCalificacion proveedorPorCalif = new ProveedorPorNumeroCalificacion();
                        proveedorPorCalif.Fk_Id_ProveedorContratista = proveedorContratista.PK_ProveedorContratista;
                        proveedorPorCalif.Fk_Id_CalificacionProveedor = calificacionProveedor.PK_CalificacionProveedor;
                        context.Tbl_Proveedor_Por_NumeroCalificacion.Add(proveedorPorCalif);
                        context.SaveChanges();

                        List<ProveedorPorProductoPorCriterio> proveedorPorProductoPorCriterio = new List<ProveedorPorProductoPorCriterio>();
                        foreach (var pcpc in seleccionEvaluacion.ListaProCritPorCalf)
                        {
                            ProveedorPorProductoPorCriterio proveedorPorProducto = new ProveedorPorProductoPorCriterio();
                            proveedorPorProducto.Fk_Id_ProveedorPorNumeroCalificacion = proveedorPorCalif.PK_ProveedorPorNumeroCalificacion;
                            proveedorPorProducto.Fk_Id_ProductoPorCriterio = pcpc.IdProductoCriterios;
                            proveedorPorProducto.Calificacion = pcpc.ddlViewBy;
                            proveedorPorProducto.CalificacionProducto = pcpc.califProducto;
                            proveedorPorProductoPorCriterio.Add(proveedorPorProducto);
                        }
                        context.Tbl_Proveedor_ProductoPorCriterio.AddRange(proveedorPorProductoPorCriterio);
                        context.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al guardar el Contratista y la Calificacion de los CriteriosSST en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool EditarProductoCriterio(EDProductoCriterio productocriterio)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {                        

                        ServicioOProducto servicioOProducto = new ServicioOProducto();
                        servicioOProducto.Nombre_ServicioOProducto = productocriterio.Tipo_Servicio;
                        servicioOProducto.PK_ServicioOProducto = productocriterio.Pk_id_Tipo_Servicio;
                        servicioOProducto.FK_Empresa = productocriterio.Fk_Empresa;
                        context.Entry(servicioOProducto).State = EntityState.Modified;
                        
                        List<ProductoPorCriterio> productoPorCriterios = context.Tbl_Producto_Por_Criterio.Where(pc => pc.Fk_Id_ServicioOProducto == servicioOProducto.PK_ServicioOProducto).ToList();
                        context.Tbl_Producto_Por_Criterio.RemoveRange(productoPorCriterios);
                        List<ProductoPorCriterio> productoPorCrits = new List<ProductoPorCriterio>();                       

                        foreach(var item in productocriterio.Pk_Id_Criterio1)
                        {
                            ProductoPorCriterio productoPorCriterio = new ProductoPorCriterio();
                            productoPorCriterio.Fk_Id_ServicioOProducto = productocriterio.Pk_id_Tipo_Servicio;
                            productoPorCriterio.Fk_Id__CriterioSST = item;
                            productoPorCrits.Add(productoPorCriterio);
                        }

                        context.Tbl_Producto_Por_Criterio.AddRange(productoPorCrits);                        
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al guardar Productos y Criterios en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<EDServicioProducto> ObtenerProductosPorCriterios(int idEmpresa)
        {
            List<EDServicioProducto> Pro_Crit = null;            
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Pro_Crit = (from s in context.Tbl_ServicioOProducto
                            where s.FK_Empresa == idEmpresa
                            select new EDServicioProducto { 
                                idServicioProducto = s.PK_ServicioOProducto,
                                DescripcionProducto = s.Nombre_ServicioOProducto,
                                CriterioLista = s.ProductoPorCriterio.Select(ppc => new EDCriteriosSST
                                                {
                                                    IdCriterioSST = ppc.CriterioSST.PK_CriterioSST,
                                                    NombreCriterioSST = ppc.CriterioSST.Criterio,
                                                }
                                                ).ToList()
                            }).ToList();               
            }           
            return Pro_Crit;
        }

        public List<EDSeleccionYEvaluacion> ObtenerProveedoresContratistas(int idEmpresa)
        {
            List<EDSeleccionYEvaluacion> Prov_Contrat = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Prov_Contrat = (from pnc in context.Tbl_Proveedor_Por_NumeroCalificacion
                                //join p in context.Tbl_Proveedor_Por_Producto on pnc.Fk_Id_ProveedorContratista equals p.Fk_Id_ProveedorContratista
                                where pnc.ProveedorContratista.IdEmpresa == idEmpresa
                                select new EDSeleccionYEvaluacion
                                {
                                    idProveedorContratista = pnc.ProveedorContratista.PK_ProveedorContratista,
                                    PK_ProveedorPorNumeroCalificacion = pnc.PK_ProveedorPorNumeroCalificacion,
                                    nameProveedor = pnc.ProveedorContratista.Nombre_ProveedorContratista,
                                    nitProveedor = pnc.ProveedorContratista.Nit_ProveedorContratista,
                                    fechapi = pnc.CalificacionProveedor.Fecha_Calificacion,
                                    calif = pnc.CalificacionProveedor.ResultadoCalificacion,
                                    observacion = pnc.CalificacionProveedor.Observaciones,
                                    ListaArchivos = pnc.CalificacionProveedor.ProveedorPorAnexo.Select(ppa => ppa.ArchivosAnexos.rutaAnexos).ToList(),
                                    NumeroCalificion = pnc.CalificacionProveedor.NumeroCalificion,
                                    ListaProCritPorCalf = pnc.ProveedorPorProductoPorCriterio.Select(x => new EDCalificacionInt
                                    {
                                        //idServicioProducto = x.ProductoPorCriterio.Fk_Id_ServicioOProducto,
                                        Nombre_ServicioOProducto = x.ProductoPorCriterio.ServicioOProducto.Nombre_ServicioOProducto,
                                        //IdProductoCriterios = x.ProductoPorCriterio.Fk_Id__CriterioSST,
                                        //ddlViewBy = x.Calificacion
                                    }).ToList(),
                                    //ListaProCritPorCalf.GroupBy(i => i.Nombre_ServicioOProducto)
                                    //.Select(group => group.First()).ToList(),
                                }).ToList();
            }
            //return Prov_Contrat.GroupBy(i => i.idProveedorContratista)
            //                   .Select(group => group.First())
            //                   .ToList();
            return Prov_Contrat;
        }

        public List<EDProveedorContratista> ObtenerListaProveedores(int idEmpresa)
        {
            List<EDProveedorContratista> Proveedores = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Proveedores = (from pc in context.Tbl_ProveedorContratista
                               where pc.IdEmpresa == idEmpresa
                               select new EDProveedorContratista
                               {
                                   PK_ProveedorContratista = pc.PK_ProveedorContratista,
                                   Nombre_ProveedorContratista = pc.Nombre_ProveedorContratista,
                                   Nit_ProveedorContratista = pc.Nit_ProveedorContratista,
                                   CalificacionHistorico = pc.CalificacionHist,
                                   fechapi  = pc.VigenciaContrato,
                                   frecuenciaEvaluacion = pc.FrecuenciaEval
                               }).ToList();
            }
                return Proveedores;
        }

        public EDServicioProducto ObtenerProducto(int idProducto)
        {
            EDServicioProducto Pro_Crit = null;            
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Pro_Crit = (from s in context.Tbl_ServicioOProducto
                            where s.PK_ServicioOProducto == idProducto
                            select new EDServicioProducto { 
                                idServicioProducto = s.PK_ServicioOProducto,
                                DescripcionProducto = s.Nombre_ServicioOProducto,
                                selectProdCrit =  s.ProductoPorCriterio.Select(p => p.Fk_Id__CriterioSST).ToList(),
                                CriterioLista = s.ProductoPorCriterio.Select(ppc => new EDCriteriosSST
                                                {
                                                    IdCriterioSST = ppc.CriterioSST.PK_CriterioSST,
                                                    NombreCriterioSST = ppc.CriterioSST.Criterio,
                                                    IdProductoCriterios = ppc.Id_Pk_ProductoPorCriterio,
                                                }
                                ).ToList()
                            }).FirstOrDefault();
                
            }            
            return Pro_Crit;

        }

        public EDSeleccionYEvaluacion ObtenerProveedorContratistaEditar(int idProveePorCalif)
        {
            EDSeleccionYEvaluacion Prov_Contrat = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Prov_Contrat = (from pnc in context.Tbl_Proveedor_Por_NumeroCalificacion
                                where pnc.PK_ProveedorPorNumeroCalificacion == idProveePorCalif
                                select new EDSeleccionYEvaluacion
                                {
                                    idProveedorContratista = pnc.ProveedorContratista.PK_ProveedorContratista,
                                    PK_ProveedorPorNumeroCalificacion = pnc.PK_ProveedorPorNumeroCalificacion,
                                    nameProveedor = pnc.ProveedorContratista.Nombre_ProveedorContratista,
                                    nitProveedor = pnc.ProveedorContratista.Nit_ProveedorContratista,
                                    fechapi = pnc.CalificacionProveedor.Fecha_Calificacion,
                                    calif = pnc.CalificacionProveedor.ResultadoCalificacion,
                                    observacion = pnc.CalificacionProveedor.Observaciones,
                                    ListaArchivos = pnc.CalificacionProveedor.ProveedorPorAnexo.Select(ppa => ppa.ArchivosAnexos.rutaAnexos).ToList(),
                                    NumeroCalificion = pnc.CalificacionProveedor.NumeroCalificion,
                                    ListaProCritPorCalf = pnc.ProveedorPorProductoPorCriterio.Select(x => new EDCalificacionInt
                                    {
                                        idServicioProducto = x.ProductoPorCriterio.Fk_Id_ServicioOProducto,
                                        Nombre_ServicioOProducto = x.ProductoPorCriterio.ServicioOProducto.Nombre_ServicioOProducto,
                                        IdProductoCriterios = x.ProductoPorCriterio.Id_Pk_ProductoPorCriterio,
                                        NombreCriterio = x.ProductoPorCriterio.CriterioSST.Criterio,
                                        ddlViewBy = x.Calificacion,
                                        califProducto = x.CalificacionProducto
                                    }).ToList(),                                   
                                }).FirstOrDefault();
            }
           return Prov_Contrat;
        }

        public List<EDSeleccionYEvaluacion> ObtenerProveedorContratistaGraficar(int idProveedor)
        {
            List<EDSeleccionYEvaluacion> Prov_Contrat = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Prov_Contrat = (from pnc in context.Tbl_Proveedor_Por_NumeroCalificacion.AsEnumerable()
                                where pnc.Fk_Id_ProveedorContratista == idProveedor
                                select new EDSeleccionYEvaluacion
                                {
                                    nameProveedor = pnc.ProveedorContratista.Nombre_ProveedorContratista,
                                    nitProveedor = pnc.ProveedorContratista.Nit_ProveedorContratista,
                                    fechapi = pnc.CalificacionProveedor.Fecha_Calificacion,
                                    calif = pnc.CalificacionProveedor.ResultadoCalificacion,
                                    NumeroCalificion = pnc.CalificacionProveedor.NumeroCalificion,
                                    fecha = pnc.CalificacionProveedor.Fecha_Calificacion.ToString("dd/MM/yyyy"),
                                }).ToList();
            }
            return Prov_Contrat;
        }

        public bool EditarProveedor(EDProveedorContratista ProveedorContratista)
        {
             using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        //ProveedorContratista proveedorContratista1 = new ProveedorContratista();
                        var proveeCont = context.Tbl_ProveedorContratista.Where(y => y.PK_ProveedorContratista == ProveedorContratista.PK_ProveedorContratista).First();
                        proveeCont.Nombre_ProveedorContratista = ProveedorContratista.Nombre_ProveedorContratista;
                        proveeCont.Nit_ProveedorContratista = ProveedorContratista.Nit_ProveedorContratista;
                        proveeCont.IdEmpresa = ProveedorContratista.idEmpresa;
                        proveeCont.CalificacionHist = ProveedorContratista.CalificacionHistorico;
                        proveeCont.FrecuenciaEval = ProveedorContratista.frecuenciaEvaluacion;
                        proveeCont.VigenciaContrato = ProveedorContratista.fechapi;
                        context.Entry(proveeCont).State = EntityState.Modified;
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al editar el proveedor en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }

        }

        public EDProveedorContratista ObtenerProveedor(int idProveedor)
        {
            EDProveedorContratista Prov_Contrat = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Prov_Contrat = (from pc in context.Tbl_ProveedorContratista
                                where pc.PK_ProveedorContratista == idProveedor
                                select new EDProveedorContratista
                                {
                                    PK_ProveedorContratista = pc.PK_ProveedorContratista,
                                    Nombre_ProveedorContratista = pc.Nombre_ProveedorContratista,
                                    Nit_ProveedorContratista = pc.Nit_ProveedorContratista,
                                    CalificacionHistorico = pc.CalificacionHist,
                                    fechapi = pc.VigenciaContrato,
                                    frecuenciaEvaluacion = pc.FrecuenciaEval
                                }).FirstOrDefault();
            }
            return Prov_Contrat;
        }

        public bool EliminarCalificacionProveedor(int idProveePorCalif)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {                        
                        ProveedorPorNumeroCalificacion proveCalf = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(z => z.PK_ProveedorPorNumeroCalificacion == idProveePorCalif).FirstOrDefault();
                        CalificacionProveedor calif = context.Tbl_CalificacionProveedor.Where(y => y.PK_CalificacionProveedor == proveCalf.Fk_Id_CalificacionProveedor).FirstOrDefault();
                        List<ProveedorPorAnexo> proAnex = context.Tbl_Proveedor_Por_Anexo.Where(x => x.Fk_Id_CalificacionProveedor == calif.PK_CalificacionProveedor).ToList();
                        int conta = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(m => m.Fk_Id_ProveedorContratista == proveCalf.Fk_Id_ProveedorContratista).ToList().Count();
                        ProveedorContratista provCont = context.Tbl_ProveedorContratista.Where(k => k.PK_ProveedorContratista == proveCalf.Fk_Id_ProveedorContratista).FirstOrDefault();
                        List<ArchivosAnexos> archivList = new List<ArchivosAnexos>();
                        foreach(var rem in proAnex)
                        {
                            ArchivosAnexos archivAn = context.Tbl_Archivos_Anexos.Where(t => t.PK_Archivos_Anexos == rem.Fk_Id_Archivos_Anexos).FirstOrDefault();
                            archivList.Add(archivAn);
                        }
                        context.Tbl_Archivos_Anexos.RemoveRange(archivList);
                        if(conta == 1)
                        {
                            context.Tbl_ProveedorContratista.Remove(provCont);
                        }
                        context.Tbl_CalificacionProveedor.Remove(calif);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al eliminar la calificacion del proveedor en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<string> ConsultarAnexosProveedor(int idProveePorCalif)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                   
                        ProveedorPorNumeroCalificacion proveCalf = context.Tbl_Proveedor_Por_NumeroCalificacion.Where(z => z.PK_ProveedorPorNumeroCalificacion == idProveePorCalif).FirstOrDefault();
                        CalificacionProveedor calif = context.Tbl_CalificacionProveedor.Where(y => y.PK_CalificacionProveedor == proveCalf.Fk_Id_CalificacionProveedor).FirstOrDefault();
                        List<ProveedorPorAnexo> proAnex = context.Tbl_Proveedor_Por_Anexo.Where(x => x.Fk_Id_CalificacionProveedor == calif.PK_CalificacionProveedor).ToList();
                        ProveedorContratista provCont = context.Tbl_ProveedorContratista.Where(k => k.PK_ProveedorContratista == proveCalf.Fk_Id_ProveedorContratista).FirstOrDefault();
                        List<string> archivList = new List<string>();
                        foreach (var rem in proAnex)
                        {
                            ArchivosAnexos archivAn = context.Tbl_Archivos_Anexos.Where(t => t.PK_Archivos_Anexos == rem.Fk_Id_Archivos_Anexos).FirstOrDefault();
                            archivList.Add(archivAn.rutaAnexos);
                        }

                        return archivList;
                }
            }
        }

        public bool EliminarProductoCriterio(int idProducto)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ServicioOProducto product = context.Tbl_ServicioOProducto.Where(p => p.PK_ServicioOProducto == idProducto).FirstOrDefault();
                        context.Tbl_ServicioOProducto.Remove(product);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(CriteriosSSTManager), string.Format("Error al eliminar el ProductoPorCriterio en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

    }
}
