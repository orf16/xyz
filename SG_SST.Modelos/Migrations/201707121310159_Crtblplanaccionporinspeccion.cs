namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crtblplanaccionporinspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_PlanAccionPorInspeccion",
                c => new
                    {
                        Pk_Id_PlanAccionPorInspeccion = c.Int(nullable: false, identity: true),
                        Fk_Id_Actividad = c.Int(nullable: false),
                        Fk_Id_Inspeccion = c.Int(nullable: false),
                        ActividadAccion_Pk_Id_Actividad = c.Int(),
                        Inspecciones_Pk_Id_Inspecciones = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_PlanAccionPorInspeccion)
                .ForeignKey("dbo.Tbl_ActividadAccion", t => t.ActividadAccion_Pk_Id_Actividad)
                .ForeignKey("dbo.Tbl_Inspecciones", t => t.Inspecciones_Pk_Id_Inspecciones)
                .Index(t => t.ActividadAccion_Pk_Id_Actividad)
                .Index(t => t.Inspecciones_Pk_Id_Inspecciones);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_PlanAccionPorInspeccion", "Inspecciones_Pk_Id_Inspecciones", "dbo.Tbl_Inspecciones");
            DropForeignKey("dbo.Tbl_PlanAccionPorInspeccion", "ActividadAccion_Pk_Id_Actividad", "dbo.Tbl_ActividadAccion");
            DropIndex("dbo.Tbl_PlanAccionPorInspeccion", new[] { "Inspecciones_Pk_Id_Inspecciones" });
            DropIndex("dbo.Tbl_PlanAccionPorInspeccion", new[] { "ActividadAccion_Pk_Id_Actividad" });
            DropTable("dbo.Tbl_PlanAccionPorInspeccion");
        }
    }
}
