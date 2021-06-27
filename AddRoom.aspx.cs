using System;
using MySql.Data.MySqlClient;

namespace Asset_Tracking_System
{
    public partial class AddRoom : System.Web.UI.Page
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