CREATE VIEW [dbo].[VW_EstudioPuestosdeTrabajo]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_EstudioPuestoTrabajo.Numero_Identificacion, dbo.Tbl_EstudioPuestoTrabajo.Trabajador_Primer_Nombre, 
                         dbo.Tbl_EstudioPuestoTrabajo.Trabajador_Segundo_Nombre, dbo.Tbl_EstudioPuestoTrabajo.Trabajador_Primer_Apellido, dbo.Tbl_EstudioPuestoTrabajo.Trabajador_Segundo_Apellido, 
                         dbo.Tbl_EstudioPuestoTrabajo.Cargo_Empleado, dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_EstudioPuestoTrabajo.FechaAnalisis, dbo.Tbl_Diagnosticos.Descripcion, 
                         dbo.Tbl_Tipo_Analisis_Puesto_Trabajo.Nombre_Tipo_Analisis_Puesto_Trabajo, dbo.Tbl_ObjetivoAnalisis.Nombre_ObjetivoAnalisis
FROM            dbo.Tbl_EstudioPuestoTrabajo INNER JOIN
                         dbo.Tbl_Diagnosticos ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Diagnostico = dbo.Tbl_Diagnosticos.PK_Id_Diagnostico INNER JOIN
                         dbo.Tbl_ObjetivoAnalisis ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_ObjetivoAnalisis = dbo.Tbl_ObjetivoAnalisis.Pk_Id_ObjetivoAnalisis INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_EmpresaProceso INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_EmpresaProceso.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa ON dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_EmpresaProceso.Fk_Id_Proceso INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Sede = dbo.Tbl_Sede.Pk_Id_Sede AND dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tipo_Analisis_Puesto_Trabajo ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Tipo_Analisis_Puesto_Trabajo = dbo.Tbl_Tipo_Analisis_Puesto_Trabajo.Pk_Id_Tipo_Analisis_Puesto_Trabajo

GO



