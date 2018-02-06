namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajuste_Empleado_Tercero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_EmpleadoTercero", "Genero", c => c.String(maxLength: 1));
            AddColumn("dbo.Tbl_EmpleadoTercero", "Direccion", c => c.String(maxLength: 20));
            AddColumn("dbo.Tbl_EmpleadoTercero", "Telefono", c => c.String(maxLength: 15));
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_id_departamento", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_id_municipio", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_id_zona", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_EmpleadoTercero", "Ocupacion_habitual", c => c.String(maxLength: 15));
            AddColumn("dbo.Tbl_EmpleadoTercero", "Fecha_ingreso_empresa", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_id_jornada_habitual", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_id_departamento");
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_id_municipio");
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_id_zona");
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_id_jornada_habitual");
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_departamento", "dbo.Tbl_Departamento", "Pk_Id_Departamento", cascadeDelete: false);
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_jornada_habitual", "dbo.Tbl_Tipo_Jornada", "Pk_Id_Tipo_Jornada", cascadeDelete: false);
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_municipio", "dbo.Tbl_Municipio", "Pk_Id_Municipio", cascadeDelete: false);
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_zona", "dbo.Tbl_ZonaLugar", "PK_ZonaLugar", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_zona", "dbo.Tbl_ZonaLugar");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_municipio", "dbo.Tbl_Municipio");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_jornada_habitual", "dbo.Tbl_Tipo_Jornada");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_id_departamento", "dbo.Tbl_Departamento");
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_id_jornada_habitual" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_id_zona" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_id_municipio" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_id_departamento" });
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_id_jornada_habitual");
            DropColumn("dbo.Tbl_EmpleadoTercero", "Fecha_ingreso_empresa");
            DropColumn("dbo.Tbl_EmpleadoTercero", "Ocupacion_habitual");
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_id_zona");
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_id_municipio");
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_id_departamento");
            DropColumn("dbo.Tbl_EmpleadoTercero", "Telefono");
            DropColumn("dbo.Tbl_EmpleadoTercero", "Direccion");
            DropColumn("dbo.Tbl_EmpleadoTercero", "Genero");
        }
    }
}
