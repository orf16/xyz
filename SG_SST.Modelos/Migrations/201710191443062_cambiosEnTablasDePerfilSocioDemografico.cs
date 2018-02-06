namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosEnTablasDePerfilSocioDemografico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Raza", "dbo.Tbl_Raza");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil", "dbo.Tbl_Ocupaciones_De_Perfil");
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Clasificacion_De_Peligro" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Raza" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_OcupacionPerfil" });
            RenameColumn(table: "dbo.Tbl_PerfilSocioDemograficoPlanificacion", name: "FK_OcupacionPerfil", newName: "Ocupaciones_Perfil_PK_OcupacionPerfil");
            CreateTable(
                "dbo.Tbl_Condiciones_Riesgo_Perfil",
                c => new
                    {
                        PK_Condiciones_Riesgo_Perfil = c.Int(nullable: false, identity: true),
                        FK_Clasificacion_De_Peligro = c.Int(nullable: false),
                        OtroPeligro = c.String(),
                        tiempoExposicion = c.String(),
                        FK_PerfilSocioDemografico = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Condiciones_Riesgo_Perfil)
                .ForeignKey("dbo.Tbl_Clasificacion_De_Peligro", t => t.FK_Clasificacion_De_Peligro, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", t => t.FK_PerfilSocioDemografico, cascadeDelete: true)
                .Index(t => t.FK_Clasificacion_De_Peligro)
                .Index(t => t.FK_PerfilSocioDemografico);
            
            CreateTable(
                "dbo.Tbl_Etnia",
                c => new
                    {
                        PK_Etnia = c.Int(nullable: false, identity: true),
                        Descripcion_Etnia = c.String(),
                    })
                .PrimaryKey(t => t.PK_Etnia);
            
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Etnia", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "caracteristicasFisicas", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "caracteristicasPsicologicas", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "evaluacionesMedicasRequeridas", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "nitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Proceso", c => c.Int());
            AlterColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "PK_Numero_Documento_Empl", c => c.String());
            AlterColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupaciones_Perfil_PK_OcupacionPerfil", c => c.Int());
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Etnia");
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Proceso");
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupaciones_Perfil_PK_OcupacionPerfil");
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Proceso", "dbo.Tbl_Proceso", "Pk_Id_Proceso");
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Etnia", "dbo.Tbl_Etnia", "PK_Etnia", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupaciones_Perfil_PK_OcupacionPerfil", "dbo.Tbl_Ocupaciones_De_Perfil", "PK_OcupacionPerfil");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Tipo_Documento");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Nombre1");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Nombre2");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Apellido1");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Apellido2");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Clasificacion_De_Peligro");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "OtroPeligro");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Direccion");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Raza");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "GrupoEtarios");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Cargo");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FechaIngresoEmpresa");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "AntecedentesExpLaboral");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "tiempoExpos");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "EvaluacionMedica");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Edad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Edad", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "EvaluacionMedica", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "tiempoExpos", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "AntecedentesExpLaboral", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FechaIngresoEmpresa", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Cargo", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "GrupoEtarios", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Raza", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Direccion", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "OtroPeligro", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Clasificacion_De_Peligro", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Apellido2", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Apellido1", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Nombre2", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Nombre1", c => c.String());
            AddColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Tipo_Documento", c => c.String());
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupaciones_Perfil_PK_OcupacionPerfil", "dbo.Tbl_Ocupaciones_De_Perfil");
            DropForeignKey("dbo.Tbl_Condiciones_Riesgo_Perfil", "FK_PerfilSocioDemografico", "dbo.Tbl_PerfilSocioDemograficoPlanificacion");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Etnia", "dbo.Tbl_Etnia");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_Condiciones_Riesgo_Perfil", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "Ocupaciones_Perfil_PK_OcupacionPerfil" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Proceso" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Etnia" });
            DropIndex("dbo.Tbl_Condiciones_Riesgo_Perfil", new[] { "FK_PerfilSocioDemografico" });
            DropIndex("dbo.Tbl_Condiciones_Riesgo_Perfil", new[] { "FK_Clasificacion_De_Peligro" });
            AlterColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Ocupaciones_Perfil_PK_OcupacionPerfil", c => c.Int(nullable: false));
            AlterColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "PK_Numero_Documento_Empl", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Proceso");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "nitEmpresa");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "evaluacionesMedicasRequeridas");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "caracteristicasPsicologicas");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "caracteristicasFisicas");
            DropColumn("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Etnia");
            DropTable("dbo.Tbl_Etnia");
            DropTable("dbo.Tbl_Condiciones_Riesgo_Perfil");
            RenameColumn(table: "dbo.Tbl_PerfilSocioDemograficoPlanificacion", name: "Ocupaciones_Perfil_PK_OcupacionPerfil", newName: "FK_OcupacionPerfil");
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil");
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Raza");
            CreateIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Clasificacion_De_Peligro");
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_OcupacionPerfil", "dbo.Tbl_Ocupaciones_De_Perfil", "PK_OcupacionPerfil", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Raza", "dbo.Tbl_Raza", "PK_Raza", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro", "PK_Clasificacion_De_Peligro", cascadeDelete: true);
        }
    }
}
