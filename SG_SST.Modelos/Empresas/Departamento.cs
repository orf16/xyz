// <copyright file="Departamento.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Andres Correa.</author>
// <date>08/01/2017</date>
// <summary>Modelo que contiene los campos de la tabla Departamento</summary>

namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using EntidadesDominio.Empresas;

    [Table("Tbl_Departamento")]
    public class Departamento
    {
        [Key]
        public int Pk_Id_Departamento { get; set; }       


        [Display(Name = "Nombre Departamento")]
        public string Nombre_Departamento { get; set; }


        [Display(Name = "Codigo Departamento")]
        public string Codigo_Departamento { get; set; }


        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDDepartamento ObtenerED()
        {
            return new EDDepartamento
            {
                Pk_Id_Departamento = Pk_Id_Departamento,
                Codigo_Departamento = Codigo_Departamento,
                Nombre_Departamento = Nombre_Departamento
            };
        }







    }
}