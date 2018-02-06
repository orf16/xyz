namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_algoritmo_11202017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "calificacion_personas", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "interpretacion_personas", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "color_personas", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "calificacion_recursos", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "interpretacion_recursos", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "color_recursos", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "calificacion_sistemas_procesos", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "interpretacion_sistemas_procesos", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "color_sistemas_procesos", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "color_sistemas_procesos");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "interpretacion_sistemas_procesos");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "calificacion_sistemas_procesos");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "color_recursos");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "interpretacion_recursos");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "calificacion_recursos");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "color_personas");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "interpretacion_personas");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "calificacion_personas");
        }
    }
}
