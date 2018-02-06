namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionCampoEmailPersonaParaCUComunicaciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Empleado_Tematica", "Email_Persona", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Empleado_Tematica", "Email_Persona");
        }
    }
}
