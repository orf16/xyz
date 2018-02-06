namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarNuevoCampoDxOtro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Otro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Otro");
        }
    }
}
