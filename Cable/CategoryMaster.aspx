<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" EnableViewState="false" AutoEventWireup="true" CodeFile="CategoryMaster.aspx.cs" Inherits="CategoryMaster" Title="Category Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <div>
    <table style="width: 80%;" align="center">
     <tr>
        <td class="LMSHeader">
            <span style="height:20px">
                <asp:Label runat="server" ID="extraSpace">&nbsp;&nbsp;</asp:Label>             
            </span>
        </td>
    </tr>
        <tr style="width:80%" align="left">
        <td>
            <asp:GridView ID="GrdCategory" runat="server" DataSourceID="srcGridView" AutoGenerateColumns="False" 
                OnRowCreated="GrdCategory_RowCreated" Width="80%" PageSize="15"
                OnRowCancelingEdit="GrdCategory_RowCancelingEdit" 
                OnRowCommand="GrdCategory_RowCommand" 
                OnRowDataBound="GrdCategory_RowDataBound" AllowPaging="True" 
                DataKeyNames="CategoryId" OnDataBound="GrdCategory_DataBound" 
                onrowupdating="GrdCategory_RowUpdating" EnableViewState="False"
                onprerender="GrdCategory_PreRender" onrowupdated="GrdCategory_RowUpdated">
                <Columns>
                   <asp:TemplateField HeaderText="Product Category">
                        <ItemStyle Width="80%" HorizontalAlign="Left" />
                        <FooterStyle Width="80%" HorizontalAlign="Left" />
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCatDescr" runat="server" Text='<%# Bind("CategoryName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvDescr" runat="server" ControlToValidate="txtCatDescr"
                                Display="Dynamic" EnableClientScript="False" ErrorMessage="Description is mandatory">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryDescr" runat="server" Text='<%# Bind("CategoryName") %>' ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                                <asp:TextBox ID="txtAddDescr" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox><asp:RequiredFieldValidator ID="rvAddDescr" runat="server" ControlToValidate="txtAddDescr"
                                Display="Dynamic" EnableClientScript="true" ErrorMessage="Description is mandatory">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>                                                       
                    <asp:TemplateField>
                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                        <FooterStyle Width="20%" HorizontalAlign="Center" />
                         <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Edit"  />
                         </ItemTemplate>
                         <EditItemTemplate>
                            <asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update" ></asp:LinkButton> 
                            <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                         </EditItemTemplate>
                         <FooterTemplate>
                            <asp:LinkButton ID="lbInsert" runat="server" CommandName="Insert" Text="Save"></asp:LinkButton> 
                            <asp:LinkButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                         </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>Goto Page 
                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="dropDownPage">
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
            <td style="width: 100%" align="left">
                <asp:LinkButton ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click">Click here to Add New Category</asp:LinkButton>
             </td>
        </tr>
        <tr height="10%" width="100%"> 
            <td style="width: 100%" align="left">
                <asp:ObjectDataSource ID="srcGridView" runat="server" InsertMethod="InsertRecord" SelectMethod="GetCategoryData" 
                TypeName="BusinessLogic" onupdating="srcGridView_Updating" >
                    <SelectParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr> 
        <tr width="100%">
            <td align="left" style="width: 100%">
            </td>
        </tr>
    </table>    
</div>
<br />
<br />
</asp:Content>

