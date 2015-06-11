<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthComparisonReport.aspx.cs" Inherits="MonthComparisonReport" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comparison Report</title>
     <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript"  language="JavaScript">
    function unl()
    {
    document.form1.submit();
    }
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
<body style="background-color:White">
    <form id="form1" runat="server">
   <div style="margin:20px 80px 20px 60px; text-align:center">
         <table cellpadding="2" style="border: solid 0px Silver; text-align: left" cellspacing="2"
            width="90%">
            <tr>
                <td colspan="4" class="SectionHeader" >
                        Monthly Comparison Report
                </td>
            </tr>
            <tr>
            <td class="LMSLeftColumnColor">Month</td>
            <td class="LMSLeftColumnColor"><asp:DropDownList ID="drpSMonth" runat="server">
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            <asp:ListItem Value="5">5</asp:ListItem>
            <asp:ListItem Value="6">6</asp:ListItem>
            <asp:ListItem Value="7">7</asp:ListItem>
            <asp:ListItem Value="8">8</asp:ListItem>
            <asp:ListItem Value="9">9</asp:ListItem>
            <asp:ListItem Value="10">10</asp:ListItem>
            <asp:ListItem Value="11">11</asp:ListItem>
            <asp:ListItem Value="12">12</asp:ListItem>
            </asp:DropDownList></td>
            <td class="LMSLeftColumnColor">Year</td>
            <td class="LMSLeftColumnColor">
            <asp:DropDownList ID="drpSYear" runat="server">
            <asp:ListItem Value="2015">2015</asp:ListItem>
            <asp:ListItem Value="2014">2014</asp:ListItem>
            <asp:ListItem Value="2013">2013</asp:ListItem>
            <asp:ListItem Value="2012">2012</asp:ListItem>
            <asp:ListItem Value="2011">2011</asp:ListItem>
            <asp:ListItem Value="2010">2010</asp:ListItem>
            <asp:ListItem Value="2009">2009</asp:ListItem>
            <asp:ListItem Value="2008">2008</asp:ListItem>
            <asp:ListItem Value="2007">2007</asp:ListItem>
            <asp:ListItem Value="2006">2006</asp:ListItem>
            <asp:ListItem Value="2005">2005</asp:ListItem>
            <asp:ListItem Value="2004">2004</asp:ListItem>
            <asp:ListItem Value="2003">2003</asp:ListItem>
            <asp:ListItem Value="2002">2002</asp:ListItem>
            <asp:ListItem Value="2001">2001</asp:ListItem>
            </asp:DropDownList> 
            </td>
            </tr>
            
            <tr>
            <td class="LMSLeftColumnColor">Month To Compare</td>
            <td class="LMSLeftColumnColor"><asp:DropDownList ID="drpEMonth" runat="server">
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            <asp:ListItem Value="5">5</asp:ListItem>
            <asp:ListItem Value="6">6</asp:ListItem>
            <asp:ListItem Value="7">7</asp:ListItem>
            <asp:ListItem Value="8">8</asp:ListItem>
            <asp:ListItem Value="9">9</asp:ListItem>
            <asp:ListItem Value="10">10</asp:ListItem>
            <asp:ListItem Value="11">11</asp:ListItem>
            <asp:ListItem Value="12">12</asp:ListItem>
            </asp:DropDownList></td>
            <td class="LMSLeftColumnColor">Year To Compare</td>
            <td class="LMSLeftColumnColor">
            <asp:DropDownList ID="drpEYear" runat="server">
            <asp:ListItem Value="2015">2015</asp:ListItem>
            <asp:ListItem Value="2014">2014</asp:ListItem>
            <asp:ListItem Value="2013">2013</asp:ListItem>
            <asp:ListItem Value="2012">2012</asp:ListItem>
            <asp:ListItem Value="2011">2011</asp:ListItem>
            <asp:ListItem Value="2010">2010</asp:ListItem>
            <asp:ListItem Value="2009">2009</asp:ListItem>
            <asp:ListItem Value="2008">2008</asp:ListItem>
            <asp:ListItem Value="2007">2007</asp:ListItem>
            <asp:ListItem Value="2006">2006</asp:ListItem>
            <asp:ListItem Value="2005">2005</asp:ListItem>
            <asp:ListItem Value="2004">2004</asp:ListItem>
            <asp:ListItem Value="2003">2003</asp:ListItem>
            <asp:ListItem Value="2002">2002</asp:ListItem>
            <asp:ListItem Value="2001">2001</asp:ListItem>
            </asp:DropDownList> 
            </td>
            </tr>
             <tr>
                <td colspan="4">
                <asp:Button ID="btnPrint" runat="server" SkinID="skinButtonBig" OnClick="btnPrint_Click" Text="Generate Report"/>
                &nbsp; <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" class="button"   />&nbsp;
                </td>
            </tr>
            </table>
            <div id="divPrint" style="font-family:Verdana; font-size:11px;  ">
            <table cellspacing="4" >
            <tr>
            <td colspan="2"><h4>Month Comparison Report</h4></td>
            </tr>
            <tr>
            <td>Month 1:</td>
            <td><asp:Label ID="lblMonthStart" runat="server" CssClass=""lblFont"></asp:Label></td>
            </tr>
            <tr>
            <td>Month 2:</td>
            <td><asp:Label ID="lblMonthCompare" runat="server" CssClass=""lblFont"></asp:Label></td>
            </tr>
            </table>
     <wc:ReportGridView runat="server" BorderWidth="1" ID="gvReport" GridLines="Both" HeaderStyle-HorizontalAlign="Left"   
                     AutoGenerateColumns="false" PrintPageSize="47" AllowPrintPaging="true"  OnRowDataBound="gvReport_RowDataBound"  
                Width="100%" CellPadding="3" ShowFooter="true"   style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Details Found Matching" >
                <PageHeaderTemplate>
                <br />
                 
               
                <br />
            </PageHeaderTemplate>
            
            <Columns>
                
                <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"   DataField="Area" HeaderText="Area"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1Bill" HeaderText="Month1Bill"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"    DataField="Month2Bill" HeaderText="Month2Bill"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1SubsCash" HeaderText="Month1SubsCash"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2SubsCash" HeaderText="Month2SubsCash"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1adj" HeaderText="Month1adj"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2adj" HeaderText="Month1adj"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1InstCash" HeaderText="Month1InstCash"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2InstCash" HeaderText="Month2InstCash"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1ReInstCash" HeaderText="Month1ReInstCash"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2ReInstCash" HeaderText="Month2ReInstCash"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left"   DataField="Month1NewCustCount" HeaderText="Month1NewCustCount"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left"   DataField="Month2NewCustCount" HeaderText="Month2NewCustCount"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left"   DataField="Month1RcCustCount" HeaderText="Month1RcCustCount"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left"   DataField="Month2RcCustCount" HeaderText="Month2RcCustCount"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left"   DataField="Month1DcCustCount" HeaderText="Month1DcCustCount"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left"   DataField="Month2DcCustCount" HeaderText="Month2DcCustCount"/>
               
               
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"    DataField="Month1NewCustValue" HeaderText="Month1NewCustValue"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2NewCustValue" HeaderText="Month2NewCustValue"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1RcCustValue" HeaderText="Month1RcCustValue"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2RcCustValue" HeaderText="Month2RcCustValue"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month1DcCustValue" HeaderText="Month1DcCustValue"/>
                <asp:BoundField ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"   DataField="Month2DcCustValue" HeaderText="Month2DcCustValue"/>
            </Columns>            
            <PagerTemplate>
        
            </PagerTemplate>
            <PageFooterTemplate>
                
            </PageFooterTemplate>
            <FooterStyle Font-Bold="true" />
        </wc:ReportGridView>
        
        </div>
    </div>
    </form>
</body>
</html>

