namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigAgregarFK_EmpresaTbl_MatrizRequisitosLegales : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_Requisitos_legales_Otros", new[] { "FK_Empresa" });
            AddColumn("dbo.Tbl_Matriz_RequisitosLegales", "FK_Empresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Matriz_RequisitosLegales", "FK_Empresa");
            AddForeignKey("dbo.Tbl_Matriz_RequisitosLegales", "FK_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "FK_Empresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "FK_Empresa", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tbl_Matriz_RequisitosLegales", "FK_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_Matriz_RequisitosLegales", new[] { "FK_Empresa" });
            DropColumn("dbo.Tbl_Matriz_RequisitosLegales", "FK_Empresa");
            CreateIndex("dbo.Tbl_Requisitos_legales_Otros", "FK_Empresa");
            AddForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
        }
    }
}
