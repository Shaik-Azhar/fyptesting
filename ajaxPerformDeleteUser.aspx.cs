using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformDeleteUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("userID"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            long userID;

            try
            {
                userID = long.Parse(Request.Form["userID"]);
            }
            catch
            {
                Response.Write("Invalid user ID!");
                return;
            }

            if (userID <= 0)
            {
                Response.Write("Invalid user ID!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "UPDATE users SET WasDeleted=1 WHERE ID=@userID";
                    command.Parameters.AddWithValue("userID", userID);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}