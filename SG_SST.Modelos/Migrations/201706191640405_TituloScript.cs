namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TituloScript : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Incidentes", "Persona_direccion", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Incidentes", "Persona_direccion");
        }
    }
}
