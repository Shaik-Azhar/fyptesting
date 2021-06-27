using System;
using System.Linq;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformDeleteVendor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("vendorID"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            long vendorID;

            if (!long.TryParse(Request.Form["vendorID"], out vendorID))
            {
                Response.Write("Invalid vendor ID!");
                return;
            }

            if (vendorID <= 0)
            {
                Response.Write("Invalid vendor ID!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "UPDATE vendors SET WasDeleted=1 WHERE ID=@vendorID";
                    command.Parameters.AddWithValue("vendorID", vendorID);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}