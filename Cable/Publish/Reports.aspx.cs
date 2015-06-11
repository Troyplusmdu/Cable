using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            System.Text.StringBuilder cashScript = new System.Text.StringBuilder();
            cashScript.Append("window.open('CustomerReport.aspx");
            cashScript.Append(" ','Cash', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=700,width=800,left=10,top=10, scrollbars=yes');");
            lnkCustDetails.Attributes.Add("OnClick", cashScript.ToString());

            System.Text.StringBuilder cashReport = new System.Text.StringBuilder();
            cashReport.Append("window.open('CashDetailsReport.aspx");
            cashReport.Append(" ','Cash', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=700,width=800,left=10,top=10, scrollbars=yes');");
            lnkCashDetails.Attributes.Add("OnClick", cashReport.ToString());

            System.Text.StringBuilder billReport = new System.Text.StringBuilder();
            billReport.Append("window.open('BillDetailReport.aspx");
            billReport.Append(" ','Cash', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1200,width=800,left=10,top=10, scrollbars=yes');");
            lnkBillDetail.Attributes.Add("OnClick", billReport.ToString());

            System.Text.StringBuilder outstndBalReport = new System.Text.StringBuilder();
            outstndBalReport.Append("window.open('OutstandingBalance.aspx");
            outstndBalReport.Append(" ','OutstandingBalance', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1200,width=800,left=10,top=10, scrollbars=yes');");
            lnkOutStndBal.Attributes.Add("OnClick", outstndBalReport.ToString());

            System.Text.StringBuilder adjstmntReport = new System.Text.StringBuilder();
            adjstmntReport.Append("window.open('AdjustmentDetailsReport.aspx");
            adjstmntReport.Append(" ','AdjstMnt', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1200,width=600,left=10,top=10, scrollbars=yes');");
            lnkAdjstReport.Attributes.Add("OnClick", adjstmntReport.ToString());

            System.Text.StringBuilder BnkStmtReport = new System.Text.StringBuilder();
            BnkStmtReport.Append("window.open('BankStatementReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkBnkStmtReport.Attributes.Add("OnClick", BnkStmtReport.ToString());

            System.Text.StringBuilder LedgerReport = new System.Text.StringBuilder();
            LedgerReport.Append("window.open('LedgerReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkLedgerReport.Attributes.Add("OnClick", LedgerReport.ToString());

            System.Text.StringBuilder DayBookReport = new System.Text.StringBuilder();
            DayBookReport.Append("window.open('DayBookReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkDayBookReport.Attributes.Add("OnClick", DayBookReport.ToString());

            System.Text.StringBuilder AssDetReport = new System.Text.StringBuilder();
            AssDetReport.Append("window.open('AssetDetailsReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkAssDetReport.Attributes.Add("OnClick", AssDetReport.ToString());

            System.Text.StringBuilder BusTransReport = new System.Text.StringBuilder();
            BusTransReport.Append("window.open('SummaryReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkBusTransReport.Attributes.Add("OnClick", BusTransReport.ToString());

            System.Text.StringBuilder FraudReport = new System.Text.StringBuilder();
            FraudReport.Append("window.open('FraudCheckReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkFraudReport.Attributes.Add("OnClick", FraudReport.ToString());

            System.Text.StringBuilder ComparisonReport = new System.Text.StringBuilder();
            ComparisonReport.Append("window.open('MonthComparisonReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=800,width=900,left=10,top=10, scrollbars=yes');");
            lnkMonthlyReport.Attributes.Add("OnClick", ComparisonReport.ToString());


            if (Session["CashMode"] != null)
            {
                if (Session["CashMode"].ToString() == "Book")
                {
                    lnkBookEntryReport.Visible = true;
                    System.Text.StringBuilder BookEntryReport = new System.Text.StringBuilder();
                    BookEntryReport.Append("window.open('BookReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
                    lnkBookEntryReport.Attributes.Add("OnClick", BookEntryReport.ToString());
                }
                else
                {
                    lnkBookEntryReport.Visible = false;
                }
            }

            if (!Page.User.IsInRole("DUELSTRPT"))
                lnkCustDetails.Visible = false;

            if (!Page.User.IsInRole("CASHRPT"))
                lnkCashDetails.Visible = false;

            if (!Page.User.IsInRole("BILLRPT"))
                lnkBillDetail.Visible = false;

            if (!Page.User.IsInRole("CSHOUTANY"))
                lnkOutStndBal.Visible = false;

            if (!Page.User.IsInRole("ADJREPT"))
                lnkAdjstReport.Visible = false;

            if (!Page.User.IsInRole("BNKSTRPT"))
                lnkBnkStmtReport.Visible = false;

            if (!Page.User.IsInRole("LEDGRPT"))
                lnkLedgerReport.Visible = false;

            if (!Page.User.IsInRole("DAYBKREPRT"))
                lnkDayBookReport.Visible = false;

            if (!Page.User.IsInRole("ASSDETRPT"))
                lnkAssDetReport.Visible = false;

            if (!Page.User.IsInRole("BUSTRNSRPT"))
                lnkBusTransReport.Visible = false;

            if (!Page.User.IsInRole("FRDCHKRPT"))
                lnkFraudReport.Visible = false;

            if (!Page.User.IsInRole("BKENTRPT"))
                lnkBookEntryReport.Visible = false;

            if (!Page.User.IsInRole("TRBALRPT"))
                lnkBtnTrailBal.Visible = false;

            if (!Page.User.IsInRole("BALSHTRPT"))
                lnkBtnBalSheet.Visible = false;

            if (!Page.User.IsInRole("PRLSSRPT"))
                lnkBtnProfitLoss.Visible = false;

            if (!Page.User.IsInRole("MNTCMPRPT"))
                lnkMonthlyReport.Visible = false;

        }
    }
}
