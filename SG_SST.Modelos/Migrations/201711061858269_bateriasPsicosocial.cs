namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bateriasPsicosocial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_BateriaGestion",
                c => new
                    {
                        Pk_Id_BateriaGestion = c.Int(nullable: false, identity: true),
                        FechaRegistro = c.DateTime(nullable: false),
                        FechaFinalizacion = c.DateTime(nullable: false),
                        Finalizada = c.Boolean(nullable: false),
                        Estado = c.Int(nullable: false),
                        bateriaExtra = c.Int(nullable: false),
                        TokenPublico = c.String(nullable: false, maxLength: 50),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        Fk_Id_Bateria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_BateriaGestion)
                .ForeignKey("dbo.Tbl_Bateria", t => t.Fk_Id_Bateria, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: false)
                .Index(t => t.Fk_Id_Empresa)
                .Index(t => t.Fk_Id_Bateria);
            
            CreateTable(
                "dbo.Tbl_Bateria",
                c => new
                    {
                        Pk_Id_Bateria = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripción = c.String(nullable: false, maxLength: 1000),
                        Fecha_publicacion = c.String(nullable: false, maxLength: 1000),
                        TiposAplicacion = c.String(nullable: false, maxLength: 1000),
                        ModalidadesAplicacion = c.String(nullable: false, maxLength: 1000),
                        Poblacion = c.String(nullable: false, maxLength: 1000),
                        Objetivo = c.String(nullable: false, maxLength: 1000),
                        Baremacion = c.String(nullable: false, maxLength: 1000),
                        TipoInstrumento = c.String(nullable: false, maxLength: 1000),
                        NumeroItems = c.String(nullable: false, maxLength: 1000),
                        DuracionAplicacion = c.String(nullable: false, maxLength: 1000),
                        Materiales = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Pk_Id_Bateria);
            
            CreateTable(
                "dbo.Tbl_BateriaDimension",
                c => new
                    {
                        Pk_Id_BateriaDimension = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 1000),
                        Descripcion = c.String(nullable: false, maxLength: 1000),
                        Fk_Id_Bateria = c.Int(nullable: false),
                        FactorTransformacion = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_BateriaDimension)
                .ForeignKey("dbo.Tbl_Bateria", t => t.Fk_Id_Bateria, cascadeDelete: true)
                .Index(t => t.Fk_Id_Bateria);
            
            CreateTable(
                "dbo.Tbl_BateriaCuestionario",
                c => new
                    {
                        Pk_Id_BateriaCuestionario = c.Int(nullable: false, identity: true),
                        Pregunta = c.String(nullable: false, maxLength: 1000),
                        Fk_Id_BateriaDimension = c.Int(nullable: false),
                        Orden = c.Int(nullable: false),
                        Pagina = c.Int(nullable: false),
                        Dominio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_BateriaCuestionario)
                .ForeignKey("dbo.Tbl_BateriaDimension", t => t.Fk_Id_BateriaDimension, cascadeDelete: true)
                .Index(t => t.Fk_Id_BateriaDimension);
            
            CreateTable(
                "dbo.Tbl_BateriaResultado",
                c => new
                    {
                        Pk_Id_BateriaResultado = c.Int(nullable: false, identity: true),
                        Fk_Id_BateriaUsuario = c.Int(nullable: false),
                        Fk_Id_BateriaCuestionario = c.Int(nullable: false),
                        Valor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_BateriaResultado)
                .ForeignKey("dbo.Tbl_BateriaCuestionario", t => t.Fk_Id_BateriaCuestionario, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_BateriaUsuario", t => t.Fk_Id_BateriaUsuario, cascadeDelete: true)
                .Index(t => t.Fk_Id_BateriaUsuario)
                .Index(t => t.Fk_Id_BateriaCuestionario);
            
            CreateTable(
                "dbo.Tbl_BateriaUsuario",
                c => new
                    {
                        Pk_Id_BateriaUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 3000),
                        NumeroIdentificacion = c.String(nullable: false, maxLength: 1000),
                        TipoDocumento = c.String(nullable: false, maxLength: 500),
                        CorreoElectronico = c.String(maxLength: 1000),
                        Id_Empresa = c.Int(nullable: false),
                        TipoConv = c.Int(nullable: false),
                        EstadoEnvio = c.Int(nullable: false),
                        NumeroIntentos = c.Int(nullable: false),
                        RegistroOperacion = c.String(),
                        RegistroOperacionExtra = c.String(),
                        MailBody = c.String(),
                        CheckPag9 = c.String(),
                        CheckPag10 = c.String(),
                        DocumentoDigitado = c.String(),
                        ConfirmacionParticipacion = c.String(),
                        FechaEnvio = c.DateTime(),
                        FechaRespuesta = c.DateTime(),
                        Edad = c.String(maxLength: 3000),
                        NombreEvaluador = c.String(maxLength: 3000),
                        IdEvaluador = c.String(maxLength: 3000),
                        Profesion = c.String(maxLength: 3000),
                        Postgrado = c.String(maxLength: 3000),
                        TarjetaProfesional = c.String(maxLength: 3000),
                        Licencia = c.String(maxLength: 3000),
                        FechaExpedicion = c.DateTime(),
                        TokenPrivado = c.String(nullable: false, maxLength: 25),
                        Fk_Id_BateriaGestion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_BateriaUsuario)
                .ForeignKey("dbo.Tbl_BateriaGestion", t => t.Fk_Id_BateriaGestion, cascadeDelete: true)
                .Index(t => t.Fk_Id_BateriaGestion);
            
            CreateTable(
                "dbo.Tbl_BateriaInicial",
                c => new
                    {
                        Pk_Id_BateriaInicial = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Sexo = c.String(),
                        AñoNac = c.String(),
                        EstadoCivil = c.String(),
                        NivEstudios = c.String(),
                        Profesion = c.String(),
                        ResidenciaMun = c.String(),
                        ResidenciaDep = c.String(),
                        Estrato = c.String(),
                        TipoVivienda = c.String(),
                        PersonasDependen = c.String(),
                        LugarTrabajoMun = c.String(),
                        LugarTrabajoDep = c.String(),
                        AñosConEmpresa = c.String(),
                        AñosConEmpresaNum = c.String(),
                        CargoConEmpresa = c.String(),
                        TipoCargo = c.String(),
                        AñosOficio = c.String(),
                        AñosOficioNum = c.String(),
                        AreaEmpresa = c.String(),
                        TipoContrato = c.String(),
                        HorasEstablecidas = c.String(),
                        TipoSalario = c.String(),
                        Fk_Id_BateriaUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_BateriaInicial);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_BateriaGestion", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_BateriaGestion", "Fk_Id_Bateria", "dbo.Tbl_Bateria");
            DropForeignKey("dbo.Tbl_BateriaResultado", "Fk_Id_BateriaUsuario", "dbo.Tbl_BateriaUsuario");
            DropForeignKey("dbo.Tbl_BateriaUsuario", "Fk_Id_BateriaGestion", "dbo.Tbl_BateriaGestion");
            DropForeignKey("dbo.Tbl_BateriaResultado", "Fk_Id_BateriaCuestionario", "dbo.Tbl_BateriaCuestionario");
            DropForeignKey("dbo.Tbl_BateriaCuestionario", "Fk_Id_BateriaDimension", "dbo.Tbl_BateriaDimension");
            DropForeignKey("dbo.Tbl_BateriaDimension", "Fk_Id_Bateria", "dbo.Tbl_Bateria");
            DropIndex("dbo.Tbl_BateriaUsuario", new[] { "Fk_Id_BateriaGestion" });
            DropIndex("dbo.Tbl_BateriaResultado", new[] { "Fk_Id_BateriaCuestionario" });
            DropIndex("dbo.Tbl_BateriaResultado", new[] { "Fk_Id_BateriaUsuario" });
            DropIndex("dbo.Tbl_BateriaCuestionario", new[] { "Fk_Id_BateriaDimension" });
            DropIndex("dbo.Tbl_BateriaDimension", new[] { "Fk_Id_Bateria" });
            DropIndex("dbo.Tbl_BateriaGestion", new[] { "Fk_Id_Bateria" });
            DropIndex("dbo.Tbl_BateriaGestion", new[] { "Fk_Id_Empresa" });
            DropTable("dbo.Tbl_BateriaInicial");
            DropTable("dbo.Tbl_BateriaUsuario");
            DropTable("dbo.Tbl_BateriaResultado");
            DropTable("dbo.Tbl_BateriaCuestionario");
            DropTable("dbo.Tbl_BateriaDimension");
            DropTable("dbo.Tbl_Bateria");
            DropTable("dbo.Tbl_BateriaGestion");
        }
    }
}
