using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;



namespace SG_SST.Models.Empleado
{
    [Table("Tbl_Empleado")]
    public class tblEmpleado
    {

        [Key]
        public int ID_Empleado { get; set; }


        [ForeignKey("TipoDocumento")]
        [Display(Name = "Tipo Documento")]

        public int FK_Tipo_Documento_Empl { get; set; }

        [ForeignKey("PK_IDTipo_Documento")]
        public virtual TipoDocumento TipoDocumento { get; set; }


        [Display(Name = "Numero de Documento")]
        public int PK_Numero_Documento_Empl { get; set; }


        [Display(Name = "Nombre1")]
        public string Nombre1 { get; set; }


        [Display(Name = "Nombre2")]
        public string Nombre2 { get; set; }


        [Display(Name = "Apellido1")]
        public string Apellido1 { get; set; }


        [Display(Name = "Apellido2")]
        public string Apellido2 { get; set; }



        [Display(Name = "Estado Empleado")]
        [ForeignKey("Estado_Empleado")]
        public int FK_ID_Estado { get; set; }

        [ForeignKey("PK_IDEmpleadoEst")]
        public virtual Estado_Empleado Estado_Empleado { get; set; }


        [Display(Name = "Tipo Cotizante")]
        [ForeignKey("TipoCotizante")]
        public int FK_ID_Tipo_Cotizante { get; set; }

        [ForeignKey("Pk_Id_Cotizante")]
        public virtual TipoCotizante TipoCotizante { get; set; }


       
       /// Obtiene y establece una clave foranea a empresa
        //</summary>
       [ForeignKey("Empresa")]
       public int FK_Empresa { get; set; }
        ///<summary>
        ///Obtiene y establece un objeto de tipo empresa
        ///</summary>
       [ForeignKey("Pk_Id_Empresa")]
       public virtual Empresa Empresa { get; set; }
       



    }
}