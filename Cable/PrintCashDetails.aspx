<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCashDetails.aspx.cs" Inherits="PrintCashDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Preview - Cash Details Report</title>
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
<body style="font-size:11px; font-family :verdana;text-align:center" onbeforeunload="unl()">
    <form id="form1" runat="server">
    <br />
     <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
         <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="javascript:CallPrint('divPrint')" />
    &nbsp;
     <asp:Button ID="btnBack" Text="Back" runat="server" onclick="btnBack_Click" />
     <div id="divPrint" style="font-size:11px;font-family:verdana; text-align:center ">
      
        <table width="600px" border="0" style="font-family:Verdana; font-size:11px;  ">
<%--  <tr>
 <td width="140px">TIN#: 33251603073</td>
  <td align="center" width="320px" >J.J Traders</td>
  <td width="140px">Ph:(0452)-27465266</td>
  </tr>
  <tr>
  <td>CST#</td>
  <td align="center">37,North Masi Street,Madurai</td>
  <td>Date: <asp:Label ID="lblDate" runat="server"> </asp:Label></td>
  </tr>
  <tr>
  <td>&nbsp;</td>
  <td align="center">Madurai</td>
  <td>&nbsp;</td>
  </tr>--%>
  <tr>
  <td colspan="3"><br /><h5>Cash Details Report From <asp:Label ID="lblStartDate" runat="server"> </asp:Label> To <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5> </td>
  
  </tr>
  </table>  
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvCashDetails" 
                     AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" 
                Width="600px" onrowdatabound="gvCashDetails_RowDataBound" style="font-family:Verdana; font-size:11px;  ">
                <PageHeaderTemplate>
                <br />
                 
               
                <br />
            </PageHeaderTemplate>
            
            <Columns>
                <asp:BoundField DataField="Area" HeaderText="Area"/>
                <asp:BoundField DataField="Name" HeaderText="Name"/>
                <asp:BoundField DataField="Code" HeaderText="Code"/>
                <asp:BoundField DataField="Doorno" HeaderText="Doorno"/>
                <asp:BoundField DataField="DatePaid" HeaderText="Date Paid"/>
                <asp:BoundField DataField="Billno" HeaderText="Billno"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="Amount"  HeaderText="Amount"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="Discount" HeaderText="Discount"/>                
                <asp:BoundField DataField="CashType" HeaderText="Type"/>
            </Columns>            
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
                <hr /> 
            </PageFooterTemplate>
        </wc:ReportGridView>
        <br />
        <table width="600" border="0" style="font-family:Verdana; font-size:11px;  ">
        <tr>
            <td width="465px" align="right" ><b>Subscription Amount :</b></td>
            <td width="70px" align="right" ><hr /><asp:Label ID="lblSubAmount" runat="server"></asp:Label><hr /></td>
            <td width="65px">&nbsp;</td>
        </tr>
        <tr>
            <td width="465px" align="right" ><b>Installation Amount :</b></td>
            <td width="70px" align="right" ><hr /><asp:Label ID="lblInstAmount" runat="server"></asp:Label><hr /></td>
            <td width="65px">&nbsp;</td>
        </tr>
        <tr>
            <td width="465px" align="right" ><b>Re-Installation Amount :</b></td>
            <td width="70px" align="right" ><hr /><asp:Label ID="lblReInstAmount" runat="server"></asp:Label><hr /></td>
            <td width="65px">&nbsp;</td>
        </tr>
        <tr>
            <td width="465px" align="right" ><b>Total Amount :</b></td>
            <td width="70px" align="right" ><hr /><asp:Label ID="lblAmount" runat="server"></asp:Label><hr /></td>
            <td width="65px">&nbsp;</td>
        </tr>
        </table>
       <br /><br />
       
    </div>
    </form>
</body>
</html>
