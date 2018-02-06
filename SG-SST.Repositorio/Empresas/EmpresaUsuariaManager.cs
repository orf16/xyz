using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Empresas;
using SG_SST.Models;
using SG_SST.Audotoria;
using System.Data;
using SG_SST.Models.Empresas;
using System.Data.SqlClient;

namespace SG_SST.Repositorio.Empresas
{
    public class EmpresaUsuariaManager : IEmpresaUsuaria
    {
        public string DescripcionDepartamento(string idDepartamento)
        {
            throw new NotImplementedException();

        }

        public string DescripcionMunicipio(string idDepartamento, string idMunicipio)
        {
            throw new NotImplementedException();
        }

        public bool EliminarEmpresasUsuarias(string DocumentoEmpresa)
        {
            bool rta = false;


            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<Empresa_Usuaria> eu = (from e in context.Tbl_Empresas_Usuarias
                                                      where e.Documento_Empresa == DocumentoEmpresa
                                                      select e).ToList();

                        if (eu.Count > 0)
                        {
                            foreach (Empresa_Usuaria item in eu)
                            {
                                context.Tbl_Empresas_Usuarias.Remove(item);
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        rta =  true;
                    }
                    catch (Exception ex)
                    {
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error eliminando empresas usuarias asociadas: {0}, {1}. error: {2}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        rta = false;
                    }
                }
            }
            return rta;
        }

        public DataTable EmpresaUsuaria()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PK_Id_Empresa_Usuaria", typeof(int));
            table.Columns.Add("Documento_Empresa", typeof(string));
            table.Columns.Add("Documento_Empresa_Usuaria", typeof(string));
            table.Columns.Add("FK_Id_Tipo_Documento", typeof(int));
            table.Columns.Add("Razon_Social", typeof(string));
            table.Columns.Add("Direccion", typeof(string));
            table.Columns.Add("FK_Id_Departamento", typeof(int));
            table.Columns.Add("FK_Id_Municipio", typeof(int));

            return table;
        }

        public bool ExisteEmpresaUsuaria(string DocumentoEmpresa, string DocumentoEmpresaUsuaria)
        {
            bool rta = false;


            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<Empresa_Usuaria> eu = (from e in context.Tbl_Empresas_Usuarias
                                                    where e.Documento_Empresa_Usuaria == DocumentoEmpresaUsuaria &&
                                                    e.Documento_Empresa == DocumentoEmpresa
                                                    select e).ToList();

                        if (eu.Count > 0)
                            rta = true;
                        else
                            rta = false;
                    }
                    catch (Exception ex)
                    {
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error eliminando empresas usuarias asociadas: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        rta = false;
                    }
                }
            }
            return rta;
        }

        public bool GuardarEmpresasUsuarias(DataTable drEmpresasUsuarias)
        {
            bool rta = false;

            try
            {
                var result = false;
                bool empresasInsertadas = false;
                SqlTransaction transaction = null;

                using (var context = new SG_SSTContext())
                {
                    SqlConnection con = context.Database.Connection as SqlConnection;
                    con.Open();
                    if (drEmpresasUsuarias.Rows.Count > 0)
                    {

                        //Se inicia una transacción
                        using (transaction = con.BeginTransaction())
                        {
                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(con, SqlBulkCopyOptions.FireTriggers, transaction))
                            {
                                bulkcopy.BatchSize = 1000;
                                bulkcopy.NotifyAfter = 100;
                                bulkcopy.DestinationTableName = "Tbl_Empresas_Usuarias";
                                bulkcopy.WriteToServer(drEmpresasUsuarias);
                            }
                            transaction.Commit();
                            empresasInsertadas = true;
                        }

                    }

                    con.Close();
                }
                rta = true;
            }

            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("Error Grabando empresas Usuarias Asociadas : {0},  Error: {1}", DateTime.Now, ex.StackTrace), ex);
                rta = false;
            }
            return rta;

        }

        public List<EDDepartamento> ObtenerDepartamentos()
        {
            List<EDDepartamento>  lstDepartamento = new List<EDDepartamento>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    lstDepartamento = context.Tbl_Departamento.Select(
                        a => new EDDepartamento
                        {
                            Codigo_Departamento = a.Pk_Id_Departamento.ToString(),
                            Nombre_Departamento = a.Nombre_Departamento.Trim().ToUpper()
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo departamentos: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return lstDepartamento;
        }

        public List<EDTipoDocumento> ObtenerDocumentos()
        {
            List<EDTipoDocumento> lstDocumentos = new List<EDTipoDocumento>();
            try
            {

                using (SG_SSTContext context = new SG_SSTContext())
                {
                    lstDocumentos = context.Tbl_TipoDocumentos.Select(
                        a => new EDTipoDocumento
                        {
                            Id_Tipo_Documento = a.PK_IDTipo_Documento,
                            Descripcion = a.Descripcion.Trim().ToUpper(),
                            Sigla = a.Sigla
                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo documentos: {0}, error: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return lstDocumentos;
        }

        public List<EDMunicipio> ObtenerMunicipio(int PK_Departamento)
        {

        List<EDMunicipio> municipio = null;
            try
            {
                using (var context = new SG_SSTContext())
                {
                    municipio = (from a in context.Tbl_Municipio
                                 where a.Fk_Nombre_Departamento == PK_Departamento
                                 select new EDMunicipio
                                 {
                                     IdMunicipio = a.Pk_Id_Municipio,
                                     NombreMunicipio = a.Nombre_Municipio
                                 }).ToList();
                }
                return municipio;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(EmpresaUsuariaManager), string.Format("error trayendo departamentos: {0}, {1}. error: {2}", DateTime.Now, ex.StackTrace), ex);

            }
            return municipio;
        }

        public List<EDMunicipio> ObtenerMunicipiosConDetps()
        {
            throw new NotImplementedException();
        }


    }
}
