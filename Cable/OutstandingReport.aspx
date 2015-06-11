<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutstandingReport.aspx.cs" Inherits="OutstandingReport" %>

 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Outstanding Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />  
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />  
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
  
   
    <script type="text/javascript"  language="JavaScript">
    function unl()
    {
   
    document.form1.submit();
    }
    </script>
   
</head>
<body style="font-family:Verdana;font-size:12px; " onbeforeunload="unl()">
    <form id="form1" runat="server">
 <br />
    <br />
    <div>
    <table width="40%">
    <tr>
    <td colspan="3"><h3>Outstanding Report</h3></td>
    </tr>
 
  <tr>
  <td>Ledger name</td>
  <td colspan="2">
      <asp:DropDownList ID="drpLedgerName" runat="server" Width="200px" 
           DataTextField="GroupName" 
          DataValueField="GroupID"> </asp:DropDownList></td>
  </tr>
  <tr>
        <td colspan="3"> 
            <br />
            <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" CssClass="btnStyle" onclick="btnReport_Click" 
            Text="Oustanding Report" />
            <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" 
                ShowSummary="false" />
            
        </td>
  </tr>
</table>
    </div>
    <div>
        

           <asp:HiddenField ID="hdFilename" runat="server" />
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    </div>
    </form>
   </body>
</html>
