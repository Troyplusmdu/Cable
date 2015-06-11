<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="UserMaintenance.aspx.cs" Inherits="UserMaintenance" Title="Manage Users" %>
<%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
 <div style="text-align:left">
     <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="-1" HeaderCssClass="hdrSearch"
        AutoSize="None"
        FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false"
        SuppressHeaderPostbacks="true">
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" HeaderCssClass="hdrSearch" HeaderSelectedCssClass="hdrSearch"
                runat="server">
                <Header>
                </Header>
                <Content>
    <table style="border: 0px solid #5078B3" style="width: 80%;">
        <tr>
            <br />
            <td class="SectionHeader" colspan="4">
                User Maintenance</td>
        </tr>
        <tr>
            <td class="tblLeft" style="width: 19%">
                User Name :
            </td>
            <td style="width: 15%; text-align:left">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="cssTextBox" Width="150px"></asp:TextBox>
            </td>
            <td colspan="2" style="width: 15%; text-align:left">
                <asp:Button ID="lnkBtnSearchId" runat="server" OnClick="lnkBtnSearch_Click" SkinID="skinBtnSearch" TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td style="width: 20px" colspan="4">
            </td>
        </tr>
        </table>
                </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
        <br />                  
        <table style="border: 0px solid #5078B3" style="width: 80%;">
            <tr>
                <td>
                    <asp:Button ID="lnkBtnAdd" runat="server" SkinID="skinBtnAddNew" OnClick="lnkBtnAdd_Click" ></asp:Button>                 
                </td>
            </tr>
        </table>
        <br />
        <table style="border: 0px solid #5078B3" style="width: 80%;">
        <tr style="width: 100%; text-align:center">
            <td width="100%" colspan="4">
               <asp:GridView ID="GrdViewCust" TabIndex="6" Width="100%" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="username" GridLines="None"
                    OnRowCreated="GrdViewCust_RowCreated" AllowPaging="true" DataSourceID="GridSource" OnRowCommand="GrdViewCust_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="username" HeaderText="User">
                            <ItemStyle Width="35%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email">
                            <ItemStyle Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="Locked" HeaderText="Locked">
                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:CheckBoxField>
                        <uc:BoundUrlColumn DataField="username" Tooltip="Edit User Roles" BaseUrl="UserDetails.aspx?ID="
                            IconPath="App_Themes/DefaultTheme/Images/edit.png" ItemStyle-BorderWidth="0"><ItemStyle Width="50px" BorderWidth="0" CssClass="center" />
                        </uc:BoundUrlColumn>
                        <asp:TemplateField HeaderStyle-Width="25px">
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" OnClientClick="return confirm('Are you sure you want to delete the User?');" 
                                 CommandName="Delete"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server" ID="btnFirst" />
                        <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrevious" />
                        <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext" />
                        <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server" ID="btnLast" />
                    </PagerTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="GridSource" runat="server" DeleteMethod="DeleteUserInfo"
                    SelectMethod="ListUsers" TypeName="BusinessLogic">
                    <DeleteParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="username" Type="String" />
                    </DeleteParameters>                    
                </asp:ObjectDataSource>
            </td>
         </tr>
 </table>
 </div>
</asp:Content>

