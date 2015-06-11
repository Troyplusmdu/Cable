<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="BookCashEntry.aspx.cs" Inherits="BookCashEntry" Title="Cash Entry" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<script language="javascript" src="Scripts/date.js" type="text/javascript"></script>
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

<script language="javascript" type="text/javascript">

function ConfirmCancel() 
{
    var txtAmount = GettxtBoxName("txtAmount");
    var Amount = document.getElementById(txtAmount).value;

    var txtCustBal = GettxtBoxName("txtCustBalance");
    var Balance = document.getElementById(txtCustBal).value;
   
    var hiddenVal = document.getElementById("ctl00_cplhControlPanel_hidRetVal").value;
    Balance = Balance - Amount;
    
    var entryType = document.getElementById("ctl00_cplhControlPanel_ddEntryType");
    var Val = entryType.options[entryType.selectedIndex].value
    
    if( Balance < 0 && Val == "CASH")
    {
        var rv = confirm("Balance will go Negative. Are you sure you want to continue with this Cash Entry?"); 
        
        if(rv == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    else
    {
        return true;
    }
}

</script>
       <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 0px solid #5078B3;">
        <tr>
            <td colspan="5" align="left">
                <uc1:errordisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="item alignLeft" colspan="5">
                * represents mandatory fields
            </td>
        </tr>
        </table>
       <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;">
        <tr style="width: 100%">
            <td class="SectionHeader" style="text-align:center" colspan="5">
                <span>Cash Details </span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td  style="width: 25%">
                Book : *
                <asp:CompareValidator ID="cvBook" runat="server" ControlToValidate="ddBook" Enabled ="false"
                    ErrorMessage="Book is Mandatory" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddBook" runat="server" AppendDataBoundItems="True"
                    SkinID="skinDdlBox" DataTextField="BookName" DataValueField="BookID" AutoPostBack="true"
                    Width="100%" OnSelectedIndexChanged="ddBook_SelectedIndexChanged">
                    <asp:ListItem Value="0"> -- Select Book -- </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td  style="width: 25%">
                Bill No :
                <asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo"
                    ErrorMessage="Bill No. is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtBillNo" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                &nbsp;
            </td>
        </tr>
        <tr style="width: 100%">
            <td  style="width: 25%">
                Customer Code : *<asp:RequiredFieldValidator ID="rvCustCode" runat="server" ControlToValidate="txtCustCode"
                    ErrorMessage="Customer Code is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td  style="width: 25%">
                Area : *<asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea"
                    ErrorMessage="Area is Mandatory" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" DataSourceID="srcArea"
                    SkinID="skinDdlBox" DataTextField="area" DataValueField="area" Width="100%">
                    <asp:ListItem Value="0"> -- Select Area -- </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 25%">
                <asp:ImageButton ID="btnDetails" runat="server" Text="Find" CausesValidation="false"
                    ImageUrl="~/App_Themes/DefaultTheme/Images/Icon_View.gif" OnClick="btnDetails_Click" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td  style="width: 25%">
                Customer Name :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustName" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td  style="width: 25%">
                Balance :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustBalance" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td >
                Cash Entry Type :
            </td>
            <td>
                <asp:DropDownList ID="ddEntryType" runat="server" OnSelectedIndexChanged="ddEntryType_SelectedIndexChanged"
                    Width="100%" AutoPostBack="true" SkinID="skinDdlBox">
                    <asp:ListItem Value="CASH">Subscription Cash</asp:ListItem>
                    <asp:ListItem Value="INST">Installtion Cash</asp:ListItem>
                    <asp:ListItem Value="REINST">Re-Installtion Cash</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td >
                Customer Mobile:
            </td>
            <td>
                <asp:TextBox ID="txtCustMobile" runat="server" SkinID="skinTxtBox" MaxLength="10"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr style="width: 100%">
            <td  style="width: 25%">
                Amount: *<asp:RequiredFieldValidator ID="rvAmnt" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Amount is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td  style="width: 25%">
                Date Paid(dd/MM/yyyy): *<asp:CompareValidator ControlToValidate="txtDatePaid" Operator="DataTypeCheck" Type="Date"
                    ErrorMessage="Date format should be dd/MM/yyyy" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
            </td>
            <td style="width: 25%; text-align:left">
                <asp:TextBox ID="txtDatePaid" runat="server" Width="100px" CssClass="cssTextBox"></asp:TextBox>
                <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtDatePaid')});</script>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%" runat="server" id="InstRow" visible="false">
            <td  style="width: 25%">
                Installation Cash: *
                <asp:RequiredFieldValidator ID="rvInstCash" runat="server" ErrorMessage="Installation Cash is Mandatory"
                    ControlToValidate="txtInstCash"></asp:RequiredFieldValidator>
            </td>
            <td  style="width: 25%">
                <asp:TextBox ID="txtInstCash" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                <asp:HiddenField ID="hidRetVal" Value="" runat="server" />
            </td>
            <td style="width: 25%">
            </td>
            <td style="width: 25%">
                &nbsp;
            </td>
            <td style="width: 25%">
                &nbsp;
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="2" style="height: 16px">
            
            </td>
            <td style="width: 25%" align="center">
                &nbsp;
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT BookID,BookName FROM tblBook Where BookStatus='Open' And FlagCollected = 'Y' Order By BookName ASC "
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td style="width: 50%" colspan="2" align="right">
                <asp:Button ID="lnkBtncancel" runat="server" CausesValidation="false"
                    SkinID="skinBtnCancel" />
                <asp:Button ID="lnkBtnSave" runat="server" SkinID="skinBtnSave" OnClientClick="Validator();"
                    OnClick="lnkBtnSave_Click" />
            </td>            
        </tr>
        </table>
        <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 0px solid #5078B3;">
        <tr style="width: 100%">
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server">
                    <table width="100%">
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label1" runat="server">Book Total Amount :</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label2" runat="server">Total cash Enteted:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashEnteted" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label3" runat="server">Total Remaining:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashRem" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr style="width: 100%">
            <td align="center" style="width: 25%">
                                <asp:SqlDataSource ID="srcBook" runat="server" SelectCommand="SELECT BookID,BookName FROM tblBook Where BookStatus='Open' And FlagCollected = 'Y' Order By BookName ASC "
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td align="center" style="width: 25%">
                <asp:SqlDataSource ID="srcReason" runat="server" SelectCommand="SELECT reason FROM ReasonMaster" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td style="width: 25%">
                <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="5" style="height: 16px">
                <asp:HiddenField ID="hdDateEnt" Value="" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
