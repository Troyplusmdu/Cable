<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ManageBooks.aspx.cs" Inherits="ManageBooks" Title="Manage Books" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">

<script language="javascript" type="text/javascript"> 

    function Validator()
    {
        var ddCashCollected = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_ddCashCollected').value;
        
        var txtAmount = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtAmount').value;
        
        if(ddCashCollected == 'N')
        {
            if(txtAmount.length > 0)
            {
                if(txtAmount > 0)
                {
                    alert("If Cash is not Collected. Please do not enter the Book Amount.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
        }
        else
        {
            if(txtAmount.length == 0)
            {
                alert("If Cash is Collected. Please enter the Book Amount.");
                Page_IsValid = false;
                return window.event.returnValue = false;
             }
             else
             {
                if(txtAmount == 0)
                {
                    alert("If Cash is Collected. Please enter the Book Amount.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
             }
        }
    }
    
    function ValidatorEdit()
    {
        var ddCashCollected = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_ddCashCollected').value;
        
        var txtAmount = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtAmount').value;
        
        if(ddCashCollected == 'N')
        {
            if(txtAmount.length > 0)
            {
                if(txtAmount > 0)
                {
                    alert("If Cash is not Collected. Please do not enter the Book Amount.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
        }
        else
        {
            if(txtAmount.length == 0)
            {
                alert("If Cash is Collected. Please enter the Book Amount.");
                Page_IsValid = false;
                return window.event.returnValue = false;
             }
             else
             {
                if(txtAmount == 0)
                {
                    alert("If Cash is Collected. Please enter the Book Amount.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
             }
        }
    }
    
</script>
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
                                <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5"
                                        style="border: 0px solid #5078B3">
                                        <tr>
                                            <td colspan="4" style="text-align:center" class="SectionHeader" >
                                                Search Books
                                            </td>
                                        </tr>
                                        <tr style="height: 25px">
                                        <td style="width: 15%" class="tblLeft">
                                            Book Ref:
                                        </td>
                                        <td style="width: 30%" class="tblLeft">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                        </td>
                                        <td style="width: 30%" class="tblLeft">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="98%">
                                                <asp:ListItem Value="0">-- All --</asp:ListItem>
                                                <asp:ListItem Value="BookRef">Book Ref</asp:ListItem>
                                                <asp:ListItem Value="BookName">Book Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Button ID="btnSearch" runat="server" SkinID="skinBtnSearch" ></asp:Button>
                                        </td>
                                    </tr>
                                        <tr style="height: 25px">
                                        <td style="width: 15%" class="tblLeft">
                                            Open book ?
                                        </td>
                                        <td style="width: 30%" class="tblLeft">
                                            <asp:CheckBox ID="chkStatus" runat="server"/>
                                        </td>
                                        <td style="width: 30%" class="tblLeft">
                                        </td>
                                        <td style="width: 25%">
                                            
                                        </td>                                   
                                     </tr>   
                                </table>
                        </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>

    <table style="width: 100%;" align="center">
        <tr>
            <td align="left" style="width:100%">
                <uc1:errordisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%; text-align:left">
                                <br />
                                <asp:Button ID="lnkBtnAdd" runat="server" SkinID="skinBtnAddNew" OnClick="lnkBtnAdd_Click"  ></asp:Button>
                                <br />
                <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                    OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit" DataKeyNames="BookID" OnItemUpdated="frmViewAdd_ItemUpdated"
                                    OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                    EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted" OnDataBound="frmViewAdd_DataBound">
                                    <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle" BorderColor="#cccccc" Height="20px" />
                                    <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc" VerticalAlign="middle" Height="20px" />
                                    <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad" Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                    <EditItemTemplate>
                                      <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5"
                                            style="border: 1px solid #5078B3">
                                            <tr>
                                                <td style="width: 25%">
                                                    Book Ref: *
                                                    <asp:RequiredFieldValidator runat="server" ID="rqSearch" ControlToValidate="txtRefNo" ErrorMessage="BookRef is Mandatory"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("BookRef") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%">
                                                    Book Name: *
                                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtBookName">BookName is Mandatory</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtBookName" runat="server" Text='<%# Bind("BookName") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Start Entry: *
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtStartEntry" FilterType="Numbers" />
                                                    <asp:RequiredFieldValidator runat="server" ID="rv1" ControlToValidate="txtStartEntry" ErrorMessage="StartEntry is Mandatory"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtStartEntry" runat="server" Text='<%# Bind("StartEntry") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%">
                                                    End Entry: *
                                                    <cc1:FilteredTextBoxExtender ID="ftst" runat="server" TargetControlID="txtEndEntry" FilterType="Numbers" />
                                                    <asp:RequiredFieldValidator runat="server" ID="rv2" ControlToValidate="txtEndEntry" ErrorMessage="EndEntry is Mandatory"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator runat="server" ID="cmpEnt" ControlToValidate="txtEndEntry" Type="Integer" ControlToCompare="txtStartEntry" Operator="GreaterThan" ErrorMessage="EndEntry should be Greater than StartEntry"></asp:CompareValidator>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtEndEntry" runat="server" Text='<%# Bind("EndEntry") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; height: 34px;">
                                                    Total Amount: *
                                                    <asp:RequiredFieldValidator runat="server" ID="rv3" Enabled="false" ControlToValidate="txtAmount" ErrorMessage="Amount is Mandatory"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width:25%">
                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%; height: 34px;">
                                                    Book Date(dd/MM/yyyy): *
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateCreated" Display="Dynamic" EnableClientScript="True">Book Date is mandatory</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ControlToValidate="txtDateCreated" Operator="DataTypeCheck" Type="Date"
                                                        ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                </td>
                                                <td style="width: 25%; height: 34px;">
                                                    <asp:TextBox ID="txtDateCreated" runat="server" Text='<%# Bind("DateCreated") %>' Width="100px" CssClass="cssTextBox"></asp:TextBox>
                                                    <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtDateCreated')});</script>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <td style="width: 25%; height: 34px;">
                                                     Cash Collected ?</td>
                                                 <td style="width:25%">
                                                    <asp:DropDownList ID="ddCashCollected" runat="server" Enabled="false"
                                                         SelectedValue='<%# Bind("FlagCollected") %>' SkinID="skinDdlBox" 
                                                         AppendDataBoundItems="True" Width="100%" 
                                                         ondatabound="ddCashCollected_DataBound">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                                 <td style="width: 25%; height: 34px;">
                                                     Status</td>
                                                 <td style="width: 25%; height: 34px;">
                                                    <asp:DropDownList ID="ddStatus" runat="server" SelectedValue='<%# Bind("BookStatus") %>' SkinID="skinDdlBox" AppendDataBoundItems="True" Width="100%">
                                                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                                        <asp:ListItem Value="Open">Open</asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                             </tr>                                            
                                            <tr>
                                                <td style="width: 25%">
                                                </td>
                                                <td style="width: 25%">
                                                </td>
                                                <td style="width: 25%">
                                                </td>
                                                <td align="right" style="width:25%" valign="bottom">
                                                    <table cellspacing="0">
                                                        <tr width="100%">
                                                            <td style="width: 67px">
                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                    SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                    SkinID="skinBtnSave" OnClick="UpdateButton_Click"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <InsertItemTemplate>
                                        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5"
                                            style="border: 1px solid #5078B3">
                                             <tr>
                                                <td style="width: 25%">
                                                    Book Ref: *
                                                    <asp:RequiredFieldValidator runat="server" ID="rqSearch" ControlToValidate="txtRefNo" ErrorMessage="BookRef is Mandatory"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("BookRef") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%">
                                                    Book Name: *
                                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtBookName">BookName is Mandatory</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtBookName" runat="server" Text='<%# Bind("BookName") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Start Entry: *
                                                    <asp:RequiredFieldValidator runat="server" ID="rv1" ControlToValidate="txtStartEntry" ErrorMessage="StartEntry is Mandatory"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="ftst" runat="server" TargetControlID="txtStartEntry" FilterType="Numbers" />
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtStartEntry" runat="server" Text='<%# Bind("StartEntry") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%">
                                                    End Entry: *
                                                    <asp:CompareValidator runat="server" ID="cmpEnt" ControlToValidate="txtEndEntry" Type="Integer" ControlToCompare="txtStartEntry" Operator="GreaterThan" ErrorMessage="EndEntry should be Greater than StartEntry"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator runat="server" ID="rv2" ControlToValidate="txtEndEntry" ErrorMessage="EndEntry is Mandatory"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEndEntry" FilterType="Numbers" />
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="txtEndEntry" runat="server" Text='<%# Bind("EndEntry") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; height: 34px;">
                                                    Total Amount: *
                                                    <asp:RequiredFieldValidator runat="server" ID="rv3" Enabled="false" ControlToValidate="txtAmount" ErrorMessage="Amount is Mandatory"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width:25%">
                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%; height: 34px;">
                                                    Book Date(dd/MM/yyyy): *
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateCreated" Display="Dynamic" EnableClientScript="True">Book Date is mandatory</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ControlToValidate="txtDateCreated" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                </td>
                                                <td style="width: 25%; height: 34px;">
                                                <asp:TextBox ID="txtDateCreated" runat="server" Text='<%# Bind("DateCreated") %>' Width="100px" CssClass="cssTextBox"></asp:TextBox>
                                                <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtDateCreated')});</script>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <td style="width: 25%; height: 34px;">
                                                     Cash Collected ?</td>
                                                 <td style="width:25%">
                                                    <asp:DropDownList ID="ddCashCollected" runat="server" SkinID="skinDdlBox" SelectedValue='<%# Bind("FlagCollected") %>' AppendDataBoundItems="True" Width="100%">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                                 <td style="width: 25%; height: 34px;">
                                                     Status</td>
                                                 <td style="width: 25%; height: 34px;">
                                                    <asp:DropDownList ID="ddStatus" runat="server" Enabled="false" SelectedValue='<%# Bind("BookStatus") %>' SkinID="skinDdlBox" AppendDataBoundItems="True" Width="100%">
                                                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                                        <asp:ListItem Value="Open" Selected="True">Open</asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                             </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                </td>
                                                <td style="width: 25%">
                                                </td>
                                                <td style="width: 25%">
                                                </td>
                                                <td align="right" style="width: 25%">
                                                    <table cellspacing="0">
                                                        <tr width="100%">
                                                            <td style="height: 26px">
                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click" SkinID="skinBtnCancel"></asp:Button>
                                                            </td>
                                                            <td style="height: 26px">
                                                            </td>
                                                            <td style="height: 26px">
                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" OnClientClick="Validator();" CommandName="Insert" SkinID="skinBtnSave"
                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </InsertItemTemplate>
                                </asp:FormView>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%">
                <br />
                <asp:GridView ID="GrdViewBook" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    OnRowCreated="GrdViewBook_RowCreated" Width="100%" EnableViewState="False" DataSourceID="GridSource"
                    AllowPaging="True" DataKeyNames="BookID" EmptyDataText="No Book Details Found. Please try again."
                    OnRowCommand="GrdViewBook_RowCommand" 
                    OnRowDataBound="GrdViewBook_RowDataBound" >
                    <EmptyDataRowStyle CssClass="GrdContent" />
                    <Columns>
                        <asp:BoundField DataField="BookRef" HeaderText="Book Ref" />
                        <asp:BoundField DataField="BookName" HeaderText="Book Name"/>
                        <asp:BoundField DataField="StartEntry" HeaderText="Start Entry" />
                        <asp:BoundField DataField="EndEntry" HeaderText="End Entry" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="BookStatus" HeaderText="Book Status" />
                        <asp:BoundField DataField="DateCreated" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Book Date" />
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select"  />
                            </ItemTemplate>
                            <ItemStyle CssClass="command"></ItemStyle>
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
                <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListBooks" TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetBookForId"
                    TypeName="BusinessLogic" InsertMethod="InsertBook" OnUpdating="frmSource_Updating" 
                    OnInserting="frmSource_Inserting" UpdateMethod="UpdateBook">
                    <UpdateParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="BookID" Type="Int32" />
                        <asp:Parameter Name="BookRef" Type="String" />
                        <asp:Parameter Name="BookName" Type="String" />
                        <asp:Parameter Name="StartEntry" Type="Int32" />
                        <asp:Parameter Name="EndEntry" Type="Int32" />
                        <asp:Parameter Name="Amount" Type="Double" />
                        <asp:Parameter Name="dateCreated" Type="DateTime" />
                        <asp:Parameter Name="FlagCollected" Type="String" />
                        <asp:Parameter Name="BookStatus" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GrdViewBook" Name="BookID" PropertyName="SelectedValue" Type="Int32" />
                        <asp:SessionParameter SessionField="Company" Type="String" Name="connection" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="BookRef" Type="String" />
                        <asp:Parameter Name="BookName" Type="String" />
                        <asp:Parameter Name="StartEntry" Type="Int32" />
                        <asp:Parameter Name="EndEntry" Type="Int32" />
                        <asp:Parameter Name="Amount" Type="Double" />
                        <asp:Parameter Name="dateCreated" Type="DateTime" />
                        <asp:Parameter Name="FlagCollected" Type="String" />
                        <asp:Parameter Name="BookStatus" Type="String" />
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

