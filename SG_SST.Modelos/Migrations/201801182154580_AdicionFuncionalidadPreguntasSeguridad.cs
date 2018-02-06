namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionFuncionalidadPreguntasSeguridad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_PreguntasSeguridad",
                c => new
                    {
                        Pk_Id_PreguntaSeguridad = c.Int(nullable: false, identity: true),
                        NombrePreguntaSeguridad = c.String(),
                        Descricion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_PreguntaSeguridad);
            
            CreateTable(
                "dbo.Tbl_RespuestasAPreguntasSeguridad",
                c => new
                    {
                        Pk_Id_RespuestaAPreguntaSeguridad = c.Int(nullable: false, identity: true),
                        Fk_Id_PreguntaSeguridad = c.Int(nullable: false),
                        RespuestareguntaSeguridad = c.String(),
                        CodUsuarioAprobar = c.Int(),
                        CodUsuarioSistema = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_RespuestaAPreguntaSeguridad)
                .ForeignKey("dbo.Tbl_PreguntasSeguridad", t => t.Fk_Id_PreguntaSeguridad, cascadeDelete: true)
                .Index(t => t.Fk_Id_PreguntaSeguridad);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_RespuestasAPreguntasSeguridad", "Fk_Id_PreguntaSeguridad", "dbo.Tbl_PreguntasSeguridad");
            DropIndex("dbo.Tbl_RespuestasAPreguntasSeguridad", new[] { "Fk_Id_PreguntaSeguridad" });
            DropTable("dbo.Tbl_RespuestasAPreguntasSeguridad");
            DropTable("dbo.Tbl_PreguntasSeguridad");
        }
    }
}
