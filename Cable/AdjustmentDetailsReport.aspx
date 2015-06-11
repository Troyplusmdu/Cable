<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjustmentDetailsReport.aspx.cs" Inherits="AdjustmentDetailsReport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjustments Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script type="text/javascript"  language="JavaScript" src="Scripts/calendar_eu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div style="background: white; margin: 20px 50px 20px 60px; text-align: center">
        <table style="border:solid 1px Silver" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td colspan="3" class="SectionHeader">
                    Adjustment Details Report
                </td>
            </tr>
            <tr>
                <td class="LMSLeftColumnColor" align="right" width="25%">
                    Area:
                </td>
                <td width="25%">
                    <asp:DropDownList ID="drpArea" runat="server" SkinID="skinDdlBox" DataSourceID="srcArea" DataTextField="area" DataValueField="area" Width="100%">
                        <asp:ListItem Value="0"> -- All Areas -- </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="25%">
                </td>
            </tr>
            <tr>
                <td class="LMSLeftColumnColor" width="25%">
                    Start Date:
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="100px" MaxLength="10"  />
                    <script type="text/javascript"  language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtStartDate'});</script>
                </td>
                <td align="left" width="25%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="LMSLeftColumnColor" width="25%">
                    End Date:
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtEndDate" runat="server" Width="100px" MaxLength="10"  />
                    <script type="text/javascript" language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtEndDate'});</script>
                </td>
                <td align="left" width="25%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnPrint" runat="server" SkinID="skinButtonBig" OnClick="btnPrint_Click"
                        Text="Print Report" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
    </div>
    <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
        ProviderName="System.Data.OleDb"></asp:SqlDataSource>
    </form>
</body>
</html>
