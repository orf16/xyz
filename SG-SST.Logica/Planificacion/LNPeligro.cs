using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Planificacion
{
    public class LNPeligro
    {
        private static IPeligro peligro = IMPeligro.Peligro();

        public bool GuardarPeligro(EDPeligro edpeligro) 
        {
            return peligro.GuardarPeligro(edpeligro);
        }

        public List<string> ObtenerLugaresPeligros(int id_Empresa) 
        {
            return peligro.ObtenerLugaresPeligros(id_Empresa);
        }

        public List<EDClasificacionDePeligro> ObtenerClasificaciónPorSede(int IdSede)
        {
            List<EDClasificacionDePeligro> Lista_EDPeligro = peligro.ObtenerClasificaciónPorSede(IdSede);
            return Lista_EDPeligro;
        }
        public List<EDPeligro> ObtenerPeligrosPorClaseyEmpresa(int IdClase, int IdEmpresa)
        {
            List<EDPeligro> Lista_EDPeligro = peligro.ObtenerPeligrosPorClaseyEmpresa(IdClase, IdEmpresa);
            return Lista_EDPeligro;
        }
        public string ObtenerClasificación(int IdClasificacion)
        {
            string ClasPeligro = peligro.ObtenerClasificación(IdClasificacion);
            return ClasPeligro;
        }
        public List<EDClasificacionDePeligro> ObtenerClasificaciónPorTipo(int IdTipoPeligro)
        {
            List<EDClasificacionDePeligro> Lista_EDPeligro = peligro.ObtenerClasificaciónPorTipo(IdTipoPeligro);
            return Lista_EDPeligro;
        }
        public EDPeligro ObtenerPeligrosPorId(int IdPeligro)
        {
            EDPeligro Lista_EDPeligro = peligro.ObtenerPeligrosPorId(IdPeligro);
            return Lista_EDPeligro;
        }
    }
}
