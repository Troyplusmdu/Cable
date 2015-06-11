<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="UserDetails.aspx.cs" Inherits="UserDetails" Title="User Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <script type="text/javascript">
    
    function pageLoad(){
        //  get the behavior associated with the tab control
        var tabContainer = $find('ctl00_cplhControlPanel_tabEditContol_tabEditProdMaster_tabs2');
        //  get all of the tabs from the container
        var tabs = tabContainer.get_tabs();
        
        //  loop through each of the tabs and attach a handler to
        //  the tab header's mouseover event
        for(var i = 0; i < tabs.length; i++){
            var tab = tabs[i];
            
            $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function(){
                    tabContainer.set_activeTab(this);             
                }
            ));
        }
    }
    
    </script>

<div style="text-align:left">
<cc1:TabContainer ID="tabEditContol" runat="server" Width="100%" CssClass="ajax__tab_yuitabview-theme" 
    ActiveTabIndex="0">
    <cc1:TabPanel ID="tabEditProdMaster" runat="server" HeaderText="User Details">
        <ContentTemplate>
        <table style="width: 100%;" align="center">
        <tr>
            <td colspan="4" align="left">
            </td>
        </tr>
        <tr >
            <td class="tblLeft" style="width: 19%">
                User Name : *
                <asp:RequiredFieldValidator ID="rvUserName" runat="server" 
                    ControlToValidate="txtUserName" Display="Dynamic" >UserName is mandatory</asp:RequiredFieldValidator>
            </td>
            <td style="width: 23%">
                <asp:TextBox ID="txtUserName" runat="server" SkinID="skinTxtBox" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="tblLeft" style="width: 19%">
                Email :
            </td>
            <td style="width: 23%">
                <asp:TextBox ID="txtEmail" runat="server" SkinID="skinTxtBox" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  style="width: 19%">
            </td>
            <td style="width: 23%">
                <asp:CheckBox runat="server" ID="chkAccLocked" Visible="False" />
            </td>
            <td style="width: 19%">
            </td>
            <td style="width: 23%">
            </td>
        </tr>
        <tr>
            <td style="width: 19%" colspan="4">
                <br />
                <br />
                <cc1:TabContainer ID="tabs2" runat="server"   Width="100%" CssClass="ajax__tab_yuitabview-theme" BehaviorID="tabTest">
					<cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Master Access">
						<ContentTemplate>
							<div style="text-align:left; width:100%">
								<asp:CheckBoxList ID="chckMaster" RepeatColumns="3" CellPadding="5" runat="server" Width="100%">
								</asp:CheckBoxList>
							</div>
						</ContentTemplate>
					</cc1:TabPanel>
					<cc1:TabPanel ID="tabBilling" runat="server" HeaderText="Accounting/Asset Access" Visible="false">
						<ContentTemplate>
							<div style="text-align:left; width:100%">
								<asp:CheckBoxList ID="chkBilling" RepeatColumns="3" Width="100%" CellPadding="5" RepeatLayout="Table" runat="server">
								</asp:CheckBoxList>
							</div>
						</ContentTemplate>
					</cc1:TabPanel>
					<cc1:TabPanel ID="tabReports" runat="server" RepeatLayout="Table" HeaderText="Reports Access">
						<ContentTemplate>
							<div style="text-align:left; width:100%">
								<asp:CheckBoxList ID="chkReport" runat="server" Width="100%" CellPadding="3" RepeatColumns="3" RepeatLayout="Table" Height="250px">
								</asp:CheckBoxList>
							</div>
						</ContentTemplate>
					</cc1:TabPanel>
					<cc1:TabPanel ID="tabMobile" runat="server" HeaderText="Mobile Access">
						<ContentTemplate>
							<div style="text-align:left; width:100%">
								<asp:CheckBoxList ID="chkMobile" RepeatColumns="3" Width="100%" CellPadding="5" RepeatLayout="Table" runat="server">
								</asp:CheckBoxList>
							</div>
						</ContentTemplate>
					</cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
         <tr>
            <td colspan="2">
            </td>
            <td style="width: 23%" colspan="2" align="right">
                <asp:Button ID="lnkBtncancel" runat="server" Text="Cancel" 
                    CausesValidation="False" SkinID="skinBtnCancel" onclick="lnkBtncancel_Click" />&nbsp;
                <asp:Button ID="lnkBtnSave" runat="server" Text="Update" SkinID="skinBtnUpdate" onclick="lnkBtnSave_Click" />
            </td>
        </tr>
</table>
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
</div>
<br />
</asp:Content>

