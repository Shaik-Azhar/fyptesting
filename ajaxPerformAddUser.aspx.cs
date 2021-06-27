using System;
using System.Linq;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformAddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("username") || !postKeys.Contains("name") || !postKeys.Contains("password") || !postKeys.Contains("accountType"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string username = Request.Form["username"];

            if (String.IsNullOrEmpty(username))
            {
                Response.Write("Please enter username!");
                return;
            }

            string name = Request.Form["name"];

            if (String.IsNullOrEmpty(name))
            {
                Response.Write("Please enter name!");
                return;
            }

            string password = Request.Form["password"];

            if (String.IsNullOrEmpty(password))
            {
                Response.Write("Please enter password!");
                return;
            }

            string accountType = Request.Form["accountType"];

            if (String.IsNullOrEmpty(accountType))
            {
                Response.Write("Please select account type!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO users (Username,Name,PasswordHash,AccountType) VALUES " +
                                          "(@username,@name,@password,@accountType)";
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("password", Utils.HashPassword(password));
                    command.Parameters.AddWithValue("accountType", accountType);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}