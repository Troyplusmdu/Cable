<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="CompanyInfo.aspx.cs" Inherits="CompanyInfo" Title="Company Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <div style="text-align: left">
                    <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 0px solid #5078B3;">
                        <tr style="width: 100%; text-align:center">
                            <td class="SectionHeader" colspan="4">
                                <span>Company Information</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                COMPANY
                                <asp:RequiredFieldValidator ValidationGroup="Save" ID="rqCompany" ForeColor="Red"
                                    Font-Bold="true" runat="server" Text="*" ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" colspan="2">
                                EMAIL &nbsp;<asp:RegularExpressionValidator ID="rgEmail" runat="server" ErrorMessage="Wrong Email Format"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Save"
                                    ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:TextBox ID="txtCompanyName" runat="server" Width="300px" CssClass="cssTextBox"></asp:TextBox>
                            </td>
                            <td align="left" colspan="2" valign="top">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="cssTextBox" Width="275px" Height="16px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                ADDRESS
                            </td>
                            <td align="left">
                                CITY
                            </td>
                            <td align="left">
                                STATE
                            </td>
                            <td align="left">
                                PINCODE
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:TextBox TextMode="MultiLine" ID="txtAddress" runat="server" CssClass="cssTextBox"
                                    Width="125px" Height="30px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtState" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtPincode" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>                            
                        </tr>
                        <tr>
                            <td align="left">
                                TIN NO
                            </td>
                            <td align="left">
                                CST NO
                            </td>
                            <td align="left">
                                FAX
                            </td>
                            <td align="left">
                                PHONE
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:TextBox ID="txtTin" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCST" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFAX" runat="server" CssClass="cssTextBox" Width="125px" Height="16px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            </td>
                            <td align="left" valign="top">
                                
                            </td>
                            <td align="left" valign="top">
                                
                            </td>
                            <td align="left" valign="top">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                               
                            </td>
                            <td align="left">
                                
                            </td>
                            <td align="left">
                            </td>
                            <td align="left">
                            </td>
                        </tr>                           
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" Text="Save" SkinID="skinBtnSave"
                                    OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
</asp:Content>

