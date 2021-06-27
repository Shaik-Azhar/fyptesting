using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Asset_Tracking_System
{
    public partial class ajaxGenerateArchive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("startDate") || !postKeys.Contains("endDate"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string startDate = Request.Form["startDate"];

            if (String.IsNullOrEmpty(startDate))
            {
                Response.Write("Please enter start date!");
                return;
            }

            string endDate = Request.Form["endDate"];

            if (String.IsNullOrEmpty(endDate))
            {
                Response.Write("Please enter end date!");
                return;
            }

            string html = "<h3>Archived Assets</h3>" +
                          "<table width='90%' border='1' class='manage-table'>" +
                          "<tr>" + 
                          "    <th>No</th>" +
                          "    <th>Asset Name</th>" +
                          "    <th>Vendor</th>" +
                          "    <th>Date Deleted</th>" +
                          "</tr>";


            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT Name,assets.deletedDate,VendorID FROM assets WHERE assets.deletedDate BETWEEN '" + startDate + "' AND '" + endDate + "' AND assets.WasDeleted=1";
                      //  "WHERE  assets.deletedDate BETWEEN '" + startDate + "' AND '" + endDate + "'";
                       
                    //"SELECT assets.Name,assets.deletedDate,vendors.VendorName FROM assets,vendors WHERE  assets.deletedDate BETWEEN '" + startDate + "' AND '" + endDate + "'";
                    //"SELECT assets.deletedDate,assets.Name,rooms.Name,vendors.VendorName FROM assets,rooms,vendors,transferhistory WHERE  transferhistory.NewRoomID=rooms.ID  AND assets.deletedDate BETWEEN '" + startDate + "' AND '" + endDate + "'";
                     
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int no = 1;

                        while (reader.Read())
                        {  
                            string assetName = dbHelper.ReadString(reader, 0);
                            string vendorName = dbHelper.ReadString(reader, 2);
                            string deleteDate = dbHelper.ReadString(reader, 1); 

                            html += "<tr valign='top' align='center'>" +
                                    "    <td>" + no++ + "</td>" +
                                    "    <td>" + assetName + "</td>" +
                                    "    <td>" + vendorName + "</td>" +
                                    "    <td>" + deleteDate + "</td>" +
                                    "</tr>";
                        }

                        reader.Close();
                    }
                }

                conn.Close();
            }

            html += "</table>";

            Response.Write(html);
        }
        public static string GetRoomName(long roomID)
        {
            string roomName = "";

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT Name FROM rooms WHERE ID=@roomID";
                    command.Parameters.AddWithValue("roomID", roomID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            roomName = dbHelper.ReadString(reader, 0);
                        }

                        reader.Close();
                    }
                }

                conn.Close();
            }

            return roomName;
        }

        protected void btnReturnToMainMenu_Click(object sender, EventArgs e)
        {
            if (UserProfile.accountType.Equals("Operator"))
                Response.Redirect("MainOperator.aspx");
            else if (UserProfile.accountType.Equals("Administrator"))
                Response.Redirect("MainAdmin.aspx");
            else
                Response.Redirect("Login.aspx");
        }
    }
}
        