namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevasTablasUC28ProveedoresYContratistas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorContratista", "dbo.Tbl_ProveedorContratista");
            DropIndex("dbo.Tbl_Proveedor_ProductoPorCriterio", new[] { "Fk_Id_ProveedorContratista" });
            CreateTable(
                "dbo.Tbl_Archivos_Anexos",
                c => new
                    {
                        PK_Archivos_Anexos = c.Int(nullable: false, identity: true),
                        rutaAnexos = c.String(),
                    })
                .PrimaryKey(t => t.PK_Archivos_Anexos);
            
            CreateTable(
                "dbo.Tbl_Proveedor_Por_Anexo",
                c => new
                    {
                        Id_Pk_ProveedorPorAnexo = c.Int(nullable: false, identity: true),
                        Fk_Id_CalificacionProveedor = c.Int(nullable: false),
                        Fk_Id_Archivos_Anexos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_ProveedorPorAnexo)
                .ForeignKey("dbo.Tbl_Archivos_Anexos", t => t.Fk_Id_Archivos_Anexos, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_CalificacionProveedor", t => t.Fk_Id_CalificacionProveedor, cascadeDelete: true)
                .Index(t => t.Fk_Id_CalificacionProveedor)
                .Index(t => t.Fk_Id_Archivos_Anexos);
            
            CreateTable(
                "dbo.Tbl_CalificacionProveedor",
                c => new
                    {
                        PK_CalificacionProveedor = c.Int(nullable: false, identity: true),
                        Fecha_Calificacion = c.DateTime(nullable: false),
                        ResultadoCalificacion = c.Double(nullable: false),
                        Observaciones = c.String(),
                        NumeroCalificion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_CalificacionProveedor);
            
            CreateTable(
                "dbo.Tbl_Proveedor_Por_NumeroCalificacion",
                c => new
                    {
                        PK_ProveedorPorNumeroCalificacion = c.Int(nullable: false, identity: true),
                        Fk_Id_ProveedorContratista = c.Int(nullable: false),
                        Fk_Id_CalificacionProveedor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_ProveedorPorNumeroCalificacion)
                .ForeignKey("dbo.Tbl_CalificacionProveedor", t => t.Fk_Id_CalificacionProveedor, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ProveedorContratista", t => t.Fk_Id_ProveedorContratista, cascadeDelete: true)
                .Index(t => t.Fk_Id_ProveedorContratista)
                .Index(t => t.Fk_Id_CalificacionProveedor);
            
            AddColumn("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorPorNumeroCalificacion", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Proveedor_ProductoPorCriterio", "CalificacionProducto", c => c.Double(nullable: false));
            AddColumn("dbo.Tbl_ProveedorContratista", "IdEmpresa", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_ProveedorContratista", "FrecuenciaEval", c => c.String());
            AddColumn("dbo.Tbl_ProveedorContratista", "CalificacionHist", c => c.Int());
            AddColumn("dbo.Tbl_ProveedorContratista", "VigenciaContrato", c => c.DateTime());
            CreateIndex("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorPorNumeroCalificacion");
            AddForeignKey("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorPorNumeroCalificacion", "dbo.Tbl_Proveedor_Por_NumeroCalificacion", "PK_ProveedorPorNumeroCalificacion", cascadeDelete: true);
            DropColumn("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorContratista");
            DropColumn("dbo.Tbl_ProveedorContratista", "Fecha_Calificacion");
            DropColumn("dbo.Tbl_ProveedorContratista", "ResultadoCalificacion");
            DropColumn("dbo.Tbl_ProveedorContratista", "Observaciones");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_ProveedorContratista", "Observaciones", c => c.String());
            AddColumn("dbo.Tbl_ProveedorContratista", "ResultadoCalificacion", c => c.Double(nullable: false));
            AddColumn("dbo.Tbl_ProveedorContratista", "Fecha_Calificacion", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorContratista", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tbl_Proveedor_Por_Anexo", "Fk_Id_CalificacionProveedor", "dbo.Tbl_CalificacionProveedor");
            DropForeignKey("dbo.Tbl_Proveedor_Por_NumeroCalificacion", "Fk_Id_ProveedorContratista", "dbo.Tbl_ProveedorContratista");
            DropForeignKey("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorPorNumeroCalificacion", "dbo.Tbl_Proveedor_Por_NumeroCalificacion");
            DropForeignKey("dbo.Tbl_Proveedor_Por_NumeroCalificacion", "Fk_Id_CalificacionProveedor", "dbo.Tbl_CalificacionProveedor");
            DropForeignKey("dbo.Tbl_Proveedor_Por_Anexo", "Fk_Id_Archivos_Anexos", "dbo.Tbl_Archivos_Anexos");
            DropIndex("dbo.Tbl_Proveedor_ProductoPorCriterio", new[] { "Fk_Id_ProveedorPorNumeroCalificacion" });
            DropIndex("dbo.Tbl_Proveedor_Por_NumeroCalificacion", new[] { "Fk_Id_CalificacionProveedor" });
            DropIndex("dbo.Tbl_Proveedor_Por_NumeroCalificacion", new[] { "Fk_Id_ProveedorContratista" });
            DropIndex("dbo.Tbl_Proveedor_Por_Anexo", new[] { "Fk_Id_Archivos_Anexos" });
            DropIndex("dbo.Tbl_Proveedor_Por_Anexo", new[] { "Fk_Id_CalificacionProveedor" });
            DropColumn("dbo.Tbl_ProveedorContratista", "VigenciaContrato");
            DropColumn("dbo.Tbl_ProveedorContratista", "CalificacionHist");
            DropColumn("dbo.Tbl_ProveedorContratista", "FrecuenciaEval");
            DropColumn("dbo.Tbl_ProveedorContratista", "IdEmpresa");
            DropColumn("dbo.Tbl_Proveedor_ProductoPorCriterio", "CalificacionProducto");
            DropColumn("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorPorNumeroCalificacion");
            DropTable("dbo.Tbl_Proveedor_Por_NumeroCalificacion");
            DropTable("dbo.Tbl_CalificacionProveedor");
            DropTable("dbo.Tbl_Proveedor_Por_Anexo");
            DropTable("dbo.Tbl_Archivos_Anexos");
            CreateIndex("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorContratista");
            AddForeignKey("dbo.Tbl_Proveedor_ProductoPorCriterio", "Fk_Id_ProveedorContratista", "dbo.Tbl_ProveedorContratista", "PK_ProveedorContratista", cascadeDelete: true);
        }
    }
}
