namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Acciones",
                c => new
                    {
                        Pk_Id_Accion = c.Int(nullable: false, identity: true),
                        Id_Accion = c.Int(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 100),
                        Fecha_dil = c.DateTime(nullable: false),
                        Fecha_ocurrencia = c.DateTime(nullable: false),
                        Clase = c.String(nullable: false, maxLength: 100),
                        Fecha_hall = c.DateTime(nullable: false),
                        Halla_Num_Doc = c.String(nullable: false, maxLength: 50),
                        Halla_Nombre = c.String(nullable: false, maxLength: 50),
                        Halla_TipoDoc = c.String(nullable: false, maxLength: 50),
                        Halla_Cargo = c.String(nullable: false, maxLength: 50),
                        Halla_Sede = c.String(nullable: false, maxLength: 2000),
                        Correccion = c.String(maxLength: 2000),
                        Causa_Raiz = c.String(nullable: false, maxLength: 2000),
                        Cambio_Doc = c.String(maxLength: 20),
                        Des_Cambio_Doc = c.String(maxLength: 2000),
                        Verificacion = c.String(nullable: false, maxLength: 1000),
                        Eficacia = c.String(maxLength: 50),
                        Firma_Auditor = c.Binary(),
                        Nombre_Auditor = c.String(nullable: false, maxLength: 50),
                        Cargo_Auditor = c.String(nullable: false, maxLength: 50),
                        Firma_Responsable = c.Binary(),
                        Nombre_Responsable = c.String(nullable: false, maxLength: 50),
                        Cargo_Responsable = c.String(nullable: false, maxLength: 50),
                        Fk_Id_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Accion)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_ActividadAccion",
                c => new
                    {
                        Pk_Id_Actividad = c.Int(nullable: false, identity: true),
                        Actividad = c.String(nullable: false, maxLength: 1000),
                        Responsable = c.String(nullable: false, maxLength: 100),
                        FechaFinalizacion = c.DateTime(nullable: false),
                        RutaFirma = c.String(),
                        Fk_Id_Accion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Actividad)
                .ForeignKey("dbo.Tbl_Acciones", t => t.Fk_Id_Accion, cascadeDelete: true)
                .Index(t => t.Fk_Id_Accion);
            
            CreateTable(
                "dbo.Tbl_Analisis",
                c => new
                    {
                        Pk_Id_Analisis = c.Int(nullable: false, identity: true),
                        Id_Analisis = c.Int(nullable: false),
                        Tipo = c.Short(nullable: false),
                        ValorTxt = c.String(nullable: false, maxLength: 200),
                        Parent_Id = c.Int(nullable: false),
                        Fk_Id_Accion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Analisis)
                .ForeignKey("dbo.Tbl_Acciones", t => t.Fk_Id_Accion, cascadeDelete: true)
                .Index(t => t.Fk_Id_Accion);
            
            CreateTable(
                "dbo.Tbl_ArchivosAccion",
                c => new
                    {
                        Pk_Id_Archivo = c.Int(nullable: false, identity: true),
                        Token_Archivo = c.String(),
                        NombreArchivo = c.String(nullable: false, maxLength: 200),
                        Ruta = c.String(nullable: false, maxLength: 200),
                        Fk_Id_Accion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Archivo)
                .ForeignKey("dbo.Tbl_Acciones", t => t.Fk_Id_Accion, cascadeDelete: true)
                .Index(t => t.Fk_Id_Accion);
            
            CreateTable(
                "dbo.Tbl_Empresa",
                c => new
                    {
                        Pk_Id_Empresa = c.Int(nullable: false, identity: true),
                        Nit_Empresa = c.String(),
                        Tipo_Documento = c.String(),
                        Identificacion_Representante = c.Int(nullable: false),
                        Razon_Social = c.String(),
                        Direccion = c.String(),
                        Telefono = c.Int(),
                        Fax = c.Int(),
                        Riesgo = c.Int(),
                        Total_Empleados = c.Int(),
                        ID_Seccional = c.Int(),
                        ID_Sector_Economico = c.Int(),
                        Email = c.String(),
                        Sitio_Web = c.String(),
                        Codigo_Actividad = c.Int(nullable: false),
                        Descripcion_Actividad = c.String(),
                        Fecha_Vigencia_Actual = c.String(),
                        Flg_Estado = c.String(),
                        Zona = c.String(),
                        imagen_proceso = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Gobierno",
                c => new
                    {
                        Pk_Id_Gobierno = c.Int(nullable: false, identity: true),
                        Nit_Empresa = c.Int(nullable: false),
                        Mision = c.String(),
                        Vision = c.String(),
                        Fk_Id_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Gobierno)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Organigrama",
                c => new
                    {
                        Pk_Id_Organigrama = c.Int(nullable: false, identity: true),
                        Descripcion_Organigrama = c.String(),
                        Imagen_Organigrama = c.String(),
                        Fk_Id_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Organigrama)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_EmpleadoOrg",
                c => new
                    {
                        Id_EmpleadoOrg = c.Int(nullable: false, identity: true),
                        Jefe_Inmediato = c.String(),
                        Cargo_Empleado = c.String(nullable: false),
                        Fk_Id_EmpleadoOrg = c.Int(),
                        Fk_Id_Organigrama = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_EmpleadoOrg)
                .ForeignKey("dbo.Tbl_Organigrama", t => t.Fk_Id_Organigrama, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_EmpleadoOrg", t => t.Fk_Id_EmpleadoOrg)
                .Index(t => t.Fk_Id_EmpleadoOrg)
                .Index(t => t.Fk_Id_Organigrama);
            
            CreateTable(
                "dbo.Tbl_EmpresaProceso",
                c => new
                    {
                        Pk_Id_ProcesoEmpresa = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        Fk_Id_Proceso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_ProcesoEmpresa)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Proceso", t => t.Fk_Id_Proceso, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa)
                .Index(t => t.Fk_Id_Proceso);
            
            CreateTable(
                "dbo.Tbl_Proceso",
                c => new
                    {
                        Pk_Id_Proceso = c.Int(nullable: false, identity: true),
                        Descripcion_Proceso = c.String(nullable: false),
                        Fk_Id_Proceso = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_Proceso)
                .ForeignKey("dbo.Tbl_Proceso", t => t.Fk_Id_Proceso)
                .Index(t => t.Fk_Id_Proceso);
            
            CreateTable(
                "dbo.Tbl_Peligro",
                c => new
                    {
                        PK_Peligro = c.Int(nullable: false, identity: true),
                        Nombre_Del_Profesional = c.String(),
                        Numero_De_Documento = c.String(),
                        Numero_De_Licencia_SST = c.String(),
                        Fecha_De_Evaluacion = c.DateTime(nullable: false),
                        Lugar = c.String(),
                        Actividad = c.String(),
                        Tarea = c.String(),
                        FLG_Rutinaria = c.Boolean(nullable: false),
                        Fuente_Generadora_De_Peligro = c.String(),
                        Otro = c.String(),
                        Fuente = c.String(),
                        Medio = c.String(),
                        Eliminacion = c.String(),
                        Sustitucion = c.String(),
                        Controles_De_Ingenieria = c.String(),
                        Controles_Administrativos = c.String(),
                        Elementos_De_Proteccion = c.String(),
                        Accion_De_Prevencion = c.String(),
                        FK_Clasificacion_De_Peligro = c.Int(nullable: false),
                        FK_Sede = c.Int(nullable: false),
                        FK_Proceso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Peligro)
                .ForeignKey("dbo.Tbl_Clasificacion_De_Peligro", t => t.FK_Clasificacion_De_Peligro, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Proceso", t => t.FK_Proceso, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_Sede, cascadeDelete: true)
                .Index(t => t.FK_Clasificacion_De_Peligro)
                .Index(t => t.FK_Sede)
                .Index(t => t.FK_Proceso);
            
            CreateTable(
                "dbo.Tbl_Clasificacion_De_Peligro",
                c => new
                    {
                        PK_Clasificacion_De_Peligro = c.Int(nullable: false, identity: true),
                        Descripcion_Clase_De_Peligro = c.String(),
                        Detalle_Clase_De_Peligro = c.String(),
                        FK_Tipo_De_Peligro = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Clasificacion_De_Peligro)
                .ForeignKey("dbo.Tbl_Tipo_De_Peligro", t => t.FK_Tipo_De_Peligro, cascadeDelete: true)
                .Index(t => t.FK_Tipo_De_Peligro);
            
            CreateTable(
                "dbo.Tbl_Tipo_De_Peligro",
                c => new
                    {
                        PK_Tipo_De_Peligro = c.Int(nullable: false, identity: true),
                        Descripcion_Del_Peligro = c.String(),
                    })
                .PrimaryKey(t => t.PK_Tipo_De_Peligro);
            
            CreateTable(
                "dbo.Tbl_Consecuencia_Por_Peligro",
                c => new
                    {
                        PK_Consecuencia_Por_Peligro = c.Int(nullable: false, identity: true),
                        FK_Peligro = c.Int(nullable: false),
                        FK_Consecuencia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Consecuencia_Por_Peligro)
                .ForeignKey("dbo.Tbl_Consecuencia", t => t.FK_Consecuencia, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Peligro", t => t.FK_Peligro, cascadeDelete: true)
                .Index(t => t.FK_Peligro)
                .Index(t => t.FK_Consecuencia);
            
            CreateTable(
                "dbo.Tbl_Consecuencia",
                c => new
                    {
                        PK_Consecuencia = c.Int(nullable: false, identity: true),
                        Valor_Consecuencia = c.Int(nullable: false),
                        Descripcion_Consecuencia = c.String(),
                        FK_Grupo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Consecuencia)
                .ForeignKey("dbo.Tbl_Grupo", t => t.FK_Grupo, cascadeDelete: true)
                .Index(t => t.FK_Grupo);
            
            CreateTable(
                "dbo.Tbl_Estimacion_De_Riesgo",
                c => new
                    {
                        PK_Estimacion_De_Riesgo = c.Int(nullable: false, identity: true),
                        FK_Probabilidad = c.Int(nullable: false),
                        FK_Consecuencia = c.Int(nullable: false),
                        Detalle_Estimacion = c.String(),
                        RiesgoNoAceptable = c.Boolean(nullable: false),
                        ValorDelRiesgo = c.Int(),
                    })
                .PrimaryKey(t => t.PK_Estimacion_De_Riesgo)
                .ForeignKey("dbo.Tbl_Consecuencia", t => t.FK_Consecuencia, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Probabilidad", t => t.FK_Probabilidad, cascadeDelete: true)
                .Index(t => t.FK_Probabilidad)
                .Index(t => t.FK_Consecuencia);
            
            CreateTable(
                "dbo.Tbl_Estimacion_Riesgo_Por_RAM",
                c => new
                    {
                        PK_Estimacion_Riesgo_Por_RAM = c.Int(nullable: false, identity: true),
                        FK_RAM = c.Int(nullable: false),
                        FK_Estimacion_De_Riesgo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Estimacion_Riesgo_Por_RAM)
                .ForeignKey("dbo.Tbl_Estimacion_De_Riesgo", t => t.FK_Estimacion_De_Riesgo)
                .ForeignKey("dbo.Tbl_RAM", t => t.FK_RAM, cascadeDelete: true)
                .Index(t => t.FK_RAM)
                .Index(t => t.FK_Estimacion_De_Riesgo);
            
            CreateTable(
                "dbo.Tbl_RAM",
                c => new
                    {
                        PK_RAM = c.Int(nullable: false, identity: true),
                        Consecuencias_Reales = c.String(),
                        Consecuencias_Potenciales = c.String(),
                        Nivel_De_Riesgo = c.String(),
                        FK_Persona_Expuesta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_RAM)
                .ForeignKey("dbo.Tbl_Persona_Expuesta_INSHT_RAM", t => t.FK_Persona_Expuesta, cascadeDelete: true)
                .Index(t => t.FK_Persona_Expuesta);
            
            CreateTable(
                "dbo.Tbl_Persona_Expuesta_INSHT_RAM",
                c => new
                    {
                        PK_Persona_Expuesta = c.Int(nullable: false, identity: true),
                        Planta_Directo = c.Int(nullable: false),
                        Contratista = c.Int(nullable: false),
                        Temporal = c.Int(nullable: false),
                        Estudiante_Pasante = c.Int(nullable: false),
                        Visitante = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Horas_De_Exposicion_Planta = c.Int(nullable: false),
                        Horas_De_Exposicion_Contratista = c.Int(nullable: false),
                        Horas_De_Exposicion_Temporal = c.Int(nullable: false),
                        Horas_De_Exposicion_Estudiante = c.Int(nullable: false),
                        Horas_De_Exposicion_Visitante = c.Int(nullable: false),
                        FK_Peligro = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Persona_Expuesta)
                .ForeignKey("dbo.Tbl_Peligro", t => t.FK_Peligro, cascadeDelete: true)
                .Index(t => t.FK_Peligro);
            
            CreateTable(
                "dbo.Tbl_INSHT",
                c => new
                    {
                        PK_INSHT = c.Int(nullable: false, identity: true),
                        FK_Persona_Expuesta = c.Int(nullable: false),
                        Estimacion_Riesgo = c.String(),
                        Medidas_De_Control = c.String(),
                        Procedimientos_De_Trabajo = c.String(),
                        Informacion = c.String(),
                        Formacion = c.String(),
                        Riesgo_Controlado = c.Boolean(nullable: false),
                        Accion_Requerida = c.String(),
                        Responsable = c.String(),
                        Fecha_Finalizacion = c.DateTime(nullable: false),
                        Fecha_De_Comprobacion = c.DateTime(nullable: false),
                        FirmaResponsable = c.String(),
                    })
                .PrimaryKey(t => t.PK_INSHT)
                .ForeignKey("dbo.Tbl_Persona_Expuesta_INSHT_RAM", t => t.FK_Persona_Expuesta, cascadeDelete: true)
                .Index(t => t.FK_Persona_Expuesta);
            
            CreateTable(
                "dbo.Tbl_Probabilidad_Por_PersonaExpuesta",
                c => new
                    {
                        PK_Probabilidad_Por_PersonaExpuesta = c.Int(nullable: false, identity: true),
                        FK_Probabilidad = c.Int(nullable: false),
                        Fk_Persona_Expuesta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Probabilidad_Por_PersonaExpuesta)
                .ForeignKey("dbo.Tbl_Persona_Expuesta_INSHT_RAM", t => t.Fk_Persona_Expuesta, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Probabilidad", t => t.FK_Probabilidad, cascadeDelete: true)
                .Index(t => t.FK_Probabilidad)
                .Index(t => t.Fk_Persona_Expuesta);
            
            CreateTable(
                "dbo.Tbl_Probabilidad",
                c => new
                    {
                        PK_Probabilidad = c.Int(nullable: false, identity: true),
                        Descripcion_Probabilidad = c.String(),
                        FK_Metodologia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Probabilidad)
                .ForeignKey("dbo.Tbl_Tipo_Metodologia", t => t.FK_Metodologia)
                .Index(t => t.FK_Metodologia);
            
            CreateTable(
                "dbo.Tbl_Tipo_Metodologia",
                c => new
                    {
                        PK_Metodologia = c.Int(nullable: false, identity: true),
                        Descripcion_Metodologia = c.String(),
                    })
                .PrimaryKey(t => t.PK_Metodologia);
            
            CreateTable(
                "dbo.Tbl_Grupo",
                c => new
                    {
                        PK_Grupo = c.Int(nullable: false, identity: true),
                        Descripcion_Grupo = c.String(),
                        FK_Metodologia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Grupo)
                .ForeignKey("dbo.Tbl_Tipo_Metodologia", t => t.FK_Metodologia, cascadeDelete: true)
                .Index(t => t.FK_Metodologia);
            
            CreateTable(
                "dbo.Tbl_GTC45",
                c => new
                    {
                        PK_GTC45 = c.Int(nullable: false, identity: true),
                        FLG_Higienico = c.Boolean(nullable: false),
                        FLG_Tipo_De_Calificacion = c.Boolean(nullable: false),
                        Efectos_Posibles = c.String(),
                        Nivel_De_Probablidad = c.Int(nullable: false),
                        Nivel_De_Riesgo = c.Int(nullable: false),
                        Numero_De_Expuestos = c.Int(nullable: false),
                        Peor_Consecuencia = c.String(),
                        FLG_Requisito_Legal = c.Boolean(nullable: false),
                        FK_Peligro = c.Int(nullable: false),
                        FK_Nivel_De_Deficiencia = c.Int(nullable: false),
                        FK_Nivel_De_Exposicion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_GTC45)
                .ForeignKey("dbo.Tbl_Nivel_De_Deficiencia", t => t.FK_Nivel_De_Deficiencia, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Nivel_De_Exposicion", t => t.FK_Nivel_De_Exposicion, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Peligro", t => t.FK_Peligro, cascadeDelete: true)
                .Index(t => t.FK_Peligro)
                .Index(t => t.FK_Nivel_De_Deficiencia)
                .Index(t => t.FK_Nivel_De_Exposicion);
            
            CreateTable(
                "dbo.Tbl_Nivel_De_Deficiencia",
                c => new
                    {
                        PK_Nivel_De_Deficiencia = c.Int(nullable: false, identity: true),
                        FLAG_Cuantitativa = c.Boolean(nullable: false),
                        Valor_Deficiencia = c.Int(nullable: false),
                        Descripcion_Deficiciencia = c.String(),
                    })
                .PrimaryKey(t => t.PK_Nivel_De_Deficiencia);
            
            CreateTable(
                "dbo.Tbl_Nivel_De_Exposicion",
                c => new
                    {
                        PK_Nivel_De_Exposicion = c.Int(nullable: false, identity: true),
                        Valor_Exposicion = c.Int(nullable: false),
                        Descripcion_Exposicion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Nivel_De_Exposicion);
            
            CreateTable(
                "dbo.Tbl_Sede",
                c => new
                    {
                        Pk_Id_Sede = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        Nombre_Sede = c.String(),
                        Direccion_Sede = c.String(),
                        Sector = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Sede)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Centro_de_Trabajo",
                c => new
                    {
                        Pk_Id_Centro_de_Trabajo = c.Int(nullable: false, identity: true),
                        ID_Centro = c.Int(nullable: false),
                        Descripcion_Actividad = c.String(),
                        Codigo_Actividad = c.Int(nullable: false),
                        Numero_Trabajadores = c.Int(nullable: false),
                        Fk_Id_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Centro_de_Trabajo)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: true)
                .Index(t => t.Fk_Id_Sede);
            
            CreateTable(
                "dbo.Tbl_RecursoporSede",
                c => new
                    {
                        Pk_Id_RecursoporSede = c.Int(nullable: false, identity: true),
                        Fk_Id_Recurso = c.Int(nullable: false),
                        Fk_Id_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_RecursoporSede)
                .ForeignKey("dbo.Tbl_Recurso", t => t.Fk_Id_Recurso, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_Id_Sede, cascadeDelete: true)
                .Index(t => t.Fk_Id_Recurso)
                .Index(t => t.Fk_Id_Sede);
            
            CreateTable(
                "dbo.Tbl_Recurso",
                c => new
                    {
                        Pk_Id_Recurso = c.Int(nullable: false, identity: true),
                        Nombre_Recurso = c.String(nullable: false),
                        Periodo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Recurso);
            
            CreateTable(
                "dbo.Tbl_RecursoFase",
                c => new
                    {
                        Pk_Id_RecursoFase = c.Int(nullable: false, identity: true),
                        Fk_Id_Recurso = c.Int(nullable: false),
                        Fk_Id_Fase = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_RecursoFase)
                .ForeignKey("dbo.Tbl_Fase", t => t.Fk_Id_Fase, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Recurso", t => t.Fk_Id_Recurso, cascadeDelete: true)
                .Index(t => t.Fk_Id_Recurso)
                .Index(t => t.Fk_Id_Fase);
            
            CreateTable(
                "dbo.Tbl_Fase",
                c => new
                    {
                        Pk_Id_Fase = c.Int(nullable: false, identity: true),
                        Descripcion_Fase = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Fase);
            
            CreateTable(
                "dbo.Tbl_RecursoTipoRecurso",
                c => new
                    {
                        Pk_Id_RecursoTipoRecurso = c.Int(nullable: false, identity: true),
                        Fk_Id_Recurso = c.Int(nullable: false),
                        Fk_Id_TipoRecurso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_RecursoTipoRecurso)
                .ForeignKey("dbo.Tbl_Recurso", t => t.Fk_Id_Recurso, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_TipoRecurso", t => t.Fk_Id_TipoRecurso, cascadeDelete: true)
                .Index(t => t.Fk_Id_Recurso)
                .Index(t => t.Fk_Id_TipoRecurso);
            
            CreateTable(
                "dbo.Tbl_TipoRecurso",
                c => new
                    {
                        Pk_Id_TipoRecurso = c.Int(nullable: false, identity: true),
                        Descripcion_Tipo_Recurso = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_TipoRecurso);
            
            CreateTable(
                "dbo.Tbl_SedeMunicipio",
                c => new
                    {
                        id_sedeMunicipio = c.Int(nullable: false, identity: true),
                        Fk_id_Sede = c.Int(nullable: false),
                        Fk_Id_Municipio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_sedeMunicipio)
                .ForeignKey("dbo.Tbl_Municipio", t => t.Fk_Id_Municipio, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Sede", t => t.Fk_id_Sede, cascadeDelete: true)
                .Index(t => t.Fk_id_Sede)
                .Index(t => t.Fk_Id_Municipio);
            
            CreateTable(
                "dbo.Tbl_Municipio",
                c => new
                    {
                        Pk_Id_Municipio = c.Int(nullable: false, identity: true),
                        Nombre_Municipio = c.String(),
                        Codigo_Municipio = c.String(),
                        Fk_Nombre_Departamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Municipio)
                .ForeignKey("dbo.Tbl_Departamento", t => t.Fk_Nombre_Departamento, cascadeDelete: true)
                .Index(t => t.Fk_Nombre_Departamento);
            
            CreateTable(
                "dbo.Tbl_Departamento",
                c => new
                    {
                        Pk_Id_Departamento = c.Int(nullable: false, identity: true),
                        Nombre_Departamento = c.String(),
                        Codigo_Departamento = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Departamento);
            
            CreateTable(
                "dbo.Tbl_Rol",
                c => new
                    {
                        Pk_Id_Rol = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Fk_Id_Empresa = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_Rol)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Cargo_Por_Rol",
                c => new
                    {
                        Id_Pk_CargoPorRol = c.Int(nullable: false, identity: true),
                        Fk_Id_Cargo = c.Int(nullable: false),
                        Fk_Id_Rol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_CargoPorRol)
                .ForeignKey("dbo.Tbl_Cargo", t => t.Fk_Id_Cargo, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Rol", t => t.Fk_Id_Rol, cascadeDelete: true)
                .Index(t => t.Fk_Id_Cargo)
                .Index(t => t.Fk_Id_Rol);
            
            CreateTable(
                "dbo.Tbl_Cargo",
                c => new
                    {
                        Pk_Id_Cargo = c.Int(nullable: false, identity: true),
                        Nombre_Cargo = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Cargo);
            
            CreateTable(
                "dbo.Tbl_Privilegios_Por_Rol",
                c => new
                    {
                        Id_Pk_PrivilegioRol = c.Int(nullable: false, identity: true),
                        Fk_Id_Rol = c.Int(nullable: false),
                        Fk_Id_Privilegios = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_PrivilegioRol)
                .ForeignKey("dbo.Tbl_Privilegios", t => t.Fk_Id_Privilegios, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Rol", t => t.Fk_Id_Rol, cascadeDelete: true)
                .Index(t => t.Fk_Id_Rol)
                .Index(t => t.Fk_Id_Privilegios);
            
            CreateTable(
                "dbo.Tbl_Privilegios",
                c => new
                    {
                        Pk_Id_Privilegio = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Privilegio);
            
            CreateTable(
                "dbo.Tbl_Rendicion_Cuenta_Por_Rol",
                c => new
                    {
                        Id_Pk_RendicionDeCuentasPorRol = c.Int(nullable: false, identity: true),
                        Fk_Id_Rol = c.Int(nullable: false),
                        Fk_Id_RendicionDeCuentas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_RendicionDeCuentasPorRol)
                .ForeignKey("dbo.Tbl_RendicionDeCuentas", t => t.Fk_Id_RendicionDeCuentas, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Rol", t => t.Fk_Id_Rol, cascadeDelete: true)
                .Index(t => t.Fk_Id_Rol)
                .Index(t => t.Fk_Id_RendicionDeCuentas);
            
            CreateTable(
                "dbo.Tbl_RendicionDeCuentas",
                c => new
                    {
                        Pk_Id_RendicionDeCuentas = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_RendicionDeCuentas);
            
            CreateTable(
                "dbo.Tbl_Responsabilidades_Por_Rol",
                c => new
                    {
                        Id_Pk_ResponsabilidadesPorRol = c.Int(nullable: false, identity: true),
                        Fk_Id_Rol = c.Int(nullable: false),
                        Fk_Id_Responsabilidades = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_ResponsabilidadesPorRol)
                .ForeignKey("dbo.Tbl_Responsabilidades", t => t.Fk_Id_Responsabilidades, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Rol", t => t.Fk_Id_Rol, cascadeDelete: true)
                .Index(t => t.Fk_Id_Rol)
                .Index(t => t.Fk_Id_Responsabilidades);
            
            CreateTable(
                "dbo.Tbl_Responsabilidades",
                c => new
                    {
                        Pk_Id_Responsabilidades = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Responsabilidades);
            
            CreateTable(
                "dbo.Tbl_Rol_Por_Tematica",
                c => new
                    {
                        Id_Pk_RolPorTematica = c.Int(nullable: false, identity: true),
                        Fk_Id_Tematica = c.Int(nullable: false),
                        Fk_Id_Rol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_RolPorTematica)
                .ForeignKey("dbo.Tbl_Rol", t => t.Fk_Id_Rol, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tematica", t => t.Fk_Id_Tematica, cascadeDelete: true)
                .Index(t => t.Fk_Id_Tematica)
                .Index(t => t.Fk_Id_Rol);
            
            CreateTable(
                "dbo.Tbl_Tematica",
                c => new
                    {
                        Id_Tematica = c.Int(nullable: false, identity: true),
                        Tematicas = c.String(),
                        Area = c.String(),
                        Diseno = c.String(),
                        Objetivo = c.String(),
                        DirigidoA = c.String(),
                        TipoTematica = c.Int(nullable: false),
                        NombreDocumento = c.String(),
                        SesionEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.Id_Tematica);
            
            CreateTable(
                "dbo.Tbl_Tematica_Por_Empresa",
                c => new
                    {
                        Id_Pk_TematicaPorEmpresa = c.Int(nullable: false, identity: true),
                        Fk_Id_Tematica = c.Int(nullable: false),
                        Fk_Id_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_TematicaPorEmpresa)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tematica", t => t.Fk_Id_Tematica, cascadeDelete: true)
                .Index(t => t.Fk_Id_Tematica)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_UsuarioRol",
                c => new
                    {
                        Pk_Id_UsuarioRol = c.Int(nullable: false, identity: true),
                        Fk_Id_Rol = c.Int(nullable: false),
                        Fk_Id_Usuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_UsuarioRol)
                .ForeignKey("dbo.Tbl_Rol", t => t.Fk_Id_Rol)
                .ForeignKey("dbo.Tbl_Usuario", t => t.Fk_Id_Usuario, cascadeDelete: true)
                .Index(t => t.Fk_Id_Rol)
                .Index(t => t.Fk_Id_Usuario);
            
            CreateTable(
                "dbo.Tbl_Usuario",
                c => new
                    {
                        Pk_Id_Usuario = c.Int(nullable: false, identity: true),
                        Fk_Tipo_Documento = c.Int(nullable: false),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre_Usuario = c.String(),
                        Imagen_Firma = c.String(),
                        nit_Empresa = c.String(),
                        Fk_Id_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Usuario)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tipo_Documento", t => t.Fk_Tipo_Documento, cascadeDelete: true)
                .Index(t => t.Fk_Tipo_Documento)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Tipo_Documento",
                c => new
                    {
                        PK_IDTipo_Documento = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.PK_IDTipo_Documento);
            
            CreateTable(
                "dbo.Tbl_Hallazgo",
                c => new
                    {
                        Pk_Id_Hallazgo = c.Int(nullable: false, identity: true),
                        Halla_Norma = c.String(maxLength: 200),
                        Halla_Numeral = c.String(maxLength: 200),
                        Halla_Descripcion = c.String(nullable: false, maxLength: 2000),
                        Halla_Proceso = c.String(maxLength: 200),
                        Fk_Id_Accion = c.Int(nullable: false),
                        Fk_Id_Proceso = c.Int(nullable: false),
                        Proceso_Pk_Id_Proceso = c.Int(),
                    })
                .PrimaryKey(t => t.Pk_Id_Hallazgo)
                .ForeignKey("dbo.Tbl_Acciones", t => t.Fk_Id_Accion, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Proceso", t => t.Proceso_Pk_Id_Proceso)
                .Index(t => t.Fk_Id_Accion)
                .Index(t => t.Proceso_Pk_Id_Proceso);
            
            CreateTable(
                "dbo.Tbl_Seguimiento",
                c => new
                    {
                        Pk_Id_Seguimiento = c.Int(nullable: false, identity: true),
                        Fecha_Seg = c.DateTime(nullable: false),
                        Observaciones = c.String(maxLength: 1000),
                        RutaFirma = c.String(),
                        Fk_Id_Accion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Seguimiento)
                .ForeignKey("dbo.Tbl_Acciones", t => t.Fk_Id_Accion, cascadeDelete: true)
                .Index(t => t.Fk_Id_Accion);
            
            CreateTable(
                "dbo.Tbl_Actividad_Presupuesto",
                c => new
                    {
                        PK_Actividad_Presupuesto = c.Int(nullable: false, identity: true),
                        DescripcionActividad = c.String(),
                        FK_Actividad_Presupuesto = c.Int(),
                    })
                .PrimaryKey(t => t.PK_Actividad_Presupuesto)
                .ForeignKey("dbo.Tbl_Actividad_Presupuesto", t => t.FK_Actividad_Presupuesto)
                .Index(t => t.FK_Actividad_Presupuesto);
            
            CreateTable(
                "dbo.Tbl_Prepuesto_Por_Mes",
                c => new
                    {
                        PK_Prepuesto_Por_Mes = c.Int(nullable: false, identity: true),
                        PresupuestoMes = c.Double(nullable: false),
                        PresupuestoEjecutadoPorMes = c.Double(nullable: false),
                        Mes = c.Int(nullable: false),
                        ComentarioPresupuestoMesEjecutado = c.String(),
                        FK_Presupuesto = c.Int(nullable: false),
                        FK_Actividad_Presupuesto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Prepuesto_Por_Mes)
                .ForeignKey("dbo.Tbl_Actividad_Presupuesto", t => t.FK_Actividad_Presupuesto, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Presupuesto", t => t.FK_Presupuesto, cascadeDelete: true)
                .Index(t => t.FK_Presupuesto)
                .Index(t => t.FK_Actividad_Presupuesto);
            
            CreateTable(
                "dbo.Tbl_Presupuesto",
                c => new
                    {
                        PK_Prepuesto = c.Int(nullable: false, identity: true),
                        RubroTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Prepuesto);
            
            CreateTable(
                "dbo.Tbl_Presupuesto_Por_Año",
                c => new
                    {
                        PK_Presupuesto_Por_Año = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        FK_Presupuesto = c.Int(nullable: false),
                        FK_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Presupuesto_Por_Año)
                .ForeignKey("dbo.Tbl_Presupuesto", t => t.FK_Presupuesto, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_Sede, cascadeDelete: true)
                .Index(t => t.FK_Presupuesto)
                .Index(t => t.FK_Sede);
            
            CreateTable(
                "dbo.Tbl_Actividades_Criterio",
                c => new
                    {
                        Pk_Id_Actividad_Criterio = c.Int(nullable: false, identity: true),
                        Fk_Id_Actividad = c.Int(nullable: false),
                        Fk_Id_Eval_Estandar_Minimo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Actividad_Criterio)
                .ForeignKey("dbo.Tbl_Actividades_Evaluacion", t => t.Fk_Id_Actividad, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Evaluacion_Estandares_Minimos", t => t.Fk_Id_Eval_Estandar_Minimo, cascadeDelete: true)
                .Index(t => t.Fk_Id_Actividad)
                .Index(t => t.Fk_Id_Eval_Estandar_Minimo);
            
            CreateTable(
                "dbo.Tbl_Actividades_Evaluacion",
                c => new
                    {
                        Pk_Id_Actividad = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 500),
                        Responsable = c.String(maxLength: 50),
                        FechaFin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Actividad);
            
            CreateTable(
                "dbo.Tbl_Evaluacion_Estandares_Minimos",
                c => new
                    {
                        Pk_Id_Eval_Estandar_Minimo = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa_Evaluar = c.Int(nullable: false),
                        Fk_Id_Criterio = c.Int(nullable: false),
                        Fk_Id_Config_Valoracion_SubEstand = c.Int(nullable: false),
                        Justificacion = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Pk_Id_Eval_Estandar_Minimo)
                .ForeignKey("dbo.Tbl_Config_Valoracion_SubEstandares", t => t.Fk_Id_Config_Valoracion_SubEstand, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Criterios", t => t.Fk_Id_Criterio, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Empresas_Evaluar", t => t.Fk_Id_Empresa_Evaluar, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa_Evaluar)
                .Index(t => t.Fk_Id_Criterio)
                .Index(t => t.Fk_Id_Config_Valoracion_SubEstand);
            
            CreateTable(
                "dbo.Tbl_Config_Valoracion_SubEstandares",
                c => new
                    {
                        Pk_Id_Config_Valoracion_SubEstand = c.Int(nullable: false, identity: true),
                        Fk_Id_SubEstandar = c.Int(nullable: false),
                        Fk_Id_Valoracion_Criterio = c.Int(nullable: false),
                        Id_Dpendiente = c.Int(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Pk_Id_Config_Valoracion_SubEstand)
                .ForeignKey("dbo.Tbl_SubEstandares", t => t.Fk_Id_SubEstandar, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Valoracion_Criterios", t => t.Fk_Id_Valoracion_Criterio, cascadeDelete: true)
                .Index(t => t.Fk_Id_SubEstandar)
                .Index(t => t.Fk_Id_Valoracion_Criterio);
            
            CreateTable(
                "dbo.Tbl_SubEstandares",
                c => new
                    {
                        Pk_Id_SubEstandar = c.Int(nullable: false, identity: true),
                        Fk_Id_Estandar = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 1000),
                        Descripcion_Corta = c.String(maxLength: 500),
                        Procentaje_Max = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_SubEstandar)
                .ForeignKey("dbo.Tbl_Estandares", t => t.Fk_Id_Estandar, cascadeDelete: true)
                .Index(t => t.Fk_Id_Estandar);
            
            CreateTable(
                "dbo.Tbl_Criterios",
                c => new
                    {
                        Pk_Id_Criterio = c.Int(nullable: false, identity: true),
                        Fk_Id_SubEstandar = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 4000),
                        Descripcion_Corta = c.String(maxLength: 1000),
                        Numeral = c.String(maxLength: 10),
                        Marco_Legal = c.String(maxLength: 4000),
                        Modo_Verificacion = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Pk_Id_Criterio)
                .ForeignKey("dbo.Tbl_SubEstandares", t => t.Fk_Id_SubEstandar)
                .Index(t => t.Fk_Id_SubEstandar);
            
            CreateTable(
                "dbo.Tbl_Estandares",
                c => new
                    {
                        Pk_Id_Estandar = c.Int(nullable: false, identity: true),
                        Fk_Id_Ciclo = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 500),
                        Porcentaje_Max = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Pk_Id_Estandar)
                .ForeignKey("dbo.Tbl_Ciclos", t => t.Fk_Id_Ciclo, cascadeDelete: true)
                .Index(t => t.Fk_Id_Ciclo);
            
            CreateTable(
                "dbo.Tbl_Ciclos",
                c => new
                    {
                        Pk_Id_Ciclo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50),
                        Porcentaje_Max = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Pk_Id_Ciclo);
            
            CreateTable(
                "dbo.Tbl_Valoracion_Criterios",
                c => new
                    {
                        Pk_Id_Valoracion_Criterio = c.Int(nullable: false, identity: true),
                        Id_Valoracion_Criterio_Padre = c.Int(),
                        Descripcion = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Pk_Id_Valoracion_Criterio);
            
            CreateTable(
                "dbo.Tbl_Empresas_Evaluar",
                c => new
                    {
                        Pk_Id_Empresa_Evaluar = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa = c.Int(nullable: false),
                        CodSede = c.Int(nullable: false),
                        Responsable_SGSST = c.String(),
                        Elaborado_Por = c.String(maxLength: 20),
                        Num_Licencia_SOSL = c.String(maxLength: 15),
                        Fecha_Diligencia_Eval_Inicial = c.DateTime(),
                        Fecha_Diligencia_Eval_EstMin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Pk_Id_Empresa_Evaluar)
                .ForeignKey("dbo.Tbl_Empresa", t => t.Fk_Id_Empresa, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Empresa_Aspectos",
                c => new
                    {
                        Pk_Id_Empresa_Aspecto = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa_Evaluar = c.Int(nullable: false),
                        Fk_Id_Aspecto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Empresa_Aspecto)
                .ForeignKey("dbo.Tbl_Aspectos", t => t.Fk_Id_Aspecto, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Empresas_Evaluar", t => t.Fk_Id_Empresa_Evaluar, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa_Evaluar)
                .Index(t => t.Fk_Id_Aspecto);
            
            CreateTable(
                "dbo.Tbl_Aspectos",
                c => new
                    {
                        Pk_Id_Aspecto = c.Int(nullable: false, identity: true),
                        Fk_Id_Valoracion_Aspecto = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Observacion = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Pk_Id_Aspecto)
                .ForeignKey("dbo.Tbl_Valoracion_Aspectos", t => t.Fk_Id_Valoracion_Aspecto, cascadeDelete: true)
                .Index(t => t.Fk_Id_Valoracion_Aspecto);
            
            CreateTable(
                "dbo.Tbl_Valoracion_Aspectos",
                c => new
                    {
                        Pk_Id_Valoracion_Aspecto = c.Int(nullable: false, identity: true),
                        Valoracion = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Pk_Id_Valoracion_Aspecto);
            
            CreateTable(
                "dbo.Tbl_Evaluacion_Inicial_Aspectos",
                c => new
                    {
                        Pk_Id_Evaluacion_Inicial_Aspecto = c.Int(nullable: false, identity: true),
                        Fk_Id_Empresa_Aspecto = c.Int(nullable: false),
                        Valor_Valoracion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Pk_Id_Evaluacion_Inicial_Aspecto)
                .ForeignKey("dbo.Tbl_Empresa_Aspectos", t => t.Fk_Id_Empresa_Aspecto, cascadeDelete: true)
                .Index(t => t.Fk_Id_Empresa_Aspecto);
            
            CreateTable(
                "dbo.Tbl_AntecedentesExposicionLaboral",
                c => new
                    {
                        PK_AntecedentesExposicionLaboral = c.Int(nullable: false, identity: true),
                        Descripcion_AntecedentesExposicionLaboral = c.String(),
                    })
                .PrimaryKey(t => t.PK_AntecedentesExposicionLaboral);
            
            CreateTable(
                "dbo.Tbl_Ausencias",
                c => new
                    {
                        Pk_Id_Ausencias = c.Int(nullable: false, identity: true),
                        FK_Id_Ausencias_Padre = c.Int(nullable: false),
                        Documento_Persona = c.String(),
                        NitEmpresa = c.String(),
                        FK_Id_Contingencia = c.Int(nullable: false),
                        FK_Id_Diagnostico = c.Int(nullable: false),
                        FK_Id_Sede = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        Fecha_Fin = c.DateTime(nullable: false),
                        DiasAusencia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Factor_Prestacional = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observaciones = c.String(),
                        FechaRegistro = c.DateTime(),
                        FechaModificacion = c.DateTime(),
                        FK_Id_Departamento = c.Int(nullable: false),
                        FK_Id_Municipio = c.Int(nullable: false),
                        Sexo = c.String(),
                        Tipo_Vinculacion = c.String(),
                        FK_Id_Ocupacion = c.Int(nullable: false),
                        Edad = c.Int(),
                        Eps = c.String(),
                        FK_Id_EmpresaUsuaria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Ausencias)
                .ForeignKey("dbo.Tbl_Contingencias", t => t.FK_Id_Contingencia, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Diagnosticos", t => t.FK_Id_Diagnostico, cascadeDelete: true)
                .Index(t => t.FK_Id_Contingencia)
                .Index(t => t.FK_Id_Diagnostico);
            
            CreateTable(
                "dbo.Tbl_Contingencias",
                c => new
                    {
                        PK_Id_Contingencia = c.Int(nullable: false, identity: true),
                        Tipo_Contingencia = c.String(),
                        Detalle = c.String(),
                        Fecha_Ingreso = c.DateTime(nullable: false),
                        Fecha_Modificacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_Contingencia);
            
            CreateTable(
                "dbo.Tbl_Diagnosticos",
                c => new
                    {
                        PK_Id_Diagnostico = c.Int(nullable: false, identity: true),
                        Codigo_CIE = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_Diagnostico);
            
            CreateTable(
                "dbo.Tbl_CIU",
                c => new
                    {
                        Pk_Id_CIU = c.Int(nullable: false, identity: true),
                        Codigo_CIU = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_CIU);
            
            CreateTable(
                "dbo.Tbl_Competencia",
                c => new
                    {
                        Pk_Id_Competencia = c.Int(nullable: false, identity: true),
                        Id_rol = c.Int(nullable: false),
                        Id_Cargo = c.Int(nullable: false),
                        Tematica = c.String(),
                        Documento = c.String(),
                        Id_SessionEmpresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Competencia);
            
            CreateTable(
                "dbo.Tbl_ConfiguracionesHHT",
                c => new
                    {
                        id_Configuracion = c.Int(nullable: false, identity: true),
                        Documento_empresa = c.String(),
                        Ano = c.Int(nullable: false),
                        Mes = c.Int(nullable: false),
                        Dias_Laborales = c.Int(nullable: false),
                        Horas_Laborales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Num_Trabajadores_XT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Dias_Trabajados_DTM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Horas_Hombre_HTD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Horas_Extras_NHE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Horas_Ausentismo_NHA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha_Modificacion = c.DateTime(nullable: false),
                        Total_HHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id_Configuracion);
            
            CreateTable(
                "dbo.Tbl_Cumplimiento_Evaluacion",
                c => new
                    {
                        PK_Cumplimiento_Evaluacion = c.Int(nullable: false, identity: true),
                        Descripcion_Cumplimiento_Evaluacion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Cumplimiento_Evaluacion);
            
            CreateTable(
                "dbo.Tbl_Dias_Laborables",
                c => new
                    {
                        PK_Id_Dia_Laborable = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Id_Dia_Laborable);
            
            CreateTable(
                "dbo.Tbl_Dias_Laborables_Empresa",
                c => new
                    {
                        PK_Id_Dias_Laborables_Empresa = c.Int(nullable: false, identity: true),
                        Documento_empresa = c.String(),
                        FK_Id_Dias_Laborables = c.Int(),
                    })
                .PrimaryKey(t => t.PK_Id_Dias_Laborables_Empresa);
            
            CreateTable(
                "dbo.Tbl_Doc_Dx_Condiciones_De_Salud",
                c => new
                    {
                        Pk_DocDxCondicionesDeSalud = c.Int(nullable: false, identity: true),
                        Nombre_Diagnostico = c.String(),
                        Nombre_Documento = c.String(),
                        FK_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_DocDxCondicionesDeSalud)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_Sede, cascadeDelete: true)
                .Index(t => t.FK_Sede);
            
            CreateTable(
                "dbo.Tbl_Documentacion_Organizacion",
                c => new
                    {
                        ID_Documentacion_Org = c.Int(nullable: false, identity: true),
                        FK_TipoModuloOrganizacion = c.Int(nullable: false),
                        NombreArchivo_Documentacion = c.String(),
                        FechaModificacion_Documentacion = c.DateTime(nullable: false),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Documentacion_Org)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_TipoModulo_Organizacion", t => t.FK_TipoModuloOrganizacion, cascadeDelete: true)
                .Index(t => t.FK_TipoModuloOrganizacion)
                .Index(t => t.FK_Empresa);
            
            CreateTable(
                "dbo.Tbl_TipoModulo_Organizacion",
                c => new
                    {
                        ID_TipoModulo_Organizacion = c.Int(nullable: false, identity: true),
                        Descripcion_ModuloOrg = c.String(),
                    })
                .PrimaryKey(t => t.ID_TipoModulo_Organizacion);
            
            CreateTable(
                "dbo.Tbl_Dx_Condiciones_De_Salud",
                c => new
                    {
                        Pk_DxCondicionesDeSalud = c.Int(nullable: false, identity: true),
                        Fecha_Dx = c.DateTime(nullable: false),
                        Lugar = c.String(),
                        Trabajadores_Lugar = c.Int(nullable: false),
                        Sintomatologia = c.String(),
                        Trabajadores_Sintomatologia = c.Int(nullable: false),
                        Prueba_Clinica = c.String(),
                        Trabajadores_Con_Prueba = c.Int(nullable: false),
                        Prueba_P_Clinica = c.String(),
                        Trabajadores_Con_Prueba_P = c.Int(nullable: false),
                        Trabajadores_Con_Diagnostico = c.Int(nullable: false),
                        FK_Diagnostico = c.Int(nullable: false),
                        FK_Clasificacion_De_Peligro = c.Int(nullable: false),
                        FK_Sede = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_DxCondicionesDeSalud)
                .ForeignKey("dbo.Tbl_Clasificacion_De_Peligro", t => t.FK_Clasificacion_De_Peligro, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Diagnosticos", t => t.FK_Diagnostico, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Sede", t => t.FK_Sede, cascadeDelete: true)
                .Index(t => t.FK_Diagnostico)
                .Index(t => t.FK_Clasificacion_De_Peligro)
                .Index(t => t.FK_Sede);
            
            CreateTable(
                "dbo.Tbl_Elemento_Matriz",
                c => new
                    {
                        PK_Elemento_Matriz = c.Int(nullable: false, identity: true),
                        Descripcion_Elemento = c.String(),
                        FK_Matriz = c.Int(nullable: false),
                        FK_TipoElementoAnalisis = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Elemento_Matriz)
                .ForeignKey("dbo.Tbl_Matriz", t => t.FK_Matriz, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tipo_Elemento_Analisis", t => t.FK_TipoElementoAnalisis, cascadeDelete: true)
                .Index(t => t.FK_Matriz)
                .Index(t => t.FK_TipoElementoAnalisis);
            
            CreateTable(
                "dbo.Tbl_Matriz",
                c => new
                    {
                        PK_Matriz = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Matriz)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
            CreateTable(
                "dbo.Tbl_Tipo_Elemento_Analisis",
                c => new
                    {
                        PK_Tipo_Elemneto_Analisis = c.Int(nullable: false, identity: true),
                        Descripcion_Elemento = c.String(),
                        FK_Tipo_Analisis = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Tipo_Elemneto_Analisis)
                .ForeignKey("dbo.Tbl_Tipo_Analisis", t => t.FK_Tipo_Analisis, cascadeDelete: true)
                .Index(t => t.FK_Tipo_Analisis);
            
            CreateTable(
                "dbo.Tbl_Tipo_Analisis",
                c => new
                    {
                        PK_Tipo_Analisis = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Tipo_Analisis);
            
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
                .PrimaryKey(t => t.IDEmpleado_PerfilSocioDemoGrafico)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
            CreateTable(
                "dbo.Tbl_EmpleadoTercero",
                c => new
                    {
                        ID_Empleado = c.Int(nullable: false, identity: true),
                        FK_Tipo_Documento_Empl = c.Int(nullable: false),
                        PK_Numero_Documento_Empl = c.Int(nullable: false),
                        Nombre1 = c.String(),
                        Nombre2 = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        FK_Ocupacion_Empl = c.Int(nullable: false),
                        Email = c.String(),
                        Telefono = c.Int(nullable: false),
                        Ocupacion_Empl = c.String(),
                        Cargo_Empl = c.String(),
                        Email_Empl = c.String(),
                        FK_EmpresaTercero = c.Int(nullable: false),
                        FKRelacionLaboralTercero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Empleado)
                .ForeignKey("dbo.Tbl_EmpresaTercero", t => t.FK_EmpresaTercero, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Ocupacion", t => t.FK_Ocupacion_Empl, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_RelacionesLaboralesTercero", t => t.FKRelacionLaboralTercero, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tipo_Documento", t => t.FK_Tipo_Documento_Empl, cascadeDelete: true)
                .Index(t => t.FK_Tipo_Documento_Empl)
                .Index(t => t.FK_Ocupacion_Empl)
                .Index(t => t.FK_EmpresaTercero)
                .Index(t => t.FKRelacionLaboralTercero);
            
            CreateTable(
                "dbo.Tbl_EmpresaTercero",
                c => new
                    {
                        Pk_Id_Empresa = c.Int(nullable: false, identity: true),
                        Nit_Empresa = c.String(),
                        Razon_Social = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Empresa);
            
            CreateTable(
                "dbo.Tbl_Ocupacion",
                c => new
                    {
                        PK_Ocupacion = c.Int(nullable: false, identity: true),
                        Descripcion_Ocupacion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Ocupacion);
            
            CreateTable(
                "dbo.Tbl_RelacionesLaboralesTercero",
                c => new
                    {
                        Pk_Id_RelacionesLaboralesTercero = c.Int(nullable: false, identity: true),
                        Descripcion_Relacion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_RelacionesLaboralesTercero);
            
            CreateTable(
                "dbo.Tbl_Empresas_Usuarias",
                c => new
                    {
                        PK_Id_Empresa_Usuaria = c.Int(nullable: false, identity: true),
                        Documento_Empresa = c.String(),
                        Documento_Empresa_Usuaria = c.String(),
                        FK_Id_Tipo_Documento = c.Int(nullable: false),
                        Razon_Social = c.String(),
                        Direccion = c.String(),
                        FK_Id_Departamento = c.Int(nullable: false),
                        FK_Id_Municipio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_Id_Empresa_Usuaria);
            
            CreateTable(
                "dbo.Tbl_Estado_Civil",
                c => new
                    {
                        PK_Estado_Civil = c.Int(nullable: false, identity: true),
                        Descripcion_EstadoCivil = c.String(),
                    })
                .PrimaryKey(t => t.PK_Estado_Civil);
            
            CreateTable(
                "dbo.Tbl_Estado_Empl",
                c => new
                    {
                        PK_IDEmpleadoEst = c.Int(nullable: false, identity: true),
                        EstEmplead = c.String(),
                    })
                .PrimaryKey(t => t.PK_IDEmpleadoEst);
            
            CreateTable(
                "dbo.Tbl_Estado_RequisitoslegalesOtros",
                c => new
                    {
                        PK_Estado_RequisitoslegalesOtros = c.Int(nullable: false, identity: true),
                        Descripcion_Estado_RequisitoslegalesOtros = c.String(),
                    })
                .PrimaryKey(t => t.PK_Estado_RequisitoslegalesOtros);
            
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
                "dbo.Tbl_InconsistenciasLaborales",
                c => new
                    {
                        PKInconsistencia = c.Int(nullable: false, identity: true),
                        FKTipoInconsistencia = c.Int(nullable: false),
                        DescripcionInconsistencia = c.String(),
                    })
                .PrimaryKey(t => t.PKInconsistencia)
                .ForeignKey("dbo.Tbl_TipoInconsistenciaLaboral", t => t.FKTipoInconsistencia, cascadeDelete: true)
                .Index(t => t.FKTipoInconsistencia);
            
            CreateTable(
                "dbo.Tbl_TipoInconsistenciaLaboral",
                c => new
                    {
                        PKTipoInconsistencia = c.Int(nullable: false, identity: true),
                        DescripcionTipInc = c.String(),
                    })
                .PrimaryKey(t => t.PKTipoInconsistencia);
            
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
                .PrimaryKey(t => t.ID_Informacion_laboral)
                .ForeignKey("dbo.Tbl_AntecedentesExposicionLaboral", t => t.FK_AntecedentesExposicionLaboral, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_EmpleadoPerfilSocioDemografico", t => t.FKIDEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Ocupacion", t => t.FK_Ocupacion_Empl, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_VinculacionLaboral", t => t.FK_VinculacionLaboral, cascadeDelete: true)
                .Index(t => t.FKIDEmpleado)
                .Index(t => t.FK_Ocupacion_Empl)
                .Index(t => t.FK_VinculacionLaboral)
                .Index(t => t.FK_AntecedentesExposicionLaboral);
            
            CreateTable(
                "dbo.Tbl_VinculacionLaboral",
                c => new
                    {
                        PK_VinculacionLaboral = c.Int(nullable: false, identity: true),
                        Descripcion_VinculacionLaboral = c.String(),
                    })
                .PrimaryKey(t => t.PK_VinculacionLaboral);
            
            CreateTable(
                "dbo.Tbl_Ingresos",
                c => new
                    {
                        PK_Ingresos = c.Int(nullable: false, identity: true),
                        Descripcion_Ingresos = c.String(),
                    })
                .PrimaryKey(t => t.PK_Ingresos);
            
            CreateTable(
                "dbo.Tbl_Interpretacion_De_Probabilidad",
                c => new
                    {
                        PK_Interpretacion_De_Probabilidad = c.Int(nullable: false, identity: true),
                        Nivel_Inferior = c.Int(nullable: false),
                        Nivel_Superior = c.Int(nullable: false),
                        Interpretacion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Interpretacion_De_Probabilidad);
            
            CreateTable(
                "dbo.Tbl_Interpretacion_Nivel_Riesgo",
                c => new
                    {
                        PK_Interpretacion_Nivel_Riesgo = c.Int(nullable: false, identity: true),
                        Nivel_Inferior = c.Int(nullable: false),
                        Nivel_Superior = c.Int(nullable: false),
                        Resultado = c.String(),
                        Interpretacion = c.String(),
                    })
                .PrimaryKey(t => t.PK_Interpretacion_Nivel_Riesgo);
            
            CreateTable(
                "dbo.Tbl_Obligaciones_Arl",
                c => new
                    {
                        Pk_Id_Obligaciones_Arl = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Obligaciones_Arl);
            
            CreateTable(
                "dbo.Tbl_Obligaciones_Empleadores",
                c => new
                    {
                        Pk_Id_Obligaciones_Empleadores = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Obligaciones_Empleadores);
            
            CreateTable(
                "dbo.Tbl_OtrasInteracciones",
                c => new
                    {
                        ID_OtrasInteraciones = c.Int(nullable: false, identity: true),
                        Nit_Empresa = c.Int(nullable: false),
                        TipoDocumento_Archivo = c.String(),
                        Archivo_OtrasInteracciones = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_OtrasInteraciones)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
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
                .PrimaryKey(t => t.PK_PeligroSede)
                .ForeignKey("dbo.Tbl_EmpleadoPerfilSocioDemografico", t => t.FKIDEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_SedePeligro", t => t.FK_Sede, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_ZonaLugar", t => t.FK_ZonaLugar, cascadeDelete: true)
                .Index(t => t.FKIDEmpleado)
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
            
            CreateTable(
                "dbo.Tbl_PerfilSocioDemografico",
                c => new
                    {
                        PK_PerfilSocioDemografico = c.Int(nullable: false, identity: true),
                        FK_IDEmpleado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_PerfilSocioDemografico)
                .ForeignKey("dbo.Tbl_Empleado", t => t.FK_IDEmpleado, cascadeDelete: true)
                .Index(t => t.FK_IDEmpleado);
            
            CreateTable(
                "dbo.Tbl_Empleado",
                c => new
                    {
                        ID_Empleado = c.Int(nullable: false, identity: true),
                        FK_Tipo_Documento_Empl = c.Int(nullable: false),
                        PK_Numero_Documento_Empl = c.Int(nullable: false),
                        Nombre1 = c.String(),
                        Nombre2 = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        FK_ID_Estado = c.Int(nullable: false),
                        FK_ID_Tipo_Cotizante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Empleado)
                .ForeignKey("dbo.Tbl_Estado_Empl", t => t.FK_ID_Estado, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_TipoCotizante", t => t.FK_ID_Tipo_Cotizante, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tipo_Documento", t => t.FK_Tipo_Documento_Empl, cascadeDelete: true)
                .Index(t => t.FK_Tipo_Documento_Empl)
                .Index(t => t.FK_ID_Estado)
                .Index(t => t.FK_ID_Tipo_Cotizante);
            
            CreateTable(
                "dbo.Tbl_TipoCotizante",
                c => new
                    {
                        Pk_Id_Cotizante = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_Cotizante);
            
            CreateTable(
                "dbo.Tbl_Politica",
                c => new
                    {
                        intCod_Politica = c.Int(nullable: false, identity: true),
                        Firma_Rep = c.Boolean(nullable: false),
                        strDescripcion_Politica = c.String(),
                        Archivo_Politica = c.String(),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.intCod_Politica)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .Index(t => t.FK_Empresa);
            
            CreateTable(
                "dbo.Tbl_Raza",
                c => new
                    {
                        PK_Raza = c.Int(nullable: false, identity: true),
                        Descripcion_Raza = c.String(),
                    })
                .PrimaryKey(t => t.PK_Raza);
            
            CreateTable(
                "dbo.Tbl_Requisitos_legales_Otros",
                c => new
                    {
                        PK_RequisitosLegalesOtros = c.Int(nullable: false, identity: true),
                        Norma = c.String(maxLength: 50),
                        Sistema = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                        Ente = c.String(),
                        Articulo = c.String(),
                        Descripcion = c.String(),
                        Modificacion = c.String(),
                        Sugerencias = c.String(),
                        PartesInteresadas = c.String(),
                        Clase_De_Peligro = c.String(),
                        Peligro = c.String(),
                        Aspectos = c.String(),
                        Impactos = c.String(),
                        Evidencia_Cumplimiento = c.String(),
                        FK_Cumplimiento_Evaluacion = c.Int(nullable: false),
                        Hallazgo = c.String(),
                        FK_Estado_RequisitoslegalesOtros = c.Int(nullable: false),
                        Responsable = c.String(),
                        Fecha_Seguimiento_Control = c.DateTime(nullable: false),
                        Fecha_Actualizacion = c.DateTime(nullable: false),
                        FK_Empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PK_RequisitosLegalesOtros)
                .ForeignKey("dbo.Tbl_Empresa", t => t.FK_Empresa, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Cumplimiento_Evaluacion", t => t.FK_Cumplimiento_Evaluacion, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Estado_RequisitoslegalesOtros", t => t.FK_Estado_RequisitoslegalesOtros, cascadeDelete: true)
                .Index(t => t.FK_Cumplimiento_Evaluacion)
                .Index(t => t.FK_Estado_RequisitoslegalesOtros)
                .Index(t => t.FK_Empresa);
            
            CreateTable(
                "dbo.Tbl_Sexo",
                c => new
                    {
                        PK_Sexo = c.Int(nullable: false, identity: true),
                        Descripcion_TurnoTrabajo = c.String(),
                    })
                .PrimaryKey(t => t.PK_Sexo);
            
            CreateTable(
                "dbo.Tbl_Valoracion_Final",
                c => new
                    {
                        Pk_Id_Valoracion_Final = c.Int(nullable: false, identity: true),
                        Rango_Inicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rango_Final = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CriterioEvaluacion = c.String(maxLength: 100),
                        Valoracion = c.String(maxLength: 100),
                        Accion = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Pk_Id_Valoracion_Final);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Estado_RequisitoslegalesOtros", "dbo.Tbl_Estado_RequisitoslegalesOtros");
            DropForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Cumplimiento_Evaluacion", "dbo.Tbl_Cumplimiento_Evaluacion");
            DropForeignKey("dbo.Tbl_Requisitos_legales_Otros", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Politica", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_PerfilSocioDemografico", "FK_IDEmpleado", "dbo.Tbl_Empleado");
            DropForeignKey("dbo.Tbl_Empleado", "FK_Tipo_Documento_Empl", "dbo.Tbl_Tipo_Documento");
            DropForeignKey("dbo.Tbl_Empleado", "FK_ID_Tipo_Cotizante", "dbo.Tbl_TipoCotizante");
            DropForeignKey("dbo.Tbl_Empleado", "FK_ID_Estado", "dbo.Tbl_Estado_Empl");
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_ZonaLugar", "dbo.Tbl_ZonaLugar");
            DropForeignKey("dbo.Tbl_PeligroSede", "FK_Sede", "dbo.Tbl_SedePeligro");
            DropForeignKey("dbo.Tbl_PeligroSede", "FKIDEmpleado", "dbo.Tbl_EmpleadoPerfilSocioDemografico");
            DropForeignKey("dbo.Tbl_OtrasInteracciones", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_VinculacionLaboral", "dbo.Tbl_VinculacionLaboral");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FKIDEmpleado", "dbo.Tbl_EmpleadoPerfilSocioDemografico");
            DropForeignKey("dbo.Tbl_Informacion_laboral", "FK_AntecedentesExposicionLaboral", "dbo.Tbl_AntecedentesExposicionLaboral");
            DropForeignKey("dbo.Tbl_InconsistenciasLaborales", "FKTipoInconsistencia", "dbo.Tbl_TipoInconsistenciaLaboral");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Tipo_Documento_Empl", "dbo.Tbl_Tipo_Documento");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FKRelacionLaboralTercero", "dbo.Tbl_RelacionesLaboralesTercero");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Ocupacion_Empl", "dbo.Tbl_Ocupacion");
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_EmpresaTercero", "dbo.Tbl_EmpresaTercero");
            DropForeignKey("dbo.Tbl_EmpleadoPerfilSocioDemografico", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Elemento_Matriz", "FK_TipoElementoAnalisis", "dbo.Tbl_Tipo_Elemento_Analisis");
            DropForeignKey("dbo.Tbl_Tipo_Elemento_Analisis", "FK_Tipo_Analisis", "dbo.Tbl_Tipo_Analisis");
            DropForeignKey("dbo.Tbl_Elemento_Matriz", "FK_Matriz", "dbo.Tbl_Matriz");
            DropForeignKey("dbo.Tbl_Matriz", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Diagnostico", "dbo.Tbl_Diagnosticos");
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropForeignKey("dbo.Tbl_Documentacion_Organizacion", "FK_TipoModuloOrganizacion", "dbo.Tbl_TipoModulo_Organizacion");
            DropForeignKey("dbo.Tbl_Documentacion_Organizacion", "FK_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Doc_Dx_Condiciones_De_Salud", "FK_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Ausencias", "FK_Id_Diagnostico", "dbo.Tbl_Diagnosticos");
            DropForeignKey("dbo.Tbl_Ausencias", "FK_Id_Contingencia", "dbo.Tbl_Contingencias");
            DropForeignKey("dbo.Tbl_Actividades_Criterio", "Fk_Id_Eval_Estandar_Minimo", "dbo.Tbl_Evaluacion_Estandares_Minimos");
            DropForeignKey("dbo.Tbl_Evaluacion_Estandares_Minimos", "Fk_Id_Empresa_Evaluar", "dbo.Tbl_Empresas_Evaluar");
            DropForeignKey("dbo.Tbl_Evaluacion_Inicial_Aspectos", "Fk_Id_Empresa_Aspecto", "dbo.Tbl_Empresa_Aspectos");
            DropForeignKey("dbo.Tbl_Empresa_Aspectos", "Fk_Id_Empresa_Evaluar", "dbo.Tbl_Empresas_Evaluar");
            DropForeignKey("dbo.Tbl_Empresa_Aspectos", "Fk_Id_Aspecto", "dbo.Tbl_Aspectos");
            DropForeignKey("dbo.Tbl_Aspectos", "Fk_Id_Valoracion_Aspecto", "dbo.Tbl_Valoracion_Aspectos");
            DropForeignKey("dbo.Tbl_Empresas_Evaluar", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Evaluacion_Estandares_Minimos", "Fk_Id_Criterio", "dbo.Tbl_Criterios");
            DropForeignKey("dbo.Tbl_Evaluacion_Estandares_Minimos", "Fk_Id_Config_Valoracion_SubEstand", "dbo.Tbl_Config_Valoracion_SubEstandares");
            DropForeignKey("dbo.Tbl_Config_Valoracion_SubEstandares", "Fk_Id_Valoracion_Criterio", "dbo.Tbl_Valoracion_Criterios");
            DropForeignKey("dbo.Tbl_Config_Valoracion_SubEstandares", "Fk_Id_SubEstandar", "dbo.Tbl_SubEstandares");
            DropForeignKey("dbo.Tbl_SubEstandares", "Fk_Id_Estandar", "dbo.Tbl_Estandares");
            DropForeignKey("dbo.Tbl_Estandares", "Fk_Id_Ciclo", "dbo.Tbl_Ciclos");
            DropForeignKey("dbo.Tbl_Criterios", "Fk_Id_SubEstandar", "dbo.Tbl_SubEstandares");
            DropForeignKey("dbo.Tbl_Actividades_Criterio", "Fk_Id_Actividad", "dbo.Tbl_Actividades_Evaluacion");
            DropForeignKey("dbo.Tbl_Prepuesto_Por_Mes", "FK_Presupuesto", "dbo.Tbl_Presupuesto");
            DropForeignKey("dbo.Tbl_Presupuesto_Por_Año", "FK_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Presupuesto_Por_Año", "FK_Presupuesto", "dbo.Tbl_Presupuesto");
            DropForeignKey("dbo.Tbl_Prepuesto_Por_Mes", "FK_Actividad_Presupuesto", "dbo.Tbl_Actividad_Presupuesto");
            DropForeignKey("dbo.Tbl_Actividad_Presupuesto", "FK_Actividad_Presupuesto", "dbo.Tbl_Actividad_Presupuesto");
            DropForeignKey("dbo.Tbl_Seguimiento", "Fk_Id_Accion", "dbo.Tbl_Acciones");
            DropForeignKey("dbo.Tbl_Hallazgo", "Proceso_Pk_Id_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_Hallazgo", "Fk_Id_Accion", "dbo.Tbl_Acciones");
            DropForeignKey("dbo.Tbl_Acciones", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_UsuarioRol", "Fk_Id_Usuario", "dbo.Tbl_Usuario");
            DropForeignKey("dbo.Tbl_Usuario", "Fk_Tipo_Documento", "dbo.Tbl_Tipo_Documento");
            DropForeignKey("dbo.Tbl_Usuario", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_UsuarioRol", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_Rol_Por_Tematica", "Fk_Id_Tematica", "dbo.Tbl_Tematica");
            DropForeignKey("dbo.Tbl_Tematica_Por_Empresa", "Fk_Id_Tematica", "dbo.Tbl_Tematica");
            DropForeignKey("dbo.Tbl_Tematica_Por_Empresa", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Rol_Por_Tematica", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_Responsabilidades_Por_Rol", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_Responsabilidades_Por_Rol", "Fk_Id_Responsabilidades", "dbo.Tbl_Responsabilidades");
            DropForeignKey("dbo.Tbl_Rendicion_Cuenta_Por_Rol", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_Rendicion_Cuenta_Por_Rol", "Fk_Id_RendicionDeCuentas", "dbo.Tbl_RendicionDeCuentas");
            DropForeignKey("dbo.Tbl_Privilegios_Por_Rol", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_Privilegios_Por_Rol", "Fk_Id_Privilegios", "dbo.Tbl_Privilegios");
            DropForeignKey("dbo.Tbl_Rol", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Cargo_Por_Rol", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_Cargo_Por_Rol", "Fk_Id_Cargo", "dbo.Tbl_Cargo");
            DropForeignKey("dbo.Tbl_EmpresaProceso", "Fk_Id_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_Proceso", "Fk_Id_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_Peligro", "FK_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_SedeMunicipio", "Fk_id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_SedeMunicipio", "Fk_Id_Municipio", "dbo.Tbl_Municipio");
            DropForeignKey("dbo.Tbl_Municipio", "Fk_Nombre_Departamento", "dbo.Tbl_Departamento");
            DropForeignKey("dbo.Tbl_RecursoporSede", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_RecursoporSede", "Fk_Id_Recurso", "dbo.Tbl_Recurso");
            DropForeignKey("dbo.Tbl_RecursoTipoRecurso", "Fk_Id_TipoRecurso", "dbo.Tbl_TipoRecurso");
            DropForeignKey("dbo.Tbl_RecursoTipoRecurso", "Fk_Id_Recurso", "dbo.Tbl_Recurso");
            DropForeignKey("dbo.Tbl_RecursoFase", "Fk_Id_Recurso", "dbo.Tbl_Recurso");
            DropForeignKey("dbo.Tbl_RecursoFase", "Fk_Id_Fase", "dbo.Tbl_Fase");
            DropForeignKey("dbo.Tbl_Sede", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Centro_de_Trabajo", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Peligro", "FK_Proceso", "dbo.Tbl_Proceso");
            DropForeignKey("dbo.Tbl_GTC45", "FK_Peligro", "dbo.Tbl_Peligro");
            DropForeignKey("dbo.Tbl_GTC45", "FK_Nivel_De_Exposicion", "dbo.Tbl_Nivel_De_Exposicion");
            DropForeignKey("dbo.Tbl_GTC45", "FK_Nivel_De_Deficiencia", "dbo.Tbl_Nivel_De_Deficiencia");
            DropForeignKey("dbo.Tbl_Consecuencia_Por_Peligro", "FK_Peligro", "dbo.Tbl_Peligro");
            DropForeignKey("dbo.Tbl_Consecuencia_Por_Peligro", "FK_Consecuencia", "dbo.Tbl_Consecuencia");
            DropForeignKey("dbo.Tbl_Consecuencia", "FK_Grupo", "dbo.Tbl_Grupo");
            DropForeignKey("dbo.Tbl_Estimacion_De_Riesgo", "FK_Probabilidad", "dbo.Tbl_Probabilidad");
            DropForeignKey("dbo.Tbl_Estimacion_Riesgo_Por_RAM", "FK_RAM", "dbo.Tbl_RAM");
            DropForeignKey("dbo.Tbl_RAM", "FK_Persona_Expuesta", "dbo.Tbl_Persona_Expuesta_INSHT_RAM");
            DropForeignKey("dbo.Tbl_Probabilidad_Por_PersonaExpuesta", "FK_Probabilidad", "dbo.Tbl_Probabilidad");
            DropForeignKey("dbo.Tbl_Probabilidad", "FK_Metodologia", "dbo.Tbl_Tipo_Metodologia");
            DropForeignKey("dbo.Tbl_Grupo", "FK_Metodologia", "dbo.Tbl_Tipo_Metodologia");
            DropForeignKey("dbo.Tbl_Probabilidad_Por_PersonaExpuesta", "Fk_Persona_Expuesta", "dbo.Tbl_Persona_Expuesta_INSHT_RAM");
            DropForeignKey("dbo.Tbl_Persona_Expuesta_INSHT_RAM", "FK_Peligro", "dbo.Tbl_Peligro");
            DropForeignKey("dbo.Tbl_INSHT", "FK_Persona_Expuesta", "dbo.Tbl_Persona_Expuesta_INSHT_RAM");
            DropForeignKey("dbo.Tbl_Estimacion_Riesgo_Por_RAM", "FK_Estimacion_De_Riesgo", "dbo.Tbl_Estimacion_De_Riesgo");
            DropForeignKey("dbo.Tbl_Estimacion_De_Riesgo", "FK_Consecuencia", "dbo.Tbl_Consecuencia");
            DropForeignKey("dbo.Tbl_Peligro", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropForeignKey("dbo.Tbl_Clasificacion_De_Peligro", "FK_Tipo_De_Peligro", "dbo.Tbl_Tipo_De_Peligro");
            DropForeignKey("dbo.Tbl_EmpresaProceso", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_Organigrama", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_EmpleadoOrg", "Fk_Id_EmpleadoOrg", "dbo.Tbl_EmpleadoOrg");
            DropForeignKey("dbo.Tbl_EmpleadoOrg", "Fk_Id_Organigrama", "dbo.Tbl_Organigrama");
            DropForeignKey("dbo.Tbl_Gobierno", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropForeignKey("dbo.Tbl_ArchivosAccion", "Fk_Id_Accion", "dbo.Tbl_Acciones");
            DropForeignKey("dbo.Tbl_Analisis", "Fk_Id_Accion", "dbo.Tbl_Acciones");
            DropForeignKey("dbo.Tbl_ActividadAccion", "Fk_Id_Accion", "dbo.Tbl_Acciones");
            DropIndex("dbo.Tbl_Requisitos_legales_Otros", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Requisitos_legales_Otros", new[] { "FK_Estado_RequisitoslegalesOtros" });
            DropIndex("dbo.Tbl_Requisitos_legales_Otros", new[] { "FK_Cumplimiento_Evaluacion" });
            DropIndex("dbo.Tbl_Politica", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_ID_Tipo_Cotizante" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_ID_Estado" });
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Tipo_Documento_Empl" });
            DropIndex("dbo.Tbl_PerfilSocioDemografico", new[] { "FK_IDEmpleado" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_ZonaLugar" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_PeligroSede", new[] { "FKIDEmpleado" });
            DropIndex("dbo.Tbl_OtrasInteracciones", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_AntecedentesExposicionLaboral" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_VinculacionLaboral" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FK_Ocupacion_Empl" });
            DropIndex("dbo.Tbl_Informacion_laboral", new[] { "FKIDEmpleado" });
            DropIndex("dbo.Tbl_InconsistenciasLaborales", new[] { "FKTipoInconsistencia" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FKRelacionLaboralTercero" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_EmpresaTercero" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_Ocupacion_Empl" });
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_Tipo_Documento_Empl" });
            DropIndex("dbo.Tbl_EmpleadoPerfilSocioDemografico", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Tipo_Elemento_Analisis", new[] { "FK_Tipo_Analisis" });
            DropIndex("dbo.Tbl_Matriz", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Elemento_Matriz", new[] { "FK_TipoElementoAnalisis" });
            DropIndex("dbo.Tbl_Elemento_Matriz", new[] { "FK_Matriz" });
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Clasificacion_De_Peligro" });
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Diagnostico" });
            DropIndex("dbo.Tbl_Documentacion_Organizacion", new[] { "FK_Empresa" });
            DropIndex("dbo.Tbl_Documentacion_Organizacion", new[] { "FK_TipoModuloOrganizacion" });
            DropIndex("dbo.Tbl_Doc_Dx_Condiciones_De_Salud", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_Ausencias", new[] { "FK_Id_Diagnostico" });
            DropIndex("dbo.Tbl_Ausencias", new[] { "FK_Id_Contingencia" });
            DropIndex("dbo.Tbl_Evaluacion_Inicial_Aspectos", new[] { "Fk_Id_Empresa_Aspecto" });
            DropIndex("dbo.Tbl_Aspectos", new[] { "Fk_Id_Valoracion_Aspecto" });
            DropIndex("dbo.Tbl_Empresa_Aspectos", new[] { "Fk_Id_Aspecto" });
            DropIndex("dbo.Tbl_Empresa_Aspectos", new[] { "Fk_Id_Empresa_Evaluar" });
            DropIndex("dbo.Tbl_Empresas_Evaluar", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_Estandares", new[] { "Fk_Id_Ciclo" });
            DropIndex("dbo.Tbl_Criterios", new[] { "Fk_Id_SubEstandar" });
            DropIndex("dbo.Tbl_SubEstandares", new[] { "Fk_Id_Estandar" });
            DropIndex("dbo.Tbl_Config_Valoracion_SubEstandares", new[] { "Fk_Id_Valoracion_Criterio" });
            DropIndex("dbo.Tbl_Config_Valoracion_SubEstandares", new[] { "Fk_Id_SubEstandar" });
            DropIndex("dbo.Tbl_Evaluacion_Estandares_Minimos", new[] { "Fk_Id_Config_Valoracion_SubEstand" });
            DropIndex("dbo.Tbl_Evaluacion_Estandares_Minimos", new[] { "Fk_Id_Criterio" });
            DropIndex("dbo.Tbl_Evaluacion_Estandares_Minimos", new[] { "Fk_Id_Empresa_Evaluar" });
            DropIndex("dbo.Tbl_Actividades_Criterio", new[] { "Fk_Id_Eval_Estandar_Minimo" });
            DropIndex("dbo.Tbl_Actividades_Criterio", new[] { "Fk_Id_Actividad" });
            DropIndex("dbo.Tbl_Presupuesto_Por_Año", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_Presupuesto_Por_Año", new[] { "FK_Presupuesto" });
            DropIndex("dbo.Tbl_Prepuesto_Por_Mes", new[] { "FK_Actividad_Presupuesto" });
            DropIndex("dbo.Tbl_Prepuesto_Por_Mes", new[] { "FK_Presupuesto" });
            DropIndex("dbo.Tbl_Actividad_Presupuesto", new[] { "FK_Actividad_Presupuesto" });
            DropIndex("dbo.Tbl_Seguimiento", new[] { "Fk_Id_Accion" });
            DropIndex("dbo.Tbl_Hallazgo", new[] { "Proceso_Pk_Id_Proceso" });
            DropIndex("dbo.Tbl_Hallazgo", new[] { "Fk_Id_Accion" });
            DropIndex("dbo.Tbl_Usuario", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_Usuario", new[] { "Fk_Tipo_Documento" });
            DropIndex("dbo.Tbl_UsuarioRol", new[] { "Fk_Id_Usuario" });
            DropIndex("dbo.Tbl_UsuarioRol", new[] { "Fk_Id_Rol" });
            DropIndex("dbo.Tbl_Tematica_Por_Empresa", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_Tematica_Por_Empresa", new[] { "Fk_Id_Tematica" });
            DropIndex("dbo.Tbl_Rol_Por_Tematica", new[] { "Fk_Id_Rol" });
            DropIndex("dbo.Tbl_Rol_Por_Tematica", new[] { "Fk_Id_Tematica" });
            DropIndex("dbo.Tbl_Responsabilidades_Por_Rol", new[] { "Fk_Id_Responsabilidades" });
            DropIndex("dbo.Tbl_Responsabilidades_Por_Rol", new[] { "Fk_Id_Rol" });
            DropIndex("dbo.Tbl_Rendicion_Cuenta_Por_Rol", new[] { "Fk_Id_RendicionDeCuentas" });
            DropIndex("dbo.Tbl_Rendicion_Cuenta_Por_Rol", new[] { "Fk_Id_Rol" });
            DropIndex("dbo.Tbl_Privilegios_Por_Rol", new[] { "Fk_Id_Privilegios" });
            DropIndex("dbo.Tbl_Privilegios_Por_Rol", new[] { "Fk_Id_Rol" });
            DropIndex("dbo.Tbl_Cargo_Por_Rol", new[] { "Fk_Id_Rol" });
            DropIndex("dbo.Tbl_Cargo_Por_Rol", new[] { "Fk_Id_Cargo" });
            DropIndex("dbo.Tbl_Rol", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_Municipio", new[] { "Fk_Nombre_Departamento" });
            DropIndex("dbo.Tbl_SedeMunicipio", new[] { "Fk_Id_Municipio" });
            DropIndex("dbo.Tbl_SedeMunicipio", new[] { "Fk_id_Sede" });
            DropIndex("dbo.Tbl_RecursoTipoRecurso", new[] { "Fk_Id_TipoRecurso" });
            DropIndex("dbo.Tbl_RecursoTipoRecurso", new[] { "Fk_Id_Recurso" });
            DropIndex("dbo.Tbl_RecursoFase", new[] { "Fk_Id_Fase" });
            DropIndex("dbo.Tbl_RecursoFase", new[] { "Fk_Id_Recurso" });
            DropIndex("dbo.Tbl_RecursoporSede", new[] { "Fk_Id_Sede" });
            DropIndex("dbo.Tbl_RecursoporSede", new[] { "Fk_Id_Recurso" });
            DropIndex("dbo.Tbl_Centro_de_Trabajo", new[] { "Fk_Id_Sede" });
            DropIndex("dbo.Tbl_Sede", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_GTC45", new[] { "FK_Nivel_De_Exposicion" });
            DropIndex("dbo.Tbl_GTC45", new[] { "FK_Nivel_De_Deficiencia" });
            DropIndex("dbo.Tbl_GTC45", new[] { "FK_Peligro" });
            DropIndex("dbo.Tbl_Grupo", new[] { "FK_Metodologia" });
            DropIndex("dbo.Tbl_Probabilidad", new[] { "FK_Metodologia" });
            DropIndex("dbo.Tbl_Probabilidad_Por_PersonaExpuesta", new[] { "Fk_Persona_Expuesta" });
            DropIndex("dbo.Tbl_Probabilidad_Por_PersonaExpuesta", new[] { "FK_Probabilidad" });
            DropIndex("dbo.Tbl_INSHT", new[] { "FK_Persona_Expuesta" });
            DropIndex("dbo.Tbl_Persona_Expuesta_INSHT_RAM", new[] { "FK_Peligro" });
            DropIndex("dbo.Tbl_RAM", new[] { "FK_Persona_Expuesta" });
            DropIndex("dbo.Tbl_Estimacion_Riesgo_Por_RAM", new[] { "FK_Estimacion_De_Riesgo" });
            DropIndex("dbo.Tbl_Estimacion_Riesgo_Por_RAM", new[] { "FK_RAM" });
            DropIndex("dbo.Tbl_Estimacion_De_Riesgo", new[] { "FK_Consecuencia" });
            DropIndex("dbo.Tbl_Estimacion_De_Riesgo", new[] { "FK_Probabilidad" });
            DropIndex("dbo.Tbl_Consecuencia", new[] { "FK_Grupo" });
            DropIndex("dbo.Tbl_Consecuencia_Por_Peligro", new[] { "FK_Consecuencia" });
            DropIndex("dbo.Tbl_Consecuencia_Por_Peligro", new[] { "FK_Peligro" });
            DropIndex("dbo.Tbl_Clasificacion_De_Peligro", new[] { "FK_Tipo_De_Peligro" });
            DropIndex("dbo.Tbl_Peligro", new[] { "FK_Proceso" });
            DropIndex("dbo.Tbl_Peligro", new[] { "FK_Sede" });
            DropIndex("dbo.Tbl_Peligro", new[] { "FK_Clasificacion_De_Peligro" });
            DropIndex("dbo.Tbl_Proceso", new[] { "Fk_Id_Proceso" });
            DropIndex("dbo.Tbl_EmpresaProceso", new[] { "Fk_Id_Proceso" });
            DropIndex("dbo.Tbl_EmpresaProceso", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_EmpleadoOrg", new[] { "Fk_Id_Organigrama" });
            DropIndex("dbo.Tbl_EmpleadoOrg", new[] { "Fk_Id_EmpleadoOrg" });
            DropIndex("dbo.Tbl_Organigrama", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_Gobierno", new[] { "Fk_Id_Empresa" });
            DropIndex("dbo.Tbl_ArchivosAccion", new[] { "Fk_Id_Accion" });
            DropIndex("dbo.Tbl_Analisis", new[] { "Fk_Id_Accion" });
            DropIndex("dbo.Tbl_ActividadAccion", new[] { "Fk_Id_Accion" });
            DropIndex("dbo.Tbl_Acciones", new[] { "Fk_Id_Empresa" });
            DropTable("dbo.Tbl_Valoracion_Final");
            DropTable("dbo.Tbl_Sexo");
            DropTable("dbo.Tbl_Requisitos_legales_Otros");
            DropTable("dbo.Tbl_Raza");
            DropTable("dbo.Tbl_Politica");
            DropTable("dbo.Tbl_TipoCotizante");
            DropTable("dbo.Tbl_Empleado");
            DropTable("dbo.Tbl_PerfilSocioDemografico");
            DropTable("dbo.Tbl_ZonaLugar");
            DropTable("dbo.Tbl_SedePeligro");
            DropTable("dbo.Tbl_PeligroSede");
            DropTable("dbo.Tbl_OtrasInteracciones");
            DropTable("dbo.Tbl_Obligaciones_Empleadores");
            DropTable("dbo.Tbl_Obligaciones_Arl");
            DropTable("dbo.Tbl_Interpretacion_Nivel_Riesgo");
            DropTable("dbo.Tbl_Interpretacion_De_Probabilidad");
            DropTable("dbo.Tbl_Ingresos");
            DropTable("dbo.Tbl_VinculacionLaboral");
            DropTable("dbo.Tbl_Informacion_laboral");
            DropTable("dbo.Tbl_TipoInconsistenciaLaboral");
            DropTable("dbo.Tbl_InconsistenciasLaborales");
            DropTable("dbo.Tbl_Hijos");
            DropTable("dbo.Tbl_GradoEscolaridad");
            DropTable("dbo.Tbl_Estrato");
            DropTable("dbo.Tbl_Estado_RequisitoslegalesOtros");
            DropTable("dbo.Tbl_Estado_Empl");
            DropTable("dbo.Tbl_Estado_Civil");
            DropTable("dbo.Tbl_Empresas_Usuarias");
            DropTable("dbo.Tbl_RelacionesLaboralesTercero");
            DropTable("dbo.Tbl_Ocupacion");
            DropTable("dbo.Tbl_EmpresaTercero");
            DropTable("dbo.Tbl_EmpleadoTercero");
            DropTable("dbo.Tbl_EmpleadoPerfilSocioDemografico");
            DropTable("dbo.Tbl_Tipo_Analisis");
            DropTable("dbo.Tbl_Tipo_Elemento_Analisis");
            DropTable("dbo.Tbl_Matriz");
            DropTable("dbo.Tbl_Elemento_Matriz");
            DropTable("dbo.Tbl_Dx_Condiciones_De_Salud");
            DropTable("dbo.Tbl_TipoModulo_Organizacion");
            DropTable("dbo.Tbl_Documentacion_Organizacion");
            DropTable("dbo.Tbl_Doc_Dx_Condiciones_De_Salud");
            DropTable("dbo.Tbl_Dias_Laborables_Empresa");
            DropTable("dbo.Tbl_Dias_Laborables");
            DropTable("dbo.Tbl_Cumplimiento_Evaluacion");
            DropTable("dbo.Tbl_ConfiguracionesHHT");
            DropTable("dbo.Tbl_Competencia");
            DropTable("dbo.Tbl_CIU");
            DropTable("dbo.Tbl_Diagnosticos");
            DropTable("dbo.Tbl_Contingencias");
            DropTable("dbo.Tbl_Ausencias");
            DropTable("dbo.Tbl_AntecedentesExposicionLaboral");
            DropTable("dbo.Tbl_Evaluacion_Inicial_Aspectos");
            DropTable("dbo.Tbl_Valoracion_Aspectos");
            DropTable("dbo.Tbl_Aspectos");
            DropTable("dbo.Tbl_Empresa_Aspectos");
            DropTable("dbo.Tbl_Empresas_Evaluar");
            DropTable("dbo.Tbl_Valoracion_Criterios");
            DropTable("dbo.Tbl_Ciclos");
            DropTable("dbo.Tbl_Estandares");
            DropTable("dbo.Tbl_Criterios");
            DropTable("dbo.Tbl_SubEstandares");
            DropTable("dbo.Tbl_Config_Valoracion_SubEstandares");
            DropTable("dbo.Tbl_Evaluacion_Estandares_Minimos");
            DropTable("dbo.Tbl_Actividades_Evaluacion");
            DropTable("dbo.Tbl_Actividades_Criterio");
            DropTable("dbo.Tbl_Presupuesto_Por_Año");
            DropTable("dbo.Tbl_Presupuesto");
            DropTable("dbo.Tbl_Prepuesto_Por_Mes");
            DropTable("dbo.Tbl_Actividad_Presupuesto");
            DropTable("dbo.Tbl_Seguimiento");
            DropTable("dbo.Tbl_Hallazgo");
            DropTable("dbo.Tbl_Tipo_Documento");
            DropTable("dbo.Tbl_Usuario");
            DropTable("dbo.Tbl_UsuarioRol");
            DropTable("dbo.Tbl_Tematica_Por_Empresa");
            DropTable("dbo.Tbl_Tematica");
            DropTable("dbo.Tbl_Rol_Por_Tematica");
            DropTable("dbo.Tbl_Responsabilidades");
            DropTable("dbo.Tbl_Responsabilidades_Por_Rol");
            DropTable("dbo.Tbl_RendicionDeCuentas");
            DropTable("dbo.Tbl_Rendicion_Cuenta_Por_Rol");
            DropTable("dbo.Tbl_Privilegios");
            DropTable("dbo.Tbl_Privilegios_Por_Rol");
            DropTable("dbo.Tbl_Cargo");
            DropTable("dbo.Tbl_Cargo_Por_Rol");
            DropTable("dbo.Tbl_Rol");
            DropTable("dbo.Tbl_Departamento");
            DropTable("dbo.Tbl_Municipio");
            DropTable("dbo.Tbl_SedeMunicipio");
            DropTable("dbo.Tbl_TipoRecurso");
            DropTable("dbo.Tbl_RecursoTipoRecurso");
            DropTable("dbo.Tbl_Fase");
            DropTable("dbo.Tbl_RecursoFase");
            DropTable("dbo.Tbl_Recurso");
            DropTable("dbo.Tbl_RecursoporSede");
            DropTable("dbo.Tbl_Centro_de_Trabajo");
            DropTable("dbo.Tbl_Sede");
            DropTable("dbo.Tbl_Nivel_De_Exposicion");
            DropTable("dbo.Tbl_Nivel_De_Deficiencia");
            DropTable("dbo.Tbl_GTC45");
            DropTable("dbo.Tbl_Grupo");
            DropTable("dbo.Tbl_Tipo_Metodologia");
            DropTable("dbo.Tbl_Probabilidad");
            DropTable("dbo.Tbl_Probabilidad_Por_PersonaExpuesta");
            DropTable("dbo.Tbl_INSHT");
            DropTable("dbo.Tbl_Persona_Expuesta_INSHT_RAM");
            DropTable("dbo.Tbl_RAM");
            DropTable("dbo.Tbl_Estimacion_Riesgo_Por_RAM");
            DropTable("dbo.Tbl_Estimacion_De_Riesgo");
            DropTable("dbo.Tbl_Consecuencia");
            DropTable("dbo.Tbl_Consecuencia_Por_Peligro");
            DropTable("dbo.Tbl_Tipo_De_Peligro");
            DropTable("dbo.Tbl_Clasificacion_De_Peligro");
            DropTable("dbo.Tbl_Peligro");
            DropTable("dbo.Tbl_Proceso");
            DropTable("dbo.Tbl_EmpresaProceso");
            DropTable("dbo.Tbl_EmpleadoOrg");
            DropTable("dbo.Tbl_Organigrama");
            DropTable("dbo.Tbl_Gobierno");
            DropTable("dbo.Tbl_Empresa");
            DropTable("dbo.Tbl_ArchivosAccion");
            DropTable("dbo.Tbl_Analisis");
            DropTable("dbo.Tbl_ActividadAccion");
            DropTable("dbo.Tbl_Acciones");
        }
    }
}
