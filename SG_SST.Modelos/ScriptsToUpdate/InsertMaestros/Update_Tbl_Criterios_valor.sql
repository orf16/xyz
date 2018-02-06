UPDATE Tbl_Criterios SET Valor = 0.5 where Fk_Id_SubEstandar = 1
UPDATE Tbl_Criterios SET Valor = 2 where Fk_Id_SubEstandar = 2
UPDATE Tbl_Criterios SET Valor = 1 where Fk_Id_SubEstandar IN (3,4,5,8,10,11,13,14,16)
UPDATE Tbl_Criterios SET Valor = 2 where Fk_Id_SubEstandar IN (6,7,9,12)
UPDATE Tbl_Criterios SET Valor = 2 where Pk_Id_Criterio IN (32,33)
UPDATE Tbl_Criterios SET Valor = 1 where Pk_Id_Criterio =34
UPDATE Tbl_Criterios SET Valor = 4 where Pk_Id_Criterio IN (41,42,44)
UPDATE Tbl_Criterios SET Valor = 3 where Pk_Id_Criterio =43
UPDATE Tbl_Criterios SET Valor = 2.5 where Fk_Id_SubEstandar IN (18,21)
UPDATE Tbl_Criterios SET Valor = 5 where Fk_Id_SubEstandar = 19
UPDATE Tbl_Criterios SET Valor = 1.25 where Fk_Id_SubEstandar = 20
UPDATE Tbl_Config_Valoracion_SubEstandares SET Valor = 0 where Pk_Id_Config_Valoracion_SubEstand IN (6,8)