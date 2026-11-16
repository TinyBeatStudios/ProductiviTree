using System;
using System.Collections.Generic;
using Business_Logic_Layer;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI_Layer
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int userId = (Session["UserId"] as int?) ?? 1;
            ThemeLink.Href = ResolveUrl("~/Themes/" + new ShopService().GetThemeCssFile(userId));
        }
    }
}