// <copyright file="Procesos.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Andres Correa.</author>
// <date>13/02/2017</date>
// <summary>Modelo que contiene los campos de la tabla Procesos</summary>

namespace SG_SST.Models.Empresas
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using SG_SST.Models.Planificacion;
    using SG_SST.Models.Aplicacion;

    [Table("Tbl_Proceso")]
    public class Proceso
    {
        [Key]
        public int Pk_Id_Proceso { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio.")]
        [Display(Name = "SubProceso")]
        public String Descripcion_Proceso { get; set; }
        

        [ForeignKey("Procesos")]
        public int? Fk_Id_Proceso { get; set; }
        [ForeignKey("Pk_Id_Proceso")]
        public virtual Proceso Procesos { get; set; }



        //[ForeignKey("Empresa")]
        //public int Fk_Id_Empresa { get; set; }

        //[ForeignKey("Pk_Id_Empresa")]
        //public virtual Empresa Empresa { get; set; }

        public ICollection<ProcesoEmpresa> ProcesoEmpresa { get; set; }

        public ICollection<Peligro> Peligros { get; set; }
        public ICollection<Inspecciones> Inspecciones { get; set; }
        public ICollection<EPPSuministro> EPPSuministros { get; set; }
        //public ICollection<CrearInspeccion> CrearInspeccion { get; set; }
    }
}