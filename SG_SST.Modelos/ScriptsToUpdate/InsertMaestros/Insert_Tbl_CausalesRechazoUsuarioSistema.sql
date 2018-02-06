delete from Tbl_CausalesRechazoUsuariosSistema;
DBCC CHECKIDENT ('Tbl_CausalesRechazoUsuariosSistema', RESEED, 0);
insert into Tbl_CausalesRechazoUsuariosSistema values('Empleador en proceso de normalización de pagos.','La empresa se encuentra inactiva en SIARP');
insert into Tbl_CausalesRechazoUsuariosSistema values('Empleador con afiliación inconsistente.','La infirmación del empleador no se encuentra en SIARP');
insert into Tbl_CausalesRechazoUsuariosSistema values('Empleador en proceso de traslado.','El empleado no trabaja más en la sede.');
insert into Tbl_CausalesRechazoUsuariosSistema values('Solicitud de rechazo por parte del cliente.','El cliente solicita la inactivación de su cuenta.');