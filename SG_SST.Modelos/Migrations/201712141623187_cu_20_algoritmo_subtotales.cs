namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_20_algoritmo_subtotales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_vul_eme_IdentificacionAmenazas", "subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_vul_eme_Personas", "subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_vul_eme_Recursos", "subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_vul_eme_sistemas_procesos", "subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_vul_eme_sistemas_procesos", "subtotal");
            DropColumn("dbo.Tbl_vul_eme_Recursos", "subtotal");
            DropColumn("dbo.Tbl_vul_eme_Personas", "subtotal");
            DropColumn("dbo.Tbl_vul_eme_IdentificacionAmenazas", "subtotal");
        }
    }
}
