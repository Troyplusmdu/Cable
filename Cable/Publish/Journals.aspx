<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="Journals.aspx.cs" Inherits="Journals" Title="Journals" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<script language="javascript" type="text/javascript">

function PrintItem(ID) 
{ 
  window.showModalDialog('PrintJournal.aspx?ID=' + ID ,self,'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;') ;
}
</script>

    <table style="width: 100%;" align="center">
        <tr>
            <td class="LMSHeader" style="width: 918px; height: 20px;">
                <span></span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%; text-align:center" valign="middle">
            <asp:HiddenField ID="hdJournal" runat="server" />
                <table style="width: 100%;border: 1px solid black" align="center" cellpadding="3" cellspacing="5" class="accordionContent">
                                    <tr>
                                        <td colspan="4" align="left" class="searchHeader">
                                            Search
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" class="tblLeft">
                                            Ref No:
                                            <ajX:TextBoxWatermarkExtender ID="TBWERefno" runat="server"   TargetControlID="txtRefno"  WatermarkText="Type Ref No" WatermarkCssClass="watermark" />
                                            <ajX:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtRefno" FilterType="Numbers"/>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtRefno" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%" class="tblLeft">
                                            Narration:
                                        </td>
                                        <td style="width: 35%" class="tblLeft">
                                            <asp:TextBox ID="txtNaration" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                            <ajX:TextBoxWatermarkExtender ID="TBWENarration" runat="server" TargetControlID="txtNaration"
                                                WatermarkText="Type Ref No" WatermarkCssClass="watermark" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" class="tblLeft">
                                            Date:
                                        </td>
                                        <td style="width: 20%" align="left"  >
                                            <asp:TextBox ID="txtDate" runat="server" SkinID="skinTxtBoxMedium" width="100px" MaxLength="10"></asp:TextBox>
                                              <script type="text/javascript"   language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$AccordionPane2_content$txtDate'});</script>
                                          
                                           
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Button ID="cmdSearch" runat="server" OnClick="cmdSearch_Click" Text="Search" SkinID="skinButtonMedium" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                <br />
                <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                    DefaultMode="Edit" DataKeyNames="TransNo" Visible="False" OnItemUpdated="frmViewAdd_ItemUpdated" OnItemCreated="frmViewAdd_ItemCreated" OnItemInserted="frmViewAdd_ItemInserted">
                    <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                        BorderColor="#cccccc" Height="20px" />
                    <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                        VerticalAlign="middle" Height="20px" />
                    <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                        Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                    <EditItemTemplate>
                        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent" style="border: 1px solid black">
                            <tr>
                                <td colspan="4" align="left" class="searchHeader">
                                    Journal
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%;height: 34px;">
                                    Ref No:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRefnum"
                                        ValidationGroup="editVal" Display="Dynamic" EnableClientScript="False" ErrorMessage="Refno is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtRefnum" ValidationGroup="editVal" runat="server" Text='<%# Bind("RefNo") %>'
                                        Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                               <td style="width: 25%">
                                    Narration:*<asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarr"
                                        ValidationGroup="editVal" Display="Dynamic" EnableClientScript="False" ErrorMessage="Narration is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td  style="width: 25%">
                                    <asp:TextBox ID="txtNarr" ValidationGroup="editVal" runat="server" Height="50px"
                                        TextMode="MultiLine" Text='<%# Bind("Narration") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%">
                                    Debtor:*
                                    <asp:RequiredFieldValidator ID="reqDebtor" ErrorMessage="Debtor is mandatory" InitialValue="0"
                                        ControlToValidate="cmbDebtor" runat="server" ValidationGroup="editVal" Display="Dynamic" />
                                </td>
                                <td style="width: 25%">
                                    <ajX:ComboBox ID="cmbDebtor" runat="server" CssClass="ajax__combobox_inputcontainer" AutoPostBack="False"
                                        DropDownStyle="Simple" AutoCompleteMode="Suggest" DataSourceID="srcCreditorDebitor"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                                        ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </ajX:ComboBox>
                                </td>
                                <td style="width: 25%">
                                    Creditor:*
                                    <asp:RequiredFieldValidator ID="reqCreditor" ErrorMessage="Creditor is mandatory"
                                        InitialValue="0" ControlToValidate="cmbCreditor" runat="server" ValidationGroup="editVal"
                                        Display="Dynamic" />
                                </td>
                                </td>
                                <td style="width: 25%">
                                    <ajX:ComboBox ID="cmbCreditor" runat="server" CssClass="ajax__combobox_inputcontainer"
                                        AutoPostBack="False" DropDownStyle="Simple" AutoCompleteMode="Suggest" DataSourceID="srcCreditorDebitor"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                                        ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </ajX:ComboBox>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%">
                                    Amount:*<asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                        Display="Dynamic" EnableClientScript="False" ValidationGroup="editVal" ErrorMessage="Amount is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAmount" runat="server" ValidationGroup="editVal" Text='<%# Bind("Amount") %>'
                                        Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Date:*
                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ValidationGroup="editVal"
                                        ControlToValidate="txtTransDate" Display="Dynamic" EnableClientScript="False"
                                        ErrorMessage="Trasaction Date is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtTransDate" SkinID="skinTxtBoxMedium" runat="server" ValidationGroup="editVal"
                                        Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>' Width="100px"   MaxLength="10"></asp:TextBox>
                                   
                                   <script type="text/javascript"   language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$AccordionPane3_content$frmViewAdd$txtTransDate'});</script>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" ValidationGroup="editVal"
                                        CommandName="Update" Text="Update" OnClick="UpdateButton_Click"></asp:Button>&nbsp;&nbsp;
                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="Cancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent" style="border: 1px solid black">
                            <tr>
                                <td colspan="4" align="left" class="searchHeader">
                                    Edit Journal
                                </td>
                            </tr>
                            <tr style="height: 34px;" class="tblLeft">
                                <td style="width: 25%">
                                    Ref No:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRefnum"
                                        ValidationGroup="addVal" Display="Dynamic" EnableClientScript="False" ErrorMessage="Refno is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtRefnum" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo") %>'
                                        Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Narration: *<asp:RequiredFieldValidator ValidationGroup="addVal" ID="rvNarration"
                                        runat="server" ControlToValidate="txtNarr" Display="Dynamic" EnableClientScript="False"
                                        ErrorMessage="Narration is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td colspan="3" style="width: 25%">
                                    <asp:TextBox ID="txtNarr" ValidationGroup="addVal" runat="server" Height="50px" TextMode="MultiLine"
                                        Text='<%# Bind("Narration") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 34px;" class="tblLeft">
                                <td style="width: 25%">
                                    Debtor: *
                                    <asp:RequiredFieldValidator ID="reqDebtor" ErrorMessage="Debtor is mandatory" InitialValue="0"
                                        ControlToValidate="cmbDebtor" runat="server" ValidationGroup="addVal" Display="Dynamic" />
                                </td>
                                <td style="width: 25%">
                                    <ajX:ComboBox ID="cmbDebtor" runat="server" CssClass="ajax__combobox_inputcontainer" AutoPostBack="False"
                                        DropDownStyle="Simple" AutoCompleteMode="Suggest" DataSourceID="srcCreditorDebitor"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                                        ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </ajX:ComboBox>
                                </td>
                                <td style="width: 25%">
                                    Creditor:*<asp:RequiredFieldValidator ID="reqCreditor" ErrorMessage="Creditor is mandatory"
                                        InitialValue="0" ControlToValidate="cmbCreditor" runat="server" ValidationGroup="addVal"
                                        Display="Dynamic" />
                                </td>
                                <td style="width: 25%">
                                    <ajX:ComboBox ID="cmbCreditor" runat="server" CssClass="ajax__combobox_inputcontainer"
                                        AutoPostBack="False" DropDownStyle="Simple" AutoCompleteMode="Suggest" DataSourceID="srcCreditorDebitor"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                                        ValidationGroup="addVal" OnDataBound="cmbCreditor_DataBound">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </ajX:ComboBox>
                                </td>
                            </tr>
                            <tr style="height: 34px;" class="tblLeft">
                                <td style="width: 25%">
                                    Amount: *<asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                        Display="Dynamic" EnableClientScript="False" ValidationGroup="addVal" ErrorMessage="Amount is mandatory"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAmount" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount") %>'
                                        Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Date: *
                                    <asp:RequiredFieldValidator ID="rvStock" ValidationGroup="addVal" runat="server"
                                        ControlToValidate="txtTransDate" Display="Dynamic" EnableClientScript="False"
                                        ErrorMessage="Trasaction Date is mandatory"></asp:RequiredFieldValidator>
                                    <ajX:CalendarExtender ID="CalendarExtender2" Animated="true" runat="server" TargetControlID="txtTransDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </td>
                                <td style="width: 25%">
                                   <asp:TextBox ID="txtTransDate" SkinID="skinTxtBoxMedium" runat="server" ValidationGroup="editVal"
                                        Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>' Width="100px"   MaxLength="10"></asp:TextBox>
                                   
                                   <script type="text/javascript"   language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$AccordionPane3_content$frmViewAdd$txtTransDate'});</script>
                                </td>
                            </tr>
                            <tr style="height: 34px;" class="tblLeft">
                                <td style="height: 26px">
                                    <td style="height: 26px">
                                    </td>
                                    <td style="height: 26px">
                                    </td>
                                </td>
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" ValidationGroup="addVal" CausesValidation="True"
                                            CommandName="Insert" Text="Save" OnClick="InsertButton_Click"></asp:Button>
                                        &nbsp;&nbsp;
                                        <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Cancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ImageButton ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" ImageUrl="~/App_Themes/DefaultTheme/Images/BtnAddnew.png"></asp:ImageButton>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%; text-align:left">
                <asp:Button ID="cmdListAll" runat="server" OnClick="cmdListAll_Click" Text="List All" SkinID="skinButtonMedium" /> &nbsp;<asp:Button ID="cmdPrint" runat="server" ValidationGroup="editval" Text="Print"
                Enabled="false" OnClick="cmdPrint_Click" />
                <br />
                <br />
                <asp:GridView ID="GrdViewJournal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="TransNo" AllowPaging="True" EmptyDataText="No Journals found for the search criteria"
                    OnRowCreated="GrdViewJournal_RowCreated" OnSelectedIndexChanged="GrdViewJournal_SelectedIndexChanged" OnRowCommand="GrdViewJournal_RowCommand"
                    OnPageIndexChanging="GrdViewJournal_PageIndexChanging" OnRowDeleting="GrdViewJournal_RowDeleting" OnRowDataBound="GrdViewJournal_RowDataBound">
                    <EmptyDataRowStyle CssClass="GrdContent"  />
                    <Columns>
                        <asp:BoundField DataField="Refno" HeaderText="Ref no" />
                        <asp:BoundField DataField="TransDate" HeaderText="Date" />
                        <asp:BoundField DataField="Debi" HeaderText="Debtor" />
                        <asp:BoundField DataField="Cred" HeaderText="Creditor" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />                       
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                            <ajX:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Journal?" runat="server">
                            </ajX:ConfirmButtonExtender>
                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <a href='<%# DataBinder.Eval(Container, "DataItem.TransNo", "javascript:PrintItem({0});") %>'> 
                                <img alt="Fulfilment" border="0" src="App_Themes/DefaultTheme/Images/RightLink1.gif"> </a> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="False" />
                    </Columns>
                    <EmptyDataRowStyle CssClass="GrdContent" />
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" runat="server" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                            AutoPostBack="true" SkinID="skinPagerDdlBox">
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
                <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorJ"
                    TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetJournalForId"
                    TypeName="BusinessLogic" InsertMethod="InsertJournal" OnUpdating="frmSource_Updating"
                    OnInserting="frmSource_Inserting"   UpdateMethod="UpdateJournal">
                    <UpdateParameters>
                        <asp:Parameter Name="TransNo" Type="Int32" />
                        <asp:Parameter Name="RefNo" Type="String" />
                        <asp:Parameter Name="TransDate" Type="DateTime" />
                        <asp:Parameter Name="DebitorID" Type="Int32" />
                        <asp:Parameter Name="CreditorID" Type="Int32" />
                        <asp:Parameter Name="Amount" Type="Double" />
                        <asp:Parameter Name="Narration" Type="String" />
                        <asp:Parameter Name="VoucherType" Type="String" />
                        <asp:Parameter Name="sPath" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GrdViewJournal" Name="TransNo" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="hdDataSource" Name="ConStr" Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="RefNo" Type="String" />
                        <asp:Parameter Name="TransDate" Type="DateTime" />
                        <asp:Parameter Name="DebitorID" Type="Int32" />
                        <asp:Parameter Name="CreditorID" Type="Int32" />
                        <asp:Parameter Name="Amount" Type="Double" />
                        <asp:Parameter Name="Narration" Type="String" />
                        <asp:Parameter Name="VoucherType" Type="String" />
                        <asp:Parameter Name="sPath" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListJournal" TypeName="BusinessLogic"
                    DeleteMethod="DeleteJournal" OnDeleting="GridSource_Deleting">
                    <DeleteParameters>
                        <asp:Parameter Name="TransNo" Type="Int32" />
                        <asp:Parameter Name="sPath" Type="String" />
                    </DeleteParameters>
                    <%-- <SelectParameters>
                <asp:Parameter Name="sRefno" Type="String" />
                <asp:Parameter Name="sNaration" Type="String" />
                <asp:Parameter Name="sDate" Type="String" />
                <asp:Parameter Name="sPath" Type="String" />
            </SelectParameters>--%>
                </asp:ObjectDataSource>
                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                <asp:HiddenField ID="hdDataSource" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    
</asp:Content>
