namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacionTablasUC45ParticipacionConsultaSST : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ConsultaSST", "Fecha_Consulta", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_ConsultaSST", "Fecha_Revision", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tbl_ConsultaSST", "Observaciones", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "NombreArchivo1", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "NombreArchivo1_download", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "Ruta1", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "NombreArchivo2", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "NombreArchivo2_download", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "Ruta2", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "NombreArchivo3", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "NombreArchivo3_download", c => c.String());
            AddColumn("dbo.Tbl_ConsultaSST", "Ruta3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ConsultaSST", "Ruta3");
            DropColumn("dbo.Tbl_ConsultaSST", "NombreArchivo3_download");
            DropColumn("dbo.Tbl_ConsultaSST", "NombreArchivo3");
            DropColumn("dbo.Tbl_ConsultaSST", "Ruta2");
            DropColumn("dbo.Tbl_ConsultaSST", "NombreArchivo2_download");
            DropColumn("dbo.Tbl_ConsultaSST", "NombreArchivo2");
            DropColumn("dbo.Tbl_ConsultaSST", "Ruta1");
            DropColumn("dbo.Tbl_ConsultaSST", "NombreArchivo1_download");
            DropColumn("dbo.Tbl_ConsultaSST", "NombreArchivo1");
            DropColumn("dbo.Tbl_ConsultaSST", "Observaciones");
            DropColumn("dbo.Tbl_ConsultaSST", "Fecha_Revision");
            DropColumn("dbo.Tbl_ConsultaSST", "Fecha_Consulta");
        }
    }
}
