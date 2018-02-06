namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacion_EmpresaTercero : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", "dbo.Tbl_EmpresaTercero");
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_EmpresaTercero" });
            DropPrimaryKey("dbo.Tbl_EmpresaTercero");
            AddColumn("dbo.Tbl_EmpresaTercero", "PK_Nit_Empresa", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Tbl_EmpresaTercero", "PK_Nit_Empresa");
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero");
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", "dbo.Tbl_EmpresaTercero", "PK_Nit_Empresa");
            DropColumn("dbo.Tbl_EmpresaTercero", "Pk_Id_Empresa");
            DropColumn("dbo.Tbl_EmpresaTercero", "Nit_Empresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_EmpresaTercero", "Nit_Empresa", c => c.String());
            AddColumn("dbo.Tbl_EmpresaTercero", "Pk_Id_Empresa", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", "dbo.Tbl_EmpresaTercero");
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_EmpresaTercero" });
            DropPrimaryKey("dbo.Tbl_EmpresaTercero");
            AlterColumn("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_EmpresaTercero", "PK_Nit_Empresa");
            AddPrimaryKey("dbo.Tbl_EmpresaTercero", "Pk_Id_Empresa");
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero");
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", "dbo.Tbl_EmpresaTercero", "Pk_Id_Empresa", cascadeDelete: true);
        }
    }
}
