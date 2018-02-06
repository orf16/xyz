namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacionOcupacionTablasPerfilSocioDemogrico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Ocupaciones_De_Perfil",
                c => new
                    {
                        PK_OcupacionPerfil = c.Int(nullable: false, identity: true),
                        codigo = c.String(),
                        grupoPrimario = c.String(),
                    })
                .PrimaryKey(t => t.PK_OcupacionPerfil);
            
            AddColumn("dbo.Tbl_Reportes", "ConsecutivoReporte", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "tiempoExpos", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil");
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil", "dbo.Tbl_Ocupaciones_De_Perfil", "PK_OcupacionPerfil", cascadeDelete: true);
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupacion");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FactorRiesgoPeligro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FactorRiesgoPeligro", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupacion", c => c.String());
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil", "dbo.Tbl_Ocupaciones_De_Perfil");
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_OcupacionPerfil" });
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "tiempoExpos");
            DropColumn("dbo.Tbl_Reportes", "ConsecutivoReporte");
            DropTable("dbo.Tbl_Ocupaciones_De_Perfil");
        }
    }
}
