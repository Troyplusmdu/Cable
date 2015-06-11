<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileReports.aspx.cs" Inherits="MobileReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
<br />
<br />
    <table  width="99%" style="border:1px solid black; margin:0 0 0 0px" cellpadding="5"  cellspacing="5">
     <tr align="center" >
        <td>
			<asp:Button ID="lnkCustDetails" Height="30px" runat="server" Font-Bold="true" Text="Due List Report" Font-Size="Medium" Width="90%" SkinID="skinButtonCol"/>
        </td>    
     </tr>
     <tr align="center">
        <td>
			<asp:Button ID="lnkMonthlyReport" Height="30px" runat="server" Font-Bold="true" Text="Month Comparison Report" Width="90%" Font-Size="Medium" SkinID="skinButtonCol"/>
		</td>
     </tr>          
     <tr align="center">
        <td>
			<asp:Button ID="lnkBtnBack" Height="30px" runat="server" Text="Go Back" Width="90%" Font-Size="Medium"
                Font-Bold="false" SkinID="skinButtonCol" onclick="lnkBtnBack_Click"/>
		</td>
     </tr>               
     </table>
    </div>
    </form>
</body>
</html>
