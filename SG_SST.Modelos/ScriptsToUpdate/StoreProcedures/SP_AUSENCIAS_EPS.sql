
ALTER PROCEDURE [dbo].[SP_AUSENCIAS_EPS] (@anio int, @idOrigen int, @idEmpresaUsuaria int, @idSede int, @idDepartamento int, @nitEmpresa varchar(20))
AS
BEGIN
   declare @condiAnio varchar(100);
   declare @condidOrigen varchar(100);
   declare @condSede varchar(100);
   declare @condiDepartamento varchar(100);
   declare @empresaUsuaria varchar(50);
   declare @sql varchar(4000);
   declare @condicion VARCHAR(1000) = CONCAT(' WHERE a.NitEmpresa = ',@nitEmpresa, ' and ');
   declare @condicion2 VARCHAR(1000) = ' and a.FK_Id_Ausencias_Padre = 0 group by a.Eps, DATEPART(MONTH,a.fechainicio)';
   declare @consulta VARCHAR(4000) = 'select '''''''' as Contingencia, isnull(a.Eps, ''''No especificada'''') as Evento, 
									  DATEPART(MONTH,a.fechainicio) as Mes, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total 
									  from Tbl_Ausencias a 
									  inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia';

	if @anio > 0 
	begin
	 set @condiAnio = CONCAT('DATEPART(YEAR,fechainicio) = ',@anio);
	end;
	if @idOrigen > 0
	begin
		set @condidOrigen = CONCAT('c.FK_Tipo_Contingencia = ',@idOrigen); 
	end;
	if @idSede > 0
	begin
	 set @condSede = CONCAT('a.Fk_Id_Sede = ',@idSede); 
	end;
	if @idDepartamento > 0
	begin
	 set @condiDepartamento = CONCAT('a.FK_Id_Departamento = ',@idDepartamento);
	end;
	if @idEmpresaUsuaria > 0
	begin
	 set @empresaUsuaria = CONCAT('a.FK_Id_EmpresaUsuaria = ',@idEmpresaUsuaria);
	end;

	if @condiAnio <> ''
	begin
	 set @condicion = @condicion +  @condiAnio;
	 if @condidOrigen <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condidOrigen;
	  end
	  if @condSede <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condSede;
	  end
	  if @condiDepartamento <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condiDepartamento;
	  end
	  if @empresaUsuaria <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @empresaUsuaria;
	  end
	end
	else if @idOrigen <> ''
	  begin
	   set @condicion = @condicion + @idOrigen;
		if @condSede <> ''
	  begin
	   set @condicion = @condicion + ' and '+ @condSede;
	  end
		if @condiDepartamento <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @condiDepartamento;
	   end
		if @empresaUsuaria <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @empresaUsuaria;
	   end
	 end
	else if @condSede <> ''
	  begin
	   set @condicion = @condicion + @condSede;		
		if @condiDepartamento <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @condiDepartamento;
	   end
		if @empresaUsuaria <> ''
	   begin
	    set @condicion = @condicion + ' and '+ @empresaUsuaria;
	   end
	  end
	else if @condiDepartamento <> ''
	  begin
	    set @condicion = @condicion + @condiDepartamento;
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