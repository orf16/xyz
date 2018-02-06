namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class desbloqueo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Planeacion_Inspeccion", "Fk_Id_Maestro_Tipo_Inspeccion", "dbo.Tbl_Maestro_Planeación_Inspeccion");
            DropIndex("dbo.Tbl_Planeacion_Inspeccion", new[] { "Fk_Id_Maestro_Tipo_Inspeccion" });
            DropColumn("dbo.Tbl_Planeacion_Inspeccion", "Fk_Id_Maestro_Tipo_Inspeccion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Planeacion_Inspeccion", "Fk_Id_Maestro_Tipo_Inspeccion", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Planeacion_Inspeccion", "Fk_Id_Maestro_Tipo_Inspeccion");
            AddForeignKey("dbo.Tbl_Planeacion_Inspeccion", "Fk_Id_Maestro_Tipo_Inspeccion", "dbo.Tbl_Maestro_Planeación_Inspeccion", "Pk_Id_Maestro_Tipo_Inspeccion", cascadeDelete: true);
        }
    }
}
