namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PesvCreacionTablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_PlanVial",
                c => new
                    {
                        Pk_Id_SegVial = c.Int(nullable: false, identity: true),
                        Id_Consecutivo = c.Int(nullable: false),
                        Fecha_Registro = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Fk_Id_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_SegVial)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: true)
                .Index(t => t.Fk_Id_Sede);
            
            CreateTable(
                "dbo.Tbl_SegVialResultado",
                c => new
                    {
                        Pk_Id_SegVialResultado = c.Int(nullable: false, identity: true),
                        Aplica = c.Boolean(nullable: false),
                        Aplica_s = c.Short(nullable: false),
                        Existencia = c.Boolean(nullable: false),
                        Existencia_s = c.Short(nullable: false),
                        Responde = c.Boolean(nullable: false),
                        Responde_s = c.Short(nullable: false),
                        ValorObtenido = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Observaciones = c.String(maxLength: 500),
                        Fk_Id_PlanVial = c.Int(nullable: false),
                        Fk_Id_SegVialParametroDetalle = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_SegVialResultado)
                .ForeignKey("dbo.Tbl_PlanVial", t => t.Fk_Id_PlanVial, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_SegVialDetalle", t => t.Fk_Id_SegVialParametroDetalle, cascadeDelete: true)
                .Index(t => t.Fk_Id_PlanVial)
                .Index(t => t.Fk_Id_SegVialParametroDetalle);
            
            CreateTable(
                "dbo.Tbl_SegVialDetalle",
                c => new
                    {
                        Pk_Id_SegVialParametroDetalle = c.Int(nullable: false, identity: true),
                        Numeral = c.String(nullable: false, maxLength: 30),
                        VariableDesc = c.String(nullable: false, maxLength: 500),
                        CriterioAval = c.String(nullable: false, maxLength: 1000),
                        Fk_Id_SegVialPilar = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_SegVialParametroDetalle)
                .ForeignKey("dbo.Tbl_SegVialParametro", t => t.Fk_Id_SegVialPilar, cascadeDelete: true)
                .Index(t => t.Fk_Id_SegVialPilar);
            
            CreateTable(
                "dbo.Tbl_SegVialParametro",
                c => new
                    {
                        Pk_Id_SegVialParametro = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        Numeral = c.String(nullable: false, maxLength: 30),
                        ParametroDef = c.String(nullable: false, maxLength: 500),
                        Valor_Parametro = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Fk_Id_SegVialPilar = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_SegVialParametro)
                .ForeignKey("dbo.Tbl_SegVialPilar", t => t.Fk_Id_SegVialPilar, cascadeDelete: true)
                .Index(t => t.Fk_Id_SegVialPilar);
            
            CreateTable(
                "dbo.Tbl_SegVialPilar",
                c => new
                    {
                        Pk_Id_SegVialPilar = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Version = c.Int(nullable: false),
                        Valor_Ponderado = c.Decimal(nullable: false, precision: 5, scale: 2),
                    })
                .PrimaryKey(t => t.Pk_Id_SegVialPilar);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_SegVialResultado", "Fk_Id_SegVialParametroDetalle", "dbo.Tbl_SegVialDetalle");
            DropForeignKey("dbo.Tbl_SegVialDetalle", "Fk_Id_SegVialPilar", "dbo.Tbl_SegVialParametro");
            DropForeignKey("dbo.Tbl_SegVialParametro", "Fk_Id_SegVialPilar", "dbo.Tbl_SegVialPilar");
            DropForeignKey("dbo.Tbl_SegVialResultado", "Fk_Id_PlanVial", "dbo.Tbl_PlanVial");
            DropForeignKey("dbo.Tbl_PlanVial", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropIndex("dbo.Tbl_SegVialParametro", new[] { "Fk_Id_SegVialPilar" });
            DropIndex("dbo.Tbl_SegVialDetalle", new[] { "Fk_Id_SegVialPilar" });
            DropIndex("dbo.Tbl_SegVialResultado", new[] { "Fk_Id_SegVialParametroDetalle" });
            DropIndex("dbo.Tbl_SegVialResultado", new[] { "Fk_Id_PlanVial" });
            DropIndex("dbo.Tbl_PlanVial", new[] { "Fk_Id_Sede" });
            DropTable("dbo.Tbl_SegVialPilar");
            DropTable("dbo.Tbl_SegVialParametro");
            DropTable("dbo.Tbl_SegVialDetalle");
            DropTable("dbo.Tbl_SegVialResultado");
            DropTable("dbo.Tbl_PlanVial");
        }
    }
}
