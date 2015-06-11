<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashDetailsReport.aspx.cs"
    Inherits="CashDetailsReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cash Details Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />

    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>

    <script type="text/javascript" language="JavaScript">
    function unl()
    {
   
    document.form1.submit();
    }
    </script>

    <%--<script language="javascript" type="text/javascript" src="datetimepicker.js"></script>--%>
</head>
<body onbeforeunload="unl()" style="font-family: Verdana; font-size: 12px;">
    <form id="form1" runat="server">
    <div>
        <br />
        <table cellpadding="2" style="border: solid 1px Silver; text-align: left" cellspacing="4" width="90%" border="0">
            <tr style="text-align: center">
                <td colspan="3" class="SectionHeader">
                    Cash Details Report
                </td>
            </tr>
            <tr>
                <td>
                    Area :
                </td>
                <td colspan="2" align="left">
                    <asp:DropDownList ID="drpArea" runat="server" SkinID="skinDdlBox" DataTextField="area"
                        DataValueField="area">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    Start Date :
                </td>
                <td align="left" width="40%">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="100px" CssClass="cssTextBox" MaxLength="10" />

                    <script type="text/javascript" language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtStartDate'});</script>

                </td>
                <td align="left" width="40%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="Dynamic" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    End Date :
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEndDate" runat="server" Width="100px" CssClass="cssTextBox" MaxLength="10" />

                    <script type="text/javascript" language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtEndDate'});</script>

                </td>
                <td align="left">
                    <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="Dynamic" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Cash Type :
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpCashType" runat="server" AppendDataBoundItems="True" SkinID="skinDdlBox">
                        <asp:ListItem Value="SUBSCRIPTION">SUBSCRIPTION</asp:ListItem>
                        <asp:ListItem Value="INSTALLATION">INSTALLATION</asp:ListItem>
                        <asp:ListItem Value="REINSTALLATION">REINSTALLATION</asp:ListItem>
                        <asp:ListItem Value="ALL"> -- ALL -- </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2" align="left">
                    <asp:Button ID="btnPrint" runat="server" CssClass="Button" Width="120px" OnClick="btnPrint_Click"
                        Text="Print Report" OnClientClick="javascript:CallPrint('divPrint')" />
                </td>
            </tr>
        </table>
        <%-- <asp:ValidationSummary id="valSummary" runat="server"  ShowMessageBox="true" ShowSummary="false"   />--%>
    </div>
    <asp:HiddenField ID="hdFilename" runat="server" />
    <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    </form>
</body>
</html>
