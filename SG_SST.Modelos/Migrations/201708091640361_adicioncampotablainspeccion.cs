namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicioncampotablainspeccion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Inspecciones", "Descripcion_Tipo_Inspeccion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Inspecciones", "Descripcion_Tipo_Inspeccion");
        }
    }
}
