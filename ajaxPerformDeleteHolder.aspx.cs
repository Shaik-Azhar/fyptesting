using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Asset_Tracking_System
{
    public partial class ajaxPerformDeleteHolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("holderID"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            long holderID;

            if (!long.TryParse(Request.Form["holderID"], out holderID))
            {
                Response.Write("Invalid vendor ID!");
                return;
            }

            if (holderID <= 0)
            {
                Response.Write("Invalid Holder ID!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "UPDATE assetholder SET WasDeleted=1 WHERE ID=@holderID";
                    command.Parameters.AddWithValue("holderID", holderID);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}