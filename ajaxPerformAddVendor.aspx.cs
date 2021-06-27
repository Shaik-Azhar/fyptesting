using System;
using System.Linq;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformAddVendor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("vendorName") || !postKeys.Contains("contactPerson") || !postKeys.Contains("mobileNumber"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string vendorName = Request.Form["vendorName"];

            if (String.IsNullOrEmpty(vendorName))
            {
                Response.Write("Please enter vendor name!");
                return;
            }

            string contactPerson = Request.Form["contactPerson"];

            if (String.IsNullOrEmpty(contactPerson))
            {
                Response.Write("Please enter contact person!");
                return;
            }

            string mobileNumber = Request.Form["mobileNumber"];

            if (String.IsNullOrEmpty(mobileNumber))
            {
                Response.Write("Please enter mobile number!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO vendors (VendorName,ContactPerson,MobileNumber) VALUES " +
                                          "(@vendorName,@contactPerson,@mobileNumber)";
                    command.Parameters.AddWithValue("vendorName", vendorName);
                    command.Parameters.AddWithValue("contactPerson", contactPerson);
                    command.Parameters.AddWithValue("mobileNumber", mobileNumber);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}