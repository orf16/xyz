namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionModuloUsuarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_RolesSistema", "Bloqueado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_RolesSistema", "Bloqueado");
        }
    }
}
