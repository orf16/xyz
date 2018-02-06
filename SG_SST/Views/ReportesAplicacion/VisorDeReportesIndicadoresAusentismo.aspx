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
                    case "IndicadorAusentismo":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.SP_AcumuladoTotalContingenciasReporteAusentismoDataTable dtv42 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.SP_AcumuladoTotalContingenciasReporteAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.SP_AcumuladoTotalContingenciasReporteAusentismoTableAdapter dav42 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.SP_AcumuladoTotalContingenciasReporteAusentismoTableAdapter();
                        dav42.FillByFiltro(dtv42, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
                        ReportDataSource RD42 = new ReportDataSource();
                        RD42.Value = dtv42;
                        RD42.Name = "DataSet2";
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_CONTINGENCIASDataTable dtv43 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_CONTINGENCIASDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_CONTINGENCIASTableAdapter dav43 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_CONTINGENCIASTableAdapter();
                        dav43.FillByFiltro(dtv43);
                        ReportDataSource RD43 = new ReportDataSource();
                        RD43.Value = dtv43;
                        RD43.Name = "Contingencias";
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD42);
                        ReportViewer1.LocalReport.DataSources.Add(RD43);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parametersIndAus = new ReportParameter[3];
                        //Establecemos el valor de los parámetros
                        parametersIndAus[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                        parametersIndAus[1] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);
                        parametersIndAus[2] = new ReportParameter("contingenciaTexto", SG_SST.Reportes.RecursoParametros.ContigenciaTexto);
                       
                        ReportViewer1.LocalReport.SetParameters(parametersIndAus);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "AusentismoComparacion":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.SP_AcumuladoTotalContingenciasReporteAusentismoDataTable dtv1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.SP_AcumuladoTotalContingenciasReporteAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.SP_AcumuladoTotalContingenciasReporteAusentismoTableAdapter dav1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.SP_AcumuladoTotalContingenciasReporteAusentismoTableAdapter();
                        dav1.FillByFiltro(dtv1, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
                        ReportDataSource RD1 = new ReportDataSource();
                        RD1.Value = dtv1;
                        RD1.Name = "DataSet2";
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_CONTINGENCIASDataTable dtv2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_CONTINGENCIASDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_CONTINGENCIASTableAdapter dav2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_CONTINGENCIASTableAdapter();
                        dav2.FillByFiltro(dtv2);
                        ReportDataSource RD2 = new ReportDataSource();
                        RD2.Value = dtv2;
                        RD2.Name = "Contingencias";
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.SP_AcumuladoTotalContingenciasReporteAusentismoDataTable dtv3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.SP_AcumuladoTotalContingenciasReporteAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.SP_AcumuladoTotalContingenciasReporteAusentismoTableAdapter dav3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.SP_AcumuladoTotalContingenciasReporteAusentismoTableAdapter();
                        dav3.FillByFiltro(dtv3, SG_SST.Reportes.RecursoParametros.AnioComparacion, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
                        ReportDataSource RD3 = new ReportDataSource();
                        RD3.Value = dtv3;
                        RD3.Name = "AnioDeComparacion";
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD1);
                        ReportViewer1.LocalReport.DataSources.Add(RD2);
                        ReportViewer1.LocalReport.DataSources.Add(RD3);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parametersIndAusComp = new ReportParameter[4];
                        //Establecemos el valor de los parámetros
                        parametersIndAusComp[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                        parametersIndAusComp[1] = new ReportParameter("anioComparar", SG_SST.Reportes.RecursoParametros.AnioComparacion.ToString());
                        parametersIndAusComp[2] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);
                        parametersIndAusComp[3] = new ReportParameter("contingenciaTexto", SG_SST.Reportes.RecursoParametros.ContigenciaTexto);
                       
                        ReportViewer1.LocalReport.SetParameters(parametersIndAusComp);
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
