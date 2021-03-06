USE [SGSST]
GO
create table Tbl_Gerentes_Positiva
(
[PK_Gerentes] [int] IDENTITY(1,1) NOT NULL,
[ZONA] [nvarchar](max) NULL,
[SUCURSAL] [nvarchar](max) NULL,
[CARGO] [nvarchar](max) NULL,
[NOMBRES] [nvarchar](max) NULL,
[DEPARTAMENTO] [nvarchar](max) NULL,
[CIUDAD] [nvarchar](max) NULL,
[CORREO] [nvarchar](max) NULL
)


go


INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('BOGOTA','BOGOTA COORDINADORA','GERENTE SUCURSAL','DIANA MARITZA SANDOVAL NAVAS ','BOGOTA D.C.','BOGOTA D.C.','diana.sandoval@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('BOGOTA','BOGOTA CUNDINAMARCA','GERENTE SUCURSAL','WILSON OMAR GONZALEZ RODRIGUEZ','CUNDINAMARCA','BOGOTA D.C.','wilsono.gonzalez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('BOGOTA','META','GERENTE SUCURSAL','NANCY ELIZABETH VALCARCEL MOJICA','META','VILLAVICENCIO','nancy.valcarcel@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('BOGOTA','GUAVIARE, VICHADA, GUANIA, VAUPES Y AMAZONAS','GERENTE SUCURSAL','DIANA MARITZA SANDOVAL NAVAS ','GUAVIARE, VICHADA, GUANIA, VAUPES Y AMAZONAS','SAN JOSE DEL GUAVIARE, PTO CARREÑO, PTO INIRIDA, MITU, LETICIA','diana.sandoval@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','ATLANTICO COORDINADORA','GERENTE SUCURSAL','MARGARITA MARIA GONZALEZ MARTINEZ','ATLANTICO ','BARRANQUILLA','margaritam.gonzalez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','MAGDALENA','GERENTE SUCURSAL','HERNANDO DE LA HOZ GONZALEZ','MAGDALENA','SANTA MARTA','hernando.hoz@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','BOLIVAR','GERENTE SUCURSAL','ENEDIS MARIA TAPIA MEJIA','BOLIVAR','CARTAGENA','enedis.tapias@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','CESAR','GERENTE SUCURSAL','RITA CRUZ OCAÑA MARTINEZ','CESAR','VALLEDUPAR','rita.ocana@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','LA GUAJIRA','GERENTE SUCURSAL','ALFREDO ZULETA VALLE','LA GUAJIRA','RIOHACHA','alfredo.zuleta@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','SUCRE','GERENTE SUCURSAL','LUIGI FABIAN OSORIO COLL','SUCRE','SINCELEJO ','luigi.osorio@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','CORDOBA','GERENTE SUCURSAL','RODRIGO ANTONIO BURGOS DE LA ESPRIELLA','CORDOBA','MONTERIA','rodrigo.burgos@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ATLANTICO','SAN ANDRES','GERENTE SUCURSAL','MARGARITA MARIA GONZALEZ MARTINEZ','SAN ANDRES','SAN ANDRES','margaritam.gonzalez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ANTIOQUIA','ANTIOQUIA COORDINADORA','GERENTE SUCURSAL','JOSE MAURO PALACIO MORALES','ANTIOQUIA ','MEDELLIN','mauro.palacio@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ANTIOQUIA','CHOCO','GERENTE SUCURSAL','JOSE MAURO PALACIO MORALES','CHOCO','QUIBDO','mauro.palacio@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ANTIOQUIA','CALDAS','GERENTE SUCURSAL','CARLOS IVAN HEREDIA FERREIRA','CALDAS','MANIZALES','carlosi.heredia@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ANTIOQUIA','QUINDIO','GERENTE SUCURSAL','MARINA GUTIERREZ GOMEZ','QUINDIO','ARMENIA','marina.gutierrez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('ANTIOQUIA','RISARALDA','GERENTE SUCURSAL','JAVIER BARUC BUILES POVEDA','RISARALDA','PEREIRA','javier.builes@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('SANTANDER','SANTANDER COORDINADORA','GERENTE SUCURSAL','CLAUDIA  CONSUELO  RIBERO ROJAS ','SANTANDER','BUCARAMANGA','claudia.ribero@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('SANTANDER','NORTE DE SANTANDER','GERENTE SUCURSAL','LUIS EDUARDO CARRASCAL QUINTERO ','NORTE DE SANTANDER','CUCUTA','luis.carrascal@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('SANTANDER','ARAUCA','GERENTE SUCURSAL','NORBERTO ALONSO ESPINEL ORTEGA','ARAUCA','ARAUCA','norberto.espinel@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('SANTANDER','BOYACA ','GERENTE SUCURSAL','OSMEL ULLOA CASTELLANOS','BOYACA ','TUNJA','osmel.ulloa@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('SANTANDER','CASANARE','GERENTE SUCURSAL','ISAIAS TRISTANCHO CEDIEL','CASANARE','YOPAL','isaias.tristancho@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','VALLE COORDINADORA','GERENTE SUCURSAL','JUAN CARLOS ARBOLEDA ANGULO','VALLE','CALI','juanc.arboleda@positiva.gov.co ')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','TOLIMA','GERENTE SUCURSAL','YOLANDA ZAPATA GUZMAN','TOLIMA','IBAGUE','yolanda.zapata@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','HUILA','GERENTE SUCURSAL','STELLA ROA BUSTOS','HUILA','NEIVA','stella.roa@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','CAUCA','GERENTE SUCURSAL','FELIPE CAMPO ARROYO','CAUCA','POPAYÁN','felipe.campo@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','NARIÑO','GERENTE SUCURSAL','XIMENA PATRICIA  ENRIQUEZ LOZANO','NARIÑO','PASTO','ximena.enriquez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','PUTUMAYO','GERENTE SUCURSAL','RENATO LOPEZ LEGARDA','PUTUMAYO','MOCOA','renato.lopez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('VALLE','CAQUETA','GERENTE SUCURSAL','EFREN SUAREZ ARDILA','CAQUETA','FLORENCIA','efren.suarez@positiva.gov.co')
INSERT INTO [dbo].[Tbl_Gerentes_Positiva]      VALUES    ('BOGOTA','GERENCIA  CORREDORES','GERENTE SUCURSAL','ALEYDA MEDINA ALVAREZ ','BOGOTA D.C.','BOGOTA D.C.','aleyda.medina@positiva.gov.co')
