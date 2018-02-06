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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Aplicacion
{
    public class EPPManager : IEPP
    {
        #region "EPPCargo"

        public bool CrearEDEPPCargo(EDEPPCargo domainEntity)
        {
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    EPPCargo entity = (from s in db.Tbl_EPPCargo
                                       where s.Pk_Id_EPPCargo == domainEntity.Pk_Id_EPPCargo
                                       select s).First();

                    if (entity == null)
                    {
                        entity = new EPPCargo();
                    }

                    FillEntity(domainEntity, entity);
                    db.Tbl_EPPCargo.Add(entity);
                    db.SaveChanges();
                }
                return false;
            }
            catch
            {
                return false;
            }


        }

        private void FillEntity(EDEPPCargo domainEntity, EPPCargo entity)
        {
            entity.Cantidad = domainEntity.Cantidad;
            entity.Fk_Id_Cargo = domainEntity.Fk_Id_Cargo;
            entity.Fk_Id_EPP = domainEntity.Fk_Id_EPP;
            entity.Pk_Id_EPPCargo = domainEntity.Pk_Id_EPPCargo;
        }

        public bool ActualizarEDEPPCargo(EDEPPCargo domainEntity)
        {
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    EPPCargo entity = (from s in db.Tbl_EPPCargo
                                       where s.Pk_Id_EPPCargo == domainEntity.Pk_Id_EPPCargo
                                       select s).First();

                    FillEntity(domainEntity, entity);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EDEPPCargo> ConsultarEDEPPCargos(int IdEPP)
        {
            List<EDEPPCargo> list = new List<EDEPPCargo>();
            EDEPPCargo domainEntity;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var entities = (from s in db.Tbl_EPPCargo
                                where s.Fk_Id_EPP == IdEPP
                                select s).ToList<EPPCargo>();
                foreach (EPPCargo c in entities)
                {
                    domainEntity = new EDEPPCargo();
                    FillEntity(domainEntity, c);
                    list.Add(domainEntity);
                }
            }
            return list;
        }

        #endregion

        #region "EPPSuministroEPP"

        private bool CrearEDEPPSuministroEPP(EDEPPSuministroEPP domainEntity, EPP ePP, EPPSuministro suministro)
        {
            EPPSuministroEPP entidad;

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    entidad = (from s in db.Tbl_EPPSuministroEPP
                               where s.Pk_Id_EPPSuministroEPP == domainEntity.Pk_Id_EPPSuministroEPP
                               select s).First();

                    if (entidad == null)
                    {
                        entidad = new EPPSuministroEPP();
                        FillEntity(domainEntity, entidad, ePP, suministro);
                        db.Tbl_EPPSuministroEPP.Add(entidad);
                    }
                    else
                    {
                        FillEntity(domainEntity, entidad, ePP, suministro);
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void FillEntity(EDEPPSuministroEPP domainEntity, EPPSuministroEPP entidad, EPP ePP, EPPSuministro ePPSuministro)
        {
            entidad.Cantidad = domainEntity.Cantidad;
            entidad.EPP = ePP;
            entidad.EPPSuministro = ePPSuministro;
            entidad.Fecha = domainEntity.Fecha;
            entidad.Fk_Id_EPP = domainEntity.Fk_Id_EPP;
            entidad.Fk_Id_EPPSuministro = domainEntity.Fk_Id_EPPSuministro;
            entidad.Pk_Id_EPPSuministroEPP = domainEntity.Pk_Id_EPPSuministroEPP;
        }

        public bool ActualizarEDEPPSuministroEPP(EDEPPSuministroEPP domainEntity, EPP ePP, EPPSuministro ePPSuministro)
        {
            EPPSuministroEPP entidad;

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    entidad = (from s in db.Tbl_EPPSuministroEPP
                               where s.Pk_Id_EPPSuministroEPP == domainEntity.Pk_Id_EPPSuministroEPP
                               select s).First();

                    if (entidad == null)
                    {
                        entidad = new EPPSuministroEPP();
                        FillEntity(domainEntity, entidad, ePP, ePPSuministro);
                        db.Tbl_EPPSuministroEPP.Add(entidad);
                    }
                    else
                    {
                        FillEntity(domainEntity, entidad, ePP, ePPSuministro);
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<EDEPPSuministroEPP> ConsultarEDEPPSuministroEPP(int idSuministroEPP)
        {
            List<EDEPPSuministroEPP> list = new List<EDEPPSuministroEPP>();
            EDEPPSuministroEPP domainEntity;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var entities = (from s in db.Tbl_EPPSuministroEPP
                                where s.Fk_Id_EPPSuministro == idSuministroEPP
                                select s).ToList();

                foreach (EPPSuministroEPP c in entities)
                {
                    domainEntity = new EDEPPSuministroEPP();
                    FillDomainEntity(domainEntity, c);
                    list.Add(domainEntity);
                }
            }
            return list;
        }

        private void FillDomainEntity(EDEPPSuministroEPP domainEntity, EPPSuministroEPP entity)
        {
            domainEntity.Cantidad = entity.Cantidad;
            domainEntity.Fecha = entity.Fecha;
            domainEntity.Fk_Id_EPP = entity.Fk_Id_EPP;
            domainEntity.Fk_Id_EPPSuministro = entity.Fk_Id_EPPSuministro;
            domainEntity.Pk_Id_EPPSuministroEPP = entity.Pk_Id_EPPSuministroEPP;
        }

        #endregion

        #region "EPPSuministro"

        public bool CrearEDEPPSuministro(EDEPPSuministro domainEntity, Proceso proceso, Sede sede)
        {
            EPPSuministro entidad;

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    entidad = (from s in db.Tbl_EPPSuministro
                               where s.Pk_Id_SuministroEPP == domainEntity.Pk_Id_SuministroEPP
                               select s).First();



                    if (entidad == null)
                    {
                        entidad = new EPPSuministro();
                        FillEntity(domainEntity, entidad, proceso, sede);
                        db.Tbl_EPPSuministro.Add(entidad);
                    }
                    else
                    {
                        FillEntity(domainEntity, entidad, proceso, sede);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void FillEntity(EDEPPSuministro domainEntity, EPPSuministro entidad, Proceso proceso, Sede sede)
        {
            //entidad.Cargos = null;
            entidad.CedulaTrabajador = domainEntity.CedulaTrabajador;
            //entidad.EPPSuministroEPPs;
            entidad.Fk_Id_Proceso = domainEntity.Fk_Id_Proceso;
            entidad.Fk_Id_Sede = domainEntity.Fk_Id_Sede;
            entidad.NombreTrabajador = domainEntity.NombreTrabajador;
            entidad.Pk_Id_SuministroEPP = domainEntity.Pk_Id_SuministroEPP;
            entidad.Proceso = proceso;
            entidad.Sede = sede;
        }

        public bool ActualizarEDEPPSuministro(EDEPPSuministro domainEntity, Proceso proceso, Sede sede)
        {
            EPPSuministro entidad;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                entidad = (from s in db.Tbl_EPPSuministro
                           where s.Pk_Id_SuministroEPP == domainEntity.Pk_Id_SuministroEPP
                           select s).First();

                if (entidad != null)
                {
                    FillEntity(domainEntity, entidad, proceso, sede);
                }
                else
                {
                    return false;
                }
                db.SaveChanges();
                return true;
            }
        }

        public List<EDEPPSuministro> ConsultarEDEPPSuministro(int idEPPSuministro)
        {
            List<EDEPPSuministro> list = new List<EDEPPSuministro>();
            EDEPPSuministro domainEntity;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var entities = (from s in db.Tbl_EPPSuministro
                                where s.Pk_Id_SuministroEPP == idEPPSuministro
                                select s).ToList();

                foreach (EPPSuministro c in entities)
                {
                    domainEntity = new EDEPPSuministro();
                    list.Add(domainEntity);
                }
            }
            return list;
        }

        #endregion

        #region "EPP"
        public bool GuardarEDEPP(EDEPP admoEPP, ClasificacionDePeligro clasificacionDePeligro, Empresa empresa)
        {
            EPP epp;
            //Revisar si existe o no el EPP a crear
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    epp = (from s in db.Tbl_EPP
                           where s.Pk_Id_EPP == admoEPP.Pk_Id_EPP
                           select s).First();

                    //si no esta epp, crearla
                    if (epp == null)
                    {
                        epp = new EPP();
                        FillEntity(admoEPP, epp);
                        db.Tbl_EPP.Add(epp);
                    }
                    else
                    {
                        //Llenar la entidad con los campos de la entidad de dominio
                        FillEntity(admoEPP, epp);
                    }
                    epp.ClasificacionDePeligro = clasificacionDePeligro;
                    epp.Empresa = empresa;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void FillEntity(EDEPP domainEntity, EPP entity)
        {
            entity.Pk_Id_EPP = domainEntity.Pk_Id_EPP;
            entity.NombreEPP = domainEntity.NombreEPP;
            entity.ParteCuerpo = domainEntity.ParteCuerpo;
            entity.EspecificacionTecnica = domainEntity.EspecificacionTecnica;
            entity.Uso = domainEntity.Uso;
            entity.Mantenimiento = domainEntity.Mantenimiento;
            entity.VidaUtil = domainEntity.VidaUtil;
            entity.Reposicion = domainEntity.Reposicion;
            entity.DisposicionFinal = domainEntity.DisposicionFinal;
            entity.ArchivoImagen = domainEntity.ArchivoImagen;
            entity.ArchivoImagen_download = domainEntity.ArchivoImagen_download;
            entity.RutaImage = domainEntity.RutaImage;
            entity.NombreArchivo = domainEntity.NombreArchivo;
            entity.NombreArchivo_download = domainEntity.NombreArchivo_download;
            entity.RutaArchivo = domainEntity.RutaArchivo;
            entity.Fk_Id_Clasificacion_De_Peligro = domainEntity.Fk_Id_Clasificacion_De_Peligro;
            entity.Fk_Id_Empresa = domainEntity.Fk_Id_Empresa;
        }

        public bool ActualizarEDEPP(EDEPP admoEPP, List<EDEPPCargo> cargos)
        {
            EPP epp;
            ClasificacionDePeligro clasificacionDePeligro;
            Empresa empresa;
            //Revisar si existe o no el EPP a crear
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    epp = (from s in db.Tbl_EPP
                           where s.Pk_Id_EPP == admoEPP.Pk_Id_EPP
                           select s).First();

                    clasificacionDePeligro = (from p in db.Tbl_Clasificacion_De_Peligro
                                              where p.PK_Clasificacion_De_Peligro == epp.Fk_Id_Clasificacion_De_Peligro
                                              select p).First();

                    empresa = (from e in db.Tbl_Empresa
                               where e.Pk_Id_Empresa == epp.Fk_Id_Empresa
                               select e).First();

                    //si no esta epp, crearla
                    if (epp == null) epp = new EPP();

                    //Llenar la entidad con los campos de la entidad de dominio
                    FillEntity(admoEPP, epp);

                    epp.ClasificacionDePeligro = clasificacionDePeligro;
                    epp.Empresa = empresa;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<EDEPP> ConsultarEDEPPs(int idEmpresa)
        {
            List<EDEPP> lista = new List<EDEPP>();
            EDEPP domainEntity;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var epps = (from s in db.Tbl_EPP
                            where s.Fk_Id_Empresa == idEmpresa
                            select s);

                foreach (EPP s in epps)
                {
                    domainEntity = new EDEPP();
                    FillDomainEntity(domainEntity, s);
                    lista.Add(domainEntity);
                }
            }

            return lista;
        }

        private void FillDomainEntity(EDEPP domainEntity, EPP entity)
        {
            domainEntity.ArchivoImagen = entity.ArchivoImagen;
            domainEntity.ArchivoImagen_download = entity.ArchivoImagen_download;
            domainEntity.DisposicionFinal = entity.DisposicionFinal;
            domainEntity.EspecificacionTecnica = entity.EspecificacionTecnica;
            domainEntity.Fk_Id_Clasificacion_De_Peligro = entity.Fk_Id_Clasificacion_De_Peligro;
            domainEntity.Fk_Id_Empresa = entity.Fk_Id_Empresa;
            domainEntity.Mantenimiento = entity.Mantenimiento;
            domainEntity.NombreArchivo = entity.NombreArchivo;
            domainEntity.NombreArchivo_download = entity.NombreArchivo_download;
            domainEntity.NombreEPP = entity.NombreEPP;
            domainEntity.ParteCuerpo = entity.ParteCuerpo;
            domainEntity.Pk_Id_EPP = entity.Pk_Id_EPP;
            domainEntity.Reposicion = entity.Reposicion;
            domainEntity.RutaArchivo = entity.RutaArchivo;
            domainEntity.RutaImage = entity.RutaImage;
            domainEntity.Uso = entity.Uso;
            domainEntity.VidaUtil = entity.VidaUtil;

        }
        #endregion
        private static EDEPP GetED(EPP entity, int idEmpresa, EDClasificacionDePeligro classificacionPeligro)
        {
            EDEPP nuevaEntidad = new EDEPP
            {
                Pk_Id_EPP = entity.Pk_Id_EPP,
                NombreEPP = entity.NombreEPP,
                ParteCuerpo = entity.ParteCuerpo,
                EspecificacionTecnica = entity.EspecificacionTecnica,
                Uso = entity.Uso,
                Mantenimiento = entity.Mantenimiento,
                VidaUtil = entity.VidaUtil,
                Reposicion = entity.Reposicion,
                DisposicionFinal = entity.DisposicionFinal,
                ArchivoImagen = entity.ArchivoImagen,
                ArchivoImagen_download = entity.ArchivoImagen_download,
                RutaImage = entity.RutaImage,
                NombreArchivo = entity.NombreArchivo,
                NombreArchivo_download = entity.NombreArchivo_download,
                RutaArchivo = entity.RutaArchivo,
                Fk_Id_Clasificacion_De_Peligro = entity.Fk_Id_Clasificacion_De_Peligro,
                Fk_Id_Empresa = entity.Fk_Id_Empresa
            };

            throw new NotImplementedException();
            nuevaEntidad.Cargos = new List<EDEPPCargo>();
            nuevaEntidad.EPPSuministroEPPs = new List<EDEPPSuministroEPP>();

            return nuevaEntidad;
        }
        private List<EPPCargo> GetEPPCargos(int IdEPP)
        {
            throw new NotImplementedException();
        }
        private static EDEPPCargo getED(EPPCargo entity)
        {
            EDEPPCargo edominio = new EDEPPCargo();

            edominio.Cantidad = entity.Cantidad;
            edominio.Fk_Id_Cargo = entity.Fk_Id_Cargo;
            edominio.Fk_Id_EPP = entity.Fk_Id_EPP;
            edominio.Pk_Id_EPPCargo = entity.Pk_Id_EPPCargo;

            return edominio;
        }
        private static EPPCargo getED(EDEPPCargo edominio)
        {
            EPPCargo entity = new EPPCargo();

            entity.Cantidad = edominio.Cantidad;
            entity.Fk_Id_Cargo = edominio.Fk_Id_Cargo;
            entity.Fk_Id_EPP = edominio.Fk_Id_EPP;
            entity.Pk_Id_EPPCargo = entity.Pk_Id_EPPCargo;

            return entity;
        }
        private static EPP getEntity(EDEPP domainEntity, ClasificacionDePeligro clasificacionDePeligro)
        {
            EPP nuevaEntidad = new EPP();

            nuevaEntidad.Pk_Id_EPP = domainEntity.Pk_Id_EPP;
            nuevaEntidad.NombreEPP = domainEntity.NombreEPP;
            nuevaEntidad.ParteCuerpo = domainEntity.ParteCuerpo;
            nuevaEntidad.EspecificacionTecnica = domainEntity.EspecificacionTecnica;
            nuevaEntidad.Uso = domainEntity.Uso;
            nuevaEntidad.Mantenimiento = domainEntity.Mantenimiento;
            nuevaEntidad.VidaUtil = domainEntity.VidaUtil;
            nuevaEntidad.Reposicion = domainEntity.Reposicion;
            nuevaEntidad.DisposicionFinal = domainEntity.DisposicionFinal;
            nuevaEntidad.ArchivoImagen = domainEntity.ArchivoImagen;
            nuevaEntidad.ArchivoImagen_download = domainEntity.ArchivoImagen_download;
            nuevaEntidad.RutaImage = domainEntity.RutaImage;
            nuevaEntidad.NombreArchivo = domainEntity.NombreArchivo;
            nuevaEntidad.NombreArchivo_download = domainEntity.NombreArchivo_download;
            nuevaEntidad.RutaArchivo = domainEntity.RutaArchivo;
            nuevaEntidad.Fk_Id_Clasificacion_De_Peligro = domainEntity.Fk_Id_Clasificacion_De_Peligro;
            nuevaEntidad.Fk_Id_Empresa = domainEntity.Fk_Id_Empresa;

            return nuevaEntidad;
        }
        public bool GuardarMasivoEPP(List<EDEPP> ListaEDEPP)
        {
            bool Probar = false;
            List<EPP> ListaEPP = new List<EPP>();
            List<EPPCargo> ListaEPPCargo = new List<EPPCargo>();
            foreach (var item in ListaEDEPP)
            {
                EPP EPP = new EPP();
                EPP.Pk_Id_EPP = item.Pk_Id_EPP;
                EPP.NombreEPP = item.NombreEPP;
                EPP.ParteCuerpo = item.ParteCuerpo;
                EPP.EspecificacionTecnica = item.EspecificacionTecnica;
                EPP.Uso = item.Uso;
                EPP.Mantenimiento = item.Mantenimiento;
                EPP.VidaUtil = item.VidaUtil;
                EPP.Reposicion = item.Reposicion;
                EPP.DisposicionFinal = item.DisposicionFinal;
                EPP.ArchivoImagen = item.ArchivoImagen;
                EPP.ArchivoImagen_download = item.ArchivoImagen_download;
                EPP.RutaImage = item.RutaImage;
                EPP.NombreArchivo = item.NombreArchivo;
                EPP.NombreArchivo_download = item.NombreArchivo_download;
                EPP.RutaArchivo = item.RutaArchivo;
                EPP.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                EPP.Fk_Id_Empresa = item.Fk_Id_Empresa;
                foreach (var item1 in item.Cargos)
                {
                    EPPCargo EPPCargo = new EPPCargo();
                    EPPCargo.Cantidad = item1.Cantidad;
                    EPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                    EPPCargo.AdmoEPP = EPP;
                    ListaEPPCargo.Add(EPPCargo);
                }
                ListaEPP.Add(EPP);
            }

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    foreach (var item in ListaEPP)
                    {
                        db.Tbl_EPP.Add(item);
                    }

                    foreach (var item in ListaEPPCargo)
                    {
                        db.Tbl_EPPCargo.Add(item);
                    }

                    try
                    {
                        db.SaveChanges();
                        Probar = true;
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            catch (Exception)
            {

            }


            return Probar;
        }
        public List<EDEPP> ConsultaMatrizEpp(string Nombre, int IdClasPel, int IdCargo, int idEmpresa)
        {
            List<EDEPP> ListaEDEPP = new List<EDEPP>();
            List<EPP> ListaEPP = new List<EPP>();
            if (IdCargo != 0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_EPP
                                    join car in db.Tbl_EPPCargo on s.Pk_Id_EPP equals car.Fk_Id_EPP
                                    where s.Fk_Id_Empresa == idEmpresa && car.Fk_Id_Cargo == IdCargo
                                    select s).ToList<EPP>();
                    if (Listavar != null)
                    {
                        ListaEPP = Listavar;
                    }
                }
            }
            else
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_EPP
                                    where s.Fk_Id_Empresa == idEmpresa
                                    select s).ToList<EPP>();
                    if (Listavar != null)
                    {
                        ListaEPP = Listavar;
                    }
                }
            }
            if (IdClasPel != 0)
            {
                var Listavar = (from s in ListaEPP
                                where s.Fk_Id_Clasificacion_De_Peligro == IdClasPel
                                select s).ToList<EPP>();

                if (Listavar != null)
                {
                    ListaEPP = Listavar;
                }
            }
            if (Nombre != null)
            {
                if (Nombre != string.Empty)
                {
                    var Listavar = (from s in ListaEPP
                                    where s.NombreEPP.ToLower().Contains(Nombre.ToLower())
                                    select s).ToList<EPP>();

                    if (Listavar != null)
                    {
                        ListaEPP = Listavar;
                    }
                }
            }
            foreach (var item in ListaEPP)
            {
                EDEPP EPP = new EDEPP();
                List<EPPCargo> ListaEPPCargo = new List<EPPCargo>();
                List<EDEPPCargo> ListaEDEPPCargo = new List<EDEPPCargo>();
                EPP.Pk_Id_EPP = item.Pk_Id_EPP;
                EPP.NombreEPP = item.NombreEPP;
                EPP.ParteCuerpo = item.ParteCuerpo;
                EPP.EspecificacionTecnica = item.EspecificacionTecnica;
                EPP.Uso = item.Uso;
                EPP.Mantenimiento = item.Mantenimiento;
                EPP.VidaUtil = item.VidaUtil;
                EPP.Reposicion = item.Reposicion;
                EPP.DisposicionFinal = item.DisposicionFinal;
                EPP.ArchivoImagen = item.ArchivoImagen;
                EPP.ArchivoImagen_download = item.ArchivoImagen_download;
                EPP.RutaImage = item.RutaImage;
                EPP.NombreArchivo = item.NombreArchivo;
                EPP.NombreArchivo_download = item.NombreArchivo_download;
                EPP.RutaArchivo = item.RutaArchivo;
                EPP.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                EPP.Fk_Id_Empresa = item.Fk_Id_Empresa;
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_EPPCargo
                                    where s.Fk_Id_EPP == item.Pk_Id_EPP
                                    select s).ToList<EPPCargo>();
                    if (Listavar != null)
                    {
                        ListaEPPCargo = Listavar;
                    }
                }
                foreach (var item1 in ListaEPPCargo)
                {
                    EDEPPCargo EPPCargo = new EDEPPCargo();
                    EPPCargo.Cantidad = item1.Cantidad;
                    EPPCargo.Pk_Id_EPPCargo = item1.Pk_Id_EPPCargo;
                    EPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                    EPPCargo.Fk_Id_EPP = item1.Fk_Id_EPP;
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        var Listavar = (from s in db.Tbl_Cargo
                                        where s.Pk_Id_Cargo == item1.Fk_Id_Cargo
                                        select s).FirstOrDefault<Cargo>();
                        if (Listavar != null)
                        {
                            EPPCargo.Nombre = Listavar.Nombre_Cargo;
                        }
                    }
                    ListaEDEPPCargo.Add(EPPCargo);
                }
                EPP.Cargos = ListaEDEPPCargo;
                ListaEDEPP.Add(EPP);
            }
            return ListaEDEPP;
        }
        public List<EDEPP> ConsultaMatrizEppCargo(int IdCargo, int idEmpresa)
        {
            List<EDEPP> ListaEDEPP = new List<EDEPP>();
            List<EPP> ListaEPP = new List<EPP>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                join car in db.Tbl_EPPCargo on s.Pk_Id_EPP equals car.Fk_Id_EPP
                                where s.Fk_Id_Empresa == idEmpresa && car.Fk_Id_Cargo == IdCargo
                                select s).ToList<EPP>();
                if (Listavar != null)
                {
                    ListaEPP = Listavar;
                }
            }

            foreach (var item in ListaEPP)
            {
                EDEPP EPP = new EDEPP();
                EPP.Pk_Id_EPP = item.Pk_Id_EPP;
                EPP.NombreEPP = item.NombreEPP;
                EPP.ParteCuerpo = item.ParteCuerpo;
                EPP.EspecificacionTecnica = item.EspecificacionTecnica;
                EPP.Uso = item.Uso;
                EPP.Mantenimiento = item.Mantenimiento;
                EPP.VidaUtil = item.VidaUtil;
                EPP.Reposicion = item.Reposicion;
                EPP.DisposicionFinal = item.DisposicionFinal;
                EPP.ArchivoImagen = item.ArchivoImagen;
                EPP.ArchivoImagen_download = item.ArchivoImagen_download;
                EPP.RutaImage = item.RutaImage;
                EPP.NombreArchivo = item.NombreArchivo;
                EPP.NombreArchivo_download = item.NombreArchivo_download;
                EPP.RutaArchivo = item.RutaArchivo;
                EPP.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                EPP.Fk_Id_Empresa = item.Fk_Id_Empresa;
                ListaEDEPP.Add(EPP);
            }
            return ListaEDEPP;
        }
        public List<EDEPP> ConsultaMatrizEppCargo1(int IdCargo, string Nit)
        {
            List<EDEPP> ListaEDEPP = new List<EDEPP>();
            List<EPP> ListaEPP = new List<EPP>();
            List<EPP> ListaEPP_dist = new List<EPP>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                join car in db.Tbl_EPPCargo on s.Pk_Id_EPP equals car.Fk_Id_EPP
                                join emp in db.Tbl_Empresa on s.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                                where emp.Nit_Empresa== Nit && car.Fk_Id_Cargo == IdCargo
                                select s).ToList<EPP>();
                if (Listavar != null)
                {
                    ListaEPP = Listavar;
                }
            }

            foreach (var item in ListaEPP)
            {
                EDEPP EPP = new EDEPP();
                EPP.Pk_Id_EPP = item.Pk_Id_EPP;
                EPP.NombreEPP = item.NombreEPP;
                EPP.ParteCuerpo = item.ParteCuerpo;
                EPP.EspecificacionTecnica = item.EspecificacionTecnica;
                EPP.Uso = item.Uso;
                EPP.Mantenimiento = item.Mantenimiento;
                EPP.VidaUtil = item.VidaUtil;
                EPP.Reposicion = item.Reposicion;
                EPP.DisposicionFinal = item.DisposicionFinal;
                EPP.ArchivoImagen = item.ArchivoImagen;
                EPP.ArchivoImagen_download = item.ArchivoImagen_download;
                EPP.RutaImage = item.RutaImage;
                EPP.NombreArchivo = item.NombreArchivo;
                EPP.NombreArchivo_download = item.NombreArchivo_download;
                EPP.RutaArchivo = item.RutaArchivo;
                EPP.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                EPP.Fk_Id_Empresa = item.Fk_Id_Empresa;
                ListaEDEPP.Add(EPP);
            }
            return ListaEDEPP;
        }
        public List<EDEPP> ConsultaMatrizEppCargo2(string Nit)
        {
            List<EDEPP> ListaEDEPP = new List<EDEPP>();
            List<EPP> ListaEPP = new List<EPP>();
            List<EPP> ListaEPP_dist = new List<EPP>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                join car in db.Tbl_EPPCargo on s.Pk_Id_EPP equals car.Fk_Id_EPP
                                join emp in db.Tbl_Empresa on s.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                                where emp.Nit_Empresa == Nit
                                select s).ToList<EPP>();
                if (Listavar != null)
                {
                    ListaEPP = Listavar;
                }
            }

            foreach (var item in ListaEPP)
            {
                EDEPP EPP = new EDEPP();
                EPP.Pk_Id_EPP = item.Pk_Id_EPP;
                EPP.NombreEPP = item.NombreEPP;
                EPP.ParteCuerpo = item.ParteCuerpo;
                EPP.EspecificacionTecnica = item.EspecificacionTecnica;
                EPP.Uso = item.Uso;
                EPP.Mantenimiento = item.Mantenimiento;
                EPP.VidaUtil = item.VidaUtil;
                EPP.Reposicion = item.Reposicion;
                EPP.DisposicionFinal = item.DisposicionFinal;
                EPP.ArchivoImagen = item.ArchivoImagen;
                EPP.ArchivoImagen_download = item.ArchivoImagen_download;
                EPP.RutaImage = item.RutaImage;
                EPP.NombreArchivo = item.NombreArchivo;
                EPP.NombreArchivo_download = item.NombreArchivo_download;
                EPP.RutaArchivo = item.RutaArchivo;
                EPP.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                EPP.Fk_Id_Empresa = item.Fk_Id_Empresa;
                ListaEDEPP.Add(EPP);
            }
            return ListaEDEPP;
        }
        public List<EDEPP> ConsultaMatrizEppPersona(string IdPersona, int idEmpresa)
        {
            List<EDEPP> ListaEDEPP = new List<EDEPP>();
            List<EPP> ListaEPP = new List<EPP>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                join car in db.Tbl_EPPCargo on s.Pk_Id_EPP equals car.Fk_Id_EPP
                                where s.Fk_Id_Empresa == idEmpresa 
                                select s).ToList<EPP>();
                if (Listavar != null)
                {
                    ListaEPP = Listavar;
                }
            }

            foreach (var item in ListaEPP)
            {
                EDEPP EPP = new EDEPP();
                EPP.Pk_Id_EPP = item.Pk_Id_EPP;
                EPP.NombreEPP = item.NombreEPP;
                EPP.ParteCuerpo = item.ParteCuerpo;
                EPP.EspecificacionTecnica = item.EspecificacionTecnica;
                EPP.Uso = item.Uso;
                EPP.Mantenimiento = item.Mantenimiento;
                EPP.VidaUtil = item.VidaUtil;
                EPP.Reposicion = item.Reposicion;
                EPP.DisposicionFinal = item.DisposicionFinal;
                EPP.ArchivoImagen = item.ArchivoImagen;
                EPP.ArchivoImagen_download = item.ArchivoImagen_download;
                EPP.RutaImage = item.RutaImage;
                EPP.NombreArchivo = item.NombreArchivo;
                EPP.NombreArchivo_download = item.NombreArchivo_download;
                EPP.RutaArchivo = item.RutaArchivo;
                EPP.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                EPP.Fk_Id_Empresa = item.Fk_Id_Empresa;
                ListaEDEPP.Add(EPP);
            }
            return ListaEDEPP;
        }
        public EDEPP ConsultarEPPDownload(int IdEPP, int idEmpresa)
        {
            EDEPP EDEPP = new EDEPP();
            EPP EPP = new EPP();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                where s.Pk_Id_EPP == IdEPP && s.Fk_Id_Empresa == idEmpresa
                                select s).FirstOrDefault<EPP>();
                if (Listavar != null)
                {
                    EPP = Listavar;
                    EDEPP.Pk_Id_EPP = EPP.Pk_Id_EPP;
                    EDEPP.NombreEPP = EPP.NombreEPP;
                    EDEPP.NombreArchivo = EPP.NombreArchivo;
                    EDEPP.NombreArchivo_download = EPP.NombreArchivo_download;
                    EDEPP.RutaArchivo = EPP.RutaArchivo;
                    EDEPP.Fk_Id_Empresa = EPP.Fk_Id_Empresa;
                }
            }
            return EDEPP;
        }
        public EDEPP ConsultarEPP(int IdEPP, int idEmpresa)
        {
            EDEPP EDEPP = new EDEPP();
            EPP EPP = new EPP();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                where s.Pk_Id_EPP == IdEPP && s.Fk_Id_Empresa == idEmpresa
                                select s).FirstOrDefault<EPP>();
                if (Listavar != null)
                {
                    List<EPPCargo> ListaEPPCargo = new List<EPPCargo>();
                    List<EDEPPCargo> ListaEDEPPCargo = new List<EDEPPCargo>();
                    EPP = Listavar;
                    EDEPP.Pk_Id_EPP = EPP.Pk_Id_EPP;
                    EDEPP.NombreEPP = EPP.NombreEPP;
                    EDEPP.ParteCuerpo = EPP.ParteCuerpo;
                    EDEPP.EspecificacionTecnica = EPP.EspecificacionTecnica;
                    EDEPP.Uso = EPP.Uso;
                    EDEPP.Mantenimiento = EPP.Mantenimiento;
                    EDEPP.VidaUtil = EPP.VidaUtil;
                    EDEPP.Reposicion = EPP.Reposicion;
                    EDEPP.DisposicionFinal = EPP.DisposicionFinal;
                    EDEPP.ArchivoImagen = EPP.ArchivoImagen;
                    EDEPP.ArchivoImagen_download = EPP.ArchivoImagen_download;
                    EDEPP.RutaImage = EPP.RutaImage;
                    EDEPP.NombreArchivo = EPP.NombreArchivo;
                    EDEPP.NombreArchivo_download = EPP.NombreArchivo_download;
                    EDEPP.RutaArchivo = EPP.RutaArchivo;
                    EDEPP.Fk_Id_Clasificacion_De_Peligro = EPP.Fk_Id_Clasificacion_De_Peligro;
                    EDEPP.Fk_Id_Empresa = EPP.Fk_Id_Empresa;
                    

                    var Listavar1 = (from s in db.Tbl_EPPCargo
                                     where s.Fk_Id_EPP == EPP.Pk_Id_EPP
                                     select s).ToList<EPPCargo>();
                    if (Listavar1 != null)
                    {
                        ListaEPPCargo = Listavar1;
                    }
                    foreach (var item1 in ListaEPPCargo)
                    {
                        EDEPPCargo EPPCargo = new EDEPPCargo();
                        EPPCargo.Cantidad = item1.Cantidad;
                        EPPCargo.Pk_Id_EPPCargo = item1.Pk_Id_EPPCargo;
                        EPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                        EPPCargo.Fk_Id_EPP = item1.Fk_Id_EPP;
                        var Listavar2 = (from s in db.Tbl_Cargo
                                         where s.Pk_Id_Cargo == item1.Fk_Id_Cargo
                                         select s).FirstOrDefault<Cargo>();
                        if (Listavar2 != null)
                        {
                            EPPCargo.Nombre = Listavar2.Nombre_Cargo;
                        }
                        ListaEDEPPCargo.Add(EPPCargo);
                    }
                    EDEPP.Cargos = ListaEDEPPCargo;
                }
            }
            return EDEPP;
        }
        public bool EliminarEPP(int IdElemento, int IdEmpresa)
        {
            bool Probar = false;
            EPP EPPEliminar = new EPP();
            List<EPPCargo> EPPCargoEliminar = new List<EPPCargo>();


            using (SG_SSTContext db = new SG_SSTContext())
            {
                var epp = (from s in db.Tbl_EPP
                           where s.Pk_Id_EPP == IdElemento && s.Fk_Id_Empresa == IdEmpresa
                           select s).FirstOrDefault<EPP>();
                if (epp != null)
                {
                    EPPEliminar = epp;

                    //var eppasig = (from s in db.Tbl_EPPCargo
                    //              where s.Fk_Id_EPP == EPPEliminar.Pk_Id_EPP
                    //              select s).ToList<EPPCargo>();
                    //if (eppasig != null)
                    //{
                    //    return Probar;
                    //}

                    try
                    {
                        var eppcar = (from s in db.Tbl_EPPCargo
                                      where s.Fk_Id_EPP == EPPEliminar.Pk_Id_EPP
                                      select s).ToList<EPPCargo>();
                        if (eppcar != null)
                        {
                            EPPCargoEliminar = eppcar;
                            foreach (var item in EPPCargoEliminar)
                            {
                                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }
                        }
                        db.Entry(EPPEliminar).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        Probar = true;
                    }
                    catch (Exception ex)
                    {
                        string dgsd = ex.ToString();
                    }
                }
            }
            return Probar;
        }
        public bool ComprobarAsignacionEPP(int IdElemento, int IdEmpresa)
        {
            bool Probar = false;
            EPP EPPEliminar = new EPP();
            List<EPPCargo> EPPCargoEliminar = new List<EPPCargo>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var epp = (from s in db.Tbl_EPP
                           where s.Pk_Id_EPP == IdElemento && s.Fk_Id_Empresa == IdEmpresa
                           select s).FirstOrDefault<EPP>();
                if (epp != null)
                {
                    EPPEliminar = epp;

                    var eppasig = (from s in db.Tbl_EPPSuministroEPP
                                   where s.Fk_Id_EPP == EPPEliminar.Pk_Id_EPP
                                   select s).ToList<EPPSuministroEPP>();
                    if (eppasig != null)
                    {
                        if (eppasig.Count > 0)
                        {
                            Probar = true;
                            return Probar;
                        }
                    }
                }
            }
            return Probar;
        }
        public bool GuardarControlSuministro(EDEPPSuministro EDEPPSuministro, List<EDEPPSuministroEPP> ListaSuministros)
        {
            bool Probar = false;
            EPPSuministro NuevoEPPSuministro = new EPPSuministro();
            List<EPPSuministroEPP> ListaEPPSuministroEPP = new List<EPPSuministroEPP>();
            NuevoEPPSuministro.CedulaTrabajador = EDEPPSuministro.CedulaTrabajador;
            NuevoEPPSuministro.NombreTrabajador = EDEPPSuministro.NombreTrabajador;
            NuevoEPPSuministro.Fk_Id_Proceso = EDEPPSuministro.Fk_Id_Proceso;
            NuevoEPPSuministro.Fk_Id_Sede = EDEPPSuministro.Fk_Id_Sede;
            NuevoEPPSuministro.Fk_Id_Cargo = EDEPPSuministro.Fk_Id_Cargo;
            NuevoEPPSuministro.Fk_Id_Empresa = EDEPPSuministro.Fk_Id_Empresa;
            NuevoEPPSuministro.Fecha = DateTime.Today;
            foreach (var item in ListaSuministros)
            {
                EPPSuministroEPP EPPSuministroEPP = new EPPSuministroEPP();
                EPPSuministroEPP.Cantidad = item.Cantidad;
                EPPSuministroEPP.Fecha = item.Fecha;
                EPPSuministroEPP.Fk_Id_EPP = item.Fk_Id_EPP;
                EPPSuministroEPP.EPPSuministro = NuevoEPPSuministro;
                ListaEPPSuministroEPP.Add(EPPSuministroEPP);
            }
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_EPPSuministro.Add(NuevoEPPSuministro);
                    foreach (var item in ListaEPPSuministroEPP)
                    {
                        db.Tbl_EPPSuministroEPP.Add(item);
                    }
                    db.SaveChanges();
                    Probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return Probar;
            }
            return Probar;
        }
        public EDEPPSuministro UltimoSuministro(int Id_Empresa)
        {
            EDEPPSuministro UltimoSuministro = new EDEPPSuministro();
            EPPSuministro EPPSuministro = new EPPSuministro();
            List<EPPSuministro> ListaSuministros = new List<EPPSuministro>();
            int UltimoRegistro = 0;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Sumdef = (from s in db.Tbl_EPPSuministro
                              where s.Fk_Id_Empresa == Id_Empresa
                              select s).ToList<EPPSuministro>();

                if (Sumdef != null)
                {
                    ListaSuministros = Sumdef;
                }
            }
            UltimoRegistro = ListaSuministros.Max(item => item.Pk_Id_SuministroEPP);
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Sumdef = (from s in db.Tbl_EPPSuministro
                              where s.Pk_Id_SuministroEPP == UltimoRegistro
                              select s).FirstOrDefault<EPPSuministro>();
                if (Sumdef != null)
                {
                    EPPSuministro = Sumdef;
                }
            }
            UltimoSuministro.CedulaTrabajador = EPPSuministro.CedulaTrabajador;
            UltimoSuministro.NombreTrabajador = EPPSuministro.NombreTrabajador;
            UltimoSuministro.Fk_Id_Cargo = EPPSuministro.Fk_Id_Cargo;
            UltimoSuministro.Fk_Id_Proceso = EPPSuministro.Fk_Id_Proceso;
            UltimoSuministro.Fk_Id_Sede = EPPSuministro.Fk_Id_Sede;
            UltimoSuministro.Fk_Id_Empresa = EPPSuministro.Fk_Id_Empresa;
            UltimoSuministro.Pk_Id_SuministroEPP = EPPSuministro.Pk_Id_SuministroEPP;
            return UltimoSuministro;
        }
        public List<EDEPPSuministro> ConsultaListaAsignacion(string FechaAntes, string FechaDespues, int Cargo, string Cedula, int Riesgo, int Sede, int idEmpresa)
        {
            List<EDEPPSuministro> ListaEDEPPSuministro = new List<EDEPPSuministro>();
            List<EPPSuministro> ListaEPPSuministro = new List<EPPSuministro>();
            DateTime FechaA_conv = DateTime.MinValue;
            DateTime FechaD_conv = DateTime.MinValue;
            if (FechaAntes != null && FechaDespues != null)
            {
                if (FechaAntes != string.Empty && FechaDespues != string.Empty)
                {
                    try
                    {
                        FechaA_conv = DateTime.Parse(FechaAntes);
                        FechaD_conv = DateTime.Parse(FechaDespues);
                    }
                    catch (Exception)
                    {
                    }
                }
            }


            if (Riesgo != 0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Sumdef = (from s in db.Tbl_EPPSuministro
                                  join d in db.Tbl_EPPSuministroEPP on s.Pk_Id_SuministroEPP equals d.Fk_Id_EPPSuministro
                                  join e in db.Tbl_EPP on d.Fk_Id_EPP equals e.Pk_Id_EPP
                                  where s.Fk_Id_Empresa == idEmpresa && e.Fk_Id_Clasificacion_De_Peligro == Riesgo
                                  select s).ToList<EPPSuministro>();

                    if (Sumdef != null)
                    {
                        ListaEPPSuministro = Sumdef;
                        ListaEPPSuministro = ListaEPPSuministro.Distinct().ToList();
                    }
                }
            }
            else
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Sumdef = (from s in db.Tbl_EPPSuministro
                                  where s.Fk_Id_Empresa == idEmpresa
                                  select s).ToList<EPPSuministro>();

                    if (Sumdef != null)
                    {
                        ListaEPPSuministro = Sumdef;
                    }
                }
            }



            if (FechaA_conv != DateTime.MinValue && FechaD_conv != DateTime.MinValue)
            {
                ListaEPPSuministro = ListaEPPSuministro.Where(s => s.Fecha >= FechaA_conv).ToList();
                ListaEPPSuministro = ListaEPPSuministro.Where(s => s.Fecha <= FechaD_conv).ToList();
            }

            if (Cargo != 0)
            {
                ListaEPPSuministro = ListaEPPSuministro.Where(s => s.Fk_Id_Cargo == Cargo).ToList();
            }

            if (Sede != 0)
            {
                ListaEPPSuministro = ListaEPPSuministro.Where(s => s.Fk_Id_Sede == Sede).ToList();
            }

            if (Cedula != "")
            {
                ListaEPPSuministro = ListaEPPSuministro.Where(s => s.CedulaTrabajador == Cedula).ToList();
            }

            foreach (var item in ListaEPPSuministro)
            {
                EDEPPSuministro EDEPPSuministro = new EDEPPSuministro();
                EDEPPSuministro.Pk_Id_SuministroEPP = item.Pk_Id_SuministroEPP;
                EDEPPSuministro.CedulaTrabajador = item.CedulaTrabajador;
                EDEPPSuministro.NombreTrabajador = item.NombreTrabajador;
                EDEPPSuministro.Fk_Id_Cargo = item.Fk_Id_Cargo;
                EDEPPSuministro.Fk_Id_Proceso = item.Fk_Id_Proceso;
                EDEPPSuministro.Fk_Id_Sede = item.Fk_Id_Sede;
                EDEPPSuministro.Fk_Id_Empresa = item.Fk_Id_Empresa;
                EDEPPSuministro.Fecha = item.Fecha;
                ListaEDEPPSuministro.Add(EDEPPSuministro);
            }

            return ListaEDEPPSuministro;
        }
        public EDEPPSuministro ConsultaListaAsignacionId(int id, int idEmpresa)
        {
            EDEPPSuministro EDEPPSuministro = new EDEPPSuministro();
            EPPSuministro EPPSuministro = new EPPSuministro();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Sumdef = (from s in db.Tbl_EPPSuministro
                              where s.Fk_Id_Empresa == idEmpresa && s.Pk_Id_SuministroEPP == id
                              select s).FirstOrDefault<EPPSuministro>();

                if (Sumdef != null)
                {
                    EPPSuministro = Sumdef;
                }
            }

            List<EPPSuministroEPP> ListaSuministroEPPdetalle = new List<EPPSuministroEPP>();
            List<EDEPPSuministroEPP> ListaEDSuministroEPPdetalle = new List<EDEPPSuministroEPP>();
            EDEPPSuministro.Pk_Id_SuministroEPP = EPPSuministro.Pk_Id_SuministroEPP;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Sumdef = (from s in db.Tbl_EPPSuministroEPP
                              where s.Fk_Id_EPPSuministro == EPPSuministro.Pk_Id_SuministroEPP
                              select s).ToList<EPPSuministroEPP>();

                if (Sumdef != null)
                {
                    ListaSuministroEPPdetalle = Sumdef;
                    foreach (var item1 in ListaSuministroEPPdetalle)
                    {
                        EDEPPSuministroEPP EPPSuministroEPP = new EDEPPSuministroEPP();
                        EPPSuministroEPP.Pk_Id_EPPSuministroEPP = item1.Pk_Id_EPPSuministroEPP;
                        EPPSuministroEPP.Cantidad = item1.Cantidad;
                        EPPSuministroEPP.Fecha = item1.Fecha;
                        EPPSuministroEPP.Fk_Id_EPP = item1.Fk_Id_EPP;
                        EPPSuministroEPP.Fk_Id_EPPSuministro = item1.Fk_Id_EPPSuministro;
                        ListaEDSuministroEPPdetalle.Add(EPPSuministroEPP);
                    }
                }
            }

            EDEPPSuministro.CedulaTrabajador = EPPSuministro.CedulaTrabajador;
            EDEPPSuministro.NombreTrabajador = EPPSuministro.NombreTrabajador;
            EDEPPSuministro.Fk_Id_Cargo = EPPSuministro.Fk_Id_Cargo;
            EDEPPSuministro.Fk_Id_Proceso = EPPSuministro.Fk_Id_Proceso;
            EDEPPSuministro.Fk_Id_Sede = EPPSuministro.Fk_Id_Sede;
            EDEPPSuministro.Fk_Id_Empresa = EPPSuministro.Fk_Id_Empresa;
            EDEPPSuministro.Fecha = EPPSuministro.Fecha;
            EDEPPSuministro.ListaEPPSuministros = ListaEDSuministroEPPdetalle;

            return EDEPPSuministro;
        }
        public bool EliminarAsigEPP(int IdAsig, int IdEmpresa)
        {
            bool Probar = false;
            EPPSuministro SuministroEliminar = new EPPSuministro();
            List<EPPSuministroEPP> SumdetalleEliminar = new List<EPPSuministroEPP>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var epp = (from s in db.Tbl_EPPSuministro
                           where s.Pk_Id_SuministroEPP == IdAsig && s.Fk_Id_Empresa == IdEmpresa
                           select s).FirstOrDefault<EPPSuministro>();
                if (epp != null)
                {
                    SuministroEliminar = epp;
                    try
                    {
                        var eppcar = (from s in db.Tbl_EPPSuministroEPP
                                      where s.Fk_Id_EPPSuministro == SuministroEliminar.Pk_Id_SuministroEPP
                                      select s).ToList<EPPSuministroEPP>();
                        if (eppcar != null)
                        {
                            SumdetalleEliminar = eppcar;
                            foreach (var item in SumdetalleEliminar)
                            {
                                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }
                        }
                        db.Entry(SuministroEliminar).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        Probar = true;
                    }
                    catch (Exception ex)
                    {
                        string dgsd = ex.ToString();
                    }
                }
            }
            return Probar;
        }
        public bool GuardarEPP(EDEPP EDEPP)
        {
            bool Probar = false;
            List<EPPCargo> ListaEPPCargo = new List<EPPCargo>();
            EPP EPP = new EPP();
            EPP.Pk_Id_EPP = EDEPP.Pk_Id_EPP;
            EPP.NombreEPP = EDEPP.NombreEPP;
            EPP.ParteCuerpo = EDEPP.ParteCuerpo;
            EPP.EspecificacionTecnica = EDEPP.EspecificacionTecnica;
            EPP.Uso = EDEPP.Uso;
            EPP.Mantenimiento = EDEPP.Mantenimiento;
            EPP.VidaUtil = EDEPP.VidaUtil;
            EPP.Reposicion = EDEPP.Reposicion;
            EPP.DisposicionFinal = EDEPP.DisposicionFinal;
            EPP.ArchivoImagen = EDEPP.ArchivoImagen;
            EPP.ArchivoImagen_download = EDEPP.ArchivoImagen_download;
            EPP.RutaImage = EDEPP.RutaImage;
            EPP.NombreArchivo = EDEPP.NombreArchivo;
            EPP.NombreArchivo_download = EDEPP.NombreArchivo_download;
            EPP.RutaArchivo = EDEPP.RutaArchivo;
            EPP.Fk_Id_Clasificacion_De_Peligro = EDEPP.Fk_Id_Clasificacion_De_Peligro;
            EPP.Fk_Id_Empresa = EDEPP.Fk_Id_Empresa;
            foreach (var item1 in EDEPP.Cargos)
            {
                EPPCargo EPPCargo = new EPPCargo();
                EPPCargo.Cantidad = item1.Cantidad;
                EPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                EPPCargo.AdmoEPP = EPP;
                ListaEPPCargo.Add(EPPCargo);
            }
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_EPP.Add(EPP);
                    foreach (var item in ListaEPPCargo)
                    {
                        db.Tbl_EPPCargo.Add(item);
                    }
                    try
                    {
                        db.SaveChanges();
                        Probar = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Probar;
        }
        public bool EditarEPP(EDEPP EDEPP)
        {
            bool ProbarEditar = false;
            EPP EPP = new EPP();
            List<EPPCargo> ListaEPPCargo = new List<EPPCargo>();
            List<EPPCargo> ListaCargoActual = new List<EPPCargo>();
            List<EPPCargo> ListaCargoGuardar = new List<EPPCargo>();
            List<EPPCargo> ListaCargoEditar = new List<EPPCargo>();
            List<EPPCargo> ListaCargoEliminar = new List<EPPCargo>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var car = (from s in db.Tbl_EPPCargo
                           where s.Fk_Id_EPP == EDEPP.Pk_Id_EPP
                           select s).ToList<EPPCargo>();
                if (car != null)
                {
                    ListaCargoActual = car;
                }
            }

            EPP.Pk_Id_EPP = EDEPP.Pk_Id_EPP;
            EPP.NombreEPP = EDEPP.NombreEPP;
            EPP.ParteCuerpo = EDEPP.ParteCuerpo;
            EPP.EspecificacionTecnica = EDEPP.EspecificacionTecnica;
            EPP.Uso = EDEPP.Uso;
            EPP.Mantenimiento = EDEPP.Mantenimiento;
            EPP.VidaUtil = EDEPP.VidaUtil;
            EPP.Reposicion = EDEPP.Reposicion;
            EPP.DisposicionFinal = EDEPP.DisposicionFinal;
            EPP.ArchivoImagen = EDEPP.ArchivoImagen;
            EPP.ArchivoImagen_download = EDEPP.ArchivoImagen_download;
            EPP.RutaImage = EDEPP.RutaImage;
            EPP.NombreArchivo = EDEPP.NombreArchivo;
            EPP.NombreArchivo_download = EDEPP.NombreArchivo_download;
            EPP.RutaArchivo = EDEPP.RutaArchivo;
            EPP.Fk_Id_Clasificacion_De_Peligro = EDEPP.Fk_Id_Clasificacion_De_Peligro;
            EPP.Fk_Id_Empresa = EDEPP.Fk_Id_Empresa;
            foreach (var item1 in EDEPP.Cargos)
            {
                EPPCargo EPPCargo = new EPPCargo();
                EPPCargo.Cantidad = item1.Cantidad;
                EPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                EPPCargo.Fk_Id_EPP = EPP.Pk_Id_EPP;
                ListaEPPCargo.Add(EPPCargo);
            }

            foreach (var item in ListaEPPCargo)
            {
                EPPCargo EPPCargoBD = new EPPCargo();
                EPPCargoBD = ListaCargoActual.Where(s => s.Fk_Id_Cargo == item.Fk_Id_Cargo).FirstOrDefault();
                if (EPPCargoBD!=null)
                {
                    if (EPPCargoBD.Pk_Id_EPPCargo != 0)
                    {
                        if (EPPCargoBD.Cantidad != item.Cantidad)
                        {
                            EPPCargoBD.Cantidad = item.Cantidad;
                            ListaCargoEditar.Add(EPPCargoBD);
                        }
                    }
                    else
                    {
                        ListaCargoGuardar.Add(item);
                    }
                }
                else
                {
                    ListaCargoGuardar.Add(item);
                }
                
            }
            foreach (var item in ListaCargoActual)
            {
                EPPCargo EPPCargoBD = new EPPCargo();
                EPPCargoBD = ListaEPPCargo.Where(s => s.Fk_Id_Cargo == item.Fk_Id_Cargo).FirstOrDefault();
                if (EPPCargoBD == null)
                {
                    ListaCargoEliminar.Add(item);
                }
            }


            using (SG_SSTContext db = new SG_SSTContext())
            {
                db.Entry(EPP).State = EntityState.Modified;
                foreach (var item in ListaCargoGuardar)
                {
                    db.Tbl_EPPCargo.Add(item);
                }
                foreach (var item in ListaCargoEditar)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                foreach (var item in ListaCargoEliminar)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                try
                {
                    db.SaveChanges();
                    ProbarEditar = true;
                }
                catch (Exception)
                {
                }
            }
            return ProbarEditar;
        }
        public List<EDCargo> ListaCargos()
        {
            List<Cargo> ListaCargos = new List<Cargo>();
            List<EDCargo> ListaEDCargos = new List<EDCargo>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Cargo
                                select s).ToList<Cargo>();
                if (Listavar != null)
                {
                    ListaCargos = Listavar;
                    foreach (var item in ListaCargos)
                    {
                        EDCargo EDCargo = new EDCargo();
                        EDCargo.IDCargo = item.Pk_Id_Cargo;
                        EDCargo.NombreCargo = item.Nombre_Cargo;
                        ListaEDCargos.Add(EDCargo);
                    }
                }
            }

            return ListaEDCargos;
        }
        public List<EDEPP> ConsultaMatrizEppUsuario(EDCargo EDCargo, List<EDCargo> ListaEDCargo, string Nit)
        {
            List<EDEPP> NuevaLista = new List<EDEPP>();

            foreach (var item in ListaEDCargo)
            {
                if (item.NombreCargo!=null)
                {
                    string textoNormalizado = item.NombreCargo.Normalize(NormalizationForm.FormD);
                    string textoSinAcentos = Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]+", "");
                    textoSinAcentos = textoSinAcentos.Replace(" ", "");
                    item.NombreCargo = textoSinAcentos;
                }
            }
            if (EDCargo!=null)
            {
                EDCargo EDCargo1 = ListaEDCargo.Where(s => s.NombreCargo == EDCargo.NombreCargo).FirstOrDefault();
                if (EDCargo1 != null)
                {
                    int idCargo = EDCargo1.IDCargo;

                    NuevaLista = ConsultaMatrizEppCargo1(idCargo, Nit);
                }
            }
            else
            {

            }

            


            return NuevaLista;
        }
        public EDEPP ConsultarEPPAPP(int IdEPP, string NIT)
        {
            EDEPP EDEPP = new EDEPP();
            EPP EPP = new EPP();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_EPP
                                join emp in db.Tbl_Empresa on s.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                                where s.Pk_Id_EPP == IdEPP && emp.Nit_Empresa== NIT
                                select s).FirstOrDefault<EPP>();
                if (Listavar != null)
                {
                    EPP = Listavar;
                    EDEPP.Pk_Id_EPP = EPP.Pk_Id_EPP;
                    EDEPP.NombreEPP = EPP.NombreEPP;
                    EDEPP.ParteCuerpo = EPP.ParteCuerpo;
                    EDEPP.EspecificacionTecnica = EPP.EspecificacionTecnica;
                    EDEPP.Uso = EPP.Uso;
                    EDEPP.Mantenimiento = EPP.Mantenimiento;
                    EDEPP.VidaUtil = EPP.VidaUtil;
                    EDEPP.Reposicion = EPP.Reposicion;
                    EDEPP.DisposicionFinal = EPP.DisposicionFinal;
                    EDEPP.ArchivoImagen = EPP.ArchivoImagen;
                    EDEPP.ArchivoImagen_download = EPP.ArchivoImagen_download;
                    EDEPP.RutaImage = EPP.RutaImage;
                    EDEPP.NombreArchivo = EPP.NombreArchivo;
                    EDEPP.NombreArchivo_download = EPP.NombreArchivo_download;
                    EDEPP.RutaArchivo = EPP.RutaArchivo;
                    EDEPP.Fk_Id_Clasificacion_De_Peligro = EPP.Fk_Id_Clasificacion_De_Peligro;
                    EDEPP.Fk_Id_Empresa = EPP.Fk_Id_Empresa;
                }
            }
            return EDEPP;
        }
    }
}
