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
using System.IO;

public partial class PrintOutstandingReport : System.Web.UI.Page
{
    public Double ovrBal = 0.0;
    public Double thisMonth = 0.0;
    public Double thisPayment = 0.0;
    public Double thisBal = 0.0;
    public Double thisAdj = 0.0;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        printPreview();
        if (hdToDelete.Value == "BrowserClose")
        {
            deleteFile();
        }
    }
    protected void printPreview()
    {
        string sFilename = string.Empty;

        if (Session["OutStandingRptData"] != null)
        {
            ds = new DataSet();
            //sFilename = Session["dataSet"].ToString();
            //if (File.Exists(sFilename))
            //{
            //    ds = (DataSet)Session["OutStandingRptData"];
            //    ViewState["filename"] = ds;
            //}
            //else
            //{
            //    ds = (DataSet)ViewState["filename"];
            //}

            ds = (DataSet)Session["OutStandingRptData"];
            ViewState["filename"] = ds;

            if (ds != null)
            {
                gvOutsDetails.DataSource = ds;
                gvOutsDetails.DataBind();
            }
        }
        if (Session["month"] != null)
        {
            lblMonth.Text = Session["month"].ToString();
        }
        if (Session["year"] != null)
        {
            lblYear.Text = Session["year"].ToString();
        }
        lblDate.Text = DateTime.Now.ToShortDateString();
        deleteFile();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        deleteFile();

        Response.Redirect("OutstandingBalance.aspx");  

        //if (Session["dataSet"] != null)
        //{         
        //        //Response.Redirect("OutstandingBalance.aspx");  
        //}
        //else
        //{
        //    Response.Redirect("Default.aspx");
        //}
    }

    protected void gvOutsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //amt = amt + 50;// Convert.ToDouble(e.Row.Cells[6].Text);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            thisMonth = thisMonth + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CurrentBill"));
            thisPayment = thisPayment + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CashDetailAmt"));
            thisBal = thisBal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ThisBalance"));
            thisAdj = thisAdj + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AdjustmentAmt"));
            ovrBal = ovrBal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OverallBalance"));   
        }
        lblAdj.Text = thisAdj.ToString();
        lblBalance.Text  = thisBal.ToString();
        lblCurrent.Text = thisMonth.ToString();
        lblPayment.Text = thisPayment.ToString();
        lblOverall.Text = ovrBal.ToString();
    }

    private void deleteFile()
    {
        if (Session["dataSet"] != null)
        {
            string delFilename = Session["dataSet"].ToString();
            if (File.Exists(delFilename))
                File.Delete(delFilename);
        }
    }

}
