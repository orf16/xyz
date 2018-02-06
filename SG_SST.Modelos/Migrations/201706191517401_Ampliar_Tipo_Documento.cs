namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ampliar_Tipo_Documento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Tipo_Documento", "AplicaPersonas", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tbl_Tipo_Documento", "AplicaEmpresas", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Tipo_Documento", "AplicaEmpresas");
            DropColumn("dbo.Tbl_Tipo_Documento", "AplicaPersonas");
        }
    }
}
