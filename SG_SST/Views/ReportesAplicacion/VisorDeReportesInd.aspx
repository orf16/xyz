<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorDeReportesInd.aspx.cs" Inherits="SG_SST.Views.ReportesAplicacion.VisorDeReportesInd" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
           MostrarReporteInc();
        }
        protected void MostrarReporteInc()
        {
            if (!IsPostBack)
            {

                switch (SG_SST.Reportes.RecursoParametros.Reporte)
                {
                    case "AccionCorrectivaPreventiva":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;
                            
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejActividadesDataTable dtv1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejActividadesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejActividadesTableAdapter dav1 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejActividadesTableAdapter();

                       
                        dav1.FillByCerradas1(dtv1, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD = new ReportDataSource();
                        RD.Value = dtv1;
                        RD.Name = "DataSetCerradas1";
                        
                     
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejActividadesDataTable dtv2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejActividadesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejActividadesTableAdapter dav2 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejActividadesTableAdapter();

                        dav2.FillByCerradas2(dtv2, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                
                        ReportDataSource RD2 = new ReportDataSource();
                        RD2.Value = dtv2;
                        RD2.Name = "DataSetCerradas2";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejTotalDataTable dtv3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejTotalDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejTotalTableAdapter dav3 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejTotalTableAdapter();

                        dav3.FillByTotal1(dtv3, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);
                
                        
                        ReportDataSource RD3= new ReportDataSource();
                        RD3.Value = dtv3;
                        RD3.Name = "DataSetTotal1";

                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejTotalDataTable dtv4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndAccionesCorrPrevMejTotalDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejTotalTableAdapter dav4 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndAccionesCorrPrevMejTotalTableAdapter();



                        dav4.FillByTotal2(dtv4, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);
             
                        ReportDataSource RD4 = new ReportDataSource();
                        RD4.Value = dtv4;
                        RD4.Name = "DataSetTotal2";
                        
                        
                    

                       

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD);
                        ReportViewer1.LocalReport.DataSources.Add(RD2);
                        ReportViewer1.LocalReport.DataSources.Add(RD3);
                        ReportViewer1.LocalReport.DataSources.Add(RD4);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportViewer1.Visible = true;
                        
                        
                             ReportParameter[] parameters4 = new ReportParameter[1];
                        //Establecemos el valor de los parámetros

                      
                        parameters4[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());



                        ReportViewer1.LocalReport.SetParameters(parameters4);
                        
                        
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "CondicionInsegura":
                        
                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTable1DataTable dtv5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTable1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTable1TableAdapter dav5 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTable1TableAdapter();

                       
                        dav5.FillByCerradas1(dtv5,SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD5 = new ReportDataSource();
                        RD5.Value = dtv5;
                        RD5.Name = "DataSetCerradas1";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTable1DataTable dtv6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTable1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTable1TableAdapter dav6 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTable1TableAdapter();

                        dav6.FillByCerradas2(dtv6, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);
                
                        ReportDataSource RD6 = new ReportDataSource();
                        RD6.Value = dtv6;
                        RD6.Name = "DataSetCerradas2";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndConInsegurasTotalSemetreDataTable dtv7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndConInsegurasTotalSemetreDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndConInsegurasTotalSemetreTableAdapter dav7 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndConInsegurasTotalSemetreTableAdapter();

                        dav7.FillByTotal1(dtv7, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);
                
                        
                        ReportDataSource RD7= new ReportDataSource();
                        RD7.Value = dtv7;
                        RD7.Name = "DataSetTotal1";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndConInsegurasTotalSemetreDataTable dtv8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndConInsegurasTotalSemetreDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndConInsegurasTotalSemetreTableAdapter dav8 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndConInsegurasTotalSemetreTableAdapter();



                        dav8.FillByTotal2(dtv8, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);
             
                        ReportDataSource RD8 = new ReportDataSource();
                        RD8.Value = dtv8;
                        RD8.Name = "DataSetTotal2";
                      
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD5);
                        ReportViewer1.LocalReport.DataSources.Add(RD6);
                        ReportViewer1.LocalReport.DataSources.Add(RD7);
                        ReportViewer1.LocalReport.DataSources.Add(RD8);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                       
                        ReportParameter[] parameters5 = new ReportParameter[1];
                        //Establecemos el valor de los parámetros

                      
                        parameters5[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());



                        ReportViewer1.LocalReport.SetParameters(parameters5);
                        
                        
                     
                        
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();


                        break;


                    case "PlanTrabajoAnual":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTableTotalPlanAnualDataTable dtv9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTableTotalPlanAnualDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTableTotalPlanAnualTableAdapter dav9 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTableTotalPlanAnualTableAdapter();

                        //Sede
                        dav9.FillByTotal(dtv9, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.SedeInd, SG_SST.Reportes.RecursoParametros.Anio);
                      
                        
                        ReportDataSource RD9 = new ReportDataSource();
                        RD9.Value = dtv9;
                        RD9.Name = "DataSetTotal";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTableMensualPlanAnualDataTable dtv10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTableMensualPlanAnualDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTableMensualPlanAnualTableAdapter dav10 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTableMensualPlanAnualTableAdapter();

                        dav10.FillByMensual(dtv10, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.SedeInd);
                        
                        ReportDataSource RD10 = new ReportDataSource();
                        RD10.Value = dtv10;
                        RD10.Name = "DataSetMensual";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTableEjecutadasPlanAnualDataTable dtv11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.DataTableEjecutadasPlanAnualDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTableEjecutadasPlanAnualTableAdapter dav11 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.DataTableEjecutadasPlanAnualTableAdapter();

                        dav11.FillByEjecutadas(dtv11, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.SedeInd);
                        

                        ReportDataSource RD11 = new ReportDataSource();
                        RD11.Value = dtv11;
                        RD11.Name = "DataSetEjecutadas";



                 

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD9);
                        ReportViewer1.LocalReport.DataSources.Add(RD10);
                        ReportViewer1.LocalReport.DataSources.Add(RD11);
                   
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        
                        ReportParameter[] parameters8 = new ReportParameter[2];
                        //Establecemos el valor de los parámetros

                      
                        parameters8[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                        parameters8[1] = new ReportParameter("sedeTexto", SG_SST.Reportes.RecursoParametros.SedeTexto);



                        ReportViewer1.LocalReport.SetParameters(parameters8);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();


                        break;

                    case "EstandaresMinimos":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndEvalEstandaresMinimosDataTable dtv12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndEvalEstandaresMinimosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndEvalEstandaresMinimosTableAdapter dav12 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndEvalEstandaresMinimosTableAdapter();

                        //Sede
                        dav12.FillByEvaluaciones(dtv12, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        
                         ReportDataSource RD12 = new ReportDataSource();
                        RD12.Value = dtv12;
                        RD12.Name = "DataSetEvaluaciones";


                        ReportViewer1.LocalReport.DataSources.Clear();
                     
                        ReportViewer1.LocalReport.DataSources.Add(RD12);
                   
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        
                        ReportParameter[] parameters6 = new ReportParameter[1];
                        //Establecemos el valor de los parámetros

                      
                        parameters6[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());



                        ReportViewer1.LocalReport.SetParameters(parameters6);
                        
                        
                        
                        
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "AccidentesDeTrabajo":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable dtv13 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter dav13 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter();

                        
                        dav13.FillByTotalAt(dtv13, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD13 = new ReportDataSource();
                        RD13.Value = dtv13;
                        RD13.Name = "DataSetTotalAT";
                                               
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable dtv14 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter dav14 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter();

                        
                        dav14.FillByTotalHHT(dtv14, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD14 = new ReportDataSource();
                        RD14.Value = dtv14;
                        RD14.Name = "DataSetTotalHHT";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable dtv15 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter dav15 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter();

                        
                        dav15.FillByTotalATSede(dtv15, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD15 = new ReportDataSource();
                        RD15.Value = dtv15;
                        RD15.Name = "DataSetTotalATSede";
                        
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable dtv15a = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter dav15a = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter();

                        
                        dav15a.FillByTotalATMensual(dtv15a, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD15a = new ReportDataSource();
                        RD15a.Value = dtv15a;
                        RD15a.Name = "DataSetTotalATMensual";
                            
                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD13);
                        ReportViewer1.LocalReport.DataSources.Add(RD14);
                        ReportViewer1.LocalReport.DataSources.Add(RD15);
                        ReportViewer1.LocalReport.DataSources.Add(RD15a);

                        //Array que contendrá los parámetros
                       ReportParameter[] parameters = new ReportParameter[2];
                        //Establecemos el valor de los parámetros
                      
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);

                        parameters[0] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);

                        parameters[1] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());

                   
                   
                        
                        ReportViewer1.LocalReport.SetParameters(parameters);
                        
                        
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "TasaAccidentalidad":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndTasaAccidentalidadDataTable dtv16 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndTasaAccidentalidadDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndTasaAccidentalidadTableAdapter dav16 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndTasaAccidentalidadTableAdapter();

                     
                        dav16.FillByTotalAT(dtv16, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD16 = new ReportDataSource();
                        RD16.Value = dtv16;
                        RD16.Name = "DataSetTotalAT";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndTasaAccidentalidadDataTable dtv17 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndTasaAccidentalidadDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndTasaAccidentalidadTableAdapter dav17 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndTasaAccidentalidadTableAdapter();

                     
                        dav17.FillByPromTrabajadores(dtv17, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);
                        

                        ReportDataSource RD17 = new ReportDataSource();
                        RD17.Value = dtv17;
                        RD17.Name = "DataSetPromTrab";
                        
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndTasaAccidentalidadDataTable dtv18 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndTasaAccidentalidadDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndTasaAccidentalidadTableAdapter dav18 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndTasaAccidentalidadTableAdapter();

                     
                        dav18.FillByTotalATMes(dtv18,SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        

                        ReportDataSource RD18 = new ReportDataSource();
                        RD18.Value = dtv18;
                        RD18.Name = "DataSetTotalATMes";
                        
                        
                        
                        
                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD16);

                        ReportViewer1.LocalReport.DataSources.Add(RD17);

                        ReportViewer1.LocalReport.DataSources.Add(RD18);
                        

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        
                        ReportParameter[] parameters9 = new ReportParameter[1];
                        //Establecemos el valor de los parámetros

                      
                        parameters9[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());



                        ReportViewer1.LocalReport.SetParameters(parameters9);
                        
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "CapacitacionEntrenamiento":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndPlanCapacitacionDataTable dtv19 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndPlanCapacitacionDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndPlanCapacitacionTableAdapter dav19 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndPlanCapacitacionTableAdapter();

                
                        dav19.FillByMeses(dtv19,SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD19 = new ReportDataSource();
                        RD19.Value = dtv19;
                        RD19.Name = "DataSetMeses";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndPlanCapacitacionDataTable dtv20 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndPlanCapacitacionDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndPlanCapacitacionTableAdapter dav20 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndPlanCapacitacionTableAdapter();

                
                        dav20.FillByTotal(dtv20,SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD20 = new ReportDataSource();
                        RD20.Value = dtv20;
                        RD20.Name = "DataSetTotal";





                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD19);

                        ReportViewer1.LocalReport.DataSources.Add(RD20);

                   

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                       
                        ReportParameter[] parameters7 = new ReportParameter[1];
                        //Establecemos el valor de los parámetros

                      
                        parameters7[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());



                        ReportViewer1.LocalReport.SetParameters(parameters7);
                        
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "FrecuenciaAusentismo":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable dtv21 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter dav21 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter();

                      
                        dav21.FillByTotalAus(dtv21, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        
                     
                        ReportDataSource RD21 = new ReportDataSource();
                        RD21.Value = dtv21;
                        RD21.Name = "DataSetTotalAus";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable dtv22 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter dav22 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter();

                        dav22.FillByTotalAusSede(dtv22, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        
                     
                        ReportDataSource RD22 = new ReportDataSource();
                        RD22.Value = dtv22;
                        RD22.Name = "DataSetTotalAusSede";
                        
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable dtv23 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter dav23 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter();

                     
                        dav23.FillByTotalHHT(dtv23, SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);
                        
                     
                        ReportDataSource RD23 = new ReportDataSource();
                        RD23.Value = dtv23;
                        RD23.Name = "DataSetTotalHHT";

                        
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable dtv21b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter dav21b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAusentismoTableAdapter();

                      
                        dav21b.FillByTotalAusMensual(dtv21b, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        
                     
                        ReportDataSource RD21b = new ReportDataSource();
                        RD21b.Value = dtv21b;
                        RD21b.Name = "DataSetTotalAusMensual";
                        
                       




                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD21);

                        ReportViewer1.LocalReport.DataSources.Add(RD22);

                        ReportViewer1.LocalReport.DataSources.Add(RD23);
                        ReportViewer1.LocalReport.DataSources.Add(RD21b);

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        //Establecemos el valor de los parámetros
                      
                        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                          ReportParameter[] parameters2 = new ReportParameter[3];
                      
                        parameters2[0] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);
                        parameters2[1] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                      
                        parameters2[2] = new ReportParameter("contingenciaTexto",SG_SST.Reportes.RecursoParametros.ContigenciaTexto);

                        
                        ReportViewer1.LocalReport.SetParameters(parameters2);
                        
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "SeveridadAusentismo":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable dtv24 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter dav24 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter();


                        dav24.FillByTotalAus(dtv24, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);


                        ReportDataSource RD24 = new ReportDataSource();
                        RD24.Value = dtv24;
                        RD24.Name = "DataSetTotalAus";

                    
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable dtv25 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter dav25 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter();

                        dav25.FillByTotalAusSede(dtv25, SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);


                        ReportDataSource RD25 = new ReportDataSource();
                        RD25.Value = dtv25;
                        RD25.Name = "DataSetTotalAusSede";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable dtv26 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter dav26 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter();


                        dav26.FillByTotalHHT(dtv26, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);


                        ReportDataSource RD26 = new ReportDataSource();
                        RD26.Value = dtv26;
                        RD26.Name = "DataSetTotalHHT";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable dtv26b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadAusentismoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter dav26b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadAusentismoTableAdapter();


                        dav26b.FillByTotalAusMensual(dtv26b,SG_SST.Reportes.RecursoParametros.Contigencia, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        

                        ReportDataSource RD26b = new ReportDataSource();
                        RD26b.Value = dtv26b;
                        RD26b.Name = "DataSetTotalAusMensual";

                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD24);

                        ReportViewer1.LocalReport.DataSources.Add(RD25);

                        ReportViewer1.LocalReport.DataSources.Add(RD26);


                        ReportViewer1.LocalReport.DataSources.Add(RD26b);


                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parameters3 = new ReportParameter[3];
                        //Establecemos el valor de los parámetros

                      
                        parameters3[0] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);
                        parameters3[1] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                        parameters3[2] = new ReportParameter("contingenciaTexto", SG_SST.Reportes.RecursoParametros.ContigenciaTexto);




                        ReportViewer1.LocalReport.SetParameters(parameters3);

                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;



                    case "SeveridadAccidenteTrabajo":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadATDataTable dtv27 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadATDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadATTableAdapter dav27 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadATTableAdapter();


                        dav27.FillByTotalAus(dtv27,  SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);
             
                     
                        ReportDataSource RD27 = new ReportDataSource();
                        RD27.Value = dtv27;
                        RD27.Name = "DataSetTotalAus";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadATDataTable dtv28 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadATDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadATTableAdapter dav28 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadATTableAdapter();


                        dav28.FillByTotalAusSede(dtv28,SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
             
                     
                        ReportDataSource RD28 = new ReportDataSource();
                        RD28.Value = dtv28;
                        RD28.Name = "DataSetTotalAusSede";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable dtv29 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter dav29 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter();


                        dav29.FillByTotalHHT(dtv29, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);
              
                     
                        ReportDataSource RD29 = new ReportDataSource();
                        RD29.Value = dtv29;
                        RD29.Name = "DataSetTotalHHT";

                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadATDataTable dtv28b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndSeveridadATDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadATTableAdapter dav28b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndSeveridadATTableAdapter();


                        dav28b.FillByTotalAusMensual(dtv28b,SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.NitEmpresa);
             
                     
                        ReportDataSource RD28b = new ReportDataSource();
                        RD28b.Value = dtv28b;
                        RD28b.Name = "DataSetTotalAusMensual";

                       

                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD27);

                        ReportViewer1.LocalReport.DataSources.Add(RD28);
                        ReportViewer1.LocalReport.DataSources.Add(RD29);
                        ReportViewer1.LocalReport.DataSources.Add(RD28b);



                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parameters10 = new ReportParameter[2];
                        //Establecemos el valor de los parámetros


                        parameters10[0] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);

                        parameters10[1] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                       


                        ReportViewer1.LocalReport.SetParameters(parameters10);

                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;


                    case "LesionesIncapacitantes":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesTotalATDataTable dtv31 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesTotalATDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTotalATTableAdapter dav31 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTotalATTableAdapter();


                        dav31.FillByTotalAT(dtv31,  SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);
                        

                        ReportDataSource RD31 = new ReportDataSource();
                        RD31.Value = dtv31;
                        RD31.Name = "DataSetTotalAT";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable dtv32 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndFrecuenciaAccidentesTrabajoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter dav32 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndFrecuenciaAccidentesTrabajoTableAdapter();


                        dav32.FillByTotalHHT(dtv32,  SG_SST.Reportes.RecursoParametros.NitEmpresa,SG_SST.Reportes.RecursoParametros.Anio);


                        ReportDataSource RD32 = new ReportDataSource();
                        RD32.Value = dtv32;
                        RD32.Name = "DataSetTotalHHT";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesTotalATSedeDataTable dtv33 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesTotalATSedeDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTotalATSedeTableAdapter dav33 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTotalATSedeTableAdapter();


                        dav33.FillByTotalATSede(dtv33,  SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);


                        ReportDataSource RD33 = new ReportDataSource();
                        RD33.Value = dtv33;
                        RD33.Name = "DataSetTotalATSede";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesTotalDiasATDataTable dtv34 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesTotalDiasATDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTotalDiasATTableAdapter dav34 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTotalDiasATTableAdapter();


                        dav34.FillByTotalDiasAT(dtv34, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);


                        ReportDataSource RD34 = new ReportDataSource();
                        RD34.Value = dtv34;
                        RD34.Name = "DataSetTotalDiasAT";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesDataTable dtv34b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IndLesionesIncapacitantesDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTableAdapter dav34b = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IndLesionesIncapacitantesTableAdapter();


                        dav34b.FillByTotalATMensual(dtv34b, SG_SST.Reportes.RecursoParametros.Anio,SG_SST.Reportes.RecursoParametros.NitEmpresa);


                        ReportDataSource RD34b = new ReportDataSource();
                        RD34b.Value = dtv34b;
                        RD34b.Name = "DataSetTotalATMensual";



                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD31);

                        ReportViewer1.LocalReport.DataSources.Add(RD32);
                        ReportViewer1.LocalReport.DataSources.Add(RD33);
                        ReportViewer1.LocalReport.DataSources.Add(RD34);
                        ReportViewer1.LocalReport.DataSources.Add(RD34b);


                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parameters11 = new ReportParameter[2];
                        //Establecemos el valor de los parámetros


                        parameters11[0] = new ReportParameter("k", SG_SST.Reportes.RecursoParametros.ConstanteK);

                        parameters11[1] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());




                        ReportViewer1.LocalReport.SetParameters(parameters11);

                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();
                        break;

                    case "CumplimientoRequisitosLegales":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.indCumGeneralesDataSet1DataTable dtv35 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.indCumGeneralesDataSet1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.indCumGeneralesDataSet1TableAdapter dav35 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.indCumGeneralesDataSet1TableAdapter();


                        dav35.FillDataSet1(dtv35, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);


                        ReportDataSource RD35 = new ReportDataSource();
                        RD35.Value = dtv35;
                        RD35.Name = "DataSet1";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.indCumGeneralesDataSet2DataTable dtv36 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.indCumGeneralesDataSet2DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.indCumGeneralesDataSet2TableAdapter dav36 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.indCumGeneralesDataSet2TableAdapter();


                        dav36.FillByDataSet2(dtv36, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio);


                        ReportDataSource RD36 = new ReportDataSource();
                        RD36.Value = dtv36;
                        RD36.Name = "DataSet2";
                        
                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportViewer1.LocalReport.DataSources.Add(RD35);

                        ReportViewer1.LocalReport.DataSources.Add(RD36);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                       ReportParameter[] parametersReqLeg = new ReportParameter[1];
                        //Establecemos el valor de los parámetros


                       parametersReqLeg[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());




                       ReportViewer1.LocalReport.SetParameters(parametersReqLeg);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();

                        break;


                    case "ComiteCoppast":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet1DataTable dtv37 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet1TableAdapter dav37 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet1TableAdapter();


                        dav37.FillByFiltro(dtv37, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede);

                       

                        ReportDataSource RD37 = new ReportDataSource();
                        RD37.Value = dtv37;
                        RD37.Name = "DataSet1";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet2DataTable dtv38 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet2DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet2TableAdapter dav38 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet2TableAdapter();


                        dav38.FillByFiltro(dtv38, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede);

                       

                        ReportDataSource RD38 = new ReportDataSource();
                        RD38.Value = dtv38;
                        RD38.Name = "DataSet2";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet3DataTable dtv39 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet3DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet3TableAdapter dav39 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet3TableAdapter();


                        dav39.FillByFiltro(dtv39, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede);

                   
                       

                        ReportDataSource RD39 = new ReportDataSource();
                        RD39.Value = dtv39;
                        RD39.Name = "DataSet3";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet4DataTable dtv40 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet4DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet4TableAdapter dav40 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet4TableAdapter();


                        dav40.FillByFiltro(dtv40, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede);

                       
                       

                        ReportDataSource RD40 = new ReportDataSource();
                        RD40.Value = dtv40;
                        RD40.Name = "DataSet4";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet5DataTable dtv41 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndCoppastDataSet5DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet5TableAdapter dav41 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndCoppastDataSet5TableAdapter();


                        dav41.FillByFiltro(dtv41, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede);

                       
                       

                        ReportDataSource RD41 = new ReportDataSource();
                        RD41.Value = dtv41;
                        RD41.Name = "DataSet5";
                        
                        
                        


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD37);
                        ReportViewer1.LocalReport.DataSources.Add(RD38);
                        ReportViewer1.LocalReport.DataSources.Add(RD39);
                        ReportViewer1.LocalReport.DataSources.Add(RD40);
                        ReportViewer1.LocalReport.DataSources.Add(RD41);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parametersCoppast = new ReportParameter[2];
                        //Establecemos el valor de los parámetros


                        parametersCoppast[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                        parametersCoppast[1] = new ReportParameter("sedeTexto", SG_SST.Reportes.RecursoParametros.SedeTexto);




                        ReportViewer1.LocalReport.SetParameters(parametersCoppast);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();

                        break;



                    case "DxCondicionesSalud":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable dtv42 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter dav42 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter();


                        dav42.FillByDxCIE10(dtv42, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
     

                        ReportDataSource RD42 = new ReportDataSource();
                        RD42.Value = dtv42;
                        RD42.Name = "DataSetDxCIE10";

                    
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable dtv43 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter dav43 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter();


                        dav43.FillByPruebasClinicas(dtv43, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
     

                        ReportDataSource RD43 = new ReportDataSource();
                        RD43.Value = dtv43;
                        RD43.Name = "DataSetPruebasClinicas";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable dtv44 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter dav44 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter();


                        dav44.FillByPruebasPClinicas(dtv44, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
     

                        ReportDataSource RD44 = new ReportDataSource();
                        RD44.Value = dtv44;
                        RD44.Name = "DataSetPruebasParaclinicas";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable dtv45 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.IndDxSaludDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter dav45 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.IndDxSaludTableAdapter();


                        dav45.FillBySintomatologia(dtv45, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
     

                        ReportDataSource RD45 = new ReportDataSource();
                        RD45.Value = dtv45;
                        RD45.Name = "DataSetSintomatologia";
                        
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD42);
                        ReportViewer1.LocalReport.DataSources.Add(RD43);
                        ReportViewer1.LocalReport.DataSources.Add(RD44);
                        ReportViewer1.LocalReport.DataSources.Add(RD45);
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parametersDxSalud = new ReportParameter[3];
                        //Establecemos el valor de los parámetros


                        parametersDxSalud[0] = new ReportParameter("anio", SG_SST.Reportes.RecursoParametros.Anio.ToString());
                        parametersDxSalud[1] = new ReportParameter("sedeTexto", SG_SST.Reportes.RecursoParametros.SedeTexto);
                        parametersDxSalud[2] = new ReportParameter("procesoTexto", SG_SST.Reportes.RecursoParametros.ProcesoTexto);

                        ReportViewer1.LocalReport.SetParameters(parametersDxSalud);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();

                        break;


                    case "PerfilSocioDemografico":

                        ReportViewer1.Reset();
                        ReportViewer1.Visible = false;

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_GradoEscolaridadPerfilSocioDataTable dtv46 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_GradoEscolaridadPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_GradoEscolaridadPerfilSocioTableAdapter dav46 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_GradoEscolaridadPerfilSocioTableAdapter();


                        dav46.FillByGradoEscolaridad(dtv46, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD46 = new ReportDataSource();
                        RD46.Value = dtv46;
                        RD46.Name = "DataSet1GradoEsc";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_CiudadResidenciaPerfilSocioDataTable dtv47 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_CiudadResidenciaPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_CiudadResidenciaPerfilSocioTableAdapter dav47 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_CiudadResidenciaPerfilSocioTableAdapter();


                        dav47.FillByCiudad(dtv47, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD47 = new ReportDataSource();
                        RD47.Value = dtv47;
                        RD47.Name = "DataSet2Ciudad";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_SexoPerfilSocioDataTable dtv48 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_SexoPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_SexoPerfilSocioTableAdapter dav48 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_SexoPerfilSocioTableAdapter();


                        dav48.FillBySexo(dtv48, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD48 = new ReportDataSource();
                        RD48.Value = dtv48;
                        RD48.Name = "DataSet3Sexo";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IngresosPerfilSocioDataTable dtv49 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_IngresosPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IngresosPerfilSocioTableAdapter dav49 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_IngresosPerfilSocioTableAdapter();


                        dav49.FillByIngresos(dtv49, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD49 = new ReportDataSource();
                        RD49.Value = dtv49;
                        RD49.Name = "DataSet4Ingresos";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_VinculacionLaboralPerfilSocioDataTable dtv50 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_VinculacionLaboralPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_VinculacionLaboralPerfilSocioTableAdapter dav50 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_VinculacionLaboralPerfilSocioTableAdapter();


                        dav50.FillByVinculacionLaboral(dtv50, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD50 = new ReportDataSource();
                        RD50.Value = dtv50;
                        RD50.Name = "DataSet5VinculacionLab";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_EstratoSocioeconoPerfilSocioDataTable dtv51 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_EstratoSocioeconoPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_EstratoSocioeconoPerfilSocioTableAdapter dav51 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_EstratoSocioeconoPerfilSocioTableAdapter();


                        dav51.FillByEstrato(dtv51, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD51 = new ReportDataSource();
                        RD51.Value = dtv51;
                        RD51.Name = "DataSet6EstratoSoc";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_EstadoCivilPerfilSocioDataTable dtv52 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_EstadoCivilPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_EstadoCivilPerfilSocioTableAdapter dav52 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_EstadoCivilPerfilSocioTableAdapter();


                        dav52.FillByEstadoCivil(dtv52, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD52 = new ReportDataSource();
                        RD52.Value = dtv52;
                        RD52.Name = "DataSet7EstadoCivil";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_ConyuguePerfilSocioDataTable dtv53 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_ConyuguePerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_ConyuguePerfilSocioTableAdapter dav53 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_ConyuguePerfilSocioTableAdapter();


                        dav53.FillByConyuge(dtv53, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD53 = new ReportDataSource();
                        RD53.Value = dtv53;
                        RD53.Name = "DataSet8Conyuge";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_HijosPerfilSocioDataTable dtv54 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_HijosPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_HijosPerfilSocioTableAdapter dav54 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_HijosPerfilSocioTableAdapter();


                        dav54.FillByHijos(dtv54, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD54 = new ReportDataSource();
                        RD54.Value = dtv54;
                        RD54.Name = "DataSet9Hijos";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_EtniaPerfilsocioDataTable dtv55 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_EtniaPerfilsocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_EtniaPerfilsocioTableAdapter dav55 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_EtniaPerfilsocioTableAdapter();
                        dav55.FillByEtnia(dtv55, SG_SST.Reportes.RecursoParametros.NitEmpresa,  SG_SST.Reportes.RecursoParametros.Sede, SG_SST.Reportes.RecursoParametros.Proceso);
                      
                        ReportDataSource RD55 = new ReportDataSource();
                        RD55.Value = dtv55;
                        RD55.Name = "DataSet10Etnia";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_GeneralPerfilSocioDataTable dtv56 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.V_GeneralPerfilSocioDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_GeneralPerfilSocioTableAdapter dav56 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.V_GeneralPerfilSocioTableAdapter();
                        dav56.FillByFiltro(dtv56, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                      
                        ReportDataSource RD56 = new ReportDataSource();
                        RD56.Value = dtv56;
                        RD56.Name = "DataSet11Generalperfil";

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD46);
                        ReportViewer1.LocalReport.DataSources.Add(RD47);
                        ReportViewer1.LocalReport.DataSources.Add(RD48);
                        ReportViewer1.LocalReport.DataSources.Add(RD49);
                        ReportViewer1.LocalReport.DataSources.Add(RD50);
                        ReportViewer1.LocalReport.DataSources.Add(RD51);
                        ReportViewer1.LocalReport.DataSources.Add(RD52);
                        ReportViewer1.LocalReport.DataSources.Add(RD53);
                        ReportViewer1.LocalReport.DataSources.Add(RD54);
                        ReportViewer1.LocalReport.DataSources.Add(RD55);
                        ReportViewer1.LocalReport.DataSources.Add(RD56);
                        
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                        ReportParameter[] parametersPerfilSocio = new ReportParameter[2];
                        //Establecemos el valor de los parámetros
                        parametersPerfilSocio[0] = new ReportParameter("sedeTexto", SG_SST.Reportes.RecursoParametros.SedeTexto);
                        parametersPerfilSocio[1] = new ReportParameter("procesoTexto", SG_SST.Reportes.RecursoParametros.ProcesoTexto);

                        ReportViewer1.LocalReport.SetParameters(parametersPerfilSocio);
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.Refresh();

                        break;

                    case "Comunicaciones":

                        ReportViewer1.Reset();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_UnionTotalDataTable dtv57 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_UnionTotalDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_UnionTotalTableAdapter dav57 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_UnionTotalTableAdapter();

                        dav57.FillByUnionTotal(dtv57, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD57 = new ReportDataSource();
                        RD57.Value = dtv57;
                        RD57.Name = "uniontotal";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_ComunicaAPPDataTable dtv58 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_ComunicaAPPDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_ComunicaAPPTableAdapter dav58 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_ComunicaAPPTableAdapter();


                        dav58.FillByComunicaApp(dtv58, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD58 = new ReportDataSource();
                        RD58.Value = dtv58;
                        RD58.Name = "ComunicAPP";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_AdjuntosDataTable dtv59 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_AdjuntosDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_AdjuntosTableAdapter dav59 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_AdjuntosTableAdapter();


                        dav59.FillByAdjuntos(dtv59, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Estado, SG_SST.Reportes.RecursoParametros.Anio);

                        ReportDataSource RD59= new ReportDataSource();
                        RD59.Value = dtv59;
                        RD59.Name = "adjuntos";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.ComunicacionesInternosIndDataTable dtv60 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.ComunicacionesInternosIndDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.ComunicacionesInternosIndTableAdapter dav60 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.ComunicacionesInternosIndTableAdapter();


                        dav60.FillByInternos(dtv60, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Estado);
                      
                        ReportDataSource RD60 = new ReportDataSource();
                        RD60.Value = dtv60;
                        RD60.Name = "Internos";


                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesAñoComunicaDataTable dtv61 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesAñoComunicaDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesAñoComunicaTableAdapter dav61 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesAñoComunicaTableAdapter();


                        dav61.Fill(dtv61, SG_SST.Reportes.RecursoParametros.NitEmpresa);

                        ReportDataSource RD61 = new ReportDataSource();
                        RD61.Value = dtv61;
                        RD61.Name = "añoscomuni";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_EstadoDataTable dtv62 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.Comunicaciones_EstadoDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_EstadoTableAdapter dav62 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.Comunicaciones_EstadoTableAdapter();


                        dav62.FillByFiltro(dtv62);

                        ReportDataSource RD62 = new ReportDataSource();
                        RD62.Value = dtv62;
                        RD62.Name = "estado";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.ComunicacionesEfectiComuniIndDataTable dtv63 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.ComunicacionesEfectiComuniIndDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.ComunicacionesEfectiComuniIndTableAdapter dav63 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.ComunicacionesEfectiComuniIndTableAdapter();


                        dav63.FillByEfectiComun(dtv63, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD63 = new ReportDataSource();
                        RD63.Value = dtv63;
                        RD63.Name = "efecticomuni";

                        SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesNoEfectiDataTable dtv64 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSet.ComunicacionesNoEfectiDataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesNoEfectiTableAdapter dav64 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetTableAdapters.ComunicacionesNoEfectiTableAdapter();


                        dav64.FillByNoEfecti(dtv64, SG_SST.Reportes.RecursoParametros.NitEmpresa, SG_SST.Reportes.RecursoParametros.Anio, SG_SST.Reportes.RecursoParametros.Estado);

                        ReportDataSource RD64 = new ReportDataSource();
                        RD64.Value = dtv64;
                        RD64.Name = "noefecti";
                        
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.ComunicacionesDataSet1DataTable dtv65 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetInd.ComunicacionesDataSet1DataTable();
                        SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.ComunicacionesDataSet1TableAdapter dav65 = new SG_SST.Views.ReportesAplicacion.SGSSTDataSetIndTableAdapters.ComunicacionesDataSet1TableAdapter();


                        dav65.FillByDataSet1(dtv65, SG_SST.Reportes.RecursoParametros.NitEmpresa);
                     
                        ReportDataSource RD65 = new ReportDataSource();
                        RD65.Value = dtv65;
                        RD65.Name = "DataSet1";



                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(RD57);
                        ReportViewer1.LocalReport.DataSources.Add(RD58);
                        ReportViewer1.LocalReport.DataSources.Add(RD59);
                        ReportViewer1.LocalReport.DataSources.Add(RD60);
                        ReportViewer1.LocalReport.DataSources.Add(RD61);
                        ReportViewer1.LocalReport.DataSources.Add(RD62);
                        ReportViewer1.LocalReport.DataSources.Add(RD63);
                        ReportViewer1.LocalReport.DataSources.Add(RD64);
                        ReportViewer1.LocalReport.DataSources.Add(RD65);
                        
                   
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Content/ReportesFisicos/Indicadores/" + SG_SST.Reportes.RecursoParametros.NombreReporte);
                     
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
        <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Height="1200px" Width="100%" Enabled="true"
                BackColor="White" BorderColor="black" Font-Bold="true" InternalBorderStyle="None" ShowParameterPrompts="true"   
                AsyncRendering="false" ShowExportControls="true" ShowBackButton="False" ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" ShowZoomControl="False" InteractivityPostBackMode="SynchronousOnDrillthrough" PageCountMode="Actual" >

     </rsweb:ReportViewer>
    </div>
       
    </form>
</body>
</html>
