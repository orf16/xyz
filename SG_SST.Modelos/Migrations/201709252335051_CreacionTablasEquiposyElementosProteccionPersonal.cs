namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionTablasEquiposyElementosProteccionPersonal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                            "dbo.Tbl_AdministracionEMHInspecciones",
                            c => new
                            {
                                Pk_Id_EHMInspecciones = c.Int(nullable: false, identity: true),
                                Fk_Id_Inspecciones = c.Int(nullable: false),
                                Fk_Id_AdmoEMH = c.Int(nullable: false),
                            })
                            .PrimaryKey(t => t.Pk_Id_EHMInspecciones)
                            .ForeignKey("dbo.Tbl_AdministracionEMH", t => t.Fk_Id_AdmoEMH, cascadeDelete: false)
                            .ForeignKey("dbo.Tbl_Inspecciones", t => t.Fk_Id_Inspecciones, cascadeDelete: true)
                            .Index(t => t.Fk_Id_Inspecciones)
                            .Index(t => t.Fk_Id_AdmoEMH);

            CreateTable(
                "dbo.Tbl_AdministracionEMH",
                c => new
                {
                    Pk_Id_AdmoEMH = c.Int(nullable: false, identity: true),
                    TipoElemento = c.String(nullable: false, maxLength: 50),
                    NombreElemento = c.String(nullable: false, maxLength: 250),
                    CodigoElemento = c.String(nullable: false, maxLength: 250),
                    Marca = c.String(nullable: false, maxLength: 250),
                    Modelo = c.String(nullable: false, maxLength: 250),
                    Fabricante = c.String(nullable: false, maxLength: 250),
                    Fecha_Fab = c.DateTime(nullable: false),
                    HorasVida = c.Int(nullable: false),
                    Ubicacion = c.String(nullable: false, maxLength: 250),
                    Caracteristicas = c.String(nullable: false, maxLength: 2000),
                    NombreResponsable = c.String(nullable: false, maxLength: 500),
                    CargoResponsable = c.String(nullable: false, maxLength: 250),
                    Estado = c.Short(nullable: false),
                    ArchivoImagen1 = c.String(maxLength: 2000),
                    ArchivoImagen1_download = c.String(maxLength: 2000),
                    RutaImage1 = c.String(maxLength: 3000),
                    ArchivoImagen2 = c.String(maxLength: 2000),
                    ArchivoImagen2_download = c.String(maxLength: 2000),
                    RutaImage2 = c.String(maxLength: 3000),
                    ArchivoImagen3 = c.String(maxLength: 2000),
                    ArchivoImagen3_download = c.String(maxLength: 2000),
                    RutaImage3 = c.String(maxLength: 3000),
                    ArchivoImagen4 = c.String(maxLength: 2000),
                    ArchivoImagen4_download = c.String(maxLength: 2000),
                    RutaImage4 = c.String(maxLength: 3000),
                    ArchivoImagen5 = c.String(maxLength: 2000),
                    ArchivoImagen5_download = c.String(maxLength: 2000),
                    RutaImage5 = c.String(maxLength: 3000),
                    NombreArchivo1 = c.String(maxLength: 2000),
                    NombreArchivo1_download = c.String(maxLength: 2000),
                    Ruta1 = c.String(maxLength: 3000),
                    NombreArchivo2 = c.String(maxLength: 2000),
                    NombreArchivo2_download = c.String(),
                    Ruta2 = c.String(maxLength: 3000),
                    NombreArchivo3 = c.String(maxLength: 2000),
                    NombreArchivo3_download = c.String(),
                    Ruta3 = c.String(maxLength: 3000),
                    Fecha_Baja = c.DateTime(),
                    Motivo_Baja = c.String(maxLength: 250),
                    Fk_Id_Empresa = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_AdmoEMH)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa);

            CreateTable(
                "dbo.Tbl_EPP",
                c => new
                {
                    Pk_Id_EPP = c.Int(nullable: false, identity: true),
                    NombreEPP = c.String(nullable: false, maxLength: 250),
                    ParteCuerpo = c.String(nullable: false, maxLength: 250),
                    EspecificacionTecnica = c.String(nullable: false, maxLength: 1000),
                    Uso = c.String(nullable: false, maxLength: 1000),
                    Mantenimiento = c.String(nullable: false, maxLength: 1000),
                    VidaUtil = c.String(nullable: false, maxLength: 200),
                    Reposicion = c.String(nullable: false, maxLength: 200),
                    DisposicionFinal = c.String(nullable: false, maxLength: 1000),
                    ArchivoImagen = c.String(maxLength: 2000),
                    ArchivoImagen_download = c.String(maxLength: 2000),
                    RutaImage = c.String(maxLength: 3000),
                    NombreArchivo = c.String(maxLength: 2000),
                    NombreArchivo_download = c.String(maxLength: 2000),
                    RutaArchivo = c.String(maxLength: 3000),
                    Fk_Id_Clasificacion_De_Peligro = c.Int(nullable: false),
                    Fk_Id_Empresa = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_EPP)
                .ForeignKey("dbo.Tbl_Clasificacion_De_Peligro", t => t.Fk_Id_Clasificacion_De_Peligro, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Clasificacion_De_Peligro)
                .Index(t => t.Fk_Id_Empresa);

            CreateTable(
                "dbo.Tbl_EPPCargo",
                c => new
                {
                    Pk_Id_EPPCargo = c.Int(nullable: false, identity: true),
                    Cantidad = c.Int(nullable: false),
                    Fk_Id_Cargo = c.Int(nullable: false),
                    Fk_Id_EPP = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_EPPCargo)
                .ForeignKey("dbo.Tbl_EPP", t => t.Fk_Id_EPP, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Cargo", t => t.Fk_Id_Cargo, cascadeDelete: false)
                .Index(t => t.Fk_Id_Cargo)
                .Index(t => t.Fk_Id_EPP);

            CreateTable(
                "dbo.Tbl_EPPSuministro",
                c => new
                {
                    Pk_Id_SuministroEPP = c.Int(nullable: false, identity: true),
                    CedulaTrabajador = c.String(nullable: false, maxLength: 100),
                    NombreTrabajador = c.String(nullable: false, maxLength: 500),
                    Fecha = c.DateTime(nullable: false),
                    Fk_Id_Proceso = c.Int(nullable: false),
                    Fk_Id_Sede = c.Int(nullable: false),
                    Fk_Id_Cargo = c.Int(nullable: false),
                    Fk_Id_Empresa = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_SuministroEPP)
                .ForeignKey("dbo.Tbl_Cargo", t => t.Fk_Id_Cargo, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Proceso", t => t.Fk_Id_Proceso, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: false)
                .Index(t => t.Fk_Id_Proceso)
                .Index(t => t.Fk_Id_Sede)
                .Index(t => t.Fk_Id_Cargo)
                .Index(t => t.Fk_Id_Empresa);

            CreateTable(
                "dbo.Tbl_EPPSuministroEPP",
                c => new
                {
                    Pk_Id_EPPSuministroEPP = c.Int(nullable: false, identity: true),
                    Cantidad = c.Int(nullable: false),
                    Fecha = c.DateTime(nullable: false),
                    Fk_Id_EPP = c.Int(nullable: false),
                    Fk_Id_EPPSuministro = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_EPPSuministroEPP)
                .ForeignKey("dbo.Tbl_EPP", t => t.Fk_Id_EPP, cascadeDelete: false)
                .ForeignKey("dbo.Tbl_EPPSuministro", t => t.Fk_Id_EPPSuministro, cascadeDelete: true)
                .Index(t => t.Fk_Id_EPP)
                .Index(t => t.Fk_Id_EPPSuministro);

            CreateTable(
                "dbo.Tbl_PeligroEMH",
                c => new
                {
                    Pk_Id_PeligroEMH = c.Int(nullable: false, identity: true),
                    Fk_Id_Peligro = c.Int(nullable: false),
                    Fk_Id_AdmoEMH = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Pk_Id_PeligroEMH)
                .ForeignKey("dbo.Tbl_AdministracionEMH", t => t.Fk_Id_AdmoEMH, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Peligro", t => t.Fk_Id_Peligro, cascadeDelete: false)
                .Index(t => t.Fk_Id_Peligro)
                .Index(t => t.Fk_Id_AdmoEMH);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_AdministracionEMHInspecciones", "Fk_Id_Inspecciones", "dbo.Tbl_Inspecciones");
            DropForeignKey("dbo.Tbl_AdministracionEMHInspecciones", "Fk_Id_AdmoEMH", "dbo.Tbl_AdministracionEMH");
            DropForeignKey("dbo.Tbl_AdministracionEMH", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_EPP", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_EPP", "Fk_Id_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropForeignKey("dbo.Tbl_EPPCargo", "Fk_Id_Cargo", "dbo.Tbl_Cargo");
            DropForeignKey("dbo.Tbl_EPPSuministro", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_EPPSuministro", "Fk_Id_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_PeligroEMH", "Fk_Id_Peligro", "dbo.Tbl_Peligro");
            DropForeignKey("dbo.Tbl_PeligroEMH", "Fk_Id_AdmoEMH", "dbo.Tbl_AdministracionEMH");
            DropForeignKey("dbo.Tbl_EPPSuministroEPP", "Fk_Id_EPPSuministro", "dbo.Tbl_EPPSuministro");
            DropForeignKey("dbo.Tbl_EPPSuministroEPP", "Fk_Id_EPP", "dbo.Tbl_EPP");
            DropForeignKey("dbo.Tbl_EPPSuministro", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_EPPSuministro", "Fk_Id_Cargo", "dbo.Tbl_Cargo");
            DropForeignKey("dbo.Tbl_EPPCargo", "Fk_Id_EPP", "dbo.Tbl_EPP");
            DropIndex("dbo.Tbl_PeligroEMH", new[] { "Fk_Id_AdmoEMH" });
            DropIndex("dbo.Tbl_PeligroEMH", new[] { "Fk_Id_Peligro" });
            DropIndex("dbo.Tbl_EPPSuministroEPP", new[] { "Fk_Id_EPPSuministro" });
            DropIndex("dbo.Tbl_EPPSuministroEPP", new[] { "Fk_Id_EPP" });
            DropIndex("dbo.Tbl_EPPSuministro", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_EPPSuministro", new[] { "Fk_Id_Cargo" });
            DropIndex("dbo.Tbl_EPPSuministro", new[] { "Fk_Id_Sede" });
            DropIndex("dbo.Tbl_EPPSuministro", new[] { "Fk_Id_Proceso" });
            DropIndex("dbo.Tbl_EPPCargo", new[] { "Fk_Id_EPP" });
            DropIndex("dbo.Tbl_EPPCargo", new[] { "Fk_Id_Cargo" });
            DropIndex("dbo.Tbl_EPP", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_EPP", new[] { "Fk_Id_Clasificacion_De_Peligro" });
            DropIndex("dbo.Tbl_AdministracionEMH", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_AdministracionEMHInspecciones", new[] { "Fk_Id_AdmoEMH" });
            DropIndex("dbo.Tbl_AdministracionEMHInspecciones", new[] { "Fk_Id_Inspecciones" });
            DropTable("dbo.Tbl_PeligroEMH");
            DropTable("dbo.Tbl_EPPSuministroEPP");
            DropTable("dbo.Tbl_EPPSuministro");
            DropTable("dbo.Tbl_EPPCargo");
            DropTable("dbo.Tbl_EPP");
            DropTable("dbo.Tbl_AdministracionEMH");
            DropTable("dbo.Tbl_AdministracionEMHInspecciones");
        }
    }
}
