namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificaTbls_Accion_seg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion");
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_Ocupacion_Empl" });
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl");
            DropColumn("dbo.Tbl_EmpleadoTercero", "Telefono");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_EmpleadoTercero", "Telefono", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl");
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion", "PK_Ocupacion", cascadeDelete: true);
        }
    }
}
