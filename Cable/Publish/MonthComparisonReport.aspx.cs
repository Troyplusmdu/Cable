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

public partial class MonthComparisonReport : System.Web.UI.Page
{

    public double month1bill = 0;
    public double month2bill = 0;
    public double month1subs = 0;
    public double month2subs = 0;
    public double month1adj = 0;
    public double month2adj = 0;
    public double month1ins = 0;
    public double month2ins = 0;
    public double month1reins = 0;
    public double month2reins = 0;
    public double month1ncCnt = 0;
    public double month2ncCnt = 0;
    public double month1rcCnt = 0;
    public double month2rcCnt = 0;
    public double month1dcCnt = 0;
    public double month2dcCnt = 0;
    public double month1ncValue = 0;
    public double month2ncValue = 0;
    public double month1rcValue = 0;
    public double month2rcValue = 0;
    public double month1dcValue = 0;
    public double month2dcValue = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ListItem liMonth = drpSMonth.Items.FindByText(Convert.ToString(DateTime.Now.Month));
            if (liMonth != null) liMonth.Selected = true;

            ListItem liYear = drpSYear.Items.FindByText(Convert.ToString(DateTime.Now.Year));
            if (liYear != null) liYear.Selected = true;


            ListItem liMonth2 = drpEMonth.Items.FindByText(Convert.ToString(DateTime.Now.Month));
            if (liMonth2 != null) liMonth2.Selected = true;

