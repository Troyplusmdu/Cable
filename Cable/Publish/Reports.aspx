<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" Title="Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <br />
    <br />
    <table align="center"  width="50%" style="border:1px solid black; margin:0 0 0 50px" cellpadding="5"  cellspacing="5">
    <tr>
        <td class="SectionHeader">Customer Reports</td>
    </tr>
     <tr align="center" >
        <td>
			<asp:Button ID="lnkCustDetails" runat="server" Text="Due List Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>    
     </tr>
     <tr align="center">
        <td>
			<asp:Button ID="lnkCashDetails" runat="server" Text="Cash Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>        
     </tr>
     <tr align="center">
        <td>
			<asp:Button ID="lnkBillDetail" runat="server" Text="Bill Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>            
        </td>        
     </tr>
     <tr align="center">
        <td>
			<asp:Button ID="lnkOutStndBal" runat="server" Text="Cash Outstanding Analysis" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>        
     </tr>
     <tr align="center">
        <td>
			<asp:Button ID="lnkAdjstReport" runat="server" Text="Adjustments Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>        
     </tr>
	 <tr align="center" style="display:none">
        <td>
			<asp:Button ID="lnkBnkStmtReport" runat="server" Text="Bank Statement" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>
     </tr>
      <tr style="display:none" align="center">
        <td>
			<asp:Button ID="lnkLedgerReport" runat="server" Text="Ledger Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>
     </tr>
      <tr style="display:none" align="center">
        <td>
			<asp:Button ID="lnkDayBookReport" runat="server" Text="Day Book Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>
     </tr>   
      <tr align="center">
        <td>
			<asp:Button ID="lnkAssDetReport" runat="server" Text="Asset Details Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
        </td>
     </tr>   
      <tr align="center">
        <td>
			<asp:Button ID="lnkBusTransReport" runat="server" Text="Business Transaction Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr>  
	 <tr align="center">
        <td>
			<asp:Button ID="lnkFraudReport" runat="server" Text="Fraud Check Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr> 
      <tr align="center">
        <td>
			<asp:Button ID="lnkBookEntryReport" runat="server" Text="Book Entry Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr> 
     <tr align="center" style="display:none">
        <td>
			<asp:Button ID="lnkBtnTrailBal" runat="server" Text="Trial Balance Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr>   
     <tr align="center" style="display:none">
        <td>
			<asp:Button ID="lnkBtnBalSheet" runat="server" Text="Balance Sheet Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr>     
     <tr align="center" style="display:none">
        <td>
			<asp:Button ID="lnkBtnProfitLoss" runat="server" Text="Profit Loss Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr>     
     <tr align="center">
        <td>
			<asp:Button ID="lnkMonthlyReport" runat="server" Text="Month Comparison Report" Width="90%" Font-Bold="false" SkinID="skinButtonCol"/>
		</td>
     </tr>     
   </table>
   <br />
   <br />
</asp:Content>
