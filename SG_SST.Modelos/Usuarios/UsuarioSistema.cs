using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Models.Empleado;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_UsuarioSistema")]
    public class UsuarioSistema
    {
        [Key]
        public int Pk_Id_UsuarioSistema { get; set; }

        [ForeignKey("TipoDocumento")]
        public int Fk_Id_TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string ClaveSalt { get; set; }
        public string ClaveHash { get; set; }
        public bool Activo { get; set; }
        public bool PrimerAcceso { get; set; }
        public DateTime? PeriodoInactivacionCuenta { get; set; }

        [ForeignKey("PK_IDTipo_Documento")]
        public virtual TipoDocumento TipoDocumento { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDUsuarioSistema ObtenerED()
        {
            return new EDUsuarioSistema
            {
                IdUsuarioSistema = this.Pk_Id_UsuarioSistema,
                Documento = this.Documento,
                Correo = this.Correo,
                ClaveSalt = this.ClaveSalt,
                ClaveHash = this.ClaveHash,
                Activo = this.Activo
            };
        }
    }
}
