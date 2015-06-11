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

public partial class BillDetailReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;
        if (!Page.IsPostBack)
        {
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");
            hdFilename.Value = "Reports\\" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["BillDetailsFileName"].ToString();

            srcArea.ConnectionString = connStr;

            if(Page.Request.Cookies["StartDate"] != null)
            {
                txtStartDate.Text = Page.Request.Cookies["StartDate"].Value;
                txtEndDate.Text = Page.Request.Cookies["EndDate"].Value;
            }
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Page.Response.Cookies.Add(new HttpCookie("StartDate", txtStartDate.Text));
        Page.Response.Cookies.Add(new HttpCookie("EndDate", txtEndDate.Text));

        GenerateDs();
        Session["dataset"] = hdFilename.Value;
        Session["startDate"] = txtStartDate.Text;
        Session["endDate"] = txtEndDate.Text;
        Session["Filename"] = hdFilename.Value;
        Response.Redirect("PrintBillReport.aspx");
    }

    public void GenerateDs()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            sDataSource = connStr.Remove(0, 45);
        }
        else
        {
            deleteFile();
            Response.Redirect("~/Login.aspx");
        }
        string sArea = string.Empty;
       
        string sXmlPath = Server.MapPath(hdFilename.Value);
        string sXmlNodeName = "BillDetails";

        DateTime startDate, endDate;
        CustomerReportBL.ReportClass custReport;

        sArea = drpArea.SelectedItem.Text.Trim();

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        custReport = new CustomerReportBL.ReportClass();
        custReport.generateBillDetailsReport(sArea.Replace("'","''"), startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);
        DataSet ds = new DataSet();
        ds.ReadXml(sXmlPath, XmlReadMode.InferSchema);
        //return ds;

    }
    private void deleteFile()
    {
        if (Session["Filename"] != null)
        {
            string delFilename = Session["Filename"].ToString();
            if (File.Exists(Server.MapPath(delFilename)))
                File.Delete(Server.MapPath(delFilename));
        }
    }
}
