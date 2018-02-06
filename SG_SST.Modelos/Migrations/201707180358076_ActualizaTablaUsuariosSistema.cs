namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaTablaUsuariosSistema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_UsuarioSistema", "PeriodoInactivacionCuenta", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_UsuarioSistema", "PeriodoInactivacionCuenta");
        }
    }
}
