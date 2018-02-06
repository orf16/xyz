namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambionombreconfiguracion : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tbl_ConfiguracionposInspeccion", newName: "Tbl_ConfiguracionporInspeccion");
            RenameTable(name: "dbo.MaestroConfiguracionPrioridades", newName: "Tbl_Maestro_Configuracion_Prioridad");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tbl_Maestro_Configuracion_Prioridad", newName: "MaestroConfiguracionPrioridades");
            RenameTable(name: "dbo.Tbl_ConfiguracionporInspeccion", newName: "Tbl_ConfiguracionposInspeccion");
        }
    }
}
