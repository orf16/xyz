USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[SP_AUSENTISMO_CONSULTAR]    Script Date: 14/08/2017 9:08:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_AUSENTISMO_CONSULTAR] (@documento int, @fechaInicial varchar(12), @fechaFin varchar(12), @idSede int, @idDiagnostico int, @idEmpresaUsuaria int, @nitEmpresa varchar(15))
AS
BEGIN
   declare @condiDocumento varchar(100);
   declare @condiFecha varchar(100);
   declare @condSede varchar(100);
   declare @condiDiagnostico varchar(100);
   declare @empresaUsuaria varchar(50);
   declare @sql varchar(4000);
   declare @condicion VARCHAR(1000) = ' WHERE ';
   declare @condicion2 VARCHAR(1000) = ' AND FK_Id_Ausencias_Padre = 0 AND NitEmpresa = '+ @nitEmpresa;
   declare @consulta VARCHAR(4000) = 'select distinct a.Pk_Id_Ausencias as IdAusencias, isnull(a.NombrePersona,'''''''') as NombrePersona, a.Documento_Persona as Documento, ''''Ausencia'''' as TipoRegistro, dp.Nombre_Departamento as Departamento, mp.Nombre_Municipio as Municipio,  a.FechaModificacion, 
	sd.Nombre_Sede nombreRegional, C.Detalle, a.fechainicio, a.Fecha_Fin as fechafin, a.diasausencia, Di.Codigo_CIE + '''' '''' + Di.Descripcion as Descripcion, a.costo, 
	a.Factor_Prestacional as FactorPrestacional, a.observaciones from Tbl_Ausencias a
	join Tbl_Municipio mp on a.FK_Id_Municipio = mp.Pk_Id_Municipio
	join Tbl_Departamento dp on a.FK_Id_Departamento = dp.Pk_Id_Departamento
	join Tbl_Sede sd on a.FK_Id_Sede = sd.Pk_Id_Sede
    join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
    join Tbl_Diagnosticos di on a.FK_Id_Diagnostico = Di.PK_Id_Diagnostico';

	if @documento > 0 
	begin
	 set @condiDocumento = CONCAT('a.Documento_Persona = ',CONVERT(VARCHAR, @documento));
	end;
	if @fechaInicial <> '' and @fechaFin <> ''
	begin
		set @condiFecha = 'a.fechainicio Between CONVERT(datetime,'''''+CONVERT(VARCHAR, @fechaInicial, 120)+''''',120) and CONVERT(datetime,'''''+CONVERT(VARCHAR, @fechaFin, 120)+''''',120)';
	end;
	if @idSede > 0
	begin
	 set @condSede = 'a.FK_Id_Sede = ' + CONVERT(VARCHAR, @idSede);
	end;
	if @idDiagnostico > 0
	begin
	 set @condiDiagnostico = 'a.FK_Id_Diagnostico = ' + CONVERT(VARCHAR, @idDiagnostico);
	end;
	if @idEmpresaUsuaria > 0
	begin
	 set @empresaUsuaria = 'a.FK_Id_EmpresaUsuaria = ' + CONVERT(VARCHAR, @idEmpresaUsuaria);
	end;

	if @condiDocumento <> ''
	begin
	 set @condicion = @condicion +  @condiDocumento;
	 if @condiFecha <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condiFecha;
	  end
	  if @condSede <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condSede;
	  end
	  if @condiDiagnostico <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condiDiagnostico;
	  end
	  if @empresaUsuaria <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @empresaUsuaria;
	  end
	end
	else if @condiFecha <> ''
	  begin
	   set @condicion = @condicion + @condiFecha;
		if @condSede <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condSede;
	  end
		if @condiDiagnostico <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @condiDiagnostico;
	   end
		if @empresaUsuaria <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @empresaUsuaria;
	   end
	 end
	else if @condSede <> ''
	  begin
	   set @condicion = @condicion + @condSede;		
		if @condiDiagnostico <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @condiDiagnostico;
	   end
		if @empresaUsuaria <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @empresaUsuaria;
	   end
	  end
	else if @condiDiagnostico <> ''
	  begin
	    set @condicion = @condicion + @condiDiagnostico;
		if @empresaUsuaria <> ''
	    begin
	     set @condicion = @condicion + ' and '+ @empresaUsuaria;
	    end
	  end
	  else if @empresaUsuaria <> ''
	   begin
	    set @condicion = @condicion + @empresaUsuaria;
	   end
	  
	  set @sql = CONCAT('exec (''', @consulta, @condicion, @condicion2,''')');
	  
	  EXEC (@sql);    

END;