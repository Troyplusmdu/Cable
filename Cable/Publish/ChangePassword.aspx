<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Change Password" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <br />
    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>
    <br />
    <table style="width: 50%;" align="center" style="border: 1px solid #5078B3">
        <tr>
            <td colspan="2" align="left">
                <uc1:errorDisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="SectionHeader" colspan="2">Change Your Password</td>
        </tr>
        <tr>
            <td class="tblLeft" style="width: 25%">
                Current Password : *
                <asp:RequiredFieldValidator ID="rvCurrPass" runat="server" ControlToValidate="txtCurrentPass"
                Display="Dynamic" EnableClientScript="False" ErrorMessage="Enter Current password">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCurrentPass" runat="server" TextMode="Password" MaxLength="15" SkinID="skinTxtBox" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tblLeft" style="width: 25%">
                New Password : *
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPass"
                Display="Dynamic" EnableClientScript="False" ErrorMessage="Enter New password">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" MaxLength="15" SkinID="skinTxtBox" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tblLeft" style="width: 25%">
                Confirm New Password : *
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirmPass"
                Display="Dynamic" EnableClientScript="False" ErrorMessage="Enter Confirm password">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="txtConfirmPass" ControlToCompare="txtNewPass"
                Display="Dynamic" Operator="Equal" EnableClientScript="False" ErrorMessage="New password and Confirm password dosent match.">*</asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" MaxLength="15" SkinID="skinTxtBox" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 23%" colspan="2" align="right">
                <asp:Button ID="lnkBtncancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="button" />&nbsp;
                <asp:Button ID="lnkBtnSave" runat="server" Text="Change Password" 
                    CssClass="button" onclick="lnkBtnSave_Click" />
            </td>
        </tr>
</table>
</asp:Content>

