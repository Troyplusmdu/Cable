<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ReconDate.aspx.cs" Inherits="ReconDate" Title="Change Recon Date" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<table width="60%" style="border: 0px solid #5078B3">
        <tr>
            <td colspan="4" align="left">
                <uc1:errorDisplay ID="errorDisplay" runat="server" />
                <br />
            </td>
        </tr>
</table>
<table width="60%" style="border: 1px solid #5078B3">
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="3">
                <span>Recon Date</span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="tblLeft" style="width: 25%">
                Date: *&nbsp;
                 <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtReconDate" Display="Dynamic" EnableClientScript="True">Date is mandatory</asp:RequiredFieldValidator>
                 <asp:CompareValidator ControlToValidate="txtReconDate" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 35%">
                <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left">
					<tr>
						<td style="width: 88%">
							<asp:TextBox ID="txtReconDate" runat="server" Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
						</td>
						<td style="width: 12%; text-align: center">
							<script type="text/javascript" language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtReconDate')});</script>
						</td>
					</tr>
				</table>
            </td>
            <td style="width: 25%">
               <asp:Button ID="lnkBtnUpdate" runat="server" Text="Update" Width="75px" 
                    SkinID="skinBtnSave" onclick="lnkBtnUpdate_Click" />
            </td>
        </tr>
  </table>
</asp:Content>

