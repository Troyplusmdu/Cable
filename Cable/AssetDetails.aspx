<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="AssetDetails.aspx.cs" Inherits="AssetDetails" Title="Assest Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
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
    <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3">
        <tr>
            <td colspan="4" align="center" class="SectionHeader">
                Search Assets
            </td>
        </tr>
        <tr style="height: 20px">
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
                Asset area: *
            </td>
            <td style="width: 25%" class="tblLeft">
                <asp:DropDownList ID="drpSAssetarea" AppendDataBoundItems="true" DataTextField="area"
                    DataValueField="area" Width="125px" Height="21px" runat="server" CssClass="drpDownListMedium">
                    <asp:ListItem Value="0">--Select Area--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 25%" class="tblLeft">
                Asset Status: *
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="drpSAStatus" Width="125px" Height="21px" runat="server" CssClass="drpDownListMedium">
                    <asp:ListItem Value="0">-- Select Status --</asp:ListItem>
                    <asp:ListItem>New</asp:ListItem>
                    <asp:ListItem>Used</asp:ListItem>
                    <asp:ListItem>Scrapped</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 25%" class="tblLeft">
                Asset no :
            </td>
            <td style="width: 25%" class="tblLeft">
                <asp:TextBox ID="txtSAssetno" runat="server" Width="125px" Height="16px" CssClass="txtBox"> </asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtSAssetno"
                    FilterType="Numbers" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="tblLeft">
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
        <table style="width: 100%;text-align:left" align="left" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;">
            <tr>
                <td colspan="4" align="left" class="SectionHeader">
                    Asset Details
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblLeft">
                    Asset Code: *
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="Code is mandatory"
                        InitialValue="0" ControlToValidate="drpAssetCode" runat="server" ValidationGroup="saveVal" CssClass="tblLeft" />
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:DropDownList ID="drpAssetCode" AppendDataBoundItems="true" DataValueField="AssetCode"
                        DataTextField="AssetCode" runat="server" SkinID="skinDdlBox">
                        <asp:ListItem Value="0">--Select Code--</asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td style="width: 25%" class="tblLeft">
                    Asset Status: *
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="Status is mandatory"
                        InitialValue="0" ControlToValidate="drpAssetStatus" runat="server" ValidationGroup="saveVal" CssClass="tblLeft" />
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:DropDownList ID="drpAssetStatus" AppendDataBoundItems="true" runat="server"
                        SkinID="skinDdlBox">
                        <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                        <asp:ListItem>New</asp:ListItem>
                        <asp:ListItem>Used</asp:ListItem>
                        <asp:ListItem>Scrapped</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblLeft">
                    Asset Location: *
                    <asp:RequiredFieldValidator ID="rvBill" runat="server" ControlToValidate="txtLocation"
                        CssClass="tblLeft" ValidationGroup="saveVal">Location is mandatory</asp:RequiredFieldValidator>
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:TextBox ID="txtLocation" runat="server" MaxLength="30" Width="125px" Height="16px"
                        SkinID="skinTxtBox"></asp:TextBox>
                </td>
                <td style="width: 25%" class="tblLeft">
                    Asset area:
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:DropDownList ID="drpAssetArea" runat="server" AppendDataBoundItems="true" DataTextField="area"
                        DataValueField="area" SkinID="skinDdlBox">
                        <asp:ListItem Value="0">--Select Area--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblLeft">
                    Serial No:*
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:TextBox ID="txtSerialNo" runat="server" MaxLength="30" SkinID="skinTxtBox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSerialNo"
                        CssClass="tblLeft" ValidationGroup="saveVal">Location is mandatory</asp:RequiredFieldValidator>
                </td>
                <td style="width: 100%; text-align:right" colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="skinBtnSave" ValidationGroup="saveVal"
                        OnClick="btnSave_Click" />&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" SkinID="skinBtnUpdate"
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
    <br />
    <table align="center" style="width: 100%;">
        <tr>
            <td style="width: 100%">
                <asp:GridView ID="GrdViewAsset" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" OnPageIndexChanging="GrdViewAsset_PageIndexChanging"
                    OnRowCreated="GrdViewAsset_RowCreated" DataKeyNames="AssetNo" PageSize="10" EmptyDataText="No Asset Details found for the search criteria"
                    OnSelectedIndexChanged="GrdViewAsset_SelectedIndexChanged" OnRowDeleting="GrdViewAsset_RowDeleting">
                    <EmptyDataRowStyle CssClass="GrdContent" />
                    <Columns>
                        <asp:BoundField DataField="AssetCode" HeaderText="Asset Code" />
                        <asp:BoundField DataField="AssetStatus" HeaderText="Status" />
                        <asp:BoundField DataField="AssetLocation" HeaderText="Location" />
                        <asp:BoundField DataField="AssetArea" HeaderText="Area" />
                        <asp:BoundField DataField="DateEntered" HeaderText="Entered Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="SerialNo" HeaderText="Serial No" />
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Asset Details?"
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
                <asp:HiddenField ID="hdAsset" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    <br />
</div>    
</asp:Content>
