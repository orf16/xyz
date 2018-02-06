namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiomigmartesperfil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "ZonaLugar", c => c.String());
            AlterColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "AntecedentesExpLaboral", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "AntecedentesExpLaboral", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "ZonaLugar");
        }
    }
}
