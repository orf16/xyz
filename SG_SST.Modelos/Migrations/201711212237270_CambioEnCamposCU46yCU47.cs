namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambioEnCamposCU46yCU47 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tbl_AccionesActaConvivencia", "AccionARealizar", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Tbl_ActasConvivencia", "TemaReunion", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_ActasConvivencia", "Conclusiones", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_AccionesActaCopasst", "AccionARealizar", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Tbl_ActasCopasst", "TemaReunion", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_ActasCopasst", "Conclusiones", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_TemasActaCopasst", "Tema", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_TemasActaCopasst", "Observaciones", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_AccionesActaQuejas", "AccionARealizar", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Tbl_ActaConvivenciaQuejas", "AspectosNoResueltos", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_ActaConvivenciaQuejas", "Compromisos", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_CompromisosPendientes", "CompromisoPendiente", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Tbl_SeguimientoActaConvivencia", "CompromisosAdquiridos", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_SeguimientoActaConvivencia", "Observaciones", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tbl_TemasActaConvivencia", "Tema", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_TemasActaConvivencia", "Observaciones", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_TemasActaConvivencia", "Observaciones", c => c.String(maxLength: 200));
            AlterColumn("dbo.Tbl_TemasActaConvivencia", "Tema", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Tbl_SeguimientoActaConvivencia", "Observaciones", c => c.String(maxLength: 400));
            AlterColumn("dbo.Tbl_SeguimientoActaConvivencia", "CompromisosAdquiridos", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_CompromisosPendientes", "CompromisoPendiente", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_ActaConvivenciaQuejas", "Compromisos", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_ActaConvivenciaQuejas", "AspectosNoResueltos", c => c.String(maxLength: 300));
            AlterColumn("dbo.Tbl_AccionesActaQuejas", "AccionARealizar", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Tbl_TemasActaCopasst", "Observaciones", c => c.String(maxLength: 200));
            AlterColumn("dbo.Tbl_TemasActaCopasst", "Tema", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Tbl_ActasCopasst", "Conclusiones", c => c.String(maxLength: 400));
            AlterColumn("dbo.Tbl_ActasCopasst", "TemaReunion", c => c.String(maxLength: 150));
            AlterColumn("dbo.Tbl_AccionesActaCopasst", "AccionARealizar", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Tbl_ActasConvivencia", "Conclusiones", c => c.String(maxLength: 400));
            AlterColumn("dbo.Tbl_ActasConvivencia", "TemaReunion", c => c.String(maxLength: 150));
            AlterColumn("dbo.Tbl_AccionesActaConvivencia", "AccionARealizar", c => c.String(nullable: false, maxLength: 400));
        }
    }
}
