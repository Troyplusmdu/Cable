<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutstandingBalance.aspx.cs"
    Inherits="OutstandingBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Outstanding Balance Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript"  language="JavaScript">
    function unl()
    {
    
    document.form1.submit();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table cellpadding="2" style="border: solid 1px Silver; text-align: left" cellspacing="4" width="90%" border="0">
            <tr>
                <td colspan="4" class="SectionHeader" >
                        Outstanding Balance Report
                </td>
            </tr>
            <tr>
            <td >Month</td>
            <td ><asp:DropDownList ID="drpMonth" runat="server">
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            <asp:ListItem Value="5">5</asp:ListItem>
            <asp:ListItem Value="6">6</asp:ListItem>
            <asp:ListItem Value="7">7</asp:ListItem>
            <asp:ListItem Value="8">8</asp:ListItem>
            <asp:ListItem Value="9">9</asp:ListItem>
            <asp:ListItem Value="10">10</asp:ListItem>
            <asp:ListItem Value="11">11</asp:ListItem>
            <asp:ListItem Value="12">12</asp:ListItem>
            </asp:DropDownList></td>
            <td>Year</td>
            <td>
            <asp:DropDownList ID="drpYear" runat="server">
            <asp:ListItem Value="2015">2015</asp:ListItem>
            <asp:ListItem Value="2014">2014</asp:ListItem>
            <asp:ListItem Value="2013">2013</asp:ListItem>
            <asp:ListItem Value="2012">2012</asp:ListItem>
            <asp:ListItem Value="2011">2011</asp:ListItem>
            <asp:ListItem Value="2010">2010</asp:ListItem>
            <asp:ListItem Value="2009">2009</asp:ListItem>
            <asp:ListItem Value="2008">2008</asp:ListItem>
            <asp:ListItem Value="2007">2007</asp:ListItem>
            <asp:ListItem Value="2006">2006</asp:ListItem>
            <asp:ListItem Value="2005">2005</asp:ListItem>
            <asp:ListItem Value="2004">2004</asp:ListItem>
            <asp:ListItem Value="2003">2003</asp:ListItem>
            <asp:ListItem Value="2002">2002</asp:ListItem>
            <asp:ListItem Value="2001">2001</asp:ListItem>
            </asp:DropDownList> 
            </td>
            </tr>
            <tr>
                <td width="20%">
                    Area:
                </td>
                <td width="20%">
                    <asp:DropDownList ID="drpArea" runat="server" SkinID="skinDdlBox" DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                        <asp:ListItem Value="0"> -- All Areas -- </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" width="40%">
                    <asp:Button ID="btnPrint" runat="server" CssClass="Button" OnClick="btnPrint_Click" Text="Print Report"/>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
     <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
    <asp:SqlDataSource ID="AccessDataSource2" runat="server" ProviderName="System.Data.OleDb"
        SelectCommand="SELECT DISTINCT [category] FROM [CustomerMaster]"></asp:SqlDataSource>
         <asp:HiddenField ID="hdFilename" runat="server" />
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    </form>
</body>
</html>

