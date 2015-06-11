<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintOutstandingReport.aspx.cs"
    Inherits="PrintOutstandingReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Preview - Outstanding Balance Report</title>

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

    <style type="text/css">
        .style1
        {
            width: 461px;
        }
    </style>

</head>
<body style="font-family: Verdana; font-size: 11px;" onbeforeunload="unl()">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    <br />
    <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="javascript:CallPrint('divPrint')" />
    &nbsp;
    <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" />
    <div id="divPrint" style="font-size: 11px; font-family: verdana;">
        <table width="600px" border="0" style="font-family: Verdana; font-size: 11px;">
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center" class="style1">
                    &nbsp;</td>
                <td align="right">
                    Date: <asp:Label ID="lblDate" runat="server"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <h5>
                        Outstanding Balance Report</h5>
                </td>
            </tr>
            <tr>
            <td colspan="3">Month : <asp:Label ID="lblMonth" runat="server"></asp:Label>&nbsp;Year : <asp:Label ID="lblYear" runat="server"></asp:Label></td>
            </tr>
        </table>
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvOutsDetails" AutoGenerateColumns="false"
            PrintPageSize="23" AllowPrintPaging="true" Width="600px" OnRowDataBound="gvOutsDetails_RowDataBound"
            Style="font-family: Verdana; font-size: 11px;">
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:BoundField DataField="Area" HeaderText="Area" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Doorno" HeaderText="Doorno" />
                <asp:BoundField DataField="Code" HeaderText="Code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" DataField="CurrentBill"
                    HeaderText="This Month Bill" />
                <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" DataField="AdjustmentAmt"
                    HeaderText="This Month Adjustment" />
                <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" DataField="CashDetailAmt"
                    HeaderText="This Month Payment" />
                <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="ThisBalance" ItemStyle-Width="80px"
                    HeaderText="This Month Balance" />
                <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="OverallBalance" ItemStyle-Width="80px"
                    HeaderText="Overall Balance" />
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
                <hr />
                <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>--%>
            </PageFooterTemplate>
        </wc:ReportGridView>
        <br />
        <table width="600" border="0" style="font-family: Verdana; font-size: 11px;">
            <tr>
                <td width="230px" align="right">
                    This month
                </td>
                <td width="74px" align="center">
                    Bill Total
                </td>
                <td width="74px" align="center">
                    Adjustment Total
                </td>
                <td width="74px" align="center">
                    Payment Total
                </td>
                <td width="74px" align="center">
                    Balance Total
                </td>
                <td width="74px" align="center">
                    Ovr Balance Total
                </td>
            </tr>
            <tr>
                <td width="230px" align="right">
                </td>
                <td width="74px" align="right">
                    <hr />
                    <asp:Label ID="lblCurrent" runat="server"></asp:Label><hr />
                </td>
                <td width="74px" align="right">
                    <hr />
                    <asp:Label ID="lblAdj" runat="server"></asp:Label><hr />
                </td>
                <td width="74px" align="right">
                    <hr />
                    <asp:Label ID="lblPayment" runat="server"></asp:Label><hr />
                </td>
                <td width="74px" align="right">
                    <hr />
                    <asp:Label ID="lblBalance" runat="server"></asp:Label><hr />
                </td>
                <td width="74px" align="right">
                    <hr />
                    <asp:Label ID="lblOverall" runat="server"></asp:Label><hr />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
