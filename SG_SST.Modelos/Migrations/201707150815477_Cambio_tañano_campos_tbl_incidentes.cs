namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio_tañano_campos_tbl_incidentes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_Incidentes", "Persona_primer_apellido", c => c.String(maxLength: 50));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_segundo_apellido", c => c.String(maxLength: 50));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_primer_nombre", c => c.String(maxLength: 50));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_segundo_nombre", c => c.String(maxLength: 50));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_direccion", c => c.String(maxLength: 50));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_ocupacion_habitual", c => c.String(maxLength: 500));
            AlterColumn("dbo.Tbl_Incidentes", "Incidente_nombre_labor", c => c.String(maxLength: 500));
            AlterColumn("dbo.Tbl_Incidentes", "Incidente_sitio_incidente_otro", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_Incidentes", "Incidente_sitio_incidente_otro", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Incidente_nombre_labor", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_ocupacion_habitual", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_direccion", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_segundo_nombre", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_primer_nombre", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_segundo_apellido", c => c.String(maxLength: 20));
            AlterColumn("dbo.Tbl_Incidentes", "Persona_primer_apellido", c => c.String(maxLength: 20));
        }
    }
}
