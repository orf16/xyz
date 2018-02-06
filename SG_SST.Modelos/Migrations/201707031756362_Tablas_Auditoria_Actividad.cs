namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablas_Auditoria_Actividad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_AuditoriaCronograma", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias");
            CreateTable(
                "dbo.Tbl_ActividadAuditoria",
                c => new
                    {
                        Pk_Id_Actividad = c.Int(nullable: false, identity: true),
                        Actividad = c.String(nullable: false, maxLength: 1000),
                        Responsable = c.String(nullable: false, maxLength: 500),
                        FechaFinalizacion = c.DateTime(nullable: false),
                        Fk_Id_Auditoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Actividad)
                .ForeignKey("dbo.Tbl_Auditorias", t => t.Fk_Id_Auditoria, cascadeDelete: true)
                .Index(t => t.Fk_Id_Auditoria);
            
            AddColumn("dbo.Tbl_AuditoriaListaVer", "Compromiso", c => c.String(maxLength: 3000));
            AddColumn("dbo.Tbl_AuditoriaListaVer", "Responsable", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaListaVer", "FechaCierre", c => c.DateTime());
            AddColumn("dbo.Tbl_AuditoriaPrograma", "Numero_Id_Responsable", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaPrograma", "Numero_Id_Copasst", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "Conclusiones", c => c.String(nullable: false, maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "NombreArchivoRes", c => c.String(maxLength: 200));
            AddColumn("dbo.Tbl_AuditoriaInforme", "RutaArchivoRes", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "FirmaScrImageRes", c => c.String());
            AddColumn("dbo.Tbl_AuditoriaInforme", "Nombre_Responsable", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "Numero_Id_Responsable", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "NombreArchivoAuditor", c => c.String(maxLength: 200));
            AddColumn("dbo.Tbl_AuditoriaInforme", "RutaArchivoAuditor", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "FirmaScrImageAuditor", c => c.String());
            AddColumn("dbo.Tbl_AuditoriaInforme", "Nombre_Auditor", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaInforme", "Numero_Id_Auditor", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Nombre_Responsable", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Nombre_Copasst", c => c.String(maxLength: 1000));
            AddForeignKey("dbo.Tbl_AuditoriaCronograma", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias", "Pk_Id_Auditoria");
            DropColumn("dbo.Tbl_AuditoriaPrograma", "Cargo_Responsable");
            DropColumn("dbo.Tbl_AuditoriaPrograma", "Cargo_Copasst");
            DropColumn("dbo.Tbl_AuditoriaInforme", "Ubicacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_AuditoriaInforme", "Ubicacion", c => c.String(nullable: false, maxLength: 1000));
            AddColumn("dbo.Tbl_AuditoriaPrograma", "Cargo_Copasst", c => c.String());
            AddColumn("dbo.Tbl_AuditoriaPrograma", "Cargo_Responsable", c => c.String());
            DropForeignKey("dbo.Tbl_AuditoriaCronograma", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias");
            DropForeignKey("dbo.Tbl_ActividadAuditoria", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias");
            DropIndex("dbo.Tbl_ActividadAuditoria", new[] { "Fk_Id_Auditoria" });
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Nombre_Copasst", c => c.String());
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Nombre_Responsable", c => c.String());
            DropColumn("dbo.Tbl_AuditoriaInforme", "Numero_Id_Auditor");
            DropColumn("dbo.Tbl_AuditoriaInforme", "Nombre_Auditor");
            DropColumn("dbo.Tbl_AuditoriaInforme", "FirmaScrImageAuditor");
            DropColumn("dbo.Tbl_AuditoriaInforme", "RutaArchivoAuditor");
            DropColumn("dbo.Tbl_AuditoriaInforme", "NombreArchivoAuditor");
            DropColumn("dbo.Tbl_AuditoriaInforme", "Numero_Id_Responsable");
            DropColumn("dbo.Tbl_AuditoriaInforme", "Nombre_Responsable");
            DropColumn("dbo.Tbl_AuditoriaInforme", "FirmaScrImageRes");
            DropColumn("dbo.Tbl_AuditoriaInforme", "RutaArchivoRes");
            DropColumn("dbo.Tbl_AuditoriaInforme", "NombreArchivoRes");
            DropColumn("dbo.Tbl_AuditoriaInforme", "Conclusiones");
            DropColumn("dbo.Tbl_AuditoriaPrograma", "Numero_Id_Copasst");
            DropColumn("dbo.Tbl_AuditoriaPrograma", "Numero_Id_Responsable");
            DropColumn("dbo.Tbl_AuditoriaListaVer", "FechaCierre");
            DropColumn("dbo.Tbl_AuditoriaListaVer", "Responsable");
            DropColumn("dbo.Tbl_AuditoriaListaVer", "Compromiso");
            DropTable("dbo.Tbl_ActividadAuditoria");
            AddForeignKey("dbo.Tbl_AuditoriaCronograma", "Fk_Id_Auditoria", "dbo.Tbl_Auditorias", "Pk_Id_Auditoria", cascadeDelete: true);
        }
    }
}
