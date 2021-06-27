using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asset_Tracking_System
{
    public partial class ajaxAddAssetOperator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] postKeys = Request.Form.AllKeys;

            if (!postKeys.Contains("name") || !postKeys.Contains("description") || !postKeys.Contains("room") || !postKeys.Contains("barcode") ||
                !postKeys.Contains("brand") || !postKeys.Contains("model") || !postKeys.Contains("acquiredDate") || !postKeys.Contains("price") ||
                !postKeys.Contains("vendorID") || !postKeys.Contains("status") || !postKeys.Contains("typeasset") || !postKeys.Contains("holderID"))
            {
                Response.Write("Missing argument(s)!");
                return;
            }

            string name = Request.Form["name"];

            if (String.IsNullOrEmpty(name))
            {
                Response.Write("Please enter name!");
                return;
            }

            string description = Request.Form["description"];

            if (String.IsNullOrEmpty(description))
            {
                Response.Write("Please enter description!");
                return;
            }

            long roomID;

            try
            {
                roomID = long.Parse(Request.Form["room"]);
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

            string barcode = Request.Form["barcode"];

            if (String.IsNullOrEmpty(barcode))
            {
                Response.Write("Please enter barcode!");
                return;
            }

            string brand = Request.Form["brand"];

            if (String.IsNullOrEmpty(brand))
            {
                Response.Write("Please enter brand!");
                return;
            }

            string model = Request.Form["model"];

            if (String.IsNullOrEmpty(model))
            {
                Response.Write("Please enter model!");
                return;
            }

            string acquiredDate = Request.Form["acquiredDate"];

            if (String.IsNullOrEmpty(acquiredDate))
            {
                Response.Write("Please enter acquired date!");
                return;
            }

            string strPrice = Request.Form["price"];

            if (String.IsNullOrEmpty(strPrice))
            {
                Response.Write("Please enter price!");
                return;
            }

            double price;

            if (!double.TryParse(strPrice, out price) || price <= 0.0)
            {
                Response.Write("Please enter a valid price!");
                return;
            }

            string strVendorID = Request.Form["vendorID"];

            if (String.IsNullOrEmpty(strVendorID))
            {
                Response.Write("Please select vendor!");
                return;
            }

            long vendorID;

            if (!long.TryParse(strVendorID, out vendorID))
            {
                Response.Write("Please select vendor!");
                return;
            }

            string status = Request.Form["status"];

            if (String.IsNullOrEmpty(status))
            {
                Response.Write("Please select status!");
                return;
            }

            string typeasset = Request.Form["typeasset"];

            if (String.IsNullOrEmpty(typeasset))
            {
                Response.Write("Please select Asset Type!");
                return;
            }

            string strHolderID = Request.Form["holderID"];

            if (String.IsNullOrEmpty(strHolderID))
            {
                Response.Write("Please select vendor!");
                return;
            }

            long holderID;

            if (!long.TryParse(strHolderID, out holderID))
            {
                Response.Write("Please select vendor!");
                return;
            }


            var dbHelper = new DatabaseHelper();

            using (var conn = dbHelper.GetDatabaseConnection())

            {
                using (var command = dbHelper.GetSQLiteCommand(conn))
                {

                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO assets (Name,Description,RoomID,Barcode,Brand,Model,AcquiredDate,Price,VendorID,Status,CreatedBy,assetType,HolderID) VALUES " +
                                      "(@name,@description,@roomID,@barcode,@brand,@model,'" + acquiredDate + "',@price,@vendorID,@status,@createdBy,@typeasset,@holderID)";


                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("description", description);
                    command.Parameters.AddWithValue("roomID", roomID);
                    command.Parameters.AddWithValue("barcode", barcode);
                    command.Parameters.AddWithValue("brand", brand);
                    command.Parameters.AddWithValue("model", model);
                    command.Parameters.AddWithValue("price", price);
                    command.Parameters.AddWithValue("vendorID", vendorID);
                    command.Parameters.AddWithValue("status", status);
                    command.Parameters.AddWithValue("createdBy", UserProfile.userID);
                    command.Parameters.AddWithValue("typeasset", typeasset);
                    command.Parameters.AddWithValue("holderID", holderID);
                    command.ExecuteNonQuery();





                }
                conn.Close();
            }


            Response.Write("Success");

        }
    }
}
