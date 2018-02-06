using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using System.Data.Entity.SqlServer;
using System;


///tabla donde se relacionan la matriz con sus correspondientes requisitos legales - modulo requisitos legales
namespace SG_SST.Models.Planificacion
{

    [Table("Tbl_Requisitos_Matriz")]
    public class Requisitos_Matriz
    {
        [Key]
        public int PK_Requisitos_Matriz { get; set; }


        [ForeignKey("Tbl_Requisitos_legales_Otros")]
        public int FK_RequisitosLegalesOtros { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_RequisitosLegalesOtros")]
        public virtual RequisitosLegalesOtros Tbl_Requisitos_legales_Otros { get; set; }



        [ForeignKey("Tbl_Matriz_RequisitosLegales")]
        public int FK_MatrizRequisitosLegales { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("PK_MatrizRequisitosLegales")]
        public virtual MatrizRequisitosLegales Tbl_Matriz_RequisitosLegales { get; set; }


    }
}




