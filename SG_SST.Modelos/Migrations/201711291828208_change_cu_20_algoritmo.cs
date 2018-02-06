namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_cu_20_algoritmo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Eme_FrentesAccionAdjuntos", "plan_evacuacion", c => c.String());
            AlterColumn("dbo.Tbl_Eme_FrentesAccionAdjuntos", "fk_id_sede", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Eme_FrentesAccionAdjuntos", "fk_id_sede", c => c.String());
            DropColumn("dbo.Tbl_Eme_FrentesAccionAdjuntos", "plan_evacuacion");
        }
    }
}