            ListItem liYear2 = drpEYear.Items.FindByText(Convert.ToString(DateTime.Now.Year));
            if (liYear2 != null) liYear2.Selected = true;
        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {



        int syear = Convert.ToInt32(drpSYear.SelectedItem.Text);
        int smonth = Convert.ToInt32(drpSMonth.SelectedItem.Text);
        int eyear = Convert.ToInt32(drpEYear.SelectedItem.Text);
        int emonth = Convert.ToInt32(drpEMonth.SelectedItem.Text);

        lblMonthStart.Text = retMonth(smonth) + " - " + syear;
        lblMonthCompare.Text = retMonth(emonth) + " - " + eyear;

        if (syear <= DateTime.Now.Year && eyear <= DateTime.Now.Year)
        {

            //if (smonth <= DateTime.Now.Month  && emonth <= DateTime.Now.Month)
            //{
            if (syear == DateTime.Now.Year && smonth > DateTime.Now.Month)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select the Year less than or equal to Current Year And  month less than or equal to current month')", true);
            }
            if (eyear == DateTime.Now.Year && emonth > DateTime.Now.Month)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select the Year less than or equal to Current Year And  month less than or equal to current month')", true);
            }
            DataSet ds = GenerateDs();
            if (ds != null)
            {
                gvReport.DataSource = ds;
                gvReport.DataBind();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select the Year less than or equal to current year')", true);
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            e.Row.Cells[1].Text = lblMonthStart.Text + " Monthly Bill";
            e.Row.Cells[2].Text = lblMonthCompare.Text + " Monthly Bill";

            e.Row.Cells[3].Text = lblMonthStart.Text + " Subs Cash";
            e.Row.Cells[4].Text = lblMonthCompare.Text + " Subs Cash";

            e.Row.Cells[5].Text = lblMonthStart.Text + " Adjustment";
            e.Row.Cells[6].Text = lblMonthCompare.Text + " Adjustment";

            e.Row.Cells[7].Text = lblMonthStart.Text + " Installation";
            e.Row.Cells[8].Text = lblMonthCompare.Text + " Installation";

            e.Row.Cells[9].Text = lblMonthStart.Text + " ReInstallation";
            e.Row.Cells[10].Text = lblMonthCompare.Text + " ReInstallation";

            e.Row.Cells[11].Text = lblMonthStart.Text + " NC Count";
            e.Row.Cells[12].Text = lblMonthCompare.Text + " NC Count";

            e.Row.Cells[13].Text = lblMonthStart.Text + " RC Count";
            e.Row.Cells[14].Text = lblMonthCompare.Text + " RC Count";

            e.Row.Cells[15].Text = lblMonthStart.Text + " DC Count";
            e.Row.Cells[16].Text = lblMonthCompare.Text + " DC Count";

            e.Row.Cells[17].Text = lblMonthStart.Text + " NC Value";
            e.Row.Cells[18].Text = lblMonthCompare.Text + " NC Value";

            e.Row.Cells[19].Text = lblMonthStart.Text + " RC Value";
            e.Row.Cells[20].Text = lblMonthCompare.Text + " RC Value";

            e.Row.Cells[21].Text = lblMonthStart.Text + " DC Value";
            e.Row.Cells[22].Text = lblMonthCompare.Text + " DC Value";


        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            month1bill = month1bill + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1Bill"));
            month2bill = month2bill + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2Bill"));

            month1subs = month1subs + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1SubsCash"));
            month2subs = month2subs + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2SubsCash"));

            month1adj = month1adj + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1adj"));
            month2adj = month2adj + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2adj"));

            month1ins = month1ins + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1InstCash"));
            month2ins = month2ins + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2InstCash"));

            month1reins = month1reins + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1ReInstCash"));
            month2reins = month2reins + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2ReInstCash"));

            month1ncCnt = month1ncCnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1NewCustCount"));
            month2ncCnt = month2ncCnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2NewCustCount"));

            month1rcCnt = month1rcCnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1RCCustCount"));
            month2rcCnt = month2rcCnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2RCCustCount"));

            month1dcCnt = month1dcCnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1DCCustCount"));
            month2dcCnt = month2dcCnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2DCCustCount"));

            month1ncValue = month1ncValue + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1NewCustValue"));
            month2ncValue = month2ncValue + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2NewCustValue"));

            month1rcValue = month1rcValue + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1RCCustValue"));
            month2rcValue = month2rcValue + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2RCCustValue"));

            month1dcValue = month1dcValue + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month1DCCustValue"));
            month2dcValue = month2dcValue + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Month2DCCustValue"));

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Right;


            e.Row.Cells[1].Text = month1bill.ToString("N2");
            e.Row.Cells[2].Text = month2bill.ToString("N2");
            e.Row.Cells[3].Text = month1subs.ToString("N2");
            e.Row.Cells[4].Text = month2subs.ToString("N2");
            e.Row.Cells[5].Text = month1adj.ToString("N2");
            e.Row.Cells[6].Text = month2adj.ToString("N2");
            e.Row.Cells[7].Text = month1ins.ToString("N2");
            e.Row.Cells[8].Text = month2ins.ToString("N2");
            e.Row.Cells[9].Text = month1reins.ToString("N2");
            e.Row.Cells[10].Text = month2reins.ToString("N2");
            e.Row.Cells[11].Text = month1ncCnt.ToString("N2");
            e.Row.Cells[12].Text = month2ncCnt.ToString("N2");
            e.Row.Cells[13].Text = month1rcCnt.ToString("N2");
            e.Row.Cells[14].Text = month2rcCnt.ToString("N2");
            e.Row.Cells[15].Text = month1dcCnt.ToString("N2");
            e.Row.Cells[16].Text = month2dcCnt.ToString("N2");
            e.Row.Cells[17].Text = month1ncValue.ToString("N2");
            e.Row.Cells[18].Text = month2ncValue.ToString("N2");
            e.Row.Cells[19].Text = month1rcValue.ToString("N2");
            e.Row.Cells[20].Text = month2rcValue.ToString("N2");
            e.Row.Cells[21].Text = month1dcValue.ToString("N2");
            e.Row.Cells[22].Text = month2dcValue.ToString("N2");

        }
    }
    public string retMonth(int Month)
    {
        string monthname = string.Empty;
        if (Month == 12)
            monthname = "Dec";
        else if (Month == 11)
            monthname = "Nov";
        else if (Month == 10)
            monthname = "Oct";
        else if (Month == 9)
            monthname = "Sep";
        else if (Month == 8)
            monthname = "Aug";
        else if (Month == 7)
            monthname = "Jul";
        else if (Month == 6)
            monthname = "Jun";
        else if (Month == 5)
            monthname = "May";
        else if (Month == 4)
            monthname = "Apr";
        else if (Month == 3)
            monthname = "Mar";
        else if (Month == 2)
            monthname = "Feb";
        else
            monthname = "Jan";


        return monthname;
    }
    public DataSet GenerateDs()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        }

        string sDataSource = connStr;
        CustomerReportBL.ReportClass rpt;
        int smonth = 0;
        int syear = 0;
        int emonth = 0;
        int eyear = 0;

        smonth = Convert.ToInt32(drpSMonth.SelectedItem.Text);
        syear = Convert.ToInt32(drpSYear.SelectedItem.Text);
        emonth = Convert.ToInt32(drpEMonth.SelectedItem.Text);
        eyear = Convert.ToInt32(drpEYear.SelectedItem.Text);


        rpt = new CustomerReportBL.ReportClass();
        DataSet ds = rpt.MonthlyComparisonReport(smonth, syear, emonth, eyear, sDataSource);
        return ds;
    }
}

