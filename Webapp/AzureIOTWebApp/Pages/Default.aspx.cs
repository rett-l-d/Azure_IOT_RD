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

            if (!Context.Request.IsSecureConnection)
            {
                string url = "https://" + Context.Request.Url.Host + Context.Request.RawUrl;
                Response.Redirect(url, true);
            }


        }
    }
}