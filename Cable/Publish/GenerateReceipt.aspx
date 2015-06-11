<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="GenerateReceipt.aspx.cs" Inherits="GenerateReceipt" Title="Generate Receipt" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<script language="javascript" type="text/javascript">

    /*@cc_on @*/
    /*@if (@_win32 && @_jscript_version>=5)

    function window.confirm(str)
    {
	    execScript('n = msgbox("'+str+'","4132")', "vbscript");
	    return(n == 6);
    }

    @end @*/

    function ConfirmSMS() 
    {
        
        if(Page_IsValid)
        {
	        var confSMS = document.getElementById('ctl00_cplhControlPanel_hdSMS').value;
    	
	        var confSMSRequired = document.getElementById('ctl00_cplhControlPanel_hdSMSRequired').value;
    	
	        if(confSMSRequired == "YES")
	        {
		        var rv = confirm("Do you want to send SMS to all Customers?"); 
        		
		        if(rv == true)
		        {
			        document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
			        return true;
		        }
		        else
		        {
			        document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
			        return true;
		        }
	        }
	    }
    }
</script>
<br />
<table width="80%">
        <tr>
            <td colspan="4" align="left">
                <uc1:errorDisplay ID="errorDisplay" runat="server" />
                <br />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="4">
                <span>Generate Receipt </span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="tblLeft" style="width: 25%;padding-left:15px">
                Date: *&nbsp;
                <asp:CompareValidator ID="cvDate" runat="server" ControlToValidate="DropDownList1"
                    ErrorMessage="Date is Mandatory" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:DropDownList ID="DropDownList1" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True" Width="100%">
                    <asp:ListItem Value="0">Please select Month</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 50%;vertical-align:top; text-align:left">
               <asp:Button ID="lnkBtncancel" runat="server" CssClass="Button" onclick="lnkBtncancel_Click" Text="Generate Receipts" OnClientClick="javascript:ConfirmSMS();" />
            </td>
        </tr>
</table>
<asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
<asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />            
</asp:Content>

