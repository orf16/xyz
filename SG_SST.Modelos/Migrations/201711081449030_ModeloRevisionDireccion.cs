namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloRevisionDireccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ActaRevision",
                c => new
                    {
                        PK_Id_ActaRevision = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Num_Acta = c.Int(nullable: false),
                        Fecha_Creacion_Acta = c.DateTime(nullable: false),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        FK_Sede = c.Int(nullable: false),
                        Fecha_Inicial_Revision = c.DateTime(nullable: false),
                        Fecha_Final_Revision = c.DateTime(nullable: false),
                        Elaborada = c.String(),
                        Firma_Gerente_General = c.String(),
                        Firma_Representante_SGSST = c.Boolean(nullable: false),
                        Firma_Responsable_SGSST = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_ActaRevision)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_Sede, cascadeDelete: true)
                .Index(t => t.FK_Sede);
            
            CreateTable(
                "dbo.Tbl_AdjuntoAgendaRevision",
                c => new
                    {
                        PK_Id_AdjuntoAgendaRevision = c.Int(nullable: false, identity: true),
                        FK_AgendaRevision = c.Int(nullable: false),
                        Nombre_Archivo = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_AdjuntoAgendaRevision)
                .ForeignKey("dbo.Tbl_AgendaRevision", t => t.FK_AgendaRevision, cascadeDelete: true)
                .Index(t => t.FK_AgendaRevision);
            
            CreateTable(
                "dbo.Tbl_AgendaRevision",
                c => new
                    {
                        PK_Id_Agenda = c.Int(nullable: false, identity: true),
                        FK_ActaRevision = c.Int(nullable: false),
                        Titulo = c.String(),
                        Desarrollo = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_Agenda)
                .ForeignKey("dbo.Tbl_ActaRevision", t => t.FK_ActaRevision, cascadeDelete: true)
                .Index(t => t.FK_ActaRevision);
            
            CreateTable(
                "dbo.Tbl_ItemRevision",
                c => new
                    {
                        PK_Id_ItemRevision = c.Int(nullable: false, identity: true),
                        Tema = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_ItemRevision);
            
            CreateTable(
                "dbo.Tbl_ParticipanteRevision",
                c => new
                    {
                        PK_Id_ParticipanteRevision = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Documento = c.String(),
                        Cargo = c.String(),
                        FK_ActaRevision = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_ParticipanteRevision)
                .ForeignKey("dbo.Tbl_ActaRevision", t => t.FK_ActaRevision, cascadeDelete: true)
                .Index(t => t.FK_ActaRevision);
            
            CreateTable(
                "dbo.Tbl_PlanAccionRevision",
                c => new
                    {
                        PK_Id_PlanAccion = c.Int(nullable: false, identity: true),
                        FK_Acta = c.Int(nullable: false),
                        Actividad = c.String(),
                        Responsable = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Num_Acta = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_PlanAccion)
                .ForeignKey("dbo.Tbl_ActaRevision", t => t.FK_Acta, cascadeDelete: true)
                .Index(t => t.FK_Acta);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_PlanAccionRevision", "FK_Acta", "dbo.Tbl_ActaRevision");
            DropForeignKey("dbo.Tbl_ParticipanteRevision", "FK_ActaRevision", "dbo.Tbl_ActaRevision");
            DropForeignKey("dbo.Tbl_AdjuntoAgendaRevision", "FK_AgendaRevision", "dbo.Tbl_AgendaRevision");
            DropForeignKey("dbo.Tbl_AgendaRevision", "FK_ActaRevision", "dbo.Tbl_ActaRevision");
            DropForeignKey("dbo.Tbl_ActaRevision", "FK_Sede", "dbo.Tbl_Sede");
            DropIndex("dbo.Tbl_PlanAccionRevision", new[] { "FK_Acta" });
            DropIndex("dbo.Tbl_ParticipanteRevision", new[] { "FK_ActaRevision" });
            DropIndex("dbo.Tbl_AgendaRevision", new[] { "FK_ActaRevision" });
            DropIndex("dbo.Tbl_AdjuntoAgendaRevision", new[] { "FK_AgendaRevision" });
            DropIndex("dbo.Tbl_ActaRevision", new[] { "FK_Sede" });
            DropTable("dbo.Tbl_PlanAccionRevision");
            DropTable("dbo.Tbl_ParticipanteRevision");
            DropTable("dbo.Tbl_ItemRevision");
            DropTable("dbo.Tbl_AgendaRevision");
            DropTable("dbo.Tbl_AdjuntoAgendaRevision");
            DropTable("dbo.Tbl_ActaRevision");
        }
    }
}
