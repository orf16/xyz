
namespace SG_SST.Interfaces.Aplicacion
{
    using SG_SST.EntidadesDominio.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IAdquicisionBienes
    {

        /// <summary>
        /// Repositorio que me guarda un Manual de Adquision de bienes o contratacines
        /// </summary>
        /// <param name="documento">Manual guardado objeto de tipo EDManualAdquisicion </param>
        /// <returns>retorna verdadero si fue posible guardarlo en la base de datos o false del caso contrario</returns>
        bool GuardarManualAdquisiones(EDManualAdquisicion documento);

        /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo EDManualAdquisicion
        /// </summary>
        /// <param name="idEmpresa">id o pk de la empresa</param>
        /// <returns>lista de objetos de tipo EDManualAdquisicion</returns>
        List<EDManualAdquisicion> ObtenerManualAdquisiones(int idEmpresa);

        /// <summary>
        /// Repositorio que obtiene el manual de adquisición de bienes o contrataciones
        /// </summary>
        /// <param name="idManualAdq">id o pk del documento</param>
        /// <returns>el documento de diagnostico</returns>
        string ObtenerManualAdquisionBienes(int idManualAdq);

        /// <summary>
        /// Repositorio que elimina un manual de adquisición de bienes o contrataciones
        /// </summary>
        /// <param name="idManualAdq">id o pk del documento a eliminar</param>
        /// <returns>retorna verdadero si fue posible eliminarlo en la base de datos o false del caso contrario</returns>
        bool EliminarManualAdqBienes(int idManualAdq);

    }
}
