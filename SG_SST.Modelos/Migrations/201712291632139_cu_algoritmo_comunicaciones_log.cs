namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_algoritmo_comunicaciones_log : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ComunicacionesLog",
                c => new
                    {
                        pk_id_log = c.Int(nullable: false, identity: true),
                        fk_id_comunicaciones = c.Int(nullable: false),
                        enviado_rechazado = c.Boolean(nullable: false),
                        modulo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_log);
            
            AddColumn("dbo.Tbl_ComunicacionesInternas", "TokenPublico", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ComunicacionesInternas", "TokenPublico");
            DropTable("dbo.Tbl_ComunicacionesLog");
        }
    }
}
