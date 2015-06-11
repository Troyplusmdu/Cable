<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<br />
<br />
<br />
<table width="100%" id="tblWarning" runat="server" class="tblCenter alignCenter" style="border-top:1px Black" cellpadding="5" cellspacing="5">
  <tr>
	<td>
	    <div id="divWarning" runat="server" class="lblFont" style="border: solid 1 px black;font-weight:bold;color:Green; ">
		    Note : The application is currently configured to work Offline. You are not allowed to Modify Data Online until the system is switched back to Online. Please contact Administrator for further Information. Thank You!
		</div>
	</td>
  </tr>
 </table>
</asp:Content>

