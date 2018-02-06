
CREATE VIEW [dbo].[V_INSHT_MATRIIZ]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social AS razonsoc, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, Tbl_Proceso_1.Descripcion_Proceso AS SUBPROCESO, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS PELIGRO_CLASIFICACION, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO_DESCRIPCIÓN, 
                         COUNT(dbo.Tbl_Persona_Expuesta_INSHT_RAM.Total) AS NUMERO_DE_EXPUESTOS_TOTAL, dbo.Tbl_INSHT.Estimacion_Riesgo AS ESTIMACIÓN_DEL_RIESGO, 
                         CASE WHEN dbo.Tbl_INSHT.Riesgo_Controlado = 'true' THEN 'Si' ELSE 'No' END AS RIESGO_CONTROLADO, dbo.Tbl_Peligro.Fecha_De_Evaluacion AS FECHA, MONTH(dbo.Tbl_Peligro.Fecha_De_Evaluacion) 
                         AS MES, YEAR(dbo.Tbl_Peligro.Fecha_De_Evaluacion) AS AÑO, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Peligro INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Peligro.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Peligro.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_INSHT INNER JOIN
                         dbo.Tbl_Persona_Expuesta_INSHT_RAM ON dbo.Tbl_INSHT.FK_Persona_Expuesta = dbo.Tbl_Persona_Expuesta_INSHT_RAM.PK_Persona_Expuesta ON 
                         dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Persona_Expuesta_INSHT_RAM.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Probabilidad INNER JOIN
                         dbo.Tbl_Consecuencia_Por_Peligro INNER JOIN
                         dbo.Tbl_Tipo_Metodologia INNER JOIN
                         dbo.Tbl_Consecuencia INNER JOIN
                         dbo.Tbl_Grupo ON dbo.Tbl_Consecuencia.FK_Grupo = dbo.Tbl_Grupo.PK_Grupo ON dbo.Tbl_Tipo_Metodologia.PK_Metodologia = dbo.Tbl_Grupo.FK_Metodologia ON 
                         dbo.Tbl_Consecuencia_Por_Peligro.FK_Consecuencia = dbo.Tbl_Consecuencia.PK_Consecuencia ON dbo.Tbl_Probabilidad.FK_Metodologia = dbo.Tbl_Tipo_Metodologia.PK_Metodologia ON 
                         dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Consecuencia_Por_Peligro.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso LEFT OUTER JOIN
                         dbo.Tbl_Tipo_De_Peligro AS Tbl_Tipo_De_Peligro_1 ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = Tbl_Tipo_De_Peligro_1.PK_Tipo_De_Peligro
GROUP BY dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_INSHT.Estimacion_Riesgo, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Empresa.Razon_Social, Tbl_Proceso_1.Descripcion_Proceso, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_INSHT.Riesgo_Controlado, dbo.Tbl_Peligro.Fecha_De_Evaluacion, 
                         MONTH(dbo.Tbl_Peligro.Fecha_De_Evaluacion), YEAR(dbo.Tbl_Peligro.Fecha_De_Evaluacion), CASE WHEN dbo.Tbl_INSHT.Riesgo_Controlado = 'true' THEN 'Si' ELSE 'No' END, dbo.Tbl_Empresa.Nit_Empresa
GO

