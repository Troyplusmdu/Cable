<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="CustomerDetails.aspx.cs" Inherits="CustomerDetails" Title="Customer Details" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>
<script language="javascript" type="text/javascript">

function Validator()
{
    var txtMobile = document.getElementById('ctl00_cplhControlPanel_txtPhoneNo').value;
    
    if(txtMobile.length > 0)
    {
        if(txtMobile.length != 10)
        {
            alert("Customer Mobile Number should be minimum of 10 digits.");
            Page_IsValid = false;
            return window.event.returnValue = false;
        }
        
        if(txtMobile.charAt(0) == "0")
        {
            alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
            Page_IsValid = false;
            return window.event.returnValue = false;
        }
    }
    else
    {
        Page_IsValid = true;
    }
}

</script>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="4" align="left">
                <uc1:errorDisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="item alignLeft" colspan="4">
                * represents mandatory fields          
            </td>
        </tr>
    </table>
    <table style="width: 100%;text-align:left" align="left" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;">
       <tr style="width: 100%">
            <td class="SectionHeader" colspan="4">
                <span>Customer Details</span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Customer Name: *<asp:RequiredFieldValidator ID="rvCustName" runat="server" ControlToValidate="txtCustName"
                  EnableClientScript="false"  ErrorMessage="Customer Name is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                &nbsp;
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtCustName" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                Area: *
                <asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea" ErrorMessage="Area is Mandatory"
                    Operator="GreaterThan" EnableClientScript="false" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td  style="width: 25%">
                <table cellpadding="0" cellspacing="0" style="width:100%; text-align:left">
                    <tr>
                        <td style="width:88%">
                            <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" Width="100%" AutoPostBack="true"
                                DataSourceID="srcArea" DataTextField="area" SkinID="skinDdlBox"
                                DataValueField="area" onselectedindexchanged="ddArea_SelectedIndexChanged">
                                <asp:ListItem Text=" -- Select Area -- " Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:12%">
                            <asp:ImageButton ID="imgBtnHist" runat="server" ImageUrl="~/App_Themes/DefaultTheme/Images/UserUnApproved.gif" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Cust Code :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 25%">
                Category: *
                <asp:CompareValidator ID="cvCategory" runat="server" ControlToValidate="ddCategory"
                    ErrorMessage="Category is Mandatory" EnableClientScript="false" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddCategory" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True" Width="100%">
                    <asp:ListItem Value="0"> -- Select Category -- </asp:ListItem>
                    <asp:ListItem>DC</asp:ListItem>
                    <asp:ListItem>RC</asp:ListItem>
                    <asp:ListItem>NC</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Door No:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDoorNo"
                  EnableClientScript="false"  ErrorMessage="Door No is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDoorNo" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                Address 1: *
                <asp:RequiredFieldValidator ID="rvAdd1" runat="server" ControlToValidate="txtAdd1"
                    EnableClientScript="false" ErrorMessage="Address 1 is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtAdd1" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Address 2:
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtAdd2" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                Place:
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtPlace" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Phone No:
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtPhoneNo" MaxLength="10" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                Effective Date: *
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEffDate"
                  EnableClientScript="false"  ErrorMessage="Effective Date is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%; vertical-align:top">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtEffDate" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                             <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$txtEffDate'});</script>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Installation Charge:
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtInstCrge" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                Monthly Charge: *<asp:RequiredFieldValidator ID="rvMnthlyCrge" runat="server" ControlToValidate="txtMnthCrge"
                    EnableClientScript="false" ErrorMessage="Monthly Charge is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtMnthCrge" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                Prevalied:
            </td>
            <td class="lmsrightcolumncolor alignLeft" style="width: 5%">
                <asp:CheckBox ID="chkPrev" runat="server" CssClass="CheckBox" />
            </td>
            <td style="width: 25%">
                Balance :
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtBalance" runat="server" SkinID="skinTxtBox" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
			<td style="width: 25%">
				Asset : *
				<asp:CompareValidator ID="cvAsset" runat="server" ControlToValidate="ddAsset"
                    ErrorMessage="Asset is Mandatory" EnableClientScript="false" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
			</td>
			<td style="width: 25%">
				<asp:DropDownList ID="ddAsset" runat="server" AppendDataBoundItems="True" Width="100%"
                    SkinID="skinDdlBox" AutoPostBack="false">
                    <asp:ListItem Value="0"> -- Select Asset -- </asp:ListItem>
                </asp:DropDownList>
			</td>
			<td style="width: 25%">
				
			</td>
			<td style="width: 25%">
				
			</td>
        </tr>
        <tr style="width: 100%">
            <td colspan="4" style="width: 25%; height:16px">
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                <asp:Button ID="btnCashHistory" runat="server" Text="View Cash History" CssClass="Button" CausesValidation="false" />
            </td>
            <td style="width: 25%" align="center">
                &nbsp;
            </td>
            <td style="width: 25%" align="center">
                &nbsp;
            </td>
            <td style="width: 25%" align="right">
                <asp:Button ID="lnkBtncancel" runat="server" SkinID="skinBtnCancel" CausesValidation="false" OnClick="lnkBtncancel_Click"/>
                <asp:Button ID="lnkBtnSave" runat="server" SkinID="skinBtnSave" OnClick="lnkBtnSave_Click" OnClientClick="Validator();" />
            </td>
        </tr>
        </table>
        <table visible="true" style="width: 100%;">
          <tr style="width: 100%">
            <td style="width: 25%; text-align:left">
            </td>
            <td align="center" style="width: 25%">
				<asp:SqlDataSource ID="srcAsset" runat="server"
                    SelectCommand="SELECT M.AssetCode,M.AssetDesc,D.AssetStatus,D.AssetLocation,D.AssetArea,D.SerialNo,D.AssetNo FROM AssetMaster M Inner Join AssetDetails D on M.AssetCode = D.AssetCode Where D.AssetStatus <> 'Scrapped' "  ProviderName="System.Data.OleDb">
                </asp:SqlDataSource>
            </td>
            <td align="center" style="width: 25%">
                <asp:SqlDataSource ID="srcArea" runat="server"
                    SelectCommand="SELECT [area] FROM [AreaMaster]" ProviderName="System.Data.OleDb">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

