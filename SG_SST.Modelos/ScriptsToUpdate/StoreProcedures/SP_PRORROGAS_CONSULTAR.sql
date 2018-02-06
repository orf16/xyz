USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[SP_PRORROGAS_CONSULTAR]    Script Date: 27/07/2017 2:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_PRORROGAS_CONSULTAR] (@idAusenciaPadre int, @documento varchar(20) )
AS
BEGIN
   select distinct a.Pk_Id_Ausencias as IdAusencias, isnull(a.NombrePersona,'') as NombrePersona, a.Documento_Persona as Documento, dp.Nombre_Departamento as Departamento, 'Prorroga' as TipoRegistro, mp.Nombre_Municipio as Municipio,  a.FechaModificacion, 
	sd.Nombre_Sede nombreRegional, C.Detalle, a.fechainicio, a.Fecha_Fin as fechafin, a.diasausencia, Di.Codigo_CIE + ' ' + Di.Descripcion as Descripcion, a.costo, 
	a.Factor_Prestacional as FactorPrestacional from Tbl_Ausencias a
	join Tbl_Municipio mp on a.FK_Id_Municipio = mp.Pk_Id_Municipio
	join Tbl_Departamento dp on a.FK_Id_Departamento = dp.Pk_Id_Departamento
	join Tbl_Sede sd on a.FK_Id_Sede = sd.Pk_Id_Sede
    join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
    join Tbl_Diagnosticos di on a.FK_Id_Diagnostico = Di.PK_Id_Diagnostico
	WHERE a.FK_Id_Ausencias_Padre = @idAusenciaPadre and a.Documento_Persona = @documento
	order by a.FechaInicio 
END;