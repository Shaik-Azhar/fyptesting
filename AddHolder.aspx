﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHolder.aspx.cs" Inherits="Asset_Tracking_System.AddHolder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>PPSKOMP AMS</title>
    <link href="mainadmin.css" rel="stylesheet" />
</head> 
<body>
    <script type="text/javascript">
        function Save()
        {
            var holderName = document.getElementById('holderName').value
           

            if (window.XMLHttpRequest)
            {
                // code for modern browsers
                xmlhttp = new XMLHttpRequest()
            }
            else
            {
                // code for old IE browsers
                xmlhttp = new ActiveXObject('Microsoft.XMLHTTP')
            }

            xmlhttp.onreadystatechange = function()
            {
                if (this.readyState == 4 && this.status == 200)
                {
                    if (this.responseText === 'Success')
                    {
                        alert('Vendor added successfully to database!')
                        location.href = 'ManageHolderInformation.aspx'
                    }
                    else
                        alert(this.responseText)
                }
            }

            xmlhttp.open('POST', 'ajaxPerformAddHolder.aspx', true)
            xmlhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')
            xmlhttp.send('holderName=' + holderName)

            return false
        }
    </script>
    <form id="frmAddVendor" onsubmit="return Save()">
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
        <div>
            <center>
                <table width="100%">
                    <tr align="center">
                        <td width="20%">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <input type="button" value="Cancel" style="width:150px" onclick="location.href='ManageHolderInformation.aspx'" /></td>
                        <td width="60%"><h2>Add Holder</h2></td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                </table><br />
                <table width="50%">
                    <tr>
                        <th colspan="2">Holder Information</th>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td width="40%">Holder Name:</td>
                        <td width="60%"><input type="text" id="holderName" style="width:100%" required="required" /></td>
                    </tr>
                   
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2"><center><input type="submit" value="Save" style="width:150px" /></center></td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
