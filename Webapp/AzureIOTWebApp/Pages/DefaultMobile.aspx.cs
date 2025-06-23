using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureIOTWebApp
{
    public partial class _DefaultMobile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var GetData = new DBTelemetryData();


            var dt = GetData.GetTelemetryDataNonblocking();
        }

       
    }
}