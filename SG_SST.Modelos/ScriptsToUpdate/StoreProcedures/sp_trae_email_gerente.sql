USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAE_EMAIL_GERENTE]    Script Date: 13/08/2017 3:31:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_TRAE_EMAIL_GERENTE]
 @razonSocialnit varchar(50) = null
AS
BEGIN
  SELECT gp.CORREO as Id_Tipo , gp.Nombres as Descripcion  FROM dbo.tbl_empresa em, dbo.Tbl_Sede se , dbo.Tbl_SedeMunicipio sem,
  dbo.Tbl_Departamento dep, dbo.Tbl_Municipio mun , dbo.Tbl_Gerentes_Positiva gp
  where em.Pk_Id_Empresa = se.Fk_Id_Empresa 
  and se.Pk_Id_Sede = sem.Fk_id_Sede
  and em.Nit_Empresa = @razonSocialnit
  and se.Pk_Id_Sede = (select min (sea.Pk_Id_Sede) from dbo.Tbl_Sede sea
						where sea.Fk_Id_Empresa = se.Fk_Id_Empresa)
  and dep.Pk_Id_Departamento = mun.Fk_Nombre_Departamento
  AND sem.Fk_Id_Municipio = MUN.Pk_Id_Municipio
  and dep.Nombre_Departamento = gp.DEPARTAMENTO
  ORDER BY se.Pk_Id_Sede asc
END

