using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.EntidadesDominio.Planificacion;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_ZonaLugar")]
    public class ZonaLugar
    {
        [Key]
        public int PK_ZonaLugar { get; set; }
        public string Descripcion_ZonaLugar { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDZonaLugar ObtenerED()
        {
            return new EDZonaLugar
            {
                Descripcion_ZonaLugar = Descripcion_ZonaLugar,
                PK_ZonaLugar = PK_ZonaLugar
            };
        }
    }

}
