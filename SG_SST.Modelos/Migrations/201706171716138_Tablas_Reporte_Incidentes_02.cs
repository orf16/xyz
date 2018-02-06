namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablas_Reporte_Incidentes_02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_Incidente_Consecuencia", "Nombre_consecuencia", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Tbl_Sitio_Incidente", "Nombre_Sitio", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Sitio_Incidente", "Nombre_Sitio", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Tbl_Incidente_Consecuencia", "Nombre_consecuencia", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
