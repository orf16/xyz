using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDDepartamento
    {
        public int Pk_Id_Departamento { get; set; }
        public string Nombre_Departamento { get; set; }
        public string Codigo_Departamento { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDDepartamento ObtenerED()
        {
            return new EDDepartamento
            {
                Pk_Id_Departamento = Pk_Id_Departamento,
                Codigo_Departamento = Codigo_Departamento,
                Nombre_Departamento = Nombre_Departamento
            };
        }
    }
}
