namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gestioncambiocampoOtroTblgestioncambiomig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_GestionDelCambio", "Otro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_GestionDelCambio", "Otro");
        }
    }
}
