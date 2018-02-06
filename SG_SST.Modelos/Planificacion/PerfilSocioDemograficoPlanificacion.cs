using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Empleado;

namespace SG_SST.Models.Planificacion
{
      [Table("Tbl_PerfilSocioDemograficoPlanificacion")]
    public class PerfilSocioDemograficoPlanificacion
    {

        [Key]
        public int IDEmpleado_PerfilSocioDemoGrafico { get; set; }

        public string PK_Numero_Documento_Empl { get; set; }
        //public string Nombre1 { get; set; }
        //public string Nombre2 { get; set; }
        //public string Apellido1 { get; set; }
        //public string Apellido2 { get; set; }

        public string GradoEscolaridad { get; set; }
        public string Ingresos { get; set; }
     
    
        public bool Conyuge { get; set; }
        public bool Hijos { get; set; }

        /// Obtiene y establece una clave foranea a estrato
        /// </summary>
        [ForeignKey("Tbl_Estrato")]
        public int FK_Estrato { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo estrato
        /// </summary>
        [ForeignKey("PK_Estrato")]
        public virtual Estrato Tbl_Estrato { get; set; }


        /// Obtiene y establece una clave foranea a Estado_Civil
        /// </summary>
        [ForeignKey("Tbl_Estado_Civil")]
        public int FK_Estado_Civil { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo Estado_Civil
        /// </summary>
        [ForeignKey("PK_Estado_Civil")]
        public virtual EstadoCivil Tbl_Estado_Civil { get; set; }

        [ForeignKey("municipios")]
        public int FK_Ciudad { get; set; }

        [ForeignKey("Pk_Id_Municipio")]
        public virtual Municipio municipios { get; set; }


        ////// Obtiene y establece una clave foranea a etnia
        ////// cambiar
        ////// </summary>
        [ForeignKey("Tbl_Etnia")]
        public int FK_Etnia { get; set; }

        [ForeignKey("PK_Etnia")]
        public virtual Etnia Tbl_Etnia { get; set; }

     
        public String Sexo { get; set; }


        /// Obtiene y establece una clave foranea a Vinculacion Laboral
        /// </summary>
        [ForeignKey("Tbl_VinculacionLaboral")]
        public int FK_VinculacionLaboral { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo Vinculacion Laboral
        /// </summary>
        [ForeignKey("PK_VinculacionLaboral")]
        public virtual VinculacionLaboral Tbl_VinculacionLaboral { get; set; }

        public String TurnoTrabajo { get; set; }
        //public String Cargo { get; set; }
        //public DateTime FechaIngresoEmpresa { get; set; }
        public DateTime FechaIngresoUltimoCargo { get; set; }

        //public String ocupacion { get; set; }
        public String caracteristicasFisicas { get; set; }

        public String caracteristicasPsicologicas { get; set; }

        public String evaluacionesMedicasRequeridas { get; set; }

        public String nitEmpresa { get; set; }

        //public int Edad { get; set; }

        [ForeignKey("Procesos")]
        public int? FK_Proceso { get; set; }

        [ForeignKey("Pk_Id_Proceso")]
        public virtual Proceso Procesos { get; set; }

        /// Obtiene y establece una clave foranea a sede
        /// </summary>
        [ForeignKey("Sede")]
        public int Fk_Sede { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Sede ")]
        public virtual Sede Sede { get; set; }

        public String ZonaLugar { get; set; }

    }
}
