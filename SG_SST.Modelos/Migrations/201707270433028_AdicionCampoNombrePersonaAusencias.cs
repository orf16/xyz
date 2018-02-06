namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionCampoNombrePersonaAusencias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Ausencias", "NombrePersona", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Ausencias", "NombrePersona");
        }
    }
}
