// <copyright file="Empresa.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Andres Correa.</author>
// <date>08/01/2017</date>
// <summary>Modelo de datos de la tabla empresa.</summary>

namespace SG_SST.Models.Empresas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Organizacion;
    using SG_SST.Models.Aplicacion;

    [Table("Tbl_Empresa")]
    public class Empresa
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de la tabla empresa.
        /// </summary>
        [Key]

        public int Pk_Id_Empresa { get; set; }

        [Display(Name = "NIT")]
        public string Nit_Empresa { get; set; }
        public string Tipo_Documento { get; set; }
        public int Identificacion_Representante { get; set; }

        [Display(Name = "Razón Social")]
        public string Razon_Social { get; set; }

        //[Display(Name = "Dirección Principal")]
        //public string Direccion_Principal { get; set; }

        public string Direccion { get; set; }
        public int? Telefono { get; set; }

        public int? Fax { get; set; }

        public int? Riesgo { get; set; }

        [Display(Name = "Numero Empleados")]
        public int? Total_Empleados { get; set; }

        public int? ID_Seccional { get; set; }

        public int? ID_Sector_Economico { get; set; }



        public string Email { get; set; }


        [Display(Name = "Sitio Web")]
        public string Sitio_Web { get; set; }
        [Display(Name = "Codigo Actividad")]
        public int Codigo_Actividad { get; set; }


        [Display(Name = "Actividad Economica")]
        public string Descripcion_Actividad { get; set; }

        //[ForeignKey("CIU")]
        //public int Fk_Id_CIU { get; set; }
        //[ForeignKey("Pk_Id_CIU")]
        //public virtual CIU CIU { get; set; }

        [Display(Name = "Fecha Afiliación")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string Fecha_Vigencia_Actual { get; set; }

        //[Display(Name = "Fecha Retiro")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public DateTime Fecha_Retiro { get; set; }

        public string Flg_Estado { get; set; }

        public string Zona { get; set; }

        public string imagen_proceso { get; set; }
        public string Logo_Empresa { get; set; }

        public ICollection<Gobierno> Gobiernos { get; set; }

        public ICollection<ProcesoEmpresa> ProcesoEmpresa { get; set; }
        public ICollection<Sede> sedes { get; set; }
        public ICollection<Organigrama> Organigrama { get; set; }
        //public ICollection <RolesPorEmpresa> RolesPorEmpresas { get; set; }
        public ICollection<Rol> Roles { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
        public ICollection<TematicaPorEmpresa> TematicaPorEmpresa { get; set; }

        //public ICollection<CrearInspeccion> CrearInspeccion { get; set; }
        public ICollection<AdmoEMH> AdmoEMHs { get; set; }
        public ICollection<EPP> EPPs { get; set; }
        public ICollection<EPPSuministro> EPPSuministros { get; set; }
        public ICollection<BateriaGestion> BateriaGestiones { get; set; }
        public EntidadesDominio.Ausentismo.EDEmpresa ObtenerED()
        {
            return new EntidadesDominio.Ausentismo.EDEmpresa
            {
                PK_Id_empresa = Pk_Id_Empresa,
                DireccionEmpresa = Direccion,
                IdEmpresa = Nit_Empresa,
                EmailEmpresa = Email,
                ActEconoPrincipal = Descripcion_Actividad,
                RazonSocial = Razon_Social,
            };
        }
    }
}