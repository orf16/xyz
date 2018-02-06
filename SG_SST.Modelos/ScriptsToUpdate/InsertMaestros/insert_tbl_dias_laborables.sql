delete Tbl_Dias_Laborables;
SET IDENTITY_INSERT Tbl_Dias_Laborables ON
GO
insert into Tbl_Dias_Laborables (pk_id_dia_laborable, descripcion) values (1,'Lunes a Viernes');
insert into Tbl_Dias_Laborables (pk_id_dia_laborable, descripcion) values  (2,'Lunes a Sabado');
SET IDENTITY_INSERT Tbl_Dias_Laborables ON
GO