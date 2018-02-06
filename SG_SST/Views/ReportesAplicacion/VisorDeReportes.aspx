<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorDeReportes.aspx.cs" Inherits="SG_SST.ReportesAplicacion.VisorDeReportes" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script runat="server">



        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarReporte();
                string CtrlID = string.Empty;
                if (Request.QueryString!=null)
                {
                    string hjh = Request.QueryString.ToString();
                }
            }
        }

        protected void MostrarReporte()
        {
            if (!IsPostBack)
            {

                ReportViewer1.LocalReport.Refresh();

                switch (SG_SST.Reportes.RecursoParametros.Reporte)
                {
                    case "AUSENTISMO":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_Proceso_asDataTable dtv = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_Proceso_asDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_Proceso_asTableAdapter dav = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_Proceso_asTableAdapter();

                        dav.FillByFiltro(dtv, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD = new ReportDataSource();
                        RD.Value = dtv;
                        RD.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "PRESUPUESTO":
                        //prueba 
                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Presupuesta_Dataset1DataTable dtv1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Presupuesta_Dataset1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Presupuesta_Dataset1TableAdapter dav1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Presupuesta_Dataset1TableAdapter();

                        dav1.FillByDataSet1(dtv1,SG_SST.Reportes.RecursoParametros.SedeInd, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);


                        ReportDataSource RD1 = new ReportDataSource();
                        RD1.Value = dtv1;
                        RD1.Name = "DataSet1";



                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Tbl_Presupuesto_Por_AñoDataTable dtv30 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Tbl_Presupuesto_Por_AñoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Tbl_Presupuesto_Por_AñoTableAdapter dav30 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Tbl_Presupuesto_Por_AñoTableAdapter();

                        dav30.FillDataSet4(dtv30, SG_SST.Reportes.RecursoParametros.SedeInd,SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD30 = new ReportDataSource();
                        RD30.Value = dtv30;
                        RD30.Name = "DataSet4";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Presupuesto_Dataset3DataTable dtv31 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Presupuesto_Dataset3DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Presupuesto_Dataset3TableAdapter dav31 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Presupuesto_Dataset3TableAdapter();

                        dav31.FillDataset3(dtv31,SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD31 = new ReportDataSource();
                        RD31.Value = dtv31;
                        RD31.Name = "DataSet3";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD1);
                        ReportViewer1.LocalReport.DataSources.Add(RD30);
                        ReportViewer1.LocalReport.DataSources.Add(RD31);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);

                        //ReportParameter[] parameters1 = new ReportParameter[2];

                        //parameters1[0] = new ReportParameter("sede", SG_SST.Reportes.RecursoParametros.SedeTexto);
                        //parameters1[1] = new ReportParameter("periodo", SG_SST.Reportes.RecursoParametros.Anio.ToString());

                        //ReportViewer1.LocalReport.SetParameters(parameters1);
                        ReportViewer1.LocalReport.Refresh();


                        break;

                    case "COMPETENCIAS":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_competenciaDataTable dtv2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_competenciaDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_competenciaTableAdapter dav2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_competenciaTableAdapter();

                        dav2.FillByFiltro(dtv2, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD2 = new ReportDataSource();
                        RD2.Value = dtv2;
                        RD2.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD2);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();

                        break;
                    case "METODOLOGIAINSHT":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_INSHT_MATRIIZDataTable dtv3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_INSHT_MATRIIZDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_INSHT_MATRIIZTableAdapter dav3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_INSHT_MATRIIZTableAdapter();

                        dav3.FillByFiltro(dtv3, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD3 = new ReportDataSource();
                        RD3.Value = dtv3;
                        RD3.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD3);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;
                    case "PLANTRABAJO":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanTrabajoEmpresaDataTable dtv4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanTrabajoEmpresaDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanTrabajoEmpresaTableAdapter dav4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanTrabajoEmpresaTableAdapter();

                        dav4.FillByFiltro(dtv4, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD4 = new ReportDataSource();
                        RD4.Value = dtv4;
                        RD4.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD4);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;
                    case "DIAGNOSTICOSALUD":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.DxSaludDataSet1DataTable dtv5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.DxSaludDataSet1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.DxSaludDataSet1TableAdapter dav5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.DxSaludDataSet1TableAdapter();

                        dav5.FillByFiltro(dtv5, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD5 = new ReportDataSource();
                        RD5.Value = dtv5;
                        RD5.Name = "DataSet1";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.DxSaludDataSet2DataTable dtv32 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.DxSaludDataSet2DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.DxSaludDataSet2TableAdapter dav32 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.DxSaludDataSet2TableAdapter();

                        dav32.FillByFiltro(dtv32, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD32 = new ReportDataSource();
                        RD32.Value = dtv32;
                        RD32.Name = "DataSet2";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.DxSaludDataSet3DataTable dtv33 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.DxSaludDataSet3DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.DxSaludDataSet3TableAdapter dav33 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.DxSaludDataSet3TableAdapter();

                        dav33.FillByFiltro(dtv33, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD33 = new ReportDataSource();
                        RD33.Value = dtv33;
                        RD33.Name = "DataSet3";


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD5);
                        ReportViewer1.LocalReport.DataSources.Add(RD32);
                        ReportViewer1.LocalReport.DataSources.Add(RD33);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "AccionesCorrectivas":

                        //ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_ACCIONESCORRECTIVASPREVENTIVASDataTable dtv6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_ACCIONESCORRECTIVASPREVENTIVASDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_ACCIONESCORRECTIVASPREVENTIVASTableAdapter dav6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_ACCIONESCORRECTIVASPREVENTIVASTableAdapter();

                        dav6.FillByFiltro(dtv6, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD6 = new ReportDataSource();
                        RD6.Value = dtv6;
                        RD6.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD6);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;
                    case "GestionCambio":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_GestiondelcambioDataTable dtv7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_GestiondelcambioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_GestiondelcambioTableAdapter dav7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_GestiondelcambioTableAdapter();

                        dav7.FillByFiltro(dtv7, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD7 = new ReportDataSource();
                        RD7.Value = dtv7;
                        RD7.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD7);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "Incidentes":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_Reporte_IncidentesDataTable dtv8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_Reporte_IncidentesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_Reporte_IncidentesTableAdapter dav8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_Reporte_IncidentesTableAdapter();

                        dav8.FillByFiltro(dtv8, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD8 = new ReportDataSource();
                        RD8.Value = dtv8;
                        RD8.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD8);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "InspeccionesSeguridad":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_InspeccionesDataTable dtv9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_InspeccionesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_InspeccionesTableAdapter dav9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_InspeccionesTableAdapter();

                        dav9.FillByFiltro(dtv9, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD9 = new ReportDataSource();
                        RD9.Value = dtv9;
                        RD9.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD9);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "PerfilSocio":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PERFIL_SOCIODEMOGRAFICODataTable dtv10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PERFIL_SOCIODEMOGRAFICODataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PERFIL_SOCIODEMOGRAFICOTableAdapter dav10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PERFIL_SOCIODEMOGRAFICOTableAdapter();

                        dav10.FillByFiltro(dtv10, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD10 = new ReportDataSource();
                        RD10.Value = dtv10;
                        RD10.Name = "DataSet1";

                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.PageCountMode = PageCountMode.Actual;
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD10);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "IdentificacionPeligro":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_GTC45_MATRIZDataTable dtv11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_GTC45_MATRIZDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_GTC45_MATRIZTableAdapter dav11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_GTC45_MATRIZTableAdapter();

                        dav11.FillByFiltro(dtv11, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD11 = new ReportDataSource();
                        RD11.Value = dtv11;
                        RD11.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD11);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "MetodologiaRam":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_Matriz_RAMDataTable dtv12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_Matriz_RAMDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_Matriz_RAMTableAdapter dav12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_Matriz_RAMTableAdapter();

                        dav12.FillByFiltro(dtv12, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD12 = new ReportDataSource();
                        RD12.Value = dtv12;
                        RD12.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD12);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "PuestosTrabajo":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.VW_EstudioPuestosdeTrabajoDataTable dtv13 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.VW_EstudioPuestosdeTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.VW_EstudioPuestosdeTrabajoTableAdapter dav13 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.VW_EstudioPuestosdeTrabajoTableAdapter();

                        dav13.FillByFiltro(dtv13, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD13 = new ReportDataSource();
                        RD13.Value = dtv13;
                        RD13.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD13);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "PlanEmergenciaAccion":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanEmergenciaFrentesAccionDataTable dtv14 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanEmergenciaFrentesAccionDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanEmergenciaFrentesAccionTableAdapter dav14 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanEmergenciaFrentesAccionTableAdapter();

                        dav14.FillByFiltro(dtv14, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD14 = new ReportDataSource();
                        RD14.Value = dtv14;
                        RD14.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD14);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "PlanEmergenciaGeneral":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanEmergenciaInfoGeneralDataTable dtv15 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanEmergenciaInfoGeneralDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanEmergenciaInfoGeneralTableAdapter dav15 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanEmergenciaInfoGeneralTableAdapter();

                        dav15.FillByFiltro(dtv15, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD15 = new ReportDataSource();
                        RD15.Value = dtv15;
                        RD15.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD15);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "ActosCondicionesInseguras":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_reporteactosycondinsegurosDataTable dtv16 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_reporteactosycondinsegurosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_reporteactosycondinsegurosTableAdapter dav16 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_reporteactosycondinsegurosTableAdapter();

                        dav16.FillByFiltro(dtv16, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD16 = new ReportDataSource();
                        RD16.Value = dtv16;
                        RD16.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD16);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "AdquisicionesBienes":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_ADQUISCIONESDEBIENESOCONTRATACIÓNDataTable dtv17 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_ADQUISCIONESDEBIENESOCONTRATACIÓNDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_ADQUISCIONESDEBIENESOCONTRATACIÓNTableAdapter dav17 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_ADQUISCIONESDEBIENESOCONTRATACIÓNTableAdapter();

                        dav17.FillByFiltro(dtv17, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD17 = new ReportDataSource();
                        RD17.Value = dtv17;
                        RD17.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD17);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "RelacionesLaborales":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.VistaRelacionesLaboralesDataTable dtv18 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.VistaRelacionesLaboralesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.VistaRelacionesLaboralesTableAdapter dav18 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.VistaRelacionesLaboralesTableAdapter();

                        dav18.FillByFiltro(dtv18, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD18 = new ReportDataSource();
                        RD18.Value = dtv18;
                        RD18.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD18);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "PlanCapacitacion":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanCapacitacionDataTable dtv19 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_PlanCapacitacionDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanCapacitacionTableAdapter dav19 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_PlanCapacitacionTableAdapter();

                        dav19.FillByFiltro(dtv19, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD19 = new ReportDataSource();
                        RD19.Value = dtv19;
                        RD19.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD19);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;



                    case "MEDIDASPREV":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_MEDIDASPREVENCIÓNCONTROLDataTable dtv22 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_MEDIDASPREVENCIÓNCONTROLDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_MEDIDASPREVENCIÓNCONTROLTableAdapter dav22 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_MEDIDASPREVENCIÓNCONTROLTableAdapter();

                        dav22.FillByFiltro(dtv22, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD22 = new ReportDataSource();
                        RD22.Value = dtv22;
                        RD22.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD22);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "ENFERMEDADLAB":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_REPORTE_ENFERMEDADES_LABORALESDataTable dtv23 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_REPORTE_ENFERMEDADES_LABORALESDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_REPORTE_ENFERMEDADES_LABORALESTableAdapter dav23 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_REPORTE_ENFERMEDADES_LABORALESTableAdapter();

                        dav23.FillByFiltro(dtv23,SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD23 = new ReportDataSource();
                        RD23.Value = dtv23;
                        RD23.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD23);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "PLANESACCION":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_planesaccion_todoDataTable dtv24 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.V_planesaccion_todoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_planesaccion_todoTableAdapter dav24 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.V_planesaccion_todoTableAdapter();

                        dav24.FillByFiltro(dtv24, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD24 = new ReportDataSource();
                        RD24.Value = dtv24;
                        RD24.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD24);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;


                    case "IncidenteAT":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.VW_IncidentesELDataTable dtv25 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.VW_IncidentesELDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.VW_IncidentesELTableAdapter dav25 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.VW_IncidentesELTableAdapter();

                        dav25.FillByFiltro(dtv25, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD25 = new ReportDataSource();
                        RD25.Value = dtv25;
                        RD25.Name = "DataSet1";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD25);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "ActividadesComunicaciones":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_UnionTotalDataTable dtv26 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_UnionTotalDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_UnionTotalTableAdapter dav26 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_UnionTotalTableAdapter();

                        dav26.FillByUnionTotal(dtv26, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD26 = new ReportDataSource();
                        RD26.Value = dtv26;
                        RD26.Name = "uniontotal";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_ComunicaAPPDataTable dtv27 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_ComunicaAPPDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_ComunicaAPPTableAdapter dav27 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_ComunicaAPPTableAdapter();


                        dav27.FillByComunicaApp(dtv27, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD27 = new ReportDataSource();
                        RD27.Value = dtv27;
                        RD27.Name = "ComunicAPP";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_AdjuntosDataTable dtv28 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_AdjuntosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_AdjuntosTableAdapter dav28 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_AdjuntosTableAdapter();


                        dav28.FillByAdjuntos(dtv28, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Estado,SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD28 = new ReportDataSource();
                        RD28.Value = dtv28;
                        RD28.Name = "adjuntos";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_InternosDataTable dtv29 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_InternosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_InternosTableAdapter dav29 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_InternosTableAdapter();


                        dav29.FillByInternos(dtv29, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD29 = new ReportDataSource();
                        RD29.Value = dtv29;
                        RD29.Name = "internos";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesAñoComunicaDataTable dtv40 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesAñoComunicaDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesAñoComunicaTableAdapter dav40 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesAñoComunicaTableAdapter();


                        dav40.Fill(dtv40, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD40 = new ReportDataSource();
                        RD40.Value = dtv40;
                        RD40.Name = "añoscomuni";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_EstadoDataTable dtv41 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_EstadoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_EstadoTableAdapter dav41 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_EstadoTableAdapter();


                        dav41.FillByFiltro(dtv41);

                        ReportDataSource RD41 = new ReportDataSource();
                        RD41.Value = dtv41;
                        RD41.Name = "estado";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesEfectiComuniDataTable dtv42 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesEfectiComuniDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesEfectiComuniTableAdapter dav42 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesEfectiComuniTableAdapter();


                        dav42.FillByEfecticoComuni(dtv42, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD42 = new ReportDataSource();
                        RD42.Value = dtv42;
                        RD42.Name = "efecticomuni";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesNoEfectiDataTable dtv43 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesNoEfectiDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesNoEfectiTableAdapter dav43 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesNoEfectiTableAdapter();


                        dav43.FillByNoEfecti(dtv43, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD43 = new ReportDataSource();
                        RD43.Value = dtv43;
                        RD43.Name = "noefecti";



                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD26);
                        ReportViewer1.LocalReport.DataSources.Add(RD27);
                        ReportViewer1.LocalReport.DataSources.Add(RD28);
                        ReportViewer1.LocalReport.DataSources.Add(RD29);
                        ReportViewer1.LocalReport.DataSources.Add(RD40);
                        ReportViewer1.LocalReport.DataSources.Add(RD41);
                        ReportViewer1.LocalReport.DataSources.Add(RD42);
                        ReportViewer1.LocalReport.DataSources.Add(RD43);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parametersComunicaciones = new ReportParameter[1];
                        //Establecemos el valor de los parámetros


                        parametersComunicaciones[0] = new ReportParameter("estado", SG_SST.Reportes.RecursoParametros.Estado);




                        ReportViewer1.LocalReport.SetParameters(parametersComunicaciones);

                        ReportViewer1.LocalReport.Refresh();
                        break;
                }
            }

        }

    </script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1200px" Width="100%" Enabled="true" AutoPostBack="false"
                BackColor="White" BorderColor="black" Font-Bold="true" InternalBorderStyle="None" ShowParameterPrompts="false" InteractivityPostBackMode="AlwaysAsynchronous"  
                AsyncRendering="false" ShowBackButton="False" ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" ShowZoomControl="False" ViewStateMode="Enabled" ValidateRequestMode="Inherit" ExportContentDisposition="OnlyHtmlInline">
            </rsweb:ReportViewer>    
        </div>
    </form>
</body>
</html>
