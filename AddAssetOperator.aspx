<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAssetOperator.aspx.cs" Inherits="Asset_Tracking_System.AddAssetOperator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PPSKOMP AMS</title>
     <link href="mainadmin.css" rel="stylesheet" />
    
    
    <style type="text/css">
        .auto-style1 {
            height: 730px;
        }
    </style>
</head>
<body>
    <script type="text/javascript">

        
        function Save()
        {
            var name = document.getElementById('name').value
            var description = document.getElementById('description').value

            var roomEl = document.getElementById('room')
            var room = roomEl.options[roomEl.selectedIndex].value

            var barcode = document.getElementById('barcode').value
            var brand = document.getElementById('brand').value
            var model = document.getElementById('model').value
            var acquiredDate = document.getElementById('acquiredDate').value
            var price = document.getElementById('price').value

            var vendorEl = document.getElementById('vendor')
            var vendorID = vendorEl.options[vendorEl.selectedIndex].value

            var statusEl = document.getElementById('status')
            var status = statusEl.options[statusEl.selectedIndex].value

            var typeassetE1 = document.getElementById('typeasset')
            var typeasset = typeassetE1.options[typeassetE1.selectedIndex].value

            var holderEl = document.getElementById('holder')
            var holderID = holderEl.options[holderEl.selectedIndex].value

            if (window.XMLHttpRequest) {
                // code for modern browsers
                xmlhttp = new XMLHttpRequest()
            }
            else {
                // code for old IE browsers
                xmlhttp = new ActiveXObject('Microsoft.XMLHTTP')
            }

            xmlhttp.onreadystatechange = function()
            {
                if (this.readyState == 4 && this.status == 200)
                {
                    if (this.responseText === 'Success')
                    {
                        alert('Asset added successfully to database!')
                        location.href = 'ManageAsset.aspx'
                    }
                    else
                        alert(this.responseText)
                }
            }

            xmlhttp.open('POST', 'ajaxPerformAddAssetOperator.aspx', true)
            xmlhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')
            xmlhttp.send('name=' + name + '&description=' + description + "&room=" + room + "&barcode=" + barcode + "&brand=" + brand +
                '&model=' + model + '&acquiredDate=' + acquiredDate + '&price=' + price + '&vendorID=' + vendorID + '&status=' + status + '&typeasset=' + typeasset + '&holderID=' + holderID)

            return false
        }
    </script>
    <form id="frmAddAsset" onsubmit="return Save()">
        <div>
            <nav>

                <ul>
                    <li><a href="#">Operations</a>
                        <ul>
                             <li><a href="ManageAsset.aspx">Manage Asset</a></li>
                             <li><a href="ManageRoomInformation.aspx">Manage Location</a></li>
                             <li><a href="ManageVendorInformation.aspx">Manage Vendors</a></li>
                             <li><a href="ManageUsers.aspx">Manage Users</a></li>
                             <li><a href="ManageHolderInformation.aspx">Manage Asset Holders</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Reports</a>
                        <ul>
                             <li><a href="ViewAsset.aspx">View Full Asset Details</a></li>
                             <li><a href="Reports.aspx">Reports</a></li>
                             <li><a href="financeSummary.aspx">Finance Summary</a></li>
                             <li><a href="checkinout.aspx">Check-In/Check-Out</a></li>
                             
                        </ul>
                    </li>
                    <li><a href="Archive.aspx">Archive</a></li>
                    <li><a href="#">Logout</a></li>
                </ul>
            </nav>
            </div>
        <div class="container">
            <center>
                <table width="100%">
                    <tr>
                        <td width="20%">
                            <br />
                            <br />
                            <br />
                            <br />
                            <input type="button" value="Cancel" style="width:150px" onclick="location.href='ManageAssetForOperator.aspx'" />
                        </td>
               
                        <td width="60%"><h2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </h2>
                            <h2>&nbsp;</h2>
                            <h2>&nbsp;&nbsp;&nbsp;&nbsp; Add A Brand New Asset</h2></td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                </table><br />
                 <div class="wrapper">
                <div class="form">
                <table width="50%" class="auto-style1">
                    <tr>
                        <th colspan="2">Asset Information</th>
                    </tr>
                    <form action="#">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
              
                    <tr>
               
                        <div class ="input_field">
                        <td width="40%">Name:</td>
                        <td width="60%"><input type="text" id="name" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Description:</td>
                        <td><input type="text" id="description" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Room:</td>
                            <div class="custom_select">
                        <td><select id="room" style="width:100%" required="required">
                            <%=Asset_Tracking_System.AddAssetOperator.GenerateRoomOptions()%>
                            </select></td>
                                </div>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Barcode:</td>
                        <td><input type="number" id="barcode" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Brand:</td>
                        <td><input type="text" id="brand" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Model:</td>
                        <td><input type="text" id="model" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Acquired Date:</td>
                        <td><input type="date" id="acquiredDate" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Price:</td>
                        <td><input type="number" min="0.01" step="any" id="price" style="width:100%" required="required" /></td>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <div class =" input_field">
                        <td>Vendor:</td>
                             <div class="custom_select">
                        <td><select id="vendor" style="width:100%" required="required">
                            <%=Asset_Tracking_System.AddAssetOperator.GenerateVendorOptions()%>
                            </select></td>
                                 </div>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>

                    <tr>
                        <div class =" input_field">
                        <td>Status:</td>
                             <div class="custom_select">
                        <td><select id="status" style="width:100%" required="required">
                            <option value="Active">Active</option>
                            <option value="Inactive">Inactive</option>
                            </select></td>
                                 </div>
                            </div>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <div class =" input_field">
                     <td>Asset Type:</td>
                         <div class="custom_select">
                        <td><select id="typeasset" style="width:100%" required="required">
                            <option value="ICT">ICT</option>
                            <option value="Physical">Other Physical Assets</option>
                            </select></td>
                             </div>
                        </div>
                         <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                        <div class ="input_field">
                         <td>Asset Holder:</td>
                             <div class="custom_select">
                        <td><select id="holder" style="width:100%" required="required">
                            <%=Asset_Tracking_System.AddAssetOperator.GenerateHolderOptions()%>
                            </select></td>
                                 </div>
                            </div>
                        <div class="inputfield">
                        <td colspan="2"><center><input type="submit" value="Save" style="width:150px" /></center></td>
                            </div>
                        </form>
                        </div>
                        </div>
                    
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>