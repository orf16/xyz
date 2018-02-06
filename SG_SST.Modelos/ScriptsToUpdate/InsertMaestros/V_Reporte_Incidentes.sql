
CREATE VIEW [dbo].[V_Reporte_Incidentes]
AS
SELECT        dbo.V_sede.Razon_Social, dbo.V_sede.SEDE AS Nombre_Sede, dbo.Tbl_Incidentes.Incidente_fecha, YEAR(dbo.Tbl_Incidentes.Incidente_fecha) AS Año, MONTH(dbo.Tbl_Incidentes.Incidente_fecha) AS Mes, 
                         dbo.Tbl_Tipo_Incidente.Nombre_Incidente AS Tipo_Incidente, CASE WHEN Tbl_ZonaLugar.Descripcion_ZonaLugar = 'U' THEN 'Urbano' ELSE 'Rural' END AS Zona, dbo.Tbl_Sitio_Incidente.Nombre_Sitio, 
                         CASE WHEN Tbl_Incidentes.Incidente_ocurre_dentro_empresa = 'true' THEN 'Dentro de la empresa' ELSE Tbl_Incidentes.Incidente_sitio_incidente_otro END AS [Lugar donde ocurrió el incidente], 
                         dbo.Tbl_Incidentes.Incidente_descripcion, dbo.Tbl_Incidente_Consecuencia.Nombre_consecuencia, dbo.V_sede.Nit_Empresa
FROM            dbo.Tbl_Incidentes INNER JOIN
                         dbo.Tbl_Tipo_Incidente ON dbo.Tbl_Incidentes.FK_id_incidente_tipo_incidente = dbo.Tbl_Tipo_Incidente.Pk_Id_Tipo_Incidente INNER JOIN
                         dbo.Tbl_ZonaLugar ON dbo.Tbl_Incidentes.FK_id_zonalugar_incidente = dbo.Tbl_ZonaLugar.PK_ZonaLugar INNER JOIN
                         dbo.Tbl_Sitio_Incidente ON dbo.Tbl_Incidentes.FK_id_sitio_incidente = dbo.Tbl_Sitio_Incidente.Pk_Id_Sitio_Incidente INNER JOIN
                         dbo.V_sede ON dbo.Tbl_Incidentes.FK_id_sede_general = dbo.V_sede.Pk_Id_Sede LEFT OUTER JOIN
                         dbo.Tbl_Incidente_Consecuencia ON dbo.Tbl_Incidentes.FK_id_consecuencia_incidente = dbo.Tbl_Incidente_Consecuencia.Pk_Id_Incidente_Consecuencia
GROUP BY dbo.Tbl_Tipo_Incidente.Nombre_Incidente, dbo.Tbl_Incidentes.Incidente_fecha, dbo.Tbl_Incidentes.FK_id_zonalugar_incidente, dbo.Tbl_ZonaLugar.Descripcion_ZonaLugar, 
                         dbo.Tbl_Incidentes.Incidente_sitio_incidente_otro, dbo.Tbl_Incidentes.Incidente_descripcion, dbo.Tbl_Incidentes.Incidente_ocurre_dentro_empresa, dbo.Tbl_Sitio_Incidente.Nombre_Sitio, 
                         dbo.Tbl_Incidente_Consecuencia.Nombre_consecuencia, dbo.V_sede.SEDE, dbo.V_sede.Razon_Social, dbo.V_sede.Nit_Empresa
GO

