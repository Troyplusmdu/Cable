<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="~/GroupInfo.aspx.cs" Inherits="GroupInfo" Title="Group Information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    <table style="width: 80%;">
        <tr>
        <td class="LMSHeader" align="Left">
            <span style="height:20px">
                <asp:Label runat="server" ID="extraSpace">&nbsp;&nbsp;</asp:Label>             
            </span>
        </td>
        </tr>
        <tr style="width:80%">
            <td>
              <asp:FormView ID="frmViewDetails" runat="server" Width="100%" DataKeyNames="GroupID" DefaultMode="Edit" DataSourceID="frmSource" OnItemUpdated="frmViewDetails_ItemUpdated" OnItemInserted="frmViewDetails_ItemInserted" OnItemCommand="frmViewDetails_ItemCommand" OnItemInserting="frmViewDetails_ItemInserting" >
              <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"  bordercolor="#cccccc" Height="20px" />
              <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" bordercolor="#cccccc" VerticalAlign="middle"  Height="20px" />
              <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad" Height="25px" bordercolor="#cccccc" VerticalAlign="Middle" />
                  <EditItemTemplate>
                     <table width="60%">
                     <tr>
                        <td style="width: 20%" class="GrdHeaderbgClr GrdHdrContent allPad">
                            Group Name:<asp:RequiredFieldValidator ID="rvEEstMnds" runat="server"
                                ControlToValidate="txtGroupName" Display="Dynamic" EnableClientScript="False"
                                ErrorMessage="Estimated Mandays is Mandatory">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtGroupName" runat="server" Text='<%# Bind("GroupName") %>'
                                Width="100%" SkinID="skinTxtBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 20%;" class="GrdHeaderbgClr GrdHdrContent allPad">
                            Heading:<asp:CompareValidator ID="cvHeading" runat="server" ControlToValidate="ddHeading"
                                Display="Dynamic" EnableClientScript="False" ErrorMessage="Please select the Item"
                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator></td>
                        <td style="width: 25%;">
                            <asp:DropDownList ID="ddHeading" DataSourceID="srcHeading" SkinID="skinDdlBox" runat="server" SelectedValue='<%# Bind("HeadingID") %>' DataTextField="Heading" DataValueField="HeadingID" Width="100%" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                      </tr>
                     <tr>
                         <td style="width: 20%">
                         </td>
                         <td align="right" style="width: 25%">
                        <asp:Button ID="UpdateCancelButton" runat="server" CommandName="Cancel" Text="Cancel" OnClick="UpdateCancelButton_Click"></asp:Button>&nbsp;<asp:Button ID="UpdateButton" runat="server"  CausesValidation="True" CommandName="Update" Text="Update" OnClick="UpdateButton_Click"></asp:Button></td>
                     </tr>
                    </table>
                     <table cellspacing="0">
                      <tr>
                        <td style="width: 20%">
                            &nbsp;</td>
                        <td style="width: 25%" align="right">
                        <asp:ObjectDataSource ID="srcHeading" runat="server" SelectMethod="ListHeading" TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 20%">
                        <td style="width: 25%">
                            &nbsp;
                        </td>
                      </tr>
                    </table>
                    </EditItemTemplate> 
                  <InsertItemTemplate>
                      <table width="60%">
                      <tr>
                        <td style="width: 20%" class="GrdHeaderbgClr GrdHdrContent allPad">
                            Group Name:<asp:RequiredFieldValidator ID="rvIgroup" runat="server"
                                ControlToValidate="txtIGroup" Display="Dynamic" EnableClientScript="False"
                                ErrorMessage="Group Name is Mandatory">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtIGroup" runat="server" Text='<%# Bind("GroupName") %>' Width="97%" SkinID="txtData"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 20%" class="GrdHeaderbgClr GrdHdrContent allPad">
                            Heading:<asp:CompareValidator ID="cvHeading" runat="server" ControlToValidate="ddIHeading"
                                ErrorMessage="Heading is Mandatory" Operator="GreaterThan" ValueToCompare="0" EnableClientScript="False">*</asp:CompareValidator></td>
                        <td style="width: 25%">
                          <asp:DropDownList ID="ddIHeading" DataSourceID="srcHeading" runat="server" DataTextField="Heading" SkinID="skinDdlBox" DataValueField="HeadingID" SelectedValue='<%# Bind("HeadingID") %>' Width="100%" AppendDataBoundItems="True">
                              <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                          <td style="width: 20%">
                          </td>
                          <td style="width: 25%; text-align:right">
                            <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="InsertCancelButton_Click"></asp:Button>&nbsp;<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"  Text="Save" OnClick="InsertButton_Click"></asp:Button>
                          </td>
                      </tr>
                      <tr>
                        <td style="width: 25%">
                            <asp:ObjectDataSource ID="srcHeading" runat="server" SelectMethod="ListHeading" TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                            </SelectParameters>
                            </asp:ObjectDataSource>
                        <td style="width: 25%">
                        </td>
                      </tr>
                    </table>
                    </InsertItemTemplate> 
                </asp:FormView> 
            </td>
        </tr>
        <tr style="width:100%">
        <td>
            <asp:GridView id="grdViewGroup" runat="server" EmptyDataText="No Data found!"
                DataKeyNames="GroupID"  AutoGenerateColumns="False" AllowPaging="True" 
                Width="100%" OnRowCreated="grdGroup_RowCreated" 
                DataSourceID="grdSource" OnRowCommand="grdViewGroup_RowCommand" 
                onrowdatabound="grdViewGroup_RowDataBound" >
            	<Columns>
					<asp:BoundField DataField="GroupName" HeaderText="Group Name">
                        <ItemStyle Width="50%" Wrap="False" />
                    </asp:BoundField>
					<asp:BoundField DataField="Heading" HeaderText="Heading">
                        <ItemStyle Wrap="true" Width="40%" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select"  />
                            </ItemTemplate>
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
        <tr>
            <td style="text-align:left">
                <asp:ImageButton ID="lnkBtnAddGroup" runat="server" onclick="lnkBtnAddGroup_Click" ImageUrl="~/App_Themes/DefaultTheme/Images/BtnAddnew.png"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="grdSource" runat="server" SelectMethod="ListGroupInfo" TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetGroupInfoForId"
                    TypeName="BusinessLogic" UpdateMethod="UpdateGroupInfo" InsertMethod="InsertGroupInfo"  OnInserting="frmSource_Inserting" OnUpdating="frmSource_Updating" >
                    <SelectParameters>
                        <asp:ControlParameter ControlID="grdViewGroup" Name="GroupID" PropertyName="SelectedValue" Type="Int32" />
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="GroupId" Type="Int32" />
                        <asp:Parameter Name="HeadingId" Type="Int32" />
                        <asp:Parameter Name="GroupName" Type="String" />
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="GroupId" Type="Int32" />
                        <asp:Parameter Name="HeadingId" Type="Int32" />
                        <asp:Parameter Name="GroupName" Type="String" />
                        <asp:Parameter Name="Order" Type="Int32" />
                        <asp:SessionParameter Name="connection" SessionField="Company" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

