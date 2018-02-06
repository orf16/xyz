namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacionDxSalud : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Diagnostico", "dbo.Tbl_Diagnosticos");
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Diagnostico" });
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Clasificacion_De_Peligro" });
            CreateTable(
                "dbo.Tbl_Clasificacion_Peligro_Dx",
                c => new
                    {
                        idClasificacionPeligroDx = c.Int(nullable: false, identity: true),
                        FK_DxCondicionesDeSalud = c.Int(nullable: false),
                        FK_Clasificacion_De_Peligro = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idClasificacionPeligroDx)
                .ForeignKey("dbo.Tbl_Clasificacion_De_Peligro", t => t.FK_Clasificacion_De_Peligro, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", t => t.FK_DxCondicionesDeSalud, cascadeDelete: true)
                .Index(t => t.FK_DxCondicionesDeSalud)
                .Index(t => t.FK_Clasificacion_De_Peligro);
            
            CreateTable(
                "dbo.Tbl_Diagnostico_Cie10_Dx",
                c => new
                    {
                        idDiagnosticoCie10Dx = c.Int(nullable: false, identity: true),
                        FK_DxCondicionesDeSalud = c.Int(nullable: false),
                        Trabajadores_Con_Diagnostico = c.Int(nullable: false),
                        FK_Diagnostico = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idDiagnosticoCie10Dx)
                .ForeignKey("dbo.Tbl_Diagnosticos", t => t.FK_Diagnostico, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", t => t.FK_DxCondicionesDeSalud, cascadeDelete: true)
                .Index(t => t.FK_DxCondicionesDeSalud)
                .Index(t => t.FK_Diagnostico);
            
            CreateTable(
                "dbo.Tbl_Pruebas_Clinica_Dx",
                c => new
                    {
                        idPruebasClinicas = c.Int(nullable: false, identity: true),
                        Prueba_Clinica = c.String(),
                        Trabajadores_Con_Prueba = c.Int(nullable: false),
                        FK_DxCondicionesDeSalud = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idPruebasClinicas)
                .ForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", t => t.FK_DxCondicionesDeSalud, cascadeDelete: true)
                .Index(t => t.FK_DxCondicionesDeSalud);
            
            CreateTable(
                "dbo.Tbl_Pruebas_P_Clinica_Dx",
                c => new
                    {
                        idPruebasPClinicas = c.Int(nullable: false, identity: true),
                        Prueba_P_Clinica = c.String(),
                        Trabajadores_Con_Prueba_P = c.Int(nullable: false),
                        FK_DxCondicionesDeSalud = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idPruebasPClinicas)
                .ForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", t => t.FK_DxCondicionesDeSalud, cascadeDelete: true)
                .Index(t => t.FK_DxCondicionesDeSalud);
            
            CreateTable(
                "dbo.Tbl_Sintomatologia_Dx",
                c => new
                    {
                        idSintomatologia = c.Int(nullable: false, identity: true),
                        Sintomatologia = c.String(),
                        Trabajadores_Sintomatologia = c.Int(nullable: false),
                        FK_DxCondicionesDeSalud = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idSintomatologia)
                .ForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", t => t.FK_DxCondicionesDeSalud, cascadeDelete: true)
                .Index(t => t.FK_DxCondicionesDeSalud);
            
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Fecha_Inicial_Dx", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Fecha_Final_Dx", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "vigencia", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Responsable_informacion", c => c.String());
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Profesion_Responsable", c => c.String());
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Tarjeta_Profesional", c => c.String());
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso");
            AddForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", "dbo.Tbl_Proceso", "Pk_Id_Proceso", cascadeDelete: true);
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Sintomatologia");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Sintomatologia");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Prueba_Clinica");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Con_Prueba");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Prueba_P_Clinica");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Con_Prueba_P");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Con_Diagnostico");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Diagnostico");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Clasificacion_De_Peligro");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Otro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Otro", c => c.String());
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Clasificacion_De_Peligro", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Diagnostico", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Con_Diagnostico", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Con_Prueba_P", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Prueba_P_Clinica", c => c.String());
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Con_Prueba", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Prueba_Clinica", c => c.String());
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Trabajadores_Sintomatologia", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Sintomatologia", c => c.String());
            DropForeignKey("dbo.Tbl_Clasificacion_Peligro_Dx", "FK_DxCondicionesDeSalud", "dbo.Tbl_Dx_Condiciones_De_Salud");
            DropForeignKey("dbo.Tbl_Sintomatologia_Dx", "FK_DxCondicionesDeSalud", "dbo.Tbl_Dx_Condiciones_De_Salud");
            DropForeignKey("dbo.Tbl_Pruebas_P_Clinica_Dx", "FK_DxCondicionesDeSalud", "dbo.Tbl_Dx_Condiciones_De_Salud");
            DropForeignKey("dbo.Tbl_Pruebas_Clinica_Dx", "FK_DxCondicionesDeSalud", "dbo.Tbl_Dx_Condiciones_De_Salud");
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_Diagnostico_Cie10_Dx", "FK_DxCondicionesDeSalud", "dbo.Tbl_Dx_Condiciones_De_Salud");
            DropForeignKey("dbo.Tbl_Diagnostico_Cie10_Dx", "FK_Diagnostico", "dbo.Tbl_Diagnosticos");
            DropForeignKey("dbo.Tbl_Clasificacion_Peligro_Dx", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropIndex("dbo.Tbl_Sintomatologia_Dx", new[] { "FK_DxCondicionesDeSalud" });
            DropIndex("dbo.Tbl_Pruebas_P_Clinica_Dx", new[] { "FK_DxCondicionesDeSalud" });
            DropIndex("dbo.Tbl_Pruebas_Clinica_Dx", new[] { "FK_DxCondicionesDeSalud" });
            DropIndex("dbo.Tbl_Diagnostico_Cie10_Dx", new[] { "FK_Diagnostico" });
            DropIndex("dbo.Tbl_Diagnostico_Cie10_Dx", new[] { "FK_DxCondicionesDeSalud" });
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Proceso" });
            DropIndex("dbo.Tbl_Clasificacion_Peligro_Dx", new[] { "FK_Clasificacion_De_Peligro" });
            DropIndex("dbo.Tbl_Clasificacion_Peligro_Dx", new[] { "FK_DxCondicionesDeSalud" });
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Tarjeta_Profesional");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Profesion_Responsable");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Responsable_informacion");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "vigencia");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Fecha_Final_Dx");
            DropColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "Fecha_Inicial_Dx");
            DropTable("dbo.Tbl_Sintomatologia_Dx");
            DropTable("dbo.Tbl_Pruebas_P_Clinica_Dx");
            DropTable("dbo.Tbl_Pruebas_Clinica_Dx");
            DropTable("dbo.Tbl_Diagnostico_Cie10_Dx");
            DropTable("dbo.Tbl_Clasificacion_Peligro_Dx");
            CreateIndex("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Clasificacion_De_Peligro");
            CreateIndex("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Diagnostico");
            AddForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Diagnostico", "dbo.Tbl_Diagnosticos", "PK_Id_Diagnostico", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro", "PK_Clasificacion_De_Peligro", cascadeDelete: true);
        }
    }
}
