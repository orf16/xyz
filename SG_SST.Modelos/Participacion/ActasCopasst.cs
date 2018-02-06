using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_ActasCopasst")]
    public class ActasCopasst
    {
        [Key]
        public int PK_Id_Acta { get; set; }
        [Required]
        public int Consecutivo_Acta { get; set; }
        public DateTime Fecha { get; set; }
        [StringLength(300)]
        public string TemaReunion { get; set; }
        [StringLength(1000)]
        public string Conclusiones { get; set; }
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("Sede")]
        public int Fk_Id_Sede { get; set; }
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }
        [StringLength(150)]
        public string NombreEmpresa { get; set; }
        [StringLength(60)]
        public string NombreUsuario { get; set; }
        [StringLength(200)]
        public string NombreArchivo { get; set; }

        public List<MiembrosActaCopasst> MiembrosActaCopasst { get; set; }
        public List<AuditoriaActaCopasst> AuditoriaActaCopasst { get; set; }
        public List<Participantes> Participantes { get; set; }
        public List<AccionesActaCopasst> AccionesActaCopasst { get; set; }
        public List<TemasActaCopasst> TemasActaCopasst { get; set; }
    }
} 
