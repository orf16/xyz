namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Añadir_tablas_auditorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_AuditoriaPrograma",
                c => new
                {
                    Pk_Id_Programa = c.Int(nullable: false, identity: true),
                    Titulo = c.String(nullable: false, maxLength: 500),
                    Objetivo = c.String(nullable: false, maxLength: 3000),
                    Alcance = c.String(nullable: false, maxLength: 3000),
                    Metodologia = c.String(nullable: false, maxLength: 3000),
                    Competencia = c.String(nullable: false, maxLength: 3000),
                    Recursos = c.String(nullable: false, maxLength: 3000),
                    Fecha_Programacion = c.DateTime(nullable: false),
                    Año = c.Int(nullable: false),
                    Periodicidad = c.Int(nullable: false),
                    NombreArchivoRes = c.String(maxLength: 200),
                    RutaArchivoRes = c.String(maxLength: 1000),
                    FirmaScrImageRes = c.String(),
                    Nombre_Responsable = c.String(),
                    Cargo_Responsable = c.String(),
                    NombreArchivoCopasst = c.String(maxLength: 200),
                    RutaArchivoPres = c.String(maxLength: 1000),
                    FirmaScrImagePres = c.String(),
                    Nombre_Copasst = c.String(),
                    Cargo_Copasst = c.String(),
                    SedeAuditoria = c.String(maxLength: 2000),
                    Fk_Id_Empresa = c.Int(nullable: false),
                    Fk_Id_Sede = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_Programa)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: false)
                .Index(t => t.Fk_Id_Empresa)
                .Index(t => t.Fk_Id_Sede);

            CreateTable(
                "dbo.Tbl_Auditorias",
                c => new
                {
                    Pk_Id_Auditoria = c.Int(nullable: false, identity: true),
                    Periodo = c.Int(nullable: false),
                    Objetivo = c.String(nullable: false, maxLength: 3000),
                    Alcance = c.String(nullable: false, maxLength: 3000),
                    Criterios = c.String(nullable: false, maxLength: 3000),
                    FechaRealizacion = c.DateTime(nullable: false),
                    Auditado = c.String(nullable: false, maxLength: 1000),
                    Auditador = c.String(nullable: false, maxLength: 1000),
                    Requisito = c.String(nullable: false, maxLength: 1000),
                    Duracion = c.String(nullable: false, maxLength: 1000),
                    Fk_Id_Programa = c.Int(nullable: false),
                    Fk_Id_Proceso = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_Auditoria)
                .ForeignKey("dbo.Tbl_AuditoriaPrograma", t => t.Fk_Id_Programa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Proceso", t => t.Fk_Id_Proceso, cascadeDelete: true)
                .Index(t => t.Fk_Id_Programa)
                .Index(t => t.Fk_Id_Proceso);

            CreateTable(
                "dbo.Tbl_AuditoriaCronograma",
                c => new
                {
                    Pk_Id_Cronograma_Auditoria = c.Int(nullable: false, identity: true),
                    Tema = c.String(nullable: false, maxLength: 1000),
                    Responsable = c.String(nullable: false, maxLength: 1000),
                    Fecha_Hora = c.DateTime(nullable: false),
                    TiempoEstimado = c.String(nullable: false, maxLength: 1000),
                    Lugar = c.String(nullable: false, maxLength: 1000),
                    Fk_Id_Auditoria = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_Cronograma_Auditoria)
                .ForeignKey("dbo.Tbl_Auditorias", t => t.Fk_Id_Auditoria, cascadeDelete: true)
                .Index(t => t.Fk_Id_Auditoria);

            CreateTable(
                "dbo.Tbl_AuditoriaListaVer",
                c => new
                {
                    Pk_Id_Lista_Verificacion = c.Int(nullable: false, identity: true),
                    Pregunta = c.String(nullable: false, maxLength: 3000),
                    Requisito = c.String(nullable: false, maxLength: 3000),
                    Hallazgo = c.String(maxLength: 3000),
                    Tipo_Hallazgo = c.String(maxLength: 300),
                    Fk_Id_Auditoria = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_Lista_Verificacion)
                .ForeignKey("dbo.Tbl_Auditorias", t => t.Fk_Id_Auditoria, cascadeDelete: true)
                .Index(t => t.Fk_Id_Auditoria);

            CreateTable(
                "dbo.Tbl_AuditoriaInforme",
                c => new
                {
                    Pk_Id_Auditoria = c.Int(nullable: false),
                    FechaRealizacion = c.String(nullable: false),
                    Ubicacion = c.String(nullable: false, maxLength: 1000),
                })
                .PrimaryKey(t => t.Pk_Id_Auditoria)
                .ForeignKey("dbo.Tbl_Auditorias", t => t.Pk_Id_Auditoria)
                .Index(t => t.Pk_Id_Auditoria);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Tbl_AuditoriaInforme", "Pk_Id_Auditoria", "dbo.Tbl_Auditorias");
            DropForeignKey("dbo.Tbl_AuditoriaPrograma", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_AuditoriaPrograma", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Auditorias", "Fk_Id_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_Auditorias", "Fk_Id_Programa", "dbo.Tbl_AuditoriaPrograma");
            DropForeignKey("dbo.Tbl_AuditoriaListaVer", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias");
            DropForeignKey("dbo.Tbl_AuditoriaCronograma", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias");
            DropIndex("dbo.Tbl_AuditoriaInforme", new[] { "Pk_Id_Auditoria" });
            DropIndex("dbo.Tbl_AuditoriaListaVer", new[] { "Fk_Id_Auditoria" });
            DropIndex("dbo.Tbl_AuditoriaCronograma", new[] { "Fk_Id_Auditoria" });
            DropIndex("dbo.Tbl_Auditorias", new[] { "Fk_Id_Proceso" });
            DropIndex("dbo.Tbl_Auditorias", new[] { "Fk_Id_Programa" });
            DropIndex("dbo.Tbl_AuditoriaPrograma", new[] { "Fk_Id_Sede" });
            DropIndex("dbo.Tbl_AuditoriaPrograma", new[] { "Fk_Id_Empresa" });
            DropTable("dbo.Tbl_AuditoriaInforme");
            DropTable("dbo.Tbl_AuditoriaListaVer");
            DropTable("dbo.Tbl_AuditoriaCronograma");
            DropTable("dbo.Tbl_Auditorias");
            DropTable("dbo.Tbl_AuditoriaPrograma");
        }
    }
}
