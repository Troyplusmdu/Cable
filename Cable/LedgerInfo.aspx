<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="LedgerInfo.aspx.cs" Inherits="LedgerInfo" Title="Ledger Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table style="width: 100%;" align="center">
        <tr>
            <td class="LMSHeader" style="width: 918px; height: 20px;">
                <span></span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%" valign="middle">
                <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                    style="border: 1px solid black">
                    <tr>
                        <td colspan="4" align="left" class="searchHeader">
                            Search
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="width: 25%" class="tblLeft">
                            Ledger: *
                        </td>
                        <td style="width: 25%">
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtSearch"
                                WatermarkCssClass="watermark" WatermarkText="Type the Value" runat="server">
                            </cc1:TextBoxWatermarkExtender>
                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                        </td>
                        <td style="width: 25%">
                            &nbsp;
                            <asp:DropDownList ID="ddCriteria" runat="server" Width="100%">
                                <asp:ListItem Value="0">-- All --</asp:ListItem>
                                <asp:ListItem Value="LedgerName">Ledger Name</asp:ListItem>
                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinButtonMedium" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                    DataKeyNames="LedgerID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                    OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                    OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted"
                    OnItemUpdated="frmViewAdd_ItemUpdated">
                    <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                        BorderColor="#cccccc" Height="20px" />
                    <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                        VerticalAlign="middle" Height="20px" />
                    <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                        Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                    <EditItemTemplate>
                       <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                            style="border: 1px solid black">
                            <tr>
                                <td colspan="4" align="left" class="searchHeader">
                                    Ledger Info
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Ledger Name: *
                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Name is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtLdgrName" runat="server" Text='<%# Bind("LedgerName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Alias Name: *
                                    <asp:RequiredFieldValidator ID="rvAliasName" runat="server" ControlToValidate="txtAliasName"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAliasName" runat="server" Text='<%# Bind("AliasName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px;" class="tblLeft">
                                <td style="width: 25%">
                                    Account Group: *
                                    <asp:CompareValidator ID="cvPhase" runat="server" ControlToValidate="ddAccGroup"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Group is Mandatory"
                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                </td>
                                <td style="width: 25%; height: 28px;">
                                    <asp:DropDownList ID="ddAccGroup" DataSourceID="srcGroupInfo" runat="server" SelectedValue='<%# Bind("GroupID") %>'
                                        DataTextField="GroupName" DataValueField="GroupID" Width="98%" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 25%">
                                    Opening Balance: *
                                    <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtOpenBal" runat="server" Text='<%# Bind("OpenBalance") %>' CssClass="txtBox"
                                        Width="60%"></asp:TextBox>
                                    <asp:DropDownList ID="ddCRDR" runat="server" Width="25%" CssClass="drpDownList" SelectedValue='<%# Bind("DRORCR") %>'>
                                        <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                        <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Contact Name:
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtContName" runat="server" Text='<%# Bind("ContactName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Phone No:
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Address:
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAdd1" runat="server" Text='<%# Bind("Add1") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAdd2" runat="server" Text='<%# Bind("Add2") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAdd3" runat="server" Text='<%# Bind("Add3") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 25%">
                                </td>
                                <td align="right" colspan="3" valign="bottom">
                                    <table cellspacing="0">
                                        <tr width="100%">
                                            <td style="width: 67px">
                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update" OnClick="UpdateButton_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfo"
                                        TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                    <InsertItemTemplate>
                        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                            style="border: 1px solid black">
                            <tr class="tblLeft">
                                <td colspan="4" align="left" class="searchHeader">
                                    Add Ledger Info
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Ledger Name: *
                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Name is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtLdgrName" runat="server" Text='<%# Bind("LedgerName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Alias Name: *
                                    <asp:RequiredFieldValidator ID="rvAliasName" runat="server" ControlToValidate="txtAliasName"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAliasName" runat="server" Text='<%# Bind("AliasName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Account Group: *
                                    <asp:CompareValidator ID="cvPhase" runat="server" ControlToValidate="ddAccGroup"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Group is Mandatory"
                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                </td>
                                <td style="width: 25%; height: 28px;">
                                    <asp:DropDownList ID="ddAccGroup" DataSourceID="srcGroupInfo" SkinID="skinDdlBox"
                                        runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName"
                                        DataValueField="GroupID" Width="100%" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 25%">
                                    Opening Balance: *
                                    <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtOpenBal" runat="server" Text='<%# Bind("OpenBalance") %>' CssClass="txtBox"
                                        Width="60%"></asp:TextBox>
                                    <asp:DropDownList ID="ddCRDR" runat="server" Width="25%" CssClass="drpDownList" SelectedValue='<%# Bind("DRORCR") %>'>
                                        <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                        <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Contact Name:
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtContName" runat="server" Text='<%# Bind("ContactName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Phone No:
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                    Address:
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAdd1" runat="server" Text='<%# Bind("Add1") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAdd2" runat="server" Text='<%# Bind("Add2") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr style="height: 30px" class="tblLeft">
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAdd3" runat="server" Text='<%# Bind("Add3") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                    <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfo"
                                        TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                                <td align="right" style="width: 25%">
                                    <table cellspacing="0">
                                        <tr width="100%">
                                            <td style="height: 26px">
                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                    Text="Save" OnClick="InsertButton_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ImageButton ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" ImageUrl="~/App_Themes/DefaultTheme/Images/BtnAddnew.png"></asp:ImageButton>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%">
                <br />
                <asp:GridView ID="GrdViewLedger" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    OnRowCreated="GrdViewLedger_RowCreated" Width="100%" DataSourceID="GridSource"
                    AllowPaging="True" DataKeyNames="LedgerID" EmptyDataText="No Data Found. Please try again."
                    OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" />
                        <asp:BoundField DataField="AliasName" HeaderText="Alias Name" />
                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                        <asp:BoundField DataField="OpenBalance" HeaderText="Open Balance" />
                        <asp:BoundField DataField="Credit" HeaderText="Credit" />
                        <asp:BoundField DataField="Debit" HeaderText="Debit" />
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                <asp:HiddenField ID="DRORCR" runat="server" Value='<%# Bind("DRORCR") %>' />
                                <asp:HiddenField ID="OpenBalance" runat="server" Value='<%# Bind("OpenBalance") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
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
        <tr width="100%">
            <td style="width: 918px" align="left">
                <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListLedgerInfo"
                    TypeName="BusinessLogic"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetLedgerInfoForId"
                    TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                    InsertMethod="InsertLedgerInfo" UpdateMethod="UpdateLedgerInfo">
                    <UpdateParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="LedgerID" Type="Int32" />
                        <asp:Parameter Name="LedgerName" Type="String" />
                        <asp:Parameter Name="AliasName" Type="String" />
                        <asp:Parameter Name="GroupID" Type="Int32" />
                        <asp:Parameter Name="OpenBalanceDR" Type="Int32" />
                        <asp:Parameter Name="OpenBalanceCR" Type="Int32" />
                        <asp:Parameter Name="OpenBalance" Type="Int32" />
                        <asp:Parameter Name="ContactName" Type="String" />
                        <asp:Parameter Name="DRORCR" Type="String" />
                        <asp:Parameter Name="Add1" Type="String" />
                        <asp:Parameter Name="Add2" Type="String" />
                        <asp:Parameter Name="Add3" Type="String" />
                        <asp:Parameter Name="Phone" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GrdViewLedger" Name="LedgerID" PropertyName="SelectedValue"
                            Type="String" />
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="LedgerName" Type="String" />
                        <asp:Parameter Name="AliasName" Type="String" />
                        <asp:Parameter Name="GroupID" Type="Int32" />
                        <asp:Parameter Name="OpenBalanceDR" Type="Int32" />
                        <asp:Parameter Name="OpenBalanceCR" Type="Int32" />
                        <asp:Parameter Name="OpenBalance" Type="Int32" />
                        <asp:Parameter Name="ContactName" Type="String" />
                        <asp:Parameter Name="DRORCR" Type="String" />
                        <asp:Parameter Name="Add1" Type="String" />
                        <asp:Parameter Name="Add2" Type="String" />
                        <asp:Parameter Name="Add3" Type="String" />
                        <asp:Parameter Name="Phone" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
            </td>
        </tr>
    </table>
</asp:Content>
