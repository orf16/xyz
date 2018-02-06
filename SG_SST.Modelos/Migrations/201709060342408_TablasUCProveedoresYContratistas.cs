namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablasUCProveedoresYContratistas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_CriterioSST",
                c => new
                    {
                        PK_CriterioSST = c.Int(nullable: false, identity: true),
                        Criterio = c.String(),
                    })
                .PrimaryKey(t => t.PK_CriterioSST);
            
            CreateTable(
                "dbo.Tbl_Producto_Por_Criterio",
                c => new
                    {
                        Id_Pk_ProductoPorCriterio = c.Int(nullable: false, identity: true),
                        Fk_Id_ServicioOProducto = c.Int(nullable: false),
                        Fk_Id__CriterioSST = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_ProductoPorCriterio)
                .ForeignKey("dbo.Tbl_CriterioSST", t => t.Fk_Id__CriterioSST, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ServicioOProducto", t => t.Fk_Id_ServicioOProducto, cascadeDelete: true)
                .Index(t => t.Fk_Id_ServicioOProducto)
                .Index(t => t.Fk_Id__CriterioSST);
            
            CreateTable(
                "dbo.Tbl_Proveedor_ProductoPorCriterio",
                c => new
                    {
                        Id_Pk_ProveedorPorProductoPorCriterio = c.Int(nullable: false, identity: true),
                        Fk_Id_ProveedorContratista = c.Int(nullable: false),
                        Fk_Id_ProductoPorCriterio = c.Int(nullable: false),
                        Calificacion = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_ProveedorPorProductoPorCriterio)
                .ForeignKey("dbo.Tbl_Producto_Por_Criterio", t => t.Fk_Id_ProductoPorCriterio, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ProveedorContratista", t => t.Fk_Id_ProveedorContratista, cascadeDelete: true)
                .Index(t => t.Fk_Id_ProveedorContratista)
                .Index(t => t.Fk_Id_ProductoPorCriterio);
            
            CreateTable(
                "dbo.Tbl_ProveedorContratista",
                c => new
                    {
                        PK_ProveedorContratista = c.Int(nullable: false, identity: true),
                        Nombre_ProveedorContratista = c.String(),
                        Nit_ProveedorContratista = c.String(),
                        Fecha_Calificacion = c.DateTime(nullable: false),
                        ResultadoCalificacion = c.Double(nullable: false),
                        Observaciones = c.String(),
                    })
                .PrimaryKey(t => t.PK_ProveedorContratista);
            
            CreateTable(
                "dbo.Tbl_Proveedor_Por_Producto",
                c => new
                    {
                        Id_Pk_ProveedorPorProducto = c.Int(nullable: false, identity: true),
                        Fk_Id_ProveedorContratista = c.Int(nullable: false),
                        Fk_Id_ServicioOProducto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_ProveedorPorProducto)
                .ForeignKey("dbo.Tbl_ProveedorContratista", t => t.Fk_Id_ProveedorContratista, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ServicioOProducto", t => t.Fk_Id_ServicioOProducto, cascadeDelete: true)
                .Index(t => t.Fk_Id_ProveedorContratista)
                .Index(t => t.Fk_Id_ServicioOProducto);
            
            CreateTable(
                "dbo.Tbl_ServicioOProducto",
                c => new
                    {
                        PK_ServicioOProducto = c.Int(nullable: false, identity: true),
                        Nombre_ServicioOProducto = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_ServicioOProducto)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
            CreateTable(
                "dbo.Tbl_ManualGuiaAdBienes",
                c => new
                    {
                        PK_ManualGuiaAdBienes = c.Int(nullable: false, identity: true),
                        Nombre_Manual = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_ManualGuiaAdBienes)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_ManualGuiaAdBienes", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Producto_Por_Criterio", "Fk_Id_ServicioOProducto", "dbo.Tbl_ServicioOProducto");
            DropForeignKey("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorContratista", "dbo.Tbl_ProveedorContratista");
            DropForeignKey("dbo.Tbl_Proveedor_Por_Producto", "Fk_Id_ServicioOProducto", "dbo.Tbl_ServicioOProducto");
            DropForeignKey("dbo.Tbl_ServicioOProducto", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Proveedor_Por_Producto", "Fk_Id_ProveedorContratista", "dbo.Tbl_ProveedorContratista");
            DropForeignKey("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProductoPorCriterio", "dbo.Tbl_Producto_Por_Criterio");
            DropForeignKey("dbo.Tbl_Producto_Por_Criterio", "Fk_Id__CriterioSST", "dbo.Tbl_CriterioSST");
            DropIndex("dbo.Tbl_ManualGuiaAdBienes", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_ServicioOProducto", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Proveedor_Por_Producto", new[] { "Fk_Id_ServicioOProducto" });
            DropIndex("dbo.Tbl_Proveedor_Por_Producto", new[] { "Fk_Id_ProveedorContratista" });
            DropIndex("dbo.Tbl_Proveedor_ProductoPorCriterio", new[] { "Fk_Id_ProductoPorCriterio" });
            DropIndex("dbo.Tbl_Proveedor_ProductoPorCriterio", new[] { "Fk_Id_ProveedorContratista" });
            DropIndex("dbo.Tbl_Producto_Por_Criterio", new[] { "Fk_Id__CriterioSST" });
            DropIndex("dbo.Tbl_Producto_Por_Criterio", new[] { "Fk_Id_ServicioOProducto" });
            DropTable("dbo.Tbl_ManualGuiaAdBienes");
            DropTable("dbo.Tbl_ServicioOProducto");
            DropTable("dbo.Tbl_Proveedor_Por_Producto");
            DropTable("dbo.Tbl_ProveedorContratista");
            DropTable("dbo.Tbl_Proveedor_ProductoPorCriterio");
            DropTable("dbo.Tbl_Producto_Por_Criterio");
            DropTable("dbo.Tbl_CriterioSST");
        }
    }
}
