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
using System.Xml.Linq;

public partial class SummaryReport : System.Web.UI.Page
{
    Double SumCashPurchase = 0.0d;

    Double SumCapex = 0.0d;
    Double SumOpex = 0.0d;
    Double SumReceipt = 0.0d;
    Double SumSubs = 0.0d;
    Double SumSubscash = 0.0d;
    Double SumCon = 0.0d;
    Double SumRecon = 0.0d;

    Double SumNc = 0.0d;
    Double SumRc = 0.0d;
    Double SumNcCur = 0.0d;
    Double SumRcCur = 0.0d;
    Double SumDcCur = 0.0d;


    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            DataSet companyInfo = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        divPrint.Visible = true;
        DateTime startDate, endDate;
        //string sDataSource = sDatasource;//ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        GenerateCashPurchase(sDataSource, startDate, endDate);
        GenerateReceipt(sDataSource, startDate, endDate);
        GenerateCable(sDataSource, startDate, endDate, "Subscription A/C");
        GenerateCable(sDataSource, startDate, endDate, "Connection A/C");
        GenerateCable(sDataSource, startDate, endDate, "Re-Connection A/C");
        GenerateCableSubsCash(sDataSource, startDate, endDate, "Subscription A/C");

        GeneratePayment(sDataSource, startDate, endDate, "CAPEX");
        GeneratePayment(sDataSource, startDate, endDate, "OPEX");
        lblDat.Text = startDate.ToShortDateString() + " To " + endDate.ToShortDateString();
        GenerateCustomerList(sDataSource);

    }
    protected void GenerateCustomerList(string sDataSource)
    {
        CustomerReportBL.ReportClass rpt = new CustomerReportBL.ReportClass();
        DataSet ds = rpt.getCustomersTotal(sDataSource, "NC");
        gvNew.DataSource = ds;
        gvNew.DataBind();

        ds = rpt.getCustomersTotal(sDataSource, "RC");
        gvRc.DataSource = ds;
        gvRc.DataBind();

        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        ds = rpt.getCustomersTotal(sDataSource, "NC", startDate, endDate);
        gvNCCur.DataSource = ds;
        gvNCCur.DataBind();


        ds = rpt.getCustomersTotal(sDataSource, "RC", startDate, endDate);
        gvRCCur.DataSource = ds;
        gvRCCur.DataBind();


        ds = rpt.getCustomersTotal(sDataSource, "DC", startDate, endDate);
        gvDCCur.DataSource = ds;
        gvDCCur.DataBind();


    }
    protected void GenerateReceipt(string sDataSource, DateTime startDate, DateTime endDate)
    {
        CustomerReportBL.ReportClass rptCash = new CustomerReportBL.ReportClass();
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        DataSet ds = rptCash.getReceipt(startDate, endDate, sDataSource);

        grdReceipt.DataSource = ds;
        grdReceipt.DataBind();

    }
    protected void GeneratePayment(string sDataSource, DateTime startDate, DateTime endDate, string expenseType)
    {
        CustomerReportBL.ReportClass rptCash = new CustomerReportBL.ReportClass();
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        DataSet ds = rptCash.getPayment(expenseType, startDate, endDate, sDataSource);
        if (expenseType == "CAPEX")
        {
            GrdViewPaymentC.DataSource = ds;
            GrdViewPaymentC.DataBind();
        }
        else
        {
            GrdViewPaymentO.DataSource = ds;
            GrdViewPaymentO.DataBind();
        }
    }
    protected void GenerateCashPurchase(string sDataSource, DateTime startDate, DateTime endDate)
    {
        CustomerReportBL.ReportClass rptCash = new CustomerReportBL.ReportClass();
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        DataSet ds = rptCash.generatePurchaseReport(sDataSource, startDate, endDate);
        gvCashPurchase.DataSource = ds;
        gvCashPurchase.DataBind();
    }
    protected void GenerateCable(string sDataSource, DateTime startDate, DateTime endDate, String ac)
    {
        CustomerReportBL.ReportClass rptCash = new CustomerReportBL.ReportClass();
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        //DataSet ds = rptCash.getCableAc(startDate, endDate,ac, sDataSource);
        DataSet ds = new DataSet();
        if (ac == "Subscription A/C")
        {
            grdSubs.DataSource = rptCash.getSubscriptionAc(startDate, endDate, ac, sDataSource);
            grdSubs.DataBind();
        }
        if (ac == "Connection A/C")
        {
            grdConn.DataSource = rptCash.getCableAc(startDate, endDate, ac, sDataSource); ;
            grdConn.DataBind();
        }
        if (ac == "Re-Connection A/C")
        {
            grdReConn.DataSource = rptCash.getCableAc(startDate, endDate, ac, sDataSource); ;
            grdReConn.DataBind();
        }


    }
    protected void GenerateCableSubsCash(string sDataSource, DateTime startDate, DateTime endDate, String ac)
    {
        CustomerReportBL.ReportClass rptCash = new CustomerReportBL.ReportClass();
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        //DataSet ds = rptCash.getCableAc(startDate, endDate,ac, sDataSource);
        DataSet ds = new DataSet();
        if (ac == "Subscription A/C")
        {
            grdSubsC.DataSource = rptCash.getCableAc(startDate, endDate, ac, sDataSource);
            grdSubsC.DataBind();
        }
    }
    #region Row bund Events
    protected void gvCashPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
            Label lblPurchaseID = e.Row.FindControl("lblPurchaseID") as Label;
            int purchaseID = Convert.ToInt32(lblPurchaseID.Text);
            CustomerReportBL.ReportClass rptProduct = new CustomerReportBL.ReportClass();
            DataSet ds = rptProduct.getProducts(purchaseID, sDataSource);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();
            }
            SumCashPurchase = SumCashPurchase + Convert.ToDouble(lblTotalAmt.Text);
            lblGrantCashPurchase.Text = "Rs. " + SumCashPurchase.ToString("f2");


            string paymode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymode"));
            Label payMode = (Label)e.Row.FindControl("lblPaymode");
            if (paymode == "1")
                payMode.Text = "Cash";
            else if (paymode == "2")
                payMode.Text = "Bank";
            else
                payMode.Text = "Credit";
        }
    }

    protected void GrdViewPaymentC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumCapex = SumCapex + Convert.ToDouble(lblTotalAmt.Text);
            lblPaymentCapex.Text = "Rs. " + SumCapex.ToString("f2");

        }
    }
    protected void GrdViewPaymentO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumOpex = SumOpex + Convert.ToDouble(lblTotalAmt.Text);
            lblPaymentOpex.Text = "Rs. " + SumOpex.ToString("f2");

        }
    }
    //grdReceipt_RowDataBound
    protected void grdReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumReceipt = SumReceipt + Convert.ToDouble(lblTotalAmt.Text);
            lblTotalReceipt.Text = "Rs. " + SumReceipt.ToString("f2");

        }
    }
    protected void grdSubs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumSubs = SumSubs + Convert.ToDouble(lblTotalAmt.Text);
            lblTotalSubs.Text = "Rs. " + SumSubs.ToString("f2");

        }
    }
    protected void grdSubsC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumSubscash = SumSubscash + Convert.ToDouble(lblTotalAmt.Text);
            lblSubsCash.Text = "Rs. " + SumSubscash.ToString("f2");

        }
    }
    protected void grdConn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumCon = SumCon + Convert.ToDouble(lblTotalAmt.Text);
            lblTotalCon.Text = "Rs. " + SumCon.ToString("f2");

        }
    }
    protected void grdReConn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // sumDbl = 0;
        //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = e.Row.FindControl("gvProducts") as GridView;
            Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;

            SumRecon = SumRecon + Convert.ToDouble(lblTotalAmt.Text);
            lblTotalRecon.Text = "Rs. " + SumRecon.ToString("f2");

        }
    }


    protected void gvNC_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SumNc = SumNc + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            lblNc.Text = "Rs. " + SumNc.ToString("f2");
        }
    }
    protected void gvRC_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SumRc = SumRc + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            lblRc.Text = "Rs. " + SumRc.ToString("f2");
        }
    }
    protected void gvNCCur_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SumNcCur = SumNcCur + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            lblNcCur.Text = "Rs. " + SumNcCur.ToString("f2");
        }
    }
    protected void gvRCCur_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SumRcCur = SumRcCur + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            lblRcCur.Text = "Rs. " + SumRcCur.ToString("f2");
        }
    }
    protected void gvDCCur_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SumDcCur = SumDcCur + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            lblDcCur.Text = "Rs. " + SumDcCur.ToString("f2");
        }
    }
    // 
    #endregion
}
