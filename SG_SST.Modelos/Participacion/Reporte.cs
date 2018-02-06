using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_Reportes")]
    public class Reporte
    {
        [Key]
        public int PK_Id_Reportes { get; set; }
        public int ConsecutivoReporte { get; set; }
        public string RazonSocialEmpresa { get; set; }
       

        public DateTime fechaSistema { get; set; }

        public string FK_NitEmpresa { get; set; }

        /// Obtiene y establece la clave foranea a la tabla  sede.
        /// </summary>
        [ForeignKey("Sede")]
        public int FK_Sede { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de sede.
        /// </summary>
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }


        [ForeignKey("Procesos")]
        public int? FK_Proceso { get; set; }

        [ForeignKey("Pk_Id_Proceso")]
        public virtual Proceso Procesos { get; set; }


        //relacion entre tabla reporte y tipo reporte
        [ForeignKey("TipoReporte")]
        public int FK_Tipo_Reporte { get; set; }

        [ForeignKey("Pk_Id_Tipo_Reporte")]
        public virtual TipoReporte TipoReporte { get; set; }



        public string Area_Lugar { get; set; }

        public DateTime Fecha_Ocurrencia { get; set; }

        public int Cedula_Quien_Reporta { get; set; }


        public string descripcion_Reporte { get; set; }
        public string Causa_Reporte { get; set; }
        public string Sugerencias_Reporte { get; set; }

    
      
        public bool medioAcceso { get; set; }



    }
}
