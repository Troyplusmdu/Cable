<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="Configuration.aspx.cs" Inherits="Configuration" Title="System Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <script language="javascript" type="text/javascript">

    function ConfirmOnline()
    {
            var rv = confirm("The System is currently configured to work OFFLINE. Are you sure you want to change it to Online?"); 
            
            if(rv == true)
            {
                return true;
            }
            else
            {
                return false;
            }
    }

    function ConfirmOffline()
    {
            var rv = confirm("The System is currently configured to work ONLINE. Are you sure you want to change it to Offline?"); 
            
            if(rv == true)
            {
                return true;
            }
            else
            {
                return false;
            }
    }

    function ConfirmContinueOffline()
    {
            var rv = confirm("The System is currently configured to work ONLINE. Are you sure you want to continue working Offline?"); 
            
            if(rv == true)
            {
                return true;
            }
            else
            {
                return false;
            }
    }
    </script>

    <br />
    <br />
    <asp:UpdateProgress runat="server" ID="uProcess1">
        <ProgressTemplate>
            <progresstemplate>        
        <div id="divLoading" class="divCenter"  align="center">
            <table border="0" cellpadding="0" cellspacing="0" align="center" 
                style="width: 100%; height:100%">
                <tr>
                    <td height="10">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" valign="middle" style="width:100%">
                        <img id="imgLoading" src="App_Themes/DefaultTheme/Images/loading.gif" align="middle" vspace="0" hspace="0" style="vertical-align:middle" /><br />
                        Please wait... donot cancel until it completes the current action.    
                    </td>
                </tr>
                <tr>
                    <td height="10">&nbsp;</td>
                </tr>
            </table>
        </div>
    </progresstemplate>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" width="50%" style="border: 1px solid black; margin: 0 0 0 50px"
                cellpadding="5" cellspacing="5">
                <tr>
                    <td class="SectionHeader">
                        System Configuration
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="btnOffline" runat="server" Text="Offline" TabIndex="5" Width="90%"
                            Font-Bold="false" SkinID="skinButtonCol" CausesValidation="false" OnClick="btnOffline_Click" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="btnOnline" runat="server" Text="Online" TabIndex="5" CausesValidation="false"
                            OnClick="btnOnline_Click" Width="90%" Font-Bold="false" SkinID="skinButtonCol" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="btnContOffline" runat="server" Text="Continue Offline" CausesValidation="false"
                            OnClick="btnContOffline_Click" Width="90%" Font-Bold="false" SkinID="skinButtonCol" />
                    </td>
                </tr>
            </table>
            <br />
            <table align="center" width="50%" style="border: 1px solid black; margin: 0 0 0 50px"
                cellpadding="5" cellspacing="5">
                <tr>
                    <td colspan="2" height="5">
                        <br />
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblErrorMsg" Font-Italic="True"
                            Font-Size="8pt" ForeColor="#009900"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
