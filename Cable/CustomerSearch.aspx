<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="CustomerSearch.aspx.cs" Inherits="CustomerSearch" Title="Customer Search" %>
<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">

    <script language="javascript" type="text/javascript">

        function clickButton(e, buttonid)
        { 
              var evt = e ? e : window.event;
              var bt = document.getElementById(buttonid);

              if (bt)
              { 
                  if (evt.keyCode == 13)
                  { 
                        bt.click(); 
                        return false; 
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
        <table style="width: 100%;" align="center" cellpadding="2" cellspacing="2" style="border: 0px solid #5078B3">
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="5">
                <span>Search Customer </span>
            </td>
        </tr>
        <tr>
            <td class="tblLeft" style="width: 20%">
                Area :</td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" Width="100%" SkinID="skinDdlBox"
                    DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                    <asp:ListItem Value="0">   -- All Areas --    </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 20%" class="tblLeft">
                Active Customer ? :</td>
            <td style="width: 25%; text-align:left">
                <asp:CheckBox ID="CheckBox1" runat="server" />
            </td>
            <td style="width: 10%">
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tblLeft">
                Customer Name :</td>
            <td style="width: 25%">
                <asp:TextBox ID="txtUserId" runat="server" SkinID="skinTxtBoxGrid" Width="100%"></asp:TextBox>
            </td>
            <td style="width: 20%" class="tblLeft">
                Code :</td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCode" runat="server" SkinID="skinTxtBoxGrid" Width="100%"></asp:TextBox>
            </td>
            <td style="width: 10%;text-align:left; height:20px; padding-left:25px">
            </td>
        </tr>
        <tr>
            <td class="tblLeft" style="width: 20%">
                Door No :</td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDoorNo" runat="server" SkinID="skinTxtBoxGrid" Width="100%"></asp:TextBox>
            </td>
            <td style="width: 20%">
                
            </td>
            <td style="width: 25%;" class="tblLeft">
                <asp:Button ID="lnkBtnSearchId" runat="server" OnClick="lnkBtnSearch_Click" SkinID="skinBtnSearch" ></asp:Button>
            </td>
            <td style="width: 10%;text-align:left; height:20px; padding-left:25px">
                
            </td>
        </tr>        
        </table>
        </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
        <table style="width: 100%;" align="center" cellpadding="0" cellspacing="0"
                                        style="border: 0px solid #5078B3">
        <tr>
            <td style="width: 20px" colspan="5">
                <asp:SqlDataSource ID="srcArea" runat="server"
                    SelectCommand="SELECT [area] FROM [AreaMaster]" ProviderName="System.Data.OleDb">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <uc1:errordisplay ID="errordisplay1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px; text-align:left" colspan="5">
                <asp:Button ID="lnkBtnAdd" runat="server" SkinID="skinBtnAddNew" PostBackUrl="~/CustomerDetails.aspx?ID="  ></asp:Button>
                <br />
            </td>
        </tr>
        <tr>
            <td style="height: 20px; text-align:left" colspan="5" class="ajax__combobox_textboxcontainer ajax__tab_hover">
                <asp:Label ID="lblTotal" runat="server" ></asp:Label>
            </td>
        </tr>                
        <tr style="width: 100%; text-align:left">
            <td width="100%" colspan="5">
                <br />
                <asp:GridView ID="GrdViewCust" TabIndex="6" DataKeyNames="areanamecode" 
                    Width="100%" runat="server" AutoGenerateColumns="False"
                    OnRowCreated="GrdViewCust_RowCreated" AllowPaging="true" 
                    OnRowDataBound="GrdViewCust_RowDataBound" OnDataBound="GrdViewCust_DataBound"
                    OnRowCommand="GrdViewCust_RowCommand" OnRowDeleting="DeleteCustomer" 
                    onrowdeleted="GrdViewCust_RowDeleted">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Customer">
                            <ItemStyle Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="code" HeaderText="Code">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="area" HeaderText="Area">
                            <ItemStyle Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="doorno" HeaderText="Door No">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="category" HeaderText="Category">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="balance" HeaderText="Balance">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="effectdate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Effective Date">
                            <ItemStyle Width="15%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <uc:BoundUrlColumn ItemStyle-CssClass="command" DataField="areanamecode" Tooltip="Edit Customer" BaseUrl="CustomerDetails.aspx?ID="
                            IconPath="App_Themes/DefaultTheme/Images/edit-page-blue.gif" ItemStyle-BorderWidth="0"><ItemStyle Width="50px" HorizontalAlign="Center" BorderWidth="0" />
                        </uc:BoundUrlColumn>
                        <asp:TemplateField ItemStyle-CssClass="command">
                            <ItemTemplate>
                            <asp:HiddenField ID="lblCategory" runat="Server" Value='<%# Bind("category") %>' />
                            <span class="tblLeft" onclick="return confirm('Are you sure to Delete the record?')">
                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                            </span>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                            SkinID="dropDownPage">
                        </asp:DropDownList>
                    </PagerTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

