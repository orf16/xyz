namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tabla_Reportes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Reportes",
                c => new
                    {
                        Pk_Id_Reportes = c.Int(nullable: false, identity: true),
                        Id_Consecutivo_Reportes = c.Int(nullable: false),
                        FK_RazonSocialEmpresa = c.String(),
                        FK_NitEmpresa = c.String(),
                        FK_Id_Sede = c.Int(nullable: false),
                        FK_Id_Proceso = c.Int(nullable: false),
                        FK_Tipo_Reporte = c.Int(nullable: false),
                        Fecha_Sistema = c.DateTime(),
                        Fecha_Ocurrencia = c.DateTime(nullable: false),
                        Area_Lugar = c.String(),
                        Descripcion_Reporte = c.String(),
                        Causa_Reporte = c.String(),
                        Sugerencias_Reporte = c.String(),
                        Tipo_Vinculacion = c.String(),
                        FK_Id_Ocupacion = c.Int(nullable: false),
                        Cedula_Quien_Reporta = c.Int(),
                        Nombre_Quien_Reporta = c.String(),
                        Cargo_Quien_Reporta = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Reportes)
                .ForeignKey("dbo.Tbl_Tipo_Reporte", t => t.FK_Tipo_Reporte, cascadeDelete: true)
                .Index(t => t.FK_Tipo_Reporte);
            
            CreateTable(
                "dbo.Tbl_Tipo_Reporte",
                c => new
                    {
                        Pk_Id_Tipo_Reporte = c.Int(nullable: false, identity: true),
                        Descripcion_Tipo_Reporte = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Tipo_Reporte);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Reportes", "FK_Tipo_Reporte", "dbo.Tbl_Tipo_Reporte");
            DropIndex("dbo.Tbl_Reportes", new[] { "FK_Tipo_Reporte" });
            DropTable("dbo.Tbl_Tipo_Reporte");
            DropTable("dbo.Tbl_Reportes");
        }
    }
}
