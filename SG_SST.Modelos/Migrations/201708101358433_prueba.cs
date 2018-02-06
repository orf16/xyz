namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prueba : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ActividadAccion", "Estado", c => c.Byte(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Edad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Edad");
            DropColumn("dbo.Tbl_ActividadAccion", "Estado");
        }
    }
}
