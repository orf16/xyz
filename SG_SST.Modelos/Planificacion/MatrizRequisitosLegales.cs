using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using System.Data.Entity.SqlServer;
using System;

//tabla usada en el módulo requisitos legales - nombre de la matriz que se crea que contiene los requisitos legales
namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Matriz_RequisitosLegales")]
    public class MatrizRequisitosLegales
    {
        [Key]
        public int PK_MatrizRequisitosLegales { get; set; }
        public string NombreMatriz { get; set; }


        [ForeignKey("Empresa")]
        public int FK_Empresa { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }




    }
}

