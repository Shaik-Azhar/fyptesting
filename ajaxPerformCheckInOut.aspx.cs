using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Asset_Tracking_System
{
    public partial class ajaxPerformCheckInOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("barcode"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string strBarcode = Request.Form["barcode"];

            if (String.IsNullOrEmpty(strBarcode))
            {
                Response.Write("Please enter barcode!");
                return;
            }

            long barcode;

            if (!long.TryParse(strBarcode, out barcode))
            {
                Response.Write("Invalid barcode!");
                return;
            }

            string html = "";
            string button = "";

            string roomOptions = Utils.GenerateRoomOptions();

            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())
            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {
                    command.CommandText = "SELECT assets.ID,assets.Name,assets.Description,rooms.ID,rooms.Name,assets.Status FROM assets,rooms WHERE assets.Barcode=@barcode AND assets.RoomID=rooms.ID AND assets.WasDeleted=0";
                    command.Parameters.AddWithValue("barcode", "" + barcode);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long assetID = dbHelper.ReadLong(reader, 0);
                            string assetName = dbHelper.ReadString(reader, 1);
                            string assetDescription = dbHelper.ReadString(reader, 2);
                            long roomID = dbHelper.ReadLong(reader, 3);
                            string roomName = dbHelper.ReadString(reader, 4);
                            string assetStatus = dbHelper.ReadString(reader, 5);

                            
                                

                                html = "<table width='60%'>" +
                                       "    <tr valign='top' align'center'>" +
                                       "    <td width='100%'><table width='100%'>" +
                                       "        <tr>" +
                                       "            <td width='40%'>Asset Name:</td>" +
                                       "            <td width='60%'>" + assetName + "</td>" +
                                       "        </tr>" +
                                       "        <tr>" +
                                       "            <td colspan='2'><br /></td>" +
                                       "        </tr>" +
                                       "        <tr>" +
                                       "            <td>Description:</td>" +
                                       "            <td>" + assetDescription + "</td>" +
                                       "        </tr>" +
                                       "        <tr>" +
                                       "            <td colspan='2'><br /></td>" +
                                       "        </tr>" +
                                       "        <tr>" +
                                       "            <td>Current Location:</td>" +
                                       "            <td>" + assetStatus + "</td>" +
                                       "        </tr>" +
                                       "        <tr>" +
                                       "            <td colspan='2'><br /></td>" +
                                       "        </tr>" +
                                       
                                       "        </table></td>" +
                                       "    </tr>" +
                                       "</table>";

                                


                            
                            

                        }
                      

                        reader.Close();
                    }
                }
                conn.Close();
            }

            if (!string.IsNullOrEmpty(html))
                Response.Write("Success" + html);
            else
                Response.Write("Asset not found!");
        }
    }
}