namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio_Campo_ProgramaAuditoria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Periodicidad", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_AuditoriaPrograma", "Periodicidad", c => c.Int(nullable: false));
        }
    }
}
