

CREATE VIEW [dbo].[V_sede]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Sede.Pk_Id_Sede
FROM            dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_SedeMunicipio ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_SedeMunicipio.Fk_id_Sede
GROUP BY dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Pk_Id_Sede

