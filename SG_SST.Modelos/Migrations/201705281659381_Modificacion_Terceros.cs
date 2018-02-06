namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacion_Terceros : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_TipoTercero",
                c => new
                    {
                        Pk_Id_TipoTercero = c.Int(nullable: false, identity: true),
                        Descripcion_Tipo_Tercero = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_TipoTercero);
            
            AddColumn("dbo.Tbl_EmpleadoTercero", "Numero_Documento_Empl", c => c.String());
            DropColumn("dbo.Tbl_EmpleadoTercero", "PK_Numero_Documento_Empl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_EmpleadoTercero", "PK_Numero_Documento_Empl", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_EmpleadoTercero", "Numero_Documento_Empl");
            DropTable("dbo.Tbl_TipoTercero");
        }
    }
}
