
ALTER PROCEDURE [dbo].[SP_AUSENCIAS_GRUPOS_ETARIOS] (@anio int, @idOrigen int, @idEmpresaUsuaria int, @idSede int, @idDepartamento int, @nitEmpresa varchar(20))
AS
BEGIN
   declare @condiAnio varchar(100);
   declare @condidOrigen varchar(100);
   declare @condSede varchar(100);
   declare @condiDepartamento varchar(100);
   declare @empresaUsuaria varchar(50);
   declare @sql varchar(8000);
   declare @condicion VARCHAR(1000) = CONCAT(' WHERE a.NitEmpresa = ',@nitEmpresa, ' and ');
   declare @condicion2 VARCHAR(1000) = ' and a.Edad between 18 and 25 and a.FK_Id_Ausencias_Padre = 0 group by a.Edad, DATEPART(MONTH, a.fechainicio)';
   declare @condicion3 VARCHAR(1000) = ' and a.Edad between 26 and 35 and a.FK_Id_Ausencias_Padre = 0 group by a.Edad, DATEPART(MONTH, a.fechainicio)';
   declare @condicion4 VARCHAR(1000) = ' and a.Edad between 36 and 45 and a.FK_Id_Ausencias_Padre = 0 group by a.Edad, DATEPART(MONTH, a.fechainicio)';
   declare @condicion5 VARCHAR(1000) = ' and a.Edad between 46 and 55 and a.FK_Id_Ausencias_Padre = 0 group by a.Edad, DATEPART(MONTH, a.fechainicio)';
   declare @condicion6 VARCHAR(1000) = ' and a.Edad > 55 and a.FK_Id_Ausencias_Padre = 0 group by a.Edad, DATEPART(MONTH, a.fechainicio)';
   declare @consulta VARCHAR(4000) = 'select '''''''' as Contingencia, ''''18 a 25'''' as Evento, DATEPART(MONTH, a.fechainicio) as Mes, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total from Tbl_Ausencias a
										inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia';

   declare @consulta2 VARCHAR(4000) = ' UNION select '''''''' as Contingencia, ''''26 a 35'''' as Evento, DATEPART(MONTH, a.fechainicio) as Mes, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total from Tbl_Ausencias a
										inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia';

	declare @consulta3 VARCHAR(4000) = ' UNION select '''''''' as Contingencia, ''''36 a 45'''' as Evento, DATEPART(MONTH, a.fechainicio) as Mes, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total from Tbl_Ausencias a
										inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia';

	declare @consulta4 VARCHAR(4000) = ' UNION select '''''''' as Contingencia, ''''46 a 55'''' as Evento, DATEPART(MONTH, a.fechainicio) as Mes, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total from Tbl_Ausencias a
										inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia';

	declare @consulta5 VARCHAR(4000) = ' UNION select '''''''' as Contingencia, ''''Mayor a 55'''' as Evento, DATEPART(MONTH, a.fechainicio) as Mes, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total from Tbl_Ausencias a
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
	  
	  set @sql = CONCAT('exec (''', @consulta, @condicion, @condicion2, @consulta2, @condicion, @condicion3,@consulta3, @condicion, @condicion4,
	  @consulta4, @condicion, @condicion5,@consulta5, @condicion, @condicion6,''')');
	  
	  EXEC (@sql);
	  --print (@sql);

END;