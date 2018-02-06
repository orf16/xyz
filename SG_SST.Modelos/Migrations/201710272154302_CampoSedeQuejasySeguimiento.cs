namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoSedeQuejasySeguimiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ActaConvivenciaQuejas", "Fk_Id_Sede", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_SeguimientoActaConvivencia", "Fk_Id_Sede", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_SeguimientoActaConvivencia", "Fk_Id_Sede");
            DropColumn("dbo.Tbl_ActaConvivenciaQuejas", "Fk_Id_Sede");
        }
    }
}
