namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioforaneas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_PlanAccionPorInspeccion", "Inspecciones_Pk_Id_Inspecciones", "dbo.Tbl_Inspecciones");
            DropIndex("dbo.Tbl_PlanAccionPorInspeccion", new[] { "Inspecciones_Pk_Id_Inspecciones" });
            RenameColumn(table: "dbo.Tbl_PlanAccionPorInspeccion", name: "Inspecciones_Pk_Id_Inspecciones", newName: "Fk_Id_Inspecciones");
            AlterColumn("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspecciones", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspecciones");
            AddForeignKey("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspecciones", "dbo.Tbl_Inspecciones", "Pk_Id_Inspecciones", cascadeDelete: true);
            DropColumn("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspeccion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspeccion", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspecciones", "dbo.Tbl_Inspecciones");
            DropIndex("dbo.Tbl_PlanAccionPorInspeccion", new[] { "Fk_Id_Inspecciones" });
            AlterColumn("dbo.Tbl_PlanAccionPorInspeccion", "Fk_Id_Inspecciones", c => c.Int());
            RenameColumn(table: "dbo.Tbl_PlanAccionPorInspeccion", name: "Fk_Id_Inspecciones", newName: "Inspecciones_Pk_Id_Inspecciones");
            CreateIndex("dbo.Tbl_PlanAccionPorInspeccion", "Inspecciones_Pk_Id_Inspecciones");
            AddForeignKey("dbo.Tbl_PlanAccionPorInspeccion", "Inspecciones_Pk_Id_Inspecciones", "dbo.Tbl_Inspecciones", "Pk_Id_Inspecciones");
        }
    }
}
