namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Incidente_Actualizacion_General : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Incidentes", "General_actividad_economica_id", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "General_actividad_economica", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_codigo", c => c.String(maxLength: 7));
            AddColumn("dbo.Tbl_Incidentes", "General_razon_social", c => c.String(maxLength: 150));
            AddColumn("dbo.Tbl_Incidentes", "FK_id_tipo_documento_general", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "General_numero_identificación", c => c.String(maxLength: 15));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_direccion", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_telefono", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_correo_electronico", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento_id", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_zona_id", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_zona", c => c.String(maxLength: 1));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_municipio_id", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_municipio", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "Persona_departamento_id", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "Persona_municipio_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Incidentes", "FK_id_tipo_documento_general");
            AddForeignKey("dbo.Tbl_Incidentes", "FK_id_tipo_documento_general", "dbo.Tbl_Tipo_Documento", "PK_IDTipo_Documento", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_tipo_documento_general", "dbo.Tbl_Tipo_Documento");
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_tipo_documento_general" });
            DropColumn("dbo.Tbl_Incidentes", "Persona_municipio_id");
            DropColumn("dbo.Tbl_Incidentes", "Persona_departamento_id");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_municipio");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_municipio_id");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_zona");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_zona_id");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento_id");
            DropColumn("dbo.Tbl_Incidentes", "General_correo_electronico");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_telefono");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_direccion");
            DropColumn("dbo.Tbl_Incidentes", "General_numero_identificación");
            DropColumn("dbo.Tbl_Incidentes", "FK_id_tipo_documento_general");
            DropColumn("dbo.Tbl_Incidentes", "General_razon_social");
            DropColumn("dbo.Tbl_Incidentes", "General_codigo");
            DropColumn("dbo.Tbl_Incidentes", "General_actividad_economica");
            DropColumn("dbo.Tbl_Incidentes", "General_actividad_economica_id");
        }
    }
}
