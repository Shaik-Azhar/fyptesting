using System;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("userID") || !postKeys.Contains("oldPassword") || !postKeys.Contains("newPassword"))
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

            string oldPassword = Request.Form["oldPassword"];

            if (String.IsNullOrEmpty(oldPassword))
            {
                Response.Write("Please enter old password!");
                return;
            }

            string newPassword = Request.Form["newPassword"];

            if (String.IsNullOrEmpty(newPassword))
            {
                Response.Write("Please enter new password!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            bool isPasswordCorrect = false;

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM users WHERE ID=@userID AND PasswordHash=@passwordHash";
                    command.Parameters.AddWithValue("userID", userID);
                    command.Parameters.AddWithValue("passwordHash", Utils.HashPassword(oldPassword));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        if (dbHelper.ReadLong(reader, 0) == 1)
                            isPasswordCorrect = true;

                        reader.Close();
                    }
                }

                if (isPasswordCorrect)
                {
                    using (var command = dbHelper.GetSQLiteCommand(conn))
                    {
                        command.CommandText = "UPDATE users SET PasswordHash=@passwordHash WHERE ID=@userID";
                        command.Parameters.AddWithValue("passwordHash", Utils.HashPassword(newPassword));
                        command.Parameters.AddWithValue("userID", userID);

                        command.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }

            if (isPasswordCorrect)
                Response.Write("Success");
            else
                Response.Write("Invalid");
        }
    }
}