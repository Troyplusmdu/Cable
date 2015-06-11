<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="Payments.aspx.cs" Inherits="Payments" Title="Payments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<script language="javascript" type="text/javascript">

function PrintItem(ID) 
{ 
  window.showModalDialog('./PrintPayment.aspx?ID=' + ID ,self,'dialogWidth:1000px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;') ;
}


function AdvancedTest(id) 
{ 
       
       var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tblBank');   
       var adv = document.getElementById('ctl00$cplhControlPanel$hidAdvancedState') ; 
       var grd = document.getElementById("<%= frmViewAdd.ClientID %>");  
       
       var rdoArray = grd.getElementsByTagName("input");  
       
       
       for(i=0;i<=rdoArray.length-1;i++)  
        {  
            //alert(rdoArray[i].type);
            if(rdoArray[i].type == 'radio')
            {
                
                if( rdoArray[i].value == 'Cash' && rdoArray[i].checked == true)
                {
                    panel.className = "hidden" ; 
                    adv.value = panel.className ; 
                }
                else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) 
                { 
                    panel.className = "AdvancedSearch" ; 
                    adv.value = panel.className ; 
                }        
                
            }
        }  

      /* if (panel.className == "AdvancedSearch") 
       { 
            panel.className = "hidden" ; 
            adv.value = panel.className ; 
       } 
       else if (panel.className == "hidden") 
       { 
            panel.className = "AdvancedSearch" ; 
            adv.value = panel.className ; 
       } 
       else
       {
            panel.className = "AdvancedSearch" ; 
            adv.value = panel.className ; 
       }*/
} 
<!-- 
function Advanced() 
{ 
        var table = document.getElementById('tblBank'); 
        var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState') ; 
        
        var tr = table.getElementsByTagName('tr') ; 
        
        for (i = 0; i < tr.length; i++) 
        { 
                if (tr[i].className == "AdvancedSearch") 
                { 
                        tr[i].className = "hidden" ; 
                        adv.value = tr[i].className ; 
                } 
                else if (tr[i].className == "hidden") 
                { 
                        tr[i].className = "AdvancedSearch" ; 
                        adv.value = tr[i].className ; 
                }                               
        } 
} 
//--> 
    </script>
    <table style="width: 100%;" align="center">
        <tr style="width: 100%">
            <td style="width: 100%">
                <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                    style="border: 1px solid black">
                    <tr>
                        <td colspan="4" align="left" class="searchHeader">
                            Search
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="width: 25%" class="tblLeft">
                            Ref No/ Pay To: *
                            <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
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
                                <asp:ListItem Value="RefNo">Ref No</asp:ListItem>
                                <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                <asp:ListItem Value="LedgerName">Paid To</asp:ListItem>
                                <asp:ListItem Value="Narration">Narration</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinButtonMedium" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                    OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit" DataKeyNames="TransNo"
                    OnItemUpdated="frmViewAdd_ItemUpdated" OnItemCreated="frmViewAdd_ItemCreated"
                    Visible="False" OnItemInserting="frmViewAdd_ItemInserting" EmptyDataText="No Records"
                    OnItemInserted="frmViewAdd_ItemInserted">
                    <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle" BorderColor="#cccccc" Height="20px" />
                    <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc" VerticalAlign="middle" Height="20px" />
                    <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad" Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                    <EditItemTemplate>
                        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                            style="border: 1px solid black">
                            <tr>
                                <td colspan="4" align="left" class="searchHeader">
                                    Subscriber Expenses
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%">
                                    Ref No: *
                                    <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                        Display="Dynamic" EnableClientScript="True">RefNo is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' Width="100%"
                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    Date: *
                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                        Display="Dynamic" EnableClientScript="True">Trasaction Date is mandatory</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                        ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                </td>
                                <td style="width: 25%">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left">
                                        <tr>
                                            <td style="width: 88%">
                                                <asp:TextBox ID="txtTransDate" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                    Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                            </td>
                                            <td style="width: 12%; text-align: center">
                                                <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtTransDate')});</script>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%">
                                    Payed To: *
                                    <asp:CompareValidator ID="cvPayedTo" runat="server" ControlToValidate="ComboBox2"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Payed To is Mandatory"
                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                </td>
                                <td style="width: 25%">
                                    <cc1:ComboBox ID="ComboBox2" runat="server" CssClass="ajax__combobox_inputcontainer"
                                        AutoPostBack="False" DropDownStyle="Simple" AutoCompleteMode="Suggest" DataSourceID="srcCreditorDebitor"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                                        OnDataBound="ComboBox2_DataBound">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </cc1:ComboBox>
                                </td>
                                <td style="width: 25%">
                                    Amount: *<asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                        Display="Dynamic" EnableClientScript="True">Amount is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%; height: 34px;">
                                    Payment Made By: *<asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%; height: 34px;">
                                    <asp:RadioButtonList ID="chkPayTo" onclick="javascript:AdvancedTest(this.id);" runat="server"
                                        AutoPostBack="false" Width="100%" OnDataBound="chkPayTo_DataBound" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                        <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="Cheque"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2" style="width: 50%">
                                    <asp:Panel ID="PanelBank" runat="server">
                                        <table width="100%" id="tblBank" class="" runat="server">
                                            <tr>
                                                <td style="width: 50%">
                                                    Bank Name: *
                                                    <asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks" Display="Dynamic"
                                                        EnableClientScript="False" ErrorMessage="Phase is Mandatory" Operator="GreaterThan"
                                                        ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddBanks" runat="server" OnDataBound="ddBanks_DataBound" DataSourceID="srcBanks"
                                                        DataTextField="LedgerName" DataValueField="LedgerID" Width="98%" AppendDataBoundItems="True">
                                                        <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    Cheque No: *
                                                    <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                        Display="Dynamic" EnableClientScript="False">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' Width="100%"
                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%; height: 34px;">
                                    Expense Type: *<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="rdExpense" Display="Dynamic" EnableClientScript="True" ErrorMessage="Expense Type is mandatory.">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%; height: 34px;">
                                    <asp:RadioButtonList ID="rdExpense" runat="server" AutoPostBack="false" Width="100%"
                                        OnDataBound="rdExpense_DataBound">
                                        <asp:ListItem Text="Capex" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="Opex"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 25%">
                                    Narration: *<asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarration"
                                        Display="Dynamic" EnableClientScript="True">Narration is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtNarration" runat="server" Height="50px" TextMode="MultiLine"
                                        Text='<%# Bind("Narration") %>' Width="110%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tblLeft">
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 25%">
                                    <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorJ"
                                        TypeName="BusinessLogic">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                                <td align="right" style="width: 25%" valign="bottom">
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
                    </EditItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                    <InsertItemTemplate>
                        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="accordionContent"
                            style="border: 1px solid black">
                            <tr>
                                <td colspan="4" align="left" class="searchHeader">
                                    Subscriber Expenses
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="tblLeft">
                                    Ref No: *
                                    <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                        Display="Dynamic" EnableClientScript="True">RefNo is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' Width="100%"
                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    Date: *
                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                        Display="Dynamic" EnableClientScript="True">Trasaction Date is mandatory</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                        ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left">
                                        <tr>
                                            <td style="width: 88%">
                                                <asp:TextBox ID="txtTransDate" runat="server" Text='<%# Bind("TransDate") %>' Width="100%"
                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                            </td>
                                            <td style="width: 12%; text-align: center">

                                                <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtTransDate')});</script>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="tblLeft">
                                    Payed To: *
                                    <asp:CompareValidator ID="cvPayedTo" runat="server" ControlToValidate="ComboBox2"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Payed To is Mandatory"
                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <cc1:ComboBox ID="ComboBox2" runat="server" CssClass="ajax__combobox_inputcontainer"
                                        AutoPostBack="False" DropDownStyle="Simple" AutoCompleteMode="Suggest" DataSourceID="srcCreditorDebitor"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </cc1:ComboBox>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    Amount: *<asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                        Display="Dynamic" EnableClientScript="True">Amount is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; height: 34px;" class="tblLeft">
                                    Payment Made By: *<asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%; height: 34px;" class="tblLeft">
                                    <asp:RadioButtonList ID="chkPayTo" runat="server" OnDataBound="chkPayTo_DataBound"
                                        onclick="javascript:AdvancedTest(this.id);" AutoPostBack="false" Width="100%"
                                        OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                        <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="Cheque"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2" style="width: 50%" class="tblLeft">
                                    <asp:Panel ID="PanelBank" runat="server">
                                        <table width="100%" id="tblBank" class="" runat="server">
                                            <tr>
                                                <td style="width: 50%" class="tblLeft">
                                                    Bank Name: *
                                                    <asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks" Display="Dynamic"
                                                        EnableClientScript="False" ErrorMessage="Phase is Mandatory" Operator="GreaterThan"
                                                        ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                                <td style="width: 50%" class="tblLeft">
                                                    <asp:DropDownList ID="ddBanks" runat="server" SelectedValue='<%# Bind("CreditorID") %>'
                                                        DataSourceID="srcBanks" DataTextField="LedgerName" DataValueField="LedgerID"
                                                        Width="98%" AppendDataBoundItems="True">
                                                        <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%" class="tblLeft">
                                                    Cheque No: *
                                                    <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                        Display="Dynamic" EnableClientScript="False">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 50%" class="tblLeft">
                                                    <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' Width="100%"
                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; height: 34px;" class="tblLeft" class="tblLeft">
                                    Expense Type: *<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="rdExpense" Display="Dynamic" EnableClientScript="True" ErrorMessage="Expense Type is mandatory.">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%; height: 34px;" class="tblLeft">
                                    <asp:RadioButtonList ID="rdExpense" runat="server" AutoPostBack="false" Width="100%"
                                        OnDataBound="rdExpense_DataBound">
                                        <asp:ListItem Text="Capex" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="Opex"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    Narration: *<asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarration"
                                        Display="Dynamic" EnableClientScript="True">Narration is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:TextBox ID="txtNarration" runat="server" Height="50px" TextMode="MultiLine"
                                        Text='<%# Bind("Narration") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="tblLeft">
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorJ"
                                        TypeName="BusinessLogic">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                                <td align="right" style="width: 25%" class="tblLeft">
                                    <table cellspacing="0">
                                        <tr width="100%">
                                            <td style="height: 26px">
                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                            </td>
                                            <td style="height: 26px" class="tblLeft">
                                            </td>
                                            <td style="height: 26px" class="tblLeft">
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
                <table style="width: 100%;" cellpadding="0" cellspacing="0" align="center" >
                    <tr>
                        <td style="width: 100%">
                            <br />
                            <asp:GridView ID="GrdViewPayment" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewPayment_RowCreated" Width="100%" DataSourceID="GridSource"
                                AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Payments found for the search criteria"
                                OnRowCommand="GrdViewPayment_RowCommand" OnRowDataBound="GrdViewPayment_RowDataBound"
                                OnRowDeleting="GrdViewPayment_RowDeleting" OnRowDeleted="GrdViewPayment_RowDeleted"
                                PageSize="20">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                                    <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                                    <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                                    <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                                    <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Narration" HeaderText="Narration" />
                                    <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
                                    <asp:TemplateField ItemStyle-CssClass="command">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Payment?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container, "DataItem.TransNo", "javascript:PrintItem({0});") %>'>
                                                <img alt="Fulfilment" border="0" src="App_Themes/DefaultTheme/Images/RightLink1.gif">
                                            </a>
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
                </table>
            </td>
        </tr>
        <tr width="100%">
            <td style="width: 918px" align="left">
                <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListPayments"
                    TypeName="BusinessLogic" DeleteMethod="DeletePayment" OnDeleting="GridSource_Deleting">
                    <DeleteParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="TransNo" Type="Int32" />
                        <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                    </DeleteParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetPaymentForId"
                    TypeName="BusinessLogic" InsertMethod="InsertPayment" OnUpdating="frmSource_Updating"
                    OnInserting="frmSource_Inserting" UpdateMethod="UpdatePayment">
                    <UpdateParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="TransNo" Type="Int32" />
                        <asp:Parameter Name="RefNo" Type="String" />
                        <asp:Parameter Name="TransDate" Type="DateTime" />
                        <asp:Parameter Name="DebitorID" Type="Int32" />
                        <asp:Parameter Name="CreditorID" Type="Int32" />
                        <asp:Parameter Name="Amount" Type="Double" />
                        <asp:Parameter Name="Narration" Type="String" />
                        <asp:Parameter Name="VoucherType" Type="String" />
                        <asp:Parameter Name="ChequeNo" Type="String" />
                        <asp:Parameter Name="Paymode" Type="String" />
                        <asp:Parameter Name="ExpenseType" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GrdViewPayment" Name="TransNo" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:SessionParameter SessionField="Company" Type="String" Name="connection" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                        <asp:Parameter Name="RefNo" Type="String" />
                        <asp:Parameter Name="TransDate" Type="DateTime" />
                        <asp:Parameter Name="DebitorID" Type="Int32" />
                        <asp:Parameter Name="CreditorID" Type="Int32" />
                        <asp:Parameter Name="Amount" Type="Double" />
                        <asp:Parameter Name="Narration" Type="String" />
                        <asp:Parameter Name="VoucherType" Type="String" />
                        <asp:Parameter Name="ChequeNo" Type="String" />
                        <asp:Parameter Name="PaymentMode" Type="String" />
                        <asp:Parameter Name="ExpenseType" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
            </td>
        </tr>
    </table>
    <br />
    <input type="hidden" id="hidAdvancedState" runat="server" />
</asp:Content>
