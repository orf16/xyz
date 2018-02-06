namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creaciontblinspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Inspecciones",
                c => new
                    {
                        Pk_Id_Inspecciones = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        Fk_Id_PlaneacionInspeccion = c.Int(nullable: false),
                        Sede = c.String(),
                        Proceso = c.String(),
                        AreaLugar = c.String(),
                        Hora = c.String(),
                        ResponsableLugar = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Inspecciones)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Planeacion_Inspeccion", t => t.Fk_Id_PlaneacionInspeccion, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa)
                .Index(t => t.Fk_Id_PlaneacionInspeccion);
            
            AddColumn("dbo.Tbl_AsistentesporInspeccion", "Fk_Id_Inspeccion", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_Inspecciones", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_AsistentesporInspeccion", "Fk_Id_Inspeccion");
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_Inspecciones");
            AddForeignKey("dbo.Tbl_AsistentesporInspeccion", "Fk_Id_Inspeccion", "dbo.Tbl_Inspecciones", "Pk_Id_Inspecciones", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_Inspecciones", "dbo.Tbl_Inspecciones", "Pk_Id_Inspecciones", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_PlaneacionInspeccion", "dbo.Tbl_Planeacion_Inspeccion");
            DropForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_Inspecciones", "dbo.Tbl_Inspecciones");
            DropForeignKey("dbo.Tbl_AsistentesporInspeccion", "Fk_Id_Inspeccion", "dbo.Tbl_Inspecciones");
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "Fk_Id_Inspecciones" });
            DropIndex("dbo.Tbl_AsistentesporInspeccion", new[] { "Fk_Id_Inspeccion" });
            DropIndex("dbo.Tbl_Inspecciones", new[] { "Fk_Id_PlaneacionInspeccion" });
            DropIndex("dbo.Tbl_Inspecciones", new[] { "Fk_Id_Empresa" });
            DropColumn("dbo.Tbl_ConfiguracionporInspeccion", "Fk_Id_Inspecciones");
            DropColumn("dbo.Tbl_AsistentesporInspeccion", "Fk_Id_Inspeccion");
            DropTable("dbo.Tbl_Inspecciones");
        }
    }
}
