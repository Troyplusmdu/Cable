﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayBookReport.aspx.cs" Inherits="DayBookReport" %>

 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Day Book Report</title>
  <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />  
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
    <div>
    
    </div> 
    <br />
    <div>
    <table cellpadding ="2" cellspacing="4" width="70%" border="0" >
    <tr>
    <td colspan="3"><h3>Day Book Report</h3></td>
    </tr>
    <tr>
        <td align="right" width="20%">
           Ledger Name :
        </td>
        <td align="left" width="40%">
           <asp:DropDownList ID="ddLedgers" runat="server" AutoPostBack="False" DataSourceID="srcCreditorDebitor"
                DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true">
                <asp:ListItem Text=" -- All -- " Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="left" width="40%">
            <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorJ"
                TypeName="BusinessLogic">
                <SelectParameters>
                    <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>            
        </td>        
    </tr>
  <tr>
    <td align="right" width="20%">
      Start Date :
    </td>
    <td align="left" width="40%">
      <asp:TextBox ID="txtStartDate"  Runat="server" CssClass="txtStyle" Width="100px" MaxLength="10" />
       <script type="text/javascript"  language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtStartDate'});</script>
      <%-- <a href="javascript:NewCal('txtStartDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
  
    </td>
    <td  align="left" width="40%">
      
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtStartDate" Display="None" 
            ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td align="right">
      End Date :
    </td>
    <td align="left">
      <asp:TextBox ID="txtEndDate" Runat="server" CssClass="txtStyle" Width="100px" MaxLength="10"/>
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
        <td colspan="2" align="left"> 
            <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" CssClass="btnStyle" onclick="btnReport_Click" 
            Text="Day Book Report" />
            <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" 
                ShowSummary="false" />
        </td>
  </tr>
</table>
    </div>
           <asp:HiddenField ID="hdFilename" runat="server" />
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" /> 
    </form>
</body>
</html>