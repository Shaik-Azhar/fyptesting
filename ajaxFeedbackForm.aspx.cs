using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asset_Tracking_System
{
    public partial class ajaxFeedbackForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("aid") || !postKeys.Contains("name") || !postKeys.Contains("barcode") || !postKeys.Contains("message") )
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string aid = Request.Form["aid"];

            if (String.IsNullOrEmpty(aid))
            {
                Response.Write("Please enter Asset ID!");
                return;
            }

            string name = Request.Form["name"];

            if (String.IsNullOrEmpty(name))
            {
                Response.Write("Please enter Asset Name!");
                return;
            }

            
            string barcode = Request.Form["barcode"];

            if (String.IsNullOrEmpty(barcode))
            {
                Response.Write("Please enter barcode!");
                return;
            }

            string message = Request.Form["message"];

            if (String.IsNullOrEmpty(message))
            {
                Response.Write("Please enter your Feedback!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO feedbackform (assetID,assetName,barcode,message) VALUES " +
                                          "(@aid,@name,@barcode,@message)";
                    command.Parameters.AddWithValue("aid", aid);
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("barcode", barcode);
                    command.Parameters.AddWithValue("message", message);
                  
                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}