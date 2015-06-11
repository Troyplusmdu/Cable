<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="Purchase.aspx.cs" Inherits="Purchase" Title="Purchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
  <script type="text/javascript" language="JavaScript">
    function confirm_delete()
    {
      if (confirm("Are you sure you want to delete the purchase bill ?")==true)
        return true;
      else
        return false;
    }
    function unl()
    {
        document.getElementById("hdDel").value="BrowserClose";
        document.form1.submit();
    }
    
    function setDelFlag()
    {
        document.getElementById("delFlag").value="1";
    }
  </script>
  <script type="text/javascript" language="JavaScript">
  function ShowMyModalPopup(customerid)
{ 
 var modal = $find('ModalPopupExtender1'); 
 modal.show(); 
 WebService.FetchOneCustomer(customerid,DisplayResult);
}
 function HideModalPopup()
{
  var modal = $find('ModalPopupExtender1');
  modal.hide();
}
function fnClickUpdate(sender, e)
{
  __doPostBack(sender,e);
}
  </script>
<br />
<div>
        <ajX:Accordion ID="Accordion1" FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
            runat="server" AutoSize="None" SelectedIndex="0" FadeTransitions="True" TransitionDuration="250"
            >
            <Panes>
                <ajX:AccordionPane ID="AccordionPane3" runat="server">
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
                                    Bill no:
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" Width="100%" runat="server"
                                        SkinID="skinTxtBoxGrid" MaxLength="8"></asp:TextBox>
                                    <ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtBillnoSrc"
                                        WatermarkText="Search Bill No" WatermarkCssClass="watermark" />
                                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoSrc"
                                        FilterType="Numbers" />
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:Button ValidationGroup="search" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                        Text="Search" />
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:RequiredFieldValidator ValidationGroup="search" ID="rqSearchBill" runat="server"
                                        Text="Search Box is Empty" ControlToValidate="txtBillnoSrc"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </Content>
                </ajX:AccordionPane>
            </Panes>
        </ajX:Accordion>
       
        <asp:Panel ID="purchasePanel" runat="server" Visible="false">
            <table width="100%" class="accordionContent alignCenter">
                <tr>
                    <td colspan="4" class="accordionHeader">
                        Purchase
                    </td>
                </tr>
                 <tr style="height: 25px">
                    <td style="width: 25%" class="tblLeft">
                        Bill No:
                        <br />
                        
                    </td>
                    <td style="width: 25%" class="tblLeft">
                        <asp:TextBox ID="txtBillno" runat="server" ValidationGroup="purchaseval" SkinID="skinTxtBoxGrid"
                            MaxLength="8"></asp:TextBox>
                       
                        
                    </td>
                   
                    <td style="width: 25%" class="tblLeft">
                        Bill Date : *
                         </td>
                    <td style="width: 25%" class="tblLeft">
                        <asp:TextBox ID="txtBillDate" runat="server" SkinID="skinTxtBoxMedium" Width="100px" MaxLength="10" ValidationGroup="purchaseval"></asp:TextBox>
                      <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$txtBillDate'});</script>


                    </td>
                </tr>
                <tr>
                   <td><asp:RequiredFieldValidator ID="rvBill" runat="server" ControlToValidate="txtBillno"
                            Display="Dynamic" EnableClientScript="False" ValidationGroup="purchaseval">Billno is mandatory</asp:RequiredFieldValidator></td>
                   <td> <ajX:TextBoxWatermarkExtender ID="TBWERefno" runat="server" TargetControlID="txtBillno"
                            WatermarkText="Bill No" WatermarkCssClass="watermark" /></td>
                   <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" EnableClientScript="False" ValidationGroup="purchaseval">BillDate is mandatory</asp:RequiredFieldValidator>
                        <ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtBillDate" WatermarkText="Bill Date" WatermarkCssClass="watermark" />
                  </td>
                   <td></td>
                </tr>
                 <tr >
                    <td style="width: 25%" class="tblLeft">
                        <br />
                    Supplier:
                        <br />
                     
                    </td>
                    <td style="width: 25%" class="tblLeft">
                        <ajX:ComboBox ID="cmbSupplier" runat="server" CssClass="ajax__combobox_inputcontainer"
                            AutoPostBack="false" DropDownStyle="DropDown" ValidationGroup="purchaseval" AutoCompleteMode="Suggest"
                            DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true">
                            <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                        </ajX:ComboBox>
                        <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                            TypeName="BusinessLogic"></asp:ObjectDataSource>
                    </td>
                    <td style="width: 25%" class="tblLeft">
                        Paymode:
                        <br />
                        
                    </td>
                    <td style="width: 25%" class="tblLeft">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                        <ajX:ComboBox ID="cmdPaymode" runat="server" CssClass="ajax__combobox_inputcontainer" AutoPostBack="true"
                            DropDownStyle="DropDown" AutoCompleteMode="Suggest" ValidationGroup="purchaseval"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="cmdPaymode_SelectedIndexChanged">
                            <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                             
                       
                        </ajX:ComboBox> 
                          </ContentTemplate>
        </asp:UpdatePanel> 
                    </td>
                </tr>
                <tr>
                    <td><asp:RequiredFieldValidator ID="reqSuppllier" Text="Supplier is mandatory" InitialValue="0"
                            ControlToValidate="cmbSupplier" runat="server" ValidationGroup="purchaseval" /></td>
                            <td></td>
                            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="Paymode is mandatory"
                            InitialValue="0" ControlToValidate="cmdPaymode" runat="server" ValidationGroup="purchaseval" /></td>
                            <td></td>
                </tr>
                
                <tr>
                <td style="width: 25%" class="tblLeft">Amount : *</td>
                <td class="tblLeft">  <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBoxMedium" Width="100px" MaxLength="10" ValidationGroup="purchaseval"></asp:TextBox></td>
               <td colspan="2" class="tblLeft" ><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="Amount is mandatory"
                            ControlToValidate="txtAmount" runat="server" ValidationGroup="purchaseval" /></td>
                </tr>
                <tr>
                
                <td colspan="4" class="tblLeft" >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlBank" runat="server" Visible="false">
                 <table width="100%"><tr>
                    <td style="width: 25%" class="tblLeft">
                        <asp:Label ID="lblCheque" Text="Cheque No:" runat="server"></asp:Label>
                        <br />
                       
                    </td>
                    <td style="width: 25%" class="tblLeft">
                        <asp:TextBox ID="txtChequeNo" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                        
                    </td>
                    <td style="width: 25%" class="tblLeft">
                        <asp:Label ID="lblBankname" Text="Bank name:" runat="server"></asp:Label>
                        <br />
                       
                    </td>
                    <td style="width: 25%" class="tblLeft">
                        <ajX:ComboBox ID="cmbBankName" runat="server" CssClass="ajax__combobox_inputcontainer"
                            AutoPostBack="False" DropDownStyle="DropDown" AutoCompleteMode="Suggest" 
                            DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                            ValidationGroup="purchaseval">
                            <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                        </ajX:ComboBox>
                        <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                          <SelectParameters>
                               <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    </tr>
                    <tr>
                    <td> <asp:RequiredFieldValidator ID="rvCheque" Text="Chequeno is mandatory" ControlToValidate="txtChequeNo"
                            runat="server" ValidationGroup="purchaseval" Enabled="false" /></td>
                    <td><ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtChequeNo"
                            WatermarkText="Cheque No" WatermarkCssClass="watermark" />
                        <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtChequeNo"
                            FilterType="Numbers" /></td>
                    <td> <asp:RequiredFieldValidator ID="rvbank" ErrorMessage="Bankname is mandatory" InitialValue="0"
                            ControlToValidate="cmbBankName" runat="server" ValidationGroup="purchaseval"
                            Enabled="false" /></td>
                    <td></td>
                    </tr>
                    </table>
                </asp:Panel>
                  </ContentTemplate>
        </asp:UpdatePanel>
                   
                </td>
                </tr>
                  <tr>
                <td class ="tblLeft">Sales Return:</td>
                <td class ="tblLeft"><asp:DropDownList ID="drpSalesReturn" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSalesReturn_SelectedIndexChanged" CssClass="drpDownList" >
                <asp:ListItem Text="No" Value="No" Selected></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                </asp:DropDownList></td>
                 <td class ="tblLeft" style="width: 25%">
                Sales Return Reason:
                </td>
                <td style="width: 25%" class ="tblLeft">
                
                <asp:TextBox ID="txtSRReason" runat="server" TextMode="MultiLine" Width="175px" Height="40px" ></asp:TextBox>
                
                </td>
                </tr>
                <tr>
                <td colspan="3"></td>
                <td><asp:RequiredFieldValidator ID="rqSalesReturn" runat="server" CssClass="lblFont" Text="Sales Return Reason is mandatory"
                           Enabled="false"   ControlToValidate="txtSRReason" ValidationGroup="purchaseval" /></td>
                </tr>
            </table>
            <br />
            <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: Verdana;
                font-size: 12px;" Text=""></asp:Label>
                 
            <table class="tblLeft" width="100%" cellpadding="4" border="0" style="border: 1px solid black;">
                <tr style="background-color: Navy; color: White;">
                    <td width="10%">
                        Asset Code
                    </td>
                    <td width="15%">
                        Asset Description
                    </td>
                    <td width="10%">
                        Asset Category
                    </td>
                    <td width="20%">
                        Location
                    </td>
                    <td width="15%">
                        Area 
                    </td>
                    <td width="5%">
                        Qty 
                    </td>
                    <td width="35%">
                        Serial No
                    </td>
                    
                </tr>
                <tr>
                    <td valign="top">
                    <asp:UpdatePanel ID="upd" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList autopostback="true"  OnSelectedIndexChanged="drpAssetCode_SelectedIndexChanged"   ValidationGroup="product" ID="drpAssetCode" AppendDataBoundItems="true" DataValueField = "AssetCode" DataTextField="AssetCode"   runat="server"  width="125px"   CssClass="drpDownListMedium">
                         <asp:ListItem Value="0">--Select Code--</asp:ListItem>
                    </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    
                       
                    </td>
                    <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3"  runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblAssetDesc" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4"  runat="server" >
                    <ContentTemplate>
                        <asp:Label ID="lblAssetCat" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td valign="top">
                       <asp:TextBox ID="txtLocation" runat="server" MaxLength="30" Width="125px" Height="16px" CssClass="txtBox" ></asp:TextBox>
                   
                    </td>
                    <td valign="top"> 
                         <asp:DropDownList ID="drpAssetArea" runat="server" AppendDataBoundItems="true" DataTextField="area" DataValueField="area"  width="125px" Height="21px"  CssClass="drpDownListMedium">
                        <asp:ListItem Value="0">--Select Area--</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                   
                    <td valign="top">
                       <asp:TextBox ID="txtQtyAdd" runat="server" Width="60px" SkinID="skinTxtBoxSmall" ValidationGroup="product"></asp:TextBox>
                       
                    </td>
                    <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel11"  runat="server" >
                    <ContentTemplate>
                        <asp:TextBox ID="txtSerialNo" SkinID="skinTxtBox" MaxLength="30" runat="server" Width="200px" ValidationGroup="product"></asp:TextBox>&nbsp;
                      &nbsp;<br /><asp:Button SkinID ="skinButtonMedium"  ID="btnSerial" runat="server" Text="Add" OnClick="btnSerial_Click" />&nbsp;<asp:Button SkinID ="skinButtonMedium"  ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" /><br />
                      <br /><asp:ListBox SelectionMode="Multiple"  ID="lstSerial" runat="server" SkinID="skinLstBoxMedium"   Width="200px" CssClass="tblLeft" ></asp:ListBox>
                      </ContentTemplate> 
                      </asp:UpdatePanel> 
                    </td>
                </tr>
                <tr>
                <td colspan="3">
                 <asp:RequiredFieldValidator ID="rv1" ValidationGroup="product" Text="Asset Code Selection is mandatory"
                            InitialValue="0" ControlToValidate="drpAssetCode" runat="server"  />
                        <asp:ObjectDataSource ID="objSrc" runat="server" SelectMethod="ListAssetCode"
                            TypeName="BusinessLogic"></asp:ObjectDataSource>
                     
                </td>
                <td>
                
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLocation" CssClass="tblLeft"
                             ValidationGroup="product">Location is mandatory</asp:RequiredFieldValidator>
                </td>
                <td colspan="3" >
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="Quantity is mandatory"
                            ControlToValidate="txtQtyAdd" runat="server" ValidationGroup="product"  />
                             <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtQtyAdd"
                        FilterType="Numbers"  />
                          
                </td>
                
                </tr>
                <tr>
                    <td colspan="6" align="right">
                  
                        <asp:Button ID="cmdSaveProduct" Style="width: 100px;" runat="server" Text="Add Product"
                            OnClick="cmdSaveProduct_Click" ValidationGroup="product" />
                    </td>
                </tr>
            </table>
             
            <br />
            <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
            <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
            <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
            <asp:HiddenField ID="hdMode" runat="server" Value="New" />
            <asp:Panel ID="Panel1" runat="server" BackColor="LightGray" Height="269px" Width="428px" style="display:none">  
            Serial Number : <asp:TextBox ID="txtSerial" runat="server" MaxLength="30"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Update" OnClick=" Button1_Click" />
