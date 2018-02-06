
CREATE VIEW [dbo].[V_competencia]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social AS EMPRESA, dbo.Tbl_Rol.Descripcion AS ROL, dbo.Tbl_Cargo.Nombre_Cargo AS CARGO, dbo.Tbl_Tematica.Tematicas AS COMPETENCIA, 
                         COUNT(dbo.Tbl_UsuarioRol.Pk_Id_UsuarioRol) AS NUMERO_DE_PERSONAS, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Cargo_Por_Rol INNER JOIN
                         dbo.Tbl_Cargo INNER JOIN
                         dbo.Tbl_Cargo_Por_Rol AS Tbl_Cargo_Por_Rol_1 ON dbo.Tbl_Cargo.Pk_Id_Cargo = Tbl_Cargo_Por_Rol_1.Fk_Id_Cargo INNER JOIN
                         dbo.Tbl_Rol ON Tbl_Cargo_Por_Rol_1.Fk_Id_Rol = dbo.Tbl_Rol.Pk_Id_Rol INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Rol.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa ON dbo.Tbl_Cargo_Por_Rol.Fk_Id_Cargo = dbo.Tbl_Cargo.Pk_Id_Cargo AND 
                         dbo.Tbl_Cargo_Por_Rol.Fk_Id_Rol = dbo.Tbl_Rol.Pk_Id_Rol INNER JOIN
                         dbo.Tbl_Rol_Por_Tematica ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_Rol_Por_Tematica.Fk_Id_Rol INNER JOIN
                         dbo.Tbl_Tematica ON dbo.Tbl_Rol_Por_Tematica.Fk_Id_Tematica = dbo.Tbl_Tematica.Id_Tematica INNER JOIN
                         dbo.Tbl_UsuarioRol ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_UsuarioRol.Fk_Id_Rol
GROUP BY dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Rol.Descripcion, dbo.Tbl_Cargo.Nombre_Cargo, dbo.Tbl_Tematica.Tematicas, dbo.Tbl_Empresa.Nit_Empresa
GO


