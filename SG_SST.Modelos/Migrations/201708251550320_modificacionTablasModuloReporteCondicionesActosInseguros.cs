namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacionTablasModuloReporteCondicionesActosInseguros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ActividadesActosInseguros", "NombreActividad", c => c.String());
            DropColumn("dbo.Tbl_Reportes", "Id_Consecutivo_Reportes");
            DropColumn("dbo.Tbl_Reportes", "Nombre_Quien_Reporta");
            DropColumn("dbo.Tbl_Reportes", "Cargo_Quien_Reporta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Reportes", "Cargo_Quien_Reporta", c => c.String());
            AddColumn("dbo.Tbl_Reportes", "Nombre_Quien_Reporta", c => c.String());
            AddColumn("dbo.Tbl_Reportes", "Id_Consecutivo_Reportes", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_ActividadesActosInseguros", "NombreActividad");
        }
    }
}
