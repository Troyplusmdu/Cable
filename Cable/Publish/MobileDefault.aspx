<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileDefault.aspx.cs" Inherits="MobileDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile Home</title>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <br />
    <br />
    <table align="center" width="98%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
        cellpadding="5" cellspacing="5">
        <tr align="center">
            <td>
                <asp:Button ID="BtnCashEntry" runat="server" Font-Size="Medium" Text="CASH ENTRY" 
                    Width="100%" Height="30px"
                    Font-Bold="true" SkinID="skinButtonCol" onclick="BtnCashEntry_Click"/>
            </td>
        </tr>    
        <tr align="center">
            <td>
                <asp:Button ID="BtnCustomerMaster" runat="server" Font-Size="Medium" Text="CUSTOMER MASTER" 
                    Width="100%" Height="30px"
                    Font-Bold="true" SkinID="skinButtonCol" onclick="BtnCustomerMaster_Click"/>
            </td>
        </tr>            
        <tr align="center">
            <td>
                <asp:Button ID="BtnReports" runat="server" Font-Size="Medium" Text="REPORTS"
                    Width="100%" Height="30px" Font-Bold="true" SkinID="skinButtonCol" 
                    onclick="BtnReports_Click"/>
            </td>
        </tr>                                    
        <tr align="center">
            <td>
                <asp:Button ID="lnkLogout" runat="server" Font-Size="Medium" Font-Bold="false" Text="LOGOUT" Width="100%" Height="30px"
                    SkinID="skinButtonCol" onclick="lnkBtnLogout_Click" />
            </td>
        </tr>            
     </table>    
    </div>
    </form>
</body>
</html>
