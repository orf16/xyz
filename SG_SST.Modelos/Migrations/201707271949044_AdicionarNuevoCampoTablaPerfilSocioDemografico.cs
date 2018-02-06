namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarNuevoCampoTablaPerfilSocioDemografico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "OtroPeligro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "OtroPeligro");
        }
    }
}
