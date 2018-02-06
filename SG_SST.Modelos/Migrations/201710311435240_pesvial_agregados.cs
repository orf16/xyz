namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pesvial_agregados : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_SegVialParametro", "Fk_Id_Empresa", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_SegVialParametro", "disabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_SegVialParametro", "disabled");
            DropColumn("dbo.Tbl_SegVialParametro", "Fk_Id_Empresa");
        }
    }
}
