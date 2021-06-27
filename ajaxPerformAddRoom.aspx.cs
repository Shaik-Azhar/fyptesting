using System;
using System.Linq;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformAddRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("name") || !postKeys.Contains("description"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string name = Request.Form["name"];

            if (String.IsNullOrEmpty(name))
            {
                Response.Write("Please enter name!");
                return;
            }

            string description = Request.Form["description"];

            if (String.IsNullOrEmpty(description))
            {
                Response.Write("Please enter description!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO rooms (Name,Description) VALUES " +
                                          "(@name,@description)";
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("description", description);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}