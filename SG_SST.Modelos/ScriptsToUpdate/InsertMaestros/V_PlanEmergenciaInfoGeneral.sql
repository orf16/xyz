CREATE VIEW [dbo].[V_PlanEmergenciaInfoGeneral]
AS
SELECT        ig.NitEmpresa, ig.razon_social, s.Nombre_Sede, ig.direccion_sede, ig.departamento_sede, ig.municipio_sede, ig.lindero_norte, ig.lindero_sur, ig.lindero_oriente, 
                         ig.lindero_occidente, ig.acceso_principales, ig.acceso_alternas, ig.representante, do.trabajadores_cantidad, do.contratista_cantidad, do.visitante_cantidad, 
                         do.cliente_cantidad, ci.ventilacion_mecanica, ci.ascensores, ci.sotanos, ci.red_hidraulica, ci.transformadores, ci.plantas_electricas, ci.escaleras, ci.zonas_parqueo, 
                         ci.areas_especiales, g.punto_encuentro, g.ubicacion_hidrantes, ig.fk_id_sede
FROM            dbo.Tbl_Eme_InfoGeneral AS ig INNER JOIN
                         dbo.Tbl_Sede AS s ON ig.fk_id_sede = s.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Eme_DescripcionOcupacion AS do ON ig.fk_id_sede = do.fk_id_sede INNER JOIN
                         dbo.Tbl_Eme_CaracteristicasInstalacion AS ci ON ig.fk_id_sede = ci.fk_id_sede INNER JOIN
                         dbo.Tbl_Eme_Georeferenciacion AS g ON ig.fk_id_sede = g.fk_id_sede

GO
