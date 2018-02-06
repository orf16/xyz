namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionUpdateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_AntecedentesExposicionLaboral",
                c => new
                    {
                        PK_AntecedentesExposicionLaboral = c.Int(nullable: false, identity: true),
                        Descripcion_AntecedentesExposicionLaboral = c.String(),
                    })
                .PrimaryKey(t => t.PK_AntecedentesExposicionLaboral);
            
            CreateTable(
                "dbo.Tbl_Estrato",
                c => new
                    {
                        PK_Estrato = c.Int(nullable: false, identity: true),
                        Descripcion_Estrato = c.String(),
                    })
                .PrimaryKey(t => t.PK_Estrato);
            
            CreateTable(
                "dbo.Tbl_GradoEscolaridad",
                c => new
                    {
                        PK_GradoEscolaridad = c.Int(nullable: false, identity: true),
                        Descripcion_GradoEscolaridad = c.String(),
                    })
                .PrimaryKey(t => t.PK_GradoEscolaridad);
            
            CreateTable(
                "dbo.Tbl_Hijos",
                c => new
                    {
                        PK_Hijos = c.Int(nullable: false, identity: true),
                        Descripcion_Hijos = c.String(),
                    })
                .PrimaryKey(t => t.PK_Hijos);
            
            CreateTable(
                "dbo.Tbl_Informacion_laboral",
                c => new
                    {
                        ID_Informacion_laboral = c.Int(nullable: false, identity: true),
                        FK_IDEmpleado = c.Int(nullable: false),
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
                .PrimaryKey(t => t.ID_Informacion_laboral)
                .ForeignKey("dbo.Tbl_AntecedentesExposicionLaboral", t => t.FK_AntecedentesExposicionLaboral, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Empleado", t => t.FK_IDEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_VinculacionLaboral", t => t.FK_VinculacionLaboral, cascadeDelete: true)
                .Index(t => t.FK_IDEmpleado)
                .Index(t => t.FK_VinculacionLaboral)
                .Index(t => t.FK_AntecedentesExposicionLaboral);
            
            CreateTable(
                "dbo.Tbl_Estado_Civil",
                c => new
                    {
                        PK_Estado_Civil = c.Int(nullable: false, identity: true),
                        Descripcion_EstadoCivil = c.String(),
                    })
                .PrimaryKey(t => t.PK_Estado_Civil);
            
            CreateTable(
                "dbo.Tbl_Ingresos",
                c => new
                    {
                        PK_Ingresos = c.Int(nullable: false, identity: true),
                        Descripcion_Ingresos = c.String(),
                    })
                .PrimaryKey(t => t.PK_Ingresos);
            
            CreateTable(
                "dbo.Tbl_Ocupacion",
                c => new
                    {
                        PK_Ocupacion = c.Int(nullable: false, identity: true),
                        Descripcion_Ocupacion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Ocupacion);
            
            CreateTable(
                "dbo.Tbl_Raza",
                c => new
                    {
                        PK_Raza = c.Int(nullable: false, identity: true),
                        Descripcion_Raza = c.String(),
                    })
                .PrimaryKey(t => t.PK_Raza);
            
            CreateTable(
                "dbo.Tbl_Sexo",
                c => new
                    {
                        PK_Sexo = c.Int(nullable: false, identity: true),
                        Descripcion_TurnoTrabajo = c.String(),
                    })
                .PrimaryKey(t => t.PK_Sexo);
            
            CreateTable(
                "dbo.Tbl_VinculacionLaboral",
                c => new
                    {
                        PK_VinculacionLaboral = c.Int(nullable: false, identity: true),
                        Descripcion_VinculacionLaboral = c.String(),
                    })
                .PrimaryKey(t => t.PK_VinculacionLaboral);
            
            CreateTable(
                "dbo.Tbl_PeligroSede",
                c => new
                    {
                        PK_PeligroSede = c.Int(nullable: false, identity: true),
                        FK_IDEmpleado = c.Int(nullable: false),
                        FK_Sede = c.Int(nullable: false),
                        Municipio = c.String(),
                        Departamento = c.String(),
                        FK_ZonaLugar = c.Int(nullable: false),
                        FK_ClasificacionPeligros = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_PeligroSede)
                .ForeignKey("dbo.Tbl_Empleado", t => t.FK_IDEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_SedePeligro", t => t.FK_Sede, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ZonaLugar", t => t.FK_ZonaLugar, cascadeDelete: true)
                .Index(t => t.FK_IDEmpleado)
                .Index(t => t.FK_Sede)
                .Index(t => t.FK_ZonaLugar);
            
            CreateTable(
                "dbo.Tbl_SedePeligro",
                c => new
                    {
                        PK_SedePeligro = c.Int(nullable: false, identity: true),
                        Descripcion_TurnoTrabajo = c.String(),
                    })
                .PrimaryKey(t => t.PK_SedePeligro);
            
            CreateTable(
                "dbo.Tbl_ZonaLugar",
                c => new
                    {
                        PK_ZonaLugar = c.Int(nullable: false, identity: true),
                        Descripcion_ZonaLugar = c.String(),
                    })
                .PrimaryKey(t => t.PK_ZonaLugar);
            
            AddColumn("dbo.Tbl_Empleado", "Nombres", c => c.String());
            AddColumn("dbo.Tbl_Empleado", "Apellidos", c => c.String());
            AddColumn("dbo.Tbl_Empleado", "FK_GradoEscolaridad_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_Ingresos_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_Ciudad_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "Direccion_Empl", c => c.String());
            AddColumn("dbo.Tbl_Empleado", "FK_Hijos_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_Estrato_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_EstadoCivil_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_Raza_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_Ocupacion_Empl", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Empleado", "FK_Sexo_Empl", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Empleado", "FK_GradoEscolaridad_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_Ingresos_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_Hijos_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_Estrato_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_EstadoCivil_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_Raza_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_Ocupacion_Empl");
            CreateIndex("dbo.Tbl_Empleado", "FK_Sexo_Empl");
            AddForeignKey("dbo.Tbl_Empleado", "FK_EstadoCivil_Empl", "dbo.Tbl_Estado_Civil", "PK_Estado_Civil", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_Estrato_Empl", "dbo.Tbl_Estrato", "PK_Estrato", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_GradoEscolaridad_Empl", "dbo.Tbl_GradoEscolaridad", "PK_GradoEscolaridad", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_Hijos_Empl", "dbo.Tbl_Hijos", "PK_Hijos", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_Ingresos_Empl", "dbo.Tbl_Ingresos", "PK_Ingresos", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion", "PK_Ocupacion", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_Raza_Empl", "dbo.Tbl_Raza", "PK_Raza", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Empleado", "FK_Sexo_Empl", "dbo.Tbl_Sexo", "PK_Sexo", cascadeDelete: true);
            DropColumn("dbo.Tbl_Empleado", "Primer_Nombre_Empl");
            DropColumn("dbo.Tbl_Empleado", "Segundo_Nombre_Empl");
            DropColumn("dbo.Tbl_Empleado", "Primer_Apellido_Empl");
            DropColumn("dbo.Tbl_Empleado", "Segundo_Apellido_Empl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Empleado", "Segundo_Apellido_Empl", c => c.String());
            AddColumn("dbo.Tbl_Empleado", "Primer_Apellido_Empl", c => c.String());
            AddColumn("dbo.Tbl_Empleado", "Segundo_Nombre_Empl", c => c.String());
            AddColumn("dbo.Tbl_Empleado", "Primer_Nombre_Empl", c => c.String());
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_ZonaLugar", "dbo.Tbl_ZonaLugar");
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_Sede", "dbo.Tbl_SedePeligro");
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_IDEmpleado", "dbo.Tbl_Empleado");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_VinculacionLaboral", "dbo.Tbl_VinculacionLaboral");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_IDEmpleado", "dbo.Tbl_Empleado");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Sexo_Empl", "dbo.Tbl_Sexo");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Raza_Empl", "dbo.Tbl_Raza");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Ingresos_Empl", "dbo.Tbl_Ingresos");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Hijos_Empl", "dbo.Tbl_Hijos");
            DropForeignKey("dbo.Tbl_Empleado", "FK_GradoEscolaridad_Empl", "dbo.Tbl_GradoEscolaridad");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Estrato_Empl", "dbo.Tbl_Estrato");
            DropForeignKey("dbo.Tbl_Empleado", "FK_EstadoCivil_Empl", "dbo.Tbl_Estado_Civil");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_AntecedentesExposicionLaboral", "dbo.Tbl_AntecedentesExposicionLaboral");
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_ZonaLugar" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_IDEmpleado" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Sexo_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Ocupacion_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Raza_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_EstadoCivil_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Estrato_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Hijos_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Ingresos_Empl" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_GradoEscolaridad_Empl" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_AntecedentesExposicionLaboral" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_VinculacionLaboral" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_IDEmpleado" });
            DropColumn("dbo.Tbl_Empleado", "FK_Sexo_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_Ocupacion_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_Raza_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_EstadoCivil_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_Estrato_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_Hijos_Empl");
            DropColumn("dbo.Tbl_Empleado", "Direccion_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_Ciudad_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_Ingresos_Empl");
            DropColumn("dbo.Tbl_Empleado", "FK_GradoEscolaridad_Empl");
            DropColumn("dbo.Tbl_Empleado", "Apellidos");
            DropColumn("dbo.Tbl_Empleado", "Nombres");
            DropTable("dbo.Tbl_ZonaLugar");
            DropTable("dbo.Tbl_SedePeligro");
            DropTable("dbo.Tbl_PeligroSede");
            DropTable("dbo.Tbl_VinculacionLaboral");
            DropTable("dbo.Tbl_Sexo");
            DropTable("dbo.Tbl_Raza");
            DropTable("dbo.Tbl_Ocupacion");
            DropTable("dbo.Tbl_Ingresos");
            DropTable("dbo.Tbl_Estado_Civil");
            DropTable("dbo.Tbl_Informacion_laboral");
            DropTable("dbo.Tbl_Hijos");
            DropTable("dbo.Tbl_GradoEscolaridad");
            DropTable("dbo.Tbl_Estrato");
            DropTable("dbo.Tbl_AntecedentesExposicionLaboral");
        }
    }
}
