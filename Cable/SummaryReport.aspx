<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummaryReport.aspx.cs" Inherits="SummaryReport" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Business Transaction Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />  
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />  
    
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>
      <script type="text/javascript"  language="JavaScript">
 function CallPrint(strid)
  {
      var prtContent = document.getElementById(strid);
      var WinPrint = window.open('','','letf=0,top=0,width=600,,toolbar=0,scrollbars=1,status=0');
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
    <a name="#Top"></a>
    <table width="100%" cellpadding="2" style="font-weight:bold">
      <tr>
        <td colspan="14" align ="center" ><h3>BUSINESS TRANSACTION REPORT</h3></td>
        </tr>
   <tr>
<td bgcolor="#E9F1F6"  align="center"> <a href="#CaPD">Purchase</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#paCP">Payment - CAPEX</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#paOP">Payment - OPEX</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#rc">Receipt</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#Subs">Subscription A/c</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#cons">Connection A/c</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#recons">Re-Connection A/c</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#subsc">Subscription Cash A/c</a></td>
<td bgcolor="#E9F1F6" align="center" ><a class="lblFont">All Customer Connections</a>
<table>
<tr>
<td bgcolor="#E9F1F6" align="center"><a href="#nccust">New</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#rccust">Reconnected</a></td>

</tr>
</table>
</td>
<td bgcolor="#E9F1F6" align="center"><a class="lblFont">Customer Connections</a>
<table>
<tr>
<td bgcolor="#E9F1F6" align="center"><a href="#nccustp">New</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#rccustp">Reconnected</a></td>
<td bgcolor="#E9F1F6" align="center"><a href="#dccustp">Disconnected</a></td>
</tr>
</table>
</td>
<td bgcolor="#E9F1F6" align="center"><input visible=true type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" Class="button" />&nbsp;</td>
</tr>

    </table>
     
     <h5></h5>
     <table cellpadding ="2" cellspacing="2" border="0" width="50%">
      

          <tr>
    <td align="right" width="40%" class="tblLeft" >
      Start Date:
    </td>
    <td align="left" width="60%" class="tblLeft" >
      <asp:TextBox ID="txtStartDate" Width="100px" MaxLength="10" Runat="server"  CssClass="txtStyle" />
      <%-- <img src="cal.gif" width="16" height="16" onclick="javascript:NewCal('txtStartDate','ddmmyyyy',false,24)" style="cursor:pointer" border="0" alt="Pick a date">--%>
     <script language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtStartDate'});</script>

    
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtStartDate" Display="None" 
            ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td align="right" class="tblLeft" >
      End Date:
    </td>
    <td align="left" class="tblLeft" >
      <asp:TextBox ID="txtEndDate" Width="100px" MaxLength="10"  Runat="server" CssClass="txtStyle" />
      <script language="JavaScript">new tcal ({'formname': 'form1','controlname': 'txtEndDate'});</script>
    <%--<img src="cal.gif" width="16" onclick="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)" style="cursor:pointer" height="16" border="0" alt="Pick a date">
    --%>
    
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtEndDate" Display="None" 
            ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Display="None" 
            ErrorMessage="Start Date Should Be Less Than the End Date" 
            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
    </td>
  </tr>
        <tr>
        <td colspan="2" align="center"> 
            <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server"  Width="200px" onclick="btnReport_Click" 
            Text="Display Report" />
           </td>
        </tr>
        </table>
        
        <asp:ValidationSummary id="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false"   />
        <br />
    </div>
     <a name="#CaPD"></a>
	   <div id="divPrint" runat="server" visible="false" style="font-family:Verdana; font-size:11px;">
       <%--(Start)1. Cash purchase Details --%>
       <h5>1. Purchase Details</h5>
         <asp:GridView DataKeyNames="PurchaseiD" style="font-family:Verdana; font-size:11px;  " SkinID="ReportGrid" Width="100%" EmptyDataText="No Cash Purchase Found Matching" ID="gvCashPurchase" GridLines="None"  AutoGenerateColumns="False" runat="server" OnRowDataBound="gvCashPurchase_RowDataBound"  ForeColor="#333333" >
         <Columns>
            <asp:TemplateField HeaderText="Bill Date">
                <ItemTemplate>
                     <asp:Label Visible="true"  ID="lblBillDate" runat="server" Text ='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                     <asp:Label Visible="false" ID="lblPurchaseID" runat="server" Text = '<%# Eval("PurchaseID") %>'></asp:Label>
                </ItemTemplate> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Billno">
                <ItemTemplate>
                    <asp:Label style="font-family:Verdana; font-size:11px;  " ID="lblBillno" runat="server" Text = '<%# Eval("Billno") %>' />
                </ItemTemplate> 
            </asp:TemplateField> 
            
            
              <asp:TemplateField HeaderText="Ledger Name">
                <ItemTemplate>
                     <asp:Label   ID="lblLedger" runat="server" Text = '<%# Eval("LedgerName") %>' />
                </ItemTemplate> 
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="Paymode">
                        <ItemTemplate>
                         <asp:Label ID="lblPaymode" runat="server" ></asp:Label>
                        </ItemTemplate> 
                         </asp:TemplateField> 
            <asp:TemplateField HeaderText="Purchase Total Value">
                <ItemTemplate>
                     <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("TotalAmt","{0:f2}") %>' />
                </ItemTemplate> 
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Purchased Items">
                <ItemTemplate>
                <br />
                            <wc:ReportGridView runat="server" BorderWidth="1"   ID="gvProducts" ShowFooter="true" 
                             AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" 
                             Width="400px" style="font-family:Verdana; font-size:11px;  " >
                            <PageHeaderTemplate>
                                <br />
                                <br />
                            </PageHeaderTemplate>
                    
                            <Columns>
                                     <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="25%" >    
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCode" runat="server" Text = '<%# Eval("ItemCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="50%">    
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemDesc" runat="server" Text = '<%# Eval("AssetDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Qty" ItemStyle-Width="25%">  
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text = '<%# Eval("Qty") %>' />       
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                  
                                      
                                    
                                     
                                   
                             </Columns>          
                            <PagerTemplate>
                        
                            </PagerTemplate>
                            <PageFooterTemplate>
                            <br /> 
                            </PageFooterTemplate>
                       </wc:ReportGridView>
              </ItemTemplate> 
            </asp:TemplateField>
        
        </Columns>
        </asp:GridView><br />
        <b class="tblLeft">Total Cash Purchase: </b><asp:label Font-Bold="true"  ID="lblGrantCashPurchase" CssClass="tblLeft" runat="server"></asp:label><br />
       <%--(End)1. Cash purchase Details--%>
      
	 <a href="#Top">Back To Top</a>
          <a name="#paCP"></a>
           <h5>2. Payment Details - Expense Type : CAPEX</h5>           
                 <asp:GridView DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Payments Found Matching"  Width="100%" ID="GrdViewPaymentC" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" 
                  OnRowDataBound="GrdViewPaymentC_RowDataBound"
                 >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                         <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
                         
            
           
        
        </Columns>
        </asp:GridView><br />
        <b class="tblLeft" >Total CAPEX Payment: </b><asp:label Font-Bold="true"  ID="lblPaymentCapex" runat="server" CssClass="tblLeft" ></asp:label><br />    
        <a name="#paOP"></a>
	 <a href="#Top">Back To Top</a>   
           <h5>3. Payment Details - Expense Type : OPEX </h5>           
                 <asp:GridView  OnRowDataBound="GrdViewPaymentO_RowDataBound" DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Payments Found Matching"  Width="100%" ID="GrdViewPaymentO" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                          <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
        </Columns>
        </asp:GridView><br /> 
        <b class="tblLeft" >Total OPEX Payment: </b><asp:label Font-Bold="true"  ID="lblPaymentOpex" runat="server" CssClass="tblLeft" ></asp:label><br />    
         <a href="#Top">Back To Top</a> 
         <a name="#rc"></a>
           <h5>4. Receipt Details</h5>   
            <asp:GridView  OnRowDataBound="grdReceipt_RowDataBound" DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Receipts Found Matching"  Width="100%" ID="grdReceipt" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                          <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
                        
           
        
        </Columns>
        </asp:GridView><br />  
          <b class="tblLeft" >Total Receipt: </b><asp:label Font-Bold="true"  ID="lblTotalReceipt" runat="server" CssClass="tblLeft" ></asp:label><br />    
         <a href="#Top">Back To Top</a> 
         <a name="#subs"></a>
           <h5>5. Subscribtion A/C </h5>   
            <asp:GridView  OnRowDataBound="grdSubs_RowDataBound" DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion A/C  Found Matching"  Width="100%" ID="grdSubs" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                          <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
        </Columns>
        </asp:GridView><br />  
          <b class="tblLeft" >Total Subscribtion A/C: </b><asp:label Font-Bold="true"  ID="lblTotalSubs" runat="server" CssClass="tblLeft" ></asp:label><br />    
         <a href="#Top">Back To Top</a> 
         <a name="#cons"></a>
         <h5>6. Installation A/c</h5>   
            <asp:GridView  OnRowDataBound="grdConn_RowDataBound" DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Connection A/C  Found Matching"  Width="100%" ID="grdConn" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                          <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
                        
           
        
        </Columns>
        </asp:GridView><br />  
          <b class="tblLeft" >Total Connection A/C: </b><asp:label Font-Bold="true"  ID="lblTotalCon" runat="server" CssClass="tblLeft" ></asp:label><br />    
         
         
        <a href="#Top">Back To Top</a> 
         <a name="#recons"></a>
         <h5>7. Re-Installation A/c</h5>   
            <asp:GridView  OnRowDataBound="grdReConn_RowDataBound" DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Re-Connection A/C  Found Matching"  Width="100%" ID="grdReConn" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                          <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
                        
           
        
        </Columns>
        </asp:GridView><br />  
          <b class="tblLeft" >Total Re-Connection A/C: </b><asp:label Font-Bold="true"  ID="lblTotalRecon" runat="server" CssClass="tblLeft" ></asp:label><br />    
         <a href="#Top">Back To Top</a>
         
         <a name="#subsc"></a>
           <h5>9. Subscribtion Cash A/C </h5>   
            <asp:GridView  OnRowDataBound="grdSubsC_RowDataBound" DataKeyNames="TransNo" SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion Cash A/C  Found Matching"  Width="100%" ID="grdSubsC" GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              <asp:BoundField DataField="RefNo" HeaderText="Ref No." />
                        <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Debi" HeaderText="Payed To" />
                        <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" />
                        <asp:BoundField DataField="Paymode" HeaderText="Pay Mode" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Check No" />
                          <asp:TemplateField HeaderText="Amount">
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                 <asp:Label  ID="lblTotal" runat="server" Text = '<%# Eval("Amount","{0:f2}") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
        </Columns>
        </asp:GridView><br />  
          <b class="tblLeft" >Total Subscribtion Cash A/C: </b><asp:label Font-Bold="true"  ID="lblSubsCash" runat="server" CssClass="tblLeft" ></asp:label><br />    
        <a href="#Top">Back To Top</a>
         
        
           <h5>10.All Current Connections </h5>   
           <br />  
            <a name="#nccust"></a>
           <b>A. New Connection Customers</b>
           <asp:GridView ID="gvNew"  OnRowDataBound="gvNC_RowDataBound"  SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion Cash A/C  Found Matching"  Width="100%"  GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              
                        <asp:BoundField DataField="Area" HeaderText="" />
                        <asp:BoundField DataField="Total" HeaderText="" />
                        
        </Columns>
        </asp:GridView><br />
          <b class="tblLeft" >Total : </b><asp:label Font-Bold="true"  ID="lblNc" runat="server" CssClass="tblLeft" ></asp:label><br />    
          <a href="#Top">Back To Top</a><br />
        <a name="#rccust"></a>
        <b>B. Re Connection Customers</b>
           <asp:GridView ID="gvRc"  OnRowDataBound="gvRC_RowDataBound"  SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion Cash A/C  Found Matching"  Width="100%"  GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              
                        <asp:BoundField DataField="Area" HeaderText="" />
                        <asp:BoundField DataField="Total" HeaderText="" />
                        
        </Columns>
        </asp:GridView><br />
          <b class="tblLeft" >Total : </b><asp:label Font-Bold="true"  ID="lblRc" runat="server" CssClass="tblLeft" ></asp:label><br />    
        <a href="#Top">Back To Top</a><br />
        
        
        <h5>11. Current Connections during the period <asp:Label ID="lblDat" runat="server" CssClass="lblFont"  ></asp:Label></h5>   
           <br /> 
           
            <a name="#nccustp"></a>
           <b>A. New Connection Customers</b>
           <asp:GridView ID="gvNCCur"  OnRowDataBound="gvNCCur_RowDataBound"  SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion Cash A/C  Found Matching"  Width="100%"  GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              
                        <asp:BoundField DataField="Area" HeaderText="" />
                        <asp:BoundField DataField="Total" HeaderText="" />
                        
        </Columns>
        </asp:GridView><br />
          <b class="tblLeft" >Total : </b><asp:label Font-Bold="true"  ID="lblNcCur" runat="server" CssClass="tblLeft" ></asp:label><br />    
          <a href="#Top">Back To Top</a><br />
        <a name="#rccustp"></a>
        <b>B. Re Connection Customers</b>
           <asp:GridView ID="gvRCCur"  OnRowDataBound="gvRCCur_RowDataBound"  SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion Cash A/C  Found Matching"  Width="100%"  GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              
                        <asp:BoundField DataField="Area" HeaderText="" />
                        <asp:BoundField DataField="Total" HeaderText="" />
                                
        </Columns>
        </asp:GridView><br />
          <b class="tblLeft" >Total : </b><asp:label Font-Bold="true"  ID="lblRcCur" runat="server" CssClass="tblLeft" ></asp:label><br />    
        <a href="#Top">Back To Top</a><br />
         <a name="#dccustp"></a>
        <b>C. DisConnection Customers</b>
           <asp:GridView ID="gvDCCur"  OnRowDataBound="gvDCCur_RowDataBound"  SkinID="ReportGrid" style="font-family:Verdana; font-size:11px;  " EmptyDataText="No Subscribtion Cash A/C  Found Matching"  Width="100%"  GridLines="None"  AutoGenerateColumns="False" runat="server" ForeColor="#333333" >
         <Columns>
              
                        <asp:BoundField DataField="Area" HeaderText="" />
                        <asp:BoundField DataField="Total" HeaderText="" />
                        
        </Columns>
        </asp:GridView><br />
          <b class="tblLeft" >Total : </b><asp:label Font-Bold="true"  ID="lblDcCur" runat="server" CssClass="tblLeft" ></asp:label><br />    
       <a href="#Top">Back To Top</a><br />
        
         </div>
    </form>
</body>
</html>

