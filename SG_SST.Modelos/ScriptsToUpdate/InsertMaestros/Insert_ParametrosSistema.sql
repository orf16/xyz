USE [SGSST]
GO
delete from tbl_parametrosSistema;
DBCC CHECKIDENT ('tbl_parametrosSistema', RESEED, 0);
insert into tbl_parametrosSistema
values('RutaHttpSitio', 'http://localhost:3143');
insert into tbl_parametrosSistema
values('ServidorStmp', '10.114.158.8');
insert into Tbl_ParametrosSistema
values('PuertoServidorStmp', '25');
insert into tbl_parametrosSistema
values('RemitenteNotificaion', 'Gerente ALISTA');
insert into tbl_parametrosSistema
values('CorreoRemitente', 'noreply@positiva.gov.co');
insert into tbl_parametrosSistema
values('CaracteresPasswordTemporal', 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890');
insert into tbl_parametrosSistema
values('LongitudPasswordTemporal', '10');
insert into Tbl_ParametrosSistema
values('UsuarioServidorStmp', 'noreply@positiva.gov.co');
insert into Tbl_ParametrosSistema
values('ClaceServidorStmp', 'P0s1t1v4');
insert into Tbl_ParametrosSistema
values('CantRegistrosPagPaginador', '20');
insert into Tbl_ParametrosSistema
values('RolesBaseEmpresa', 'Representante Legal,RESPONSABLE DE SGSST,Profesional SST,COPASST / VIGIA,Comité de Convivencia,Coordinador de Alturas,Trabajadores');