USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerCantidadUsuariosParaAprobar]    Script Date: 07/07/2017 22:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerCantidadUsuariosParaAprobar]
	@NumDocEmp varchar(50), @NumDocUsu varchar(50), @RolSeleccionado varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	declare @CantByPag int = 0
	create table #UsuariosPorDocEmpresa (IdUsuario int);
	create table #UsuariosPorDocUsuario (IdUsuario int);
	create table #UsuariosPorRol (IdUsuario int);
	set @CantByPag = (select convert(int, valor) from Tbl_ParametrosSistema where NombreParametro = 'CantRegistrosPagPaginador')
	if(ISNULL(@NumDocEmp, '') != '' and ISNULL(@NumDocUsu, '') != '' and ISNULL(@RolSeleccionado, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		and ua.NumeroDocumentoUsuario = @NumDocUsu
		and ua.Fk_Id_RolSistema = @RolSeleccionado
	end
	else if(ISNULL(@NumDocEmp, '') != '' and ISNULL(@NumDocUsu, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		and ua.NumeroDocumentoUsuario = @NumDocUsu
	end
	else if(ISNULL(@NumDocEmp, '') != '' and ISNULL(@RolSeleccionado, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		and ua.Fk_Id_RolSistema = @RolSeleccionado
	end
	else if(ISNULL(@NumDocEmp, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
	end
	else if(ISNULL(@NumDocUsu, '') != '' and ISNULL(@RolSeleccionado, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoUsuario = @NumDocUsu
		and ua.Fk_Id_RolSistema = @RolSeleccionado
	end
	else if(ISNULL(@NumDocUsu, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoUsuario = @NumDocUsu
	end
	else if(ISNULL(@RolSeleccionado, '') != '')
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.Fk_Id_RolSistema = @RolSeleccionado
	end
	else
	begin
		select	count(ua.Pk_id_UsuarioParaAprobar) as Cuantos
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
	end
END

