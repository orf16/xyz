using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.EntidadesDominio.Empleado;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_VinculacionLaboral")]
    public class VinculacionLaboral
    {
        [Key]
        public int PK_VinculacionLaboral { get; set; }
        public string Descripcion_VinculacionLaboral { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDVinculacionLaboral ObtenerED()
        {
            return new EDVinculacionLaboral
            {
                 PK_VinculacionLaboral = PK_VinculacionLaboral,
                 Descripcion_VinculacionLaboral = Descripcion_VinculacionLaboral
            };
        }

    }
}
