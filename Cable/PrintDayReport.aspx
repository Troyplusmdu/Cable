<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDayReport.aspx.cs" Inherits="PrintDayReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Preview - DayBook Report</title>
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
<body style="font-size:11px; font-family :verdana; " onbeforeunload="unl()">
    <form id="form1" runat="server">
    <div>
      <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')"  class="button"/>&nbsp;
     <asp:Button ID="btnBack" Text="Back" runat="server" onclick="btnBack_Click" Visible="True" /><br /><br />
     <div id="divPrint" style="font-family:Verdana; font-size:11px;  ">
 <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
        <table width="600px" border="0" style="font-family:Verdana; font-size:11px;  " >
 <tr>
 <td width="140px" align="left"<%-->TIN#: --%><asp:label ID="lblTNGST" runat="server"></asp:label></td>
  <td align="center" width="320px" ><asp:Label ID="lblCompany" runat="server"></asp:Label></td>
  <td width="140px" align="left"><%--Ph: --%><asp:Label ID="lblPhone" runat="server"></asp:Label>  </td>
  </tr>
  <tr>
  <td align="left"><%--GST#:--%> <asp:Label ID="lblGSTno" runat="server"></asp:Label></td>
  <td align="center"><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
  <td align="left"><%--Date:--%> <asp:Label ID="lblBillDate" runat="server"></asp:Label></td>
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
  <td colspan="3"><br /><h5>DayBook  From <asp:Label ID="lblStartDate" runat="server"> </asp:Label> To <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5> </td>
  
  </tr>
  </table>  
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" GridLines="none"  
                     AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" 
                Width="600px"  style="font-family:Verdana; font-size:11px;  " CellPadding="5" onrowdatabound="gvLedger_RowDataBound">
                <PageHeaderTemplate>
                <br />
                 
               
                <br />
            </PageHeaderTemplate>
            
            <Columns>
                
                
                <asp:TemplateField HeaderText="Date" ItemStyle-VerticalAlign="Top"  >
                <ItemTemplate>
                <asp:Label ID="lblTranDate" runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate> 
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Particulars" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                <asp:Label ID="lblDebtor"  runat="server" Text='<%# Eval("Debitor") %>'></asp:Label><br /><br />
                <asp:Label ID="lblCreditor"  runat="server" Text='<%# Eval("Creditor") %>'></asp:Label><br /><br />
                <asp:Label ID="lblNarration"  runat="server" Text='<%# Eval("Narration") %>'></asp:Label><br />
                </ItemTemplate> 
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Debit" ItemStyle-VerticalAlign="Top">
                <ItemTemplate >
                <asp:Label ID="lblDebit" Text='<%# Eval("Debit","{0:f2}") %>' runat="server"></asp:Label><br /><br />
                </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit" ItemStyle-VerticalAlign="Top">
                <ItemTemplate ><br /><br />
                <asp:Label ID="lblCredit" Text='<%# Eval("Credit","{0:f2}") %>' runat="server"></asp:Label>
                </ItemTemplate> 
                </asp:TemplateField>  
                
               <asp:TemplateField >
                <ItemTemplate >
                <asp:Label ID="obCr" Visible="false"  runat="server"></asp:Label>
                </ItemTemplate> 
                </asp:TemplateField> 
               
               
            </Columns>            
            <PagerTemplate>
        
            </PagerTemplate>
            <PageFooterTemplate>
               
                <br />
                              <hr />
            
                
            </PageFooterTemplate>
        </wc:ReportGridView>
        <br />
    
    </div>
    </form>
</body>
</html>
