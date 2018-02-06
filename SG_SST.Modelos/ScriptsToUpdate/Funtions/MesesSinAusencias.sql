USE [SGSST]
GO
/****** Object:  UserDefinedFunction [dbo].[MesesSinAusencias]    Script Date: 30/05/2017 23:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[MesesSinAusencias](@anio varchar(10))
RETURNS TABLE
AS
RETURN
(
	WITH MesesNoAusencias AS
	(
	SELECT 0 AS mes
	UNION ALL 
	SELECT mes + 1 FROM MesesNoAusencias 
	WHERE mes < 11
	)

	SELECT  datepart(Month,CONVERT(DATE,DATEADD(MONTH,mes,@anio))) AS Mes,
			0 as EventosPorMes, 0 as DiasAusenciaPorMes, 0 as HorasTrabajadas, 0 as NumeroTrabajadores
	from MesesNoAusencias
)