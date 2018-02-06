namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_20_algoritmo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Eme_FrentesAccionAdjuntos",
                c => new
                    {
                        pk_id_adjunto = c.Int(nullable: false, identity: true),
                        fk_id_sede = c.String(),
                        plan_seguridad_fisica = c.String(),
                        plan_atencion_medica = c.String(),
                        plan_contraincendios = c.String(),
                        plan_rutas_evacuacion = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_adjunto);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_Eme_FrentesAccionAdjuntos");
        }
    }
}
