using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Politica
{
    [Table("Tbl_Politica")]
    public class mPolitica
    {
        [Key]
        public int intCod_Politica { get; set; }

        //public int nit_Empresa { get; set; }
       
        public bool Firma_Rep { get; set; }
        public string strDescripcion_Politica { get; set; }

        public String Archivo_Politica { get; set; }
   
        /// <summary>
        /// Obtiene y establece una clave foranea a empresa
        /// </summary>
        [ForeignKey("Empresa")]
        public int FK_Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }


    
  

    }
    
}