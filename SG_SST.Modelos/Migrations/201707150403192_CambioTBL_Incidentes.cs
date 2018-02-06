namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioTBL_Incidentes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_usuariosistema_persona", "dbo.Tbl_UsuarioSistema");
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_usuariosistema_persona" });
            AddColumn("dbo.Tbl_Incidentes", "Dia_Semana_Incidente", c => c.String());
            DropColumn("dbo.Tbl_Incidentes", "General_usuario_empresa_nit");
            DropColumn("dbo.Tbl_Incidentes", "General_actividad_economica");
            DropColumn("dbo.Tbl_Incidentes", "General_codigo");
            DropColumn("dbo.Tbl_Incidentes", "General_razon_social");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_direccion");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_telefono");
            DropColumn("dbo.Tbl_Incidentes", "General_correo_electronico");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento_id");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_zona");
            DropColumn("dbo.Tbl_Incidentes", "General_sede_principal_municipio");
            DropColumn("dbo.Tbl_Incidentes", "Persona_departamento");
            DropColumn("dbo.Tbl_Incidentes", "Persona_municipio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Incidentes", "Persona_municipio", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "Persona_departamento", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_municipio", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_zona", c => c.String(maxLength: 1));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_departamento_id", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Incidentes", "General_correo_electronico", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_telefono", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_sede_principal_direccion", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_razon_social", c => c.String(maxLength: 150));
            AddColumn("dbo.Tbl_Incidentes", "General_codigo", c => c.String(maxLength: 7));
            AddColumn("dbo.Tbl_Incidentes", "General_actividad_economica", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_Incidentes", "General_usuario_empresa_nit", c => c.String());
            DropColumn("dbo.Tbl_Incidentes", "Dia_Semana_Incidente");
            CreateIndex("dbo.Tbl_Incidentes", "FK_id_usuariosistema_persona");
            AddForeignKey("dbo.Tbl_Incidentes", "FK_id_usuariosistema_persona", "dbo.Tbl_UsuarioSistema", "Pk_Id_UsuarioSistema", cascadeDelete: true);
        }
    }
}