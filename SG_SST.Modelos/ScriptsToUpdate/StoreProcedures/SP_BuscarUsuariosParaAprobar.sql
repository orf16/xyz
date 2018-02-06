USE [SGSST]
GO
/****** Object:  StoredProcedure [dbo].[BuscarUsuariosParaAprobar]    Script Date: 07/07/2017 22:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Victoria
-- Description:	Devuelve los registros de los usuarios pendientes por aprobación 
--				Basándose en los criterios de búsqueda
-- =============================================
ALTER PROCEDURE [dbo].[BuscarUsuariosParaAprobar] 
	@NumDocEmp varchar(50), @NumDocUsu varchar(50), @RolSeleccionado varchar(50), @Pagina int = 1
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
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		and ua.NumeroDocumentoUsuario = @NumDocUsu
		and ua.Fk_Id_RolSistema = @RolSeleccionado
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else if(ISNULL(@NumDocEmp, '') != '' and ISNULL(@NumDocUsu, '') != '')
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		and ua.NumeroDocumentoUsuario = @NumDocUsu
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else if(ISNULL(@NumDocEmp, '') != '' and ISNULL(@RolSeleccionado, '') != '')
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		and ua.Fk_Id_RolSistema = @RolSeleccionado
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else if(ISNULL(@NumDocEmp, '') != '')
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoEmprsa = @NumDocEmp
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else if(ISNULL(@NumDocUsu, '') != '' and ISNULL(@RolSeleccionado, '') != '')
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoUsuario = @NumDocUsu
		and ua.Fk_Id_RolSistema = @RolSeleccionado
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else if(ISNULL(@NumDocUsu, '') != '')
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.NumeroDocumentoUsuario = @NumDocUsu
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else if(ISNULL(@RolSeleccionado, '') != '')
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		where ua.Fk_Id_RolSistema = @RolSeleccionado
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
	else
	begin
		select	ua.Pk_id_UsuarioParaAprobar as IdUsuarioPorAprobar, ua.Nombres, ua.Apellidos, ua.EmailUsuario as Correo, tde.Descripcion as TipoDocumentoEmpresa,
				ua.NumeroDocumentoEmprsa as NumDocumentoEmpresa, ua.RazonSocial as RazonSocialEmpresa, ua.MunicipioSedePpal as MunicipioSedePpalEmpresa,
				tda.Descripcion as TipoDocumentoAfi, ua.NumeroDocumentoUsuario as NumDocumentoAfi,
				ua.Fk_Id_RolSistema as RolUsuario, rs.Nombre as NombreRol
		from Tbl_UsuariosParaAprobar ua
		inner join Tbl_Tipo_Documento tde on ua.TipoDocumentoEmpresa = tde.PK_IDTipo_Documento
		inner join Tbl_Tipo_Documento tda on ua.TipoDocumentoUsuario = tda.PK_IDTipo_Documento
		inner join tbl_RolesSistema rs on ua.Fk_Id_RolSistema = rs.Pk_Id_RolSistema
		order by ua.Nombres
		OFFSET (@Pagina-1) * @CantByPag ROWS FETCH NEXT @CantByPag ROWS ONLY
	end
END
