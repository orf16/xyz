

CREATE VIEW [dbo].[V_ADQUISCIONESDEBIENESOCONTRATACIÓN]
AS
SELECT        dbo.Tbl_Empresa.Nit_Empresa, dbo.Tbl_Empresa.Razon_Social, dbo.Tbl_Sede.Nombre_Sede, dbo.Tbl_ProveedorContratista.Nombre_ProveedorContratista, 
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

