namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosTablasInspecciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_CondicionInsegura", "Estado_Condicion", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Inspecciones", "Id_Inspeccion", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Inspecciones", "Estado_Inspeccion", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Inspecciones", "Fk_IdEmpresa", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Inspecciones", "Area_Lugar", c => c.String());
            AddColumn("dbo.Tbl_Inspecciones", "Responsable_Lugar", c => c.String());
            AddColumn("dbo.Tbl_Planeacion_Inspeccion", "IdEmpresa", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Planeacion_Inspeccion", "EstadoPlaneacion", c => c.String());
            AddColumn("dbo.Tbl_Planeacion_Inspeccion", "ConsecutivoPlan", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_Inspecciones", "AreaLugar");
            DropColumn("dbo.Tbl_Inspecciones", "ResponsableLugar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Inspecciones", "ResponsableLugar", c => c.String());
            AddColumn("dbo.Tbl_Inspecciones", "AreaLugar", c => c.String());
            DropColumn("dbo.Tbl_Planeacion_Inspeccion", "ConsecutivoPlan");
            DropColumn("dbo.Tbl_Planeacion_Inspeccion", "EstadoPlaneacion");
            DropColumn("dbo.Tbl_Planeacion_Inspeccion", "IdEmpresa");
            DropColumn("dbo.Tbl_Inspecciones", "Responsable_Lugar");
            DropColumn("dbo.Tbl_Inspecciones", "Area_Lugar");
            DropColumn("dbo.Tbl_Inspecciones", "Fk_IdEmpresa");
            DropColumn("dbo.Tbl_Inspecciones", "Estado_Inspeccion");
            DropColumn("dbo.Tbl_Inspecciones", "Id_Inspeccion");
            DropColumn("dbo.Tbl_CondicionInsegura", "Estado_Condicion");
        }
    }
}
