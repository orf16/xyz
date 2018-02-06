
CREATE VIEW [dbo].[VistaRelacionesLaborales]
AS
SELECT        dbo.Tbl_EmpleadoTercero.Nombre1, dbo.Tbl_EmpleadoTercero.Nombre2, dbo.Tbl_EmpleadoTercero.Apellido1, dbo.Tbl_EmpleadoTercero.Apellido2, dbo.Tbl_EmpleadoTercero.FechaNacimiento, 
                         dbo.Tbl_EmpleadoTercero.Email, dbo.Tbl_EmpleadoTercero.Ocupacion_Empl, dbo.Tbl_EmpleadoTercero.Cargo_Empl, dbo.Tbl_EmpleadoTercero.Email_Empl, 
                         dbo.Tbl_EmpleadoTercero.Numero_Documento_Empl, dbo.Tbl_EmpleadoTercero.Ocupacion_habitual, dbo.Tbl_Estado_Empl.EstEmplead, dbo.Tbl_EmpresaTercero.Razon_Social, 
                         dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_EmpleadoTercero INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_EmpleadoTercero.FK_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_EmpresaTercero ON dbo.Tbl_EmpleadoTercero.FK_EmpresaTercero = dbo.Tbl_EmpresaTercero.PK_Nit_Empresa INNER JOIN
                         dbo.Tbl_Empleado ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Empleado.FK_Empresa INNER JOIN
                         dbo.Tbl_Estado_Empl ON dbo.Tbl_Empleado.FK_ID_Estado = dbo.Tbl_Estado_Empl.PK_IDEmpleadoEst
WHERE        (dbo.Tbl_EmpresaTercero.PK_Nit_Empresa = 860011153)
GO


