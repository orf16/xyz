namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_encuestas_algoritmo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ComunicacionesEncuestas", "URL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ComunicacionesEncuestas", "URL");
        }
    }
}
