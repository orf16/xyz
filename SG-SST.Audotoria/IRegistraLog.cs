using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Audotoria
{
    public interface IRegistraLog
    {
        /// <summary>
        /// Escribe un log cuando se entra al método cuyo nombre
        /// se pasa por parámetro.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="nombreMetodo"></param>
        void EntrarMetodo(Type tipo, string nombreMetodo);

        /// <summary>
        /// Escribe un log cuando se sale del método cuyo nombre
        /// se pasa por parámetro.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="nombreMetodo"></param>
        void SalirMetodo(Type tipo, string nombreMetodo);

        /// <summary>
        /// Escribe un log cuando ocurre un error en algun punto
        /// de la clase pasada por parámetro.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="mensaje"></param>
        /// <param name="ex"></param>
        void RegistrarError(Type tipo, string mensaje, Exception ex);

        /// <summary>
        /// Escribe un log cuando existe un mensaje de alerta en el sistema.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="mensaje"></param>
        void RegistrarMsgAlerta(Type tipo, string mensaje);

        /// <summary>
        /// Escribe un log cuando se especifica un mensaje informativo en el sistema,
        /// </summary>
        /// <param name="mensaje">The message.</param>
        void RegistrarMsgInformacion(Type tipo, string mensaje);
    }
}
