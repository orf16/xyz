using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using SG_SST.Models.Ausentismo;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class AusenciaManager : IAusencia
    {
        /// <summary>
        /// vALIDA QUE NO SE ESTE RPORTANDO UN AUSENTISMO EN FECHAS YA REPORTADAS
        /// </summary>
        /// <param name="ausencia"></param>
        /// <returns></returns>
        public List<EDAusencia> ValidarCruceAusentismo(EDAusencia ausencia)
        {
            DateTime fecha = ausencia.FechaInicio;
            fecha = fecha.AddDays(-365);
            string documento = string.Empty;
            string nit = string.Empty;
            int idempresusuario;
            List<EDAusencia> AusenciasList = new List<EDAusencia>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    if (ausencia.IdAusencia > 0)
                    {
                        var ausenciaPadre = context.Tbl_Ausencias.Where(au => au.Pk_Id_Ausencias == ausencia.IdAusencia).Select(au => au.FK_Id_Ausencias_Padre).FirstOrDefault();
                        if (ausenciaPadre > 0)
                            ausencia.IdAusenciaPadre = ausenciaPadre;
                        else
                            ausencia.IdAusenciaPadre = ausencia.IdAusencia;

                        var datosAusenciaPadre = context.Tbl_Ausencias.Where(au => au.Pk_Id_Ausencias == ausencia.IdAusencia).Select(au => au).FirstOrDefault();
                        documento = datosAusenciaPadre.Documento_Persona;
                        nit = datosAusenciaPadre.NitEmpresa;
                        idempresusuario = datosAusenciaPadre.FK_Id_EmpresaUsuaria;
                    }
                    else
                    {
                        documento = ausencia.Documento;
                        nit = ausencia.IdEmpresa;
                        //idempresusuario = ausencia.IdEmpresaUsuaria;
                    }


                    AusenciasList = (from a in context.Tbl_Ausencias
                                     where a.FechaInicio >= fecha 
                                     && a.NitEmpresa.Equals(nit)
                                     && a.Documento_Persona.Equals(documento) 
                                     //&& a.FK_Id_EmpresaUsuaria == idempresusuario
                                     select new EDAusencia()
                                     {
                                         FechaInicio = a.FechaInicio,
                                         FechaFin = a.Fecha_Fin,
                                         Documento = a.Documento_Persona
                                     }).ToList();
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(AusenciaManager), string.Format("Erorr en el guardado de ausencias - verificando cruce: {0}, {1}. Error: {2}", DateTime.Now, ausencia.FechaInicio.ToString(), ausencia.FechaFin.ToString(), ex.StackTrace), ex);
                return AusenciasList;
            }

            return AusenciasList;
        }
        /// <summary>
        /// Guarda en base de datos una nueva ausencia asociada
        /// a un afiliado
        /// </summary>
        /// <param name="ausencia"></param>
        /// <returns></returns>
        public bool GuardarAusencia(EDAusencia ausencia)
        {
            try
            {
                var result = false;
                using (var context = new SG_SSTContext())
                {
                    var nuevaAusencia = new Ausencia();
                    nuevaAusencia.NombrePersona = ausencia.NombrePersona;
                    nuevaAusencia.Documento_Persona = ausencia.Documento;
                    nuevaAusencia.NitEmpresa = ausencia.IdEmpresa;
                    nuevaAusencia.FK_Id_EmpresaUsuaria = ausencia.IdEmpresaUsuaria;
                    nuevaAusencia.FK_Id_Departamento = ausencia.idDepartamento;
                    nuevaAusencia.FK_Id_Municipio = ausencia.idMunicipio;
                    nuevaAusencia.FK_Id_Contingencia = ausencia.IdContingencia;
                    nuevaAusencia.FK_Id_Diagnostico = ausencia.IdDiagnostico;
                    nuevaAusencia.FK_Id_Sede = ausencia.IdSede;
                    nuevaAusencia.FK_Id_Proceso = ausencia.IdProceso;
                    nuevaAusencia.FechaInicio = ausencia.FechaInicio;
                    nuevaAusencia.Fecha_Fin = ausencia.FechaFin;
                    nuevaAusencia.DiasAusencia = ausencia.DiasAusencia;
                    nuevaAusencia.Costo = ausencia.Costo;
                    nuevaAusencia.Factor_Prestacional = ausencia.FactorPrestacional;
                    nuevaAusencia.Observaciones = ausencia.Observaciones;
                    nuevaAusencia.FK_Id_Ocupacion = ausencia.IdOcupacion;
                    nuevaAusencia.Sexo = ausencia.Sexo;
                    nuevaAusencia.Tipo_Vinculacion = ausencia.TipoVinculacion;
                    nuevaAusencia.Edad = ausencia.Edad;
                    nuevaAusencia.Eps = ausencia.Eps;
                    nuevaAusencia.FechaModificacion = DateTime.Now;
                    nuevaAusencia.FechaRegistro = DateTime.Now;

                    context.Tbl_Ausencias.Add(nuevaAusencia);
                    context.SaveChanges();
                    result = true;
                    return result;
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(AusenciaManager), string.Format("Guardado la ausencias: {0}, {1}", DateTime.Now, nuevaAusencia.FechaInicio.ToString(), nuevaAusencia.Fecha_Fin.ToString()), new Exception());
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(AusenciaManager), string.Format("Erorr en el guardado de ausencias: {0}, {1}. Error: {2}", DateTime.Now, ausencia.FechaInicio.ToString(), ausencia.FechaFin.ToString(), ex.StackTrace), ex);
                return false;
            }
        }
        public IEnumerable<Resultados> ConsultarAusencia(EDAusencia edAusencia)
        {
            try
            {
                List<Resultados> resultAusencias = new List<Resultados>();
                using (var context = new SG_SSTContext())
                {
                    var AusenciasPadre = context.Database.SqlQuery<Resultados>("SP_AUSENTISMO_CONSULTAR @documento, @fechaInicial, @fechaFin, @idSede, @idDiagnostico, @idEmpresaUsuaria",
                        new SqlParameter("@documento", int.Parse(edAusencia.Documento)),
                        new SqlParameter("@fechaInicial", string.IsNullOrEmpty (edAusencia.strFechaInicio) ? "" : string.Format("{0}/{1}/{2}", edAusencia.strFechaInicio.Split('/')[2], edAusencia.strFechaInicio.Split('/')[1], edAusencia.strFechaInicio.Split('/')[0])),
                        new SqlParameter("@fechaFin", string.IsNullOrEmpty(edAusencia.strFechaFin) ? "" : string.Format("{0}/{1}/{2}", edAusencia.strFechaFin.Split('/')[2], edAusencia.strFechaFin.Split('/')[1], edAusencia.strFechaFin.Split('/')[0])),
                        new SqlParameter("@idSede", edAusencia.IdSede),
                        new SqlParameter("@idDiagnostico", edAusencia.IdDiagnostico),
                        new SqlParameter("@idEmpresaUsuaria", edAusencia.IdEmpresaUsuaria)
                        //,                        new SqlParameter("@nitEmpresa", edAusencia.IdEmpresa)
                        ).ToList();

                    foreach (var ausencia in AusenciasPadre)
                    {
                        resultAusencias.Add(ausencia);
                        var AusenciasProrrogas = context.Database.SqlQuery<Resultados>("SP_PRORROGAS_CONSULTAR @idAusenciaPadre, @documento",
                        new SqlParameter("@idAusenciaPadre", ausencia.IdAusencias),
                        new SqlParameter("@documento", ausencia.Documento)).ToList();
                        resultAusencias.AddRange(AusenciasProrrogas);
                    }
                };

                return resultAusencias;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //TODO se comenta por que se debe cambiar el guardado
        public bool ProrrogarAusencia(EDAusencia prorrogar)
        {
            try
            {
                var result = false;
                using (var context = new SG_SSTContext())
                {
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
                    nuevaAusencia.FK_Id_Proceso = datosAusenciaPadre.FK_Id_Proceso;
                    nuevaAusencia.DiasAusencia = prorrogar.DiasAusencia;
                    nuevaAusencia.Costo = prorrogar.Costo;
                    nuevaAusencia.Factor_Prestacional = prorrogar.FactorPrestacional;
                    nuevaAusencia.FK_Id_Ocupacion = datosAusenciaPadre.FK_Id_Ocupacion;
                    nuevaAusencia.Sexo = datosAusenciaPadre.Sexo;
                    nuevaAusencia.Tipo_Vinculacion = datosAusenciaPadre.Tipo_Vinculacion;
                    nuevaAusencia.Edad = datosAusenciaPadre.Edad;
                    nuevaAusencia.Eps = datosAusenciaPadre.Eps;
                    nuevaAusencia.NombrePersona = datosAusenciaPadre.NombrePersona;
                    nuevaAusencia.FechaModificacion = DateTime.Now;
                    nuevaAusencia.FechaRegistro = DateTime.Now;

                    context.Tbl_Ausencias.Add(nuevaAusencia);
                    context.SaveChanges();
                    result = true;

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(AusenciaManager), string.Format("Error Prorrogando la ausencias: {0}, {1}. Error: {2}", DateTime.Now, prorrogar.FechaInicio.ToString(), prorrogar.FechaFin.ToString(), ex.StackTrace), ex);
                return false;
            }
            return true;
        }

        //SE COMENTA POR QUE SE DEBEN OBTENER LAS SEDES DE LA EMPRESA
        public List<string> ObtenerSedes()
        {
            //List<string> Sedes = new List<string>();
            //try
            //{
            //    using (var context = new SG_SSTContext())
            //    {
            //        Sedes = context.Tbl_Ausencias.Select(a => a.nombreRegional).Distinct().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var log = new RegistraLog();
            //    log.RegistrarError(typeof(AusenciaManager), string.Format("Error Prorrogando la ausencias: {0}, {1}. Error: {2}", DateTime.Now, ex.StackTrace), ex);
            //    return Sedes;
            //}

            return null;
        }


        public EDCargueMasivo InsertarAusenciasCargueMasivo(List<EDAusencia> AusenciasMasivo)
        {
            bool result = true;
            List<EDAusencia> Ausenciastmp = new List<EDAusencia>();
            EDCargueMasivo edCargue = new EDCargueMasivo();
            edCargue.Message = string.Empty;
            int contador = 1;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int idpadre = 0;
                        foreach (EDAusencia ausencia in AusenciasMasivo)
                        {
                            contador++;
                            Ausencia nuevaAusencia = new Ausencia();

                            bool puedeCrear = VerificarCrucesEnCargueMasivo(ausencia, Ausenciastmp);
                            if (puedeCrear)
                            {
                                if (ausencia.consecutivoPadre > 0)
                                {
                                    nuevaAusencia.FK_Id_Ausencias_Padre = idpadre;
                                    nuevaAusencia.Documento_Persona = ausencia.Documento;
                                    nuevaAusencia.NitEmpresa = ausencia.IdEmpresa;
                                    nuevaAusencia.FK_Id_EmpresaUsuaria = ausencia.IdEmpresaUsuaria;
                                    nuevaAusencia.FK_Id_Departamento = ausencia.idDepartamento;
                                    nuevaAusencia.FK_Id_Municipio = ausencia.idMunicipio;
                                    nuevaAusencia.FK_Id_Contingencia = ausencia.IdContingencia;
                                    nuevaAusencia.FK_Id_Diagnostico = ausencia.IdDiagnostico;
                                    nuevaAusencia.FK_Id_Sede = ausencia.IdSede;
                                    nuevaAusencia.FK_Id_Proceso = ausencia.IdProceso;
                                    nuevaAusencia.FechaInicio = ausencia.FechaInicio;
                                    nuevaAusencia.Fecha_Fin = ausencia.FechaFin;
                                    nuevaAusencia.DiasAusencia = ausencia.DiasAusencia;
                                    nuevaAusencia.Costo = ausencia.Costo;
                                    nuevaAusencia.Factor_Prestacional = ausencia.FactorPrestacional;
                                    nuevaAusencia.Observaciones = ausencia.Observaciones;
                                    nuevaAusencia.FK_Id_Ocupacion = ausencia.IdOcupacion;
                                    nuevaAusencia.Sexo = ausencia.Sexo;
                                    nuevaAusencia.Tipo_Vinculacion = ausencia.TipoVinculacion;
                                    nuevaAusencia.Edad = ausencia.Edad;
                                    nuevaAusencia.Eps = ausencia.Eps;
                                    nuevaAusencia.FechaModificacion = DateTime.Now;
                                    nuevaAusencia.FechaRegistro = DateTime.Now;
                                    nuevaAusencia.NombrePersona = ausencia.NombrePersona;

                                    context.Tbl_Ausencias.Add(nuevaAusencia);
                                    context.SaveChanges();
                                    Ausenciastmp.Add(ausencia);
                                }
                                else
                                {
                                    nuevaAusencia.Documento_Persona = ausencia.Documento;
                                    nuevaAusencia.NitEmpresa = ausencia.IdEmpresa;
                                    nuevaAusencia.FK_Id_EmpresaUsuaria = ausencia.IdEmpresaUsuaria;
                                    nuevaAusencia.FK_Id_Departamento = ausencia.idDepartamento;
                                    nuevaAusencia.FK_Id_Municipio = ausencia.idMunicipio;
                                    nuevaAusencia.FK_Id_Contingencia = ausencia.IdContingencia;
                                    nuevaAusencia.FK_Id_Diagnostico = ausencia.IdDiagnostico;
                                    nuevaAusencia.FK_Id_Sede = ausencia.IdSede;
                                    nuevaAusencia.FK_Id_Proceso = ausencia.IdProceso;
                                    nuevaAusencia.FechaInicio = ausencia.FechaInicio;
                                    nuevaAusencia.Fecha_Fin = ausencia.FechaFin;
                                    nuevaAusencia.DiasAusencia = ausencia.DiasAusencia;
                                    nuevaAusencia.Costo = ausencia.Costo;
                                    nuevaAusencia.Factor_Prestacional = ausencia.FactorPrestacional;
                                    nuevaAusencia.Observaciones = ausencia.Observaciones;
                                    nuevaAusencia.FK_Id_Ocupacion = ausencia.IdOcupacion;
                                    nuevaAusencia.Sexo = ausencia.Sexo;
                                    nuevaAusencia.Tipo_Vinculacion = ausencia.TipoVinculacion;
                                    nuevaAusencia.Edad = ausencia.Edad;
                                    nuevaAusencia.Eps = ausencia.Eps;
                                    nuevaAusencia.FechaModificacion = DateTime.Now;
                                    nuevaAusencia.FechaRegistro = DateTime.Now;
                                    nuevaAusencia.NombrePersona = ausencia.NombrePersona;

                                    context.Tbl_Ausencias.Add(nuevaAusencia);
                                    context.SaveChanges();
                                    idpadre = nuevaAusencia.Pk_Id_Ausencias;
                                    Ausenciastmp.Add(ausencia);
                                }                                
                            }
                            else
                            {
                                Transaction.Rollback();
                                edCargue.Message = "Proceso fallido, las fechas del registro de la fila " +contador+ " se cruzan con registros ya reportados. ";
                                result = false;
                                break;
                            }
                        }
                        if (result)
                        {
                            Transaction.Commit();
                            edCargue.Message = "OK";
                        }
                    }
                    catch
                    {
                        Transaction.Rollback();
                        edCargue.Message = "El proceso de cargue fallo, comuniquese con el administrador del sistema";
                    }
                }
            }

            return edCargue;
        }

        private bool VerificarCrucesEnCargueMasivo(EDAusencia ausencia, List<EDAusencia> Ausenciastmp)
        {

            List<EDAusencia> AusenciasList = ValidarCruceAusentismo(ausencia);
            AusenciasList.AddRange(Ausenciastmp);
            bool puedeCrear = true;
            foreach (var item in AusenciasList.Where(a => a.Documento.Equals(ausencia.Documento)).Select(a => a).ToList())
            {
                if (DateTime.Compare(ausencia.FechaFin, item.FechaInicio) >= 0 && DateTime.Compare(ausencia.FechaFin, item.FechaFin) <= 0)
                {
                    puedeCrear = false;
                    break;
                }
                else if (DateTime.Compare(ausencia.FechaInicio, item.FechaFin) <= 0 && DateTime.Compare(ausencia.FechaInicio, item.FechaInicio) >= 0)
                {
                    puedeCrear = false;
                    break;
                }
                else if (DateTime.Compare(ausencia.FechaInicio, item.FechaInicio) <= 0 && DateTime.Compare(ausencia.FechaFin, item.FechaFin) >= 0)
                {
                    puedeCrear = false;
                    break;
                }
            }
            return puedeCrear;
        }

        /// <summary>
        /// Consulta de alertas por ausentismo.
        /// </summary>
        /// <param name="parametros"></param>
        public List<EDAlertaAusentismo> ConsultarAlertaAusencia(EDAlertaAusentismoParametros parametros)
        {
            var Resultado = new List<EDAlertaAusentismo>();

            try
            {
                using (var context = new SG_SSTContext())
                {
                    foreach (var item in context.Database.SqlQuery<EDAlertaAusentismo>(
                        "SP_ALERTA_AUSENTISMO_CONSULTAR @idEmpresaUsuaria, @anioGestion",
                        new SqlParameter("@idEmpresaUsuaria", parametros.IdEmpresaUsuaria),
                        new SqlParameter("@anioGestion", parametros.AnioGestion)))
                    {
                        // Ajustar las fechas en caso que el periodo de la ausencia 
                        // comience el año anterior o termine el próximo año.
                        if (item.FechaInicio.Year < parametros.AnioGestion)
                            item.FechaInicio = new DateTime(parametros.AnioGestion, 1, 1);
                        if (item.FechaFin.Year > parametros.AnioGestion)
                            item.FechaFin = new DateTime(parametros.AnioGestion, 12, 31, 23, 59, 59);
                        Resultado.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return Resultado;
        }

        public List<string> BuscarDocumentos(string prefijo)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                return context.Tbl_Ausencias.Where(d => d.Documento_Persona.Contains(prefijo)).Select(d => d.Documento_Persona).Distinct().ToList();
            }
        }

        /// <summary>
        /// Busca un diagnóstico por el criterio de búsqueda.
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        public List<EDDiagnostico> BuscarDiagnostico(string prefijo)
        {
            var context = new SG_SSTContext();
            return context.Tbl_Diagnosticos.Where(d => d.Descripcion.Contains(prefijo) || d.Codigo_CIE.Contains(prefijo)).Select(d => new EDDiagnostico()
            {
                IdDiagnostico = d.PK_Id_Diagnostico,
                Codigo = d.Codigo_CIE,
                Descripcion = d.Descripcion
            }).ToList();
        }

        public bool GuardarDiasLaborables(string NitEmpresa, int IdDiasSeleccionado)
        {
            bool result = false;
            try
            {
                using (var context = new SG_SSTContext())
                {
                    var conf = context.Tbl_Dias_Laborables_Empresa.Where(d => d.Documento_empresa.Equals(NitEmpresa)).Select(d => d).FirstOrDefault();
                    if (conf == null)
                    {

                        Dias_Laborables_Empresa dle = new Dias_Laborables_Empresa();
                        dle.Documento_empresa = NitEmpresa;
                        dle.FK_Id_Dias_Laborables = IdDiasSeleccionado;

                        context.Tbl_Dias_Laborables_Empresa.Add(dle);
                    }
                    else
                    {
                        conf.FK_Id_Dias_Laborables = IdDiasSeleccionado;
                    }
                    context.SaveChanges();
                    result = true;
                }
            }catch (Exception e)
            {
                return result;
            }
            return result;
        }

        public int ObtenerDiasLaborablesEmpresa(string NitEmpresa)
        {
            int iddias = 1;
            using (var context = new SG_SSTContext())
            {
                var conf = context.Tbl_Dias_Laborables_Empresa.Where(d => d.Documento_empresa.Equals(NitEmpresa)).Select(d => d).FirstOrDefault();
                if (conf != null)
                    iddias = (int) conf.FK_Id_Dias_Laborables;  
            }
            return iddias;
        }

        public List<EDDiasLaborables> ConsultarDiasLaborables(string idEmpresa)
        {
            List<EDDiasLaborables> diasLaborables = new List<EDDiasLaborables>();
            using (var context = new SG_SSTContext())
            {

                var diasLaborablestmp = context.Tbl_Dias_Laborables.Select(d => new EDDiasLaborables ()
                {
                    IdDiaLaborable = d.PK_Id_Dia_Laborable,
                    Dia = d.Descripcion
                }).ToList();

                var conf = context.Tbl_Dias_Laborables_Empresa.Where(d => d.Documento_empresa.Equals(idEmpresa)).Select(d => d).FirstOrDefault();
                if (conf != null)
                    foreach(var item in diasLaborablestmp )
                    {
                        if (conf.FK_Id_Dias_Laborables == item.IdDiaLaborable)
                            item.Seleccionado = true;
                        else
                            item.Seleccionado = false;
                        diasLaborables.Add(item);
                    }
            }
            return diasLaborables;
        }
        public EDAusencia ConsultarAusenciaEliminar(string NitEmpresa, int idAusencia)
        {
            EDAusencia EDAusencia = new EDAusencia();
            using (var context = new SG_SSTContext())
            {
                var aus = (from s in context.Tbl_Ausencias
                                where s.Pk_Id_Ausencias == idAusencia && s.NitEmpresa == NitEmpresa
                           select s).FirstOrDefault<Ausencia>();
                if (aus!=null)
                {
                    EDAusencia.consecutivoPadre = aus.FK_Id_Ausencias_Padre;
                    EDAusencia.IdAusencia = aus.Pk_Id_Ausencias;
                }
            }
            return EDAusencia;
        }
        public bool EliminarAusencia(string NitEmpresa, int idAusencia)
        {
            EDAusencia EDAusencia = new EDAusencia();
            using (var context = new SG_SSTContext())
            {
                var aus = (from s in context.Tbl_Ausencias
                           where s.Pk_Id_Ausencias == idAusencia && s.NitEmpresa == NitEmpresa
                           select s).FirstOrDefault<Ausencia>();
                if (aus != null)
                {
                    context.Entry(aus).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public List<EDAusencia> ConsultarAusenciaProrrogas(string NitEmpresa, int idAusencia)
        {
            List<EDAusencia> ListaEDAusencia = new List<EDAusencia>();
            using (var context = new SG_SSTContext())
            {
                var aus = (from s in context.Tbl_Ausencias
                           where s.FK_Id_Ausencias_Padre == idAusencia && s.NitEmpresa == NitEmpresa
                           select s).ToList<Ausencia>();
                if (aus != null)
                {
                    foreach (var item in aus)
                    {
                        EDAusencia EDAusencia = new EDAusencia();
                        EDAusencia.consecutivoPadre = item.FK_Id_Ausencias_Padre;
                        EDAusencia.Costo = item.Costo;
                        EDAusencia.DiasAusencia = item.DiasAusencia;
                        EDAusencia.Documento = item.Documento_Persona;
                        ListaEDAusencia.Add(EDAusencia);
                    }
                    if (ListaEDAusencia.Count>0)
                    {
                        return ListaEDAusencia;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return ListaEDAusencia;
        }

        public string ConsultarDiagnostico(int idDiagnostico)
        {
            string huhi = "";
            using (var context = new SG_SSTContext())
            {
                var dia = (from s in context.Tbl_Diagnosticos
                           where s.PK_Id_Diagnostico== idDiagnostico
                           select s).FirstOrDefault<Diagnostico>();
                if (dia != null)
                {
                    return dia.Descripcion;
                }
            }
            return "";
        }
    }
}
