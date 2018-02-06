<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorDeReportesAus.aspx.cs" Inherits="SG_SST.Views.ReportesAplicacion.VisorDeReportesAus" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
 <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
           MostrarReporte();
        }

        protected void MostrarReporte()
        {
            if (!IsPostBack)
            {
             
                switch (SG_SST.Reportes.RecursoParametros.Reporte)
                {
                    case "EVENTOENFERMEDADES":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false; 
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_CANTIDAD_ENFERMEDADESDataTable dtv = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_CANTIDAD_ENFERMEDADESDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_CANTIDAD_ENFERMEDADESTableAdapter dav = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_CANTIDAD_ENFERMEDADESTableAdapter();

                     //   dav.FillByFiltro(dtv, SG_SST.Reportes.RecursoParametros.NitEmpresa,2017,null,null,null,null);
                        dav.FillByFiltro(dtv, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
               
                        ReportDataSource RD = new ReportDataSource();
                        RD.Value = dtv;
                        RD.Name = "DataSet1";
                      
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;
                        
                        ReportViewer1.LocalReport.Refresh();
                      
                        break;

                    case "DIASENFERMEDADES":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_ENFERMEDADESDataTable dtv1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_ENFERMEDADESDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_ENFERMEDADESTableAdapter dav1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_ENFERMEDADESTableAdapter();

                      //  dav1.FillByFiltro(dtv1, SG_SST.Reportes.RecursoParametros.NitEmpresa, 2017,null,null,null,null);
                        dav1.FillByFiltro(dtv1, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
               
                        ReportDataSource RD1 = new ReportDataSource();
                        RD1.Value = dtv1;
                        RD1.Name = "DataSet1";
                        ReportViewer1.Reset();
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD1);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                  
                        ReportViewer1.LocalReport.Refresh();
               
                        break;

                    case "EPS":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_EPSDataTable dtv2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_EPSDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_EPSTableAdapter dav2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_EPSTableAdapter();

                      //  dav2.FillByFiltro(dtv2, SG_SST.Reportes.RecursoParametros.NitEmpresa, 2017, null, null, null, null);
                        dav2.FillByFiltro(dtv2, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
               
                        ReportDataSource RD2 = new ReportDataSource();
                        RD2.Value = dtv2;
                        RD2.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD2);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "SEXO":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_SEXODataTable dtv3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_SEXODataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_SEXOTableAdapter dav3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_SEXOTableAdapter();

                        //  dav2.FillByFiltro(dtv2, SG_SST.Reportes.RecursoParametros.NitEmpresa, 2017, null, null, null, null);
                        dav3.FillByFiltro(dtv3, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);
                  
                        ReportDataSource RD3 = new ReportDataSource();
                        RD3.Value = dtv3;
                        RD3.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD3);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "DIASCONTIGENCIA":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_CONTINGENCIADataTable dtv4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_CONTINGENCIADataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_CONTINGENCIATableAdapter dav4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_CONTINGENCIATableAdapter();

                        dav4.FillByFiltro(dtv4, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD4 = new ReportDataSource();
                        RD4.Value = dtv4;
                        RD4.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD4);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;


                    case "EVENTOSCONTIGENCIA":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_NUMERO_EVENTOS_CONTINGENCIADataTable dtv5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_NUMERO_EVENTOS_CONTINGENCIADataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_NUMERO_EVENTOS_CONTINGENCIATableAdapter dav5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_NUMERO_EVENTOS_CONTINGENCIATableAdapter();

                        dav5.FillByFiltro(dtv5, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD5 = new ReportDataSource();
                        RD5.Value = dtv5;
                        RD5.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD5);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "COSTOCONTIGENCIA":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_COSTO_CONTINGENCIADataTable dtv6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_COSTO_CONTINGENCIADataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_COSTO_CONTINGENCIATableAdapter dav6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_COSTO_CONTINGENCIATableAdapter();

                        dav6.FillByFiltro(dtv6, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD6 = new ReportDataSource();
                        RD6.Value = dtv6;
                        RD6.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD6);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "GRUPOETAREO":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENCIAS_GRUPOS_ETARIOSDataTable dtv7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENCIAS_GRUPOS_ETARIOSDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENCIAS_GRUPOS_ETARIOSTableAdapter dav7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENCIAS_GRUPOS_ETARIOSTableAdapter();

                        dav7.FillByFiltro(dtv7, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD7 = new ReportDataSource();
                        RD7.Value = dtv7;
                        RD7.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD7);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;



                    case "TIPOVINCULACION":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENCIA_POR_TIPO_VINCULACIONDataTable dtv8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENCIA_POR_TIPO_VINCULACIONDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENCIA_POR_TIPO_VINCULACIONTableAdapter dav8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENCIA_POR_TIPO_VINCULACIONTableAdapter();

                        dav8.FillByFiltro(dtv8, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD8 = new ReportDataSource();
                        RD8.Value = dtv8;
                        RD8.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD8);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "SEDE":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_SEDESDataTable dtv9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_SEDESDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_SEDESTableAdapter dav9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_SEDESTableAdapter();

                        dav9.FillByFiltro(dtv9, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD9 = new ReportDataSource();
                        RD9.Value = dtv9;
                        RD9.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD9);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "DEPARTAMENTOS":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_DEPARTAMENTODataTable dtv10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_DEPARTAMENTODataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_DEPARTAMENTOTableAdapter dav10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_DEPARTAMENTOTableAdapter();

                        dav10.FillByFiltro(dtv10, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD10 = new ReportDataSource();
                        RD10.Value = dtv10;
                        RD10.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD10);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "OCUPACION":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_OCUPACIONDataTable dtv11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.VW_REPORTE_AUSENTISMO_OCUPACIONDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_OCUPACIONTableAdapter dav11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.VW_REPORTE_AUSENTISMO_OCUPACIONTableAdapter();

                        dav11.FillByFiltro(dtv11, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                        ReportDataSource RD11 = new ReportDataSource();
                        RD11.Value = dtv11;
                        RD11.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD11);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "PROCESO":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.V_Ausencias_ProcesoDataTable dtv12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAuns.V_Ausencias_ProcesoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.V_Ausencias_ProcesoTableAdapter dav12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetAunsTableAdapters.V_Ausencias_ProcesoTableAdapter();

                        dav12.FillByFiltro(dtv12, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Departamento, SG_SST.Reportes.RecursoParametros.Origen, SG_SST.Reportes.RecursoParametros.EmpresaUsuaria);

                  
                        ReportDataSource RD12 = new ReportDataSource();
                        RD12.Value = dtv12;
                        RD12.Name = "DataSet1";
                       
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD12);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Ausentismo/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        
                        ReportViewer1.LocalReport.Refresh();

                        break;
                        
                }             
            }

        }
           
    </script>
    <title></title>
</head>
<body>
   <form  runat="server">
       <div>
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1200px" Width="100%" Enabled="true"
                BackColor="White" BorderColor="black" Font-Bold="true" InternalBorderStyle="None" ShowParameterPrompts="true"   
                AsyncRendering="false" ShowBackButton="False" ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" ShowZoomControl="False" >
            </rsweb:ReportViewer>    

        </div>
 </form>
</body>
</html>
