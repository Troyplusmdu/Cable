<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="UpdateTransactions.aspx.cs" Inherits="UpdateTransactions" Title="Update Transactions" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <br />
    <table align="center"  width="70%" style="border:1px solid black; margin:0 0 0 50px" cellpadding="5"  cellspacing="5">
    <tr>
        <td class="SectionHeader">Update Transactions</td>
    </tr>
     <tr align="center" >
        <td>
            <asp:Button ID="btnRecTrans" runat="server" Text="Update All Transactions"  Width="70%" Font-Bold="false" SkinID="skinButtonCol" onclick="btnRecTrans_Click" />
        </td>    
     </tr>
     <tr>
        <td>
            <table width="100%">
              <tr>
                 <td align="left">
                    <uc1:errorDisplay ID="errorDisplay" runat="server" />
                 </td>
              </tr>
            </table>
        </td>
    </tr>
   </table>
</asp:Content>

