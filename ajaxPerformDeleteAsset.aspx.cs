using System;
using System.Linq;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformDeleteAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("assetID"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            long assetID;

            try
            {
                assetID = long.Parse(Request.Form["assetID"]);
            }
            catch
            {
                Response.Write("Invalid asset ID!");
                return;
            }

            if (assetID <= 0)
            {
                Response.Write("Invalid asset ID!");
                return;
            }

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    
                   // command.CommandText = "UPDATE assets SET deletedDate=now() WHERE ID=@assetID";
                    command.CommandText = "UPDATE assets " +
                        "SET WasDeleted=1," +
                        "    deletedDate=now()" +
                        "WHERE ID=@assetID";
                    




                    command.Parameters.AddWithValue("assetID", assetID);
                    
                    command.ExecuteNonQuery();
                    
                }
                

                conn.Close();
            }
           

            Response.Write("Success");
        }
    }
}