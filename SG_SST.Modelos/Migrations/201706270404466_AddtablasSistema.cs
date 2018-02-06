namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtablasSistema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ParametrosSistema",
                c => new
                    {
                        IdParametro = c.Int(nullable: false, identity: true),
                        NombreParametro = c.String(),
                        Valor = c.String(),
                    })
                .PrimaryKey(t => t.IdParametro);
            
            CreateTable(
                "dbo.Tbl_PlantillasCorreosSistema",
                c => new
                    {
                        IdPlantilla = c.Int(nullable: false, identity: true),
                        NombrePlantilla = c.String(),
                        Plantilla = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.IdPlantilla);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_PlantillasCorreosSistema");
            DropTable("dbo.Tbl_ParametrosSistema");
        }
    }
}
