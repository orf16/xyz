namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_usuarios_susritos_app : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ComunicacionesUsuariosSuscritosAPP",
                c => new
                    {
                        PK_Id_Suscrito = c.Int(nullable: false, identity: true),
                        IdentificacionUsuario = c.String(),
                        PlayerID = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_Suscrito);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_ComunicacionesUsuariosSuscritosAPP");
        }
    }
}
