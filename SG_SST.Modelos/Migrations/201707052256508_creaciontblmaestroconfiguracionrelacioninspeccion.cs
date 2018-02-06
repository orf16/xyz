namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creaciontblmaestroconfiguracionrelacioninspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfiguracionporInspeccions",
                c => new
                    {
                        Pk_Id_ConfiguracionPorInspeccion = c.Int(nullable: false, identity: true),
                        Fk_Id_MaestroConfiguracion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_ConfiguracionPorInspeccion)
                .ForeignKey("dbo.MaestroConfiguracionPrioridades", t => t.Fk_Id_MaestroConfiguracion, cascadeDelete: true)
                .Index(t => t.Fk_Id_MaestroConfiguracion);
            
            CreateTable(
                "dbo.MaestroConfiguracionPrioridades",
                c => new
                    {
                        Pk_Id_MaestroConfiguracion = c.Int(nullable: false, identity: true),
                        DescripcionPrioridad = c.String(),
                        DiasDesde = c.Int(nullable: false),
                        DiasHasta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_MaestroConfiguracion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConfiguracionporInspeccions", "Fk_Id_MaestroConfiguracion", "dbo.MaestroConfiguracionPrioridades");
            DropIndex("dbo.ConfiguracionporInspeccions", new[] { "Fk_Id_MaestroConfiguracion" });
            DropTable("dbo.MaestroConfiguracionPrioridades");
            DropTable("dbo.ConfiguracionporInspeccions");
        }
    }
}
