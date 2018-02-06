namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creaciontablaconfiguracioninspeccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Configuracion_Inspeccion",
                c => new
                    {
                        Pk_Id_ConfiguracionInspeccion = c.Int(nullable: false, identity: true),
                        DescripcionPrioridadConf = c.String(),
                        DiasDesdeConfig = c.Int(nullable: false),
                        DiasHastaConfig = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_ConfiguracionInspeccion);
            
            AddColumn("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion", c => c.Int());
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion");
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion", "dbo.Tbl_Configuracion_Inspeccion", "Pk_Id_ConfiguracionInspeccion");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion", "dbo.Tbl_Configuracion_Inspeccion");
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion" });
            DropColumn("dbo.Tbl_ConfiguracionporInspeccion", "ConfiguracionInspeccion_Pk_Id_ConfiguracionInspeccion");
            DropTable("dbo.Tbl_Configuracion_Inspeccion");
        }
    }
}
