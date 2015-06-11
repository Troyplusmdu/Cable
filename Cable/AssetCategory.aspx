<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="AssetCategory.aspx.cs" Inherits="AssetCategory" Title="Asset Category" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<div style="text-align:left; width:90%">
    <asp:HiddenField ID="hdAsset" runat="server" Value="0" />
    <asp:HiddenField ID="hdCat" runat="server" Value="0" />
    <br />
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
                <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3">
        <tr>
            <td colspan="4" align="center" class="SectionHeader">
                Search Categories
            </td>
        </tr>
        <tr>
            <td style="width: 25%" class="tblLeft">
                Asset Category:
            </td>
            <td style="width: 25%" class="tblLeft">
                <asp:TextBox ID="txtSCat" runat="server" MaxLength="30" Width="125px" Height="16px"
                    CssClass="txtBox"> </asp:TextBox>
            </td>
            <td colspan="2" class="tblLeft">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    SkinID="skinBtnSearch" />
            </td>
        </tr>
    </table>
            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
    <asp:Panel ID="pnlAsset" runat="server" Visible="false">
        <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;">
            <tr>
                <td colspan="4" align="left" class="SectionHeader">
                    Category
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblLeft">
                    Asset Category:<asp:RequiredFieldValidator ID="rvCat" runat="server" ControlToValidate="txtCat"
                        CssClass="tblLeft" ValidationGroup="saveVal" ErrorMessage="Category is mandatory">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtCat" runat="server" MaxLength="30" Width="140px"
                        CssClass="cssTextBox"> </asp:TextBox>
                </td>
                <td colspan="2" class="tblLeft">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="skinBtnSave"
                        ValidationGroup="saveVal" />
                    &nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" SkinID="skinBtnSave"
                        ValidationGroup="saveVal" OnClick="btnUpdate_Click" Enabled="false" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="skinBtnCancel"
                        OnClick="btnCancel_Click" Enabled="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" SkinID="skinBtnAddNew"></asp:Button>
    <br />
    <table style="width: 100%;" cellpadding="0" cellspacing="0"  align="center">        
        <tr>
            <td style="width: 100%">
                <br />
                <asp:GridView ID="GrdViewAsset" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" OnPageIndexChanging="GrdViewAsset_PageIndexChanging"
                    OnRowCreated="GrdViewAsset_RowCreated" DataKeyNames="CategoryID" PageSize="10"
                    EmptyDataText="No Asset Details found for the search criteria" OnSelectedIndexChanged="GrdViewAsset_SelectedIndexChanged"
                    OnRowDeleting="GrdViewAsset_RowDeleting">
                    <EmptyDataRowStyle CssClass="GrdContent" />
                    <Columns>
                        <asp:BoundField DataField="CategoryID" HeaderText="Asset Category ID" />
                        <asp:BoundField DataField="CategoryDescription" HeaderText="Asset Category" />
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Asset Category?"
                                    runat="server">
                                </cc1:ConfirmButtonExtender>
                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox"
                            OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                            ID="btnFirst" />
                        <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                            ID="btnPrevious" />
                        <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                            ID="btnNext" />
                        <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                            ID="btnLast" />
                    </PagerTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>    
</asp:Content>
