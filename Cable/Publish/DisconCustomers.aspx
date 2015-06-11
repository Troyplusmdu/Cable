<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisconCustomers.aspx.cs" Title="Disconnected Customers"  Inherits="DisconCustomers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Disconnected Customers</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" style="width: 100%;" align="center">
    <tr>
         <td class="SectionHeader">Disconnected Customers
         </td>
    </tr>
        <tr style="width:100%">
        <td>
          <asp:GridView  id="grdCash" runat="server" Width="100%" PageSize="100" GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" 
                DataSourceID="ObjectDataSource1" 
                EmptyDataText="Customer not found." 
                OnRowCommand="grdCash_RowCommand" OnRowCreated="grdCash_RowCreated" 
                OnRowDataBound="grdCash_RowDataBound">
				<Columns>
				    <asp:BoundField DataField="code" HeaderText="Code" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="name" HeaderText="Name" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="area" HeaderText="Area" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="doorno" HeaderText="Door No" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="category" HeaderText="Category" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="effectDate" HeaderText="Effective Date" DataFormatString="{0:dd/MM/yyyy}" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				</Columns>
				<PagerTemplate>Goto Page 
                <asp:DropDownList ID="ddlPageSelector" SkinID="dropDownPage" runat="server" AutoPostBack="true">
                </asp:DropDownList>
                <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                    ID="btnFirst" />
                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                    ID="btnPrevious" />
                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                    ID="btnNext" />
                <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                    ID="btnLast" />
                </PagerTemplate> 			
			</asp:GridView>
			
        </td>
    </tr>
    </table>    
    </div>
     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetDisconCustomers" TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Area" QueryStringField="Area" Type="String" />
                        <asp:QueryStringParameter Name="Name" QueryStringField="Name" Type="String" />
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </SelectParameters>
     </asp:ObjectDataSource>
    </form>
</body>
</html>
