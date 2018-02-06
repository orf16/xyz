namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsuariosSistema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_UsuarioSistema",
                c => new
                    {
                        Pk_Id_UsuarioSistema = c.Int(nullable: false, identity: true),
                        Documento = c.String(),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                        Correo = c.String(),
                        Clave = c.String(),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_UsuarioSistema);
            
            CreateTable(
                "dbo.Tbl_UsuarioSistemaEmpresa",
                c => new
                    {
                        Pk_Id_UsuarioSistemaEmpresa = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        Fk_Id_UsuarioSistema = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_UsuarioSistemaEmpresa)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_UsuarioSistema", t => t.Fk_Id_UsuarioSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa)
                .Index(t => t.Fk_Id_UsuarioSistema);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_UsuarioSistemaEmpresa", "Fk_Id_UsuarioSistema", "dbo.Tbl_UsuarioSistema");
            DropForeignKey("dbo.Tbl_UsuarioSistemaEmpresa", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_UsuarioSistemaEmpresa", new[] { "Fk_Id_UsuarioSistema" });
            DropIndex("dbo.Tbl_UsuarioSistemaEmpresa", new[] { "Fk_Id_Empresa" });
            DropTable("dbo.Tbl_UsuarioSistemaEmpresa");
            DropTable("dbo.Tbl_UsuarioSistema");
        }
    }
}
