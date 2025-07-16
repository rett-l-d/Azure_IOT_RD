using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureIOTWebApp.Pages
{
    public partial class DataTablesDemo : System.Web.UI.Page
    {
        public static string datacachedStr { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.Request.IsSecureConnection)
            {
                string url = "https://" + Context.Request.Url.Host + Context.Request.RawUrl;
                Response.Redirect(url, true);
            }

            bool datacached = false;
            if (HttpContext.Current.Session["CacheNote"] != null)
            {
                datacached = (bool)HttpContext.Current.Session["CacheNote"];
            }

            if (HttpContext.Current.Session["lastupdateTime"] == null)
            {
                HttpContext.Current.Session["lastupdateTime"] = DateTime.Now.ToString();
            }

            if (datacached)
            {
                datacachedStr = "Data retreived from cache. Last Update was at " + HttpContext.Current.Session["lastupdateTime"];
            }
            else
            {
                HttpContext.Current.Session["lastupdateTime"] = DateTime.Now.ToString();
                datacachedStr = "Data has been updated at " + HttpContext.Current.Session["lastupdateTime"];
            }
        }

        [WebMethod]
        public static string BindTelemetryData()
        {
            var GetData = new DBTelemetryData();


            var result =  GetData.GetOrSetJsonCache(false, out bool datacached);

            if (datacached)
            {
                HttpContext.Current.Session["CacheNote"] = datacached;
            }

            return result;

        }
    }
}