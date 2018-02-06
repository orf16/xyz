using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IPeligro
    {
        /// <summary>
        /// Definicion del Metodo que me graba un peligro y me retorna verdadero si fue posible grabar
        /// el peligro o falso sino fue posible
        /// </summary>
        /// <param name="peligro">Peligro a grabarle a la sede </param>
        /// <returns>retorna si fue exitosa o no el guardado del peligro</returns>
        bool GuardarPeligro(EDPeligro peligro);

        /// <summary>
        /// Definicion del metodo que retornar todos los lugares donde se presentan los peligros 
        /// filtrados por empresa
        /// </summary>
        /// <param name="id_Empresa">pk o id de la empresa</param>
        /// <returns></returns>
        List<string> ObtenerLugaresPeligros(int id_Empresa);
        

        List<EDPeligro> ObtenernerZonasPorEmpresa(string nit_Empresa);

        List<EDClasificacionDePeligro> ObtenerClasificaciónPorSede(int IdSede);
        List<EDPeligro> ObtenerPeligrosPorClaseyEmpresa(int IdClase, int IdEmpresa);
        string ObtenerClasificación(int IdClasificacion);
        List<EDClasificacionDePeligro> ObtenerClasificaciónPorTipo(int IdTipoPeligro);
        EDPeligro ObtenerPeligrosPorId(int IdPeligro);
    }
}
