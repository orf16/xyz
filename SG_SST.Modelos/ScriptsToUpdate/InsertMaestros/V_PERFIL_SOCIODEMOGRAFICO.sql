
CREATE VIEW [dbo].[V_PERFIL_SOCIODEMOGRAFICO]
AS
SELECT        dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_PerfilSocioDemograficoPlanificacion.ZonaLugar AS [ZONA/LUGAR], dbo.Tbl_UsuarioSistema.Nombres, dbo.Tbl_UsuarioSistema.Apellidos, 
                         dbo.Tbl_UsuarioSistema.Documento AS [NÚMERO DOCUMENTO], dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.GradoEscolaridad AS [GRADO DE ESCOLARIDAD], dbo.Tbl_PerfilSocioDemograficoPlanificacion.Ingresos, 
                         dbo.Tbl_Municipio.Nombre_Municipio AS [MUNICIPIO DE RESIDENCIA], dbo.Tbl_PerfilSocioDemograficoPlanificacion.Conyuge AS CONYUGUE, dbo.Tbl_PerfilSocioDemograficoPlanificacion.Hijos, 
                         dbo.Tbl_Estrato.Descripcion_Estrato AS [ESTRATO SOCIOECONÓMICO
ESTRATO SOCIOECONOMICO], dbo.Tbl_Estado_Civil.Descripcion_EstadoCivil AS [ESTADO CIVIL], dbo.Tbl_Etnia.Descripcion_Etnia AS ETNIA,
                          dbo.Tbl_PerfilSocioDemograficoPlanificacion.Sexo, dbo.Tbl_VinculacionLaboral.Descripcion_VinculacionLaboral AS [VINCULACIÓN LABORAL], 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.TurnoTrabajo AS [TURNO DE TRABAJO], 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.caracteristicasFisicas AS CARACTERISTICAS_FISICAS_DESEMPEÑO_DE_CARGO, 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.caracteristicasPsicologicas AS CARACTERISTICAS_PSICOLOGICAS, 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.evaluacionesMedicasRequeridas AS [EVALUACIÓN_MEDICA_REQUERIDA_PARA EL_DESEMPEÑO_DEL_CARGO], 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS PELIGRO, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS TIPO_DE_PELIGRO, dbo.Tbl_Peligro.Otro AS PELIGRO_OTRO, 
                         dbo.Tbl_Condiciones_Riesgo_Perfil.tiempoExposicion AS [TIEMPO_EXPOSICIÓN(MESES)], dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_PerfilSocioDemograficoPlanificacion INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.Fk_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Etnia ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Etnia = dbo.Tbl_Etnia.PK_Etnia INNER JOIN
                         dbo.Tbl_Estrato ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Estrato = dbo.Tbl_Estrato.PK_Estrato INNER JOIN
                         dbo.Tbl_Estado_Civil ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Estado_Civil = dbo.Tbl_Estado_Civil.PK_Estado_Civil INNER JOIN
                         dbo.Tbl_VinculacionLaboral ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_VinculacionLaboral = dbo.Tbl_VinculacionLaboral.PK_VinculacionLaboral INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Municipio ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Ciudad = dbo.Tbl_Municipio.Pk_Id_Municipio INNER JOIN
                         dbo.Tbl_Departamento ON dbo.Tbl_Municipio.Fk_Nombre_Departamento = dbo.Tbl_Departamento.Pk_Id_Departamento INNER JOIN
                         dbo.Tbl_UsuarioSistemaEmpresa ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_UsuarioSistemaEmpresa.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_UsuarioSistema ON dbo.Tbl_UsuarioSistemaEmpresa.Fk_Id_UsuarioSistema = dbo.Tbl_UsuarioSistema.Pk_Id_UsuarioSistema LEFT OUTER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede AND dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Ocupaciones_De_Perfil ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.Ocupaciones_Perfil_PK_OcupacionPerfil = dbo.Tbl_Ocupaciones_De_Perfil.PK_OcupacionPerfil LEFT OUTER JOIN
                         dbo.Tbl_Condiciones_Riesgo_Perfil ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.IDEmpleado_PerfilSocioDemoGrafico = dbo.Tbl_Condiciones_Riesgo_Perfil.FK_PerfilSocioDemografico
GO

