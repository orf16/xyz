using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using System;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_BateriaUsuario")]
    public class BateriaUsuario
    {
        [Key]
        public int Pk_Id_BateriaUsuario { get; set; }

        [MaxLength(3000)]
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string NumeroIdentificacion { get; set; }

        [MaxLength(500)]
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string TipoDocumento { get; set; }

        [MaxLength(1000)]
        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required()]
        public int Id_Empresa { get; set; }

        [Required()]
        public int TipoConv { get; set; }

        [Required()]
        public int EstadoEnvio { get; set; }
        [Required()]
        public int NumeroIntentos { get; set; }

        [DisplayName("Registro Operacion")]
        public string RegistroOperacion { get; set; }
        [DisplayName("Registro Operacion Extra")]
        public string RegistroOperacionExtra { get; set; }

        [DisplayName("MailBody")]
        public string MailBody { get; set; }

        [DisplayName("CheckPag9")]
        public string CheckPag9 { get; set; }

        [DisplayName("CheckPag10")]
        public string CheckPag10 { get; set; }

        [DisplayName("DocumentoDigitado")]
        public string DocumentoDigitado { get; set; }

        [DisplayName("ConfirmacionParticipacion")]
        public string ConfirmacionParticipacion { get; set; }

        public Nullable<System.DateTime> FechaEnvio { get; set; }

        public Nullable<System.DateTime> FechaRespuesta { get; set; }


        [MaxLength(3000)]
        [DisplayName("Edad")]
        public string Edad { get; set; }
        [MaxLength(3000)]
        [DisplayName("Nombre del Evaluador")]
        public string NombreEvaluador { get; set; }
        [MaxLength(3000)]
        [DisplayName("Número de identificación del Evaluador")]
        public string IdEvaluador { get; set; }
        [MaxLength(3000)]
        [DisplayName("Profesión del Evaluador")]
        public string Profesion { get; set; }
        [MaxLength(3000)]
        [DisplayName("Postgrado del Evaluador")]
        public string Postgrado { get; set; }
        [MaxLength(3000)]
        [DisplayName("No Tarjeta Profesional del Evaluador")]
        public string TarjetaProfesional { get; set; }
        [MaxLength(3000)]
        [DisplayName("No Licencia en salud ocupacional")]
        public string Licencia { get; set; }

        [MaxLength(4000)]
        [DisplayName("Observaciones")]
        public string Observaciones { get; set; }
        [MaxLength(4000)]
        [DisplayName("Recomendaciones")]
        public string Recomendaciones { get; set; }

        [DisplayName("Fecha de Expedición de la Licencia en Salud Ocupacional")]
        public Nullable<System.DateTime> FechaExpedicion { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string TokenPrivado { get; set; }

        [DisplayName("Fecha de Informe")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaInforme { get; set; }

        [ForeignKey("BateriaGestion")]
        public int Fk_Id_BateriaGestion { get; set; }
        [ForeignKey("Pk_Id_BateriaGestion")]
        public virtual BateriaGestion BateriaGestion { get; set; }

        public ICollection<BateriaResultado> BateriaResultados { get; set; }
    }
}
