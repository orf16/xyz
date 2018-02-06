namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarDatosAdicionales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_DatosAdicionalesUsuario",
                c => new
                    {
                        Pk_Id_DatoAdicionalUsuario = c.Int(nullable: false, identity: true),
                        NombreDato = c.String(),
                        ValorDato = c.String(),
                        CodUsuarioAprobar = c.Int(),
                        CodUsuarioSistema = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_DatoAdicionalUsuario);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_DatosAdicionalesUsuario");
        }
    }
}
