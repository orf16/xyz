using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.Interfaces.Usuarios;
using SG_SST.InterfazManager.Aplicacion;
using SG_SST.InterfazManager.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Aplicacion
{
    public class LNBateria
    {
        private static IBateria bat = IMBateria.IBateria();
        private static IUsuario us = IMUsuario.UsuarioSesion();
        public List<EDBateriaCuestionario> ConsultarFormulario(int Pagina, int IdBateria)
        {
            List<EDBateriaCuestionario> ListaFormulario = bat.ConsultarFormulario(Pagina, IdBateria);
            return ListaFormulario;
        }
        public int PaginaIntralaboralA(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int Pagina = bat.PaginaIntralaboralA(formdata, EDBateriaUsuario);
            return Pagina;
        }
        public int PaginaIntralaboralB(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int Pagina = bat.PaginaIntralaboralB(formdata, EDBateriaUsuario);
            return Pagina;
        }
        public int PaginaExtralaboral(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int Pagina = bat.PaginaExtralaboral(formdata, EDBateriaUsuario);
            return Pagina;
        }
        public int PaginaEstres(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int Pagina = bat.PaginaEstres(formdata, EDBateriaUsuario);
            return Pagina;
        }
        public bool GuardarEncuesta(List<EDBateriaResultado> ListaResultado, string key, int form)
        {
            bool ProbarGuardar = bat.GuardarEncuesta(ListaResultado, key, form);
            return ProbarGuardar;
        }
        public bool GuardarEncuestaExtra(List<EDBateriaResultado> ListaResultado, string key, int form)
        {
            bool ProbarGuardar = bat.GuardarEncuestaExtra(ListaResultado, key, form);
            return ProbarGuardar;
        }
        public bool TerminarEncuesta(int pkusuario)
        {
            bool ProbarGuardar = bat.TerminarEncuesta(pkusuario);
            return ProbarGuardar;

        }
        public bool TerminarEncuestaRechazo(int pkusuario)
        {
            bool ProbarGuardar = bat.TerminarEncuesta(pkusuario);
            return ProbarGuardar;

        }
        public List<EDBateria> ConsultarBaterias()
        {
            List<EDBateria> ListaBaterias = bat.ConsultarBaterias();
            return ListaBaterias;
        }
        public EDBateriaGestion CrearGestionNuevo(EDBateriaGestion EDBateriaGestionC)
        {
            EDBateriaGestion EDBateriaGestion = bat.CrearGestionNuevo(EDBateriaGestionC);
            return EDBateriaGestion;
        }
        public bool CrearConvocado(EDBateriaUsuario EDBateriaUsuario)
        {
            bool guardar =bat.CrearConvocado(EDBateriaUsuario);
            return guardar;
        }
        public bool ActualizarResultados(EDBateriaUsuario EDBateriaUsuario, int pkEmpresa)
        {
            bool guardar = bat.ActualizarResultados(EDBateriaUsuario, pkEmpresa);
            return guardar;
        }
        public bool CrearConvocadoPublico(EDBateriaUsuario EDBateriaUsuario)
        {
            bool guardar = bat.CrearConvocadoPublico(EDBateriaUsuario);
            return guardar;
        }
        public List<EDBateriaUsuario> CrearConvocadoMasivo(List<EDBateriaUsuario> ListaEDBateriaUsuario, int IdGestion, bool extra)
        {
            List<EDBateriaUsuario> guardarLista = bat.CrearConvocadoMasivo(ListaEDBateriaUsuario, IdGestion, extra);
            return guardarLista;
        }
        public List<EDBateriaUsuario> ConsultarUsuariosCorreos(int IdEmpresa, int IdConv, int IdGestion)
        {
            List<EDBateriaUsuario> ListaUsuarios = bat.ConsultarUsuariosCorreos(IdEmpresa, IdConv, IdGestion);


            return ListaUsuarios;
        }
        public EDBateriaGestion ConsultarGestion(int IdGestion, int IdEmpresa)
        {
            EDBateriaGestion EDBateriaGestion = bat.ConsultarGestion(IdGestion, IdEmpresa);
            return EDBateriaGestion;
        }
        public bool VerificarCorreoExistente(string email, string numeroId, int IdGestion)
        {
            bool validar = bat.VerificarCorreoExistente(email, numeroId, IdGestion);
            return validar;
        }
        public EDBateriaUsuario ConsultarConvocado(int IdConv, int IdEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocado(IdConv, IdEmpresa);
            return EDBateriaUsuario;
        }
        
        public EDBateriaUsuario ConsultarConvocadoExtra(int IdConv, int IdEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocadoExtra(IdConv, IdEmpresa);
            return EDBateriaUsuario;
        }
        public bool EliminarConvocado(int IdConv, int IdEmpresa)
        {
            bool validar = bat.EliminarConvocado(IdConv, IdEmpresa);
            return validar;
        }
        public List<EDRol> ConsultarRolesEmpresa(int IdEmpresa)
        {
            List<EDRol> ListaRoles = bat.ConsultarRolesEmpresa(IdEmpresa);
            return ListaRoles;
        }
        public List<EDCargo> ListaCargos()
        {
            List<EDCargo> ListaCargos = bat.ListaCargos();
            return ListaCargos;

        }
        public List<EDBateriaGestion> ConsultarListaGestion(int IdEmpresa)
        {
            List<EDBateriaGestion> Listagestion = bat.ConsultarListaGestion(IdEmpresa);
            return Listagestion;

        }
        public bool EditarEstadoGestion(int Idgestion, int Estado)
        {
            bool probar = bat.EditarEstadoGestion(Idgestion, Estado);
            return probar;
        }
        public bool EliminarGestion(int Idgestion, int IdEmpresa)
        {
            bool probar = bat.EliminarGestion(Idgestion, IdEmpresa);
            return probar;
        }
        public bool GestionConResultados(int Idgestion, int IdEmpresa)
        {
            bool probar = bat.GestionConResultados(Idgestion, IdEmpresa);
            return probar;
        }
        public EDBateriaUsuario ConsultarConvocadoKey(string key, int Form)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocadoKey(key, Form);
            return EDBateriaUsuario;
        }
        public EDBateriaUsuario ConsultarConvocadoKeyExtra(string key, int Form)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocadoKeyExtra(key, Form);
            return EDBateriaUsuario;
        }
        public EDBateriaGestion ConsultarGestionKey(string key, int Form)
        {
            EDBateriaGestion EDBateriaGestion = bat.ConsultarGestionKey(key, Form);
            return EDBateriaGestion;
        }
        public EDBateriaGestion ConsultarGestionKey1(EDBateriaUsuario EDBateriaUsuario)
        {
            EDBateriaGestion EDBateriaGestion = bat.ConsultarGestionKey1(EDBateriaUsuario);
            return EDBateriaGestion;
        }
        public EDBateriaInicial ConsultarInicialKey(int Fk_IdUsuario)
        {
            EDBateriaInicial EDBateriaInicial = bat.ConsultarInicialKey(Fk_IdUsuario);
            return EDBateriaInicial;
        }
        public bool[] GuardarInicial(EDBateriaInicial EDBateriaInicial)
        {
            bool[] ProbarGuardar = bat.GuardarInicial(EDBateriaInicial);
            return ProbarGuardar;
        }
        public bool[] ActualizarInicial(EDBateriaInicial EDBateriaInicial)
        {
            bool[] ProbarGuardar = bat.ActualizarInicial(EDBateriaInicial);
            return ProbarGuardar;
        }
        public bool ConsultarInicialbool(int Fk_IdUsuario)
        {
            bool probar = false;
            EDBateriaInicial EDBateriaInicial = bat.ConsultarInicialKey(Fk_IdUsuario);
            if (EDBateriaInicial!=null)
            {
                if (EDBateriaInicial.Pk_Id_BateriaInicial != 0)
                {
                    probar = true;
                }
            }
            return probar;
        }
        public int NumeroPagina(EDBateriaInicial EDBateriaInicial)
        {
            int pagina = bat.NumeroPagina(EDBateriaInicial);
            return pagina;
        }
        public bool EncuestaCompleta(EDBateriaUsuario EDBateriaUsuario)
        {
            bool ProbarCompletado = bat.EncuestaCompleta(EDBateriaUsuario);
            return ProbarCompletado;
        }
        public bool TieneExtra(EDBateriaUsuario EDBateriaUsuario)
        {
            bool ProbarCompletado = bat.TieneExtra(EDBateriaUsuario);
            return ProbarCompletado;
        }
        public void RecibirEditarDocumento(int pkusuario, string cedula)
        {
            bat.RecibirEditarDocumento(pkusuario, cedula);
        }
        public void EditarCheck9y10(int pkusuario, int tipo, string val)
        {
            bat.EditarCheck9y10(pkusuario, tipo, val);
        }
        public void EditarSegunCheck(int pkusuario, string Check9, string Check10)
        {

            bat.EditarSegunCheck(pkusuario, Check9, Check10);

        }
        public bool AceptarEncuesta(int pkusuario)
        {
            bool ProbarGuardar = bat.AceptarEncuesta(pkusuario);
            return ProbarGuardar;

        }

        public string PlantillaCorreo(string nombre)
        {
            string Plantilla = bat.PlantillaCorreo(nombre);
            return Plantilla;
        }

        public void EnviarCorreo(string plantilla, List<EDBateriaUsuario> ListaUsuario, string link)
        {
            string[] Lista = EnviarCorreoParaNotificacion();
            List<string> ListaCorreosEnviar = new List<string>();
            foreach (var item in ListaUsuario)
            {
                ListaCorreosEnviar.Add(item.CorreoElectronico);
            }
            int puerto = 0;
            if (int.TryParse(Lista[4], out puerto))
            {
                foreach (var item in ListaUsuario)
                {
                    if (item.EstadoEnvio != 1)
                    {
                        string links = link;
                        links = links.Replace("FORMDATA", item.TokenPrivado);
                        string Plantilla = plantilla;
                        Plantilla = Plantilla.Replace("[[LinkHttpSitio]]", links);
                        Plantilla = Plantilla.Replace("[[RutaHttpSitio]]", links);

                        string correoRemitente = Lista[3];
                        string remitente = "Administrador ALISSTA";
                        bool EnableSecured = true;
                        string Password = Lista[6];
                        int Puerto = puerto;
                        string StmpServidor = Lista[1];
                        string asunto = "Invitación participación de cuestionario ALISSTA";
                        string correoDestino = item.CorreoElectronico;

                        try
                        {

                            MailMessage mail = new MailMessage();
                            using (SmtpClient SmtpServer = new SmtpClient(StmpServidor))
                            {
                                mail.From = new MailAddress(correoRemitente, remitente, Encoding.UTF8);
                                mail.Subject = asunto;
                                mail.Body = Plantilla;
                                mail.IsBodyHtml = true;
                                mail.To.Add(correoDestino);
                                SmtpServer.Port = Puerto;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(correoRemitente, Password);
                                SmtpServer.EnableSsl = EnableSecured;
                                SmtpServer.Send(mail);
                                //Si envio Correo guardar estado
                                bat.EditarEstadoCorreo(item.Pk_Id_BateriaUsuario, Plantilla);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        public void ReenviarCorreo(EDBateriaUsuario EDBateriaUsuario)
        {
            string[] Lista = EnviarCorreoParaNotificacion();
            int puerto = 0;
            if (int.TryParse(Lista[4], out puerto))
            {
                    if (EDBateriaUsuario.EstadoEnvio != 0)
                    {
                        string Plantilla = EDBateriaUsuario.MailBody;
                        string correoRemitente = Lista[3];
                        string remitente = "Administrador ALISSTA";
                        bool EnableSecured = true;
                        string Password = Lista[6];
                        int Puerto = puerto;
                        string StmpServidor = Lista[1];
                        string asunto = "Invitación participación de cuestionario ALISSTA";
                        string correoDestino = EDBateriaUsuario.CorreoElectronico;

                        try
                        {

                            MailMessage mail = new MailMessage();
                            using (SmtpClient SmtpServer = new SmtpClient(StmpServidor))
                            {
                                mail.From = new MailAddress(correoRemitente, remitente, Encoding.UTF8);
                                mail.Subject = asunto;
                                mail.Body = Plantilla;
                                mail.IsBodyHtml = true;
                                mail.To.Add(correoDestino);
                                SmtpServer.Port = Puerto;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(correoRemitente, Password);
                                SmtpServer.EnableSsl = EnableSecured;
                                SmtpServer.Send(mail);
                                //Si envio Correo guardar estado
                                bat.EditarEstadoCorreo(EDBateriaUsuario.Pk_Id_BateriaUsuario, Plantilla);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                
            }
        }
        private string[] EnviarCorreoParaNotificacion()
        {
            string[] Lista = new string[7] {"","","","","","","" };
            var idParametros = new List<int>() {
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp
                };
            var parametros = us.ObtenerParametrosSistema(idParametros);
            Lista[0] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio).Select(p => p).FirstOrDefault().Valor;
            Lista[1] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp).Select(p => p).FirstOrDefault().Valor;
            Lista[2] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion).Select(p => p).FirstOrDefault().Valor;
            Lista[3] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente).Select(p => p).FirstOrDefault().Valor;
            Lista[4] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp).Select(p => p).FirstOrDefault().Valor;
            Lista[5] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp).Select(p => p).FirstOrDefault().Valor;
            Lista[6] = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp).Select(p => p).FirstOrDefault().Valor;

            return Lista;
        }

        public void CambiarEstadosInactivos()
        {
            bat.CambiarEstadosInactivos();
        }


        public List<EDRelacionesLaborales> ConsultarConvocadosRol(int rol)
        {
            List<EDRelacionesLaborales> ListaRelLab = bat.ConsultarConvocadosRol(rol);
            return ListaRelLab;
        }

        public List<EDBateriaGestion> ConsultarListaGestionFiltros(int IdEmpresa, string Fantes, string Fdespues, int Tipo)
        {
            List<EDBateriaGestion> Listagestion = bat.ConsultarListaGestionFiltros(IdEmpresa, Fantes, Fdespues, Tipo);
            return Listagestion;

        }

        public bool EmpresaCoincide(string nitempresa, int fkidempresa)
        {
            bool probar = bat.EmpresaCoincide(nitempresa, fkidempresa);
            return probar;
        }
        public EDBateriaUsuario ConsultarConvocadoCedula(string cedula, int pkIdgestion)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocadoCedula(cedula, pkIdgestion);
            return EDBateriaUsuario;
        }
        public EDBateriaUsuario ConsultarConvocadoId(int PkIdUsuario, int FkEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocadoId(PkIdUsuario, FkEmpresa);
            return EDBateriaUsuario;
        }
        public EDBateriaUsuario ConsultarConvocadoId1(int PkIdUsuario, int FkEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = bat.ConsultarConvocadoId1(PkIdUsuario, FkEmpresa);
            return EDBateriaUsuario;
        }
        public List<EDBateriaDimension> ListaDimensiones(int Iddominio, int bateria)
        {
            List<EDBateriaDimension> ListaDimensiones = bat.ListaDimensiones(Iddominio, bateria);
            foreach (var item in ListaDimensiones)
            {
                item.Nombre = item.Nombre.Replace("?", "-");
            }
            return ListaDimensiones;
        }
        public List<EDBateriaResultado> ListaResultados(int fkUsuario)
        {
            List<EDBateriaResultado> ListaResultados = bat.ListaResultados(fkUsuario);
            return ListaResultados;
        }
    }
}
