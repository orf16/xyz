CREATE VIEW [dbo].[V_PlanTrabajoEmpresa]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_AplicacionPlanTrabajo.Vigencia, 
                         dbo.Tbl_AplicacionPlanTrabajo.FechaInicio, dbo.Tbl_AplicacionPlanTrabajo.FechaFinal, CASE WHEN month(FechaInicio) 
                         = 1 THEN 'Enero' ELSE '' END AS [Periodo Enero], CASE WHEN month(FechaInicio) = 2 THEN 'Febrero' ELSE '' END AS [Periodo Febrero], 
                         CASE WHEN month(FechaInicio) = 3 THEN 'Marzo' ELSE '' END AS [Periodo Marzo], CASE WHEN month(FechaInicio) = 4 THEN 'Abril' ELSE '' END AS [Periodo Abril], 
                         CASE WHEN month(FechaInicio) = 5 THEN 'Mayo' ELSE '' END AS [Periodo Mayo], CASE WHEN month(FechaInicio) = 6 THEN 'Junio' ELSE '' END AS [Periodo Junio], 
                         CASE WHEN month(FechaInicio) = 7 THEN 'Julio' ELSE '' END AS [Periodo Julio], CASE WHEN month(FechaInicio) = 8 THEN 'Agosto' ELSE '' END AS [Periodo Agosto],
                          CASE WHEN month(FechaInicio) = 9 THEN 'Septiembre' ELSE '' END AS [Periodo Septiembre], CASE WHEN month(FechaInicio) 
                         = 10 THEN 'Octubre' ELSE '' END AS [Periodo Octubre], CASE WHEN month(FechaInicio) = 11 THEN 'Noviembre' ELSE '' END AS [Periodo Noviembre], 
                         CASE WHEN month(FechaInicio) = 12 THEN 'Diciembre' ELSE '' END AS [Periodo Diciembre], dbo.Tbl_AplicacionPlanTrabajoActividad.Estado AS Estado_Actividad, 
                         COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad) AS Total_Actividades_Programadas, 
                         CASE WHEN Estado = 'EJECUTADA' THEN COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad) 
                         ELSE 0 END AS Total_Actividades_Ejecutadas, 
                         CASE WHEN Estado = 'EJECUTADA' THEN COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad) 
                         ELSE 0 END * 100 / COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad) AS Porcentaje_Ejecución
FROM            dbo.Tbl_AplicacionPlanTrabajo INNER JOIN
                         dbo.Tbl_AplicacionPlanTrabajoDetalle ON dbo.Tbl_AplicacionPlanTrabajo.Pk_Id_PlanTrabajo = dbo.Tbl_AplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo INNER JOIN
                         dbo.Tbl_AplicacionPlanTrabajoActividad ON 
                         dbo.Tbl_AplicacionPlanTrabajoDetalle.Pk_Id_PlanTrabajoDetalle = dbo.Tbl_AplicacionPlanTrabajoActividad.Fk_Id_PlanTrabajoDetalle INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_AplicacionPlanTrabajo.Fk_Id_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa
GROUP BY dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_AplicacionPlanTrabajo.Vigencia, 
                         dbo.Tbl_AplicacionPlanTrabajo.FechaInicio, dbo.Tbl_AplicacionPlanTrabajo.FechaFinal, dbo.Tbl_AplicacionPlanTrabajoActividad.Estado




GO


