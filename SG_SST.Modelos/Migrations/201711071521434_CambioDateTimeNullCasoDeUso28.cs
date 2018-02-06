namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioDateTimeNullCasoDeUso28 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_ProveedorContratista", "VigenciaContrato", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_ProveedorContratista", "VigenciaContrato", c => c.DateTime());
        }
    }
}
