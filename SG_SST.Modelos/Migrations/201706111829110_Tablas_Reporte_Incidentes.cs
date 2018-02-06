namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablas_Reporte_Incidentes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Incidente_Consecuencia",
                c => new
                    {
                        Pk_Id_Incidente_Consecuencia = c.Int(nullable: false, identity: true),
                        Nombre_consecuencia = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Pk_Id_Incidente_Consecuencia);
            
            CreateTable(
                "dbo.Tbl_Incidentes",
                c => new
                    {
                        Pk_Id_Incidente = c.Int(nullable: false, identity: true),
                        General_usuario_empresa_nit = c.String(),
                        General_mismos_datos_sede_principal = c.Boolean(nullable: false),
                        FK_id_sede_general = c.Int(nullable: false),
                        FK_id_usuariosistema_persona = c.Int(nullable: false),
                        FK_id_vinculacionlaboral_persona = c.Int(nullable: false),
                        Persona_primer_apellido = c.String(maxLength: 20),
                        Persona_segundo_apellido = c.String(maxLength: 20),
                        Persona_primer_nombre = c.String(maxLength: 20),
                        Persona_segundo_nombre = c.String(maxLength: 20),
                        FK_id_tipo_documento_persona = c.Int(nullable: false),
                        Persona_numero_identificacion = c.String(nullable: false, maxLength: 15),
                        Persona_fecha_nacimiento = c.DateTime(nullable: false),
                        Persona_genero = c.String(nullable: false, maxLength: 1),
                        Persona_telefono = c.String(nullable: false, maxLength: 15),
                        Persona_departamento = c.String(nullable: false, maxLength: 20),
                        Persona_municipio = c.String(nullable: false, maxLength: 20),
                        FK_id_zonalugar_persona = c.Int(nullable: false),
                        Persona_ocupacion_habitual = c.String(nullable: false, maxLength: 20),
                        Persona_fecha_ingreso_empresa = c.DateTime(nullable: false),
                        FK_id_jornada_habitual_persona = c.Int(nullable: false),
                        Incidente_fecha = c.DateTime(nullable: false),
                        Incidente_jornada_normal = c.Boolean(nullable: false),
                        Incidente_realizaba_labor_habitual = c.Boolean(nullable: false),
                        Incidente_nombre_labor = c.String(maxLength: 20),
                        Incidente_tiempo_previo_al_incidente = c.Int(nullable: false),
                        FK_id_incidente_tipo_incidente = c.Int(nullable: false),
                        FK_id_departamento_incidente = c.Int(nullable: false),
                        FK_id_municipio_incidente = c.Int(nullable: false),
                        FK_id_zonalugar_incidente = c.Int(nullable: false),
                        Incidente_ocurre_dentro_empresa = c.Boolean(nullable: false),
                        FK_id_sitio_incidente = c.Int(nullable: false),
                        Incidente_sitio_incidente_otro = c.String(maxLength: 20),
                        FK_id_consecuencia_incidente = c.Int(nullable: false),
                        Incidente_descripcion = c.String(nullable: false, maxLength: 2000),
                        Incidente_fecha_diligenciamiento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Incidente)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_id_sede_general, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Incidente_Consecuencia", t => t.FK_id_consecuencia_incidente, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Departamento", t => t.FK_id_departamento_incidente, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Municipio", t => t.FK_id_municipio_incidente, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Sitio_Incidente", t => t.FK_id_sitio_incidente, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Tipo_Incidente", t => t.FK_id_incidente_tipo_incidente, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_ZonaLugar", t => t.FK_id_zonalugar_incidente, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Tipo_Documento", t => t.FK_id_tipo_documento_persona, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Tipo_Jornada", t => t.FK_id_jornada_habitual_persona, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_UsuarioSistema", t => t.FK_id_usuariosistema_persona, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_VinculacionLaboral", t => t.FK_id_vinculacionlaboral_persona, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_ZonaLugar", t => t.FK_id_zonalugar_persona, cascadeDelete: false)
                .Index(t => t.FK_id_sede_general)
                .Index(t => t.FK_id_usuariosistema_persona)
                .Index(t => t.FK_id_vinculacionlaboral_persona)
                .Index(t => t.FK_id_tipo_documento_persona)
                .Index(t => t.FK_id_zonalugar_persona)
                .Index(t => t.FK_id_jornada_habitual_persona)
                .Index(t => t.FK_id_incidente_tipo_incidente)
                .Index(t => t.FK_id_departamento_incidente)
                .Index(t => t.FK_id_municipio_incidente)
                .Index(t => t.FK_id_zonalugar_incidente)
                .Index(t => t.FK_id_sitio_incidente)
                .Index(t => t.FK_id_consecuencia_incidente);
            
            CreateTable(
                "dbo.Tbl_Sitio_Incidente",
                c => new
                    {
                        Pk_Id_Sitio_Incidente = c.Int(nullable: false, identity: true),
                        Nombre_Sitio = c.String(nullable: false, maxLength: 20),
                        EsOtro = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Sitio_Incidente);
            
            CreateTable(
                "dbo.Tbl_Tipo_Incidente",
                c => new
                    {
                        Pk_Id_Tipo_Incidente = c.Int(nullable: false, identity: true),
                        Nombre_Incidente = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Pk_Id_Tipo_Incidente);
            
            CreateTable(
                "dbo.Tbl_Tipo_Jornada",
                c => new
                    {
                        Pk_Id_Tipo_Jornada = c.Int(nullable: false, identity: true),
                        Nombre_Jornada = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Pk_Id_Tipo_Jornada);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_zonalugar_persona", "dbo.Tbl_ZonaLugar");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_vinculacionlaboral_persona", "dbo.Tbl_VinculacionLaboral");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_usuariosistema_persona", "dbo.Tbl_UsuarioSistema");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_jornada_habitual_persona", "dbo.Tbl_Tipo_Jornada");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_tipo_documento_persona", "dbo.Tbl_Tipo_Documento");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_zonalugar_incidente", "dbo.Tbl_ZonaLugar");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_incidente_tipo_incidente", "dbo.Tbl_Tipo_Incidente");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_sitio_incidente", "dbo.Tbl_Sitio_Incidente");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_municipio_incidente", "dbo.Tbl_Municipio");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_departamento_incidente", "dbo.Tbl_Departamento");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_consecuencia_incidente", "dbo.Tbl_Incidente_Consecuencia");
            DropForeignKey("dbo.Tbl_Incidentes", "FK_id_sede_general", "dbo.Tbl_Sede");
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_consecuencia_incidente" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_sitio_incidente" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_zonalugar_incidente" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_municipio_incidente" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_departamento_incidente" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_incidente_tipo_incidente" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_jornada_habitual_persona" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_zonalugar_persona" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_tipo_documento_persona" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_vinculacionlaboral_persona" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_usuariosistema_persona" });
            DropIndex("dbo.Tbl_Incidentes", new[] { "FK_id_sede_general" });
            DropTable("dbo.Tbl_Tipo_Jornada");
            DropTable("dbo.Tbl_Tipo_Incidente");
            DropTable("dbo.Tbl_Sitio_Incidente");
            DropTable("dbo.Tbl_Incidentes");
            DropTable("dbo.Tbl_Incidente_Consecuencia");
        }
    }
}
