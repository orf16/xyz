namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcampologoempresaTblemp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Empresa", "Logo_Empresa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Empresa", "Logo_Empresa");
        }
    }
}
