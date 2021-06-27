using System;
using System.Linq;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformDeleteRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("roomID"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            long roomID;

            try
            {
                roomID = long.Parse(Request.Form["roomID"]);
            }
            catch
            {
                Response.Write("Invalid room ID!");
                return;
            }

            if (roomID <= 0)
            {
                Response.Write("Invalid room ID!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "UPDATE rooms SET WasDeleted=1 WHERE ID=@roomID";
                    command.Parameters.AddWithValue("roomID", roomID);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            Response.Write("Success");
        }
    }
}