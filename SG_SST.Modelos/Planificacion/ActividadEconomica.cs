using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using System.Data.Entity.SqlServer;
using System;


//tabla usada en el módulo requisitos legales - datos POSIPEDIA

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Actividad_Economica")]
    public class ActividadEconomica
    {
        [Key]
        public int PK_Actividad_Economica { get; set; }
        public string Ente { get; set; }

    }
}

