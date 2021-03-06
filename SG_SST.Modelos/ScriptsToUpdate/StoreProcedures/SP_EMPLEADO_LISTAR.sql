USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[SP_EMPLEADO_LISTAR]    Script Date: 6/06/2017 9:27:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_EMPLEADO_LISTAR]
 @Estado varchar(50) = null,
 @TipoCotizante varchar(50) = null,
 @PageIndex int=1,
 @PageSize int=10,
 @PageCount int=0 out,
 @Empresa varchar(20)
AS
BEGIN
SET @PageCount = (SELECT COUNT(1) FROM Tbl_Empleado ET where ET.FK_Empresa = (select te.Pk_Id_Empresa  from Tbl_Empresa te where te.Nit_Empresa = @Empresa))
SELECT @PageCount = CASE WHEN (@PageCount%@PageSize=0) THEN @PageCount/@PageSize ELSE @PageCount/@PageSize +1 END 
SELECT
td.Descripcion as TipoDocumento ,cast (PK_Numero_Documento_Empl as varchar) as NumeroDocumento,et.Apellido1 , et.Apellido2,
et.Nombre1 , et.Nombre2 , Getdate() as FechaNacimiento, tee.EstEmplead,'OCUPACION' OCUPACION,'CARGO' CARGO, 'EMAIL' EMAIL,
tt.des_tipocoti AS TIPO_COTIZANTE FROM
	(SELECT ROW_NUMBER() OVER(ORDER BY ETT.ID_EMPLEADO)RowID, * 
FROM Tbl_Empleado ETT 
join Tbl_Empresa te on ett.FK_Empresa = te.Pk_Id_Empresa
where te.Nit_Empresa = @Empresa) et  
right join Tbl_Tipo_Documento td on et.FK_Tipo_Documento_Empl = td.PK_IDTipo_Documento
join (select tee.PK_IDEmpleadoEst,tee.EstEmplead from Tbl_Estado_Empl tee where (@Estado is null or @Estado = tee.EstEmplead)) tee on et.FK_ID_Estado = tee.PK_IDEmpleadoEst
right join (SELECT tt.Descripcion des_tipocoti , tt.Pk_Id_Cotizante from  Tbl_TipoCotizante tt where (@TipoCotizante is null or @TipoCotizante = tt.Descripcion)) tt on  et.FK_ID_Tipo_Cotizante = tt.Pk_Id_Cotizante
WHERE RowID BETWEEN (@PageSize * (@PageIndex-1) + 1) AND (@PageIndex * @PageSize)
END
