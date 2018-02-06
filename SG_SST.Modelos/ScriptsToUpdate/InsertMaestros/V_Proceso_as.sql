CREATE VIEW [dbo].[V_Proceso_as]
AS
SELECT        dbo.Tbl_Proceso.Descripcion_Proceso, CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 1 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) 
                         ELSE '0' END AS Enero, CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 2 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Febrero, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 3 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Marzo, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 4 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 5 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Mayo, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 6 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Junio, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 7 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 8 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Agosto, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 9 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Septiembre, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 10 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Octubre, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 11 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(dbo.Tbl_Ausencias.Fecha_Fin) = 12 THEN SUM(dbo.Tbl_Ausencias.DiasAusencia) ELSE '0' END AS Diciembre, 
                         dbo.Tbl_Ausencias.NitEmpresa
FROM            dbo.Tbl_Municipio INNER JOIN
                         dbo.Tbl_Departamento ON dbo.Tbl_Municipio.Fk_Nombre_Departamento = dbo.Tbl_Departamento.Pk_Id_Departamento INNER JOIN
                         dbo.Tbl_Ausencias INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Ausencias.NitEmpresa = dbo.Tbl_Empresa.Nit_Empresa INNER JOIN
                         dbo.Tbl_Contingencias ON dbo.Tbl_Ausencias.FK_Id_Contingencia = dbo.Tbl_Contingencias.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tipo_Contigencias ON dbo.Tbl_Contingencias.FK_Tipo_Contingencia = dbo.Tbl_Tipo_Contigencias.PK_Id_Tipo_Contigencia INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Ausencias.FK_Id_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso ON 
                         dbo.Tbl_Municipio.Pk_Id_Municipio = dbo.Tbl_Ausencias.FK_Id_Municipio AND 
                         dbo.Tbl_Departamento.Pk_Id_Departamento = dbo.Tbl_Ausencias.FK_Id_Departamento LEFT OUTER JOIN
                         dbo.Tbl_Empresas_Usuarias ON dbo.Tbl_Ausencias.FK_Id_EmpresaUsuaria = dbo.Tbl_Empresas_Usuarias.PK_Id_Empresa_Usuaria
GROUP BY MONTH(dbo.Tbl_Ausencias.FechaInicio), MONTH(dbo.Tbl_Ausencias.Fecha_Fin), dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_Ausencias.NitEmpresa
GO


