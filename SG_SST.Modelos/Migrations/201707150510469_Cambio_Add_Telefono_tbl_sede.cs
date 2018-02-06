namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio_Add_Telefono_tbl_sede : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Sede", "Telefono", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Sede", "Telefono");
        }
    }
}
