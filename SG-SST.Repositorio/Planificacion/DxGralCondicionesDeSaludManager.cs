
namespace SG_SST.Repositorio.Planificacion
{
    using SG_SST.Audotoria;
    using SG_SST.EntidadesDominio.Planificacion;
    using SG_SST.Interfaces.Planificacion;
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Configuration;

    public class DxGralCondicionesDeSaludManager : IDxGralCondicionesDeSalud
    {
        private int pkTipoPeligroOtro = Int32.Parse(ConfigurationManager.AppSettings["pktipoPeligro"]);
        public bool GuardarDocDXSalud(EDDocDxSalud documento)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        DocDxCondicionesDeSalud doc = new DocDxCondicionesDeSalud
                        {
                            Nombre_Diagnostico = documento.Nombre_Diagnostico,
                            Nombre_Documento = documento.Nombre_Documento,
                            FK_Sede = documento.idSede
                        };
                        context.Tbl_Doc_Dx_Condiciones_De_Salud.Add(doc);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(DxGralCondicionesDeSaludManager), string.Format("Error al guardar el documento de dx en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<EDDocDxSalud> ObtenerDocsDXSalud(int idEmpresa)
        {
            List<EDDocDxSalud> Doc_DX = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Doc_DX = (from dx in context.Tbl_Doc_Dx_Condiciones_De_Salud
                          join sd in context.Tbl_Sede on dx.FK_Sede equals sd.Pk_Id_Sede
                          where sd.Fk_Id_Empresa == idEmpresa
                          select new EDDocDxSalud
                          {
                              idEDDocDxSalud = dx.Pk_DocDxCondicionesDeSalud,
                              Nombre_Diagnostico = dx.Nombre_Diagnostico,
                              Nombre_Documento = dx.Nombre_Documento
                          }).ToList();
            }
            return Doc_DX;
        }


        public bool EliminarDocDxSalud(int idDocDx)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        DocDxCondicionesDeSalud doc = context.Tbl_Doc_Dx_Condiciones_De_Salud.Find(idDocDx);
                        context.Tbl_Doc_Dx_Condiciones_De_Salud.Remove(doc);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(DxGralCondicionesDeSaludManager), string.Format("Error al eliminar el documento de dx en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public EDDocDxSalud ObtenerDocDXSalud(int idDocDx)
        {
            EDDocDxSalud Doc_DX = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                DocDxCondicionesDeSalud doc = context.Tbl_Doc_Dx_Condiciones_De_Salud.Find(idDocDx);
                Doc_DX = new EDDocDxSalud
                          {
                              idEDDocDxSalud = doc.Pk_DocDxCondicionesDeSalud,
                              Nombre_Diagnostico = doc.Nombre_Diagnostico,
                              Nombre_Documento = doc.Nombre_Documento
                          };
            }
            return Doc_DX;
        }


        public EDDxSalud GuardarDxSalud(EDDxSalud Diagnostico)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        DxCondicionesDeSalud dxGeneral = new DxCondicionesDeSalud
                        {
                            Fecha_Dx = Diagnostico.FechaCreacionDiagnostico,
                            Lugar = Diagnostico.ZonaLugar,
                            Trabajadores_Lugar = Diagnostico.NumeroTrabajadoresLugar,
                            Fecha_Inicial_Dx = Diagnostico.Fecha_Inicial_Dx,
                            Fecha_Final_Dx = Diagnostico.Fecha_Final_Dx,
                            Responsable_informacion = Diagnostico.Responsable_informacion,
                            Profesion_Responsable = Diagnostico.Profesion_Responsable,
                            Tarjeta_Profesional = Diagnostico.Tarjeta_Profesional,
                            FK_Sede = Diagnostico.Pk_Id_Sede,
                            FK_Proceso = Diagnostico.Procesos,
                            vigencia = Diagnostico.vigencia,
                            SintomatologiaDx = Diagnostico.EDSintomatologiaDx.Select(sintomatologia => new SintomatologiaDx
                            {
                                Sintomatologia = sintomatologia.Sintomatologia,
                                Trabajadores_Sintomatologia = sintomatologia.Trabajadores_Sintomatologia,
                            }).ToList(),
                            PruebasClinicasDx = Diagnostico.EDPruebasClinicasDx.Select(pruebas => new PruebasClinicasDx
                            {
                                Prueba_Clinica = pruebas.Prueba_Clinica,
                                Trabajadores_Con_Prueba = pruebas.Trabajadores_Con_Prueba
                            }).ToList(),
                            PruebasPClinicasDx = Diagnostico.EDPruebasPClinicasDx.Select(pruebas => new PruebasPClinicasDx
                            {
                                Prueba_P_Clinica = pruebas.Prueba_P_Clinica,
                                Trabajadores_Con_Prueba_P = pruebas.Trabajadores_Con_Prueba_P
                            }).ToList(),
                            DiagnosticoCie10Dx = Diagnostico.EDDiagnosticoCie10Dx.Select(dxcie10 => new DiagnosticoCie10Dx
                            {
                                Trabajadores_Con_Diagnostico = dxcie10.NumeroTrabajadoresConDiagnostico,
                                FK_Diagnostico = dxcie10.IdDiagnostico
                            }).ToList(),
                            ClasificacionPeligroDx = Diagnostico.EDClasificacionPeligroDx.Select(clasDX => new ClasificacionPeligroDx
                            {
                                FK_Clasificacion_De_Peligro = clasDX.idClasifiacionPeligro
                            }).ToList(),

                        };

