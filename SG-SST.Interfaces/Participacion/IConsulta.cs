using SG_SST.EntidadesDominio.Participacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Participacion
{
   public interface IConsulta
    {
         EDConsultaSST GrabarConsultaSST(EDConsultaSST consulta);

         /// <summary>
         /// Repositorio que me retorna una lista de objetos de tipo ConsultaSST
         /// </summary>
         /// <param name="idEmpresa">id o pk de la empresa</param>
         /// <returns>lista de objetos de tipo ConsultaSST</returns>
         List<EDConsultaSST> ObtenerConsultasSST(int idEmpresa);

         /// <summary>
         /// Repositorio que me retorna un objeto de tipo ConsultaSST
         /// </summary>
         /// <param name="idEmpresa">id de la ConsultaSST</param>
         /// <returns>objeto de tipo ConsultaSST</returns>
         EDConsultaSST ObtenerUnaConsultaSST(int idConsulta);

         /// <summary>
         /// Repositorio que me permite editar una ConsultaSST
         /// </summary>
         /// <param name="idEmpresa">idConsulta</param>
         /// <returns>booleano true si fue exitosa la edicion de la ConsultaSST, de lo contrario false</returns>
         bool EditarGestionConsulta(EDConsultaTrazabilidad NuevoAdmonCTZB);
         /// <summary>
         /// Repositorio que me permite eliminar una ConsultaSST
         /// </summary>
         /// <param name="idEmpresa">id, ruta</param>
         /// <returns>booleano true si fue exitoso eliminar una ConsultaSST, de lo contrario false</returns>
         bool EliminarEvidenciaConsulta(int id, string ruta, int dato);

         /// <summary>
         /// Repositorio que me retorna una lista de objetos de tipo ConsultaSST por los parametros ingresados
         /// </summary>
         /// <param name="idEmpresa">tipoConsult, Fecha_ini, Fecha_Fin, idEmpresa</param>
         /// <returns>booleano true si fue exitoso consultar las ConsultaSST, de lo contrario false</returns>
         List<EDConsultaSST> ConsultarConsultasSST(EDConsultarConsultasSST consultar);
        
    }
}
