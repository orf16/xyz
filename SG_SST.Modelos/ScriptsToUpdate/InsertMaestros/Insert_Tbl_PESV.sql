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


INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('FORTALECIMIENTO EN LA GESTIÓN INSTITUCIONAL',1,0.3);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('COMPORTAMIENTO HUMANO',1,0.3);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('VEHÍCULOS SEGUROS',1,0.2);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('INFRAESTRUCTURA SEGURA ',1,0.1);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('ATENCIÓN A VÍCTIMAS',1,0.1);
INSERT INTO Tbl_SegVialPilar(Descripcion,Version,Valor_Ponderado)VALUES ('VALORES AGREGADOS O INNOVACIONES',1,0.05);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'1.1','OBJETIVOS DEL PESV',3,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'1.2','COMITÉ DE SEGURIDAD VIAL -  Mecanismo de coordinación entre todos los involucrados y cuyo objetivo será planificar, diseñar, implementar y medir las acciones, para lograr los objetivos a favor de la seguridad vial.',15,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (3,'1.3','RESPONSABLE DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL',4,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (4,'1.4','POLÍTICA DE SEGURIDAD VIAL ',10,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (5,'1.5','DIVULGACIÓN DE LA POLÍTICA DE SEGURIDAD VIAL',5,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (6,'1.6','DIAGNÓSTICO - CARACTERIZACIÓN DE LA EMPRESA',10,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (7,'1.7','DIAGNÓSTICO - RIESGOS VIALES',20,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (8,'1.8','PLANES DE ACCIÓN DE RIESGOS VIALES',15,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (9,'1.9','IMPLEMENTACION DE ACCIONES DEL PESV',10,1);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (10,'1.10','SEGUIMIENTO Y EVALUACIÓN DE PLANES DE ACCIÓN DEL PESV',8,1);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'2.1','PROCEDIMIENTO DE SELECCIÓN DE CONDUCTORES',10,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'2.2','PRUEBAS DE INGRESO DE CONDUCTORES',20,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (3,'2.3','PRUEBAS DE CONTROL PREVENTIVO DE CONDUCTORES',20,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (4,'2.4','CAPACITACION EN SEGURIDAD VIAL',20,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (5,'2.5','CONTROL DE DOCUMENTACION DE CONDUCTORES',10,2);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (6,'2.6','POLITICAS DE REGULACION DE LA EMPRESA',20,2);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'3.1','MANTENIMIENTO PREVENTIVO',50,3);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'3.2','MANTENIMIENTO CORRECTIVO',30,3);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (3,'3.3','CHEQUEO PREOPERACIONAL',20,3);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'4.1','RUTAS INTERNAS - Vías internas de la empresa, en donde circulan los vehículos',35,4);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'4.2','RUTAS EXTERNAS: Desplazamiento fuera del entorno fisico de la empresa',65,4);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'5.1','ATENCIÓN A VICTIMAS',20,5);
INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (2,'5.2','INVESTIGACIÓN DE ACCIDENTES DE TRÁNSITO',80,5);

INSERT INTO Tbl_SegVialParametro(Numero,Numeral,ParametroDef,Valor_Parametro,Fk_Id_SegVialPilar)VALUES (1,'6.1','INVESTIGACIÓN DE ACCIDENTES DE TRÁNSITO',100,6);




INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.1.1','OBJETIVO GENERAL DEL PESV','Se ha fijado un objetivo claro, concreto y realizable, así como su alcance y visión',1);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.1.2','OBJETIVOS ESPECÍFICOS DEL PESV','Los objetivos específicos se ajustan al objetivo general y de realizarse se cumpliría con los propósitos',1);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.1.3','DIRECTRICES DE LA ALTA DIRECCIÓN','Existe un documento que indique el compromiso de las directivas, para el desarrollo del PESV',1);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.1','ACTA DE COMITÉ DE SEGURIDAD VIAL','Existe un acta de conformación del comité de S.V.',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.2','OBJETIVOS DEL COMITÉ DE SEGURIDAD VIAL','Está definido el objetivo del comité de seguridad vial',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.3','INTEGRANTES DEL COMITÉ DE S.V.','El comité ha sido definido por la alta dirección',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.4','ROLES Y FUNCIONES DE LOS INTEGRANTES','Los integrantes del comité tienen relación con las labores y planes de acción inherentes al PESV',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.2.5','FRECUENCIA DE REUNIONES DEL COMITÉ DE SV','Está definida la frecuencia de las reuniones del comité de S.V.',2);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.3.1','RESPONSABLE DEL PESV','Se designó un responsable del proceso de elaboración y seguimiento del PESV, indicando el cargo dentro de la organización.',3);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.3.2','IDONEIDAD DEL RESPONSABLE DEL PESV','El responsable es idóneo para el desarrollo, implementación y seguimiento del PESV  y todas las acciones contempladas en este.',3);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.1','ESTÁ DOCUMENTADA LA POLÍTICA DE SEGURIDAD VIAL','Existe un documento que permita identificar la política de Seguridad vial de la empresa',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.2','POLÍTICA DE SEGURIDAD VIAL','Existe política de seguridad vial documentada ',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.3','POLÍTICA DE SEGURIDAD VIAL','Se adecuada al propósito de la organización.',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.4','POLÍTICA DE SEGURIDAD VIAL','Proporciona un marco de referencia para el establecimiento de los objetivos y de las metas del PESV',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.4.5','POLÍTICA DE SEGURIDAD VIAL','Incluye el compromiso de cumplir los requisitos aplicables y la mejora continua.',4);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.5.1','DIVULGACIÓN DE LA POLÍTICA DE SEGURIDAD VIAL','Existe evidencia de su divulgación, como página web de la compañía, retablos en las instalaciones de la compañía u otros?',5);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.5.2','DIVULGACIÓN DE LA POLÍTICA DE SEGURIDAD VIAL','Existe evidencia de que se ha informado al personal sobre el PESV y la política de seguridad vial?',5);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.1','CARACTERÍSTICAS DE LA EMPRESA','Está descrita la actividad económica que realiza la empresa',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.2','CARACTERÍSTICAS DE LA EMPRESA','Está documentado el análisis de la empresa, su contexto, actividades, su personal, desplazamientos, etc. ',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.3','CARACTERÍSTICAS DE LA EMPRESA','Están definidos los servicios que presta la compañía',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.4','CARACTERÍSTICAS DE LA EMPRESA','Está definida la población de personal que hace parte de la compañía',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.5','CARACTERÍSTICAS DE LA EMPRESA','Están definidos los vehículos automotores y no automotores puestos al servicio de la compañía.',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.6','CARACTERÍSTICAS DE LA EMPRESA','Están definidas las ciudades de operación de la organización',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.7','CARACTERÍSTICAS DE LA EMPRESA','Están documentados los mecanismos de contratación de los vehículos.',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.6.8','CARACTERÍSTICAS DE LA EMPRESA','Están documentados los mecanismos de contratación de conductores.',6);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.1','ENCUESTA / INSTRUMENTO PARA DETERMINAR EL RIESGO VIAL','Se diseñó una encuesta u otro instrumento o mecanismo objetivo, para el levantamiento de información del riesgo vial.',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.2','APLICACIÓN DE LA ENCUESTA','Se ha aplicado la encuesta ',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.3','APLICACIÓN DE LA ENCUESTA','Se han tenido en cuenta los riesgos de la operación in itinere y en misión',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.4','CONSOLIDACIÓN Y ANÁLISIS DE LA ENCUESTA ','Se han consolidado los resultados de la encuesta y hecho un análisis de los resultados',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.5','DEFINICIÓN DE RIESGOS VIALES DE LA EMPRESA','Se han definido riesgos viales para el personal de la empresa, dependiendo de su rol en la vía (Peatón, pasajero, ciclista, conductor)',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.6','CALIFICACIÓN Y CLASIFICACIÓN DE RIESGOS VIALES','Se han calificado los riesgos viales identificados a través de la encuesta',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.7.7','CALIFICACIÓN Y CLASIFICACIÓN DE RIESGOS VIALES','La calificación de los riesgos se ha hecho basado en alguna norma o estándar ',7);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.1','DEFINICIÓN DE PLANES DE ACCIÓN','De acuerdo con los resultados del diagnóstico de riesgos viales, se han definido planes de acción para el FACTOR HUMANO',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.2','DEFINICIÓN DE PLANES DE ACCIÓN','De acuerdo con los resultados del diagnóstico de riesgos viales se han definido planes de  acción para el FACTOR VEHICULOS',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.3','DEFINICIÓN DE PLANES DE ACCIÓN','De acuerdo con los resultados del diagnóstico de riesgos viales se han definido planes de acción para el INFRAESTRUCTURA SEGURA',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.4','DEFINICIÓN DE PLANES DE ACCIÓN','De acuerdo con los resultados del diagnóstico de riesgos viales se han definido planes de acción para ATENCIÓN A VÍCTIMAS',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.8.5','VIABILIDAD DE PLANES DE ACCIÓN','Los planes de acción propuestos, describen la viabilidad para su implementación',8);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.1','CRONOGRAMA DE IMPLEMENTACION DE PLANES DE ACCIÓN DEL PESV','Existe un cronograma de implementación de planes de acción',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.2','CRONOGRAMA DE IMPLEMENTACION DE PLANES DE ACCIÓN DEL PESV','El cronograma tiene fechas definidas para la implementación de los planes de acción',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.3','CRONOGRAMA DE IMPLEMENTACION DE PLANES DE ACCIÓN DEL PESV','Los planes de acción tienen responsables definidos dentro de la organización.',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.9.4','PRESUPUESTO PARA IMPLEMENTAR EL PESV','Se tiene definido un presupuesto para la implementación de los planes de acción, en donde se describa el costo por cada plan de acción',9);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.1','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Se tiene definido indicadores para la implementación de las acciones del PESV (Tabla de indicadores del PESV)',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.2','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Están definidos los responsables en la organización para la medición de los indicadores planteados dentro del PESV',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.3','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Están definidas las fuentes y fórmulas para el cálculo de los indicadores',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.4','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Están definidas las metas de los indicadores',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.5','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Está definida la periodicidad para la medición de los indicadores',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.6','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Existe indicadores de número de personas capacitadas en seguridad vial',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.7','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Existen indicadores de Número de accidentes de tránsito',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.8','INDICADORES DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Existen indicadores de mantenimiento preventivo',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.9','AUDITORÍAS DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Están definidos los planes de acción que se van auditar del PESV  en la organización',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.10','AUDITORÍAS DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Está descrita la metodología para el desarrollo de las auditorías ',10);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('1.10.11','AUDITORÍAS DEL PLAN ESTRATÉGICO DE SEGURIDAD VIAL','Están definidos los periodos sobre los cuales se va a desarrollar las auditorías',10);

INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.1.1','PERFIL DEL CONDUCTOR','Está definido el perfil del conductor en función al tipo de vehículo que va a conducir',11);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.1.2','PROCEDIMIENTO DE SELECCIÓN DE CONDUCTORES','Está documentado el procedimiento de selección de los conductores',11);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.1','EXÁMENES MÉDICOS','Está documentado y se han fijado criterios para la realización de los exámenes médicos a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.2','IDONEIDAD EN EXÁMENES MÉDICOS','La entidad o persona natural que realiza los examenes médicos, cuenta con idoneidad (Es un centro médico certificado)',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.3','EXÁMENES PSICOSENSO-    METRICOS','Está documentado y se han fijado criterios para la realización de los exámenes psicosensométricos a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.4','IDONEIDAD EN EXÁMENES PSICOSENSO-    MÉTRICOS','La entidad que realiza los exámenes psicosensométricos cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.5','PRUEBA TEÓRICA','Está documentado y se han fijado criterios para la realización de las pruebas teóricas a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.6','IDONEIDAD EN EXAMENES TEORICOS','La entidad o persona natural que realiza y califica los exámenes teóricos cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.7','PRUEBA PRÁCTICA','Está documentado y se han fijado criterios para la realización de las pruebas práctica a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.8','IDONEIDAD DE QUIEN REALIZA LAS PRUEBAS PRÁCTICAS','La entidad o persona natural que realiza las pruebas prácticas a los conductores, cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.9','PRUEBAS PSICOTÉCNICAS','Está documentado y se han fijado criterios para la realización de las pruebas psicotécnicas a los conductores',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.2.10','IDONEIDAD DE QUIEN REALIZA LAS PRUEBAS PSICOTÉCNICAS','La entidad o persona natural que realiza las pruebas psicotécnicas a los conductores, cuenta con idoneidad ',12);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.1','PRUEBAS PREVENTIVAS A CONDUCTORES','Está definida la frecuencia para la realización de las pruebas de control a los conductores',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.2','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas médicas de control',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.3','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas psicosensométricas ',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.4','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas teóricas',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.5','PRUEBAS PREVENTIVAS A CONDUCTORES','Pruebas prácticas',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.3.6','IDONEIDAD DE LAS PRUEBAS','Está definida la idoneidad de las personas o entidades que realizarían las pruebas de control preventivo a los conductores',13);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.1','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Existe un programa documentado de capacitación en seguridad vial',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.2','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Existe un cronograma de formación para conductores y personal de la organización',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.3','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Está definido el responsable del programa de capacitación.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.4','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas de normatividad',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.5','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas de sensibilización en los diferentes roles del factor humano',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.6','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas de como actuar frente a accidentes de tránsito.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.7','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Incluye temas basados en el diagnóstico realizado a la empresa',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.8','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','El programa tiene definida la intensidad horaria.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.9','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','El programa tiene temas acordes con los tipos de vehículos que opera la organización',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.10','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','El programa tiene incluidos temas para conductores nuevos y conductores antiguos',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.11','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','El programa es exigido tanto a conductores propios como terceros',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.12','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Se tiene establecido un modelo de evaluación de la capacitación',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.13','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores propios)','Está definido un mínimo de aciertos sobre las evaluaciones',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.14','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores No propios)','Están documentados los requisitos mínimos exigidos a los conductores no propios sobre el cumplimiento de capacitación. ',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.4.15','PROGRAMA DE CAPACITACIÓN EN SEGURIDAD VIAL - (Conductores No propios)','Esta definida la frecuencia con que se deben presentar evidencias de las capacitaciones de los conductores no propios.',14);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.1','INFORMACIÓN DE LOS CONDUCTORES','La empresa documenta y registra un mínimo de información de cada uno de los conductores, de acuerdo con lo definido en la Resolución 1565.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.2','INFORMACIÓN DE LOS CONDUCTORES','Existe un protocolo de control de documentación de los conductores, propios y tercerizados.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.3','INFORMACIÓN DE LOS CONDUCTORES','La información consignada evidencia el control y trazabilidad de las acciones ejecutadas y definidas dentro del PESV de la empresa.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.4','REPORTE DE COMPARENDOS','Se tiene definida la frecuencia de verificación de infracciones de tránsito por parte de los conductores',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.5','REPORTE DE COMPARENDOS','Se cuenta con el registro de infracciones a las normas de tránsito por parte de los conductores propios y tercerizados.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.6','REPORTE DE COMPARENDOS','Se tiene establecido un procedimiento en caso de existir comparendos por parte de los conductores propios.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.5.7','REPORTE DE COMPARENDOS','Existe un responsable para la verificación y aplicación de los procedimientos en caso de presentar comparendos los conductores.',15);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.1','POLÍTICAS DE CONTROL DE ALCOHOL Y DROGAS','Se han definido los protocolos para los controles de alcohol y drogas.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.2','POLÍTICAS DE CONTROL DE ALCOHOL Y DROGAS','Se definieron responsables, para la realización de las pruebas.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.3','POLÍTICAS DE CONTROL DE ALCOHOL Y DROGAS','Se han definido los mecanismos para la realización de las pruebas - Procedimiento',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.4','POLÍTICAS DE CONTROL DE ALCOHOL Y DROGAS','Se ha definido la idoneidad de quien realiza las pruebas',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.5','POLÍTICAS DE CONTROL DE ALCOHOL Y DROGAS','Se ha definido la periodicidad para la realizacion de las pruebas',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.6','POLÍTICAS DE CONTROL DE ALCOHOL Y DROGAS','Están definidas las acciones a tomar, para aquellos conductores cuyo resultado del exámen sea positivo.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.7','REGULACIÓN DE HORAS DE CONDUCCIÓN Y DESCANSO','Existe una política documentada para la regulación y control de horas máximas de conducción y descanso',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.8','REGULACIÓN DE HORAS DE CONDUCCIÓN Y DESCANSO','Se puede evidenciar el reporte de las jornadas laborales o la planificación de su jornada.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.9','REGULACION DE LA VELOCIDAD','La empresa promueve y establece las políticas de aplicación de los límites de velocidad de los vehículos que prestan el servicio a la empresa, propios o tercerizados, para las zonas rurales, urbanas y la definición de la velocidad en las rutas internas. ',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.10','REGULACION DE LA VELOCIDAD','Se ha fijado unos límites de velocidad para las zonas urbanas y rurales.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.11','REGULACION DE LA VELOCIDAD','Los conductores conocen las políticas de velocidad fijadas en la empresa.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.12','REGULACION DE LA VELOCIDAD','La empresa cuenta con mecanismos de control de velocidad y los monitorea.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.13','POLITICA DE USO DEL CINTURON DE SEGURIDAD','Es manifiesta la obligatoriedad del uso de los cinturones de seguridad y se controla. ',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.14','POLITICA DE USO DEL CINTURON DE SEGURIDAD','Los conductores conocen las políticas de cinturón fijadas en la empresa',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.15','POLITICA DE USO DEL CINTURON DE SEGURIDAD','La empresa cuenta con mecanismos de control de uso de cinturón de seguridad.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.16','POLÍTICA DE USO DE ELEMENTOS DE PROTECCIÓN PERSONAL - La empresa cuenta con mecanismos de control de uso de EPP.','Se ha establecido una política de uso de elementos de protección personal de acuerdo con el tipo de vehiculo a conducir.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.17','POLÍTICA DE USO DE ELEMENTOS DE PROTECCIÓN PERSONAL - La empresa cuenta con mecanismos de control de uso de EPP.','Los conductores conocen las políticas de uso de EPP fijadas en la empresa',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.18','POLÍTICA DE USO DE EQUIPOS BIDIRECCIONALES','Está expresa la prohibición del uso de equipos bidireccionales durante la conducción',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.19','POLÍTICA DE USO DE EQUIPOS BIDIRECCIONALES','La empresa cuenta con mecanismos de control de uso de equipos bidireccionales durante la conducción.',16);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('2.6.20','POLÍTICA DE USO DE EQUIPOS BIDIRECCIONALES','Se han fijado sanciones a los conductores que hacen uso de equipos bidireccionales durante la conducción.',16);


INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.1','HOJAS DE VIDA DE LOS VEHÍCULOS','Se tiene, en físico o digital, en la empresa y disponible, la carpeta de cada uno de los vehículos propios y no propios',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.2','HOJAS DE VIDA DE LOS VEHÍCULOS','Se cuenta con información como - Placas del vehículo, número de motor, kilometraje – fecha, especificaciones técnicas del vehículo, datos del propietario, datos de la empresa afiliada, etc.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.3','HOJAS DE VIDA DE LOS VEHÍCULOS','SOAT – fecha de vigencia, seguros - fechas de vigencia, revisión técnico mecánica, reporte de comparendos.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.4','HOJAS DE VIDA DE LOS VEHÍCULOS','Reporte de incidentes, reporte de accidentes.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.5','RECOMENDACIONES TÉCNICAS DE OPERACIONES DE MANTENIMIENTO','Debe conocerse toda la información y especificaciones técnicas de los vehículos, incluyendo los sistemas de seguridad activa y pasiva, registradas por escrito en carpeta independiente para cada vehículo.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.6','CRONOGRAMA DE INTERVENCIONES DE VEHÍCULOS PROPIOS','Se cuenta con una programación para las intervenciones programadas de mantenimiento preventivo a los vehículos',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.7','VERIFICACIÓN DE MANTENIMIENTO PARA VEHÍCULOS AFILIADOS (TERCEROS)','En el evento de que los vehículos sean contratados para la prestación del servicio de transporte, la empresa contratante verificará que la empresa contratista cuente y ejecute el plan.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.1.8','IDONEIDAD','Se cuenta con instalaciones propias y/o personal idóneo en la empresa para este proceso, o se tiene contrato con un centro mecánico para ello.',17);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.2.1','REGISTRO','Se llevan registros de los mantenimientos correctivos realizados a los vehículos',18);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.2.2','PROTOCOLO','En caso de fallas de los vehículos, se tienen establecidos protocolos de atención.',18);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.2.3','IDONEIDAD ','Se cuenta con instalaciones propias y/o personal idóneo en la empresa para este proceso, o se tiene contrato con un centro mecánico para ello.',18);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.3.1','INSPECCIÓN PREOPERACIONAL','Se han establecido protocolos y formatos de inspección diaria a los vehículos. ',19);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.3.2','INSPECCIÓN PREOPERACIONAL','Los operadores o conductores diligencian diariamente el formato de chequeo preoperativo.',19);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('3.3.3','INSPECCIÓN PREOPERACIONAL','Se adelantan auditorías verificando el debido diligenciamiento del listado de chequeo.',19);

INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.1','REVISIÓN ENTORNO FÍSICO DONDE SE OPERA','Existe un plano de las vías internas con la descripción de la revisión.',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.2','REVISIÓN ENTORNO FÍSICO DONDE SE OPERA','Existe conflicto en la circulación entre los vehículos, peatones, zonas de descargue y parqueaderos',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.3','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Señalizadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.4','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Demarcadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.5','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Iluminadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.6','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Separadas de las zonas de circulación de los vehículos',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.7','DESPLAZAMIENTO EN LAS ZONAS PEATONALES DE LAS INSTALACIONES','Se privilegia el paso de peatones sobre el paso vehicular',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.8','VÍAS INTERNAS DE CIRCULACIÓN DE LOS VEHÍCULOS','Señalizadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.9','VÍAS INTERNAS DE CIRCULACIÓN DE LOS VEHÍCULOS','Demarcadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.10','VÍAS INTERNAS DE CIRCULACIÓN DE LOS VEHÍCULOS','Iluminadas',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.11','VÍAS INTERNAS DE CIRCULACIÓN DE LOS VEHÍCULOS','Está definida la velocidad máxima de circulación de los vehículos',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.12','VÍAS INTERNAS DE CIRCULACIÓN DE LOS VEHÍCULOS','Existen elementos en la vía que favorezcan el control de la velocidad',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.13','PARQUEADEROS INTERNOS','Señalizados',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.14','PARQUEADEROS INTERNOS','Demarcados',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.15','PARQUEADEROS INTERNOS','Iluminados',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.16','PARQUEADEROS INTERNOS','Están definidas zonas de parqueo según el tipo de vehículo',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.1.17','MANTENIMIENTO DE SEÑALES','La empresa tiene definida una política y/o procedimiento para el mantenimiento de las vías internas y señalización.',20);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.1','ESTUDIO DE RUTAS','Se ha realizado un estudio de rutas, desde el punto de vista de seguridad vial y generado "rutogramas", cuando corresponda.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.2','ESTUDIO DE RUTAS','Se han identificado los puntos críticos y establecido las estrategias de prevención frente a los mismos.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.3','POLÍTICAS DE ADMINISTRACIÓN DE RUTAS','Se planifica el desplazamiento del personal ',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.4','POLÍTICAS DE ADMINISTRACIÓN DE RUTAS','Se tienen definidos los horarios de llegada y salida en la empresa y las jornadas de trabajo.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.5','APOYO TECNOLÓGICO','Se hace monitoreo y retroalimentación en los comportamientos viales.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.6','APOYO TECNOLÓGICO','El monitoreo de las tecnologías usadas permite generar acciones preventivas.',21);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('4.2.7','POLÍTICAS DE SOCIALIZACIÓN Y ACTUALIZACIÓN DE INFORMACIÓN: para todo el personal que haga parte de su operación, informando sobre los factores que debe tener en cuenta a la hora de realizar los desplazamientos en las vías internas y externas: ','La empresa ha establecido mecanismos de socialización e información preventiva y ha desplegado la misma en toda la organización.',21);

INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.1.1','PROTOCOLOS','Existen protocolos de atención a víctimas en caso de accidentes de tránsito. ',22);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.1.2','DIVULGACIÓN DE PROTOCOLOS','Los empleados conocen el procedimiento a seguir en los casos en que ocurra un accidente de tránsito.',22);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.1','INFORMACIÓN DOCUMENTADA DE ACCIDENTES DE TRÁNSITO','Existe registros sobre los accidentes de transito que ha tenido la empresa',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.2','ANÁLISIS DE ACCIDENTES DE TRÁNSITO','Existen variables de análisis en los casos de accidentes de tránsito (gravedad, histórico de datos, etc.)',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.3','LECCIONES APRENDIDAS','Se ha hecho divulgación de lecciones aprendidas de los accidentes de tránsito ocurridos',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.4','FUENTE DE INFORMACIÓN','Esta definida la fuente de los registros para obtener información sobre los accidentes de tránsito en la organización',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.5','PROCEDIMIENTO PARA LA INVESTIGACIÓN DE A.T.','Está definido un procedimiento para la investigación de accidentes de tránsito.',23);
INSERT INTO Tbl_SegVialDetalle(Numeral,VariableDesc,CriterioAval,Fk_Id_SegVialPilar)VALUES ('5.2.6','INDICADORES','Se elaboran indicadores de accidentes de tránsito.',23);
