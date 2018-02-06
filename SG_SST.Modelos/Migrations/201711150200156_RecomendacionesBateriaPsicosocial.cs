namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecomendacionesBateriaPsicosocial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_BateriaUsuario", "Observaciones", c => c.String(maxLength: 4000));
            AddColumn("dbo.Tbl_BateriaUsuario", "Recomendaciones", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_BateriaUsuario", "Recomendaciones");
            DropColumn("dbo.Tbl_BateriaUsuario", "Observaciones");
        }
    }
}
