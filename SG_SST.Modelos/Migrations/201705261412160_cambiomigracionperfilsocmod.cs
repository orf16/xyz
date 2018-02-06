namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiomigracionperfilsocmod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_EmpleadoPerfilSocioDemografico", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_AntecedentesExposicionLaboral", "dbo.Tbl_AntecedentesExposicionLaboral");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FKIDEmpleado", "dbo.Tbl_EmpleadoPerfilSocioDemografico");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_VinculacionLaboral", "dbo.Tbl_VinculacionLaboral");
            DropForeignKey("dbo.Tbl_PeligroSede", "FKIDEmpleado", "dbo.Tbl_EmpleadoPerfilSocioDemografico");
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_Sede", "dbo.Tbl_SedePeligro");
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_ZonaLugar", "dbo.Tbl_ZonaLugar");
            DropForeignKey("dbo.Tbl_PerfilSocioDemografico", "FK_IDEmpleado", "dbo.Tbl_Empleado");
            DropIndex("dbo.Tbl_EmpleadoPerfilSocioDemografico", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FKIDEmpleado" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_Ocupacion_Empl" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_VinculacionLaboral" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_AntecedentesExposicionLaboral" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FKIDEmpleado" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_ZonaLugar" });
            DropIndex("dbo.Tbl_PerfilSocioDemografico", new[] { "FK_IDEmpleado" });
            CreateTable(
                "dbo.Tbl_Aspecto_Base",
                c => new
                    {
                        PK_Id_Aspecto_Base = c.Int(nullable: false, identity: true),
                        AspectoBase = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_Aspecto_Base);
            
            CreateTable(
                "dbo.Tbl_PerfilSocioDemograficoPlanificacion",
                c => new
                    {
                        IDEmpleado_PerfilSocioDemoGrafico = c.Int(nullable: false, identity: true),
                        Tipo_Documento = c.String(),
                        PK_Numero_Documento_Empl = c.Int(nullable: false),
                        Nombre1 = c.String(),
                        Nombre2 = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        Fk_Sede = c.Int(nullable: false),
                        FK_Clasificacion_De_Peligro = c.Int(nullable: false),
                        GradoEscolaridad = c.String(),
                        Ingresos = c.String(),
                        Departamento = c.String(),
                        Ciudad = c.String(),
                        Direccion = c.String(),
                        Conyuge = c.Boolean(nullable: false),
                        Hijos = c.Boolean(nullable: false),
                        FK_Estrato = c.Int(nullable: false),
                        FK_Estado_Civil = c.Int(nullable: false),
                        FK_Raza = c.Int(nullable: false),
                        Ocupacion = c.String(),
                        Sexo = c.String(),
                        GrupoEtarios = c.String(),
                        FK_VinculacionLaboral = c.Int(nullable: false),
                        TurnoTrabajo = c.String(),
                        Cargo = c.String(),
                        FechaIngresoEmpresa = c.DateTime(nullable: false),
                        FechaIngresoUltimoCargo = c.DateTime(nullable: false),
                        AntecedentesExpLaboral = c.Int(nullable: false),
                        FactorRiesgoPeligro = c.String(),
                        EvaluacionMedica = c.String(),
                    })
                .PrimaryKey(t => t.IDEmpleado_PerfilSocioDemoGrafico)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Sede, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Clasificacion_De_Peligro", t => t.FK_Clasificacion_De_Peligro, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Estado_Civil", t => t.FK_Estado_Civil, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Estrato", t => t.FK_Estrato, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Raza", t => t.FK_Raza, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_VinculacionLaboral", t => t.FK_VinculacionLaboral, cascadeDelete: true)
                .Index(t => t.Fk_Sede)
                .Index(t => t.FK_Clasificacion_De_Peligro)
                .Index(t => t.FK_Estrato)
                .Index(t => t.FK_Estado_Civil)
                .Index(t => t.FK_Raza)
                .Index(t => t.FK_VinculacionLaboral);
            
            DropTable("dbo.Tbl_EmpleadoPerfilSocioDemografico");
            DropTable("dbo.Tbl_Informacion_laboral");
            DropTable("dbo.Tbl_PeligroSede");
            DropTable("dbo.Tbl_PerfilSocioDemografico");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tbl_PerfilSocioDemografico",
                c => new
                    {
                        PK_PerfilSocioDemografico = c.Int(nullable: false, identity: true),
                        FK_IDEmpleado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_PerfilSocioDemografico);
            
            CreateTable(
                "dbo.Tbl_PeligroSede",
                c => new
                    {
                        PK_PeligroSede = c.Int(nullable: false, identity: true),
                        FKIDEmpleado = c.Int(nullable: false),
                        FK_Sede = c.Int(nullable: false),
                        Municipio = c.String(),
                        Departamento = c.String(),
                        FK_ZonaLugar = c.Int(nullable: false),
                        FK_ClasificacionPeligros = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_PeligroSede);
            
            CreateTable(
                "dbo.Tbl_Informacion_laboral",
                c => new
                    {
                        ID_Informacion_laboral = c.Int(nullable: false, identity: true),
                        FKIDEmpleado = c.Int(nullable: false),
                        Cargo_Empl = c.String(),
                        FK_Ocupacion_Empl = c.Int(nullable: false),
                        FK_VinculacionLaboral = c.Int(nullable: false),
                        Turno_Trabajo = c.String(),
                        Cargo = c.String(),
                        FechaIngresoEmpresa = c.DateTime(nullable: false),
                        FechaIngresoUltimoCargo = c.DateTime(nullable: false),
                        años = c.DateTime(nullable: false),
                        Meses = c.DateTime(nullable: false),
                        Dias = c.DateTime(nullable: false),
                        FK_AntecedentesExposicionLaboral = c.Int(nullable: false),
                        EvaluacionMedicaOcupacional = c.String(),
                        FactorRiesgoPeligro = c.String(),
                    })
                .PrimaryKey(t => t.ID_Informacion_laboral);
            
            CreateTable(
                "dbo.Tbl_EmpleadoPerfilSocioDemografico",
                c => new
                    {
                        IDEmpleado_PerfilSocioDemoGrafico = c.Int(nullable: false, identity: true),
                        Tipo_Documento = c.String(),
                        PK_Numero_Documento_Empl = c.Int(nullable: false),
                        Nombre1 = c.String(),
                        Nombre2 = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDEmpleado_PerfilSocioDemoGrafico);
            
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_VinculacionLaboral", "dbo.Tbl_VinculacionLaboral");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Raza", "dbo.Tbl_Raza");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Estrato", "dbo.Tbl_Estrato");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Estado_Civil", "dbo.Tbl_Estado_Civil");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropForeignKey("dbo.Tbl_PerfilSocioDemograficoPlanificacion", "Fk_Sede", "dbo.Tbl_Sede");
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_VinculacionLaboral" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Raza" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Estado_Civil" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Estrato" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "FK_Clasificacion_De_Peligro" });
            DropIndex("dbo.Tbl_PerfilSocioDemograficoPlanificacion", new[] { "Fk_Sede" });
            DropTable("dbo.Tbl_PerfilSocioDemograficoPlanificacion");
            DropTable("dbo.Tbl_Aspecto_Base");
            CreateIndex("dbo.Tbl_PerfilSocioDemografico", "FK_IDEmpleado");
            CreateIndex("dbo.Tbl_PeligroSede", "FK_ZonaLugar");
            CreateIndex("dbo.Tbl_PeligroSede", "FK_Sede");
            CreateIndex("dbo.Tbl_PeligroSede", "FKIDEmpleado");
            CreateIndex("dbo.Tbl_Informacion_laboral", "FK_AntecedentesExposicionLaboral");
            CreateIndex("dbo.Tbl_Informacion_laboral", "FK_VinculacionLaboral");
            CreateIndex("dbo.Tbl_Informacion_laboral", "FK_Ocupacion_Empl");
            CreateIndex("dbo.Tbl_Informacion_laboral", "FKIDEmpleado");
            CreateIndex("dbo.Tbl_EmpleadoPerfilSocioDemografico", "FK_Empresa");
            AddForeignKey("dbo.Tbl_PerfilSocioDemografico", "FK_IDEmpleado", "dbo.Tbl_Empleado", "ID_Empleado", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_PeligroSede", "FK_ZonaLugar", "dbo.Tbl_ZonaLugar", "PK_ZonaLugar", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_PeligroSede", "FK_Sede", "dbo.Tbl_SedePeligro", "PK_SedePeligro", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_PeligroSede", "FKIDEmpleado", "dbo.Tbl_EmpleadoPerfilSocioDemografico", "IDEmpleado_PerfilSocioDemoGrafico", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Informacion_laboral", "FK_VinculacionLaboral", "dbo.Tbl_VinculacionLaboral", "PK_VinculacionLaboral", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Informacion_laboral", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion", "PK_Ocupacion", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Informacion_laboral", "FKIDEmpleado", "dbo.Tbl_EmpleadoPerfilSocioDemografico", "IDEmpleado_PerfilSocioDemoGrafico", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Informacion_laboral", "FK_AntecedentesExposicionLaboral", "dbo.Tbl_AntecedentesExposicionLaboral", "PK_AntecedentesExposicionLaboral", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_EmpleadoPerfilSocioDemografico", "FK_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
        }
    }
}
