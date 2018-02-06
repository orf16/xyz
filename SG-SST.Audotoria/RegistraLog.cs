using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Audotoria
{
    public class RegistraLog : IRegistraLog
    {
        private static ILog log = null;

        /// <summary>
        /// Escribe un log cuando se entra al método cuyo nombre
        /// se pasa por parámetro.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="nombreMetodo"></param>
        public void EntrarMetodo(Type tipo, string nombreMetodo)
        {
            log = LogManager.GetLogger(tipo);
            if (log.IsInfoEnabled)
                log.Info(string.Format(CultureInfo.InvariantCulture, "Entrando al método {0}", nombreMetodo));
        }

        /// <summary>
        /// Escribe un log cuando se sale del método cuyo nombre
        /// se pasa por parámetro.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="nombreMetodo"></param>
        public void SalirMetodo(Type tipo, string nombreMetodo)
        {
            log = LogManager.GetLogger(tipo);
            if (log.IsInfoEnabled)
                log.Info(string.Format(CultureInfo.InvariantCulture, "Saliendo del método {0}", nombreMetodo));
        }

        /// <summary>
        /// Registra el error pasado por parámetro
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public void RegistrarError(Type tipo, string mensaje, Exception ex)
        {
            log = LogManager.GetLogger(tipo);
            if (log.IsErrorEnabled)
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", mensaje + ": " + ex.Message), ex);
        }

        /// <summary>
        /// Escribe un log cuando existe un mensaje de alerta en el sistema.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="mensaje"></param>
        public void RegistrarMsgAlerta(Type tipo, string mensaje)
        {
            log = LogManager.GetLogger(tipo);
            if (log.IsWarnEnabled)
                log.Warn(string.Format(CultureInfo.InvariantCulture, "{0}", mensaje));
        }

        /// <summary>
        /// Escribe un log cuando se especifica un mensaje informativo en el sistema.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="mensaje"></param>
        public void RegistrarMsgInformacion(Type tipo, string mensaje)
        {
            log = LogManager.GetLogger(tipo);
            if (log.IsInfoEnabled)
                log.Info(string.Format(CultureInfo.InvariantCulture, "{0}", mensaje));
        }
    }
}
