

CREATE VIEW [dbo].[V_reporteactosycondinseguros]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa AS NIT_EMPRESA, dbo.Tbl_Empresa.Razon_Social AS RAZON_SOCIAL, dbo.Tbl_Sede.Nombre_Sede AS SEDE, 
                         dbo.Tbl_Reportes.Fecha_Ocurrencia AS FECHA, YEAR(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS AÑO, MONTH(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS MES, 
                         dbo.Tbl_Reportes.Area_Lugar AS [AREA/LUGAR], dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, dbo.Tbl_Reportes.Causa_Reporte AS CAUSA, 
                         dbo.Tbl_Reportes.Sugerencias_Reporte AS SUGERENCIA
FROM            dbo.Tbl_ActividadesActosInseguros INNER JOIN
                         dbo.Tbl_Reportes ON dbo.Tbl_ActividadesActosInseguros.FK_Id_Reportes = dbo.Tbl_Reportes.Pk_Id_Reportes INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Reportes.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tipo_Reporte ON dbo.Tbl_Reportes.FK_Tipo_Reporte = dbo.Tbl_Tipo_Reporte.Pk_Id_Tipo_Reporte INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Reportes.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso

