// <copyright file="Municipio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion del peligro de la metodologia GTC45.</summary>

namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using EntidadesDominio.Empresas;

    [Table("Tbl_Municipio")]
    public class Municipio
    {

        /// <summary>
        /// Obtiene y establece la clave primaria a la tabla empresa
        /// </summary>
        /// 
        [Key]
        public int Pk_Id_Municipio { get; set; }


        [Display(Name = "Municipio")]
        public string Nombre_Municipio { get; set; }


        [Display(Name = "Codigo Municipio")]
        public string Codigo_Municipio { get; set; }


        [Display(Name = "Nombre Departamento")]
        [ForeignKey("Departamento")]
        public int Fk_Nombre_Departamento { get; set; }
        [ForeignKey("Pk_Id_Departamento")]
        public virtual Departamento Departamento { get; set; }

        public ICollection<SedeMunicipio> SedeMunicipios { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDMunicipio ObtenerED()
        {
            var Item = new EDMunicipio
            {
                IdMunicipio = Pk_Id_Municipio,
                NombreMunicipio = Nombre_Municipio,
                CodigoMunicipio = Codigo_Municipio
            };

            if (Departamento != null)
                Item.Departamento = Departamento.ObtenerED();

            // TODO: Crear la lista para SedeMunicipios.

            return Item;
        }




    }
}