<asp:Button ID="Button2" runat="server" Text="Cancel" /> 
            </asp:Panel> 
            <ajX:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="Button2" OnCancelScript="HideModalPopup()"> </ajX:ModalPopupExtender>  
            <asp:Panel ID="pnlSales" runat="server">
            <asp:GridView ID="GrdViewItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                Width="100%" DataKeyNames="AssetCode" AllowPaging="True" ShowFooter="True" OnRowEditing="GrdViewItems_RowEditing"
                OnRowCancelingEdit="GrdViewItems_RowCancelingEdit" BackColor="White" BorderColor="#DEDFDE"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                OnPageIndexChanging="GrdViewItems_PageIndexChanging" OnRowDataBound="GrdViewItems_RowDataBound"
                OnRowUpdating="GrdViewItems_RowUpdating" OnRowDeleting="GrdViewItems_RowDeleting"  
                OnRowCreated="GrdViewItems_RowCreated" PageSize="20">
                <FooterStyle BackColor="#CCCC99" />
                <RowStyle BackColor="#F7F7DE" />
                <EmptyDataRowStyle CssClass="GrdContent" />
                <Columns>
                  
                    <asp:TemplateField HeaderText="Product Code" HeaderStyle-Width="5%" >
                        <ItemTemplate>
                            <%# Eval("AssetCode")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" ID="lblCode" Text='<%# Eval("AssetCode")%>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <%# Eval("AssetDesc")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" ID="lblProdname" Text='<%# Eval("AssetDesc")%>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asset Category" HeaderStyle-Width="30%">
                        <ItemTemplate>
                            <%# Eval("CategoryDescription")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" ID="lblDesc" Text='<%# Eval("CategoryDescription")%>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                   
                  
                    <asp:TemplateField HeaderText="Location" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <%# Eval("AssetLocation")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                              <asp:TextBox ID="txtLocation" runat="server" Text='<%# Eval("AssetLocation") %>' SkinID="skinTxtBoxSmall" MaxLength="30" ></asp:TextBox>
                             
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Area" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <%# Eval("AssetArea")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                              <asp:TextBox ID="txtArea" runat="server" Text='<%# Eval("AssetArea") %>' SkinID="skinTxtBoxSmall" MaxLength="30" ></asp:TextBox>
                             
                        
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>
                  
                   <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <%# Eval("Qty")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Qty") %>' SkinID="skinTxtBoxSmall"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="editVal" runat="server" ID="rvq" ControlToValidate="txtQty"
                                ErrorMessage="Quantity is Required"></asp:RequiredFieldValidator>
                                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtQty"
                        FilterType="Numbers" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>
                      <%--<asp:TemplateField HeaderText="SerialNo" HeaderStyle-Width="10%">
                        <ItemTemplate>
                          <asp:TextBox ID="txtSer" TextMode="MultiLine"   runat="server" Text='<%# Eval("SerialNo") %>' SkinID="skinTxtBox"></asp:TextBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSer" TextMode="MultiLine"   runat="server" Text='<%# Eval("SerialNo") %>' SkinID="skinTxtBox"></asp:TextBox>
                            
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                   
                    <asp:CommandField HeaderStyle-Width="4%" EditImageUrl="~/App_Themes/DefaultTheme/Images/edit.png" UpdateImageUrl="~/App_Themes/DefaultTheme/Images/document-save.png"  CancelImageUrl="~/App_Themes/DefaultTheme/Images/dialog-cancel.png"  ButtonType="Image"  ShowEditButton="false" ValidationGroup="editVal" />
                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="4%">
                            <ItemTemplate>
                            <ajX:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to delete this product from purchase?" runat="server">
                            </ajX:ConfirmButtonExtender>
                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                    <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Image"  DeleteImageUrl="~/App_Themes/DefaultTheme/Images/delete.png" />--%>
                </Columns>
                <%--<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />--%>
                <PagerTemplate>
                    Goto Page
                    <asp:DropDownList ID="ddlPageSelector2" runat="server" OnSelectedIndexChanged="ddlPageSelector2_SelectedIndexChanged"
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
            
            </asp:Panel>
            <br />
          
           
          
        </asp:Panel>
                <br />   
        <asp:ImageButton ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" ImageUrl="~/App_Themes/DefaultTheme/Images/BtnAddnew.png"></asp:ImageButton>
        <br />
         
        <asp:Panel ID="PanelCmd" runat="server" Visible="false">
            <asp:Button ID="cmdSave" ValidationGroup="purchaseval" runat="server" Text="Save"
                OnClick="cmdSave_Click" />
            <asp:Button ID="cmdUpdate" ValidationGroup="purchaseval" runat="server" Text="Update"
                OnClick="cmdUpdate_Click" />
            <asp:Button ID="cmdDelete" ValidationGroup="purchaseval" runat="server" Text="Delete"
                OnClick="cmdDelete_Click" OnClientClick="return confirm_delete()" />
            <asp:Button ID="cmdPrint" ValidationGroup="purchaseval" runat="server" Text="Print"
                Enabled="false" OnClick="cmdPrint_Click" Visible="false" />
                            <asp:Button ID="btnCancel"  runat="server" Text="Cancel"
               OnClick="btnCancel_Click"  />
        </asp:Panel>
        <br />
        
        <asp:Panel ID="PanelBill" runat="server" >
            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                Width="100%" DataKeyNames="PurchaseID" AllowPaging="True" EmptyDataText="No Purchase Details found for the search criteria"
                OnPageIndexChanging="GrdViewPurchase_PageIndexChanging" OnRowCommand="GrdViewPurchase_RowCommand"
                OnRowEditing="GrdViewPurchase_RowEditing" OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged"
                PageSize="20" OnRowCreated="GrdViewPurchase_RowCreated" OnRowDataBound="GrdViewPurchase_RowDataBound">
                <EmptyDataRowStyle CssClass="GrdContent" />
                <Columns>
                    <asp:BoundField DataField="PurchaseID" Visible="false" />
                    <asp:BoundField DataField="Billno" HeaderText="Bill no" />
                    <asp:BoundField DataField="BillDate" HeaderText="Date" />
                    <asp:TemplateField HeaderText="Paymode">
                    <ItemTemplate>
                     <asp:Label ID="lblPaymode" runat="server" ></asp:Label>
                    </ItemTemplate> 
                    </asp:TemplateField> 
                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" />
                    <asp:BoundField DataField="Chequeno" HeaderText="Chequeno" />
                    <asp:BoundField DataField="Creditor" HeaderText="Creditor" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" />
                     <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" />
                    <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason" />
                    <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="#3464cc" SelectText="View and Modify">
                        <ControlStyle ForeColor="#3464CC"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <EmptyDataRowStyle CssClass="GrdContent" />
                <PagerTemplate>
                 Goto Page
                    <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                        runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
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
            <input type="hidden" value="" id="hdDel" runat="server" />
            <input type="hidden" id="delFlag" value="0" runat="server" />
            <asp:HiddenField ID="hdToDelete" Value="0" runat="server" />
            <br />
        </asp:Panel>
      
    </div>
</asp:Content>

