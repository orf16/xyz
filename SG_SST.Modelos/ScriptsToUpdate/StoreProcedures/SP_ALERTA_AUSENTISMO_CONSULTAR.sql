USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[SP_ALERTA_AUSENTISMO_CONSULTAR]    Script Date: 11/06/2017 16:56:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_ALERTA_AUSENTISMO_CONSULTAR] (
	@idEmpresaUsuaria int, 
	@anioGestion int
)
AS
BEGIN
	-- Retorna la lista de ausencias para una empresa usuaria
	-- en un año de gestión.
	select 
		a.Documento_Persona DocumentoPersona, 
		a.FK_Id_Contingencia IdContingencia, 
		a.FechaInicio FechaInicio, 
		a.Fecha_Fin FechaFin,
		a.NombrePersona EmpleadoNombre,
		a.FK_Id_Diagnostico Id_Diagnostico
	from 
		Tbl_Ausencias a
	where 
		a.FK_Id_EmpresaUsuaria = @idEmpresaUsuaria
		and (
			year(a.FechaInicio) = @anioGestion
			or year(a.Fecha_Fin) = @anioGestion
			)
	order by 
		a.Documento_Persona, a.FechaInicio;

END;