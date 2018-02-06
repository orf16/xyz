namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacion_Tabla_Perfil_Socio_Democratico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Descripcion_Peligro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Descripcion_Peligro");
        }
    }
}
