<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintJournal.aspx.cs" Inherits="PrintJournal" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Print Preview</title>
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
    <div>
<br />
     <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" class="button"  />&nbsp;
     <asp:Button ID="btnBack" Text="Back" runat="server" onclick="btnBack_Click"  Visible="false" /><br /><br />
      <div id="divPrint" style="font-family:Verdana; font-size:11px;  ">
      
       <table width="600px" border="0" style="font-family:Verdana; font-size:11px;  " >
 <tr>
 <td width="140px" align="left">TIN#: <asp:label ID="lblTNGST" runat="server"></asp:label></td>
  <td align="center" width="320px" ><asp:Label ID="lblCompany" runat="server"></asp:Label></td>
  <td width="140px" align="left">Ph: <asp:Label ID="lblPhone" runat="server"></asp:Label>  </td>
  </tr>
  <tr>
  <td align="left">GST#: <asp:Label ID="lblGSTno" runat="server"></asp:Label></td>
  <td align="center"><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
  <td align="left">Date: <asp:Label ID="lblBillDate" runat="server"></asp:Label></td>
  </tr>
  <tr>
  <td>&nbsp;</td>
  <td align="center"><asp:Label ID="lblCity" runat="server" /> - <asp:Label ID="lblPincode" runat="server"></asp:Label></td>
  <td>&nbsp;</td>
  </tr>
  <tr>
  <td>&nbsp;</td>
  <td align="center"><asp:Label ID="lblState" runat="server"> </asp:Label></td>
  <td>&nbsp;</td>
  </tr>
   <tr>
  <td>&nbsp;</td>
  <td align="center">&nbsp;</td>
  <td>&nbsp;</td>
  </tr>
  <tr>
 <td width="140px" align="left">Bill no:<asp:Label ID="lblBillno" runat="server"></asp:Label></td>
  <td align="center" width="320px" ><asp:Label ID="lblSupplier" runat="server" Font-Bold="true" ></asp:Label></td>
  <td width="140px" align="left">&nbsp;  </td>
  </tr>
  <tr>
  <td colspan="3"><br /><h5>Journal</h5> </td>
  
  </tr>
  </table>  
        <br />
           <asp:HiddenField ID="hdJournal" runat="server" Value="0" />
           <wc:ReportGridView runat="server" BorderWidth="1" ID="GrdViewJournal" 
                     AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" 
                Width="600px" DataKeyNames="TransNo" AllowSorting="True"  EmptyDataText="No Journals found for the search criteria" style="font-family:Verdana; font-size:11px;  ">
               
     
                    <EmptyDataRowStyle CssClass="GrdContent"  />
                    <Columns>
                        <asp:BoundField DataField="Refno" HeaderText="Ref no" />
                        <asp:BoundField DataField="TransDate" HeaderText="Date"  DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Debtor" />
                        <asp:BoundField DataField="Ledgername" HeaderText="Creditor" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        
                       
              
                    </Columns>
        
                  
                </wc:ReportGridView>
    </div>
    </form>
</body>
</html>
