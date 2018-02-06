using SG_SST.Audotoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Utilidades
{
    public class EnvioCorreos
    {
        /// <summary>
        /// Envía el correo al destinatario pasado por parámetro.
        /// En el método se pasa por parámetro la plantilla html, el remitente, el servidor stmp, el asunto
        /// </summary>
        /// <param name="Plantilla"></param>
        /// <param name="correoRemitente"></param>
        /// <param name="remitente"></param>
        /// <param name="EnableSecured"></param>
        /// <param name="Password"></param>
        /// <param name="Puerto"></param>
        /// <param name="StmpServidor"></param>
        /// <param name="asunto"></param>
        /// <param name="correoDestino"></param>
        /// <returns></returns>
        public static bool EnviarCorreo(string Plantilla, string correoRemitente, string remitente, bool EnableSecured, string Password, int Puerto, string StmpServidor, string asunto, string correoDestino)
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                // var bll = new GeneralBLL(conexionString);
                using (SmtpClient SmtpServer = new SmtpClient(StmpServidor))
                {
                    //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                    mail.From = new MailAddress(correoRemitente, remitente, Encoding.UTF8);
                    //Aquí ponemos el asunto del correo
                    mail.Subject = asunto;
                    //Aquí ponemos el mensaje que incluirá el correo
                    mail.Body = Plantilla;
                    mail.IsBodyHtml = true;
                    //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                    mail.To.Add(correoDestino);
                    //Configuracion del SMTP
                    SmtpServer.Port = Puerto; //Puerto que utiliza Gmail para sus servicios
                                              //Especificamos las credenciales con las que enviaremos el mail
                    SmtpServer.Credentials = new System.Net.NetworkCredential(correoRemitente, Password);
                    SmtpServer.EnableSsl = EnableSecured;
                    SmtpServer.Send(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                RegistraLog registraLog = new RegistraLog();
                registraLog.RegistrarError(typeof(EnvioCorreos), string.Format("Error en el EnviarCorreo {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return false;
            }
        }
    }
}
