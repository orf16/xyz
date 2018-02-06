namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablaObjetivosst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Objetivos_SST",
                c => new
                    {
                        PK_Id_Objetivo_Empresa = c.Int(nullable: false, identity: true),
                        FK_Id_Empresa = c.Int(nullable: false),
                        Objetivo = c.String(),
                        Meta = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_Objetivo_Empresa);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_Objetivos_SST");
        }
    }
}
