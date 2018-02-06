USE [SGSST]
GO

/****** Object:  UserDefinedFunction [dbo].[Alertas_Ausentismo]    Script Date: 25/05/2017 02:37:25 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create FUNCTION [dbo].[Alertas_Ausentismo](@anio int)
RETURNS TABLE
AS
RETURN
(
SELECT
A.FechaRegistro, A.Documento_Persona AS Documento, dp.Nombre_Departamento as Departamento,
mp.Nombre_Municipio as Municipio, C.Detalle AS Contingencia, D.Descripcion AS Diagnostico,
A.DiasAusencia AS Dias_Ausencia, a.Costo 
FROM Tbl_Ausencias A
INNER JOIN Tbl_Municipio mp on a.FK_Id_Municipio = mp.Pk_Id_Municipio
INNER JOIN Tbl_Departamento dp on a.FK_Id_Departamento = dp.Pk_Id_Departamento
INNER JOIN Tbl_Diagnosticos D ON A.FK_Id_Diagnostico = D.PK_Id_Diagnostico
INNER JOIN Tbl_Contingencias C ON  A.FK_Id_Contingencia = C.PK_Id_Contingencia
WHERE a.DiasAusencia BETWEEN 60 AND 120
OR a.DiasAusencia  > 120
AND DATEPART(yy,a.FechaModificacion) = @anio
)
GO

