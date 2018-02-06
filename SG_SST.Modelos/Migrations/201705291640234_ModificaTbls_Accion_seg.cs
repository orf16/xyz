namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificaTbls_Accion_seg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Acciones", "NombreArchivoAuditor", c => c.String(maxLength: 200));
            AddColumn("dbo.Tbl_Acciones", "RutaArchivoAuditor", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_Acciones", "NombreArchivoResp", c => c.String(maxLength: 200));
            AddColumn("dbo.Tbl_Acciones", "RutaArchivoResp", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_ActividadAccion", "NombreArchivoAct", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_ActividadAccion", "RutaArchivoAct", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tbl_Seguimiento", "NombreArchivoSeg", c => c.String(maxLength: 200));
            AddColumn("dbo.Tbl_Seguimiento", "RutaArchivoSeg", c => c.String(maxLength: 1000));
            DropColumn("dbo.Tbl_Acciones", "Firma_Auditor");
            DropColumn("dbo.Tbl_Acciones", "Firma_Responsable");
            DropColumn("dbo.Tbl_ActividadAccion", "RutaFirma");
            DropColumn("dbo.Tbl_Seguimiento", "RutaFirma");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Seguimiento", "RutaFirma", c => c.String());
            AddColumn("dbo.Tbl_ActividadAccion", "RutaFirma", c => c.String());
            AddColumn("dbo.Tbl_Acciones", "Firma_Responsable", c => c.Binary());
            AddColumn("dbo.Tbl_Acciones", "Firma_Auditor", c => c.Binary());
            DropColumn("dbo.Tbl_Seguimiento", "RutaArchivoSeg");
            DropColumn("dbo.Tbl_Seguimiento", "NombreArchivoSeg");
            DropColumn("dbo.Tbl_ActividadAccion", "RutaArchivoAct");
            DropColumn("dbo.Tbl_ActividadAccion", "NombreArchivoAct");
            DropColumn("dbo.Tbl_Acciones", "RutaArchivoResp");
            DropColumn("dbo.Tbl_Acciones", "NombreArchivoResp");
            DropColumn("dbo.Tbl_Acciones", "RutaArchivoAuditor");
            DropColumn("dbo.Tbl_Acciones", "NombreArchivoAuditor");
        }
    }
}
