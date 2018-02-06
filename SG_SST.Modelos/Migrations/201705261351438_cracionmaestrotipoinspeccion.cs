namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cracionmaestrotipoinspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Maestro_Tipo_Inspeccion",
                c => new
                    {
                        Pk_Id_Tipo_Inspeccion = c.Int(nullable: false, identity: true),
                        Descripcion_Tipo_Inspeccion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Tipo_Inspeccion);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_Maestro_Tipo_Inspeccion");
        }
    }
}
