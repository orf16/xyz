namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajustetblrequisitoslgalesposipediaFKEmpresa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_Requisitos_Legales_Posipedia", new[] { "FK_Empresa" });
            DropColumn("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Empresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Empresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Empresa");
            AddForeignKey("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
        }
    }
}
