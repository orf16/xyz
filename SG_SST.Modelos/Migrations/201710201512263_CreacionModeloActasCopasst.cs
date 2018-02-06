namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionModeloActasCopasst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_AccionesActaCopasst",
                c => new
                    {
                        Pk_Id_AccionActaCopasst = c.Int(nullable: false, identity: true),
                        FechaProbable = c.DateTime(nullable: false),
                        AccionARealizar = c.String(nullable: false, maxLength: 400),
                        Responsable = c.String(nullable: false, maxLength: 60),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_AccionActaCopasst)
                .ForeignKey("dbo.Tbl_ActasCopasst", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_ActasCopasst",
                c => new
                    {
                        PK_Id_Acta = c.Int(nullable: false, identity: true),
                        Consecutivo_Acta = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        TemaReunion = c.String(maxLength: 50),
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
                "dbo.Tbl_AuditoriaActaCopasst",
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
                .ForeignKey("dbo.Tbl_ActasCopasst", t => t.Fk_Id_Acta, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_UsuarioSistema", t => t.Fk_Id_UsuarioSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_UsuarioSistema)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_MiembrosActaCopasst",
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
                .ForeignKey("dbo.Tbl_ActasCopasst", t => t.Fk_Id_Acta, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_TipoPrincipalActa", t => t.Fk_Id_TipoPrincipal)
                .ForeignKey("dbo.Tbl_TipoPrioridadMiembroComite", t => t.Fk_Id_TipoPrioridadMiembro, cascadeDelete: true)
                .Index(t => t.Fk_Id_TipoPrioridadMiembro)
                .Index(t => t.Fk_Id_TipoPrincipal)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_TipoPrincipalActa",
                c => new
                    {
                        PK_Id_TipoPrincipal = c.Int(nullable: false, identity: true),
                        DescripcionTipoPrincipal = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.PK_Id_TipoPrincipal);
            
            CreateTable(
                "dbo.Tbl_TipoPrioridadMiembroComite",
                c => new
                    {
                        PK_Id_TipoPrioridadMiembro = c.Int(nullable: false, identity: true),
                        DescripcionTipoMiembro = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.PK_Id_TipoPrioridadMiembro);
            
            CreateTable(
                "dbo.Tbl_Participantes",
                c => new
                    {
                        Pk_Id_Participante = c.Int(nullable: false, identity: true),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Participante)
                .ForeignKey("dbo.Tbl_ActasCopasst", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
            CreateTable(
                "dbo.Tbl_TemasActaCopasst",
                c => new
                    {
                        PK_Id_TemaActa = c.Int(nullable: false, identity: true),
                        Tema = c.String(nullable: false, maxLength: 150),
                        Observaciones = c.String(maxLength: 200),
                        Fk_Id_Acta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_TemaActa)
                .ForeignKey("dbo.Tbl_ActasCopasst", t => t.Fk_Id_Acta, cascadeDelete: true)
                .Index(t => t.Fk_Id_Acta);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_AccionesActaCopasst", "Fk_Id_Acta", "dbo.Tbl_ActasCopasst");
            DropForeignKey("dbo.Tbl_TemasActaCopasst", "Fk_Id_Acta", "dbo.Tbl_ActasCopasst");
            DropForeignKey("dbo.Tbl_ActasCopasst", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Participantes", "Fk_Id_Acta", "dbo.Tbl_ActasCopasst");
            DropForeignKey("dbo.Tbl_MiembrosActaCopasst", "Fk_Id_TipoPrioridadMiembro", "dbo.Tbl_TipoPrioridadMiembroComite");
            DropForeignKey("dbo.Tbl_MiembrosActaCopasst", "Fk_Id_TipoPrincipal", "dbo.Tbl_TipoPrincipalActa");
            DropForeignKey("dbo.Tbl_MiembrosActaCopasst", "Fk_Id_Acta", "dbo.Tbl_ActasCopasst");
            DropForeignKey("dbo.Tbl_AuditoriaActaCopasst", "Fk_Id_UsuarioSistema", "dbo.Tbl_UsuarioSistema");
            DropForeignKey("dbo.Tbl_AuditoriaActaCopasst", "Fk_Id_Acta", "dbo.Tbl_ActasCopasst");
            DropIndex("dbo.Tbl_TemasActaCopasst", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_Participantes", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_MiembrosActaCopasst", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_MiembrosActaCopasst", new[] { "Fk_Id_TipoPrincipal" });
            DropIndex("dbo.Tbl_MiembrosActaCopasst", new[] { "Fk_Id_TipoPrioridadMiembro" });
            DropIndex("dbo.Tbl_AuditoriaActaCopasst", new[] { "Fk_Id_Acta" });
            DropIndex("dbo.Tbl_AuditoriaActaCopasst", new[] { "Fk_Id_UsuarioSistema" });
            DropIndex("dbo.Tbl_ActasCopasst", new[] { "Fk_Id_Sede" });
            DropIndex("dbo.Tbl_AccionesActaCopasst", new[] { "Fk_Id_Acta" });
            DropTable("dbo.Tbl_TemasActaCopasst");
            DropTable("dbo.Tbl_Participantes");
            DropTable("dbo.Tbl_TipoPrioridadMiembroComite");
            DropTable("dbo.Tbl_TipoPrincipalActa");
            DropTable("dbo.Tbl_MiembrosActaCopasst");
            DropTable("dbo.Tbl_AuditoriaActaCopasst");
            DropTable("dbo.Tbl_ActasCopasst");
            DropTable("dbo.Tbl_AccionesActaCopasst");
        }
    }
}
