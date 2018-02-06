namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_algoritmo_comunicaciones_app : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ComunicadosAPP", "AsuntoAPP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ComunicadosAPP", "AsuntoAPP");
        }
    }
}
