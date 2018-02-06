namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionModeloActaComiteConvivenciaLaboral : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_AccionesActaConvivencia",
                c => new
                    {
                        Pk_Id_AccionActaConvivencia = c.Int(nullable: false, identity: true),
                        FechaProbable = c.DateTime(nullable: false),
                        AccionARealizar = c.String(nullable: false, maxLength: 400),
                        Responsable = c.String(nullable: false, maxLength: 60),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_AccionActaConvivencia)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_ActasConvivencia",
                c => new
                    {
                        PK_Id_Acta = c.Int(nullable: false, identity: true),
                        Consecutivo_Acta = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        TemaReunion = c.String(maxLength: 150),
                        Conclusiones = c.String(maxLength: 400),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        Fk_Id_Sede = c.Int(nullable: false),
                        NombreEmpresa = c.String(maxLength: 150),
                        NombreUsuario = c.String(maxLength: 60),
                        NombreArchivo = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.PK_Id_Acta)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: true)
                .Index(t => t.Fk_Id_Sede);
            
            CreateTable(
                "dbo.Tbl_AccionesActaQuejas",
                c => new
                    {
                        Pk_Id_AccionQueja = c.Int(nullable: false, identity: true),
                        AccionARealizar = c.String(nullable: false, maxLength: 300),
                        Fk_Id_Queja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_AccionQueja)
                .ForeignKey("dbo.Tbl_ActaConvivenciaQuejas", t => t.Fk_Id_Queja, cascadeDelete: true)
                .Index(t => t.Fk_Id_Queja);
            
            CreateTable(
                "dbo.Tbl_ActaConvivenciaQuejas",
                c => new
                    {
                        PK_Id_Queja = c.Int(nullable: false, identity: true),
                        Consecutivo_Queja = c.Int(nullable: false),
                        Consecutivo_Caso = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        NombreRefiereSituacion = c.String(maxLength: 50),
                        AspectosNoResueltos = c.String(maxLength: 300),
                        Compromisos = c.String(maxLength: 300),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_Queja)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_ResponsablesQuejas",
                c => new
                    {
                        Pk_Id_Responsable = c.Int(nullable: false, identity: true),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Fk_Id_Queja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Responsable)
                .ForeignKey("dbo.Tbl_ActaConvivenciaQuejas", t => t.Fk_Id_Queja, cascadeDelete: true)
                .Index(t => t.Fk_Id_Queja);
            
            CreateTable(
                "dbo.Tbl_AuditoriaActaConvivencia",
                c => new
                    {
                        Pk_Id_AuditoriaActa = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        AccionRealizada = c.String(maxLength: 200),
                        Fk_Id_UsuarioSistema = c.Int(nullable: false),
                        NombreUsuario = c.String(maxLength: 60),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_AuditoriaActa)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_UsuarioSistema", t => t.Fk_Id_UsuarioSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_UsuarioSistema)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_CompromisosPendientes",
                c => new
                    {
                        Pk_Id_Compromiso = c.Int(nullable: false, identity: true),
                        CompromisoPendiente = c.String(nullable: false, maxLength: 300),
                        FK_Id_Seguimiento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Compromiso)
                .ForeignKey("dbo.Tbl_SeguimientoActaConvivencia", t => t.FK_Id_Seguimiento, cascadeDelete: true)
                .Index(t => t.FK_Id_Seguimiento);
            
            CreateTable(
                "dbo.Tbl_SeguimientoActaConvivencia",
                c => new
                    {
                        PK_Id_Seguimiento = c.Int(nullable: false, identity: true),
                        Consecutivo_Evento = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        NombreParteInvolucrada = c.String(maxLength: 50),
                        CompromisosAdquiridos = c.String(maxLength: 300),
                        Observaciones = c.String(maxLength: 400),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_Seguimiento)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_MiembrosActaConvivencia",
                c => new
                    {
                        Pk_Id_Miembro = c.Int(nullable: false, identity: true),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Fk_Id_TipoPrioridadMiembro = c.Int(nullable: false),
                        Fk_Id_TipoPrincipal = c.Int(),
                        TipoRepresentante = c.String(maxLength: 15),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Miembro)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_TipoPrincipalActa", t => t.Fk_Id_TipoPrincipal)
                .ForeignKey("dbo.Tbl_TipoPrioridadMiembroComite", t => t.Fk_Id_TipoPrioridadMiembro, cascadeDelete: true)
                .Index(t => t.Fk_Id_TipoPrioridadMiembro)
                .Index(t => t.Fk_Id_TipoPrincipal)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_ParticipantesActasConvivencia",
                c => new
                    {
                        Pk_Id_Participante = c.Int(nullable: false, identity: true),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Participante)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_TemasActaConvivencia",
                c => new
                    {
                        PK_Id_TemaActa = c.Int(nullable: false, identity: true),
                        Tema = c.String(nullable: false, maxLength: 150),
                        Observaciones = c.String(maxLength: 200),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_TemaActa)
                .ForeignKey("dbo.Tbl_ActasConvivencia", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            AddColumn("dbo.Tbl_AccionesActaCopasst", "ActasConvivencia_PK_Id_Acta", c => c.Int());
            AddColumn("dbo.Tbl_AuditoriaActaCopasst", "ActasConvivencia_PK_Id_Acta", c => c.Int());
            AddColumn("dbo.Tbl_MiembrosActaCopasst", "ActasConvivencia_PK_Id_Acta", c => c.Int());
            AddColumn("dbo.Tbl_Participantes", "ActasConvivencia_PK_Id_Acta", c => c.Int());
            AddColumn("dbo.Tbl_TemasActaCopasst", "ActasConvivencia_PK_Id_Acta", c => c.Int());
            AlterColumn("dbo.Tbl_ActasCopasst", "TemaReunion", c => c.String(maxLength: 150));
            CreateIndex("dbo.Tbl_AccionesActaCopasst", "ActasConvivencia_PK_Id_Acta");
            CreateIndex("dbo.Tbl_AuditoriaActaCopasst", "ActasConvivencia_PK_Id_Acta");
            CreateIndex("dbo.Tbl_MiembrosActaCopasst", "ActasConvivencia_PK_Id_Acta");
            CreateIndex("dbo.Tbl_Participantes", "ActasConvivencia_PK_Id_Acta");
            CreateIndex("dbo.Tbl_TemasActaCopasst", "ActasConvivencia_PK_Id_Acta");
            AddForeignKey("dbo.Tbl_AccionesActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia", "PK_Id_Acta");
            AddForeignKey("dbo.Tbl_AuditoriaActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia", "PK_Id_Acta");
            AddForeignKey("dbo.Tbl_MiembrosActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia", "PK_Id_Acta");
            AddForeignKey("dbo.Tbl_Participantes", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia", "PK_Id_Acta");
            AddForeignKey("dbo.Tbl_TemasActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia", "PK_Id_Acta");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_TemasActaConvivencia", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_ParticipantesActasConvivencia", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_MiembrosActaConvivencia", "Fk_Id_TipoPrioridadMiembro", "dbo.Tbl_TipoPrioridadMiembroComite");
            DropForeignKey("dbo.Tbl_MiembrosActaConvivencia", "Fk_Id_TipoPrincipal", "dbo.Tbl_TipoPrincipalActa");
            DropForeignKey("dbo.Tbl_MiembrosActaConvivencia", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_CompromisosPendientes", "FK_Id_Seguimiento", "dbo.Tbl_SeguimientoActaConvivencia");
            DropForeignKey("dbo.Tbl_SeguimientoActaConvivencia", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_AuditoriaActaConvivencia", "Fk_Id_UsuarioSistema", "dbo.Tbl_UsuarioSistema");
            DropForeignKey("dbo.Tbl_AuditoriaActaConvivencia", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_AccionesActaQuejas", "Fk_Id_Queja", "dbo.Tbl_ActaConvivenciaQuejas");
            DropForeignKey("dbo.Tbl_ResponsablesQuejas", "Fk_Id_Queja", "dbo.Tbl_ActaConvivenciaQuejas");
            DropForeignKey("dbo.Tbl_ActaConvivenciaQuejas", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_AccionesActaConvivencia", "Fk_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_TemasActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_ActasConvivencia", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Participantes", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_MiembrosActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_AuditoriaActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropForeignKey("dbo.Tbl_AccionesActaCopasst", "ActasConvivencia_PK_Id_Acta", "dbo.Tbl_ActasConvivencia");
            DropIndex("dbo.Tbl_TemasActaConvivencia", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_ParticipantesActasConvivencia", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_MiembrosActaConvivencia", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_MiembrosActaConvivencia", new[] { "Fk_Id_TipoPrincipal" });
            DropIndex("dbo.Tbl_MiembrosActaConvivencia", new[] { "Fk_Id_TipoPrioridadMiembro" });
            DropIndex("dbo.Tbl_SeguimientoActaConvivencia", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_CompromisosPendientes", new[] { "FK_Id_Seguimiento" });
            DropIndex("dbo.Tbl_AuditoriaActaConvivencia", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_AuditoriaActaConvivencia", new[] { "Fk_Id_UsuarioSistema" });
            DropIndex("dbo.Tbl_ResponsablesQuejas", new[] { "Fk_Id_Queja" });
            DropIndex("dbo.Tbl_ActaConvivenciaQuejas", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_AccionesActaQuejas", new[] { "Fk_Id_Queja" });
            DropIndex("dbo.Tbl_TemasActaCopasst", new[] { "ActasConvivencia_PK_Id_Acta" });
            DropIndex("dbo.Tbl_Participantes", new[] { "ActasConvivencia_PK_Id_Acta" });
            DropIndex("dbo.Tbl_MiembrosActaCopasst", new[] { "ActasConvivencia_PK_Id_Acta" });
            DropIndex("dbo.Tbl_AuditoriaActaCopasst", new[] { "ActasConvivencia_PK_Id_Acta" });
            DropIndex("dbo.Tbl_AccionesActaCopasst", new[] { "ActasConvivencia_PK_Id_Acta" });
            DropIndex("dbo.Tbl_ActasConvivencia", new[] { "Fk_Id_Sede" });
            DropIndex("dbo.Tbl_AccionesActaConvivencia", new[] { "Fk_Id_Acta" });
            AlterColumn("dbo.Tbl_ActasCopasst", "TemaReunion", c => c.String(maxLength: 50));
            DropColumn("dbo.Tbl_TemasActaCopasst", "ActasConvivencia_PK_Id_Acta");
            DropColumn("dbo.Tbl_Participantes", "ActasConvivencia_PK_Id_Acta");
            DropColumn("dbo.Tbl_MiembrosActaCopasst", "ActasConvivencia_PK_Id_Acta");
            DropColumn("dbo.Tbl_AuditoriaActaCopasst", "ActasConvivencia_PK_Id_Acta");
            DropColumn("dbo.Tbl_AccionesActaCopasst", "ActasConvivencia_PK_Id_Acta");
            DropTable("dbo.Tbl_TemasActaConvivencia");
            DropTable("dbo.Tbl_ParticipantesActasConvivencia");
            DropTable("dbo.Tbl_MiembrosActaConvivencia");
            DropTable("dbo.Tbl_SeguimientoActaConvivencia");
            DropTable("dbo.Tbl_CompromisosPendientes");
            DropTable("dbo.Tbl_AuditoriaActaConvivencia");
            DropTable("dbo.Tbl_ResponsablesQuejas");
            DropTable("dbo.Tbl_ActaConvivenciaQuejas");
            DropTable("dbo.Tbl_AccionesActaQuejas");
            DropTable("dbo.Tbl_ActasConvivencia");
            DropTable("dbo.Tbl_AccionesActaConvivencia");
        }
    }
}
