namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacion_tabla_auditoria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_Auditorias", "Periodo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Auditorias", "Periodo", c => c.Int(nullable: false));
        }
    }
}
