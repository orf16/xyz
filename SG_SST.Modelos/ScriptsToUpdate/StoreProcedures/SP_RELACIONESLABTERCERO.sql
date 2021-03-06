
ALTER PROCEDURE [dbo].[SP_RELACIONESLABTERCERO_LISTAR]
 @razonSocialnit varchar(50) = null,
 @TipoTercero varchar(50) = null,
 @PageIndex int=1,
 @PageSize int=10,
 @PageCount int=0 out,
 @Empresa varchar(20)
AS
BEGIN
SET @PageCount = (SELECT COUNT(1) FROM Tbl_EmpleadoTercero ETT , Tbl_EmpresaTercero tet 
where ETT.FK_EmpresaTercero = tet.PK_Nit_Empresa
and ETT.fk_empresa = (select tem.Pk_Id_Empresa from  Tbl_Empresa tem  where tem.Nit_Empresa = @Empresa )) 

SELECT @PageCount = CASE WHEN (@PageCount%@PageSize=0) THEN @PageCount/@PageSize ELSE @PageCount/@PageSize +1 END 
SELECT et.ID_Empleado,et.FK_Tipo_Documento_Empl,
td.Descripcion as TipoDocumento ,et.Numero_Documento_Empl,et.Nombre1 , et.Nombre2 ,
et.Apellido1 , et.Apellido2, et.FechaNacimiento , et.Email,et.Ocupacion_Empl,et.Cargo_Empl,
et.Email_Empl, et.PK_Nit_Empresa, et.FKRelacionLaboralTercero , 
tt.Descripcion_Tipo_Tercero as RelacionesLaboralesTercero , et.Razon_Social as RazonSocial FROM
	(SELECT ROW_NUMBER() OVER(ORDER BY ETT.ID_EMPLEADO)RowID, * 
FROM Tbl_EmpleadoTercero ETT , Tbl_EmpresaTercero tet 
where ETT.FK_EmpresaTercero = tet.PK_Nit_Empresa and (tet.PK_Nit_Empresa = null or tet.PK_Nit_Empresa = @razonSocialnit)
and ETT.fk_empresa = (select tem.Pk_Id_Empresa from  Tbl_Empresa tem  where tem.Nit_Empresa = @Empresa )) 
et  
right join Tbl_Tipo_Documento td on et.FK_Tipo_Documento_Empl = td.PK_IDTipo_Documento
right join (select * from Tbl_TipoTercero tt where (@TipoTercero is null or tt.Descripcion_Tipo_Tercero=@TipoTercero))tt on  et.FKRelacionLaboralTercero = tt.Pk_Id_TipoTercero
WHERE RowID BETWEEN (@PageSize * (@PageIndex-1) + 1) AND (@PageIndex * @PageSize)
END

