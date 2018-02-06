

CREATE VIEW [dbo].[V_presupuesto]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social AS EMPRESA, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Presupuesto_Por_Año.Periodo AS PERIODO_AÑO, SUM(dbo.Tbl_Presupuesto.RubroTotal) AS RUBRO_TOTAL, 
                         SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoMes) AS TOTAL_PLANEADO, SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoEjecutadoPorMes) 
                         AS TOTAL_EJECUTADO, SUM(dbo.Tbl_Presupuesto.RubroTotal) - SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoEjecutadoPorMes) AS SALDO
FROM            dbo.Tbl_Prepuesto_Por_Mes INNER JOIN
                         dbo.Tbl_Presupuesto ON dbo.Tbl_Prepuesto_Por_Mes.FK_Presupuesto = dbo.Tbl_Presupuesto.PK_Prepuesto INNER JOIN
                         dbo.Tbl_Presupuesto_Por_Año ON dbo.Tbl_Presupuesto.PK_Prepuesto = dbo.Tbl_Presupuesto_Por_Año.FK_Presupuesto INNER JOIN
                         dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa ON 
                         dbo.Tbl_Presupuesto_Por_Año.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede
where        (dbo.Tbl_Empresa.Nit_Empresa = ?)

GROUP BY dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Presupuesto_Por_Año.Periodo


