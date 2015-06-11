<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashHistory.aspx.cs" Inherits="CashHistory" Title="Cash History" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cash History</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:5 5 5 5">
        <table cellpadding="0" cellspacing="0" style="width: 100%;" align="center">
    <tr>
         <td class="SectionHeader">Cash History
         </td>
    </tr>
        <tr style="width:100%">
        <td>
          <asp:GridView  id="grdCash" runat="server" Width="100%" DataKeyNames="slno" GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" 
           DataSourceID="srcCashHistory" EmptyDataText="No Cash details found for this Customer" OnPreRender="grdCash_PreRender" OnRowCommand="grdCash_RowCommand" OnRowCreated="grdCash_RowCreated" OnRowDataBound="grdCash_RowDataBound">
				<Columns>
				    <asp:BoundField DataField="slno" HeaderText="Sl No"><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="billno" HeaderText="Bill No." ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="amount" HeaderText="Amount" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="discount" HeaderText="Discount" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="reason" HeaderText="Reason" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="date_paid" HeaderText="Date Paid" DataFormatString="{0:dd/MM/yyyy}" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="date_entered" HeaderText="Date Entered" DataFormatString="{0:dd/MM/yyyy}" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
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
        <tr height="10%" width="100%">
            <td style="width: 100%" align="left">
               </td>
        </tr>
        <tr height="10%" width="100%">
            <td align="left" style="width: 100%">
                <asp:ObjectDataSource ID="srcCashHistory" runat="server" 
                    SelectMethod="GetCashHistory" TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:QueryStringParameter Name="Code" QueryStringField="code" Type="String" />
                        <asp:QueryStringParameter Name="Area" QueryStringField="area" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr height="10%" width="100%"> 
            <td style="width: 100%" align="left">
            </td>
        </tr> 
        <tr width="100%">
            <td align="left" style="width: 100%">
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="SectionHeader">
            <span>Adjustment History</span>
            </td>
        </tr>
        <tr style="width:100%">
        <td>
          <asp:GridView  id="grdAdjustment" runat="server" Width="100%"
                GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" 
				DataSourceID="srcAdjustments" 
                EmptyDataText="No Adjustments found for this Customer" 
                OnRowCommand="grdAdjustment_RowCommand" OnRowCreated="grdAdjustment_RowCreated" 
                OnRowDataBound="grdAdjustment_RowDataBound">
				<Columns>
				    <asp:BoundField DataField="amount" HeaderText="Amount" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="reason" HeaderText="Reason" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
				    <asp:BoundField DataField="date_entered" HeaderText="Date Entered" DataFormatString="{0:dd/MM/yyyy}" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
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
        <tr height="10%" width="100%">
            <td style="width: 100%" align="left">
               </td>
        </tr>
        <tr height="10%" width="100%">
            <td align="left" style="width: 100%">
                <asp:ObjectDataSource ID="srcAdjustments" runat="server" 
                    SelectMethod="GetAdjustHistory" TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:QueryStringParameter Name="Code" QueryStringField="code" Type="String" />
                        <asp:QueryStringParameter Name="Area" QueryStringField="area" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr height="10%" width="100%"> 
            <td style="width: 100%" align="left">
            </td>
        </tr> 
        <tr width="100%">
            <td align="left" style="width: 100%">
                
            </td>
        </tr>
    </table> 
    </div>
    </form>
    </body>
</html>

