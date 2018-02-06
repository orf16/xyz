namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moduloReporteCondicionesActosInseguros : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ActividadesActosInseguros",
                c => new
                    {
                        PK_ID_ActividadActosInseguros = c.Int(nullable: false, identity: true),
                        ResponsableActividad = c.String(),
                        FechaEjecucion = c.DateTime(nullable: false),
                        FK_Id_Reportes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_ID_ActividadActosInseguros)
                .ForeignKey("dbo.Tbl_Reportes", t => t.FK_Id_Reportes, cascadeDelete: true)
                .Index(t => t.FK_Id_Reportes);
            
            CreateTable(
                "dbo.Tbl_ImagenesReportes",
                c => new
                    {
                        PK_ImagenesReportes = c.Int(nullable: false, identity: true),
                        ruta = c.String(),
                        FK_Id_Reportes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_ImagenesReportes)
                .ForeignKey("dbo.Tbl_Reportes", t => t.FK_Id_Reportes, cascadeDelete: true)
                .Index(t => t.FK_Id_Reportes);
            
            AddColumn("dbo.Tbl_Reportes", "RazonSocialEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Reportes", "fechaSistema", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_Reportes", "FK_Sede", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Reportes", "FK_Proceso", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Reportes", "medioAcceso", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tbl_Reportes", "Cedula_Quien_Reporta", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Reportes", "FK_Sede");
            CreateIndex("dbo.Tbl_Reportes", "FK_Proceso");
            AddForeignKey("dbo.Tbl_Reportes", "FK_Proceso", "dbo.Tbl_Proceso", "Pk_Id_Proceso", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Reportes", "FK_Sede", "dbo.Tbl_Sede", "Pk_Id_Sede", cascadeDelete: true);
            DropColumn("dbo.Tbl_Reportes", "FK_RazonSocialEmpresa");
            DropColumn("dbo.Tbl_Reportes", "FK_Id_Sede");
            DropColumn("dbo.Tbl_Reportes", "FK_Id_Proceso");
            DropColumn("dbo.Tbl_Reportes", "Fecha_Sistema");
            DropColumn("dbo.Tbl_Reportes", "Tipo_Vinculacion");
            DropColumn("dbo.Tbl_Reportes", "FK_Id_Ocupacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Reportes", "FK_Id_Ocupacion", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Reportes", "Tipo_Vinculacion", c => c.String());
            AddColumn("dbo.Tbl_Reportes", "Fecha_Sistema", c => c.DateTime());
            AddColumn("dbo.Tbl_Reportes", "FK_Id_Proceso", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Reportes", "FK_Id_Sede", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Reportes", "FK_RazonSocialEmpresa", c => c.String());
            DropForeignKey("dbo.Tbl_ImagenesReportes", "FK_Id_Reportes", "dbo.Tbl_Reportes");
            DropForeignKey("dbo.Tbl_ActividadesActosInseguros", "FK_Id_Reportes", "dbo.Tbl_Reportes");
            DropForeignKey("dbo.Tbl_Reportes", "FK_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Reportes", "FK_Proceso", "dbo.Tbl_Proceso");
            DropIndex("dbo.Tbl_ImagenesReportes", new[] { "FK_Id_Reportes" });
            DropIndex("dbo.Tbl_Reportes", new[] { "FK_Proceso" });
            DropIndex("dbo.Tbl_Reportes", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_ActividadesActosInseguros", new[] { "FK_Id_Reportes" });
            AlterColumn("dbo.Tbl_Reportes", "Cedula_Quien_Reporta", c => c.Int());
            DropColumn("dbo.Tbl_Reportes", "medioAcceso");
            DropColumn("dbo.Tbl_Reportes", "FK_Proceso");
            DropColumn("dbo.Tbl_Reportes", "FK_Sede");
            DropColumn("dbo.Tbl_Reportes", "fechaSistema");
            DropColumn("dbo.Tbl_Reportes", "RazonSocialEmpresa");
            DropTable("dbo.Tbl_ImagenesReportes");
            DropTable("dbo.Tbl_ActividadesActosInseguros");
        }
    }
}
