

CREATE VIEW [dbo].[V_ACCIONESCORRECTIVASPREVENTIVAS]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Acciones.Fecha_dil AS [Fecha de Diligenciamiento], 
                         dbo.Tbl_Acciones.Fecha_hall AS [Fecha de Hallazgo], dbo.Tbl_Acciones.Origen AS [Origen del Hallazgo], 
                         dbo.Tbl_Acciones.Otro_Origen AS [Otro Origen del Hallazgo], dbo.Tbl_Acciones.Estado, dbo.Tbl_Acciones.Halla_Nombre, dbo.Tbl_ActividadAccion.Actividad, 
                         dbo.Tbl_Acciones.Tipo, dbo.Tbl_Acciones.Halla_TipoDoc, dbo.Tbl_Acciones.Halla_Cargo, dbo.Tbl_Analisis.ValorTxt AS [Valor Análisis], 
                         dbo.Tbl_Hallazgo.Halla_Proceso AS [Hallazgo Proceso], dbo.Tbl_Hallazgo.Halla_Norma AS [Hallazgo Norma], dbo.Tbl_Hallazgo.Halla_Numeral, 
                         dbo.Tbl_Acciones.Verificacion, dbo.Tbl_Acciones.Correccion, dbo.Tbl_Acciones.Cambio_Doc AS [Cambios en la documentación del SIG], 
                         dbo.Tbl_Acciones.Des_Cambio_Doc AS [Descripción Cambios en la documentación del SIG], dbo.Tbl_Seguimiento.Fecha_Seg, dbo.Tbl_Seguimiento.Observaciones, 
                         dbo.Tbl_Acciones.Eficacia, dbo.Tbl_Acciones.Nombre_Auditor, dbo.Tbl_Acciones.Cargo_Auditor, dbo.Tbl_Acciones.NombreArchivoAuditor, 
                         dbo.Tbl_Acciones.RutaArchivoAuditor
FROM            dbo.Tbl_Hallazgo RIGHT OUTER JOIN
                         dbo.Tbl_Analisis INNER JOIN
                         dbo.Tbl_Acciones INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Acciones.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_ActividadAccion ON dbo.Tbl_Acciones.Pk_Id_Accion = dbo.Tbl_ActividadAccion.Fk_Id_Accion INNER JOIN
                         dbo.Tbl_Seguimiento ON dbo.Tbl_Acciones.Pk_Id_Accion = dbo.Tbl_Seguimiento.Fk_Id_Accion ON 
                         dbo.Tbl_Analisis.Fk_Id_Accion = dbo.Tbl_Acciones.Pk_Id_Accion INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa AND dbo.Tbl_Acciones.Halla_Sede = dbo.Tbl_Sede.Pk_Id_Sede ON 
                         dbo.Tbl_Hallazgo.Fk_Id_Accion = dbo.Tbl_Acciones.Pk_Id_Accion

