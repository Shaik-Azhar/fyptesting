using System;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Asset_Tracking_System
{
    public partial class ajaxGenerateTransferHistoryReport : System.Web.UI.Page
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

            string html = "<h3>Transfer History Report</h3>" +
                          "<table width='90%' border='1' class='manage-table'>" +
                          "<tr>" +
                          "    <th>No</th>" +
                          "    <th>Timestamp</th>" +
                          "    <th>Asset Name</th>" +
                          "    <th>Current Location</th>" +
                          "    <th>Previous Location</th>" +
                          "    <th>Transferer Name</th>" +
                          "</tr>";

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT transferhistory.Timestamp,assets.Name,rooms.Name,transferhistory.OldRoomID,users.Name FROM transferhistory,assets,rooms,users WHERE transferhistory.AssetID=assets.ID AND transferhistory.NewRoomID=rooms.ID AND transferhistory.TransfererID=users.ID AND transferhistory.Timestamp BETWEEN '" + startDate + "' AND '" + endDate + "'";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int no = 1;

                        while (reader.Read())
                        {
                            string timestamp = dbHelper.ReadString(reader, 0);
                            string assetName = dbHelper.ReadString(reader, 1);
                            string newRoomName = dbHelper.ReadString(reader, 2);
                            long oldRoomID = dbHelper.ReadLong(reader, 3);
                            string transfererName = dbHelper.ReadString(reader, 4);

                            html += "<tr valign='top' align='center'>" +
                                    "    <td>" + no++ + "</td>" +
                                    "    <td>" + timestamp + "</td>" +
                                    "    <td>" + assetName + "</td>" +
                                    "    <td>" + newRoomName + "</td>" +
                                    "    <td>" + GetRoomName(oldRoomID) + "</td>" +
                                    "    <td>" + transfererName + "</td>" +
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
    }
}