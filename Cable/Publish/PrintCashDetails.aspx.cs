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
using System.IO;

public partial class PrintCashDetails : System.Web.UI.Page
{
    public Double amt = 0.0;
    public Double amtInst = 0.0;
    public Double amtReInst = 0.0;

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
        DataSet ds = new DataSet();

        if (Session["dataSet"] != null)
        {
            sFilename = Server.MapPath(Session["dataSet"].ToString());
            if (File.Exists(sFilename))
            {
                ds.ReadXml(sFilename, XmlReadMode.InferSchema);
                ViewState["filename"] = ds;
            }
            else
            {
                ds = (DataSet)ViewState["filename"];
            }
            if (ds != null)
            {
                gvCashDetails.DataSource = ds;
                gvCashDetails.DataBind();
            }

        }
        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString(); 
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }
        //lblDate.Text = DateTime.Now.ToShortDateString();
        deleteFile();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        deleteFile();
        if (Session["dataSet"] != null)
        {
          
                Response.Redirect("CashDetailsReport.aspx");
           
            if (Session["dataSet"].ToString() == "BillDetails.xml")
            {
                Response.Redirect("BillDetailsReport.aspx");
            }
           
            if (Session["dataSet"].ToString() == "AdjustmentDetails.xml")
            {
                Response.Redirect("AdjustmentDetailsReport.aspx");
            }
            if (Session["dataSet"].ToString() == "OutstandingBalance.xml")
            {
                Response.Redirect("OutstandingBalance.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void gvCashDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //amt = amt + 50;// Convert.ToDouble(e.Row.Cells[6].Text);
        if( e.Row.RowType == DataControlRowType.DataRow )
        {
            if (DataBinder.Eval(e.Row.DataItem, "CashType").ToString() == "CASH")
                amt = amt + Convert.ToDouble( DataBinder.Eval( e.Row.DataItem , "Amount" ) );

            if (DataBinder.Eval(e.Row.DataItem, "CashType").ToString() == "INST")
                amtInst = amtInst + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));

            if (DataBinder.Eval(e.Row.DataItem, "CashType").ToString() == "REINST")
                amtReInst = amtReInst + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            
        }

        lblSubAmount.Text = amt.ToString();
        lblInstAmount.Text = amtInst.ToString();
        lblReInstAmount.Text = amtReInst.ToString();
        lblAmount.Text = (amt + amtInst + amtReInst ).ToString();
    }
    private void deleteFile()
    {
        if (Session["dataSet"] != null)
        {
            string delFilename = Server.MapPath(Session["dataSet"].ToString());
            if (File.Exists(delFilename))
                File.Delete(delFilename);
        }
    }
}
