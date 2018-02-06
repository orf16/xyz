namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio_Tabla_Acciones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_Acciones", "Halla_Num_Doc", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_Acciones", "Halla_Nombre", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_Acciones", "Halla_TipoDoc", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_Acciones", "Halla_Cargo", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_Acciones", "Causa_Raiz", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Tbl_Acciones", "Verificacion", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_Acciones", "NombreArchivoAuditor", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_Acciones", "Nombre_Auditor", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tbl_Acciones", "Cargo_Auditor", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tbl_Acciones", "RutaArchivoResp", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_Acciones", "Nombre_Responsable", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tbl_Acciones", "Cargo_Responsable", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Acciones", "Cargo_Responsable", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_Acciones", "Nombre_Responsable", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_Acciones", "RutaArchivoResp", c => c.String(maxLength: 1100));
            AlterColumn("dbo.Tbl_Acciones", "Cargo_Auditor", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "Nombre_Auditor", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "NombreArchivoAuditor", c => c.String(maxLength: 200));
            AlterColumn("dbo.Tbl_Acciones", "Verificacion", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Tbl_Acciones", "Causa_Raiz", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Tbl_Acciones", "Halla_Cargo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "Halla_TipoDoc", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "Halla_Nombre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tbl_Acciones", "Halla_Num_Doc", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