                        context.Tbl_Dx_Condiciones_De_Salud.Add(dxGeneral);
                        context.SaveChanges();
                        transaction.Commit();
                        Diagnostico.IdDxCondicionesDeSalud = dxGeneral.Pk_DxCondicionesDeSalud;
                        return Diagnostico;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(DxGralCondicionesDeSaludManager), string.Format("Error al guardar  el  dx en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return Diagnostico;
                    }
                }
            }
        }

        public List<EDDxSalud> ObtenerDiagnosticosPorsedeAnio(int idEmpresa)
        {

            List<EDDxSalud> Diagnosticos = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Diagnosticos = (from dx in context.Tbl_Dx_Condiciones_De_Salud
                                join sd in context.Tbl_Sede on dx.FK_Sede equals sd.Pk_Id_Sede
                                where sd.Fk_Id_Empresa == idEmpresa
                                select new EDDxSalud
                          {
                              IdDxCondicionesDeSalud = dx.Pk_DxCondicionesDeSalud,
                              FechaCreacionDiagnostico = dx.Fecha_Dx,
                              NombreSede = sd.Nombre_Sede,
                              Pk_Id_Sede = sd.Pk_Id_Sede,
                              Fecha_Inicial_Dx = dx.Fecha_Inicial_Dx,
                              Fecha_Final_Dx = dx.Fecha_Final_Dx,
                              nombreProceso = dx.Procesos.Descripcion_Proceso,
                              ZonaLugar = dx.Lugar,
                              vigencia = dx.vigencia
                          }).ToList();
            }

            return Diagnosticos;
        }

        public List<EDDxSalud> BuscarDiagnosticosPorsedeAnio(int idEmpresa, int strZonaLugar)
        {


            List<EDDxSalud> Diagnosticos = new List<EDDxSalud>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                if (strZonaLugar == -1)
                {
                    Diagnosticos = (from dx in context.Tbl_Dx_Condiciones_De_Salud
                                    join sd in context.Tbl_Sede on dx.FK_Sede equals sd.Pk_Id_Sede
                                    where sd.Fk_Id_Empresa == idEmpresa
                                    select new EDDxSalud
                                    {
                                        IdDxCondicionesDeSalud = dx.Pk_DxCondicionesDeSalud,
                                        FechaCreacionDiagnostico = dx.Fecha_Dx,
                                        NombreSede = sd.Nombre_Sede,
                                        Pk_Id_Sede = sd.Pk_Id_Sede,
                                        Fecha_Inicial_Dx = dx.Fecha_Inicial_Dx,
                                        Fecha_Final_Dx = dx.Fecha_Final_Dx,
                                        nombreProceso = dx.Procesos.Descripcion_Proceso,
                                        ZonaLugar = dx.Lugar,
                                        vigencia = dx.vigencia
                                    }).ToList();
                }
                else
                {
                    Diagnosticos = (from dx in context.Tbl_Dx_Condiciones_De_Salud
                                    join sd in context.Tbl_Sede on dx.FK_Sede equals sd.Pk_Id_Sede
                                    where sd.Fk_Id_Empresa == idEmpresa && sd.Pk_Id_Sede == strZonaLugar
                                    select new EDDxSalud
                                    {
                                        IdDxCondicionesDeSalud = dx.Pk_DxCondicionesDeSalud,
                                        FechaCreacionDiagnostico = dx.Fecha_Dx,
                                        NombreSede = sd.Nombre_Sede,
                                        Pk_Id_Sede = sd.Pk_Id_Sede,
                                        Fecha_Inicial_Dx = dx.Fecha_Inicial_Dx,
                                        Fecha_Final_Dx = dx.Fecha_Final_Dx,
                                        nombreProceso = dx.Procesos.Descripcion_Proceso,
                                        ZonaLugar = dx.Lugar,
                                        vigencia = dx.vigencia
                                    }).ToList();
                }
            }

            return Diagnosticos;
        }

        public List<EDDxSalud> ObtenerHistoricoDxDeSedePorAnio(int idDxSalud)
        {
            List<EDDxSalud> Diagnosticos = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                Diagnosticos = (from dx in context.Tbl_Dx_Condiciones_De_Salud
                                join sd in context.Tbl_Sede on dx.FK_Sede equals sd.Pk_Id_Sede
                                join sm in context.Tbl_SedeMunicipio on sd.Pk_Id_Sede equals sm.Fk_id_Sede
                                where dx.Pk_DxCondicionesDeSalud == idDxSalud
                                select new EDDxSalud
                          {

                              IdDxCondicionesDeSalud = dx.Pk_DxCondicionesDeSalud,
                              nitEmpresa = dx.Sede.Empresa.Nit_Empresa,
                              nombreEmpresa= dx.Sede.Empresa.Razon_Social,
                              FechaCreacionDiagnostico = dx.Fecha_Dx,
                              Fecha_Inicial_Dx = dx.Fecha_Inicial_Dx,
                              Fecha_Final_Dx = dx.Fecha_Final_Dx,
                              NombreSede = sd.Nombre_Sede,
                              NombreMunicipio = sm.Municipio.Nombre_Municipio,
                              NombreDepartamento = sm.Municipio.Departamento.Nombre_Departamento,
                              ZonaLugar = dx.Lugar,
                              NumeroTrabajadoresLugar = dx.Trabajadores_Lugar,
                              vigencia = dx.vigencia,
                              Responsable_informacion = dx.Responsable_informacion,
                              Profesion_Responsable = dx.Profesion_Responsable,
                              Tarjeta_Profesional = dx.Tarjeta_Profesional,
                              nombreProceso = dx.Procesos.Descripcion_Proceso,
                              EDSintomatologiaDx = dx.SintomatologiaDx.Select(s => new EDSintomatologiaDx
                              {
                                  Sintomatologia = s.Sintomatologia,
                                  Trabajadores_Sintomatologia = s.Trabajadores_Sintomatologia,
                                  porcentajeSintomatologia = Math.Round(((double)s.Trabajadores_Sintomatologia / (double)dx.Trabajadores_Lugar * 100), 2)

                              }).ToList(),
                              EDPruebasClinicasDx = dx.PruebasClinicasDx.Select(pruebas => new EDPruebasClinicasDx
                              {
                                  Prueba_Clinica = pruebas.Prueba_Clinica,
                                  Trabajadores_Con_Prueba = pruebas.Trabajadores_Con_Prueba,
                                  porcentajePrueba = Math.Round(((double)pruebas.Trabajadores_Con_Prueba / (double)dx.Trabajadores_Lugar * 100), 2)
                              }).ToList(),
                              EDPruebasPClinicasDx = dx.PruebasPClinicasDx.Select(preubasp => new EDPruebasPClinicasDx
                              {
                                  Prueba_P_Clinica = preubasp.Prueba_P_Clinica,
                                  Trabajadores_Con_Prueba_P = preubasp.Trabajadores_Con_Prueba_P,
                                  porcentajePruebaP = Math.Round(((double)preubasp.Trabajadores_Con_Prueba_P / (double)dx.Trabajadores_Lugar * 100), 2)
                              }).ToList(),
                              EDDiagnosticoCie10Dx = dx.DiagnosticoCie10Dx.Select(cie10 => new EDDiagnosticoCie10Dx
                              {
                                  NombreDiagnosticoCIE10 = cie10.Diagnostico.Descripcion,
                                  NumeroTrabajadoresConDiagnostico = cie10.Trabajadores_Con_Diagnostico,
                                  porcentajeDiagnostico = Math.Round(((double)cie10.Trabajadores_Con_Diagnostico / (double)dx.Trabajadores_Lugar * 100), 2)
                              }).ToList(),
                              EDClasificacionPeligroDx = dx.ClasificacionPeligroDx.Select(edclasDX => new EDClasificacionPeligroDx
                              {
                                  nombreTipoPeligro = edclasDX.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                                  nombreDescripcion = edclasDX.ClasificacionDePeligro.Descripcion_Clase_De_Peligro
                              }).ToList()


                          }).ToList();
            }
            return Diagnosticos;
        }

        public List<EDDxSalud> ObtenerReporte(int idEmpresa)
        {
            List<EDDxSalud> Diagnosticos = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Diagnosticos = (from dx in context.Tbl_Dx_Condiciones_De_Salud
                                join sd in context.Tbl_Sede on dx.FK_Sede equals sd.Pk_Id_Sede
                                join sm in context.Tbl_SedeMunicipio on sd.Pk_Id_Sede equals sm.Fk_id_Sede
                                where dx.Fecha_Dx <= DateTime.Now && sd.Fk_Id_Empresa == idEmpresa
                                select new EDDxSalud
                                {

                                    IdDxCondicionesDeSalud = dx.Pk_DxCondicionesDeSalud,
                                    nitEmpresa = dx.Sede.Empresa.Nit_Empresa,
                                    nombreEmpresa = dx.Sede.Empresa.Razon_Social,
                                    FechaCreacionDiagnostico = dx.Fecha_Dx,
                                    Fecha_Inicial_Dx = dx.Fecha_Inicial_Dx,
                                    Fecha_Final_Dx = dx.Fecha_Final_Dx,
                                    vigencia = dx.vigencia,
                                    NombreSede = sd.Nombre_Sede,
                                    NombreMunicipio = sm.Municipio.Nombre_Municipio,
                                    NombreDepartamento = sm.Municipio.Departamento.Nombre_Departamento,
                                    ZonaLugar = dx.Lugar,
                                    NumeroTrabajadoresLugar = dx.Trabajadores_Lugar,
                                    Responsable_informacion = dx.Responsable_informacion,
                                    Profesion_Responsable = dx.Profesion_Responsable,
                                    Tarjeta_Profesional = dx.Tarjeta_Profesional,
                                    nombreProceso=dx.Procesos.Descripcion_Proceso,
                                    EDSintomatologiaDx = dx.SintomatologiaDx.Select(s => new EDSintomatologiaDx
                                    {
                                        Sintomatologia = s.Sintomatologia,
                                        Trabajadores_Sintomatologia = s.Trabajadores_Sintomatologia,
                                        porcentajeSintomatologia = Math.Round(((double)s.Trabajadores_Sintomatologia / (double)dx.Trabajadores_Lugar * 100), 2)
                                    }).ToList(),
                                    EDPruebasClinicasDx = dx.PruebasClinicasDx.Select(pruebas => new EDPruebasClinicasDx
                                    {
                                        Prueba_Clinica = pruebas.Prueba_Clinica,
                                        Trabajadores_Con_Prueba = pruebas.Trabajadores_Con_Prueba,
                                        porcentajePrueba = Math.Round(((double)pruebas.Trabajadores_Con_Prueba / (double)dx.Trabajadores_Lugar * 100), 2)
                                    }).ToList(),
                                    EDPruebasPClinicasDx = dx.PruebasPClinicasDx.Select(preubasp => new EDPruebasPClinicasDx
                                    {
                                        Prueba_P_Clinica = preubasp.Prueba_P_Clinica,
                                        Trabajadores_Con_Prueba_P = preubasp.Trabajadores_Con_Prueba_P,
                                        porcentajePruebaP = Math.Round(((double)preubasp.Trabajadores_Con_Prueba_P / (double)dx.Trabajadores_Lugar * 100), 2)
                                    }).ToList(),
                                    EDDiagnosticoCie10Dx = dx.DiagnosticoCie10Dx.Select(cie10 => new EDDiagnosticoCie10Dx
                                    {
                                        NombreDiagnosticoCIE10 = cie10.Diagnostico.Descripcion,
                                        NumeroTrabajadoresConDiagnostico = cie10.Trabajadores_Con_Diagnostico,
                                        porcentajeDiagnostico = Math.Round(((double)cie10.Trabajadores_Con_Diagnostico / (double)dx.Trabajadores_Lugar * 100), 2)
                                    }).ToList(),
                                    EDClasificacionPeligroDx = dx.ClasificacionPeligroDx.Select(edclasDX => new EDClasificacionPeligroDx
                                    {
                                        nombreTipoPeligro = edclasDX.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                                        nombreDescripcion = edclasDX.ClasificacionDePeligro.Descripcion_Clase_De_Peligro
                                    }).ToList()

                                }).ToList();
            }
            return Diagnosticos;
        }

        public bool EliminarDxSalud(int idDx)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        DxCondicionesDeSalud doc = context.Tbl_Dx_Condiciones_De_Salud.Find(idDx);
                        context.Tbl_Dx_Condiciones_De_Salud.Remove(doc);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(DxGralCondicionesDeSaludManager), string.Format("Error al eliminar el documento de dx en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }






        //INSERTAR  CARGUE Masivo

        public bool InsertarCargueMasivoDx(List<EDDxSalud> diagnosticos)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        foreach (EDDxSalud Diagnostico in diagnosticos)
                        {


                            DxCondicionesDeSalud dxGeneral = new DxCondicionesDeSalud
                            {
                                Fecha_Dx = Diagnostico.FechaCreacionDiagnostico,
                                Lugar = Diagnostico.ZonaLugar,
                                Trabajadores_Lugar = Diagnostico.NumeroTrabajadoresLugar,
                                Fecha_Inicial_Dx = Diagnostico.Fecha_Inicial_Dx,
                                Fecha_Final_Dx = Diagnostico.Fecha_Final_Dx,
                                Responsable_informacion = Diagnostico.Responsable_informacion,
                                Profesion_Responsable = Diagnostico.Profesion_Responsable,
                                Tarjeta_Profesional = Diagnostico.Tarjeta_Profesional,
                                FK_Sede = Diagnostico.Pk_Id_Sede,
                                FK_Proceso = Diagnostico.Procesos,
                                vigencia = Diagnostico.vigencia,
                                SintomatologiaDx = Diagnostico.EDSintomatologiaDx.Select(sintomatologia => new SintomatologiaDx
                                {
                                    Sintomatologia = sintomatologia.Sintomatologia,
                                    Trabajadores_Sintomatologia = sintomatologia.Trabajadores_Sintomatologia,
                                }).ToList(),
                                PruebasClinicasDx = Diagnostico.EDPruebasClinicasDx.Select(pruebas => new PruebasClinicasDx
                                {
                                    Prueba_Clinica = pruebas.Prueba_Clinica,
                                    Trabajadores_Con_Prueba = pruebas.Trabajadores_Con_Prueba
                                }).ToList(),
                                PruebasPClinicasDx = Diagnostico.EDPruebasPClinicasDx.Select(pruebas => new PruebasPClinicasDx
                                {
                                    Prueba_P_Clinica = pruebas.Prueba_P_Clinica,
                                    Trabajadores_Con_Prueba_P = pruebas.Trabajadores_Con_Prueba_P
                                }).ToList(),
                                DiagnosticoCie10Dx = Diagnostico.EDDiagnosticoCie10Dx.Select(dxcie10 => new DiagnosticoCie10Dx
                                {
                                    Trabajadores_Con_Diagnostico = dxcie10.NumeroTrabajadoresConDiagnostico,
                                    FK_Diagnostico = dxcie10.IdDiagnostico
                                }).ToList(),
                                ClasificacionPeligroDx = Diagnostico.EDClasificacionPeligroDx.Select(clasDX => new ClasificacionPeligroDx
                                {
                                    FK_Clasificacion_De_Peligro = clasDX.idClasifiacionPeligro
                                }).ToList(),

                            };

                            context.Tbl_Dx_Condiciones_De_Salud.Add(dxGeneral);
                            context.SaveChanges();
                            Diagnostico.IdDxCondicionesDeSalud = dxGeneral.Pk_DxCondicionesDeSalud;
                        }
                            transaction.Commit();
                            return true;
                     
                        }
                  
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(DxGralCondicionesDeSaludManager), string.Format("Error al guardar  el  dx en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                   
                        return false;
                    }
                }
            }
        }





    }
}