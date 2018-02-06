using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Models.Empleado
{

    [Table("Tbl_Tipo_Documento")]
    public class TipoDocumento
    {

        [Key]
        public int PK_IDTipo_Documento { get; set; }

        public string Descripcion { get; set; }

        public string Sigla { get; set; }

        /// <summary>
        /// Este tipo de documento aplica para personas.
        /// </summary>
        public bool AplicaPersonas { get; set; }

        /// <summary>
        /// Este tipo de documento aplica para empresas o sociedades.
        /// </summary>
        public bool AplicaEmpresas { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDTipoDocumento ObtenerED()
        {
            return new EDTipoDocumento
            {
                Id_Tipo_Documento = PK_IDTipo_Documento,
                Descripcion = Descripcion,
                Sigla = Sigla,
                AplicaEmpresas = AplicaEmpresas,
                AplicaPersonas = AplicaPersonas
            };
        }


    }
}