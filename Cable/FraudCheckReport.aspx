<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FraudCheckReport.aspx.cs" Inherits="FraudCheckReport" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fraud Check Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" /> 
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

  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="text-align:center; margin:20px 50px 0px 60px">
    <table cellpadding="2" style="border: solid 1px Silver; text-align: left" cellspacing="2"
            width="90%">
            <tr style="text-align: center">
                <td colspan="4" class="SectionHeader">
                    Fraud Check Report
                </td>
            </tr>
            <tr>
                <td class="LMSLeftColumnColor" style="width: 25%">
                    Area : *
                    <asp:CompareValidator ID="cvArea" runat="server" EnableClientScript="false" ControlToValidate="ddArea"
                        ErrorMessage="Please select Area to Generate the report" Display="Dynamic" Operator="GreaterThan"
                        ValueToCompare="0">*</asp:CompareValidator>
                    &nbsp;
                </td>
                <td style="width: 25%">
                    <asp:DropDownList ID="ddArea" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True" Width="100%"
                        DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                        <asp:ListItem Value="0">--Please Select Area--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td style="width: 25%" class="LMSLeftColumnColor">
                    Door No :
                </td>
                <td style="width: 25%; text-align: left">
                    <asp:TextBox ID="txtDoorNo" runat="server" Maxlength="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
             <td class="LMSLeftColumnColor">Customer Name : </td>
             <td class="LMSLeftColumnColor"><asp:TextBox ID="txtCustomer" runat="server" Maxlength="20"></asp:TextBox></td>
            
             <td class="LMSLeftColumnColor" colspan="2">
              <asp:Button ID="lnkBtnSearchId" runat="server" Text="Generate Report" SkinID="skinButtonBig" ToolTip="Click here to generate report"
                        TabIndex="3" OnClick="lnkBtnSearchId_Click" /> &nbsp;
                         <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" class="button" />&nbsp;
             </td>
             
            </tr>
                
     </table>
      <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster] Order by area"
                         ProviderName="System.Data.OleDb"></asp:SqlDataSource>
                        <br /><br />
                        <div id="divPrint" style="font-family:Verdana; font-size:11px;  ">
                        <h3>Fraud Check Report</h3>
                        
       <wc:ReportGridView runat="server" BorderWidth="1" ID="gvReport" 
                     AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" 
                Width="600px"  style="font-family:Verdana; font-size:11px;  " >
                <PageHeaderTemplate>
                <br />
                 
               
                <br />
            </PageHeaderTemplate>
            
            <Columns>
                <asp:BoundField DataField="area" HeaderText="Area"/>
                <asp:BoundField DataField="Code" HeaderText="Code"/>
                <asp:BoundField DataField="Name" HeaderText="Customer Name"/>
                <asp:BoundField DataField="Doorno" HeaderText="Door No"/>
                <asp:BoundField DataField="Address1" HeaderText="Address"/>
                <asp:BoundField DataField="Balance" HeaderText="Balance"/>
                <asp:BoundField DataField="Category" HeaderText="Category"/>
                <asp:BoundField DataField="effectDate" HeaderText="Effective Date" DataFormatString="{0:dd/MM/yyyy}"/>
               
            </Columns>            
            <PagerTemplate>
        
            </PagerTemplate>
            <PageFooterTemplate>
               
                <br />
                              <hr />
               <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>--%>
                
            </PageFooterTemplate>
        </wc:ReportGridView>
        </div>
                        
    </div>
    </form>
</body>
</html>
