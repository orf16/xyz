namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioAcciones_AgregarOrigenCampo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Acciones", "Origen", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Tbl_Acciones", "Otro_Origen", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tbl_ActividadAccion", "Responsable", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_ActividadAccion", "Responsable", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Tbl_Acciones", "Otro_Origen");
            DropColumn("dbo.Tbl_Acciones", "Origen");
        }
    }
}
