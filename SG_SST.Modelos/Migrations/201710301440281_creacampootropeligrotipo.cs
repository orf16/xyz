namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creacampootropeligrotipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_CondicionInsegura", "OtroTipoPeligro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_CondicionInsegura", "OtroTipoPeligro");
        }
    }
}
