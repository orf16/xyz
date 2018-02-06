using SG_SST.Models;
using SG_SST.Repositories.Empresas.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using SG_SST.Models.Empleado;
using SG_SST.Models.Empresas;

namespace SG_SST.Repositories.Empresas.Repositories
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        SG_SSTContext db;
        public string Error;
        public EmpresaRepositorio()
        {
            db = new SG_SSTContext();
        }
        public EmpresaRepositorio(SG_SSTContext db)///mandar el contexto que se va utilizar en las otras Capas.
        {
            this.db = db;
        }

        
        public Organigrama ObtenerOrganigrama(int Pk_Id_Empresa)
        {
            //Entity FrameWork
            List<Organigrama> organigramas = db.Tbl_Organigrama.Include(o => o.EmpleadosOrg).Where(o => o.Fk_Id_Empresa == Pk_Id_Empresa).ToList();//Busca los organigramas con el Pk de una Empresa en la Base de datos

            if (organigramas.Count > 0)// si hay organigramas ?
            {
                return organigramas.FirstOrDefault(); //retorna el primero
            }
            return null; //sino retorna NULL
        }

        public bool GuardarEmpresa(Empresa Empresas, Sede sede, SedeMunicipio sedemunicipio)
        {
            using (var transaction = db.Database.BeginTransaction())
            {

                try
                {
                    List<Empresa> empresas = db.Tbl_Empresa

                       .Where(rs => rs.Pk_Id_Empresa > 0).ToList();




                    foreach (var rs in empresas)
                    {

                        if (rs.Nit_Empresa == Empresas.Nit_Empresa)
                        {

                            return false;
                        
                           
                        }


                    }

                    if (Empresas.Pk_Id_Empresa > 0)
                    {
                        ModificarEmpresa(Empresas);
                    }
                    else
                    {
                        sede.SedeMunicipios = new List<SedeMunicipio>();
                        sede.SedeMunicipios.Add(sedemunicipio);
                        sede.Nombre_Sede = "Principal";

                        Empresas.sedes = new List<Sede>();
                        Empresas.sedes.Add(sede);
                        db.Tbl_Empresa.Add(Empresas);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }


        }

        public void ModificarEmpresa(Empresa Empresas)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    if (Empresas != null)
                    {
                        db.Entry(Empresas).State = EntityState.Modified;

                    }
                    else
                    {

                        db.Tbl_Empresa.Add(Empresas);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }
        }
        public bool GuardarOrganigrama(EmpleadoOrg empleadoorg, int Pk_Id_Empresa)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Organigrama organigrama = ObtenerOrganigrama(Pk_Id_Empresa);

                    if (organigrama != null)
                    {

                        organigrama.EmpleadosOrg.Add(empleadoorg);
                    }
                    else
                    {
                        Organigrama org = new Organigrama();
                        org.Fk_Id_Empresa = Pk_Id_Empresa;
                        org.EmpleadosOrg = new List<EmpleadoOrg>();
                        org.EmpleadosOrg.Add(empleadoorg);

                        db.Tbl_Organigrama.Add(org);

                    }
                    db.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }


        /// <summary>
        /// busqueda de empleados por estado_empleado y tipo_cotizante modulo relaciones laborales
        /// </summary>
        /// <param name="EstadoEmpleado"></param>
        /// <param name="TipoCotizante"></param>
        /// <returns></returns>
        public List<tblEmpleado> Busqueda_RelacionesLaborales(int EstadoEmpleado, int TipoCotizante)
        {

            var Busqueda_Relaciones2 = db.tblEmpleado.Where(g => g.FK_ID_Estado == EstadoEmpleado && g.FK_ID_Tipo_Cotizante == TipoCotizante);

            return db.tblEmpleado.Where(g => g.FK_ID_Estado == EstadoEmpleado && g.FK_ID_Tipo_Cotizante == TipoCotizante).ToList();

        }


    }

}