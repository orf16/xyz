using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SG_SST.Views.ReportesAplicacion
{
    public partial class VisorDeReportesAus : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewer1.Reset();
            this.ReportViewer1.LocalReport.Refresh();
        }

      
    }
}