namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actualiza_EmpleadoTercero : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tbl_EmpleadoTercero", "Telefono");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_EmpleadoTercero", "Telefono", c => c.Int(nullable: false));
        }
    }
}
