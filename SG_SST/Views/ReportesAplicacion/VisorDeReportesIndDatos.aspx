<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorDeReportesIndDatos.aspx.cs" Inherits="SG_SST.Views.ReportesAplicacion.VisorDeReportesIndDatos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

 <head runat="server">
  <script runat="server">

        void Page_Load(object sender, EventArgs e)
        {
           MostrarReporteIncDatos();
        }
        protected void MostrarReporteIncDatos()
        {
            if (!IsPostBack)
            {

                switch (SG_SST.Reportes.RecursoParametros.Reporte)
                {
                    case "EstandaresMinimosDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndEvalEstandaresMinimosDataTable dtv1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndEvalEstandaresMinimosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndEvalEstandaresMinimosTableAdapter dav1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndEvalEstandaresMinimosTableAdapter();

                     
                        dav1.FillByFiltro(dtv1, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD1 = new ReportDataSource();
                        RD1.Value = dtv1;
                        RD1.Name = "DataSetEvaluaciones";


                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD1);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);

                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        
                        break;


                    case "AccionCorrectivaPreventivaDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndAccionesCorrPrevMejActividadesDataTable dtv2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndAccionesCorrPrevMejActividadesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndAccionesCorrPrevMejActividadesTableAdapter dav2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndAccionesCorrPrevMejActividadesTableAdapter();


                        dav2.FillByFiltro(dtv2,  SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD = new ReportDataSource();
                        RD.Value = dtv2;
                        RD.Name = "DataSetTotal";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD);
          
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "CondicionInseguraDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndActosCondicionesInsegurasDataTable dtv3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndActosCondicionesInsegurasDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndActosCondicionesInsegurasTableAdapter dav3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndActosCondicionesInsegurasTableAdapter();


                        dav3.FillByFiltro(dtv3, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.Sede,SG_SST.Reportes.RecursoParametros.TipoReporte);
               
                        ReportDataSource RD3 = new ReportDataSource();
                        RD3.Value = dtv3;
                        RD3.Name = "DataSetTotal";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD3);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;


                    case "AccidentesDeTrabajoDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndAccidenteDeTrabajoDataTable dtv4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndAccidenteDeTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndAccidenteDeTrabajoTableAdapter dav4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndAccidenteDeTrabajoTableAdapter();


                        dav4.FillByFiltro(dtv4,  SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Sede);
                       
                        ReportDataSource RD4 = new ReportDataSource();
                        RD4.Value = dtv4;
                        RD4.Name = "DataSetTotalAT";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD4);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "FrecuenciaAusentismoDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndFrecuenciaAusentismoDataTable dtv5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndFrecuenciaAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndFrecuenciaAusentismoTableAdapter dav5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndFrecuenciaAusentismoTableAdapter();


                        dav5.FillByFiltro(dtv5,SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Sede);
                     
                        ReportDataSource RD5 = new ReportDataSource();
                        RD5.Value = dtv5;
                        RD5.Name = "DataSetTotalAus";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD5);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "CapacitacionEntrenamientoDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.Tbl_PlanCapacitacionDataTable dtv6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.Tbl_PlanCapacitacionDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.Tbl_PlanCapacitacionTableAdapter dav6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.Tbl_PlanCapacitacionTableAdapter();


                        dav6.FillByFiltro(dtv6,  SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD6 = new ReportDataSource();
                        RD6.Value = dtv6;
                        RD6.Name = "DataSetTotal";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD6);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "PlanTrabajoAnualDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndPlanTrabajoAnualDataTable dtv7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndPlanTrabajoAnualDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndPlanTrabajoAnualTableAdapter dav7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndPlanTrabajoAnualTableAdapter();


                        dav7.FillByFiltro(dtv7, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Anio);
                        
                        ReportDataSource RD7 = new ReportDataSource();
                        RD7.Value = dtv7;
                        RD7.Name = "DataSetTotal";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD7);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;


                    case "SeveridadAusentismoDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndSeveridadAusentismoDataTable dtv8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndSeveridadAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndSeveridadAusentismoTableAdapter dav8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndSeveridadAusentismoTableAdapter();


                        dav8.FillByFiltro(dtv8, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Sede);
                        
                        ReportDataSource RD8 = new ReportDataSource();
                        RD8.Value = dtv8;
                        RD8.Name = "DataSetTotalAus";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD8);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "TasaAccidentalidadDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndTasaAccidentalidadDataTable dtv9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndTasaAccidentalidadDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndTasaAccidentalidadTableAdapter dav9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndTasaAccidentalidadTableAdapter();


                        dav9.FillByFiltro(dtv9,  SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Sede);
                       
                        
                        ReportDataSource RD9 = new ReportDataSource();
                        RD9.Value = dtv9;
                        RD9.Name = "DataSetTotalAT";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD9);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "CumplimientoRequisitosLegalesDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndRequisitosLegalesDataTable dtv10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndRequisitosLegalesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndRequisitosLegalesTableAdapter dav10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndRequisitosLegalesTableAdapter();


                        dav10.FillByFiltro(dtv10, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);


                        ReportDataSource RD10 = new ReportDataSource();
                        RD10.Value = dtv10;
                        RD10.Name = "DataSetTotal";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD10);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "AccidenteDeTrabajoDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndSeveridadAusentismoDatosDataTable dtv11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndSeveridadAusentismoDatosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndSeveridadAusentismoDatosTableAdapter dav11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndSeveridadAusentismoDatosTableAdapter();


                        dav11.FillByFiltro(dtv11,  SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Sede);
                       
                        ReportDataSource RD11 = new ReportDataSource();
                        RD11.Value = dtv11;
                        RD11.Name = "DataSetTotalAus";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD11);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "LesionesIncapacitantesDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndFrecuenciaAccidentesTrabajoDatosDataTable dtv12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.V_IndFrecuenciaAccidentesTrabajoDatosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndFrecuenciaAccidentesTrabajoDatosTableAdapter dav12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.V_IndFrecuenciaAccidentesTrabajoDatosTableAdapter();


                        dav12.FillByFiltro(dtv12, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Sede);
                    
                        ReportDataSource RD12 = new ReportDataSource();
                        RD12.Value = dtv12;
                        RD12.Name = "DataSetTotal";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD12);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;


                    case "DxCondicionesSaludDatos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.IndDxSaludDatosDataTable dtv13 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndData.IndDxSaludDatosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.IndDxSaludDatosTableAdapter dav13 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndDataTableAdapters.IndDxSaludDatosTableAdapter();


                        dav13.FillByFiltro(dtv13, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede,SG_SST.Reportes.RecursoParametros.Proceso);
                        
                        ReportDataSource RD13 = new ReportDataSource();
                        RD13.Value = dtv13;
                        RD13.Name = "DataSetTotalDx";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD13);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/IndicadoresDatos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;


                        ReportViewer1.LocalReport.Refresh();

                        break;
                }
            }
        }

      </script>
</head>

<body>
    <form id="form1" runat="server">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Height="1200px" Width="100%" Enabled="true"
                BackColor="White" BorderColor="black" Font-Bold="true" InternalBorderStyle="None" ShowParameterPrompts="true"   
                AsyncRendering="false" ShowExportControls="true" ShowBackButton="False" ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" ShowZoomControl="False"  PageCountMode="Actual">

     </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
