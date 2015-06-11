<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssetDetailsReport.aspx.cs" Inherits="AssetDetailsReport" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asset Details Report</title>
     <script type="text/javascript">
  function CallPrint(strid)
  {
      var prtContent = document.getElementById(strid);
      var WinPrint = window.open('','','letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
      WinPrint.document.write(prtContent.innerHTML);
      WinPrint.document.close();
      WinPrint.focus();
      WinPrint.print();
      WinPrint.close();

}
 function unl()
    {
     
    document.form1.submit();
    }

  </script>
</head>
<body style="font-size:11px; font-family :verdana; " >
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
     <asp:HiddenField Id="hdAsset" runat="server" Value="0"/>
                                <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" Class="accordionContent" style="border:1px solid black" >
                                   
                                   <tr>
                                    <td colspan="4" align="left" class="accordionHeaderSelected">Search</td>
                                    </tr>
                                    <tr style="height: 20px">
                                      <td style="width: 25%" class="tblLeft">
                                           Asset Category:
                                        </td>
                                        <td style="width: 25%">
                                           
                                            <asp:DropDownList DataTextField = "CategoryDescription" DataValueField="CategoryID"  AppendDataBoundItems="true" ID="drpSAssetCat"  width="125px" Height="21px"  runat="server"  CssClass="drpDownListMedium">
                                             <asp:ListItem Value="0">--Select Category--</asp:ListItem>                  
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 25%" class="tblLeft">
                                           Asset area: *
                                        </td>
                                        <td style="width: 25%" class="tblLeft">
                                            <asp:DropDownList ID="drpSAssetarea" AppendDataBoundItems="true" DataTextField="area" DataValueField="area"    width="125px" Height="21px" runat="server"  CssClass="drpDownListMedium">
                                            <asp:ListItem Value="0">--Select Area--</asp:ListItem>
                                            </asp:DropDownList> 
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 25%" class="tblLeft">
                                           Asset Status: *
                                        </td>
                                          <td style="width: 25%">
                                           
                                            <asp:DropDownList ID="drpSAStatus"  width="125px" Height="21px" runat="server"  CssClass="drpDownListMedium">
                                                 <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                                                <asp:ListItem>New</asp:ListItem>
                                                <asp:ListItem>Used</asp:ListItem>
                                                <asp:ListItem>Scrapped</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       
                                     <td style="width: 25%" class="tblLeft">Asset no : </td>
                                         <td style="width: 25%" class="tblLeft">   <asp:TextBox ID="txtSAssetno" runat="server" Width="125px" Height="16px" CssClass="txtBox"> </asp:TextBox>
                                       
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtSAssetno"
                        FilterType="Numbers"  />
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="4" class="tblLeft">
                                   <asp:Button ID="btnSearch" runat="server" Text="Search"  onclick="btnSearch_Click" SkinID="skinButtonMedium" />&nbsp;
                                         <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" Class="button" />&nbsp;
                                    </td>
                                    </tr>
                                </table>
                            <br />
                             <br />
                              <div id="divPrint" style="font-family:Verdana; font-size:11px;  ">
                              <h5>Asset Details Report</h5>
                              <br />
                              <h6>Search Based on</h6>
                              <table width="30%" cellpadding="4" cellspacing="4" style="font-size:11px; font-family :verdana; " >
                              <tr>
                              <td class="tblLeft" >Category :</td>
                              <td class="tblLeft"><asp:Label  ID="lblCat" runat="server" Font-Bold="true"  ></asp:Label></td>
                              </tr>
                              <tr>
                              <td class="tblLeft">Area :</td>
                              <td class="tblLeft"> <asp:Label  ID="lblArea" runat="server" Font-Bold="true"  ></asp:Label></td>
                              </tr>
                              <tr>
                              <td class="tblLeft">Status: </td>
                              <td class="tblLeft"> <asp:Label  ID="lblStatus" runat="server" Font-Bold="true"  ></asp:Label></td>
                              </tr>
                              <tr>
                              <td class="tblLeft">Asset No:</td>
                              <td class="tblLeft"> <asp:Label  ID="lblAssetNo" runat="server" Font-Bold="true"  ></asp:Label></td>
                              </tr>
                              </table>
                                 
                               <br /><br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="GrdViewAsset" 
                     AutoGenerateColumns="false" PrintPageSize="47" AllowPrintPaging="true" 
                Width="600px"  style="font-family:Verdana; font-size:11px;  " >
                <PageHeaderTemplate>
                <br />
                 
               
                <br />
            </PageHeaderTemplate>
                            
                    <Columns>
                        <asp:BoundField DataField="AssetNo" HeaderText="Asset No" />
                        <asp:BoundField DataField="AssetCode" HeaderText="Asset Code" />
                        <asp:BoundField DataField="AssetStatus" HeaderText="Status" />
                        <asp:BoundField DataField="AssetLocation" HeaderText="Location" />
                        <asp:BoundField DataField="AssetArea" HeaderText="Area" />
                        <asp:BoundField DataField="DateEntered" HeaderText="Entered Date" DataFormatString="{0:dd/MM/yyyy}" />
                         <asp:BoundField DataField="SerialNo" HeaderText="Serial No" />
                        
                    </Columns>
                    <PageFooterTemplate>
               
                <br />
                            
             <PageFooterTemplate>
               
                <br />
                               <hr />
               Page <%# GrdViewAsset.CurrentPrintPage.ToString()%> / <%# GrdViewAsset.PrintPageCount%>
                
            </PageFooterTemplate>
                
            </PageFooterTemplate>
                 </wc:ReportGridView>
                </div>
    </div>
    </form>
</body>
</html>
