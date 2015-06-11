<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerReport.aspx.cs" Inherits="CustomerReport" Title="Customer Report" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
        <br />
        <table cellpadding="2" style="border: solid 1px Silver; text-align: left" cellspacing="4" width="90%" border="0">
            <tr style="text-align: center">
                <td colspan="4" class="SectionHeader">
                    Customer Report
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    Area : *
                    <asp:CompareValidator ID="cvArea" runat="server" EnableClientScript="false" ControlToValidate="ddArea"
                        ErrorMessage="Please select Area to Generate the report" Display="Dynamic" Operator="GreaterThan"
                        ValueToCompare="0">*</asp:CompareValidator>
                    &nbsp;
                </td>
                <td style="width: 25%">
                    <asp:DropDownList ID="ddArea" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True" Width="100%"
                        DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                        <asp:ListItem Value="0">--Please Select Area--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 25%">
                    Active Customer ? :
                </td>
                <td style="width: 25%; text-align: left">
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    Balance :
                </td>
                <td style="width: 25%" align="left" valign="top">
                    <asp:DropDownList ID="ddOper" runat="server" Style="height: 19px" CssClass="cssDropDown" Width="40px">
                        <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                        <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                        <asp:ListItem Value="=">=</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBalance" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 25%">
                    Order By :
                </td>
                <td style="width: 25%">
                    <asp:DropDownList ID="ddOrderBy" runat="server" SkinID="skinDdlBox" Style="height: 19px" Width="100%">
                        <asp:ListItem Value="name">Name</asp:ListItem>
                        <asp:ListItem Value="code">Code</asp:ListItem>
                        <asp:ListItem Value="doorno">Door No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="width: 100%">
                    <uc1:errordisplay id="errDisp" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td style="width: 25%" valign="middle">
                    <asp:Button ID="lnkBtnSearchId" runat="server" Text="Generate Report" CssClass="Button" ToolTip="Click here to generate report"
                        TabIndex="3" OnClick="lnkBtnSearchId_Click" />
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>                
            </tr>
            <tr>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td align="center" style="width: 25%" colspan="2" valign="middle">
                    <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster] Order by area"
                        ProviderName="System.Data.OleDb"></asp:SqlDataSource>
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

