namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstudioPuestoTrabajo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_EstudioPuestoTrabajo",
                c => new
                    {
                        Pk_Id_EstudioPuestoTrabajo = c.Int(nullable: false, identity: true),
                        Numero_Identificacion = c.String(maxLength: 15),
                        Trabajador_Primer_Apellido = c.String(),
                        Trabajador_Segundo_Apellido = c.String(),
                        Trabajador_Primer_Nombre = c.String(),
                        Trabajador_Segundo_Nombre = c.String(),
                        Cargo_Empleado = c.String(),
                        FK_Id_Sede = c.Int(nullable: false),
                        FK_Id_Proceso = c.Int(nullable: false),
                        FK_Id_Diagnostico = c.Int(nullable: false),
                        FK_Id_ObjetivoAnalisis = c.Int(nullable: false),
                        FK_Id_Tipo_Analisis_Puesto_Trabajo = c.Int(nullable: false),
                        FechaAnalisis = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_EstudioPuestoTrabajo)
                .ForeignKey("dbo.Tbl_Diagnosticos", t => t.FK_Id_Diagnostico, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ObjetivoAnalisis", t => t.FK_Id_ObjetivoAnalisis, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Proceso", t => t.FK_Id_Proceso, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_Id_Sede, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tipo_Analisis_Puesto_Trabajo", t => t.FK_Id_Tipo_Analisis_Puesto_Trabajo, cascadeDelete: true)
                .Index(t => t.FK_Id_Sede)
                .Index(t => t.FK_Id_Proceso)
                .Index(t => t.FK_Id_Diagnostico)
                .Index(t => t.FK_Id_ObjetivoAnalisis)
                .Index(t => t.FK_Id_Tipo_Analisis_Puesto_Trabajo);
            
            CreateTable(
                "dbo.Tbl_Archivos_Estudio_Puesto_Trabajo",
                c => new
                    {
                        PK_Id_Archivo_Estudio_Puesto_Trabajo = c.Int(nullable: false, identity: true),
                        NombreArchivo = c.String(),
                        Ruta = c.String(),
                        EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo = c.Int(),
                    })
                .PrimaryKey(t => t.PK_Id_Archivo_Estudio_Puesto_Trabajo)
                .ForeignKey("dbo.Tbl_EstudioPuestoTrabajo", t => t.EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo)
                .Index(t => t.EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo);
            
            CreateTable(
                "dbo.Tbl_ObjetivoAnalisis",
                c => new
                    {
                        Pk_Id_ObjetivoAnalisis = c.Int(nullable: false, identity: true),
                        Nombre_ObjetivoAnalisis = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_ObjetivoAnalisis);
            
            CreateTable(
                "dbo.Tbl_Seguimiento_Estudio_Puesto_Trabajo",
                c => new
                    {
                        PK_Id_Seguimiento_Estudio_Puesto_Trabajo = c.Int(nullable: false, identity: true),
                        Actividad = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Responsable = c.String(),
                        EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo = c.Int(),
                    })
                .PrimaryKey(t => t.PK_Id_Seguimiento_Estudio_Puesto_Trabajo)
                .ForeignKey("dbo.Tbl_EstudioPuestoTrabajo", t => t.EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo)
                .Index(t => t.EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo);
            
            CreateTable(
                "dbo.Tbl_Tipo_Analisis_Puesto_Trabajo",
                c => new
                    {
                        Pk_Id_Tipo_Analisis_Puesto_Trabajo = c.Int(nullable: false, identity: true),
                        Nombre_Tipo_Analisis_Puesto_Trabajo = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Tipo_Analisis_Puesto_Trabajo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_EstudioPuestoTrabajo", "FK_Id_Tipo_Analisis_Puesto_Trabajo", "dbo.Tbl_Tipo_Analisis_Puesto_Trabajo");
            DropForeignKey("dbo.Tbl_Seguimiento_Estudio_Puesto_Trabajo", "EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo", "dbo.Tbl_EstudioPuestoTrabajo");
            DropForeignKey("dbo.Tbl_EstudioPuestoTrabajo", "FK_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_EstudioPuestoTrabajo", "FK_Id_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_EstudioPuestoTrabajo", "FK_Id_ObjetivoAnalisis", "dbo.Tbl_ObjetivoAnalisis");
            DropForeignKey("dbo.Tbl_EstudioPuestoTrabajo", "FK_Id_Diagnostico", "dbo.Tbl_Diagnosticos");
            DropForeignKey("dbo.Tbl_Archivos_Estudio_Puesto_Trabajo", "EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo", "dbo.Tbl_EstudioPuestoTrabajo");
            DropIndex("dbo.Tbl_Seguimiento_Estudio_Puesto_Trabajo", new[] { "EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo" });
            DropIndex("dbo.Tbl_Archivos_Estudio_Puesto_Trabajo", new[] { "EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo" });
            DropIndex("dbo.Tbl_EstudioPuestoTrabajo", new[] { "FK_Id_Tipo_Analisis_Puesto_Trabajo" });
            DropIndex("dbo.Tbl_EstudioPuestoTrabajo", new[] { "FK_Id_ObjetivoAnalisis" });
            DropIndex("dbo.Tbl_EstudioPuestoTrabajo", new[] { "FK_Id_Diagnostico" });
            DropIndex("dbo.Tbl_EstudioPuestoTrabajo", new[] { "FK_Id_Proceso" });
            DropIndex("dbo.Tbl_EstudioPuestoTrabajo", new[] { "FK_Id_Sede" });
            DropTable("dbo.Tbl_Tipo_Analisis_Puesto_Trabajo");
            DropTable("dbo.Tbl_Seguimiento_Estudio_Puesto_Trabajo");
            DropTable("dbo.Tbl_ObjetivoAnalisis");
            DropTable("dbo.Tbl_Archivos_Estudio_Puesto_Trabajo");
            DropTable("dbo.Tbl_EstudioPuestoTrabajo");
        }
    }
}
