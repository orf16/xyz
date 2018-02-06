

CREATE VIEW [dbo].[V_Inspecciones]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Planeacion_Inspeccion.Fecha AS [FECHA PLANEACIÓN], 
                         dbo.Tbl_Inspecciones.Fecha_Realizacion AS [FECHA REALIZACIÓN], dbo.Tbl_Inspecciones.Area_Lugar, 
                         dbo.Tbl_Maestro_Planeación_Inspeccion.Descripcion_Tipo_Inspeccion AS [TIPO INSPECCIÓN], dbo.Tbl_Inspecciones.Descripcion_Tipo_Inspeccion AS Descripción, 
                         CASE WHEN dbo.Tbl_Inspecciones.Estado_Inspeccion = 1 THEN 'Ejecutada' ELSE 'No_Ejecutada' END AS Estado_Inspección, 
                         CASE WHEN dbo.Tbl_CondicionInsegura.Estado_Condicion = 1 THEN 'Ejecutada' ELSE 'No_Ejecutada' END AS Estado_condición, 
                         dbo.Tbl_Inspecciones.Responsable_Lugar AS Responsable_Planeación, dbo.Tbl_CondicionInsegura.Descripcion_Condicion, 
                         dbo.Tbl_CondicionInsegura.DescripcionRiesgoIdentificado
FROM            dbo.Tbl_Asistentes INNER JOIN
                         dbo.Tbl_AsistentesporInspeccion ON dbo.Tbl_Asistentes.Pk_Id_Asistente = dbo.Tbl_AsistentesporInspeccion.Fk_Id_Asistente INNER JOIN
                         dbo.Tbl_CondicionInsegura INNER JOIN
                         dbo.Tbl_CondicionesInsegurasporInspeccion ON 
                         dbo.Tbl_CondicionInsegura.Pk_Id_CondicionInsegura = dbo.Tbl_CondicionesInsegurasporInspeccion.Fk_Id_CondicionInsegura INNER JOIN
                         dbo.Tbl_ConfiguracionporInspeccion INNER JOIN
                         dbo.Tbl_Configuracion_Inspeccion ON 
                         dbo.Tbl_ConfiguracionporInspeccion.Fk_Id_ConfiguracionInspeccion = dbo.Tbl_Configuracion_Inspeccion.Pk_Id_ConfiguracionInspeccion INNER JOIN
                         dbo.Tbl_Inspecciones ON dbo.Tbl_ConfiguracionporInspeccion.Fk_Id_Inspecciones = dbo.Tbl_Inspecciones.Pk_Id_Inspecciones ON 
                         dbo.Tbl_CondicionesInsegurasporInspeccion.Fk_Id_Inspecciones = dbo.Tbl_Inspecciones.Pk_Id_Inspecciones ON 
                         dbo.Tbl_AsistentesporInspeccion.Fk_Id_Inspeccion = dbo.Tbl_Inspecciones.Pk_Id_Inspecciones INNER JOIN
                         dbo.Tbl_Planeacion_Inspeccion ON dbo.Tbl_Inspecciones.Fk_Id_PlaneacionInspeccion = dbo.Tbl_Planeacion_Inspeccion.Pk_Id_PlaneacionInspeccion INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Inspecciones.Fk_Id_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Maestro_Planeación_Inspeccion ON 
                         dbo.Tbl_Planeacion_Inspeccion.Fk_Id_Maestro_Tipo_Inspeccion = dbo.Tbl_Maestro_Planeación_Inspeccion.Pk_Id_Maestro_Tipo_Inspeccion

					
