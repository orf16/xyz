// <copyright file="Sede.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Robinson Andres Correa.</author>
// <date>08/01/2017</date>
// <summary>Modelo de datos de la tabla Sede.</summary>

namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using SG_SST.Models.Planificacion;
    using SG_SST.Models.Organizacion;
    using SG_SST.Models.Aplicacion;
    using EntidadesDominio.Empresas;

    [Table("Tbl_Sede")]
    public class Sede
    {
        [Key]
        [Display(Name = "ID")]
        public int Pk_Id_Sede { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

        [Display(Name = "Nombre Sede")]
        public string Nombre_Sede { get; set; }

        [Display(Name = "Dirección Sede")]
        public string Direccion_Sede { get; set; }
        public string Sector { get; set; }
        public string Telefono { get; set; }

        public ICollection<SedeMunicipio> SedeMunicipios { get; set; }

        public ICollection<CentroTrabajo> CentrosTrabajo { get; set; }

        public ICollection<Peligro> Peligros { get; set; }

        public ICollection<RecursoporSede> RecursoporSede { get; set; }

        public ICollection<Inspecciones> Inspecciones { get; set; }

        public ICollection<EPPSuministro> EPPSuministros { get; set; }

        public ICollection<PlanVial> PlanViales { get; set; }
        public ICollection<AplicacionPlanTrabajo> PlanTrabajos { get; set; }

    }
}