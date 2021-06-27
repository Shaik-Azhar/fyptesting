using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformAddHolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("holderName"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string holderName = Request.Form["holderName"];

            if (String.IsNullOrEmpty(holderName))
            {
                Response.Write("Please enter Asset Holder name!");
                return;
            }

           

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO assetholder (holderName) VALUES " +
                                          "(@holderName)";
                    command.Parameters.AddWithValue("holderName", holderName);
                   

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}