USE [SGSST]
GO
delete from Tbl_RolesSistema;
DBCC CHECKIDENT ('Tbl_RolesSistema', RESEED, 0);
insert into Tbl_RolesSistema
values('Representante Legal', 'RL', 'Representante Legal', 1, 1);
insert into Tbl_RolesSistema
values('Responsable SGSST', 'RSST', 'Responsable del sistema de Seguridad y Salud en el Trabajo', 1, 1);
--insert into Tbl_RolesSistema
--values('Lider SST', 'LSST', 'Lider del Sistema de Seguridad y Salud en el Trabajo', 1, 1);
--insert into Tbl_RolesSistema
--values('Asesor SST', 'ASST', 'Asesor del Sistema de Seguridad y Salud en el Trabajo', 1, 1);
--insert into Tbl_RolesSistema
--values('Analista SST', 'ANSST', 'Analista del Sistema de Seguridad y Salud en el Trabajo', 1, 1);
--insert into Tbl_RolesSistema
--values('Trabajador', 'TBSST', 'Trabajador del Sistema de Seguridad y Salud en el Trabajo', 1, 1);
--insert into Tbl_RolesSistema
--values('Administrador', 'ADMIN', 'Administrador del Sistema de Seguridad y Salud en el Trabajo', 1, 1);