namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificarPerfilSocioDemograficoRelaciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Ciudad", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Ciudad");
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Ciudad", "dbo.Tbl_Municipio", "Pk_Id_Municipio", cascadeDelete: true);
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Departamento");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ciudad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ciudad", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Departamento", c => c.String());
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Ciudad", "dbo.Tbl_Municipio");
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Ciudad" });
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Ciudad");
        }
    }
}
