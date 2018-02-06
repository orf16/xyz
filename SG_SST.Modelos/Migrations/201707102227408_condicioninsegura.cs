namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class condicioninsegura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_CondicionInsegura",
                c => new
                    {
                        Pk_Id_CondicionInsegura = c.Int(nullable: false, identity: true),
                        Descripcion_Condicion = c.String(),
                        UbicacionEspecificaInspeccion = c.String(),
                        RiesgoPeligroIdentificado = c.String(),
                        DescripcionRiesgoIdentificado = c.String(),
                        Evidencia = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_CondicionInsegura);
            
            CreateTable(
                "dbo.Tbl_CondicionesInsegurasporInspeccion",
                c => new
                    {
                        Pk_Id_CondicionInseguraporInspeccion = c.Int(nullable: false, identity: true),
                        Fk_Id_CondicionInsegura = c.Int(nullable: false),
                        Fk_Id_Inspecciones = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_CondicionInseguraporInspeccion)
                .ForeignKey("dbo.Tbl_CondicionInsegura", t => t.Fk_Id_CondicionInsegura, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Inspecciones", t => t.Fk_Id_Inspecciones, cascadeDelete: true)
                .Index(t => t.Fk_Id_CondicionInsegura)
                .Index(t => t.Fk_Id_Inspecciones);
            
            AddColumn("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura", c => c.Int());
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura");
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura", "dbo.Tbl_CondicionInsegura", "Pk_Id_CondicionInsegura");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura", "dbo.Tbl_CondicionInsegura");
            DropForeignKey("dbo.Tbl_CondicionesInsegurasporInspeccion", "Fk_Id_Inspecciones", "dbo.Tbl_Inspecciones");
            DropForeignKey("dbo.Tbl_CondicionesInsegurasporInspeccion", "Fk_Id_CondicionInsegura", "dbo.Tbl_CondicionInsegura");
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "CondicionInsegura_Pk_Id_CondicionInsegura" });
            DropIndex("dbo.Tbl_CondicionesInsegurasporInspeccion", new[] { "Fk_Id_Inspecciones" });
            DropIndex("dbo.Tbl_CondicionesInsegurasporInspeccion", new[] { "Fk_Id_CondicionInsegura" });
            DropColumn("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura");
            DropTable("dbo.Tbl_CondicionesInsegurasporInspeccion");
            DropTable("dbo.Tbl_CondicionInsegura");
        }
    }
}
