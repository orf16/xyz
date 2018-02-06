namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionarCampoEdadPerfilSocioDemografico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Edad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Edad");
        }
    }
}
