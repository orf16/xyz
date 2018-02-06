using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empleado;
using SG_SST.Models.Planificacion;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_EmpleadoTercero")]
    public class EmpleadoTercero
    {
        [Key]
        public int ID_Empleado { get; set; }


        [ForeignKey("TipoDocumento")]
        [Display(Name = "Tipo Documento")]

        public int FK_Tipo_Documento_Empl { get; set; }

        [ForeignKey("PK_IDTipo_Documento")]
        public virtual TipoDocumento TipoDocumento { get; set; }


        [Display(Name = "Numero de Documento")]
        public string Numero_Documento_Empl { get; set; }


        [Display(Name = "Nombre1")]
        public string Nombre1 { get; set; }


        [Display(Name = "Nombre2")]
        public string Nombre2 { get; set; }


        [Display(Name = "Apellido1")]
        public string Apellido1 { get; set; }


        [Display(Name = "Apellido2")]
        public string Apellido2 { get; set; }

        [Display(Name = "FechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public string Email { get; set; }

        [Display(Name = "Ocupación")]
        public string Ocupacion_Empl { get; set; }


        [Display(Name = "Cargo")]
        public string Cargo_Empl { get; set; }


        [Display(Name = "Email")]
        public string Email_Empl { get; set; }

        /// <summary>
        /// Sexo: M/F
        /// </summary>
        [MaxLength(1)]
        [Display(Name = "Genero")]
        public string Genero { get; set; }

        /// <summary>
        /// Dirección de residencia.
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        /// <summary>
        /// Teléfono de contacto.
        /// </summary>
        [MaxLength(15)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        /// <summary>
        /// Llave foránea para el departamento.
        /// </summary>
        [ForeignKey("Departamento_residencia")]
        public int FK_id_departamento { get; set; }

        [ForeignKey("FK_id_departamento")]
        [Display(Name = "Departamento")]
        public virtual Departamento Departamento_residencia { get; set; }

        /// <summary>
        /// Llave foránea para el municipio.
        /// </summary>
        [ForeignKey("Municipio_residencia")]
        public int FK_id_municipio { get; set; }

        [ForeignKey("FK_id_departamento")]
        [Display(Name = "Municipio")]
        public virtual Municipio Municipio_residencia { get; set; }

        public int FK_id_zona { get; set; }

        [ForeignKey("FK_id_zona")]
        [Display(Name = "Zona")]
        public virtual ZonaLugar Zona_residencia { get; set; }

        [MaxLength(15)]
        public string Ocupacion_habitual { get; set; }

        public DateTime Fecha_ingreso_empresa { get; set; }

        public int FK_id_jornada_habitual { get; set; }

        [ForeignKey("FK_id_jornada_habitual")]
        [Display(Name = "Jornada habitual")]
        public virtual TipoJornada Jornada_trabajo_habitual { get; set; }

        /// <summary>
        /// Obtiene y establece una clave foranea a empresa
        /// </summary>
        [ForeignKey("Tbl_EmpresaTercero")]
        public string FK_EmpresaTercero { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_Nit_Empresa")]
        public virtual EmpresaTercero Tbl_EmpresaTercero { get; set; }

        [ForeignKey("Tbl_Empresa")]
        public int FK_Empresa { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Tbl_Empresa { get; set; }

        [ForeignKey("Tbl_RelacionesLaboralesTercero")]
        public int FKRelacionLaboralTercero { get; set; }

        [ForeignKey("Pk_Id_RelacionesLaboralesTercero")]
        public virtual RelacionesLaboralesTercero Tbl_RelacionesLaboralesTercero { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDEmpleadoTercero ObtenerED()
        {
            return new EDEmpleadoTercero
            {
                Apellido1 = Apellido1,
                Apellido2 = Apellido2,
                Cargo_Empl = Cargo_Empl,
                Departamento =  Departamento_residencia.ObtenerED().Nombre_Departamento,
                Direccion = Direccion,
                Email = Email,
                Email_Empl = Email_Empl,
                FechaNacimiento = FechaNacimiento,
                Fecha_ingreso_empresa = Fecha_ingreso_empresa,
                FKRelacionLaboralTercero = FKRelacionLaboralTercero,
                FK_id_departamento = FK_id_departamento,
                FK_id_jornada_habitual = FK_id_jornada_habitual,
                FK_id_municipio = FK_id_municipio,
                FK_id_zona = FK_id_zona,
                FK_Tipo_Documento_Empl = FK_Tipo_Documento_Empl,
                Genero = Genero,
                ID_Empleado = ID_Empleado,
                Jornada_trabajo_habitual = Jornada_trabajo_habitual.ObtenerED(),
                Municipio = Municipio_residencia.ObtenerED().NombreMunicipio,
                Nombre1 = Nombre1,
                Nombre2 = Nombre2,
                Numero_Documento_Empl = Numero_Documento_Empl,
                Ocupacion_Empl = Ocupacion_Empl,
                Ocupacion_habitual = Ocupacion_habitual,
                PK_Nit_Empresa = Tbl_EmpresaTercero.PK_Nit_Empresa,
                RazonSocial = Tbl_EmpresaTercero.Razon_Social,
                Telefono = Telefono,
                TipoDocumento = TipoDocumento.Descripcion,
                Zona = Zona_residencia.ObtenerED()
            };
        }
    }
}
