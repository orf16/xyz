CREATE VIEW [dbo].[V_GTC45_MATRIZ]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa AS NIT, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, 
                         dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, Tbl_Proceso_1.Descripcion_Proceso AS SUBPROCESO, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS PELIGRO_CLASIFICACIÓN, 
                         dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO_DESCRIPCIÓN, 
                         dbo.Tbl_Interpretacion_Nivel_Riesgo.Resultado AS INTERPRETACIÓN_DEL_NIVEL_DE_RIESGO, 
                         CASE WHEN RiesgoNoAceptable = 1 THEN 'Aceptable' ELSE 'No_aceptable' END AS ACEPTABILIDAD_DEL_RIESGO, 
                         dbo.Tbl_GTC45.Numero_De_Expuestos AS NUMERO_DE_EXPUESTOS_TOTAL, dbo.Tbl_Peligro.Fecha_De_Evaluacion AS FECHA, 
                         MONTH(dbo.Tbl_Peligro.Fecha_De_Evaluacion) AS MES, YEAR(dbo.Tbl_Peligro.Fecha_De_Evaluacion) AS AÑO
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Peligro.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Consecuencia_Por_Peligro ON dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Consecuencia_Por_Peligro.FK_Peligro INNER JOIN
                         dbo.Tbl_Consecuencia ON dbo.Tbl_Consecuencia_Por_Peligro.FK_Consecuencia = dbo.Tbl_Consecuencia.PK_Consecuencia INNER JOIN
                         dbo.Tbl_Grupo ON dbo.Tbl_Consecuencia.FK_Grupo = dbo.Tbl_Grupo.PK_Grupo INNER JOIN
                         dbo.Tbl_GTC45 ON dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_GTC45.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Estimacion_De_Riesgo ON dbo.Tbl_Consecuencia.PK_Consecuencia = dbo.Tbl_Estimacion_De_Riesgo.FK_Consecuencia LEFT OUTER JOIN
                         dbo.Tbl_Interpretacion_Nivel_Riesgo ON dbo.Tbl_GTC45.Nivel_De_Riesgo >= dbo.Tbl_Interpretacion_Nivel_Riesgo.Nivel_Inferior AND 
                         dbo.Tbl_GTC45.Nivel_De_Riesgo <= dbo.Tbl_Interpretacion_Nivel_Riesgo.Nivel_Superior LEFT OUTER JOIN
                         dbo.Tbl_Nivel_De_Deficiencia ON dbo.Tbl_GTC45.FK_Nivel_De_Deficiencia = dbo.Tbl_Nivel_De_Deficiencia.PK_Nivel_De_Deficiencia LEFT OUTER JOIN
                         dbo.Tbl_Nivel_De_Exposicion ON dbo.Tbl_GTC45.FK_Nivel_De_Exposicion = dbo.Tbl_Nivel_De_Exposicion.PK_Nivel_De_Exposicion
GROUP BY dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Nivel_De_Exposicion.Descripcion_Exposicion, dbo.Tbl_Nivel_De_Deficiencia.Descripcion_Deficiciencia, 
                         dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Proceso.Descripcion_Proceso, Tbl_Proceso_1.Descripcion_Proceso, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_GTC45.Numero_De_Expuestos, 
                         dbo.Tbl_Interpretacion_Nivel_Riesgo.Resultado, dbo.Tbl_Estimacion_De_Riesgo.RiesgoNoAceptable, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Peligro.Fecha_De_Evaluacion

GO