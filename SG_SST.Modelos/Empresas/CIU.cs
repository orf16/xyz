// <copyright file="CIU.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Andres Correa.</author>
// <date>08/01/2017</date>
// <summary>Modelo de datos de la tabla  Maestra CIU.</summary>
namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    [Table("Tbl_CIU")]
    public class CIU
    {
        [Key]
        public int Pk_Id_CIU { get; set; }

       [Display(Name = "Codigo CIU")]
        public int Codigo_CIU { get; set; }

        [Display(Name = "Descripcion Codigo")]
        public string Descripcion { get; set; }
        
        //public ICollection<Empresa> Empresas { get; set; }

    }
}