namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_algoritmo_15_16_17_26 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ComunicacionesCargos",
                c => new
                    {
                        Pk_Id_ComunicacionesCargos = c.Int(nullable: false, identity: true),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre_Completo_Empleado = c.String(),
                        Cargo = c.String(),
                        Email = c.String(),
                        NitEmpresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_ComunicacionesCargos);
            
            CreateTable(
                "dbo.Tbl_ComunicacionesEncuestas",
                c => new
                    {
                        pk_id_encuesta = c.Int(nullable: false, identity: true),
                        fk_pk_id_encuesta = c.Int(nullable: false),
                        contenido = c.String(),
                        fechacreacion = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_encuesta);
            
            CreateTable(
                "dbo.Tbl_PlanCapacitacion",
                c => new
                    {
                        pk_id_plan_capacitacion = c.Int(nullable: false, identity: true),
                        fk_id_tipo_actividad = c.Int(nullable: false),
                        tema = c.String(),
                        fk_id_rol = c.Int(nullable: false),
                        fk_id_competencia = c.Int(nullable: false),
                        fecha_programada = c.String(),
                        hora_inicio = c.String(),
                        hora_fin = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_plan_capacitacion);
            
            CreateTable(
                "dbo.Tbl_PlanCapacitacion_Asignaciones",
                c => new
                    {
                        pk_id_asignaciones = c.Int(nullable: false, identity: true),
                        fk_id_plan_capacitacion = c.Int(nullable: false),
                        numero_documento = c.String(),
                        nombre = c.String(),
                        Enviado = c.Boolean(nullable: false),
                        asistencia = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.pk_id_asignaciones);
            
            CreateTable(
                "dbo.Tbl_PlanCapacitacion_Soporte",
                c => new
                    {
                        pk_id_soporte = c.Int(nullable: false, identity: true),
                        fk_id_plan_capacitacion = c.Int(nullable: false),
                        adjunto = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_soporte);
            
            CreateTable(
                "dbo.Tbl_vul_eme_Consolidado",
                c => new
                    {
                        pk_id_vul_consolidado = c.Int(nullable: false, identity: true),
                        fk_pk_id_plan_emergencia = c.Int(nullable: false),
                        organizacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        capacitacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dotacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        materiales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        edificacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        equipos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        servicios_publicos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        sistemas_alternos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        recuperacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.pk_id_vul_consolidado);
            
            CreateTable(
                "dbo.Tbl_vul_eme_IdentificacionAmenazas",
                c => new
                    {
                        pk_id_vul_identificacion_amenazas = c.Int(nullable: false, identity: true),
                        fk_pk_id_plan_emergencia = c.Int(nullable: false),
                        fk_id_amenaza = c.Int(nullable: false),
                        origen = c.String(),
                        fuenteriesgo = c.String(),
                        calificacion = c.String(),
                        color = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_vul_identificacion_amenazas);
            
            CreateTable(
                "dbo.Tbl_vul_eme_Personas",
                c => new
                    {
                        pk_id_vul_personas = c.Int(nullable: false, identity: true),
                        fk_pk_id_plan_emergencia = c.Int(nullable: false),
                        fk_id_aspecto = c.Int(nullable: false),
                        observacion = c.String(),
                        recomendacion = c.String(),
                        calificacion = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_vul_personas);
            
            CreateTable(
                "dbo.Tbl_vul_eme_Recursos",
                c => new
                    {
                        pk_id_vul_recursos = c.Int(nullable: false, identity: true),
                        fk_pk_id_plan_emergencia = c.Int(nullable: false),
                        fk_id_aspecto = c.Int(nullable: false),
                        observacion = c.String(),
                        recomendacion = c.String(),
                        calificacion = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_vul_recursos);
            
            CreateTable(
                "dbo.Tbl_vul_eme_sistemas_procesos",
                c => new
                    {
                        pk_id_vul_sistemas_procesos = c.Int(nullable: false, identity: true),
                        fk_pk_id_plan_emergencia = c.Int(nullable: false),
                        fk_id_aspecto = c.Int(nullable: false),
                        observacion = c.String(),
                        recomendacion = c.String(),
                        calificacion = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_vul_sistemas_procesos);
            
            CreateTable(
                "dbo.Tbl_vul_Identificacion_Personas",
                c => new
                    {
                        pk_id_identificacion_amenazas = c.Int(nullable: false, identity: true),
                        amenaza = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_identificacion_amenazas);
            
            CreateTable(
                "dbo.Tbl_vul_Personas",
                c => new
                    {
                        pk_id_personas = c.Int(nullable: false, identity: true),
                        aspectos = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_personas);
            
            CreateTable(
                "dbo.Tbl_vul_Recursos",
                c => new
                    {
                        pk_id_recursos = c.Int(nullable: false, identity: true),
                        aspectos = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_recursos);
            
            CreateTable(
                "dbo.Tbl_vul_SistemasProcesos",
                c => new
                    {
                        pk_id_sistemas_procesos = c.Int(nullable: false, identity: true),
                        aspectos = c.String(),
                        tipo = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_sistemas_procesos);
            
            AddColumn("dbo.Tbl_Empleado_Tematica", "NitEmpresa", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_ComunicacionesExternas", "Asunto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_ComunicacionesExternas", "Asunto");
            DropColumn("dbo.Tbl_Empleado_Tematica", "NitEmpresa");
            DropTable("dbo.Tbl_vul_SistemasProcesos");
            DropTable("dbo.Tbl_vul_Recursos");
            DropTable("dbo.Tbl_vul_Personas");
            DropTable("dbo.Tbl_vul_Identificacion_Personas");
            DropTable("dbo.Tbl_vul_eme_sistemas_procesos");
            DropTable("dbo.Tbl_vul_eme_Recursos");
            DropTable("dbo.Tbl_vul_eme_Personas");
            DropTable("dbo.Tbl_vul_eme_IdentificacionAmenazas");
            DropTable("dbo.Tbl_vul_eme_Consolidado");
            DropTable("dbo.Tbl_PlanCapacitacion_Soporte");
            DropTable("dbo.Tbl_PlanCapacitacion_Asignaciones");
            DropTable("dbo.Tbl_PlanCapacitacion");
            DropTable("dbo.Tbl_ComunicacionesEncuestas");
            DropTable("dbo.Tbl_ComunicacionesCargos");
        }
    }
}
