using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureIOTWebApp
{
    public partial class DataTable : Page
    {

        [WebMethod]
        public static string BindTelemetryData()
        {
            var GetData = new DBTelemetryData();


            return GetData.GetTelemetryData().Result;

        }

        protected override void OnPreInit(EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                MasterPageFile = "~/Site.Mobile.Master";
            }
            base.OnPreInit(e);
        }


    }
}