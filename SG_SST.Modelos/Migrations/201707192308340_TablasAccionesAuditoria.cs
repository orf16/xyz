namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablasAccionesAuditoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Acciones", "Estado", c => c.String(maxLength: 50));
            AlterColumn("dbo.Tbl_Auditorias", "Periodo", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Periodicidad", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_AuditoriaInforme", "FechaRealizacion", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_AuditoriaInforme", "FechaRealizacion", c => c.String(nullable: false));
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Periodicidad", c => c.String(nullable: false));
            AlterColumn("dbo.Tbl_Auditorias", "Periodo", c => c.String(nullable: false));
            DropColumn("dbo.Tbl_Acciones", "Estado");
        }
    }
}
