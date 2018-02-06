namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarCampoPerfilSocioDemografico : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Descripcion_Peligro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Descripcion_Peligro", c => c.String());
        }
    }
}
