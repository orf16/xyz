namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajustegestcambiofkclspelfkpelmodoct131017 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropIndex("dbo.Tbl_GestionDelCambio", new[] { "FK_Clasificacion_De_Peligro" });
            AddColumn("dbo.Tbl_GestionDelCambio", "FK_Tipo_De_Peligro", c => c.Int(nullable: false));
            DropTable("dbo.Tbl_ComunicadosAPP");
            DropTable("dbo.Tbl_EstadosComunicadosAPP");
            DropTable("dbo.Tbl_UsuarioComunicadoAPP");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tbl_UsuarioComunicadoAPP",
                c => new
                    {
                        PK_Id_Mensaje = c.Int(nullable: false, identity: true),
                        FK_Id_ComunicadosAPP = c.Int(nullable: false),
                        PlayerID = c.String(),
                        IdentificacionUsuario = c.String(),
                        IDEstadoComunicado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_Mensaje);

            CreateTable(
                "dbo.Tbl_EstadosComunicadosAPP",
                c => new
                    {
                        PK_Id_EstadoComunicado = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Valor = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_EstadoComunicado);

            CreateTable(
                "dbo.Tbl_ComunicadosAPP",
                c => new
                    {
                        IDComunicadosAPP = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Asunto = c.String(),
                        Destinatarios = c.String(),
                        FechaCreacion = c.String(),
                        FechaEnvio = c.String(),
                    })
                .PrimaryKey(t => t.IDComunicadosAPP);
            
            DropColumn("dbo.Tbl_GestionDelCambio", "FK_Tipo_De_Peligro");
            CreateIndex("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro");
            AddForeignKey("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro", "PK_Clasificacion_De_Peligro", cascadeDelete: true);
        }
    }
}
