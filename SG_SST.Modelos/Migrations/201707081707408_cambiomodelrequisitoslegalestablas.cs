namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiomodelrequisitoslegalestablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Actividad_Economica",
                c => new
                    {
                        PK_Actividad_Economica = c.Int(nullable: false, identity: true),
                        Ente = c.String(),
                    })
                .PrimaryKey(t => t.PK_Actividad_Economica);
            
            CreateTable(
                "dbo.Tbl_Matriz_RequisitosLegales",
                c => new
                    {
                        PK_MatrizRequisitosLegales = c.Int(nullable: false, identity: true),
                        NombreMatriz = c.String(),
                    })
                .PrimaryKey(t => t.PK_MatrizRequisitosLegales);
            
            CreateTable(
                "dbo.Tbl_Requisitos_Legales_Posipedia",
                c => new
                    {
                        PK_RequisitosLegalesOtros = c.Int(nullable: false, identity: true),
                        Tipo_Norma = c.String(),
                        Numero_Norma = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                        Ente = c.String(),
                        Articulo = c.String(),
                        Descripcion = c.String(),
                        Sugerencias = c.String(),
                        Clase_De_Peligro = c.String(),
                        Peligro = c.String(),
                        Aspectos = c.String(),
                        Impactos = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                        FK_Actividad_Economica = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_RequisitosLegalesOtros)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Actividad_Economica", t => t.FK_Actividad_Economica, cascadeDelete: true)
                .Index(t => t.FK_Empresa)
                .Index(t => t.FK_Actividad_Economica);
            
            CreateTable(
                "dbo.Tbl_Requisitos_Matriz",
                c => new
                    {
                        PK_Requisitos_Matriz = c.Int(nullable: false, identity: true),
                        FK_RequisitosLegalesOtros = c.Int(nullable: false),
                        FK_MatrizRequisitosLegales = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Requisitos_Matriz)
                .ForeignKey("dbo.Tbl_Matriz_RequisitosLegales", t => t.FK_MatrizRequisitosLegales, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Requisitos_legales_Otros", t => t.FK_RequisitosLegalesOtros, cascadeDelete: true)
                .Index(t => t.FK_RequisitosLegalesOtros)
                .Index(t => t.FK_MatrizRequisitosLegales);
            
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "Tipo_Norma", c => c.String());
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "Numero_Norma", c => c.String());
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "FK_Actividad_Economica", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Requisitos_legales_Otros", "FK_Actividad_Economica");
            AddForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Actividad_Economica", "dbo.Tbl_Actividad_Economica", "PK_Actividad_Economica", cascadeDelete: true);
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "Norma");
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "Sistema");
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "Modificacion");
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "PartesInteresadas");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "PartesInteresadas", c => c.String());
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "Modificacion", c => c.String());
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "Sistema", c => c.String());
            AddColumn("dbo.Tbl_Requisitos_legales_Otros", "Norma", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Tbl_Requisitos_Matriz", "FK_RequisitosLegalesOtros", "dbo.Tbl_Requisitos_legales_Otros");
            DropForeignKey("dbo.Tbl_Requisitos_Matriz", "FK_MatrizRequisitosLegales", "dbo.Tbl_Matriz_RequisitosLegales");
            DropForeignKey("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Actividad_Economica", "dbo.Tbl_Actividad_Economica");
            DropForeignKey("dbo.Tbl_Requisitos_Legales_Posipedia", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Actividad_Economica", "dbo.Tbl_Actividad_Economica");
            DropIndex("dbo.Tbl_Requisitos_Matriz", new[] { "FK_MatrizRequisitosLegales" });
            DropIndex("dbo.Tbl_Requisitos_Matriz", new[] { "FK_RequisitosLegalesOtros" });
            DropIndex("dbo.Tbl_Requisitos_Legales_Posipedia", new[] { "FK_Actividad_Economica" });
            DropIndex("dbo.Tbl_Requisitos_Legales_Posipedia", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Requisitos_legales_Otros", new[] { "FK_Actividad_Economica" });
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "FK_Actividad_Economica");
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "Numero_Norma");
            DropColumn("dbo.Tbl_Requisitos_legales_Otros", "Tipo_Norma");
            DropTable("dbo.Tbl_Requisitos_Matriz");
            DropTable("dbo.Tbl_Requisitos_Legales_Posipedia");
            DropTable("dbo.Tbl_Matriz_RequisitosLegales");
            DropTable("dbo.Tbl_Actividad_Economica");
        }
    }
}
