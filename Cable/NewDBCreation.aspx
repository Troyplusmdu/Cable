<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="NewDBCreation.aspx.cs" Inherits="NewDBCreation" Title="Create New Database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<div class="lblFont" style="border: solid 1 px black;background-color:aliceblue;font-weight:bold;color:Red; ">
Attention : This screen is used to create a New financial year account. new transacation will be created from now.this need to be used carefully need to be done by the approval of supervisor.
</div><br />
<asp:Label ID="lblMsg" runat="server" style="color:Red;font-weight:bold;" class="lblFont"></asp:Label>
<br />
 <asp:Button ID="btnAccount" SkinID="skinButtonBig" runat="server"  onclick="btnAccount_Click" 
            Text="Create New Account" />
</asp:Content>

