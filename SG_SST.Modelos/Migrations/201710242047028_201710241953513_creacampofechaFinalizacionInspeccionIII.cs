namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201710241953513_creacampofechaFinalizacionInspeccionIII : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Inspecciones", "Fecha_Realizacion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Inspecciones", "Fecha_Realizacion");
        }
    }
}
