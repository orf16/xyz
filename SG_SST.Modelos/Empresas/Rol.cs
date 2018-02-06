
namespace SG_SST.Models.Empresas
{
    using System;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Models.Organizacion;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Rol")]
    public class Rol
    {
        [Key]
        public int Pk_Id_Rol { get; set; }
        
        public string Descripcion { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; }

        public ICollection<PrivilegiosPorRol> PrivilegiosporRoles { get; set; }

        [ForeignKey("Empresa")]
        public int ? Fk_Id_Empresa { get; set; }

        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }       
        

        public ICollection<ResponsabilidadesPorRol> ResponsabilidadesPorRoles { get; set; }

        public ICollection<RendicionDeCuentasPorRol> RendicionDeCuentasPorRoles { get; set; }
        public ICollection<CargoPorRol> CargoPorRol { get; set; }
        public ICollection<RolPorTematica> RolPorTematica { get; set; }

        public ICollection<EmpleadoPorTematica> EmpleadoPorTematica { get; set; }



    }
}