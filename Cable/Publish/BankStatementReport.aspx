<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankStatementReport.aspx.cs" Inherits="BankStatementReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Statements Report</title>
    <link rel="Stylesheet" href="StyleSheet.css" />
   <link rel="stylesheet" href="calendar.css" />
    <script type="text/javascript"  language="JavaScript" src="calendar_eu.js"></script>
    <script type="text/javascript"  language="JavaScript">
    function unl()
    {
   
    document.form1.submit();
    }
    </script>
    
     <script language="javascript" type="text/javascript" src="Scripts\calendar_eu.js"></script>
    
</head>
<body style="font-family:Verdana;font-size:12px; " onbeforeunload="unl()">
    <form id="form1" runat="server">
    
    <br />
    <div>
    
        <br />
        <table cellpadding ="2" cellspacing="4" width="70%" border="0">
        <tr>
        <td colspan="3"><h3>BANK STATEMENT REPORT</h3></td>
        </tr>
        <tr>
        <td >Bank Transaction:</td>
        <td colspan="2" align="left"><asp:DropDownList ID="drpBankName" runat="server" 
            DataTextField="LedgerName" 
            DataValueField="LedgerID">
        </asp:DropDownList></td>
        </tr>
          <tr>
    <td align="right" width="20%">
      Start Date:
    </td>
    <td align="left" width="30%">
      <asp:TextBox ID="txtStartDate" Width="100px" MaxLength="10" Runat="server"  CssClass="txtStyle" />
      <script type="text/javascript"  language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtStartDate'});</script>

       <%--<a href="javascript:NewCal('txtStartDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
    </td>
    <td  align="left" width="50%">
    
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtStartDate" Display="None" 
            ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td align="right">
      End Date:
    </td>
    <td align="left">
      <asp:TextBox ID="txtEndDate" Width="100px" MaxLength="10"  Runat="server" CssClass="txtStyle" />
        <script type="text/javascript" language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtEndDate'});</script>

    <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
    </td>
    <td  align="left">
    
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtEndDate" Display="None" 
            ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Display="None" 
            ErrorMessage="Start Date Should Be Less Than the End Date" 
            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
    </td>
  </tr>
        <tr>
        <td>&nbsp;</td>
        <td colspan="2" align="left"> <asp:Button ID="btnReport" SkinID="skinButtonBig"  runat="server" CssClass="btnStyle" onclick="btnReport_Click" 
            Text="Bank Statement Report" />
           </td>
        </tr>
        </table>
        
        
       
        <%--<asp:AccessDataSource ID="AccessDataSource1" runat="server"  
             
            SelectCommand="SELECT [LedgerName], [LedgerID] FROM [tblLedger] WHERE ([GroupID] = ?)">
            
            <SelectParameter >
                <asp:Parameter DefaultValue="3" Name="GroupID" Type="Int16" />
            </SelectParameters>
        </asp:AccessDataSource>--%>
        <asp:ValidationSummary id="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false"   />
        <br />
    </div>
    <div>
        

           <asp:HiddenField ID="hdFilename" runat="server" />
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    </div>
    </form>
</body>
</html>
