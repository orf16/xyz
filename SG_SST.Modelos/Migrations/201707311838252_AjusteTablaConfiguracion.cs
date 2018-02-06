namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteTablaConfiguracion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_MaestroConfiguracion", "dbo.Tbl_Maestro_Configuracion_Prioridad");
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion", "dbo.Tbl_Configuracion_Inspeccion");
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "Fk_Id_MaestroConfiguracion" });
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion" });
            RenameColumn(table: "dbo.Tbl_ConfiguracionporInspeccion", name: "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion", newName: "Fk_Id_ConfiguracionInspeccion");
            AlterColumn("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_ConfiguracionInspeccion", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_ConfiguracionInspeccion");
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_ConfiguracionInspeccion", "dbo.Tbl_Configuracion_Inspeccion", "Pk_Id_ConfiguracionInspeccion", cascadeDelete: true);
            //DropColumn("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_MaestroConfiguracion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_MaestroConfiguracion", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_ConfiguracionInspeccion", "dbo.Tbl_Configuracion_Inspeccion");
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "Fk_Id_ConfiguracionInspeccion" });
            AlterColumn("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_ConfiguracionInspeccion", c => c.Int());
            RenameColumn(table: "dbo.Tbl_ConfiguracionporInspeccion", name: "Fk_Id_ConfiguracionInspeccion", newName: "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion");
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion");
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_MaestroConfiguracion");
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion", "dbo.Tbl_Configuracion_Inspeccion", "Pk_Id_ConfiguracionInspeccion");
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_MaestroConfiguracion", "dbo.Tbl_Maestro_Configuracion_Prioridad", "Pk_Id_MaestroConfiguracion", cascadeDelete: true);
        }
    }
}
