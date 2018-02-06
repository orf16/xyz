CREATE VIEW [dbo].[V_Gestiondelcambio]
AS
SELECT        dbo.Tbl_GestionDelCambio.Fecha, dbo.Tbl_GestionDelCambio.DescripcionDeCambio, dbo.Tbl_GestionDelCambio.FechaEjecucion, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS Peligro, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS Clase_De_Peligro, dbo.Tbl_GestionDelCambio.Recomendaciones, dbo.Tbl_GestionDelCambio.FechaSeguimiento, 
                         dbo.Tbl_Rol.Descripcion AS Comunicado_a, dbo.Tbl_GestionDelCambio.RequisitoLegal, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_GestionDelCambio INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_GestionDelCambio.FK_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Rol ON dbo.Tbl_GestionDelCambio.FK_Id_Rol = dbo.Tbl_Rol.Pk_Id_Rol AND dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Rol.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro
GO



