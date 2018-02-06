using System;
using System.Collections.Generic;
using System.Linq;
using SG_SST.Models.Empresas;
using SG_SST.Models.Empleado;
using SG_SST.Interfaces.Empresas;
using System.Text.RegularExpressions;
using System.Globalization;
using SG_SST.Models;
using SG_SST.Audotoria;
using System.Data.SqlClient;
using System.Data;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Interfaces.Usuarios;
using SG_SST.Utilidades;


namespace SG_SST.Repositorio.Empresas
{


    public class RelacionesLaboralesManager : IRelacionesLaborales
    {

        IRelacionesLaborales gb;
        static List<EDTipos> tipoTerceros;
        static List<EDTipos> tipoDocumentos;
        static List<EDTipos> tipoOcupacion;
        static List<RelacionesLaboralesTercero> relacionesLaboralesTercero;

        public void cargarTablas()
        {
            TraeTipoTerceros();
            TraeTipoDocumentos();
            TraeTipoOcupacion();
        }
        static void TraeTipoTerceros()
        {
            tipoTerceros = new List<EDTipos>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    //var tipoTer =  from  a in context.Tbl_TipoTercero select new { Pk_Id_TipoTercero = a.Pk_Id_TipoTercero };
                    tipoTerceros = context.Tbl_TipoTercero.Select(
                        a => new EDTipos
                        {
                            Id_Tipo = a.Pk_Id_TipoTercero,
                            Descripcion = a.Descripcion_Tipo_Tercero.Trim().ToUpper()
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("error trayendo tipos de tercero: {0}, {1}. error: {2}", DateTime.Now, ex.StackTrace), ex);
            }

        }

