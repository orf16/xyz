namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class planpesv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_AplicacionPlanTrabajo",
                c => new
                    {
                        Pk_Id_PlanTrabajo = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Vigencia = c.Int(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        FechaAplicacion = c.DateTime(),
                        RepLegalImagen = c.String(maxLength: 2000),
                        RepSGSSTImagen = c.String(maxLength: 2000),
                        RepLegalRuta = c.String(),
                        RepSGSSTRuta = c.String(),
                        RepLegalNombre = c.String(maxLength: 1000),
                        RepSGSSTNombre = c.String(maxLength: 1000),
                        RepLegalTipoDocumento = c.String(maxLength: 100),
                        RepSGSSTTipoDocumento = c.String(maxLength: 100),
                        RepLegalDocumento = c.String(maxLength: 250),
                        RepSGSSTDocumento = c.String(maxLength: 250),
                        Fk_Id_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_PlanTrabajo)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: true)
                .Index(t => t.Fk_Id_Sede);
            
            CreateTable(
                "dbo.Tbl_AplicacionPlanTrabajoDetalle",
                c => new
                    {
                        Pk_Id_PlanTrabajoDetalle = c.Int(nullable: false, identity: true),
                        Objetivo = c.String(nullable: false, maxLength: 2000),
                        Metas = c.String(nullable: false, maxLength: 2000),
                        RecursoHumano = c.String(nullable: false, maxLength: 2000),
                        RecursoTec = c.String(nullable: false, maxLength: 2000),
                        RecursoFinanciero = c.String(nullable: false, maxLength: 2000),
                        Fk_Id_PlanTrabajo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_PlanTrabajoDetalle)
                .ForeignKey("dbo.Tbl_AplicacionPlanTrabajo", t => t.Fk_Id_PlanTrabajo, cascadeDelete: true)
                .Index(t => t.Fk_Id_PlanTrabajo);
            
            CreateTable(
                "dbo.Tbl_AplicacionPlanTrabajoActividad",
                c => new
                    {
                        Pk_Id_PlanTrabajoActividad = c.Int(nullable: false, identity: true),
                        FechaProgramacionIncial = c.DateTime(nullable: false),
                        FechaEstado = c.DateTime(nullable: false),
                        Estado = c.Short(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 2000),
                        Observaciones = c.String(maxLength: 2000),
                        ResponsableNombre = c.String(nullable: false, maxLength: 1000),
                        ResponsableDocumento = c.String(nullable: false, maxLength: 250),
                        ResponsableTipoDocumento = c.String(nullable: false, maxLength: 250),
                        Fk_Id_PlanTrabajoDetalle = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_PlanTrabajoActividad)
                .ForeignKey("dbo.Tbl_AplicacionPlanTrabajoDetalle", t => t.Fk_Id_PlanTrabajoDetalle, cascadeDelete: true)
                .Index(t => t.Fk_Id_PlanTrabajoDetalle);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_AplicacionPlanTrabajo", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_AplicacionPlanTrabajoActividad", "Fk_Id_PlanTrabajoDetalle", "dbo.Tbl_AplicacionPlanTrabajoDetalle");
            DropForeignKey("dbo.Tbl_AplicacionPlanTrabajoDetalle", "Fk_Id_PlanTrabajo", "dbo.Tbl_AplicacionPlanTrabajo");
            DropIndex("dbo.Tbl_AplicacionPlanTrabajoActividad", new[] { "Fk_Id_PlanTrabajoDetalle" });
            DropIndex("dbo.Tbl_AplicacionPlanTrabajoDetalle", new[] { "Fk_Id_PlanTrabajo" });
            DropIndex("dbo.Tbl_AplicacionPlanTrabajo", new[] { "Fk_Id_Sede" });
            DropTable("dbo.Tbl_AplicacionPlanTrabajoActividad");
            DropTable("dbo.Tbl_AplicacionPlanTrabajoDetalle");
            DropTable("dbo.Tbl_AplicacionPlanTrabajo");
        }
    }
}
