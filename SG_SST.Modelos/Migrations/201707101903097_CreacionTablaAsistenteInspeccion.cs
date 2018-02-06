namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionTablaAsistenteInspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Asistentes",
                c => new
                    {
                        Pk_Id_Asistente = c.Int(nullable: false, identity: true),
                        Nombre_Asistente = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Asistente);
            
            CreateTable(
                "dbo.Tbl_AsistentesporInspeccion",
                c => new
                    {
                        Pk_Id_AsistenteporInspeccion = c.Int(nullable: false, identity: true),
                        Fk_Id_Asistente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_AsistenteporInspeccion)
                .ForeignKey("dbo.Tbl_Asistentes", t => t.Fk_Id_Asistente, cascadeDelete: true)
                .Index(t => t.Fk_Id_Asistente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_AsistentesporInspeccion", "Fk_Id_Asistente", "dbo.Tbl_Asistentes");
            DropIndex("dbo.Tbl_AsistentesporInspeccion", new[] { "Fk_Id_Asistente" });
            DropTable("dbo.Tbl_AsistentesporInspeccion");
            DropTable("dbo.Tbl_Asistentes");
        }
    }
}
