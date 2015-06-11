<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileCashEntry.aspx.cs" Inherits="MobileCashEntry" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile Cash Entry</title>
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>
<script language="javascript" type="text/javascript">

function refresh() 
{ 
    if (116==event.keyCode) 
    return false; 
} 

function Validator()
{
    var txtMobile = document.getElementById('txtMobileNo').value;
    
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

function ShowInstCashRow(id) 
{ 
    var entryType = document.getElementById("ddEntryType");
    var Val = entryType.options[entryType.selectedIndex].value

    //var ddArray = document.getElementsByTagName("option");
    var tableRow = document.getElementById('InstRow');   
            
    if(Val == 'CASH' )
    {
        tableRow.className = "hidden" ; 
    }
    else if (Val == 'REINST') 
    { 
        tableRow.className = "AdvancedSearch" ; 
    }        
    else
    { 
        tableRow.className = "AdvancedSearch" ; 
    }        
    
    document.getElementById("txtCustCode").focus();
         
     //var txtCustoCode = GettxtBoxName("txtCustCode");
     //txtCustoCode.focus();
}

document.onkeydown = function (){ refresh(); } 

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
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
    <table width="100%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px;"
        cellpadding="5" cellspacing="5">
        <tr>
            <td colspan="2" align="left">
                <uc1:errordisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="item alignLeft" colspan="3">
                * represents mandatory fields<asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true" 
					ShowSummary="false" HeaderText="Validation Messages" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Book : *
                <asp:CompareValidator ID="cvBook" runat="server" ControlToValidate="ddBook" 
                    ErrorMessage="Book is Mandatory" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="ddBook" runat="server" AppendDataBoundItems="True"
                    SkinID="skinDdlBox" DataTextField="BookName" DataValueField="BookID" AutoPostBack="false"
                    Width="100%" OnSelectedIndexChanged="ddBook_SelectedIndexChanged">
                    <asp:ListItem Value="0"> -- Select Book -- </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
       <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Date Paid(dd/MM/yyyy): *
                <asp:CompareValidator ControlToValidate="txtDatePaid" Operator="DataTypeCheck" Type="Date" EnableClientScript="true" 
                    ErrorMessage="Please enter a valid date(dd/MM/yyyy)" runat="server" ID="cmpValtxtDate">*</asp:CompareValidator>
            </td>
            <td style="width: 50%">
                <asp:TextBox ID="txtDatePaid" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Area : *<asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea" 
                    ErrorMessage="Area is Mandatory" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" DataSourceID="srcArea"
                    SkinID="skinDdlBox" DataTextField="area" DataValueField="area" Width="100%">
                    <asp:ListItem Value="0"> -- Select Area -- </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Cash Entry Type :
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="ddEntryType" runat="server" OnSelectedIndexChanged="ddEntryType_SelectedIndexChanged"
                    Width="100%" AutoPostBack="false" onchange="javascript:ShowInstCashRow(this.id);" SkinID="skinDdlBox">
                    <asp:ListItem Value="CASH">Subscription Cash</asp:ListItem>
                    <asp:ListItem Value="INST">Installtion Cash</asp:ListItem>
                    <asp:ListItem Value="REINST">Re-Installtion Cash</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>                        
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Bill No :
                <asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo" 
                    ErrorMessage="Bill No. is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 50%">
                <asp:TextBox ID="txtBillNo" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Customer Code : *<asp:RequiredFieldValidator ID="rvCustCode" runat="server" ControlToValidate="txtCustCode" 
                    ErrorMessage="Customer Code is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 50%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox" 
                    AutoPostBack="True" ontextchanged="txtCustCode_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Customer Name & Balance Amount:
            </td>
            <td style="width: 48%; text-align:left">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width:50%">
                            <asp:TextBox ID="txtCustName" runat="server" Enabled="false" Width="98%"></asp:TextBox>        
                        </td>
                        <td style="width:50%">
                            <asp:TextBox ID="txtCustBalance" runat="server" Enabled="false" Width="98%"></asp:TextBox>
                        </td>                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Mobile:
            </td>
            <td style="width: 50%">
                <asp:TextBox ID="txtMobileNo" MaxLength="10" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>        
        <tr style="width: 100%; display:none">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Balance :
            </td>
            <td style="width: 50%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Amount: *<asp:RequiredFieldValidator ID="rvAmnt" runat="server" ControlToValidate="txtAmount" 
                    ErrorMessage="Amount is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 50%">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
       </tr>
        <tr style="width: 100%" class="hidden" runat="server" id="InstRow">
            <td class="LMSLeftColumnColor" style="width: 50%">
                Installation Cash: *
                <asp:RequiredFieldValidator ID="rvInstCash" runat="server" ErrorMessage="Installation Cash is Mandatory" Enabled="false"
                    ControlToValidate="txtInstCash">*</asp:RequiredFieldValidator>
            </td>
            <td class="LMSLeftColumnColor" style="width: 50%">
                <asp:TextBox ID="txtInstCash" runat="server" SkinID="skinTextBox" Width="98%"></asp:TextBox>
            </td>
        </tr>       
        <tr style="width: 100%">
            <td style="width: 50%">
                <asp:HiddenField ID="hidRetVal" Value="" runat="server" />
            </td>
            <td style="width: 50%">
            </td>
        </tr>
        <tr style="width: 100%">            
            <td style="width: 50%" align="left">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="BtnSave" runat="server" Text="Save" OnClientClick="Validator();" SkinID="BtnMobileSave" OnClick="lnkBtnSave_Click" />
                        </td>
                        <td>
                           <asp:ImageButton ID="BtnLogout" runat="server" Text="Logout" SkinID="BtnLogout" CausesValidation="false" 
                            onclick="BtnLogout_Click" />
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; font-family:Verdana; font-size:12px" align="left">
                <asp:Panel ID="Panel1" runat="server">
                    <table>
                        <tr style="display:none">
                            <td class="alignLeft" style="width: 50%">
                                <asp:Label ID="Label1" runat="server">Total:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label2" runat="server">Total Enteted:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashEnteted" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="alignLeft" style="width: 50%">
                                <asp:Label ID="Label3" runat="server">Remaining:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashRem" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>                
                <asp:SqlDataSource ID="srcBook" runat="server" SelectCommand="SELECT BookID,BookName FROM tblBook Where BookStatus='Open' And FlagCollected = 'N' Order By BookName ASC "
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
               <asp:HiddenField ID="hdDateEnt" Value="" runat="server" />               
            </td>            
        </tr>
        <tr style="width: 100%">
            <td align="center" style="width: 50%">
                <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td align="center" style="width: 50%">
                <asp:SqlDataSource ID="srcReason" runat="server" SelectCommand="SELECT reason FROM ReasonMaster" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
