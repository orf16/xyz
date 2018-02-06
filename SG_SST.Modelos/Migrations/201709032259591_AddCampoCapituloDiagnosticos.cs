namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoCapituloDiagnosticos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Diagnosticos", "Capitulo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Diagnosticos", "Capitulo");
        }
    }
}
