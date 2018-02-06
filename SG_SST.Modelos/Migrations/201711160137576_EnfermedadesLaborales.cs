namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnfermedadesLaborales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_DocumentosEnviadosEPS",
                c => new
                    {
                        Pk_Id_DocumentoEnviadoEPS = c.Int(nullable: false, identity: true),
                        RutaDocumentoEnviadoEPS = c.String(),
                        FechaRegistroDocumento = c.DateTime(nullable: false),
                        Fk_Id_EnfermedadLaboral = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_DocumentoEnviadoEPS)
                .ForeignKey("dbo.Tbl_EnfermedadesLaboralesDiagnosticadas", t => t.Fk_Id_EnfermedadLaboral, cascadeDelete: true)
                .Index(t => t.Fk_Id_EnfermedadLaboral);
            
            CreateTable(
                "dbo.Tbl_EnfermedadesLaboralesDiagnosticadas",
                c => new
                    {
                        Pk_Id_EnfermedadLaboral = c.Int(nullable: false, identity: true),
                        CodigoEmpleado = c.Int(nullable: false),
                        CodigoDiagnosticoCIIE10 = c.Int(nullable: false),
                        Diagnostico = c.Int(nullable: false),
                        RutaDocumentoFUREL = c.String(),
                        RutaCartaEnviadaEPS = c.String(),
                        FechaEnvioDocumentosEPS = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_EnfermedadLaboral);
            
            CreateTable(
                "dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada",
                c => new
                    {
                        Pk_Id_InstanciaEnfermedadLaboral = c.Int(nullable: false, identity: true),
                        EstadoInstancia = c.Int(nullable: false),
                        QuienCalifica = c.String(),
                        FechaCalificacion = c.DateTime(nullable: false),
                        Fk_Id_EnfermedadLaboral = c.Int(nullable: false),
                        FK_Id_EstadoInstancia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_InstanciaEnfermedadLaboral)
                .ForeignKey("dbo.Tbl_EnfermedadesLaboralesDiagnosticadas", t => t.Fk_Id_EnfermedadLaboral, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_EstadosInstanciasRegistradas", t => t.FK_Id_EstadoInstancia, cascadeDelete: true)
                .Index(t => t.Fk_Id_EnfermedadLaboral)
                .Index(t => t.FK_Id_EstadoInstancia);
            
            CreateTable(
                "dbo.Tbl_EstadosInstanciasRegistradas",
                c => new
                    {
                        PK_Id_EstadoInstancia = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_EstadoInstancia);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_DocumentosEnviadosEPS", "Fk_Id_EnfermedadLaboral", "dbo.Tbl_EnfermedadesLaboralesDiagnosticadas");
            DropForeignKey("dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada", "FK_Id_EstadoInstancia", "dbo.Tbl_EstadosInstanciasRegistradas");
            DropForeignKey("dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada", "Fk_Id_EnfermedadLaboral", "dbo.Tbl_EnfermedadesLaboralesDiagnosticadas");
            DropIndex("dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada", new[] { "FK_Id_EstadoInstancia" });
            DropIndex("dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada", new[] { "Fk_Id_EnfermedadLaboral" });
            DropIndex("dbo.Tbl_DocumentosEnviadosEPS", new[] { "Fk_Id_EnfermedadLaboral" });
            DropTable("dbo.Tbl_EstadosInstanciasRegistradas");
            DropTable("dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada");
            DropTable("dbo.Tbl_EnfermedadesLaboralesDiagnosticadas");
            DropTable("dbo.Tbl_DocumentosEnviadosEPS");
        }
    }
}
