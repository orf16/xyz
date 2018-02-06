USE SGSST;  
GO 


DELETE FROM Tbl_SegVialDetalle;
DELETE FROM Tbl_SegVialParametro;
DELETE FROM Tbl_SegVialPilar;



--DBCC CHECKIDENT (Tbl_PlanVial, RESEED,0);
DBCC CHECKIDENT (Tbl_SegVialDetalle, RESEED,0);
DBCC CHECKIDENT (Tbl_SegVialParametro, RESEED,0);
DBCC CHECKIDENT (Tbl_SegVialPilar, RESEED,0);
DBCC CHECKIDENT (Tbl_SegVialResultado, RESEED,0);


INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('FORTALECIMIENTO EN LA GESTI�N INSTITUCIONAL',1,0.3);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('COMPORTAMIENTO HUMANO',1,0.3);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('VEH�CULOS SEGUROS',1,0.2);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('INFRAESTRUCTURA SEGURA ',1,0.1);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('ATENCI�N A V�CTIMAS',1,0.1);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('VALORES AGREGADOS O INNOVACIONES',1,0.05);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'1.1','OBJETIVOS DEL PESV',3,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'1.2','COMIT� DE SEGURIDAD VIAL -  Mecanismo de coordinaci�n entre todos los involucrados y cuyo objetivo ser� planificar, dise�ar, implementar y medir las acciones, para lograr los objetivos a favor de la seguridad vial.',15,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (3,'1.3','RESPONSABLE DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL',4,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (4,'1.4','POL�TICA DE SEGURIDAD VIAL ',10,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (5,'1.5','DIVULGACI�N DE LA POL�TICA DE SEGURIDAD VIAL',5,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (6,'1.6','DIAGN�STICO - CARACTERIZACI�N DE LA EMPRESA',10,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (7,'1.7','DIAGN�STICO - RIESGOS VIALES',20,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (8,'1.8','PLANES DE ACCI�N DE RIESGOS VIALES',15,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (9,'1.9','IMPLEMENTACION DE ACCIONES DEL PESV',10,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (10,'1.10','SEGUIMIENTO Y EVALUACI�N DE PLANES DE ACCI�N DEL PESV',8,1);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'2.1','PROCEDIMIENTO DE SELECCI�N DE CONDUCTORES',10,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'2.2','PRUEBAS DE INGRESO DE CONDUCTORES',20,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (3,'2.3','PRUEBAS DE CONTROL PREVENTIVO DE CONDUCTORES',20,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (4,'2.4','CAPACITACION EN SEGURIDAD VIAL',20,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (5,'2.5','CONTROL DE DOCUMENTACION DE CONDUCTORES',10,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (6,'2.6','POLITICAS DE REGULACION DE LA EMPRESA',20,2);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'3.1','MANTENIMIENTO PREVENTIVO',50,3);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'3.2','MANTENIMIENTO CORRECTIVO',30,3);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (3,'3.3','CHEQUEO PREOPERACIONAL',20,3);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'4.1','RUTAS INTERNAS - V�as internas de la empresa, en donde circulan los veh�culos',35,4);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'4.2','RUTAS EXTERNAS: Desplazamiento fuera del entorno fisico de la empresa',65,4);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'5.1','ATENCI�N A VICTIMAS',20,5);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'5.2','INVESTIGACI�N DE ACCIDENTES DE TR�NSITO',80,5);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'6.1','INVESTIGACI�N DE ACCIDENTES DE TR�NSITO',100,6);




INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.1.1','OBJETIVO GENERAL DEL PESV','Se ha fijado un objetivo claro, concreto y realizable, as� como su alcance y visi�n',1);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.1.2','OBJETIVOS ESPEC�FICOS DEL PESV','Los objetivos espec�ficos se ajustan al objetivo general y de realizarse se cumplir�a con los prop�sitos',1);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.1.3','DIRECTRICES DE LA ALTA DIRECCI�N','Existe un documento que indique el compromiso de las directivas, para el desarrollo del PESV',1);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.1','ACTA DE COMIT� DE SEGURIDAD VIAL','Existe un acta de conformaci�n del comit� de S.V.',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.2','OBJETIVOS DEL COMIT� DE SEGURIDAD VIAL','Est� definido el objetivo del comit� de seguridad vial',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.3','INTEGRANTES DEL COMIT� DE S.V.','El comit� ha sido definido por la alta direcci�n',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.4','ROLES Y FUNCIONES DE LOS INTEGRANTES','Los integrantes del comit� tienen relaci�n con las labores y planes de acci�n inherentes al PESV',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.5','FRECUENCIA DE REUNIONES DEL COMIT� DE SV','Est� definida la frecuencia de las reuniones del comit� de S.V.',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.3.1','RESPONSABLE DEL PESV','Se design� un responsable del proceso de elaboraci�n y seguimiento del PESV, indicando el cargo dentro de la organizaci�n.',3);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.3.2','IDONEIDAD DEL RESPONSABLE DEL PESV','El responsable es id�neo para el desarrollo, implementaci�n y seguimiento del PESV  y todas las acciones contempladas en este.',3);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.1','EST� DOCUMENTADA LA POL�TICA DE SEGURIDAD VIAL','Existe un documento que permita identificar la pol�tica de Seguridad vial de la empresa',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.2','POL�TICA DE SEGURIDAD VIAL','Existe pol�tica de seguridad vial documentada ',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.3','POL�TICA DE SEGURIDAD VIAL','Se adecuada al prop�sito de la organizaci�n.',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.4','POL�TICA DE SEGURIDAD VIAL','Proporciona un marco de referencia para el establecimiento de los objetivos y de las metas del PESV',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.5','POL�TICA DE SEGURIDAD VIAL','Incluye el compromiso de cumplir los requisitos aplicables y la mejora continua.',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.5.1','DIVULGACI�N DE LA POL�TICA DE SEGURIDAD VIAL','Existe evidencia de su divulgaci�n, como p�gina web de la compa��a, retablos en las instalaciones de la compa��a u otros?',5);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.5.2','DIVULGACI�N DE LA POL�TICA DE SEGURIDAD VIAL','Existe evidencia de que se ha informado al personal sobre el PESV y la pol�tica de seguridad vial?',5);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.1','CARACTER�STICAS DE LA EMPRESA','Est� descrita la actividad econ�mica que realiza la empresa',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.2','CARACTER�STICAS DE LA EMPRESA','Est� documentado el an�lisis de la empresa, su contexto, actividades, su personal, desplazamientos, etc. ',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.3','CARACTER�STICAS DE LA EMPRESA','Est�n definidos los servicios que presta la compa��a',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.4','CARACTER�STICAS DE LA EMPRESA','Est� definida la poblaci�n de personal que hace parte de la compa��a',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.5','CARACTER�STICAS DE LA EMPRESA','Est�n definidos los veh�culos automotores y no automotores puestos al servicio de la compa��a.',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.6','CARACTER�STICAS DE LA EMPRESA','Est�n definidas las ciudades de operaci�n de la organizaci�n',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.7','CARACTER�STICAS DE LA EMPRESA','Est�n documentados los mecanismos de contrataci�n de los veh�culos.',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.8','CARACTER�STICAS DE LA EMPRESA','Est�n documentados los mecanismos de contrataci�n de conductores.',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.1','ENCUESTA / INSTRUMENTO PARA DETERMINAR EL RIESGO VIAL','Se dise�� una encuesta u otro instrumento o mecanismo objetivo, para el levantamiento de informaci�n del riesgo vial.',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.2','APLICACI�N DE LA ENCUESTA','Se ha aplicado la encuesta ',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.3','APLICACI�N DE LA ENCUESTA','Se han tenido en cuenta los riesgos de la operaci�n in itinere y en misi�n',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.4','CONSOLIDACI�N Y AN�LISIS DE LA ENCUESTA ','Se han consolidado los resultados de la encuesta y hecho un an�lisis de los resultados',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.5','DEFINICI�N DE RIESGOS VIALES DE LA EMPRESA','Se han definido riesgos viales para el personal de la empresa, dependiendo de su rol en la v�a (Peat�n, pasajero, ciclista, conductor)',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.6','CALIFICACI�N Y CLASIFICACI�N DE RIESGOS VIALES','Se han calificado los riesgos viales identificados a trav�s de la encuesta',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.7','CALIFICACI�N Y CLASIFICACI�N DE RIESGOS VIALES','La calificaci�n de los riesgos se ha hecho basado en alguna norma o est�ndar ',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.1','DEFINICI�N DE PLANES DE ACCI�N','De acuerdo con los resultados del diagn�stico de riesgos viales, se han definido planes de acci�n para el FACTOR HUMANO',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.2','DEFINICI�N DE PLANES DE ACCI�N','De acuerdo con los resultados del diagn�stico de riesgos viales se han definido planes de  acci�n para el FACTOR VEHICULOS',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.3','DEFINICI�N DE PLANES DE ACCI�N','De acuerdo con los resultados del diagn�stico de riesgos viales se han definido planes de acci�n para el INFRAESTRUCTURA SEGURA',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.4','DEFINICI�N DE PLANES DE ACCI�N','De acuerdo con los resultados del diagn�stico de riesgos viales se han definido planes de acci�n para ATENCI�N A V�CTIMAS',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.5','VIABILIDAD DE PLANES DE ACCI�N','Los planes de acci�n propuestos, describen la viabilidad para su implementaci�n',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.1','CRONOGRAMA DE IMPLEMENTACION DE PLANES DE ACCI�N DEL PESV','Existe un cronograma de implementaci�n de planes de acci�n',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.2','CRONOGRAMA DE IMPLEMENTACION DE PLANES DE ACCI�N DEL PESV','El cronograma tiene fechas definidas para la implementaci�n de los planes de acci�n',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.3','CRONOGRAMA DE IMPLEMENTACION DE PLANES DE ACCI�N DEL PESV','Los planes de acci�n tienen responsables definidos dentro de la organizaci�n.',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.4','PRESUPUESTO PARA IMPLEMENTAR EL PESV','Se tiene definido un presupuesto para la implementaci�n de los planes de acci�n, en donde se describa el costo por cada plan de acci�n',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.1','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Se tiene definido indicadores para la implementaci�n de las acciones del PESV (Tabla de indicadores del PESV)',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.2','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est�n definidos los responsables en la organizaci�n para la medici�n de los indicadores planteados dentro del PESV',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.3','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est�n definidas las fuentes y f�rmulas para el c�lculo de los indicadores',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.4','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est�n definidas las metas de los indicadores',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.5','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est� definida la periodicidad para la medici�n de los indicadores',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.6','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Existe indicadores de n�mero de personas capacitadas en seguridad vial',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.7','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Existen indicadores de N�mero de accidentes de tr�nsito',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.8','INDICADORES DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Existen indicadores de mantenimiento preventivo',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.9','AUDITOR�AS DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est�n definidos los planes de acci�n que se van auditar del PESV  en la organizaci�n',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.10','AUDITOR�AS DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est� descrita la metodolog�a para el desarrollo de las auditor�as ',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.11','AUDITOR�AS DEL PLAN ESTRAT�GICO DE SEGURIDAD VIAL','Est�n definidos los periodos sobre los cuales se va a desarrollar las auditor�as',10);

INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.1.1','PERFIL DEL CONDUCTOR','Est� definido el perfil del conductor en funci�n al tipo de veh�culo que va a conducir',11);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.1.2','PROCEDIMIENTO DE SELECCI�N DE CONDUCTORES','Est� documentado el procedimiento de selecci�n de los conductores',11);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.1','EX�MENES M�DICOS','Est� documentado y se han fijado criterios para la realizaci�n de los ex�menes m�dicos a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.2','IDONEIDAD EN EX�MENES M�DICOS','La entidad o persona natural que realiza los examenes m�dicos, cuenta con idoneidad (Es un centro m�dico certificado)',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.3','EX�MENES PSICOSENSO-    METRICOS','Est� documentado y se han fijado criterios para la realizaci�n de los ex�menes psicosensom�tricos a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.4','IDONEIDAD EN EX�MENES PSICOSENSO-    M�TRICOS','La entidad que realiza los ex�menes psicosensom�tricos cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.5','PRUEBA TE�RICA','Est� documentado y se han fijado criterios para la realizaci�n de las pruebas te�ricas a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.6','IDONEIDAD EN EXAMENES TEORICOS','La entidad o persona natural que realiza y califica los ex�menes te�ricos cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.7','PRUEBA PR�CTICA','Est� documentado y se han fijado criterios para la realizaci�n de las pruebas pr�ctica a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.8','IDONEIDAD DE QUIEN REALIZA LAS PRUEBAS PR�CTICAS','La entidad o persona natural que realiza las pruebas pr�cticas a los conductores, cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.9','PRUEBAS PSICOT�CNICAS','Est� documentado y se han fijado criterios para la realizaci�n de las pruebas psicot�cnicas a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.10','IDONEIDAD DE QUIEN REALIZA LAS PRUEBAS PSICOT�CNICAS','La entidad o persona natural que realiza las pruebas psicot�cnicas a los conductores, cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.1','PRUEBAS PREVENTIVAS A CONDUCTORES','Est� definida la frecuencia para la realizaci�n de las pruebas de control a los conductores',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.2','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas m�dicas de control',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.3','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas psicosensom�tricas ',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.4','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas te�ricas',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.5','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas pr�cticas',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.6','IDONEIDAD DE LAS PRUEBAS','Est� definida la idoneidad de las personas o entidades que realizar�an las pruebas de control preventivo a los conductores',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.1','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Existe un programa documentado de capacitaci�n en seguridad vial',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.2','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Existe un cronograma de formaci�n para conductores y personal de la organizaci�n',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.3','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Est� definido el responsable del programa de capacitaci�n.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.4','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas de normatividad',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.5','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas de sensibilizaci�n en los diferentes roles del factor humano',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.6','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas de como actuar frente a accidentes de tr�nsito.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.7','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas basados en el diagn�stico realizado a la empresa',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.8','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','El programa tiene definida la intensidad horaria.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.9','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','El programa tiene temas acordes con los tipos de veh�culos que opera la organizaci�n',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.10','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','El programa tiene incluidos temas para conductores nuevos y conductores antiguos',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.11','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','El programa es exigido tanto a conductores propios como terceros',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.12','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Se tiene establecido un modelo de evaluaci�n de la capacitaci�n',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.13','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores propios)','Est� definido un m�nimo de aciertos sobre las evaluaciones',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.14','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores No propios)','Est�n documentados los requisitos m�nimos exigidos a los conductores no propios sobre el cumplimiento de capacitaci�n. ',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.15','PROGRAMA DE CAPACITACI�N EN SEGURIDAD VIAL - (Conductores No propios)','Esta definida la frecuencia con que se deben presentar evidencias de las capacitaciones de los conductores no propios.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.1','INFORMACI�N DE LOS CONDUCTORES','La empresa documenta y registra un m�nimo de informaci�n de cada uno de los conductores, de acuerdo con lo definido en la Resoluci�n 1565.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.2','INFORMACI�N DE LOS CONDUCTORES','Existe un protocolo de control de documentaci�n de los conductores, propios y tercerizados.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.3','INFORMACI�N DE LOS CONDUCTORES','La informaci�n consignada evidencia el control y trazabilidad de las acciones ejecutadas y definidas dentro del PESV de la empresa.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.4','REPORTE DE COMPARENDOS','Se tiene definida la frecuencia de verificaci�n de infracciones de tr�nsito por parte de los conductores',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.5','REPORTE DE COMPARENDOS','Se cuenta con el registro de infracciones a las normas de tr�nsito por parte de los conductores propios y tercerizados.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.6','REPORTE DE COMPARENDOS','Se tiene establecido un procedimiento en caso de existir comparendos por parte de los conductores propios.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.7','REPORTE DE COMPARENDOS','Existe un responsable para la verificaci�n y aplicaci�n de los procedimientos en caso de presentar comparendos los conductores.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.1','POL�TICAS DE CONTROL DE ALCOHOL Y DROGAS','Se han definido los protocolos para los controles de alcohol y drogas.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.2','POL�TICAS DE CONTROL DE ALCOHOL Y DROGAS','Se definieron responsables, para la realizaci�n de las pruebas.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.3','POL�TICAS DE CONTROL DE ALCOHOL Y DROGAS','Se han definido los mecanismos para la realizaci�n de las pruebas - Procedimiento',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.4','POL�TICAS DE CONTROL DE ALCOHOL Y DROGAS','Se ha definido la idoneidad de quien realiza las pruebas',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.5','POL�TICAS DE CONTROL DE ALCOHOL Y DROGAS','Se ha definido la periodicidad para la realizacion de las pruebas',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.6','POL�TICAS DE CONTROL DE ALCOHOL Y DROGAS','Est�n definidas las acciones a tomar, para aquellos conductores cuyo resultado del ex�men sea positivo.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.7','REGULACI�N DE HORAS DE CONDUCCI�N Y DESCANSO','Existe una pol�tica documentada para la regulaci�n y control de horas m�ximas de conducci�n y descanso',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.8','REGULACI�N DE HORAS DE CONDUCCI�N Y DESCANSO','Se puede evidenciar el reporte de las jornadas laborales o la planificaci�n de su jornada.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.9','REGULACION DE LA VELOCIDAD','La empresa promueve y establece las pol�ticas de aplicaci�n de los l�mites de velocidad de los veh�culos que prestan el servicio a la empresa, propios o tercerizados, para las zonas rurales, urbanas y la definici�n de la velocidad en las rutas internas.�',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.10','REGULACION DE LA VELOCIDAD','Se ha fijado unos l�mites de velocidad para las zonas urbanas y rurales.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.11','REGULACION DE LA VELOCIDAD','Los conductores conocen las pol�ticas de velocidad fijadas en la empresa.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.12','REGULACION DE LA VELOCIDAD','La empresa cuenta con mecanismos de control de velocidad y los monitorea.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.13','POLITICA DE USO DEL CINTURON DE SEGURIDAD','Es manifiesta la obligatoriedad del uso de los cinturones de seguridad y se controla.�',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.14','POLITICA DE USO DEL CINTURON DE SEGURIDAD','Los conductores conocen las pol�ticas de cintur�n fijadas en la empresa',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.15','POLITICA DE USO DEL CINTURON DE SEGURIDAD','La empresa cuenta con mecanismos de control de uso de cintur�n de seguridad.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.16','POL�TICA DE USO DE ELEMENTOS DE PROTECCI�N PERSONAL - La empresa cuenta con mecanismos de control de uso de EPP.','Se ha establecido una pol�tica de uso de elementos de protecci�n personal de acuerdo con el tipo de vehiculo a conducir.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.17','POL�TICA DE USO DE ELEMENTOS DE PROTECCI�N PERSONAL - La empresa cuenta con mecanismos de control de uso de EPP.','Los conductores conocen las pol�ticas de uso de EPP fijadas en la empresa',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.18','POL�TICA DE USO DE EQUIPOS BIDIRECCIONALES','Est� expresa la prohibici�n del uso de equipos bidireccionales durante la conducci�n',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.19','POL�TICA DE USO DE EQUIPOS BIDIRECCIONALES','La empresa cuenta con mecanismos de control de uso de equipos bidireccionales durante la conducci�n.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.20','POL�TICA DE USO DE EQUIPOS BIDIRECCIONALES','Se han fijado sanciones a los conductores que hacen uso de equipos bidireccionales durante la conducci�n.',16);


INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.1','HOJAS DE VIDA DE LOS VEH�CULOS','Se tiene, en f�sico o digital, en la empresa y disponible, la carpeta de cada uno de los veh�culos propios y no propios',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.2','HOJAS DE VIDA DE LOS VEH�CULOS','Se cuenta con informaci�n como - Placas del veh�culo, n�mero de motor, kilometraje � fecha, especificaciones t�cnicas del veh�culo, datos del propietario, datos de la empresa afiliada, etc.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.3','HOJAS DE VIDA DE LOS VEH�CULOS','SOAT � fecha de vigencia, seguros - fechas de vigencia, revisi�n t�cnico mec�nica, reporte de comparendos.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.4','HOJAS DE VIDA DE LOS VEH�CULOS','Reporte de incidentes, reporte de accidentes.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.5','RECOMENDACIONES T�CNICAS DE OPERACIONES DE MANTENIMIENTO','Debe conocerse toda la informaci�n y especificaciones t�cnicas de los veh�culos, incluyendo los sistemas de seguridad activa y pasiva, registradas por escrito en carpeta independiente para cada veh�culo.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.6','CRONOGRAMA DE INTERVENCIONES DE VEH�CULOS PROPIOS','Se cuenta con una programaci�n para las intervenciones programadas de mantenimiento preventivo a los veh�culos',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.7','VERIFICACI�N DE MANTENIMIENTO PARA VEH�CULOS AFILIADOS (TERCEROS)','En el evento de que los veh�culos sean contratados para la prestaci�n del servicio de transporte, la empresa contratante verificar� que la empresa contratista cuente y ejecute el plan.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.8','IDONEIDAD','Se cuenta con instalaciones propias y/o personal id�neo en la empresa para este proceso, o se tiene contrato con un centro mec�nico para ello.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.2.1','REGISTRO','Se llevan registros de los mantenimientos correctivos realizados a los veh�culos',18);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.2.2','PROTOCOLO','En caso de fallas de los veh�culos, se tienen establecidos protocolos de atenci�n.',18);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.2.3','IDONEIDAD ','Se cuenta con instalaciones propias y/o personal id�neo en la empresa para este proceso, o se tiene contrato con un centro mec�nico para ello.',18);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.3.1','INSPECCI�N PREOPERACIONAL','Se han establecido protocolos y formatos de inspecci�n diaria a los veh�culos. ',19);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.3.2','INSPECCI�N PREOPERACIONAL','Los operadores o conductores diligencian diariamente el formato de chequeo preoperativo.',19);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.3.3','INSPECCI�N PREOPERACIONAL','Se adelantan auditor�as verificando el debido diligenciamiento del listado de chequeo.',19);

INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.1','REVISI�N ENTORNO F�SICO DONDE SE OPERA','Existe un plano de las v�as internas con la descripci�n de la revisi�n.',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.2','REVISI�N ENTORNO F�SICO DONDE SE OPERA','Existe conflicto en la circulaci�n entre los veh�culos, peatones, zonas de descargue y parqueaderos',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.3','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Se�alizadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.4','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Demarcadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.5','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Iluminadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.6','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Separadas de las zonas de circulaci�n de los veh�culos',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.7','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Se privilegia el paso de peatones sobre el paso vehicular',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.8','V�AS INTERNAS DE CIRCULACI�N DE LOS VEH�CULOS','Se�alizadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.9','V�AS INTERNAS DE CIRCULACI�N DE LOS VEH�CULOS','Demarcadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.10','V�AS INTERNAS DE CIRCULACI�N DE LOS VEH�CULOS','Iluminadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.11','V�AS INTERNAS DE CIRCULACI�N DE LOS VEH�CULOS','Est� definida la velocidad m�xima de circulaci�n de los veh�culos',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.12','V�AS INTERNAS DE CIRCULACI�N DE LOS VEH�CULOS','Existen elementos en la v�a que favorezcan el control de la velocidad',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.13','PARQUEADEROS INTERNOS','Se�alizados',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.14','PARQUEADEROS INTERNOS','Demarcados',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.15','PARQUEADEROS INTERNOS','Iluminados',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.16','PARQUEADEROS INTERNOS','Est�n definidas zonas de parqueo seg�n el tipo de veh�culo',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.17','MANTENIMIENTO DE SE�ALES','La empresa tiene definida una pol�tica y/o procedimiento para el mantenimiento de las v�as internas y se�alizaci�n.',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.1','ESTUDIO DE RUTAS','Se ha realizado un estudio de rutas, desde el punto de vista de seguridad vial y generado "rutogramas", cuando corresponda.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.2','ESTUDIO DE RUTAS','Se han identificado los puntos cr�ticos y establecido las estrategias de prevenci�n frente a los mismos.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.3','POL�TICAS DE ADMINISTRACI�N DE RUTAS','Se planifica el desplazamiento del personal ',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.4','POL�TICAS DE ADMINISTRACI�N DE RUTAS','Se tienen definidos los horarios de llegada y salida en la empresa y las jornadas de trabajo.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.5','APOYO TECNOL�GICO','Se hace monitoreo y retroalimentaci�n en los comportamientos viales.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.6','APOYO TECNOL�GICO','El monitoreo de las tecnolog�as usadas permite generar acciones preventivas.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.7','POL�TICAS DE SOCIALIZACI�N Y ACTUALIZACI�N DE INFORMACI�N: para todo el personal que haga parte de su operaci�n, informando sobre los factores que debe tener en cuenta a la hora de realizar los desplazamientos en las v�as internas y externas: ','La empresa ha establecido mecanismos de socializaci�n e informaci�n preventiva y ha desplegado la misma en toda la organizaci�n.',21);

INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.1.1','PROTOCOLOS','Existen protocolos de atenci�n a v�ctimas en caso de accidentes de tr�nsito. ',22);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.1.2','DIVULGACI�N DE PROTOCOLOS','Los empleados conocen el procedimiento a seguir en los casos en que ocurra un accidente de tr�nsito.',22);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.1','INFORMACI�N DOCUMENTADA DE ACCIDENTES DE TR�NSITO','Existe registros sobre los accidentes de transito que ha tenido la empresa',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.2','AN�LISIS DE ACCIDENTES DE TR�NSITO','Existen variables de an�lisis en los casos de accidentes de tr�nsito (gravedad, hist�rico de datos, etc.)',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.3','LECCIONES APRENDIDAS','Se ha hecho divulgaci�n de lecciones aprendidas de los accidentes de tr�nsito ocurridos',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.4','FUENTE DE INFORMACI�N','Esta definida la fuente de los registros para obtener informaci�n sobre los accidentes de tr�nsito en la organizaci�n',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.5','PROCEDIMIENTO PARA LA INVESTIGACI�N DE A.T.','Est� definido un procedimiento para la investigaci�n de accidentes de tr�nsito.',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.6','INDICADORES','Se elaboran indicadores de accidentes de tr�nsito.',23);
