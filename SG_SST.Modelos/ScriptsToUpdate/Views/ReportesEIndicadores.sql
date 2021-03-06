USE [SGSST]
GO
/****** Object:  View [dbo].[V_ACCIONESCORRECTIVASPREVENTIVAS]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_ACCIONESCORRECTIVASPREVENTIVAS]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Acciones.Fecha_dil AS [Fecha de Diligenciamiento], 
                         dbo.Tbl_Acciones.Fecha_hall AS [Fecha de Hallazgo], dbo.Tbl_Acciones.Origen AS [Origen del Hallazgo], 
                         dbo.Tbl_Acciones.Otro_Origen AS [Otro Origen del Hallazgo], dbo.Tbl_Acciones.Estado, dbo.Tbl_Acciones.Halla_Nombre, dbo.Tbl_ActividadAccion.Actividad, 
                         dbo.Tbl_Acciones.Tipo, dbo.Tbl_Acciones.Halla_TipoDoc, dbo.Tbl_Acciones.Halla_Cargo, dbo.Tbl_Analisis.ValorTxt AS [Valor Análisis], 
                         dbo.Tbl_Hallazgo.Halla_Proceso AS [Hallazgo Proceso], dbo.Tbl_Hallazgo.Halla_Norma AS [Hallazgo Norma], dbo.Tbl_Hallazgo.Halla_Numeral, 
                         dbo.Tbl_Acciones.Verificacion, dbo.Tbl_Acciones.Correccion, dbo.Tbl_Acciones.Cambio_Doc AS [Cambios en la documentación del SIG], 
                         dbo.Tbl_Acciones.Des_Cambio_Doc AS [Descripción Cambios en la documentación del SIG], dbo.Tbl_Seguimiento.Fecha_Seg, dbo.Tbl_Seguimiento.Observaciones, 
                         dbo.Tbl_Acciones.Eficacia, dbo.Tbl_Acciones.Nombre_Auditor, dbo.Tbl_Acciones.Cargo_Auditor, dbo.Tbl_Acciones.NombreArchivoAuditor, 
                         dbo.Tbl_Acciones.RutaArchivoAuditor
FROM            dbo.Tbl_Hallazgo RIGHT OUTER JOIN
                         dbo.Tbl_Analisis INNER JOIN
                         dbo.Tbl_Acciones INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Acciones.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_ActividadAccion ON dbo.Tbl_Acciones.Pk_Id_Accion = dbo.Tbl_ActividadAccion.Fk_Id_Accion INNER JOIN
                         dbo.Tbl_Seguimiento ON dbo.Tbl_Acciones.Pk_Id_Accion = dbo.Tbl_Seguimiento.Fk_Id_Accion ON 
                         dbo.Tbl_Analisis.Fk_Id_Accion = dbo.Tbl_Acciones.Pk_Id_Accion INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa AND dbo.Tbl_Acciones.Halla_Sede = dbo.Tbl_Sede.Pk_Id_Sede ON 
                         dbo.Tbl_Hallazgo.Fk_Id_Accion = dbo.Tbl_Acciones.Pk_Id_Accion





GO
/****** Object:  View [dbo].[V_ADQUISCIONESDEBIENESOCONTRATACIÓN]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[V_ADQUISCIONESDEBIENESOCONTRATACIÓN]
AS
SELECT DISTINCT 
                         dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_ProveedorContratista.Nombre_ProveedorContratista, 
                         dbo.Tbl_ProveedorContratista.FrecuenciaEval, dbo.Tbl_ProveedorContratista.CalificacionHist, dbo.Tbl_CalificacionProveedor.Fecha_Calificacion, 
                         dbo.Tbl_CalificacionProveedor.ResultadoCalificacion, dbo.Tbl_ServicioOProducto.Nombre_ServicioOProducto, dbo.Tbl_CriterioSST.Criterio
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_ServicioOProducto ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_ServicioOProducto.FK_Empresa INNER JOIN
                         dbo.Tbl_CalificacionProveedor INNER JOIN
                         dbo.Tbl_Proveedor_Por_NumeroCalificacion ON 
                         dbo.Tbl_CalificacionProveedor.PK_CalificacionProveedor = dbo.Tbl_Proveedor_Por_NumeroCalificacion.Fk_Id_CalificacionProveedor INNER JOIN
                         dbo.Tbl_Proveedor_ProductoPorCriterio ON 
                         dbo.Tbl_Proveedor_Por_NumeroCalificacion.PK_ProveedorPorNumeroCalificacion = dbo.Tbl_Proveedor_ProductoPorCriterio.Fk_Id_ProveedorPorNumeroCalificacion INNER
                          JOIN
                         dbo.Tbl_ProveedorContratista ON 
                         dbo.Tbl_Proveedor_Por_NumeroCalificacion.Fk_Id_ProveedorContratista = dbo.Tbl_ProveedorContratista.PK_ProveedorContratista INNER JOIN
                         dbo.Tbl_Proveedor_Por_Producto ON dbo.Tbl_ProveedorContratista.PK_ProveedorContratista = dbo.Tbl_Proveedor_Por_Producto.Fk_Id_ProveedorContratista ON 
                         dbo.Tbl_ServicioOProducto.PK_ServicioOProducto = dbo.Tbl_Proveedor_Por_Producto.Fk_Id_ServicioOProducto INNER JOIN
                         dbo.Tbl_Producto_Por_Criterio ON dbo.Tbl_ServicioOProducto.PK_ServicioOProducto = dbo.Tbl_Producto_Por_Criterio.Fk_Id_ServicioOProducto AND 
                         dbo.Tbl_Proveedor_ProductoPorCriterio.Fk_Id_ProductoPorCriterio = dbo.Tbl_Producto_Por_Criterio.Id_Pk_ProductoPorCriterio INNER JOIN
                         dbo.Tbl_CriterioSST ON dbo.Tbl_Producto_Por_Criterio.Fk_Id__CriterioSST = dbo.Tbl_CriterioSST.PK_CriterioSST


GO
/****** Object:  View [dbo].[V_añoscomuni]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_añoscomuni]
AS
SELECT   distinct     NitEmpresa,
                         year(CONVERT(date, CONVERT(varchar(10), Tbl_ComunicacionesExternas.FechaCreacion, 101), 103)) AS año, 
                         'EXTERNO' AS TIPO
FROM            Tbl_ComunicacionesExternas INNER JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicacionesExternas.PK_Id_Comunicado = Tbl_ComunicacionesLog.fk_id_comunicaciones

UNION
SELECT      distinct     NitEmpresa,  year(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS año,   'INTERNO' AS TIPO
FROM            Tbl_ComunicacionesInternas


UNION
SELECT     distinct   NitEmpresa,year(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS año,  'APP' AS TIPO
FROM            Tbl_ComunicadosAPP
GO
/****** Object:  View [dbo].[V_Ausencias_Proceso]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





alter VIEW [dbo].[V_Ausencias_Proceso]
AS
SELECT        c.PK_Id_Contingencia AS idContigencia, p.Descripcion_Proceso AS Descripcion, a.FechaInicio, a.Fecha_Fin AS FechaFin, 

							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 1 THEN COUNT(*) ELSE '0' END AS Enero, 
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 2 THEN COUNT(*) ELSE '0' END AS Febrero,
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 3 THEN COUNT(*) ELSE '0' END AS Marzo, 
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 4 THEN COUNT(*) ELSE '0' END AS Abril,
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 5 THEN COUNT(*) ELSE '0' END AS Mayo, 
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 6 THEN COUNT(*) ELSE '0' END AS Junio,
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 7 THEN COUNT(*) ELSE '0' END AS Julio,
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)))  = 8 THEN COUNT(*) ELSE '0' END AS Agosto,
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 9 THEN COUNT(*) ELSE '0' END AS Septiembre,
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 10 THEN COUNT(*) ELSE '0' END AS Octubre, 
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 11 THEN COUNT(*) ELSE '0' END AS Noviembre, 
							CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 12 THEN COUNT(*) ELSE '0' END AS Diciembre, 
							count (*) as Total,

						DATEPART(YEAR, a.FechaInicio) AS anio, c.FK_Tipo_Contingencia, a.FK_Id_Sede, 
                        a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria, a.NitEmpresa,a.DiasAusencia
						from	 master.dbo.spt_values INNER JOIN
						dbo.Tbl_Ausencias AS a INNER JOIN
                        dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia ON DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio) 
                         <= a.Fecha_Fin
						INNER JOIN
                        dbo.Tbl_Proceso AS p ON a.FK_Id_Proceso = p.Pk_Id_Proceso
						--where a.NitEmpresa=892099149
					     WHERE        (master.dbo.spt_values.type = 'P') 
						 group by 
						  p.Descripcion_Proceso, 
						 c.PK_Id_Contingencia, 	
						 a.FechaInicio, 
						 a.Fecha_Fin,  
						 c.FK_Tipo_Contingencia, 
						 a.FK_Id_Sede, 
						 a.FK_Id_Departamento,
						 a.NitEmpresa,
						 a.FK_Id_EmpresaUsuaria,
						 a.DiasAusencia,
						 DATEPART(YEAR, a.FechaInicio),
						 DATEPART(MONTH, DATEADD(DAY, master.dbo.spt_values.number,  CONVERT(DATE, a.FechaInicio, 103)))






GO
/****** Object:  View [dbo].[V_AusentismoporProceso]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_AusentismoporProceso]
AS
SELECT        p.Descripcion_Proceso AS Proceso, SUM(CASE WHEN MONTH(a.Fecha_Fin) = 1 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Enero, SUM(CASE WHEN MONTH(a.Fecha_Fin) 
                         = 2 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Febrero, SUM(CASE WHEN MONTH(a.Fecha_Fin) = 3 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Marzo, SUM(CASE WHEN MONTH(a.Fecha_Fin) 
                         = 4 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Abril, SUM(CASE WHEN MONTH(a.Fecha_Fin) = 5 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Mayo, SUM(CASE WHEN MONTH(a.Fecha_Fin) 
                         = 6 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Junio, SUM(CASE WHEN MONTH(a.Fecha_Fin) = 7 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Julio, SUM(CASE WHEN MONTH(a.Fecha_Fin) 
                         = 8 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Agosto, SUM(CASE WHEN MONTH(a.Fecha_Fin) = 9 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Septiembre, SUM(CASE WHEN MONTH(a.Fecha_Fin) 
                         = 10 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Octubre, SUM(CASE WHEN MONTH(a.Fecha_Fin) = 11 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Noviembre, 
                         SUM(CASE WHEN MONTH(a.Fecha_Fin) = 12 THEN ROUND(a.DiasAusencia, 1) ELSE 0 END) AS Diciembre, SUM(ROUND(a.DiasAusencia, 1)) AS Total_Dias
FROM            dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Proceso AS p ON a.FK_Id_Proceso = p.Pk_Id_Proceso

GROUP BY p.Descripcion_Proceso





GO
/****** Object:  View [dbo].[V_CiudadResidenciaPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_CiudadResidenciaPerfilSocio]
AS
select  FK_Ciudad,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo,Pk_Id_Proceso,Descripcion_Proceso,Nombre_Municipio,
Count(Nombre_Municipio)as Total
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio

group by ps.FK_Ciudad,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, FK_Ciudad,Nombre_Municipio


GO
/****** Object:  View [dbo].[V_competencia]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_competencia]
AS
SELECT     dbo.Tbl_Empresa.Nit_Empresa,   dbo.Tbl_Empresa.Razon_Social AS EMPRESA, dbo.Tbl_Rol.Descripcion AS ROL, dbo.Tbl_Cargo.Nombre_Cargo AS CARGO, 
                         COUNT(dbo.Tbl_UsuarioRol.Pk_Id_UsuarioRol) AS NUMERO_DE_PERSONAS
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Rol ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Rol.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Cargo_Por_Rol INNER JOIN
                         dbo.Tbl_Cargo ON dbo.Tbl_Cargo_Por_Rol.Fk_Id_Cargo = dbo.Tbl_Cargo.Pk_Id_Cargo ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_Cargo_Por_Rol.Fk_Id_Rol INNER JOIN
                         dbo.Tbl_UsuarioRol ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_UsuarioRol.Fk_Id_Rol
GROUP BY dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Rol.Descripcion, dbo.Tbl_Cargo.Nombre_Cargo 


GO
/****** Object:  View [dbo].[V_Comuni_Estado]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_Comuni_Estado]
AS
SELECT   distinct     NitEmpresa,
                         EstadoComunicado as estado, 
                         'EXTERNO' AS TIPO
FROM            Tbl_ComunicacionesExternas INNER JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicacionesExternas.PK_Id_Comunicado = Tbl_ComunicacionesLog.fk_id_comunicaciones

UNION
SELECT      distinct     NitEmpresa,  EstadoEncuesta,   'INTERNO' AS TIPO
FROM            Tbl_ComunicacionesInternas


UNION
SELECT     distinct   NitEmpresa,estado,  'APP' AS TIPO
FROM          Tbl_ComunicadosAPP
      


GO
/****** Object:  View [dbo].[V_COMUNICACIONES]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_COMUNICACIONES]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_ComunicacionesInternas.Titulo, dbo.Tbl_ComunicacionesInternas.EstadoEncuesta, 
                         dbo.Tbl_ComunicacionesInternas.FechaCreacion, dbo.Tbl_ComunicacionesEncuestas.contenido AS contenido_encuesta
FROM            dbo.Tbl_ComunicacionesInternas INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_ComunicacionesInternas.NitEmpresa = dbo.Tbl_Empresa.Nit_Empresa LEFT OUTER JOIN
                         dbo.Tbl_ComunicacionesEncuestas ON dbo.Tbl_ComunicacionesInternas.PK_Id_Encuesta = dbo.Tbl_ComunicacionesEncuestas.pk_id_encuesta






GO
/****** Object:  View [dbo].[V_Comunicaciones_APP]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




alter VIEW [dbo].[V_Comunicaciones_APP]
AS


SELECT        Tbl_Empresa.Nit_Empresa, Tbl_Empresa.Razon_Social, Tbl_ComunicadosAPP.FechaCreacion, Tbl_ComunicadosAPP.FechaEnvio, Tbl_ComunicadosAPP.Estado, 
                         Tbl_ComunicadosAPP.AsuntoAPP, Tbl_ComunicadosAPP.Asunto, Tbl_ComunicadosAPP.Titulo, 
                         Tbl_ComunicacionesEncuestas.contenido AS [Contenido Encuesta]
FROM            Tbl_ComunicadosAPP INNER JOIN
                         Tbl_Empresa ON Tbl_ComunicadosAPP.NitEmpresa = Tbl_Empresa.Nit_Empresa INNER JOIN
                         Tbl_EstadosComunicadosAPP ON Tbl_ComunicadosAPP.Estado = Tbl_EstadosComunicadosAPP.Nombre LEFT OUTER JOIN
                         Tbl_ComunicacionesEncuestas ON Tbl_ComunicadosAPP.IDComunicadosAPP = Tbl_ComunicacionesEncuestas.fk_pk_id_encuesta



GO
/****** Object:  View [dbo].[V_Comunicaciones_indicador_grupo]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_Comunicaciones_indicador_grupo]
AS
SELECT        dbo.Tbl_GruposComunicaciones.NitEmpresa, dbo.Tbl_GruposComunicaciones.pk_id_grupo, dbo.Tbl_GruposComunicaciones.NombreGrupo, 
                         dbo.Tbl_GrupoUsuariosComunicaciones.nombre_contacto, dbo.Tbl_GrupoUsuariosComunicaciones.email, dbo.Tbl_GrupoUsuariosComunicaciones.Status
FROM            dbo.Tbl_GrupoUsuariosComunicaciones INNER JOIN
                         dbo.Tbl_GruposComunicaciones ON dbo.Tbl_GrupoUsuariosComunicaciones.fk_id_grupo_comunicaciones = dbo.Tbl_GruposComunicaciones.pk_id_grupo




GO
/****** Object:  View [dbo].[v_Condiciones_Salud]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[v_Condiciones_Salud]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Dx_Condiciones_De_Salud.Lugar AS ZONA_LUGAR, dbo.Tbl_Dx_Condiciones_De_Salud.vigencia, 
                         dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx AS FECHA_INICIAL, MONTH(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS MES, 
                         YEAR(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS AÑO, dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Final_Dx AS FECHA_FINAL, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS CLASIFICACIÓN_PELIGRO, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar AS [TOTAL_TRABAJADORES/ZONA-LUGAR], dbo.Tbl_Sintomatologia_Dx.Sintomatologia, 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia AS NUMERO_TRABAJADORES_SINTOMATOLOGIA, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia ELSE '0' END AS ANORMALIDAD,
                          dbo.Tbl_Pruebas_Clinica_Dx.Prueba_Clinica, dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba AS TRABAJADORES_PRUEBA, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba ELSE '0' END AS ANORMALIDAD_PRUEBA,
                          dbo.Tbl_Pruebas_P_Clinica_Dx.Prueba_P_Clinica, dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P AS TRABAJADORES_PRUEBA_P, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P ELSE '0' END AS ANORMALIDAD_PRUEBA_P,
                          dbo.Tbl_Diagnosticos.Descripcion AS DIAGNOSTICO_CIE10a, dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico AS TRABAJADORES_DIAGNOSTICO, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico > 0 THEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico ELSE '0' END AS
                          ANORMALIDAD_DIAGNOSTICO, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Sede INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Dx_Condiciones_De_Salud.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud AS Tbl_Dx_Condiciones_De_Salud_1 ON dbo.Tbl_Sede.Pk_Id_Sede = Tbl_Dx_Condiciones_De_Salud_1.FK_Sede AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = Tbl_Dx_Condiciones_De_Salud_1.FK_Proceso INNER JOIN
                         dbo.Tbl_Pruebas_Clinica_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Pruebas_P_Clinica_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Sintomatologia_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Clasificacion_Peligro_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Clasificacion_Peligro_Dx.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Diagnostico_Cie10_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud AND 
                         Tbl_Dx_Condiciones_De_Salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Diagnosticos ON dbo.Tbl_Diagnostico_Cie10_Dx.FK_Diagnostico = dbo.Tbl_Diagnosticos.PK_Id_Diagnostico LEFT OUTER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso





GO
/****** Object:  View [dbo].[V_CONTINGENCIAS]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_CONTINGENCIAS]
AS
SELECT [PK_Id_Contingencia]
      ,[Detalle]  
  FROM [SGSST].[dbo].[Tbl_Contingencias] 
  where PK_Id_Contingencia = 1 or PK_Id_Contingencia = 2 or PK_Id_Contingencia = 3 

GO
/****** Object:  View [dbo].[V_ConyuguePerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_ConyuguePerfilSocio]
AS
select  Conyuge,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,
Count(Conyuge)as Total
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio


group by ps.Conyuge,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso,Nombre_Municipio


GO
/****** Object:  View [dbo].[V_diagnospeligr]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_diagnospeligr]
AS
SELECT DISTINCT                          dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_Municipio.Nombre_Municipio, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Lugar
FROM            dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_SedeMunicipio ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_SedeMunicipio.Fk_id_Sede INNER JOIN
                         dbo.Tbl_Municipio ON dbo.Tbl_SedeMunicipio.Fk_Id_Municipio = dbo.Tbl_Municipio.Pk_Id_Municipio INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Clasificacion_Peligro_Dx ON 
                         dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_Peligro_Dx.FK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Proceso INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud ON dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Proceso ON 
                         dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud = dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud ON 
                         dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Sede LEFT OUTER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso
GROUP BY dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_Municipio.Nombre_Municipio, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Lugar



GO
/****** Object:  View [dbo].[V_Diagnostico_Condiciones_Salud]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter view [dbo].[V_Diagnostico_Condiciones_Salud]
as


SELECT DISTINCT 
                         dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS Sede, dbo.Tbl_Municipio.Nombre_Municipio, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Lugar AS Zona_lugar, dbo.Tbl_Diagnosticos.Descripcion AS Diagnostico_cie10a, dbo.Tbl_Dx_Condiciones_De_Salud.vigencia, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx AS fecha_inicial, MONTH(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS Mes, 
                         YEAR(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS Año, dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Final_Dx AS Fecha_final, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar AS [Total_trabajadores/zona-lugar], dbo.Tbl_Sintomatologia_Dx.Sintomatologia, 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia AS Número_trabajadores_sintomatologia, 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia / dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar * 100 AS PorcentajeSintomatologia, 
                         dbo.Tbl_Pruebas_Clinica_Dx.Prueba_Clinica, SUM(dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba) AS Trabajadores_prueba, 
                         dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba / dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar * 100 AS PorcentajePrueba, 
                         dbo.Tbl_Pruebas_P_Clinica_Dx.Prueba_P_Clinica, SUM(dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P) AS Trabajadores_prueba_p, 
                         dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba / dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar * 100 AS PorcentajePruebaP, 
                         SUM(dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico) AS Trabajadores_diagnostico, 
                         dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico / dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar * 100 AS PorcentajeDiagnostico
FROM            dbo.Tbl_Diagnostico_Cie10_Dx INNER JOIN
                         dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_SedeMunicipio ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_SedeMunicipio.Fk_id_Sede INNER JOIN
                         dbo.Tbl_Municipio ON dbo.Tbl_SedeMunicipio.Fk_Id_Municipio = dbo.Tbl_Municipio.Pk_Id_Municipio INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Sede INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud AS tbl_dx_condiciones_de_salud_1 ON dbo.Tbl_Sede.Pk_Id_Sede = tbl_dx_condiciones_de_salud_1.FK_Sede INNER JOIN
                         dbo.Tbl_Pruebas_Clinica_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Pruebas_P_Clinica_Dx ON 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Sintomatologia_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud ON 
                         dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud = dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud AND 
                         dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud = tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Diagnosticos ON dbo.Tbl_Diagnostico_Cie10_Dx.FK_Diagnostico = dbo.Tbl_Diagnosticos.PK_Id_Diagnostico
GROUP BY dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Dx_Condiciones_De_Salud.Lugar, dbo.Tbl_Dx_Condiciones_De_Salud.vigencia, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx, MONTH(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx), 
                         YEAR(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx), dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Final_Dx, 
                         dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico, dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar, 
                         dbo.Tbl_Sintomatologia_Dx.Sintomatologia, dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia, dbo.Tbl_Pruebas_Clinica_Dx.Prueba_Clinica, 
                         dbo.Tbl_Pruebas_P_Clinica_Dx.Prueba_P_Clinica, dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba, dbo.Tbl_Diagnosticos.Descripcion, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia)
                          ELSE '0' END, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba)
                          ELSE '0' END, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P)
                          ELSE '0' END, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico)
                          ELSE '0' END, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Municipio.Nombre_Municipio






GO
/****** Object:  View [dbo].[V_Diagnostico_datosgral]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_Diagnostico_datosgral]
AS
SELECT DISTINCT                         dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_Municipio.Nombre_Municipio, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Lugar
FROM            dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_SedeMunicipio ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_SedeMunicipio.Fk_id_Sede INNER JOIN
                         dbo.Tbl_Municipio ON dbo.Tbl_SedeMunicipio.Fk_Id_Municipio = dbo.Tbl_Municipio.Pk_Id_Municipio INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Clasificacion_Peligro_Dx ON 
                         dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_Peligro_Dx.FK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Proceso INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud ON dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Proceso ON 
                         dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud = dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud ON 
                         dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Sede LEFT OUTER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso
GROUP BY dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_Municipio.Nombre_Municipio, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Lugar


GO
/****** Object:  View [dbo].[V_DIAGNOSTICOCONDICIONESSALUD]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_DIAGNOSTICOCONDICIONESSALUD]
AS 
SELECT DISTINCT 
                         dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS Sede, dbo.Tbl_Municipio.Nombre_Municipio, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Lugar AS Zona_lugar, dbo.Tbl_Diagnosticos.Descripcion AS Diagnostico_cie10a, 
                         dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS Clasificación_del_peligro, dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.vigencia, dbo.Tbl_Proceso.Descripcion_Proceso AS Proceso,
						  
                         dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx AS fecha_inicial, 
						 MONTH(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS Mes, 
                         YEAR(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx) AS Año, 
						 dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Final_Dx AS Fecha_final, 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar AS [Total_trabajadores/zona-lugar], 
						 dbo.Tbl_Sintomatologia_Dx.Sintomatologia, 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia AS Número_trabajadores_sintomatologia, 

						 (sum(dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia) / dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar)*100 as PorcentajeSintomatologia,

						
                         
						  
						  dbo.Tbl_Pruebas_Clinica_Dx.Prueba_Clinica, 
						  SUM(dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba) 
                         AS Trabajadores_prueba, 
						 (SUM(dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba)/ sum(dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar))*100 PorcentajePrueba,
						 
						  
						  dbo.Tbl_Pruebas_P_Clinica_Dx.Prueba_P_Clinica, 
						  SUM(dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P) 
                         AS Trabajadores_prueba_p, 
						(SUM(dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba) / sum(dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar))*100 as PorcentajePruebaP , 
						 
						  
						  SUM(dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico) AS Trabajadores_diagnostico,
						  
						   ( sum(dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico)/ sum(dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar))*100 as PorcentajeDiagnostico
						   
						
FROM            dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_SedeMunicipio ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_SedeMunicipio.Fk_id_Sede INNER JOIN
                         dbo.Tbl_Municipio ON dbo.Tbl_SedeMunicipio.Fk_Id_Municipio = dbo.Tbl_Municipio.Pk_Id_Municipio INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Dx_Condiciones_De_Salud.FK_Sede INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Dx_Condiciones_De_Salud.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Dx_Condiciones_De_Salud AS tbl_dx_condiciones_de_salud_1 ON dbo.Tbl_Sede.Pk_Id_Sede = tbl_dx_condiciones_de_salud_1.FK_Sede AND 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = tbl_dx_condiciones_de_salud_1.FK_Proceso INNER JOIN
                         dbo.Tbl_Pruebas_Clinica_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Pruebas_P_Clinica_Dx ON 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Pruebas_P_Clinica_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Sintomatologia_Dx ON dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Sintomatologia_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Clasificacion_Peligro_Dx ON 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Clasificacion_Peligro_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON 
                         dbo.Tbl_Clasificacion_Peligro_Dx.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro AND 
                         dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede AND dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Diagnostico_Cie10_Dx ON 
                         dbo.Tbl_Dx_Condiciones_De_Salud.Pk_DxCondicionesDeSalud = dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud AND 
                         tbl_dx_condiciones_de_salud_1.Pk_DxCondicionesDeSalud = dbo.Tbl_Diagnostico_Cie10_Dx.FK_DxCondicionesDeSalud INNER JOIN
                         dbo.Tbl_Diagnosticos ON dbo.Tbl_Diagnostico_Cie10_Dx.FK_Diagnostico = dbo.Tbl_Diagnosticos.PK_Id_Diagnostico


GROUP BY dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Dx_Condiciones_De_Salud.Lugar, dbo.Tbl_Dx_Condiciones_De_Salud.vigencia, 
                         dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx, MONTH(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx), 
                         YEAR(dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Inicial_Dx), dbo.Tbl_Dx_Condiciones_De_Salud.Fecha_Final_Dx, dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico,
                         dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar, dbo.Tbl_Sintomatologia_Dx.Sintomatologia, dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia, 
                         dbo.Tbl_Pruebas_Clinica_Dx.Prueba_Clinica, dbo.Tbl_Pruebas_P_Clinica_Dx.Prueba_P_Clinica,dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba, dbo.Tbl_Diagnosticos.Descripcion, 
                         CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Sintomatologia_Dx.Trabajadores_Sintomatologia)
                          ELSE '0' END, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_Clinica_Dx.Trabajadores_Con_Prueba)
                          ELSE '0' END, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Pruebas_P_Clinica_Dx.Trabajadores_Con_Prueba_P)
                          ELSE '0' END, CASE WHEN dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar > 0 AND 
                         dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico > 0 THEN (dbo.Tbl_Dx_Condiciones_De_Salud.Trabajadores_Lugar / dbo.Tbl_Diagnostico_Cie10_Dx.Trabajadores_Con_Diagnostico)
                          ELSE '0' END, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Municipio.Nombre_Municipio, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro
GO
/****** Object:  View [dbo].[V_efecticomuni]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_efecticomuni]
AS
SELECT DISTINCT 
                         Tbl_ComunicacionesExternas.PK_Id_Comunicado, Tbl_ComunicacionesExternas.EstadoComunicado, Tbl_ComunicacionesExternas.FechaCreacion, 
                         MONTH(CONVERT(date, CONVERT(varchar(10), Tbl_ComunicacionesExternas.FechaEnvio, 101), 103)) AS mes, year(CONVERT(date, CONVERT(varchar(10), 
                         Tbl_ComunicacionesExternas.FechaEnvio, 101), 103)) AS año, CASE month(CONVERT(date, CONVERT(varchar(10), FechaEnvio, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaEnvio, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, Tbl_ComunicacionesExternas.FechaEnvio, Tbl_ComunicacionesExternas.NitEmpresa, 
                         'INTERNO' AS TIPO, [enviado_rechazado],modulo
FROM            Tbl_ComunicacionesExternas left JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicacionesExternas.PK_Id_Comunicado = Tbl_ComunicacionesLog.fk_id_comunicaciones
WHERE        (dbo.Tbl_ComunicacionesLog.modulo LIKE '%externo%') AND enviado_rechazado = 1
/*and [enviado_rechazado]=@efec*/ UNION
SELECT DISTINCT 
                         IDComunicadosAPP, Estado, FechaCreacion, MONTH(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS mes, year(CONVERT(date, 
                         CONVERT(varchar(10), FechaCreacion, 101), 103)) AS año, CASE month(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, FechaEnvio, NitEmpresa, 'APP' AS TIPO, [enviado_rechazado],modulo
FROM            Tbl_ComunicadosAPP inner JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicadosAPP.IDComunicadosAPP = Tbl_ComunicacionesLog.fk_id_comunicaciones
WHERE        (dbo.Tbl_ComunicacionesLog.modulo NOT LIKE '%app%') AND enviado_rechazado = 1

UNION

SELECT   pk_id_comadjunto,CASE WHEN TIPO in ('E') THEN 'Enviado' else 'En Espera' end AS Estado, fecha, year(CONVERT(date, 
                         CONVERT(varchar(10), Fecha, 101), 103)) AS año, MONTH(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) AS mes, CASE month(CONVERT(date, 
                         CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes,fecha as FechaEnvio, NitEmpresa, 'EXTERNO' AS TIPO,CASE WHEN TIPO in ('E') THEN '1' else '0' end AS 'enviado_rechazado','externo' as 'modulo'
  FROM [dbo].[Tbl_ComunicadosAdjutos] where TIPO in ('E')



GO
/****** Object:  View [dbo].[V_enfermedadlaboral]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_enfermedadlaboral]
AS
SELECT        dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion, MONTH(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion) AS Mes, 
                         YEAR(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion) AS Año, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Diagnostico, 
                         dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.EstadoInstancia, COUNT(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.EstadoInstancia) 
                         AS Total_Estado, dbo.Tbl_EstadosInstanciasRegistradas.Nombre AS Nombre_Estado, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.CodigoDiagnosticoCIIE10, 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaDocumentoFUREL, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaCartaEnviadaEPS
FROM            dbo.Tbl_DocumentosEnviadosEPS FULL OUTER JOIN
                         dbo.Tbl_EstadosInstanciasRegistradas INNER JOIN
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas INNER JOIN
                         dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada ON 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Pk_Id_EnfermedadLaboral = dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.Fk_Id_EnfermedadLaboral ON 
                         dbo.Tbl_EstadosInstanciasRegistradas.PK_Id_EstadoInstancia = dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FK_Id_EstadoInstancia ON 
                         dbo.Tbl_DocumentosEnviadosEPS.Fk_Id_EnfermedadLaboral = dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Pk_Id_EnfermedadLaboral
GROUP BY dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion, MONTH(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion), 
                         YEAR(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion), dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Diagnostico, 
                         dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.EstadoInstancia, dbo.Tbl_EstadosInstanciasRegistradas.Nombre, 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.CodigoDiagnosticoCIIE10, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaDocumentoFUREL, 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaCartaEnviadaEPS






GO
/****** Object:  View [dbo].[V_EstadoCivilPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_EstadoCivilPerfilSocio]
AS
select  FK_Estado_Civil,
ec.Descripcion_EstadoCivil as EstadoCivil,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,
Count(FK_Estado_Civil)as Total
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio
inner join Tbl_Estado_Civil as ec on PS.FK_Estado_Civil= ec.PK_Estado_Civil

group by ps.FK_Estado_Civil,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, ec.Descripcion_EstadoCivil,Nombre_Municipio



GO
/****** Object:  View [dbo].[V_EstratoSocioeconoPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_EstratoSocioeconoPerfilSocio]
AS

select  FK_Estrato,
est.Descripcion_Estrato as Estrato,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,



Count(FK_Estrato)as Total

from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio
inner join Tbl_Estrato as est on PS.FK_Estrato=est.PK_Estrato
--where Nit_Empresa=892099149
group by ps.FK_Estrato,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, est.Descripcion_Estrato,Nombre_Municipio





GO
/****** Object:  View [dbo].[V_EtniaPerfilsocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_EtniaPerfilsocio]
AS
select FK_Etnia as Etnia,
PK_Numero_Documento_Empl,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,
Descripcion_Etnia,
Count(FK_Etnia)as Total
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio
inner join Tbl_Etnia as et on PS.FK_Etnia= et.PK_Etnia
--where Nit_Empresa=892099149

group by ps.FK_Etnia,PK_Numero_Documento_Empl,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso,Nombre_Municipio,et.Descripcion_Etnia

GO
/****** Object:  View [dbo].[V_externos_comunica]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[V_externos_comunica]

AS
SELECT   pk_id_comadjunto,CASE WHEN TIPO in ('E') THEN 'Enviado' else 'En Espera' end AS Estado, fecha, MONTH(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) AS mes, CASE month(CONVERT(date, 
                         CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes,fecha as FechaEnvio, NitEmpresa, 'EXTERNO' AS TIPO
  FROM [dbo].[Tbl_ComunicadosAdjutos]


GO
/****** Object:  View [dbo].[V_GeneralPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_GeneralPerfilSocio]
AS

select  GradoEscolaridad as Grado,Conyuge,Ps.PK_Numero_Documento_Empl,m.Nombre_Municipio,Pk_Id_Sede,FechaIngresoUltimoCargo,Pk_Id_Proceso,Descripcion_Proceso,Sexo,Ingresos,FK_VinculacionLaboral,FK_Estrato,
est.Descripcion_Estrato as Estrato,
vl.Descripcion_VinculacionLaboral,
FK_Estado_Civil,
Hijos,
FK_Etnia as Etnia,
Descripcion_Etnia,
Nit_Empresa,
ec.Descripcion_EstadoCivil,
Count(GradoEscolaridad)as TotalGrado, 
Count(FK_Ciudad) as TotalCiudad,
Count(Sexo) as TotalSexo,
Count(Ingresos) as TotalIngresos,
Count(FK_VinculacionLaboral)as TotalVinculacion,
Count(FK_Estrato)as TotalEstrato,
Count(FK_Estado_Civil)as TotalEstadoCiv,
Count(Conyuge)as TotalConyuge,
Count (Hijos) as TotalHijos,
Count(FK_Etnia)as TotalEtnia
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio
inner join Tbl_VinculacionLaboral as vl on PS.FK_VinculacionLaboral=vl.PK_VinculacionLaboral
inner join Tbl_Estrato as est on PS.FK_Estrato=est.PK_Estrato
inner join Tbl_Estado_Civil as ec on PS.FK_Estado_Civil= ec.PK_Estado_Civil
inner join Tbl_Etnia as et on PS.FK_Etnia= et.PK_Etnia

group by ps.FK_Estado_Civil,ec.Descripcion_EstadoCivil,ps.GradoEscolaridad,Hijos,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, m.Nombre_Municipio, FK_Ciudad,Sexo,Ingresos,FK_Estrato,
vl.Descripcion_VinculacionLaboral,FK_VinculacionLaboral,est.Descripcion_Estrato,Conyuge,Ps.PK_Numero_Documento_Empl,FK_Etnia, et.Descripcion_Etnia,Nit_Empresa


GO
/****** Object:  View [dbo].[V_Gestiondelcambio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_Gestiondelcambio]
AS
SELECT        a.PK_GestionDelCambio, a.Fecha, a.DescripcionDeCambio, a.RequisitoLegal, a.Recomendaciones, a.FechaEjecucion, a.FechaSeguimiento, 
                         b.Descripcion_Clase_De_Peligro, c.Descripcion, e.Descripcion_Del_Peligro, a.Otro, a.FK_Clasificacion_De_Peligro, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_GestionDelCambio AS a INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro AS b ON a.FK_Clasificacion_De_Peligro = b.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro AS e ON a.FK_Tipo_De_Peligro = e.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Rol AS c ON a.FK_Id_Rol = c.Pk_Id_Rol INNER JOIN
                         dbo.Tbl_Empresa ON a.FK_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND c.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa



GO
/****** Object:  View [dbo].[V_GradoEscolaridadPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_GradoEscolaridadPerfilSocio]
AS
select  GradoEscolaridad as Grado,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo,Pk_Id_Proceso,Descripcion_Proceso,
Count(GradoEscolaridad)as Total
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
--where e.Nit_Empresa= 800089364
group by ps.GradoEscolaridad,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso




GO
/****** Object:  View [dbo].[V_GTC45_MATRIZ]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_GTC45_MATRIZ]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa AS NIT, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, 
                         dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, Tbl_Proceso_1.Descripcion_Proceso AS SUBPROCESO, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS PELIGRO_CLASIFICACIÓN, 
                         dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO_DESCRIPCIÓN, 
                         dbo.Tbl_Interpretacion_Nivel_Riesgo.Resultado AS INTERPRETACIÓN_DEL_NIVEL_DE_RIESGO, 
                         CASE WHEN RiesgoNoAceptable = 1 THEN 'Aceptable' ELSE 'No_aceptable' END AS ACEPTABILIDAD_DEL_RIESGO, 
                         dbo.Tbl_GTC45.Numero_De_Expuestos AS NUMERO_DE_EXPUESTOS_TOTAL, dbo.Tbl_Peligro.Fecha_De_Evaluacion AS FECHA, 
                         MONTH(dbo.Tbl_Peligro.Fecha_De_Evaluacion) AS MES, YEAR(dbo.Tbl_Peligro.Fecha_De_Evaluacion) AS AÑO
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Peligro.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_Consecuencia_Por_Peligro ON dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Consecuencia_Por_Peligro.FK_Peligro INNER JOIN
                         dbo.Tbl_Consecuencia ON dbo.Tbl_Consecuencia_Por_Peligro.FK_Consecuencia = dbo.Tbl_Consecuencia.PK_Consecuencia INNER JOIN
                         dbo.Tbl_Grupo ON dbo.Tbl_Consecuencia.FK_Grupo = dbo.Tbl_Grupo.PK_Grupo INNER JOIN
                         dbo.Tbl_GTC45 ON dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_GTC45.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Estimacion_De_Riesgo ON dbo.Tbl_Consecuencia.PK_Consecuencia = dbo.Tbl_Estimacion_De_Riesgo.FK_Consecuencia LEFT OUTER JOIN
                         dbo.Tbl_Interpretacion_Nivel_Riesgo ON dbo.Tbl_GTC45.Nivel_De_Riesgo >= dbo.Tbl_Interpretacion_Nivel_Riesgo.Nivel_Inferior AND 
                         dbo.Tbl_GTC45.Nivel_De_Riesgo <= dbo.Tbl_Interpretacion_Nivel_Riesgo.Nivel_Superior LEFT OUTER JOIN
                         dbo.Tbl_Nivel_De_Deficiencia ON dbo.Tbl_GTC45.FK_Nivel_De_Deficiencia = dbo.Tbl_Nivel_De_Deficiencia.PK_Nivel_De_Deficiencia LEFT OUTER JOIN
                         dbo.Tbl_Nivel_De_Exposicion ON dbo.Tbl_GTC45.FK_Nivel_De_Exposicion = dbo.Tbl_Nivel_De_Exposicion.PK_Nivel_De_Exposicion
GROUP BY dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Nivel_De_Exposicion.Descripcion_Exposicion, dbo.Tbl_Nivel_De_Deficiencia.Descripcion_Deficiciencia, 
                         dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Proceso.Descripcion_Proceso, Tbl_Proceso_1.Descripcion_Proceso, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_GTC45.Numero_De_Expuestos, 
                         dbo.Tbl_Interpretacion_Nivel_Riesgo.Resultado, dbo.Tbl_Estimacion_De_Riesgo.RiesgoNoAceptable, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Peligro.Fecha_De_Evaluacion






GO
/****** Object:  View [dbo].[V_HijosPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_HijosPerfilSocio]
AS
select Hijos,
PK_Numero_Documento_Empl,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,
Count(Hijos)as Total
from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio
--where Nit_Empresa=892099149

group by ps.Hijos,PK_Numero_Documento_Empl,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso,Nombre_Municipio


GO
/****** Object:  View [dbo].[V_IndAccionesCorrPrevMejActividades]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndAccionesCorrPrevMejActividades]
AS
SELECT        dbo.Tbl_Acciones.Pk_Id_Accion, dbo.Tbl_Acciones.Fecha_hall, YEAR(dbo.Tbl_Acciones.Fecha_hall) AS Anio, MONTH(dbo.Tbl_Acciones.Fecha_hall) AS Mes, 
                         dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Acciones.Eficacia, dbo.Tbl_Acciones.Estado, 
                         dbo.Tbl_Acciones.Tipo, dbo.Tbl_Acciones.Clase, dbo.Tbl_Acciones.Id_Accion
FROM            dbo.Tbl_Acciones INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Acciones.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa AND dbo.Tbl_Acciones.Halla_Sede = dbo.Tbl_Sede.Pk_Id_Sede


GO
/****** Object:  View [dbo].[V_IndAccionesCorrPrevMejTotal]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndAccionesCorrPrevMejTotal]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, MONTH(dbo.Tbl_Acciones.Fecha_hall) AS MES, 
                         YEAR(dbo.Tbl_Acciones.Fecha_hall) AS ANIO, dbo.Tbl_Acciones.Halla_Sede, dbo.Tbl_Acciones.Fecha_hall
FROM            dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Acciones ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Acciones.Fk_Id_Empresa AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Acciones.Halla_Sede





GO
/****** Object:  View [dbo].[V_IndConInsegurasActividades]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndConInsegurasActividades]
AS
SELECT        dbo.Tbl_Reportes.Pk_Id_Reportes, dbo.Tbl_Empresa.Nit_Empresa, YEAR(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS Anio, 
                         MONTH(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS Mes, dbo.Tbl_ActividadesActosInseguros.PK_ID_ActividadActosInseguros, 
                         dbo.Tbl_ActividadesActosInseguros.FechaEjecucion, dbo.Tbl_Actividad_Plan_Accion.FechaCierre, dbo.Tbl_ActividadesActosInseguros.NombreActividad, 
                         dbo.Tbl_Reportes.ConsecutivoReporte
FROM            dbo.Tbl_ActividadesActosInseguros INNER JOIN
                         dbo.Tbl_Reportes ON dbo.Tbl_ActividadesActosInseguros.FK_Id_Reportes = dbo.Tbl_Reportes.Pk_Id_Reportes INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Reportes.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa LEFT OUTER JOIN
                         dbo.Tbl_Actividad_Plan_Accion ON dbo.Tbl_Reportes.Pk_Id_Reportes = dbo.Tbl_Actividad_Plan_Accion.Fk_Plan_Inspección AND 
                         dbo.Tbl_ActividadesActosInseguros.PK_ID_ActividadActosInseguros = dbo.Tbl_Actividad_Plan_Accion.Fk_Id_Actividad AND 
                         dbo.Tbl_Actividad_Plan_Accion.Fk_Id_ModuloPlanAccion = 5


GO
/****** Object:  View [dbo].[V_IndConInsegurasTotalSemetre]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndConInsegurasTotalSemetre]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Reportes.Pk_Id_Reportes, 
                         dbo.Tbl_Reportes.Fecha_Ocurrencia, YEAR(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS ANIO, MONTH(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS MES, 
                         dbo.Tbl_Tipo_Reporte.Descripcion_Tipo_Reporte AS TIPO_REPORTE, dbo.Tbl_Reportes.FK_Sede, dbo.Tbl_Reportes.FK_Tipo_Reporte
FROM            dbo.Tbl_Reportes INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Reportes.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tipo_Reporte ON dbo.Tbl_Reportes.FK_Tipo_Reporte = dbo.Tbl_Tipo_Reporte.Pk_Id_Tipo_Reporte


GO
/****** Object:  View [dbo].[V_IndConvivencia]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_IndConvivencia]
AS


Select

YEAR(Convert(datetime,ac.Fecha,103)) as anio,
Month(Convert(datetime,ac.Fecha,103)) as Mes,
count (*) as total, 'Convivencia' as Tipo_Acta,
ac.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede

--CASE  WHEN '' THEN 0 ELSE YEAR(CONVERT(datetime, , 103)) END AS AnioEje
from Tbl_ActasConvivencia ac
inner join 
Tbl_Sede s on s.Pk_Id_Sede=ac.Fk_Id_Sede
LEFT OUTER JOIN Tbl_Empresa e on ac.Fk_Id_Empresa = e.Pk_Id_Empresa

group by 
Month(Convert(datetime,ac.Fecha,103)),
YEAR(Convert(datetime,ac.Fecha,103)),ac.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede



GO
/****** Object:  View [dbo].[V_IndCopasst]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_IndCopasst]
AS

Select 

YEAR(Convert(datetime,acpt.Fecha,103)) as anio,
Month(Convert(datetime,acpt.Fecha,103)) as Mes,
count (*) as total, 'Copasst' as Tipo_Acta,
acpt.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede


from Tbl_ActasCopasst acpt
inner join Tbl_Sede s on s.Pk_Id_Sede=acpt.Fk_Id_Sede
LEFT OUTER JOIN Tbl_Empresa e on acpt.Fk_Id_Empresa= e.Pk_Id_Empresa

group by 
Month(Convert(datetime,acpt.Fecha,103)),
YEAR(Convert(datetime,acpt.Fecha,103)),
acpt.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede


GO
/****** Object:  View [dbo].[V_IndEvalEstandaresMinimos]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndEvalEstandaresMinimos]
AS
SELECT        dbo.Tbl_Evaluacion_Estandares_Minimos.Fk_Id_Empresa_Evaluar, dbo.Tbl_Evaluacion_Estandares_Minimos.Valor_Calificacion, 
                         dbo.Tbl_Evaluacion_Estandares_Minimos.Pk_Id_Eval_Estandar_Minimo, dbo.Tbl_Empresas_Evaluar.Fecha_Diligencia_Eval_Inicial, 
                         dbo.Tbl_Empresas_Evaluar.Fecha_Diligencia_Eval_EstMin, dbo.Tbl_Empresas_Evaluar.Fk_Id_Empresa, dbo.Tbl_Empresa.Nit_Empresa, 
                         dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Criterios.Descripcion_Corta
FROM            dbo.Tbl_Evaluacion_Estandares_Minimos INNER JOIN
                         dbo.Tbl_Empresas_Evaluar ON 
                         dbo.Tbl_Evaluacion_Estandares_Minimos.Fk_Id_Empresa_Evaluar = dbo.Tbl_Empresas_Evaluar.Pk_Id_Empresa_Evaluar INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Empresas_Evaluar.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Criterios ON dbo.Tbl_Evaluacion_Estandares_Minimos.Fk_Id_Criterio = dbo.Tbl_Criterios.Pk_Id_Criterio



GO
/****** Object:  View [dbo].[V_IndFrecuenciaAccidentesTrabajo]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndFrecuenciaAccidentesTrabajo]
AS
SELECT        dbo.Tbl_Ausencias.NitEmpresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Ausencias.FK_Id_Sede, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Ausencias.FechaInicio, 
                         dbo.Tbl_Contingencias.Detalle, dbo.Tbl_Ausencias.FK_Id_Contingencia, dbo.Tbl_Ausencias.DiasAusencia, MONTH(dbo.Tbl_Ausencias.FechaInicio) AS Mes
FROM            dbo.Tbl_Ausencias INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Empresa.Nit_Empresa = dbo.Tbl_Ausencias.NitEmpresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Ausencias.FK_Id_Sede INNER JOIN
                         dbo.Tbl_Contingencias ON dbo.Tbl_Ausencias.FK_Id_Contingencia = dbo.Tbl_Contingencias.PK_Id_Contingencia


GO
/****** Object:  View [dbo].[V_IndFrecuenciaAccidentesTrabajoN]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_IndFrecuenciaAccidentesTrabajoN]
AS
SELECT        dbo.Tbl_Ausencias.NitEmpresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Ausencias.FK_Id_Sede, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Ausencias.FK_Id_Contingencia, dbo.Tbl_Ausencias.FechaInicio, dbo.Tbl_Ausencias.DiasAusencia
FROM            dbo.Tbl_Ausencias INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Empresa.Nit_Empresa = dbo.Tbl_Ausencias.NitEmpresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Ausencias.FK_Id_Sede



GO
/****** Object:  View [dbo].[V_IndFrecuenciaAusentismo]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndFrecuenciaAusentismo]
AS
SELECT        dbo.Tbl_Ausencias.NitEmpresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Ausencias.FK_Id_Sede, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Ausencias.FK_Id_Contingencia, dbo.Tbl_Ausencias.FechaInicio, YEAR(dbo.Tbl_Ausencias.FechaInicio) AS Anio, dbo.Tbl_Contingencias.Detalle, 
                         MONTH(dbo.Tbl_Ausencias.FechaInicio) AS Mes
FROM            dbo.Tbl_Ausencias INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Empresa.Nit_Empresa = dbo.Tbl_Ausencias.NitEmpresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Ausencias.FK_Id_Sede INNER JOIN
                         dbo.Tbl_Contingencias ON dbo.Tbl_Ausencias.FK_Id_Contingencia = dbo.Tbl_Contingencias.PK_Id_Contingencia


GO
/****** Object:  View [dbo].[V_indicador_externa]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_indicador_externa]
AS
SELECT        pk_id_comadjunto, estado, fecha, mes, NomMes, conmes, FechaEnvio, NitEmpresa, TIPO
FROM            V_externos_comunica


GO
/****** Object:  View [dbo].[V_indicador_internas]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_indicador_internas]
AS
SELECT        V_uniontotal.PK_Id_Comunicado, V_uniontotal.EstadoComunicado, V_uniontotal.FechaCreacion, V_uniontotal.mes, V_uniontotal.año, V_uniontotal.NomMes, 
                         V_uniontotal.conmes, V_uniontotal.FechaEnvio, V_uniontotal.NitEmpresa, V_uniontotal.TIPO, V_uniontotal.Titulo, V_uniontotal.Asunto, V_efecticomuni.modulo, 
                         CASE WHEN V_efecticomuni.enviado_rechazado = '1' THEN 'Enviado' ELSE 'Sin_enviar' END AS efectivo
FROM            V_uniontotal LEFT OUTER JOIN
                         V_efecticomuni ON V_uniontotal.TIPO = V_efecticomuni.TIPO AND V_uniontotal.NitEmpresa = V_efecticomuni.NitEmpresa AND 
                         V_uniontotal.PK_Id_Comunicado = V_efecticomuni.PK_Id_Comunicado
WHERE        V_uniontotal.TIPO='INTERNO'




GO
/****** Object:  View [dbo].[V_Indicador_Por_Actas]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_Indicador_Por_Actas]
AS
select *
from (Select 

CASE WHEN MONTH(Fecha) = 1 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Enero, 
CASE WHEN MONTH(Fecha) = 2 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Febrero, 
CASE WHEN MONTH(Fecha) = 3 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Marzo, 
CASE WHEN MONTH(Fecha) = 4 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Abril, 
CASE WHEN MONTH(Fecha) = 5 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Mayo, 
CASE WHEN MONTH(Fecha) = 6 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Junio, 
CASE WHEN MONTH(Fecha) = 7 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Julio, 
CASE WHEN MONTH(Fecha) = 8 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Agosto, 
CASE WHEN MONTH(Fecha) = 9 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Septiembre, 
CASE WHEN MONTH(Fecha) = 10THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Octubre, 
CASE WHEN MONTH(Fecha) = 11 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Noviembre, 
CASE WHEN MONTH(Fecha) = 12 THEN cast(count(acpt.Fecha) as decimal(19)) ELSE '0' END AS Diciembre, 

count(*) Total, 'Acta Copasst' as Tipo_Acta, 
DATEPART(YEAR,acpt.Fecha) as anio,
acpt.Fk_Id_Sede,
e.Nit_Empresa
from Tbl_ActasCopasst acpt
inner join Tbl_Sede s on s.Pk_Id_Sede=acpt.Fk_Id_Sede
LEFT OUTER JOIN Tbl_Empresa e on acpt.Fk_Id_Empresa= e.Pk_Id_Empresa
--where Nit_Empresa=892099149 
group by 
month(acpt.Fecha),
acpt.Fk_Id_Sede,
e.Nit_Empresa,
DATEPART(YEAR,acpt.Fecha) 

union Select  
CASE WHEN MONTH(Fecha) = 1 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Enero, 
CASE WHEN MONTH(Fecha) = 2 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Febrero, 
CASE WHEN MONTH(Fecha) = 3 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Marzo, 
CASE WHEN MONTH(Fecha) = 4 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Abril, 
CASE WHEN MONTH(Fecha) = 5 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Mayo, 
CASE WHEN MONTH(Fecha) = 6 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Junio, 
CASE WHEN MONTH(Fecha) = 7 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Julio, 
CASE WHEN MONTH(Fecha) = 8 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Agosto, 
CASE WHEN MONTH(Fecha) = 9 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Septiembre, 
CASE WHEN MONTH(Fecha) = 10THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Octubre, 
CASE WHEN MONTH(Fecha) = 11 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Noviembre, 
CASE WHEN MONTH(Fecha) = 12 THEN cast(count(ac.Fecha) as decimal(19)) ELSE '0' END AS Diciembre, 

count(*) as Total, 'Acta Convivencia' as Tipo_Acta,
DATEPART(YEAR,ac.Fecha) as anio,
ac.Fk_Id_Sede,
e.Nit_Empresa

from Tbl_ActasConvivencia ac
inner join Tbl_Sede s on s.Pk_Id_Sede=ac.Fk_Id_Sede

LEFT OUTER JOIN Tbl_Empresa e on ac.Fk_Id_Empresa= e.Pk_Id_Empresa
--where Nit_Empresa=892099149 
group by 
month(ac.Fecha),
DATEPART(YEAR,ac.Fecha),
e.Nit_Empresa,
ac.Fk_Id_Sede
)tabla



GO
/****** Object:  View [dbo].[V_IndicadorActaCPyCV]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_IndicadorActaCPyCV]
AS

select *
from 
(Select 

YEAR(Convert(datetime,acpt.Fecha,103)) as anio,
Month(Convert(datetime,acpt.Fecha,103)) as Mes,
count (*) as total, 'Acta Copasst' as Tipo_Acta,
acpt.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede


from Tbl_ActasCopasst acpt
inner join Tbl_Sede s on s.Pk_Id_Sede=acpt.Fk_Id_Sede
LEFT OUTER JOIN Tbl_Empresa e on acpt.Fk_Id_Empresa= e.Pk_Id_Empresa

group by 
Month(Convert(datetime,acpt.Fecha,103)),
YEAR(Convert(datetime,acpt.Fecha,103)),
acpt.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede

union Select  


YEAR(Convert(datetime,ac.Fecha,103)) as anio,
Month(Convert(datetime,ac.Fecha,103)) as Mes,
count (*) as total, 'Acta Convivencia' as Tipo_Acta,
ac.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede

--CASE  WHEN '' THEN 0 ELSE YEAR(CONVERT(datetime, , 103)) END AS AnioEje
from Tbl_ActasConvivencia ac
inner join 
Tbl_Sede s on s.Pk_Id_Sede=ac.Fk_Id_Sede
LEFT OUTER JOIN Tbl_Empresa e on ac.Fk_Id_Empresa = e.Pk_Id_Empresa

group by 
Month(Convert(datetime,ac.Fecha,103)),
YEAR(Convert(datetime,ac.Fecha,103)),ac.Fk_Id_Sede,
e.Nit_Empresa,
e.Razon_Social,
s.Nombre_Sede
)tabla


GO
/****** Object:  View [dbo].[V_indicadorcomuniAPP]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_indicadorcomuniAPP]
AS
SELECT       IDComunicadosAPP, Estado, FechaCreacion, MONTH(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS mes, year(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS año, CASE month(CONVERT(date, 
                         CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, FechaEnvio, NitEmpresa, 'APP' AS TIPO
FROM                   Tbl_ComunicadosAPP INNER JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicadosAPP.IDComunicadosAPP = Tbl_ComunicacionesLog.fk_id_comunicaciones




GO
/****** Object:  View [dbo].[V_IndLesionesIncapacitantes]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndLesionesIncapacitantes]
AS
SELECT        a1.Mes, a1.NomMes, SUM(a1.Total) AS DiasAus, ISNULL(b1.TotalAus, 0) AS NumAus, YEAR(a1.FechaInicio) AS Anio, a1.NitEmpresa
FROM            (SELECT        MONTH(DATEADD(DAY, master.dbo.spt_values.number, CONVERT(DATE, a.FechaInicio, 103))) AS Mes, CASE MONTH(DATEADD(DAY, number, 
                                                    CONVERT(DATE, a.FechaInicio, 103))) 
                                                    WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN
                                                     'Julio' WHEN 8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes,
                                                     COUNT(*) AS Total, a.FechaInicio, a.Fecha_Fin AS FechaFin, a.NitEmpresa
                          FROM            master.dbo.spt_values INNER JOIN
                                                    dbo.V_IndSeveridadAusentismo AS a ON DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio) <= a.Fecha_Fin AND YEAR(DATEADD(DAY, 
                                                    master.dbo.spt_values.number, a.FechaInicio)) = YEAR(a.FechaInicio)
                          WHERE        (master.dbo.spt_values.type = 'P') AND (a.FK_Id_Contingencia = 3)
                          GROUP BY a.FechaInicio, a.Fecha_Fin, a.NitEmpresa, DATEPART(MONTH, DATEADD(DAY, master.dbo.spt_values.number, CONVERT(DATE, a.FechaInicio, 103)))) 
                         AS a1 LEFT OUTER JOIN
                             (SELECT        CASE Mes WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN
                                                          7 THEN 'Julio' WHEN 8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre'
                                                          END AS NomMes, COUNT(*) AS TotalAus, Mes, NitEmpresa
                               FROM            dbo.V_IndFrecuenciaAusentismo
                               WHERE        (FK_Id_Contingencia = 3)
                               GROUP BY Mes, NitEmpresa) AS b1 ON a1.Mes = b1.Mes AND a1.NitEmpresa = b1.NitEmpresa
GROUP BY a1.Mes, a1.NomMes, b1.TotalAus, a1.FechaInicio, a1.NitEmpresa


GO
/****** Object:  View [dbo].[V_IndPlanCapacitacion]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_IndPlanCapacitacion]
AS
SELECT        Anio, Mes, NitEmpresa, SUM(CASE WHEN pk_id_plan_capacitacion > 0 THEN 1 ELSE 0 END) AS Programadas, 
                         SUM(CASE WHEN asistencia >= 1 THEN 1 ELSE 0 END) AS Ejecutadas
FROM            (SELECT        dbo.Tbl_PlanCapacitacion.pk_id_plan_capacitacion, YEAR(CONVERT(date, dbo.Tbl_PlanCapacitacion.fecha_programada, 103)) AS Anio, 
                                                    MONTH(CONVERT(date, dbo.Tbl_PlanCapacitacion.fecha_programada, 103)) AS Mes, dbo.Tbl_PlanCapacitacion.NitEmpresa, 
                                                    SUM(CASE WHEN Tbl_PlanCapacitacion_Asignaciones.asistencia = 1 THEN 1 ELSE 0 END) AS asistencia
                          FROM            dbo.Tbl_PlanCapacitacion LEFT OUTER JOIN
                                                    dbo.Tbl_PlanCapacitacion_Asignaciones ON 
                                                    dbo.Tbl_PlanCapacitacion.pk_id_plan_capacitacion = dbo.Tbl_PlanCapacitacion_Asignaciones.fk_id_plan_capacitacion
                          GROUP BY dbo.Tbl_PlanCapacitacion.pk_id_plan_capacitacion, YEAR(CONVERT(date, dbo.Tbl_PlanCapacitacion.fecha_programada, 103)), MONTH(CONVERT(date, 
                                                    dbo.Tbl_PlanCapacitacion.fecha_programada, 103)), dbo.Tbl_PlanCapacitacion.NitEmpresa) AS vista
GROUP BY Anio, Mes, NitEmpresa





GO
/****** Object:  View [dbo].[V_IndPlanTrabajoAnual]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndPlanTrabajoAnual]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Plan_Empresa.IdSede, dbo.Tbl_Plan_Empresa.Vigencia, 
                         dbo.Tbl_Plan_Empresa_Actividad.Estado, dbo.Tbl_Plan_Empresa_Actividad.pk_plan_empresa, dbo.Tbl_Plan_Empresa_Actividad.FechaProg, 
                         MONTH(CONVERT(datetime, dbo.Tbl_Plan_Empresa_Actividad.FechaProg, 103)) AS MesProg, YEAR(CONVERT(datetime, 
                         dbo.Tbl_Plan_Empresa_Actividad.FechaProg, 103)) AS AnioProg, dbo.Tbl_Plan_Empresa_Actividad.FechaEje, 
                         CASE Tbl_Plan_Empresa_Actividad.FechaEje WHEN '' THEN 0 ELSE MONTH(CONVERT(datetime, Tbl_Plan_Empresa_Actividad.FechaEje, 103)) END AS MesEje, 
                         CASE Tbl_Plan_Empresa_Actividad.FechaEje WHEN '' THEN 0 ELSE YEAR(CONVERT(datetime, Tbl_Plan_Empresa_Actividad.FechaEje, 103)) END AS AnioEje, 
                         ISNULL(dbo.Tbl_Plan_Empresa_Actividad.FechaReProg, '') AS Expr1, ISNULL(MONTH(CONVERT(datetime, dbo.Tbl_Plan_Empresa_Actividad.FechaReProg, 103)), 0) 
                         AS MesReprog, ISNULL(YEAR(CONVERT(datetime, dbo.Tbl_Plan_Empresa_Actividad.FechaReProg, 103)), 0) AS AnioReprog, dbo.Tbl_Plan_Empresa.FechaDesde, 
                         dbo.Tbl_Plan_Empresa.FechaHasta, dbo.Tbl_Plan_Empresa_Actividad.Actividad
FROM            dbo.Tbl_Plan_Empresa_Actividad INNER JOIN
                         dbo.Tbl_Plan_Empresa ON dbo.Tbl_Plan_Empresa_Actividad.pk_plan_empresa = dbo.Tbl_Plan_Empresa.pk_id_plan_empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Plan_Empresa.IdSede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa


GO
/****** Object:  View [dbo].[V_IndSeveridadAusentismo]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndSeveridadAusentismo]
AS
SELECT        dbo.Tbl_Ausencias.NitEmpresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Ausencias.FK_Id_Sede, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Ausencias.FK_Id_Contingencia, dbo.Tbl_Ausencias.FechaInicio, YEAR(dbo.Tbl_Ausencias.FechaInicio) AS Anio, dbo.Tbl_Ausencias.DiasAusencia, 
                         dbo.Tbl_Ausencias.Fecha_Fin, dbo.Tbl_Contingencias.Detalle, MONTH(dbo.Tbl_Ausencias.FechaInicio) AS Mes,
						 IIF(YEAR(dbo.Tbl_Ausencias.Fecha_Fin)=YEAR(dbo.Tbl_Ausencias.FechaInicio),
						  DATEDIFF ( DAY , DATEADD(DAY,-1,dbo.Tbl_Ausencias.FechaInicio) , dbo.Tbl_Ausencias.Fecha_Fin ),
						  DATEDIFF ( DAY , DATEADD(DAY,-1,dbo.Tbl_Ausencias.FechaInicio) , DATEFROMPARTS(YEAR(dbo.Tbl_Ausencias.FechaInicio),12,31) ) ) as DiasAusenciaFormateados

FROM            dbo.Tbl_Ausencias INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Empresa.Nit_Empresa = dbo.Tbl_Ausencias.NitEmpresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Ausencias.FK_Id_Sede INNER JOIN
                         dbo.Tbl_Contingencias ON dbo.Tbl_Ausencias.FK_Id_Contingencia = dbo.Tbl_Contingencias.PK_Id_Contingencia


GO
/****** Object:  View [dbo].[V_IndTasaAccidentalidad]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IndTasaAccidentalidad]
AS
SELECT        dbo.Tbl_Ausencias.NitEmpresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Ausencias.FK_Id_Sede, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Ausencias.FK_Id_Contingencia, dbo.Tbl_Ausencias.FechaInicio, MONTH(dbo.Tbl_Ausencias.FechaInicio) AS Mes, YEAR(dbo.Tbl_Ausencias.FechaInicio) 
                         AS Anio, dbo.Tbl_Contingencias.Detalle
FROM            dbo.Tbl_Ausencias INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Empresa.Nit_Empresa = dbo.Tbl_Ausencias.NitEmpresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Ausencias.FK_Id_Sede INNER JOIN
                         dbo.Tbl_Contingencias ON dbo.Tbl_Ausencias.FK_Id_Contingencia = dbo.Tbl_Contingencias.PK_Id_Contingencia


GO
/****** Object:  View [dbo].[V_IngresosPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_IngresosPerfilSocio]
AS

select  Ingresos,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,



Count(Ingresos)as Total

from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio

--Where Nit_Empresa=892099149

group by ps.Ingresos,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, Sexo,Nombre_Municipio


GO
/****** Object:  View [dbo].[V_INSHT_MATRIIZ]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_INSHT_MATRIIZ]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social AS razonsoc, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, Tbl_Proceso_1.Descripcion_Proceso AS SUBPROCESO, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS PELIGRO_CLASIFICACION, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO_DESCRIPCIÓN, 
                         COUNT(dbo.Tbl_Persona_Expuesta_INSHT_RAM.Total) AS NUMERO_DE_EXPUESTOS_TOTAL, dbo.Tbl_INSHT.Estimacion_Riesgo AS ESTIMACIÓN_DEL_RIESGO, 
                         CASE WHEN dbo.Tbl_INSHT.Riesgo_Controlado = 'true' THEN 'Si' ELSE 'No' END AS RIESGO_CONTROLADO, dbo.Tbl_Peligro.Fecha_De_Evaluacion AS FECHA, MONTH(dbo.Tbl_Peligro.Fecha_De_Evaluacion) 
                         AS MES, YEAR(dbo.Tbl_Peligro.Fecha_De_Evaluacion) AS AÑO, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Peligro INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Peligro.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Peligro.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_INSHT INNER JOIN
                         dbo.Tbl_Persona_Expuesta_INSHT_RAM ON dbo.Tbl_INSHT.FK_Persona_Expuesta = dbo.Tbl_Persona_Expuesta_INSHT_RAM.PK_Persona_Expuesta ON 
                         dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Persona_Expuesta_INSHT_RAM.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Probabilidad INNER JOIN
                         dbo.Tbl_Consecuencia_Por_Peligro INNER JOIN
                         dbo.Tbl_Tipo_Metodologia INNER JOIN
                         dbo.Tbl_Consecuencia INNER JOIN
                         dbo.Tbl_Grupo ON dbo.Tbl_Consecuencia.FK_Grupo = dbo.Tbl_Grupo.PK_Grupo ON dbo.Tbl_Tipo_Metodologia.PK_Metodologia = dbo.Tbl_Grupo.FK_Metodologia ON 
                         dbo.Tbl_Consecuencia_Por_Peligro.FK_Consecuencia = dbo.Tbl_Consecuencia.PK_Consecuencia ON dbo.Tbl_Probabilidad.FK_Metodologia = dbo.Tbl_Tipo_Metodologia.PK_Metodologia ON 
                         dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Consecuencia_Por_Peligro.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso LEFT OUTER JOIN
                         dbo.Tbl_Tipo_De_Peligro AS Tbl_Tipo_De_Peligro_1 ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = Tbl_Tipo_De_Peligro_1.PK_Tipo_De_Peligro
GROUP BY dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_INSHT.Estimacion_Riesgo, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Empresa.Razon_Social, Tbl_Proceso_1.Descripcion_Proceso, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_INSHT.Riesgo_Controlado, dbo.Tbl_Peligro.Fecha_De_Evaluacion, 
                         MONTH(dbo.Tbl_Peligro.Fecha_De_Evaluacion), YEAR(dbo.Tbl_Peligro.Fecha_De_Evaluacion), CASE WHEN dbo.Tbl_INSHT.Riesgo_Controlado = 'true' THEN 'Si' ELSE 'No' END, dbo.Tbl_Empresa.Nit_Empresa





GO
/****** Object:  View [dbo].[V_Inspecciones]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[V_Inspecciones]
AS
SELECT        i.Fecha_Realizacion, i.Pk_Id_Inspecciones, i.Fk_Id_PlaneacionInspeccion, i.Hora, i.Descripcion_Tipo_Inspeccion AS [Descripcion Tipo Inspeccion], 
                         mti.Descripcion_Tipo_Inspeccion AS [Tipo Inspección], i.Fk_Id_Sede, sede.Nombre_Sede AS Sede, i.Fk_Id_Proceso, pro.Descripcion_Proceso AS Proceso, 
                         i.Id_Inspeccion, case when i.Estado_Inspeccion=0 then 'Ejecutada' else 'Sin Ejecutar' end as Estado_Inspeccion, i.Fk_IdEmpresa, emp.Razon_Social, i.Area_Lugar, i.Responsable_Lugar, emp.Nit_Empresa, 
                         plin.Responsable_Tipo_Inspeccion, plin.Fecha, plin.IdEmpresa, plin.EstadoPlaneacion, plin.ConsecutivoPlan
FROM            dbo.Tbl_Sede AS sede FULL OUTER JOIN
                         dbo.Tbl_Inspecciones AS i ON sede.Pk_Id_Sede = i.Fk_Id_Sede FULL OUTER JOIN
                         dbo.Tbl_Empresa AS emp ON sede.Fk_Id_Empresa = emp.Pk_Id_Empresa LEFT OUTER JOIN
                         dbo.Tbl_Proceso AS pro ON i.Fk_Id_Proceso = pro.Pk_Id_Proceso FULL OUTER JOIN
                         dbo.Tbl_Maestro_Planeación_Inspeccion AS mti FULL OUTER JOIN
                         dbo.Tbl_Planeacion_Inspeccion AS plin ON mti.Pk_Id_Maestro_Tipo_Inspeccion = plin.Fk_Id_Maestro_Tipo_Inspeccion ON 
                         i.Fk_Id_PlaneacionInspeccion = plin.Pk_Id_PlaneacionInspeccion



GO
/****** Object:  View [dbo].[V_Matriz_RAM]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_Matriz_RAM]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social AS razonsoc, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, Tbl_Proceso_1.Descripcion_Proceso AS SUBPROCESO, 
                         dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro AS PELIGRO_CLASIFICACION, dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro AS PELIGRO_DESCRIPCIÓN, 
                         COUNT(dbo.Tbl_Persona_Expuesta_INSHT_RAM.Total) AS NUMERO_DE_EXPUESTOS_TOTAL, dbo.Tbl_RAM.Nivel_De_Riesgo, dbo.Tbl_Empresa.Nit_Empresa
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Peligro INNER JOIN
                         dbo.Tbl_Proceso ON dbo.Tbl_Peligro.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Peligro.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro ON dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro = dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro INNER JOIN
                         dbo.Tbl_RAM INNER JOIN
                         dbo.Tbl_Persona_Expuesta_INSHT_RAM ON dbo.Tbl_RAM.FK_Persona_Expuesta = dbo.Tbl_Persona_Expuesta_INSHT_RAM.PK_Persona_Expuesta ON 
                         dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Persona_Expuesta_INSHT_RAM.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Probabilidad INNER JOIN
                         dbo.Tbl_Consecuencia_Por_Peligro INNER JOIN
                         dbo.Tbl_Tipo_Metodologia INNER JOIN
                         dbo.Tbl_Consecuencia INNER JOIN
                         dbo.Tbl_Grupo ON dbo.Tbl_Consecuencia.FK_Grupo = dbo.Tbl_Grupo.PK_Grupo ON dbo.Tbl_Tipo_Metodologia.PK_Metodologia = dbo.Tbl_Grupo.FK_Metodologia ON 
                         dbo.Tbl_Consecuencia_Por_Peligro.FK_Consecuencia = dbo.Tbl_Consecuencia.PK_Consecuencia ON dbo.Tbl_Probabilidad.FK_Metodologia = dbo.Tbl_Tipo_Metodologia.PK_Metodologia ON 
                         dbo.Tbl_Peligro.PK_Peligro = dbo.Tbl_Consecuencia_Por_Peligro.FK_Peligro LEFT OUTER JOIN
                         dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso LEFT OUTER JOIN
                         dbo.Tbl_Tipo_De_Peligro AS Tbl_Tipo_De_Peligro_1 ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = Tbl_Tipo_De_Peligro_1.PK_Tipo_De_Peligro
GROUP BY dbo.Tbl_Proceso.Descripcion_Proceso, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Empresa.Razon_Social, Tbl_Proceso_1.Descripcion_Proceso, dbo.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro, 
                         dbo.Tbl_Tipo_De_Peligro.Descripcion_Del_Peligro, dbo.Tbl_RAM.Nivel_De_Riesgo, dbo.Tbl_Empresa.Nit_Empresa





GO
/****** Object:  View [dbo].[V_MEDIDASPREVENCIÓNCONTROL]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_MEDIDASPREVENCIÓNCONTROL]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_SegVialPilar.Descripcion AS Pilar, 
                         dbo.Tbl_SegVialParametro.Numeral AS Número, dbo.Tbl_SegVialDetalle.Numeral AS [Número parámetro], 
                         dbo.Tbl_SegVialParametro.ParametroDef AS [Parametro Definición], dbo.Tbl_SegVialDetalle.VariableDesc AS Variables, dbo.Tbl_PlanVial.Fecha_Registro,
						  
                         dbo.Tbl_SegVialDetalle.CriterioAval AS [Critero de Aval], 
						 dbo.Tbl_SegVialResultado.Aplica AS [Aplica NO], 
						 case when dbo.Tbl_SegVialResultado.Aplica_s=1 then 'Si' else 'No' end AS [Aplica SI], 
                         case when dbo.Tbl_SegVialResultado.Existencia=1 then 'Si' else 'No' end AS [Evidencia de su Existencia NO], 
						 dbo.Tbl_SegVialResultado.Existencia_s AS [Evidencia de su Existencia SI], 
                         dbo.Tbl_SegVialResultado.Responde AS [Responde a los requerimientos NO], 
						 case when dbo.Tbl_SegVialResultado.Responde_s=1 then 'Si' else 'No' end AS [Responde a los requerimientos SI], 
                         dbo.Tbl_SegVialParametro.Valor_Parametro AS [Valor Parametro], 
						 dbo.Tbl_SegVialResultado.ValorObtenido AS [Valor obtenido]
FROM            dbo.Tbl_SegVialDetalle INNER JOIN
                         dbo.Tbl_SegVialParametro ON dbo.Tbl_SegVialDetalle.Fk_Id_SegVialPilar = dbo.Tbl_SegVialParametro.Pk_Id_SegVialParametro INNER JOIN
                         dbo.Tbl_SegVialResultado ON dbo.Tbl_SegVialDetalle.Pk_Id_SegVialParametroDetalle = dbo.Tbl_SegVialResultado.Fk_Id_SegVialParametroDetalle INNER JOIN
                         dbo.Tbl_SegVialPilar ON dbo.Tbl_SegVialParametro.Fk_Id_SegVialPilar = dbo.Tbl_SegVialPilar.Pk_Id_SegVialPilar INNER JOIN
                         dbo.Tbl_PlanVial ON dbo.Tbl_SegVialResultado.Fk_Id_PlanVial = dbo.Tbl_PlanVial.Pk_Id_SegVial INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_PlanVial.Fk_Id_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa






GO
/****** Object:  View [dbo].[V_Noefecticomuni]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_Noefecticomuni]
AS

SELECT     DISTINCT   Tbl_ComunicacionesExternas.PK_Id_Comunicado, Tbl_ComunicacionesExternas.EstadoComunicado, Tbl_ComunicacionesExternas.FechaCreacion, 
                         MONTH(CONVERT(date, CONVERT(varchar(10), Tbl_ComunicacionesExternas.FechaEnvio, 101), 103)) AS mes, 
                         year(CONVERT(date, CONVERT(varchar(10), Tbl_ComunicacionesExternas.FechaEnvio, 101), 103)) AS año,CASE month(CONVERT(date, CONVERT(varchar(10), 
                         FechaEnvio, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaEnvio, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, Tbl_ComunicacionesExternas.FechaEnvio, Tbl_ComunicacionesExternas.NitEmpresa, 
                         'INTERNO' AS TIPO,[enviado_rechazado],'interno' as modulo
FROM            Tbl_ComunicacionesExternas inner JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicacionesExternas.PK_Id_Comunicado = Tbl_ComunicacionesLog.fk_id_comunicaciones
						  WHERE       enviado_rechazado=0

UNION
SELECT    DISTINCT           IDComunicadosAPP, Estado, Tbl_ComunicadosAPP.FechaCreacion, MONTH(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS mes, year(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS año, CASE month(CONVERT(date, 
                         CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, FechaEnvio, NitEmpresa, 'APP' AS TIPO,[enviado_rechazado],'app' as modulo
FROM            Tbl_ComunicadosAPP inner JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicadosAPP.IDComunicadosAPP = Tbl_ComunicacionesLog.fk_id_comunicaciones

 WHERE       enviado_rechazado=0


 UNION

 SELECT   pk_id_comadjunto,CASE WHEN TIPO in ('E') THEN 'Enviado' else 'En Espera' end AS Estado, fecha, year(CONVERT(date, 
                         CONVERT(varchar(10), Fecha, 101), 103)) AS año, MONTH(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) AS mes, CASE month(CONVERT(date, 
                         CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes,fecha as FechaEnvio, NitEmpresa, 'EXTERNO' AS TIPO,CASE WHEN TIPO in ('E') THEN '1' else '0' end AS 'enviado_rechazado','externo' as 'modulo'
  FROM [dbo].[Tbl_ComunicadosAdjutos] where TIPO in ('R')
GO
/****** Object:  View [dbo].[V_PERFIL_SOCIODEMOGRAFICO]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[V_PERFIL_SOCIODEMOGRAFICO]
AS

SELECT DISTINCT 
                         dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Sede.Nombre_Sede AS SEDE, 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.ZonaLugar AS [ZONA/LUGAR], dbo.Tbl_UsuarioSistema.Nombres, dbo.Tbl_UsuarioSistema.Apellidos, 
                         dbo.Tbl_UsuarioSistema.Documento AS [NÚMERO DOCUMENTO], dbo.Tbl_PerfilSocioDemograficoPlanificacion.GradoEscolaridad AS [GRADO DE ESCOLARIDAD], 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.Ingresos, dbo.Tbl_Municipio.Nombre_Municipio AS [MUNICIPIO DE RESIDENCIA], 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.Conyuge AS CONYUGUE, dbo.Tbl_PerfilSocioDemograficoPlanificacion.Hijos, 
                         dbo.Tbl_Estrato.Descripcion_Estrato AS [ESTRATO SOCIOECONÓMICO
ESTRATO SOCIOECONOMICO], 
                         dbo.Tbl_Estado_Civil.Descripcion_EstadoCivil AS [ESTADO CIVIL], dbo.Tbl_Etnia.Descripcion_Etnia AS ETNIA, CASE WHEN Edad BETWEEN 18 AND 
                         25 THEN ' Mayores a los  18 a 25' WHEN Edad BETWEEN 26 AND 35 THEN 'Mayores a los 26 a 35' WHEN Edad BETWEEN 36 AND 
                         45 THEN 'Mayores a los 36 a 45' WHEN Edad BETWEEN 46 AND 
                         55 THEN 'Mayores a los 46 a 55' WHEN Edad > 55 THEN 'Mayores a los 55' ELSE '' END AS 'Grupo Étareo', dbo.Tbl_PerfilSocioDemograficoPlanificacion.Sexo, 
                         dbo.Tbl_VinculacionLaboral.Descripcion_VinculacionLaboral AS [VINCULACIÓN LABORAL], 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.TurnoTrabajo AS [TURNO DE TRABAJO], 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.caracteristicasFisicas AS CARACTERISTICAS_FISICAS_DESEMPEÑO_DE_CARGO, 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.caracteristicasPsicologicas AS CARACTERISTICAS_PSICOLOGICAS, 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.evaluacionesMedicasRequeridas AS [EVALUACIÓN_MEDICA_REQUERIDA_PARA EL_DESEMPEÑO_DEL_CARGO], 
                         dbo.Tbl_Ausencias.Edad, dbo.Tbl_Ausencias.Eps
FROM            dbo.Tbl_Proceso RIGHT OUTER JOIN
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.Fk_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Etnia ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Etnia = dbo.Tbl_Etnia.PK_Etnia INNER JOIN
                         dbo.Tbl_Estrato ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Estrato = dbo.Tbl_Estrato.PK_Estrato INNER JOIN
                         dbo.Tbl_Estado_Civil ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Estado_Civil = dbo.Tbl_Estado_Civil.PK_Estado_Civil INNER JOIN
                         dbo.Tbl_VinculacionLaboral ON 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_VinculacionLaboral = dbo.Tbl_VinculacionLaboral.PK_VinculacionLaboral INNER JOIN
                         dbo.Tbl_Municipio ON dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Ciudad = dbo.Tbl_Municipio.Pk_Id_Municipio INNER JOIN
                         dbo.Tbl_Departamento ON dbo.Tbl_Municipio.Fk_Nombre_Departamento = dbo.Tbl_Departamento.Pk_Id_Departamento INNER JOIN
                         dbo.Tbl_UsuarioSistemaEmpresa ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_UsuarioSistemaEmpresa.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_UsuarioSistema ON dbo.Tbl_UsuarioSistemaEmpresa.Fk_Id_UsuarioSistema = dbo.Tbl_UsuarioSistema.Pk_Id_UsuarioSistema INNER JOIN
                         dbo.Tbl_Ausencias ON dbo.Tbl_UsuarioSistema.Documento = dbo.Tbl_Ausencias.Documento_Persona ON 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_PerfilSocioDemograficoPlanificacion.FK_Proceso LEFT OUTER JOIN
                         dbo.Tbl_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.PK_Clasificacion_De_Peligro = dbo.Tbl_Peligro.FK_Clasificacion_De_Peligro INNER JOIN
                         dbo.Tbl_Tipo_De_Peligro ON dbo.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro = dbo.Tbl_Tipo_De_Peligro.PK_Tipo_De_Peligro ON 
                         dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_Peligro.FK_Sede AND dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_Peligro.FK_Proceso LEFT OUTER JOIN
                         dbo.Tbl_Ocupaciones_De_Perfil ON 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.Ocupaciones_Perfil_PK_OcupacionPerfil = dbo.Tbl_Ocupaciones_De_Perfil.PK_OcupacionPerfil LEFT OUTER JOIN
                         dbo.Tbl_Condiciones_Riesgo_Perfil ON 
                         dbo.Tbl_PerfilSocioDemograficoPlanificacion.IDEmpleado_PerfilSocioDemoGrafico = dbo.Tbl_Condiciones_Riesgo_Perfil.FK_PerfilSocioDemografico







GO
/****** Object:  View [dbo].[V_periodosedes]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_periodosedes]
AS
SELECT DISTINCT Tbl_Presupuesto_Por_Año.Periodo, Tbl_Presupuesto_Por_Año.FK_Sede, Tbl_Presupuesto_Por_Año.FK_Presupuesto
FROM            Tbl_Presupuesto_Por_Año INNER JOIN
                         Tbl_Sede ON Tbl_Presupuesto_Por_Año.FK_Sede = Tbl_Sede.Pk_Id_Sede INNER JOIN
                         Tbl_Empresa ON Tbl_Sede.Fk_Id_Empresa = Tbl_Empresa.Pk_Id_Empresa

GO
/****** Object:  View [dbo].[V_PlanCapacitacion]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_PlanCapacitacion]
AS
SELECT DISTINCT 
                         dbo.Tbl_Empresa.Razon_Social AS [Razon Social], dbo.Tbl_Empresa.Nit_Empresa, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 1 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Enero, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 2 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Febrero, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 3 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Marzo, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 4 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Abril, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 5 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Mayo, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 6 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Junio, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 7 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Julio, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 8 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Agosto, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 9 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Septiembre, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 10 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Octubre, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 11 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Noviembre, CASE WHEN MONTH(dbo.Tbl_PlanCapacitacion.fecha_programada) 
                         = 12 THEN dbo.Tbl_PlanCapacitacion.fecha_programada END AS Diciembre, dbo.Tbl_Rol.Descripcion AS Rol, dbo.Tbl_Tematica.Tematicas AS Competencia, 
                         dbo.Tbl_PlanCapacitacion.fecha_programada AS [Fecha Programada], dbo.Tbl_PlanCapacitacion.hora_inicio, dbo.Tbl_PlanCapacitacion.hora_fin
FROM            dbo.Tbl_Rol INNER JOIN
                         dbo.Tbl_PlanCapacitacion ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_PlanCapacitacion.fk_id_rol INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Rol.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tematica_Por_Empresa ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Tematica_Por_Empresa.Fk_Id_Empresa INNER JOIN
                         dbo.Tbl_Tematica ON dbo.Tbl_Tematica_Por_Empresa.Fk_Id_Tematica = dbo.Tbl_Tematica.Id_Tematica AND 
                         dbo.Tbl_PlanCapacitacion.fk_id_competencia = dbo.Tbl_Tematica.Id_Tematica INNER JOIN
                         dbo.Tbl_Empleado_Por_Tematica ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_Empleado_Por_Tematica.Fk_Id_Rol INNER JOIN
                         dbo.Tbl_Empleado_Tematica ON dbo.Tbl_Empleado_Por_Tematica.Fk_Id_Tematica = dbo.Tbl_Empleado_Tematica.Pk_Id_EmpleadoTematica INNER JOIN
                         dbo.Tbl_Cargo_Por_Rol ON dbo.Tbl_Rol.Pk_Id_Rol = dbo.Tbl_Cargo_Por_Rol.Fk_Id_Rol INNER JOIN
                         dbo.Tbl_Cargo ON dbo.Tbl_Cargo_Por_Rol.Fk_Id_Cargo = dbo.Tbl_Cargo.Pk_Id_Cargo




GO
/****** Object:  View [dbo].[V_Planeación_Inspeccion]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_Planeación_Inspeccion]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Inspecciones.Descripcion_Tipo_Inspeccion
FROM            dbo.Tbl_Inspecciones INNER JOIN
                         dbo.Tbl_Maestro_Planeación_Inspeccion INNER JOIN
                         dbo.Tbl_Planeacion_Inspeccion ON 
                         dbo.Tbl_Maestro_Planeación_Inspeccion.Pk_Id_Maestro_Tipo_Inspeccion = dbo.Tbl_Planeacion_Inspeccion.Fk_Id_Maestro_Tipo_Inspeccion ON 
                         dbo.Tbl_Inspecciones.Fk_Id_PlaneacionInspeccion = dbo.Tbl_Planeacion_Inspeccion.Pk_Id_PlaneacionInspeccion INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Inspecciones.Fk_Id_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa





GO
/****** Object:  View [dbo].[V_PlanEmergenciaFrentesAccion]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_PlanEmergenciaFrentesAccion]
AS
SELECT DISTINCT 
                         em.Nit_Empresa, ig.razon_social, s.Nombre_Sede, fa.plan_seguridadfisica, fa.plan_primerosaux, fa.plan_contraincendios, fa.nombrecoordinador, fa.objetivos, 
                         fa.estructura, fa.proc_coordinacion, fa.proc_internos, fa.proc_externos, fa.mecanismos_alarma, fa.simulacros, fa.instructivo_evacuacion, fa.proc_retorno, 
                         ig.fk_id_sede
FROM            dbo.Tbl_Eme_InfoGeneral AS ig INNER JOIN
                         dbo.Tbl_Sede AS s ON ig.fk_id_sede = s.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Eme_FrentesAccion AS fa ON ig.fk_id_sede = fa.fk_id_sede INNER JOIN
                         dbo.Tbl_Empresa AS em ON s.Fk_Id_Empresa = em.Pk_Id_Empresa



GO
/****** Object:  View [dbo].[V_PlanEmergenciaInfoGeneral]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_PlanEmergenciaInfoGeneral]
AS
SELECT      DISTINCT  em.Nit_Empresa, ig.razon_social, s.Nombre_Sede, ig.direccion_sede, ig.departamento_sede, ig.municipio_sede, ig.lindero_norte, ig.lindero_sur, ig.lindero_oriente, 
                         ig.lindero_occidente, ig.acceso_principales, ig.acceso_alternas, ig.representante, do.trabajadores_cantidad, do.contratista_cantidad, do.visitante_cantidad, 
                         do.cliente_cantidad, ci.ventilacion_mecanica, ci.ascensores, ci.sotanos, ci.red_hidraulica, ci.transformadores, ci.plantas_elealtericas, ci.escaleras, ci.zonas_parqueo, 
                         ci.areas_especiales, g.punto_encuentro, g.ubicacion_hidrantes, ig.fk_id_sede
FROM            dbo.Tbl_Eme_InfoGeneral AS ig INNER JOIN
                         dbo.Tbl_Sede AS s ON ig.fk_id_sede = s.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Eme_DescripcionOcupacion AS do ON ig.fk_id_sede = do.fk_id_sede INNER JOIN
                         dbo.Tbl_Eme_CaracteristicasInstalacion AS ci ON ig.fk_id_sede = ci.fk_id_sede INNER JOIN
                         dbo.Tbl_Eme_Georeferenciacion AS g ON ig.fk_id_sede = g.fk_id_sede INNER JOIN
                         dbo.Tbl_Empresa AS em ON s.Fk_Id_Empresa = em.Pk_Id_Empresa



GO
/****** Object:  View [dbo].[V_planesaccion_todo]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[V_planesaccion_todo]
AS

	
	SELECT        dbo.Tbl_Empresa.Nit_Empresa, CASE WHEN dbo.Tbl_Empresa.Razon_Social IS NOT NULL 
                         THEN dbo.Tbl_Empresa.Razon_Social ELSE 'SIN EMPRESA' END AS Razon_Social, dbo.Tbl_Modulos_Plan_Accion.Modulo, 
                         dbo.Tbl_Modulos_Plan_Accion.Actividad, SUM(CASE WHEN dbo.Tbl_Actividad_Plan_Accion.FechaCierre IS NOT NULL THEN 1 ELSE 0 END) 
                         AS Totalejecutadas_plan
FROM            dbo.Tbl_PlanAccionCorrectiva RIGHT OUTER JOIN
                         dbo.Tbl_PlanAccionInspeccion AS Tbl_PlanAccionInspeccion_1 ON 
                         dbo.Tbl_PlanAccionCorrectiva.Fk_Id_PlanAcccionInspeccion = Tbl_PlanAccionInspeccion_1.Pk_Id_PlanAcccionInspeccion LEFT OUTER JOIN
                         dbo.Tbl_PlanAccionporCondicion ON 
                         Tbl_PlanAccionInspeccion_1.Pk_Id_PlanAcccionInspeccion = dbo.Tbl_PlanAccionporCondicion.Fk_Id_PlanAcccionInspeccion RIGHT OUTER JOIN
                         dbo.Tbl_ActividadesActosInseguros INNER JOIN
                         dbo.Tbl_Actividad_Plan_Accion ON 
                         dbo.Tbl_ActividadesActosInseguros.PK_ID_ActividadActosInseguros = dbo.Tbl_Actividad_Plan_Accion.Fk_Id_Actividad RIGHT OUTER JOIN
                         dbo.Tbl_Modulos_Plan_Accion ON dbo.Tbl_Actividad_Plan_Accion.Fk_Id_ModuloPlanAccion = dbo.Tbl_Modulos_Plan_Accion.Pk_Id_ModuloPlanAccion ON 
                         Tbl_PlanAccionInspeccion_1.Pk_Id_PlanAcccionInspeccion = dbo.Tbl_Actividad_Plan_Accion.Fk_Plan_Inspección FULL OUTER JOIN
                         dbo.Tbl_Reportes INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Reportes.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Acciones INNER JOIN
                         dbo.Tbl_ActividadAccion ON dbo.Tbl_Acciones.Pk_Id_Accion = dbo.Tbl_ActividadAccion.Fk_Id_Accion ON 
                         dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Acciones.Fk_Id_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa AND 
                         dbo.Tbl_Reportes.FK_NitEmpresa = dbo.Tbl_Empresa.Nit_Empresa LEFT OUTER JOIN
                         dbo.Tbl_ActasCopasst INNER JOIN
                         dbo.Tbl_AccionesActaCopasst ON dbo.Tbl_ActasCopasst.PK_Id_Acta = dbo.Tbl_AccionesActaCopasst.Fk_Id_Acta ON 
                         dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_ActasCopasst.Fk_Id_Sede FULL OUTER JOIN
                         dbo.Tbl_ActaRevision RIGHT OUTER JOIN
                         dbo.Tbl_PlanAccionRevision ON dbo.Tbl_ActaRevision.PK_Id_ActaRevision = dbo.Tbl_PlanAccionRevision.FK_Acta ON 
                         dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_ActaRevision.FK_Sede ON dbo.Tbl_ActividadesActosInseguros.FK_Id_Reportes = dbo.Tbl_Reportes.Pk_Id_Reportes
WHERE        (dbo.Tbl_Empresa.Razon_Social IS NOT NULL) 
GROUP BY dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Modulos_Plan_Accion.Modulo, dbo.Tbl_Modulos_Plan_Accion.Actividad, dbo.Tbl_Empresa.Nit_Empresa
   



GO
/****** Object:  View [dbo].[V_PlanTrabajoEmpresa]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_PlanTrabajoEmpresa]
AS

          SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_AplicacionPlanTrabajo.Vigencia,
          dbo.Tbl_AplicacionPlanTrabajo.FechaInicio, dbo.Tbl_AplicacionPlanTrabajo.FechaFinal, CASE WHEN month(FechaInicio)
          = 1 THEN 'Enero' ELSE '' END AS [Periodo Enero], CASE WHEN month(FechaInicio) = 2 THEN 'Febrero' ELSE '' END AS [Periodo Febrero],
          CASE WHEN month(FechaInicio) = 3 THEN 'Marzo' ELSE '' END AS [Periodo Marzo], CASE WHEN month(FechaInicio) = 4 THEN 'Abril' ELSE '' END AS [Periodo Abril],
          CASE WHEN month(FechaInicio) = 5 THEN 'Mayo' ELSE '' END AS [Periodo Mayo], CASE WHEN month(FechaInicio) = 6 THEN 'Junio' ELSE '' END AS [Periodo Junio],
          CASE WHEN month(FechaInicio) = 7 THEN 'Julio' ELSE '' END AS [Periodo Julio], CASE WHEN month(FechaInicio) = 8 THEN 'Agosto' ELSE '' END AS [Periodo Agosto],
          CASE WHEN month(FechaInicio) = 9 THEN 'Septiembre' ELSE '' END AS [Periodo Septiembre], CASE WHEN month(FechaInicio)
          = 10 THEN 'Octubre' ELSE '' END AS [Periodo Octubre], CASE WHEN month(FechaInicio) = 11 THEN 'Noviembre' ELSE '' END AS [Periodo Noviembre],
          CASE WHEN month(FechaInicio) = 12 THEN 'Diciembre' ELSE '' END AS [Periodo Diciembre],case when Tbl_AplicacionPlanTrabajoProgramacion.Estado=0 OR  (Tbl_AplicacionPlanTrabajoProgramacion.Estado)IS NULL then 'Sin Estado'  
when Tbl_AplicacionPlanTrabajoProgramacion.Estado=1 then 'Programada'
when Tbl_AplicacionPlanTrabajoProgramacion.Estado=2 then 'Reprogramada'
when Tbl_AplicacionPlanTrabajoProgramacion.Estado=3 then 'Ejecutada'
END as Estado,
           COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad)
          AS Total_Actividades_Programadas,
          CASE WHEN Tbl_AplicacionPlanTrabajoProgramacion.Estado = '3' THEN COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad)
          ELSE 0 END AS Total_Actividades_Ejecutadas,
          CASE WHEN Tbl_AplicacionPlanTrabajoProgramacion.Estado = '3' THEN COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad)
          * 100 / COUNT(dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad) ELSE 0 END AS Porcentaje_Ejecución
          FROM            dbo.Tbl_AplicacionPlanTrabajoActividad INNER JOIN
          dbo.Tbl_AplicacionPlanTrabajoProgramacion ON
          dbo.Tbl_AplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad = dbo.Tbl_AplicacionPlanTrabajoProgramacion.Fk_Id_PlanTrabajoActividad FULL OUTER JOIN
          dbo.Tbl_Sede INNER JOIN
          dbo.Tbl_AplicacionPlanTrabajo ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_AplicacionPlanTrabajo.Fk_Id_Sede INNER JOIN
          dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa FULL OUTER JOIN
          dbo.Tbl_AplicacionPlanTrabajoDetalle ON dbo.Tbl_AplicacionPlanTrabajo.Pk_Id_PlanTrabajo = dbo.Tbl_AplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo ON
          dbo.Tbl_AplicacionPlanTrabajoActividad.Fk_Id_PlanTrabajoDetalle = dbo.Tbl_AplicacionPlanTrabajoDetalle.Pk_Id_PlanTrabajoDetalle


         
          GROUP BY dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_AplicacionPlanTrabajo.Vigencia,
          dbo.Tbl_AplicacionPlanTrabajo.FechaInicio, dbo.Tbl_AplicacionPlanTrabajo.FechaFinal, dbo.Tbl_AplicacionPlanTrabajoProgramacion.Estado
        





GO
/****** Object:  View [dbo].[V_presupuesto]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_presupuesto]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social AS EMPRESA, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Presupuesto_Por_Año.Periodo AS PERIODO_AÑO, dbo.Tbl_Presupuesto.RubroTotal AS RUBRO_TOTAL, 
                         SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoMes) AS TOTAL_PLANEADO, SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoEjecutadoPorMes) 
                         AS TOTAL_EJECUTADO, SUM(Tbl_Prepuesto_Por_Mes.PresupuestoMes) - SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoEjecutadoPorMes) AS SALDO
FROM            dbo.Tbl_Prepuesto_Por_Mes INNER JOIN
                         dbo.Tbl_Presupuesto ON dbo.Tbl_Prepuesto_Por_Mes.FK_Presupuesto = dbo.Tbl_Presupuesto.PK_Prepuesto INNER JOIN
                         dbo.Tbl_Presupuesto_Por_Año ON dbo.Tbl_Presupuesto.PK_Prepuesto = dbo.Tbl_Presupuesto_Por_Año.FK_Presupuesto INNER JOIN
                         dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa ON 
                         dbo.Tbl_Presupuesto_Por_Año.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede
GROUP BY dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Presupuesto_Por_Año.Periodo
,dbo.Tbl_Presupuesto.RubroTotal





GO
/****** Object:  View [dbo].[V_Proceso_as]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_Proceso_as]
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
/****** Object:  View [dbo].[V_REPORTE ENFERMEDADES LABORALES]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[V_REPORTE ENFERMEDADES LABORALES]
AS
SELECT        dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_UsuarioSistema.Documento, dbo.Tbl_UsuarioSistema.Nombres, 
                         dbo.Tbl_UsuarioSistema.Apellidos, dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion, 
                         MONTH(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion) AS Mes, 
                         YEAR(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion) AS Año, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Diagnostico, 
                         dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.EstadoInstancia, COUNT(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.EstadoInstancia) 
                         AS Total_Estado, dbo.Tbl_EstadosInstanciasRegistradas.Nombre AS Nombre_Estado, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.CodigoDiagnosticoCIIE10, 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaDocumentoFUREL, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaCartaEnviadaEPS
FROM            dbo.Tbl_Empresa INNER JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_Empresa.Pk_Id_Empresa = dbo.Tbl_Sede.Fk_Id_Empresa CROSS JOIN
                         dbo.Tbl_UsuarioSistema INNER JOIN
                         dbo.Tbl_EstadosInstanciasRegistradas INNER JOIN
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas INNER JOIN
                         dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada ON 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Pk_Id_EnfermedadLaboral = dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.Fk_Id_EnfermedadLaboral ON 
                         dbo.Tbl_EstadosInstanciasRegistradas.PK_Id_EstadoInstancia = dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FK_Id_EstadoInstancia ON 
                         dbo.Tbl_UsuarioSistema.Pk_Id_UsuarioSistema = dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.CodigoEmpleado FULL OUTER JOIN
                         dbo.Tbl_DocumentosEnviadosEPS ON 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Pk_Id_EnfermedadLaboral = dbo.Tbl_DocumentosEnviadosEPS.Fk_Id_EnfermedadLaboral
GROUP BY dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion, MONTH(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion), 
                         YEAR(dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.FechaCalificacion), dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.Diagnostico, 
                         dbo.Tbl_InstanciasEnfermedadLaboralDiagnosticada.EstadoInstancia, dbo.Tbl_EstadosInstanciasRegistradas.Nombre, 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.CodigoDiagnosticoCIIE10, dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaDocumentoFUREL, 
                         dbo.Tbl_EnfermedadesLaboralesDiagnosticadas.RutaCartaEnviadaEPS, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, 
                         dbo.Tbl_UsuarioSistema.Nombres, dbo.Tbl_UsuarioSistema.Apellidos, dbo.Tbl_UsuarioSistema.Documento





GO
/****** Object:  View [dbo].[V_Reporte_Incidentes]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_Reporte_Incidentes]
AS
SELECT        dbo.V_sede.Razon_Social, dbo.V_sede.SEDE AS Nombre_Sede, dbo.Tbl_Incidentes.Incidente_fecha, YEAR(dbo.Tbl_Incidentes.Incidente_fecha) AS Año, MONTH(dbo.Tbl_Incidentes.Incidente_fecha) AS Mes, 
                         dbo.Tbl_Tipo_Incidente.Nombre_Incidente AS Tipo_Incidente, CASE WHEN Tbl_ZonaLugar.Descripcion_ZonaLugar = 'U' THEN 'Urbano' ELSE 'Rural' END AS Zona, dbo.Tbl_Sitio_Incidente.Nombre_Sitio, 
                         CASE WHEN Tbl_Incidentes.Incidente_ocurre_dentro_empresa = 'true' THEN 'Dentro de la empresa' ELSE Tbl_Incidentes.Incidente_sitio_incidente_otro END AS [Lugar donde ocurrió el incidente], 
                         dbo.Tbl_Incidentes.Incidente_descripcion, dbo.Tbl_Incidente_Consecuencia.Nombre_consecuencia, dbo.V_sede.Nit_Empresa
FROM            dbo.Tbl_Incidentes INNER JOIN
                         dbo.Tbl_Tipo_Incidente ON dbo.Tbl_Incidentes.FK_id_incidente_tipo_incidente = dbo.Tbl_Tipo_Incidente.Pk_Id_Tipo_Incidente INNER JOIN
                         dbo.Tbl_ZonaLugar ON dbo.Tbl_Incidentes.FK_id_zonalugar_incidente = dbo.Tbl_ZonaLugar.PK_ZonaLugar INNER JOIN
                         dbo.Tbl_Sitio_Incidente ON dbo.Tbl_Incidentes.FK_id_sitio_incidente = dbo.Tbl_Sitio_Incidente.Pk_Id_Sitio_Incidente INNER JOIN
                         dbo.V_sede ON dbo.Tbl_Incidentes.FK_id_sede_general = dbo.V_sede.Pk_Id_Sede LEFT OUTER JOIN
                         dbo.Tbl_Incidente_Consecuencia ON dbo.Tbl_Incidentes.FK_id_consecuencia_incidente = dbo.Tbl_Incidente_Consecuencia.Pk_Id_Incidente_Consecuencia
GROUP BY dbo.Tbl_Tipo_Incidente.Nombre_Incidente, dbo.Tbl_Incidentes.Incidente_fecha, dbo.Tbl_Incidentes.FK_id_zonalugar_incidente, dbo.Tbl_ZonaLugar.Descripcion_ZonaLugar, 
                         dbo.Tbl_Incidentes.Incidente_sitio_incidente_otro, dbo.Tbl_Incidentes.Incidente_descripcion, dbo.Tbl_Incidentes.Incidente_ocurre_dentro_empresa, dbo.Tbl_Sitio_Incidente.Nombre_Sitio, 
                         dbo.Tbl_Incidente_Consecuencia.Nombre_consecuencia, dbo.V_sede.SEDE, dbo.V_sede.Razon_Social, dbo.V_sede.Nit_Empresa





GO
/****** Object:  View [dbo].[V_reporteactosycondinseguros]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_reporteactosycondinseguros]
AS

          SELECT        dbo.Tbl_Empresa.Nit_Empresa AS NIT_EMPRESA, dbo.Tbl_Empresa.Razon_Social AS RAZON_SOCIAL, dbo.Tbl_Sede.Nombre_Sede AS SEDE,
          dbo.Tbl_Reportes.Fecha_Ocurrencia AS FECHA, YEAR(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS AÑO, MONTH(dbo.Tbl_Reportes.Fecha_Ocurrencia) AS MES,
          dbo.Tbl_Reportes.Area_Lugar AS [AREA/LUGAR], dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, dbo.Tbl_Reportes.Causa_Reporte AS CAUSA,
          dbo.Tbl_Reportes.Sugerencias_Reporte AS SUGERENCIA
          FROM            dbo.Tbl_ActividadesActosInseguros FULL OUTER JOIN
          dbo.Tbl_Reportes ON dbo.Tbl_ActividadesActosInseguros.FK_Id_Reportes = dbo.Tbl_Reportes.Pk_Id_Reportes full outer JOIN
          dbo.Tbl_Sede ON dbo.Tbl_Reportes.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
          dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
          dbo.Tbl_Tipo_Reporte ON dbo.Tbl_Reportes.FK_Tipo_Reporte = dbo.Tbl_Tipo_Reporte.Pk_Id_Tipo_Reporte INNER JOIN
          dbo.Tbl_Proceso ON dbo.Tbl_Reportes.FK_Proceso = dbo.Tbl_Proceso.Pk_Id_Proceso INNER JOIN
          dbo.Tbl_Proceso AS Tbl_Proceso_1 ON dbo.Tbl_Proceso.Fk_Id_Proceso = Tbl_Proceso_1.Pk_Id_Proceso
       


GO
/****** Object:  View [dbo].[V_sede]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_sede]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede AS SEDE, dbo.Tbl_Sede.Pk_Id_Sede
FROM            dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_SedeMunicipio ON dbo.Tbl_Sede.Pk_Id_Sede = dbo.Tbl_SedeMunicipio.Fk_id_Sede
GROUP BY dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Pk_Id_Sede






GO
/****** Object:  View [dbo].[V_sede_presupuesto]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_sede_presupuesto]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social AS EMPRESA, dbo.Tbl_Sede.Nombre_Sede, 
                         dbo.Tbl_Presupuesto_Por_Año.Periodo AS PERIODO_AÑO, SUM(dbo.Tbl_Presupuesto.RubroTotal) AS RUBRO_TOTAL, 
                         SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoMes) AS TOTAL_PLANEADO, SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoEjecutadoPorMes) 
                         AS TOTAL_EJECUTADO, SUM(dbo.Tbl_Presupuesto.RubroTotal) - SUM(dbo.Tbl_Prepuesto_Por_Mes.PresupuestoEjecutadoPorMes) AS SALDO
FROM            dbo.Tbl_Prepuesto_Por_Mes INNER JOIN
                         dbo.Tbl_Presupuesto ON dbo.Tbl_Prepuesto_Por_Mes.FK_Presupuesto = dbo.Tbl_Presupuesto.PK_Prepuesto INNER JOIN
                         dbo.Tbl_Presupuesto_Por_Año ON dbo.Tbl_Presupuesto.PK_Prepuesto = dbo.Tbl_Presupuesto_Por_Año.FK_Presupuesto INNER JOIN
                         dbo.Tbl_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa ON 
                         dbo.Tbl_Presupuesto_Por_Año.FK_Sede = dbo.Tbl_Sede.Pk_Id_Sede
GROUP BY dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_Presupuesto_Por_Año.Periodo






GO
/****** Object:  View [dbo].[V_sedes]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_sedes]
AS
SELECT        Tbl_Sede.Nombre_Sede, Tbl_Sede.Pk_Id_Sede, Tbl_Empresa.Nit_Empresa
FROM            Tbl_Empresa INNER JOIN
                         Tbl_Sede ON Tbl_Empresa.Pk_Id_Empresa = Tbl_Sede.Fk_Id_Empresa





GO
/****** Object:  View [dbo].[V_SexoPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[V_SexoPerfilSocio]
AS

select  Sexo,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo,Pk_Id_Proceso,Descripcion_Proceso,Nombre_Municipio,
Count(Sexo)as Total

from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio

group by ps.Sexo,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, Sexo,Nombre_Municipio



GO
/****** Object:  View [dbo].[V_uniontotal]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[V_uniontotal]
AS
SELECT     DISTINCT   Tbl_ComunicacionesExternas.PK_Id_Comunicado, Tbl_ComunicacionesExternas.EstadoComunicado, Tbl_ComunicacionesExternas.FechaCreacion, 
                         MONTH(CONVERT(date, CONVERT(varchar(10), Tbl_ComunicacionesExternas.FechaEnvio, 101), 103)) AS mes, 
                         year(CONVERT(date, CONVERT(varchar(10), Tbl_ComunicacionesExternas.FechaEnvio, 101), 103)) AS año,CASE month(CONVERT(date, CONVERT(varchar(10), 
                         FechaEnvio, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaEnvio, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, Tbl_ComunicacionesExternas.FechaEnvio, Tbl_ComunicacionesExternas.NitEmpresa, 
                         'INTERNO' AS TIPO,[Titulo],[Asunto]
FROM            Tbl_ComunicacionesExternas left JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicacionesExternas.PK_Id_Comunicado = Tbl_ComunicacionesLog.fk_id_comunicaciones

where NitEmpresa=800167494

 UNION
SELECT    DISTINCT           IDComunicadosAPP, Estado, FechaCreacion, 
MONTH(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS mes, 
year(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) AS año, 
CASE month(CONVERT(date, 
                         CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes,
						   
                         CASE month(CONVERT(date, CONVERT(varchar(10), FechaCreacion, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes, FechaEnvio, NitEmpresa, 'APP' AS TIPO,[Titulo],[AsuntoAPP] as Asunto
FROM            Tbl_ComunicadosAPP left JOIN
                         Tbl_ComunicacionesLog ON Tbl_ComunicadosAPP.IDComunicadosAPP = Tbl_ComunicacionesLog.fk_id_comunicaciones
where NitEmpresa=800167494

UNION

SELECT   pk_id_comadjunto,CASE WHEN TIPO='E' THEN 'Enviado' else 'En Espera' end AS Estado, fecha, 
                       MONTH(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) AS mes,
                         year(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) AS año
						 
						 ,  CASE month(CONVERT(date, 
                         CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN 'Enero' WHEN 2 THEN 'Febrero' WHEN 3 THEN 'Marzo' WHEN 4 THEN 'Abril' WHEN 5 THEN 'Mayo' WHEN 6 THEN 'Junio' WHEN 7 THEN 'Julio' WHEN
                          8 THEN 'Agosto' WHEN 9 THEN 'Septiembre' WHEN 10 THEN 'Octubre' WHEN 11 THEN 'Noviembre' WHEN 12 THEN 'Diciembre' END AS NomMes, 
                         CASE month(CONVERT(date, CONVERT(varchar(10), fecha, 101), 103)) 
                         WHEN 1 THEN '1' WHEN 2 THEN '1' WHEN 3 THEN '1' WHEN 4 THEN '1' WHEN 5 THEN '1' WHEN 6 THEN '1' WHEN 7 THEN '1' WHEN 8 THEN '1' WHEN 9 THEN '1'
                          WHEN 10 THEN '1' WHEN 11 THEN '1' WHEN 12 THEN '1' END AS conmes,fecha as FechaEnvio, NitEmpresa, 'EXTERNO' AS TIPO,NOMBRE as 'Titulo',descripcion as 'Asunto'
  FROM [dbo].[Tbl_ComunicadosAdjutos]

  where NitEmpresa=800167494

GO
/****** Object:  View [dbo].[V_VinculacionLaboralPerfilSocio]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[V_VinculacionLaboralPerfilSocio]
AS

select  FK_VinculacionLaboral,
vl.Descripcion_VinculacionLaboral,
Nit_Empresa,
Pk_Id_Sede,
FechaIngresoUltimoCargo,
Pk_Id_Proceso,
Descripcion_Proceso,
Nombre_Municipio,



Count(Sexo)as Total

from Tbl_PerfilSocioDemograficoPlanificacion as PS
inner join Tbl_Sede as s on ps.Fk_Sede=s.Pk_Id_Sede
inner join Tbl_Empresa as e on s.Fk_Id_Empresa = e.Pk_Id_Empresa 
inner join Tbl_Proceso as p on PS.FK_Proceso= p.Pk_Id_Proceso
inner Join Tbl_Municipio as m on PS.FK_Ciudad= m.Pk_Id_Municipio
inner join Tbl_VinculacionLaboral as vl on PS.FK_VinculacionLaboral=vl.PK_VinculacionLaboral
--where Nit_Empresa=892099149
group by ps.FK_VinculacionLaboral,Nit_Empresa,Pk_Id_Sede,FechaIngresoUltimoCargo, p.Descripcion_Proceso, Pk_Id_Proceso, vl.Descripcion_VinculacionLaboral,Nombre_Municipio


GO
/****** Object:  View [dbo].[VistaRelacionesLaborales]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[VistaRelacionesLaborales]
AS
SELECT        et.ID_Empleado, et.FK_Tipo_Documento_Empl, td.Descripcion AS TipoDocumento, et.Numero_Documento_Empl, et.Nombre1, et.Nombre2, et.Apellido1, et.Apellido2, 
                         et.FechaNacimiento, et.Email, et.Ocupacion_Empl, et.Cargo_Empl, et.Email_Empl, tt_1.Descripcion_Tipo_Tercero AS RelacionesLaboralesTercero, 
                         dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social
FROM            (SELECT        ETT.ID_Empleado, ETT.FK_Tipo_Documento_Empl, ETT.Nombre1, ETT.Nombre2, ETT.Apellido1, ETT.Apellido2, ETT.FechaNacimiento, ETT.Email, 
                                                    ETT.Ocupacion_Empl, ETT.Cargo_Empl, ETT.Email_Empl, ETT.FK_EmpresaTercero, ETT.FKRelacionLaboralTercero, ETT.Numero_Documento_Empl, 
                                                    ETT.FK_Empresa, ETT.Genero, ETT.Direccion, ETT.Telefono, ETT.FK_id_departamento, ETT.FK_id_municipio, ETT.FK_id_zona, ETT.Ocupacion_habitual, 
                                                    ETT.Fecha_ingreso_empresa, ETT.FK_id_jornada_habitual, tet.Razon_Social, tet.PK_Nit_Empresa
                          FROM            dbo.Tbl_EmpleadoTercero AS ETT INNER JOIN
                                                    dbo.Tbl_EmpresaTercero AS tet ON ETT.FK_EmpresaTercero = tet.PK_Nit_Empresa) AS et INNER JOIN
                         dbo.Tbl_Empresa ON et.PK_Nit_Empresa = dbo.Tbl_Empresa.Nit_Empresa INNER JOIN
                         dbo.Tbl_Tipo_Documento AS td ON et.FK_Tipo_Documento_Empl = td.PK_IDTipo_Documento INNER JOIN
                             (SELECT        Pk_Id_TipoTercero, Descripcion_Tipo_Tercero
                               FROM            dbo.Tbl_TipoTercero AS tt) AS tt_1 ON et.FKRelacionLaboralTercero = tt_1.Pk_Id_TipoTercero





GO
/****** Object:  View [dbo].[VW_EstudioPuestosdeTrabajo]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[VW_EstudioPuestosdeTrabajo]
AS



SELECT        dbo.Tbl_Proceso.Descripcion_Proceso AS PROCESO, Tbl_Proceso_1.Descripcion_Proceso AS SUBPROCESO, dbo.Tbl_Sede.Nombre_Sede AS SEDE, 
                         dbo.Tbl_Empresa.Nit_Empresa AS NIT_EMPRESA, dbo.Tbl_Empresa.Razon_Social AS RAZON_SOCIAL, dbo.Tbl_EstudioPuestoTrabajo.FechaAnalisis, 
                         YEAR(dbo.Tbl_EstudioPuestoTrabajo.FechaAnalisis) AS AÑO, MONTH(dbo.Tbl_EstudioPuestoTrabajo.FechaAnalisis) AS MES, 
                         dbo.Tbl_Tipo_Analisis_Puesto_Trabajo.Nombre_Tipo_Analisis_Puesto_Trabajo AS TIPO_ANALISIS, dbo.Tbl_ObjetivoAnalisis.Nombre_ObjetivoAnalisis AS OBJETIVO,
                          dbo.Tbl_Diagnosticos.Descripcion AS DIAGNÓSTICO, dbo.Tbl_EstudioPuestoTrabajo.Cargo_Empleado AS CARGO_EMPLEADO
FROM            dbo.Tbl_Proceso AS Tbl_Proceso_1 INNER JOIN
                         dbo.Tbl_Proceso ON Tbl_Proceso_1.Pk_Id_Proceso = dbo.Tbl_Proceso.Fk_Id_Proceso RIGHT OUTER JOIN
                         dbo.Tbl_EstudioPuestoTrabajo INNER JOIN
                         dbo.Tbl_Seguimiento_Estudio_Puesto_Trabajo ON 
                         dbo.Tbl_EstudioPuestoTrabajo.Pk_Id_EstudioPuestoTrabajo = dbo.Tbl_Seguimiento_Estudio_Puesto_Trabajo.EstudioPuestoTrabajo_Pk_Id_EstudioPuestoTrabajo INNER
                          JOIN
                         dbo.Tbl_Sede ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Sede = dbo.Tbl_Sede.Pk_Id_Sede INNER JOIN
                         dbo.Tbl_Empresa ON dbo.Tbl_Sede.Fk_Id_Empresa = dbo.Tbl_Empresa.Pk_Id_Empresa INNER JOIN
                         dbo.Tbl_Diagnosticos ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Diagnostico = dbo.Tbl_Diagnosticos.PK_Id_Diagnostico INNER JOIN
                         dbo.Tbl_ObjetivoAnalisis ON dbo.Tbl_EstudioPuestoTrabajo.FK_Id_ObjetivoAnalisis = dbo.Tbl_ObjetivoAnalisis.Pk_Id_ObjetivoAnalisis INNER JOIN
                         dbo.Tbl_Tipo_Analisis_Puesto_Trabajo ON 
                         dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Tipo_Analisis_Puesto_Trabajo = dbo.Tbl_Tipo_Analisis_Puesto_Trabajo.Pk_Id_Tipo_Analisis_Puesto_Trabajo ON 
                         dbo.Tbl_Proceso.Pk_Id_Proceso = dbo.Tbl_EstudioPuestoTrabajo.FK_Id_Proceso





GO
/****** Object:  View [dbo].[VW_IncidentesEL]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[VW_IncidentesEL]
AS
SELECT        EnfLabCalificadaI as [Murio?], FechaInvestigacionI, MunicipioI, DireccionI, NombresApellidosI, EmpleadorII, NumIdentificacionII, RazonSocialII, ActividadPrincipalII, ZonaPpalII, PlantaIII, NumIdentificacionIII, 
                         PrimerApellidoIII, SegundoApellidoIII, PrimerNombreIII, SegundoNombreIII, FechaNacimientoIII, SexoIII, CargoIV, DedicacionJL1IV, RelacionPocoProbable1IV, FechaOrigenELV
FROM            dbo.Tbl_IncidentesEL





GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENCIA_POR_TIPO_VINCULACION]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[VW_REPORTE_AUSENCIA_POR_TIPO_VINCULACION]
AS
select '''''''' as Contingencia, case a.Tipo_Vinculacion when 'NULL' then 'No Especificada' when null then 'No Especificada' else a.Tipo_Vinculacion end as Evento, 
									  DATEPART(MONTH,a.fechainicio) as Mes,CASE WHEN MONTH(a.FechaInicio) = 1 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Enero, CASE WHEN MONTH(a.FechaInicio) = 2 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) 
                         = 3 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Marzo, CASE WHEN MONTH(a.FechaInicio) = 4 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(a.FechaInicio) = 5 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) 
                         = 6 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) = 7 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(a.FechaInicio) = 8 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Agosto, CASE WHEN MONTH(a.FechaInicio) 
                         = 9 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(a.FechaInicio) = 12 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Diciembre, cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total ,DATEPART(YEAR,fechainicio) as anio,c.FK_Tipo_Contingencia,a.FK_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,a.NitEmpresa
									  from Tbl_Ausencias a 
									  inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia

where a.FK_Id_Ausencias_Padre = 0 group by a.Tipo_Vinculacion, DATEPART(MONTH,a.fechainicio),a.NitEmpresa,DATEPART(YEAR,fechainicio),c.FK_Tipo_Contingencia,a.FK_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria






GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENCIAS_GRUPOS_ETARIOS]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









alter VIEW [dbo].[VW_REPORTE_AUSENCIAS_GRUPOS_ETARIOS]
AS


select '''''''' as Contingencia, '18 a 25' as Evento, 
                        CASE WHEN MONTH(a.FechaInicio) = 1 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Enero,
						CASE WHEN MONTH(a.FechaInicio) = 2 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Febrero,
						CASE WHEN MONTH(a.FechaInicio) = 3 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Marzo, 
                        CASE WHEN MONTH(a.FechaInicio) = 4 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Abril, 
                        CASE WHEN MONTH(a.FechaInicio) = 5 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Mayo, 
                        CASE WHEN MONTH(a.FechaInicio) = 6 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Junio, 
                        CASE WHEN MONTH(a.FechaInicio) = 7 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Julio, 
                        CASE WHEN MONTH(a.FechaInicio) = 8 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Agosto, 
                        CASE WHEN MONTH(a.FechaInicio) = 9 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Septiembre, 
                        CASE WHEN MONTH(a.FechaInicio) = 10 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Octubre, 
                        CASE WHEN MONTH(a.FechaInicio) = 11 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Noviembre, 
                        CASE WHEN MONTH(a.FechaInicio) = 12 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Diciembre,  

cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total,
a.NitEmpresa, 
c.FK_Tipo_Contingencia,
a.Fk_Id_Sede,
a.FK_Id_Departamento,
a.FK_Id_EmpresaUsuaria,
DATEPART(YEAR,a.FechaInicio) as anio from Tbl_Ausencias a								
									inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
									where  a.Edad between 18 and 25 and (a.FK_Id_Ausencias_Padre = 0) 
									group by a.NitEmpresa, 
									a.Edad,
									c.FK_Tipo_Contingencia,
									a.FK_Id_Sede,
									a.FK_Id_Departamento,
									a.FK_Id_EmpresaUsuaria,
									DATEPART(YEAR,a.FechaInicio), 
									DATEPART(MONTH, a.fechainicio),
									a.Fecha_Fin

								        
								     UNION select '''''''' as Contingencia, '26 a 35' as Evento, 
									CASE WHEN MONTH(a.FechaInicio) = 1 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Enero,
									CASE WHEN MONTH(a.FechaInicio) = 2 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Febrero,
									CASE WHEN MONTH(a.FechaInicio) = 3 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Marzo, 
									CASE WHEN MONTH(a.FechaInicio) = 4 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Abril, 
									CASE WHEN MONTH(a.FechaInicio) = 5 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Mayo, 
								    CASE WHEN MONTH(a.FechaInicio) = 6 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Junio, 
									CASE WHEN MONTH(a.FechaInicio) = 7 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Julio, 
									CASE WHEN MONTH(a.FechaInicio) = 8 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Agosto, 
									CASE WHEN MONTH(a.FechaInicio) = 9 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Septiembre, 
									CASE WHEN MONTH(a.FechaInicio) = 10 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Octubre, 
									CASE WHEN MONTH(a.FechaInicio) = 11 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Noviembre, 
									CASE WHEN MONTH(a.FechaInicio) = 12 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Diciembre,  
									 cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total,a.NitEmpresa, c.FK_Tipo_Contingencia,a.Fk_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio) as anio from Tbl_Ausencias a
									 inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
									 where (a.Edad between 26 and 35) and (a.FK_Id_Ausencias_Padre = 0) 
									 group by a.NitEmpresa, a.Edad,c.FK_Tipo_Contingencia,a.FK_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio), DATEPART(MONTH, a.FechaInicio),a.Fecha_Fin
										
								      

								     UNION select '''''''' as Contingencia, '36 a 45' as Evento, 
									 CASE WHEN MONTH(a.FechaInicio) = 1 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Enero,
									 CASE WHEN MONTH(a.FechaInicio) = 2 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Febrero,
									 CASE WHEN MONTH(a.FechaInicio) = 3 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Marzo, 
                                     CASE WHEN MONTH(a.FechaInicio) = 4 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Abril, 
									 CASE WHEN MONTH(a.FechaInicio) = 5 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Mayo, 
									 CASE WHEN MONTH(a.FechaInicio) = 6 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Junio, 
									 CASE WHEN MONTH(a.FechaInicio) = 7 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Julio, 
									 CASE WHEN MONTH(a.FechaInicio) = 8 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Agosto, 
									 CASE WHEN MONTH(a.FechaInicio) = 9 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Septiembre, 
									 CASE WHEN MONTH(a.FechaInicio) = 10 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Octubre, 
									 CASE WHEN MONTH(a.FechaInicio) = 11 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Noviembre, 
									 CASE WHEN MONTH(a.FechaInicio) = 12 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Diciembre,  
								     cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total,a.NitEmpresa, c.FK_Tipo_Contingencia,a.Fk_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio) as anio from Tbl_Ausencias a
									 inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
									 where a.Edad between 36 and 45 and (a.FK_Id_Ausencias_Padre = 0) 
									 group by a.NitEmpresa, a.Edad,c.FK_Tipo_Contingencia,a.FK_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio), DATEPART(MONTH, a.FechaInicio),a.Fecha_Fin
								      

									 UNION select '''''''' as Contingencia, '46 a 55' as Evento, 
									 CASE WHEN MONTH(a.FechaInicio) = 1 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Enero,
									 CASE WHEN MONTH(a.FechaInicio) = 2 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Febrero,
									 CASE WHEN MONTH(a.FechaInicio) = 3 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Marzo, 
                                     CASE WHEN MONTH(a.FechaInicio) = 4 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Abril, 
                                     CASE WHEN MONTH(a.FechaInicio) = 5 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Mayo, 
									 CASE WHEN MONTH(a.FechaInicio) = 6 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Junio, 
									 CASE WHEN MONTH(a.FechaInicio) = 7 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Julio, 
									 CASE WHEN MONTH(a.FechaInicio) = 8 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Agosto, 
									 CASE WHEN MONTH(a.FechaInicio) = 9 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Septiembre, 
									 CASE WHEN MONTH(a.FechaInicio) = 10 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Octubre, 
									 CASE WHEN MONTH(a.FechaInicio) = 11 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Noviembre, 
									 CASE WHEN MONTH(a.FechaInicio) = 12 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Diciembre,  
									 cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total,a.NitEmpresa, c.FK_Tipo_Contingencia,a.Fk_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio) as anio from Tbl_Ausencias a
									 inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
								     where a.Edad between 46 and 55 and (a.FK_Id_Ausencias_Padre = 0) 
									 group by a.NitEmpresa, a.Edad,c.FK_Tipo_Contingencia,a.FK_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio), DATEPART(MONTH, a.FechaInicio),a.Fecha_Fin
								       
										
								     UNION select '''''''' as Contingencia, 'Mayor a 55' as Evento, 
									 CASE WHEN MONTH(a.FechaInicio) = 1 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Enero,
									 CASE WHEN MONTH(a.FechaInicio) = 2 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Febrero,
									 CASE WHEN MONTH(a.FechaInicio) = 3 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Marzo, 
                                     CASE WHEN MONTH(a.FechaInicio) = 4 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Abril, 
                                     CASE WHEN MONTH(a.FechaInicio) = 5 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Mayo, 
									 CASE WHEN MONTH(a.FechaInicio) = 6 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Junio, 
									 CASE WHEN MONTH(a.FechaInicio) = 7 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Julio, 
									 CASE WHEN MONTH(a.FechaInicio) = 8 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Agosto, 
									 CASE WHEN MONTH(a.FechaInicio) = 9 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Septiembre, 
									 CASE WHEN MONTH(a.FechaInicio) = 10 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Octubre, 
									 CASE WHEN MONTH(a.FechaInicio) = 11 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Noviembre, 
									 CASE WHEN MONTH(a.FechaInicio) = 12 THEN cast(count(a.Pk_Id_Ausencias) as decimal(19,2)) ELSE '0' END AS Diciembre,  
									 cast(count(a.Pk_Id_Ausencias) as decimal(19, 2)) as Total,a.NitEmpresa, c.FK_Tipo_Contingencia,a.Fk_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio) as anio from Tbl_Ausencias a
									 inner join Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
									 where a.Edad > 55 and (a.FK_Id_Ausencias_Padre = 0)   
									 group by a.NitEmpresa,  a.Edad,c.FK_Tipo_Contingencia,a.FK_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria,DATEPART(YEAR,a.FechaInicio), DATEPART(MONTH, a.fechainicio),a.Fecha_Fin








GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_CANTIDAD_ENFERMEDADES]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_CANTIDAD_ENFERMEDADES]
AS
select '''''''' Contingencia, d.Capitulo as Evento, 
									  DATEPART(MONTH,a.fechainicio) as Mes,CASE WHEN MONTH(a.FechaInicio) = 1 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Enero, CASE WHEN MONTH(a.FechaInicio) = 2 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) 
                         = 3 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Marzo, CASE WHEN MONTH(a.FechaInicio) = 4 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(a.FechaInicio) = 5 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) 
                         = 6 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) = 7 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(a.FechaInicio) = 8 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Agosto, CASE WHEN MONTH(a.FechaInicio) 
                         = 9 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(a.FechaInicio) = 12 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Diciembre,  cast(count(d.PK_Id_Diagnostico) AS DECIMAL(19,2)) as Total, a.NitEmpresa,DATEPART(YEAR,fechainicio) as anio,c.FK_Tipo_Contingencia,a.Fk_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria
									  from Tbl_Ausencias a 
									  JOIN dbo.Tbl_Contingencias c on a.FK_Id_Contingencia = c.PK_Id_Contingencia
									  INNER JOIN dbo.Tbl_Diagnosticos d ON D.PK_Id_Diagnostico = A.FK_Id_Diagnostico
									  where a.FK_Id_Ausencias_Padre = 0 group by d.PK_Id_Diagnostico, d.Capitulo, DATEPART(MONTH,a.fechainicio),a.NitEmpresa,DATEPART(YEAR,fechainicio),c.FK_Tipo_Contingencia,a.Fk_Id_Sede,a.FK_Id_Departamento,a.FK_Id_EmpresaUsuaria

							







GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_CONTINGENCIA]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_CONTINGENCIA]
AS
SELECT        c.PK_Id_Contingencia AS idContingencia, c.Detalle AS Descripcion, a.FechaInicio, a.Fecha_Fin AS FechaFin, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 1 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 1 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0))))
ELSE '0' END AS Enero, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 2 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 2 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0))))
ELSE '0' END AS Febrero, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 3 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 3 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Marzo, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 4 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 4 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Abril, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 5 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 5 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Mayo, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 6 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 6 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Junio, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 7 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 7 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Julio, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 8 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 8 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Agosto, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 9 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 9 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Septiembre, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 10 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 10 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Octubre, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 11 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 11 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
ELSE '0' END AS Noviembre, 
CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 12 THEN 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = 12 and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0))))
ELSE '0' END AS Diciembre, 
IIF(c.PK_Id_Contingencia not in (5,7),COUNT(*),
IIF((DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103)) IN (select CONVERT(DATE, Fecha, 103) from Tbl_DiaFestivo where Mes = MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) and anio = YEAR(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))))), 0,
	IIF((ISNULL(d.FK_Id_Dias_Laborables,1)=1),
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1,7)),COUNT(*),0), 
	IIF((DATEPART(WEEKDAY,DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) NOT IN (1)),COUNT(*),0)))) 
 AS Total, 
    a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, a.FK_Id_EmpresaUsuaria, DATEPART(YEAR, a.FechaInicio) AS anio, a.DiasAusencia, a.Costo, d.FK_Id_Dias_Laborables
FROM            master.dbo.spt_values INNER JOIN
                dbo.Tbl_Ausencias AS a INNER JOIN
                dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia ON DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio) <= a.Fecha_Fin AND
                YEAR(DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio)) = YEAR(a.FechaInicio)
				left join Tbl_Dias_Laborables_Empresa AS d ON a.NitEmpresa = d.Documento_empresa
WHERE        (master.dbo.spt_values.type = 'P')
GROUP BY c.PK_Id_Contingencia, c.Detalle, a.FechaInicio, a.Fecha_Fin, a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria, DATEPART(YEAR, a.FechaInicio), a.DiasAusencia, a.Costo, DATEPART(MONTH, DATEADD(DAY, master.dbo.spt_values.number, 
                         CONVERT(DATE, a.FechaInicio, 103))), d.FK_Id_Dias_Laborables, number


GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_COSTO_CONTINGENCIA]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_COSTO_CONTINGENCIA]
AS
SELECT        c.Detalle AS Contingencia, '''''''' AS Evento, CASE WHEN MONTH(a.FechaInicio) = 1 THEN SUM(a.Costo) ELSE '0' END AS Enero, CASE WHEN MONTH(a.FechaInicio) 
                         = 2 THEN SUM(a.Costo) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) = 3 THEN SUM(a.Costo) ELSE '0' END AS Marzo, 
                         CASE WHEN MONTH(a.FechaInicio) = 4 THEN SUM(a.Costo) ELSE '0' END AS Abril, CASE WHEN MONTH(a.FechaInicio) = 5 THEN SUM(a.Costo) 
                         ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) = 6 THEN SUM(a.Costo) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) 
                         = 7 THEN SUM(a.Costo) ELSE '0' END AS Julio, CASE WHEN MONTH(a.FechaInicio) = 8 THEN SUM(a.Costo) ELSE '0' END AS Agosto, 
                         CASE WHEN MONTH(a.FechaInicio) = 9 THEN SUM(a.Costo) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN SUM(a.Costo) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN SUM(a.Costo) ELSE '0' END AS Noviembre, CASE WHEN MONTH(a.FechaInicio) 
                         = 12 THEN SUM(a.Costo) ELSE '0' END AS Diciembre, DATEPART(YEAR, a.FechaInicio) AS anio, a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, 
                         c.FK_Tipo_Contingencia, a.FK_Id_EmpresaUsuaria, SUM(a.Costo) AS Total
FROM            dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia
WHERE        (a.FK_Id_Ausencias_Padre = 0)
GROUP BY c.Detalle, DATEPART(MONTH, a.FechaInicio), DATEPART(YEAR, a.FechaInicio), a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria



GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_DEPARTAMENTO]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_DEPARTAMENTO]
AS
SELECT        c.PK_Id_Contingencia AS idContigencia, d.Nombre_Departamento AS Descripcion, a.FechaInicio, a.Fecha_Fin AS FechaFin, CASE WHEN MONTH(DATEADD(DAY, 
                         number, CONVERT(DATE, a.FechaInicio, 103))) = 1 THEN COUNT(*) ELSE '0' END AS Enero, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, 
                         a.FechaInicio, 103))) = 2 THEN COUNT(*) ELSE '0' END AS Febrero, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) 
                         = 3 THEN COUNT(*) ELSE '0' END AS Marzo, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 4 THEN COUNT(*) 
                         ELSE '0' END AS Abril, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 5 THEN COUNT(*) ELSE '0' END AS Mayo, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 6 THEN COUNT(*) ELSE '0' END AS Junio, CASE WHEN MONTH(DATEADD(DAY, 
                         number, CONVERT(DATE, a.FechaInicio, 103))) = 7 THEN COUNT(*) ELSE '0' END AS Julio, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, 
                         a.FechaInicio, 103))) = 8 THEN COUNT(*) ELSE '0' END AS Agosto, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) 
                         = 9 THEN COUNT(*) ELSE '0' END AS Septiembre, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 10 THEN COUNT(*) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 11 THEN COUNT(*) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 12 THEN COUNT(*) ELSE '0' END AS Diciembre, COUNT(*) AS Total, 
                         DATEPART(YEAR, a.FechaInicio) AS anio, c.FK_Tipo_Contingencia, a.FK_Id_Sede, a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria, a.NitEmpresa, 
                         a.DiasAusencia
FROM            master.dbo.spt_values INNER JOIN
                         dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Departamento AS d ON a.FK_Id_Departamento = d.Pk_Id_Departamento ON DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio) 
                         <= a.Fecha_Fin AND YEAR(DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio)) = YEAR(a.FechaInicio)
WHERE        (master.dbo.spt_values.type = 'P')
GROUP BY c.PK_Id_Contingencia, d.Nombre_Departamento, a.FechaInicio, a.Fecha_Fin, a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria, DATEPART(YEAR, a.FechaInicio), a.DiasAusencia, DATEPART(MONTH, DATEADD(DAY, master.dbo.spt_values.number, CONVERT(DATE, 
                         a.FechaInicio, 103)))



GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_ENFERMEDADES]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_ENFERMEDADES]
AS
SELECT        c.PK_Id_Contingencia AS idContigencia, d.Capitulo AS Descripcion, a.FechaInicio, a.Fecha_Fin AS FechaFin, a.NitEmpresa, CASE WHEN MONTH(DATEADD(DAY, 
                         number, CONVERT(DATE, a.FechaInicio, 103))) = 1 THEN COUNT(*) ELSE '0' END AS Enero, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, 
                         a.FechaInicio, 103))) = 2 THEN COUNT(*) ELSE '0' END AS Febrero, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) 
                         = 3 THEN COUNT(*) ELSE '0' END AS Marzo, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 4 THEN COUNT(*) 
                         ELSE '0' END AS Abril, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 5 THEN COUNT(*) ELSE '0' END AS Mayo, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 6 THEN COUNT(*) ELSE '0' END AS Junio, CASE WHEN MONTH(DATEADD(DAY, 
                         number, CONVERT(DATE, a.FechaInicio, 103))) = 7 THEN COUNT(*) ELSE '0' END AS Julio, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, 
                         a.FechaInicio, 103))) = 8 THEN COUNT(*) ELSE '0' END AS Agosto, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) 
                         = 9 THEN COUNT(*) ELSE '0' END AS Septiembre, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 10 THEN COUNT(*) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 11 THEN COUNT(*) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 12 THEN COUNT(*) ELSE '0' END AS Diciembre, COUNT(*) AS Total, 
                         DATEPART(YEAR, a.FechaInicio) AS anio, c.FK_Tipo_Contingencia, a.FK_Id_Sede, a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria, a.DiasAusencia
FROM            master.dbo.spt_values INNER JOIN
                         dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Diagnosticos AS d ON d.PK_Id_Diagnostico = a.FK_Id_Diagnostico ON DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio) <= a.Fecha_Fin AND 
                         YEAR(DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio)) = YEAR(a.FechaInicio)
WHERE        (master.dbo.spt_values.type = 'P')
GROUP BY c.PK_Id_Contingencia, d.Capitulo, a.FechaInicio, a.Fecha_Fin, a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria, DATEPART(YEAR, a.FechaInicio), a.DiasAusencia, DATEPART(MONTH, DATEADD(DAY, master.dbo.spt_values.number, CONVERT(DATE, 
                         a.FechaInicio, 103)))



GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_EPS]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_EPS]
AS
SELECT        '''''''' AS Contingencia, ISNULL(a.Eps, 'No especificada') AS Evento, DATEPART(MONTH, a.FechaInicio) AS Mes,CASE WHEN MONTH(a.FechaInicio) = 1 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Enero, CASE WHEN MONTH(a.FechaInicio) = 2 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) 
                         = 3 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Marzo, CASE WHEN MONTH(a.FechaInicio) = 4 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(a.FechaInicio) = 5 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) 
                         = 6 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) = 7 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(a.FechaInicio) = 8 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Agosto, CASE WHEN MONTH(a.FechaInicio) 
                         = 9 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(a.FechaInicio) = 12 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Diciembre,  CAST(COUNT(a.Pk_Id_Ausencias) AS decimal(19, 2)) AS Total, a.NitEmpresa, DATEPART(YEAR, a.FechaInicio) 
                         AS anio, c.FK_Tipo_Contingencia, a.FK_Id_Sede, a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria
FROM            dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia
WHERE        (a.FK_Id_Ausencias_Padre = 0)
GROUP BY a.Eps, DATEPART(MONTH, a.FechaInicio), a.NitEmpresa, DATEPART(YEAR, a.FechaInicio), c.FK_Tipo_Contingencia, a.FK_Id_Sede, a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria






GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_NUMERO_EVENTOS_CONTINGENCIA]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_NUMERO_EVENTOS_CONTINGENCIA]
AS
SELECT        c.Detalle AS Contingencia, '''''''' AS Evento, CASE WHEN MONTH(a.FechaInicio) = 1 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Enero, 
                         CASE WHEN MONTH(a.FechaInicio) = 2 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) 
                         = 3 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Marzo, CASE WHEN MONTH(a.FechaInicio) = 4 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(a.FechaInicio) = 5 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) 
                         = 6 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) = 7 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(a.FechaInicio) = 8 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Agosto, CASE WHEN MONTH(a.FechaInicio) 
                         = 9 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(a.FechaInicio) = 12 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Diciembre, DATEPART(YEAR, a.FechaInicio) AS anio, a.NitEmpresa, 
                         a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, a.FK_Id_EmpresaUsuaria, COUNT(d.PK_Id_Diagnostico) AS Total
FROM            dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Diagnosticos AS d ON d.PK_Id_Diagnostico = a.FK_Id_Diagnostico
WHERE        (a.FK_Id_Ausencias_Padre = 0)
GROUP BY c.Detalle, DATEPART(MONTH, a.FechaInicio), DATEPART(YEAR, a.FechaInicio), a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria



GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_OCUPACION]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_OCUPACION]
AS
SELECT        '''''''' AS Contingencia, ISNULL(o.Descripcion_Ocupacion, 'No especificada') AS Evento, DATEPART(YEAR, a.FechaInicio) AS anio, c.FK_Tipo_Contingencia, 
                         a.FK_Id_Sede, a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria, a.NitEmpresa, CASE WHEN MONTH(a.FechaInicio) = 1 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Enero, CASE WHEN MONTH(a.FechaInicio) = 2 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) 
                         = 3 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Marzo, CASE WHEN MONTH(a.FechaInicio) = 4 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(a.FechaInicio) = 5 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) 
                         = 6 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) = 7 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(a.FechaInicio) = 8 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Agosto, CASE WHEN MONTH(a.FechaInicio) 
                         = 9 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(a.FechaInicio) = 12 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Diciembre, COUNT(a.Pk_Id_Ausencias) AS Total
FROM            dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Ocupacion AS o ON a.FK_Id_Ocupacion = o.PK_Ocupacion
WHERE        (a.FK_Id_Ausencias_Padre = 0)
GROUP BY o.Descripcion_Ocupacion, DATEPART(MONTH, a.FechaInicio), DATEPART(YEAR, a.FechaInicio), c.FK_Tipo_Contingencia, a.FK_Id_Sede, a.FK_Id_Departamento, 
                         a.FK_Id_EmpresaUsuaria, a.NitEmpresa



GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_SEDES]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_SEDES]
AS
SELECT        c.PK_Id_Contingencia AS idContigencia, sd.Nombre_Sede AS Descripcion, a.FechaInicio, a.Fecha_Fin AS FechaFin, CASE WHEN MONTH(DATEADD(DAY, number, 
                         CONVERT(DATE, a.FechaInicio, 103))) = 1 THEN COUNT(*) ELSE '0' END AS Enero, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 
                         103))) = 2 THEN COUNT(*) ELSE '0' END AS Febrero, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 3 THEN COUNT(*) 
                         ELSE '0' END AS Marzo, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 4 THEN COUNT(*) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 5 THEN COUNT(*) ELSE '0' END AS Mayo, CASE WHEN MONTH(DATEADD(DAY, 
                         number, CONVERT(DATE, a.FechaInicio, 103))) = 6 THEN COUNT(*) ELSE '0' END AS Junio, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, 
                         a.FechaInicio, 103))) = 7 THEN COUNT(*) ELSE '0' END AS Julio, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) 
                         = 8 THEN COUNT(*) ELSE '0' END AS Agosto, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 9 THEN COUNT(*) 
                         ELSE '0' END AS Septiembre, CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 10 THEN COUNT(*) ELSE '0' END AS Octubre, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 11 THEN COUNT(*) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(DATEADD(DAY, number, CONVERT(DATE, a.FechaInicio, 103))) = 12 THEN COUNT(*) ELSE '0' END AS Diciembre, COUNT(*) AS Total, 
                         DATEPART(YEAR, a.FechaInicio) AS anio, c.FK_Tipo_Contingencia, a.FK_Id_Sede, a.FK_Id_Departamento, a.FK_Id_EmpresaUsuaria, a.NitEmpresa, 
                         a.DiasAusencia
FROM            master.dbo.spt_values INNER JOIN
                         dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia INNER JOIN
                         dbo.Tbl_Sede AS sd ON a.FK_Id_Sede = sd.Pk_Id_Sede ON DATEADD(DAY, master.dbo.spt_values.number, a.FechaInicio) <= a.Fecha_Fin AND YEAR(DATEADD(DAY, 
                         master.dbo.spt_values.number, a.FechaInicio)) = YEAR(a.FechaInicio)
WHERE        (master.dbo.spt_values.type = 'P')
GROUP BY c.PK_Id_Contingencia, sd.Nombre_Sede, a.FechaInicio, a.Fecha_Fin, a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria, DATEPART(YEAR, a.FechaInicio), a.DiasAusencia, DATEPART(MONTH, DATEADD(DAY, master.dbo.spt_values.number, CONVERT(DATE, 
                         a.FechaInicio, 103)))



GO
/****** Object:  View [dbo].[VW_REPORTE_AUSENTISMO_SEXO]    Script Date: 29/01/2018 3:57:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[VW_REPORTE_AUSENTISMO_SEXO]
AS
SELECT        '''''''' AS Contingencia, ISNULL(a.Sexo, 'No especificado') AS Evento, CASE WHEN MONTH(a.FechaInicio) = 1 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Enero, CASE WHEN MONTH(a.FechaInicio) = 2 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Febrero, CASE WHEN MONTH(a.FechaInicio) 
                         = 3 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Marzo, CASE WHEN MONTH(a.FechaInicio) = 4 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Abril, 
                         CASE WHEN MONTH(a.FechaInicio) = 5 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Mayo, CASE WHEN MONTH(a.FechaInicio) 
                         = 6 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Junio, CASE WHEN MONTH(a.FechaInicio) = 7 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Julio, 
                         CASE WHEN MONTH(a.FechaInicio) = 8 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Agosto, CASE WHEN MONTH(a.FechaInicio) 
                         = 9 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Septiembre, CASE WHEN MONTH(a.FechaInicio) = 10 THEN COUNT(a.Pk_Id_Ausencias) 
                         ELSE '0' END AS Octubre, CASE WHEN MONTH(a.FechaInicio) = 11 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Noviembre, 
                         CASE WHEN MONTH(a.FechaInicio) = 12 THEN COUNT(a.Pk_Id_Ausencias) ELSE '0' END AS Diciembre, DATEPART(YEAR, a.FechaInicio) AS anio, a.NitEmpresa, 
                         a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, a.FK_Id_EmpresaUsuaria, COUNT(a.Pk_Id_Ausencias) AS Total
FROM            dbo.Tbl_Ausencias AS a INNER JOIN
                         dbo.Tbl_Contingencias AS c ON a.FK_Id_Contingencia = c.PK_Id_Contingencia
WHERE        (a.FK_Id_Ausencias_Padre = 0)
GROUP BY a.Sexo, DATEPART(MONTH, a.FechaInicio), DATEPART(YEAR, a.FechaInicio), a.NitEmpresa, a.FK_Id_Sede, a.FK_Id_Departamento, c.FK_Tipo_Contingencia, 
                         a.FK_Id_EmpresaUsuaria
