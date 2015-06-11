<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ModifyInstCash.aspx.cs" Inherits="ModifyInstCash" Title="Modify Installation/Re-Inst Cash" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<script language="javascript" src="Scripts/date.js" type="text/javascript"></script>
<script language="javascript" src="Scripts/JScript.js" type="text/javascript"></script>

<table style="width: 100%;" align="center">
     <tr>
         <td colspan="5" align="left">
           <uc1:errordisplay ID="errorDisplay" runat="server" />
         </td>
     </tr>
</table>
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
<table style="width: 100%;" align="center">
     <tr>
        <td class="SectionHeader" colspan="5">
                <span>Modify Inst\Re-Inst Cash</span>
        </td>
    </tr>
     <tr>
        <td class="tblLeft" style="width: 20%;padding-left:15px">
            Book:
            <asp:CompareValidator ID="reqBook" ErrorMessage="Please select the Book and Try again!" Text="*" Operator="GreaterThan"
             ControlToValidate="ddBook" ValueToCompare="0" runat="server" Display="Dynamic" />
        </td>
        <td style="width: 20%">
            <asp:DropDownList ID="ddBook" runat="server" AppendDataBoundItems="True" DataSourceID="srcBook" SkinID="skinDdlBox" DataTextField="BookName" DataValueField="BookID" 
                Width="100%" onselectedindexchanged="ddBook_SelectedIndexChanged">
                    <asp:ListItem Value="0"> -- Select Book -- </asp:ListItem>
             </asp:DropDownList>
        </td>
        <td class="tblLeft" style="width: 20%;padding-left:15px">
            Bill No:
        </td>
        <td style="width: 20%">
            <asp:TextBox ID="txtBillNo" runat="server" SkinID="skinTxtBoxGrid" Width="100%"></asp:TextBox>
        </td>
        <td style="width: 20%;text-align:center;">
                <asp:Button ID="lnkBtnSearchId" runat="server" SkinID="skinBtnSearch" Text="Search" ToolTip="Click here to search" TabIndex="3" />
        </td>
    </tr>
    </table>
     </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
        <br />
    <table style="width: 100%;" align="center">        
        <tr style="width:100%">
        <td colspan="5">
          <asp:GridView  id="grdCash" runat="server" Width="100%" 
                AutoGenerateColumns="False" AllowPaging="True" 
                OnRowCreated="grdCash_RowCreated" OnRowCancelingEdit="grdCash_RowCancelingEdit" 
                OnRowDataBound="grdCash_RowDataBound"  OnRowCommand="grdCash_RowCommand" DataKeyNames="ID"
                OnRowUpdating="grdCash_RowUpdating" 
                EmptyDataText="No Installation/Re-Inst Cash found for the selected Book." OnDataBound="grdCash_DataBound" 
                OnRowEditing="grdCash_RowEditing" DataSourceID="ObjectDataSource1" 
                onrowupdated="grdCash_RowUpdated">
				<Columns>
				    <asp:TemplateField HeaderText="Bill No">
				        <ItemStyle Width="12%" />
                        <ItemTemplate>
                            <asp:Label ID="lblBillNo" runat="server" EnableTheming="false" Text='<%# Bind("billno") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBillNo" runat="server" Enabled="false" Width="80px" CssClass="cssTextBox" Text='<%# Bind("billno") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Code">
					    <ItemStyle Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" EnableTheming="False" Text='<%# Bind("code") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCode" runat="server" Width="80px" CssClass="cssTextBox" Text='<%# Bind("code") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Area">
					    <ItemStyle Width="22%" />
                        <ItemTemplate>
                            <asp:Label ID="lblArea" runat="server" EnableTheming="false" Text='<%# Bind("Area") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" DataSourceID="srcArea"
                                CssClass="cssDropDown" DataTextField="area" DataValueField="area" Width="170px" SelectedValue='<%# Bind("area") %>'>
                                <asp:ListItem Value="0"> -- Select Area -- </asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
				        <ItemStyle Width="12%" />
                        <ItemTemplate>
                            <asp:Label ID="lbltype" runat="server" EnableTheming="false" Text='<%# Bind("CashType") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtType" runat="server" Enabled="false" Width="80px" CssClass="cssTextBox" Text='<%# Bind("CashType") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Amount">
					    <ItemStyle Width="12%" />
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" EnableTheming="false" Text='<%# Bind("Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="80px" CssClass="cssTextBox" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Paid">
					    <ItemStyle Width="12%" />
                        <ItemTemplate>
                            <asp:Label ID="lblDatePaid" runat="server" Text='<%# Bind("EnteredDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left">
								<tr>
									<td style="width: 88%">
										<asp:TextBox ID="txtDatePaid" runat="server" Text='<%# Bind("EnteredDate","{0:dd/MM/yyyy}") %>' Width="100px" CssClass="cssTextBox" ></asp:TextBox>
									</td>
									<td style="width: 12%; text-align: center">
										<script type="text/javascript" language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': GettxtBoxName('txtDatePaid')});</script>
									</td>
								</tr>
							</table>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                         <ItemStyle Width="5%" />
                         <ItemTemplate>
                            <asp:ImageButton ID="btnEdit"  runat="server" SkinID="edit" CommandName="Edit" CausesValidation="False" />
                         </ItemTemplate>
                         <EditItemTemplate>
                            <asp:LinkButton ID="lbUpdate" OnClientClick="Javascript:return ValidatePaidDate();" runat="server" SkinID="submenuSkin" CausesValidation="True" CommandName="Update" Text="Update" ></asp:LinkButton> 
                            <asp:LinkButton ID="lbCancel" runat="server" SkinID="submenuSkin" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                         </EditItemTemplate>
                         <FooterTemplate>
                            <asp:LinkButton ID="lbInsert" OnClientClick="Javascript:return ValidatePaidDate();" runat="server" SkinID="submenuSkin" CommandName="Insert" Text="Save" ></asp:LinkButton> 
                            <asp:LinkButton ID="lblInsCancel" runat="server" CausesValidation="False" SkinID="submenuSkin" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                         </FooterTemplate>
                    </asp:TemplateField>
				</Columns>
				<PagerTemplate>Goto Page 
                <asp:DropDownList ID="ddlPageSelector" runat="server" SkinID="skinPagerDdlBox" AutoPostBack="true">
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
        <tr > 
            <td style="width: 100%" align="left" colspan="5">
                <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]" ProviderName="System.Data.OleDb"></asp:SqlDataSource>    
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    SelectMethod="ListInstallationCash" TypeName="BusinessLogic" UpdateMethod="UpdateCash" onupdating="ObjectDataSource1_Updating">
                </asp:ObjectDataSource>
                <br />
            </td>
        </tr> 
        <tr width="100%">
            <td align="left" style="width: 100%; border: solid 1px #5D819B" colspan="5">
                <asp:Panel ID="Panel1" runat="server" CssClass="GrdContent">
                    <table>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label1" runat="server">Book Total Amount :</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label2" runat="server">Total cash Enteted:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashEnteted" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="Label3" runat="server">Total Remaining:</asp:Label>
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashRem" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:SqlDataSource ID="srcReason" runat="server" SelectCommand="SELECT reason FROM ReasonMaster"
                    EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
                <asp:SqlDataSource ID="srcBook" runat="server" SelectCommand="SELECT BookID,BookName FROM tblBook Where BookStatus='Open' Order By BookName ASC "
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
        </tr>
    </table>  
    <br />
   <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>

