namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampoTBL_Incidentes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Incidentes", "FK_id_sede_no_principal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Incidentes", "FK_id_sede_no_principal");
        }
    }
}
