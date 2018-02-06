
using SG_SST.Models;

using SG_SST.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Empleado;
using System.IO;
using System.Data.Entity;
using System.Drawing;
using SG_SST.EntidadesDominio.Empleado;

namespace SG_SST.Repositorio.MedicionEvaluacion
{
    public class AccionManager : IAccion
    {
        public List<EDAccion> ListadeAccionesPorEmpresa(int Pk_Id_Empresa)
        {
            List<EDAccion> ListaAcciones = new List<EDAccion>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var ListaAcc = (from s in db1.Tbl_Acciones
                                where s.Fk_Id_Empresa == Pk_Id_Empresa
                                select s).ToList<Accion>();
                if (ListaAcc != null)
                {
                    foreach (var item in ListaAcc)
                    {
                        EDAccion EDAccion = new EDAccion();
                        EDAccion.Pk_Id_Accion = item.Pk_Id_Accion;
                        EDAccion.Id_Accion = item.Id_Accion;
                        EDAccion.Tipo = item.Tipo;
                        EDAccion.Fecha_dil = item.Fecha_dil;
                        EDAccion.Fecha_ocurrencia = item.Fecha_ocurrencia;
                        EDAccion.Clase = item.Clase;
                        EDAccion.Fecha_hall = item.Fecha_hall;
                        EDAccion.Halla_Num_Doc = item.Halla_Num_Doc;
                        EDAccion.Halla_Nombre = item.Halla_Nombre;
                        EDAccion.Halla_TipoDoc = item.Halla_TipoDoc;
                        EDAccion.Halla_Cargo = item.Halla_Cargo;
                        EDAccion.Halla_Sede = item.Halla_Sede;
                        EDAccion.Correccion = item.Correccion;
                        EDAccion.Causa_Raiz = item.Causa_Raiz;
                        EDAccion.Cambio_Doc = item.Cambio_Doc;
                        EDAccion.Des_Cambio_Doc = item.Des_Cambio_Doc;
                        EDAccion.Verificacion = item.Verificacion;
                        EDAccion.Eficacia = item.Eficacia;
                        EDAccion.Estado = item.Estado;
                        EDAccion.NombreArchivoAuditor = item.NombreArchivoAuditor;
                        EDAccion.RutaArchivoAuditor = item.RutaArchivoAuditor;
                        EDAccion.Nombre_Auditor = item.Nombre_Auditor;
                        EDAccion.Cargo_Auditor = item.Cargo_Auditor;
                        EDAccion.NombreArchivoResp = item.NombreArchivoResp;
                        EDAccion.RutaArchivoResp = item.RutaArchivoResp;
                        EDAccion.Nombre_Responsable = item.Nombre_Responsable;
                        EDAccion.Cargo_Responsable = item.Cargo_Responsable;
                        EDAccion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                        EDAccion.Otro_Origen = item.Otro_Origen;
                        EDAccion.Origen = item.Origen;
                        ListaAcciones.Add(EDAccion);
                    }
                }
            }
            return ListaAcciones;
        }
        public List<EDAccion> ListadeAccionesPorEmpresaID(int Pk_Id_Empresa)
        {
            List<EDAccion> ListaAcciones = new List<EDAccion>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var Listavar = (from s in db1.Tbl_Acciones
                                where s.Fk_Id_Empresa == Pk_Id_Empresa
                                select s).ToList<Accion>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDAccion EDAccion = new EDAccion();
                        EDAccion.Pk_Id_Accion = item.Pk_Id_Accion;
                        EDAccion.Id_Accion = item.Id_Accion;
                        ListaAcciones.Add(EDAccion);
                    }
                }
            }
            return ListaAcciones;
        }
        public bool GuardarAccionbool(EDAccion EDAccion)
        {
            bool IdAccionGuardada = false;
            Accion Accion = new Accion();
            Accion.Id_Accion = EDAccion.Id_Accion;
            Accion.Tipo = EDAccion.Tipo;
            Accion.Fecha_dil = EDAccion.Fecha_dil;
            Accion.Fecha_ocurrencia = EDAccion.Fecha_ocurrencia;
            Accion.Clase = EDAccion.Clase;
            Accion.Fecha_hall = EDAccion.Fecha_hall;
            Accion.Halla_Num_Doc = EDAccion.Halla_Num_Doc;
            Accion.Halla_Nombre = EDAccion.Halla_Nombre;
            Accion.Halla_TipoDoc = EDAccion.Halla_TipoDoc;
            Accion.Halla_Cargo = EDAccion.Halla_Cargo;
            Accion.Halla_Sede = EDAccion.Halla_Sede;
            Accion.Correccion = EDAccion.Correccion;
            Accion.Causa_Raiz = EDAccion.Causa_Raiz;
            Accion.Cambio_Doc = EDAccion.Cambio_Doc;
            Accion.Des_Cambio_Doc = EDAccion.Des_Cambio_Doc;
            Accion.Verificacion = EDAccion.Verificacion;
            Accion.Eficacia = EDAccion.Eficacia;
            Accion.Estado = EDAccion.Estado;
            Accion.NombreArchivoAuditor = EDAccion.NombreArchivoAuditor;
            Accion.RutaArchivoAuditor = EDAccion.RutaArchivoAuditor;
            Accion.Nombre_Auditor = EDAccion.Nombre_Auditor;
            Accion.Cargo_Auditor = EDAccion.Cargo_Auditor;
            Accion.NombreArchivoResp = EDAccion.NombreArchivoResp;
            Accion.RutaArchivoResp = EDAccion.RutaArchivoResp;
            Accion.Nombre_Responsable = EDAccion.Nombre_Responsable;
            Accion.Cargo_Responsable = EDAccion.Cargo_Responsable;
            Accion.Fk_Id_Empresa = EDAccion.Fk_Id_Empresa;
            Accion.Otro_Origen = EDAccion.Otro_Origen;
            Accion.Origen = EDAccion.Origen;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Tbl_Acciones.Add(Accion);
                    db1.SaveChanges();
                    IdAccionGuardada = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return IdAccionGuardada;
            }
            return IdAccionGuardada;
        }
        public EDAccion UltimoPkId(int Pk_Id_Empresa)
        {
            EDAccion EDAccion = new EDAccion();
            List<Accion> ListaAccion = new List<Accion>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var Acciondef = (from s in db1.Tbl_Acciones
                                 where s.Fk_Id_Empresa == Pk_Id_Empresa
                                 select s).ToList<Accion>();

                if (Acciondef != null)
                {
                    ListaAccion = Acciondef;
                }
            }
            int IdMaxLista = ListaAccion.Max(item => item.Pk_Id_Accion);
            EDAccion.Pk_Id_Accion = IdMaxLista;
            return EDAccion;
        }
        public void GuardarHallazgo(EDHallazgo EDHallazgo)
        {
            Hallazgo Hallazgo = new Hallazgo();
            Hallazgo.Halla_Norma = EDHallazgo.Halla_Norma;
            Hallazgo.Halla_Numeral = EDHallazgo.Halla_Numeral;
            Hallazgo.Halla_Descripcion = EDHallazgo.Halla_Descripcion;
            Hallazgo.Halla_Proceso = EDHallazgo.Halla_Proceso;
            Hallazgo.Fk_Id_Accion = EDHallazgo.Fk_Id_Accion;
            Hallazgo.Fk_Id_Proceso = EDHallazgo.Fk_Id_Proceso;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Tbl_Hallazgo.Add(Hallazgo);
                    db1.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
        }
        public void GuardarAnalisis(EDAnalisis EDAnalisis)
        {
            Analisis Analisis = new Analisis();
            Analisis.Id_Analisis = EDAnalisis.Id_Analisis;
            Analisis.Tipo = EDAnalisis.Tipo;
            Analisis.ValorTxt = EDAnalisis.ValorTxt;
            Analisis.Parent_Id = EDAnalisis.Parent_Id;
            Analisis.Fk_Id_Accion = EDAnalisis.Fk_Id_Accion;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Tbl_Analisis.Add(Analisis);
                    db1.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
        }
        public bool GuardarSeguimiento(EDSeguimiento EDSeguimiento)
        {
            bool ProbarGuardar = false;
            Seguimiento Seguimiento = new Seguimiento();
            Seguimiento.Fecha_Seg = EDSeguimiento.Fecha_Seg;
            Seguimiento.Observaciones = EDSeguimiento.Observaciones;
            Seguimiento.NombreArchivoSeg = EDSeguimiento.NombreArchivoSeg;
            Seguimiento.RutaArchivoSeg = EDSeguimiento.RutaArchivoSeg;
            Seguimiento.Fk_Id_Accion = EDSeguimiento.Fk_Id_Accion;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_Seguimiento.Add(Seguimiento);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return ProbarGuardar;
        }
        public bool GuardarActividad(EDActividad EDActividad)
        {
            bool ProbarGuardar = false;
            ActividadAccion Actividad = new ActividadAccion();
            Actividad.Actividad = EDActividad.Actividad;
            Actividad.Responsable = EDActividad.Responsable;
            Actividad.FechaFinalizacion = EDActividad.FechaFinalizacion;
            Actividad.NombreArchivoAct = EDActividad.NombreArchivoAct;
            Actividad.RutaArchivoAct = EDActividad.RutaArchivoAct;
            Actividad.Estado = EDActividad.Estado_1;
            Actividad.Fk_Id_Accion = EDActividad.Fk_Id_Accion;
            
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_ActividadAccion.Add(Actividad);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return ProbarGuardar;
        }
        public bool GuardarArchivosAccion(EDArchivosAcciones EDArchivosAcciones)
        {
            bool ProbarGuardar = false;
            ArchivosAccion ArchivosAccion = new ArchivosAccion();
            ArchivosAccion.NombreArchivo = EDArchivosAcciones.NombreArchivo;
            ArchivosAccion.Ruta = EDArchivosAcciones.Ruta;
            ArchivosAccion.Fk_Id_Accion = EDArchivosAcciones.Fk_Id_Accion;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_ArchivosAccion.Add(ArchivosAccion);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return ProbarGuardar;
        }
        public bool EditarAccion(EDAccion Accion)
        {
            Accion AccionEd = new Accion();
            AccionEd.Pk_Id_Accion = Accion.Pk_Id_Accion;
            AccionEd.Id_Accion = Accion.Id_Accion;
            AccionEd.Tipo = Accion.Tipo;
            AccionEd.Fecha_dil = Accion.Fecha_dil;
            AccionEd.Fecha_ocurrencia = Accion.Fecha_ocurrencia;
            AccionEd.Clase = Accion.Clase;
            AccionEd.Fecha_hall = Accion.Fecha_hall;
            AccionEd.Halla_Num_Doc = Accion.Halla_Num_Doc;
            AccionEd.Halla_Nombre = Accion.Halla_Nombre;
            AccionEd.Halla_TipoDoc = Accion.Halla_TipoDoc;
            AccionEd.Halla_Cargo = Accion.Halla_Cargo;
            AccionEd.Halla_Sede = Accion.Halla_Sede;
            AccionEd.Correccion = Accion.Correccion;
            AccionEd.Causa_Raiz = Accion.Causa_Raiz;
            AccionEd.Cambio_Doc = Accion.Cambio_Doc;
            AccionEd.Des_Cambio_Doc = Accion.Des_Cambio_Doc;
            AccionEd.Verificacion = Accion.Verificacion;
            AccionEd.Eficacia = Accion.Eficacia;
            AccionEd.Estado = Accion.Estado;
            AccionEd.NombreArchivoAuditor = Accion.NombreArchivoAuditor;
            AccionEd.RutaArchivoAuditor = Accion.RutaArchivoAuditor;
            AccionEd.Nombre_Auditor = Accion.Nombre_Auditor;
            AccionEd.Cargo_Auditor = Accion.Cargo_Auditor;
            AccionEd.NombreArchivoResp = Accion.NombreArchivoResp;
            AccionEd.RutaArchivoResp = Accion.RutaArchivoResp;
            AccionEd.Nombre_Responsable = Accion.Nombre_Responsable;
            AccionEd.Cargo_Responsable = Accion.Cargo_Responsable;
            AccionEd.Fk_Id_Empresa = Accion.Fk_Id_Empresa;
            AccionEd.Otro_Origen = Accion.Otro_Origen;
            AccionEd.Origen = Accion.Origen;
            bool probar = false;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Entry(AccionEd).State = EntityState.Modified;
                    db1.SaveChanges();
                    probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return probar;
        }
        public bool EditarHallazgo(EDHallazgo EDHallazgo)
        {
            bool probar = false;
            Hallazgo editarhallazgo = new Hallazgo();
            editarhallazgo.Pk_Id_Hallazgo = EDHallazgo.Pk_Id_Hallazgo;
            editarhallazgo.Halla_Norma = EDHallazgo.Halla_Norma;
            editarhallazgo.Halla_Numeral = EDHallazgo.Halla_Numeral;
            editarhallazgo.Halla_Descripcion = EDHallazgo.Halla_Descripcion;
            editarhallazgo.Halla_Proceso = EDHallazgo.Halla_Proceso;
            editarhallazgo.Fk_Id_Accion = EDHallazgo.Fk_Id_Accion;
            editarhallazgo.Fk_Id_Proceso = EDHallazgo.Fk_Id_Proceso;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Entry(editarhallazgo).State = EntityState.Modified;
                    db1.SaveChanges();
                    probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return probar;
        }
        public bool EditarSeguimiento(EDSeguimiento EDSeguimiento)
        {
            bool probar = false;
            Seguimiento editarseguimiento = new Seguimiento();
            editarseguimiento.Pk_Id_Seguimiento = EDSeguimiento.Pk_Id_Seguimiento;
            editarseguimiento.Fecha_Seg = EDSeguimiento.Fecha_Seg;
            editarseguimiento.Observaciones = EDSeguimiento.Observaciones;
            editarseguimiento.NombreArchivoSeg = EDSeguimiento.NombreArchivoSeg;
            editarseguimiento.RutaArchivoSeg = EDSeguimiento.RutaArchivoSeg;
            editarseguimiento.Fk_Id_Accion = EDSeguimiento.Fk_Id_Accion;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Entry(editarseguimiento).State = EntityState.Modified;
                    db1.SaveChanges();
                    probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return probar;
        }
        public bool EditarActividad(EDActividad EDActividad)
        {
            bool probar = false;
            ActividadAccion editaractividad = new ActividadAccion();
            editaractividad.Pk_Id_Actividad = EDActividad.Pk_Id_Actividad;
            editaractividad.Actividad = EDActividad.Actividad;
            editaractividad.Responsable = EDActividad.Responsable;
            editaractividad.FechaFinalizacion = EDActividad.FechaFinalizacion;
            editaractividad.NombreArchivoAct = EDActividad.NombreArchivoAct;
            editaractividad.Estado = EDActividad.Estado_1;
            editaractividad.RutaArchivoAct = EDActividad.RutaArchivoAct;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Entry(editaractividad).State = EntityState.Modified;
                    db1.SaveChanges();
                    probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return probar;
        }
        public bool EditarArchivo(EDArchivosAcciones EDArchivo)
        {
            bool probar = false;
            ArchivosAccion editararchivos = new ArchivosAccion();
            editararchivos.Pk_Id_Archivo = EDArchivo.Pk_Id_Archivo;
            editararchivos.NombreArchivo = EDArchivo.NombreArchivo;
            editararchivos.Ruta = EDArchivo.Ruta;
            editararchivos.Fk_Id_Accion = EDArchivo.Fk_Id_Accion;
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Entry(editararchivos).State = EntityState.Modified;
                    db1.SaveChanges();
                    probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return probar;
        }
        public void EliminarHallazgos(EDHallazgo EDHallazgo)
        {
            Hallazgo eliminarhallazgo = new Hallazgo();
            eliminarhallazgo.Pk_Id_Hallazgo = EDHallazgo.Pk_Id_Hallazgo;
            eliminarhallazgo.Halla_Norma = EDHallazgo.Halla_Norma;
            eliminarhallazgo.Halla_Numeral = EDHallazgo.Halla_Numeral;
            eliminarhallazgo.Halla_Descripcion = EDHallazgo.Halla_Descripcion;
            eliminarhallazgo.Halla_Proceso = EDHallazgo.Halla_Proceso;
            eliminarhallazgo.Fk_Id_Accion = EDHallazgo.Fk_Id_Accion;
            eliminarhallazgo.Fk_Id_Proceso = EDHallazgo.Fk_Id_Proceso;
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                db1.Entry(eliminarhallazgo).State = System.Data.Entity.EntityState.Deleted;
                db1.SaveChanges();
            }
        }
        public bool EliminarArchivos(EDArchivosAcciones EDArchivosAcciones)
        {
            bool ProbarEliminar = false;
            ArchivosAccion eliminararchivos = new ArchivosAccion();
            eliminararchivos.Pk_Id_Archivo = EDArchivosAcciones.Pk_Id_Archivo;
            eliminararchivos.NombreArchivo = EDArchivosAcciones.NombreArchivo;
            eliminararchivos.Ruta = EDArchivosAcciones.Ruta;
            eliminararchivos.Fk_Id_Accion = EDArchivosAcciones.Fk_Id_Accion;
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                db1.Entry(eliminararchivos).State = System.Data.Entity.EntityState.Deleted;
                db1.SaveChanges();
                ProbarEliminar = true;
            }
            return ProbarEliminar;
        }
        public int UltimoIdArchivo(int idAccion, int idEmpresa)
        {
            int UltimoId = 0;
            Accion AccionConsulta = new Accion();
            List<ArchivosAccion> ListaArchivos = new List<ArchivosAccion>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Acciondef = (from s in db.Tbl_Acciones
                                 where s.Pk_Id_Accion == idAccion && s.Fk_Id_Empresa == idEmpresa
                                 select s).FirstOrDefault<Accion>();

                if (Acciondef != null)
                {
                    AccionConsulta = Acciondef;
                }
            }
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Acciondef = (from s in db.Tbl_ArchivosAccion
                                 where s.Fk_Id_Accion == AccionConsulta.Pk_Id_Accion
                                 select s).ToList<ArchivosAccion>();

                if (Acciondef != null)
                {
                    ListaArchivos = Acciondef;
                }
            }
            UltimoId = ListaArchivos.Max(item => item.Pk_Id_Archivo);
            return UltimoId;
        }
        public void EliminarAnalisis(EDAnalisis EDAnalisis)
        {
            Analisis Eliminaranalisis = new Analisis();
            Eliminaranalisis.Pk_Id_Analisis = EDAnalisis.Pk_Id_Analisis;
            Eliminaranalisis.Id_Analisis = EDAnalisis.Id_Analisis;
            Eliminaranalisis.Tipo = EDAnalisis.Tipo;
            Eliminaranalisis.ValorTxt = EDAnalisis.ValorTxt;
            Eliminaranalisis.Parent_Id = EDAnalisis.Parent_Id;
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                db1.Entry(Eliminaranalisis).State = System.Data.Entity.EntityState.Deleted;
                db1.SaveChanges();
            }
        }
        public List<EDAccion> ConsultarAcciones(string Id, string Nombre, string Estado, int idEmpresa, string Sede)
        {
            List<EDAccion> EDAccionLista = new List<EDAccion>();
            List<Accion> Lista = new List<Accion>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var Listavar = (from s in db1.Tbl_Acciones
                                where s.Fk_Id_Empresa == idEmpresa
                                select s).ToList<Accion>();
                if (Listavar != null)
                {
                    Lista = Listavar;
                }
            }

            


            if (Id != null)
            {
                if (Id != "")
                {
                    int IdAccion = 0;
                    if (int.TryParse(Id, out IdAccion))
                    {
                        var Listavar = (from s in Lista
                                        where s.Id_Accion == IdAccion
                                        select s).ToList<Accion>();

                        if (Listavar != null)
                        {
                            Lista = Listavar;
                        }
                    }
                }
            }

            if (Nombre != null)
            {
                if (Nombre != "")
                {
                    var Listavar = (from s in Lista
                                    where s.Halla_Nombre.ToLower().Contains(Nombre.ToLower())
                                    select s).ToList<Accion>();

                    if (Listavar != null)
                    {
                        Lista = Listavar;
                    }
                }
            }
            if (Sede != null)
            {
                if (Sede != "")
                {
                    var Listavar = (from s in Lista
                                    where s.Halla_Sede == Sede
                                    select s).ToList<Accion>();
                    if (Listavar != null)
                    {
                        Lista = Listavar;
                    }
                }
            }

            if (Estado != null)
            {
                if (Estado != "")
                {
                    var Listavar = (from s in Lista
                                    where s.Estado== Estado
                                    select s).ToList<Accion>();

                    if (Listavar != null)
                    {
                        Lista = Listavar;
                    }
                }
            }


            foreach (var item in Lista)
            {
                VerificarEstado(item);
            }

            foreach (var item in Lista)
            {
                EDAccion EDAccion = new EDAccion();
                EDAccion.Pk_Id_Accion = item.Pk_Id_Accion;
                EDAccion.Id_Accion = item.Id_Accion;
                EDAccion.Tipo = item.Tipo;
                EDAccion.Fecha_dil = item.Fecha_dil;
                EDAccion.Fecha_ocurrencia = item.Fecha_ocurrencia;
                EDAccion.Clase = item.Clase;
                EDAccion.Fecha_hall = item.Fecha_hall;
                EDAccion.Halla_Num_Doc = item.Halla_Num_Doc;
                EDAccion.Halla_Nombre = item.Halla_Nombre;
                EDAccion.Halla_TipoDoc = item.Halla_TipoDoc;
                EDAccion.Halla_Cargo = item.Halla_Cargo;
                EDAccion.Halla_Sede = item.Halla_Sede;
                EDAccion.Correccion = item.Correccion;
                EDAccion.Causa_Raiz = item.Causa_Raiz;
                EDAccion.Cambio_Doc = item.Cambio_Doc;
                EDAccion.Des_Cambio_Doc = item.Des_Cambio_Doc;
                EDAccion.Verificacion = item.Verificacion;
                EDAccion.Eficacia = item.Eficacia;
                EDAccion.Estado = item.Estado;
                EDAccion.NombreArchivoAuditor = item.NombreArchivoAuditor;
                EDAccion.RutaArchivoAuditor = item.RutaArchivoAuditor;
                EDAccion.Nombre_Auditor = item.Nombre_Auditor;
                EDAccion.Cargo_Auditor = item.Cargo_Auditor;
                EDAccion.NombreArchivoResp = item.NombreArchivoResp;
                EDAccion.RutaArchivoResp = item.RutaArchivoResp;
                EDAccion.Nombre_Responsable = item.Nombre_Responsable;
                EDAccion.Cargo_Responsable = item.Cargo_Responsable;
                EDAccion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                EDAccion.Otro_Origen = item.Otro_Origen;
                EDAccion.Origen = item.Origen;
                EDAccionLista.Add(EDAccion);
            }
            Lista = Lista.OrderBy(s => s.Fecha_dil).ToList();
            return EDAccionLista;
        }
        public EDAccion ConsultaAccion(int IdAccion, int idEmpresa)
        {
            int NumeroActividadesAbiertas = 0;
            EDAccion EDConsulta = new EDAccion();
            Accion AccionConsulta = new Accion();
            List<Hallazgo> ListaHallazgos = new List<Hallazgo>();
            List<ActividadAccion> ListaActividad = new List<ActividadAccion>();
            List<Seguimiento> ListaSeguimiento = new List<Seguimiento>();
            List<Analisis> ListaAnalisis = new List<Analisis>();
            List<Analisis> ListaAnalisis3 = new List<Analisis>();
            List<ArchivosAccion> ListaArchivos = new List<ArchivosAccion>();

            List<EDHallazgo> EDListaHallazgos = new List<EDHallazgo>();
            List<EDActividad> EDListaActividad = new List<EDActividad>();
            List<EDSeguimiento> EDListaSeguimiento = new List<EDSeguimiento>();
            List<EDAnalisis> EDListaAnalisis = new List<EDAnalisis>();
            List<EDArchivosAcciones> EDListaArchivos = new List<EDArchivosAcciones>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Acciondef = (from s in db.Tbl_Acciones
                                 where s.Pk_Id_Accion == IdAccion && s.Fk_Id_Empresa == idEmpresa
                                 select s).FirstOrDefault<Accion>();

                if (Acciondef != null)
                {
                    AccionConsulta = Acciondef;
                }
            }
            EDConsulta.Pk_Id_Accion = AccionConsulta.Pk_Id_Accion;
            EDConsulta.Id_Accion = AccionConsulta.Id_Accion;
            EDConsulta.Tipo = AccionConsulta.Tipo;
            EDConsulta.Fecha_dil = AccionConsulta.Fecha_dil;
            EDConsulta.Fecha_ocurrencia = AccionConsulta.Fecha_ocurrencia;
            EDConsulta.Clase = AccionConsulta.Clase;
            EDConsulta.Fecha_hall = AccionConsulta.Fecha_hall;
            EDConsulta.Halla_Num_Doc = AccionConsulta.Halla_Num_Doc;
            EDConsulta.Halla_Nombre = AccionConsulta.Halla_Nombre;
            EDConsulta.Halla_TipoDoc = AccionConsulta.Halla_TipoDoc;
            EDConsulta.Halla_Cargo = AccionConsulta.Halla_Cargo;
            EDConsulta.Halla_Sede = AccionConsulta.Halla_Sede;
            EDConsulta.Correccion = AccionConsulta.Correccion;
            EDConsulta.Causa_Raiz = AccionConsulta.Causa_Raiz;
            EDConsulta.Cambio_Doc = AccionConsulta.Cambio_Doc;
            EDConsulta.Des_Cambio_Doc = AccionConsulta.Des_Cambio_Doc;
            EDConsulta.Verificacion = AccionConsulta.Verificacion;
            EDConsulta.Eficacia = AccionConsulta.Eficacia;
            EDConsulta.Estado = AccionConsulta.Estado;
            EDConsulta.NombreArchivoAuditor = AccionConsulta.NombreArchivoAuditor;
            EDConsulta.RutaArchivoAuditor = AccionConsulta.RutaArchivoAuditor;
            EDConsulta.Nombre_Auditor = AccionConsulta.Nombre_Auditor;
            EDConsulta.Cargo_Auditor = AccionConsulta.Cargo_Auditor;
            EDConsulta.NombreArchivoResp = AccionConsulta.NombreArchivoResp;
            EDConsulta.RutaArchivoResp = AccionConsulta.RutaArchivoResp;
            EDConsulta.Nombre_Responsable = AccionConsulta.Nombre_Responsable;
            EDConsulta.Cargo_Responsable = AccionConsulta.Cargo_Responsable;
            EDConsulta.Otro_Origen = AccionConsulta.Otro_Origen;
            EDConsulta.Origen = AccionConsulta.Origen;
            if (AccionConsulta != null)
            {
                if (AccionConsulta.Pk_Id_Accion!=0)
                {
                    //Hallazgos
                    using (SG_SSTContext db = new SG_SSTContext())
                {
                    var HallazgoList = (from s in db.Tbl_Hallazgo
                                        where s.Fk_Id_Accion == IdAccion
                                        select s).ToList<Hallazgo>();

                    if (HallazgoList != null)
                    {
                        ListaHallazgos = HallazgoList;
                    }
                }
                //Analisis
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var AnalisisList = (from s in db.Tbl_Analisis
                                        where s.Fk_Id_Accion == IdAccion
                                        select s).ToList<Analisis>();

                    if (AnalisisList != null)
                    {
                        ListaAnalisis = AnalisisList;
                    }
                }
                //Actividades
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ActividadList = (from s in db.Tbl_ActividadAccion
                                         where s.Fk_Id_Accion == IdAccion
                                         select s).ToList<ActividadAccion>();

                    if (ActividadList != null)
                    {
                        ListaActividad = ActividadList;
                    }
                }
                //Seguimientos
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var SeguimientoList = (from s in db.Tbl_Seguimiento
                                           where s.Fk_Id_Accion == IdAccion
                                           select s).ToList<Seguimiento>();

                    if (SeguimientoList != null)
                    {
                        ListaSeguimiento = SeguimientoList;
                    }
                }
                //Archivos
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ArchivosList = (from s in db.Tbl_ArchivosAccion
                                        where s.Fk_Id_Accion == IdAccion
                                        select s).ToList<ArchivosAccion>();

                    if (ArchivosList != null)
                    {
                        ListaArchivos = ArchivosList;
                    }
                }
                foreach (var item in ListaActividad)
                {
                        string IdStr = RandomString(6);
                        int IdInt = 0;
                        while (int.TryParse(IdStr, out IdInt) == false)
                        {
                            IdStr = RandomString(6);
                        }
                        EDActividad EdActividad = new EDActividad();
                        EdActividad.Pk_Id_Actividad = item.Pk_Id_Actividad;
                        EdActividad.Actividad = item.Actividad;
                        EdActividad.Responsable = item.Responsable;
                        EdActividad.FechaFinalizacion = item.FechaFinalizacion;
                        EdActividad.NombreArchivoAct = item.NombreArchivoAct;
                        EdActividad.RutaArchivoAct = item.RutaArchivoAct;
                        EdActividad.Fk_Id_Accion = item.Fk_Id_Accion;
                        EdActividad.Clave = IdInt;
                        EdActividad.Estado = 1;
                        EdActividad.Estado_1 = item.Estado;
                        EDListaActividad.Add(EdActividad);
                        if (item.Estado==1)
                        {
                            NumeroActividadesAbiertas = NumeroActividadesAbiertas + 1;
                        }
                }
                foreach (var item in ListaSeguimiento)
                {
                    string IdStr = RandomString(6);
                    int IdInt = 0;
                    while (int.TryParse(IdStr, out IdInt) == false)
                    {
                        IdStr = RandomString(6);
                    }
                    EDSeguimiento EDSeguimiento = new EDSeguimiento();
                    EDSeguimiento.Pk_Id_Seguimiento = item.Pk_Id_Seguimiento;
                    EDSeguimiento.Fk_Id_Accion = item.Fk_Id_Accion;
                    EDSeguimiento.Fecha_Seg = item.Fecha_Seg;
                    EDSeguimiento.Observaciones = item.Observaciones;
                    EDSeguimiento.NombreArchivoSeg = item.NombreArchivoSeg;
                    EDSeguimiento.RutaArchivoSeg = item.RutaArchivoSeg;
                    EDSeguimiento.Clave = IdInt;
                    EDSeguimiento.Estado = 1;
                    EDListaSeguimiento.Add(EDSeguimiento);
                }
                foreach (var item in ListaHallazgos)
                {
                    string IdStr = RandomString(6);
                    int IdInt = 0;
                    while (int.TryParse(IdStr, out IdInt) == false)
                    {
                        IdStr = RandomString(6);
                    }
                    EDHallazgo EDHallazgo = new EDHallazgo();
                    EDHallazgo.Pk_Id_Hallazgo = item.Pk_Id_Hallazgo;
                    EDHallazgo.Halla_Norma = item.Halla_Norma;
                    EDHallazgo.Halla_Numeral = item.Halla_Numeral;
                    EDHallazgo.Halla_Descripcion = item.Halla_Descripcion;
                    EDHallazgo.Halla_Proceso = item.Halla_Proceso;
                    EDHallazgo.Fk_Id_Accion = item.Fk_Id_Accion;
                    EDHallazgo.Fk_Id_Proceso = item.Fk_Id_Proceso;
                    EDHallazgo.Estado = 1;
                    EDHallazgo.Clave = IdInt;
                    EDListaHallazgos.Add(EDHallazgo);
                }

                foreach (var item in ListaAnalisis)
                {
                    EDAnalisis EDAnalisis = new EDAnalisis();
                    EDAnalisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                    EDAnalisis.Id_Analisis = item.Id_Analisis;
                    EDAnalisis.Tipo = item.Tipo;
                    if (item.ValorTxt == "N/A")
                    {
                        EDAnalisis.ValorTxt = "";
                    }
                    else
                    {
                        EDAnalisis.ValorTxt = item.ValorTxt;
                    }
                    EDAnalisis.Parent_Id = item.Parent_Id;
                    EDAnalisis.Fk_Id_Accion = item.Fk_Id_Accion;
                    EDListaAnalisis.Add(EDAnalisis);
                }

                foreach (var item in ListaArchivos)
                {
                    int NumeroArchivo = 0;
                    string RandomStr = RandomString(9);
                    bool NumArchivobool = int.TryParse(RandomStr, out NumeroArchivo);
                    EDArchivosAcciones EDArchivosAcciones = new EDArchivosAcciones();
                    EDArchivosAcciones.Pk_Id_Archivo = item.Pk_Id_Archivo;
                    EDArchivosAcciones.NombreArchivo = item.NombreArchivo;
                    EDArchivosAcciones.Ruta = item.Ruta;
                    EDArchivosAcciones.Fk_Id_Accion = item.Fk_Id_Accion;
                    EDArchivosAcciones.Estado = 1;
                    EDArchivosAcciones.IdFile = NumeroArchivo;
                    EDListaArchivos.Add(EDArchivosAcciones);
                }
            }
            }
            EDConsulta.ActividadLista = EDListaActividad;
            EDConsulta.SeguimientoLista = EDListaSeguimiento;
            EDConsulta.ArchivosLista = EDListaArchivos;
            EDConsulta.AnalisisLista = EDListaAnalisis;
            EDConsulta.HallazgoLista = EDListaHallazgos;

            if (NumeroActividadesAbiertas==0 && AccionConsulta.Eficacia == "Implementada y eficaz")
            {
                if (AccionConsulta.Estado=="Abierta")
                {
                    EDConsulta.Estado = "Cerrada";
                }
            }
            if (NumeroActividadesAbiertas > 0)
            {
                if (AccionConsulta.Estado == "Cerrada")
                {
                    EDConsulta.Estado = "Abierta";
                }
            }
            if (AccionConsulta.Eficacia != "Implementada y eficaz")
            {
                if (AccionConsulta.Estado == "Cerrada")
                {
                    EDConsulta.Estado = "Abierta";
                }
            }

            return EDConsulta;
        }
        public void ConsultaAccionEstado(int idEmpresa)
        {
            List<Accion> AccionConsultaLista = new List<Accion>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Acciondef = (from s in db.Tbl_Acciones
                                 where s.Fk_Id_Empresa == idEmpresa
                                 select s).ToList<Accion>();

                if (Acciondef != null)
                {
                    AccionConsultaLista = Acciondef;
                }
            }
            foreach (var item in AccionConsultaLista)
            {
                int NumeroActividadesAbiertas = 0;
                //Estados: -1 Indeterminado, 0 Cerrada, 1 Abierta
                int  Estado = -1;
                Accion AccionConsulta = item;
                List<ActividadAccion> ListaActividad = new List<ActividadAccion>();
                //Actividades
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ActividadList = (from s in db.Tbl_ActividadAccion
                                         where s.Fk_Id_Accion == AccionConsulta.Pk_Id_Accion
                                         select s).ToList<ActividadAccion>();

                    if (ActividadList != null)
                    {
                        ListaActividad = ActividadList;
                    }
                }
                if (ListaActividad.Count()>0)
                {
                    foreach (var item1 in ListaActividad)
                    {
                        if (item1.Estado==1)
                        {
                            NumeroActividadesAbiertas = NumeroActividadesAbiertas + 1;
                        }
                    }
                }

                if (NumeroActividadesAbiertas>0)
                {
                    Estado = 1;
                }
                else
                {
                    if (AccionConsulta.Eficacia== "Implementada y eficaz")
                    {
                        Estado = 0;
                    }
                    else
                    {
                        Estado = 1;
                    }
                }

                //Generar Cambio de Estado
                if (Estado==0)
                {
                    if (AccionConsulta.Estado=="Abierta")
                    {
                        AccionConsulta.Estado = "Cerrada";
                        try
                        {
                            using (SG_SSTContext db = new SG_SSTContext())
                            {
                                db.Entry(AccionConsulta).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                    }
                }
                if (Estado==1)
                {
                    if (AccionConsulta.Estado == "Cerrada")
                    {
                        AccionConsulta.Estado = "Abierta";
                        try
                        {
                            using (SG_SSTContext db = new SG_SSTContext())
                            {
                                db.Entry(AccionConsulta).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                    }
                }


            }



        }
        public EDActividad ConsultarActividad(int IdActividad)
        {
            ActividadAccion Actividad = new ActividadAccion();
            EDActividad EDActividad = new EDActividad();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var Acciondef = (from s in db1.Tbl_ActividadAccion
                                 where s.Pk_Id_Actividad == IdActividad
                                 select s).FirstOrDefault<ActividadAccion>();

                if (Acciondef != null)
                {
                    Actividad = Acciondef;
                }
            }
            EDActividad.Pk_Id_Actividad = Actividad.Pk_Id_Actividad;
            EDActividad.Actividad = Actividad.Actividad;
            EDActividad.Responsable = Actividad.Responsable;
            EDActividad.FechaFinalizacion = Actividad.FechaFinalizacion;
            EDActividad.NombreArchivoAct = Actividad.NombreArchivoAct;
            EDActividad.RutaArchivoAct = Actividad.RutaArchivoAct;
            return EDActividad;
        }
        public EDSeguimiento ConsultarSeguimiento(int IdSeguimiento)
        {
            Seguimiento Seguimiento = new Seguimiento();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var Acciondef = (from s in db1.Tbl_Seguimiento
                                 where s.Pk_Id_Seguimiento == IdSeguimiento
                                 select s).FirstOrDefault<Seguimiento>();
                if (Acciondef != null)
                {
                    Seguimiento = Acciondef;
                }
            }
            EDSeguimiento EDSeguimiento = new EDSeguimiento();
            EDSeguimiento.Pk_Id_Seguimiento = Seguimiento.Pk_Id_Seguimiento;
            EDSeguimiento.Fecha_Seg = Seguimiento.Fecha_Seg;
            EDSeguimiento.Observaciones = Seguimiento.Observaciones;
            EDSeguimiento.NombreArchivoSeg = Seguimiento.NombreArchivoSeg;
            EDSeguimiento.RutaArchivoSeg = Seguimiento.RutaArchivoSeg;
            EDSeguimiento.Fk_Id_Accion = Seguimiento.Fk_Id_Accion;
            return EDSeguimiento;
        }
        public List<EDAnalisis> ConsultarAnalisis(int Tipo, int IdAccion)
        {
            List<Analisis> ListaAnalisisBD = new List<Analisis>();
            List<EDAnalisis> EDListaAnalisisBD = new List<EDAnalisis>();

            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var AnalisisList = (from s in db1.Tbl_Analisis
                                    where s.Fk_Id_Accion == IdAccion && s.Tipo == Tipo
                                    select s).ToList<Analisis>();

                if (AnalisisList != null)
                {
                    ListaAnalisisBD = AnalisisList;
                }
            }

            foreach (var item in ListaAnalisisBD)
            {
                EDAnalisis EDAnalisis = new EDAnalisis();

                EDAnalisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                EDAnalisis.Id_Analisis = item.Id_Analisis;
                EDAnalisis.Tipo = item.Tipo;
                EDAnalisis.ValorTxt = item.ValorTxt;
                EDAnalisis.Parent_Id = item.Parent_Id;
                EDAnalisis.Fk_Id_Accion = item.Fk_Id_Accion;

                EDListaAnalisisBD.Add(EDAnalisis);
            }

            return EDListaAnalisisBD;
        }
        public List<EDHallazgo> ListaHallazgos(int IdAccion)
        {
            List<Hallazgo> ListaHallazgos = new List<Hallazgo>();
            List<EDHallazgo> ListaEDHallazgos = new List<EDHallazgo>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var AnalisisList = (from s in db1.Tbl_Hallazgo
                                    where s.Fk_Id_Accion == IdAccion
                                    select s).ToList<Hallazgo>();
                if (AnalisisList != null)
                {
                    ListaHallazgos = AnalisisList;
                    foreach (var item in ListaHallazgos)
                    {
                        EDHallazgo EDHallazgo = new EDHallazgo();
                        EDHallazgo.Pk_Id_Hallazgo = item.Pk_Id_Hallazgo;
                        EDHallazgo.Halla_Norma = item.Halla_Norma;
                        EDHallazgo.Halla_Numeral = item.Halla_Numeral;
                        EDHallazgo.Halla_Descripcion = item.Halla_Descripcion;
                        EDHallazgo.Halla_Proceso = item.Halla_Proceso;
                        EDHallazgo.Fk_Id_Accion = item.Fk_Id_Accion;
                        EDHallazgo.Fk_Id_Proceso = item.Fk_Id_Proceso;
                        ListaEDHallazgos.Add(EDHallazgo);
                    }
                }
            }

            return ListaEDHallazgos;
        }
        public List<EDArchivosAcciones> ListaArchivos(int IdAccion)
        {
            List<ArchivosAccion> ListaArchivo = new List<ArchivosAccion>();
            List<EDArchivosAcciones> ListaEDArchivo = new List<EDArchivosAcciones>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var AnalisisList = (from s in db1.Tbl_ArchivosAccion
                                    where s.Fk_Id_Accion == IdAccion
                                    select s).ToList<ArchivosAccion>();

                if (AnalisisList != null)
                {
                    ListaArchivo = AnalisisList;
                    foreach (var item in ListaArchivo)
                    {
                        EDArchivosAcciones EDArchivo = new EDArchivosAcciones();
                        EDArchivo.Pk_Id_Archivo = item.Pk_Id_Archivo;
                        EDArchivo.NombreArchivo = item.NombreArchivo;
                        EDArchivo.Ruta = item.Ruta;
                        EDArchivo.Fk_Id_Accion = item.Fk_Id_Accion;
                        ListaEDArchivo.Add(EDArchivo);
                    }
                }
            }
            return ListaEDArchivo;
        }
        public List<EDAnalisis> ListaAnalisis(int IdAccion)
        {
            List<Analisis> ListaAnalisis = new List<Analisis>();
            List<EDAnalisis> ListaEDAnalisis = new List<EDAnalisis>();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var AnalisisList = (from s in db1.Tbl_Analisis
                                    where s.Fk_Id_Accion == IdAccion
                                    select s).ToList<Analisis>();

                if (AnalisisList != null)
                {
                    ListaAnalisis = AnalisisList;
                    foreach (var item in ListaAnalisis)
                    {
                        EDAnalisis EDAnalisis = new EDAnalisis();
                        EDAnalisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                        EDAnalisis.Id_Analisis = item.Id_Analisis;
                        EDAnalisis.Tipo = item.Tipo;
                        EDAnalisis.ValorTxt = item.ValorTxt;
                        EDAnalisis.Parent_Id = item.Parent_Id;
                        ListaEDAnalisis.Add(EDAnalisis);
                    }
                }
            }
            return ListaEDAnalisis;
        }
        public bool EliminarEncontrarAccion(int IdAccion, int IdEmpresa)
        {
            bool ProbarExistencia = false;
            Accion AccionEliminar = new Accion();
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var Acciondef = (from s in db1.Tbl_Acciones
                                 where s.Pk_Id_Accion == IdAccion && s.Fk_Id_Empresa== IdEmpresa
                                 select s).FirstOrDefault<Accion>();
                if (Acciondef != null)
                {
                    ProbarExistencia = true;
                    AccionEliminar = Acciondef;
                }
            }
            if (ProbarExistencia)
            {
                List<Hallazgo> ListaHallazgos = new List<Hallazgo>();
                List<ActividadAccion> ListaActividad = new List<ActividadAccion>();
                List<Seguimiento> ListaSeguimiento = new List<Seguimiento>();
                List<Analisis> ListaAnalisis = new List<Analisis>();
                List<ArchivosAccion> ListaArchivos = new List<ArchivosAccion>();

                //Analisis
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var AnalisisList = (from s in db1.Tbl_Analisis
                                        where s.Fk_Id_Accion == IdAccion
                                        select s).ToList<Analisis>();

                    if (AnalisisList != null)
                    {
                        ListaAnalisis = AnalisisList;
                    }
                }
                //Actividades
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var ActividadList = (from s in db1.Tbl_ActividadAccion
                                         where s.Fk_Id_Accion == IdAccion
                                         select s).ToList<ActividadAccion>();

                    if (ActividadList != null)
                    {
                        ListaActividad = ActividadList;
                    }
                }
                //Seguimientos
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var SeguimientoList = (from s in db1.Tbl_Seguimiento
                                           where s.Fk_Id_Accion == IdAccion
                                           select s).ToList<Seguimiento>();

                    if (SeguimientoList != null)
                    {
                        ListaSeguimiento = SeguimientoList;
                    }
                }
                //Archivos
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var ArchivosList = (from s in db1.Tbl_ArchivosAccion
                                        where s.Fk_Id_Accion == IdAccion
                                        select s).ToList<ArchivosAccion>();

                    if (ArchivosList != null)
                    {
                        ListaArchivos = ArchivosList;
                    }
                }
                try
                {
                    List<string> ListaArchivosServidor = new List<string>();
                    using (SG_SSTContext db1 = new SG_SSTContext())
                    {
                        //Eliminar Hallazgos
                        foreach (var item in ListaHallazgos)
                        {
                            db1.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                        //Eliminar Analisis
                        foreach (var item in ListaAnalisis)
                        {
                            db1.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                        //Eliminar Actividades
                        foreach (var item in ListaActividad)
                        {
                            if (item.NombreArchivoAct != null && item.RutaArchivoAct != null)
                            {
                                //ListaArchivosServidor.Add(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(item.RutaArchivoAct, item.NombreArchivoAct)));
                                ListaArchivosServidor.Add("");
                            }
                            db1.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                        //Eliminar Seguimientos
                        foreach (var item in ListaSeguimiento)
                        {
                            if (item.NombreArchivoSeg != null && item.RutaArchivoSeg != null)
                            {
                                //ListaArchivosServidor.Add(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(item.RutaArchivoSeg, item.NombreArchivoSeg)));
                                ListaArchivosServidor.Add("");
                            }
                            db1.Entry(item).State = System.Data.Entity.EntityState.Deleted;

                        }
                        //Eliminar Archivos
                        foreach (var item in ListaArchivos)
                        {
                            if (item.NombreArchivo != null && item.Ruta != null)
                            {
                                //ListaArchivosServidor.Add(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(item.Ruta, item.NombreArchivo)));
                                ListaArchivosServidor.Add("");
                            }
                            db1.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                        //Eliminar Accion
                        if (AccionEliminar.NombreArchivoAuditor != null && AccionEliminar.RutaArchivoAuditor != null)
                        {
                            //ListaArchivosServidor.Add(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(AccionEliminar.RutaArchivoAuditor, AccionEliminar.NombreArchivoAuditor)));
                            ListaArchivosServidor.Add("");
                        }
                        if (AccionEliminar.NombreArchivoResp != null && AccionEliminar.RutaArchivoResp != null)
                        {
                            //ListaArchivosServidor.Add(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(AccionEliminar.RutaArchivoResp, AccionEliminar.NombreArchivoResp)));
                            ListaArchivosServidor.Add("");
                        }
                        db1.Entry(AccionEliminar).State = System.Data.Entity.EntityState.Deleted;
                        db1.SaveChanges();

                        //Eliminar Archivos del servidor
                        foreach (var item in ListaArchivosServidor)
                        {
                            if (System.IO.File.Exists(item.ToString()))
                            {
                                System.IO.File.Delete(item.ToString());
                            }
                        }


                    }
                }
                catch (Exception)
                {
                    ProbarExistencia = false;
                }

            }
            return ProbarExistencia;
        }
        public List<EDCargo> ListaCargos()
        {
            List<EDCargo> ListaEDCargos = new List<EDCargo>();
            List<Cargo> ListaCargos = new List<Cargo>();
            using (SG_SSTContext db = new SG_SSTContext())
            {

                var Cargos = (from s in db.Tbl_Cargo

                              select s).ToList<Cargo>();
                if (Cargos != null)
                {
                    ListaCargos = Cargos;
                }
            }
            foreach (var item in ListaCargos)
            {
                EDCargo EDCargo = new EDCargo();
                EDCargo.IDCargo = item.Pk_Id_Cargo;
                EDCargo.NombreCargo = item.Nombre_Cargo;
                ListaEDCargos.Add(EDCargo);
            }
            return ListaEDCargos;
        }
        public bool EditarAnalisis(List<EDAnalisis> ListaAnalisisGuardar, int Tipo, int IdAccion)
        {

            bool Resultado = false;
            return Resultado;
        }
        public List<EDAnalisis> ConsultaAnalisisEdicion(int IdAccion, int idEmpresa, short Tipo)
        {

            Accion AccionConsulta = new Accion();
            List<EDAnalisis> ListaAnalisisBD = new List<EDAnalisis>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Acciondef = (from s in db.Tbl_Acciones
                                 where s.Pk_Id_Accion == IdAccion && s.Fk_Id_Empresa == idEmpresa
                                 select s).FirstOrDefault<Accion>();

                if (Acciondef != null)
                {
                    AccionConsulta = Acciondef;
                }
            }

            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var AnalisisList = (from s in db1.Tbl_Analisis
                                    where s.Fk_Id_Accion == IdAccion && s.Tipo == Tipo
                                    select s).ToList<Analisis>();

                if (AnalisisList != null)
                {
                    foreach (var item in AnalisisList)
                    {
                        EDAnalisis EDAnalisis = new EDAnalisis();
                        EDAnalisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                        EDAnalisis.Id_Analisis = item.Id_Analisis;
                        EDAnalisis.Tipo = Tipo;
                        EDAnalisis.ValorTxt = item.ValorTxt;
                        EDAnalisis.Parent_Id = item.Parent_Id;
                        EDAnalisis.Fk_Id_Accion = IdAccion;
                        ListaAnalisisBD.Add(EDAnalisis);
                    }
                }
            }
            return ListaAnalisisBD;
        }
        public bool GuardarCambiosAnalisis(List<EDAnalisis> EDAnalisisGuardar, List<EDAnalisis> EDAnalisisEditar, List<EDAnalisis> EDAnalisisEliminar, short Tipo, int IdAccion)
        {
            bool probar = false;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {

                    foreach (var item in EDAnalisisEditar)
                    {
                        Analisis Analisis = new Analisis();
                        Analisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                        Analisis.Id_Analisis = item.Id_Analisis;
                        Analisis.Tipo = Tipo;
                        Analisis.ValorTxt = item.ValorTxt;
                        Analisis.Parent_Id = item.Parent_Id;
                        Analisis.Fk_Id_Accion = IdAccion;
                        db.Entry(Analisis).State = EntityState.Modified;
                    }
                    foreach (var item in EDAnalisisEliminar)
                    {
                        Analisis Analisis = new Analisis();
                        Analisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                        db.Entry(Analisis).State = System.Data.Entity.EntityState.Deleted;
                    }
                    foreach (var item in EDAnalisisGuardar)
                    {
                        Analisis Analisis = new Analisis();
                        Analisis.Pk_Id_Analisis = item.Pk_Id_Analisis;
                        Analisis.Id_Analisis = item.Id_Analisis;
                        Analisis.Tipo = Tipo;
                        Analisis.ValorTxt = item.ValorTxt;
                        Analisis.Parent_Id = item.Parent_Id;
                        Analisis.Fk_Id_Accion = IdAccion;
                        db.Tbl_Analisis.Add(Analisis);
                    }
                    db.SaveChanges();
                    probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return probar;
        }
        public bool EliminarAnalisis(List<Analisis> ListaAnalisisGuardar, int Tipo, int IdAccion)
        {
            bool Probar = true;
            Tipo = 3;


            List<Analisis> ListaAnalisis = new List<Analisis>();
            //Analisis
            using (SG_SSTContext db1 = new SG_SSTContext())
            {
                var AnalisisList = (from s in db1.Tbl_Analisis
                                    where s.Fk_Id_Accion == IdAccion && s.Tipo == Tipo
                                    select s).ToList<Analisis>();

                if (AnalisisList != null)
                {
                    ListaAnalisis = AnalisisList;
                }
                foreach (var item in ListaAnalisis)
                {
                    db1.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                foreach (var item in ListaAnalisisGuardar)
                {
                    //Ingresar Analisis Nuevos
                    if (item.ValorTxt == null)
                    {

                    }
                    else
                    {
                        if (item.ValorTxt == "")
                        {
                            item.ValorTxt = "N/A";
                        }
                    }

                    item.Fk_Id_Accion = IdAccion;
                    db1.Tbl_Analisis.Add(item);
                }

                db1.SaveChanges();

            }




            return Probar;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void VerificarEstado(Accion AccionVer)
        {
            List<ActividadAccion> PlanAccion = new List<ActividadAccion>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var AnalisisList = (from s in db.Tbl_ActividadAccion
                                    where s.Fk_Id_Accion == AccionVer.Pk_Id_Accion
                                    select s).ToList<ActividadAccion>();
                if (AnalisisList != null)
                {
                    PlanAccion = AnalisisList;
                }
            }
            int NumeroPlanes = PlanAccion.Count;
            int NumeroPlanes_cerrados = 0;
            foreach (var item in PlanAccion)
            {
                if (item.Estado == 0)
                {
                    NumeroPlanes_cerrados++;
                }
            }

            //Estado Abierto evento=1
            //Estado Cerrado evento=0
            //Estado Indeterminado evento=-1
            int evento = -1;
            if (AccionVer.Eficacia== "Implementada y eficaz")
            {
                if (NumeroPlanes== NumeroPlanes_cerrados)
                {
                    evento = 0;
                }
            }
            else
            {
                evento = 1;
            }
            if (NumeroPlanes != NumeroPlanes_cerrados)
            {
                evento = 1;
            }

            if (evento==0)
            {
                AccionVer.Estado = "Cerrada";
            }
            if (evento == 1)
            {
                AccionVer.Estado = "Abierta";
            }
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AccionVer).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
        }

    }
}

