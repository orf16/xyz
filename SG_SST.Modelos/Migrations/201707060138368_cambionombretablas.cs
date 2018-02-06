namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambionombretablas : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ConfiguracionporInspeccions", newName: "Tbl_ConfiguracionposInspeccion");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tbl_ConfiguracionposInspeccion", newName: "ConfiguracionporInspeccions");
        }
    }
}
