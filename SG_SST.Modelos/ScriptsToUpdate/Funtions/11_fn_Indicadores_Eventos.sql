ALTER FUNCTION [dbo].[Indicadores_Eventos] (@anio int, @idEmpresaUsuaria int, @Nit varchar(20), @idContingencia int)

RETURNS @DATOS TABLE 
(
    -- Columns returned by the function
    IdContingencia int, 
    Contingencia nvarchar(1000), 
    Mes  int, 
    NumeroEventos int, 
    DiasPorEventos decimal
)
AS
Begin   
   if @idEmpresaUsuaria > 0    
    insert into @DATOS
	select	c.PK_Id_Contingencia as IdContingencia,
			c.Detalle as Contingencia, 
			DATEPART(MONTH, a.fechainicio) as Mes,
			count(a.Pk_Id_Ausencias) as NumeroEventos,
			sum(a.DiasAusencia) as DiasPorEventos
	from Tbl_Contingencias c
	inner join Tbl_Ausencias a on c.PK_Id_Contingencia = a.FK_Id_Contingencia
	where DATEPART(YEAR, a.FechaInicio) = @anio AND A.FK_Id_EmpresaUsuaria = @idEmpresaUsuaria
	and c.PK_Id_Contingencia in(@idContingencia)
	and a.FK_Id_Ausencias_Padre = 0
	and a.NitEmpresa = @Nit
	group by c.PK_Id_Contingencia, c.Detalle, DATEPART(MONTH, a.fechainicio)
	ELSE
	insert into @DATOS
	select	c.PK_Id_Contingencia as IdContingencia,
			c.Detalle as Contingencia, 
			DATEPART(MONTH, a.fechainicio) as Mes,
			count(a.Pk_Id_Ausencias) as NumeroEventos,
			sum(a.DiasAusencia) as DiasPorEventos
	from Tbl_Contingencias c
	inner join Tbl_Ausencias a on c.PK_Id_Contingencia = a.FK_Id_Contingencia
	where DATEPART(YEAR, a.FechaInicio) = @anio
	and c.PK_Id_Contingencia in(@idContingencia)
	and a.FK_Id_Ausencias_Padre = 0
	and a.NitEmpresa = @Nit
	group by c.PK_Id_Contingencia, c.Detalle, DATEPART(MONTH, a.fechainicio)
	
	return
end
