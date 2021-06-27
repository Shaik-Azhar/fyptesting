using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asset_Tracking_System
{
    public partial class AddHolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserProfile.username == null || UserProfile.name == null || UserProfile.accountType == null || !UserProfile.accountType.Equals("Administrator"))
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }
    }
}