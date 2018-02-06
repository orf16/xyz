namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_20_add_columns_allgoritmo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Eme_Georeferenciacion", "ubicacion_hidrantes", c => c.String());
            AddColumn("dbo.Tbl_Eme_Georeferenciacion", "punto_encuentro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Eme_Georeferenciacion", "punto_encuentro");
            DropColumn("dbo.Tbl_Eme_Georeferenciacion", "ubicacion_hidrantes");
        }
    }
}
