namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tbls_Accion_Segundo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tbl_Acciones", "Firma_Auditor");
            DropColumn("dbo.Tbl_Acciones", "Firma_Responsable");
            AddColumn("dbo.Tbl_Acciones", "NombreArchivoAuditor", c => c.String(maxLength: 300));
            AddColumn("dbo.Tbl_Acciones", "RutaArchivoAuditor", c => c.String(maxLength: 300));
            AddColumn("dbo.Tbl_Acciones", "NombreArchivoResp", c => c.String(maxLength: 300));
            AddColumn("dbo.Tbl_Acciones", "RutaArchivoResp", c => c.String(maxLength: 300));

            DropColumn("dbo.Tbl_Seguimiento", "RutaFirma");
            AddColumn("dbo.Tbl_Seguimiento", "RutaArchivoSeg", c => c.String(maxLength: 300));
            AddColumn("dbo.Tbl_Seguimiento", "NombreArchivoSeg", c => c.String(maxLength: 300));

            DropColumn("dbo.Tbl_ActividadAccion", "RutaFirma");
            AddColumn("dbo.Tbl_ActividadAccion", "NombreArchivoAct", c => c.String(maxLength: 300));
            AddColumn("dbo.Tbl_ActividadAccion", "RutaArchivoAct", c => c.String(maxLength: 300));
            


            AlterColumn("dbo.Tbl_Acciones", "NombreArchivoResp", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_Acciones", "RutaArchivoResp", c => c.String(maxLength: 1100));
            AlterColumn("dbo.Tbl_Acciones", "Nombre_Responsable", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_Acciones", "Cargo_Responsable", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_ActividadAccion", "NombreArchivoAct", c => c.String(maxLength: 1100));
            AlterColumn("dbo.Tbl_ActividadAccion", "RutaArchivoAct", c => c.String(maxLength: 1100));
            AlterColumn("dbo.Tbl_ArchivosAccion", "NombreArchivo", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Tbl_ArchivosAccion", "Ruta", c => c.String(nullable: false, maxLength: 220));
            AlterColumn("dbo.Tbl_Seguimiento", "Observaciones", c => c.String(maxLength: 1100));
            AlterColumn("dbo.Tbl_Seguimiento", "NombreArchivoSeg", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tbl_Seguimiento", "RutaArchivoSeg", c => c.String(maxLength: 1200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Seguimiento", "RutaArchivoSeg", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_Seguimiento", "NombreArchivoSeg", c => c.String(maxLength: 200));
            AlterColumn("dbo.Tbl_Seguimiento", "Observaciones", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_ArchivosAccion", "Ruta", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Tbl_ArchivosAccion", "NombreArchivo", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Tbl_ActividadAccion", "RutaArchivoAct", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_ActividadAccion", "NombreArchivoAct", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_Acciones", "Cargo_Responsable", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "Nombre_Responsable", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "RutaArchivoResp", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_Acciones", "NombreArchivoResp", c => c.String(maxLength: 200));
        }
    }
}
