

CREATE VIEW [dbo].[V_ACCIONESCORRECTIVASPREVENTIVAS]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, 
dbo.Tbl_Acciones.Fecha_dil as Fecha_Diligenciamiento, dbo.Tbl_Acciones.Fecha_hall as Fecha_Hallazgo, dbo.Tbl_Acciones.Origen, 
                         dbo.Tbl_Acciones.Otro_Origen, dbo.Tbl_Acciones.Estado, dbo.Tbl_Acciones.Halla_Nombre  as Hallazgo, dbo.Tbl_ActividadAccion.Actividad, dbo.Tbl_Acciones.Tipo, 
                         dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Acciones INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Acciones.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_ActividadAccion ON dbo.Tbl_Acciones.Pk_Id_Accion = dbo.Tbl_ActividadAccion.Fk_Id_Accion

