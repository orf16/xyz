namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gestioncambiomodificacionajuste : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_GestionDelCambio",
                c => new
                    {
                        PK_GestionDelCambio = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        DescripcionDeCambio = c.String(),
                        AnalisisRiesgo = c.String(),
                        RequisitoLegal = c.String(),
                        Recomendaciones = c.String(),
                        FechaEjecucion = c.DateTime(nullable: false),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        Comunicado = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_GestionDelCambio)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_GestionDelCambio", "FK_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_GestionDelCambio", new[] { "FK_Empresa" });
            DropTable("dbo.Tbl_GestionDelCambio");
        }
    }
}
