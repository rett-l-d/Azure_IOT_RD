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
        public static string datacachedStr { get; set; }
        public static string localtimestr { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool datacached = false;
            if (HttpContext.Current.Application["CacheNote"] != null)
            {
                //Retreive session state variable. If data is cached or not.
                datacached = (bool)HttpContext.Current.Application["CacheNote"]; 
            }

            if (HttpContext.Current.Application["lastupdateTime"] == null)
            {
                //If last update time session variable is null update it.
                HttpContext.Current.Application["lastupdateTime"] = DateTime.Now.ToString();
                localtimestr = HttpContext.Current.Application["lastupdateTime"].ToString();
            }

            if (datacached)
            {
                localtimestr = HttpContext.Current.Application["lastupdateTime"].ToString();
                datacachedStr = "Data retreived from cache. Last Update was at ";
            }
            else
            {
                HttpContext.Current.Application["lastupdateTime"] = DateTime.Now.ToString();
                localtimestr = HttpContext.Current.Application["lastupdateTime"].ToString();
                datacachedStr = "Data has been updated at ";
            }
        }

        [WebMethod]
        public static string BindTelemetryData()
        {
            var GetData = new DBTelemetryData();
            bool overridecache = false;

            if (HttpContext.Current.Session["refreshdata"] != null)
            {
                //Refresh Data, bypassing cache.
                overridecache = (bool)HttpContext.Current.Session["refreshdata"];
            }

            var result = GetData.GetOrSetJsonCache(overridecache, out bool datacached);
            

            if (datacached)
            {
                //set application persistent variable. Data Retreived was from cache.
                HttpContext.Current.Application["CacheNote"] = datacached;
            }

            HttpContext.Current.Session["refreshdata"] = false;
            return result;

        }

        protected override void OnPreInit(EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                MasterPageFile = "~/Site.Mobile.Master";
            }
            base.OnPreInit(e);
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["refreshdata"] = true;
            HttpContext.Current.Application["CacheNote"] = false;

            HttpContext.Current.Application["lastupdateTime"] = DateTime.Now.ToString();
            localtimestr = HttpContext.Current.Application["lastupdateTime"].ToString();
            datacachedStr = "Data has been updated at ";
        }
    }
}