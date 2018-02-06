namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanTrabajoCronograma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_AplicacionPlanTrabajoProgramacion",
                c => new
                    {
                        Pk_Id_AplicacionPlanTrabajoProgramacion = c.Int(nullable: false, identity: true),
                        FechaProgramacionIncial = c.DateTime(nullable: false),
                        FechaEstado = c.DateTime(nullable: false),
                        Estado = c.Short(nullable: false),
                        Observaciones = c.String(maxLength: 2000),
                        Fk_Id_PlanTrabajoActividad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_AplicacionPlanTrabajoProgramacion)
                .ForeignKey("dbo.Tbl_AplicacionPlanTrabajoActividad", t => t.Fk_Id_PlanTrabajoActividad, cascadeDelete: true)
                .Index(t => t.Fk_Id_PlanTrabajoActividad);
            
            AddColumn("dbo.Tbl_BateriaUsuario", "FechaInforme", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_AplicacionPlanTrabajoProgramacion", "Fk_Id_PlanTrabajoActividad", "dbo.Tbl_AplicacionPlanTrabajoActividad");
            DropIndex("dbo.Tbl_AplicacionPlanTrabajoProgramacion", new[] { "Fk_Id_PlanTrabajoActividad" });
            DropColumn("dbo.Tbl_BateriaUsuario", "FechaInforme");
            DropTable("dbo.Tbl_AplicacionPlanTrabajoProgramacion");
        }
    }
}
