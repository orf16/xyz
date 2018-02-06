using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDEmpleadoTercero
    {

        [Display(Name = "Id empleado")]
        public int ID_Empleado { get; set; }

        [Display(Name = "ID Tipo de documento")]
        public int FK_Tipo_Documento_Empl { get; set; }

        [Display(Name = "Tipo de documento")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Número de documento")]
        public string Numero_Documento_Empl { get; set; }
        [Display(Name = "Primer nombre")]
        public string Nombre1 { get; set; }
        [Display(Name = "Segundo nombre")]
        public string Nombre2 { get; set; }
        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public string strFechaNacimiento { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Ocupación")]
        public string Ocupacion_Empl { get; set; }

        [Display(Name = "Cargo")]
        public string Cargo_Empl { get; set; }

        [Display(Name = "E-mail")]
        public string Email_Empl { get; set; }

        [Display(Name = "NIT de la empresa")]
        public string PK_Nit_Empresa { get; set; }

        [Display(Name = "ID Tipo de tercero")]
        public int FKRelacionLaboralTercero { get; set; }
        [Display(Name = "Tipo de tercero")]
        public string RelacionesLaboralesTercero { get; set; }
        [Display(Name = "Razón social de la empresa")]
        public string RazonSocial { get; set; }

        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        public int FK_id_departamento { get; set; }

        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        /// <summary>
        /// Llave foránea para el municipio.
        /// </summary>
        public int FK_id_municipio { get; set; }

        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        public int FK_id_zona { get; set; }

        [Display(Name = "Zona")]
        public virtual EDZonaLugar Zona { get; set; }

        [MaxLength(15)]
        public string Ocupacion_habitual { get; set; }

        public DateTime Fecha_ingreso_empresa { get; set; }

        public int FK_id_jornada_habitual { get; set; }

        [Display(Name = "Jornada habitual")]
        public virtual EDTipoJornada Jornada_trabajo_habitual { get; set; }

        public int PageCount { get; set; }


    }
}
