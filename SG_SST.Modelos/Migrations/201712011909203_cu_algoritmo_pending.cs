namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_algoritmo_pending : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_GruposComunicaciones", "NitEmpresa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_GruposComunicaciones", "NitEmpresa");
        }
    }
}
