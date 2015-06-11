<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="AdjustmentEntry.aspx.cs" Inherits="AdjustmentEntry" Title="Adjustment Entries" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <br />
<script language="javascript" type="text/javascript">

function Validator()
{
    var txtMobile = document.getElementById('ctl00_cplhControlPanel_txtCustMobile').value;
    
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
<table width="100%">
   <tr>
      <td colspan="5" align="left">
        <uc1:errorDisplay ID="errorDisplay" runat="server" />
      </td>
   </tr>
    <tr style="width: 100%">
            <td class="item alignLeft" colspan="5">
                * represents mandatory fields          
            </td>
    </tr>
    </table>
  <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;">
    <tr style="width:100%">
        <td class="SectionHeader" colspan="5">
            <span>Adjustments
            </span>
        </td>
   </tr>
    <tr style="width:100%" >
        <td class="tblLeft" style="width:25%">
            Customer Code : *<asp:RequiredFieldValidator ID="rvCustCode" runat="server" ControlToValidate="txtCustCode"
                    ErrorMessage="Customer Code is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
        <td class="lmsrightcolumncolor" style="width:25%">
            <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox"></asp:TextBox></td>
        <td class="tblLeft" style="width:25%">
            Area : *<asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea"
                    ErrorMessage="Area is Mandatory" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
        <td style="width:25%; text-align:left">
            <asp:DropDownList ID="ddArea" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True" 
                DataSourceID="srcArea" DataTextField="area" DataValueField="area" 
                Width="100%">
                <asp:ListItem Value="0">Please Select Area</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 25%">
           <asp:ImageButton ID="btnDetails" runat="server" Text="Find" CausesValidation="false"
             ImageUrl="~/App_Themes/DefaultTheme/Images/Icon_View.gif" OnClick="btnDetails_Click" />
        </td>
    </tr>
    <tr style="width: 100%">
        <td class="tblLeft" style="width: 25%">
            Customer Name :
        </td>
        <td style="width: 25%">
            <asp:TextBox ID="txtCustName" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
        </td>
        <td class="tblLeft" style="width: 25%">
            Balance :
        </td>
        <td style="width: 25%; text-align:left">
            <asp:TextBox ID="txtCustBalance" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
        </td>
        <td style="width: 25%">
        </td>
    </tr>
    <tr style="width:100%" >
        <td class="tblLeft" style="width:25%">
            Amount : *<asp:RequiredFieldValidator ID="rvAmnt" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Amount is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
        <td style="width:25%">
            <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
        </td>
        <td class="tblLeft" style="width:25%">
            Date Paid:</td>
        <td  style="width:25%; vertical-align:middle; text-align:left">
            <asp:TextBox ID="txtDateEntered" runat="server" CssClass="cssTextBox" Width="120px"></asp:TextBox>
            <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtDateEntered')});</script>
        </td>
        <td style="width: 25%">
        </td>
    </tr>
    <tr style="width:100%" >
        <td class="tblLeft" style="width:25%">
            Reason : *<asp:CompareValidator ID="cvReason" runat="server" ControlToValidate="ddReason"
                    ErrorMessage="Please select the reason for the discount provided." 
                    Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
        <td style="width:25%">
            <asp:DropDownList ID="ddReason" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True" 
                DataSourceID="srcReason" DataTextField="reason" DataValueField="reason" 
                Width="100%">
                <asp:ListItem Value="0">Please Select Reason</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="tblLeft" style="width:25%">
            Customer Mobile:
        </td>
        <td style="width:25%; text-align:left">
            <asp:TextBox ID="txtCustMobile" runat="server" SkinID="skinTxtBox" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 25%">
        </td>
    </tr>  
    <tr style="width:100%">
        <td colspan="5" style="height: 16px">
        </td>
    </tr>
    <tr style="width:100%">
       <td style="width:25%">
           &nbsp;</td>
        <td style="width: 25%" align="center">
            &nbsp;</td>
       <td style="width:25%" align="right" colspan="2">
            <asp:Button ID="lnkBtncancel" CausesValidation="false" runat="server" SkinID="skinBtnCancel" onclick="lnkBtncancel_Click" />
             <asp:Button ID="lnkBtnSave" runat="server" SkinID="skinBtnSave" OnClick="lnkBtnSave_Click" OnClientClick="Validator();" />
       </td>
       <td style="width: 25%">
       </td>
   </tr>
</table>
<asp:SqlDataSource ID="srcArea" runat="server" 
                SelectCommand="SELECT [area] FROM [AreaMaster]"
                ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            <asp:SqlDataSource ID="srcReason" runat="server" 
                SelectCommand="SELECT [reason] FROM [AdjustmentReason]" EnableCaching="True" 
                ProviderName="System.Data.OleDb"></asp:SqlDataSource>               
</asp:Content>

