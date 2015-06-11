<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="AssetMaster.aspx.cs" Inherits="AssetMaster" Title="Asset Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<div style="text-align:left">
    <asp:HiddenField ID="hdAsset" runat="server" Value="0" />
    <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="-1" HeaderCssClass="hdrSearch"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" HeaderCssClass="hdrSearch" HeaderSelectedCssClass="hdrSearch"
                runat="server">
                <Header>
                </Header>
                <Content>
                    <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                        style="border: 1px solid black">
                        <tr>
                            <td colspan="4" align="left" class="searchHeader">
                                Search
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" class="tblLeft">
                                Asset Category:
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList DataTextField="CategoryDescription" DataValueField="CategoryID"
                                    AppendDataBoundItems="true" ID="drpSAssetCat" Width="125px" Height="21px" runat="server"
                                    CssClass="drpDownListMedium">
                                    <asp:ListItem Value="0">--Select Category--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                Asset Code:
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:TextBox ID="txtSAssetCode" runat="server" Width="125px" Height="16px" CssClass="txtBox"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tblLeft">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                    SkinID="skinButtonMedium" />
                            </td>
                        </tr>
                    </table>
                </Content>
            </cc1:AccordionPane>
        </Panes>
    </cc1:Accordion>
    <br />
    <asp:Panel ID="pnlAsset" runat="server" Visible="false">
        <table style="width: 100%;text-align:left;vertical-align:middle" align="left" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;">
            <tr>
                <td colspan="4" align="center" class="SectionHeader">
                    Asset Master
                </td>
            </tr>
            <tr>
                <td style="width: 25%;vertical-align:top" class="tblLeft">
                    Asset Code: *
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:TextBox MaxLength="10" ID="txtAssetCode" runat="server" Width="160px"
                        CssClass="cssTextBox"> </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="Asset Code is mandatory"
                        ControlToValidate="txtAssetCode" runat="server" ValidationGroup="saveVal" CssClass="tblLeft" />
                </td>
                <td style="width: 25%;vertical-align:top" class="tblLeft">
                    Asset Category: *
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:DropDownList DataTextField="CategoryDescription" DataValueField="CategoryID"
                        AppendDataBoundItems="true" ID="drpAssetCat" Height="21px" runat="server"
                        SkinID="skinDdlBox">
                        <asp:ListItem Value="0">--Select Category--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="Category is mandatory"
                        InitialValue="0" ControlToValidate="drpAssetCat" runat="server" ValidationGroup="saveVal"
                        CssClass="tblLeft" />
                </td>
            </tr>
            <tr>
                <td style="width: 25%; vertical-align:top" class="tblLeft" valign="top">
                    Asset Code Description: *
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:TextBox ID="txtDescrition" TextMode="MultiLine" MaxLength="30" runat="server"
                        Width="160px" CssClass="cssTextBox"> </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="Asset Description is mandatory"
                        ControlToValidate="txtDescrition" runat="server" ValidationGroup="saveVal" CssClass="tblLeft" />
                </td>
                <td colspan="2" style="width: 50%; text-align:right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="skinBtnSave"
                        ValidationGroup="saveVal" />
                    &nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" SkinID="skinBtnUpdate"
                        ValidationGroup="saveVal" OnClick="btnUpdate_Click" Enabled="false" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="skinBtnCancel"
                        OnClick="btnCancel_Click" Enabled="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" SkinID="skinBtnAddNew">
    </asp:Button>
    <br />
    <table style="width: 100%;" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="width: 100%">
                <br />
                <asp:GridView ID="GrdViewAsset" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" OnPageIndexChanging="GrdViewAsset_PageIndexChanging"
                    OnRowCreated="GrdViewAsset_RowCreated" DataKeyNames="AssetCode" PageSize="10"
                    EmptyDataText="No Asset Details found for the search criteria" OnSelectedIndexChanged="GrdViewAsset_SelectedIndexChanged"
                    OnRowDeleting="GrdViewAsset_RowDeleting">
                    <EmptyDataRowStyle CssClass="GrdContent" />
                    <Columns>
                        <asp:BoundField DataField="AssetCode" HeaderText="Asset Code" />
                        <asp:BoundField DataField="AssetDesc" HeaderText="Asset Description" />
                        <asp:BoundField DataField="CategoryDescription" HeaderText="Category Description" />
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Asset Code?"
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
                <br />
                <br />
            </td>
        </tr>
    </table>
</div>
</asp:Content>
