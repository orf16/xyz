CREATE PROCEDURE[dbo].[DesactivarUsuariosSistema]
AS
BEGIN

    update tbl_Usuariosistema

    set Activo = 0

    from tbl_usuariosistema us
    where us.PeriodoInactivacionCuenta<GETDATE()
END