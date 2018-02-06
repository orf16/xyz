CREATE VIEW [dbo].[V_PlanEmergenciaFrentesAccion]
AS
SELECT        ig.NitEmpresa, ig.razon_social, s.Nombre_Sede, fa.plan_seguridadfisica, fa.plan_primerosaux, fa.plan_contraincendios, fa.nombrecoordinador, fa.objetivos, 
                         fa.estructura, fa.proc_coordinacion, fa.proc_internos, fa.proc_externos, fa.mecanismos_alarma, fa.simulacros, fa.instructivo_evacuacion, fa.proc_retorno, 
                         ig.fk_id_sede
FROM            dbo.Tbl_Eme_InfoGeneral AS ig INNER JOIN
                         dbo.Tbl_Sede AS s ON ig.fk_id_sede = s.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Eme_FrentesAccion AS fa ON ig.fk_id_sede = fa.fk_id_sede

GO