        static void TraeTipoDocumentos()
        {
            tipoDocumentos = new List<EDTipos>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    tipoDocumentos = context.Tbl_TipoDocumentos.Select(a => new EDTipos { Id_Tipo = a.PK_IDTipo_Documento, Descripcion = a.Sigla.Trim().ToUpper() }).ToList();
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("error buscando tipos de documentos: {0}, {1}. error: {2}", DateTime.Now, ex.StackTrace), ex);
            }

        }

        static void TraeTipoOcupacion()
        {
            tipoOcupacion= new List<EDTipos>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    tipoOcupacion = context.Tbl_Ocupacion.Select(a => new EDTipos { Id_Tipo = a.PK_Ocupacion, Descripcion = a.Descripcion_Ocupacion.Trim().ToUpper() }).ToList();
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("error buscando tipos de ocupacion: {0}, {1}. error: {2}", DateTime.Now, ex.StackTrace), ex);
            }

        }
        public List<EDTipos> ObtenerTiposInconsistencias()
        {
            List<EDTipos> lstTiposInconsistencias = new List<EDTipos>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    lstTiposInconsistencias = context.Tbl_TipoInconsistenciaLaboral.Select(
                        a => new EDTipos
                        {
                            Id_Tipo = a.PKTipoInconsistencia,
                            Descripcion = a.DescripcionTipInc.Trim().ToUpper()
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo ObtenerTiposCotizantes: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return lstTiposInconsistencias;
        }


        public List<EDTiposS> DevuelveRazonesSocialesdeTerceros(string NitEmpresaLogueada)
        {


            List<EDTiposS> RazSocTer = null;
            try
            {
                /*
  SELECT distinct et.* from tbl_empresa em, Tbl_EmpresaTercero et, Tbl_EmpleadoTercero empt
  where empt.FK_Empresa = em.Pk_Id_Empresa
  and et.PK_Nit_Empresa = empt.FK_EmpresaTercero
  */
                using (var context = new SG_SSTContext())
                {
                    RazSocTer = (from em in context.Tbl_Empresa
                                 from et in context.Tbl_EmpresaTercero
                                 from empt in context.Tbl_EmpleadoTercero
                                 where empt.FK_Empresa == em.Pk_Id_Empresa
                                 && et.PK_Nit_Empresa == empt.FK_EmpresaTercero
                                 && em.Nit_Empresa == NitEmpresaLogueada
                                 select new EDTiposS
                                 {
                                     Descripcion = et.Razon_Social,
                                     Id_Tipo = et.PK_Nit_Empresa.ToString()
                                 }).Distinct().ToList();
                }
                return RazSocTer;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("error trayendo DevuelveRazonesSocialesdeTerceros: {0}, {1}. error: {2}", DateTime.Now, ex.StackTrace), ex);

            }
            return RazSocTer;
        }


        public bool GrabarArchivoRelacionesLaborales(System.Data.DataTable drEmpresas, System.Data.DataTable drEmpleados)
        {
            bool rta = false;

            try
            {
                var result = false;
                bool empresasInsertadas = false;
                bool empleadosInsertados = false;
                SqlTransaction transaction = null;

                using (var context = new SG_SSTContext())
                {
                    SqlConnection con = context.Database.Connection as SqlConnection;
                    con.Open();
                    if (drEmpresas.Rows.Count > 0)
                    {

                        //Se inicia una transacción
                        using (transaction = con.BeginTransaction())
                        {
                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(con, SqlBulkCopyOptions.FireTriggers, transaction))
                            {
                                bulkcopy.BatchSize = 1000;
                                bulkcopy.NotifyAfter = 100;
                                bulkcopy.DestinationTableName = "Tbl_EmpresaTercero";
                                bulkcopy.WriteToServer(drEmpresas);
                            }
                            transaction.Commit();
                            empresasInsertadas = true;
                        }

                    }

                    if (drEmpleados.Rows.Count > 0)
                    {
                        //Se inicia una transacción
                        using (transaction = con.BeginTransaction())
                        {
                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(con, SqlBulkCopyOptions.FireTriggers, transaction))
                            {
                                bulkcopy.BatchSize = 1000;
                                bulkcopy.NotifyAfter = 100;
                                bulkcopy.DestinationTableName = "Tbl_EmpleadoTercero";
                                bulkcopy.WriteToServer(drEmpleados);
                            }
                            transaction.Commit();
                            empleadosInsertados = true;
                        }

                    }
                    con.Close();
                }
                rta = true;
            }

            /*
                var ausenciaPadre = context.Tbl_Ausencias.Where(au => au.Pk_Id_Ausencias == prorrogar.IdAusencia).Select(au => au.FK_Id_Ausencias_Padre).FirstOrDefault();
                if (ausenciaPadre > 0)
                    prorrogar.IdAusenciaPadre = ausenciaPadre;
                else
                    prorrogar.IdAusenciaPadre = prorrogar.IdAusencia;

                var datosAusenciaPadre = context.Tbl_Ausencias.Where(au => au.Pk_Id_Ausencias == prorrogar.IdAusencia).Select(au => au).FirstOrDefault();

                var nuevaAusencia = new Ausencia();
                nuevaAusencia.FK_Id_Ausencias_Padre = prorrogar.IdAusenciaPadre;
                nuevaAusencia.Documento_Persona = datosAusenciaPadre.Documento_Persona;
                nuevaAusencia.NitEmpresa = datosAusenciaPadre.NitEmpresa;
                nuevaAusencia.FK_Id_EmpresaUsuaria = datosAusenciaPadre.FK_Id_EmpresaUsuaria;
                nuevaAusencia.FK_Id_Departamento = datosAusenciaPadre.FK_Id_Departamento;
                nuevaAusencia.FK_Id_Municipio = datosAusenciaPadre.FK_Id_Municipio;
                nuevaAusencia.FK_Id_Contingencia = datosAusenciaPadre.FK_Id_Contingencia;
                nuevaAusencia.FK_Id_Diagnostico = datosAusenciaPadre.FK_Id_Diagnostico;
                nuevaAusencia.FK_Id_Sede = datosAusenciaPadre.FK_Id_Sede;
                nuevaAusencia.FechaInicio = prorrogar.FechaInicio;
                nuevaAusencia.Fecha_Fin = prorrogar.FechaFin;
                nuevaAusencia.DiasAusencia = prorrogar.DiasAusencia;
                nuevaAusencia.Costo = prorrogar.Costo;
                nuevaAusencia.Factor_Prestacional = prorrogar.FactorPrestacional;
                nuevaAusencia.FK_Id_Ocupacion = datosAusenciaPadre.FK_Id_Ocupacion;
                nuevaAusencia.Sexo = datosAusenciaPadre.Sexo;
                nuevaAusencia.Tipo_Vinculacion = datosAusenciaPadre.Tipo_Vinculacion;
                nuevaAusencia.Edad = datosAusenciaPadre.Edad;
                nuevaAusencia.Eps = datosAusenciaPadre.Eps;
                nuevaAusencia.FechaModificacion = DateTime.Now;
                nuevaAusencia.FechaRegistro = DateTime.Now;

                context.Tbl_Ausencias.Add(nuevaAusencia);
                context.SaveChanges();
                result = true;

            }
        }*/
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("Error Grabando empresas de terceros y empleados de terceros : {0},  Error: {1}", DateTime.Now, ex.StackTrace), ex);
                rta = false;
            }
            return rta;
        }

        public bool ValidacionCampoCadena(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return false;

            bool rta = false;
            if (valor.Trim().Length == 0)
            {
                rta = false;
            }
            else
                rta = true;

            return rta;
        }

        public bool ValidacionCampoFecha(string valor, out DateTime fecha)
        {
            string[] formats = { "dd/MM/yyyy", "dd/MM/yy" };
            return (DateTime.TryParseExact(valor, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out fecha));
        }

        public bool ValidacionCampoNumerico(string valor, out string numero)
        {
            if (string.IsNullOrEmpty(valor))
            {
                numero = "0";
                return false;
            }
            bool rta = false;
            try
            {
                numero = Decimal.Parse(valor.Trim()).ToString();
                rta = true;
            }
            catch (Exception e)
            {
                rta = false;
                numero = "0";
            }
            return rta;
        }


        public bool ValidacionEmail(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return false;
            bool rta = false;
            if (!Regex.Match(valor, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Success)
            {
                //Email was incorrect
                rta = false;
            }
            else
            {
                rta = true;
            }
            return rta;
        }

        public int ValidacionTipoTercero(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return -1;
            int rta = -1;
            valor = valor.Trim().ToUpper();

            EDTipos elemento = tipoTerceros.FirstOrDefault(s => s.Descripcion.Contains(valor));
            if (elemento != null)
                rta = elemento.Id_Tipo;

            return rta;
        }

        public int ValidacionTipoOcupacion(int valor)
        {
            int rta = -1;

            EDTipos elemento = tipoOcupacion.FirstOrDefault(s => s.Id_Tipo == valor);
            if (elemento != null)
                rta = elemento.Id_Tipo;

            return rta;
        }

        public int ValidacionTipoDocumento(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return -1;

            int rta = -1;
            valor = valor.Trim().ToUpper();

            EDTipos elemento = tipoDocumentos.FirstOrDefault(s => s.Descripcion.Contains(valor));
            if (elemento != null)
                rta = elemento.Id_Tipo;

            return rta;
        }

        public bool ValidaExisteEmpresa(string nit)
        {
            using (var context = new SG_SSTContext())
            {
                var emp = context.Tbl_EmpresaTercero.Where(u => u.PK_Nit_Empresa == nit).Select(u => u).FirstOrDefault();
                if (emp != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ValidaExisteEmpresaEmpleado(string fk_nit, string empleadoDocumento)
        {
            using (var context = new SG_SSTContext())
            {
                if (context.Tbl_EmpleadoTercero.Where(u => u.FK_EmpresaTercero == fk_nit && u.Numero_Documento_Empl == empleadoDocumento).Any())
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public DataTable EmpresaTercero()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Razon_Social", typeof(string));
            table.Columns.Add("PK_Nit_Empresa", typeof(string));

            /*
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            */
            return table;
        }

        public DataTable EmpleadoTercero()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID_Empleado", typeof(int));
            table.Columns.Add("FK_Tipo_Documento_Empl", typeof(int));
            table.Columns.Add("Nombre1", typeof(string));
            table.Columns.Add("Nombre2", typeof(string));
            table.Columns.Add("Apellido1", typeof(string));
            table.Columns.Add("Apellido2", typeof(string));
            table.Columns.Add("FechaNacimiento", typeof(DateTime));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Ocupacion_Empl", typeof(string));
            table.Columns.Add("Cargo_Empl", typeof(string));
            table.Columns.Add("Email_Empl", typeof(string));
            table.Columns.Add("FK_EmpresaTercero", typeof(string));
            table.Columns.Add("FKRelacionLaboralTercero", typeof(int));
            table.Columns.Add("Numero_Documento_Empl", typeof(string));
            table.Columns.Add("PK_Empresa", typeof(int));

            return table;
        }

        public List<EDEmpleadoTercero> ListarTerceroRelLab(string razonSocialnit, string tipoTercero, string DocumentoEmpresa, int pageIndex, int pageSize, out int pageCount)
        {
            List<EDEmpleadoTercero> lstEmpTer = new List<EDEmpleadoTercero>();
            List<EDEmpleadoTercero> lstEmpTerAux = new List<EDEmpleadoTercero>();
            int l_pagecount = 0;
            try
            {
                //List<Resultados> resultAusencias = new List<Resultados>();
                using (var context = new SG_SSTContext())
                {
                    var p_pageCount = new SqlParameter("@pageCount", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    lstEmpTer = context.Database.SqlQuery<EDEmpleadoTercero>("SP_RELACIONESLABTERCERO_LISTAR @razonSocialnit, @tipoTercero , @PageIndex, @PageSize,@PageCount out,@Empresa",
                        new SqlParameter("@razonSocialnit", (razonSocialnit.Trim().Length == 0) ? DBNull.Value : (object)razonSocialnit),
                        new SqlParameter("@tipoTercero", (tipoTercero.Trim().Length == 0) ? DBNull.Value : (object)tipoTercero),
                        new SqlParameter("@pageIndex", pageIndex),
                        new SqlParameter("@pageSize", pageSize),
                        p_pageCount,
                        new SqlParameter("@Empresa", DocumentoEmpresa)).ToList();

                    if (p_pageCount == null)
                        l_pagecount = 0;
                    else
                        l_pagecount = (int)p_pageCount.Value;

                    foreach (var empTer in lstEmpTer)
                    {
                        empTer.PageCount = l_pagecount;
                        lstEmpTerAux.Add(empTer);
                    }
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("Error Grabando empresas de terceros y empleados de terceros : {0},  Error: {1}", DateTime.Now, ex.StackTrace), ex);
                lstEmpTerAux = null;
            }
            pageCount = l_pagecount;
            return lstEmpTerAux;
        }

        public List<EDEmpleadoRelLab> ListarEmpleadoRelLab(string estado, string tipoCotizante, string DocumentoEmpresa, int pageIndex, int pageSize, out int pageCount)
        {
            List<EDEmpleadoRelLab> lstEmpleado = new List<EDEmpleadoRelLab>();
            List<EDEmpleadoRelLab> lstEmpleadoAux = new List<EDEmpleadoRelLab>();
            int l_pagecount = 0;
            try
            {
                //List<Resultados> resultAusencias = new List<Resultados>();
                using (var context = new SG_SSTContext())
                {
                    var p_pageCount = new SqlParameter("@pageCount", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    lstEmpleado = context.Database.SqlQuery<EDEmpleadoRelLab>("SP_EMPLEADO_LISTAR @estado , @tipoCotizante , @PageIndex, @PageSize,@PageCount out,@Empresa",
                        new SqlParameter("@estado", (estado.Trim().Length == 0) ? DBNull.Value : (object)estado),
                        new SqlParameter("@tipoCotizante", (tipoCotizante.Trim().Length == 0) ? DBNull.Value : (object)tipoCotizante),
                        new SqlParameter("@pageIndex", pageIndex),
                        new SqlParameter("@pageSize", pageSize),
                        p_pageCount,
                        new SqlParameter("@Empresa", DocumentoEmpresa)).ToList();

                    if (p_pageCount == null)
                        l_pagecount = 0;
                    else
                        l_pagecount = (int)p_pageCount.Value;

                    foreach (var EDEmp in lstEmpleado)
                    {
                        EDEmp.PageCount = l_pagecount;
                        lstEmpleadoAux.Add(EDEmp);
                    }
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("Error Grabando empresas de terceros y empleados de terceros : {0},  Error: {1}", DateTime.Now, ex.StackTrace), ex);
                lstEmpleadoAux = null;
            }
            pageCount = l_pagecount;
            return lstEmpleadoAux;

        }

        public List<EDTipos> ObtenerEstadosEmpleados()
        {
            List<EDTipos> lstEstadosEmpleados = new List<EDTipos>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    lstEstadosEmpleados = context.Tbl_Estado_Empl.Select(
                        a => new EDTipos
                        {
                            Id_Tipo = a.PK_IDEmpleadoEst,
                            Descripcion = a.EstEmplead.Trim().ToUpper()
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo ObtenerEstadosEmpleados: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return lstEstadosEmpleados;
        }

        public List<EDTipos> ObtenerTiposCotizantes()
        {
            List<EDTipos> lstTiposCotizantes = new List<EDTipos>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    lstTiposCotizantes = context.Tbl_TipoCotizante.Select(
                        a => new EDTipos
                        {
                            Id_Tipo = a.Pk_Id_Cotizante,
                            Descripcion = a.Descripcion.Trim().ToUpper()
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo ObtenerTiposCotizantes: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return lstTiposCotizantes;
        }

        public DataTable TablaEmpleados()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TIPO_DOCUMENTO", typeof(string));
            table.Columns.Add("NUMERO_DOCUMENTO", typeof(string));
            table.Columns.Add("PRIMER_APELLIDO", typeof(string));
            table.Columns.Add("SEGUNDO_APELLIDO", typeof(string));
            table.Columns.Add("PRIMER_NOMBRE", typeof(string));
            table.Columns.Add("SEGUNDO_NOMBRE", typeof(string));
            table.Columns.Add("FECHA_NACIMIENTO", typeof(DateTime));
            table.Columns.Add("ESTADO", typeof(string));
            table.Columns.Add("OCUPACION", typeof(string));
            table.Columns.Add("CARGO", typeof(string));
            table.Columns.Add("EMAIL", typeof(string));

            return table;
        }

        public DataTable TablaTerceroRelLab()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TIPO_TERCERO", typeof(string));
            table.Columns.Add("NIT_EMPRESA", typeof(string));
            table.Columns.Add("RAZON_SOCIAL", typeof(string));
            table.Columns.Add("TIPO_DOCUMENTO", typeof(string));
            table.Columns.Add("NUMERO_DOCUMENTO", typeof(string));
            table.Columns.Add("PRIMER_APELLIDO", typeof(string));
            table.Columns.Add("SEGUNDO_APELLIDO", typeof(string));
            table.Columns.Add("PRIMER_NOMBRE", typeof(string));
            table.Columns.Add("SEGUNDO_NOMBRE", typeof(string));
            table.Columns.Add("FECHA_NACIMIENTO", typeof(DateTime));
            table.Columns.Add("OCUPACION", typeof(string));
            table.Columns.Add("CARGO", typeof(string));
            table.Columns.Add("EMAIL", typeof(string));

            return table;
        }

        public DataTable DescargaArchivoExcelEmpleado(string nit, string estado, string tipoCotizante)
        {
            DataTable dt = TablaEmpleados();

            List<EDEmpleadoRelLab> lstEDEmpleadoRelLab = null;
            using (var context = new SG_SSTContext())
            {
                lstEDEmpleadoRelLab = (from erl in context.tblEmpleado
                                       join emp in context.Tbl_Empresa on erl.FK_Empresa equals emp.Pk_Id_Empresa
                                       join tc in context.Tbl_TipoCotizante on erl.FK_ID_Tipo_Cotizante equals tc.Pk_Id_Cotizante
                                       join ee in context.Tbl_Estado_Empl on erl.FK_ID_Estado equals ee.PK_IDEmpleadoEst
                                       join td in context.Tbl_TipoDocumentos on erl.FK_Tipo_Documento_Empl equals td.PK_IDTipo_Documento
                                       where emp.Nit_Empresa.Trim().Equals(nit.Trim())
                                       select new EDEmpleadoRelLab()
                                       {
                                           TipoDocumento = td.Descripcion,
                                           Apellido1 = erl.Apellido1,
                                           Apellido2 = erl.Apellido2,
                                           Nombre1 = erl.Nombre1,
                                           Nombre2 = erl.Nombre2,
                                           NumeroDocumento = erl.PK_Numero_Documento_Empl.ToString(),
                                           Cargo = "CARGO",
                                           Email = "EMAIL",
                                           Ocupacion = "OCUPACION",
                                           FechaNacimiento = DateTime.Now,
                                           Estado = ee.EstEmplead.ToUpper()

                                       }).ToList();
            }

            foreach (EDEmpleadoRelLab erl in lstEDEmpleadoRelLab)
            {
                dt.Rows.Add(erl.TipoDocumento, erl.NumeroDocumento, erl.Apellido1, erl.Apellido2, erl.Nombre1, erl.Nombre2, erl.FechaNacimiento, erl.Estado, erl.Ocupacion, erl.Cargo, erl.Email);
            }

            return dt;
        }


        public DataTable DescargaArchivoExcelTerceroRelLab(string nit, string razonSocialNit, string tipoTercero)
        {
            DataTable dt = TablaTerceroRelLab();

            List<EDEmpleadoTercero> lstEmpleadoTercero = null;
            using (var context = new SG_SSTContext())
            {
                //var razonSocialNit_a = (razonSocialNit.Trim().Length == 0) ? null : razonSocialNit;
                lstEmpleadoTercero = (from erl in context.Tbl_EmpleadoTercero
                                      join emp in context.Tbl_EmpresaTercero on erl.FK_EmpresaTercero equals emp.PK_Nit_Empresa
                                      join tt in context.Tbl_TipoTercero on erl.FKRelacionLaboralTercero equals tt.Pk_Id_TipoTercero
                                      join te in context.Tbl_Empresa on erl.FK_Empresa equals te.Pk_Id_Empresa
                                      join td in context.Tbl_TipoDocumentos on erl.FK_Tipo_Documento_Empl equals td.PK_IDTipo_Documento
                                      where te.Nit_Empresa.Trim().Equals(nit.Trim())
                                      && (emp.PK_Nit_Empresa == razonSocialNit) 
                                      && tt.Descripcion_Tipo_Tercero.Trim().Equals(tipoTercero.ToUpper().Trim())
                                      select new EDEmpleadoTercero()
                                      {
                                          Apellido1 = erl.Apellido1,
                                          Apellido2 = erl.Apellido2,
                                          Nombre1 = erl.Nombre1,
                                          Nombre2 = erl.Nombre2,
                                          Numero_Documento_Empl = erl.Numero_Documento_Empl,
                                          Cargo_Empl = erl.Cargo_Empl,
                                          Email = erl.Email,
                                          Ocupacion_Empl = erl.Ocupacion_Empl,
                                          FechaNacimiento = erl.FechaNacimiento,
                                          RelacionesLaboralesTercero = tt.Descripcion_Tipo_Tercero,
                                          PK_Nit_Empresa = erl.FK_EmpresaTercero,
                                          RazonSocial = emp.Razon_Social,
                                          TipoDocumento = td.Descripcion,
                                      }).ToList();
            }

            foreach (EDEmpleadoTercero etrl in lstEmpleadoTercero)
            {
                dt.Rows.Add(etrl.RelacionesLaboralesTercero, etrl.PK_Nit_Empresa, etrl.RazonSocial, etrl.TipoDocumento, etrl.Numero_Documento_Empl, etrl.Apellido1, etrl.Apellido2, etrl.Nombre1, etrl.Nombre2, etrl.FechaNacimiento, etrl.Ocupacion_Empl, etrl.Cargo_Empl, etrl.Email);
            }

            return dt;
        }

        public List<EDTipos> ObtenerTiposTerceros()
        {
            List<EDTipos> lstTiposTerceros = new List<EDTipos>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    lstTiposTerceros = context.Tbl_TipoTercero.Select(
                        a => new EDTipos
                        {
                            Id_Tipo = a.Pk_Id_TipoTercero,
                            Descripcion = a.Descripcion_Tipo_Tercero.Trim().ToUpper()
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo ObtenerTiposTerceros: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return lstTiposTerceros;
        }

        public EDNotificarInconsistencia GrabarNotificacionInconsistenciaLaborales(EDNotificarInconsistencia notIncon)
        {
            bool rta = false;
            Inconsistecialaboral IncoLab = null;
            try
            {
                using (var context = new SG_SSTContext())
                {
                    using (var Transaction = context.Database.BeginTransaction())
                    {


                        IncoLab = new Inconsistecialaboral();

                        IncoLab.DescripcionInconsistencia = notIncon.Observacion;
                        IncoLab.FKTipoInconsistencia = notIncon.IDTipoInconsistencia;

                        context.Tbl_InconsistenciasLaborales.Add(IncoLab);
                        context.SaveChanges();
                        Transaction.Commit();
                        IncoLab.PKInconsistencia = IncoLab.PKInconsistencia;
                        notIncon.Rta = true;
                        notIncon.Id = IncoLab.PKInconsistencia;

                    }
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                notIncon.Rta = false;
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("Error Grabando Notificacion Inconsistencia Laboral: {0},  Error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return notIncon;
        }


        public List<EDTiposS> DevuelveCorreoGerente(string razonSocialnit)
        {
            List<EDTiposS> lstcorreoGerente;
            try
            {
                //List<Resultados> resultAusencias = new List<Resultados>();
                using (var context = new SG_SSTContext())
                {
                    var p_pageCount = new SqlParameter("@pageCount", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    lstcorreoGerente = context.Database.SqlQuery<EDTiposS>("SP_TRAE_EMAIL_GERENTE @razonSocialnit ",
                        new SqlParameter("@razonSocialnit", (razonSocialnit.Trim().Length == 0) ? DBNull.Value : (object)razonSocialnit)
                            ).ToList();

                    if (lstcorreoGerente == null && lstcorreoGerente.Count == 0)
                    {
                        lstcorreoGerente = new List<EDTiposS>();
                    }
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(RelacionesLaboralesManager), string.Format("Error buscando el correo del gerente : {0},  Error: {1}", DateTime.Now, ex.StackTrace), ex);
                lstcorreoGerente = null;
            }
            return lstcorreoGerente;
        }


        public EntidadesDominio.Usuario.EDParametroSistema ObtenerParametrosSistema(string NombrePlantilla)
        {
            try
            {
                EntidadesDominio.Usuario.EDParametroSistema resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_PlantillasCorreosSistema.Where(pc => pc.NombrePlantilla == NombrePlantilla).Select(pc => new EDParametroSistema()
                    {
                        IdParametro = pc.IdPlantilla,
                        NombreParametro = pc.NombrePlantilla,
                        Valor = pc.Plantilla
                    }).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return new EDParametroSistema { };//Se debe configurar para que se registe el Log
            }
        }

    }
}