// <copyright file="CentroTrabajo.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Andres Correa.</author>
// <date>08/01/2017</date>
// <summary>Modelo de datos de la tabla Centro Trabajo.</summary>
namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    [Table("Tbl_Centro_de_Trabajo")]
    public class CentroTrabajo
    {
        [Key]
        
        public int Pk_Id_Centro_de_Trabajo { get; set; }

        [Display(Name = "ID Centro")]
        public int ID_Centro { get; set; }

        [Display(Name = "Actividad Economica")]
        public string Descripcion_Actividad { get; set; }


        [Display(Name = "Codigo Actividad")]
        public int Codigo_Actividad { get; set; }


       [Display(Name = "Total Trabajadores")]
       public int Numero_Trabajadores { get; set; }


       [ForeignKey("Sede")]
       public int Fk_Id_Sede { get; set; }

       [ForeignKey("Pk_Id_Sede")]
       public virtual Sede Sede { get; set; }
 
    }
}