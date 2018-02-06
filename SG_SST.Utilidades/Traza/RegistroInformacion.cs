using System;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Utilidades.Traza
{
    /// <summary>
    /// Esta clase permite almacenar registro de acciones que pasan en el sitio, Errores, Advertencias, Información en general.
    /// Este sirve para conocer lo que pasa en el servidor en pruebas y producción
    /// La información se guarda mediante log4net, revisar web.config para ver detalle
    /// </summary>
    public class RegistroInformacion
    {
        private static ILog logger = LogManager.GetLogger("Logger");

        public static void EnviarError<T>(string mensaje)
        {
            log4net.Config.XmlConfigurator.Configure();
            logger.Error(typeof(T).Name + " - " + mensaje);
        }
    }
}
