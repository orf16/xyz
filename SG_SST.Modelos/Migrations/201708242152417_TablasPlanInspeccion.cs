namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablasPlanInspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Actividad_Plan_Accion",
                c => new
                    {
                        Pk_Id_ActividadPlanAccion = c.Int(nullable: false, identity: true),
                        Fk_Id_ModuloPlanAccion = c.Int(nullable: false),
                        Fk_Plan_Inspección = c.Int(nullable: false),
                        Fk_Id_Actividad = c.Int(nullable: false),
                        FechaCierre = c.DateTime(nullable: false),
                        Observaciones = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Pk_Id_ActividadPlanAccion)
                .ForeignKey("dbo.Tbl_Modulos_Plan_Accion", t => t.Fk_Id_ModuloPlanAccion, cascadeDelete: true)
                .Index(t => t.Fk_Id_ModuloPlanAccion);
            
            CreateTable(
                "dbo.Tbl_Modulos_Plan_Accion",
                c => new
                    {
                        Pk_Id_ModuloPlanAccion = c.Int(nullable: false, identity: true),
                        Modulo = c.String(nullable: false, maxLength: 50),
                        PlanInspeccion = c.String(nullable: false, maxLength: 100),
                        Actividad = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Pk_Id_ModuloPlanAccion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Actividad_Plan_Accion", "Fk_Id_ModuloPlanAccion", "dbo.Tbl_Modulos_Plan_Accion");
            DropIndex("dbo.Tbl_Actividad_Plan_Accion", new[] { "Fk_Id_ModuloPlanAccion" });
            DropTable("dbo.Tbl_Modulos_Plan_Accion");
            DropTable("dbo.Tbl_Actividad_Plan_Accion");
        }
    }
}
