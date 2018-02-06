namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crear_comunicados_app : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_UsuarioComunicadoAPP");
            DropTable("dbo.Tbl_EstadosComunicadosAPP");
            DropTable("dbo.Tbl_ComunicadosAPP");
        }
    }
}
