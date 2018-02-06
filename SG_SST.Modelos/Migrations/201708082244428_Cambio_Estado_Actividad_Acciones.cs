namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio_Estado_Actividad_Acciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ActividadAccion", "Estado", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ActividadAccion", "Estado");
        }
    }
}
