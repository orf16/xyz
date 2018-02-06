namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModicaTbl_Ausencias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Ausencias", "FK_Id_Proceso", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Ausencias", "FK_Id_Proceso");
        }
    }
}
