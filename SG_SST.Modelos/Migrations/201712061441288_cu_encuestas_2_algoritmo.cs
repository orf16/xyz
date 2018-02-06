namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_encuestas_2_algoritmo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ComunicacionesInternas", "URL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ComunicacionesInternas", "URL");
        }
    }
}
