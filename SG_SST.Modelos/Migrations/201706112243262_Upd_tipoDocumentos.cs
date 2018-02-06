namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd_tipoDocumentos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Tipo_Documento", "Sigla", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Tipo_Documento", "Sigla");
        }
    }
}
