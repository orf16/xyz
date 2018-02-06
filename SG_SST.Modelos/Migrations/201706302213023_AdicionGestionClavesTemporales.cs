namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionGestionClavesTemporales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_PasswordsTemporalesUsuariosSistema",
                c => new
                    {
                        Pk_Id_PasswordTemporal = c.Int(nullable: false, identity: true),
                        Fk_Id_UsuarioSistema = c.Int(nullable: false),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_PasswordTemporal)
                .ForeignKey("dbo.Tbl_UsuarioSistema", t => t.Fk_Id_UsuarioSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_UsuarioSistema);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_PasswordsTemporalesUsuariosSistema", "Fk_Id_UsuarioSistema", "dbo.Tbl_UsuarioSistema");
            DropIndex("dbo.Tbl_PasswordsTemporalesUsuariosSistema", new[] { "Fk_Id_UsuarioSistema" });
            DropTable("dbo.Tbl_PasswordsTemporalesUsuariosSistema");
        }
    }
}
