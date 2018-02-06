

CREATE VIEW [dbo].[V_PlanCapacitacion]
AS
SELECT DISTINCT 
                         dbo.Tbl_Empresa.Razon_Social AS [Razon Social], dbo.Tbl_Empresa.Nit_Empresa, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 1 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Enero, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 2 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Febrero, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 3 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Marzo, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 4 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Abril, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 5 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Mayo, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 6 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Junio, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 7 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Julio, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 8 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Agosto, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 9 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Septiembre, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 10 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Octubre, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 11 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Noviembre, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 12 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Diciembre, dbo.Tbl_Rol.Descripcion AS Rol, dbo.Tbl_Tematica.Tematicas AS Competencia, 
                         dbo.Tbl_PlanCapacitacion.fecha_programada AS [Fecha Programada], dbo.Tbl_PlanCapacitacion.hora_inicio, dbo.Tbl_PlanCapacitacion.hora_fin, 
                         dbo.Tbl_Tematica.Tematicas
FROM            dbo.Tbl_Rol INNER JOIN
                         dbo.Tbl_PlanCapacitacion ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_PlanCapacitacion.fk_id_rol INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Rol.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tematica_Por_Empresa ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Tematica_Por_Empresa.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tematica ON dbo.Tbl_Tematica_Por_Empresa.Fk_Id_Tematica = dbo.Tbl_Tematica.Id_Tematica AND 
                         dbo.Tbl_PlanCapacitacion.fk_id_competencia = dbo.Tbl_Tematica.Id_Tematica INNER JOIN
                         dbo.Tbl_Empleado_Por_Tematica ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_Empleado_Por_Tematica.Fk_Id_Rol INNER JOIN
                         dbo.Tbl_Empleado_Tematica ON dbo.Tbl_Empleado_Por_Tematica.Fk_Id_Tematica = dbo.Tbl_Empleado_Tematica.Pk_Id_EmpleadoTematica INNER JOIN
                         dbo.Tbl_Cargo_Por_Rol ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_Cargo_Por_Rol.Fk_Id_Rol INNER JOIN
                         dbo.Tbl_Cargo ON dbo.Tbl_Cargo_Por_Rol.Fk_Id_Cargo = dbo.Tbl_Cargo.Pk_Id_Cargo

