namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class algoritmoincidentes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_IncidentesAT", "TipoIdentificacionII", c => c.String());
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaUrbanaII", c => c.String());
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaII", c => c.String());
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaIII", c => c.String());
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaIV", c => c.String());
            AlterColumn("dbo.Tbl_IncidentesAT", "TipoIdentificacionXI", c => c.String());
            AlterColumn("dbo.Tbl_IncidentesAT", "TipoIdentificacionXII", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_IncidentesAT", "TipoIdentificacionXII", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_IncidentesAT", "TipoIdentificacionXI", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaIV", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaIII", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaII", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_IncidentesAT", "ZonaUrbanaII", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_IncidentesAT", "TipoIdentificacionII", c => c.Boolean(nullable: false));
        }
    }
}
