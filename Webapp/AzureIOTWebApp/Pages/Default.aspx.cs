using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureIOTWebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice && !Request.Url.AbsolutePath.EndsWith("DefaultMobile.aspx", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Pages/DefaultMobile.aspx");
            }
            var GetData = new DBTelemetryData();


            var dt = GetData.GetTelemetryDataNonblocking();


        }

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (IsMobileDevice() || Request.QueryString["view"] == "mobile")
        //    {
        //        MasterPageFile = "~/Site.Mobile.Master";
        //    }
        //    else
        //    {
        //        MasterPageFile = "~/Site.master";
        //    }
        //}

        //private bool IsMobileDevice()
        //{
        //    var userAgent = Request.UserAgent?.ToLower() ?? "";
        //    return userAgent.Contains("iphone") ||
        //           userAgent.Contains("android") ||
        //           userAgent.Contains("windows phone") ||
        //           userAgent.Contains("mobile") ||
        //           userAgent.Contains("opera mini");
        //}
    }
}