using SG_SST.EntidadesDominio.Aplicacion;
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
    public class EHMManager: IEHM
    {
        public static List<string> ListaNodos = new List<string>();
        public bool GuardarHojaVidaEMH(EDAdmoEMH EDAdmoEMH, List<EDPeligroEMH> ListaPeligro)
        {
            bool Probar = false;
            AdmoEMH NuevoAdmoEMH = new AdmoEMH();
            List<PeligroEMH> ListaPeligroSave = new List<PeligroEMH>();
            NuevoAdmoEMH.Pk_Id_AdmoEMH = EDAdmoEMH.Pk_Id_AdmoEMH;
            NuevoAdmoEMH.TipoElemento = EDAdmoEMH.TipoElemento;
            NuevoAdmoEMH.NombreElemento = EDAdmoEMH.NombreElemento;
            NuevoAdmoEMH.CodigoElemento = EDAdmoEMH.CodigoElemento;
            NuevoAdmoEMH.Marca = EDAdmoEMH.Marca;
            NuevoAdmoEMH.Modelo = EDAdmoEMH.Modelo;
            NuevoAdmoEMH.Fabricante = EDAdmoEMH.Fabricante;
            NuevoAdmoEMH.Fecha_Fab = EDAdmoEMH.Fecha_Fab;
            NuevoAdmoEMH.HorasVida = EDAdmoEMH.HorasVida;
            NuevoAdmoEMH.Ubicacion = EDAdmoEMH.Ubicacion;
            NuevoAdmoEMH.Caracteristicas = EDAdmoEMH.Caracteristicas;
            NuevoAdmoEMH.NombreResponsable = EDAdmoEMH.NombreResponsable;
            NuevoAdmoEMH.CargoResponsable = EDAdmoEMH.CargoResponsable;
            NuevoAdmoEMH.Estado = EDAdmoEMH.Estado;
            NuevoAdmoEMH.ArchivoImagen1 = EDAdmoEMH.ArchivoImagen1;
            NuevoAdmoEMH.RutaImage1 = EDAdmoEMH.RutaImage1;
            NuevoAdmoEMH.ArchivoImagen2 = EDAdmoEMH.ArchivoImagen2;
            NuevoAdmoEMH.RutaImage2 = EDAdmoEMH.RutaImage2;
            NuevoAdmoEMH.ArchivoImagen3 = EDAdmoEMH.ArchivoImagen3;
            NuevoAdmoEMH.RutaImage3 = EDAdmoEMH.RutaImage3;
            NuevoAdmoEMH.ArchivoImagen4 = EDAdmoEMH.ArchivoImagen4;
            NuevoAdmoEMH.RutaImage4 = EDAdmoEMH.RutaImage4;
            NuevoAdmoEMH.ArchivoImagen5 = EDAdmoEMH.ArchivoImagen5;
            NuevoAdmoEMH.RutaImage5 = EDAdmoEMH.RutaImage5;
            NuevoAdmoEMH.NombreArchivo1 = EDAdmoEMH.NombreArchivo1;
            NuevoAdmoEMH.Ruta1 = EDAdmoEMH.Ruta1;
            NuevoAdmoEMH.NombreArchivo2 = EDAdmoEMH.NombreArchivo2;
            NuevoAdmoEMH.Ruta2 = EDAdmoEMH.Ruta2;
            NuevoAdmoEMH.NombreArchivo3 = EDAdmoEMH.NombreArchivo3;
            NuevoAdmoEMH.Ruta3 = EDAdmoEMH.Ruta3;
            NuevoAdmoEMH.ArchivoImagen1_download = EDAdmoEMH.Imgdownload1;
            NuevoAdmoEMH.ArchivoImagen2_download = EDAdmoEMH.Imgdownload2;
            NuevoAdmoEMH.ArchivoImagen3_download = EDAdmoEMH.Imgdownload3;
            NuevoAdmoEMH.ArchivoImagen4_download = EDAdmoEMH.Imgdownload4;
            NuevoAdmoEMH.ArchivoImagen5_download = EDAdmoEMH.Imgdownload5;
            NuevoAdmoEMH.NombreArchivo1_download = EDAdmoEMH.Filedownload1;
            NuevoAdmoEMH.NombreArchivo2_download = EDAdmoEMH.Filedownload2;
            NuevoAdmoEMH.NombreArchivo3_download = EDAdmoEMH.Filedownload3;
            NuevoAdmoEMH.Fk_Id_Empresa = EDAdmoEMH.Fk_Id_Empresa;

            foreach (var item in ListaPeligro)
            {
                PeligroEMH PeligroEMH = new PeligroEMH();
                PeligroEMH.Fk_Id_Peligro = item.Fk_Id_Peligro;
                PeligroEMH.AdmoEMH = NuevoAdmoEMH;
                ListaPeligroSave.Add(PeligroEMH);
            }

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_AdministracionEMH.Add(NuevoAdmoEMH);
                    foreach (var item in ListaPeligroSave)
                    {
                        db.Tbl_PeligroEMH.Add(item);
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

            //if (Probar)
            //{
            //    List<AdmoEMH> ListaAdmoEHM = new List<AdmoEMH>();
            //    using (SG_SSTContext db = new SG_SSTContext())
            //    {
                    
            //        var EHM_Record = (from s in db.Tbl_AdministracionEMH
            //                          where s.Fk_Id_Empresa == EDAdmoEMH.Fk_Id_Empresa
            //                          select s).ToList<AdmoEMH>();

            //        if (EHM_Record != null)
            //        {
            //            ListaAdmoEHM = EHM_Record;
            //        }
            //    }
            //    int IdMaxLista = ListaAdmoEHM.Max(item => item.Pk_Id_AdmoEMH);
            //    foreach (var item in ListaPeligro)
            //    {
            //        try
            //        {
            //            PeligroEMH PeligroEMH = new PeligroEMH();
            //            PeligroEMH.Fk_Id_Peligro = item.Fk_Id_Peligro;
            //            PeligroEMH.Fk_Id_AdmoEMH = IdMaxLista;
            //            using (SG_SSTContext db = new SG_SSTContext())
            //            {
            //                db.Tbl_PeligroEMH.Add(PeligroEMH);
            //                db.SaveChanges();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            string mensaje = ex.ToString();
            //        }
            //    }
            //}
            return Probar;
        }
        public bool EditarHojaVidaEMH(EDAdmoEMH EDAdmoEMH, List<EDPeligroEMH> ListaPeligro)
        {
            bool Probar = false;

            AdmoEMH EditarAdmoEMH = new AdmoEMH();
            EditarAdmoEMH.Pk_Id_AdmoEMH = EDAdmoEMH.Pk_Id_AdmoEMH;
            EditarAdmoEMH.TipoElemento = EDAdmoEMH.TipoElemento;
            EditarAdmoEMH.NombreElemento = EDAdmoEMH.NombreElemento;
            EditarAdmoEMH.CodigoElemento = EDAdmoEMH.CodigoElemento;
            EditarAdmoEMH.Marca = EDAdmoEMH.Marca;
            EditarAdmoEMH.Modelo = EDAdmoEMH.Modelo;
            EditarAdmoEMH.Fabricante = EDAdmoEMH.Fabricante;
            EditarAdmoEMH.Fecha_Fab = EDAdmoEMH.Fecha_Fab;
            EditarAdmoEMH.HorasVida = EDAdmoEMH.HorasVida;
            EditarAdmoEMH.Ubicacion = EDAdmoEMH.Ubicacion;
            EditarAdmoEMH.Caracteristicas = EDAdmoEMH.Caracteristicas;
            EditarAdmoEMH.NombreResponsable = EDAdmoEMH.NombreResponsable;
            EditarAdmoEMH.CargoResponsable = EDAdmoEMH.CargoResponsable;
            EditarAdmoEMH.Estado = EDAdmoEMH.Estado;
            EditarAdmoEMH.ArchivoImagen1 = EDAdmoEMH.ArchivoImagen1;
            EditarAdmoEMH.RutaImage1 = EDAdmoEMH.RutaImage1;
            EditarAdmoEMH.ArchivoImagen2 = EDAdmoEMH.ArchivoImagen2;
            EditarAdmoEMH.RutaImage2 = EDAdmoEMH.RutaImage2;
            EditarAdmoEMH.ArchivoImagen3 = EDAdmoEMH.ArchivoImagen3;
            EditarAdmoEMH.RutaImage3 = EDAdmoEMH.RutaImage3;
            EditarAdmoEMH.ArchivoImagen4 = EDAdmoEMH.ArchivoImagen4;
            EditarAdmoEMH.RutaImage4 = EDAdmoEMH.RutaImage4;
            EditarAdmoEMH.ArchivoImagen5 = EDAdmoEMH.ArchivoImagen5;
            EditarAdmoEMH.RutaImage5 = EDAdmoEMH.RutaImage5;
            EditarAdmoEMH.NombreArchivo1 = EDAdmoEMH.NombreArchivo1;
            EditarAdmoEMH.Ruta1 = EDAdmoEMH.Ruta1;
            EditarAdmoEMH.NombreArchivo2 = EDAdmoEMH.NombreArchivo2;
            EditarAdmoEMH.Ruta2 = EDAdmoEMH.Ruta2;
            EditarAdmoEMH.NombreArchivo3 = EDAdmoEMH.NombreArchivo3;
            EditarAdmoEMH.Ruta3 = EDAdmoEMH.Ruta3;
            EditarAdmoEMH.ArchivoImagen1_download = EDAdmoEMH.Imgdownload1;
            EditarAdmoEMH.ArchivoImagen2_download = EDAdmoEMH.Imgdownload2;
            EditarAdmoEMH.ArchivoImagen3_download = EDAdmoEMH.Imgdownload3;
            EditarAdmoEMH.ArchivoImagen4_download = EDAdmoEMH.Imgdownload4;
            EditarAdmoEMH.ArchivoImagen5_download = EDAdmoEMH.Imgdownload5;
            EditarAdmoEMH.NombreArchivo1_download = EDAdmoEMH.Filedownload1;
            EditarAdmoEMH.NombreArchivo2_download = EDAdmoEMH.Filedownload2;
            EditarAdmoEMH.NombreArchivo3_download = EDAdmoEMH.Filedownload3;
            EditarAdmoEMH.Fk_Id_Empresa = EDAdmoEMH.Fk_Id_Empresa;

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(EditarAdmoEMH).State = EntityState.Modified;
                    db.SaveChanges();
                    Probar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return Probar;
            }

            if (Probar && ListaPeligro != null)
            {
                List<PeligroEMH> ListaPeligroExistente = new List<PeligroEMH>();
                List<PeligroEMH> ListaPeligroGuardar = new List<PeligroEMH>();
                List<PeligroEMH> ListaPeligroEliminar = new List<PeligroEMH>();
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Peligro_Record = (from s in db.Tbl_PeligroEMH
                                          where s.Fk_Id_AdmoEMH == EDAdmoEMH.Pk_Id_AdmoEMH
                                          select s).ToList<PeligroEMH>();
                    if (Peligro_Record != null)
                    {
                        ListaPeligroExistente = Peligro_Record;
                    }
                }
                foreach (var item in ListaPeligro)
                {
                    PeligroEMH PeligroEMH = ListaPeligroExistente.Where(s => s.Fk_Id_Peligro == item.Fk_Id_Peligro).FirstOrDefault();
                    if (PeligroEMH == null)
                    {
                        PeligroEMH PeligroEMH_S = new PeligroEMH();
                        PeligroEMH_S.Fk_Id_AdmoEMH = item.Fk_Id_AdmoEMH;
                        PeligroEMH_S.Fk_Id_Peligro = item.Fk_Id_Peligro;
                        ListaPeligroGuardar.Add(PeligroEMH_S);
                    }
                }
                foreach (var item in ListaPeligroExistente)
                {
                    EDPeligroEMH EDPeligroEMH = ListaPeligro.Where(s => s.Fk_Id_Peligro == item.Fk_Id_Peligro).FirstOrDefault();
                    if (EDPeligroEMH == null)
                    {
                        PeligroEMH PeligroEMH_E = new PeligroEMH();
                        PeligroEMH_E.Fk_Id_AdmoEMH = item.Fk_Id_AdmoEMH;
                        PeligroEMH_E.Pk_Id_PeligroEMH = item.Pk_Id_PeligroEMH;
                        PeligroEMH_E.Fk_Id_Peligro = item.Fk_Id_Peligro;
                        ListaPeligroEliminar.Add(PeligroEMH_E);
                    }
                }
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    foreach (var item in ListaPeligroGuardar)
                    {
                        try
                        {
                            PeligroEMH PeligroEMH_save = new PeligroEMH();
                            PeligroEMH_save.Fk_Id_Peligro = item.Fk_Id_Peligro;
                            PeligroEMH_save.Fk_Id_AdmoEMH = EDAdmoEMH.Pk_Id_AdmoEMH;
                            db.Tbl_PeligroEMH.Add(PeligroEMH_save);

                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                    }
                    foreach (var item in ListaPeligroEliminar)
                    {
                        try
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                    }
                    db.SaveChanges();
                }
            }

            if (Probar && ListaPeligro == null)
            {
                List<PeligroEMH> ListaPeligroExistente = new List<PeligroEMH>();
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Peligro_Record = (from s in db.Tbl_PeligroEMH
                                          where s.Fk_Id_AdmoEMH == EDAdmoEMH.Pk_Id_AdmoEMH
                                          select s).ToList<PeligroEMH>();
                    if (Peligro_Record != null)
                    {
                        ListaPeligroExistente = Peligro_Record;
                    }
                }
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    foreach (var item in ListaPeligroExistente)
                    {
                        try
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                    }
                    db.SaveChanges();
                }
            }
            return Probar;
        }
        public bool DarBajaEMH(EDAdmoEMH EDAdmoEMH)
        {
            bool Probar = false;
            AdmoEMH EditarAdmoEMH = new AdmoEMH();
            EditarAdmoEMH.Pk_Id_AdmoEMH = EDAdmoEMH.Pk_Id_AdmoEMH;
            EditarAdmoEMH.TipoElemento = EDAdmoEMH.TipoElemento;
            EditarAdmoEMH.NombreElemento = EDAdmoEMH.NombreElemento;
            EditarAdmoEMH.CodigoElemento = EDAdmoEMH.CodigoElemento;
            EditarAdmoEMH.Marca = EDAdmoEMH.Marca;
            EditarAdmoEMH.Modelo = EDAdmoEMH.Modelo;
            EditarAdmoEMH.Fabricante = EDAdmoEMH.Fabricante;
            EditarAdmoEMH.Fecha_Fab = EDAdmoEMH.Fecha_Fab;
            EditarAdmoEMH.HorasVida = EDAdmoEMH.HorasVida;
            EditarAdmoEMH.Ubicacion = EDAdmoEMH.Ubicacion;
            EditarAdmoEMH.Caracteristicas = EDAdmoEMH.Caracteristicas;
            EditarAdmoEMH.NombreResponsable = EDAdmoEMH.NombreResponsable;
            EditarAdmoEMH.CargoResponsable = EDAdmoEMH.CargoResponsable;
            EditarAdmoEMH.Estado = 1;
            EditarAdmoEMH.ArchivoImagen1 = EDAdmoEMH.ArchivoImagen1;
            EditarAdmoEMH.RutaImage1 = EDAdmoEMH.RutaImage1;
            EditarAdmoEMH.ArchivoImagen2 = EDAdmoEMH.ArchivoImagen2;
            EditarAdmoEMH.RutaImage2 = EDAdmoEMH.RutaImage2;
            EditarAdmoEMH.ArchivoImagen3 = EDAdmoEMH.ArchivoImagen3;
            EditarAdmoEMH.RutaImage3 = EDAdmoEMH.RutaImage3;
            EditarAdmoEMH.ArchivoImagen4 = EDAdmoEMH.ArchivoImagen4;
            EditarAdmoEMH.RutaImage4 = EDAdmoEMH.RutaImage4;
            EditarAdmoEMH.ArchivoImagen5 = EDAdmoEMH.ArchivoImagen5;
            EditarAdmoEMH.RutaImage5 = EDAdmoEMH.RutaImage5;
            EditarAdmoEMH.NombreArchivo1 = EDAdmoEMH.NombreArchivo1;
            EditarAdmoEMH.Ruta1 = EDAdmoEMH.Ruta1;
            EditarAdmoEMH.NombreArchivo2 = EDAdmoEMH.NombreArchivo2;
            EditarAdmoEMH.Ruta2 = EDAdmoEMH.Ruta2;
            EditarAdmoEMH.NombreArchivo3 = EDAdmoEMH.NombreArchivo3;
            EditarAdmoEMH.Ruta3 = EDAdmoEMH.Ruta3;
            EditarAdmoEMH.ArchivoImagen1_download = EDAdmoEMH.Imgdownload1;
            EditarAdmoEMH.ArchivoImagen2_download = EDAdmoEMH.Imgdownload2;
            EditarAdmoEMH.ArchivoImagen3_download = EDAdmoEMH.Imgdownload3;
            EditarAdmoEMH.ArchivoImagen4_download = EDAdmoEMH.Imgdownload4;
            EditarAdmoEMH.ArchivoImagen5_download = EDAdmoEMH.Imgdownload5;
            EditarAdmoEMH.NombreArchivo1_download = EDAdmoEMH.Filedownload1;
            EditarAdmoEMH.NombreArchivo2_download = EDAdmoEMH.Filedownload2;
            EditarAdmoEMH.NombreArchivo3_download = EDAdmoEMH.Filedownload3;
            EditarAdmoEMH.Fecha_Baja = EDAdmoEMH.Fecha_Baja;
            EditarAdmoEMH.Motivo_Baja = EDAdmoEMH.Motivo_Baja;


            //if (EDAdmoEMH.FK_Clasificacion_De_Peligro != 0)
            //{
            //    EditarAdmoEMH.FK_Clasificacion_De_Peligro = EDAdmoEMH.FK_Clasificacion_De_Peligro;
            //}
            EditarAdmoEMH.Fk_Id_Empresa = EDAdmoEMH.Fk_Id_Empresa;

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(EditarAdmoEMH).State = EntityState.Modified;
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
        public bool SubirEMH(EDAdmoEMH EDAdmoEMH)
        {
            bool Probar = false;
            AdmoEMH EditarAdmoEMH = new AdmoEMH();
            EditarAdmoEMH.Pk_Id_AdmoEMH = EDAdmoEMH.Pk_Id_AdmoEMH;
            EditarAdmoEMH.TipoElemento = EDAdmoEMH.TipoElemento;
            EditarAdmoEMH.NombreElemento = EDAdmoEMH.NombreElemento;
            EditarAdmoEMH.CodigoElemento = EDAdmoEMH.CodigoElemento;
            EditarAdmoEMH.Marca = EDAdmoEMH.Marca;
            EditarAdmoEMH.Modelo = EDAdmoEMH.Modelo;
            EditarAdmoEMH.Fabricante = EDAdmoEMH.Fabricante;
            EditarAdmoEMH.Fecha_Fab = EDAdmoEMH.Fecha_Fab;
            EditarAdmoEMH.HorasVida = EDAdmoEMH.HorasVida;
            EditarAdmoEMH.Ubicacion = EDAdmoEMH.Ubicacion;
            EditarAdmoEMH.Caracteristicas = EDAdmoEMH.Caracteristicas;
            EditarAdmoEMH.NombreResponsable = EDAdmoEMH.NombreResponsable;
            EditarAdmoEMH.CargoResponsable = EDAdmoEMH.CargoResponsable;
            EditarAdmoEMH.Estado = 0;
            EditarAdmoEMH.ArchivoImagen1 = EDAdmoEMH.ArchivoImagen1;
            EditarAdmoEMH.RutaImage1 = EDAdmoEMH.RutaImage1;
            EditarAdmoEMH.ArchivoImagen2 = EDAdmoEMH.ArchivoImagen2;
            EditarAdmoEMH.RutaImage2 = EDAdmoEMH.RutaImage2;
            EditarAdmoEMH.ArchivoImagen3 = EDAdmoEMH.ArchivoImagen3;
            EditarAdmoEMH.RutaImage3 = EDAdmoEMH.RutaImage3;
            EditarAdmoEMH.ArchivoImagen4 = EDAdmoEMH.ArchivoImagen4;
            EditarAdmoEMH.RutaImage4 = EDAdmoEMH.RutaImage4;
            EditarAdmoEMH.ArchivoImagen5 = EDAdmoEMH.ArchivoImagen5;
            EditarAdmoEMH.RutaImage5 = EDAdmoEMH.RutaImage5;
            EditarAdmoEMH.NombreArchivo1 = EDAdmoEMH.NombreArchivo1;
            EditarAdmoEMH.Ruta1 = EDAdmoEMH.Ruta1;
            EditarAdmoEMH.NombreArchivo2 = EDAdmoEMH.NombreArchivo2;
            EditarAdmoEMH.Ruta2 = EDAdmoEMH.Ruta2;
            EditarAdmoEMH.NombreArchivo3 = EDAdmoEMH.NombreArchivo3;
            EditarAdmoEMH.Ruta3 = EDAdmoEMH.Ruta3;
            EditarAdmoEMH.ArchivoImagen1_download = EDAdmoEMH.Imgdownload1;
            EditarAdmoEMH.ArchivoImagen2_download = EDAdmoEMH.Imgdownload2;
            EditarAdmoEMH.ArchivoImagen3_download = EDAdmoEMH.Imgdownload3;
            EditarAdmoEMH.ArchivoImagen4_download = EDAdmoEMH.Imgdownload4;
            EditarAdmoEMH.ArchivoImagen5_download = EDAdmoEMH.Imgdownload5;
            EditarAdmoEMH.NombreArchivo1_download = EDAdmoEMH.Filedownload1;
            EditarAdmoEMH.NombreArchivo2_download = EDAdmoEMH.Filedownload2;
            EditarAdmoEMH.NombreArchivo3_download = EDAdmoEMH.Filedownload3;
            EditarAdmoEMH.Fecha_Baja = null;
            EditarAdmoEMH.Motivo_Baja = null;
            EditarAdmoEMH.Fk_Id_Empresa = EDAdmoEMH.Fk_Id_Empresa;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(EditarAdmoEMH).State = EntityState.Modified;
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
        public List<EDAdmoEMH> ConsultaAdmoEMH(string Tipo, string Nombre, int idEmpresa)
        {

            List<EDAdmoEMH> NuevaLista = new List<EDAdmoEMH>();
            List<AdmoEMH> ListAdmoEMH = new List<AdmoEMH>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_AdministracionEMH
                                where s.Fk_Id_Empresa == idEmpresa
                                select s).ToList<AdmoEMH>();

                if (Listavar != null)
                {
                    ListAdmoEMH = Listavar;
                }

            }
            if (Nombre != null)
            {
                if (Nombre != "")
                {
                    var Listavar = (from s in ListAdmoEMH
                                    where s.NombreElemento.ToLower().Contains(Nombre.ToLower())
                                    select s).ToList<AdmoEMH>();

                    if (Listavar != null)
                    {
                        ListAdmoEMH = Listavar;
                    }
                }
            }
            if (Tipo != null)
            {
                if (Tipo != "")
                {
                    var Listavar = (from s in ListAdmoEMH
                                    where s.TipoElemento == Tipo
                                    select s).ToList<AdmoEMH>();
                    if (Listavar != null)
                    {
                        ListAdmoEMH = Listavar;
                    }
                }
            }

            foreach (var item in ListAdmoEMH)
            {
                EDAdmoEMH ElementoAdmo = new EDAdmoEMH();
                ElementoAdmo.Pk_Id_AdmoEMH = item.Pk_Id_AdmoEMH;
                ElementoAdmo.TipoElemento = item.TipoElemento;
                ElementoAdmo.NombreElemento = item.NombreElemento;
                ElementoAdmo.CodigoElemento = item.CodigoElemento;
                ElementoAdmo.Marca = item.Marca;
                ElementoAdmo.Modelo = item.Modelo;
                ElementoAdmo.Fabricante = item.Fabricante;
                ElementoAdmo.Fecha_Fab = item.Fecha_Fab;
                ElementoAdmo.HorasVida = item.HorasVida;
                ElementoAdmo.Ubicacion = item.Ubicacion;
                ElementoAdmo.Caracteristicas = item.Caracteristicas;
                ElementoAdmo.NombreResponsable = item.NombreResponsable;
                ElementoAdmo.CargoResponsable = item.CargoResponsable;
                ElementoAdmo.Estado = item.Estado;
                ElementoAdmo.ArchivoImagen1 = item.ArchivoImagen1;
                ElementoAdmo.RutaImage1 = item.RutaImage1;
                ElementoAdmo.ArchivoImagen2 = item.ArchivoImagen2;
                ElementoAdmo.RutaImage2 = item.RutaImage2;
                ElementoAdmo.ArchivoImagen3 = item.ArchivoImagen3;
                ElementoAdmo.RutaImage3 = item.RutaImage3;
                ElementoAdmo.ArchivoImagen4 = item.ArchivoImagen4;
                ElementoAdmo.RutaImage4 = item.RutaImage4;
                ElementoAdmo.ArchivoImagen5 = item.ArchivoImagen5;
                ElementoAdmo.RutaImage5 = item.RutaImage5;
                ElementoAdmo.NombreArchivo1 = item.NombreArchivo1;
                ElementoAdmo.Ruta1 = item.Ruta1;
                ElementoAdmo.NombreArchivo2 = item.NombreArchivo2;
                ElementoAdmo.Ruta2 = item.Ruta2;
                ElementoAdmo.NombreArchivo3 = item.NombreArchivo3;
                ElementoAdmo.Ruta3 = item.Ruta3;
                ElementoAdmo.Fk_Id_Empresa = item.Fk_Id_Empresa;
                NuevaLista.Add(ElementoAdmo);
            }
            return NuevaLista;
        }
        public EDAdmoEMH ConsultarEHM(int IdElemento, int IdEmpresa)
        {
            EDAdmoEMH ElementoAdmo = new EDAdmoEMH();
            List<PeligroEMH> ListaPeligroEMH = new List<PeligroEMH>();
            List<EDPeligro> ListaEDPeligro = new List<EDPeligro>();
            AdmoEMH AdmoEMH = new AdmoEMH();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_AdministracionEMH
                                where s.Fk_Id_Empresa == IdEmpresa && s.Pk_Id_AdmoEMH== IdElemento
                                select s).FirstOrDefault<AdmoEMH>();
                if (Listavar != null)
                {
                    AdmoEMH = Listavar;
                }
            }
            ElementoAdmo.Pk_Id_AdmoEMH = AdmoEMH.Pk_Id_AdmoEMH;
            ElementoAdmo.TipoElemento = AdmoEMH.TipoElemento;
            ElementoAdmo.NombreElemento = AdmoEMH.NombreElemento;
            ElementoAdmo.CodigoElemento = AdmoEMH.CodigoElemento;
            ElementoAdmo.Marca = AdmoEMH.Marca;
            ElementoAdmo.Modelo = AdmoEMH.Modelo;
            ElementoAdmo.Fabricante = AdmoEMH.Fabricante;
            ElementoAdmo.Fecha_Fab = AdmoEMH.Fecha_Fab;
            ElementoAdmo.HorasVida = AdmoEMH.HorasVida;
            ElementoAdmo.Ubicacion = AdmoEMH.Ubicacion;
            ElementoAdmo.Caracteristicas = AdmoEMH.Caracteristicas;
            ElementoAdmo.NombreResponsable = AdmoEMH.NombreResponsable;
            ElementoAdmo.CargoResponsable = AdmoEMH.CargoResponsable;
            ElementoAdmo.Estado = AdmoEMH.Estado;
            ElementoAdmo.ArchivoImagen1 = AdmoEMH.ArchivoImagen1;
            ElementoAdmo.RutaImage1 = AdmoEMH.RutaImage1;
            ElementoAdmo.ArchivoImagen2 = AdmoEMH.ArchivoImagen2;
            ElementoAdmo.RutaImage2 = AdmoEMH.RutaImage2;
            ElementoAdmo.ArchivoImagen3 = AdmoEMH.ArchivoImagen3;
            ElementoAdmo.RutaImage3 = AdmoEMH.RutaImage3;
            ElementoAdmo.ArchivoImagen4 = AdmoEMH.ArchivoImagen4;
            ElementoAdmo.RutaImage4 = AdmoEMH.RutaImage4;
            ElementoAdmo.ArchivoImagen5 = AdmoEMH.ArchivoImagen5;
            ElementoAdmo.RutaImage5 = AdmoEMH.RutaImage5;
            ElementoAdmo.NombreArchivo1 = AdmoEMH.NombreArchivo1;
            ElementoAdmo.Ruta1 = AdmoEMH.Ruta1;
            ElementoAdmo.NombreArchivo2 = AdmoEMH.NombreArchivo2;
            ElementoAdmo.Ruta2 = AdmoEMH.Ruta2;
            ElementoAdmo.NombreArchivo3 = AdmoEMH.NombreArchivo3;
            ElementoAdmo.Ruta3 = AdmoEMH.Ruta3;
            ElementoAdmo.Fk_Id_Empresa = AdmoEMH.Fk_Id_Empresa;
            ElementoAdmo.Imgdownload1 = AdmoEMH.ArchivoImagen1_download;
            ElementoAdmo.Imgdownload2 = AdmoEMH.ArchivoImagen2_download;
            ElementoAdmo.Imgdownload3 = AdmoEMH.ArchivoImagen3_download;
            ElementoAdmo.Imgdownload4 = AdmoEMH.ArchivoImagen4_download;
            ElementoAdmo.Imgdownload5 = AdmoEMH.ArchivoImagen5_download;
            ElementoAdmo.Filedownload1 = AdmoEMH.NombreArchivo1_download;
            ElementoAdmo.Filedownload2 = AdmoEMH.NombreArchivo2_download;
            ElementoAdmo.Filedownload3 = AdmoEMH.NombreArchivo3_download;
            ElementoAdmo.Motivo_Baja = AdmoEMH.Motivo_Baja;
            ElementoAdmo.Fecha_Baja = AdmoEMH.Fecha_Baja?? DateTime.Today;



            using (SG_SSTContext db = new SG_SSTContext())
            {
                var ListaPelEHM = (from s in db.Tbl_PeligroEMH
                                where s.Fk_Id_AdmoEMH == ElementoAdmo.Pk_Id_AdmoEMH
                                   select s).ToList<PeligroEMH>();
                if (ListaPelEHM != null)
                {
                    ListaPeligroEMH = ListaPelEHM;
                }
            }

            foreach (var item in ListaPeligroEMH)
            {
                
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ListaPel = (from s in db.Tbl_Peligro
                                       where s.PK_Peligro == item.Fk_Id_Peligro
                                    select s).FirstOrDefault<Peligro>();
                    if (ListaPel != null)
                    {
                        EDPeligro EDPeligro = new EDPeligro();
                        EDPeligro.PK_Peligro = ListaPel.PK_Peligro;
                        EDPeligro.Nombre_Del_Profesional = ListaPel.Nombre_Del_Profesional;
                        EDPeligro.Numero_De_Documento = ListaPel.Numero_De_Documento;
                        EDPeligro.Numero_De_Licencia_SST = ListaPel.Numero_De_Licencia_SST;
                        EDPeligro.Fecha_De_Evaluacion = ListaPel.Fecha_De_Evaluacion;
                        EDPeligro.Lugar = ListaPel.Lugar;
                        EDPeligro.Actividad = ListaPel.Actividad;
                        EDPeligro.Tarea = ListaPel.Tarea;
                        EDPeligro.FLG_Rutinaria = ListaPel.FLG_Rutinaria;
                        EDPeligro.Fuente_Generadora_De_Peligro = ListaPel.Fuente_Generadora_De_Peligro;
                        EDPeligro.Otro = ListaPel.Otro;
                        EDPeligro.Fuente = ListaPel.Fuente;
                        EDPeligro.Medio = ListaPel.Medio;
                        EDPeligro.Eliminacion = ListaPel.Eliminacion;
                        EDPeligro.Sustitucion = ListaPel.Sustitucion;
                        EDPeligro.Controles_De_Ingenieria = ListaPel.Controles_De_Ingenieria;
                        EDPeligro.Controles_Administrativos = ListaPel.Controles_Administrativos;
                        EDPeligro.Elementos_De_Proteccion = ListaPel.Elementos_De_Proteccion;
                        EDPeligro.Accion_De_Prevencion = ListaPel.Accion_De_Prevencion;
                        EDPeligro.FK_Clasificacion_De_Peligro = ListaPel.FK_Clasificacion_De_Peligro;
                        EDPeligro.FK_Sede = ListaPel.FK_Sede;
                        EDPeligro.FK_Proceso = ListaPel.FK_Proceso;
                        ListaEDPeligro.Add(EDPeligro);
                    }
                }
            }
            ElementoAdmo.ListaPeligros = ListaEDPeligro;
            return ElementoAdmo;
        }
        public bool EliminarEHM(int IdElemento, int IdEmpresa)
        {
            bool Probar = false;
            AdmoEMH EHMEliminar = new AdmoEMH();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var ehm = (from s in db.Tbl_AdministracionEMH
                           where s.Pk_Id_AdmoEMH == IdElemento && s.Fk_Id_Empresa == IdEmpresa
                                 select s).FirstOrDefault<AdmoEMH>();
                if (ehm != null)
                {
                    EHMEliminar = ehm;
                }
            }
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(EHMEliminar).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    Probar = true;
                }
            }
            catch (Exception)
            {
                Probar = false;
            }
            return Probar;
        }

        public List<EDProceso> ConsultaSubProcesos(int IdProceso, int Idempresa)
        {
            List<EDProceso> ListaEDProceso = new List<EDProceso>();
            List<Proceso> ListaProceso = new List<Proceso>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Procesos
                                where s.Fk_Id_Proceso == IdProceso
                                select s).ToList<Proceso>();
                if (Listavar != null)
                {
                    ListaProceso = Listavar;
                }
            }
            foreach (var item in ListaProceso)
            {
                EDProceso EDProceso = new EDProceso();
                EDProceso.Descripcion = item.Descripcion_Proceso;
                EDProceso.Id_Proceso = item.Pk_Id_Proceso;
                EDProceso.Id_Proceso_Padre = item.Fk_Id_Proceso;
                ListaEDProceso.Add(EDProceso);
            }
            return ListaEDProceso;
        }

        public List<EDEHMInspecciones> ConsultaInspeccion(string FechaI, string FechaD, int IdEHM, int IdEmpresa)
        {
            List<EDEHMInspecciones> ListaEDInspeccion = new List<EDEHMInspecciones>();
            List<EHMInspecciones> ListaEHMInspeccion = new List<EHMInspecciones>();
            List<Inspecciones> ListaInspeccion = new List<Inspecciones>();
            List<Inspecciones> ListaInspeccion1 = new List<Inspecciones>();
            List<Inspecciones> ListaInspeccion2 = new List<Inspecciones>();
            DateTime FechaA_conv = DateTime.MinValue.Date;
            DateTime FechaD_conv = DateTime.MinValue.Date;

            bool CondicionFecha = false;


            if (FechaI == null || FechaD == null)
            {
                FechaI = null;
                FechaD = null;
                CondicionFecha = false;
            }
            else
            {
                if (FechaI != string.Empty && FechaD != string.Empty)
                {
                    try
                    {
                        FechaA_conv = DateTime.Parse(FechaI);
                        FechaD_conv = DateTime.Parse(FechaD);
                        int hour = FechaD_conv.Hour;
                        int HorasFaltantes = 24 - hour;
                        FechaD_conv=FechaD_conv.AddHours(HorasFaltantes);
                        FechaD_conv=FechaD_conv.AddSeconds(-1);
                    }
                    catch (Exception)
                    {
                    }
                    CondicionFecha = true;
                }
            }
            #region SoloFechas
            

            if (CondicionFecha && IdEHM==0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var inspec = (from s in db.Tbl_Inspecciones
                                  join d in db.Tbl_AdministracionEMHInspecciones on s.Pk_Id_Inspecciones equals d.Fk_Id_Inspecciones
                                  join e in db.Tbl_AdministracionEMH on d.Fk_Id_AdmoEMH equals e.Pk_Id_AdmoEMH
                                  where s.Fk_IdEmpresa == IdEmpresa && e.Fk_Id_Empresa == IdEmpresa 
                                  select s).ToList<Inspecciones>();

                    if (inspec != null)
                    {
                        ListaInspeccion = inspec;
                        ListaInspeccion1 = ListaInspeccion.Where(s => s.PlaneacionInspeccion.Fecha >= FechaA_conv && s.PlaneacionInspeccion.Fecha <= FechaD_conv).ToList();
                    }
                }
            }
            #endregion
            #region soloIdEHM
            if (CondicionFecha==false && IdEHM != 0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var inspec = (from s in db.Tbl_Inspecciones
                                  join d in db.Tbl_AdministracionEMHInspecciones on s.Pk_Id_Inspecciones equals d.Fk_Id_Inspecciones
                                  join e in db.Tbl_AdministracionEMH on d.Fk_Id_AdmoEMH equals e.Pk_Id_AdmoEMH
                                  where s.Fk_IdEmpresa == IdEmpresa && e.Fk_Id_Empresa == IdEmpresa && d.Fk_Id_AdmoEMH== IdEHM
                                  select s).ToList<Inspecciones>();

                    if (inspec != null)
                    {
                        ListaInspeccion1 = inspec;
                    }
                }
            }
            #endregion
            #region ninguno
            if (CondicionFecha==false && IdEHM == 0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var inspec = (from s in db.Tbl_Inspecciones
                                  join d in db.Tbl_AdministracionEMHInspecciones on s.Pk_Id_Inspecciones equals d.Fk_Id_Inspecciones
                                  join e in db.Tbl_AdministracionEMH on d.Fk_Id_AdmoEMH equals e.Pk_Id_AdmoEMH
                                  where s.Fk_IdEmpresa == IdEmpresa && e.Fk_Id_Empresa == IdEmpresa
                                  select s).ToList<Inspecciones>();

                    if (inspec != null)
                    {
                        ListaInspeccion1 = inspec;
                    }
                }
            }
            #endregion
            #region Fecha&IdEHM
            if (CondicionFecha && IdEHM != 0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var inspec = (from s in db.Tbl_Inspecciones
                                  join d in db.Tbl_AdministracionEMHInspecciones on s.Pk_Id_Inspecciones equals d.Fk_Id_Inspecciones
                                  join e in db.Tbl_AdministracionEMH on d.Fk_Id_AdmoEMH equals e.Pk_Id_AdmoEMH
                                  where s.Fk_IdEmpresa == IdEmpresa && e.Fk_Id_Empresa == IdEmpresa && d.Fk_Id_AdmoEMH== IdEHM
                                  select s).ToList<Inspecciones>();

                    if (inspec != null)
                    {
                        ListaInspeccion = inspec;
                        ListaInspeccion1 = ListaInspeccion.Where(s => s.PlaneacionInspeccion.Fecha >= FechaA_conv && s.PlaneacionInspeccion.Fecha <= FechaD_conv).ToList();
                    }
                }
            }

            #endregion
            ListaInspeccion2 = ListaInspeccion1.Distinct().ToList();
            foreach (var item in ListaInspeccion2)
            {
                EDEHMInspecciones EDEHMInspecciones = new EDEHMInspecciones();
                List<AdmoEMH> ListaMaquinas = new List<AdmoEMH>();
                EDEHMInspecciones.EDDescribeinspeccion = item.Descripcion_Tipo_Inspeccion;
                EDEHMInspecciones.PK_Id_inspeccion = item.Pk_Id_Inspecciones;

                using (SG_SSTContext db = new SG_SSTContext())
                {
                    int pkId = item.Pk_Id_Inspecciones;
                    var inspec1 = (from s in db.Tbl_Planeacion_Inspeccion
                                   where s.Pk_Id_PlaneacionInspeccion==item.Fk_Id_PlaneacionInspeccion
                                   select s).FirstOrDefault<PlaneacionInspeccion>();
                    if (inspec1 != null)
                    {
                        EDEHMInspecciones.Fecha = inspec1.Fecha;
                        EDEHMInspecciones.IdConsecutivo = inspec1.ConsecutivoPlan;
                    }


                }

                using (SG_SSTContext db = new SG_SSTContext())
                {
                    int pkId = item.Pk_Id_Inspecciones;
                    var inspec1 = (from s in db.Tbl_AdministracionEMH
                                   join d in db.Tbl_AdministracionEMHInspecciones on s.Pk_Id_AdmoEMH equals d.Fk_Id_AdmoEMH
                                   where d.Fk_Id_Inspecciones == pkId
                                   select s).ToList<AdmoEMH>();
                    if (inspec1 != null)
                    {
                        ListaMaquinas = inspec1;
                    }
                    foreach (var item1 in ListaMaquinas)
                    {
                        if (EDEHMInspecciones.EDNombreInspeccion!=null)
                        {
                            if (EDEHMInspecciones.EDNombreInspeccion != string.Empty)
                            {
                                EDEHMInspecciones.EDNombreInspeccion = EDEHMInspecciones.EDNombreInspeccion + " - " + item1.NombreElemento;
                            }
                            else
                            {
                                EDEHMInspecciones.EDNombreInspeccion = item1.NombreElemento;
                            }
                        }
                        else
                        {
                            EDEHMInspecciones.EDNombreInspeccion = item1.NombreElemento;
                        }
                    }
                }

                ListaEDInspeccion.Add(EDEHMInspecciones);
            }
            ListaEDInspeccion = ListaEDInspeccion.Distinct().ToList();
            return ListaEDInspeccion;
        }

        public EDEHMInspecciones ConsultaEHMInspeccion(int IdEHMIns, int IdEmpresa)
        {
            EDEHMInspecciones EDEHMInspecciones = new EDEHMInspecciones();
            Inspecciones Inspeccion = new Inspecciones();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var inspec = (from s in db.Tbl_Inspecciones
                               where s.Pk_Id_Inspecciones == IdEHMIns
                               select s).FirstOrDefault<Inspecciones>();
                if (inspec != null)
                {
                    Inspeccion = inspec;

                }
            }

            if (Inspeccion.Pk_Id_Inspecciones!=0)
            {

            
            List<AdmoEMH> ListaMaquinas = new List<AdmoEMH>();
            EDEHMInspecciones.EDDescribeinspeccion = Inspeccion.Descripcion_Tipo_Inspeccion;
            EDEHMInspecciones.PK_Id_inspeccion = Inspeccion.Pk_Id_Inspecciones;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                int pkId = Inspeccion.Pk_Id_Inspecciones;
                var inspec1 = (from s in db.Tbl_Planeacion_Inspeccion
                               where s.Pk_Id_PlaneacionInspeccion == Inspeccion.Fk_Id_PlaneacionInspeccion
                               select s).FirstOrDefault<PlaneacionInspeccion>();
                if (inspec1 != null)
                {
                    EDEHMInspecciones.Fecha = inspec1.Fecha;
                    EDEHMInspecciones.IdConsecutivo = inspec1.ConsecutivoPlan;
                }
            }

            using (SG_SSTContext db = new SG_SSTContext())
            {

                var inspec1 = (from s in db.Tbl_AdministracionEMH
                               join d in db.Tbl_AdministracionEMHInspecciones on s.Pk_Id_AdmoEMH equals d.Fk_Id_AdmoEMH
                               where d.Fk_Id_Inspecciones == Inspeccion.Pk_Id_Inspecciones
                               select s).ToList<AdmoEMH>();
                if (inspec1 != null)
                {
                    ListaMaquinas = inspec1;
                }
                foreach (var item1 in ListaMaquinas)
                {
                    if (EDEHMInspecciones.EDNombreInspeccion != null)
                    {
                        if (EDEHMInspecciones.EDNombreInspeccion != string.Empty)
                        {
                            EDEHMInspecciones.EDNombreInspeccion = EDEHMInspecciones.EDNombreInspeccion + " - " + item1.NombreElemento;
                        }
                        else
                        {
                            EDEHMInspecciones.EDNombreInspeccion = item1.NombreElemento;
                        }
                    }
                    else
                    {
                        EDEHMInspecciones.EDNombreInspeccion = item1.NombreElemento;
                    }
                }
            }
            }

            return EDEHMInspecciones;
        }

    }
}
