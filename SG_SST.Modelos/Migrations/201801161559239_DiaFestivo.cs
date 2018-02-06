namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiaFestivo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_DiaFestivo",
                c => new
                    {
                        PK_Id_DiaFestivo = c.Int(nullable: false, identity: true),
                        Anio = c.Int(nullable: false),
                        Mes = c.Int(nullable: false),
                        Dia = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_DiaFestivo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_DiaFestivo");
        }
    }
}
