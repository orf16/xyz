namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModuloAdministracionUsuariosSistema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_CausalesRechazoUsuariosSistema",
                c => new
                    {
                        Pk_Id_CausalRechazo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_CausalRechazo);
            
            CreateTable(
                "dbo.Tbl_PermisosDenegadosPorRol",
                c => new
                    {
                        Pk_Id_PermisoDenegadoPorRol = c.Int(nullable: false, identity: true),
                        Fk_Id_PermisoSistema = c.Int(nullable: false),
                        Fk_Id_RolSistema = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_PermisoDenegadoPorRol)
                .ForeignKey("dbo.Tbl_PermisosSistema", t => t.Fk_Id_PermisoSistema, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_RolesSistema", t => t.Fk_Id_RolSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_PermisoSistema)
                .Index(t => t.Fk_Id_RolSistema);
            
            CreateTable(
                "dbo.Tbl_PermisosSistema",
                c => new
                    {
                        Pk_Id_PermisoSistema = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Controlador = c.String(),
                        Accion = c.String(),
                        Vista = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_PermisoSistema);
            
            CreateTable(
                "dbo.Tbl_RolesSistema",
                c => new
                    {
                        Pk_Id_RolSistema = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Sigla = c.String(),
                        Descripcion = c.String(),
                        CantidadUsuariosPorRol = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_RolSistema);
            
            CreateTable(
                "dbo.Tbl_RecursosPorRol",
                c => new
                    {
                        Pk_Id_RecursoPorRol = c.Int(nullable: false, identity: true),
                        Fk_Id_RolSistema = c.Int(nullable: false),
                        Fk_Id_Recurso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_RecursoPorRol)
                .ForeignKey("dbo.Tbl_RecursosSistema", t => t.Fk_Id_Recurso, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_RolesSistema", t => t.Fk_Id_RolSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_RolSistema)
                .Index(t => t.Fk_Id_Recurso);
            
            CreateTable(
                "dbo.Tbl_RecursosSistema",
                c => new
                    {
                        Pk_Id_RecursoSistema = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        UrlRecurso = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_RecursoSistema);
            
            CreateTable(
                "dbo.Tbl_UsuariosParaAprobar",
                c => new
                    {
                        Pk_id_UsuarioParaAprobar = c.Int(nullable: false, identity: true),
                        TipoDocumentoEmpresa = c.Int(nullable: false),
                        NumeroDocumentoEmprsa = c.String(),
                        TipoDocumentoUsuario = c.Int(nullable: false),
                        NumeroDocumentoUsuario = c.String(),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                        Fk_Id_RolSistema = c.Int(nullable: false),
                        EmailUsuario = c.String(),
                    })
                .PrimaryKey(t => t.Pk_id_UsuarioParaAprobar)
                .ForeignKey("dbo.Tbl_RolesSistema", t => t.Fk_Id_RolSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_RolSistema);
            
            CreateTable(
                "dbo.Tbl_UsuariosPorRol",
                c => new
                    {
                        Pk_Id_UsuarioPorRol = c.Int(nullable: false, identity: true),
                        Fk_Id_UsuarioSistema = c.Int(nullable: false),
                        Fk_Id_RolSistema = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_UsuarioPorRol)
                .ForeignKey("dbo.Tbl_RolesSistema", t => t.Fk_Id_RolSistema, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_UsuarioSistema", t => t.Fk_Id_UsuarioSistema, cascadeDelete: true)
                .Index(t => t.Fk_Id_UsuarioSistema)
                .Index(t => t.Fk_Id_RolSistema);
            
            CreateTable(
                "dbo.Tbl_UsuariosRechazadosSitema",
                c => new
                    {
                        Pk_Id_UsuarioRechazadoSistema = c.Int(nullable: false, identity: true),
                        Fk_Id_UsuarioParaActivar = c.Int(nullable: false),
                        Fk_Id_CausalRechazoUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_UsuarioRechazadoSistema)
                .ForeignKey("dbo.Tbl_CausalesRechazoUsuariosSistema", t => t.Fk_Id_CausalRechazoUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_UsuariosParaAprobar", t => t.Fk_Id_UsuarioParaActivar, cascadeDelete: true)
                .Index(t => t.Fk_Id_UsuarioParaActivar)
                .Index(t => t.Fk_Id_CausalRechazoUsuario);
            
            AddColumn("dbo.Tbl_UsuarioSistema", "Fk_Id_TipoDocumento", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_UsuarioSistema", "ClaveSalt", c => c.String());
            AddColumn("dbo.Tbl_UsuarioSistema", "ClaveHash", c => c.String());
            AddColumn("dbo.Tbl_UsuarioSistema", "PrimerAcceso", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Tbl_UsuarioSistema", "Fk_Id_TipoDocumento");
            AddForeignKey("dbo.Tbl_UsuarioSistema", "Fk_Id_TipoDocumento", "dbo.Tbl_Tipo_Documento", "PK_IDTipo_Documento", cascadeDelete: true);
            DropColumn("dbo.Tbl_UsuarioSistema", "Clave");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_UsuarioSistema", "Clave", c => c.String());
            DropForeignKey("dbo.Tbl_UsuariosRechazadosSitema", "Fk_Id_UsuarioParaActivar", "dbo.Tbl_UsuariosParaAprobar");
            DropForeignKey("dbo.Tbl_UsuariosRechazadosSitema", "Fk_Id_CausalRechazoUsuario", "dbo.Tbl_CausalesRechazoUsuariosSistema");
            DropForeignKey("dbo.Tbl_UsuariosPorRol", "Fk_Id_UsuarioSistema", "dbo.Tbl_UsuarioSistema");
            DropForeignKey("dbo.Tbl_UsuariosPorRol", "Fk_Id_RolSistema", "dbo.Tbl_RolesSistema");
            DropForeignKey("dbo.Tbl_UsuariosParaAprobar", "Fk_Id_RolSistema", "dbo.Tbl_RolesSistema");
            DropForeignKey("dbo.Tbl_RecursosPorRol", "Fk_Id_RolSistema", "dbo.Tbl_RolesSistema");
            DropForeignKey("dbo.Tbl_RecursosPorRol", "Fk_Id_Recurso", "dbo.Tbl_RecursosSistema");
            DropForeignKey("dbo.Tbl_PermisosDenegadosPorRol", "Fk_Id_RolSistema", "dbo.Tbl_RolesSistema");
            DropForeignKey("dbo.Tbl_PermisosDenegadosPorRol", "Fk_Id_PermisoSistema", "dbo.Tbl_PermisosSistema");
            DropForeignKey("dbo.Tbl_UsuarioSistema", "Fk_Id_TipoDocumento", "dbo.Tbl_Tipo_Documento");
            DropIndex("dbo.Tbl_UsuariosRechazadosSitema", new[] { "Fk_Id_CausalRechazoUsuario" });
            DropIndex("dbo.Tbl_UsuariosRechazadosSitema", new[] { "Fk_Id_UsuarioParaActivar" });
            DropIndex("dbo.Tbl_UsuariosPorRol", new[] { "Fk_Id_RolSistema" });
            DropIndex("dbo.Tbl_UsuariosPorRol", new[] { "Fk_Id_UsuarioSistema" });
            DropIndex("dbo.Tbl_UsuariosParaAprobar", new[] { "Fk_Id_RolSistema" });
            DropIndex("dbo.Tbl_RecursosPorRol", new[] { "Fk_Id_Recurso" });
            DropIndex("dbo.Tbl_RecursosPorRol", new[] { "Fk_Id_RolSistema" });
            DropIndex("dbo.Tbl_PermisosDenegadosPorRol", new[] { "Fk_Id_RolSistema" });
            DropIndex("dbo.Tbl_PermisosDenegadosPorRol", new[] { "Fk_Id_PermisoSistema" });
            DropIndex("dbo.Tbl_UsuarioSistema", new[] { "Fk_Id_TipoDocumento" });
            DropColumn("dbo.Tbl_UsuarioSistema", "PrimerAcceso");
            DropColumn("dbo.Tbl_UsuarioSistema", "ClaveHash");
            DropColumn("dbo.Tbl_UsuarioSistema", "ClaveSalt");
            DropColumn("dbo.Tbl_UsuarioSistema", "Fk_Id_TipoDocumento");
            DropTable("dbo.Tbl_UsuariosRechazadosSitema");
            DropTable("dbo.Tbl_UsuariosPorRol");
            DropTable("dbo.Tbl_UsuariosParaAprobar");
            DropTable("dbo.Tbl_RecursosSistema");
            DropTable("dbo.Tbl_RecursosPorRol");
            DropTable("dbo.Tbl_RolesSistema");
            DropTable("dbo.Tbl_PermisosSistema");
            DropTable("dbo.Tbl_PermisosDenegadosPorRol");
            DropTable("dbo.Tbl_CausalesRechazoUsuariosSistema");
        }
    }
}
