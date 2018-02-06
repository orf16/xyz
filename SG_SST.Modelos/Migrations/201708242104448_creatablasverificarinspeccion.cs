namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatablasverificarinspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_PlanAccionCorrectiva",
                c => new
                    {
                        Pk_Plan_Accion_Correctiva = c.Int(nullable: false, identity: true),
                        Adjunto_Seguimiento = c.String(),
                        Nombre_Verificador = c.String(),
                        Respuesta = c.String(),
                        Fk_Id_PlanAcccionInspeccion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Plan_Accion_Correctiva)
                .ForeignKey("dbo.Tbl_PlanAccionInspeccion", t => t.Fk_Id_PlanAcccionInspeccion, cascadeDelete: true)
                .Index(t => t.Fk_Id_PlanAcccionInspeccion);
            
            CreateTable(
                "dbo.Tbl_PlanAccionInspeccion",
                c => new
                    {
                        Pk_Id_PlanAcccionInspeccion = c.Int(nullable: false, identity: true),
                        Actividad_Plan_Accion = c.String(),
                        Responsable_Plan_Accion = c.String(),
                        Fecha_Fin_Plan_Accion = c.String(),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_PlanAcccionInspeccion);
            
            CreateTable(
                "dbo.Tbl_PlanAccionporCondicion",
                c => new
                    {
                        Pk_Id_PlanaccionporCondicion = c.Int(nullable: false, identity: true),
                        Fk_Id_PlanAcccionInspeccion = c.Int(),
                        Fk_Id_CondicionInsegura = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_PlanaccionporCondicion)
                .ForeignKey("dbo.Tbl_CondicionInsegura", t => t.Fk_Id_CondicionInsegura)
                .ForeignKey("dbo.Tbl_PlanAccionInspeccion", t => t.Fk_Id_PlanAcccionInspeccion)
                .Index(t => t.Fk_Id_PlanAcccionInspeccion)
                .Index(t => t.Fk_Id_CondicionInsegura);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_PlanAccionCorrectiva", "Fk_Id_PlanAcccionInspeccion", "dbo.Tbl_PlanAccionInspeccion");
            DropForeignKey("dbo.Tbl_PlanAccionporCondicion", "Fk_Id_PlanAcccionInspeccion", "dbo.Tbl_PlanAccionInspeccion");
            DropForeignKey("dbo.Tbl_PlanAccionporCondicion", "Fk_Id_CondicionInsegura", "dbo.Tbl_CondicionInsegura");
            DropIndex("dbo.Tbl_PlanAccionporCondicion", new[] { "Fk_Id_CondicionInsegura" });
            DropIndex("dbo.Tbl_PlanAccionporCondicion", new[] { "Fk_Id_PlanAcccionInspeccion" });
            DropIndex("dbo.Tbl_PlanAccionCorrectiva", new[] { "Fk_Id_PlanAcccionInspeccion" });
            DropTable("dbo.Tbl_PlanAccionporCondicion");
            DropTable("dbo.Tbl_PlanAccionInspeccion");
            DropTable("dbo.Tbl_PlanAccionCorrectiva");
        }
    }
}
