
CREATE VIEW [dbo].[v_Condiciones_Salud]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Dx_Condiciones_De_Salud.Lugar AS ZONA_LUGAR, dbo.Tbl_Dx_Condiciones_De_Salud.vigencia, 
                         dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx AS FECHA_INICIAL, MONTH(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS MES, 
                         YEAR(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS AÑO, dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Final_Dx AS FECHA_FINAL, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS CLASIFICACIÓN_PELIGRO, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar AS [TOTAL_TRABAJADORES/ZONA-LUGAR], dbo.Tbl_Sintomatologia_Dx.Sintomatologia, 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia AS NUMERO_TRABAJADORES_SINTOMATOLOGIA, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia ELSE '0' END AS ANORMALIDAD,
                          dbo.Tbl_Pruebas_Clinica_Dx.Prueba_Clinica, dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba AS TRABAJADORES_PRUEBA, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba ELSE '0' END AS ANORMALIDAD_PRUEBA,
                          dbo.Tbl_Pruebas_P_Clinica_Dx.Prueba_P_Clinica, dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P AS TRABAJADORES_PRUEBA_P, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P ELSE '0' END AS ANORMALIDAD_PRUEBA_P,
                          dbo.Tbl_Diagnosticos.Descripcion AS DIAGNOSTICO_CIE10a, dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico AS TRABAJADORES_DIAGNOSTICO, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico ELSE '0' END AS
                          ANORMALIDAD_DIAGNOSTICO, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Sede INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Dx_Condiciones_De_Salud.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud AS Tbl_Dx_Condiciones_De_Salud_1 ON dbo.Tbl_Sede.Pk_Id_Sede = Tbl_Dx_Condiciones_De_Salud_1.FK_Sede AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = Tbl_Dx_Condiciones_De_Salud_1.FK_Proceso INNER JOIN
                         dbo.Tbl_Pruebas_Clinica_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Pruebas_P_Clinica_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Sintomatologia_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Clasificacion_Peligro_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Clasificacion_Peligro_Dx.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Diagnostico_Cie10_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Diagnosticos ON dbo.Tbl_Diagnostico_Cie10_Dx.FK_Diagnostico = dbo.Tbl_Diagnosticos.PK_Id_Diagnostico LEFT OUTER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso
GO




