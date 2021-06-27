using System;
using MySql.Data.MySqlClient;

namespace Asset_Tracking_System
{
    public partial class AddAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserProfile.username == null || UserProfile.name == null || UserProfile.accountType == null ||
                (!UserProfile.accountType.Equals("Administrator") && !UserProfile.accountType.Equals("Operator")))
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }
       
        public static string GenerateRoomOptions()
        {
            string html = "";

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT ID,Name FROM rooms WHERE rooms.WasDeleted=0";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = dbHelper.ReadLong(reader, 0);
                            string name = dbHelper.ReadString(reader, 1);

                            html += "<option value='" + id + "'>" + name + "</option>";
                        }

                        reader.Close();
                    }
                }
                conn.Close();
            }

            return html;
        }

        public static string GenerateVendorOptions()
        {
            string html = "";

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT ID,VendorName FROM vendors WHERE vendors.WasDeleted=0";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = dbHelper.ReadLong(reader, 0);
                            string name = dbHelper.ReadString(reader, 1);

                            html += "<option value='" + id + "'>" + name + "</option>";
                        }

                        reader.Close();
                    }
                }
                conn.Close();
            }

            return html;
        }
        public static string GenerateHolderOptions()
        {
            string html = "";

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT ID,holderName FROM assetholder WHERE assetholder.WasDeleted=0";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = dbHelper.ReadLong(reader, 0);
                            string name = dbHelper.ReadString(reader, 1);

                            html += "<option value='" + id + "'>" + name + "</option>";
                        }

                        reader.Close();
                    }
                }
                conn.Close();
            }

            return html;
        }

    }
}