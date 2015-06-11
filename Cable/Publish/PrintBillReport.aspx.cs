using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

public partial class PrintBillReport : System.Web.UI.Page
{
    public Double amt = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        printPreview();
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
           
           
            gvBillDetails.DataSource = ds;
            gvBillDetails.DataBind();
        }
        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString();
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }
        lblDate.Text = DateTime.Now.ToShortDateString();
        deleteFile();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Session["dataSet"] != null)
        {
            Response.Redirect("BillDetailReport.aspx"); 
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void gvBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            amt = amt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "monthlyCharge"));
        }
        lblAmount.Text = amt.ToString();
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
