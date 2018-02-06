namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actualiza_EmpleadoTercero_B : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion");
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_Ocupacion_Empl" });
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl");
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion", "PK_Ocupacion", cascadeDelete: true);
        }
    }
}
