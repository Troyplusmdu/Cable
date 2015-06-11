<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookReport.aspx.cs" Inherits="BookReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Entry Report</title>

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

    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />

    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>

</head>
<body style="font-family: Verdana; font-size: 12px;">
    <form id="form1" runat="server">
    <div style="text-align: center; margin: 20px 50px 0px 60px">
        <table cellpadding="2" cellspacing="4" width="70%" border="0">
            <tr style="text-align: center">
                <td colspan="3">
                    <h3>
                        Book Report</h3>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 25%">
                    Start Date:
                </td>
                <td align="left" style="width: 30%">
                    <asp:TextBox ID="txtStartDate" Width="100px" MaxLength="10" runat="server" CssClass="txtStyle" />

                    <script type="text/javascript" language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtStartDate'});</script>

                </td>
                <td align="left" style="width: 50%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 25%">
                    End Date:
                </td>
                <td align="left" style="width: 30%">
                    <asp:TextBox ID="txtEndDate" Width="100px" MaxLength="10" runat="server" CssClass="txtStyle" />

                    <script type="text/javascript" language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtEndDate'});</script>

                </td>
                <td align="left" style="width: 50%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="width: 25%" align="left">
                    <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" CssClass="btnStyle"
                        OnClick="btnReport_Click" Text="Generate Report" />
                </td>
                <td style="width: 25%" align="left">
                    <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                        class="button" />&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div id="divPrint" style="font-family: Verdana; font-size: 11px;">
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvReport" AutoGenerateColumns="false"
                PrintPageSize="23" AllowPrintPaging="true" Width="600px" Style="font-family: Verdana;
                font-size: 11px;" onrowdatabound="gvReport_RowDataBound">
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:BoundField DataField="BookRef" HeaderText="Book Reference" />
                    <asp:BoundField DataField="BookName" HeaderText="Name" />
                    <asp:BoundField DataField="StartEntry" HeaderText="Start Entry" />
                    <asp:BoundField DataField="NextEntry" HeaderText="Next Entry" />
                    <asp:BoundField DataField="EndEntry" HeaderText="End Entry" />
                    <asp:BoundField DataField="BookStatus" HeaderText="Status" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f2}" />
                    <asp:BoundField DataField="DateCreated" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
                <PageFooterTemplate>
                    <br />
                    <hr />
                </PageFooterTemplate>
            </wc:ReportGridView>
        </div>
        <table width="600" border="0" style="font-family:Verdana; font-size:11px;  ">
            <tr>
                <td width="440px" align="right" ><b>Total Amount : </b></td>
                <td width="160px" align="left"><hr /><asp:Label ID="lblAmount" runat="server"></asp:Label><hr /></td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
    </div>
    </form>
</body>
</html>
