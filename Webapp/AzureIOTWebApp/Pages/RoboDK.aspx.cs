using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureIOTWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsSecureConnection)
            {
                string httpUrl = "http://" + Request.Url.Host + Request.RawUrl;
                Response.Redirect(httpUrl);
            }
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