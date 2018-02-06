namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio_adicional_tbl_incidentes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_Incidentes", "Persona_numero_identificacion", c => c.String(maxLength: 15));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_genero", c => c.String(maxLength: 1));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_telefono", c => c.String(maxLength: 15));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_direccion", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_ocupacion_habitual", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Incidente_descripcion", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Incidentes", "Incidente_descripcion", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_ocupacion_habitual", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_direccion", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_telefono", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_genero", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_numero_identificacion", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
