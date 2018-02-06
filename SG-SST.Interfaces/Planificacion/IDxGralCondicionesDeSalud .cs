
namespace SG_SST.Interfaces.Planificacion
{
    using SG_SST.EntidadesDominio.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDxGralCondicionesDeSalud
    {
        /// <summary>
        /// Repositorio que me guarda un documento de diagnostico
        /// </summary>
        /// <param name="documento">documento a guardad objeto de tipo EDDocDxSalud </param>
        /// <returns>retorna verdadero si fue posible guardarlo en la base de datos o false del caso contrario</returns>
        bool GuardarDocDXSalud(EDDocDxSalud documento);


        /// <summary>
        /// Repositorio que me retorna y una lista de objetos de tipo EDDocDxSalud
        /// </summary>
        /// <param name="idEmpresa">id o pk de la empresa</param>
        /// <returns>lista de objetos de tipo EDDocDxSalud</returns>
        List<EDDocDxSalud> ObtenerDocsDXSalud(int idEmpresa);

        /// <summary>
        /// Repositorio que elimina un documento de un diagnostico
        /// </summary>
        /// <param name="idDocDx">id o pk del documento a eliminar</param>
        /// <returns>retorna verdadero si fue posible eliminarlo en la base de datos o false del caso contrario</returns>
        bool EliminarDocDxSalud(int idDocDx);

        /// <summary>
        /// Repositorio que obtiene el documento de diagnostico general de salud
        /// </summary>
        /// <param name="idDocDx">id o pk del documento</param>
        /// <returns>el documento de diagnostico</returns>
        EDDocDxSalud ObtenerDocDXSalud(int idDocDx);

        /// <summary>
        /// Repositorio que guarda el diagnostico general de salud
        /// </summary>
        /// <param name="Diagnostico">entidad de dominio con la informacion del diagnostico</param>
        /// <returns>me retorna la entidad de dominio con id o pk del diagnostico si fue exitoso el guardado
        /// de lo contrario retorna la entidad de dominio sin id o pk </returns>
        EDDxSalud GuardarDxSalud(EDDxSalud Diagnostico);

        /// <summary>
        /// Repositorio que me retorna el historico de los diagnosticos por año de las sedes
        /// </summary>
        /// <param name="idEmpresa">pk o id de la empresa</param>
        /// <returns>Lista de diagnosticos </returns>
        List<EDDxSalud> ObtenerDiagnosticosPorsedeAnio(int idEmpresa);

        /// <summary>
        /// Repostorio que me retorna todo los diagnosticos de la sede por año
        /// </summary>
        /// <param name="idEmpresa">id o pk de la  empresa</param>
        /// <param name="anio">año en la que fue hecho el dianostivo</param>
        /// <returns>lista de diganosticos</returns>
        List<EDDxSalud> ObtenerHistoricoDxDeSedePorAnio(int idDxSalud);

        /// <summary>
        /// Repositorio que me retorna todos los diagnosticos de las sedes a la fecha
        /// </summary>
        /// <returns>lista de diagnosticos</returns>
        List<EDDxSalud> ObtenerReporte(int idEmpresa);

        bool EliminarDxSalud(int idDx);

        List<EDDxSalud> BuscarDiagnosticosPorsedeAnio(int idEmpresa, int strZonaLugar);



        bool InsertarCargueMasivoDx(List<EDDxSalud> diagnosticos);
    }
}
