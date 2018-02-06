using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Interfaces.Usuarios;
using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.Models.LiderazgoGerencial;
using SG_SST.Models.Usuarios;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Usuarios
{
    public class UsuarioManager : IUsuario
    {
        RegistraLog registroLog = new RegistraLog();
        /// <summary>
        /// Obtiene los tipos de documento configurados en el sistema
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDTipoDocumento> ObtenerTiposDocumento()
        {
            try
            {
                List<EDTipoDocumento> resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_TipoDocumentos.Select(td => new EDTipoDocumento()
                    {
                        Id_Tipo_Documento = td.PK_IDTipo_Documento,
                        Descripcion = td.Descripcion,
                        Sigla = td.Sigla
                    }).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return new List<EDTipoDocumento>() { };//Se debe configurar para que se registe el Log
            }
        }

        /// <summary>
        /// Obtiene los roles definidos en el sistema
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDRolSistema> ObtenerRolesSistema()
        {
            try
            {
                List<EDRolSistema> resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_RolesSistema.Select(rs => new EDRolSistema()
                    {
                        IdRolSistema = rs.Pk_Id_RolSistema,
                        NombreRol = rs.Nombre,
                        Sigla = rs.Sigla,
                        CantidadMaxUsuarios = rs.CantidadUsuariosPorRol,
                        Activo = rs.Activo
                    }).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return new List<EDRolSistema>() { };//Se debe configurar para que se registe el Log
            }
        }

        /// <summary>
        /// Obitiene el listado de causales de rechazo de un usuario
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDCausalRechazoUsuarioSistema> ObtenerCausalesRechazoUsuariosSistema()
        {
            try
            {
                List<EDCausalRechazoUsuarioSistema> resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_CausalesRechazoUsuariosSistems.Select(crs => new EDCausalRechazoUsuarioSistema()
                    {
                        IdCausalRechazo = crs.Pk_Id_CausalRechazo,
                        NombreCausalRechazo = crs.Nombre,
                        DescripcionCausalRechazo = crs.Descripcion
                    }).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return new List<EDCausalRechazoUsuarioSistema>() { };//Se debe configurar para que se registe el Log
            }
        }

        /// <summary>
        /// Obtiene la preguntas de seguridad configuradas en el sistema Alista
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDPreguntaSeguridad> ObtenerPreguntasSeguridad()
        {
            try
            {
                List<EDPreguntaSeguridad> resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_PreguntasSeguridad.Select(ps => new EDPreguntaSeguridad()
                    {
                        IdPreguntaSeguridad = ps.Pk_Id_PreguntaSeguridad,
                        NombrePreguntaSeguridad = ps.NombrePreguntaSeguridad,
                        Descripcion = ps.Descricion
                    }).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return new List<EDPreguntaSeguridad>() { };//Se debe configurar para que se registe el Log
            }
        }
        public IEnumerable<EDParametroSistema> ObtenerParametrosSistema(List<int> idParametros)
        {
            try
            {
                List<EDParametroSistema> resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_ParametrosSistema.Where(ps => idParametros.Contains(ps.IdParametro)).Select(ps => new EDParametroSistema()
                    {
                        IdParametro = ps.IdParametro,
                        Valor = ps.Valor
                    }).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return new List<EDParametroSistema>() { };//Se debe configurar para que se registe el Log
            }
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
        public EDParametroSistema ObtenerParametrosSistema(int codPlantilla)
        {
            try
            {
                EDParametroSistema resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_PlantillasCorreosSistema.Where(pc => pc.IdPlantilla == codPlantilla).Select(pc => new EDParametroSistema()
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

        /// <summary>
        /// Retorna los datos del usuario asociado al documento pasado por parametro
        /// </summary>
        /// <returns></returns>
        public EDUsuarioSistema ObtenerUsuarioPorId(string documento)
        {
            try
            {
                EDUsuarioSistema resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Tbl_UsuarioSistema.Where(us => us.Documento.Equals(documento)).Select(us => new EDUsuarioSistema
                    {
                        IdUsuarioSistema = us.Pk_Id_UsuarioSistema,
                        Documento = us.Documento,
                        ClaveSalt = us.ClaveSalt,
                        ClaveHash = us.ClaveHash,
                        Correo = us.Correo,
                        Activo = us.Activo,

                    }).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene las empresas que no estan registras en Alista
        /// </summary>
        /// <param name="usuariosAprobar"></param>
        /// <returns></returns>
        public Dictionary<string, string> ObtenerEmpresasSinRegistrar(List<EDUsuarioPorAprobar> usuariosAprobar)
        {
            try
            {
                List<int> usuariosConEmpresas = null;
                var idUsuariosAprobar = usuariosAprobar.Select(ua => ua.IdUsuarioPorAprobar).ToList();
                Dictionary<string, string> empresasSinRegistrar = null;
                using (var context = new SG_SSTContext())
                {
                    usuariosConEmpresas = (from e in context.Tbl_Empresa
                                           join td in context.Tbl_TipoDocumentos on e.Tipo_Documento.ToUpper() equals td.Sigla.ToUpper()
                                           join ua in context.Tbl_UsuariosParaAprobar on new { A = td.PK_IDTipo_Documento, B = e.Nit_Empresa } equals new { A = ua.TipoDocumentoEmpresa, B = ua.NumeroDocumentoEmprsa }
                                           where idUsuariosAprobar.Contains(ua.Pk_id_UsuarioParaAprobar)
                                           select ua.Pk_id_UsuarioParaAprobar).ToList();
                    var empresas = (from up in context.Tbl_UsuariosParaAprobar
                                    join td in context.Tbl_TipoDocumentos on up.TipoDocumentoEmpresa equals td.PK_IDTipo_Documento
                                    where !usuariosConEmpresas.Contains(up.Pk_id_UsuarioParaAprobar)
                                    select new
                                    {
                                        IdEmpresa = (up == null ? 0 : up.Pk_id_UsuarioParaAprobar),
                                        Tid = (td == null ? string.Empty : td.Sigla),
                                        Nid = (up == null ? string.Empty : up.NumeroDocumentoEmprsa)
                                    }).ToList();
                    empresasSinRegistrar = (from e in empresas
                                            where idUsuariosAprobar.Contains(e.IdEmpresa)
                                            group e by e.Nid into agrp

                                            select new
                                            {
                                                Nid = agrp.Key,
                                                Tid = (agrp.FirstOrDefault() == null ? string.Empty : agrp.FirstOrDefault().Tid)
                                            }).ToDictionary(e => e.Nid, e => e.Tid);
                }
                return empresasSinRegistrar;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Devuelve el listado de usuarios del sistema a crear a partir
        /// de los usuarios aprobados
        /// </summary>
        /// <param name="usuariosAprobar"></param>
        /// <returns></returns>
        public List<EDUsuarioSistema> ObtenerDatosUsuariosSistema(List<EDUsuarioPorAprobar> usuariosAprobar)
        {
            try
            {
                List<EDUsuarioSistema> usuariosSistema = null;
                List<EDUsuarioSistema> usuariosSistemaFnal = null;
                var idUsuariosAprobar = usuariosAprobar.Select(ua => ua.IdUsuarioPorAprobar).ToList();
                using (var context = new SG_SSTContext())
                {
                    usuariosSistema = (from e in context.Tbl_Empresa
                                       join td in context.Tbl_TipoDocumentos on e.Tipo_Documento.ToUpper() equals td.Sigla.ToUpper()
                                       join ua in context.Tbl_UsuariosParaAprobar on new { A = td.PK_IDTipo_Documento, B = e.Nit_Empresa } equals new { A = ua.TipoDocumentoEmpresa, B = ua.NumeroDocumentoEmprsa }
                                       where idUsuariosAprobar.Contains(ua.Pk_id_UsuarioParaAprobar)
                                       select new EDUsuarioSistema()
                                       {
                                           IdUsuarioSistema = ua.Pk_id_UsuarioParaAprobar,//este id es el usuario a aprobar
                                           Nombres = ua.Nombres,
                                           Apellidos = ua.Apellidos,
                                           IdRol = ua.Fk_Id_RolSistema,
                                           CodEmpresa = e.Pk_Id_Empresa,
                                           TipoDocumentoEmpresa = td.PK_IDTipo_Documento,
                                           DocumentoEmpresa = e.Nit_Empresa,
                                           TipoDocumento = ua.TipoDocumentoUsuario,
                                           Documento = ua.NumeroDocumentoUsuario,
                                           Correo = ua.EmailUsuario,
                                       }).ToList();
                }
                if (usuariosSistema != null && usuariosSistema.Count > 0)
                {
                    usuariosSistemaFnal = (from us in usuariosSistema
                                           join ua in usuariosAprobar on us.IdUsuarioSistema equals ua.IdUsuarioPorAprobar
                                           select new EDUsuarioSistema()
                                           {
                                               IdUsuarioSistema = us.IdUsuarioSistema,
                                               Nombres = us.Nombres,
                                               Apellidos = us.Apellidos,
                                               IdRol = us.IdRol,
                                               CodEmpresa = us.CodEmpresa,
                                               TipoDocumentoEmpresa = us.TipoDocumentoEmpresa,
                                               DocumentoEmpresa = us.DocumentoEmpresa,
                                               TipoDocumento = us.TipoDocumento,
                                               Documento = us.Documento,
                                               Correo = us.Correo,
                                               PeriodoInactividad = ua.PeriodoInactividad
                                           }).ToList();

                }
                return usuariosSistemaFnal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene el listado de usuarios del sistema para aprobar
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDUsuarioPorAprobar> ObtenerUsuariosParaAprobar(string numDocEmp, string numDocUsu, string rolSeleccionado, int paginaActual)
        {
            try
            {
                List<EDUsuarioPorAprobar> resultado = null;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Database.SqlQuery<EDUsuarioPorAprobar>("BuscarUsuariosParaAprobar @NumDocEmp, @NumDocUsu, @RolSeleccionado, @Pagina",
                        new SqlParameter("@NumDocEmp", numDocEmp),
                        new SqlParameter("@NumDocUsu", numDocUsu),
                        new SqlParameter("@RolSeleccionado", rolSeleccionado),
                        new SqlParameter("@Pagina", paginaActual)).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numDocEmp"></param>
        /// <param name="numDocUsu"></param>
        /// <param name="rolSeleccionado"></param>
        /// <returns></returns>
        public int ObtenerTotalUsuariosParaAprobar(string numDocEmp, string numDocUsu, string rolSeleccionado)
        {
            try
            {
                var resultado = 0;
                using (var context = new SG_SSTContext())
                {
                    resultado = context.Database.SqlQuery<int>("ObtenerCantidadUsuariosParaAprobar @NumDocEmp, @NumDocUsu, @RolSeleccionado",
                        new SqlParameter("@NumDocEmp", numDocEmp),
                        new SqlParameter("@NumDocUsu", numDocUsu),
                        new SqlParameter("@RolSeleccionado", rolSeleccionado)).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Valida que no exista un usuario registrado bajo el mismo documento de
        /// empresa y afiliado
        /// </summary>
        /// <param name="usuarioRegistrar"></param>
        /// <returns></returns>
        public bool ValidarExistenciaUsuario(EDUsuarioPorAprobar usuarioRegistrar, out int error)
        {
            try
            {
                var resultado = false;
                using (var context = new SG_SSTContext())
                {
                    var usuarioSistema = (from u in context.Tbl_UsuarioSistema
                                          join ue in context.Tbl_UsuarioSistemaEmpresa on u.Pk_Id_UsuarioSistema equals ue.Fk_Id_UsuarioSistema
                                          join emp in context.Tbl_Empresa on ue.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                                          where u.Documento.Equals(usuarioRegistrar.NumDocumentoAfi) && emp.Nit_Empresa.Equals(usuarioRegistrar.NumDocumentoEmpresa)
                                          select u).FirstOrDefault();
                    if (usuarioSistema != null)
                        resultado = true;
                }
                error = 0;
                return resultado;
            }
            catch (Exception ex)
            {
                error = 1;
                return false;
            }
        }

        /// <summary>
        /// Registra al empleado que ha solicitado la creaciión de su usuario en el sistema
        /// </summary>
        /// <param name="usuarioSistema"></param>
        /// <returns></returns>
        public bool RegistrarUsuarioParaAprobar(EDUsuarioPorAprobar usuarioAprobar)
        {
            try
            {
                var resultado = false;
                using (var context = new SG_SSTContext())
                {
                    using (var tx = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var tipoDocumentoEmpresa = context.Tbl_TipoDocumentos.Where(td => td.Sigla.ToUpper().Equals(usuarioAprobar.TipoDocumentoEmpresa.ToUpper())).Select(td => td.PK_IDTipo_Documento).FirstOrDefault();
                            var tipoDocumentoEmpleado = context.Tbl_TipoDocumentos.Where(td => td.Sigla.ToUpper().Equals(usuarioAprobar.TipoDocumentoAfi.ToUpper())).Select(td => td.PK_IDTipo_Documento).FirstOrDefault();
                            var usuarioPorAprobar = new UsuarioParaAprobar();
                            usuarioPorAprobar.TipoDocumentoEmpresa = tipoDocumentoEmpresa;
                            usuarioPorAprobar.NumeroDocumentoEmprsa = usuarioAprobar.NumDocumentoEmpresa;
                            usuarioPorAprobar.RazonSocial = usuarioAprobar.RazonSocialEmpresa;
                            usuarioPorAprobar.MunicipioSedePpal = usuarioAprobar.MunicipioSedePpalEmpresa;
                            usuarioPorAprobar.TipoDocumentoUsuario = tipoDocumentoEmpleado;
                            usuarioPorAprobar.NumeroDocumentoUsuario = usuarioAprobar.NumDocumentoAfi;
                            usuarioPorAprobar.Nombres = usuarioAprobar.Nombres;
                            usuarioPorAprobar.Apellidos = usuarioAprobar.Apellidos;
                            usuarioPorAprobar.Fk_Id_RolSistema = usuarioAprobar.RolUsuario;
                            usuarioPorAprobar.EmailUsuario = usuarioAprobar.Correo;
                            context.Tbl_UsuariosParaAprobar.Add(usuarioPorAprobar);
                            context.SaveChanges();
                            if(usuarioAprobar.PreguntasSeguridadSeleccionadas.Count > 0)
                            {
                                var preguntasSeguridadSelec = new List<RespuestaAPreguntaSeguridad>();
                                foreach (var ps in usuarioAprobar.PreguntasSeguridadSeleccionadas)
                                {
                                    var rps = new RespuestaAPreguntaSeguridad();
                                    rps.Fk_Id_PreguntaSeguridad = ps.CodPreguntaSeguridad;
                                    rps.RespuestareguntaSeguridad = ps.RespuestaSeguridad;
                                    rps.CodUsuarioAprobar = usuarioPorAprobar.Pk_id_UsuarioParaAprobar;
                                    preguntasSeguridadSelec.Add(rps);
                                }
                                context.Tbl_RespuestasAPreguntasSeguridad.AddRange(preguntasSeguridadSelec);
                                context.SaveChanges();
                            }
                            if (!string.IsNullOrEmpty(usuarioAprobar.DepartamentoSedePpalEmpresa))
                            {
                                var nuevoDatoUsuario = new DatoAdicionalUsuario();
                                nuevoDatoUsuario.CodUsuarioAprobar = usuarioPorAprobar.Pk_id_UsuarioParaAprobar;
                                nuevoDatoUsuario.NombreDato = "Departamento";
                                nuevoDatoUsuario.ValorDato = usuarioAprobar.DepartamentoSedePpalEmpresa;
                                context.Tbl_DatosAdicionalesUsuario.Add(nuevoDatoUsuario);
                                context.SaveChanges();
                            }
                            if (!string.IsNullOrEmpty(usuarioAprobar.MunicipioSedePpalEmpresa))
                            {
                                var nuevoDatoUsuario = new DatoAdicionalUsuario();
                                nuevoDatoUsuario.CodUsuarioAprobar = usuarioPorAprobar.Pk_id_UsuarioParaAprobar;
                                nuevoDatoUsuario.NombreDato = "Municipio";
                                nuevoDatoUsuario.ValorDato = usuarioAprobar.MunicipioSedePpalEmpresa;
                                context.Tbl_DatosAdicionalesUsuario.Add(nuevoDatoUsuario);
                                context.SaveChanges();
                            }
                            if (!string.IsNullOrEmpty(usuarioAprobar.Telefono))
                            {
                                var nuevoDatoUsuario = new DatoAdicionalUsuario();
                                nuevoDatoUsuario.CodUsuarioAprobar = usuarioPorAprobar.Pk_id_UsuarioParaAprobar;
                                nuevoDatoUsuario.NombreDato = "Telefono";
                                nuevoDatoUsuario.ValorDato = usuarioAprobar.Telefono;
                                context.Tbl_DatosAdicionalesUsuario.Add(nuevoDatoUsuario);
                                context.SaveChanges();
                            }
                            tx.Commit();
                            resultado = true;
                        }catch(Exception ex)
                        {
                            tx.Rollback();
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Inserta una lista de entidades en bloques de 100
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="usuariosRegistrar"></param>
        /// <returns></returns>
        public bool InsertarUsuariosAprobadosSistema(List<EDUsuarioSistema> usuariosRegistrar)
        {
            try
            {
                var resultado = false;
                using (var context = new SG_SSTContext())
                {
                    using (var tx = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var usuario in usuariosRegistrar)
                            {
                                //se registra un nuevo UsuarioSistema
                                var nuevoUsuario = new UsuarioSistema();
                                nuevoUsuario.Fk_Id_TipoDocumento = usuario.TipoDocumento;
                                nuevoUsuario.ClaveSalt = usuario.ClaveSalt;
                                nuevoUsuario.ClaveHash = usuario.ClaveHash;
                                nuevoUsuario.Correo = usuario.Correo;
                                nuevoUsuario.Documento = usuario.Documento;
                                nuevoUsuario.Nombres = usuario.Nombres;
                                nuevoUsuario.Apellidos = usuario.Apellidos;
                                nuevoUsuario.Activo = true;
                                nuevoUsuario.PrimerAcceso = true;
                                nuevoUsuario.PeriodoInactivacionCuenta = usuario.PeriodoInactividad;
                                context.Tbl_UsuarioSistema.Add(nuevoUsuario);
                                context.SaveChanges();
                                usuario.IdUsuarioSistema = nuevoUsuario.Pk_Id_UsuarioSistema;
                                //se registra un nuevo UsuarioSistemaEmpresa
                                var nuevoUsuarioEmpresa = new UsuarioSistemaEmpresa();
                                nuevoUsuarioEmpresa.Fk_Id_Empresa = usuario.CodEmpresa;
                                nuevoUsuarioEmpresa.Fk_Id_UsuarioSistema = usuario.IdUsuarioSistema;
                                context.Tbl_UsuarioSistemaEmpresa.Add(nuevoUsuarioEmpresa);
                                context.SaveChanges();
                                //se registra un nuevo usuario por rol
                                var usuariosPorRol = new UsuarioPorRol();
                                usuariosPorRol.Fk_Id_RolSistema = usuario.IdRol;
                                usuariosPorRol.Fk_Id_UsuarioSistema = nuevoUsuario.Pk_Id_UsuarioSistema;
                                context.Tbl_UsuariosPorRol.Add(usuariosPorRol);
                                //se registra una nueva clave temporal para el usuario
                                var passwordTemporal = new PasswordTemporalUsuariosSistema();
                                passwordTemporal.Fk_Id_UsuarioSistema = nuevoUsuario.Pk_Id_UsuarioSistema;
                                passwordTemporal.Password = usuario.Clave;
                                passwordTemporal.PasswordSalt = nuevoUsuario.ClaveSalt;
                                passwordTemporal.PasswordHash = nuevoUsuario.ClaveHash;
                                context.Tbl_PasswordsTemporalesUsuariosSistema.Add(passwordTemporal);
                                context.SaveChanges();
                                usuario.IdUsuarioSistema = nuevoUsuario.Pk_Id_UsuarioSistema;
                                int codUsuarioPorAprobar = 0;
                                EliminarUsuariosPorAprovar(usuario, out codUsuarioPorAprobar);
                                //actualizar id usuario en preguntas seguridad
                                var rtaptasdad = context.Tbl_RespuestasAPreguntasSeguridad.Where(rps => rps.CodUsuarioAprobar == codUsuarioPorAprobar).Select(rps => rps).ToList();
                                if (rtaptasdad != null && rtaptasdad.Count() > 0)
                                    rtaptasdad.ForEach(rps => { rps.CodUsuarioSistema = nuevoUsuario.Pk_Id_UsuarioSistema; rps.CodUsuarioAprobar = null; });
                                context.SaveChanges();
                                var empresaActualizar = context.Tbl_Empresa.Where(e => e.Pk_Id_Empresa == usuario.CodEmpresa).Select(e => e).FirstOrDefault();
                                if (empresaActualizar != null)
                                {
                                    empresaActualizar.Nit_Empresa = usuario.DocumentoEmpresa;
                                    context.SaveChanges();
                                }
                            }
                            tx.Commit();
                            resultado = true;
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// se insertan los usuarios que fueron rechazados por alguna causal
        /// </summary>
        /// <param name="usuariosSistema"></param>
        /// <returns></returns>
        public bool InsertarUsuariosNoAprobadosSistema(List<EDUsuarioPorAprobar> usuariosRechazados)
        {
            try
            {
                var resultado = false;
                using (var context = new SG_SSTContext())
                {
                    using (var tx = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (usuariosRechazados != null && usuariosRechazados.Count > 0)
                            {
                                foreach (var usuario in usuariosRechazados)
                                {
                                    //nuevo UsuarioSistema
                                    var nuevoUsuario = new UsuarioRechazadoSistema();
                                    nuevoUsuario.Fk_Id_UsuarioParaActivar = usuario.IdUsuarioPorAprobar;
                                    nuevoUsuario.Fk_Id_CausalRechazoUsuario = usuario.CausalRechazo;
                                    context.Tbl_UsuariosRechazadosSitema.Add(nuevoUsuario);
                                    EliminarUsuariosPorRechazar(context, usuario);
                                    context.SaveChanges();
                                }
                                tx.Commit();
                                resultado = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public EDUsuarioSistema AutenticarUsuario(EDUsuarioSistema usuarioActual)
        {
            try
            {
                EDUsuarioSistema resultado = null;
                using (var context = new SG_SSTContext())
                {
                    var r = (from u in context.Tbl_UsuarioSistema
                                 //join r in context.Tbl_UsuariosPorRol on u.Pk_Id_UsuarioSistema equals r.Fk_Id_UsuarioSistema
                             join ue in context.Tbl_UsuarioSistemaEmpresa on u.Pk_Id_UsuarioSistema equals ue.Fk_Id_UsuarioSistema
                             join e in context.Tbl_Empresa on ue.Fk_Id_Empresa equals e.Pk_Id_Empresa
                             where u.Documento.Equals(usuarioActual.Documento) && e.Nit_Empresa.Equals(usuarioActual.DocumentoEmpresa)
                             select new EDUsuarioSistema()
                             {
                                 IdUsuarioSistema = u.Pk_Id_UsuarioSistema,
                                 CodEmpresa = e.Pk_Id_Empresa,
                                 TipoDocumentoSiglaEmpresa = e.Tipo_Documento,
                                 DocumentoEmpresa = e.Nit_Empresa,
                                 RazonSocial = e.Razon_Social,
                                 TipoDocumentoSigla = u.TipoDocumento.Sigla,
                                 Documento = u.Documento,
                                 Nombres = u.Nombres,
                                 Apellidos = u.Apellidos,
                                 ClaveSalt = u.ClaveSalt,
                                 ClaveHash = u.ClaveHash,
                                 PrimerAcceso = u.PrimerAcceso,
                                 Activo = u.Activo
                             });
                    resultado = r.FirstOrDefault();                  
                }

                return resultado;
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(UsuarioManager), string.Format("Error en la accion AutenticarUsuario Servicio Autenticacion . Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return null;//Se debe configurar para que se registe el Log
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDocEmp"></param>
        /// <param name="numDocEmp"></param>
        /// <param name="tipoDocUsuario"></param>
        /// <param name="numDocUsuario"></param>
        /// <returns></returns>
        public EDUsuarioSistema ConsultarUsuarioPorRelacionLaboral(string tipoDocEmp, string numDocEmp, string tipoDocUsuario, string numDocUsuario)
        {
            try
            {
                EDUsuarioSistema usuarioSistema = null;
                using (var context = new SG_SSTContext())
                {
                    //obtener el usuario de sistema que desea recuperar su clave
                    usuarioSistema = (from u in context.Tbl_UsuarioSistema
                                          join ue in context.Tbl_UsuarioSistemaEmpresa on u.Pk_Id_UsuarioSistema equals ue.Fk_Id_UsuarioSistema
                                          join e in context.Tbl_Empresa on ue.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                          join td in context.Tbl_TipoDocumentos on u.Fk_Id_TipoDocumento equals td.PK_IDTipo_Documento
                                          where td.Sigla == tipoDocUsuario
                                          && e.Nit_Empresa.Equals(numDocEmp)
                                          && u.Documento.Equals(numDocUsuario)
                                          && e.Tipo_Documento.Equals(tipoDocEmp)
                                          select new EDUsuarioSistema() {
                                              IdUsuarioSistema = u.Pk_Id_UsuarioSistema,
                                              Correo = u.Correo,
                                              Activo = u.Activo,
                                              Nombres = u.Nombres,
                                              Apellidos = u.Apellidos,
                                              Documento = u.Documento,
                                              PrimerAcceso = u.PrimerAcceso
                                          }).FirstOrDefault();
                }
                return usuarioSistema;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public EDUsuarioSistema ConsultarDatosUsuarioPorRelacionLaboral(string tipoDocEmp, string numDocEmp, string tipoDocUsuario, string numDocUsuario)
        {
            try
            {
                EDUsuarioSistema usuarioSistema = null;
                using (var context = new SG_SSTContext())
                {
                    //obtener el usuario de sistema que desea recuperar su clave
                    usuarioSistema = (from u in context.Tbl_UsuarioSistema
                                      join ue in context.Tbl_UsuarioSistemaEmpresa on u.Pk_Id_UsuarioSistema equals ue.Fk_Id_UsuarioSistema
                                      join e in context.Tbl_Empresa on ue.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                      join td in context.Tbl_TipoDocumentos on u.Fk_Id_TipoDocumento equals td.PK_IDTipo_Documento
                                      where td.Sigla == tipoDocUsuario
                                      && e.Nit_Empresa.Equals(numDocEmp)
                                      && u.Documento.Equals(numDocUsuario)
                                      && e.Tipo_Documento.Equals(tipoDocEmp)
                                      select new EDUsuarioSistema()
                                      {
                                          IdUsuarioSistema = u.Pk_Id_UsuarioSistema,
                                          Correo = u.Correo,
                                          Activo = u.Activo,
                                          Nombres = u.Nombres,
                                          Apellidos = u.Apellidos,
                                          Documento = u.Documento,
                                          PrimerAcceso = u.PrimerAcceso
                                      }).FirstOrDefault();
                    if (usuarioSistema != null)
                    {
                        usuarioSistema.PreguntasSeguridad = context.Tbl_RespuestasAPreguntasSeguridad.Where(rps => rps.CodUsuarioSistema == usuarioSistema.IdUsuarioSistema).Select(rps => new PreguntasSeguridadSeleccionada()
                        {
                            CodPreguntaSeguridad = rps.Fk_Id_PreguntaSeguridad,
                            NombrePregunta = rps.PreguntaSeguridad.NombrePreguntaSeguridad,
                            RespuestaSeguridad = rps.RespuestareguntaSeguridad
                        }).ToList();
                    }
                }
                return usuarioSistema;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool CambiarClaveUsuario(EDUsuarioSistema usuario)
        {
            try
            {
                var resultado = false;
                using (var context = new SG_SSTContext())
                {
                    //obtener el usuario sistema
                    var usuarioSistema = context.Tbl_UsuarioSistema.Where(u => u.Pk_Id_UsuarioSistema == usuario.IdUsuarioSistema).Select(u => u).FirstOrDefault();
                    if (usuarioSistema != null)
                    {
                        usuarioSistema.ClaveSalt = usuario.ClaveSalt;
                        usuarioSistema.ClaveHash = usuario.ClaveHash;
                        usuarioSistema.Activo = usuario.Activo;
                        usuarioSistema.PrimerAcceso = usuario.PrimerAcceso;
                        context.SaveChanges();
                        resultado = true;
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ActualizarClaveUsuarioParaRecuperarClave(EDUsuarioSistema usuario)
        {
            try
            {
                var resultado = false;
                using (var context = new SG_SSTContext())
                {
                    //obtener el usuario sistema
                    var usuarioSistema = context.Tbl_UsuarioSistema.Where(u => u.Pk_Id_UsuarioSistema == usuario.IdUsuarioSistema).Select(u => u).FirstOrDefault();
                    if (usuarioSistema != null)
                    {
                        usuarioSistema.ClaveSalt = usuario.ClaveSalt;
                        usuarioSistema.ClaveHash = usuario.ClaveHash;
                        usuarioSistema.Activo = usuario.Activo;
                        usuarioSistema.PrimerAcceso = usuario.PrimerAcceso;
                        context.SaveChanges();
                        resultado = true;
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Valida que exista creado al menos un usuario con el rol pasado por parámetro y retorna:
        /// 0: Si el usuario no existe, 1: Si el usuario existe y se encuentra Inactivo, 2: si el usuario
        /// existe y se encuentra Activo
        /// </summary>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public int ValidarExistenciaUsuarioRolRepresLegal(string tipoDocumentoEmpresa, string numeroDocEmpresa, int RolEvaluar)
        {
            try
            {
                var resultado = 0;
                using (var context = new SG_SSTContext())
                {
                    var result = (from u in context.Tbl_UsuarioSistema
                                  join ur in context.Tbl_UsuariosPorRol on u.Pk_Id_UsuarioSistema equals ur.Fk_Id_UsuarioSistema
                                  join ue in context.Tbl_UsuarioSistemaEmpresa on u.Pk_Id_UsuarioSistema equals ue.Fk_Id_UsuarioSistema
                                  join e in context.Tbl_Empresa on ue.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                  where e.Nit_Empresa.ToUpper().Equals(numeroDocEmpresa) && e.Tipo_Documento.ToUpper().Equals(tipoDocumentoEmpresa.ToUpper()) && ur.Fk_Id_RolSistema == RolEvaluar
                                  select u).FirstOrDefault();
                    if (result == null)
                    {
                        //validar si existe un usuario bajo este rol pendiente por aprobación
                        var tipDocumentoEmpresa = context.Tbl_TipoDocumentos.Where(td => td.Sigla.ToUpper().Equals(tipoDocumentoEmpresa.ToUpper())).Select(td => td.PK_IDTipo_Documento).FirstOrDefault();
                        var usuarioPorAprobar = context.Tbl_UsuariosParaAprobar
                            .Where(ua => ua.TipoDocumentoEmpresa == tipDocumentoEmpresa && ua.NumeroDocumentoEmprsa.Equals(numeroDocEmpresa) && ua.Fk_Id_RolSistema == RolEvaluar)
                            .Select(ua => ua).FirstOrDefault();
                        if (usuarioPorAprobar == null)
                            resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.NoExiste;
                        else
                            resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.ExistePorAprobar;
                    }
                    else if (result.Activo)
                        resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.ExisteActivo;
                    else
                        resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.ExisteInactivo;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// Retorna la cantidad de usuarios por el rol pasado por parámetro, que están asociados a la empresa relacionada
        /// con el tipo y número de identificación. Devuelve el siguiente resultado:
        /// 0: Si no existe ningun usuario, 1: si ýa existen todos los usuarios permitidos para ese rol y se encuentran aprobados,
        /// 2. Si ya existen todos los usuarios permitidos para ese rol pero aun hay usuarios sin aprobar
        /// </summary>
        /// <param name="tipoDocumentoEmpresa"></param>
        /// <param name="documentoEmpresa"></param>
        /// <param name="idRolSeleccionado"></param>
        /// <returns></returns>
        public int validarCantidadUsuariosPorRol(string tipoDocumentoEmpresa, string documentoEmpresa, int idRolSeleccionado)
        {
            try
            {
                var resultado = 0;
                using (var context = new SG_SSTContext())
                {
                    var cantidadMaxUsuarios = context.Tbl_RolesSistema.Where(r => r.Pk_Id_RolSistema == idRolSeleccionado).Select(r => r.CantidadUsuariosPorRol).FirstOrDefault();
                    var cantUsuariosAprobados = (from u in context.Tbl_UsuarioSistema
                                                 join ur in context.Tbl_UsuariosPorRol on u.Pk_Id_UsuarioSistema equals ur.Fk_Id_UsuarioSistema
                                                 join ue in context.Tbl_UsuarioSistemaEmpresa on u.Pk_Id_UsuarioSistema equals ue.Fk_Id_UsuarioSistema
                                                 join e in context.Tbl_Empresa on ue.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                                 where e.Nit_Empresa.ToUpper().Equals(documentoEmpresa) && e.Tipo_Documento.ToUpper().Equals(tipoDocumentoEmpresa.ToUpper()) && ur.Fk_Id_RolSistema == idRolSeleccionado
                                                 select u).Count();

                    var cantUsuariosSinAprobar = (from ua in context.Tbl_UsuariosParaAprobar
                                                  join td in context.Tbl_TipoDocumentos on ua.TipoDocumentoEmpresa equals td.PK_IDTipo_Documento
                                                  where ua.NumeroDocumentoEmprsa.Equals(documentoEmpresa) && td.Sigla.ToUpper().Equals(tipoDocumentoEmpresa.ToUpper())
                                                  && ua.Fk_Id_RolSistema == idRolSeleccionado
                                                  select ua).Count();
                    //ya se superó la cantidad de usuarios y están aprobados
                    if (cantUsuariosAprobados >= cantidadMaxUsuarios)
                        resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaCantidadUsuariosPorRol.NoSePuedeCrearTodosAprobados;
                    //ya se superó la cantidad de usuarios y algunos se encuentran sin aprobar
                    else if ((cantUsuariosAprobados + cantUsuariosSinAprobar) >= cantidadMaxUsuarios)
                        resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaCantidadUsuariosPorRol.NoSePuedeCrearAlgunosSinAprobar;
                    else
                        resultado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaCantidadUsuariosPorRol.SePuedeCrear;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Inserta un registro controlando la cantidad de commits
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <param name="count"></param>
        /// <param name="commitCount"></param>
        /// <param name="recreateContext"></param>
        /// <returns></returns>
        private SG_SSTContext AdicionarUsuarioContexto(SG_SSTContext context, UsuarioSistema usuario, UsuarioSistemaEmpresa usuarioSxEmp, int count, int commitCount, bool recreateContext)
        {
            context.Set<UsuarioSistema>().Add(usuario);
            context.Set<UsuarioSistemaEmpresa>().Add(usuarioSxEmp);
            if (count % commitCount == 0)
            {
                context.SaveChanges();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new SG_SSTContext();
                    context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            return context;
        }

        /// <summary>
        /// Elimina los usuarios que han sido aprobados
        /// </summary>
        /// <param name="usuariosEliminar"></param>
        private void EliminarUsuariosPorAprovar(EDUsuarioSistema usuariosEliminar, out int codUsuarioPorAprobar)
        {
            codUsuarioPorAprobar = 0;
            try
            {
                using (var context = new SG_SSTContext())
                {
                    var usuario = context.Tbl_UsuariosParaAprobar.Where(ua => ua.TipoDocumentoEmpresa == usuariosEliminar.TipoDocumentoEmpresa
                    && ua.NumeroDocumentoEmprsa == usuariosEliminar.DocumentoEmpresa
                    && ua.TipoDocumentoUsuario == usuariosEliminar.TipoDocumento
                    && ua.NumeroDocumentoUsuario == usuariosEliminar.Documento).Select(ua => ua).FirstOrDefault();
                    if (usuario != null)
                    {
                        codUsuarioPorAprobar = usuario.Pk_id_UsuarioParaAprobar;
                        context.Tbl_UsuariosParaAprobar.Remove(usuario);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuariosEliminar"></param>
        private void EliminarUsuariosPorRechazar(SG_SSTContext context, EDUsuarioPorAprobar usuariosEliminar)
        {
            try
            {
                if (context != null)
                {
                    var usuario = context.Tbl_UsuariosParaAprobar.Where(ua => ua.Pk_id_UsuarioParaAprobar == usuariosEliminar.IdUsuarioPorAprobar).Select(ua => ua).FirstOrDefault();
                    if (usuario != null)
                        context.Tbl_UsuariosParaAprobar.Remove(usuario);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuariosEliminar"></param>
        private void EliminarUsuariosPorAprovar(EDUsuarioPorAprobar usuariosEliminar)
        {
            try
            {
                using (var context = new SG_SSTContext())
                {
                    var tipoIdentificacionEmpresa = (from e in context.Tbl_Empresa
                                                     join td in context.Tbl_TipoDocumentos on e.Tipo_Documento.ToUpper() equals td.Sigla.ToUpper()
                                                     where td.Sigla.Equals(usuariosEliminar.TipoDocumentoEmpresa)
                                                     select td.PK_IDTipo_Documento).FirstOrDefault();
                    var tipoIdentificacionAfiliado = (from e in context.Tbl_Empresa
                                                      join td in context.Tbl_TipoDocumentos on e.Tipo_Documento.ToUpper() equals td.Sigla.ToUpper()
                                                      where td.Sigla.Equals(usuariosEliminar.TipoDocumentoAfi)
                                                      select td.PK_IDTipo_Documento).FirstOrDefault();

                    var usuario = context.Tbl_UsuariosParaAprobar.Where(ua => ua.TipoDocumentoEmpresa == tipoIdentificacionEmpresa
                    && ua.NumeroDocumentoEmprsa == usuariosEliminar.NumDocumentoEmpresa
                    && ua.TipoDocumentoUsuario == tipoIdentificacionAfiliado
                    && ua.NumeroDocumentoUsuario == usuariosEliminar.NumDocumentoAfi).Select(ua => ua).FirstOrDefault();
                    if (usuario != null)
                    {
                        context.Tbl_UsuariosParaAprobar.Remove(usuario);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ActualizarRolesNuevaEmpresa(int id)
        {
            try
            {
                using (var context = new SG_SSTContext())
                {
                    context.Tbl_Rol.Where(r => r.Fk_Id_Empresa.Equals(null)).ToList().ForEach(r => r.Fk_Id_Empresa = id);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
