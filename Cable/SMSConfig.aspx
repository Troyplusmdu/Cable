<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="SMSConfig.aspx.cs" Inherits="SMSConfig" Title="SMS Configuration" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <table width="70%" style="border: 0px solid #5078B3">
        <tr>
            <td align="left">
                <uc1:errordisplay ID="errorDisplay" runat="server" />
                <br />
            </td>
        </tr>
    </table>
    <table width="70%" style="border: 1px solid #5078B3">
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="3">
                <span>SMS Configuration</span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="tblLeft" style="width: 25%">
                SMS Required?
            </td>
            <td class="lmsrightcolumncolor" style="width: 35%;text-align:left">
                <asp:RadioButtonList runat="server" ID="rdoSMSReq" CssClass="tblLeft" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="width: 25%">
            </td>
        </tr>        
        <tr style="width: 100%">
            <td class="tblLeft" style="width: 25%">
                Send Copy SMS?
            </td>
            <td class="lmsrightcolumncolor" style="width: 35%;text-align:left">
                <asp:RadioButtonList runat="server" ID="rdoCopyReq" CssClass="tblLeft" 
                    RepeatDirection="Horizontal" AutoPostBack="true"
                    onselectedindexchanged="rdoCopyReq_SelectedIndexChanged">
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="width: 25%">
               <asp:Button ID="lnkBtnUpdate" runat="server" Text="Update" Width="75px" 
                    SkinID="skinBtnUpdate" onclick="lnkBtnUpdate_Click"/>
            </td>
        </tr>
        <tr style="width: 100%" id="rowMobile" runat="server">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Mobile No:
            </td>
            <td class="lmsrightcolumncolor" style="width: 35%">
                <asp:TextBox ID="txtReconDate" runat="server" Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
  </table>
  <br />
  <br />
  <br />
    <table width="70%" style="border: 1px solid #5078B3">
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="3">
                <span>Adhoc SMS</span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="tblLeft" style="width: 25%">
                Area :</td>
            <td style="width:45%; text-align:left">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" Width="100%" SkinID="skinDdlBox"
                    DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                    <asp:ListItem Value="0">   -- All Areas --    </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 25%; text-align:left">
                <asp:Button ID="BtnGetNos" runat="server" Text="Get Mobile Nos" Width="100px" 
                    CssClass="Button" onclick="BtnGetNos_Click"/>
            </td>            
        </tr>
        <tr>
                <td class="tblLeft" style="width: 25%">
                    Balance :
                </td>
                <td style="width: 50%" align="left" valign="top">
                    <asp:DropDownList ID="ddOper" runat="server" Style="height: 19px" SkinID="skinDdlBox" Width="30%">
                        <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                        <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                        <asp:ListItem Value="=">=</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBalance" runat="server" SkinID="skinTxtBox" Height="14px"></asp:TextBox>
                </td>        
                <td style="width: 25%"></td>                
        </tr>
        <tr>        
            <td class="tblLeft" style="width: 25%">
                Mobile No's:(seperate multiple No's by Comma(,))<asp:RequiredFieldValidator ID="rvMobileNos" ValidationGroup="sendSMS" runat="server" ControlToValidate="txtMobileNos"
                  EnableClientScript="true"  ErrorMessage="Atleast 1 mobile no is mandatory." Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" colspan="2" style="width: 60%">
                <asp:TextBox ID="txtMobileNos" runat="server" CssClass="cssTextBox" Width="98%" TextMode="MultiLine" Height="30px"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="tblLeft" style="width: 25%">
                Text<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="sendSMS" runat="server" ControlToValidate="txtMessage"
                  EnableClientScript="true"  ErrorMessage="Text is mandatory." Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" colspan="2" style="width: 60%;text-align:left">
                <asp:TextBox ID="txtMessage" runat="server" Width="98%" CssClass="cssTextBox" TextMode="MultiLine" Height="60px" ></asp:TextBox>
            </td>
        </tr>    
        <tr>
            <td style="width: 100%; text-align:right" colspan="3">
               <asp:Button ID="BtnSendSMS" runat="server" Text="Send SMS" Width="75px" ValidationGroup="sendSMS"
                    CssClass="Button" onclick="BtnSendSMS_Click"/>
            </td>
        </tr>                            
    </table>  
    <asp:SqlDataSource ID="srcArea" runat="server"
        SelectCommand="SELECT [area] FROM [AreaMaster]" ProviderName="System.Data.OleDb">
    </asp:SqlDataSource>    
</asp:Content>

