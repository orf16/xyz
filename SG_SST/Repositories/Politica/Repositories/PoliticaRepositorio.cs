using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.Models.Politica;
using SG_SST.Repositories.Politica.IRepositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Models.Revision;


namespace SG_SST.Repositories.Politica.Repositories
{
    public class PoliticaRepositorio : IPoliticaRepositorio
    {
        SG_SSTContext dbPol;
        public string Mensage;
        public string Error;
        public PoliticaRepositorio()
        {
            dbPol = new SG_SSTContext();
        }

        internal SG_SST.Repositories.Politica.IRepositories.IPoliticaRepositorio IPoliticaRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        /// <summary>
        /// Método paea crear una política de seguridad y salud en el trabajo 
        /// /// </summary>

        public bool GrabarPolitica(mPolitica politica)
        {
            try
            {
                if (politica.Archivo_Politica != null || politica.strDescripcion_Politica != null)
                {


                    dbPol.Tbl_Politica.Add(politica);
                    dbPol.SaveChanges();
                    return true;
                }
                else
                {
                    return false;

                }

            }
            catch (Exception)
            {
                return false;
                throw;

            }

        }


        public bool EliminaPolitica(int ID_Empresa)
        {
            try
            {
                //mPolitica mobjPolitica= dbPol.Tbl_Politica.Find(ID_Empresa);
                mPolitica pol = dbPol.Tbl_Politica.Where(g => g.FK_Empresa == ID_Empresa).FirstOrDefault();

                if (pol.strDescripcion_Politica != null || pol.Archivo_Politica != null)
                {

                    dbPol.Tbl_Politica.Remove(pol);
                    dbPol.SaveChanges();


                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                Error = "Ingrese una Política de Seguridad y Salud en el Trabajo";
                return false;
            }
        }

        public mPolitica ObtenerPolitica(int ID_Empresa)
        {

            //Entity FrameWork
            mPolitica pol = dbPol.Tbl_Politica.Where(g => g.FK_Empresa == ID_Empresa).FirstOrDefault();
            // linq     
            mPolitica objpol = (from g in dbPol.Tbl_Politica
                                where g.FK_Empresa == ID_Empresa
                                select g).FirstOrDefault();
            return objpol;
        }

        public ActaRevision ObtenerActa(int ID_Empresa, int IdActa)
        {

            //Entity FrameWork
            ActaRevision act = dbPol.Tbl_ActaRevision.Where(g => g.Fk_Id_Empresa == ID_Empresa & g.PK_Id_ActaRevision == IdActa).FirstOrDefault();
            // linq     
            //// mPolitica acts = (from g in dbPol.Tbl_ActaRevision
            //                     where g.Fk_Id_Empresa == ID_Empresa
            //                     select g).FirstOrDefault();
            return act;
        }


        public string ObtenerPoliticaEmpresa(int ID_Empresa)
        {
            string Politica_Empresa = "";
            var politicas = dbPol.Tbl_Politica.Where(g => g.FK_Empresa == ID_Empresa);
            if (politicas != null)
            {

                try
                {
                    Politica_Empresa = politicas.FirstOrDefault().strDescripcion_Politica;
                }
                catch (Exception)
                {
                    Error = "Ingrese una Política de Seguridad y Salud en el Trabajo";
                    return "";
                }


            }
            return Politica_Empresa;
        }


        public bool ModficarPolitica(mPolitica politica)
        {
            using (var transaction = dbPol.Database.BeginTransaction())
            {
                try
                {
                    if (politica.strDescripcion_Politica != null)
                    {

                        dbPol.Entry(politica).State = EntityState.Modified;
                        dbPol.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool ModficarActaRevision(ActaRevision acta)
        {
            using (var transaction = dbPol.Database.BeginTransaction())
            {
                try
                {
                    if (acta.Firma_Representante_SGSST != null)
                    {

                        dbPol.Entry(acta).State = EntityState.Modified;
                        dbPol.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool ModficarActaRevisionR(ActaRevision acta)
        {
            using (var transaction = dbPol.Database.BeginTransaction())
            {
                try
                {
                    if (acta.Firma_Representante_SGSST != null)
                    {

                        dbPol.Entry(acta).State = EntityState.Modified;
                        dbPol.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool GrabarOtrasInteracciones(Mod_OtrasInteracciones OtrasInteracciones)
        {
            if (OtrasInteracciones.Archivo_OtrasInteracciones != null)
            {

                //OtrasInteracciones.FK_Empresa = SessionDataSG_SST.EmpresaSession.Pk_Id_Empresa;
                dbPol.Tbl_OtrasInteracciones.Add(OtrasInteracciones);
                dbPol.SaveChanges();
                return true;
            }
            return false;
        }


        public bool EliminarOtrasInteracciones(int ID_Empresa)
        {
            try
            {
                Mod_OtrasInteracciones mod_OtrasInteracciones = dbPol.Tbl_OtrasInteracciones.Find(ID_Empresa);
                dbPol.Tbl_OtrasInteracciones.Remove(mod_OtrasInteracciones);
                dbPol.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ModificaOtrasInteracciones(Mod_OtrasInteracciones OtrasInteracciones)
        {
            using (var transaction = dbPol.Database.BeginTransaction())
            {
                try
                {
                    dbPol.Entry(OtrasInteracciones).State = EntityState.Modified;
                    dbPol.SaveChanges();
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

        public Mod_OtrasInteracciones ObtenerDocumentoPrivado(int id)
        {
            //Entity FrameWork
            Mod_OtrasInteracciones objOtrasInteracciones = dbPol.Tbl_OtrasInteracciones.Where(g => g.ID_OtrasInteraciones == id).FirstOrDefault();
            Mod_OtrasInteracciones objOtrasInter = (from g in dbPol.Tbl_OtrasInteracciones
                                                    where g.ID_OtrasInteraciones == id
                                                    select g).FirstOrDefault();
            return objOtrasInteracciones;
        }


        public bool CambiarTipoDocuemtoPrivado(Mod_OtrasInteracciones OtrasInteracciones)
        {
            using (var transaction = dbPol.Database.BeginTransaction())
            {
                try
                {
                    dbPol.Entry(OtrasInteracciones).State = EntityState.Modified;
                    dbPol.SaveChanges();
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
        /// Consulta donde se obtiene un objeto de tipo otras interacciones y directrices
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public string ObtenerNombreArchivoOtrasInteracciones(int id)
        {
            string Interacciones_Empresa = "";
            var Interacciones = dbPol.Tbl_OtrasInteracciones.Where(g => g.ID_OtrasInteraciones == id);
            if (Interacciones != null)
            {
                Interacciones_Empresa = Interacciones.FirstOrDefault().Archivo_OtrasInteracciones;
            }
            return Interacciones_Empresa;
        }


        /// <summary>
        /// Metodo para buscar Archivos Otras-Interacciones segun palabra que se ingrese.
        /// </summary>
        /// <param name="ID_Empresa"></param>
        /// <param name="Palabra_Busqueda"></param>
        /// <returns></returns>
        public string ObtenerOtrasInteraccionesBusqueda(string Palabra_Busqueda)
        {

            string Interacciones_Empresa = "";
            var Interacciones = dbPol.Tbl_OtrasInteracciones.Where(g => g.Archivo_OtrasInteracciones == Palabra_Busqueda);
            if (Interacciones != null)
            {
                Interacciones_Empresa = Interacciones.FirstOrDefault().Archivo_OtrasInteracciones;
            }
            return Interacciones_Empresa;
        }

        /*
        public IEnumerable<EDUsuario> ValidarExisteFirma(int idempresa)//se valida que la empresa tenga usuario representante legal
        {
            List<EDUsuario> edpus = null;
                            using (var context = new SG_SSTContext())
                            {
                                edpus = (from r in context.Tbl_UsuarioRol
                                         join u in context.Tbl_Usuario on r.Fk_Id_Usuario equals u.Pk_Id_Usuario
                                            join b in context.Tbl_Rol on r.Fk_Id_Rol equals b.Pk_Id_Rol
                                         where b.Descripcion == "Representante Legal" && u.Fk_Id_Empresa == idempresa
                                            select new EDUsuario()
                                            {
                                                   
                                                  Pk_Id_Usuario = u.Pk_Id_Usuario,
 
                                                  Fk_Tipo_Documento = u.Fk_Tipo_Documento, 
     
                                                  Numero_Documento = u.Numero_Documento,

                                                  Nombre_Usuario = u.Nombre_Usuario,
     
                                                  Imagen_Firma = u.Imagen_Firma,

                                                  nit_Empresa = u.nit_Empresa,
 
                                                  Fk_Id_Empresa = u.Fk_Id_Empresa,


                                            }).ToList();      


        }
                            return edpus;
                  
        }
        */






        public Usuario ValidarExisteFirma(int idempresa)
        {
            Usuario edperfil = null;
            using (var context = new SG_SSTContext())
            {
                edperfil = (from u in context.Tbl_Usuario
                            join r in context.Tbl_UsuarioRol on u.Pk_Id_Usuario equals r.Fk_Id_Usuario
                            join b in context.Tbl_Rol on r.Fk_Id_Rol equals b.Pk_Id_Rol
                            where b.Descripcion == "REPRESENTANTE LEGAL" && u.Fk_Id_Empresa == idempresa
                            select u

                           ).FirstOrDefault<Usuario>();
            }
            return edperfil;

        }







        public bool FirmarepLegal(mPolitica politica)
        {
            using (var transaction = dbPol.Database.BeginTransaction())
            {
                try
                {
                    dbPol.Entry(politica).State = EntityState.Modified;
                    dbPol.SaveChanges();
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














    }




























}