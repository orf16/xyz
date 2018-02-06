namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacionCampoDiagnostico : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_EnfermedadesLaboralesDiagnosticadas", "Diagnostico", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_EnfermedadesLaboralesDiagnosticadas", "Diagnostico", c => c.Int(nullable: false));
        }
    }
}
