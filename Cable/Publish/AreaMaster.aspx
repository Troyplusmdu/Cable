<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="AreaMaster.aspx.cs" Inherits="AreaMaster" Title="Area Master" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhTab" runat="Server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <div>
    <table style="width: 100%;" align="center" cellspacing="0" cellpadding="0">
        <tr height="10%" width="100%">
            <td style="width: 100%" align=left>
                <asp:Button ID="lnkBtnAdd" runat="server" SkinID="skinBtnAddNew" OnClick="lnkBtnAdd_Click"  ></asp:Button>
             </td>
        </tr>
        <tr style="width: 100%">
            <td>
                <br />
            </td>
        </tr>
        <tr style="width:100%">
        <td>
          <asp:GridView  id="grdAreaMaster" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" 
          OnRowCreated="grdAreaMaster_RowCreated" OnRowCancelingEdit="grdAreaMaster_RowCancelingEdit" OnRowDataBound="grdAreaMaster_RowDataBound"  OnRowCommand="grdAreaMaster_RowCommand" OnRowUpdating="grdAreaMaster_RowUpdating" EmptyDataText="No Data Found for selected User" OnPreRender="grdAreaMaster_PreRender" OnDataBound="grdAreaMaster_DataBound" OnRowEditing="grdAreaMaster_RowEditing" DataSourceID="SqlDataSource1">
				<Columns>
				    <asp:TemplateField HeaderText="Area">
				        <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="lblArea" runat="server" EnableTheming="false" Text='<%# Bind("area") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtArea" runat="server" Enabled="false" Text='<%# Bind("area") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFooArea" runat="server" Text='<%# Bind("area") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Person Incharge">
					    <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="lblPIncharge" runat="server" EnableTheming="False" Text='<%# Bind("personincharge") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPIncharge" runat="server" Text='<%# Bind("personincharge") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFooPIncharge" runat="server" Text='<%# Bind("personincharge") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Contact No">
					    <ItemStyle Width="15%" />
                        <ItemTemplate>
                            <asp:Label ID="lblContactNo" runat="server" EnableTheming="false" Text='<%# Bind("contactno") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCntactNo" runat="server" Text='<%# Bind("contactno") %>' ></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFooContactNo" runat="server" Text='<%# Bind("contactno") %>' ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Mobile">
					    <ItemStyle Width="15%" />
                        <ItemTemplate>
                            <asp:Label ID="lblMobileNo" runat="server" EnableTheming="false" Text='<%# Bind("mobileno") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMobileNo" runat="server" Text='<%# Bind("mobileno") %>' ></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFoomobileNo" runat="server" Text='<%# Bind("mobileno") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Within Network">
					    <ItemStyle Width="10%" />
                        <ItemTemplate>
                            &nbsp;<asp:CheckBox ID="chkHWithinntwrk" runat="server" Checked='<%# Bind("within_network") %>' Enabled="False" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            &nbsp;<asp:CheckBox ID="chkWithinntwrk" runat="server" Checked='<%# Bind("within_network") %>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            &nbsp;<asp:CheckBox ID="chkFooWithinntwrk" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                         <ItemStyle Width="5%" />
                         <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Edit" CausesValidation="False" />
                         </ItemTemplate>
                         <EditItemTemplate>
                            <asp:LinkButton ID="lbUpdate" runat="server" SkinID="submenuSkin" CausesValidation="True" CommandName="Update" Text="Update" ></asp:LinkButton> 
                            <asp:LinkButton ID="lbCancel" runat="server" SkinID="submenuSkin" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                         </EditItemTemplate>
                         <FooterTemplate>
                            <asp:LinkButton ID="lbInsert" runat="server" SkinID="submenuSkin" CommandName="Insert" Text="Save" ></asp:LinkButton> 
                            <asp:LinkButton ID="lblInsCancel" runat="server" CausesValidation="False" SkinID="submenuSkin" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                         </FooterTemplate>
                    </asp:TemplateField>
				</Columns>
				<PagerTemplate>Goto Page 
                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true">
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
        <tr height="10%" width="100%">
            <td style="width: 100%" align=right>
                <br />
                <asp:Button ID="lnkBtnDownload" runat="server" CssClass="Button" OnClick="lnkBtnDownload_Click" Text="Click here to Download" ></asp:Button>
            </td>
        </tr>
        <tr height="10%" width="100%"> 
            <td style="width: 100%" align=left>
                
                <br />
                &nbsp;<asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ProviderName="System.Data.OleDb" UpdateCommand="UPDATE area SET area='ass' WHERE area = '0'" SelectCommand="SELECT [area], [personIncharge], [contactno], [mobileno], [within_network], [areacount] FROM [AreaMaster]" InsertCommand="INSERT INTO [AreaMaster] ([area], [personIncharge], [contactno], [mobileno], [within_network], [areacount]) VALUES (?, ?, ?, ?, ?, ?)" OnUpdating="SqlDataSource1_Updating" OnInserting="SqlDataSource1_Inserting">
                    <InsertParameters>
                        <asp:Parameter Name="area" Type="String" />
                        <asp:Parameter Name="personIncharge" Type="String" />
                        <asp:Parameter Name="contactno" Type="String" />
                        <asp:Parameter Name="mobileno" Type="String" />
                        <asp:Parameter Name="within_network" Type="Boolean" />
                    </InsertParameters>
                </asp:SqlDataSource>                
            </td>
        </tr> 
        <tr width="100%">
            <td align="left" style="width: 100%">
                
            </td>
        </tr>
    </table>   
</div>
</asp:Content>

