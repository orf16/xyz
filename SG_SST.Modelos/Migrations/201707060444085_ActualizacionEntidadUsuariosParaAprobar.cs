namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionEntidadUsuariosParaAprobar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_UsuariosParaAprobar", "RazonSocial", c => c.String());
            AddColumn("dbo.Tbl_UsuariosParaAprobar", "MunicipioSedePpal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_UsuariosParaAprobar", "MunicipioSedePpal");
            DropColumn("dbo.Tbl_UsuariosParaAprobar", "RazonSocial");
        }
    }
}
